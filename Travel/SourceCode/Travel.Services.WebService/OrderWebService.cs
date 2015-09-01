using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Services;
using Travel.Infrastructure.CommonFunctions;
using Travel.Infrastructure.CommonFunctions.Ajax;
using Travel.Application.DomainModules.Order.DTOs;
using Travel.Application.DomainModules.Order.Service;
using Travel.Infrastructure.DomainDataAccess.Order;

namespace Travel.Services.WebService
{
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

        [WebMethod(EnableSession = true)]
        public void GetMyOrders(string code)
        {
            try
            {
                var openId = GetOpenIDByCodeID(code);
                var orderList = new OrderService().MyOrders(openId);

                Context.Response.Write(AjaxResult.Success(orderList));
            }
            catch (Exception exception)
            {
                Context.Response.Write(AjaxResult.Error(exception.Message));
            }
        }

        //public string MyOrders(string openId)
        //{
        //    if (!string.IsNullOrEmpty(openId))
        //    {
        //        var orders = new OrderService().MyOrders(openId);

        //        return JSONHelper.ObjectToJson<List<OrderDTO>>(orders.ToList());
        //    }
        //    else
        //    {
        //        return string.Empty;
        //    }
        //}

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