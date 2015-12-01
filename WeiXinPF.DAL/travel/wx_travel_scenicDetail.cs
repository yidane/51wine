using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Dapper;
using WeiXinPF.Common;
using WeiXinPF.DBUtility;

namespace WeiXinPF.DAL
{
    public class wx_travel_scenicDetail
    {
        public wx_travel_scenicDetail()
        {
        }

        public int Add(Model.wx_travel_scenicDetail model)
        {
            using (IDbConnection db = DbFactory.GetOpenedConnection())
            {

                string query = @"Insert Into dbo.wx_travel_scenicDetail
                                     (ScenicId,Name,Cover,BackgroundImage,Digest,Content,Audio,AutoAudio,LoopAudio,Video,AutoVideo,OriginalLink)
                                 Values  
                                     (@ScenicId,@Name,@Cover,@BackgroundImage,@Digest,@Content,@Audio,@AutoAudio,@LoopAudio,@Video,@AutoVideo,@OriginalLink)
                                 Select @Id=Scope_Identity()
                                   ";
                DynamicParameters dynamicParameters = new DynamicParameters();
                dynamicParameters.AddDynamicParams(model);
                dynamicParameters.Add("@Id", dbType: DbType.Int32, direction: ParameterDirection.Output);
                db.Execute(query, dynamicParameters);

                return dynamicParameters.Get<int>("@Id");
            }
        }

        public bool Update(Model.wx_travel_scenicDetail model)
        {
            StringBuilder queryBuilder = new StringBuilder();
            queryBuilder.Append("Update [dbo].[wx_travel_scenicDetail]");
            queryBuilder.Append(" Set [ScenicId] = @ScenicId");
            queryBuilder.Append(",[Name] = @Name");
            queryBuilder.Append(",[Cover] = @Cover");
            queryBuilder.Append(",[BackgroundImage] = @BackgroundImage");
            queryBuilder.Append(",[Digest] = @Digest");
            queryBuilder.Append(",[Content] = @Content");
            queryBuilder.Append(",[Audio] = @Audio");
            queryBuilder.Append(",[AutoAudio] = @AutoAudio");
            queryBuilder.Append(",[LoopAudio] = @LoopAudio");
            queryBuilder.Append(",[Video] = @Video");
            queryBuilder.Append(",[AutoVideo] = @AutoVideo");
            queryBuilder.Append(",[OriginalLink] = @OriginalLink");
            queryBuilder.Append(" Where Id=@Id");

            using (IDbConnection db = DbFactory.GetOpenedConnection())
            {
                return db.Execute(queryBuilder.ToString(), model) > 0;
            }
        }

        public bool Delete(int id)
        {
            string query = "Delete wx_travel_scenicDetail Where Id=@Id";
            using (IDbConnection db = DbFactory.GetOpenedConnection())
            {
                return db.Execute(query, new { Id = id }) > 0;
            }
        }

        public Model.wx_travel_scenicDetail GetModel(int id)
        {
            using (IDbConnection db = DbFactory.GetOpenedConnection())
            {
                string query = @"Select * From dbo.wx_travel_scenicDetail Where Id=@Id";

                return db.Query<Model.wx_travel_scenicDetail>(query, new { Id = id }).FirstOrDefault();
            }
        }

        public DataSet GetPageList(int pageSize, int pageIndex, string strWhere, string filedOrder, out int recordCount)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" from wx_travel_scenicDetail ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }

            recordCount = Convert.ToInt32(DbHelperSQL.GetSingle(PagingHelper.CreateCountingSql(strSql.ToString())));
            return DbHelperSQL.Query(PagingHelper.CreatePagingSql(recordCount, pageSize, pageIndex, strSql.ToString(), filedOrder));
        }

        public List<Model.wx_travel_scenicDetail> GetModelByScenicId(int scenicId)
        {
            using (IDbConnection db = DbFactory.GetOpenedConnection())
            {
                string query = @"Select * From dbo.wx_travel_scenicDetail Where ScenicId=@ScenicId";
                return db.Query<Model.wx_travel_scenicDetail>(query, new { ScenicId = scenicId }).ToList();
            }
        }

        public List<Model.wx_travel_scenicDetail> GetModelByWid(int wid)
        {
            using (IDbConnection db = DbFactory.GetOpenedConnection())
            {
                string query = @"Select b.* From dbo.wx_travel_scenic a Inner Join dbo.wx_travel_scenicDetail b On a.Id=b.ScenicId Where a.wid=@wid";
                return db.Query<Model.wx_travel_scenicDetail>(query, new { wid = wid }).ToList();
            }
        } 
    }
}
