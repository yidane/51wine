using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Travel.Presentation.WebPlugin.Order
{
    public class OrderEntity
    {
        public Guid OrderID { get; set; }
        public DateTime TradeTime { get; set; }
        public string OpenID { get; set; }
        public string ContactPersonName { get; set; }
        public string MobilePhoneNumber { get; set; }
        public string IdentityCardNumber { get; set; }
        public string TradeNo { get; set; }
        public string WeChatTradeNo { get; set; }
        public string OrderStatusDes { get; set; }
        public string OrderStatus { get; set; }
        public decimal TotalPrice { get; set; }
        public int TicketCount { get; set; }
        public decimal TicketPrice { get; set; }
        public string TicketCategoryName { get; set; }
    }
}