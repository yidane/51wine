using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Travel.Application.DomainModules.WeChat;

namespace Travel.Presentation.Web.WXAPI
{
    public partial class getOpenId : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Write(string.Format("Page_Load{0}", "<br></br>"));

            if (Request.UrlReferrer != null)
                Response.Write(string.Format("{0}{1}", Request.UrlReferrer.ToString(), "<br></br>"));

            Response.Write(string.Format("{0}{1}", Request.Url.ToString(), "<br></br>"));

            foreach (string key in Request.QueryString.AllKeys)
            {
                Response.Write(string.Format("【{0}】【{1}】{2}", key, Request.QueryString[key], "<br></br>"));
            }

            var code = Request.QueryString["Code"];
            var weChatService = new WeChatService();
            var tokenResult = weChatService.GetAccessToken(code);
            Response.Write(Travel.Infrastructure.CommonFunctions.Ajax.AjaxResult.Success(tokenResult));
        }
    }
}