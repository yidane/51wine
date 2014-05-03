using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wine.Infrastructure.Model.User;
using Wine.Infrastructure.BLL.User;

namespace Wine.WebFacade.User
{
    public class CustomerConsigneeFacade
    {
        public CustomerConsigneeInfo Insert(CustomerConsigneeInfo t)
        {
            return new CustomerConsigneeInfoBLL().Insert(t);
        }

        public List<CustomerConsigneeInfo> Query(Customer customer)
        {
            return new CustomerConsigneeInfoBLL().Query(customer);
        }

        public CustomerConsigneeInfo QueryRecentlyCustomerConsigneeInfo(Customer customer)
        {
            return new CustomerConsigneeInfoBLL().QueryRecentlyCustomerConsigneeInfo(customer);
        }

        public CustomerConsigneeInfo QueryCustomerConsigneeInfo(Customer customer, int consigneeInfoID)
        {
            return new CustomerConsigneeInfoBLL().QueryCustomerConsigneeInfo(customer, consigneeInfoID);
        }
    }
}