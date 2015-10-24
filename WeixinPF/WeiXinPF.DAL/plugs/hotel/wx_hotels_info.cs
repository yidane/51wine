using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using WeiXinPF.DBUtility;
using WeiXinPF.Common;//Please add references
using Dapper;
using System.Linq;
using System.Collections.Generic;

namespace WeiXinPF.DAL
{
    /// <summary>
    /// 数据访问类:wx_hotels_info
    /// </summary>
    public partial class wx_hotels_info
    {
        public wx_hotels_info()
        { }
        #region  BasicMethod

        /// <summary>
        /// 得到最大ID
        /// </summary>
        public int GetMaxId()
        {
            return DbHelperSQL.GetMaxID("id", "wx_hotels_info");
        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from wx_hotels_info");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
                    new SqlParameter("@id", SqlDbType.Int,4)
            };
            parameters[0].Value = id;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(WeiXinPF.Model.wx_hotels_info model)
        {
            StringBuilder query = new StringBuilder();
            query.Append("Insert Into[dbo].[wx_hotels_info]");
            query.Append("     ([wid],[hotelName],[hotelAddress],[hotelPhone],[mobilPhone],[noticeEmail],[emailPws],[smtp],[coverPic],[topPic],[orderLimit],[listMode],[messageNotice],[pwd],[hotelIntroduct],[orderRemark],[createDate],[sortid],[xplace],[yplace],[HotelCode],[Operator],[HotelLevel],[Recommend])");
            query.Append(" Values");
            query.Append("     (@wid, @hotelName, @hotelAddress, @hotelPhone, @mobilPhone, @noticeEmail, @emailPws, @smtp, @coverPic, @topPic, @orderLimit, @listMode, @messageNotice, @pwd, @hotelIntroduct, @orderRemark, @createDate, @sortid, @xplace, @yplace, @HotelCode, @Operator, @HotelLevel,@Recommend);");
            query.Append("Select @Id = Scope_Identity();");
            query.Append("Update dbo.wx_hotels_info Set HotelCode=Right('0000'+Cast(@Id As Varchar(10)),4) Where Id=@Id");
            using (IDbConnection db = DbFactory.GetOpenedConnection())
            {
                DynamicParameters dynamicParameters = new DynamicParameters();
                dynamicParameters.AddDynamicParams(model);
                dynamicParameters.Add("@Id", dbType: DbType.Int32, direction: ParameterDirection.Output);

                db.Execute(query.ToString(), dynamicParameters);

                return dynamicParameters.Get<int>("@Id");
            }
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(WeiXinPF.Model.wx_hotels_info model)
        {
            StringBuilder query = new StringBuilder();
            query.Append(" Update[dbo].[wx_hotels_info]");
            query.Append("   Set[wid] = @wid");
            query.Append("     ,[hotelName] = @hotelName");
            query.Append("     ,[hotelAddress] = @hotelAddress");
            query.Append("     ,[hotelPhone] = @hotelPhone");
            query.Append("     ,[mobilPhone] = @mobilPhone");
            query.Append("     ,[noticeEmail] = @noticeEmail");
            query.Append("     ,[emailPws] = @emailPws");
            query.Append("     ,[smtp] = @smtp");
            query.Append("     ,[coverPic] = @coverPic");
            query.Append("     ,[topPic] = @topPic");
            query.Append("     ,[orderLimit] = @orderLimit");
            query.Append("     ,[listMode] = @listMode");
            query.Append("     ,[messageNotice] = @messageNotice");
            query.Append("     ,[pwd] = @pwd");
            query.Append("     ,[hotelIntroduct] = @hotelIntroduct");
            query.Append("     ,[orderRemark] = @orderRemark");
            query.Append("     ,[createDate] = @createDate");
            query.Append("     ,[sortid] = @sortid");
            query.Append("     ,[xplace] = @xplace");
            query.Append("     ,[yplace] = @yplace");
            query.Append("     ,[HotelCode] = @HotelCode");
            query.Append("     ,[Operator] = @Operator");
            query.Append("     ,[HotelLevel] = @HotelLevel");
            query.Append("     ,[Recommend]=@Recommend");
            query.Append(" Where id=@Id");

            using (IDbConnection db = DbFactory.GetOpenedConnection())
            {
                return db.Execute(query.ToString(), model) > 0;
            }
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from wx_hotels_info ");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
                    new SqlParameter("@id", SqlDbType.Int,4)
            };
            parameters[0].Value = id;

            int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 批量删除数据
        /// </summary>
        public bool DeleteList(string idlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from wx_hotels_info ");
            strSql.Append(" where id in (" + idlist + ")  ");
            int rows = DbHelperSQL.ExecuteSql(strSql.ToString());
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public WeiXinPF.Model.wx_hotels_info GetModel(int id)
        {
            using (IDbConnection db = DbFactory.GetOpenedConnection())
            {
                string query = "Select * from wx_hotels_info where id=@Id ";
                return db.Query<Model.wx_hotels_info>(query, new { Id = id }).FirstOrDefault();
            }

            //         StringBuilder strSql = new StringBuilder();
            //         strSql.Append("select  top 1 * from wx_hotels_info ");
            //         strSql.Append(" where id=@id");
            //         SqlParameter[] parameters = {
            //		new SqlParameter("@id", SqlDbType.Int,4)
            //};
            //         parameters[0].Value = id;

            //         WeiXinPF.Model.wx_hotels_info model = new WeiXinPF.Model.wx_hotels_info();
            //         DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            //         if (ds.Tables[0].Rows.Count > 0)
            //         {
            //             return DataRowToModel(ds.Tables[0].Rows[0]);
            //         }
            //         else
            //         {
            //             return null;
            //         }
        }

        public List<Model.wx_hotels_info> GetModelList(string strWhere)
        {
            string query = "Select * from wx_hotels_info";
            if (!string.IsNullOrEmpty(strWhere))
            {
                query = query + " Where " + strWhere;
            }
            using (IDbConnection db = DbFactory.GetOpenedConnection())
            {
                return db.Query<Model.wx_hotels_info>(query).ToList();
            }
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public WeiXinPF.Model.wx_hotels_info DataRowToModel(DataRow row)
        {
            WeiXinPF.Model.wx_hotels_info model = new WeiXinPF.Model.wx_hotels_info();
            if (row != null)
            {
                if (row["id"] != null && row["id"].ToString() != "")
                {
                    model.id = int.Parse(row["id"].ToString());
                }
                if (row["wid"] != null && row["wid"].ToString() != "")
                {
                    model.wid = int.Parse(row["wid"].ToString());
                }
                if (row["hotelName"] != null)
                {
                    model.hotelName = row["hotelName"].ToString();
                }
                if (row["hotelAddress"] != null)
                {
                    model.hotelAddress = row["hotelAddress"].ToString();
                }
                if (row["hotelPhone"] != null)
                {
                    model.hotelPhone = row["hotelPhone"].ToString();
                }
                if (row["mobilPhone"] != null)
                {
                    model.mobilPhone = row["mobilPhone"].ToString();
                }
                if (row["noticeEmail"] != null)
                {
                    model.noticeEmail = row["noticeEmail"].ToString();
                }
                if (row["emailPws"] != null)
                {
                    model.emailPws = row["emailPws"].ToString();
                }
                if (row["smtp"] != null)
                {
                    model.smtp = row["smtp"].ToString();
                }
                if (row["coverPic"] != null)
                {
                    model.coverPic = row["coverPic"].ToString();
                }
                if (row["topPic"] != null)
                {
                    model.topPic = row["topPic"].ToString();
                }
                if (row["orderLimit"] != null && row["orderLimit"].ToString() != "")
                {
                    model.orderLimit = int.Parse(row["orderLimit"].ToString());
                }
                if (row["listMode"] != null && row["listMode"].ToString() != "")
                {
                    if ((row["listMode"].ToString() == "1") || (row["listMode"].ToString().ToLower() == "true"))
                    {
                        model.listMode = true;
                    }
                    else
                    {
                        model.listMode = false;
                    }
                }
                if (row["messageNotice"] != null && row["messageNotice"].ToString() != "")
                {
                    model.messageNotice = int.Parse(row["messageNotice"].ToString());
                }
                if (row["pwd"] != null)
                {
                    model.pwd = row["pwd"].ToString();
                }
                if (row["hotelIntroduct"] != null)
                {
                    model.hotelIntroduct = row["hotelIntroduct"].ToString();
                }
                if (row["orderRemark"] != null)
                {
                    model.orderRemark = row["orderRemark"].ToString();
                }
                if (row["createDate"] != null && row["createDate"].ToString() != "")
                {
                    model.createDate = DateTime.Parse(row["createDate"].ToString());
                }
                if (row["sortid"] != null && row["sortid"].ToString() != "")
                {
                    model.sortid = int.Parse(row["sortid"].ToString());
                }
                if (row["xplace"] != null && row["xplace"].ToString() != "")
                {
                    model.xplace = decimal.Parse(row["xplace"].ToString());
                }
                if (row["yplace"] != null && row["yplace"].ToString() != "")
                {
                    model.yplace = decimal.Parse(row["yplace"].ToString());
                }
            }
            return model;
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM wx_hotels_info ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            if (Top > 0)
            {
                strSql.Append(" top " + Top.ToString());
            }
            strSql.Append(" * ");
            strSql.Append(" FROM wx_hotels_info ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 获取记录总数
        /// </summary>
        public int GetRecordCount(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) FROM wx_hotels_info ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            object obj = DbHelperSQL.GetSingle(strSql.ToString());
            if (obj == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM ( ");
            strSql.Append(" SELECT ROW_NUMBER() OVER (");
            if (!string.IsNullOrEmpty(orderby.Trim()))
            {
                strSql.Append("order by T." + orderby);
            }
            else
            {
                strSql.Append("order by T.id desc");
            }
            strSql.Append(")AS Row, T.*  from wx_hotels_info T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                strSql.Append(" WHERE " + strWhere);
            }
            strSql.Append(" ) TT");
            strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(strSql.ToString());
        }

        /*
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public DataSet GetList(int PageSize,int PageIndex,string strWhere)
        {
            SqlParameter[] parameters = {
                    new SqlParameter("@tblName", SqlDbType.VarChar, 255),
                    new SqlParameter("@fldName", SqlDbType.VarChar, 255),
                    new SqlParameter("@PageSize", SqlDbType.Int),
                    new SqlParameter("@PageIndex", SqlDbType.Int),
                    new SqlParameter("@IsReCount", SqlDbType.Bit),
                    new SqlParameter("@OrderType", SqlDbType.Bit),
                    new SqlParameter("@strWhere", SqlDbType.VarChar,1000),
                    };
            parameters[0].Value = "wx_hotels_info";
            parameters[1].Value = "id";
            parameters[2].Value = PageSize;
            parameters[3].Value = PageIndex;
            parameters[4].Value = 0;
            parameters[5].Value = 0;
            parameters[6].Value = strWhere;	
            return DbHelperSQL.RunProcedure("UP_GetRecordByPage",parameters,"ds");
        }*/

        #endregion  BasicMethod
        #region  ExtensionMethod
        public DataSet GetList(int pageSize, int pageIndex, string strWhere, string filedOrder, out int recordCount)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM wx_hotels_info ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }

            recordCount = Convert.ToInt32(DbHelperSQL.GetSingle(PagingHelper.CreateCountingSql(strSql.ToString())));
            return DbHelperSQL.Query(PagingHelper.CreatePagingSql(recordCount, pageSize, pageIndex, strSql.ToString(), filedOrder));
        }

        public DataSet GetList()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM wx_hotels_info ");

            return DbHelperSQL.Query(strSql.ToString());
        }

        #endregion  ExtensionMethod
    }
}

