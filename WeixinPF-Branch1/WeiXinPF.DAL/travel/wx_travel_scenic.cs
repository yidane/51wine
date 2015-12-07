using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Dapper;
using WeiXinPF.Common;
using WeiXinPF.DBUtility;
using WeiXinPF.Model;

namespace WeiXinPF.DAL
{
    public class wx_travel_scenic
    {
        public wx_travel_scenic()
        {
        }
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Model.wx_travel_scenic model)
        {
            using (IDbConnection db = DbFactory.GetOpenedConnection())
            {
                string query = @"Insert Into dbo.wx_travel_scenic
                                    (wid,Name,Description,TemplateId,FirstBgImg,IdentifyImg,DisplayAction,SecondBgImg,AutoDisplayNextPage,Delay,AudioPath,AutoPlayAudio,AudioLoop,VideoPath,AutoPlayVideo,extInt1,extInt2,extStr1,extStr2)
                                 Values
                                    (@wid,@Name,@Description,@TemplateId,@FirstBgImg,@IdentifyImg,@DisplayAction,@SecondBgImg,@AutoDisplayNextPage,@Delay,@AudioPath,@AutoPlayAudio,@AudioLoop,@VideoPath,@AutoPlayVideo,@extInt1,@extInt2,@extStr1,@extStr2)
                                Select @Id=Scope_Identity()
                                ";
                DynamicParameters dynamicParameters = new DynamicParameters();
                dynamicParameters.AddDynamicParams(model);
                dynamicParameters.Add("@Id", dbType: DbType.Int32, direction: ParameterDirection.Output);

                db.Execute(query, dynamicParameters);

                return dynamicParameters.Get<int>("@Id");
            }
        }

        public List<Model.wx_travel_scenic> GetModelList(string strWhere)
        {
            if (!string.IsNullOrEmpty(strWhere))
            {
                strWhere = " Where " + strWhere;
            }

            using (IDbConnection db = DbFactory.GetOpenedConnection())
            {
                return db.Query<Model.wx_travel_scenic>("Select * From wx_travel_scenic " + strWhere).ToList();
            }
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Model.wx_travel_scenic model)
        {
            using (IDbConnection db = DbFactory.GetOpenedConnection())
            {
                StringBuilder queryBuilder = new StringBuilder();
                queryBuilder.Append("Update[dbo].[wx_travel_scenic] ");
                queryBuilder.Append("Set [wid] = @wid");
                queryBuilder.Append(",[Name] = @Name");
                queryBuilder.Append(",[Description] = @Description");
                queryBuilder.Append(",[TemplateId] = @TemplateId");
                queryBuilder.Append(",[FirstBgImg] = @FirstBgImg");
                queryBuilder.Append(",[IdentifyImg] = @IdentifyImg");
                queryBuilder.Append(",[DisplayAction] = @DisplayAction");
                queryBuilder.Append(",[SecondBgImg] = @SecondBgImg");
                queryBuilder.Append(",[AutoDisplayNextPage] = @AutoDisplayNextPage");
                queryBuilder.Append(",[Delay] = @Delay");
                queryBuilder.Append(",[AudioPath] = @AudioPath");
                queryBuilder.Append(",[AutoPlayAudio] = @AutoPlayAudio");
                queryBuilder.Append(",[AudioLoop] = @AudioLoop");
                queryBuilder.Append(",[VideoPath] = @VideoPath");
                queryBuilder.Append(",[AutoPlayVideo] = @AutoPlayVideo");
                queryBuilder.Append(",[extInt1] = @extInt1");
                queryBuilder.Append(",[extInt2] = @extInt2");
                queryBuilder.Append(",[extStr1] = @extStr1");
                queryBuilder.Append(",[extStr2] = @extStr2");
                queryBuilder.Append(" Where Id=@Id");

                return db.Execute(queryBuilder.ToString(), model) > 0;
            }
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int id)
        {
            using (IDbConnection db = DbFactory.GetOpenedConnection())
            {
                string query = "delete from wx_travel_scenic where Id=@Id";

                return db.Execute(query, new { Id = id }) > 0;
            }
        }

        /// <summary>
        /// 批量删除数据
        /// </summary>
        public bool DeleteList(int[] idlist)
        {
            using (IDbConnection db = DbFactory.GetOpenedConnection())
            {
                string query = "delete from wx_travel_scenic where Id in @Ids";
                return db.Execute(query, new { Ids = idlist }) > 0;
            }
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Model.wx_travel_scenic GetModel(int id)
        {
            using (IDbConnection db = DbFactory.GetOpenedConnection())
            {
                string query = "select * from wx_travel_scenic Where Id=@Id";

                var result = db.Query<Model.wx_travel_scenic>(query, new { Id = id }).ToList();

                return result.Any() ? result.SingleOrDefault() : null;
            }
        }

        public DataSet GetPageList(int pageSize, int pageIndex, string strWhere, string filedOrder, out int recordCount)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" from wx_travel_scenic ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }

            recordCount = Convert.ToInt32(DbHelperSQL.GetSingle(PagingHelper.CreateCountingSql(strSql.ToString())));
            return DbHelperSQL.Query(PagingHelper.CreatePagingSql(recordCount, pageSize, pageIndex, strSql.ToString(), filedOrder));
        }

    }
}
