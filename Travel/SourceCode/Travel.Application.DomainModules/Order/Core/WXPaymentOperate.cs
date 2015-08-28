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
                                       body = ticket.TicketName,
                                       detail = ticket.Price.ToString(),
                                       total_fee = Decimal.ToInt32(order.TotalFee() * 100)
                                   };
            var WXPaymentService = new JsApiPay();

            return WXPaymentService.GetUnifiedOrderResult(orderRequest);
        }

        public void GetPaymentCompleteInfomation(object paymentComplete)
        {
            throw new NotImplementedException();
        }
    }
}
