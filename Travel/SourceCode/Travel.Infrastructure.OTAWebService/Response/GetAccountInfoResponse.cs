using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Travel.Infrastructure.OTAWebService.Response
{
    public class GetAccountInfoResponse
    {
        public int AGN_BALANCE_ID { get; set; }
        public int AGN_BALANCE_OTAID { get; set; }
        public int AGN_BALANCE_BALANCE { get; set; }
        public int AGN_BALANCE_CREDIT { get; set; }
        public int AGN_BALANCE_CREDIT_BL { get; set; }
        public string AGN_BALANCE_UPDATE { get; set; }
        public int AGN_BALANCE_STATUS { get; set; }
        public int AGN_BALANCE_PARKID { get; set; }
        public int AGN_BALANCE_ORGID { get; set; }
        public string ISLOGIDEL { get; set; }
        public string AGN_BALANCE_DELID { get; set; }
        public string AGN_BALANCE_DELNAME { get; set; }
        public string AGN_BALANCE_DELDATE { get; set; }
        public int AGN_BALANCE_FROZEN { get; set; }
    }
}
