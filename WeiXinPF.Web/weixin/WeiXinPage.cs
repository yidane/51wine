using WeiXinPF.BLL;
using WeiXinPF.Common;
using WeiXinPF.Model;
using Senparc.Weixin.MP;
using Senparc.Weixin.MP.AdvancedAPIs;
using Senparc.Weixin.MP.CommonAPIs;
using Senparc.Weixin.MP.Helpers;
using Senparc.Weixin.MP.TenPayLibV3;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;

namespace WeiXinPF.Web.weixin
{
    /// <summary>
    /// 前端微信第3方页面的父级页面
    /// author:李　朴
    /// datetime:2015-8-8
    /// 
    /// </summary>
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
            Model.wxcodeconfig config= wBll.GetModelByWid(wid, "");
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
        /// 授权判断，页面跳转
        /// </summary>
        /// <param name="state"></param>
        /// <param name="targetUrl">目标地址</param>
        public void OAuth2BaseProc(Model.wx_userweixin model, string state, string targetUrl)
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



        #region 分享的jssdk代码

        /// <summary>
        /// 若不管用，请在公众号官方后台---功能设置----js接口安全域名 设置
        ///    初始化微信分享
        /// </summary>
        /// <param name="wxModel"></param>
        /// <param name="fxUrl">分享的目标url，如果传过来的值为空字符串或者为null则默认是当前的网址</param>
        public void jssdkInit(Model.wx_userweixin wxModel, string fxUrl="")
        {

            fxModel.appid = wxModel.AppId;
            fxModel.timestamp = JSSDKHelper.GetTimestamp();
            fxModel.nonce = JSSDKHelper.GetNoncestr();
            fxModel.thisUrl = HttpContext.Current.Request.Url.ToString();
          
            if (fxUrl == null || fxUrl.Trim() == "")
            {
                fxModel.fxUrl = fxModel.thisUrl;
            }
            else
            {
                fxModel.fxUrl = fxUrl;
            }
            string error="";
            string ticket = WeiXinPF.WeiXinComm.WeiXinCRMComm.getJsApiTicket(wxModel.id,out  error);
            if (error != "")
            {  //取临时票据出现问题

                return;
            }
            JSSDKHelper jsHelper = new JSSDKHelper();
            //获取签名
            var signature = jsHelper.GetSignature(ticket, fxModel.nonce, fxModel.timestamp, fxModel.thisUrl);

            fxModel.signature = signature;
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