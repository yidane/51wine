using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Travel.Infrastructure.WeiXin.Message;

namespace Travel.Common.WeiXin.Plugin
{
    /// <summary>
    /// Invoker的上下文环境
    /// </summary>
    public class InvokerContext
    {
        public ReceiveMessage Message { get; set; }
    }
}
