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
    }
}
