using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using Travel.Infrastructure.DomainDataAccess.User;

namespace Travel.Infrastructure.DomainDataAccess.Coupon.Entitys
{
    public class CouponUsage
    {
        [Key]
        public Guid CouponUsageId { get; set; }

        public Guid CouponId { get; set; }
        public String OpenId { get; set; }

        /// <summary>
        /// 接收到优惠券时间
        /// </summary>
        public DateTime ReceivedTime { get; set; }

        [ForeignKey("CouponId")]
        public virtual Coupon Coupon { get; set; }
        [ForeignKey("OpenId")]
        public virtual UserInfo User { get; set; }
    }
}
