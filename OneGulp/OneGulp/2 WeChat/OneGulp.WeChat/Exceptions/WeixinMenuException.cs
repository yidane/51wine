/*----------------------------------------------------------------
    Copyright (C) 2015 Senparc
    
    文件名：WeChatException.cs
    文件功能描述：微信菜单异常处理类
    
    
    创建标识：Senparc - 20150211
    
    修改标识：Senparc - 20150303
    修改描述：整理接口
----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OneGulp.WeChat.Exceptions
{
    public class WeChatMenuException : WeChatException
    {
        public WeChatMenuException(string message)
            : base(message, null)
        {
        }

        public WeChatMenuException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
