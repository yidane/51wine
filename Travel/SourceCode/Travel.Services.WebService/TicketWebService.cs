using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Services;
using Travel.Application.DomainModules.Order.Core;
using Travel.Application.DomainModules.Order.Entity;
using Travel.Application.DomainModules.WeChat;
using Travel.Infrastructure.CommonFunctions.Ajax;
using Travel.Infrastructure.WeiXin.Advanced.Pay;
using Travel.Infrastructure.WeiXin.Advanced.Pay.Model;

namespace Travel.Services.WebService
{
    public class TicketWebService : BaseWebService
    {
        [WebMethod]
        public void CreateOrder(string code, string ticketId, int ticketCount, string couponId, int couponCount, string orderNo, string contractName, string contractPhone, string contractIdCard)
        {
            try
            {
                ///var accessToken = new WeChatService().GetAccessToken(code);
                //if (!string.IsNullOrEmpty(accessToken.openid))
                {
                    //var orderRequestEntity = new OrderRequestEntity()
                    //    {
                    //        OpenId = accessToken.openid,
                    //        TicketCategory = Guid.NewGuid().ToString(),
                    //        Count = ticketCount,
                    //        CouponId = Guid.NewGuid().ToString(),
                    //        ContactPersonName = contractName,
                    //        MobilePhoneNumber = contractPhone,
                    //        IdentityCardNumber = contractIdCard
                    //    };

                    //var otaOrder = new OTAOrder(orderRequestEntity);

                    JsApiPay jsApiPay = new JsApiPay();
                    UnifiedOrderRequest unifiedOrderRequest = new UnifiedOrderRequest();
                    unifiedOrderRequest.body = "yidane Test body";
                    unifiedOrderRequest.openid = "obzTswxzFzzzdWdAKf2mWx3CrpXk";
                    //unifiedOrderRequest.openid = accessToken.openid;
                    unifiedOrderRequest.attach = "attach test";
                    unifiedOrderRequest.total_fee = 1;
                    unifiedOrderRequest.goods_tag = "goods_tag test";
                    unifiedOrderRequest.time_start = DateTime.Now.ToString("yyyyMMddHHmmss");
                    unifiedOrderRequest.time_expire = DateTime.Now.AddMinutes(10).ToString("yyyyMMddHHmmss");

                    var result = jsApiPay.GetUnifiedOrderResult(unifiedOrderRequest);


                    //UnifiedOrderResult result = new UnifiedOrderResult();
                    //result.return_code = "SUCCESS";
                    //result.return_msg = "OK";
                    //result.appid = "wxdd6127bdb5e7611c";
                    //result.mch_id = "1266087601";
                    //result.nonce_str = "vHtgBVFIVNrQpmux";
                    //result.sign = "11ED616A6FE93377F62F15278612F209";
                    //result.result_code = "SUCCESS";
                    //result.prepay_id = "prepay_id=wx20150828204046a93fc0882f0359646391";
                    //result.trade_type = "JSAPI";

                    //otaOrder.CreateOrderMain();

                    var jsParameter = result.GetJsApiParameters();

                    Context.Response.Write(AjaxResult.Success(jsParameter));
                }
                //else
                //{
                //    throw new Exception("无效参数Code");
                //}
            }
            catch (Exception exception)
            {
                Context.Response.Write(AjaxResult.Error(exception.Message));
            }
        }
    }
}