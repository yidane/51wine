namespace Travel.Infrastructure.DomainDataAccess.Order
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Diagnostics;
    using System.Linq;

    using Travel.Infrastructure.DomainDataAccess.Migrations;

    public class TicketEntity
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int TicketId { get; set; }

        public Guid OrderId { get; set; }

        [ForeignKey("OrderId")]
        public virtual OrderEntity Order { get; set; }

        public Guid? RefundOrderId { get; set; }

        public Guid? RefundOrderDetailId { get; set; }

        [Required]
        public Guid OrderDetailId { get; set; }

        /// <summary>
        /// 票种Id
        /// </summary>
        public Guid TicketCategoryId { get; set; }

        /// <summary>
        /// 票编号
        /// </summary>
        [Required, MaxLength(50)]
        public string TicketCode { get; set; }

        [Required]
        public decimal Price { get; set; }

        /// <summary>
        /// 票状态
        /// </summary>
        [Required, MaxLength(20)]
        public string TicketStatus { get; set; }

        /// <summary>
        /// 电子码
        /// </summary>
        [MaxLength(50)]
        public string ECode { get; set; }

        public DateTime CreateTime { get; set; }

        public DateTime LatestModifyTime { get; set; }

        /// <summary>
        /// 游玩开始时间
        /// </summary>
        public DateTime TicketStartTime { get; set; }

        /// <summary>
        /// 游玩结束时间
        /// </summary>
        public DateTime TicketEndTime { get; set; }

        public void ModifyTicket()
        {
            using (var db = new TravelDBContext())
            {
                db.Ticket.Attach(this);
                db.Entry(this).State = EntityState.Modified;
                db.SaveChanges();
            }
        }

        public static void ModifyTickets(ICollection<TicketEntity> tickets)
        {
            using (var db = new TravelDBContext())
            {
                db.Ticket.AddOrUpdate(tickets.ToArray());
                db.SaveChanges();
            }
        }

        public static ICollection<TicketEntity> GetTicketsByOrderId(Guid orderId)
        {
            using (var db = new TravelDBContext())
            {
                return db.Ticket.Where(item => item.OrderId.Equals(orderId)).ToList();
            }
        }
    }
}
