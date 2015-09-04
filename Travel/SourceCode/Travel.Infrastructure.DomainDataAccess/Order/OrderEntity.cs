namespace Travel.Infrastructure.DomainDataAccess.Order
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Data;
    using System.Data.Common;
    using System.Data.Entity;
    using System.Data.Entity.Core.Metadata.Edm;
    using System.Data.Entity.Migrations;
    using System.Data.SqlClient;
    using System.Linq;

    using EntityState = System.Data.Entity.EntityState;

    public class OrderEntity
    {
        [Key]
        public Guid OrderId { get; set; }

        /// <summary>
        /// 订单号
        /// </summary>
        public string OrderCode { get; set; }

        /// <summary>
        /// 微信订单号
        /// </summary>
        public string WXOrderCode { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 微信账号
        /// </summary>
        public string OpenId { get; set; }

        /// <summary>
        /// 联系人
        /// </summary>
        public string ContactPersonName { get; set; }

        /// <summary>
        /// 手机号
        /// </summary>
        public string MobilePhoneNumber { get; set; }

        /// <summary>
        /// 身份证号
        /// </summary>
        public string IdentityCardNumber { get; set; }

        /// <summary>
        /// 订单状态
        /// </summary>
        public string OrderStatus { get; set; }

        /// <summary>
        /// 是否使用优惠券
        /// </summary>
        public bool HasCoupon { get; set; }

        /// <summary>
        /// 优惠券Id
        /// </summary>
        public Guid? CouponId { get; set; }

        /// <summary>
        /// 订单详情（包含购买和退单详情）
        /// </summary>
        public virtual ICollection<OrderDetailEntity> OrderDetails { get; set; }

        /// <summary>
        /// 票明细
        /// </summary>
        public virtual ICollection<TicketEntity> Tickets { get; set; }

        public void AddOrder()
        {
            using (var db = new TravelDBContext())
            {
                db.Order.Add(this);
                db.SaveChanges();
            }
        }

        public void ModifyOrder()
        {
            using (var db = new TravelDBContext())
            {
                
                db.Order.Attach(this);
                foreach (var ticket in this.Tickets)
                {
                    db.Entry(ticket).State = EntityState.Modified;
                }

                foreach (var detail in this.OrderDetails)
                {
                    db.Entry(detail).State = EntityState.Modified;
                }

                ////db.Order.AddOrUpdate(this, this);
                //db.Entry(this.Tickets).State=EntityState.Modified;
                db.Entry(this).State = EntityState.Modified;
                //db.Entry(this.Tickets).State = EntityState.Modified;
                db.SaveChanges();
            }
        }

        public static OrderEntity GetOrderByOrderCode(string orderCode)
        {
            OrderEntity order = null;

            using (var db = new TravelDBContext())
            {
                order = db.Order.Include(item => item.Tickets)
                    .Include(item => item.OrderDetails)
                    .FirstOrDefault(item => item.OrderCode.Equals(orderCode));
            }

            return order;
        }

        public static OrderEntity GetOrderByOrderId(Guid orderId)
        {
            using (var db = new TravelDBContext())
            {
                return db.Order.Include(item => item.Tickets)
                    .Include(item => item.OrderDetails)
                    .FirstOrDefault(item => item.OrderId.Equals(orderId));
            }
        }

        public decimal GetCategoryTotalFee(Guid orderDetailCategory)
        {
            var orderDetail_Create =
                this.OrderDetails.Where(
                    item => item.OrderDetailCategoryId.Equals(orderDetailCategory));            

            return orderDetail_Create.Sum(detail => detail.TotalPrice);
        }

        public decimal TotalFee()
        {
            var tickets = TicketEntity.GetTicketsByOrderId(this.OrderId);

            return tickets.Sum(item => item.Price);
        }

        public static IList<TicketEntity> GetTicketForSearch(int pageIndex, int pageSize)
        {
            using (var db = new TravelDBContext())
            {
                var param = new List<SqlParameter>()
                                {
                                    new SqlParameter(){ParameterName = "@PageIndex", SqlDbType = SqlDbType.Int, Value = pageIndex},
                                    new SqlParameter(){ParameterName = "@PageSize", SqlDbType = SqlDbType.Int, Value = pageSize}
                                };
                 return db.Database.SqlQuery<TicketEntity>("USP_Ticket_GetTicketForSearchStatus @PageIndex, @PageSize", param.ToArray()).ToList();
            }
        }

        public static IList<OrderEntity> GetMyOrders(string openId)
        {
            using (var db = new TravelDBContext())
            {
                return db.Order.Include(item =>item.Tickets).Include(item => item.OrderDetails).Where(item => item.OpenId.Equals(openId)).ToList();
            }
        }

        public void DeleteOrder()
        {
            using (var db = new TravelDBContext())
            {
                var order = db.Set<OrderEntity>()
                            .Include(item => item.Tickets)
                            .Include(item => item.OrderDetails)
                            .FirstOrDefault(item => item.OrderId.Equals(this.OrderId));

                db.Order.Remove(order);
                db.SaveChanges();
            }
        }
    }
}
