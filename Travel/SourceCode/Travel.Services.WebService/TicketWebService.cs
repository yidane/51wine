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
        public void CreateOrder(string code, string ticketCategoryId, string ticketName, int ticketCount, string couponId, int couponCount, string orderNo, string contractName, string contractPhone, string contractIdCard)
        {
            try
            {
                Session["OpenID"] = "obzTsw5qxlbwGYYZJC9b-91J-X1Y";

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
                            TicketCategory = ticketCategoryId,
                            TicketName = ticketName,
                            Count = ticketCount,
                            CouponId = string.Empty,
                            ContactPersonName = contractName,
                            MobilePhoneNumber = contractPhone,
                            IdentityCardNumber = contractIdCard
                        };

                    var otaOrder = new OTAOrder(orderRequestEntity);

                    otaOrder.CreateOrderMain();

                    var jsParameter = otaOrder.UnifiedOrderResult.GetJsApiParameters();

                    #region Test Code
                    //var jsApiPay = new JsApiPay();
                    //var unifiedOrderRequest = new UnifiedOrderRequest
                    //{
                    //    body = "yidane Test body",
                    //    openid = openID,
                    //    attach = "attach test",
                    //    total_fee = 1,
                    //    goods_tag = "goods_tag test"
                    //};

                    //var result = jsApiPay.GetUnifiedOrderResult(unifiedOrderRequest);
                    //var jsParameter = result.GetJsApiParameters();
                    #endregion

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