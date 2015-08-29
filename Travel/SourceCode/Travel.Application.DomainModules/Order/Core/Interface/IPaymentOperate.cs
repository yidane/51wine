using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Travel.Application.DomainModules.Order.Core.Interface
{
    using Travel.Infrastructure.DomainDataAccess.Order;
    using Travel.Infrastructure.WeiXin.Advanced.Pay.Model;

    public interface IPaymentOperate
    {
        //Order MainOrder { get; set; }

        /// <summary>
        /// 本地订单生成后，向微信支付提交预支付请求
        /// </summary>
        /// <param name="order"></param>
        /// <returns>预支付交易会话标识等信息（prepay_id）</returns>
        UnifiedOrderResult ConnectedPaymentPlatform(OrderEntity order);

        /// <summary>
        /// 供微信支付通知使用方法
        /// </summary>
        /// <param name="paymentComplete"></param>
        /// <returns>处理是否成功</returns>
        string GetPaymentCompleteInfomation(PaymentNotify paymentComplete);
    }
}
