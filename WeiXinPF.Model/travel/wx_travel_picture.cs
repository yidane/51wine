using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;

namespace WeiXinPF.Model
{
    /// <summary>
    /// 景区图片相册
    /// </summary>
    public class wx_travel_picture
    {
        public int Id { get; set; }
        /// <summary>
        /// 景点ID
        /// </summary>
        public int DetailId { get; set; }

        /// <summary>
        /// 图片名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 图片路径
        /// </summary>
        public string PicPath { get; set; }

        /// <summary>
        /// 图片说明
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int OrderNo { get; set; }

        /// <summary>
        /// 上传时间
        /// </summary>
        public DateTime CreateDate { get; set; }
    }
}
