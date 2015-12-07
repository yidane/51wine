using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WeiXinPF.Model
{
    /// <summary>
    /// 分享的实体
    /// </summary>
    public class FxModel
    {

       
        public string appId { get; set; }
        public string timestamp { get; set; }
        public string nonceStr { get; set; }
        /// <summary>
        /// 分享弹窗上的标题
        /// </summary>
        public string fxTitle { get; set; }
        /// <summary>
        /// /// 分享弹窗上的图片
        /// </summary>
        public string fxImg { get; set; }

        /// <summary>
        /// 分享弹窗上的内容
        /// </summary>
        public string fxContent { get; set; }

        /// <summary>
        /// 当前的网址
        /// </summary>
        public string thisUrl { get; set; }

        /// <summary>
        /// 分享引导网址
        /// </summary>
        public string fxUrl { get; set; }

        /// <summary>
        /// 签名
        /// </summary>
        public string signature { get; set;  }



    }
}
