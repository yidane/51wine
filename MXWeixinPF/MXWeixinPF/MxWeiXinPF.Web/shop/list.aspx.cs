using MxWeiXinPF.Templates;
using System;
using System.Collections.Generic;
using MxWeiXinPF.Common;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Senparc.Weixin.MP.AdvancedAPIs;
using System.Data;
using Senparc.Weixin.MP.AdvancedAPIs.OAuth;
using Senparc.Weixin.MP;

namespace MxWeiXinPF.Web.shop
{
    public partial class list : ShopBasePage
    {
        override protected void OnInit(EventArgs e)
        {
            base.OnInit(e);
            if (errInitTemplates != "")
            {
                Response.Write(errInitTemplates);
                return;
            }

            //1获得模版基本信息04e11d01b9df2865
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
          
            string type=MyCommFun.QueryString("type");
            if (type != "")
            {
                type = "&type=new";
            }

            string thisUrl = MyCommFun.getWebSite() + "/shop/list.aspx?wid=" + wid + "&cid=" + MyCommFun.RequestInt("cid") + type;
            OAuth2BaseProc(wxModel, "list", thisUrl);
            //授权结束


            serverPath = MyCommFun.GetRootPath() + "/shop/templates/" + templateFileName + "/list.html";
            ShopTemplateMgr template = new ShopTemplateMgr("/shop/templates/" + templateFileName, serverPath, wid);
            template.tType = TemplateType.Class;
            template.openid = openid;
            template.OutPutHtml(wid);
            DataSet ds = new DataSet();

        }
    }
}