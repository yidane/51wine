using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Travel.Application.DomainModules.Order.Core
{
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
                var dateTicket = this.MainOrder.DateTicketList.FirstOrDefault(item => item.DateTicketId == ticket.TicketId);
                var orderDetail = this.MainOrder.OrderObj.OrderDetails.FirstOrDefault();

                if (dateTicket != null && orderDetail != null)
                {
                    otaDetail.Add(new OTARequest.Detail()
                                 {
                                     OrderNO = otaOrder.OrderNO,
                                     ItemID = orderDetail.OrderDetailId.ToString(),
                                     ProductCode = ticket.TicketCode,
                                     ProductID = ticket.TicketId,
                                     ProductPackID = dateTicket.TicketPackageId,
                                     ProductName = dateTicket.TicketName,
                                     ProductPrice = dateTicket.TicketPrice,
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

        public bool OrderRelease()
        {
            throw new NotImplementedException();
        }

        public void OrderFinish()
        {
            var orderFinish = new OTARequest.OrderFinishRequest()
                                  {
                                      OtaOrderNO = this.MainOrder.OrderObj.OrderCode
                                  };

            var result = this._serviceManager.OrderFinish(orderFinish);

            if (result.IsTrue)
            {
                if (result.ResultData.Count == this.MainOrder.OrderObj.Tickets.Count)
                {
                    var arrTickets = this.MainOrder.OrderObj.Tickets.ToArray();
                    for (int i = 0; i < this.MainOrder.OrderObj.Tickets.Count; i++)
                    {
                        arrTickets[i].ECode = result.ResultData[i].ECode;
                        arrTickets[i].TicketStatus = Order.TicketStatus_WaitUse;
                    }

                    this.MainOrder.OrderObj.Tickets = arrTickets.ToList();
                    // 更改订单状态为
                    this.MainOrder.OrderObj.OrderStatus = Order.OrderStatus_WaitUse;
                    this.MainOrder.OrderObj.ModifyOrder();
                }
                else
                {
                    throw new Exception();
                }
            }
            else
            {
                throw new InvalidOperationException("OrderFinish");
            }
        }

        public bool ChangeOrderEdit()
        {
            throw new NotImplementedException();
        }
    }
}
