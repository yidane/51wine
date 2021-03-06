﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WeiXinPF.Application.DomainModules.Coupon.DTOS
{
    public class CouponBaseInfoDTO
    {
        public string actName { get; internal set; }

        public string brief { get; internal set; }
        public string endDate { get; internal set; }
        public string contractInfo { get; set; }
        public int? personMaxTimes { get; set; }
        public int? dayMaxTimes { get; set; }
        public string djPwd { get; set; }
        public string beginDate { get; set; }
        /// <summary>
        /// 用户参加次数
        /// </summary>
        public int hasCjTimes { get; set; }
        public string cfcjhf { get; internal set; }
        public string beginPic { get; internal set; }
        public string endNotice { get; internal set; }
        public string endContent { get; internal set; }
        public string endPic { get; internal set; }

    }
}
