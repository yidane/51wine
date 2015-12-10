using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OneGulp.WeChat.MP.TenPayLibV3;
using OneGulp.WeChat.MP.TenPayLibV3.TenPayV3.Model;

namespace WeiXinPF.Web.weixin.WeChatPay.Notify
{
    interface IPayNotify
    {
        bool PayNotify(PaymentNotify paymentNotify, out string message);
    }
}
