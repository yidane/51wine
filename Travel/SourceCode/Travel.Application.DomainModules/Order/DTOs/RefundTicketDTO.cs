using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Travel.Application.DomainModules.Order.DTOs
{
    public class RefundTicketDTO
    {
        public int TicketId { get; set; }

        public string OrderId { get; set; }

        public string TicketName { get; set; }

        public string TicketCategoryId { get; set; }

        public string TicketCode { get; set; }

        public string Price { get; set; }

        public string TicketStatus { get; set; }

        public string ECode { get; set; }
    }
}
