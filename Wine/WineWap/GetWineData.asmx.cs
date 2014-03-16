using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace WineWap
{
    /// <summary>
    /// GetWineData 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
    // [System.Web.Script.Services.ScriptService]
    public class GetWineData : System.Web.Services.WebService
    {

        [WebMethod]
        public void GetWineDataList(int id, bool isMore, int currentPage)
        {
            WineDataCache oWineDataCache = new WineDataCache();
            Context.Response.Write(oWineDataCache.GetHtml(id, isMore, currentPage));
        }

        [WebMethod]
        public void GetWineDetail(string url)
        {

        }
    }
}
