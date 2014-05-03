using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wine.Infrastructure.Model.User;
using System.Data.SqlClient;

namespace Wine.Infrastructure.DAO.User
{
    internal class CustomerConsigneeInfoDAO : BaseDAO<CustomerConsigneeInfo>
    {
        #region BaseDAO Implements
        public override CustomerConsigneeInfo Insert(CustomerConsigneeInfo t)
        {
            var sqlparams = new List<SqlParameter>();
            sqlparams.Add(new SqlParameter("@CustomerID", t.CustomerID));
            sqlparams.Add(new SqlParameter("@ConsigneeUserName", t.ConsigneeUserName));
            sqlparams.Add(new SqlParameter("@ConsigneeMobile", t.ConsigneeMobile));
            sqlparams.Add(new SqlParameter("@DeliverRegionID", t.DeliverRegionID));
            sqlparams.Add(new SqlParameter("@Adress", t.Adress));
            sqlparams.Add(new SqlParameter("@Email", t.Email));

            t.CustomerConsigneeInfoID = Convert.ToInt32(SqlHelper.ExecuteScalar(this.DBConnection, System.Data.CommandType.StoredProcedure, "USP_CustomerConsigneeInfo_Insert", sqlparams.ToArray()));
            return t;
        }

        public override List<CustomerConsigneeInfo> Query(CustomerConsigneeInfo t)
        {
            var sqlparams = new List<SqlParameter>();
            sqlparams.Add(new SqlParameter("@Customer", t.CustomerID));

            var result = SqlHelper.ExecuteDataTable(this.DBConnection, System.Data.CommandType.StoredProcedure, "USP_CustomerConsigneeInfo_QueryCustomerAllConsigneeInfo", sqlparams.ToArray());
            if (result != null && result.Rows.Count > 0)
                return result.ToList<CustomerConsigneeInfo>();
            return null;
        }

        public override void Delete(CustomerConsigneeInfo t)
        {
            throw new NotImplementedException();
        }

        public override void Update(CustomerConsigneeInfo t)
        {
            throw new NotImplementedException();
        }
        #endregion

        public CustomerConsigneeInfo QueryRecentlyCustomerConsigneeInfo(int customerID)
        {
            var sqlparams = new List<SqlParameter>();
            sqlparams.Add(new SqlParameter("@CustomerID", customerID));
            var result = SqlHelper.ExecuteDataTable(this.DBConnection, System.Data.CommandType.StoredProcedure, "USP_CustomerConsigneeInfo_QueryRecentlyCustomerConsigneeInfo", sqlparams.ToArray());
            if (result != null && result.Rows.Count > 0)
                return result.ToList<CustomerConsigneeInfo>()[0];
            return null;
        }

        public CustomerConsigneeInfo QueryCustomerConsigneeInfo(int customerID, int consigneeInfoID)
        {
            var sqlparams = new List<SqlParameter>();
            sqlparams.Add(new SqlParameter("@CustomerID", customerID));
            sqlparams.Add(new SqlParameter("@CustomerConsigneeInfoID", consigneeInfoID));
            var result = SqlHelper.ExecuteDataTable(this.DBConnection, System.Data.CommandType.StoredProcedure, "USP_CustomerConsigneeInfo_QueryByID", sqlparams.ToArray());
            if (result != null && result.Rows.Count > 0)
                return result.ToList<CustomerConsigneeInfo>()[0];
            return null;
        }
    }
}
