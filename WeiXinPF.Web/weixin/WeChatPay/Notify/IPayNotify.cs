using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OneGulp.WeChat.MP.TenPayLibV3;

namespace WeiXinPF.Web.weixin.WeChatPay.Notify
{
    interface IPayNotify
    {
        bool PayNotify(PaymentNotify paymentNotify, out string message);
    }
}
