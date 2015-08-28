using System.Collections.Generic;
using Travel.Infrastructure.CommonFunctions;

namespace Travel.Infrastructure.OTAWebService.Request
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
        public string Type
        {
            get { return "00"; }
        }

        public string parkCode
        {
            get { return OTAConfigManager.ParkCode; }
        }

        public override string ToString()
        {
            var postOrder = new
                {
                    Ptime = Ptime,
                    Order = Order.ToString(),
                    Details = JSONHelper.Serialize(Details),
                    Edittype = Edittype,
                    Type = Type,
                    parkCode = parkCode
                };
            return JSONHelper.Serialize(postOrder);
        }
    }

    public class Order
    {
        public string OrderNO { get; set; }
        public string LinkName { get; set; }
        public string LinkPhone { get; set; }
        public string LinkICNO { get; set; }
        public string CreateTime { get; set; }

        public override string ToString()
        {
            return JSONHelper.Serialize(this);
        }
    }

    public class Detail
    {
        public string OrderNO { get; set; }
        public string ItemID { get; set; }
        public string ProductCode { get; set; }
        public int ProductID { get; set; }
        public int ProductPackID { get; set; }
        public string ProductName { get; set; }
        public decimal ProductPrice { get; set; }
        public int ProductCount { get; set; }
        public string ProductSDate { get; set; }
        public string ProductEDate { get; set; }
        public string ItemType { get; set; }
        public string ECode { get; set; }

        public override string ToString()
        {
            return JSONHelper.Serialize(this);
        }
    }
}