﻿/*----------------------------------------------------------------
    Copyright (C) 2015 Senparc
    
    文件名：OrderApi.cs
    文件功能描述：微小店图片接口
    
    
    创建标识：Senparc - 20150827
----------------------------------------------------------------*/

/* 
   微小店接口，官方API：http://mp.WeChat.qq.com/wiki/index.php?title=%E5%BE%AE%E4%BF%A1%E5%B0%8F%E5%BA%97%E6%8E%A5%E5%8F%A3
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using OneGulp.WeChat.MP.CommonAPIs;
using OneGulp.WeChat.MP.Entities;

namespace OneGulp.WeChat.MP.AdvancedAPIs.MerChant
{
    /// <summary>
    /// 微小店图片接口
    /// </summary>
    public static class PictureApi
    {

        public static PictureResult UploadImg(string accessToken, string fileName)
        {
            var urlFormat = "https://api.WeiXin.qq.com/merchant/common/upload_img?access_token={0}&filename={1}";
            var url = string.IsNullOrEmpty(accessToken) ? urlFormat : string.Format(urlFormat, accessToken, fileName);

            var json=new PictureResult();

            using (var fs = OneGulp.WeChat.Helpers.FileHelper.GetFileStream(fileName))
            {
                var jsonText = OneGulp.WeChat.HttpUtility.RequestUtility.HttpPost(url, null, fs);
                json = OneGulp.WeChat.HttpUtility.Post.GetResult<PictureResult>(jsonText);
            }
            return json;
        }
    }
}
