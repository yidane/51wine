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
        public int id { get; set; }
        public string sn { get; set; }
        public int sortid { get; set; }
        public string jxname { get; set; }
        public string jpname { get; set; }
        public string getTime { get; set; }
        public bool status { get; set; }

        public string beginDate { get; set; }
        public string endDate { get; set; }

    }
}
