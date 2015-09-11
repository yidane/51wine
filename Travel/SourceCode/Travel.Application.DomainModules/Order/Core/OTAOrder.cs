﻿using System;
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
        /// 锁定当日可售票，并更改票的状态
        /// </summary>
        private void OTAOrder_OnPreCreateOrder(object sender, EventArgs eventArgs)
        {
            this.dailyProducts = DailyProductEntity.DailyProduct.ToList();
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

                // 将状态发生修改的票会写数据库
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
            var refundRequest = new RefundOrderRequest();

            if (eventArg != null)
            {
                var refundFee = TicketEntity.GetTicketsByOrderId(this.OrderObj.OrderId)
                        .Where(item => item.RefundOrderId.Equals(eventArg.refundOrder.RefundOrderId))
                        .Sum(item => item.Price);

                refundRequest.transaction_id = this.OrderObj.WXOrderCode;
                //refundRequest.out_trade_no = this.OrderObj.OrderCode;
                refundRequest.out_refund_no = eventArg.refundOrder.RefundOrderCode;


                refundRequest.total_fee = 2; // Decimal.ToInt32(this.OrderObj.TotalFee() * 100);

                refundRequest.refund_fee = 2; // Decimal.ToInt32(refundFee * 100);


                // todo: 处理返回值 
                var refundResponse = this._paymentOperate.RefundPay(refundRequest);

                WriteLog(refundResponse.return_msg);
                if (refundResponse.return_code.Equals("SUCCESS")
                    && refundResponse.result_code.Equals("SUCCESS"))
                {
                    foreach (var ticket in eventArg.tickets)
                    {
                        ticket.TicketStatus = OrderStatus.TicketStatus_Refund_WaitRefundFee;
                        ticket.LatestModifyTime = DateTime.Now;
                    }

                    TicketEntity.ModifyTickets(eventArg.tickets);
                }
                else
                {
                    throw new OrderPaymentFailException(refundResponse.return_msg, OrderPaymentStep.ApplyRefund, "RESULT_FAIL");
                }
            }
        }

        #endregion

        protected static void WriteLog(string content)
        {
            var path = AppDomain.CurrentDomain.BaseDirectory + "log";
            if (!Directory.Exists(path))//如果日志目录不存在就创建
            {
                Directory.CreateDirectory(path);
            }

            string time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");//获取当前系统时间
            string filename = path + "/" + DateTime.Now.ToString("yyyy-MM-dd") + ".txt";//用日期对日志文件命名

            //创建或打开日志文件，向日志文件末尾追加记录
            StreamWriter mySw = File.AppendText(filename);

            mySw.WriteLine(time + "   |" + content + Environment.NewLine);

            //关闭日志文件
            mySw.Close();
        }
    }
}