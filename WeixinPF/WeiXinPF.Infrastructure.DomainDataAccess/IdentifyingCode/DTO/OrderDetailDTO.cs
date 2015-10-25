using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WeiXinPF.Infrastructure.DomainDataAccess.IdentifyingCode.DTO
{
    public class OrderDetailDTO
    {
        public int Id { get; set; }

        public int OrderId { get; set; }

        public string PayStatus { get; set; }

        public string OrderNumber { get; set; }

        public string CustomerName { get; set; }

        public double PayAmount { get; set; }

        public DateTime ModifyTime { get; set; }
    }
}
