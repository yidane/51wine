using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Travel.Infrastructure.WeiXin.Advanced.Pay.Model
{
    public class PaymentNotify
    {
        /// <summary>
        /// 返回状态码
        /// <example>SUCCESS</example>
        /// <remarks>SUCCESS/FAIL 此字段是通信标识，非交易标识，交易是否成功需要查看result_code来判断</remarks>
        /// </summary>
        public string return_code { get; set; }

        /// <summary>
        /// 返回信息
        /// <example>签名失败</example>
        /// <remarks>返回信息，如非空，为错误原因 签名失败 参数格式校验错误</remarks>
        /// </summary>
        public string return_msg { get; set; }

        /// <summary>
        /// 公众账号ID
        /// <example>wx8888888888888888</example>
        /// <remarks>调用接口提交的公众账号ID</remarks>
        /// </summary>
        public string appid { get; set; }

        /// <summary>
        /// 商户号
        /// <example>1900000109</example>
        /// <remarks>调用接口提交的商户号</remarks>
        /// </summary>
        public string mch_id { get; set; }

        /// <summary>
        /// 设备号
        /// <example>013467007045764</example>
        /// <remarks>调用接口提交的终端设备号，</remarks>
        /// </summary>
        public string device_info { get; set; }

        /// <summary>
        /// 随机字符串
        /// <example>5K8264ILTKCH16CQ2502SI8ZNMTM67VS</example>
        /// <remarks>微信返回的随机字符串</remarks>
        /// </summary>
        public string nonce_str { get; set; }

        /// <summary>
        /// 签名
        /// <example>C380BEC2BFD727A4B6845133519F3AD6</example>
        /// <remarks>微信返回的签名，详见签名算法</remarks>
        /// </summary>
        public string sign { get; set; }

        /// <summary>
        /// 业务结果
        /// <example>SUCCESS</example>
        /// <remarks>SUCCESS/FAIL</remarks>
        /// </summary>
        public string result_code { get; set; }

        /// <summary>
        /// 错误代码
        /// <example>SYSTEMERROR</example>
        /// <remarks>详细参见第6节错误列表</remarks>
        /// </summary>
        public string err_code { get; set; }

        /// <summary>
        /// 错误代码描述
        /// <example>系统错误</example>
        /// <remarks>错误返回的信息描述</remarks>
        /// </summary>
        public string err_code_des { get; set; }

        /// <summary>
        /// 用户标识
        /// </summary>
        public string openid { get; set; }

        /// <summary>
        /// 是否关注公众账号
        /// </summary>
        public string is_subscribe { get; set; }

        /// <summary>
        /// 交易类型
        /// </summary>
        public string trade_type { get; set; }

        /// <summary>
        /// 付款银行
        /// </summary>
        public string bank_type { get; set; }

        /// <summary>
        /// 总金额
        /// </summary>
        public int total_fee { get; set; }

        /// <summary>
        /// 货币种类
        /// </summary>
        public string fee_type { get; set; }

        /// <summary>
        /// 现金支付金额
        /// </summary>
        public string cash_fee { get; set; }

        /// <summary>
        /// cash_fee_type
        /// </summary>
        public string cash_fee_type { get; set; }

        /// <summary>
        /// 代金券或立减优惠金额
        /// </summary>
        public int coupon_fee { get; set; }

        /// <summary>
        /// 代金券或立减优惠使用数量
        /// </summary>
        public int coupon_count { get; set; }

        /// <summary>
        /// 代金券或立减优惠ID,coupon_id_$n,n为下标
        /// </summary>
        public string[] coupon_ids { get; set; }

        /// <summary>
        /// 单个代金券或立减优惠支付金额
        /// </summary>
        public string[] coupon_fees { get; set; }

        /// <summary>
        /// 微信支付订单号
        /// </summary>
        public string transaction_id { get; set; }

        /// <summary>
        /// 商户订单号
        /// </summary>
        public string out_trade_no { get; set; }

        /// <summary>
        /// 商家数据包
        /// </summary>
        public string attach { get; set; }

        /// <summary>
        /// 支付完成时间
        /// </summary>
        public string time_end { get; set; }
    }
}
