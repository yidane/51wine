using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wine.Infrastructure.Model.User;
using Wine.Infrastructure.DAO.User;

namespace Wine.Infrastructure.BLL.User
{
    public class CustomerConsigneeInfoBLL
    {
        public CustomerConsigneeInfo Insert(CustomerConsigneeInfo t)
        {
            return new CustomerConsigneeInfoDAO().Insert(t);
        }

        public List<CustomerConsigneeInfo> Query(Customer customer)
        {
            CustomerConsigneeInfo oCustomerConsigneeInfo = new CustomerConsigneeInfo();
            oCustomerConsigneeInfo.CustomerID = customer.CustomerID;

            return new CustomerConsigneeInfoDAO().Query(oCustomerConsigneeInfo);
        }

        public CustomerConsigneeInfo QueryRecentlyCustomerConsigneeInfo(Customer customer)
        {
            return new CustomerConsigneeInfoDAO().QueryRecentlyCustomerConsigneeInfo(customer.CustomerID);
        }

        public CustomerConsigneeInfo QueryCustomerConsigneeInfo(Customer customer, int consigneeInfoID)
        {
            return new CustomerConsigneeInfoDAO().QueryCustomerConsigneeInfo(customer.CustomerID, consigneeInfoID);
        }
    }
}