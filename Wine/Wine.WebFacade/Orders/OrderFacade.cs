using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wine.Infrastructure.Model.Orders;
using Wine.Infrastructure.BLL.Ordes;

namespace Wine.WebFacade.Orders
{
    public class OrderFacade
    {
        public bool CommitOrder(Order order)
        {
            return new OrderBLL().CreateNewOrder(order);
        }
    }
}
