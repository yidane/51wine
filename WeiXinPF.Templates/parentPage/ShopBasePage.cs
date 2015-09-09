
using WeiXinPF.Common;
using Senparc.Weixin.MP;
using Senparc.Weixin.MP.AdvancedAPIs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;


namespace WeiXinPF.Templates
{
    public class ShopBasePage : System.Web.UI.Page
    {
        /// <summary>
        /// 模版的物理路径
        /// </summary>
        public string serverPath { get; set; }

        /// <summary>
        /// 模版的虚拟路径，比如/shop/templates/default
        /// </summary>
        public string tPath { get; set; }

        /// <summary>
        /// 模版文件名称
        /// </summary>
        public string templateFileName { get; set; }

        /// <summary>
        /// 微帐号wid
        /// </summary>
        public int wid { get; set; }

        public string openid = "";

        /// <summary>
        /// 初始化模版的错误信息
        /// </summary>
        public string errInitTemplates { get; set; }

        public ShopBasePage()
        {
            serverPath = "";
            templateFileName = "";
            errInitTemplates = "";
            wid = MyCommFun.RequestInt("wid");
            if (wid == 0)
            {
                errInitTemplates = "链接地址或者参数错误！";
                return;
            }
            BLL.wx_userweixin wuwBll = new BLL.wx_userweixin();
            bool isExist = wuwBll.wxCodeLegal(wid);
            if (!isExist)
            {
                errInitTemplates = "账号已过期或已被禁用！";
                return;

            }


        }

        /// <summary>
        /// 授权判断，页面跳转
        /// </summary>
        /// <param name="state"></param>
        /// <param name="targetUrl">目标地址</param>
        public void OAuth2BaseProc(Model.wx_userweixin model, string state, string targetUrl)
        {
            BLL.wx_property_info propertyBll = new BLL.wx_property_info();
            Model.wx_property_info propertyEntity = propertyBll.GetModelByIName(wid, MXEnums.WXPropertyKeyName.OpenOauth.ToString());
            if (propertyEntity.iContent == "0")
            {
                //关闭了网页授权
                openid = MyCommFun.RequestOpenid();
            }
            else
            {
                string code = MyCommFun.QueryString("code");
                if (code == null || code.Trim() == "")
                {
                    if (targetUrl == null || targetUrl.Trim() == "")
                    {
                        targetUrl = MyCommFun.getTotalUrl();
                    }
                    //thisUrl = MyCommFun.getWebSite() + "/weixin/huodong/index.aspx";
                    string newUrl = OAuthApi.GetAuthorizeUrl(model.AppId, targetUrl, state, OAuthScope.snsapi_base);
                    // WXLogs.AddLog(model.id, "OAuth授权跳转页面", "获得OAuth2BaseProc", newUrl);
                    Response.Redirect(newUrl);
                }
                else
                {
                    var result = OAuthApi.GetAccessToken(model.AppId, model.AppSecret, code);
                    openid = result.openid;
                }
            }

        }



    }
}
