﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Travel.Application.DomainModules.Order.Core
{
    using System.Diagnostics;
    using System.Globalization;

    using Travel.Application.DomainModules.Order.Core.Interface;
    using Travel.Infrastructure.DomainDataAccess.Order;
    using Travel.Infrastructure.OTAWebService;
    using Travel.Infrastructure.OTAWebService.Response;

    using OTARequest = Travel.Infrastructure.OTAWebService.Request;

    public class OTAOrderOperate:IOrderOperate
    {
        private OTAServiceManager _serviceManager;

        public Order MainOrder { get; set; }

        public OTAOrderOperate(Order order)
        {
            this._serviceManager = new OTAServiceManager();
            this.MainOrder = order;            
        }

        public static OTAResult<List<GetProductsResponse>> GetDailyTickets(DateTime date)
        {
            var request = new OTARequest.GetProductRequest(date);

            return new OTAServiceManager().GetProducts(request);
        }

        /// <summary>
        /// 订单占用
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        public OTAResult<OrderOccupiesResponse> OrderOccupies()
        {
            var otaOrderRequest = new OTARequest.OrderOccupiesRequest();
            var otaDetail = new List<OTARequest.Detail>();
            var otaPostOrder = new OTARequest.PostOrder()
                                   {
                                       Ptime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                                       Edittype = string.Empty,                                       
                                   };
            var otaOrder = new OTARequest.Order()
                               {
                                   OrderNO = this.MainOrder.OrderObj.OrderCode,
                                   LinkName = this.MainOrder.OrderObj.ContactPersonName,
                                   LinkPhone = this.MainOrder.OrderObj.MobilePhoneNumber,
                                   LinkICNO = this.MainOrder.OrderObj.IdentityCardNumber,
                                   CreateTime = this.MainOrder.OrderObj.CreateTime.ToString(CultureInfo.InvariantCulture)
                               };            

            foreach (var ticket in this.MainOrder.OrderObj.Tickets)
            {
                var product = this.MainOrder.dailyProducts.FirstOrDefault(item => item.ProductCategoryId.Equals(ticket.TicketCategoryId));

                if (product != null)
                {
                    otaDetail.Add(new OTARequest.Detail()
                    {
                        OrderNO = otaOrder.OrderNO,
                        ItemID = ticket.TicketId.ToString(),
                        ProductCode = ticket.TicketCode,
                        ProductID = ticket.TicketProductId,
                        ProductPackID = product.ProductPackageId,
                        ProductName = product.ProductName,
                        ProductPrice = ticket.Price,
                        ProductCount = 1,
                        ProductEDate = ticket.TicketEndTime.ToString("yyyy-MM-dd"),
                        ProductSDate = ticket.TicketStartTime.ToString("yyyy-MM-dd"),
                        ItemType = string.Empty,
                        ECode = string.Empty
                    });
                }
                else
                {
                    otaDetail.Clear();
                    break;
                }
            }

            if (otaDetail.Any())
            {
                otaPostOrder.Order = otaOrder;
                otaPostOrder.Details = otaDetail;
                otaOrderRequest.postOrder = otaPostOrder;

                return this._serviceManager.OrderOccupies(otaOrderRequest);
            }
            else
            {
                return new OTAResult<OrderOccupiesResponse> { IsTrue = false };                
            }
        }

        public OTAResult<OrderReleaseResponse> OrderRelease()
        {
            var request = new OTARequest.OrderReleaseRequest() { OtaOrderNO = this.MainOrder.OrderObj.OrderId.ToString() };

            return new OTAServiceManager().OrderRelease(request);
        }

        public static OTAResult<OrderReleaseResponse> OrderRelease(string orderId)
        {
            var request = new OTARequest.OrderReleaseRequest() { OtaOrderNO = orderId };

            return new OTAServiceManager().OrderRelease(request);
        }

        public OTAResult<List<OrderFinishResponse>> OrderFinish()
        {
            var orderFinish = new OTARequest.OrderFinishRequest()
                                  {
                                      OtaOrderNO = this.MainOrder.OrderObj.OrderCode
                                  };

            return this._serviceManager.OrderFinish(orderFinish);            
        }

        public OTAResult<List<ChangeOrderEditResponse>> ChangeOrderEdit(ICollection<TicketEntity> refundTickets)
        {
            var changeRequest = new OTARequest.ChangeOrderEditRequest();
            var postOrder = new OTARequest.EditPostOrder()
                                {
                                    Ptime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                                    Edittype = "2",
                                    Count = refundTickets.Count
                                };
            var order = new OTARequest.EditOrder()
                            {
                                OrderNo = MainOrder.OrderObj.OrderCode
                            };
            var orderDetails = new List<OTARequest.EditOrderDetail>();

            foreach (var ticket in refundTickets)
            {
                orderDetails.Add(new OTARequest.EditOrderDetail()
                                     {
                                         ProductCode = ticket.ECode,
                                         Starttime = string.Empty
                                     });
            }

            postOrder.Order = order;
            postOrder.Details = orderDetails;
            changeRequest.PostOrder = postOrder;

            return this._serviceManager.ChangeOrderEdit(changeRequest);
        }

    }
}
