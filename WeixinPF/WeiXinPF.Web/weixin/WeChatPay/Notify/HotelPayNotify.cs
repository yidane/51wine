using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WeiXinPF.Web.weixin.WeChatPay.Notify
{
    public class HotelPayNotify : IPayNotify
    {
        public bool PayNotify(OneGulp.WeChat.MP.TenPayLibV3.PaymentNotify paymentNotify, out string message)
        {
            throw new NotImplementedException();
        }
    }
}