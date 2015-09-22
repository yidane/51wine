using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Travel.Infrastructure.WeiXin.Advanced.Pay.Model
{
    public class RefundQueryResponse
    {
        public string result_code { get; set; }
        public string err_code { get; set; }
        public string err_code_des { get; set; }
        public string appid { get; set; }
        public string mch_id { get; set; }
        public string device_info { get; set; }
        public string nonce_str { get; set; }
        public string sign { get; set; }
        public string transaction_id { get; set; }
        public string out_trade_no { get; set; }
        public int total_fee { get; set; }
        public string fee_type { get; set; }
        public int cash_fee { get; set; }
        public int refund_count { get; set; }
        public List<RefundInfo> RefundInfoList { get; set; }
    }

    public class RefundInfo
    {
        public string out_refund_no { get; set; }
        public string refund_id { get; set; }
        public string refund_channel { get; set; }
        public int refund_fee { get; set; }
        public int coupon_refund_fee { get; set; }
        public int coupon_refund_count { get; set; }
        public string refund_status { get; set; }
        public List<CouponRefundInfo> CouponRefundInfoList { get; set; }
    }

    public class CouponRefundInfo
    {
        public string coupon_refund_batch_idP { get; set; }
        public string coupon_refund_id { get; set; }
        public int coupon_refund_fee { get; set; }
    }
}

