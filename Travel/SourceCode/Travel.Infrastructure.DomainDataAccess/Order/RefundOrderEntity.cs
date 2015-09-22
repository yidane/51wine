using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Travel.Infrastructure.DomainDataAccess.Order
{
    using System.ComponentModel.DataAnnotations;
    using System.Data.Entity;

    public class RefundOrderEntity
    {
        [Key]
        public Guid RefundOrderId { get; set; }

        [Required]
        public Guid OrderId { get; set; }

        [Required]
        public string RefundOrderCode { get; set; }

        [Required(AllowEmptyStrings = true)]
        public string WXRefundOrderCode { get; set; }

        [Required]
        public DateTime CreateTime { get; set; }

        [Required]
        public string RefundStatus { get; set; }

        [Required]
        public DateTime LatestModifyTime { get; set; }

        [Required(AllowEmptyStrings = true)]
        public string OperatorName { get; set; }

        public void Add()
        {
            using (var db = new TravelDBContext())
            {
                db.RefundOrder.Add(this);
                db.SaveChanges();
            }
        }

        public void Modify()
        {
            using (var db = new TravelDBContext())
            {
                db.RefundOrder.Attach(this);
                db.Entry(this).State = EntityState.Modified;
                db.SaveChanges();
            }
        }

        public void Delete()
        {
            using (var db = new TravelDBContext())
            {
                db.RefundOrder.Attach(this);
                db.Entry(this).State = EntityState.Deleted;
                //db.RefundOrder.Remove(this);
                db.SaveChanges();
            }
        }

        public static RefundOrderEntity GetRefundOrder(Guid refundOrderId)
        {
            using (var db = new TravelDBContext())
            {
                return db.RefundOrder.FirstOrDefault(item => item.RefundOrderId.Equals(refundOrderId));
            }
        }
    }
}
