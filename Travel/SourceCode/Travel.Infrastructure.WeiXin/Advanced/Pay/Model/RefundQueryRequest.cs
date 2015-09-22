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

        public WxPayData CreatePayData()
        {
            var rtnPayData = new WxPayData();
            if (!string.IsNullOrEmpty(transaction_id))
            {
                rtnPayData.SetValue("transaction_id", transaction_id);
                return rtnPayData;
            }

            if (!string.IsNullOrEmpty(out_trade_no))
            {
                rtnPayData.SetValue("out_trade_no", out_trade_no);
                return rtnPayData;
            }

            if (!string.IsNullOrEmpty(out_refund_no))
            {
                rtnPayData.SetValue("",out_refund_no);
                return rtnPayData;
            }

            if (!string.IsNullOrEmpty(refund_id))
            {
                rtnPayData.SetValue("refund_id", refund_id);
                return rtnPayData;
            }

            throw new Exception("退款查询接口中，out_refund_no、out_trade_no、transaction_id、refund_id四个参数必填一个！");
        }
    }
}
