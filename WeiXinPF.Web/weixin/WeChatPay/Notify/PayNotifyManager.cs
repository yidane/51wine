using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OneGulp.WeChat.MP.TenPayLibV3;
using WeiXinPF.Infrastructure.DomainDataAccess.Payment;

namespace WeiXinPF.Web.weixin.WeChatPay.Notify
{
    public class PayNotifyManager
    {
        public static bool PayNotify(PayModuleEnum payModule, PaymentNotify paymentNotify, out string message)
        {
            message = string.Empty;
            IPayNotify payNotify = null;

            //记录通知信息
            SavePayNotifyInfo(payModule, paymentNotify);

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

        private static void SavePayNotifyInfo(PayModuleEnum payModule, PaymentNotify paymentNotify)
        {
            var payNotifyInfo = new PayNotifyInfo();
            payNotifyInfo.ModuleName = payModule.ToString();
            payNotifyInfo.NotifyID = Guid.NewGuid();
            payNotifyInfo.appid = paymentNotify.appid;
            payNotifyInfo.attach = paymentNotify.attach;
            payNotifyInfo.bank_type = paymentNotify.bank_type;
            payNotifyInfo.cash_fee = paymentNotify.cash_fee;
            payNotifyInfo.cash_fee_type = paymentNotify.cash_fee_type;
            payNotifyInfo.coupon_count = paymentNotify.coupon_count;
            payNotifyInfo.coupon_fee = paymentNotify.coupon_fee;
            payNotifyInfo.CreateTime = DateTime.Now;
            payNotifyInfo.device_info = paymentNotify.device_info;
            payNotifyInfo.err_code = paymentNotify.err_code;
            payNotifyInfo.err_code_des = paymentNotify.err_code_des;
            payNotifyInfo.fee_type = paymentNotify.fee_type;
            payNotifyInfo.is_subscribe = paymentNotify.is_subscribe;
            payNotifyInfo.mch_id = paymentNotify.mch_id;
            payNotifyInfo.nonce_str = paymentNotify.nonce_str;
            payNotifyInfo.openid = paymentNotify.openid;
            payNotifyInfo.out_trade_no = paymentNotify.out_trade_no;
            payNotifyInfo.result_code = paymentNotify.result_code;
            payNotifyInfo.return_code = paymentNotify.return_code;
            payNotifyInfo.return_msg = paymentNotify.return_msg;
            payNotifyInfo.sign = paymentNotify.sign;
            payNotifyInfo.time_end = paymentNotify.time_end;
            payNotifyInfo.total_fee = paymentNotify.total_fee;
            payNotifyInfo.trade_type = paymentNotify.trade_type;
            payNotifyInfo.transaction_id = paymentNotify.transaction_id;

            payNotifyInfo.Add();
        }
    }
}