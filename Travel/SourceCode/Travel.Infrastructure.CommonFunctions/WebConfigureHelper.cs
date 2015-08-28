using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Travel.Infrastructure.CommonFunctions
{
    public class WebConfigureHelper
    {
        public class ConnectionStrings
        {

            #region DbConnection
            private static string m_DbConnection = string.Empty;
            /// <summary>
            /// 数据库连接字符串
            /// </summary>
            public static string DbConnection
            {
                get
                {
                    if (string.IsNullOrEmpty(m_DbConnection))
                        m_DbConnection = System.Configuration.ConfigurationManager.AppSettings["DBConnection"];

                    return m_DbConnection;
                }
            }
            #endregion
        }

        public class Appsettings
        {
            public class WeChatSetting
            {
                /// <summary>
                /// 微信账户信息
                /// </summary>
                public class WeChatAccount
                {
                    /// <summary>
                    /// 公众号AppID
                    /// </summary>
                    public string AppID
                    {
                        get
                        {
                            var appId = System.Configuration.ConfigurationManager.AppSettings["AppID"];
                            if (string.IsNullOrEmpty(appId))
                                throw new Exception("AppID尚未配置或配置的值为空");
                            return appId;
                        }
                    }

                    /// <summary>
                    /// 公众号密码
                    /// </summary>
                    public string AppSecret
                    {
                        get
                        {
                            var appSecret = System.Configuration.ConfigurationManager.AppSettings["AppSecret"];
                            if (string.IsNullOrEmpty(appSecret))
                                throw new Exception("AppSecret尚未配置或配置的值为空");
                            return appSecret;
                        }
                    }
                }

                public class WeChatPay
                {
                    /// <summary>
                    /// 商户号
                    /// </summary>
                    public string MCHID
                    {
                        get
                        {
                            var mCHID = System.Configuration.ConfigurationManager.AppSettings["MCHID"];
                            if (string.IsNullOrEmpty(mCHID))
                                throw new Exception("mCHID尚未配置或配置的值为空");
                            return mCHID;
                        }
                    }

                    /// <summary>
                    /// 商户支付密码
                    /// </summary>
                    public string KEY
                    {
                        get
                        {
                            var mCHID = System.Configuration.ConfigurationManager.AppSettings["MCHID"];
                            if (string.IsNullOrEmpty(mCHID))
                                throw new Exception("mCHID尚未配置或配置的值为空");
                            return mCHID;
                        }
                    }

                    /// <summary>
                    /// 统一下单时候订单失效时间，单位（分钟）
                    /// </summary>
                    public int UnifiedOrderTimeExpire
                    {
                        get
                        {
                            //微信设置订单失效的最小时间为5分钟
                            var timeExpire = 5;
                            var timeExpireString = System.Configuration.ConfigurationManager.AppSettings["UnifiedOrderTimeExpire"];
                            if (string.IsNullOrEmpty(timeExpireString))
                                return timeExpire;
                            int.TryParse(timeExpireString, out timeExpire);
                            return timeExpire;
                        }
                    }

                    /// <summary>
                    /// 统一下单操作过期时间，单位（秒）
                    /// </summary>
                    public int UnifiedOrderTimeOut
                    {
                        get
                        {
                            //设置过期时间为6秒
                            var timeExpire = 6;
                            var timeExpireString = System.Configuration.ConfigurationManager.AppSettings["UnifiedOrderTimeOut"];
                            if (string.IsNullOrEmpty(timeExpireString))
                                return timeExpire;
                            int.TryParse(timeExpireString, out timeExpire);
                            return timeExpire;
                        }
                    }

                    /// <summary>
                    /// 微信下单异步通知url
                    /// </summary>
                    public string UnifiedOrderNotifyUrl
                    {
                        get
                        {
                            var unifiedOrderNotifyUrl = System.Configuration.ConfigurationManager.AppSettings["UnifiedOrderNotifyUrl"];
                            if (string.IsNullOrEmpty(unifiedOrderNotifyUrl))
                                throw new Exception("UnifiedOrderNotifyUrl尚未配置或配置的值为空");
                            return unifiedOrderNotifyUrl;
                        }
                    }

                    /// <summary>
                    /// 证书路径
                    /// </summary>
                    public string SSLCERT_PATH
                    {
                        get
                        {
                            var cerPath = System.Configuration.ConfigurationManager.AppSettings["SSLCERT_PATH"];
                            if (string.IsNullOrEmpty(cerPath))
                                throw new Exception("SSLCERT_PATH尚未配置或配置的值为空");
                            return cerPath;
                        }
                    }

                    /// <summary>
                    /// 证书密码
                    /// </summary>
                    public string SSLCERT_PASSWORD
                    {
                        get
                        {
                            var cerPassword = System.Configuration.ConfigurationManager.AppSettings["SSLCERT_PASSWORD"];
                            if (string.IsNullOrEmpty(cerPassword))
                                throw new Exception("SSLCERT_PASSWORD尚未配置或配置的值为空");
                            return cerPassword;
                        }
                    }
                }
            }

            /// <summary>
            /// OTA服务参数配置
            /// </summary>
            public class OTAConfigSetting
            {
                public static string OTAServiceUrl
                {
                    get
                    {
                        var oTAServiceUrl = System.Configuration.ConfigurationManager.AppSettings["OTAServiceUrl"];
                        if (string.IsNullOrEmpty(oTAServiceUrl))
                            throw new Exception("OTAServiceUrl尚未配置或配置的值为空");
                        return oTAServiceUrl;
                    }
                }

                public static string MerchantCode
                {
                    get
                    {
                        var merchantCode = System.Configuration.ConfigurationManager.AppSettings["MerchantCode"];
                        if (string.IsNullOrEmpty(merchantCode))
                            throw new Exception("MerchantCode尚未配置或配置的值为空");
                        return merchantCode;
                    }
                }

                public static string MerchantKey
                {
                    get
                    {
                        var merchantKey = System.Configuration.ConfigurationManager.AppSettings["MerchantKey"];
                        if (string.IsNullOrEmpty(merchantKey))
                            throw new Exception("MerchantKey尚未配置或配置的值为空");
                        return merchantKey;
                    }
                }

                public static string ParkCode
                {
                    get
                    {
                        var parkCode = System.Configuration.ConfigurationManager.AppSettings["ParkCode"];
                        if (string.IsNullOrEmpty(parkCode))
                            throw new Exception("ParkCode尚未配置或配置的值为空");
                        return parkCode;
                    }
                }
            }
        }
    }
}
