using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace WeiXinPF.Infrastructure.DomainDataAccess.Photo
{
    [Table("wx_travel_photoUserParticipated")]
    public  partial class photoUserParticipated
    {
        #region Properties
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        [ForeignKey("ActionInfo")]
        public int aid { get; set; }

        public virtual photoActionInfo ActionInfo { get; set; }
       
        /// <summary>
        /// 用户openid
        /// </summary>
        public string openid { get; set; }
        
       /// <summary>
       /// 参与时间
       /// </summary>
        public string participatedDate { get; set; }
        /// <summary>
        /// 上传图片
        /// </summary>
        public string participatedImg { get; set; }

        /// <summary>
        /// 是否分享到朋友圈
        /// </summary>
        public bool isShareTimeline { get; set; }
        /// <summary>
        /// 是否发送给好友
        /// </summary>
        public bool isShareAppMessage { get; set; }

        #endregion
    }

    public partial class photoUserParticipated
    {
        #region Method
        #endregion
    }
}
