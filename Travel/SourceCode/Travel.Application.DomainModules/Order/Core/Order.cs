using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Travel.Application.DomainModules.Order.Core
{
    using System.Collections.Specialized;
    using System.Data;
    using System.Globalization;
    using System.Transactions;

    using Travel.Application.DomainModules.Order.Core.Interface;
    using Travel.Application.DomainModules.Order.Entity;
    using Travel.Infrastructure.DomainDataAccess.Order;
    using Travel.Infrastructure.WeiXin.Common.Ticket;

    public class Order
    {
        protected IOrderOperate _orderOperate;

        protected IPaymentOperate _paymentOperate;

        private EventHandlerList _events;

        public OrderRequestEntity OrderRequest;

        public OrderEntity OrderObj;

        public IList<DateTicketEntity> DateTicketList;

        internal static readonly object EventPreValidate = new Object();
        internal static readonly object EventPreCreateOrder = new Object();
        internal static readonly object EventCreateOrderComplete = new Object();
        internal static readonly object EvnetRefundPayComplete = new object();

        public Order(OrderRequestEntity orderRequest)
        {
            this.OrderRequest = orderRequest;
        }

        public Order(OrderEntity order)
        {
            this.OrderObj = order;
        }

        #region Events
        /// <summary>
        /// Gets the events.
        /// </summary>
        protected EventHandlerList Events
        {
            get
            {
                return this._events ?? (this._events = new EventHandlerList());
            }
        }


        public event EventHandler PreValidate
        {
            add
            {
                this.Events.AddHandler(EventPreValidate, value);
            }

            remove
            {
                this.Events.RemoveHandler(EventPreValidate, value);
            }
        }

        public event EventHandler PreCreateOrder
        {
            add
            {
                this.Events.AddHandler(EventPreCreateOrder, value);
            }

            remove
            {
                this.Events.RemoveHandler(EventPreCreateOrder, value);
            }
        }

        public event EventHandler CreateOrderComplete
        {
            add
            {
                this.Events.AddHandler(EventCreateOrderComplete, value);
            }

            remove
            {
                this.Events.RemoveHandler(EventCreateOrderComplete, value);
            }
        }

        public event EventHandler RefundPayComplete
        {
            add
            {
                this.Events.AddHandler(EvnetRefundPayComplete, value);
            }

            remove
            {
                this.Events.RemoveHandler(EvnetRefundPayComplete, value);
            }
        }

        protected virtual void OnPreValidate(EventArgs e)
        {
            var handler = (EventHandler)this.Events[EventPreValidate];
            if (handler != null)
            {
                handler(this, e);
            }
        }

        protected virtual void OnPreCreateOrder(EventArgs e)
        {
            var handler = (EventHandler)this.Events[EventPreCreateOrder];
            if (handler != null)
            {
                handler(this, e);
            }
        }

        protected virtual void OnCreateOrderComplete(EventArgs e)
        {
            var handler = (EventHandler)this.Events[EventCreateOrderComplete];
            if (handler != null)
            {
                handler(this, e);
            }
        }

        public class RefundEventArgs : EventArgs
        {
            public OrderDetailEntity orderDetail { get; set; }

            public RefundOrderEntity refundOrder { get; set; }

            public ICollection<TicketEntity> tickets { get; set; }
        }

        protected virtual void OnRefundPayComplete(EventArgs e)
        {
            var handler = (EventHandler)this.Events[EvnetRefundPayComplete];
            if (handler != null)
            {
                handler(this, e);
            }
        }

        #endregion

        #region 生成订单相关方法

        /// <summary>
        /// 生成订单主方法
        /// </summary>
        public void CreateOrderMain()
        {
            try
            {
                this.OnPreValidate(EventArgs.Empty);
                this.OnPreCreateOrder(EventArgs.Empty);
                this.ConbinedOrderComponent();
                this.OnCreateOrderComplete(EventArgs.Empty);
            }
            catch (OrderOperateFailException orderException)
            {
                this.ProcessOrderOperationException(orderException);
            }
            catch (OrderPaymentFailException orderPaymentException)
            {
                this.ProcessOrderPaymentException(orderPaymentException);
            }
            catch (Exception)
            {
                // todo:针对不同事件报出的异常进行不同的异常处理
                throw;
            }
        }

        /// <summary>
        /// 组合订单各项
        /// </summary>
        protected void ConbinedOrderComponent()
        {
            this.CreateOrder();

            if (this.OrderObj != null)
            {
                var orderDetail = this.CreateOrderDetail();

                if (orderDetail != null)
                {
                    this.OrderObj.OrderDetails = orderDetail;
                }
                else
                {
                    throw new OrderOperateFailException("创建订单明细失败", OrderOperationStep.OrderCreate);
                }

                var tickets = this.CreateTicket();

                if (tickets != null && tickets.Any())
                {
                    this.OrderObj.Tickets = tickets;
                }
                else
                {
                    throw new OrderOperateFailException("创建售出票失败", OrderOperationStep.OrderCreate);
                }
            }
            else
            {
                throw new OrderOperateFailException("创建订单主体失败", OrderOperationStep.OrderCreate);
            }

            try
            {
                this.OrderObj.AddOrder();
            }
            catch (Exception)
            {
                throw new OrderOperateFailException("订单保存失败", OrderOperationStep.OrderCreate, "SAVEDATA_FAIL");
            }
        }

        /// <summary>
        /// 创建用户申请的门票
        /// </summary>
        /// <returns></returns>
        protected IList<TicketEntity> CreateTicket()
        {
            var tickets = new List<TicketEntity>();

            if (this.DateTicketList.Any())
            {
                foreach (var dateTicket in this.DateTicketList)
                {
                    var ticketCategory =
                        TicketCategoryEntity.TodayTicketCategory.FirstOrDefault(
                            item => item.TicketName.Equals(this.OrderRequest.TicketName));

                    if (ticketCategory != null)
                    {
                        tickets.Add(new TicketEntity()
                                    {
                                        TicketId = dateTicket.DateTicketId,
                                        OrderId = this.OrderObj.OrderId,
                                        RefundOrderId = default(Guid?),
                                        RefundOrderDetailId = default(Guid?),
                                        OrderDetailId = this.OrderObj.OrderDetails.First().OrderDetailId,
                                        TicketCategoryId = ticketCategory.TicketCategoryId,
                                        TicketCode = dateTicket.TicketCode,
                                        Price = dateTicket.TicketPrice,
                                        TicketStatus = OrderStatus.TicketStatus_Init,
                                        ECode = string.Empty,
                                        CreateTime = DateTime.Now,
                                        LatestModifyTime = DateTime.Now,
                                        TicketStartTime = DateTime.Now,
                                        TicketEndTime = DateTime.Now.AddYears(1)
                                    });
                    }
                    else
                    {
                        tickets.Clear();
                        break;
                    }
                }
            }

            return tickets;
        }

        /// <summary>
        /// 创建用户订单
        /// </summary>
        protected void CreateOrder()
        {
            this.OrderObj = new OrderEntity()
                         {
                             OrderId = Guid.NewGuid(),
                             OrderCode = this.CreateOrderCode(),
                             WXOrderCode = string.Empty,
                             CreateTime = DateTime.Now,
                             OpenId = this.OrderRequest.OpenId,
                             ContactPersonName = this.OrderRequest.ContactPersonName,
                             MobilePhoneNumber = this.OrderRequest.MobilePhoneNumber,
                             IdentityCardNumber = this.OrderRequest.IdentityCardNumber,
                             OrderStatus = OrderStatus.OrderStatus_Init,
                             HasCoupon = !string.IsNullOrEmpty(this.OrderRequest.CouponId),
                             CouponId = string.IsNullOrEmpty(this.OrderRequest.CouponId) ? default(Guid?) : Guid.Parse(this.OrderRequest.CouponId),
                             OrderDetails = new List<OrderDetailEntity>(),
                             Tickets = new List<TicketEntity>()
                         };
        }

        /// <summary>
        /// 创建用户订单详情
        /// </summary>
        /// <returns></returns>
        protected IList<OrderDetailEntity> CreateOrderDetail()
        {
            var checkedTicketCategory =
                TicketCategoryEntity.TodayTicketCategory.FirstOrDefault(
                    item => item.TicketCategoryId.Equals(Guid.Parse(this.OrderRequest.TicketCategory)));

            if (checkedTicketCategory != null)
            {
                return new List<OrderDetailEntity>()
                           {
                               new OrderDetailEntity()
                                   {
                                       OrderDetailId = Guid.NewGuid(),
                                       OrderId = this.OrderObj.OrderId,
                                       OrderDetailCategoryId = Guid.Parse(OrderStatus.OrderDetailCategory_Create),
                                       TicketCategoryId =
                                           Guid.Parse(
                                               this.OrderRequest
                                           .TicketCategory),
                                       Count = this.OrderRequest.Count,
                                       SingleTicketPrice =
                                           checkedTicketCategory.Price,
                                       IsDiscount = false,
                                       DiscountCategoryId = default(Guid?),
                                       TotalPrice =
                                           checkedTicketCategory.Price
                                           * this.OrderRequest.Count
                                   }
                           };
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 创建订单编码
        /// </summary>
        /// <returns></returns>
        protected string CreateOrderCode()
        {
            return "C" + DateTime.Now.ToString("yyyyMMddHHmmssffff") + this.GetRandom();
        }

        private static Random myRan = new Random();

        private string GetRandom()
        {
            return myRan.Next(1000, 9998).ToString(CultureInfo.InvariantCulture);
        }

        #endregion

        #region 订单完成

        protected virtual void OrderComfirm()
        {
            throw new NotImplementedException();
        }

        #endregion

        #region 退票请求处理

        /// <summary>
        /// 退票请求处理
        /// </summary>
        /// <param name="tickets">此参数必须通过ECode从数据库查询得到</param>
        public void ProcessRefundRequestMain(IList<TicketEntity> tickets)
        {
            try
            {
                if (this.IsRefundRequestCorrect(tickets))
                {
                    this.ProcessRefund(tickets);
                }
            }
            catch (OrderOperateFailException orderException)
            {
                this.ProcessOrderOperationException(orderException);
            }
            
        }

        protected virtual void ProcessRefund(ICollection<TicketEntity> tickets)
        {
            throw new InvalidOperationException();
        }

        /// <summary>
        /// 判断申请退票操作的票的状态是否正确
        /// </summary>
        /// <param name="tickets"></param>
        /// <returns></returns>
        private bool IsRefundRequestCorrect(IEnumerable<TicketEntity> tickets)
        {
            var canTicketRefund = false;

            foreach (var ticket in tickets)
            {
                canTicketRefund = ticket.TicketStatus.Equals(OrderStatus.TicketStatus_WaitUse);

                if (!canTicketRefund)
                {
                    break;
                }
            }

            return canTicketRefund;
        }

        #endregion

        #region 退款处理

        /// <summary>
        /// 处理退款
        /// </summary>
        /// <param name="tickets">所有tickets必须是同一个订单的</param>
        public void ProcessRefundPayment(ICollection<TicketEntity> tickets)
        {
            if (this.OrderObj != null)
            {
                var orderDetail = new OrderDetailEntity()
                                      {
                                          OrderDetailId = Guid.NewGuid(),
                                          OrderDetailCategoryId = Guid.Parse(OrderStatus.OrderDetailCategory_Refund),
                                          OrderId = this.OrderObj.OrderId,
                                          TicketCategoryId = tickets.First().TicketCategoryId,
                                          Count = tickets.Count,
                                          SingleTicketPrice = tickets.First().Price,
                                          IsDiscount = false,
                                          DiscountCategoryId = default(Guid?)
                                      };
                orderDetail.TotalPrice = orderDetail.TotalFee();

                var refundOrder = new RefundOrderEntity()
                                      {
                                          RefundOrderId = Guid.NewGuid(),
                                          OrderId = this.OrderObj.OrderId,
                                          RefundOrderCode = this.CreateRefundOrderCode(),
                                          WXRefundOrderCode = string.Empty,
                                          CreateTime = DateTime.Now,
                                          RefundStatus = OrderStatus.RefundOrderStatus_Init,
                                          LatestModifyTime = DateTime.Now,
                                          OperatorName = "后台管理员"
                                      };

                foreach (var refundTicket in tickets)
                {
                    refundTicket.RefundOrderId = refundOrder.RefundOrderId;
                    refundTicket.RefundOrderDetailId = orderDetail.OrderDetailId;
                    refundTicket.TicketStatus = OrderStatus.TicketStatus_Refund_RefundPayProcessing;
                    refundTicket.LatestModifyTime = DateTime.Now;
                }

                using (var scope = new TransactionScope())
                {
                    refundOrder.Add();
                    orderDetail.Add();
                    TicketEntity.ModifyTickets(tickets);

                    scope.Complete();
                }


                var eventArgs = new RefundEventArgs
                                    {
                                        orderDetail = orderDetail,
                                        refundOrder = refundOrder,
                                        tickets = tickets
                                    };

                try
                {
                    this.OnRefundPayComplete(eventArgs);
                }
                catch (OrderPaymentFailException orderException)
                {
                    orderException.param = new Dictionary<string, object>
                                               {
                                                   { "refundOrder", refundOrder },
                                                   { "orderDetail", orderDetail },
                                                   { "tickets", tickets }
                                               };
                    this.ProcessOrderPaymentException(orderException);
                    //throw;
                }
                catch (Exception)
                {

                    throw;
                }

            }
        }

        protected string CreateRefundOrderCode()
        {
            return "T" + DateTime.Now.ToString("yyyyMMddHHmmssffff") + this.GetRandom();
        }

        #endregion

        #region 异常处理流程

        public void ProcessOrderOperationException(OrderOperateFailException orderExcepion)
        {
            // todo: 第一步，记录异常日志；第二步，异常处理；第三步，记录处理方式
            var needProcess = false;
            var processMethod = string.Empty;
            var isProcessSuccess = true;

            switch (orderExcepion.OperationStep)
            {
                case OrderOperationStep.GetDailyTicket:
                    if (orderExcepion.ErrorCode.Equals("RESULT_NULL"))
                    {
                        OTAOrder.SetDailyTicket();
                        needProcess = true;
                        processMethod = "重新获取景区票务数据";
                    }
                    break;
                case OrderOperationStep.OrderOccupy:
                    this.OrderObj.DeleteOrder();
                    foreach (var dateTicket in this.DateTicketList)
                    {
                        dateTicket.CurrentStatus = OrderStatus.DateTicketStatus_Init;
                        dateTicket.LatestStatusModifyTime = DateTime.Now;
                    }

                    DateTicketEntity.Update(this.DateTicketList);

                    needProcess = true;
                    processMethod = "回滚Order表和DateTicket表中的数据";
                    break;
                case OrderOperationStep.OrderCreate:
                    foreach (var dateTicket in this.DateTicketList)
                    {
                        dateTicket.CurrentStatus = OrderStatus.DateTicketStatus_Init;
                        dateTicket.LatestStatusModifyTime = DateTime.Now;
                    }

                    DateTicketEntity.Update(this.DateTicketList);

                    needProcess = true;
                    processMethod = "回滚锁定DateTicket表中的数据";
                    break;
                case OrderOperationStep.OrderChange:
                    break;
                case OrderOperationStep.GetOrderStatus:
                    break;
                default:
                    break;
            }
        }

        public void ProcessOrderPaymentException(OrderPaymentFailException orderException)
        {
            var needProcess = false;
            var processMethod = string.Empty;
            var isProcessSuccess = true;

            switch (orderException.PaymentStep)
            {
                case OrderPaymentStep.UnifiedOrder:
                    var result = this._orderOperate.OrderRelease();

                    if (result.IsTrue)
                    {
                        this.OrderObj.DeleteOrder();
                        foreach (var dateTicket in this.DateTicketList)
                        {
                            dateTicket.CurrentStatus = OrderStatus.DateTicketStatus_Init;
                            dateTicket.LatestStatusModifyTime = DateTime.Now;
                        }

                        DateTicketEntity.Update(this.DateTicketList);

                        needProcess = true;
                        processMethod = "回滚Order表和DateTicket表中的数据";
                    }
                    else
                    {
                        needProcess = true;
                        processMethod = "统一下单失败，释放订单失败";
                        isProcessSuccess = false;
                    }
                    break;
                case OrderPaymentStep.ApplyRefund:
                    if (orderException.param.ContainsKey("refundOrder")
                        && orderException.param.ContainsKey("orderDetail")
                        && orderException.param.ContainsKey("tickets"))
                    {
                        var refundOrder = orderException.param["refundOrder"] as RefundOrderEntity;
                        var orderDetail = orderException.param["orderDetail"] as OrderDetailEntity;
                        var tickets = orderException.param["tickets"] as ICollection<TicketEntity>;

                        refundOrder.Delete();
                        orderDetail.Delete();
                        foreach (var ticket in tickets)
                        {
                            ticket.TicketStatus = OrderStatus.TicketStatus_Refund_Audit;
                            ticket.LatestModifyTime = DateTime.Now;
                            ticket.RefundOrderId = default(Guid?);
                            ticket.RefundOrderDetailId = default(Guid);
                        }

                        TicketEntity.ModifyTickets(tickets);
                    }
                    
                    break;
                case OrderPaymentStep.PaymentResultNotify:
                    break;
                case OrderPaymentStep.RefundQuery:
                    break;
                default:
                    break;
            }
        }

        #endregion
    }
}
