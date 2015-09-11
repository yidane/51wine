using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeiXinPF.Common
{
    public class ErrorInfo
    {
        public int Code { get; set; }
        public string Message { get; set; }
        public string Details { get; set; }
        public ErrorInfo()
        {

        }
        public ErrorInfo(string message)
        {
            Message = message;
        }

        public ErrorInfo(int code)
        {
            Code = code;
        }

        public ErrorInfo(int code, string message)
            : this(message)
        {
            Code = code;
        }

        public ErrorInfo(string message, string details)
            : this(message)
        {
            Details = details;
        }

        public ErrorInfo(int code, string message, string details)
            : this(message, details)
        {
            Code = code;
        }
    }
}
