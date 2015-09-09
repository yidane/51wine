using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace WeiXinPF.Common
{
    public class JsonException : ApplicationException, ISerializable
    {
        public string ErrorType { get; set; }
        public JsonException(string message,string type) : base(message)
        {

            ErrorType = type;
        }

        public JsonException(string message, string type, Exception innerException)
                : base(message,innerException)
            {
            ErrorType = type;
        } 
    }
}
