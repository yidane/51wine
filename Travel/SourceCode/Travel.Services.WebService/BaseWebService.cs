using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Travel.Application.DomainModules.WeChat;
using Travel.Infrastructure.WeiXin.User;

namespace Travel.Services.WebService
{
    public class BaseWebService : System.Web.Services.WebService
    {
        protected string GetOpenIdByCodeId(string code)
        {
            string openId;
            if (Session == null)
            {
                openId = GetOpenIDFromWeChat(code);
            }
            else
            {
#if DEBUG
                Session["OpenId"] = "obzTswxzFzzzdWdAKf2mWx3CrpXk";
#endif
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

        /// <summary>
        /// 判断是否OpenId
        /// </summary>
        /// <param name="openId"></param>
        /// <returns></returns>
        protected bool IsOpenId(string openId)
        {
            var userInfo = new UserInfoHelper().GetUserInfoByOpenId(openId);
            return userInfo != null;
        }

        private string GetOpenIDFromWeChat(string code)
        {
            var accessToken = new WeChatService().GetAccessToken(code);
            return accessToken.openid;
        }
    }
}