using System;
using System.Collections.Generic;
using System.Configuration;
using System.Threading;
using Travel.Infrastructure.WeiXin.Config;

namespace Travel.Infrastructure.WeiXin.Common
{
    public class Credential
    {
        //private static string _accessToken;

        ///// <summary>
        ///// 多access_token缓存（根据appid），满足一个服务器服务于多个微信公号的需求。
        ///// </summary>
        //private readonly static Dictionary<string, string> MultiTokenCache = new Dictionary<string, string>();

        ///// <summary>
        ///// 获取缓存的（最后一次获取的）AccessToken。
        ///// 注意，即使缓存的token已过期或不存在也不会获取新AccessToken。
        ///// </summary>
        //public static string CachedAccessToken
        //{
        //    get
        //    {
        //        return _accessToken;
        //    }
        //}

        /// <summary>
        /// 获取access_token填写client_credential
        /// </summary>
        public readonly static string GrantType = "client_credential";

        public WeChatAccountManager WeChatAccountManager { get; private set; }

        /// <summary>
        /// 获取access_token。会缓存，过期后自动重新获取新的token。
        /// </summary>
        public string AccessToken
        {
            get
            {
                //if (!MultiTokenCache.ContainsKey(WeChatAccountManager.AppId) || string.IsNullOrEmpty(MultiTokenCache[WeChatAccountManager.AppId]))
                //{
                //    var helper = GetHelper();
                //    var ret = helper.Get<TokenResult>(new FormData
                //    {
                //       { "grant_type",GrantType},
                //       { "appid",WeChatAccountManager.AppId},
                //       { "secret",WeChatAccountManager.AppSecret},
                //    });

                //    if (ret.IsSucceed)
                //    {
                //        _accessToken = ret.access_token;
                //        MultiTokenCache[WeChatAccountManager.AppId] = _accessToken;//缓存
                //        var autoEvent = new AutoResetEvent(false);
                //        var timer = new Timer(state =>
                //        {
                //            _accessToken = null;
                //            autoEvent.Set();
                //        }, autoEvent, (ret.expires_in - 3)/*避免时间误差*/ * 1000, Timeout.Infinite);
                //        timer.Dispose(autoEvent);
                //    }
                //}

                //return _accessToken;
                var token = OneGulp.WeChat.MP.CommonAPIs.CommonApi.GetToken(WeChatAccountManager.AppId,
                                                                       WeChatAccountManager.AppSecret);
                if (token != null)
                    return token.access_token;
                return string.Empty;
            }
        }

        private HttpHelper GetHelper()
        {
            var ret = new HttpHelper(WeChatUrlConfigManager.SystemManager.TokenUrl);
            return ret;
        }

        /// <summary>
        /// 获取配置文件中的凭证信息并填充到Credential实例
        /// </summary>
        /// <returns></returns>
        public Credential()
        {
            WeChatAccountManager = WeChatAccountManager.CreateDefaultInstance();
        }

        public Credential(WeChatAccountManager weChatAccountManager)
        {
            WeChatAccountManager = weChatAccountManager;
        }
    }

    /// <summary>
    /// 获取凭证时的响应数据
    /// </summary>
    public class TokenResult
    {
        /// <summary>
        /// 获取到的凭证
        /// </summary>
        public string access_token { get; set; }

        /// <summary>
        /// 凭证有效时间，单位：秒
        /// </summary>
        public int expires_in { get; set; }

        /// <summary>
        /// 错误信息号
        /// </summary>
        public int errcode { get; set; }

        /// <summary>
        /// 错误消息文本
        /// </summary>
        public string errmsg { get; set; }

        /// <summary>
        /// 是否成功
        /// </summary>
        public bool IsSucceed
        {
            get
            {
                return !string.IsNullOrEmpty(access_token);
            }
        }
    }
}
