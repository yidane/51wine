using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wine.Infrastructure.Model;
using Wine.Infrastructure.Model.User;
using Wine.Util;
using Wine.WebFacade.User;

namespace Wine.WebServices
{
    public class CustomerService
    {

        #region 注册登录服务
        /// <summary>
        /// 用户注册
        /// </summary>
        /// <param name="loginName"></param>
        /// <param name="nickName"></param>
        /// <param name="password1"></param>
        /// <param name="password2"></param>
        /// <returns></returns>
        public string CostomerRegister(string loginName, string nickName, string password1, string password2, out Customer customer)
        {
            customer = null;
            try
            {
                var isSucceed = false;
                var errorMessage = string.Empty;
                //判断两次密码输入是否相同
                if (string.Equals(password1.Trim(), password2.Trim()))
                {
                    customer = new Wine.WebFacade.User.CustomerFacade().CustomerRegister(
                                                                                                                           new Customer()
                                                                                                                           {
                                                                                                                               LoginName = loginName.Trim(),
                                                                                                                               NickName = nickName.Trim(),
                                                                                                                               Password = EncryptionHelper.Encrypt(password1.Trim())
                                                                                                                           }
                                                                                                                       );
                    if (customer != null)
                        isSucceed = true;

                    return new WebServiceResult<Customer>(isSucceed, "注册成功").ToString();
                }
                else
                {
                    return new WebServiceResult<string>(false, "两次输入的密码不相同").ToString();
                }
            }
            catch (Exception ex)
            {
                return new WebServiceResult<string>(false, ex.Message).ToString();
            }
        }

        /// <summary>
        /// 检测用户名是否存在
        /// </summary>
        /// <param name="loginName"></param>
        /// <returns></returns>
        public string CheckAccountExists(string loginName)
        {
            return string.Empty;
        }

        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="accountType"></param>
        /// <param name="loginName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public Customer LoginIn(int accountType, string loginName, string password)
        {
            return new Wine.WebFacade.User.CustomerFacade().CustomerLogin(accountType, loginName, EncryptionHelper.Encrypt(password));
        }
        #endregion

        #region 账户信息

        #region 收货人信息

        public string AddConsignee(object customerSession, int regionID, string adress, string consigneeUserName, string mobile)
        {
            try
            {
                if (customerSession == null)
                    return new WebServiceResult<object>(false, null, "登录已超时").ToString();
                var customer = customerSession as Customer;

                CustomerConsigneeInfo newCustomerConsigneeInfo = new CustomerConsigneeInfo();
                newCustomerConsigneeInfo.CustomerID = customer.CustomerID;
                newCustomerConsigneeInfo.ConsigneeUserName = consigneeUserName;
                newCustomerConsigneeInfo.ConsigneeMobile = mobile;
                newCustomerConsigneeInfo.Adress = adress;
                newCustomerConsigneeInfo.DeliverRegionID = regionID;
                newCustomerConsigneeInfo.Email = string.Empty;

                var result = new CustomerConsigneeFacade().Insert(newCustomerConsigneeInfo);
                var isSucceed = (result != null && result.CustomerConsigneeInfoID > 0);
                return new WebServiceResult<object>(isSucceed, result).ToString();
            }
            catch (Exception ex)
            {
                return new WebServiceResult<object>(false, null, ex.Message).ToString();
            }
        }

        public CustomerConsigneeInfo QueryCustomerConsigneeInfo(object customerSession, int consigneeInfoID)
        {
            try
            {
                if (customerSession == null)
                    return null;
                var customer = customerSession as Customer;

                return new CustomerConsigneeFacade().QueryCustomerConsigneeInfo(customer, consigneeInfoID);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public string QueryRecentConsignee(object customerSession)
        {
            try
            {
                if (customerSession == null)
                    return new WebServiceResult<object>(false, null, "登录已超时").ToString();
                var customer = customerSession as Customer;

                var result = new CustomerConsigneeFacade().QueryRecentlyCustomerConsigneeInfo(customer);
                return new WebServiceResult<object>(true, result).ToString();
            }
            catch (Exception ex)
            {
                return new WebServiceResult<object>(false, null, ex.Message).ToString();
            }
        }

        public string QueryAllConsignee(object customerSession)
        {
            try
            {
                if (customerSession == null)
                    return new WebServiceResult<object>(false, null, "登录已超时").ToString();
                var customer = customerSession as Customer;

                var result = new CustomerConsigneeFacade().Query(customer);
                var isSucceed = (result != null && result.Count > 0);
                return new WebServiceResult<object>(isSucceed, result).ToString();
            }
            catch (Exception ex)
            {
                return new WebServiceResult<object>(false, null, ex.Message).ToString();
            }
        }
        #endregion

        #endregion
    }
}