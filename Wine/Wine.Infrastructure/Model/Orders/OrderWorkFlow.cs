using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wine.Infrastructure.Model.Orders
{
    /// <summary>
    /// 订单工作流
    /// </summary>
    public class OrderWorkFlow
    {
        /// <summary>
        /// 流水号
        /// </summary>
        public Int64 ID { get; set; }

        /// <summary>
        /// 订单号
        /// </summary>
        public string OrderNO { get; set; }

        /// <summary>
        /// 订单状态
        /// </summary>
        public int OrderStatus { get; set; }

        /// <summary>
        /// 时间戳
        /// </summary>
        public DateTime CurrentTime { get; set; }
    }
}
