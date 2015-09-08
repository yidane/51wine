﻿using WeiXinPF.Common;
using WeiXinPF.Templates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WeiXinPF.Web.portalpage
{
    public partial class weixin_join : PortalBasePage
    {
        override protected void OnInit(EventArgs e)
        {
            base.OnInit(e);
            if (errInitTemplates != "")
            {
                Response.Write(errInitTemplates);
                return;
            }


            tPath = MyCommFun.GetRootPath() + "/templates_portal/join.html";
            PortalTemplate template = new PortalTemplate(tPath);
            template.tType = TemplateType.News;

            template.OutPutHtml(templateIndexFileName);
        }
    }
}