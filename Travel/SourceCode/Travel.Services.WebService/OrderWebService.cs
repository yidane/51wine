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
    using Travel.Application.DomainModules.Order.Core;

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
                var exLog = new ExceptionLogEntity()
                {
                    ExceptionLogId = Guid.NewGuid(),
                    Module = "获取门票列表",
                    CreateTime = DateTime.Now,
                    ExceptionType = exception.GetType().FullName,
                    ExceptionMessage = exception.Message,
                    TrackMessage = exception.StackTrace,
                    HasExceptionProcessing = false,
                    NeedProcess = false,
                    ProcessFunction = string.Empty
                };

                exLog.Add(); 

                Context.Response.Write(AjaxResult.Error(exception.Message));
            }
        }

        [WebMethod(EnableSession = true)]
        public void MyOrders(string code)
        {
            string openId = string.Empty;
            try
            {
                openId = GetOpenIDByCodeID(code);

                if (!string.IsNullOrEmpty(openId))
                {
                    try
                    {
                        new OrderService().GetMyRefundTicketsStatus(openId);
                    }
                    catch (Exception ex)
                    {
                        var exLog = new ExceptionLogEntity()
                        {
                            ExceptionLogId = Guid.NewGuid(),
                            Module = "获取退款状态" + "------OpenId:" + openId,
                            CreateTime = DateTime.Now,
                            ExceptionType = ex.GetType().FullName,
                            ExceptionMessage = ex.Message,
                            TrackMessage = ex.StackTrace,
                            HasExceptionProcessing = false,
                            NeedProcess = false,
                            ProcessFunction = string.Empty
                        };

                        exLog.Add();                     
                    }
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
                var exLog = new ExceptionLogEntity()
                {
                    ExceptionLogId = Guid.NewGuid(),
                    Module = "获取我的订单" + "------OpenId:" + openId,
                    CreateTime = DateTime.Now,
                    ExceptionType = exception.GetType().FullName,
                    ExceptionMessage = exception.Message,
                    TrackMessage = exception.StackTrace,
                    HasExceptionProcessing = false,
                    NeedProcess = false,
                    ProcessFunction = string.Empty
                };

                exLog.Add(); 

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
                var exLog = new ExceptionLogEntity()
                {
                    ExceptionLogId = Guid.NewGuid(),
                    Module = "获取订单详情" + "------OrderId:" + orderId,
                    CreateTime = DateTime.Now,
                    ExceptionType = exception.GetType().FullName,
                    ExceptionMessage = exception.Message,
                    TrackMessage = exception.StackTrace,
                    HasExceptionProcessing = false,
                    NeedProcess = false,
                    ProcessFunction = string.Empty
                };

                exLog.Add(); 

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
                var exLog = new ExceptionLogEntity()
                {
                    ExceptionLogId = Guid.NewGuid(),
                    Module = "获取我的票" + "------OrderId:" + orderId,
                    CreateTime = DateTime.Now,
                    ExceptionType = exception.GetType().FullName,
                    ExceptionMessage = exception.Message,
                    TrackMessage = exception.StackTrace,
                    HasExceptionProcessing = false,
                    NeedProcess = false,
                    ProcessFunction = string.Empty
                };

                exLog.Add(); 

                Context.Response.Write(AjaxResult.Error(exception.Message));
            }
        }

        [WebMethod(EnableSession = true)]
        public void MyRefundTickets(string code)
        {
            string openId = string.Empty;
            try
            {
                openId = GetOpenIDByCodeID(code);
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
                var exLog = new ExceptionLogEntity()
                {
                    ExceptionLogId = Guid.NewGuid(),
                    Module = "获取我的退票列表" + "------OpenId:" + openId,
                    CreateTime = DateTime.Now,
                    ExceptionType = exception.GetType().FullName,
                    ExceptionMessage = exception.Message,
                    TrackMessage = exception.StackTrace,
                    HasExceptionProcessing = false,
                    NeedProcess = false,
                    ProcessFunction = string.Empty
                };

                exLog.Add(); 
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
                var exLog = new ExceptionLogEntity()
                {
                    ExceptionLogId = Guid.NewGuid(),
                    Module = "退票操作" + "------OrderId:" + orderId,
                    CreateTime = DateTime.Now,
                    ExceptionType = ex.GetType().FullName,
                    ExceptionMessage = ex.Message,
                    TrackMessage = ex.StackTrace,
                    HasExceptionProcessing = false,
                    NeedProcess = false,
                    ProcessFunction = string.Empty
                };

                exLog.Add();
                Context.Response.Write(AjaxResult.Error(ex.Message));
            }
            catch (Exception exception)
            {
                var exLog = new ExceptionLogEntity()
                {
                    ExceptionLogId = Guid.NewGuid(),
                    Module = "退票操作" + "------OrderId:" + orderId,
                    CreateTime = DateTime.Now,
                    ExceptionType = exception.GetType().FullName,
                    ExceptionMessage = exception.Message,
                    TrackMessage = exception.StackTrace,
                    HasExceptionProcessing = false,
                    NeedProcess = false,
                    ProcessFunction = string.Empty
                };

                exLog.Add(); 

                Context.Response.Write(AjaxResult.Error(exception.Message));
            }
        }
    }
}