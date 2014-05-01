using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Services;
using Wine.WebServices;
using Wine.Infrastructure.Model.Commodity;

namespace WineWap.Services
{
    /// <summary>
    /// MarketService 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
    // [System.Web.Script.Services.ScriptService]
    public class MarketService : System.Web.Services.WebService
    {
        [WebMethod(EnableSession = true)]
        public void QueryWineList(int wineType, int id)
        {
            Context.Response.ContentType = "json";

            var sum = 0;
            GoodsType goodType = null;
            var wineList = new Wine.WebServices.MarketService().QueryWineList(wineType, id, out sum, out goodType);

            var html = string.Empty;
            wineList.ForEach(wine => html += ToString(wine));
            var data = new
            {
                IsOver = !(wineList.Count == 10),
                ID = (wineList != null && wineList.Count > 0) ? wineList[wineList.Count - 1].ID : 0,
                Html = html,
                Title = goodType != null ? goodType.TypeName : string.Empty
            };
            var output = new WebServiceResult<object>(true, data, string.Empty).ToString();

            Context.Response.Write(output);
        }

        private string ToString(Goods goods)
        {
            StringBuilder outHtmlBuilder = new StringBuilder();
            outHtmlBuilder.Append("<li>");
            outHtmlBuilder.Append("<a class=\"img\" href=\"WineDetail.html?id={0}\"></a>");
            outHtmlBuilder.Append("<span class=\"frame\"><img src=\"{1}\" /></span>");
            outHtmlBuilder.Append("<div class=\"info\">");
            outHtmlBuilder.Append("<div class=\"title\">");
            outHtmlBuilder.Append("<span class=\"name\"><a href=\"WineDetail.html?id={0}\">{2}</a></span>");
            outHtmlBuilder.Append("</div>");
            outHtmlBuilder.Append("<div class=\"price clearfix\">");
            outHtmlBuilder.Append("<p class='jx-price'>{3}</p>");
            outHtmlBuilder.Append("<p class=\"market-price\"><S>{4}</S></p>");
            outHtmlBuilder.Append("<p class=\"count\">{5}</p>");
            outHtmlBuilder.Append("</div>");
            //outHtmlBuilder.Append("<div class=\"judge\">");
            //outHtmlBuilder.Append("<span class=\"score\">5分</span><span class=\"article\">0人评论</span>");
            //outHtmlBuilder.Append("</div>");
            outHtmlBuilder.Append("</div>");
            outHtmlBuilder.Append("</li>");
            return string.Format(outHtmlBuilder.ToString(), goods.GoodsID, goods.Pictureurl, goods.GoodsName, goods.CurrentPrice, goods.HistoryPrice, 0);
        }

        [WebMethod(EnableSession = true)]
        public void QueryWineDetail(int id)
        {
            var goods = new Wine.WebServices.MarketService().QueryGoodsDetail(id);
            string outputString = string.Empty;
            if (goods != null)
            {
                outputString = new WebServiceResult<Goods>(true, goods).ToString();
            }
            else
            {
                outputString = new WebServiceResult<Goods>(false, "数据初始化失败").ToString();
            }

            Context.Response.ContentType = "json";
            Context.Response.Write(outputString);
        }
    }
}
