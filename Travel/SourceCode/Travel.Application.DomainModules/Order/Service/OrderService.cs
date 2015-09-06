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
        /// <summary>
        /// 获取可售票列表
        /// </summary>
        /// <returns></returns>
        public IList<TicketCategoryDTO> GetTicketCategoryList()
        {
            var categories = TicketCategoryEntity.TodayTicketCategory;
            var dto = new List<TicketCategoryDTO>();

            if (!categories.Any())
            {
                OTAOrder.SetDailyTicket();
                categories = TicketCategoryEntity.TodayTicketCategory;
            }

            dto.Add(new TicketCategoryDTO()
                                    {
                                        category = 1,
                                        content = categories.Select(item => new TicketCategorySub()
                                                                                {
                                                                                    ticketCategoryId = item.TicketCategoryId.ToString(),
                                                                                    ticketType = "1",
                                                                                    price = item.Price,
                                                                                    ticketName = item.TicketName,
                                                                                    canUse = item.TicketName.Contains("一进") && item.TicketName.Contains("车票"),
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
                                                    canUse = item.TicketName.Contains("一进") && item.TicketName.Contains("车票"),
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
                                            canUse = item.TicketName.Contains("一进") && item.TicketName.Contains("车票"),
                                            type = item.Type,
                                            image = string.Format("url(../images/ticket{0}.jpg)", new Random((unchecked((int)DateTime.Now.Ticks + categories.IndexOf(item)))).Next(1, 8).ToString())
                                        }).ToList()
                                    });

            return dto;
        }

        /// <summary>
        /// 获取我的订单列表
        /// </summary>
        /// <param name="openId"></param>
        /// <returns></returns>
        public IList<OrderDTO> MyOrders(string openId)
        {
            var orders = OrderEntity.GetMyOrders(openId);
            var dto = new List<OrderDTO>();
            List<OrderDTO> sortList = null;

            if (orders.Any(item => item.OrderStatus.Equals(OrderStatus.OrderStatus_WaitUse)
                || item.OrderStatus.Equals(OrderStatus.OrderStatus_Used)))
            {
                sortList=new List<OrderDTO>();
                dto = orders.Where(item => item.OrderStatus.Equals(OrderStatus.OrderStatus_WaitUse)
                                            || item.OrderStatus.Equals(OrderStatus.OrderStatus_Used))
                        .Select(item => new OrderDTO()
                        {
                            OrderId = item.OrderId.ToString(),
                            OrderCode = item.OrderCode,
                            singleTicketPrice = item.Tickets.First().Price,
                            TotelFee = item.TotalFee(),
                            TicketCount = item.Tickets.Count,
                            TicketName = TicketCategoryEntity.GetTicketNameByTicketCategoryId(item.Tickets.First().TicketCategoryId),
                            BuyTime = item.Tickets.First().CreateTime.ToString("yyyy-MM-dd"),
                            OrderStatus = this.GetOrderStatus(item),
                            hasRefundTicket = item.Tickets.Any(this.RefundStatus()),
                            RefundType = this.GetRefundType(item) }).ToList();
                sortList.InsertRange(sortList.Count, dto.Where(item => item.OrderStatus.Equals("未使用")));
                sortList.InsertRange(sortList.Count, dto.Where(item => item.OrderStatus.Equals("已过期")));
                sortList.InsertRange(sortList.Count, dto.Where(item => item.OrderStatus.Equals("已使用")));
                sortList.InsertRange(sortList.Count, dto.Where(item => item.OrderStatus.Equals(string.Empty)));
            }

            return sortList;
        }

        private string GetOrderStatus(OrderEntity order)
        {
            if (order.OrderStatus.Equals(OrderStatus.OrderStatus_WaitUse))
            {
                return "未使用";
            }
            else if (!order.OrderStatus.Equals(OrderStatus.OrderStatus_Used) && order.CreateTime.AddYears(1).Date < DateTime.Now.Date)
            {
                return "已过期";
            }
            else if (order.OrderStatus.Equals(OrderStatus.OrderStatus_Used))
            {
                return "已使用";
            }
            else
            {
                return string.Empty;
            }
        }

        private Func<TicketEntity, bool> RefundStatus()
        {
            return
                ticket =>
                    ticket.TicketStatus.Equals(OrderStatus.TicketStatus_Refund_Audit)
                    || ticket.TicketStatus.Equals(OrderStatus.TicketStatus_Refund_RefundPayProcessing)
                    || ticket.TicketStatus.Equals(OrderStatus.TicketStatus_Refund_WaitRefundFee)
                    || ticket.TicketStatus.Equals(OrderStatus.TicketStatus_Refund_Complete);
        }

        private string GetRefundType(OrderEntity order)
        {
            if (order.Tickets.Any(this.RefundStatus()))
            {
                return order.Tickets.Count(this.RefundStatus()).Equals(order.Tickets.Count) ? "全部退票" : "部分退票";
            }
            else
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// 获取订单详情
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
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
            var tickets = new List<RefundTicketDTO>();

            if (!string.IsNullOrEmpty(openId))
            {
                var myOrders =
                OrderEntity.GetMyOrders(openId)
                    .Where(
                        item =>
                        item.OrderStatus.Equals(OrderStatus.OrderStatus_WaitUse)
                        || item.OrderStatus.Equals(OrderStatus.OrderStatus_Used));


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
                                OrderCode = order.OrderCode,
                                DeadLineDate = item.TicketStartTime.ToString("yyyy-MM-dd"),
                                TicketCategoryId = item.TicketCategoryId.ToString(),
                                TicketName = TicketCategoryEntity.GetTicketNameByTicketCategoryId(item.TicketCategoryId),
                                TicketCode = item.TicketCode,
                                Price = item.Price.ToString(),
                                TicketStatus = this.getTicketStatus(item.TicketStatus),
                                ECode = item.ECode
                            }).ToList());
                }
            }
            
            return tickets;
        }

        private string getTicketStatus(string statusCode)
        {
            var statusName = string.Empty;

            switch (statusCode)
            {
                case OrderStatus.TicketStatus_Refund_Audit:
                    statusName = "退票审核中";
                    break;
                case OrderStatus.TicketStatus_Refund_RefundPayProcessing:
                    statusName = "退款受理中";
                    break;
                case OrderStatus.TicketStatus_Refund_WaitRefundFee:
                    statusName = "等待退款";
                    break;
                case OrderStatus.TicketStatus_Refund_Complete:
                    statusName = "退票完成";
                    break;
                default:
                    statusName = "退票失败";
                    break;
            }

            return statusName;
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

        public void OrderRelease(string orderId)
        {
            if (!string.IsNullOrEmpty(orderId))
            {
                var result = OTAOrderOperate.OrderRelease(orderId);

                if (!result.IsTrue)
                {
                    throw new InvalidOperationException("释放订单错误");
                }
            }
            else
            {
                throw new ArgumentNullException("订单号不能为空");
            }
        }
    }
}
