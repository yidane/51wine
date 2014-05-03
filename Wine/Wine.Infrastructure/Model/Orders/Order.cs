using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wine.Infrastructure.Model.User;

namespace Wine.Infrastructure.Model.Orders
{
    /// <summary>
    /// 订单
    /// </summary>
    public class Order
    {
        /// <summary>
        /// 订单流水号
        /// </summary>
        public int OrderID { get; set; }
        /// <summary>
        /// 订单号
        /// </summary>
        public string OrderNO { get; set; }

        /// <summary>
        /// 订单类型
        /// </summary>
        public int OrderType { get; set; }

        /// <summary>
        /// 订单状态
        /// </summary>
        public int OrderStatus { get; set; }

        /// <summary>
        /// 订单拥有者
        /// </summary>
        public int OrderOwnerID { get; set; }

        /// <summary>
        /// 订单商品详细
        /// </summary>
        public List<OrderDetail> Details { get; set; }

        /// <summary>
        /// 总价
        /// </summary>
        public decimal TotalPrice { get; set; }

        /// <summary>
        /// 顾客配送地址ID
        /// </summary>
        public int CustomerConsigneeInfoID { get; set; }

        /// <summary>
        /// 收货人姓名
        /// </summary>
        public string ConsigneeUserName { get; set; }

        /// <summary>
        /// 收货人电话
        /// </summary>
        public string ConsigneeMobile { get; set; }

        /// <summary>
        ///配送区域
        /// </summary>
        public int DeliverRegionID { get; set; }

        /// <summary>
        /// 配送详细地址
        /// </summary>
        public string Adress { get; set; }

        /// <summary>
        /// 顾客备注
        /// </summary>
        public string CustomerRemarks { get; set; }
    }
}