using Travel.Infrastructure.CommonFunctions;

namespace Travel.Infrastructure.WeiXin.Advanced.Pay
{
    public class WxPayConfig
    {
        //=======【基本信息设置】=====================================
        /* 微信公众号信息配置
        * APPID：绑定支付的APPID（必须配置）
        * MCHID：商户号（必须配置）
        * KEY：商户支付密钥，参考开户邮件设置（必须配置）
        * APPSECRET：公众帐号secert（仅JSAPI支付的时候需要配置）
        */

        /// <summary>
        /// 公众号AppID
        /// </summary>
        public static string APPID
        {
            get
            {
                return WebConfigureHelper.Appsettings.WeChatSetting.WeChatAccount.AppID;
            }
        }

        /// <summary>
        /// 公众号密码
        /// </summary>
        public static string APPSECRET
        {
            get
            {
                return WebConfigureHelper.Appsettings.WeChatSetting.WeChatAccount.AppSecret;
            }
        }

        /// <summary>
        /// 商户号
        /// </summary>
        public static string MCHID
        {
            get
            {
                return WebConfigureHelper.Appsettings.WeChatSetting.WeChatPay.MCHID;
            }
        }

        /// <summary>
        /// 商户支付密码
        /// </summary>
        public static string KEY
        {
            get
            {
                return WebConfigureHelper.Appsettings.WeChatSetting.WeChatPay.KEY;
                return "4A5E7B87F3324A6DA22E55FDC12150B6";
            }
        }

        //=======【证书路径设置】===================================== 
        /* 证书路径,注意应该填写绝对路径（仅退款、撤销订单时需要）
        */
        public static string SSLCERT_PATH
        {
            get
            {
                return WebConfigureHelper.Appsettings.WeChatSetting.WeChatPay.SSLCERT_PATH;
            }
        }

        public static string SSLCERT_PASSWORD
        {
            get
            {
                return WebConfigureHelper.Appsettings.WeChatSetting.WeChatPay.SSLCERT_PASSWORD;
            }
        }

        /// <summary>
        /// 微信请求过期时间
        /// </summary>
        public int UnifiedOrderTimeOut
        {
            get
            {
                return WebConfigureHelper.Appsettings.WeChatSetting.WeChatPay.UnifiedOrderTimeOut;
            }
        }

        /// <summary>
        /// 微信下单订单失效时间
        /// </summary>
        public int UnifiedOrderTimeExpire
        {
            get
            {
                return WebConfigureHelper.Appsettings.WeChatSetting.WeChatPay.UnifiedOrderTimeExpire;
            }
        }

        //=======【支付结果通知url】===================================== 
        /* 支付结果通知回调url，用于商户接收支付结果
        */
        public static string NOTIFY_URL
        {
            get
            {
                return WebConfigureHelper.Appsettings.WeChatSetting.WeChatPay.UnifiedOrderNotifyUrl;
            }
        }

        //=======【商户系统后台机器IP】===================================== 
        /* 此参数可手动配置也可在程序中自动获取
        */
        public static string IP
        {
            get
            {
                return "219.234.2.189";
            }
        }


        //=======【代理服务器设置】===================================
        /* 默认IP和端口号分别为0.0.0.0和0，此时不开启代理（如有需要才设置）
        */
        public static string PROXY_URL
        {
            get { return "proxy1.bj.petrochina:8080"; }
        }

        //=======【上报信息配置】===================================
        /* 测速上报等级，0.关闭上报; 1.仅错误时上报; 2.全量上报
        */
        public const int REPORT_LEVENL = 1;

        //=======【日志级别】===================================
        /* 日志等级，0.不输出日志；1.只输出错误信息; 2.输出错误和正常信息; 3.输出错误信息、正常信息和调试信息
        */
        public const int LOG_LEVENL = 0;
    }
}
