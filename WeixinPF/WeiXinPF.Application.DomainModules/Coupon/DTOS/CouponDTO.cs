using System; 

namespace WeiXinPF.Application.DomainModules.Coupon.DTOS
{
    /// <summary>
    /// 奖品
    /// </summary>
    public class CouponDTO
    {


        public Guid couponId { get; set; }
        //public string openId { get; set; }
        public Guid couponUsageId { get; set; }

        public string getTime { get; set; }

        public string title { get; set; }
        public string subTitle { get; set; }

        public string beginTime { get; set; }

        public string endTime { get; set; }

        public string couponType { get; set; }

        public int status { get; set; }
        public string usedTime { get; set; }

    }
}