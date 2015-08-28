using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Travel.Infrastructure.OTAWebService.Response
{
    public class GetProductsResponse
    {
        public string ProductID { get; set; }
        public int ProductPackID { get; set; }
        public string ProductCode { get; set; }
        public string ProductName { get; set; }
        public decimal ProductPrice { get; set; }
        public string ProductType { get; set; }
        public string Date { get; set; }
        public string PRODUCT_ISTHEATRE { get; set; }
    }
}
