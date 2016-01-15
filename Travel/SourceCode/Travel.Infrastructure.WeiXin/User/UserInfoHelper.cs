using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Travel.Infrastructure.WeiXin.Common;
using Travel.Infrastructure.WeiXin.Config;

namespace Travel.Infrastructure.WeiXin.User
{
    public class UserInfoHelper
    {
        public UserInfo GetUserInfoByOpenId(string openId)
        {
            if (string.IsNullOrEmpty(openId))
                return null;

            var weChatAccountManager = WeChatAccountManager.CreateDefaultInstance();
            var token = OneGulp.WeChat.MP.CommonAPIs.CommonApi.GetToken(weChatAccountManager.AppId, weChatAccountManager.AppSecret);
            var userInfo = OneGulp.WeChat.MP.CommonAPIs.CommonApi.GetUserInfo(token.access_token, openId);
            if (userInfo != null)
            {
                var rtnUserInfo = new UserInfo();
                rtnUserInfo.city = userInfo.city;
                rtnUserInfo.country = userInfo.country;
                rtnUserInfo.groupid = userInfo.groupid;
                rtnUserInfo.headimgurl = userInfo.headimgurl;
                rtnUserInfo.language = userInfo.language;
                rtnUserInfo.nickname = userInfo.nickname;
                rtnUserInfo.openid = userInfo.openid;
                rtnUserInfo.province = userInfo.province;
                rtnUserInfo.remark = userInfo.remark;
                rtnUserInfo.sex = userInfo.sex;
                rtnUserInfo.subscribe = userInfo.subscribe;
                rtnUserInfo.subscribe_time = userInfo.subscribe_time;

                return rtnUserInfo;
            }

            return null;
        }

        public UserInfo GetUserInfoByOpenID(string accessToken, string openId)
        {
            var httpHelper = new HttpHelper(WeChatUrlConfigManager.UserManager.GetUserInfoUrl);
            var formData = new FormData()
                {
                    {"access_token",accessToken},
                    {"openid",openId},
                    {"lang","zh_CN"}
                };

            var result = httpHelper.Get<UserInfo>(formData);

            return result;
        }
    }
}
