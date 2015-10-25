using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WeiXinPF.Infrastructure.DomainDataAccess.IdentifyingCode.DTO
{
    public class IdentifyingCodeDTO
    {
        public string ProductName { get; set; }

        public string Number { get; set; }

        public double Price { get; set; }

        public string IdentifyingCode { get; set; }

        public string Status { get; set; }

        public DateTime ModifyTime { get; set; }
    }
}
