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
using Travel.Application.DomainModules.WeChat.DTOS;
using Travel.Infrastructure.CommonFunctions.Ajax;
using Travel.Infrastructure.WeiXin.Advanced.Pay;
using Travel.Infrastructure.WeiXin.Advanced.Pay.Model;

namespace Travel.Services.WebService
{
    public class TicketWebService : BaseWebService
    {
        [WebMethod(EnableSession = true)]
        public void CreateOrder(string code, string ticketId, int ticketCount, string couponId, int couponCount, string orderNo, string contractName, string contractPhone, string contractIdCard)
        {
            try
            {
                AccessTokenDTO accessToken = null;
                var openID = string.Empty;
                if (Session["OpenID"] == null)
                {
                    accessToken = new WeChatService().GetAccessToken(code);
                    if (accessToken != null && !string.IsNullOrEmpty(accessToken.openid))
                    {
                        Session["OpenID"] = accessToken.openid;
                        openID = accessToken.openid;
                    }
                }
                else
                {
                    openID = Session["OpenID"].ToString();
                }

                if (!string.IsNullOrEmpty(openID))
                {
                    var orderRequestEntity = new OrderRequestEntity()
                        {
                            OpenId = openID,
                            TicketCategory = "956BFF5E-AA6A-454E-8F46-BD4175235C9E",
                            TicketName = "布尔津测试门票",
                            Count = ticketCount,
                            CouponId = string.Empty,
                            ContactPersonName = contractName,
                            MobilePhoneNumber = contractPhone,
                            IdentityCardNumber = contractIdCard
                        };

                    var otaOrder = new OTAOrder(orderRequestEntity);

                    //JsApiPay jsApiPay = new JsApiPay();
                    //UnifiedOrderRequest unifiedOrderRequest = new UnifiedOrderRequest();
                    //unifiedOrderRequest.body = "yidane Test body";
                    ////unifiedOrderRequest.openid = "obzTsw5qxlbwGYYZJC9b-91J-X1Y"; //Zidane
                    //unifiedOrderRequest.openid = openID;
                    ////unifiedOrderRequest.openid = "obzTswxzFzzzdWdAKf2mWx3CrpXk"; //xqfgbc
                    //unifiedOrderRequest.attach = "attach test";
                    //unifiedOrderRequest.total_fee = 1;
                    //unifiedOrderRequest.goods_tag = "goods_tag test";

                    //var result = jsApiPay.GetUnifiedOrderResult(unifiedOrderRequest);

                    otaOrder.CreateOrderMain();
                    var jsParameter = otaOrder.UnifiedOrderResult.GetJsApiParameters();

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


                    //var jsParameter = result.GetJsApiParameters();

                    //throw new Exception(AjaxResult.Success(jsParameter).ToString());

                    Context.Response.Write(AjaxResult.Success(jsParameter));
                }
                else
                {
                    throw new Exception("获取OpenID失败");
                }
            }
            catch (Exception exception)
            {
                Context.Response.Write(AjaxResult.Error(exception.Message));
            }
        }
    }
}