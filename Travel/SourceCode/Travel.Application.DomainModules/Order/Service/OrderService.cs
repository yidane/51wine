using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Travel.Application.DomainModules.Order.Service
{
    using Travel.Infrastructure.DomainDataAccess.Order;
    using Travel.Infrastructure.OTAWebService;
    using Travel.Infrastructure.OTAWebService.Request;

    public class OrderService
    {
        public static void SearchTicketStatus(int pageSize)
        {
            var pageIndex = 1;
            var service = new OTAServiceManager();

            while (true)
            {
                var tickets = OrderEntity.GetTicketForSearch(pageIndex, pageSize);
                var s = new GetAllOrderStatusRequest();
                //s.
                //service.GetAllOrderStatus()


                if (tickets.Count == pageSize)
                {
                    pageIndex++;
                }
                else
                {
                    break;
                }
            }
        }
    }
}
