using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Travel.Application.DomainModules.Coupon.DTOS
{
   public  class CouponListDTO
    {
        public List<CouponDTO> ExpiredCoupons { get; set; }

        public List<CouponDTO> UnExpiredCoupons { get; set; }
        public List<CouponDTO> UsedCoupons { get; internal set; }

        public CouponListDTO() {
            ExpiredCoupons = new List<CouponDTO>();
            UnExpiredCoupons = new List<CouponDTO>();
            UsedCoupons = new List<CouponDTO>();
        }
    }
}
