using WeiXinPF.BLL;
using WeiXinPF.Common;
using WeiXinPF.Model;
using Senparc.Weixin.MP;
using Senparc.Weixin.MP.AdvancedAPIs;
using Senparc.Weixin.MP.CommonAPIs;
using Senparc.Weixin.MP.TenPayLibV3;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;

//1e2124dd04e11d01b9df2865f85944be
namespace WeiXinPF.Web.UI
{
    public class WeiXinPage : System.Web.UI.Page
    {

        //分享js的参数
        public FxModel fxModel = new FxModel();
        public string openid = "";
        public string mywebSite = MyCommFun.getWebSite();
        /// <summary>
        /// 只允许微信里访问
        /// </summary>
        /// <param name="flg"></param>
        public void OnlyWeiXinLook()
        {

            string value = BLL.wx_sysConfig.GetConfigValue("onlyweixinlook");
            if (value.ToLower() == "true")
            {
                String userAgent = Request.UserAgent;
                if (userAgent.IndexOf("MicroMessenger") <= -1)
                {
                    Response.Write("请在微信浏览器里访问");
                    Response.End();

                }
            }


        }

        public Model.wxcodeconfig getwebsiteinfo(int wid)
        {
            wsiteBll wBll = new wsiteBll();
            return wBll.GetModelByWid(wid, "");
        }

        /// <summary>
        /// 获得版权信息
        /// </summary>
        /// <param name="wid"></param>
        /// <returns></returns>
        public string getwebcopyright(int wid)
        {
            wsiteBll wBll = new wsiteBll();
            Model.wxcodeconfig config = wBll.GetModelByWid(wid, "");
            if (config != null)
            {
                return config.wcopyright;
            }
            else
            {
                return "";
            }


        }



       /// <summary>
        /// 1 授权判断，页面跳转;
        /// 2 获取openid
       /// </summary>
       /// <param name="model"></param>
       /// <param name="state"></param>
        /// <param name="targetUrl">目标地址</param>
        public void OAuth2BaseProc(Model.wx_userweixin model, string state, string targetUrl)
        {
            string isTest = MyCommFun.getAppSettingValue("isOAuthTest");
            if (isTest == "1")
            {
                openid = MyCommFun.RequestOpenid();
                return;
            }

            string code = MyCommFun.QueryString("code");
            if (code == null || code.Trim() == "")
            {
                if (targetUrl == null || targetUrl.Trim() == "")
                {
                    targetUrl = MyCommFun.getTotalUrl();
                }
                //thisUrl = MyCommFun.getWebSite() + "/weixin/huodong/index.aspx";
                string newUrl = OAuthApi.GetAuthorizeUrl(model.AppId, targetUrl, state, OAuthScope.snsapi_base);
                Response.Redirect(newUrl);
            }
            else
            {
                if (MyCommFun.getCookie("cookie_openid") == "")
                {
                    var result = OAuthApi.GetAccessToken(model.AppId, model.AppSecret, code);
                    openid = result.openid;
                    MyCommFun.setCookie("cookie_openid", openid, 100);
                }
                openid = MyCommFun.getCookie("cookie_openid");
               
            }

        }



        #region 分享的jssdk代码

        /// <summary>
        /// 若不管用，请在公众号官方后台---功能设置----js接口安全域名 设置
        ///    初始化微信分享
        /// </summary>
        /// <param name="wxModel"></param>
        /// <param name="fxUrl">分享的目标url，如果传过来的值为空字符串或者为null则默认是当前的网址</param>
        public void jssdkInit(Model.wx_userweixin wxModel, string fxUrl)
        {

            fxModel.appid = wxModel.AppId;
            fxModel.timestamp = TenPayV3Util.GetTimestamp();
            fxModel.nonce = TenPayV3Util.GetNoncestr();
            fxModel.thisUrl = HttpContext.Current.Request.Url.ToString();
            // fxModel.fxUrl = MyCommFun.getWebSite() + "/weixin/huodong/index.aspx";
            if (fxUrl == null || fxUrl.Trim() == "")
            {
                fxModel.fxUrl = fxModel.thisUrl;
            }
            else
            {
                fxModel.fxUrl = fxUrl;
            }
            JsApiTicketContainer.Register(wxModel.AppId, wxModel.AppSecret);
            var ticketResult = JsApiTicketContainer.GetTicketResult(wxModel.AppId); //获取Ticket完整结果（包括当前过期秒数）

            // 这里参数的顺序要按照 key 值 ASCII 码升序排序  
            string rawstring = "jsapi_ticket=" + ticketResult.ticket + "&noncestr=" + fxModel.nonce + "&timestamp=" + fxModel.timestamp + "&url=" + fxModel.thisUrl + "";
            fxModel.signature = SHA1_Hash(rawstring);
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
