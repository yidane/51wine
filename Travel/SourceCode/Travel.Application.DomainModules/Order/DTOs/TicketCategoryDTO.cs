using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Travel.Application.DomainModules.Order.DTOs
{
    public class TicketCategoryDTO
    {
        public int category { get; set; }

        public IList<TicketCategorySub> content { get; set; }
    }

    public class TicketCategorySub
    {
        public string ticketCategoryId { get; set; }

        public string ticketType { get; set; }

        public decimal price { get; set; }

        public string ticketName { get; set; }

        public string type { get; set; }

        public string image { get; set; }

        public bool canUse { get; set; }
    }
}
