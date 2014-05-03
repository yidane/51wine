using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wine.Infrastructure.Model;
using Wine.Infrastructure.BLL;
using Wine.Infrastructure.Model.User;

namespace Wine.WebFacade.User
{
    public class CustomerFacade
    {
        public Customer CustomerRegister(Customer customer)
        {
            return new CustomerBLL().CostomerRegister(customer);
        }

        public Customer CustomerLogin(int accountType, string loginName, string password)
        {
            return new CustomerBLL().CustomerLogin(accountType, loginName, password);
        }
    }
}