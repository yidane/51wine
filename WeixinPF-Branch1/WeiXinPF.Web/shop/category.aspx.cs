﻿using WeiXinPF.Templates;
using System;
using System.Collections.Generic;
using WeiXinPF.Common;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WeiXinPF.Web.shop
{
    public partial class category : ShopBasePage
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

            BLL.wx_userweixin bll = new BLL.wx_userweixin();
            Model.wx_userweixin wxModel = bll.GetModel(wid);
            string thisUrl = MyCommFun.getTotalUrl();
            OAuth2BaseProc(wxModel, "cart", thisUrl);

            serverPath = MyCommFun.GetRootPath() + "/shop/templates/" + templateFileName + "/category.html";
            ShopTemplateMgr template = new ShopTemplateMgr("/shop/templates/" + templateFileName, serverPath, wid);
            template.tType = TemplateType.Index;
            template.openid = MyCommFun.RequestOpenid();
            template.OutPutHtml(wid);
        }
    }
}