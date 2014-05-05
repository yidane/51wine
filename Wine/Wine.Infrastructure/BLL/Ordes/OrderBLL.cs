using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wine.Infrastructure.Model.Orders;
using Wine.Infrastructure.Model.User;
using Wine.Infrastructure.DAO.Orders;

namespace Wine.Infrastructure.BLL.Ordes
{
    public class OrderBLL
    {
        public bool CreateNewOrder(Order order)
        {
            return (new OrderDAO().Insert(order) != null) ? true : false;
        }

        public List<Order> QueryMyOrder(Customer customer)
        {
            return new OrderDAO().QueryMyOrder(customer.CustomerID);
        }

        public List<Order> QueryAllOrderByPage(int pageIndex, int pageSize)
        {
            return new OrderDAO().QueryAllOrderByPage(pageIndex, pageSize);
        }

        public bool CloseOrder(int customerId, int orderID)
        {
            return new OrderDAO().CloseOrder(customerId, orderID);
        }
    }
}
