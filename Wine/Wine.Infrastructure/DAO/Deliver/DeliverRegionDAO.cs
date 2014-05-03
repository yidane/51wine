using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wine.Infrastructure.Model.Deliver;

namespace Wine.Infrastructure.DAO.Deliver
{
    internal class DeliverRegionDAO : BaseDAO<DeliverRegionDAO>
    {
        #region BaseDAO Implements
        public override DeliverRegionDAO Insert(DeliverRegionDAO t)
        {
            throw new NotImplementedException();
        }

        public override List<DeliverRegionDAO> Query(DeliverRegionDAO t)
        {
            throw new NotImplementedException();
        }

        public override void Delete(DeliverRegionDAO t)
        {
            throw new NotImplementedException();
        }

        public override void Update(DeliverRegionDAO t)
        {
            throw new NotImplementedException();
        }
        #endregion

        public List<DeliverRegion> QueryAllDeliverRegion()
        {
            return SqlHelper.ExecuteDataTable(this.DBConnection, System.Data.CommandType.Text, "SELECT	* FROM	DeliverRegion").ToList<DeliverRegion>();
        }
    }
}
