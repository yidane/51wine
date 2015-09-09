using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Travel.Presentation.WebPlugin.Order
{
    public class OrderEntity
    {
        public DateTime TradeTime { get; set; }
        public string AppID { get; set; }
        public string ContactPersonName { get; set; }
        public string MobilePhoneNumber { get; set; }
        public string IdentityCardNumber { get; set; }
        public string TradeNo { get; set; }
        public string WeChatTradeNo { get; set; }
        public string OrderStatusDes { get; set; }
        public string OrderStatus { get; set; }
        public float TotalPrice { get; set; }
        public int TicketCount { get; set; }
        public float TicketPrice { get; set; }
    }
}