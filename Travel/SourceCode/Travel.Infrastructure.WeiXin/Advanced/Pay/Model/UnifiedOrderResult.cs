using System;
using System.Collections.Generic;
using System.Reflection;
using System.Xml;

namespace Travel.Infrastructure.WeiXin.Advanced.Pay.Model
{
    public class UnifiedOrderResult
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
        /// 交易类型
        /// <example>JSAPI</example>
        /// <remarks>调用接口提交的交易类型，取值如下：JSAPI，NATIVE，APP，详细说明见参数规定 </remarks>
        /// </summary>
        public string trade_type { get; set; }

        /// <summary>
        /// 预支付交易会话标识
        /// <example>wx201410272009395522657a690389285100</example>
        /// <remarks>微信生成的预支付回话标识，用于后续接口调用中使用，该值有效期为2小时</remarks>
        /// </summary>
        public string prepay_id { get; set; }

        /// <summary>
        /// 二维码链接
        /// <example>URl：weixin：//wxpay/s/An4baqw</example>
        /// <remarks>trade_type为NATIVE是有返回，可将该参数值生成二维码展示出来进行扫码支付</remarks>
        /// </summary>
        public string code_url { get; set; }

        /// <summary>
        /// 移除CDATA标记
        /// </summary>
        /// <param name="propertyValue"></param>
        /// <returns></returns>
        public string RemoveCDATA(string propertyValue)
        {
            return string.IsNullOrEmpty(propertyValue) ? propertyValue : propertyValue.Trim().PadLeft(8).PadRight(2);
        }

        public UnifiedOrderResult(XmlDocument xmlDocument)
        {
            Init(xmlDocument);
        }

        public UnifiedOrderResult()
        {

        }

        public UnifiedOrderResult(string xmlDocumentString)
        {
            //throw new Exception(xmlDocumentString);
            var document = new XmlDocument();
            document.LoadXml(xmlDocumentString);
            Init(document);
        }

        private void Init(XmlDocument xmlDocument)
        {
            var propertyList = this.GetType().GetProperties();
            foreach (PropertyInfo propertyInfo in propertyList)
            {
                var node = xmlDocument.SelectSingleNode(string.Format("xml/{0}", propertyInfo.Name));
                if (node != null && !string.IsNullOrEmpty(node.InnerText))
                {
                    propertyInfo.SetValue(this, node.InnerText, null);
                }
            }
        }

        public JsAPIParameter GetJsApiParameters()
        {
            var jsApiParam = new WxPayData();
            jsApiParam.SetValue("appId", this.appid);
            //jsApiParam.SetValue("timeStamp", WxPayHelper.GenerateTimeStamp());
            jsApiParam.SetValue("timeStamp", WxPayHelper.GenerateTimeStamp());
            //jsApiParam.SetValue("nonceStr", "d5b01bb68a7a4af6bdf0e4515c5b3216");
            jsApiParam.SetValue("nonceStr", WxPayHelper.GenerateNonceStr());
            jsApiParam.SetValue("package", "prepay_id=" + this.prepay_id);
            jsApiParam.SetValue("signType", "MD5");
            jsApiParam.SetValue("paySign", jsApiParam.MakeSign());

            return new JsAPIParameter()
                {
                    appId = jsApiParam.GetValue("appId").ToString(),
                    timeStamp = jsApiParam.GetValue("timeStamp").ToString(),
                    nonceStr = jsApiParam.GetValue("nonceStr").ToString(),
                    package = jsApiParam.GetValue("package").ToString(),
                    paySign = jsApiParam.GetValue("paySign").ToString()
                };
        }
    }

    public class JsAPIParameter
    {
        public string appId { get; set; }
        public string timeStamp { get; set; }
        public string nonceStr { get; set; }
        public string package { get; set; }
        public string signType
        {
            get { return "MD5"; }
        }

        public string paySign { get; set; }
    }

    public class Err_Code
    {
        public string CodeName { get; set; }
        public string Description { get; set; }
        public string Reason { get; set; }
        public string Solution { get; set; }
        private string m_FriendlyMessage = string.Empty;
        public string FriendlyMessage
        {
            get { return string.IsNullOrEmpty(m_FriendlyMessage) ? Solution : m_FriendlyMessage; }
            set { m_FriendlyMessage = value; }
        }

        public Err_Code(string codeName, string description, string reason, string solution, string friendlyMessage)
        {
            CodeName = codeName;
            Description = description;
            Reason = reason;
            Solution = solution;
            FriendlyMessage = friendlyMessage;
        }
    }

    public class Err_CodeManager
    {
        private static readonly List<Err_Code> ErrorCodeList = new List<Err_Code>()
            {
                    new Err_Code("NOAUTH","商户无此接口权限","商户未开通此接口权限","请商户前往申请此接口权限",""),
                    new Err_Code("NOTENOUGH","余额不足","用户帐号余额不足","用户帐号余额不足，请用户充值或更换支付卡后再支付",""),
                    new Err_Code("ORDERPAID","商户订单已支付","商户订单已支付，无需重复操作","商户订单已支付，无需更多操作",""),
                    new Err_Code("ORDERCLOSED","订单已关闭","当前订单已关闭，无法支付","当前订单已关闭，请重新下单",""),
                    new Err_Code("SYSTEMERROR","系统错误","系统超时","系统异常，请用相同参数重新调用",""),
                    new Err_Code("APPID_NOT_EXIST","APPID不存在","参数中缺少APPID","请检查APPID是否正确",""),
                    new Err_Code("MCHID_NOT_EXIST","MCHID不存在","参数中缺少MCHID","请检查MCHID是否正确",""),
                    new Err_Code("APPID_MCHID_NOT_MATCH","appid和mch_id不匹配","appid和mch_id不匹配","请确认appid和mch_id是否匹配",""),
                    new Err_Code("LACK_PARAMS","缺少参数","缺少必要的请求参数","请检查参数是否齐全",""),
                    new Err_Code("OUT_TRADE_NO_USED","商户订单号重复","同一笔交易不能多次提交","请核实商户订单号是否重复提交",""),
                    new Err_Code("SIGNERROR","签名错误","参数签名结果不正确","请检查签名参数和方法是否都符合签名算法要求",""),
                    new Err_Code("XML_FORMAT_ERROR","XML格式错误","XML格式错误","请检查XML参数格式是否正确",""),
                    new Err_Code("REQUIRE_POST_METHOD","请使用post方法","未使用post传递参数","请检查请求参数是否通过post方法提交",""),
                    new Err_Code("POST_DATA_EMPTY","post数据为空","post数据不能为空","请检查post数据是否为空",""),
                    new Err_Code("NOT_UTF8","编码格式错误","未使用指定编码格式","请使用NOT_UTF8编码格式","")
            };

        public static Err_Code GetErrCode(string errCodeName)
        {
            var err_Code = ErrorCodeList.Find(errCode => string.Equals(errCode.CodeName, errCodeName, StringComparison.CurrentCultureIgnoreCase));
            if (err_Code == null)
                throw new Exception("未能解析WeChat所暴露出来的异常信息");

            return err_Code;
        }
    }
}
