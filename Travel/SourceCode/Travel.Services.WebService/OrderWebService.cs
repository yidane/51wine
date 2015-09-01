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
        public void TicketCategoryList()
        {
            try
            {
                var categories = new OrderService().GetTicketCategoryList();

                Context.Response.Write(AjaxResult.Success(categories));
            }
            catch (Exception)
            {
                Context.Response.Write(AjaxResult.Error("方法异常"));
            }
            
        }

        public void MyOrders(string openId)
        {
            try
            {
                if (!string.IsNullOrEmpty(openId))
                {
                    var orders = new OrderService().MyOrders(openId);

                    Context.Response.Write(AjaxResult.Success(orders));
                }
                else
                {
                    Context.Response.Write(AjaxResult.Error("openId不能为空"));
                }
            }
            catch (Exception)
            {
                Context.Response.Write(AjaxResult.Error("方法异常"));
            }            
        }

        public void MyTickets(string orderId)
        {
            try
            {
                if (!string.IsNullOrEmpty(orderId))
                {
                    var tickets = new OrderService().MyTickets(orderId);

                    Context.Response.Write(AjaxResult.Success(tickets));
                }
                else
                {
                    Context.Response.Write(AjaxResult.Error("订单号不能为空"));
                }
            }
            catch (Exception)
            {
                Context.Response.Write(AjaxResult.Error("方法异常"));
            }            
        }
    }
}