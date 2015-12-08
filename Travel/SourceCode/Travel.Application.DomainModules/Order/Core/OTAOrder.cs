using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Travel.Application.DomainModules.Order.Core
{
    using System.Globalization;
    using System.IO;
    using System.Transactions;

    using Travel.Application.DomainModules.Order.Core.Interface;
    using Travel.Application.DomainModules.Order.Entity;
    using Travel.Infrastructure.DomainDataAccess.Migrations;
    using Travel.Infrastructure.DomainDataAccess.Order;
    using Travel.Infrastructure.OTAWebService;
    using Travel.Infrastructure.OTAWebService.Response;
    using Travel.Infrastructure.WeiXin.Advanced.Pay;
    using Travel.Infrastructure.WeiXin.Advanced.Pay.Model;
    using Travel.Infrastructure.WeiXin.Log;

    public class OTAOrder : Order
    {
        public string PaymentType;

        public string PrepayId;

        public UnifiedOrderResult UnifiedOrderResult;

        public OTAOrder(OrderRequestEntity orderRequest)
            : base(orderRequest)
        {
            this._orderOperate = new OTAOrderOperate(this);
            this.PreValidate += OTAOrder_PreValidate;
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

        void OTAOrder_PreValidate(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 锁定当日可售票，并更改票的状态
        /// </summary>
        private void OTAOrder_OnPreCreateOrder(object sender, EventArgs eventArgs)
        {
            this.dailyProducts = DailyProductEntity.DailyProduct;
        }

        /// <summary>
        /// 锁定景区票务系统的订单（票）,并修改本地订单和票的状态
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OTAOrder_CreateOrderComplete(object sender, EventArgs e)
        {
            var oatLockOrderResult = this._orderOperate.OrderOccupies();

            LogHelper.WriteOperateLog("锁定景区门票", 
                this.OrderObj.OrderCode, 
                this.OrderObj.OrderId.ToString(), 
                "OrderOccupies", 
                oatLockOrderResult.IsTrue, 
                oatLockOrderResult.IsTrue ? string.Empty : oatLockOrderResult.ResultCode, 
                oatLockOrderResult.IsTrue ? string.Empty : oatLockOrderResult.ResultMsg);


            if (oatLockOrderResult.IsTrue)
            {
                // 修改订单状态为待支付，票的状态为待支付
                OrderObj.OrderStatus = OrderStatus.OrderStatus_WaitPay;
                foreach (var ticket in OrderObj.Tickets)
                {
                    ticket.TicketStatus = OrderStatus.TicketStatus_WaitPay;
                    ticket.LatestModifyTime = DateTime.Now;
                }

                OrderObj.ModifyOrder();
            }
            else
            {
                throw new OrderOperateFailException("锁定订单错误", OrderOperationStep.OrderOccupy, "RESULT_FAIL");
            }
        }

        private void OTAOrder_InvokePaymentService(object sender, EventArgs e)
        {
            var paymentOperate = new WXPaymentOperate();

            var result = paymentOperate.ConnectedPaymentPlatform(this.OrderObj);

            LogHelper.WriteOperateLog("微信支付统一下单",
                this.OrderObj.OrderCode,
                this.OrderObj.OrderId.ToString(),
                "ConnectedPaymentPlatform",
                result.result_code.ToUpper().Equals("SUCCESS") && result.return_code.ToUpper().Equals("SUCCESS"),
                result.result_code.ToUpper().Equals("SUCCESS") && result.return_code.ToUpper().Equals("SUCCESS") ? string.Empty : result.err_code,
                result.result_code.ToUpper().Equals("SUCCESS") && result.return_code.ToUpper().Equals("SUCCESS") ? string.Empty : result.err_code_des);

            if (this.CheckUnifiedOrderResponse(result))
            {
                this.PaymentType = result.trade_type;
                this.PrepayId = result.prepay_id;
                this.UnifiedOrderResult = result;
            }
            else
            {
                throw new OrderPaymentFailException("统一下单操作失败", OrderPaymentStep.UnifiedOrder, "RETURN_NULL");
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
                        throw new OrderPaymentFailException("统一下单操作失败", OrderPaymentStep.UnifiedOrder, "RESULT_FAIL");
                    }
                }
                else
                {
                    throw new OrderPaymentFailException("统一下单操作失败", OrderPaymentStep.UnifiedOrder, "RETURN_FAIL");
                }
            }

            return false;
        }


        #region 退票请求处理

        /// <summary>
        /// 退票请求处理
        /// </summary>
        /// <param name="tickets"></param>
        protected override void ProcessRefund(ICollection<TicketEntity> tickets)
        {
            var editResult = this._orderOperate.ChangeOrderEdit(tickets);
            var failedTickets = new List<TicketEntity>();

            LogHelper.WriteOperateLog("退票处理",
                this.OrderObj.OrderCode,
                this.OrderObj.OrderId.ToString(),
                "ChangeOrderEdit",
                editResult.IsTrue,
                editResult.IsTrue ? string.Empty : editResult.ResultCode,
                editResult.IsTrue ? string.Empty : editResult.ResultMsg);

            if (editResult.IsTrue)
            {
                foreach (var ticketResponse in editResult.ResultData)
                {
                    if (ticketResponse.IsSuccel)
                    {
                        var ticket = tickets.FirstOrDefault(item => item.ECode.Equals(ticketResponse.ProductCode));

                        if (ticket != null)
                        {
                            ticket.TicketStatus = OrderStatus.TicketStatus_Refund_Audit;
                            ticket.LatestModifyTime = DateTime.Now;
                        }
                    }
                    else
                    {
                        var failTicket = tickets.FirstOrDefault(item => item.ECode.Equals(ticketResponse.ProductCode));

                        if (failTicket != null)
                        {
                            failedTickets.Add(failTicket);
                        }
                    }
                }

                // 将状态发生修改的票回写数据库
                TicketEntity.ModifyTickets(tickets.Where(item => item.TicketStatus.Equals(OrderStatus.TicketStatus_Refund_Audit)).ToList());

                // todo: 对更改状态失败的票进行操作
            }
            else
            {
                throw new OrderOperateFailException(editResult.ResultMsg, OrderOperationStep.OrderChange, "REFUND_RETURN_FAIL");
            }
        }

        #endregion

        #region 退款处理

        void OTAOrder_RefundPayComplete(object sender, EventArgs e)
        {
            var eventArg = e as RefundEventArgs;
            RefundOrderResponse refundResponse = null;
            if (eventArg != null)
            {
                refundResponse = this.RefundPay(eventArg.refundOrder);

                LogHelper.WriteOperateLog("微信退款",
                    this.OrderObj.OrderCode,
                    this.OrderObj.OrderId.ToString(),
                    "RefundPay",
                    refundResponse.result_code.ToUpper().Equals("SUCCESS") && refundResponse.return_code.ToUpper().Equals("SUCCESS"),
                    refundResponse.result_code.ToUpper().Equals("SUCCESS") && refundResponse.return_code.ToUpper().Equals("SUCCESS") ? string.Empty : refundResponse.err_code,
                    refundResponse.result_code.ToUpper().Equals("SUCCESS") && refundResponse.return_code.ToUpper().Equals("SUCCESS") ? string.Empty : refundResponse.err_code_des);

                if (refundResponse.return_code.Equals("SUCCESS")
                    && refundResponse.result_code.Equals("SUCCESS"))
                {
                    foreach (var ticket in eventArg.tickets)
                    {
                        ticket.TicketStatus = OrderStatus.TicketStatus_Refund_WaitRefundFee;
                        ticket.LatestModifyTime = DateTime.Now;
                    }

                    eventArg.refundOrder.WXRefundOrderCode = refundResponse.refund_id;
                    eventArg.refundOrder.RefundStatus = OrderStatus.RefundOrderStatus_WaitRefundFee;
                    eventArg.refundOrder.LatestModifyTime = DateTime.Now;

                    using (var scope = new TransactionScope())
                    {
                        TicketEntity.ModifyTickets(eventArg.tickets);
                        eventArg.refundOrder.Modify();

                        scope.Complete();
                    }
                }
                else
                {
                    throw new OrderPaymentFailException(refundResponse.return_msg, OrderPaymentStep.ApplyRefund, "RESULT_FAIL");
                }
            }
        }

        public RefundOrderResponse RefundPay(RefundOrderEntity refundOrder)
        {
            var refundRequest = new RefundOrderRequest();
            var refundTickets =
                    TicketEntity.GetTicketsByOrderId(this.OrderObj.OrderId)
                        .Where(item => item.RefundOrderId.Equals(refundOrder.RefundOrderId)).ToList();
            var refundFee = refundTickets.Sum(item => item.Price);

            refundRequest.transaction_id = this.OrderObj.WXOrderCode;
            refundRequest.out_refund_no = refundOrder.RefundOrderCode;
            refundRequest.total_fee = decimal.ToInt32(this.OrderObj.TotalFee() * 100);
            refundRequest.refund_fee = decimal.ToInt32(refundFee * 100);

            // todo: 处理返回值 
            return this._paymentOperate.RefundPay(refundRequest);
        }

        public static string RefundQuery(Guid refundOrderId)
        {
            var refundOrder = RefundOrderEntity.GetRefundOrder(refundOrderId);

            if (refundOrder != null)
            {
                var queryResponse = new WXPaymentOperate().RefundQuery(refundOrder.RefundOrderCode);

                LogHelper.WriteOperateLog("微信退款查询",
                    refundOrder.RefundOrderCode,
                    refundOrderId.ToString(),
                    "RefundQuery",
                    queryResponse.IsSuccess,
                    queryResponse.IsSuccess ? string.Empty : queryResponse.err_code,
                    queryResponse.IsSuccess ? string.Empty : queryResponse.err_code_des);

                if (queryResponse.IsSuccess)
                {
                    var queryResult = queryResponse.RefundInfoList.FirstOrDefault(item => item.out_refund_no.Equals(refundOrder.RefundOrderCode));
                    if (queryResult != null)
                    {
                        return queryResult.refund_status.ToUpper();
                    }
                }
            }

            return string.Empty;
        }

        #endregion
    }
}
