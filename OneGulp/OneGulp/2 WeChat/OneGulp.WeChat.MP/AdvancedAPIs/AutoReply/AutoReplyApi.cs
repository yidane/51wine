/*----------------------------------------------------------------
    Copyright (C) 2015 Senparc
    
    文件名：AutoReplyApi.cs
    文件功能描述：获取自动回复规则接口
    
    
    创建标识：Senparc - 20150907
----------------------------------------------------------------*/

/*
    Api地址：http://mp.WeChat.qq.com/wiki/7/7b5789bb1262fb866d01b4b40b0efecb.html
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OneGulp.WeChat.HttpUtility;
using OneGulp.WeChat.MP.AdvancedAPIs.Analysis;
using OneGulp.WeChat.MP.AdvancedAPIs.AutoReply;
using OneGulp.WeChat.MP.CommonAPIs;

namespace OneGulp.WeChat.MP.AdvancedAPIs
{
    /// <summary>
    /// 获取自动回复规则
    /// </summary>
    public static class AutoReplyApi
    {
        /// <summary>
        /// 获取自动回复规则
        /// </summary>
        /// <param name="accessTokenOrAppId">调用接口凭证</param>
        /// <returns></returns>
        public static GetCurrentAutoreplyInfoResult GetCurrentAutoreplyInfo(string accessTokenOrAppId)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                string urlFormat = "https://api.WeiXin.qq.com/cgi-bin/get_current_autoreply_info?access_token={0}";

                return CommonJsonSend.Send<GetCurrentAutoreplyInfoResult>(accessToken, urlFormat, null, CommonJsonSendType.GET);

            }, accessTokenOrAppId);
        }
    }
}