using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wine.Infrastructure.Model.Orders;
using Wine.Infrastructure.BLL.Ordes;
using Wine.Infrastructure.Model.User;

namespace Wine.WebFacade.Orders
{
    public class OrderFacade
    {
        public bool CommitOrder(Order order)
        {
            return new OrderBLL().CreateNewOrder(order);
        }

        public List<Order> QueryMyOrder(Customer customer)
        {
            return new OrderBLL().QueryMyOrder(customer);
        }

        public List<Order> QueryAllOrderByPage(int pageIndex, int pageSize)
        {
            return new OrderBLL().QueryAllOrderByPage(pageIndex, pageSize);
        }

        public bool CloseOrder(int customerID, int orderID)
        {
            return new OrderBLL().CloseOrder(customerID, orderID);
        }
    }
}
