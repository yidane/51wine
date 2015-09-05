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
                                                                                    image = string.Format("url(../images/ticket{0}.jpg)", new Random((unchecked((int)DateTime.Now.Ticks + categories.IndexOf(item)))).Next(1, 8).ToString())
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
                                                    image = string.Format("url(../images/ticket{0}.jpg)", new Random((unchecked((int)DateTime.Now.Ticks + categories.IndexOf(item)))).Next(1, 8).ToString())
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
                                            image = string.Format("url(../images/ticket{0}.jpg)", new Random((unchecked((int)DateTime.Now.Ticks + categories.IndexOf(item)))).Next(1, 8).ToString())
                                        }).ToList()
                                    });

            return dto;
        }

        public IList<OrderDTO> MyOrders(string openId)
        {
            var orders = OrderEntity.GetMyOrders(openId);
            var dto = orders.Where(item => item.OrderStatus.Equals(OrderStatus.OrderStatus_WaitUse))
                .Select(item => new OrderDTO()
                                                {
                                                    OrderId = item.OrderId.ToString(),
                                                    OrderCode = item.OrderCode,
                                                    singleTicketPrice = item.Tickets.First().Price,
                                                    TotelFee = item.TotalFee(),
                                                    TicketCount = item.Tickets.Count,
                                                    TicketName = TicketCategoryEntity.TodayTicketCategory.FirstOrDefault(i => i.TicketCategoryId.Equals(item.Tickets.First().TicketCategoryId)).TicketName,
                                                    BuyTime = item.Tickets.First().CreateTime.ToString("yyyy-MM-dd")
                                                }).ToList();

            return dto;
        }

        public OrderWithDetailListDTO GetOrderByOrderID(Guid orderId)
        {
            var order = OrderEntity.GetOrderByOrderId(orderId);
            var ticketList = TicketEntity.GetTicketsByOrderId(orderId);
            var orderWithDetailListDTO = new OrderWithDetailListDTO()
                {
                    ContactPersonName = order.ContactPersonName,
                    MobilePhoneNumber = order.MobilePhoneNumber,
                    IdentityCardNumber = order.IdentityCardNumber,
                    OrderCode = order.OrderCode,
                    OrderId = order.OrderId,
                    TicketCodeList = ticketList.Select(item => item.ECode).ToList(),
                    PayTime = order.CreateTime.ToShortDateString(),
                    SinglePrice = ticketList.First().Price.ToString()
                };

            return orderWithDetailListDTO;
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

        public IList<RefundTicketDTO> MyRefundTickets(string openId)
        {
            var myOrders =
                OrderEntity.GetMyOrders(openId)
                    .Where(
                        item =>
                        item.OrderStatus.Equals(OrderStatus.OrderStatus_WaitUse)
                        || item.OrderStatus.Equals(OrderStatus.OrderStatus_Used));

            var tickets = new List<RefundTicketDTO>();
            foreach (var order in myOrders)
            {
                tickets.AddRange(
                    order.Tickets.Where(
                        item => item.TicketStatus.Equals(OrderStatus.TicketStatus_Refund_Audit)
                        || item.TicketStatus.Equals(OrderStatus.TicketStatus_Refund_RefundPayProcessing)
                        || item.TicketStatus.Equals(OrderStatus.TicketStatus_Refund_Complete)
                        || item.TicketStatus.Equals(OrderStatus.TicketStatus_Refund_WaitRefundFee))
                        .Select(item => new RefundTicketDTO
                                            {
                                                TicketId = item.TicketId,
                                                OrderId = item.OrderId.ToString(),
                                                TicketCategoryId = item.TicketCategoryId.ToString(),
                                                TicketCode = item.TicketCode,
                                                Price = item.Price.ToString(),
                                                TicketStatus = item.TicketStatus,
                                                ECode = item.ECode
                                            }).ToList());
            }

            return tickets;
        }

        public void RefundTickets(string orderId, int refundTicketsNumber)
        {
            Guid gOrder;

            if (Guid.TryParse(orderId, out gOrder))
            {
                if (!gOrder.Equals(Guid.Empty))
                {
                    var order = OrderEntity.GetOrderByOrderId(gOrder);
                    var otaOrder = new OTAOrder(order);
                    var refundTickets =
                            TicketEntity.GetTicketsByOrderId(gOrder)
                                .Where(item => item.TicketStatus.Equals(OrderStatus.TicketStatus_WaitUse))
                                .ToList();

                    if (refundTickets.Any())
                    {
                        if (refundTickets.Count >= refundTicketsNumber)
                        {
                            otaOrder.ProcessRefundRequestMain(refundTickets.Take(refundTicketsNumber).ToList());
                        }
                        else
                        {
                            throw new ArgumentException("退票张数大于可退张数");
                        }
                    }
                    else
                    {
                        throw new ArgumentException("订单中没有可退票");
                    }
                }
            }
        }

        public void SearchTicketStatus(int pageSize)
        {
            var pageIndex = 1;
            var service = new OTAServiceManager();

            while (true)
            {
                var tickets = OrderEntity.GetTicketForSearch(pageIndex, pageSize);
                var ECodes = new List<OrderNoCode>();

#if DEBUG
                foreach (var ticket in tickets)
                {
                    Console.WriteLine(ticket.OrderId + " OrderId:" + ticket.OrderId);
                }
#endif

                foreach (var ticket in tickets)
                {
                    ECodes.Add(new OrderNoCode() { OrderCode = ticket.ECode });
                }

                var resp = service.GetAllOrderStatus(new GetAllOrderStatusRequest() { PostOrder = ECodes });

                this.OrderCompleteProcess(
                    tickets.Where(item => item.TicketStatus.Equals(OrderStatus.TicketStatus_WaitUse)).ToList(),
                    resp.ResultData);

#if DEBUG

                Console.WriteLine("Refund ticket count: " + tickets.Where(item => item.TicketStatus.Equals(OrderStatus.TicketStatus_Refund_Audit)).ToList().Count);

#endif

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
                    case "P":
                    case "M":
                        changedStatusTickets.Add(ticket);
                        break;
                    case "E":
                    default:
                        break;
                }
            }

            // todo: 未经过测试
            foreach (var someOrderTickets in changedStatusTickets.GroupBy(item => item.OrderId))
            {
                var orderTickets = someOrderTickets.ToList();
                var order = OrderEntity.GetOrderByOrderId(orderTickets.First().OrderId);
                var otaOrder = new OTAOrder(order);

                otaOrder.ProcessRefundPayment(orderTickets);
            }
        }

        private void OrderCompleteProcess(IEnumerable<TicketEntity> completeTickets, IList<GetAllOrderStatusResponse> resps)
        {
            var changedStatusTickets = new List<TicketEntity>();
            foreach (var ticket in completeTickets)
            {
                var respStatus = resps.FirstOrDefault(item => item.OrderCode.Equals(ticket.ECode));

                switch (respStatus.OrderStatus)
                {
                    case "T":
                    case "O":
                        changedStatusTickets.Add(ticket);
                        break;
                    default:
                        break;
                }
            }

            if (changedStatusTickets.Any())
            {
                foreach (var ticket in changedStatusTickets)
                {
                    ticket.TicketStatus = OrderStatus.TicketStatus_Used;
                    ticket.LatestModifyTime = DateTime.Now;
                }
                TicketEntity.ModifyTickets(changedStatusTickets);
            }

        }
    }
}
