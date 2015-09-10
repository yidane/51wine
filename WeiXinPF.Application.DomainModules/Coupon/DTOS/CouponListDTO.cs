using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WeiXinPF.Application.DomainModules.Coupon.DTOS
{
   public  class CouponListDTO
    {
        public CouponBaseInfoDTO BaseInfo { get; set; }
        public List<CouponPrizeDTO> ExpiredCoupons { get; set; }

        public List<CouponPrizeDTO> UnExpiredCoupons { get; set; }
        public List<CouponPrizeDTO> UsedCoupons { get; internal set; }

        public CouponListDTO() {
            ExpiredCoupons = new List<CouponPrizeDTO>();
            UnExpiredCoupons = new List<CouponPrizeDTO>();
            UsedCoupons = new List<CouponPrizeDTO>();
        }
    }
}
