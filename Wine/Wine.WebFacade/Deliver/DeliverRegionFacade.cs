using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wine.Infrastructure.Model.Deliver;
using Wine.Infrastructure.BLL.Deliver;

namespace Wine.WebFacade.Deliver
{
    public class DeliverRegionFacade
    {
        public List<DeliverRegion> QueryAllDeliverRegion()
        {
            return new DeliverRegionBLL().QueryAllDeliverRegion();
        }
    }
}
