﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WeiXinPF.Web.weixin.WeChatPay.Notify
{
    public class HotelPayNotify : IPayNotify
    {
        public bool PayNotify(OneGulp.WeChat.MP.TenPayLibV3.PaymentNotify paymentNotify, out string message)
        {
            message = string.Empty;

            //完成支付后续操作
            try
            {
                new BLL.wx_hotel_dingdan().PaySuccess(paymentNotify.out_trade_no);
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
