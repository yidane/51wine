using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Weixin.UserGroup
{
    /// <summary>
    /// 用户分组，一个公众号最多支持500分组。
    /// </summary>
    public class Group
    {
        /// <summary>
        /// 分组id，由微信分配  
        /// </summary>
        public int id { get; set; }

        /// <summary>
        /// 分组名字，UTF8编码 。最长支持30字符。
        /// </summary>
        public string name { get; set; }

    }
}