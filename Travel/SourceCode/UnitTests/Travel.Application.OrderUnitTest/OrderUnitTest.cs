using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Travel.Application.OrderUnitTest
{
    using System.Text.RegularExpressions;

    using NUnit.Framework;

    using Travel.Application.DomainModules.Order.Core;
    using Travel.Application.DomainModules.Order.Entity;
    using Travel.Application.DomainModules.Order.Service;
    using Travel.Infrastructure.DomainDataAccess.Order;
    using Travel.Infrastructure.WeiXin.Advanced.Pay.Model;
    using Travel.Services.WebService;

    [TestFixture]
    public class OrderUnitTest
    {
        public OrderRequestEntity OrderRequest;

        public PaymentNotify PaymentNotify;

        public IList<TicketEntity> refundTickets;
        
        [SetUp]
        public void Init()
        {
            this.OrderRequest = new OrderRequestEntity()
                               {
                                   OpenId = "obzTswxzFzzzdWdAKf2mWx3CrpXk",
                                   TicketCategory = "956BFF5E-AA6A-454E-8F46-BD4175235C9E",
                                   TicketName = "布尔津测试门票",
                                   Count = 1,
                                   CouponId = string.Empty,
                                   ContactPersonName = "龚博",
                                   MobilePhoneNumber = "11111",
                                   IdentityCardNumber = "21212121212121"
                               };

            this.PaymentNotify = new PaymentNotify()
                                     {
                                         appid = "wx2421b1c4370ec43b",
                                         attach = "测试",
                                         bank_type = "CFT",
                                         fee_type = "CNY",
                                         is_subscribe = "Y",
                                         mch_id = "100000100",
                                         nonce_str = "5d2b6c2a8db53831f7eda20af46e531c",
                                         openid = "obzTswxzFzzzdWdAKf2mWx3CrpXk",
                                         out_trade_no = "C2015090121204252482826",
                                         result_code = "SUCCESS",
                                         return_code = "SUCCESS",
                                         sign = "B552ED6B279343CB493C5DD0D78AB241",
                                         time_end = "20150829131540",
                                         total_fee = 260,
                                         trade_type = "JSAPI",
                                         transaction_id = "1409811653"
                                     };

            this.refundTickets =
                TicketEntity.GetTicketsByOrderId(Guid.Parse("EFDC3E4A-737F-4A3F-8DAE-1ADC36923764"))
                .Where(item => item.TicketId.Equals(57963)).ToList();
        }

        [Test]
        public void CreateOrderCode_CreateOrder_ReturnUnRepeatOrderCode()
        {
            var otaOrder = new OTAOrder(this.OrderRequest);

            otaOrder.CreateOrderMain();
        }

        [Test]
        public void GetPaymentNotify_PaymentNotify_ReturnVoid()
        {
            var wxPay = new WXPaymentOperate();

            wxPay.GetPaymentCompleteInfomation(this.PaymentNotify);
        }

        [Test]
        public void SubmitRefundRequest_RefundOrder_ReturnAddRefundToRefundQueue()
        {
            if (refundTickets != null && refundTickets.Any())
            {
                var order = OrderEntity.GetOrderByOrderId(refundTickets.First().OrderId);
                var otaOrder = new OTAOrder(order);

                otaOrder.ProcessRefundRequestMain(this.refundTickets);
            }            
        }

        [Test]
        public void RefundPay_RefundOrder_ReturnWXComfirmRefundPayRequest()
        {
            if (refundTickets != null && refundTickets.Any())
            {
                var order = OrderEntity.GetOrderByOrderId(refundTickets.First().OrderId);
                var otaOrder = new OTAOrder(order);

                otaOrder.ProcessRefundPayment(refundTickets);
            }
        }

        [Test]
        public void SearchTicketStatus_Order_Return()
        {
            var service = new OrderService();

            service.SearchTicketStatus(100);
        }

        [Test]
        public void procedureTest()
        {
            var t = new List<TicketEntity>()
                        {
                            new TicketEntity()
                              {
                                  TicketCode = "gb",
                                  OrderId = Guid.Parse("1D580DEE-2E6D-4323-AB40-984FCD4901D5")
                              },
                              new TicketEntity()
                                  {
                                      TicketCode = "gbs",
                                    OrderId = Guid.Parse("1D580DEE-2E6D-4323-AB40-984FCD4901D5")
                                  },
                                  new TicketEntity()
                                      {
                                          TicketCode = "gbc",
                                            OrderId = Guid.Parse("1D580DEE-2E6D-4323-AB40-984FCD4901D5")
                                      },
                                      new TicketEntity()
                                          {
                                              TicketCode = "ss",
                                                OrderId = Guid.Parse("BC4FEFE8-8214-41DD-8FE8-1B16394B77B3")
                                          },
                                           new TicketEntity()
                                          {
                                              TicketCode = "ss",
                                                OrderId = Guid.Parse("BC4FEFE8-8214-41DD-8FE8-1B16394B77B3")
                                          }
                        };

            foreach (var group in t.GroupBy(item=>item.OrderId))
            {
                var d = group.ToList();
            }
        }

        [Test]
        public void webservice()
        {
            var service = new OrderWebService();

            service.MyOrders("obzTswxzFzzzdWdAKf2mWx3CrpXk");
        }
    }
}