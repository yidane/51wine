using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using WeiXinPF.DBUtility;

namespace WeiXinPF.DAL
{
    public class wx_hotel_admin
    {
        public int Add(Model.wx_hotel_admin model)
        {
            using (IDbConnection db = DbFactory.GetOpenedConnection())
            {
                StringBuilder queryBuilder = new StringBuilder();

                queryBuilder.Append("Insert Into [dbo].[wx_hotel_admin]");
                queryBuilder.Append("   (ManagerId, HotelId)");
                queryBuilder.Append("Values");
                queryBuilder.Append("   (@ManagerId,@HotelId)");
                queryBuilder.Append("Select @Id=Scope_Identity()");

                DynamicParameters dynamicParameters = new DynamicParameters();
                dynamicParameters.AddDynamicParams(model);
                dynamicParameters.Add("@Id", dbType: DbType.Int32, direction: ParameterDirection.Output);

                db.Execute(queryBuilder.ToString(), dynamicParameters);

                return dynamicParameters.Get<int>("@Id");
            }
        }


        public Model.wx_hotel_admin GetModel(int uid)
        {
            using (IDbConnection db = DbFactory.GetOpenedConnection())
            {
                string query = "Select * From [dbo].[wx_hotel_admin] Where ManagerId=@ManagerId";

                return db.Query<Model.wx_hotel_admin>(query, new
                {
                    ManagerId = uid
                }).FirstOrDefault();
            }
        }

        public List<Model.wx_hotel_admin> GetModelList(string strWhere)
        {
            if (!string.IsNullOrEmpty(strWhere))
            {
                strWhere = " Where " + strWhere;
            }

            string query = "Select * From [dbo].[wx_hotel_admin]" + strWhere;
            using (IDbConnection db = DbFactory.GetOpenedConnection())
            {
                return db.Query<Model.wx_hotel_admin>(query).ToList();
            }
        }
    }
}
