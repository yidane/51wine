using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Travel.Infrastructure.WeiXin.Advanced.Pay.Model
{
    public class RefundOrderRequest
    {
        /// <summary>
        /// 公众账号ID
        /// </summary>
        public string appid
        {
            get { return WxPayConfig.APPID; }
        }

        /// <summary>
        /// 商户号
        /// </summary>
        public string mch_id
        {
            get { return WxPayConfig.MCHID; }
        }

        /// <summary>
        /// 设备号
        /// </summary>
        public string device_info
        {
            get { return string.Empty; }
        }

        /// <summary>
        /// 随机字符串
        /// </summary>
        public string nonce_str
        {
            get { return WxPayHelper.GenerateNonceStr(); }
        }

        /// <summary>
        /// 签名
        /// </summary>
        public string sign
        {
            get;
            set;
        }

        /// <summary>
        /// 微信订单号
        /// </summary>
        public string transaction_id { get; set; }

        /// <summary>
        /// 商户订单号
        /// </summary>
        public string out_trade_no { get; set; }

        /// <summary>
        /// 商户退款单号
        /// </summary>
        public string out_refund_no { get; set; }

        /// <summary>
        /// 总金额
        /// </summary>
        public int total_fee { get; set; }

        /// <summary>
        /// 退款金额
        /// </summary>
        public int refund_fee { get; set; }

        /// <summary>
        /// 货币种类
        /// </summary>
        public string refund_fee_type
        {
            get { return "CNY"; }
        }

        /// <summary>
        /// 操作员
        /// </summary>
        public string op_user_id
        {
            get { return WxPayConfig.MCHID; }
        }
    }
}
