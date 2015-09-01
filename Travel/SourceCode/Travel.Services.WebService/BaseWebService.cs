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
            string openId;
            if (Session == null)
            {
                openId = GetOpenIDFromWeChat(code);
            }
            else
            {
                //Session["OpenId"] = "obzTswxzFzzzdWdAKf2mWx3CrpXk";
                if (Session["OpenID"] == null)
                {
                    openId = GetOpenIDFromWeChat(code);
                    Session["OpenID"] = openId;
                }
                else
                {
                    openId = Session["OpenID"].ToString();
                }
            }

            if (string.IsNullOrEmpty(openId))
                throw new Exception("获取OpenID失败");

            return openId;
        }

        private string GetOpenIDFromWeChat(string code)
        {
            var accessToken = new WeChatService().GetAccessToken(code);
            return accessToken.openid;
        }
    }
}