namespace WeiXinPF.Infrastructure.DomainDataAccess.IdentifyingCode.DTO
{
    public class IdentifyingCodeDetailSearchDTO
    {
        public int OrderId { get; set; }

        public int ProductId { get; set; }

        public string ProductName { get; set; }

        public double Price { get; set; }

        public int Status { get; set; }

        public int Number { get; set; }

        public string LeaveTime { get; set; }

        public string ArriveTime { get; set; }

        public double TotelPrice { get; set; }
    }
}
