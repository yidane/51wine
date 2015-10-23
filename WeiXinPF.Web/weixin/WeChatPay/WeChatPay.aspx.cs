using System;
using WeiXinPF.Common;
using WeiXinPF.WeiXinComm;

namespace WeiXinPF.Web.weixin.WeChatPay
{
    public partial class WeChatPay : System.Web.UI.Page
    {
        public int wid = 0;
        public string body = string.Empty;
        public string attach = string.Empty;
        public string out_trade_no = string.Empty;
        public int total_fee = 0;
        public string openid = string.Empty;

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
                    wid = payDataModel.wid;
                    body = payDataModel.body;
                    attach = payDataModel.attach;
                    out_trade_no = payDataModel.out_trade_no;
                    total_fee = payDataModel.total_fee;
                    openid = payDataModel.openid;
                }
                else
                {
                    Response.Clear();
                    Response.Write("无效的参数");
                }
            }
            else
            {
                Response.Clear();
                Response.Write("不完整的参数");
            }
        }
    }
}