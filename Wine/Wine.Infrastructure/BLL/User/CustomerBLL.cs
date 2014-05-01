using Wine.Infrastructure.Model.User;
using Wine.Infrastructure.DAO.User;

namespace Wine.Infrastructure.BLL
{
    public class CustomerBLL
    {
        public Customer CostomerRegister(Customer customer)
        {
            return new CustomerDAO().Insert(customer);
        }

        public Customer CustomerLogin(int accountType, string loginName, string password)
        {
            return new CustomerDAO().Login(accountType, loginName, password);
        }
    }
}