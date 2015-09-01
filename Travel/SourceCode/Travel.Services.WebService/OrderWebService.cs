using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using Travel.Infrastructure.CommonFunctions;

namespace Travel.Presentation.Web.WebService
{
    using Travel.Application.DomainModules.Order.DTOs;
    using Travel.Application.DomainModules.Order.Service;
    using Travel.Infrastructure.DomainDataAccess.Order;
    using Travel.Services.WebService;

    /// <summary>
    /// OrderWebService 的摘要说明
    /// </summary>
    public class OrderWebService : BaseWebService
    {

        [WebMethod(EnableSession = true)]
        public string TicketCategoryList()
        {
            var categories = new OrderService().GetTicketCategoryList();

            return JSONHelper.ObjectToJson<List<TicketCategoryDTO>>(categories.ToList());
        }

        public string MyOrders(string openId)
        {
            if (!string.IsNullOrEmpty(openId))
            {
                var orders = new OrderService().MyOrders(openId);

                return JSONHelper.ObjectToJson<List<OrderDTO>>(orders.ToList());
            }
            else
            {
                return string.Empty;
            }
        }

        public string MyTickets(string orderId)
        {
            if (!string.IsNullOrEmpty(orderId))
            {
                var tickets = new OrderService().MyTickets(orderId);

                return JSONHelper.ObjectToJson<List<TicketEntity>>(tickets.ToList());
            }
            else
            {
                return string.Empty;
            }
        }
    }
}