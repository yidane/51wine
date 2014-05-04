using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wine.Infrastructure.Model.Orders
{
    /// <summary>
    /// 订单详细
    /// </summary>
    public class OrderDetail
    {
        /// <summary>
        /// 流水号
        /// </summary>
        public int OrderDetailID { get; set; }

        /// <summary>
        /// 订单ID
        /// </summary>
        public int OrderID { get; set; }

        /// <summary>
        /// 商品ID
        /// </summary>
        public int GoodsID { get; set; }

        /// <summary>
        /// 商品名称
        /// </summary>
        public string GoodsName { get; set; }

        /// <summary>
        /// 商品价格
        /// </summary>
        public decimal GoodsPrice { get; set; }

        /// <summary>
        /// 图链接
        /// </summary>
        public string PictureUrl { get; set; }

        /// <summary>
        /// 购买数量
        /// </summary>
        public int GoodsCount { get; set; }
    }
}
