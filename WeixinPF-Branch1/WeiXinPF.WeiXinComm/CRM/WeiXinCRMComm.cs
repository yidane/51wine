using WeiXinPF.BLL;
using OneGulp.WeChat.MP.AdvancedAPIs;
using OneGulp.WeChat.MP.AdvancedAPIs.User;
using OneGulp.WeChat.MP.CommonAPIs;
using OneGulp.WeChat.MP.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WeiXinPF.WeiXinComm
{
    public class WeiXinCRMComm
    {
        private WeiXinCRMComm() { }



        /// <summary>
        /// 及时获得access_token值
        /// access_token是公众号的全局唯一票据，公众号调用各接口时都需使用access_token。正常情况下access_token有效期为7200秒，
        /// 重复获取将导致上次获取的access_token失效。
        /// 每日限额获取access_token.我们将access_token保存到数据库里，间隔时间为20分钟，从微信公众平台获得一次。
        /// </summary>
        /// <returns></returns>
        public static string getAccessToken(int wid, out string error)
        {
            string token = "";
            error = "";
            try
            {
                wx_property_info pBll = new wx_property_info();
                BLL.wx_userweixin wBll = new wx_userweixin();

                Model.wx_userweixin weixininfo = wBll.GetModel(wid);
                if (weixininfo.AppId == null || weixininfo.AppSecret == null
                    || weixininfo.AppId.Trim().Length <= 0 || weixininfo.AppSecret.Trim().Length <= 0)
                {
                    error = "appId或者AppSecret未填写完全,请在[我的公众帐号]里补全信息！";
                    WXLogs.AddLog(wid, "access_token", "获得access_token", error);
                    return "";
                }
                WeiXinPF.Model.wx_property_info wxProperty = new WeiXinPF.Model.wx_property_info();
                //第一次插入微信属性表
                if (!pBll.ExistsWid(wid, "access_token"))
                {


                    AccessTokenResult result = CommonApi.GetToken(weixininfo.AppId, weixininfo.AppSecret);
                    token = result.access_token;
                    pBll.AddAccess_Token(wid, token, result.expires_in);
                    return token;

                    //WeChatAccountManager manager = WeChatAccountManager.CreateInstance(weixininfo.AppId, weixininfo.AppSecret);
                    //Credential credential = new Credential(manager);
                    //token = credential.AccessToken;
                    //pBll.AddAccess_Token(wid, token, 7000);
                    //return token;
                }

                wxProperty = pBll.GetModelList("iName='access_token' and wid=" + wid)[0];
                double chajunSecond = (DateTime.Now - wxProperty.createDate.Value).TotalSeconds;

                if (chajunSecond >= wxProperty.expires_in)
                {
                    //从微信平台重新获得access_token
                    AccessTokenResult result = CommonApi.GetToken(weixininfo.AppId, weixininfo.AppSecret);
                    token = result.access_token;
                    //更新到数据库里
                    wxProperty.iContent = token;
                    wxProperty.createDate = DateTime.Now;
                    wxProperty.expires_in = result.expires_in;

                    //WeChatAccountManager manager = WeChatAccountManager.CreateInstance(weixininfo.AppId, weixininfo.AppSecret);
                    //Credential credential = new Credential(manager);
                    //token = credential.AccessToken;
                    ////更新到数据库里
                    //wxProperty.iContent = token;
                    //wxProperty.createDate = DateTime.Now;
                    //wxProperty.expires_in = 7000;
                    //pBll.Update(wxProperty);

                }
                else
                {
                    token = wxProperty.iContent;
                }


            }
            catch (Exception ex)
            {
                error = "获得access_token出错:" + ex.Message;
                WXLogs.AddLog(wid, "access_token", "获得access_token", error);
            }
            return token;
        }





        /// <summary>
        ///【强制刷新】access_token值
        /// access_token是公众号的全局唯一票据，公众号调用各接口时都需使用access_token。正常情况下access_token有效期为7200秒，
        /// 重复获取将导致上次获取的access_token失效。
        /// 每日限额获取access_token.我们将access_token保存到数据库里，间隔时间为20分钟，从微信公众平台获得一次。
        /// </summary>
        /// <returns></returns>
        public static string FlushAccessToken(int wid, out string error)
        {
            string token = "";
            error = "";
            try
            {
                wx_property_info pBll = new wx_property_info();
                BLL.wx_userweixin wBll = new wx_userweixin();

                Model.wx_userweixin weixininfo = wBll.GetModel(wid);
                if (weixininfo.AppId == null || weixininfo.AppSecret == null || weixininfo.AppId.Trim().Length <= 0 || weixininfo.AppSecret.Trim().Length <= 0)
                {
                    error = "appId或者AppSecret未填写完全,请在[我的公众帐号]里补全信息！";
                    return "";
                }

                var result = CommonApi.GetToken(weixininfo.AppId, weixininfo.AppSecret);
                token = result.access_token;


                //第一次插入微信属性表
                if (!pBll.ExistsWid(wid, "access_token"))
                {
                    //插入
                    pBll.AddAccess_Token(wid, token, result.expires_in);

                }
                else
                {
                    WeiXinPF.Model.wx_property_info wxProperty = new WeiXinPF.Model.wx_property_info();
                    wxProperty = pBll.GetModelList("iName='access_token' and wid=" + wid)[0];
                    //更新到数据库里
                    wxProperty.iContent = token;
                    wxProperty.createDate = DateTime.Now;
                    wxProperty.expires_in = result.expires_in;
                    pBll.Update(wxProperty);

                }

            }
            catch (Exception ex)
            {
                error = "获得access_token出错:" + ex.Message;
                WXLogs.AddLog(wid, "access_token", "获得access_token(FlushAccessToken)", error);
            }

            return token;
        }

        /// <summary>
        /// 获取jssdk 里的临时票据
        /// </summary>
        /// <param name="wid"></param>
        /// <param name="error"></param>
        /// <returns></returns>
        public static string getJsApiTicket(int wid, out string error)
        {
            string atErr = "";
            string accessToken = getAccessToken(wid, out atErr);
            if (atErr != "")
            {
                accessToken = FlushAccessToken(wid, out atErr);
            }
            if (accessToken == "")
            {
                error = "取accessToken值出现异常";
                WXLogs.AddLog(wid, "getJsApiTicket", "获得getJsApiTicket", "WeiXinPF.WeiXinComm.getJsApiTicket" + error);
                return "";
            }

            string token = "";
            error = "";
            try
            {
                wx_property_info pBll = new wx_property_info();
                BLL.wx_userweixin wBll = new wx_userweixin();

                Model.wx_userweixin weixininfo = wBll.GetModel(wid);
                if (weixininfo.AppId == null || weixininfo.AppSecret == null || weixininfo.AppId.Trim().Length <= 0 || weixininfo.AppSecret.Trim().Length <= 0)
                {
                    error = "appId或者AppSecret未填写完全,请在[我的公众帐号]里补全信息！";
                    return "";
                }
                WeiXinPF.Model.wx_property_info wxProperty = new WeiXinPF.Model.wx_property_info();
                //第一次插入微信属性表
                if (!pBll.ExistsWid(wid, "JsApiTicket"))
                {
                    string type = "jsapi";
                    var url = string.Format("https://api.weixin.qq.com/cgi-bin/ticket/getticket?access_token={0}&type={1}",
                                      accessToken, type);

                    JsApiTicketResult result = OneGulp.WeChat.HttpUtility.Get.GetJson<JsApiTicketResult>(url);
                    token = result.ticket;
                    //存入属性表
                    wxProperty.wid = wid;
                    wxProperty.iName = "JsApiTicket";
                    wxProperty.iContent = token;
                    wxProperty.createDate = DateTime.Now;
                    wxProperty.expires_in = result.expires_in;
                    wxProperty.count = 1;
                    pBll.Add(wxProperty);

                    return token;
                }

                wxProperty = pBll.GetModelList("iName='JsApiTicket' and wid=" + wid)[0];
                double chajunSecond = (DateTime.Now - wxProperty.createDate.Value).TotalSeconds;

                if (chajunSecond >= wxProperty.expires_in)
                {  //从微信平台重新获得access_token
                    string type = "jsapi";
                    var url = string.Format("https://api.weixin.qq.com/cgi-bin/ticket/getticket?access_token={0}&type={1}",
                                      accessToken, type);

                    JsApiTicketResult result = OneGulp.WeChat.HttpUtility.Get.GetJson<JsApiTicketResult>(url);
                    token = result.ticket;

                    //更新到数据库里
                    wxProperty.iContent = token;
                    wxProperty.createDate = DateTime.Now;
                    wxProperty.expires_in = result.expires_in;
                    pBll.Update(wxProperty);

                }
                else
                {
                    token = wxProperty.iContent;
                }


            }
            catch (Exception ex)
            {
                error = "获得getJsApiTicket出错:" + ex.Message;
                WXLogs.AddLog(wid, "getJsApiTicket", "获得getJsApiTicket", "WeiXinPF.WeiXinComm.getJsApiTicket" + error);
            }
            return token;
        }

    }
}
