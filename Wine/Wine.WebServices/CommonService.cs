using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wine.WebFacade.Deliver;

namespace Wine.WebServices
{
    public class CommonService
    {
        public string QueryAllDeliverRegion()
        {
            var data = new DeliverRegionFacade().QueryAllDeliverRegion();
            var isSucceed = data != null;
            return new WebServiceResult<object>(isSucceed, data).ToString();
        }
    }
}
