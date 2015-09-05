using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Travel.Infrastructure.DomainDataAccess.Order
{
    using System.ComponentModel.DataAnnotations;
    using System.Data.Entity.Core.Metadata.Edm;
    using System.Data.Entity.Migrations;

    using Travel.Infrastructure.DomainDataAccess.Migrations;

    public class TicketCategoryEntity
    {
        [Key]
        public Guid TicketCategoryId { get; set; }

        /// <summary>
        /// 此类型的票的执行日期（每天获取新的门票类型，并且当天有效）
        /// </summary>
        [Required(AllowEmptyStrings = false)]
        public DateTime ImplementationDate { get; set; }

        /// <summary>
        /// 门票包Id（每个合作商在某个景点只有一个门票包，里面可以包含多种类型的门票）
        /// </summary>
        [Required(AllowEmptyStrings = false)]
        public int TicketPackageId { get; set; }

        [MaxLength(20), Required(AllowEmptyStrings = true)]
        public string TicketType { get; set; }

        /// <summary>
        /// 对票的分类，如门票，车票等
        /// </summary>
        [MaxLength(20),Required(AllowEmptyStrings = false)]
        public string Type { get; set; }

        /// <summary>
        /// 票价
        /// </summary>
        [Required(AllowEmptyStrings = false)]
        public decimal Price { get; set; }

        /// <summary>
        /// 票名称
        /// </summary>
        [MaxLength(50), Required(AllowEmptyStrings = false)]
        public string TicketName { get; set; }

        private static IList<TicketCategoryEntity> _ticketCategory;

        private static DateTime currentDate = DateTime.Now.Date;

        public static IList<TicketCategoryEntity> TodayTicketCategory {
            get
            {
                //if (_ticketCategory == null || !_ticketCategory.Any() || !DateTime.Now.Date.Equals(currentDate))
                //{
                //    currentDate = DateTime.Now.Date;

                    using (var db = new TravelDBContext())
                    {
                        var today = DateTime.Now.Date;
                        var tomorrow = today.AddDays(1);
                        _ticketCategory = db.TicketCategory.Where(item => item.ImplementationDate >= today
                            && item.ImplementationDate < tomorrow).ToList();
                    }
                //}

                return _ticketCategory;
            }
        }

        public static void SetTicketCategory(IEnumerable<TicketCategoryEntity> categories)
        {
            using (var db = new TravelDBContext())
            {
                db.Set<TicketCategoryEntity>().AddRange(categories);
                db.SaveChanges();
            }
        }
    }
}
