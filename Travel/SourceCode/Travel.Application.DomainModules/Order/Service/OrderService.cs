using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Travel.Application.DomainModules.Order.Service
{
    using Travel.Application.DomainModules.Order.Core;
    using Travel.Infrastructure.DomainDataAccess.Order;
    using Travel.Infrastructure.OTAWebService;
    using Travel.Infrastructure.OTAWebService.Request;
    using Travel.Infrastructure.OTAWebService.Response;

    public class OrderService
    {
        public void SearchTicketStatus(int pageSize)
        {
            var pageIndex = 1;
            var service = new OTAServiceManager();

            while (true)
            {
                var tickets = OrderEntity.GetTicketForSearch(pageIndex, pageSize);
                var ECodes = new List<OrderNoCode>();

                foreach (var ticket in tickets)
                {
                    ECodes.Add(new OrderNoCode() { OrderCode = ticket.ECode });
                }

                var resp = service.GetAllOrderStatus(new GetAllOrderStatusRequest() { PostOrder = ECodes });

                this.RefundProcess(
                    tickets.Where(item => item.TicketStatus.Equals(OrderStatus.TicketStatus_Refund_Audit)).ToList(),
                    resp.ResultData);

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

        private void RefundProcess(IEnumerable<TicketEntity> refundTickets, IList<GetAllOrderStatusResponse> resps)
        {
            var changedStatusTickets = new List<TicketEntity>();
            foreach (var ticket in refundTickets)
            {
                var respStatus = resps.FirstOrDefault(item => item.OrderCode.Equals(ticket.ECode));

                switch (respStatus.OrderStatus)
                {
                    case "A":
                    case "M":
                        changedStatusTickets.Add(ticket);
                        break;
                    case "E":
                    case "P":
                    default:
                        break;
                }
            }

            foreach (var someOrderTickets in changedStatusTickets.GroupBy(item => item.OrderId))
            {
                var orderTickets = someOrderTickets.ToList();
                var order = OrderEntity.GetOrderByOrderId(orderTickets.First().OrderId);
                var otaOrder = new OTAOrder(order);

                otaOrder.ProcessRefundPayment(orderTickets);
            }
        }
    }
}
