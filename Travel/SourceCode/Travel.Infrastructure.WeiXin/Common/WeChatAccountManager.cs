using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace Travel.Infrastructure.WeiXin.Common
{
    public class WeChatAccountManager
    {
        public string AppId { get; private set; }
        public string AppSecret { get; private set; }

        /// <summary>
        /// 获取默认配置的WeChat账号
        /// </summary>
        /// <returns></returns>
        public static WeChatAccountManager CreateDefaultInstance()
        {
            var appId = ConfigurationManager.AppSettings["WeixinAppId"];
            var appSecret = ConfigurationManager.AppSettings["WeixinSecret"];

            return CreateInstance(appId, appSecret);
        }

        /// <summary>
        /// 获取自定义WeChat账号
        /// </summary>
        /// <param name="appId"></param>
        /// <param name="appSecret"></param>
        /// <returns></returns>
        public static WeChatAccountManager CreateInstance(string appId, string appSecret)
        {
            if (string.IsNullOrEmpty(appId))
                throw new ArgumentNullException("appId", "appID不能为空");
            if (string.IsNullOrEmpty(appSecret))
                throw new ArgumentNullException("appSecret", "appSecret不能为空");

            return new WeChatAccountManager()
            {
                AppId = appId,
                AppSecret = appSecret
            };
        }
    }
}
