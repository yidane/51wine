using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Travel.Application.DomainModules.Order.Core;
using Travel.Application.DomainModules.Order.Entity;

namespace Travel.Infrastructure.Ticket.Test
{
    [TestClass]
    public class OTAOrderTest
    {
        [TestMethod]
        public void CreateOrderMain()
        {
            var orderRequestEntity = new OrderRequestEntity()
            {
                OpenId = "obzTsw5qxlbwGYYZJC9b-91J-X1Y",
                TicketCategory = "956BFF5E-AA6A-454E-8F46-BD4175235C9E",
                TicketName = "布尔津测试门票",
                Count = 1,
                CouponId = string.Empty,
                ContactPersonName = "易必浩",
                MobilePhoneNumber = "18910063051",
                IdentityCardNumber = "420921198603095113"
            };

            var otaOrder = new OTAOrder(orderRequestEntity);

            otaOrder.CreateOrderMain();

            var jsParameter = otaOrder.UnifiedOrderResult.GetJsApiParameters();

            Assert.IsTrue(jsParameter != null);
        }
    }
}
