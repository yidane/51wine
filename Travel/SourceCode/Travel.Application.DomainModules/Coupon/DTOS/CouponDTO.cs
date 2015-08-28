using System;
using Travel.Infrastructure.DomainDataAccess.Coupon.Entitys;

namespace Travel.Application.DomainModules.Coupon.DTOS
{
    public class CouponDTO
    {
        

        public Guid couponId { get; set; }
        //public string openId { get; set; }

        public string getTime { get; set; }
       
        public string title { get; set; }
        public string subTitle { get; set; }

        public string beginTime { get; set; }

        public string endTime { get; set; }

        public string couponType { get; set; }

        public CouponState status { get; set; }
        public string usedTime { get; set; }

    }
}