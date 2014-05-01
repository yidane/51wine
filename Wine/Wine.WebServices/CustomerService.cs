using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wine.Infrastructure.Model;
using Wine.Infrastructure.Model.User;
using Wine.Util;

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
                    customer = new Wine.WebFacade.CustomerFacade().CustomerRegister(
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
            return new Wine.WebFacade.CustomerFacade().CustomerLogin(accountType, loginName, EncryptionHelper.Encrypt(password));
        }
        #endregion

        #region 账户信息

        #endregion
    }
}