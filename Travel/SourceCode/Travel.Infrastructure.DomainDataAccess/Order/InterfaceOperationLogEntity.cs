using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Travel.Infrastructure.DomainDataAccess.Order
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class InterfaceOperationLogEntity
    {
        [Key]
        public Guid InterfaceOperationLogId { get; set; }

        /// <summary>
        /// 模块名称
        /// </summary>
        [Required, MaxLength(128)]        
        public string Module { get; set; }

        /// <summary>
        /// 关联订单编号
        /// </summary>
        [Required, MaxLength(50)]
        public string OrderCode { get; set; }

        /// <summary>
        /// 当前操作对象主键
        /// </summary>
        [MaxLength(512)]
        public string OperateObjectId { get; set; }

        [Required]
        [MaxLength(100)]
        public string OperateName { get; set; }

        [Required]
        public bool IsOperateSuccess { get; set; }

        public DateTime CreateTime { get; set; }

        [MaxLength(100)]
        public string ErrorCode { get; set; }

        [MaxLength(512)]
        public string ErrorDescription { get; set; }

        public void Add()
        {
            using (var db = new TravelDBContext())
            {
                db.InterfaceOperationLog.Add(this);
                db.SaveChanges();
            }
        }

    }
}
