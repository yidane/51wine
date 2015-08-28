using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace Travel.Infrastructure.DomainDataAccess.Coupon.Entitys
{
    public class CouponType
    {
        [Key]
        public Guid CouponTypeId { get; set; }
        public string CouponTypeName { get; set; }
        public virtual ICollection<Coupon> Coupon { get; set; }
    }
}
