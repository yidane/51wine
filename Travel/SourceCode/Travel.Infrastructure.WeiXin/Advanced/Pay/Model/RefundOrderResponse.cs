using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

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

        public RefundOrderResponse()
        {

        }

        public RefundOrderResponse(string xmlString)
        {
            var xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(xmlString);
            Init(xmlDocument);
        }

        public RefundOrderResponse(XmlDocument xmlDocument)
        {
            Init(xmlDocument);
        }

        public void Init(XmlDocument xmlDocument)
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
                err_code = GetNodeInnerText(xmlDocument, "xmlDocument");
                err_code_des = GetNodeInnerText(xmlDocument, "err_code_des");
                total_fee = int.Parse(GetNodeInnerText(xmlDocument, "total_fee"));
                fee_type = GetNodeInnerText(xmlDocument, "fee_type");
                cash_fee = int.Parse(GetNodeInnerText(xmlDocument, "cash_fee"));
                var couponfee = GetNodeInnerText(xmlDocument, "coupon_fee");
                var couponCount = GetNodeInnerText(xmlDocument, "coupon_count");
                transaction_id = GetNodeInnerText(xmlDocument, "transaction_id");
                out_trade_no = GetNodeInnerText(xmlDocument, "out_trade_no");
            }
        }

        private string GetNodeInnerText(XmlDocument xmlDocument, string nodeName)
        {
            var node = xmlDocument.SelectSingleNode(string.Format("xml/{0}", nodeName));
            return node != null ? node.InnerText : string.Empty;
        }
    }
}
