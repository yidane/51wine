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
    using Travel.Infrastructure.WeiXin.Advanced.Pay;
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
                                   TicketCategory = "D33F0298-A61D-4B4E-A25D-E7A60EB41948",
                                   TicketName = "微信测试门票",
                                   Count = 1,
                                   CouponId = string.Empty,
                                   ContactPersonName = "红酒",
                                   MobilePhoneNumber = "11111",
                                   IdentityCardNumber = "12345678901"
                               };

            this.PaymentNotify = new PaymentNotify()
                                     {
                                         appid = "wxdd6127bdb5e7611c",
                                         attach = "测试123",
                                         bank_type = "CFT",
                                         fee_type = "CNY",
                                         is_subscribe = "Y",
                                         mch_id = "1266087601",
                                         nonce_str = "5d2b6c2a8db53831f7eda20af46e531c",
                                         openid = "obzTswxzFzzzdWdAKf2mWx3CrpXk",
                                         out_trade_no = "C2015091015011979123379",
                                         result_code = "SUCCESS",
                                         return_code = "SUCCESS",
                                         sign = "B552ED6B279343CB493C5DD0D78AB241",
                                         time_end = "20150929131540",
                                         total_fee = 1,
                                         trade_type = "JSAPI",
                                         transaction_id = "1407611653333333"
                                     };

            this.refundTickets =
                TicketEntity.GetTicketsByOrderId(Guid.Parse("AD499E20-BE89-4FE9-B17B-7D76F8BE29DE"))
                .Where(item => item.TicketId.Equals(2120370)).ToList();
        }

        [Test]
        public void CreateOrderCode_CreateOrder_ReturnUnRepeatOrderCode()
        {

            var str = Enum.GetName(typeof(OrderOperationStep), OrderOperationStep.OrderChange);
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
            var service = new OrderService();

            service.SearchTicketStatus(100);
        }

        [Test]
        public void MyOrder_OrderService_ReturnOrderList()
        {
            var service = new OrderService();

            var list = service.MyOrders("obzTswxzFzzzdWdAKf2mWx3CrpXk");
        }

        [Test]
        public void DailyFirstBuyTecket_BuyTicket_ReturnNULL()
        {
            var list = TicketCategoryEntity.TodayTicketCategory;


            var service = new OrderService();

            var category = service.GetTicketCategoryList();

            var first = category.FirstOrDefault().content[0];
            this.OrderRequest.TicketCategory = first.ticketCategoryId;
            this.OrderRequest.TicketName = first.ticketName;
            var otaOrder = new OTAOrder(this.OrderRequest);

            otaOrder.CreateOrderMain();
        }

        [Test]
        public void DailyFirstRefundTecket_RefundTicket_ReturnNULL()
        {
            var service = new OrderService();

            var orders = service.MyOrders("obzTswxzFzzzdWdAKf2mWx3CrpXk").Where(item => item.OrderCode.Equals("C2015092200263658215349")).ToList();

            if (orders.Any())
            {
                service.RefundTickets(orders[0].OrderId, 1);
            }
        }

        [Test]
        public void CompleteRefundTicketOperate_RefundTicket_ReturnChangeTicketStatus()
        {
            //var myOrders = OrderEntity.GetMyOrders("obzTswxzFzzzdWdAKf2mWx3CrpXk").ToList();

            new OrderService().GetMyRefundTicketsStatus("obzTsw3oHcJPbZ8X3mCUnGuCFDOY");
        }

        [Test]
        public void SearchTicketStatus_Order_Return()
        {
            var service = new OrderService();

            service.MyRefundTickets("obzTswxzFzzzdWdAKf2mWx3CrpXk");
            //service.RefundTickets("35D7650B-21C1-45CD-B15D-2203857B7977", 1);
            //service.SearchTicketStatus(100);
        }

        [Test]
        public void RefundTickets()
        {
            var refundTicket = RefundOrderEntity.GetRefundOrder(Guid.Parse("FF72D557-6148-4E2D-A580-8D8F154A6CD6"));

            refundTicket.RefundStatus = "ROS50002";
            refundTicket.Modify();
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

            foreach (var group in t.GroupBy(item => item.OrderId))
            {
                var d = group.ToList();
            }
        }

        [Test]
        public void webservice()
        {
            var service = new OrderService();

            service.GetTicketCategoryList();
            //JsApiPay api = new JsApiPay();

            //api.QueryOrder("1003800117201509080821933860");
            //service.MyTickets("1C99E930-D0D6-420E-B279-D3A9C3CE1930");
            //service.TicketCategoryList();
            //service.MyOrders("obzTswxzFzzzdWdAKf2mWx3CrpXk");
        }

    }
}