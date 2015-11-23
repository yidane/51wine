using OneGulp.WeChat.MP.TenPayLibV3;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Travel.Infrastructure.WeiXin.Advanced.Pay.Model
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
                return rtnRequestHandler;
            }

            if (!string.IsNullOrEmpty(out_trade_no))
            {
                rtnRequestHandler.SetParameter("out_trade_no", out_trade_no);
                return rtnRequestHandler;
            }

            if (!string.IsNullOrEmpty(out_refund_no))
            {
                rtnRequestHandler.SetParameter("out_refund_no", out_refund_no);
                return rtnRequestHandler;
            }

            if (!string.IsNullOrEmpty(refund_id))
            {
                rtnRequestHandler.SetParameter("refund_id", refund_id);
                return rtnRequestHandler;
            }

            throw new Exception("退款查询接口中，out_refund_no、out_trade_no、transaction_id、refund_id四个参数必填一个！");
        }
    }
}
