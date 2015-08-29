using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;


namespace Travel.Infrastructure.DomainDataAccess.Coupon.Entitys
{
   public  class Coupon
    {
        [Key]
        public Guid CouponId { get; set; }
        public string Title { get; set; }
        public string SubTitle { get; set; }

        public DateTime BeginTime { get; set; }

        public DateTime EndTime { get; set; }

       

        public string Extend1 { get; set; }
        public string Extend2 { get; set; }
        public string Extend3 { get; set; }
        public string Extend4 { get; set; }


        public Guid CouponTypeId { get; set; }

        [ForeignKey("CouponTypeId")]
        public virtual CouponType Type { get; set; }

       
    }
}
