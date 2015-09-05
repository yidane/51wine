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
            catch (Exception exception)
            {
                Context.Response.Write(AjaxResult.Error(exception.Message));
            }
        }

        [WebMethod(EnableSession = true)]
        public void MyOrders(string code)
        {
            try
            {
                var openId = GetOpenIDByCodeID(code);

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
            catch (Exception exception)
            {
                Context.Response.Write(AjaxResult.Error(exception.Message));
            }
        }

        [WebMethod(EnableSession = true)]
        public void GetOrderWithDetailList(string orderId)
        {
            try
            {
                var result = new OrderService().GetOrderByOrderID(new Guid(orderId));
                Context.Response.Write(AjaxResult.Success(result));
            }
            catch (Exception exception)
            {
                Context.Response.Write(AjaxResult.Error(exception.Message));
            }
        }

        [WebMethod(EnableSession = true)]
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
            catch (Exception exception)
            {
                Context.Response.Write(AjaxResult.Error(exception.Message));
            }
        }

        [WebMethod(EnableSession = true)]
        public void MyRefundTickets(string code)
        {
            try
            {
                var openId = GetOpenIDByCodeID(code);
                if (!string.IsNullOrEmpty(openId))
                {
                    var tickets = new OrderService().MyRefundTickets(openId);

                    Context.Response.Write(AjaxResult.Success(tickets));
                }
                else
                {
                    Context.Response.Write(AjaxResult.Error("用户不能为空"));
                }
            }
            catch (Exception exception)
            {
                Context.Response.Write(AjaxResult.Error(exception.Message));
            }
        }

        [WebMethod]
        public void RefundTickets(string orderId, int refundTicketNumber)
        {
            try
            {
                if (!string.IsNullOrEmpty(orderId) && refundTicketNumber > 0)
                {
                    new OrderService().RefundTickets(orderId, refundTicketNumber);

                    Context.Response.Write(AjaxResult.Success("退票成功"));
                }
                else
                {
                    Context.Response.Write(AjaxResult.Error("订单号或退票数量有误"));
                }
            }
            catch (ArgumentException ex)
            {
                Context.Response.Write(AjaxResult.Error(ex.Message));
            }
            catch (Exception exception)
            {
                Context.Response.Write(AjaxResult.Error(exception.Message));
            }
        }
    }
}