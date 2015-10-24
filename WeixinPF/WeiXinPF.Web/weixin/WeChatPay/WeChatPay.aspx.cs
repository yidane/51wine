using System;
using System.Web;
using OneGulp.WeChat.MP.Helpers;
using WeiXinPF.Common;
using WeiXinPF.WeiXinComm;

namespace WeiXinPF.Web.weixin.WeChatPay
{
    public partial class WeChatPay : System.Web.UI.Page
    {
        //注册微信支付脚本
        public int IsRegister = 0;
        public string appId = string.Empty;
        public string timestamp = string.Empty;
        public string nonceStr = string.Empty;
        public string signature = string.Empty;

        //支付参数
        public int wid = 0;
        public string body = string.Empty;
        public string payModuleID = string.Empty;
        public string out_trade_no = string.Empty;
        public int total_fee = 0;
        public string openid = string.Empty;
        public string OrderID = string.Empty;

        //事件
        public string BeforePay = string.Empty;
        public string PaySuccess = string.Empty;
        public string PayFail = string.Empty;
        public string PayCancel = string.Empty;
        public string PayComplete = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            var payData = Request.QueryString["payData"];
            var ticket = Request.QueryString["ticket"];

            if (!string.IsNullOrEmpty(payData) && !string.IsNullOrEmpty(ticket))
            {
                payData = EncryptionManager.AESDecrypt(payData, ticket);

                var payDataModel = JSONHelper.Deserialize<UnifiedOrderEntity>(payData);

                if (payDataModel != null)
                {
                    //赋值支付参数
                    wid = payDataModel.wid;
                    body = payDataModel.body;
                    payModuleID = payDataModel.PayModuleID.ToString();
                    out_trade_no = payDataModel.out_trade_no;
                    total_fee = payDataModel.total_fee;
                    openid = payDataModel.openid;
                    OrderID = payDataModel.OrderId;
                }
            }
            else
            {
                Response.Clear();
                Response.Write("不完整的参数");
            }
        }

        /// <summary>
        /// 注册支付脚本
        /// <param name="weixinId"></param>
        /// <param name="errorMessage"></param>
        /// </summary>
        public bool RegisterJWeiXin(int weixinId, out string errorMessage)
        {
            try
            {
                errorMessage = string.Empty;
                var ticket = WeiXinCRMComm.getJsApiTicket(weixinId, out errorMessage);
                if (!string.IsNullOrEmpty(errorMessage))
                    return false;

                var wxModel = new BLL.wx_userweixin().GetModel(weixinId);
                if (wxModel == null)
                {
                    errorMessage = "不合法的参数wid";
                    return false;
                }

                appId = wxModel.AppId;
                nonceStr = JSSDKHelper.GetNoncestr();
                timestamp = JSSDKHelper.GetTimestamp();
                signature = new JSSDKHelper().GetSignature(ticket, nonceStr, timestamp, Request.Url.ToString());

                IsRegister = 1;
                return true;
            }
            catch (Exception exception)
            {
                errorMessage = exception.Message;
                return false;
            }
        }
    }
}