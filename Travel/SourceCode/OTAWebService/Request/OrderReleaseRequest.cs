using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OTAWebService.Request
{
    public class OrderReleaseRequest
    {
        public string OtaOrderNO { get; set; }
        public Parameters Parameters { get; set; }
    }

    public class Parameters
    {
        /// <summary>
        /// 查询类型 00门票  03剧场票
        /// </summary>
        public string type { get; set; }

        /// <summary>
        /// 景区编码：gspf
        /// </summary>
        public string parkCode { get; set; }
    }
}