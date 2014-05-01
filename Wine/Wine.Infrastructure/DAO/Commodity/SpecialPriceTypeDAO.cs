using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wine.Infrastructure.Model.Commodity;

namespace Wine.Infrastructure.DAO.Commodity
{
    internal class SpecialPriceTypeDAO : BaseDAO<SpecialPriceType>
    {
        #region BaseDAO Implments
        public override SpecialPriceType Insert(SpecialPriceType t)
        {
            throw new NotImplementedException();
        }

        public override List<SpecialPriceType> Query(SpecialPriceType t)
        {
            throw new NotImplementedException();
        }

        public override void Delete(SpecialPriceType t)
        {
            throw new NotImplementedException();
        }

        public override void Update(SpecialPriceType t)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Custom Implements

        public List<SpecialPriceType> QueryAll()
        {
            var sql = string.Format("SELECT	* FROM	dbo.SpecialPriceType");
            var result = SqlHelper.ExecuteDataTable(this.DBConnection, CommandType.Text, sql);
            if (result != null && result.Rows.Count > 0)
                return result.ToList<SpecialPriceType>();
            return null;
        }

        public void TruncateAndBulkSpecialPriceType(List<SpecialPriceType> specialPriceTypeList)
        {
            var dataTableSchema = SqlHelper.ExecuteDataTable(this.DBConnection, System.Data.CommandType.StoredProcedure, "USP_SpecialPriceMapping_GetTableSchema");

            specialPriceTypeList.ForEach(delegate(SpecialPriceType specialPriceType)
            {
                specialPriceType.GoodsList.ForEach(delegate(int id)
                {
                    var newRow = dataTableSchema.NewRow();
                    newRow["TypeID"] = specialPriceType.TypeID;
                    newRow["GoodsID"] = id;
                    dataTableSchema.Rows.Add(newRow);
                });
            });

            SqlBulkHelper.BulkInsert(this.DBConnection, "SpecialPriceMapping", dataTableSchema);
        }

        #endregion
    }
}
