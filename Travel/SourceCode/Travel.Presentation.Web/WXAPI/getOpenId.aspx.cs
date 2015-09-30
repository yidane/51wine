using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Travel.Application.DomainModules.WeChat;
using Travel.Infrastructure.DomainDataAccess.Order;
using Travel.Infrastructure.WeiXin.Advanced.Pay;
using Travel.Infrastructure.WeiXin.Advanced.Pay.Model;
using System.Linq;

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
            //var refundRequest = new RefundOrderRequest();
            //var order = OrderEntity.GetOrderByOrderId(Guid.Parse("EAF0393F-772A-4AEE-8509-61AE873F7B7B"));
            //var refundTickets = TicketEntity.GetTicketsByOrderId(order.OrderId);
            //var refundFee = refundTickets.Sum(item => item.Price);

            //refundRequest.transaction_id = order.WXOrderCode;
            //refundRequest.out_refund_no = "T20150930124113579";
            //refundRequest.total_fee = decimal.ToInt32(order.TotalFee() * 100);
            //refundRequest.refund_fee = decimal.ToInt32(refundFee * 100);

            var refundRequest = new RefundOrderRequest
            {
                transaction_id = "1003800113201509301044597815",
                //out_trade_no = "C2015090204153143034787",
                total_fee = 26500,
                refund_fee = 26500,
                out_refund_no = "T2015090218440551242147"// WxPayHelper.GenerateOutTradeNo()
            };

            JsApiPay jsApiPay = new JsApiPay();
            var result = jsApiPay.Refund(refundRequest);
        }
    }
}