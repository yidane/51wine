using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wine.Infrastructure.Model.Orders;
using System.Data.SqlClient;

namespace Wine.Infrastructure.DAO.Orders
{
    internal class OrderDAO : BaseDAO<Order>
    {
        #region BaseDAO Implements
        public override Order Insert(Order t)
        {
            using (SqlConnection con = new SqlConnection(this.DBConnection))
            {
                con.Open();

                SqlTransaction tran = con.BeginTransaction();
                try
                {
                    var sqlparams = new List<SqlParameter>();
                    sqlparams.Add(new SqlParameter("@OrderNO", t.OrderNO));
                    sqlparams.Add(new SqlParameter("@OrderType", t.OrderType));
                    sqlparams.Add(new SqlParameter("@OrderStatus", t.OrderStatus));
                    sqlparams.Add(new SqlParameter("@OrderOwnerID", t.OrderOwnerID));
                    sqlparams.Add(new SqlParameter("@TotalPrice", t.TotalPrice));
                    sqlparams.Add(new SqlParameter("@CustomerConsigneeInfoID", t.CustomerConsigneeInfoID));
                    sqlparams.Add(new SqlParameter("@ConsigneeUserName", t.ConsigneeUserName));
                    sqlparams.Add(new SqlParameter("@ConsigneeMobile", t.ConsigneeMobile));
                    sqlparams.Add(new SqlParameter("@DeliverRegionID", t.DeliverRegionID));
                    sqlparams.Add(new SqlParameter("@Adress", t.Adress));
                    sqlparams.Add(new SqlParameter("@CustomerRemarks", t.CustomerRemarks));

                    t.OrderID = Convert.ToInt32(SqlHelper.ExecuteScalar(tran, "USP_DeliverOrder_Insert", sqlparams.ToArray()));

                    foreach (OrderDetail detail in t.Details)
                    {
                        var sqlparamsDetail = new List<SqlParameter>();
                        sqlparamsDetail.Add(new SqlParameter("@OrderID", t.OrderID));
                        sqlparamsDetail.Add(new SqlParameter("@GoodsID", detail.GoodsID));
                        sqlparamsDetail.Add(new SqlParameter("@GoodsName", detail.GoodsName));
                        sqlparamsDetail.Add(new SqlParameter("@GoodPrice", detail.GoodsPrice));
                        sqlparamsDetail.Add(new SqlParameter("@GoodsCount", detail.GoodsCount));
                        SqlHelper.ExecuteNonQuery(tran, "USP_DeliverOrderDetail_Insert", sqlparamsDetail.ToArray());
                    }

                    tran.Commit();
                    return t;
                }
                catch
                {
                    tran.Rollback();
                    throw;
                }
            }
        }

        public override List<Order> Query(Order t)
        {
            throw new NotImplementedException();
        }

        public override void Delete(Order t)
        {
            throw new NotImplementedException();
        }

        public override void Update(Order t)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
