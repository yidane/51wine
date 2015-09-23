using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WeiXinPF.Model
{
    public class wx_travel_marker
    {
        public int Id { get; set; }
        /// <summary>
        /// 公众号ID
        /// </summary>
        public int wid { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 说明
        /// </summary>
        public string Remark { get; set; }
        /// <summary>
        /// 手绘图上的坐标-Top
        /// </summary>
        public double Top { get; set; }
        /// <summary>
        /// 手绘图上的坐标-Left
        /// </summary>
        public double Left { get; set; }
        /// <summary>
        /// 经度
        /// </summary>
        public double Lng { get; set; }
        /// <summary>
        /// 纬度
        /// </summary>
        public double Lat { get; set; }

        /// <summary>
        /// 详情链接
        /// </summary>
        public string Url { get; set; }
        /// <summary>
        /// 引路描述
        /// </summary>
        public string Description { get; set; }
        public string extStr1 { get; set; }
        public string extStr2 { get; set; }
        public int? extInt1 { get; set; }
        public int? extInt2 { get; set; }
    }
}
