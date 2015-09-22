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
                        this.OrderObj = OrderEntity.GetOrderByOrderCode(this.PaymentResponse.out_trade_no);
                        this.OnPrePayment(EventArgs.Empty);
                        if (!this.IsPaymentInfoProcessed)
                        {
                            this.ProcessPayment();
                        }
                    }
                }           
            }
            catch (Exception ex)
            {
                this.IsProcessPaymentInfoSuccess = false;

                var exLog = new ExceptionLogEntity()
                                {
                                    ExceptionLogId = Guid.NewGuid(),
                                    Module = "微信支付通知",
                                    CreateTime = DateTime.Now,
                                    ExceptionType = ex.GetType().FullName,
                                    ExceptionMessage = ex.Message,
                                    TrackMessage = ex.StackTrace,
                                    HasExceptionProcessing = false,
                                    NeedProcess = false,
                                    ProcessFunction = ex.Source
                                };

                exLog.Add();
            }            

            try
            {
                if (!this.IsPaymentInfoProcessed && this.IsProcessPaymentInfoSuccess)
                {
                    this.OnAfterPaymentComplete(EventArgs.Empty);
                }
            }
            catch (Exception ex)
            {
                this.IsProcessPaymentInfoSuccess = false;

                var exLog = new ExceptionLogEntity()
                {
                    ExceptionLogId = Guid.NewGuid(),
                    Module = "订单结束",
                    CreateTime = DateTime.Now,
                    ExceptionType = ex.GetType().FullName,
                    ExceptionMessage = ex.Message,
                    TrackMessage = ex.StackTrace,
                    HasExceptionProcessing = false,
                    NeedProcess = false,
                    ProcessFunction = ex.Source
                };

                exLog.Add();
            }
            
            this.OnPaymentComplete(EventArgs.Empty);
        }

        protected void ProcessPayment()
        {
            if (this.OrderObj != null)
            {
                this.OrderObj.OrderStatus = OrderStatus.OrderStatus_PayComplete;
                this.OrderObj.WXOrderCode = this.PaymentResponse.transaction_id;
                foreach (var ticket in this.OrderObj.Tickets)
                {
                    ticket.TicketStatus = OrderStatus.TicketStatus_PayComplete;
                    ticket.LatestModifyTime = DateTime.Now;
                }

                this.OrderObj.ModifyOrder();
                this.IsProcessPaymentInfoSuccess = true;
            }
            else
            {
                throw new ArgumentNullException("Order", "订单不存在");
            }
        }
    }
}
