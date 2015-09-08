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
                                       out_trade_no = order.OrderCode,
                                       attach = "1",
                                       goods_tag = "1",
                                       total_fee = 2  //Decimal.ToInt32(order.GetCategoryTotalFee(Guid.Parse(OrderStatus.OrderDetailCategory_Create)) * 100)
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
