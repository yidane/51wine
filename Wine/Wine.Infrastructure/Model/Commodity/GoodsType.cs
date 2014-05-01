using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wine.Infrastructure.Model.Commodity
{
    public class GoodsType
    {
        /// <summary>
        /// 商品类型ID
        /// </summary>
        public int TypeID { get; set; }

        /// <summary>
        /// 商品类型名称
        /// </summary>
        public string TypeName { get; set; }

        /// <summary>
        /// 类型原始Url
        /// </summary>
        public string OriginalUrl { get; set; }

        /// <summary>
        /// 类型获取更多Url
        /// </summary>
        public string MoreDataUrl { get; set; }
    }
}
