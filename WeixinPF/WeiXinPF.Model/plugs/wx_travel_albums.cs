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
    public class wx_travel_albums
    {
        public int Id { get; set; }
        public int ScenicDetailId { get; set; }
        public string ThumbPath { get; set; }
    }
}
