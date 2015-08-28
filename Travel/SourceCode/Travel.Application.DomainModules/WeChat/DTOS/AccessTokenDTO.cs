using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Travel.Infrastructure.WeiXin.Common.OAuth2;

namespace Travel.Application.DomainModules.WeChat.DTOS
{
    public class AccessTokenDTO
    {
        public string access_token { get; set; }
        public int expires_in { get; set; }
        public string refresh_token { get; set; }
        public string openid { get; set; }
        public string scope { get; set; }

        public AccessTokenDTO(AccessTokenResult accessTokenResult)
        {
            this.access_token = accessTokenResult.access_token;
            this.expires_in = accessTokenResult.expires_in;
            this.refresh_token = accessTokenResult.refresh_token;
            this.openid = accessTokenResult.openid;
            this.scope = accessTokenResult.scope;
        }
    }
}