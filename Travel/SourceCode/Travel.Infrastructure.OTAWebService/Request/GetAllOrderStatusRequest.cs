using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Travel.Infrastructure.CommonFunctions;

namespace Travel.Infrastructure.OTAWebService.Request
{
    public class GetAllOrderStatusRequest
    {
        public List<OrderNoCode> PostOrder { get; set; }
        public string BusinessType
        {
            get { return "00"; }
        }

        public string ParkCode
        {
            get { return OTAConfigManager.ParkCode; }
        }

        public override string ToString()
        {
            return JSONHelper.Serialize(this.PostOrder);
        }
    }

    public class OrderNoCode
    {
        public string OrderCode { get; set; }
    }
}
