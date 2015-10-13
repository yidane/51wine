using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using System.Web.Services;
using WeiXinPF.Common;
using WeiXinPF.WeiXinComm;
using OneGulp.WeChat.MP.Helpers;

namespace WeiXinPF.Web.weixin.WeChatPay
{
    /// <summary>
    /// WeChatService 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    [System.Web.Script.Services.ScriptService]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
    // [System.Web.Script.Services.ScriptService]
    public class WeChatService : System.Web.Services.WebService
    {

        /// <summary>
        /// jsapi初始化
        /// </summary>
        /// <param name="wid"></param>
        /// <param name="url"></param>
        [WebMethod]
        public void WeChatConfigInit(int wid, string url)
        {
            string errorString;
            var ticket = WeiXinCRMComm.getJsApiTicket(wid, out errorString);
            if (!string.IsNullOrEmpty(errorString))
            {
                HttpContext.Current.Response.Write(AjaxResult.Error(errorString));
            }
            else
            {
                var wxModel = new BLL.wx_userweixin().GetModel(wid);
                if (wxModel == null)
                {
                    HttpContext.Current.Response.Write(AjaxResult.Error("不合法的参数wid"));
                    return;
                }

                var noncestr = JSSDKHelper.GetNoncestr();
                var timestamp = JSSDKHelper.GetTimestamp();
                var singature = new JSSDKHelper().GetSignature(ticket, noncestr, timestamp, url);

                var result = new
                    {
                        appId = wxModel.AppId,
                        timestamp = timestamp,
                        nonceStr = noncestr,
                        signature = singature
                    };

                HttpContext.Current.Response.Write(AjaxResult.Success(result));
            }
        }

        /// <summary>
        /// 统一下单
        /// </summary>
        /// <param name="request"></param>
        [WebMethod]
        public void UnifiedOrder(UnifiedOrderRequest request)
        {
            HttpContext.Current.Response.Write(AjaxResult.Success("yidane"));
        }
    }

    /// <summary>
    /// 统一下单对象
    /// </summary>
    [DataContract]
    public class UnifiedOrderRequest
    {
        [DataMember]
        public int wid { get; set; }
        [DataMember]
        public string body { get; set; }
        [DataMember]
        public string attach { get; set; }
        [DataMember]
        public string out_trade_no { get; set; }
        [DataMember]
        public int total_fee { get; set; }
        [DataMember]
        public string openid { get; set; }
    }
}
