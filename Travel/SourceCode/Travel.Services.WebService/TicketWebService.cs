using System;
using System.Web;
using System.Web.Services;
using Travel.Application.DomainModules.Order.Core;
using Travel.Application.DomainModules.Order.Entity;
using Travel.Application.DomainModules.Order.Service;
using Travel.Infrastructure.CommonFunctions.Ajax;

namespace Travel.Services.WebService
{
    public class TicketWebService : BaseWebService
    {
        [WebMethod(EnableSession = true)]
        public void CreateOrder(string openId, string ticketCategoryId, string ticketName, int ticketCount,
            string couponId, int couponCount, string orderNo, string contractName, string contractPhone,
            string contractIdCard, DateTime preUseTime)
        {
            try
            {
                while (ticketName.Contains("%"))
                {
                    ticketName = HttpUtility.UrlDecode(ticketName);
                }

                if (IsOpenId(openId))
                {
                    var orderRequestEntity = new OrderRequestEntity
                    {
                        OpenId = openId,
                        TicketCategory = ticketCategoryId,
                        TicketName = ticketName,
                        Count = ticketCount,
                        CouponId = string.Empty,
                        ContactPersonName = contractName,
                        MobilePhoneNumber = contractPhone,
                        IdentityCardNumber = contractIdCard,
                        PreUseTime = preUseTime
                    };

                    var otaOrder = new OTAOrder(orderRequestEntity);

                    otaOrder.CreateOrderMain();

                    if (otaOrder.UnifiedOrderResult == null)
                        throw new Exception("门票已达今日限额");

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

                    var result = new
                    {
                        jsParameter.appId,
                        jsParameter.timeStamp,
                        jsParameter.nonceStr,
                        jsParameter.package,
                        jsParameter.paySign,
                        orderId = otaOrder.OrderObj.OrderId
                    };

                    Context.Response.Write(AjaxResult.Success(result));
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

        [WebMethod(EnableSession = true)]
        public void OrderRelease(string orderId)
        {
            //在支付失败或者支付有问题时候，从OTA中取消订单占用，撤销微信订单
            try
            {
                var orderService = new OrderService();
                orderService.OrderRelease(orderId);
            }
            catch (Exception exception)
            {
            }
        }
    }
}