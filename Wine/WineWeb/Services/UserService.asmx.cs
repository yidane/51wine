using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace WineWeb.Services
{
    /// <summary>
    /// UserService 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
    // [System.Web.Script.Services.ScriptService]
    public class UserService : System.Web.Services.WebService
    {

        [WebMethod(EnableSession = true)]
        public void InitUserMenu()
        {
            Context.Response.Write("{\"IsSuccess\":true,\"Message\":\"\",\"Data\":[{\"MenuId\":null,\"Caption\":\"订单管理\",\"Image\":\"config.png\",\"Url\":null,\"SubMenu\":[{\"MenuId\":\"d5f0d074-6f09-41c7-a48f-232781637866\",\"Caption\":\"订单管理\",\"Image\":\"1.png\",\"Url\":\"SystemView/OrderList.html\",\"SubMenu\":[]}]}]}");
        }
    }
}