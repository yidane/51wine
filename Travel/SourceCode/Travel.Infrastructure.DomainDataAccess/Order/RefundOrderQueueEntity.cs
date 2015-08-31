using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Travel.Infrastructure.DomainDataAccess.Order
{
    using System.ComponentModel.DataAnnotations;

    using Travel.Infrastructure.DomainDataAccess.Migrations;

    public class RefundOrderQueueEntity
    {
        [Key]
        public Guid QueueId { get; set; }

        public Guid OrderId { get; set; }

        public string ECode { get; set; }

        public DateTime CreateTime { get; set; }

        public string RefundQueueStatus { get; set; }

        public static ICollection<RefundOrderQueueEntity> RefundOrderQueue
        {
            get
            {
                using (var db = new TravelDBContext())
                {
                    return db.RefundOrderQueue.ToList();
                }
            }
        }

        public static void AddRefundOrders(ICollection<RefundOrderQueueEntity> refundOrders)
        {
            using (var db = new TravelDBContext())
            {
                db.RefundOrderQueue.AddRange(refundOrders);
                db.SaveChanges();
            }
        }
    }
}
