using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Travel.Application.DomainModules.WeChat.DTOS
{
    public class CardTicketDTO
    {
        public string shopId { get; set; }
        public string cardType { get; set; }
        public string cardId { get; set; }
        public string timestamp { get; set; }
        public string nonceStr { get; set; }
        public string signType { get; set; }
        public string cardSign { get; set; }
    }
}
