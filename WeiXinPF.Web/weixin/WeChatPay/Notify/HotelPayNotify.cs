using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WeiXinPF.Web.weixin.WeChatPay.Notify
{
    using System.Transactions;

    using WeiXinPF.Application.DomainModules.IdentifyingCode.Service;

    public class HotelPayNotify : IPayNotify
    {
        public bool PayNotify(OneGulp.WeChat.MP.TenPayLibV3.PaymentNotify paymentNotify, out string message)
        {
            message = string.Empty;
            var Pay_Ready = 1;
            var isModifySuccess = false;

            //完成支付后续操作
            try
            {
                using (var scope = new TransactionScope())
                {
                    new BLL.wx_hotel_dingdan().PaySuccess(paymentNotify.out_trade_no);
                    isModifySuccess = IdentifyingCodeService.ModifyIdentifyingCodeInfoStatus(paymentNotify.out_trade_no, Pay_Ready);

                    if (isModifySuccess)
                    {
                        scope.Complete();
                    }
                }
                
                return isModifySuccess;
            }
            catch (Exception exception)
            {
                message = exception.Message;
                return false;
            }
        }
    }
}
