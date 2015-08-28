using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Travel.Infrastructure.WeiXin.Advanced.Pay;
using Travel.Infrastructure.WeiXin.Advanced.Pay.Model;

namespace Travel.Infrastructure.WeiXin.Test
{
    [TestClass]
    public class WeChatPayUnitTest
    {
        [TestMethod]
        public void UnifiedGetUnifiedOrderResult()
        {
            JsApiPay jsApiPay = new JsApiPay();
            UnifiedOrderRequest unifiedOrderRequest = new UnifiedOrderRequest();
            unifiedOrderRequest.body = "yidane Test body";
            unifiedOrderRequest.openid = "obzTswxzFzzzdWdAKf2mWx3CrpXk";
            unifiedOrderRequest.attach = "attach test";
            unifiedOrderRequest.total_fee = 1;
            unifiedOrderRequest.goods_tag = "goods_tag test";
            unifiedOrderRequest.time_start = DateTime.Now.ToString("yyyyMMddHHmmss");
            unifiedOrderRequest.time_expire = DateTime.Now.AddMinutes(20).ToString("yyyyMMddHHmmss");


            jsApiPay.GetUnifiedOrderResult(unifiedOrderRequest);

            Assert.IsTrue(true);
        }
    }
}
