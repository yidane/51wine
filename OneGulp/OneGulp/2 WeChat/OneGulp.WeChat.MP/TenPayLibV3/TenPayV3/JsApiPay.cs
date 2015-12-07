using OneGulp.WeChat.MP.TenPayLibV3.TenPayV3.Model;

namespace OneGulp.WeChat.MP.TenPayLibV3.TenPayV3
{
    public class JsApiPay
    {

        /// <summary>
        /// 调用统一下单，获得下单结果
        /// </summary>
        /// <param name="request">统一下单结果</param>
        /// <returns>统一下单结果</returns>
        public UnifiedOrderResponse GetUnifiedOrderResult(UnifiedOrderRequest request)
        {
            var packageReqHandler = new RequestHandler(null);
            //初始化
            packageReqHandler.Init();

            var timeStamp = TenPayV3Util.GetTimestamp();
            var nonceStr = TenPayV3Util.GetNoncestr();

            //设置package订单参数
            packageReqHandler.SetParameter("appid", request.appid);		  //公众账号ID
            packageReqHandler.SetParameter("mch_id", request.mch_id);		  //商户号
            packageReqHandler.SetParameter("nonce_str", nonceStr);                    //随机字符串
            packageReqHandler.SetParameter("body", request.body);  //商品描述
            packageReqHandler.SetParameter("attach", request.attach);
            packageReqHandler.SetParameter("out_trade_no", request.out_trade_no);		//商家订单号
            packageReqHandler.SetParameter("total_fee", request.total_fee.ToString());			        //商品金额,以分为单位(money * 100).ToString()
            packageReqHandler.SetParameter("spbill_create_ip", request.spbill_create_ip);   //用户的公网ip，不是商户服务器IP
            packageReqHandler.SetParameter("notify_url", request.notify_url);		    //接收财付通通知的URL
            packageReqHandler.SetParameter("trade_type", TenPayV3Type.JSAPI.ToString());//交易类型
            packageReqHandler.SetParameter("openid", request.openid);	                    //用户的openId

            string sign = packageReqHandler.CreateMd5Sign("key", request.key);
            packageReqHandler.SetParameter("sign", sign);	                    //签名

            string data = packageReqHandler.ParseXML();

            var unifiedOrderResult = TenPayV3Helper.Unifiedorder(data);
            var rtnUnifiedOrderResult = new UnifiedOrderResponse(unifiedOrderResult);

            return rtnUnifiedOrderResult;
        }

        /// <summary>
        /// 退款
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public RefundOrderResponse Refund(RefundOrderRequest request)
        {
            var payData = request.CreatePayData();
            var result = TenPayV3Helper.Refund(payData.ParseXML(), request.CertificaterPath, request.CertificaterPassword);
            return new RefundOrderResponse(result);
        }

        /// <summary>
        /// 订单查询
        /// </summary>
        /// <param name="transaction_id"></param>
        /// <returns></returns>
        public bool QueryOrder(string transaction_id)
        {
            var req = new RequestHandler(null);
            req.SetParameter("transaction_id", transaction_id);
            var res = TenPayV3Helper.OrderQuery(req.ParseXML());
            //if (res.GetValue("return_code").ToString() == "SUCCESS" &&
            //    res.GetValue("result_code").ToString() == "SUCCESS")
            //{
            //    return true;
            //}
            //else
            //{
            //    return false;
            //}

            return true;
        }

        /// <summary>
        /// 退款查询
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public RefundQueryResponse RefundQuery(RefundQueryRequest request)
        {
            var refundQueryRequest = request.CreatePayData();
            var result = TenPayV3Helper.RefundQuery(refundQueryRequest.ParseXML());
            return new RefundQueryResponse(result);
        }
    }
}
