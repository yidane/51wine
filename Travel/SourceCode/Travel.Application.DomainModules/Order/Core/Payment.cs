using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Travel.Application.DomainModules.Order.Core
{
    using System.ComponentModel;
    using System.Dynamic;

    using Travel.Application.DomainModules.Order.Core.Interface;
    using Travel.Infrastructure.DomainDataAccess.Order;
    using Travel.Infrastructure.WeiXin.Advanced.Pay.Model;

    public class Payment
    {
        internal PaymentNotify PaymentResponse;

        internal OrderEntity OrderObj;

        internal IPaymentOperate PaymentOperate;

        internal bool IsPaymentInfoProcessed;

        internal bool IsProcessPaymentInfoSuccess;

        internal string WXTurnbackResult;

        public Payment(PaymentNotify paymentResponse)
        {
            // todo: PaymentResponse需要明确的对象及属性
            this.PaymentResponse = paymentResponse;
        }

        public Payment(OrderEntity order)
        {
            this.OrderObj = order;
        }

        #region Events

        private EventHandlerList _events;
        protected EventHandlerList Events
        {
            get
            {
                return this._events ?? (this._events = new EventHandlerList());
            }
        }

        internal static readonly object EventPrePayment = new Object();

        internal static readonly object EventPaymentComplete = new Object();

        internal static readonly object EventAfterPaymentComplete = new object();

        public event EventHandler PrePayment
        {
            add
            {
                this.Events.AddHandler(EventPrePayment, value);
            }

            remove
            {
                this.Events.RemoveHandler(EventPrePayment, value);
            }
        }

        public event EventHandler PaymentComplete
        {
            add
            {
                this.Events.AddHandler(EventPaymentComplete, value);
            }

            remove
            {
                this.Events.RemoveHandler(EventPaymentComplete, value);
            }
        }

        public event EventHandler AfterPaymentComplete
        {
            add
            {
                this.Events.AddHandler(EventAfterPaymentComplete, value);
            }

            remove
            {
                this.Events.RemoveHandler(EventAfterPaymentComplete, value);
            }
        }

        protected virtual void OnPrePayment(EventArgs e)
        {
            var handler = (EventHandler)this.Events[EventPrePayment];
            if (handler != null)
            {
                handler(this, e);
            }
        }

        protected virtual void OnPaymentComplete(EventArgs e)
        {
            var handler = (EventHandler)this.Events[EventPaymentComplete];
            if (handler != null)
            {
                handler(this, e);
            }
        }

        protected virtual void OnAfterPaymentComplete(EventArgs e)
        {
            var handler = (EventHandler)this.Events[EventAfterPaymentComplete];
            if (handler != null)
            {
                handler(this, e);
            }
        }

        #endregion

        /// <summary>
        /// 订单操作锁。在查询或修改订单前，使用锁将表独占
        /// </summary>
        internal static object orderLock = new object();


        public void ProcessPaymentMain()
        {
            try
            {
                if (this.PaymentResponse.return_code.ToUpper().Equals("SUCCESS") 
                    && this.PaymentResponse.result_code.ToUpper().Equals("SUCCESS"))
                {
                    lock (orderLock)
                    {
                        this.OrderObj = OrderEntity.GetOrderByOrderId(this.PaymentResponse.out_trade_no);
                        this.OnPrePayment(EventArgs.Empty);
                        if (!this.IsPaymentInfoProcessed)
                        {
                            this.ProcessPayment();
                        }
                    }
                }           
            }
            catch (Exception)
            {
                this.IsProcessPaymentInfoSuccess = false;
            }

            this.OnPaymentComplete(EventArgs.Empty);


            try
            {
                if (!this.IsPaymentInfoProcessed && this.IsProcessPaymentInfoSuccess)
                {
                    this.OnAfterPaymentComplete(EventArgs.Empty);
                }
            }
            catch (Exception)
            {
                
                throw;
            }
            
        }

        protected void ProcessPayment()
        {
            if (this.OrderObj != null)
            {
                var dateTickets = new List<DateTicketEntity>();

                this.OrderObj.OrderStatus = Order.OrderStatus_PayComplete;
                this.OrderObj.WXOrderCode = this.PaymentResponse.transaction_id;
                foreach (var ticket in this.OrderObj.Tickets)
                {
                    ticket.TicketStatus = Order.TicketStatus_PayComplete;

                    var dateTicket = DateTicketEntity.GetDateTicketByTicketId(ticket.TicketId);
                    if (dateTicket != null)
                    {
                        dateTicket.CurrentStatus = Order.DateTicketStatus_PayComplete;
                        dateTickets.Add(dateTicket);
                    }
                }

                this.OrderObj.ModifyOrder();
                DateTicketEntity.Update(dateTickets);

                this.IsProcessPaymentInfoSuccess = true;
            }
            else
            {
                throw new ArgumentNullException("Order", "订单不存在");
            }
        }
    }
}
