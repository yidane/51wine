using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Travel.Application.DomainModules.Order.Core
{
    using System.Globalization;

    using Travel.Application.DomainModules.Order.Core.Interface;
    using Travel.Application.DomainModules.Order.Entity;
    using Travel.Infrastructure.DomainDataAccess.Order;
    using Travel.Infrastructure.OTAWebService.Response;
    using Travel.Infrastructure.WeiXin.Advanced.Pay.Model;

    public class OTAOrder : Order
    {
        public string PaymentType;

        public string PrepayId;

        public UnifiedOrderResult UnifiedOrderResult;

        public OTAOrder(OrderRequestEntity orderRequest)
            : base(orderRequest)
        {
            this._orderOperate = new OTAOrderOperate(this);
            this.PreCreateOrder += this.OTAOrder_OnPreCreateOrder;
            this.CreateOrderComplete += this.OTAOrder_CreateOrderComplete;
            this.CreateOrderComplete += this.OTAOrder_InvokePaymentService;
        }
        public OTAOrder(OrderEntity order)
            : base(order)
        {
            this._orderOperate = new OTAOrderOperate(this);
            this._paymentOperate = new WXPaymentOperate();
            this.RefundPayComplete += this.OTAOrder_RefundPayComplete;
        }        

        /// <summary>
        /// 全局锁变量
        /// </summary>
        private static object ticketLock = new object();

        /// <summary>
        /// 锁定当日可售票，并更改票的状态
        /// </summary>
        private void OTAOrder_OnPreCreateOrder(object sender, EventArgs eventArgs)
        {
            try
            {
                lock (ticketLock)
                {
                    this.DateTicketList = DateTicketEntity.GetTodayTicketByNomber(this.OrderRequest.Count, this.OrderRequest.TicketName);

                    if (this.DateTicketList != null && this.DateTicketList.Any())
                    {
                        foreach (var item in this.DateTicketList)
                        {
                            item.CurrentStatus = OrderStatus.DateTicketStatus_Lock;
                        }

                        DateTicketEntity.Update(this.DateTicketList);
                    }
                    else
                    {
                        throw new ArgumentNullException("dateticketlist");
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// 锁定景区票务系统的订单（票）,并修改本地订单和票的状态
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OTAOrder_CreateOrderComplete(object sender, EventArgs e)
        {
            var oatLockOrderResult = this._orderOperate.OrderOccupies();

            if (oatLockOrderResult.IsTrue)
            {
                // 修改订单状态为待支付，票的状态为待支付
                OrderObj.OrderStatus = OrderStatus.OrderStatus_WaitPay;
                foreach (var ticket in OrderObj.Tickets)
                {
                    ticket.TicketStatus = OrderStatus.TicketStatus_WaitPay;
                }

                OrderObj.ModifyOrder();
            }
            else
            {
                throw new InvalidOperationException("CreateOrderComplete");
            }
        }

        private void OTAOrder_InvokePaymentService(object sender, EventArgs e)
        {
            var paymentOperate = new WXPaymentOperate();

            var result = paymentOperate.ConnectedPaymentPlatform(this.OrderObj);
            if (this.CheckUnifiedOrderResponse(result))
            {
                this.PaymentType = result.trade_type;
                this.PrepayId = result.prepay_id;
                this.UnifiedOrderResult = result;
            }
        }

        private bool CheckUnifiedOrderResponse(UnifiedOrderResult paymentResponse)
        {
            if (paymentResponse != null)
            {
                if (paymentResponse.return_code.ToUpper().Equals("success".ToUpper()))
                {
                    if (paymentResponse.result_code.ToUpper().Equals("success".ToUpper()))
                    {
                        return true;
                    }
                    else
                    {
                        throw new InvalidOperationException("PaymentResponse.Result_Code");
                    }
                }
                else
                {
                    throw new InvalidOperationException("PaymentResponse.Return_Code");
                }
            }

            return false;
        }


        #region 退票请求处理

        private static object refundOrderLock = new object();

        /// <summary>
        /// 退票请求处理
        /// </summary>
        /// <param name="tickets"></param>
        protected override void ProcessRefund(ICollection<TicketEntity> tickets)
        {
            var editResult = this._orderOperate.ChangeOrderEdit(tickets);
            var refundOrders = new List<RefundOrderQueueEntity>();

#if DEBUG
            //editResult.IsTrue = true;
            //editResult.ResultData = new List<ChangeOrderEditResponse>()
            //                          {
            //                              new ChangeOrderEditResponse()
            //                                  {
            //                                      IsSucceed = true,
            //                                      ProductCode = "530267905188"
            //                                  }
            //                          };
#endif

            if (editResult.IsTrue)
            {
                foreach (var ticketResponse in editResult.ResultData)
                {                    
                    if (ticketResponse.IsSuccel)
                    {
                        refundOrders.Add(new RefundOrderQueueEntity()
                                             {
                                                 QueueId = Guid.NewGuid(),
                                                 OrderId = this.OrderObj.OrderId,
                                                 ECode = ticketResponse.ProductCode,
                                                 CreateTime = DateTime.Now,
                                                 RefundQueueStatus = OrderStatus.RefundOrderQueueStatus_Init
                                             });
                    }
                    else
                    {
                        refundOrders.Clear();
                        break;
                    }
                }

                if (refundOrders != null && refundOrders.Any())
                {
                    // 获取退票列表，检查是否有将要提交的退票申请重复的申请，没有则修改票状态，并加入退票队列
                    lock (refundOrderLock)
                    {
                        var queue = RefundOrderQueueEntity.RefundOrderQueue;
                        var isRefundOrderExists = false;

                        foreach (var refundOrder in refundOrders)
                        {
                            if (queue.Any(item => item.ECode.Equals(refundOrder.ECode)))
                            {
                                isRefundOrderExists = true;
                                break;
                            }
                        }

                        if (!isRefundOrderExists)
                        {
                            foreach (var ticket in tickets)
                            {
                                ticket.TicketStatus = OrderStatus.TicketStatus_Refund_Audit;
                                ticket.LatestModifyTime = DateTime.Now;
                            }

                            TicketEntity.ModifyTickets(tickets);
                            RefundOrderQueueEntity.AddRefundOrders(refundOrders);
                        }
                        else
                        {
                            throw new InvalidOperationException("Refund ECode Exists");
                        }
                    }
                }
                else
                {
                    throw new InvalidOperationException("Refund");
                }
            }
            else
            {
                throw new InvalidOperationException("Refund");
            }
        }

        #endregion

        #region 退款处理

        void OTAOrder_RefundPayComplete(object sender, EventArgs e)
        {
            var eventArg = e as RefundEventArgs;
            var refundRequest = new RefundOrderRequest();

            if (eventArg != null)
            {
                var refundFee = TicketEntity.GetTicketsByOrderId(this.OrderObj.OrderId)
                        .Where(item => item.RefundOrderId.Equals(eventArg.refundOrder.RefundOrderId))
                        .Sum(item => item.Price);

                refundRequest.transaction_id = this.OrderObj.WXOrderCode;
                refundRequest.out_trade_no = this.OrderObj.OrderCode;
                refundRequest.out_refund_no = eventArg.refundOrder.RefundOrderCode;
                refundRequest.total_fee = Decimal.ToInt32(this.OrderObj.TotalFee() * 100);
                refundRequest.refund_fee = Decimal.ToInt32(refundFee * 100);

                // todo: 处理返回值 
                var refundResponse = this._paymentOperate.RefundPay(refundRequest);

                if (refundResponse.return_code.Equals("SUCCESS")
                    &&refundResponse.result_code.Equals("SUCCESS"))
                {
                    foreach (var ticket in eventArg.tickets)
                    {
                        ticket.TicketStatus = OrderStatus.TicketStatus_Refund_WaitRefundFee;
                        ticket.LatestModifyTime = DateTime.Now;
                    }

                    TicketEntity.ModifyTickets(eventArg.tickets);
                }
            }
        }

        #endregion
    }
}
