using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Travel.Application.OrderUnitTest
{
    using NUnit.Framework;

    using Travel.Application.DomainModules.Order.Core;
    using Travel.Application.DomainModules.Order.Entity;

    [TestFixture]
    public class OrderUnitTest
    {
        public OrderRequestEntity OrderRequest;

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
        }

        [Test]
        public void CreateOrderCode_CreateOrder_ReturnUnRepeatOrderCode()
        {
            var otaOrder = new OTAOrder(this.OrderRequest);

            otaOrder.CreateOrderMain();
        }

        [Test]
        public void SaveOrder_BuyTicket_ReturnVoid()
        {
            

           
        }
    }
}