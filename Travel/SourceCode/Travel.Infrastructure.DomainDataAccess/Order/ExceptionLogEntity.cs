using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Travel.Infrastructure.DomainDataAccess.Order
{
    using System.ComponentModel.DataAnnotations;
    using System.Net.Cache;

    using Travel.Infrastructure.WeiXin.Advanced.Pay.Model;

    public class ExceptionLogEntity
    {
        [Key]
        public Guid ExceptionLogId { get; set; }

        [Required]
        [MaxLength(100)]
        public string Module { get; set; }

        public DateTime CreateTime { get; set; }

        [Required]
        [MaxLength(214)]
        public string ExceptionType { get; set; }

        [Required]
        public string ExceptionMessage { get; set; }

        [Required]
        public string TrackMessage { get; set; }

        /// <summary>
        /// 是否有自定义异常处理程序
        /// </summary>
        public bool HasExceptionProcessing { get; set; }

        /// <summary>
        /// 是否需要处理
        /// </summary>
        public bool NeedProcess { get; set; }

        /// <summary>
        /// 处理方式
        /// </summary>
        [MaxLength(512)]
        public string ProcessFunction { get; set; }

        public void Add()
        {
            using (var db = new TravelDBContext())
            {
                db.ExceptionLog.Add(this);
                db.SaveChanges();
            }
        }
    }
}
