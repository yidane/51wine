using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wine.Infrastructure.Model.Deliver
{
    public class DeliverRegion
    {
        /// <summary>
        /// 区域ID
        /// </summary>
        public int RegionID { get; set; }

        /// <summary>
        /// 区域名称
        /// </summary>
        public string RegionName { get; set; }

        /// <summary>
        /// 能否配送
        /// </summary>
        public bool CanDeliver { get; set; }

        /// <summary>
        /// 下一级区域
        /// </summary>
        public List<DeliverRegion> ChildDeliverRegionList { get; set; }
    }
}