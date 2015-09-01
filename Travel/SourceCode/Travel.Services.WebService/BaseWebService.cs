using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Travel.Application.DomainModules.WeChat;

namespace Travel.Services.WebService
{
    public class BaseWebService : System.Web.Services.WebService
    {
        protected string GetOpenIDByCodeID(string code)
        {
            if (Session == null)
            {
                return GetOpenIDFromWeChat(code);
            }
            else
            {
                if (Session["OpenID"] == null)
                {
                    var accessToken = GetOpenIDFromWeChat(code);
                    Session["OpenID"] = accessToken;

                    return accessToken;
                }
                else
                {
                    return Session["OpenID"].ToString();
                }
            }
        }

        private string GetOpenIDFromWeChat(string code)
        {
            var accessToken = new WeChatService().GetAccessToken(code);
            return accessToken.openid;
        }
    }
}