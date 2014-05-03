using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wine.Infrastructure.Model.Deliver;
using Wine.Infrastructure.DAO.Deliver;

namespace Wine.Infrastructure.BLL.Deliver
{
    public class DeliverRegionBLL
    {
        public List<DeliverRegion> QueryAllDeliverRegion()
        {
            return new DeliverRegionDAO().QueryAllDeliverRegion();
        }
    }
}
