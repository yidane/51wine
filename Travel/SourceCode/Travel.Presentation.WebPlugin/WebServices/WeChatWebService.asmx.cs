using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using Travel.Application.DomainModules.WeChat;
using Travel.Application.DomainModules.WeChat.DTOS;
using Travel.Infrastructure.CommonFunctions.Ajax;
using Travel.Infrastructure.WeiXin.Common.OAuth2;

namespace Travel.Presentation.WebPlugin.WebServices
{
    /// <summary>
    /// WeChatService 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
    [System.Web.Script.Services.ScriptService]
    public class WeChatWebService : System.Web.Services.WebService
    {

        [WebMethod]
        public void GetUserAnalysis(DateTime beginDate, DateTime endDate)
        {
            var userStatisticsList = new WeChatService().GetUserStatistics(beginDate, endDate);
            if (userStatisticsList.Count > 0)
            {
                Context.Response.Write(AjaxResult.Success(userStatisticsList));
            }
        }
    }
}
