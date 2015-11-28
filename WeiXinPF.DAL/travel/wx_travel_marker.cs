using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using WeiXinPF.DBUtility;

namespace WeiXinPF.DAL
{
    public class wx_travel_marker
    {

        
        public int Add(Model.wx_travel_marker model)
        {
            using (IDbConnection db = DbFactory.GetOpenedConnection())
            {
                StringBuilder queryBuilder = new StringBuilder();

                queryBuilder.Append("Insert Into dbo.wx_travel_marker");
                queryBuilder.Append("   (wid,Name,Remark,[Left],[Top],Lng,Lat,Url,Description,extStr1,extStr2,extInt1,extInt2,Recommend)");
                queryBuilder.Append("Values ");
                queryBuilder.Append("   (@wid,@Name,@Remark,@Left,@Top,@Lng,@Lat,@Url,@Description,@extStr1,@extStr2,@extInt1,@extInt2,@Recommend)");
                queryBuilder.Append("Select @Id=Scope_Identity()");

                DynamicParameters dynamicParameters = new DynamicParameters();
                dynamicParameters.AddDynamicParams(model);
                dynamicParameters.Add("@Id", dbType: DbType.Int32, direction: ParameterDirection.Output);

                db.Execute(queryBuilder.ToString(), dynamicParameters);

                return dynamicParameters.Get<int>("@Id");
            }
        }

        public bool Update(Model.wx_travel_marker model)
        {
            using (IDbConnection db = DbFactory.GetOpenedConnection())
            {
                StringBuilder queryBuilder = new StringBuilder();
                queryBuilder.Append("Update [dbo].[wx_travel_marker]");
                queryBuilder.Append("   Set [wid] = @wid");
                queryBuilder.Append("      ,[Name] = @Name");
                queryBuilder.Append("      ,[Remark] = @Remark");
                queryBuilder.Append("      ,[Left] = @Left");
                queryBuilder.Append("      ,[Top] = @Top");
                queryBuilder.Append("      ,[Lng] = @Lng");
                queryBuilder.Append("      ,[Lat] = @Lat");
                queryBuilder.Append("      ,[Url] = @Url");
                queryBuilder.Append("      ,[Description] = @Description");
                queryBuilder.Append("      ,[extStr1] = @extStr1");
                queryBuilder.Append("      ,[extStr2] = @extStr2");
                queryBuilder.Append("      ,[extInt1] = @extInt1");
                queryBuilder.Append("      ,[extInt2] = @extInt2");
                queryBuilder.Append("      ,[Recommend] = @Recommend");
                queryBuilder.Append(" Where [Id]=@Id");

                return db.Execute(queryBuilder.ToString(), model) > 0;
            }
        }

        public bool Delete(int id)
        {
            using (IDbConnection db = DbFactory.GetOpenedConnection())
            {
                return db.Execute("Delete [dbo].[wx_travel_marker] Where Id=@Id", new { Id = id }) > 0;
            }
        }

        public Model.wx_travel_marker GetModel(int id)
        {
            using (IDbConnection db = DbFactory.GetOpenedConnection())
            {
                return db.Query<Model.wx_travel_marker>("Select * From [dbo].[wx_travel_marker] Where Id=@Id",
                    new { Id = id }
                ).FirstOrDefault();
            }
        }

        public List<Model.wx_travel_marker> GetModelList(string strWhere)
        {
            if (!string.IsNullOrEmpty(strWhere))
            {
                strWhere = "Where " + strWhere;
            }

            using (IDbConnection db = DbFactory.GetOpenedConnection())
            {
                return db.Query<Model.wx_travel_marker>("Select * From [dbo].[wx_travel_marker] " + strWhere).ToList();
            }
        }

        
    }
}
