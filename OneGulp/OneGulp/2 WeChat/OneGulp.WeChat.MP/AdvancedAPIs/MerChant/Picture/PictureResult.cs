﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OneGulp.WeChat.Entities;

namespace OneGulp.WeChat.MP.AdvancedAPIs.MerChant
{
    /// <summary>
    /// 上传图片返回结果
    /// </summary>
    public class PictureResult : WxJsonResult
    {
        /// <summary>
        /// 图片Url
        /// </summary>
        public string image_url { get; set; }
    }
}

