using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Travel.Infrastructure.WeiXin.Message;

namespace Travel.Common.WeiXin.Plugin
{
    /// <summary>
    /// 响应菜单事件的插件基类。
    /// </summary>
    public class MenuKeyPlugin : Plugin
    {
        public string Key { get; private set; }

        protected MenuKeyPlugin(string key)
        {
            Key = key;
        }

        public override bool CanProcess(PluginContext ctx)
        {
            var msg = ctx.ReceiveMessage;
            return msg is RecEventMessage && Key.Equals((msg as RecEventMessage).EventKey);
        }
    }
}
