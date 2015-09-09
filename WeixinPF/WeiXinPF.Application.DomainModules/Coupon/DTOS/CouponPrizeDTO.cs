using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WeiXinPF.Application.DomainModules.Coupon.DTOS
{
    /// <summary>
    /// 奖品
    /// </summary>
    public class CouponPrizeDTO
    {
        public string sn { get; set; }
        public int sortid { get; set; }
        public string jxname { get; set; }
        public string jpname { get; set; }
        public int uid { get; set; }
    }
}
