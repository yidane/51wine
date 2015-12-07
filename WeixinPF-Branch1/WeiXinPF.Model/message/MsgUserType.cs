using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WeiXinPF.Model.message
{
    public enum MsgUserType
    {
        /// <summary>
        /// 单个后台管理员
        /// </summary>
        User = 0,
        /// <summary>
        /// 酒店管理员 组
        /// </summary>
        Hotel,
        /// <summary>
        /// 餐饮管理员 组
        /// </summary>
        Shop,
        /// <summary>
        /// 景区管理员 组
        /// </summary>
        Scenic,
        /// <summary>
        /// 微信用户
        /// </summary>
        WeChatCustomer
    }
}
