using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Travel.Application.DomainModules.Order.Entity
{
    public class OrderRequestEntity
    {
        public string OpenId { get; set; }

        public string TicketCategory { get; set; }

        public string TicketName { get; set; }

        public int Count { get; set; }

        public string CouponId { get; set; }

        public string ContactPersonName { get; set; }

        public string MobilePhoneNumber { get; set; }

        public string IdentityCardNumber { get; set; }

    }
}
