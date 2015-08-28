using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Travel.Infrastructure.WeiXin.Config;

namespace Travel.Infrastructure.WeiXin.Common.OAuth2
{
    public class AccessToken
    {
        private readonly WeChatAccountManager m_WeChatAccountManager = null;
        public AccessToken(WeChatAccountManager weChatAccountManager)
        {
            m_WeChatAccountManager = weChatAccountManager;
        }

        public AccessTokenResult GetBaseAsseccTokenResult(string code)
        {
            var formData = new FormData()
                {
                    {"appid",m_WeChatAccountManager.AppId},
                    {"secret",m_WeChatAccountManager.AppSecret},
                    {"code",code},
                    {"grant_type","authorization_code"}
                };
            var httpHelper = CreateHttpHelper();
            return httpHelper.Get<AccessTokenResult>(formData);
        }

        private HttpHelper CreateHttpHelper()
        {
            var httpHelper = new HttpHelper(WeChatUrlConfigManager.SystemManager.Access_TokenUrl);
            return httpHelper;
        }
    }

    public class AccessTokenResult
    {
        public string access_token { get; set; }
        public int expires_in { get; set; }
        public string refresh_token { get; set; }
        public string openid { get; set; }
        public string scope { get; set; }
    }
}
