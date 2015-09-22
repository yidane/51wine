using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Travel.Infrastructure.DomainDataAccess.Order
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class StatusEntity
    {
        [Key]
        public Guid StatusId { get; set; }

        private string _module;

        [Required, MaxLength(50)]
        public string Module {
            get
            {
                return string.IsNullOrEmpty(this._module) ? string.Empty : this._module.ToUpper();
            }

            set
            {
                this._module = value;
            }
        }

        /// <summary>
        /// 状态中文名称
        /// </summary>
        [Required, MaxLength(50)]
        public string StatusCNName { get; set; }

        /// <summary>
        /// 状态英文名称
        /// </summary>
        [Required, MaxLength(50)]
        public string StatusENName { get; set; }

        /// <summary>
        /// 状态编号
        /// </summary>
        [Required, MaxLength(50)]
        public string StatusCode { get; set; }

        /// <summary>
        /// 状态描述
        /// </summary>
        [Required, MaxLength(50)]
        public string StatusDescription { get; set; }

        public DateTime CreateTime { get; set; }

        [Required, MaxLength(20)]
        public string Sort { get; set; }


        private static IList<StatusEntity> _status; 
        public static IList<StatusEntity> Status {
            get
            {
                if (_status == null || !_status.Any())
                {
                    using (var db = new TravelDBContext())
                    {
                        _status = db.Status.ToList();
                    }
                }

                return _status;
            }

            private set
            {
                _status = value;
            }
        }

        public static void RefreshStatus()
        {
            using (var db = new TravelDBContext())
            {
                Status = db.Status.ToList();
            }
        }
    }
}
