using MxWeiXinPF.Templates;
using System;
using System.Collections.Generic;
using MxWeiXinPF.Common;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Senparc.Weixin.MP.AdvancedAPIs;
using Senparc.Weixin.MP.AdvancedAPIs.OAuth;
using Senparc.Weixin.MP;

namespace MxWeiXinPF.Web.shop
{
    public partial class cart : ShopBasePage
    {
        override protected void OnInit(EventArgs e)
        {
            base.OnInit(e);
            if (errInitTemplates != "")
            {
                Response.Write(errInitTemplates);
                return;
            }

            //1获得模版基本信息
            BLL.wx_module_templates tBll = new BLL.wx_module_templates();
            templateFileName = tBll.GetTemplatesFileNameByWid("shop", wid);
            if (templateFileName == null || templateFileName.Trim() == "")
            {
                errInitTemplates = "不存在该帐号或者该帐号尚未设置模版！";
                Response.Write(errInitTemplates);
                Response.End();
                return;
            }

            //授权
            BLL.wx_userweixin bll = new BLL.wx_userweixin();
            Model.wx_userweixin wxModel = bll.GetModel(wid);
            //string openid = "";
            //string code = MyCommFun.QueryString("code");
            //if (code == null || code.Trim() == "")
            //{ 
            //    string thisUrl = MyCommFun.getWebSite() + "/shop/cart.aspx?wid=" + wid;
            //    string newUrl = OAuthApi.GetAuthorizeUrl(wxModel.AppId, thisUrl, "reg", OAuthScope.snsapi_base);
            //    Response.Redirect(newUrl);
            //}
            //else
            //{
            //    var result = OAuthApi.GetAccessToken(wxModel.AppId, wxModel.AppSecret, code);
            //    openid = result.openid;
            //}
          
            string thisUrl = MyCommFun.getWebSite() + "/shop/cart.aspx?wid=" + wid;
            OAuth2BaseProc(wxModel, "cart", thisUrl);
            //授权结束


            serverPath = MyCommFun.GetRootPath() + "/shop/templates/" + templateFileName + "/cart.html";
            ShopTemplateMgr template = new ShopTemplateMgr("/shop/templates/" + templateFileName, serverPath, wid);
            template.tType = TemplateType.Cart;
            template.openid = openid;
            template.OutPutHtml(wid);
        }
    }
}