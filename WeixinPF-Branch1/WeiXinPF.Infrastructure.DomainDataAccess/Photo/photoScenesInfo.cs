using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace WeiXinPF.Infrastructure.DomainDataAccess.Photo
{
    [Table("wx_travel_photoScenesInfo")]
    public partial class photoScenesInfo
    {
        #region Properties
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        [ForeignKey("ActionInfo")]

        public int aid { get; set; }

        public virtual photoActionInfo ActionInfo { get; set; }
        /// <summary>
        /// 湖怪形象设置
        /// </summary>
        public string  hgImg { get; set; }

        /// <summary>
        /// 湖怪动作设置
        /// </summary>
        public string hgAction { get; set; }

        /// <summary>
        /// 背景设置
        /// </summary>
        public string  backgroundImg { get; set; }

        /// <summary>
        /// 背景链接
        /// </summary>
        public string backgroundLink { get; set; }

        /// <summary>
        /// 音频文件
        /// </summary>
        public string audio { get; set; }

        /// <summary>
        /// 是否循环播放
        /// </summary>
        public bool isloop { get; set; }

        /// <summary>
        /// 是否自动播放
        /// </summary>
        public bool isAutoPlay { get; set; }
        #endregion
    }

    public partial class photoScenesInfo
    {
        #region Method
        #endregion
    }
}
