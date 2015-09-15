using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using Senparc.Weixin.MP;
using Senparc.Weixin.MP.AdvancedAPIs;
using Senparc.Weixin.MP.AdvancedAPIs.OAuth;
using Senparc.Weixin.MP.Helpers;
using WeiXinPF.BLL;
using WeiXinPF.Common;
using WeiXinPF.Model;

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

        #region 授权 获取openid 获取用户
        /// <summary>
        /// 授权判断获取openid，如果code为空页面跳转
        /// </summary>
        /// <param name="model"></param>
        /// <param name="state"></param>
        /// <param name="code">默认读取请求中的code属性</param>
        /// <param name="targetUrl">目标地址</param>
        protected string OAuth2BaseProc(Model.wx_userweixin model, string state, string code ,
            string targetUrl)
        {
            string openid = String.Empty;


#if DEBUG
            string isTest = MyCommFun.getAppSettingValue("isOAuthTest");
            if (isTest == "1")
            {
                openid = MyCommFun.RequestOpenid();
                return openid;
            } 
#endif

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

                //41008	缺少oauth code
                throw new JsonException(newUrl, "41008");
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
        protected OAuthUserInfo GetUser(int wid, string state,string code,string targetUrl)
        {
            OAuthUserInfo user = null;

#if DEBUG
            string isTest = MyCommFun.getAppSettingValue("isOAuthTest");

            if (isTest == "1")
            {
                user = new OAuthUserInfo()
                {
                    openid = "obzTsw4p1nhpl97G1xJwKicDNsiQ",
                    nickname = "谷巍",
                    sex = 1,
                    city = "广州",
                    province = "广东",
                    country = "中国",
                    headimgurl = "http://wx.qlogo.cn/mmopen/PiajxSqBRaEJrWO5PQSm2auIaHPNnTPCZAfAwNyXVsUADnVhInAYbkYo70cpvEujyOXBibwAUEGpULiafevgqQ9dg/0",

                };
                return user;
            }   
#endif

            string error;
            var accessToken = WeiXinPF.WeiXinComm.WeiXinCRMComm.getAccessToken(wid, out error);
            if (string.IsNullOrEmpty(error))
            {
                BLL.wx_userweixin bll = new BLL.wx_userweixin();
                Model.wx_userweixin wxModel = bll.GetModel(wid);
                var openId = OAuth2BaseProc(wxModel, state,code, targetUrl);

                user = OAuthApi.GetUserInfo(accessToken, openId);
            }
            else
            {
                throw new Exception(error);
            }
            return user;
        }
        #endregion



        #region 分享的jssdk代码

        /// <summary>
        /// 若不管用，请在公众号官方后台---功能设置----js接口安全域名 设置
        ///    初始化微信分享
        /// </summary>
        /// <param name="wxModel"></param>
        /// <param name="currentUrl">分享的目标url，如果传过来的值为空字符串或者为null则默认是当前的网址</param>
        public FxModel jssdkInit(Model.wx_userweixin wxModel, string currentUrl )
        {
            FxModel fxModel = new FxModel();
            fxModel.appId = wxModel.AppId;
            fxModel.timestamp = JSSDKHelper.GetTimestamp();
            fxModel.nonceStr = JSSDKHelper.GetNoncestr();
            fxModel.thisUrl = currentUrl;
            fxModel.fxUrl = fxModel.thisUrl;
           
            string error = "";
            string ticket = WeiXinPF.WeiXinComm.WeiXinCRMComm.getJsApiTicket(wxModel.id, out error);
            if (error != "")
            {  //取临时票据出现问题
                throw new Exception(error);
            }
            JSSDKHelper jsHelper = new JSSDKHelper();
            //获取签名
            var signature = jsHelper.GetSignature(ticket, fxModel.nonceStr, fxModel.timestamp, fxModel.thisUrl);

            fxModel.signature = signature;
            return fxModel;
        }//end:function
        //SHA1哈希加密算法  
        public string SHA1_Hash(string str_sha1_in)
        {
            SHA1 sha1 = new SHA1CryptoServiceProvider();
            byte[] bytes_sha1_in = System.Text.UTF8Encoding.Default.GetBytes(str_sha1_in);
            byte[] bytes_sha1_out = sha1.ComputeHash(bytes_sha1_in);
            string str_sha1_out = BitConverter.ToString(bytes_sha1_out);
            str_sha1_out = str_sha1_out.Replace("-", "").ToLower();
            return str_sha1_out;
        }


        #endregion
    }
}
