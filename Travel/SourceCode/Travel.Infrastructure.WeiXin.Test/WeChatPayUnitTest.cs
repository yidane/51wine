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
            unifiedOrderRequest.openid = "obzTsw5qxlbwGYYZJC9b-91J-X1Y";
            unifiedOrderRequest.attach = "attach test";
            unifiedOrderRequest.total_fee = 1;
            unifiedOrderRequest.goods_tag = "goods_tag test";
            //unifiedOrderRequest.time_start = DateTime.Now.ToString("yyyyMMddHHmmss");
            //unifiedOrderRequest.time_expire = DateTime.Now.AddMinutes(20).ToString("yyyyMMddHHmmss");


            var result = jsApiPay.GetUnifiedOrderResult(unifiedOrderRequest);

            Assert.IsTrue(true);
        }

        [TestMethod]
        public void UnifiedGetUnifiedOrderResultCreateParameter()
        {
            JsApiPay jsApiPay = new JsApiPay();
            UnifiedOrderRequest unifiedOrderRequest = new UnifiedOrderRequest();
            unifiedOrderRequest.body = "yidane Test body";
            unifiedOrderRequest.openid = "obzTsw5qxlbwGYYZJC9b-91J-X1Y";
            unifiedOrderRequest.attach = "attach test";
            unifiedOrderRequest.total_fee = 1;
            unifiedOrderRequest.goods_tag = "goods_tag test";
            //unifiedOrderRequest.time_start = DateTime.Now.ToString("yyyyMMddHHmmss");
            //unifiedOrderRequest.time_expire = DateTime.Now.AddMinutes(20).ToString("yyyyMMddHHmmss");


            var result = jsApiPay.GetUnifiedOrderResult(unifiedOrderRequest);

            var p = result.GetJsApiParameters();

            Assert.IsTrue(true);
        }
    }
}
