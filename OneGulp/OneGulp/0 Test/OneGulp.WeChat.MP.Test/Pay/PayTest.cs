using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OneGulp.WeChat.MP.TenPayLibV3;
using OneGulp.WeChat.MP.TenPayLibV3.TenPayV3;
using OneGulp.WeChat.MP.TenPayLibV3.TenPayV3.Model;

namespace OneGulp.WeChat.MP.Test.Pay
{
    [TestClass]
    public class PayTest : WeChatAppAcount
    {
        [TestMethod]
        public void TestRefundQuery()
        {

            //T2015090218440551242147 OK
            //T2015100216032715713132 OK
            //T2015100216032662785285 OK
            //T2015110313561153962627 OK
            //T2015100216032444424530 OK
            //T2015120716004120118359 Processing 2003800113201512070094790952
            var mch_id = "1260203301";
            var out_refund_no = "T2015120716004120118359";
            var nonce_str = TenPayV3Util.GetNoncestr();

            var requestHandler = new RequestHandler(null);
            requestHandler.SetParameter("appid", appId);
            requestHandler.SetParameter("mch_id", mch_id);
            requestHandler.SetParameter("nonce_str", nonce_str);
            requestHandler.SetParameter("out_refund_no", out_refund_no);
            var request = new RefundQueryRequest
            {
                appid = appId,
                mch_id = mch_id,
                nonce_str = nonce_str,
                out_refund_no = out_refund_no,
                sign = requestHandler.CreateMd5Sign("key", "kanasijingquguanweihuizhanghao12")
            };

            JsApiPay jsApiPay = new JsApiPay();
            var result = jsApiPay.RefundQuery(request);
        }
    }
}