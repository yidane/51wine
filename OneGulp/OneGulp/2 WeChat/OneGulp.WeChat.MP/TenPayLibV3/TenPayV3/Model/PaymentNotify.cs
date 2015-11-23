using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace OneGulp.WeChat.MP.TenPayLibV3
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
        public int cash_fee { get; set; }

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
        /// 单个代金券或立减优惠支付金额
        /// </summary>
        public List<Coupon> CouponList { get; set; }

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

        public PaymentNotify()
        {

        }

        public PaymentNotify(string notifyMessage)
        {
            var xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(notifyMessage);
            InitPaymentNotify(xmlDocument);
        }

        public PaymentNotify(XmlDocument xmlDocument)
        {
            InitPaymentNotify(xmlDocument);
        }

        private void InitPaymentNotify(XmlDocument xmlDocument)
        {
            var return_codeNode = xmlDocument.SelectSingleNode("xml/return_code");
            var return_msgNode = xmlDocument.SelectSingleNode("xml/return_msg");

            if (return_codeNode != null)
                return_code = return_codeNode.InnerText;

            if (return_msgNode != null)
                return_msg = return_msgNode.InnerText;

            if (string.Equals(return_code, "SUCCESS"))
            {
                appid = GetNodeInnerText(xmlDocument, "appid");
                mch_id = GetNodeInnerText(xmlDocument, "mch_id");
                device_info = GetNodeInnerText(xmlDocument, "device_info");
                nonce_str = GetNodeInnerText(xmlDocument, "nonce_str");
                sign = GetNodeInnerText(xmlDocument, "sign");
                result_code = GetNodeInnerText(xmlDocument, "result_code");
                err_code = GetNodeInnerText(xmlDocument, "err_code");
                err_code_des = GetNodeInnerText(xmlDocument, "err_code_des");
                openid = GetNodeInnerText(xmlDocument, "openid");
                is_subscribe = GetNodeInnerText(xmlDocument, "is_subscribe");
                trade_type = GetNodeInnerText(xmlDocument, "trade_type");
                bank_type = GetNodeInnerText(xmlDocument, "bank_type");
                total_fee = int.Parse(GetNodeInnerText(xmlDocument, "total_fee"));
                fee_type = GetNodeInnerText(xmlDocument, "fee_type");

                var cash_fee_text = GetNodeInnerText(xmlDocument, "cash_fee");
                if (!string.IsNullOrEmpty(cash_fee_text))
                    cash_fee = int.Parse(cash_fee_text);
                cash_fee_type = GetNodeInnerText(xmlDocument, "cash_fee_type");
                var couponfee = GetNodeInnerText(xmlDocument, "coupon_fee");
                coupon_fee = string.IsNullOrEmpty(couponfee) ? 0 : int.Parse(couponfee);
                var couponCount = GetNodeInnerText(xmlDocument, "coupon_count");
                coupon_count = string.IsNullOrEmpty(couponCount) ? 0 : int.Parse(couponCount);

                var id = 0;
                var coupon_id = GetNodeInnerText(xmlDocument, "coupon_id_0");
                var coupon_id_fee = GetNodeInnerText(xmlDocument, "coupon_fee_0");
                CouponList = new List<Coupon>();
                while (!string.IsNullOrEmpty(coupon_id) && !string.IsNullOrEmpty(coupon_id_fee))
                {
                    CouponList.Add(new Coupon(coupon_id, coupon_id_fee));
                    id = id + 1;
                    coupon_id = GetNodeInnerText(xmlDocument, string.Format("coupon_id_{0}", id));
                    coupon_id_fee = GetNodeInnerText(xmlDocument, string.Format("coupon_fee_{0}", id));
                }

                transaction_id = GetNodeInnerText(xmlDocument, "transaction_id");
                out_trade_no = GetNodeInnerText(xmlDocument, "out_trade_no");
                attach = GetNodeInnerText(xmlDocument, "attach");
                time_end = GetNodeInnerText(xmlDocument, "time_end");
            }
        }

        private string GetNodeInnerText(XmlDocument xmlDocument, string nodeName)
        {
            var node = xmlDocument.SelectSingleNode(string.Format("xml/{0}", nodeName));
            return node != null ? node.InnerText : string.Empty;
        }

        public string Report(out bool isSucceed)
        {
            isSucceed = false;
            if (string.Equals(return_code, "SUCCESS", StringComparison.CurrentCultureIgnoreCase))
            {
                //检查支付结果中transaction_id是否存在
                if (string.IsNullOrEmpty(transaction_id))
                {
                    //若transaction_id不存在，则立即返回结果给微信支付后台
                    var res = new RequestHandler(null);
                    res.SetParameter("return_code", "FAIL");
                    res.SetParameter("return_msg", "支付结果中微信订单号不存在");

                    return res.ParseXML();
                }

                //查询订单，判断订单真实性
                if (!new JsApiPay().QueryOrder(transaction_id))
                {
                    //若订单查询失败，则立即返回结果给微信支付后台
                    var res = new RequestHandler(null);
                    res.SetParameter("return_code", "FAIL");
                    res.SetParameter("return_msg", "订单查询失败");

                    return res.ParseXML();
                }
                else
                {
                    //查询订单成功
                    var res = new RequestHandler(null);
                    res.SetParameter("return_code", "SUCCESS");
                    res.SetParameter("return_msg", "OK");

                    isSucceed = true;
                    return res.ParseXML();
                }
            }
            else
            {
                var res = new RequestHandler(null);
                res.SetParameter("return_code", "FAIL");
                res.SetParameter("return_msg", "统一下单失败");
                return res.ParseXML();
            }
        }

        public static string ReportError(string message)
        {
            var res = new RequestHandler(null);
            res.SetParameter("return_code", "FAIL");
            res.SetParameter("return_msg", message);

            return res.ParseXML();
        }

        public static string ReportSuccess()
        {
            var res = new RequestHandler(null);
            res.SetParameter("return_code", "SUCCESS");
            res.SetParameter("return_msg", "OK");

            return res.ParseXML();
        }
    }

    /// <summary>
    ///  代金券或立减优惠ID
    /// </summary>
    public class Coupon
    {
        public string coupon_id { get; set; }
        public int coupon_fee { get; set; }

        public Coupon(string coupon_id, string coupon_id_fee)
        {
            this.coupon_id = coupon_id;
            this.coupon_fee = int.Parse(coupon_id_fee);
        }
    }
}
