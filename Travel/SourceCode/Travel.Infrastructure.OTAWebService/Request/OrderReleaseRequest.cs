using Travel.Infrastructure.CommonFunctions;

namespace Travel.Infrastructure.OTAWebService.Request
{
    public class OrderReleaseRequest
    {
        public string OtaOrderNO { get; set; }
        public Parameter Parameters
        {
            get { return new Parameter(); }
        }
    }

    public class Parameter
    {
        /// <summary>
        /// 查询类型 00门票  03剧场票
        /// </summary>
        public string type { get { return "00"; } }

        /// <summary>
        /// 景区编码：gspf
        /// </summary>
        public string parkCode
        {
            get { return OTAConfigManager.ParkCode; }
        }

        public override string ToString()
        {
            return JSONHelper.Serialize(this);
        }
    }
}