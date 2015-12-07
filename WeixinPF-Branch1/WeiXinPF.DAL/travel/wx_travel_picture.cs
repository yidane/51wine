using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using Dapper;
using WeiXinPF.Common;
using WeiXinPF.DBUtility;
using WeiXinPF.Model;

namespace WeiXinPF.DAL
{
    public class wx_travel_picture
    {
        public wx_travel_picture()
        {
        }

        public int Add(Model.wx_travel_picture model)
        {
            using (IDbConnection db = DbFactory.GetOpenedConnection())
            {
                StringBuilder queryBuilder = new StringBuilder();

                queryBuilder.Append("Insert Into [dbo].[wx_travel_picture]");
                queryBuilder.Append("   ([DetailId],[Name],[PicPath],[Content],[OrderNo],[CreateDate])");
                queryBuilder.Append("Values");
                queryBuilder.Append("   (@DetailId,@Name,@PicPath,@Content,@OrderNo,@CreateDate)");
                queryBuilder.Append("Select @Id=Scope_Identity()");

                DynamicParameters dynamicParameters = new DynamicParameters();
                dynamicParameters.AddDynamicParams(model);
                dynamicParameters.Add("@Id", dbType: DbType.Int32, direction: ParameterDirection.Output);

                db.Execute(queryBuilder.ToString(), dynamicParameters);

                return dynamicParameters.Get<int>("@Id");
            }
        }

        public List<Model.wx_travel_picture> GetModelList(string strWhere)
        {
            if (!string.IsNullOrEmpty(strWhere))
            {
                strWhere = "Where " + strWhere;
            }

            using (IDbConnection db = DbFactory.GetOpenedConnection())
            {
                return db.Query<Model.wx_travel_picture>("Select * From [dbo].[wx_travel_picture] " + strWhere).ToList();
            }
        }

        public bool Update(Model.wx_travel_picture model)
        {
            using (IDbConnection db = DbFactory.GetOpenedConnection())
            {
                StringBuilder queryBuilder = new StringBuilder();
                queryBuilder.Append("Update [dbo].[wx_travel_picture]");
                queryBuilder.Append(" Set [DetailId] = @DetailId");
                queryBuilder.Append(",[Name] = @Name");
                queryBuilder.Append(",[PicPath] = @PicPath");
                queryBuilder.Append(",[Content] = @Content");
                queryBuilder.Append(",[OrderNo] = @OrderNo");
                queryBuilder.Append(",[CreateDate] = @CreateDate");
                queryBuilder.Append(" Where Id=@Id");

                return db.Execute(queryBuilder.ToString(), model) > 0;
            }
        }

        public bool Delete(int id)
        {
            using (IDbConnection db = DbFactory.GetOpenedConnection())
            {
                return db.Execute("Delete [dbo].[wx_travel_picture] Where Id=@Id", new { Id = id }) > 0;
            }
        }

        public Model.wx_travel_picture GetModel(int id)
        {
            using (IDbConnection db = DbFactory.GetOpenedConnection())
            {
                return db.Query<Model.wx_travel_picture>("Select * From [dbo].[wx_travel_picture] Where Id=@Id",
                    new { Id = id }
                ).FirstOrDefault();
            }
        }

        public DataSet GetPageList(int pageSize, int pageIndex, string strWhere, string filedOrder, out int recordCount)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" from wx_travel_picture ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }

            recordCount = Convert.ToInt32(DbHelperSQL.GetSingle(PagingHelper.CreateCountingSql(strSql.ToString())));
            return DbHelperSQL.Query(PagingHelper.CreatePagingSql(recordCount, pageSize, pageIndex, strSql.ToString(), filedOrder));
        }
    }
}
