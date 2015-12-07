using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;

namespace WeiXinPF.Infrastructure.DomainDataAccess.Message
{
    [Table("dt_common_ShortMsg")]
    public partial class ShortMsg
    {
        #region Propetries
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [MaxLength(128)]
        public string Title { get; set; }
        [Required, MaxLength(2000)]
        public string Content { get; set; }

        [MaxLength(128)]
        public string Type { get; set; }

        [MaxLength(128)]
        public string MenuType { get; set; }

        [Required, DefaultValue(false)]
        public bool IsShowButton { get; set; }

        public string ButtonText { get; set; }
        public string ButtonUrl { get; set; }
        public string ButtonMutipleUrl { get; set; }

        [Required]
        public DateTime CreateTime { get; set; }
        [Required, DefaultValue(false)]
        public bool IsRead { get; set; }

        [Required]
        public string FromUserId { get; set; }
        [Required]
        public int MsgToUserType { get; set; }

        [Required]
        public int MsgFromUserType { get; set; }
        [Required]
        public string ToUserId { get; set; } 
        #endregion
    }


}
