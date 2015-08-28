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
        public UserInfo GetUserInfoByOpenID(string openId)
        {
            var credential = new Credential(WeChatAccountManager.CreateDefaultInstance());
            var httpHelper = new HttpHelper(WeChatUrlConfigManager.UserManager.GetUserInfoUrl);

            var formData = new FormData()
                {
                    {"access_token",credential.AccessToken},
                    {"openid",openId},
                    {"lang","zh_CN"}
                };

            var result = httpHelper.Get<UserInfo>(formData);

            return result;
        }
    }
}
