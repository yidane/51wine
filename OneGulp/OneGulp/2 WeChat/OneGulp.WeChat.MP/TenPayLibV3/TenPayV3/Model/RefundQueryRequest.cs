using System;

namespace OneGulp.WeChat.MP.TenPayLibV3.TenPayV3.Model
{
    public class RefundQueryRequest
    {
        public string appid { get; set; }
        public string mch_id { get; set; }
        public string device_info { get; set; }
        public string nonce_str { get; set; }
        public string sign { get; set; }
        public string transaction_id { get; set; }
        public string out_trade_no { get; set; }
        public string out_refund_no { get; set; }
        public string refund_id { get; set; }

        public RequestHandler CreatePayData()
        {
            var rtnRequestHandler = new RequestHandler(null);
            if (!string.IsNullOrEmpty(transaction_id))
            {
                rtnRequestHandler.SetParameter("transaction_id", transaction_id);
            }

            if (!string.IsNullOrEmpty(out_trade_no))
            {
                rtnRequestHandler.SetParameter("out_trade_no", out_trade_no);
            }

            if (!string.IsNullOrEmpty(out_refund_no))
            {
                rtnRequestHandler.SetParameter("out_refund_no", out_refund_no);
            }

            if (!string.IsNullOrEmpty(refund_id))
            {
                rtnRequestHandler.SetParameter("refund_id", refund_id);
            }

            if (rtnRequestHandler.GetAllParameters().Count == 0)
                throw new Exception("退款查询接口中，out_refund_no、out_trade_no、transaction_id、refund_id四个参数必填一个！");

            if (string.IsNullOrEmpty(appid))
                throw new Exception("退款查询接口中，appId参数必填");
            if (string.IsNullOrEmpty(mch_id))
                throw new Exception("退款查询接口中，mch_id参数必填");
            if (string.IsNullOrEmpty(nonce_str))
                throw new Exception("退款查询接口中，nonce_str参数必填");
            if (string.IsNullOrEmpty(sign))
                throw new Exception("退款查询接口中，sign参数必填");

            rtnRequestHandler.SetParameter("appid", appid);
            rtnRequestHandler.SetParameter("mch_id", mch_id);
            rtnRequestHandler.SetParameter("nonce_str", nonce_str);
            rtnRequestHandler.SetParameter("sign", sign);

            if (!string.IsNullOrEmpty(device_info))
                rtnRequestHandler.SetParameter("device_info", device_info);

            return rtnRequestHandler;
        }
    }
}