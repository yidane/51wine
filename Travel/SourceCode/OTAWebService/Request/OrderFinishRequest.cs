using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OTAWebService.Request
{
    public class OrderFinishRequest
    {
        public string OtaOrderNO { get; set; }
        public Parameters Parameters { get; set; }
    }
}