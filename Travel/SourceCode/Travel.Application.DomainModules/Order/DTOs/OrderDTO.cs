using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Travel.Application.DomainModules.Order.DTOs
{
    public class OrderDTO
    {
        public string OrderId { get; set; }

        public string OrderCode { get; set; }

        public decimal singleTicketPrice { get; set; }

        public decimal TotelFee { get; set; }

        public string TicketName { get; set; }

        public string BuyTime { get; set; }

        public int TicketCount { get; set; }

        public string OrderStatus { get; set; }

        public bool hasRefundTicket { get; set; }

        /// <summary>
        /// 退票类型，部分，全部
        /// </summary>
        public string RefundType { get; set; }
    }
}
