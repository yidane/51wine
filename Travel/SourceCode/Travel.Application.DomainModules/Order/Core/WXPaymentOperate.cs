using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Travel.Application.DomainModules.Order.Core
{
    using Travel.Application.DomainModules.Order.Core.Interface;
    using Travel.Infrastructure.DomainDataAccess.Order;
    using Travel.Infrastructure.WeiXin.Advanced.Pay;
    using Travel.Infrastructure.WeiXin.Advanced.Pay.Model;

    public class WXPaymentOperate : IPaymentOperate
    {
        public UnifiedOrderResult ConnectedPaymentPlatform(OrderEntity order)
        {
            var ticket =
                TicketCategoryEntity.TodayTicketCategory.FirstOrDefault(
                    item => item.TicketCategoryId.Equals(order.Tickets.FirstOrDefault().TicketCategoryId));
            var orderRequest = new UnifiedOrderRequest()
                                   {
                                       openid = order.OpenId,
                                       body = ticket.TicketName,
                                       detail = ticket.Price.ToString(),
                                       attach = "1",
                                       goods_tag = "1",
                                       total_fee = Decimal.ToInt32(order.GetCategoryTotalFee(Guid.Parse("{CE7B7E52-3811-44B6-AF9A-7562E0A773D2}")) * 100)
                                   };
            var WXPaymentService = new JsApiPay();

            return WXPaymentService.GetUnifiedOrderResult(orderRequest);
        }

        public string GetPaymentCompleteInfomation(PaymentNotify paymentComplete)
        {
            var wxPay = new WXPayment(paymentComplete);

            wxPay.ProcessPaymentMain();

            return wxPay.WXTurnbackResult;
        }

        public RefundOrderResponse RefundPay(RefundOrderRequest refundOrder)
        {
            return new JsApiPay().Refund(refundOrder);
        }
    }
}
