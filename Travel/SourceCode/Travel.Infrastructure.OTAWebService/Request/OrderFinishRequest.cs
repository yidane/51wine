namespace Travel.Infrastructure.OTAWebService.Request
{
    public class OrderFinishRequest
    {
        public string OtaOrderNO { get; set; }
        public Parameter Parameters
        {
            get
            {
                return new Parameter();
            }
        }
        public int platformSend { get { return 0; } }
    }
}