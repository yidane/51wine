using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using Senparc.Weixin.MP;
using Senparc.Weixin.MP.AdvancedAPIs;
using Senparc.Weixin.MP.AdvancedAPIs.OAuth;
using WeiXinPF.BLL;
using WeiXinPF.Common;

namespace WeiXinPF.WebService
{
    public class BaseWebService : System.Web.Services.WebService
    {
        protected string GetOpenIDByCodeID(string code)
        {
            string openId;
            if (Session == null)
            {
                openId = GetOpenIDFromWeChat(code);
            }
            else
            {
#if DEBUG
                Session["OpenId"] = "obzTswxzFzzzdWdAKf2mWx3CrpXk";
#endif
                if (Session["OpenID"] == null)
                {
                    openId = GetOpenIDFromWeChat(code);
                    Session["OpenID"] = openId;
                }
                else
                {
                    openId = Session["OpenID"].ToString();
                }
            }

            if (string.IsNullOrEmpty(openId))
                throw new Exception("获取OpenID失败");

            return openId;
        }

        private string GetOpenIDFromWeChat(string code)
        {
            //todo:add wechatservice
            //var accessToken = new WeChatService().GetAccessToken(code);
            //return accessToken.openid;
          
            return string.Empty;
        }

        /// <summary>
        /// 授权判断获取openid，如果code为空页面跳转
        /// </summary>
        /// <param name="model"></param>
        /// <param name="state"></param>
        /// <param name="code">默认读取请求中的code属性</param>
        /// <param name="targetUrl">目标地址</param>
        protected string OAuth2BaseProc(Model.wx_userweixin model, string state,string code="",
            string targetUrl=null)
        {
            string openid=String.Empty;
            string isTest = MyCommFun.getAppSettingValue("isOAuthTest");

            if (isTest == "1")
            {
                openid = MyCommFun.RequestOpenid();
                return openid;
            }

            //如果不传
            if (string.IsNullOrEmpty(code))
            {
                code = MyCommFun.QueryString("code");
            }
            if (code == null || code.Trim() == "")
            {
                if (targetUrl == null || targetUrl.Trim() == "")
                {
                    targetUrl = MyCommFun.getTotalUrl();
                }
                //thisUrl = MyCommFun.getWebSite() + "/weixin/huodong/index.aspx";
                string newUrl = OAuthApi.GetAuthorizeUrl(model.AppId, targetUrl, state, OAuthScope.snsapi_base);
             HttpContext.Current.Response.Redirect(newUrl);
            }
            else
            {
                
                if (MyCommFun.getCookie("cookie_openid") == "")
                {
                    OAuthAccessTokenResult result = OAuthApi.GetAccessToken(model.AppId, model.AppSecret, code);
                    openid = result.openid;
                    MyCommFun.setCookie("cookie_openid", openid, 100);
                }
                openid = MyCommFun.getCookie("cookie_openid");

            }

            return openid;
        }

        /// <summary>
        /// 获取用户基本信息
        /// </summary>
        /// <param name="wid"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        protected OAuthUserInfo GetUser(int wid, string state)
        {
            OAuthUserInfo user = null;
            string error;
            var accessToken = WeiXinPF.WeiXinComm.WeiXinCRMComm.getAccessToken(wid, out error);
            if (string.IsNullOrEmpty(error))
            {
                BLL.wx_userweixin bll = new BLL.wx_userweixin();
                Model.wx_userweixin wxModel = bll.GetModel(wid);
                var openId = OAuth2BaseProc(wxModel, state);
                
                user = OAuthApi.GetUserInfo(accessToken, openId);
            }
            else
            {
                throw new Exception(error);
            }
            return user;
        }
    }
}
