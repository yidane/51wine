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
            Context.Response.Write("{\"IsSuccess\":true,\"Message\":\"\",\"Data\":[{\"MenuId\":null,\"Caption\":\"权限管理\",\"Image\":\"config.png\",\"Url\":null,\"SubMenu\":[{\"MenuId\":\"d5f0d074-6f09-41c7-a48f-232781637866\",\"Caption\":\"账号管理\",\"Image\":\"1.png\",\"Url\":\"SystemView/AccountMgt.aspx\",\"SubMenu\":[]}]},{\"MenuId\":null,\"Caption\":\"系统管理\",\"Image\":\"cog.png\",\"Url\":null,\"SubMenu\":[{\"MenuId\":\"fcf951b8-441f-45e1-bb8f-3b780ae4497f\",\"Caption\":\"考核细则管理\",\"Image\":\"10.png\",\"Url\":\"SystemView/AssessConfig.aspx\",\"SubMenu\":[]},{\"MenuId\":\"b77c1e0a-bcdd-485d-be29-b5ef8fc3efb8\",\"Caption\":\"考核规则管理\",\"Image\":\"2.png\",\"Url\":\"SystemView/AppConfig.aspx\",\"SubMenu\":[]},{\"MenuId\":\"8ac39830-2fbd-4976-b28a-72ca3b99512e\",\"Caption\":\"用户映射\",\"Image\":\"5.png\",\"Url\":\"SystemView/UserMgt.aspx\",\"SubMenu\":[]},{\"MenuId\":\"fe07306e-7750-4a0b-b29b-c7097034b285\",\"Caption\":\"单位映射\",\"Image\":\"org.png\",\"Url\":\"SystemView/OrgMgt.aspx\",\"SubMenu\":[]},{\"MenuId\":\"fb10f855-8f92-4e63-9019-fadab5bedc99\",\"Caption\":\"服务接口管理\",\"Image\":\"4.png\",\"Url\":\"SystemView/ServiceMgt.aspx\",\"SubMenu\":[]},{\"MenuId\":\"857e6b6b-2711-44df-8edb-c4d2a3aa0f80\",\"Caption\":\"节假日管理\",\"Image\":\"clock.png\",\"Url\":\"SystemView/HolidayMgt.aspx\",\"SubMenu\":[]},{\"MenuId\":\"7e9a77ce-2894-48c3-a818-5e77998821f6\",\"Caption\":\"考核日志查询\",\"Image\":\"3.png\",\"Url\":\"SystemView/AssessLogView.aspx\",\"SubMenu\":[]}]}]}");
        }
    }
}