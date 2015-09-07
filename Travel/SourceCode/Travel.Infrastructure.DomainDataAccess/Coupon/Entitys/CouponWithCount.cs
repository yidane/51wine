using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Travel.Infrastructure.DomainDataAccess.Coupon.Entitys
{
   public  class CouponWithCount
    {
        public Coupon Coupon { get; set; }
        public int Count { get; set; }
    }
}
