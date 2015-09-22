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
            var product = ProductCategoryEntity.ProductCategory.FirstOrDefault(item => item.ProductCategoryId.Equals(order.Tickets.FirstOrDefault().TicketCategoryId));

            var orderRequest = new UnifiedOrderRequest()
                                   {
                                       openid = order.OpenId,
                                       body = product.ProductName,
                                       detail = product.ProductPrice.ToString(),
                                       out_trade_no = order.OrderCode,
                                       attach = "1",
                                       goods_tag = "1",
                                       total_fee = order.Tickets.Count * 2 //Decimal.ToInt32(order.GetCategoryTotalFee(Guid.Parse(OrderStatus.OrderDetailCategory_Create)) * 100)
                                   };
            var WXPaymentService = new JsApiPay();

            return WXPaymentService.GetUnifiedOrderResult(orderRequest);
        }

        public string GetPaymentCompleteInfomation(PaymentNotify paymentComplete)
        {
            var wxPay = new WXPayment(paymentComplete);

            try
            {
                var log = new InterfaceOperationLogEntity()
                {
                    InterfaceOperationLogId = Guid.NewGuid(),
                    CreateTime = DateTime.Now,
                    Module = "微信支付通知",
                    OrderCode = paymentComplete.out_trade_no,
                    OperateObjectId = paymentComplete.transaction_id,
                    OperateName = "PaymentNotify",
                    IsOperateSuccess = paymentComplete.result_code.ToUpper().Equals("SUCCESS") && paymentComplete.return_code.ToUpper().Equals("SUCCESS")
                };
                log.ErrorCode = log.IsOperateSuccess ? string.Empty : paymentComplete.err_code;
                log.ErrorDescription = log.IsOperateSuccess ? string.Empty : paymentComplete.err_code_des;
                log.Add();
            }
            catch (Exception)
            {

            }

            wxPay.ProcessPaymentMain();

            return wxPay.WXTurnbackResult;
        }

        public RefundOrderResponse RefundPay(RefundOrderRequest refundOrder)
        {
            return new JsApiPay().Refund(refundOrder);
        }

        public RefundQueryResponse RefundQuery(string refundOrderCode)
        {
            var request = new RefundQueryRequest() { out_refund_no = refundOrderCode };

            return new JsApiPay().RefundQuery(request);
        }
    }
}
