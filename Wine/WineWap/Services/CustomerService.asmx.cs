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
        public void LogOut()
        {
            Session["LoginUser"] = null;
            Session["RecentlyConsignee"] = null;
            Session["GoodsID"] = null;
            Session["Count"] = null;

            Context.Response.ContentType = "json";
            Context.Response.Write(new WebServiceResult<object>(true, true).ToString());
        }

        [WebMethod(EnableSession = true)]
        public void HasLogin()
        {
            var hasLogin = Session["LoginUser"] != null;
            var result = new
            {
                HasLogin = hasLogin,
                User = hasLogin ? (Session["LoginUser"] as Customer) : null
            };
            var outputString = new WebServiceResult<object>(true, result);
            Context.Response.ContentType = "json";
            Context.Response.Write(outputString);
        }

        [WebMethod(EnableSession = true)]
        public void AddConsignee(int regionID, string adress, string consigneeUserName, string mobile)
        {
            object newConsignee = null;
            Context.Response.ContentType = "json";
            var outputString = new Wine.WebServices.CustomerService().AddConsignee(Session["LoginUser"], out newConsignee, regionID, adress, consigneeUserName, mobile);

            if (newConsignee != null)
            {
                Session["RecentlyConsignee"] = newConsignee;
            }

            Context.Response.Write(outputString);
        }

        [WebMethod(EnableSession = true)]
        public void SetRecentlyConsignee(int consigneeID)
        {
            Session["RecentlyConsignee"] = new Wine.WebServices.CustomerService().QueryCustomerConsigneeInfo(Session["LoginUser"], consigneeID);
            Context.Response.ContentType = "json";
            Context.Response.Write(new WebServiceResult<object>(true, true).ToString());
        }

        [WebMethod(EnableSession = true)]
        public void QueryAllConsignee()
        {
            Context.Response.ContentType = "json";
            Context.Response.Write(new Wine.WebServices.CustomerService().QueryAllConsignee(Session["LoginUser"]));
        }

        [WebMethod(EnableSession = true)]
        public void QueryRecentlyConsignee()
        {
            Context.Response.ContentType = "json";
            if (Session["RecentlyConsignee"] != null)
            {
                Context.Response.Write(new WebServiceResult<object>(true, Session["RecentlyConsignee"] as CustomerConsigneeInfo).ToString());
            }
            else
            {
                Context.Response.Write(new Wine.WebServices.CustomerService().QueryRecentConsignee(Session["LoginUser"]));
            }
        }
    }
}