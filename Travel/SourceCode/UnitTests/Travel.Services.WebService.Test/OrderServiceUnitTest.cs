using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Travel.Application.DomainModules.Order.Service;
using Travel.Infrastructure.DomainDataAccess.Order;

namespace Travel.Services.WebService.Test
{
    [TestClass]
    public class OrderServiceUnitTest
    {
        [TestMethod]
        public void CompleteRefundTicketOperate_RefundTicket_ReturnChangeTicketStatus()
        {
            var myOrders = OrderEntity.GetMyOrders("obzTswxzFzzzdWdAKf2mWx3CrpXk").ToList();

            new OrderService().GetMyRefundTicketsStatus("obzTswxzFzzzdWdAKf2mWx3CrpXk");
        }
    }
}
