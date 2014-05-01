using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wine.Infrastructure.Model.Orders
{
    public class OrderType
    {
        /// <summary>
        /// 订单类型ID
        /// </summary>
        public int OrderTypeID { get; set; }

        /// <summary>
        /// 订单类型名称
        /// </summary>
        public string OrderTypeName { get; set; }
    }
}
