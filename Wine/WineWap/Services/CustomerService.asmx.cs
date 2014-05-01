using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using Wine.Infrastructure.Model.User;
using Wine.WebServices;

namespace WineWap.Services
{
    /// <summary>
    /// CustomerService 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
    // [System.Web.Script.Services.ScriptService]
    public class CustomerService : System.Web.Services.WebService
    {
        [WebMethod(EnableSession = true)]
        public void CommonRegister(string loginName, string nickName, string password, string password2)
        {
            Context.Response.ContentType = "json";
            Customer customer = null;
            var outputMessage = new Wine.WebServices.CustomerService().CostomerRegister(loginName, nickName, password, password2, out customer);
            if (customer != null)
            {
                Session["LoginUser"] = customer;
            }
            Context.Response.Write(outputMessage);
        }

        [WebMethod(EnableSession = true)]
        public void CheckAccountExists(string loginName)
        {
            Context.Response.ContentType = "json";
            Context.Response.Write(new Wine.WebServices.CustomerService().CheckAccountExists(loginName));
        }

        [WebMethod(EnableSession = true)]
        public void Login(int accountType, string loginName, string password)
        {
            var loginUser = new Wine.WebServices.CustomerService().LoginIn(accountType, loginName, password);
            var isSucceed = false;
            var message = string.Empty;
            if (loginUser != null)
            {
                Session["LoginUser"] = loginUser;
                isSucceed = true;
            }
            else
            {
                message = "账户或密码错误";
            }

            var outputMessage = new WebServiceResult<Customer>(isSucceed, loginUser, message).ToString();

            Context.Response.ContentType = "json";
            Context.Response.Write(outputMessage);
        }

        [WebMethod(EnableSession = true)]
        public void HasLogin()
        {
            var hasLogin = Session["LoginUser"] != null;
            var outputString = new WebServiceResult<object>(true, hasLogin);
            Context.Response.ContentType = "json";
            Context.Response.Write(outputString);
        }
    }
}