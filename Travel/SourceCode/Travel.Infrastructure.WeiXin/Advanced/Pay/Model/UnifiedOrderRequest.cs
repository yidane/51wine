using System;

namespace Travel.Infrastructure.WeiXin.Advanced.Pay.Model
{
    public class UnifiedOrderRequest
    {
        /// <summary>
        /// 公众账号ID (必填)
        /// <example>wx8888888888888888</example>
        /// <remarks>微信分配的公众账号ID（企业号corpid即为此appId）</remarks>
        /// </summary>
        [Require(true, "appid必填")]
        public string appid { get; set; }

        /// <summary>
        /// 商户号 (必填)
        /// <example>1900000109</example>
        /// <remarks>微信支付分配的商户号</remarks>
        /// </summary>
        [Require(true, "")]
        public string mch_id { get; set; }

        /// <summary>
        /// 设备号 (选填)
        /// <example>013467007045764</example>
        /// <remarks>终端设备号(门店号或收银设备ID)，注意：PC网页或公众号内支付请传"WEB"</remarks>
        /// </summary>
        [Require(false, "")]
        public string device_info { get; set; }

        /// <summary>
        /// 随机字符串 (必填)
        /// <example>5K8264ILTKCH16CQ2502SI8ZNMTM67VS</example>
        /// <remarks>随机字符串，不长于32位。推荐随机数生成算法 https://pay.weixin.qq.com/wiki/doc/api/jsapi.php?chapter=4_3</remarks>
        /// </summary>
        [Require(true, "")]
        public string nonce_str { get; set; }

        /// <summary>
        /// 签名 （必填）
        /// <example>C380BEC2BFD727A4B6845133519F3AD6</example>
        /// <remarks>签名算法 https://pay.weixin.qq.com/wiki/doc/api/jsapi.php?chapter=4_3</remarks>
        /// </summary>
        [Require(true, "")]
        public string sign { get; set; }

        /// <summary>
        /// 商品描述 (必填)
        /// <example>Ipad mini  16G  白色</example>
        /// <remarks>商品或支付单简要描述</remarks>
        /// </summary>
        [Require(true, "")]
        public string body { get; set; }

        /// <summary>
        /// 商品详情 (选填)
        /// <example>Ipad mini  16G  白色</example>
        /// <remarks>商品名称明细列表</remarks>
        /// </summary>
        [Require(false, "")]
        public string detail { get; set; }

        /// <summary>
        /// 附加数据 （选填）
        /// <example>说明</example>
        /// <remarks>附加数据，在查询API和支付通知中原样返回，该字段主要用于商户携带订单的自定义数据</remarks>
        /// </summary>
        [Require(false, "")]
        public string attach { get; set; }

        /// <summary>
        /// 商户订单号 (必填)
        /// <example>1217752501201407033233368018</example>
        /// <remarks>商户系统内部的订单号,32个字符内、可包含字母, 其他说明见商户订单号 https://pay.weixin.qq.com/wiki/doc/api/jsapi.php?chapter=4_2</remarks>
        /// </summary>
        [Require(true, "")]
        public string out_trade_no
        {
            get
            {
                return WxPayHelper.GenerateOutTradeNo();
            }
        }

        /// <summary>
        /// 货币类型 （选填）
        /// <example>CNY</example>
        /// <remarks>符合ISO 4217标准的三位字母代码，默认人民币：CNY，其他值列表详见货币类型 https://pay.weixin.qq.com/wiki/doc/api/jsapi.php?chapter=4_2</remarks>
        /// </summary>
        [Require(false, "")]
        public string fee_type { get; set; }

        /// <summary>
        /// 总金额 （必填）
        /// <example>888</example>
        /// <remarks>订单总金额，只能为整数，详见支付金额 https://pay.weixin.qq.com/wiki/doc/api/jsapi.php?chapter=4_2</remarks>
        /// </summary>
        [Require(true, "")]
        public int total_fee { get; set; }

        /// <summary>
        /// 终端IP (必填)
        /// <example>8.8.8.8</example>
        /// <remarks>APP和网页支付提交用户端ip，Native支付填调用微信支付API的机器IP。</remarks>
        /// </summary>
        [Require(true, "")]
        public string spbill_create_ip { get; set; }

        /// <summary>
        /// 交易起始时间 （选填）
        /// <example>20091225091010</example>
        /// <remarks>订单生成时间，格式为yyyyMMddHHmmss，如2009年12月25日9点10分10秒表示为20091225091010。其他详见时间规则</remarks>
        /// </summary>
        [Require(false, "")]
        public string time_start
        {
            get
            {
                return DateTime.Now.ToString("yyyyMMddHHmmss");
            }
        }

        /// <summary>
        /// 交易结束时间 （必填）
        /// <example>20091227091010</example>
        /// <remarks>订单失效时间，格式为yyyyMMddHHmmss，如2009年12月27日9点10分10秒表示为20091227091010。其他详见时间规则 注意：最短失效时间间隔必须大于5分钟</remarks>
        /// </summary>
        [Require(true, "")]
        public string time_expire
        {
            get { return DateTime.Now.AddMinutes(10).ToString("yyyyMMddHHmmss"); }
        }

        /// <summary>
        /// 商品标记 （选填）
        /// <example>WXG</example>
        /// <remarks>商品标记，代金券或立减优惠功能的参数，说明详见代金券或立减优惠</remarks>
        /// </summary>
        [Require(false, "")]
        public string goods_tag { get; set; }

        /// <summary>
        /// 通知地址 （必填）
        /// <example>http://www.baidu.com</example>
        /// <remarks>接收微信支付异步通知回调地址</remarks>
        /// </summary>
        [Require(true, "")]
        public string notify_url { get; set; }

        /// <summary>
        /// 交易类型 （必填）
        /// <example>JSAPI</example>
        /// <remarks>取值如下：JSAPI，NATIVE，APP，WAP,详细说明见参数规定</remarks>
        /// </summary>
        [Require(true, "")]
        public string trade_type { get; set; }

        /// <summary>
        /// 商品ID （选填）
        /// <example>12235413214070356458058</example>
        /// <remarks>trade_type=NATIVE，此参数必传。此id为二维码中包含的商品ID，商户自行定义。</remarks>
        /// </summary>
        [Require(false, "")]
        public string product_id { get; set; }

        /// <summary>
        /// 指定支付方式 （选填）
        /// <example>no_credit</example>
        /// <remarks>no_credit--指定不能使用信用卡支付</remarks>
        /// </summary>
        [Require(false, "")]
        public string limit_pay { get; set; }

        /// <summary>
        /// 用户标识  （选填）
        /// <example>oUpF8uMuAJO_M2pxb1Q9zNjWeS6o</example>
        /// <remarks>trade_type=JSAPI，此参数必传，用户在商户appid下的唯一标识。openid如何获取，可参考【获取openid】。企业号请使用【企业号OAuth2.0接口】获取企业号内成员userid，再调用【企业号userid转openid接口】进行转换</remarks>
        /// </summary>
        [Require(false, "")]
        public string openid { get; set; }
    }

    public class RequireAttribute : Attribute
    {
        public bool Require { get; set; }
        public string ErrorMessage { get; set; }

        public RequireAttribute(bool require, string errorMessage)
        {
            Require = require;
            ErrorMessage = errorMessage;
        }
    }
}
