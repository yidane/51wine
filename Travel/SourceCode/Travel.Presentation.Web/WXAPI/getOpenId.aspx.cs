using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Travel.Application.DomainModules.WeChat;
using Travel.Infrastructure.WeiXin.Advanced.Pay;
using Travel.Infrastructure.WeiXin.Advanced.Pay.Model;

namespace Travel.Presentation.Web.WXAPI
{
    public partial class getOpenId : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Write(string.Format("Page_Load{0}", "<br></br>"));

            if (Request.UrlReferrer != null)
                Response.Write(string.Format("{0}{1}", Request.UrlReferrer.ToString(), "<br></br>"));

            Response.Write(string.Format("{0}{1}", Request.Url.ToString(), "<br></br>"));

            foreach (string key in Request.QueryString.AllKeys)
            {
                Response.Write(string.Format("【{0}】【{1}】{2}", key, Request.QueryString[key], "<br></br>"));
            }

            var code = Request.QueryString["Code"];
            var weChatService = new WeChatService();
            var tokenResult = weChatService.GetAccessToken(code);
            Response.Write(Travel.Infrastructure.CommonFunctions.Ajax.AjaxResult.Success(tokenResult));
        }

        protected void btnRefund_Click(object sender, EventArgs e)
        {
            var refundOrderRequest = new RefundOrderRequest
            {
                out_trade_no = "C2015090204153143034787",
                total_fee = 1,
                refund_fee = 1,
                out_refund_no = WxPayHelper.GenerateOutTradeNo()
            };

            JsApiPay jsApiPay = new JsApiPay();
            var result = jsApiPay.Refund(refundOrderRequest);
        }
    }
}