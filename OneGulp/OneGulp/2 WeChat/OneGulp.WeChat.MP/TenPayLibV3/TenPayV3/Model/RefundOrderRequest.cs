using OneGulp.WeChat.MP.TenPayLibV3;
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
            get;
            set;
        }

        /// <summary>
        /// 商户号
        /// </summary>
        public string mch_id
        {
            get;
            set;
        }

        /// <summary>
        /// 设备号
        /// </summary>
        public string device_info
        {
            get;
            set;
        }

        /// <summary>
        /// 随机字符串
        /// </summary>
        public string nonce_str
        {
            get;
            set;
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
            get;
            set;
        }

        /// <summary>
        /// 证书路径
        /// </summary>
        /// <returns></returns>
        public string CertificaterPath { get; set; }

        /// <summary>
        /// 证书密码
        /// </summary>
        public string CertificaterPassword { get; set; }

        public RequestHandler CreatePayData()
        {
            var rtnRequestHandler = new RequestHandler(null);
            if (!string.IsNullOrEmpty(transaction_id))//微信订单号存在的条件下，则已微信订单号为准
            {
                rtnRequestHandler.SetParameter("transaction_id", transaction_id);
            }
            else//微信订单号不存在，才根据商户订单号去退款
            {
                rtnRequestHandler.SetParameter("out_trade_no", out_trade_no);
            }

            rtnRequestHandler.SetParameter("total_fee", this.total_fee.ToString());//订单总金额
            rtnRequestHandler.SetParameter("refund_fee", this.refund_fee.ToString());//退款金额
            rtnRequestHandler.SetParameter("out_refund_no", this.out_refund_no);//随机生成商户退款单号
            rtnRequestHandler.SetParameter("op_user_id", this.op_user_id);//操作员，默认为商户号

            return rtnRequestHandler;
        }
    }
}
