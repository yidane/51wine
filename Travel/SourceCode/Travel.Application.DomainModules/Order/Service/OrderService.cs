using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Travel.Application.DomainModules.Order.Service
{
    using Travel.Application.DomainModules.Order.Core;
    using Travel.Application.DomainModules.Order.DTOs;
    using Travel.Infrastructure.DomainDataAccess.Order;
    using Travel.Infrastructure.OTAWebService;
    using Travel.Infrastructure.OTAWebService.Request;
    using Travel.Infrastructure.OTAWebService.Response;

    public class OrderService
    {
        public IList<TicketCategoryDTO> GetTicketCategoryList()
        {
            var categories = TicketCategoryEntity.TodayTicketCategory;
            var dto = new List<TicketCategoryDTO>();

            dto.Add(new TicketCategoryDTO()
                        {
                            category = 1,
                            content = categories.Select(item => new TicketCategorySub()
                                                                    {
                                                                        ticketCategoryId = item.TicketCategoryId.ToString(),
                                                                        ticketType = "1",
                                                                        price = item.Price,
                                                                        ticketName = item.TicketName,
                                                                        type = item.Type,
                                                                        image = string.Format("url(../images/ticket{0}.jpg)", new Random().Next(1,8).ToString())
                                                                    }).ToList()
                        });

            dto.Add(new TicketCategoryDTO()
                        {
                            category = 2,
                            content = categories
                            .Where(item => item.Type.Equals("mp"))
                            .Select(item => new TicketCategorySub()
                                    {
                                        ticketCategoryId = item.TicketCategoryId.ToString(),
                                        ticketType = "2",
                                        price = item.Price,
                                        ticketName = item.TicketName,
                                        type = item.Type,
                                        image = string.Format("url(../images/ticket{0}.jpg)", new Random().Next(1, 8).ToString())                                                                   
                                    }).ToList()
                        });

            dto.Add(new TicketCategoryDTO()
            {
                category = 3,
                content = categories
                .Where(item => item.Type.Equals("cp"))
                .Select(item => new TicketCategorySub()
                {
                    ticketCategoryId = item.TicketCategoryId.ToString(),
                    ticketType = "3",
                    price = item.Price,
                    ticketName = item.TicketName,
                    type = item.Type,
                    image = string.Format("url(../images/ticket{0}.jpg)", new Random().Next(1, 8).ToString())
                }).ToList()
            });

            return dto;
        }

        public IList<OrderDTO> MyOrders(string openId)
        {
            var orders = OrderEntity.GetMyOrders(openId);
            var dto = orders.Select(item => new OrderDTO()
                                                {
                                                    OrderId = item.OrderId.ToString(),
                                                    OrderCode = item.OrderCode,
                                                    singleTicketPrice = item.Tickets.First().Price,
                                                    TotelFee = item.TotalFee(),
                                                    TicketName = TicketCategoryEntity.TodayTicketCategory.FirstOrDefault(i =>i.TicketCategoryId.Equals(item.Tickets.First().TicketCategoryId)).TicketName,
                                                    BuyTime = item.Tickets.First().CreateTime.ToString("yyyy-MM-dd")
                                                }).ToList();


            return dto;
        }

        public IList<TicketEntity> MyTickets(string orderId)
        {
            Guid gOrder;

            if (Guid.TryParse(orderId, out gOrder))
            {
                if (!gOrder.Equals(Guid.Empty))
                {
                    return TicketEntity.GetTicketsByOrderId(gOrder).ToList();
                }
            }

            return null;
        }

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
