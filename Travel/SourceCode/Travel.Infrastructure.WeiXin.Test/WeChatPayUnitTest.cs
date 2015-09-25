using System;
using System.IO;
using System.Net;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Travel.Infrastructure.WeiXin.Advanced.Pay;
using Travel.Infrastructure.WeiXin.Advanced.Pay.Model;

namespace Travel.Infrastructure.WeiXin.Test
{
    [TestClass]
    public class WeChatPayUnitTest
    {
        [TestMethod]
        public void GetUnifiedOrderResultTest()
        {
            var jsApiPay = new JsApiPay();
            var unifiedOrderRequest = new UnifiedOrderRequest
                {
                    body = "yidane Test body",
                    openid = "obzTsw5qxlbwGYYZJC9b-91J-X1Y",
                    out_trade_no = WxPayHelper.GenerateOutTradeNo(),
                    attach = "attach test",
                    total_fee = 1,
                    goods_tag = "goods_tag test"
                };
            //unifiedOrderRequest.time_start = DateTime.Now.ToString("yyyyMMddHHmmss");
            //unifiedOrderRequest.time_expire = DateTime.Now.AddMinutes(20).ToString("yyyyMMddHHmmss");


            var result = jsApiPay.GetUnifiedOrderResult(unifiedOrderRequest);

            Assert.IsTrue(true);
        }

        [TestMethod]
        public void GetUnifiedOrderResultCreateParameterTest()
        {
            var jsApiPay = new JsApiPay();
            var unifiedOrderRequest = new UnifiedOrderRequest
                {
                    body = "yidane Test body",
                    openid = "obzTsw5qxlbwGYYZJC9b-91J-X1Y",
                    out_trade_no = WxPayHelper.GenerateOutTradeNo(),
                    attach = "attach test",
                    total_fee = 1,
                    goods_tag = "goods_tag test"
                };
            //unifiedOrderRequest.time_start = DateTime.Now.ToString("yyyyMMddHHmmss");
            //unifiedOrderRequest.time_expire = DateTime.Now.AddMinutes(20).ToString("yyyyMMddHHmmss");


            var result = jsApiPay.GetUnifiedOrderResult(unifiedOrderRequest);

            var p = result.GetJsApiParameters();

            Assert.IsTrue(true);
        }

        [TestMethod]
        public void NotifyTest()
        {
            try
            {
                var notifyUrl = "http://localhost:43512/Message/MessageNotify.aspx";
                string postData = @"<xml>
  <appid><![CDATA[wx2421b1c4370ec43b]]></appid>
  <attach><![CDATA[支付测试]]></attach>
  <bank_type><![CDATA[CFT]]></bank_type>
  <fee_type><![CDATA[CNY]]></fee_type>
  <is_subscribe><![CDATA[Y]]></is_subscribe>
  <mch_id><![CDATA[10000100]]></mch_id>
  <nonce_str><![CDATA[5d2b6c2a8db53831f7eda20af46e531c]]></nonce_str>
  <openid><![CDATA[oUpF8uMEb4qRXf22hE3X68TekukE]]></openid>
  <out_trade_no><![CDATA[1409811653]]></out_trade_no>
  <result_code><![CDATA[SUCCESS]]></result_code>
  <return_code><![CDATA[SUCCESS]]></return_code>
  <sign><![CDATA[B552ED6B279343CB493C5DD0D78AB241]]></sign>
  <sub_mch_id><![CDATA[10000100]]></sub_mch_id>
  <time_end><![CDATA[20140903131540]]></time_end>
  <total_fee>1</total_fee>
  <trade_type><![CDATA[JSAPI]]></trade_type>
  <transaction_id><![CDATA[1004400740201409030005092168]]></transaction_id>
</xml>"; // 要发放的数据 
                byte[] byteArray = Encoding.UTF8.GetBytes(postData);

                HttpWebRequest objWebRequest = (HttpWebRequest)WebRequest.Create(notifyUrl);
                objWebRequest.Method = "POST";
                objWebRequest.ContentType = "application/x-www-form-urlencoded";
                objWebRequest.ContentLength = byteArray.Length;
                Stream newStream = objWebRequest.GetRequestStream();
                // Send the data. 
                newStream.Write(byteArray, 0, byteArray.Length); //写入参数 
                newStream.Close();

                HttpWebResponse response = (HttpWebResponse)objWebRequest.GetResponse();
                StreamReader sr = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
                string textResponse = sr.ReadToEnd(); // 返回的数据
            }
            catch (Exception exception)
            {
                throw;
            }
        }

        [TestMethod]
        public void RefundOrderTest()
        {
            var refundOrderRequest = new RefundOrderRequest
                {
                    transaction_id = "1003310117201509250990172393",
                    // out_trade_no = "C2015090204471208755383",
                    total_fee = 1,
                    refund_fee = 1,
                    out_refund_no = WxPayHelper.GenerateOutTradeNo()
                };

            JsApiPay jsApiPay = new JsApiPay();
            jsApiPay.Refund(refundOrderRequest);
        }

        [TestMethod]
        public void RefundQueryTest()
        {
            var refundQueryRequest = new RefundQueryRequest();
            refundQueryRequest.transaction_id = "1003310117201509230963841378";

            var result = new JsApiPay().RefundQuery(refundQueryRequest);

            Assert.IsTrue(result != null);
        }
    }
}
