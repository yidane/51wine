using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Travel.Infrastructure.DomainDataAccess.Order
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Migrations;
    using System.Data.Entity.Migrations.Model;
    using System.Runtime.Remoting.Contexts;

    public class DateTicketEntity
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column(Order = 1)]
        public int DateTicketId { get; set; }

        [MaxLength(50), Required(AllowEmptyStrings = false)]
        public string TicketCode { get; set; }

        /// <summary>
        /// 门票包Id
        /// </summary>
        public int TicketPackageId { get; set; }

        /// <summary>
        /// 门票名称
        /// </summary>
        [MaxLength(50), Required(AllowEmptyStrings = false)]
        public string TicketName { get; set; }

        public decimal TicketPrice { get; set; }

        /// <summary>
        /// 门票类型
        /// </summary>
        [MaxLength(20), Required(AllowEmptyStrings = true)]
        public string TicketType { get; set; }

        /// <summary>
        /// 查询日期
        /// </summary>
        [Key, Required(AllowEmptyStrings = false)]
        [Column(Order = 2)]
        public DateTime SearchDateTime { get; set; }

        /// <summary>
        /// 当前状态
        /// </summary>
        [ConcurrencyCheck]
        [MaxLength(20), Required(AllowEmptyStrings = false)]
        public string CurrentStatus { get; set; }

        /// <summary>
        /// 最新状态的更新时间
        /// </summary>
        [Required(AllowEmptyStrings = false)]
        public DateTime LatestStatusModifyTime { get; set; }


        public static DateTicketEntity GetDateTicketByTicketId(int ticketId, DateTime date)
        {
            using (var db = new TravelDBContext())
            {
                var now = DateTime.Now.Date;
                var tomorrow = now.AddDays(1);
                return db.DateTicket.FirstOrDefault(item => item.DateTicketId.Equals(ticketId) 
                    && item.SearchDateTime >= now
                    && item.SearchDateTime <tomorrow);
            }
        }

        public static IList<DateTicketEntity> GetTodayTicketByNomber(int nomber, string ticketName)
        {
            var now = DateTime.Now.Date;
            var tomorrow = now.AddDays(1);

            // todo: 修改订票方式
            //using (var db = new TravelDBContext())
            //{
            //    return db.DateTicket.Where(
            //            item =>
            //                item.SearchDateTime >= now
            //                && item.SearchDateTime < tomorrow
            //                && item.CurrentStatus.Equals(OrderStatus.DateTicketStatus_Init)
            //                && item.TicketName.Equals(ticketName))
            //        .OrderBy(item => item.DateTicketId)
            //        .Take(nomber).ToList();
            //}

            using (var db = new TravelDBContext())
            {
                return db.DateTicket.Where(
                        item =>
                            item.SearchDateTime >= now
                            && item.SearchDateTime < tomorrow
                            && item.TicketName.Equals(ticketName)).ToList();
            }
        }

        public static void Update(IList<DateTicketEntity> dateTickets)
        {            
            using (var db = new TravelDBContext())
            {
                db.Set<DateTicketEntity>().AddOrUpdate<DateTicketEntity>(dateTickets.ToArray());
                db.SaveChanges();
            }
        }

        public static bool IsDateTicketExists(DateTime date)
        {
            var now = date.Date;
            var tomorrow = now.AddDays(1);
            using (var db = new TravelDBContext())
            {

                return db.Set<DateTicketEntity>().Any(item => item.SearchDateTime >= now
                    && item.SearchDateTime < tomorrow);
            }
        }

        public static void SetDateTickets(IEnumerable<DateTicketEntity> dateTickets)
        {            
            using (var db = new TravelDBContext())
            {
                db.Set<DateTicketEntity>().AddRange(dateTickets);
                db.SaveChanges();
            }
        }
    }
}
