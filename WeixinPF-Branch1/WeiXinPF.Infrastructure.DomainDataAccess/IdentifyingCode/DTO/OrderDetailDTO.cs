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

        public int Number { get; set; }

        public string ProductName { get; set; }

        public DateTime ArriveTime { get; set; }

        public DateTime LeaveTime { get; set; }

        public double Price { get; set; }

        public double PayAmount { get; set; }

        public DateTime ModifyTime { get; set; }

        public double RefundAmount { get; set; }
        public double RealAmount { get; set; }
    }
}
