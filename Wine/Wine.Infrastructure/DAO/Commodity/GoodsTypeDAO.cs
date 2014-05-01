using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Threading.Tasks;
using Wine.Infrastructure.Model.Commodity;

namespace Wine.Infrastructure.DAO.Commodity
{
    internal class GoodsTypeDAO : BaseDAO<GoodsType>
    {

        public List<GoodsType> QueryAllGoodsType()
        {
            var sql = string.Format("SELECT	* FROM	dbo.GoodsType");
            var result = SqlHelper.ExecuteDataTable(this.DBConnection, CommandType.Text, sql);
            if (result != null && result.Rows.Count > 0)
                return result.ToList<GoodsType>();
            return null;
        }

        #region BaseDAO Implements
        public override GoodsType Insert(GoodsType t)
        {
            throw new NotImplementedException();
        }

        public override List<GoodsType> Query(GoodsType t)
        {
            throw new NotImplementedException();
        }

        public override void Delete(GoodsType t)
        {
            throw new NotImplementedException();
        }

        public override void Update(GoodsType t)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
