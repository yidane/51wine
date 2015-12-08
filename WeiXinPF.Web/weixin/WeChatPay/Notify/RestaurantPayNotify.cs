using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WeiXinPF.Web.weixin.WeChatPay.Notify
{
    public class RestaurantPayNotify : IPayNotify
    {
        public bool PayNotify(OneGulp.WeChat.MP.TenPayLibV3.TenPayV3.Model.PaymentNotify paymentNotify, out string message)
        {
            message = string.Empty;

            //完成支付后续操作
            try
            {
                new BLL.wx_diancai_dingdan_manage().PaySuccess(paymentNotify.out_trade_no);
                return true;
            }
            catch (Exception exception)
            {
                message = exception.Message;
                return false;
            }
        }
    }
}