using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WeiXinPF.Infrastructure.DomainDataAccess.Payment
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity;

    /// <summary>
    /// 核销功能所需的验证码
    /// </summary>
    [Table("wx_Verification_IdentifyingCodeInfo")]
    public class IdentifyingCodeInfo
    {
        [Key]
        public Guid IdentifyingCodeId { get; set; }

        [Required]
        public int Wid { get; set; }

        [Required]
        public string ShopId { get; set; }

        [Required, MaxLength(50)]
        public string ModuleName { get; set; }

        /// <summary>
        /// 订单Id
        /// </summary>
        [Required, MaxLength(50)]
        public string OrderId { get; set; }

        /// <summary>
        /// 订单编码
        /// </summary>
        [Required, MaxLength(50)]
        public string OrderCode { get; set; }

        /// <summary>
        /// 单件商品的Id
        /// </summary>
        [Required, MaxLength(100)]        
        public string ProductId { get; set; }

        /// <summary>
        /// 单件商品的编码
        /// </summary>
        [Required, MaxLength(100)]
        public string ProductCode { get; set; }

        /// <summary>
        /// 商品识别码
        /// </summary>
        [Required, MaxLength(100)]
        public string IdentifyingCode { get; set; }

        [Required]
        public DateTime CreateTime { get; set; }

        [Required]
        public DateTime ModifyTime { get; set; }

        [Required]
        public int Status { get; set; }
    }
}
