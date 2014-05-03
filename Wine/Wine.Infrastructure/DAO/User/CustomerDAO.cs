using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using Wine.Infrastructure.DAO;
using Wine.Infrastructure.Model;
using Wine;
using Wine.Util;
using Wine.Infrastructure.Model.User;

namespace Wine.Infrastructure.DAO.User
{
    internal class CustomerDAO : BaseDAO<Customer>
    {
        #region BaseDAO Implements
        public override Customer Insert(Customer t)
        {
            var sqlparams = new List<SqlParameter>();
            sqlparams.Add(new SqlParameter("@LoginName", t.LoginName));
            sqlparams.Add(new SqlParameter("@NickName", t.NickName));
            sqlparams.Add(new SqlParameter("@Password", t.Password));

            var result = SqlHelper.ExecuteDataTable(ConfigManager.DBConnection, CommandType.StoredProcedure, "USP_Customer_Insert", sqlparams.ToArray());

            if (result == null)
                return null;

            return result.ToList<Customer>()[0];
        }

        public override List<Customer> Query(Customer t)
        {
            return null;
        }

        public override void Delete(Customer t)
        {
            throw new NotImplementedException();
        }

        public override void Update(Customer t)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Custom Implements

        public bool Contain(string loginName)
        {
            var sqlparams = new List<SqlParameter>();
            sqlparams.Add(new SqlParameter("@LoginName", loginName));

            var result = SqlHelper.ExecuteScalar(ConfigManager.DBConnection, CommandType.StoredProcedure, "USP_Customer_CheckLoginNameExists", sqlparams.ToArray());
            if (result != null && result != DBNull.Value)
                return Convert.ToBoolean(result);

            return false;
        }

        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="accountType"></param>
        /// <param name="loginName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public Customer Login(int accountType, string loginName, string password)
        {
            var sqlparams = new List<SqlParameter>();
            sqlparams.Add(new SqlParameter("@AccountType", accountType));
            sqlparams.Add(new SqlParameter("@LoginName", loginName));
            sqlparams.Add(new SqlParameter("@Password", password));

            var dt = SqlHelper.ExecuteDataTable(ConfigManager.DBConnection, CommandType.StoredProcedure, "USP_Customer_Login", sqlparams.ToArray());
            if (dt != null && dt.Rows.Count > 0)
                return dt.ToList<Customer>()[0];

            return null;
        }

        #endregion
    }
}
