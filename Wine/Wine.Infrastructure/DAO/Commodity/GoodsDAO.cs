using System;
using System.Collections.Generic;
using Wine.Infrastructure.Model.Commodity;
using System.Data;
using System.Data.SqlClient;

namespace Wine.Infrastructure.DAO.Commodity
{
    internal class GoodsDAO : BaseDAO<Goods>
    {
        #region BaseDAO Implements
        public override Goods Insert(Goods t)
        {
            throw new NotImplementedException();
        }

        public override List<Goods> Query(Goods t)
        {
            throw new NotImplementedException();
        }

        public override void Delete(Goods t)
        {
            throw new NotImplementedException();
        }

        public override void Update(Goods t)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Custom Implements
        public Goods QueryGoods(int id)
        {
            var sqlparams = new List<SqlParameter>();
            sqlparams.Add(new SqlParameter("@ID", id));

            var result = SqlHelper.ExecuteDataTable(this.DBConnection, CommandType.StoredProcedure, "USP_Goods_QueryGoodsDetail", sqlparams.ToArray());
            if (result != null && result.Rows.Count > 0)
                return result.ToList<Goods>()[0];
            return null;
        }


        public List<Goods> QueryGoodsByGoodsType(GoodsType goodsType)
        {
            return null;
        }

        public List<Goods> QueryGoodsByGoodsType(int goodsType, int id, out int sum)
        {
            var sqlparams = new List<SqlParameter>();
            sqlparams.Add(new SqlParameter("@WineType", goodsType));
            sqlparams.Add(new SqlParameter("@ID", id));
            sum = 0;
            var sumSqlparam = new SqlParameter("@Sum", sum);
            sumSqlparam.Direction = ParameterDirection.Output;
            sqlparams.Add(sumSqlparam);

            var result = SqlHelper.ExecuteDataTable(this.DBConnection, CommandType.StoredProcedure, "USP_Goods_QueryGoods", sqlparams.ToArray());

            if (result != null && result.Rows.Count > 0)
                return result.ToList<Goods>();
            return null;
        }

        public void TruncateAndBulkGoods(List<Goods> goodsList)
        {
            var dataTableSchema = SqlHelper.ExecuteDataTable(this.DBConnection, System.Data.CommandType.StoredProcedure, "USP_Goods_GetTableSchema");

            goodsList.ForEach(delegate(Goods goods)
            {
                var newRow = dataTableSchema.NewRow();
                newRow["CurrentPrice"] = goods.CurrentPrice;
                newRow["GoodsID"] = goods.GoodsID;
                newRow["GoodsName"] = goods.GoodsName;
                newRow["GoodsTypeID"] = goods.GoodsTypeID;
                newRow["HistoryPrice"] = goods.HistoryPrice;
                newRow["Pictureurl"] = goods.Pictureurl;

                dataTableSchema.Rows.Add(newRow);
            });

            SqlBulkHelper.BulkInsert(this.DBConnection, "Goods", dataTableSchema);
        }

        #endregion
    }
}
