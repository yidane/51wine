using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using Wine.Infrastructure.Model.Orders;

namespace Wine.Infrastructure.DAO.Orders
{
    internal class OrderDetailDAO : BaseDAO<OrderDetail>
    {
        #region BaseDAO Implements
        public override OrderDetail Insert(OrderDetail t)
        {
            var sqlparams = new List<SqlParameter>();
            sqlparams.Add(new SqlParameter("@OrderID", t.OrderID));
            sqlparams.Add(new SqlParameter("@GoodsID", t.GoodsID));
            sqlparams.Add(new SqlParameter("@GoodsName", t.GoodsName));
            sqlparams.Add(new SqlParameter("@GoodPrice", t.GoodsPrice));
            sqlparams.Add(new SqlParameter("@GoodsCount", t.GoodsCount));

            t.OrderDetailID = Convert.ToInt32(SqlHelper.ExecuteScalar(this.DBConnection, System.Data.CommandType.StoredProcedure, "USP_DeliverOrderDetail_Insert", sqlparams.ToArray()));

            return t;
        }

        public override List<OrderDetail> Query(OrderDetail t)
        {
            throw new NotImplementedException();
        }

        public override void Delete(OrderDetail t)
        {
            throw new NotImplementedException();
        }

        public override void Update(OrderDetail t)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
