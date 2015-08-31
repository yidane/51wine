using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Travel.Infrastructure.WeiXin.Advanced.Pay.Model
{
    public class RefundOrderResponse
    {
        /// <summary>
        /// 返回状态码
        /// </summary>
        public string return_code { get; set; }

        /// <summary>
        /// 返回信息
        /// </summary>
        public string return_msg { get; set; }

        /// <summary>
        /// 业务结果
        /// </summary>
        public string result_code { get; set; }

        /// <summary>
        /// 错误代码
        /// </summary>
        public string err_code { get; set; }

        /// <summary>
        /// 错误代码描述
        /// </summary>
        public string err_code_des { get; set; }

        /// <summary>
        /// 公众账号ID
        /// </summary>
        public string appid { get; set; }

        /// <summary>
        /// 商户号
        /// </summary>
        public string mch_id { get; set; }

        /// <summary>
        /// 设备号
        /// </summary>
        public string device_info { get; set; }

        /// <summary>
        /// 随机字符串
        /// </summary>
        public string nonce_str { get; set; }

        /// <summary>
        /// 签名
        /// </summary>
        public string sign { get; set; }

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
        /// 微信退款单号
        /// </summary>
        public string refund_id { get; set; }

        /// <summary>
        /// 退款渠道
        /// </summary>
        public string refund_channel { get; set; }

        /// <summary>
        /// 退款渠道描叙
        /// </summary>
        public string refund_channel_des
        {
            get
            {
                if (string.Equals(refund_channel, "ORIGINAL"))
                {
                    return "原路退款";
                }
                else if (string.Equals(refund_channel, "BALANCE"))
                {
                    return "退回到余额";
                }
                else
                {
                    return string.Empty;
                }
            }
        }

        /// <summary>
        /// 退款金额
        /// </summary>
        public int refund_fee { get; set; }

        /// <summary>
        /// 订单总金额
        /// </summary>
        public int total_fee { get; set; }

        /// <summary>
        /// 订单金额货币种类
        /// </summary>
        public string fee_type { get; set; }

        /// <summary>
        /// 现金支付金额
        /// </summary>
        public int cash_fee { get; set; }

        /// <summary>
        /// 现金退款金额
        /// </summary>
        public int cash_refund_fee { get; set; }

        /// <summary>
        /// 代金券或立减优惠退款金额
        /// </summary>
        public int coupon_refund_fee { get; set; }

        /// <summary>
        /// 代金券或立减优惠使用数量
        /// </summary>
        public int coupon_refund_count { get; set; }

        /// <summary>
        /// 代金券或立减优惠ID
        /// </summary>
        public string coupon_refund_id { get; set; }
    }
}
