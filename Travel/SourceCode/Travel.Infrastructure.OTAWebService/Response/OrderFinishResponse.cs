namespace Travel.Infrastructure.OTAWebService.Response
{
    public class OrderFinishResponse
    {
        public string OrderNo { get; set; }
        public string ItemID { get; set; }
        public string ProductCode { get; set; }
        public int ProductPackID { get; set; }
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public decimal ProductPrice { get; set; }
        public int ProductCount { get; set; }
        public string ProductSDate { get; set; }
        public string ProductEDate { get; set; }
        public string ItemType { get; set; }
        public string ECode { get; set; }
    }
}
