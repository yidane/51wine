using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WeiXinPF.Application.DomainModules.IdentifyingCode.DTO
{
    public class IdentifyingCodeDetailSearchDTO
    {
        public string ProductId { get; set; }

        public string ProductName { get; set; }

        public decimal Price { get; set; }

        public int Status { get; set; }

    }
}
