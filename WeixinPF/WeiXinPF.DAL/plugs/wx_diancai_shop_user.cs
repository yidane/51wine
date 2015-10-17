using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using WeiXinPF.DBUtility;

namespace WeiXinPF.DAL
{
   public class wx_diancai_shop_user
    {

        public int Add(Model.wx_diancai_shop_user model)
        {
            using (IDbConnection db = DbFactory.GetOpenedConnection())
            {
                StringBuilder queryBuilder = new StringBuilder();

                queryBuilder.Append("Insert Into [dbo].[wx_diancai_shop_user]");
                queryBuilder.Append("   (ManagerId, ShopId,AdminId)");
                queryBuilder.Append("Values");
                queryBuilder.Append("   (@ManagerId,@ShopId,@AdminId)");
                queryBuilder.Append("Select @Id=Scope_Identity()");

                DynamicParameters dynamicParameters = new DynamicParameters();
                dynamicParameters.AddDynamicParams(model);
                dynamicParameters.Add("@Id", dbType: DbType.Int32, direction: ParameterDirection.Output);

                db.Execute(queryBuilder.ToString(), dynamicParameters);

                return dynamicParameters.Get<int>("@Id");
            }
        }

        public Model.wx_diancai_shop_user GetModel(int uid)
        {
            using (IDbConnection db = DbFactory.GetOpenedConnection())
            {
                string query = "Select * From [dbo].[wx_diancai_shop_user] Where ManagerId=@ManagerId";

                return db.Query<Model.wx_diancai_shop_user>(query, new
                {
                    ManagerId = uid
                }).FirstOrDefault();
            }
        }

        public List<Model.wx_diancai_shop_user> GetModelList(string strWhere)
        {
            if (!string.IsNullOrEmpty(strWhere))
            {
                strWhere = " Where " + strWhere;
            }

            string query = "Select * From [dbo].[wx_diancai_shop_user]" + strWhere;
            using (IDbConnection db = DbFactory.GetOpenedConnection())
            {
                return db.Query<Model.wx_diancai_shop_user>(query).ToList();
            }
        }
    }
}
