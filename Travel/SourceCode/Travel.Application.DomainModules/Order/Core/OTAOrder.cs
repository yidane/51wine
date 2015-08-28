using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Travel.Application.DomainModules.Order.Core
{
    using Travel.Application.DomainModules.Order.Core.Interface;
    using Travel.Application.DomainModules.Order.Entity;
    using Travel.Infrastructure.DomainDataAccess.Order;
    using Travel.Infrastructure.WeiXin.Advanced.Pay.Model;

    public class OTAOrder : Order
    {
        public string PaymentType;

        public string PrepayId;

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
                            item.CurrentStatus = DateTicketStatus_Lock;
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

            if (oatLockOrderResult)
            {
                // 修改订单状态为待支付，票的状态为待支付
                OrderObj.OrderStatus = OrderStatus_WaitPay;
                foreach (var ticket in OrderObj.Tickets)
                {
                    ticket.TicketStatus = TicketStatus_WaitPay;
                }

                OrderObj.ModifyOrder();
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
    }
}
