using MxWeiXinPF.Templates;
using System;
using System.Collections.Generic;
using MxWeiXinPF.Common;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Senparc.Weixin.MP;
using Senparc.Weixin.MP.AdvancedAPIs;
//04e11d01b9df2865
namespace MxWeiXinPF.Web.shop
{
    public partial class detail : ShopBasePage
    {
        override protected void OnInit(EventArgs e)
        {
            base.OnInit(e);
            if (errInitTemplates != "")
            {
                Response.Write(errInitTemplates);
                return;
            }

            //授权
            BLL.wx_userweixin bll = new BLL.wx_userweixin();
            Model.wx_userweixin wxModel = bll.GetModel(wid);
            
            string thisUrl = MyCommFun.getTotalUrl();
            OAuth2BaseProc(wxModel, "detail", thisUrl);

            //授权结束

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


            serverPath = MyCommFun.GetRootPath() + "/shop/templates/" + templateFileName + "/detail.html";
            ShopTemplateMgr template = new ShopTemplateMgr("/shop/templates/" + templateFileName, serverPath, wid);
            template.tType = TemplateType.News;
            template.openid = openid;
            template.OutPutHtml(wid);
        }
    }

    /**************************************
 *
 * author:李朴
 * company:上海沐雪网络科技有限公司
 * qq:23002807
 * website:http://uweixin.cn
 * taobao:https://item.taobao.com/item.htm?spm=686.1000925.0.0.5HYEHQ&id=520523216527  
 * createDate:2013-11-1
 * update:2014-12-30
 * 
 ***********************************/

}