using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OTAWebService.Request
{
    public class OrderOccupiesRequest
    {
        public PostOrder postOrder { get; set; }
    }

    public class PostOrder
    {
        public string Ptime { get; set; }
        public Order Order { get; set; }
        public List<Detail> Details { get; set; }
        public string Edittype { get; set; }
        public string Type { get; set; }
        public string parkCode { get; set; }
    }

    public class Order
    {
        public string OrderNO { get; set; }
        public string LinkName { get; set; }
        public string LinkPhone { get; set; }
        public string LinkICNO { get; set; }
        public string CreateTime { get; set; }
    }

    public class Detail
    {
        public string OrderNO { get; set; }
        public string ItemID { get; set; }
        public string ProductCode { get; set; }
        public int ProductID { get; set; }
        public int ProductPackID { get; set; }
        public decimal ProductPrice { get; set; }
        public int ProductCount { get; set; }
        public string ProductSDate { get; set; }
        public string ProductEDate { get; set; }
    }
}
