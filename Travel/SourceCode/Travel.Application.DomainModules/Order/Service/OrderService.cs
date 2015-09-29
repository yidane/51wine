using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Travel.Application.DomainModules.Order.Service
{
    using System.ComponentModel;
    using System.Data.Entity.Core.Metadata.Edm;
    using System.Security.Cryptography.X509Certificates;
    using System.Transactions;

    using Travel.Application.DomainModules.Order.Core;
    using Travel.Application.DomainModules.Order.DTOs;
    using Travel.Infrastructure.DomainDataAccess.Order;
    using Travel.Infrastructure.OTAWebService;
    using Travel.Infrastructure.OTAWebService.Request;
    using Travel.Infrastructure.OTAWebService.Response;
    using Travel.Infrastructure.WeiXin.Advanced.Pay.Model;

    public class OrderService
    {
        /// <summary>
        /// 获取可售票列表
        /// </summary>
        /// <returns></returns>
        public IList<TicketCategoryDTO> GetTicketCategoryList()
        {
            var dto = new List<TicketCategoryDTO>();
            var dailyProducts = DailyProductEntity.DailyProduct;

            if (dailyProducts.Any())
            {
                // 不要直接join DailyProductEntity.DailyProduct，否则如果有新类型票种，将无法及时显示
                var productCategories =
                        ProductCategoryEntity.ProductCategory
                            .Where(item => item.CurrentStatus.Equals("10001"))
                            .Join(
                                dailyProducts,
                                category => category.ProductCategoryId,
                                daily => daily.ProductCategoryId,
                                (a, b) => a).ToList();


                if (productCategories.Any())
                {
                    dto.Add(new TicketCategoryDTO()
                                            {
                                                category = 1,
                                                content = productCategories.Select(item => new TicketCategorySub()
                                                                                        {
                                                                                            ticketCategoryId = item.ProductCategoryId.ToString(),
                                                                                            ticketType = "1",
                                                                                            price = item.ProductPrice,
                                                                                            ticketName = item.ProductName,
                                                                                            canUse = true,
                                                                                            type = item.ProductType,
                                                                                            image = string.Format("url(../images/ticket{0}.jpg)", new Random(unchecked((int)DateTime.Now.Ticks + productCategories.IndexOf(item))).Next(1, 8).ToString())
                                                                                        }).ToList()
                                            });

                    dto.Add(new TicketCategoryDTO()
                                            {
                                                category = 2,
                                                content = productCategories
                                                .Where(item => !item.ProductName.Contains("车票"))
                                                .Select(item => new TicketCategorySub()
                                                        {
                                                            ticketCategoryId = item.ProductCategoryId.ToString(),
                                                            ticketType = "2",
                                                            price = item.ProductPrice,
                                                            ticketName = item.ProductName,
                                                            canUse = true,
                                                            type = "mp",
                                                            image = string.Format("url(../images/ticket{0}.jpg)", new Random(unchecked((int)DateTime.Now.Ticks + productCategories.IndexOf(item))).Next(1, 8).ToString())
                                                        }).ToList()
                                            });

                    dto.Add(new TicketCategoryDTO()
                                            {
                                                category = 3,
                                                content = productCategories
                                                .Where(item => item.ProductName.Contains("车票"))
                                                .Select(item => new TicketCategorySub()
                                                {
                                                    ticketCategoryId = item.ProductCategoryId.ToString(),
                                                    ticketType = "3",
                                                    price = item.ProductPrice,
                                                    ticketName = item.ProductName,
                                                    canUse = true,
                                                    type = "cp",
                                                    image = string.Format("url(../images/ticket{0}.jpg)", new Random(unchecked((int)DateTime.Now.Ticks + productCategories.IndexOf(item))).Next(1, 8).ToString())
                                                }).ToList()
                                            });
                }
            }

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
                sortList = new List<OrderDTO>();
                dto = orders.Where(item => item.OrderStatus.Equals(OrderStatus.OrderStatus_WaitUse)
                                            || item.OrderStatus.Equals(OrderStatus.OrderStatus_Used))
                        .Select(item => new OrderDTO()
                        {
                            OrderId = item.OrderId.ToString(),
                            OrderCode = item.OrderCode,
                            singleTicketPrice = item.Tickets.First().Price,
                            TotelFee = item.TotalFee(),
                            TicketCount = item.Tickets.Count,
                            TicketName = ProductCategoryEntity.ProductCategory.FirstOrDefault(category => category.ProductCategoryId.Equals(item.Tickets.First().TicketCategoryId)).ProductName,
                            BuyTime = item.Tickets.First().CreateTime.ToString("yyyy-MM-dd"),
                            DeadTime = item.Tickets.First().CreateTime.AddDays(365).ToString("yyyy-MM-dd"),
                            OrderStatus = this.GetOrderStatus(item),
                            hasRefundTicket = item.Tickets.Any(this.RefundStatus()),
                            RefundType = this.GetRefundType(item)
                        }).ToList();

                sortList.InsertRange(sortList.Count, dto.Where(item => item.OrderStatus.Equals("未使用") && string.IsNullOrEmpty(item.RefundType)).OrderByDescending(item => item.BuyTime));
                sortList.InsertRange(sortList.Count, dto.Where(item => item.OrderStatus.Equals("未使用") && item.RefundType.Equals("部分退票")).OrderByDescending(item => item.BuyTime));
                sortList.InsertRange(sortList.Count, dto.Where(item => item.OrderStatus.Equals("未使用") && item.RefundType.Equals("全部退票")).OrderByDescending(item => item.BuyTime));
                sortList.InsertRange(sortList.Count, dto.Where(item => item.OrderStatus.Equals("已过期")).OrderByDescending(item => item.BuyTime));
                sortList.InsertRange(sortList.Count, dto.Where(item => item.OrderStatus.Equals("已使用")).OrderByDescending(item => item.BuyTime));
                sortList.InsertRange(sortList.Count, dto.Where(item => item.OrderStatus.Equals(string.Empty)).OrderByDescending(item => item.BuyTime));
            }

            return sortList;
        }

        public void GetMyRefundTicketsStatus(string openId)
        {
            var orders = OrderEntity.GetMyOrders(openId).ToList();
            var tickets = new List<TicketEntity>();

            orders.ForEach(item => tickets.AddRange(item.Tickets.Where(ticket => ticket.TicketStatus.Equals(OrderStatus.TicketStatus_Refund_WaitRefundFee))));
            if (tickets.Any())
            {
                foreach (var refundOrderGroup in tickets.GroupBy(item => item.RefundOrderId))
                {
                    if (refundOrderGroup.Key != null && (refundOrderGroup.Key.Value != default(Guid?) || refundOrderGroup.Key.Value.Equals(Guid.Empty)))
                    {
                        var strStatus = OTAOrder.RefundQuery(refundOrderGroup.Key.Value);
                        var refundTickets = refundOrderGroup.ToList();

                        if (refundTickets.Any())
                        {
                            switch (strStatus)
                            {
                                case "SUCCESS":
                                    refundTickets.ForEach(item =>
                                        {
                                            item.TicketStatus = OrderStatus.TicketStatus_Refund_Complete;
                                            item.LatestModifyTime = DateTime.Now;
                                        });
                                    TicketEntity.ModifyTickets(refundTickets);
                                    break;
                                case "FAIL":
                                    break;
                                case "CHANGE":
                                    break;
                                case "NOTSURE":
                                    var otaOrder = new OTAOrder(refundTickets.FirstOrDefault().Order);

                                    var refundOrder = RefundOrderEntity.GetRefundOrder(refundOrderGroup.Key.Value);
                                    RefundOrderResponse response = null;
                                    if (refundOrder != null)
                                    {
                                        response = otaOrder.RefundPay(refundOrder);

                                        if (response.return_code.Equals("SUCCESS")
                                            && response.result_code.Equals("SUCCESS"))
                                        {
                                            foreach (var ticket in refundTickets)
                                            {
                                                ticket.TicketStatus = OrderStatus.TicketStatus_Refund_WaitRefundFee;
                                                ticket.LatestModifyTime = DateTime.Now;
                                            }

                                            refundOrder.WXRefundOrderCode = response.refund_id;
                                            refundOrder.RefundStatus = OrderStatus.RefundOrderStatus_WaitRefundFee;
                                            refundOrder.LatestModifyTime = DateTime.Now;

                                            using (var scope = new TransactionScope())
                                            {
                                                TicketEntity.ModifyTickets(refundTickets);
                                                refundOrder.Modify();

                                                scope.Complete();
                                            }
                                        }
                                        else
                                        {
                                            var orderDetail = OrderDetailEntity.GetOrderDetail(refundTickets.FirstOrDefault().RefundOrderDetailId.Value);

                                            refundOrder.Delete();
                                            orderDetail.Delete();
                                            foreach (var ticket in refundTickets)
                                            {
                                                ticket.TicketStatus = OrderStatus.TicketStatus_Refund_Audit;
                                                ticket.LatestModifyTime = DateTime.Now;
                                                ticket.RefundOrderId = default(Guid?);
                                                ticket.RefundOrderDetailId = default(Guid);
                                            }

                                            TicketEntity.ModifyTickets(tickets);
                                        }
                                    }

                                    break;
                                default: break;
                            }
                        }
                    }
                }
            }

            // 检查订单是否已全部使用完毕并修改订单状态
            orders.Where(item => !item.OrderStatus.Equals(OrderStatus.OrderStatus_Used)).ToList()
                .ForEach(
                    item =>
                    {
                        if (OrderEntity.IsOrderFinish(item))
                        {
                            item.OrderStatus = OrderStatus.OrderStatus_Used;
                            item.ModifyOrder();
                        }
                    });
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
                    TicketCodeList = this.GetTicketCodeWithOrder(ticketList).ToList(),
                    PayTime = order.CreateTime.ToShortDateString(),
                    UseRange = string.Format("{0}至{1}", order.CreateTime.ToShortDateString(), order.CreateTime.AddDays(365).ToShortDateString()),
                    SinglePrice = ticketList.First().Price.ToString(),
                    TotalPrice = ticketList.Count * ticketList.First().Price
                };

            return orderWithDetailListDTO;
        }

        private IList<string> GetTicketCodeWithOrder(ICollection<TicketEntity> tickets)
        {
            var sortList = new List<string>();

            sortList.InsertRange(sortList.Count, tickets.Except(tickets.Where(this.RefundStatus())).Select(item => item.ECode));
            sortList.InsertRange(sortList.Count, tickets.Where(this.RefundStatus()).Select(item => item.ECode));

            return sortList;
        }

        public IList<TicketEntity> MyTickets(string orderId)
        {
            Guid gOrder;

            if (Guid.TryParse(orderId, out gOrder))
            {
                if (!gOrder.Equals(Guid.Empty))
                {
                    return TicketEntity.GetTicketsByOrderId(gOrder).Where(this.RefundStatus()).ToList();
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
                        order.Tickets.Where(this.RefundStatus())
                            .Select(item =>
                                {
                                    var productCategory =
                                        ProductCategoryEntity.ProductCategory.FirstOrDefault(
                                            category => category.ProductCategoryId.Equals(item.TicketCategoryId));
                                    return productCategory != null
                                               ? new RefundTicketDTO
                                                     {
                                                         TicketId = item.TicketId,
                                                         OrderId = item.OrderId.ToString(),
                                                         OrderCode = order.OrderCode,
                                                         BuyTime = order.CreateTime.ToString("yyyy-MM-dd"),
                                                         DeadLineDate =
                                                             item.TicketStartTime.ToString("yyyy-MM-dd"),
                                                         TicketCategoryId =
                                                             item.TicketCategoryId.ToString(),
                                                         TicketName = productCategory.ProductName,
                                                         TicketCode = item.TicketCode,
                                                         Price = item.Price.ToString(),
                                                         TicketStatus =
                                                             this.getTicketStatus(item.TicketStatus),
                                                         ECode = item.ECode
                                                     }
                                               : null;
                                }));
                }
            }

            return tickets.Where(item => item != null).ToList();
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

                if (resp != null && resp.IsTrue)
                {
#if DEBUG
                    foreach (var value in resp.ResultData)
                    {
                        Console.WriteLine("OrderCompleteProcessData: " + value.OrderStatusDesc +
                        " responseData: " + value.RefundCount);
                    }


#endif

                    this.OrderCompleteProcess(
                        tickets.Where(item => item.TicketStatus.Equals(OrderStatus.TicketStatus_WaitUse)).ToList(),
                        resp.ResultData);

#if DEBUG

                    Console.WriteLine("Refund ticket count: " + tickets.Where(item => item.TicketStatus.Equals(OrderStatus.TicketStatus_Refund_Audit)).ToList().Count);

#endif

                    this.RefundProcess(
                        tickets.Where(item => item.TicketStatus.Equals(OrderStatus.TicketStatus_Refund_Audit) 
                            || item.TicketStatus.Equals(OrderStatus.TicketStatus_Refund_RefundPayProcessing)).ToList(),
                        resp.ResultData);
                }
                else
                {
                    //获取状态失败
                    Console.WriteLine("获取订单状态失败");

                    if (resp != null)
                    {
                        Console.WriteLine(resp.ResultMsg);
                    }
                }

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

                if (respStatus != null)
                {
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
            }

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

                if (respStatus != null)
                {
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
            }

            if (changedStatusTickets.Any())
            {
                foreach (var ticket in changedStatusTickets)
                {
                    ticket.TicketStatus = OrderStatus.TicketStatus_Used;
                    ticket.LatestModifyTime = DateTime.Now;
                }

                TicketEntity.ModifyTickets(changedStatusTickets);

                // 设置所有门票消费完毕的订单为已使用
                foreach (var ticketsInOrder in changedStatusTickets.GroupBy(item => item.OrderId))
                {
                    var order = ticketsInOrder.FirstOrDefault().Order;

                    if (OrderEntity.IsOrderFinish(order))
                    {
                        order.OrderStatus = OrderStatus.OrderStatus_Used;
                        order.ModifyOrder();
                    }
                }
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
