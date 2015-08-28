using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Travel.Application.DomainModules.Order.Core
{
    using System.Data;
    using System.Globalization;
    using Travel.Application.DomainModules.Order.Core.Interface;
    using Travel.Application.DomainModules.Order.Entity;
    using Travel.Infrastructure.DomainDataAccess.Order;
    using Travel.Infrastructure.WeiXin.Common.Ticket;

    public class Order
    {
        protected IOrderOperate _orderOperate;

        protected IPaymentOperate _paymentOperate;

        private EventHandlerList _events;

        /// <summary>
        /// 初始订单状态
        /// </summary>
        public const string OrderStatus_Init = "OS20001";
        public const string OrderStatus_WaitPay = "OS20002";
        public const string OrderStatus_PayComplete = "OS20003";
        public const string OrderStatus_WaitRefund = "OS20004";
        public const string OrderStatus_WaitUse = "OS20005";
        public const string DateTicketStatus_Init = "DTS10001";
        public const string DateTicketStatus_Lock = "DTS10002";
        public const string DateTicketStatus_PayComplete = "DTS10003";
        public const string TicketStatus_Init = "TS30001";

        /// <summary>
        /// 待付款
        /// </summary>
        public const string TicketStatus_WaitPay = "TS30006";


        /// <summary>
        /// 已付款
        /// </summary>
        public const string TicketStatus_PayComplete = "TS30002";

        /// <summary>
        /// 待使用
        /// </summary>
        public const string TicketStatus_WaitUse = "TS30003";

        /// <summary>
        /// 待退票
        /// </summary>
        public const string TicketStatus_WaitRefund = "TS30004";

        /// <summary>
        /// 已退票
        /// </summary>
        public const string TicketStatus_Refunded = "TS30005";

        /// <summary>
        /// 购票的详情类型
        /// </summary>
        public const string OrderDetailCategory_Create = "{CE7B7E52-3811-44B6-AF9A-7562E0A773D2}";

        /// <summary>
        /// 退票的详情类型
        /// </summary>
        public const string OrderDetailCategory_Refund = "{2A09F347-480A-4AF3-95AE-480984262DD0}";

        public OrderRequestEntity OrderRequest;

        public OrderEntity OrderObj;

        public IList<DateTicketEntity> DateTicketList;

        internal static readonly object EventPreValidate = new Object();
        internal static readonly object EventPreCreateOrder = new Object();
        internal static readonly object EventCreateOrderComplete = new Object();
        

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
            catch (Exception)
            {
                
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
                    throw new ArgumentNullException("OrderDetail");
                }

                var tickets = this.CreateTicket();

                if (tickets != null && tickets.Any())
                {
                    this.OrderObj.Tickets = tickets;
                }
                else
                {
                    throw new ArgumentNullException("Ticket");
                }
            }
            else
            {
                throw new ArgumentNullException("Order");
            }

            this.OrderObj.AddOrder();
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
                                        TicketCategoryId = ticketCategory.TicketCategoryId,
                                        TicketCode = dateTicket.TicketCode,
                                        TicketStatus = TicketStatus_Init,
                                        ECode = string.Empty,
                                        CreateTime = DateTime.Now,
                                        LatestModifyTime = DateTime.Now,
                                        TicketStartTime = DateTime.Now.AddYears(1),
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
                             CreateTime = DateTime.Now,
                             OpenId = this.OrderRequest.OpenId,
                             ContactPersonName = this.OrderRequest.ContactPersonName,
                             MobilePhoneNumber = this.OrderRequest.MobilePhoneNumber,
                             IdentityCardNumber = this.OrderRequest.IdentityCardNumber,
                             OrderStatus = OrderStatus_Init,
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
                                       OrderDetailCategoryId = Guid.Parse(OrderDetailCategory_Create),
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
    }
}
