using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WeiXinPF.Common
{
    /// <summary>
    /// 获取OpenID时的异常
    /// </summary>
    public class UnAuthException : Exception
    {
        public string Code { get; set; }
        public string RedirectUrl { get; set; }

        public UnAuthException(string redirectUrl, string code)
        {
            RedirectUrl = redirectUrl;
            Code = code;
        }
    }
}
