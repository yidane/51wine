using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WeiXinPF.WeChat
{
    public class RequireAttribute : Attribute
    {
        public bool Require { get; set; }
        public string ErrorMessage { get; set; }

        public RequireAttribute(bool require, string errorMessage)
        {
            Require = require;
            ErrorMessage = errorMessage;
        }
    }
}
