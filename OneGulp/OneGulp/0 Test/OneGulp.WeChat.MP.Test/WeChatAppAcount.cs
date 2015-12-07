using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OneGulp.WeChat.MP.Entities;

namespace OneGulp.WeChat.MP.Test
{
    public class WeChatAppAcount
    {
        #region 华清蜃景账户密码
        //protected static string appId = "wxdd6127bdb5e7611c";
        //protected static string appSecret = "78fb32f17d30a6ade836319283ccf118";
        #endregion

        #region MyRegion
        protected static string appId = "wx4986bb37d9c48302";
        protected static string appSecret = "02b5f776b089dcef370daefa1ee42b50";
        #endregion

        protected string access_token
        {
            get
            {
                var token = GetToken();
                if (token != null)
                    return token.access_token;
                Assert.IsFalse(false, "获取asscee_tokn失败");
                return String.Empty;
            }
        }

        /// <summary>
        /// 获取Token
        /// </summary>
        /// <returns></returns>
        protected AccessTokenResult GetToken()
        {
            return CommonAPIs.CommonApi.GetToken(appId, appSecret);
        }
    }
}