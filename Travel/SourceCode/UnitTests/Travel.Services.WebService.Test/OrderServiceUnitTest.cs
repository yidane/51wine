using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Travel.Application.DomainModules.Order.Service;
using Travel.Infrastructure.DomainDataAccess.Order;
using System.Collections.Generic;
using System.Linq;

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

        [TestMethod]
        public void SelectTest()
        {
            var list = new List<int>();
            for (int i = 0; i < 10; i++)
            {
                list.Add(i);
            }
            var result = list.Select(item => item < 5);
        }
    }
}
