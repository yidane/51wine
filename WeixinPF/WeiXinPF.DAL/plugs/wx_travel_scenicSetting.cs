using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Dapper;
using WeiXinPF.Common;
using WeiXinPF.DBUtility;

namespace WeiXinPF.DAL
{
    public class wx_travel_scenicSetting
    {

        public wx_travel_scenicSetting()
        {
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Model.wx_travel_scenicSetting model)
        {
            using (IDbConnection conn = DbFactory.GetOpenedConnection())
            {
                StringBuilder queryBuilder = new StringBuilder();
                queryBuilder.Append("Insert Into [wx_travel_scenicSetting] (");
                queryBuilder.Append("[ScenicId],[TemplateName],[BackgroundImage],[Audio],[AutoAudio],[LoopAudio],[Video],[AutoVideo])");
                queryBuilder.Append(" Values (");
                queryBuilder.Append("@ScenicId,@TemplateName,@BackgroundImage,@Audio,@AutoAudio,@LoopAudio,@Video,@AutoVideo); ");
                queryBuilder.Append("Select @Id=Scope_Identity()");

                DynamicParameters dpParameters = new DynamicParameters();
                dpParameters.Add("@ScenicId", model.ScenicId);
                dpParameters.Add("@TemplateName", model.TemplateName);
                dpParameters.Add("@BackgroundImage", model.BackgroundImage);
                dpParameters.Add("@Audio", model.Audio);
                dpParameters.Add("@AutoAudio", model.AutoAudio);
                dpParameters.Add("@LoopAudio", model.LoopAudio);
                dpParameters.Add("@Video", model.Video);
                dpParameters.Add("@AutoVideo", model.AutoVideo);
                dpParameters.Add("@Id", dbType: DbType.Int32, direction: ParameterDirection.Output);

                conn.Execute(queryBuilder.ToString(), dpParameters);

                return dpParameters.Get<int>("@Id");
            }
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Model.wx_travel_scenicSetting model)
        {
            using (IDbConnection conn = DbFactory.GetOpenedConnection())
            {
                StringBuilder querBuilder = new StringBuilder();
                querBuilder.Append("Update [dbo].[wx_travel_scenicSetting]");
                querBuilder.Append("Set [ScenicId] = @ScenicId");
                querBuilder.Append(",[TemplateName] = @TemplateName");
                querBuilder.Append(",[BackgroundImage] = @BackgroundImage");
                querBuilder.Append(",[Audio] = @Audio");
                querBuilder.Append(",[AutoAudio] = @AutoAudio");
                querBuilder.Append(",[LoopAudio] = @LoopAudio");
                querBuilder.Append(",[Video] = @Video");
                querBuilder.Append(",[AutoVideo] = @AutoVideo");
                querBuilder.Append("Where Id=@Id");

                int rows = conn.Execute(querBuilder.ToString(), model);
                return rows > 0;
            }
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int id)
        {
            using (IDbConnection conn = DbFactory.GetOpenedConnection())
            {
                StringBuilder querBuilder = new StringBuilder();
                querBuilder.Append("delete from wx_travel_scenicSetting ");
                querBuilder.Append(" where Id=@Id");

                int rows = conn.Execute(querBuilder.ToString(), new { ID = id });
                return rows > 0;
            }
        }

        /// <summary>
        /// 批量删除数据
        /// </summary>
        public bool DeleteList(string idlist)
        {
            using (IDbConnection conn = DbFactory.GetOpenedConnection())
            {
                StringBuilder querBuilder = new StringBuilder();
                querBuilder.Append("delete from wx_travel_scenicSetting ");
                querBuilder.Append(" where Id in (" + idlist + ")  ");
                int rows = conn.Execute(querBuilder.ToString());
                return rows > 0;
            }
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Model.wx_travel_scenicSetting GetModel(int id)
        {
            using (IDbConnection conn = DbFactory.GetOpenedConnection())
            {
                string query = "select * from wx_travel_scenicSetting Where Id=@Id";

                var result = conn.Query<Model.wx_travel_scenicSetting>(query, new { Id = id }).ToList();

                return result.Any() ? result.Single() : null;
            }
        }

        public DataSet GetPageList(int pageSize, int pageIndex, string strWhere, string filedOrder, out int recordCount)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" from wx_travel_scenicSetting ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }

            recordCount = Convert.ToInt32(DbHelperSQL.GetSingle(PagingHelper.CreateCountingSql(strSql.ToString())));
            return DbHelperSQL.Query(PagingHelper.CreatePagingSql(recordCount, pageSize, pageIndex, strSql.ToString(), filedOrder));
        }

        public Model.wx_travel_scenicSetting GetModelByScenicId(int scenicId)
        {
            using (IDbConnection conn = DbFactory.GetOpenedConnection())
            {
                string query = "Select * From dbo.wx_travel_scenicSetting Where ScenicId=@ScenicId";

                var result = conn.Query(query, new { ScenicId = scenicId }).ToList();

                return result.Any() ? result.Single() : null;
            }
        }
    }
}

