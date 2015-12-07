using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WeiXinPF.Application.DomainModules.Map.DTOS
{
    /// <summary>
    /// 地图dto
    /// </summary>
    public class MapDTO
    {
        public int id { get; set; }
        public string name { get; set; }
        public string remark { get; set; }
        public string address { get; set; }
        public Position position { get; set; }
    }

    /// <summary>
    /// 坐标对象
    /// </summary>
    public class Position
    {
        /// <summary>
        /// 纬度
        /// </summary>
        public double lat { get; set; }
        /// <summary>
        /// 经度
        /// </summary>
        public double lng { get; set; }
    }
}
