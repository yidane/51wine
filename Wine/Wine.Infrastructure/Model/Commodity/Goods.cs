using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wine.Infrastructure.Model.Commodity
{
    public class Goods
    {
        public int ID { get; set; }

        /// <summary>
        /// 商品ID
        /// </summary>
        public int GoodsID { get; set; }
        /// <summary>
        /// 商品类别
        /// </summary>
        public int GoodsTypeID { get; set; }
        /// <summary>
        /// 商品名称
        /// </summary>
        public string GoodsName { get; set; }
        /// <summary>
        /// 历史价位
        /// </summary>
        public decimal HistoryPrice { get; set; }
        /// <summary>
        /// 当前价位
        /// </summary>
        public decimal CurrentPrice { get; set; }
        /// <summary>
        /// 图片链接
        /// </summary>
        public string Pictureurl { get; set; }
    }
}