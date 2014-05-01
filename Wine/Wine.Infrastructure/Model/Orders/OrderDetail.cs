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
        public string OrderNO { get; set; }

        public int GoodsID { get; set; }
    }
}
