using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OneGulp.WeChat.MP.TenPayLibV3;

namespace WeiXinPF.Web.weixin.WeChatPay.Notify
{
    public class PayNotifyManager
    {
        public static bool PayNotify(PayModuleEnum payModule, PaymentNotify paymentNotify, out string message)
        {
            message = string.Empty;
            IPayNotify payNotify = null;
            switch (payModule)
            {
                case PayModuleEnum.Restaurant:
                    payNotify = new RestaurantPayNotify();
                    break;
                case PayModuleEnum.Hotel:
                    payNotify = new HotelPayNotify();
                    break;
                default:
                    message = "异常参数支付模块ID";
                    return false;
            }

            return payNotify.PayNotify(paymentNotify, out message);
        }
    }
}