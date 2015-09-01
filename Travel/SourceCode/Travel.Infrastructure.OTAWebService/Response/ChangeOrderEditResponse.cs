using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Travel.Infrastructure.OTAWebService.Response
{
    public class ChangeOrderEditResponse
    {
        public DateTime StartTime { get; set; }
        public string ProductCode { get; set; }
        public bool IsSuccel { get; set; }
        public int CountP { get; set; }
        public int Audit { get; set; }
        public string AuditDesc
        {
            get
            {
                switch (Audit)
                {
                    case 0:
                        return "不需要审核";
                    case 1:
                        return "需要审核";
                    default:
                        return string.Empty;
                }
            }
        }
    }
}
