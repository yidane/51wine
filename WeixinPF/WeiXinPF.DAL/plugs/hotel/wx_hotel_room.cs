using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using WeiXinPF.DBUtility;
using WeiXinPF.Common;//Please add references
using Dapper;
using System.Linq;
using System.Collections.Generic;
using WeiXinPF.Model;

namespace WeiXinPF.DAL
{
    /// <summary>
    /// 数据访问类:wx_hotel_room
    /// </summary>
    public partial class wx_hotel_room
    {
        public wx_hotel_room()
        { }
        #region  BasicMethod

        /// <summary>
        /// 得到最大ID
        /// </summary>
        public int GetMaxId()
        {
            return DbHelperSQL.GetMaxID("id", "wx_hotel_room");
        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from wx_hotel_room");
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
        public int Add(WeiXinPF.Model.wx_hotel_room model)
        {
            StringBuilder query = new StringBuilder();
            query.Append("Insert Into[dbo].[wx_hotel_room]");
            query.Append("([hotelid],[roomType],[indroduce],[roomPrice],[salePrice],[facilities],[createDate],[sortid],[RoomCode],[UseInstruction],[RefundRule],[Status],[ExpiryDate_Begin],[ExpiryDate_End])");
            query.Append("Values");
            query.Append("(@hotelid, @roomType, @indroduce, @roomPrice, @salePrice, @facilities, @createDate, @sortid, @RoomCode, @UseInstruction, @RefundRule, @Status,@ExpiryDate_Begin,@ExpiryDate_End);");
            query.Append("Select @Id = Scope_Identity()");
            //query.Append("Update dbo.wx_hotel_room Set RoomCode=Right('0000'+Cast(@Id As Varchar(10)),4) Where Id=@Id");
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
        public bool Update(WeiXinPF.Model.wx_hotel_room model)
        {
            StringBuilder query = new StringBuilder();
            query.Append("Update[dbo].[wx_hotel_room]");
            query.Append("     Set[hotelid] = @hotelid");
            query.Append("       ,[roomType] = @roomType");
            query.Append("       ,[indroduce] = @indroduce");
            query.Append("       ,[roomPrice] = @roomPrice");
            query.Append("       ,[salePrice] = @salePrice");
            query.Append("       ,[facilities] = @facilities");
            query.Append("       ,[createDate] = @createDate");
            query.Append("       ,[sortid] = @sortid");
            query.Append("       ,[RoomCode] = @RoomCode");
            query.Append("       ,[UseInstruction] = @UseInstruction");
            query.Append("       ,[RefundRule] = @RefundRule");
            query.Append("       ,[Status] = @Status");
            query.Append("       ,[ExpiryDate_Begin] = @ExpiryDate_Begin");
            query.Append("       ,[ExpiryDate_End] = @ExpiryDate_End");
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
            strSql.Append("delete from wx_hotel_room ");
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
            strSql.Append("delete from wx_hotel_room ");
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
        public WeiXinPF.Model.wx_hotel_room GetModel(int id)
        {
            string query = "Select * From wx_hotel_room Where id=@Id";
            using (IDbConnection db = DbFactory.GetOpenedConnection())
            {
                return db.Query<Model.wx_hotel_room>(query, new { Id = id }).FirstOrDefault();
            }
        }

        public List<Model.wx_hotel_room> GetModelList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * FROM wx_hotel_room ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" Where " + strWhere);
            }

            using (IDbConnection db = DbFactory.GetOpenedConnection())
            {
                return db.Query<Model.wx_hotel_room>(strSql.ToString()).ToList();
            }
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public WeiXinPF.Model.wx_hotel_room DataRowToModel(DataRow row)
        {
            WeiXinPF.Model.wx_hotel_room model = new WeiXinPF.Model.wx_hotel_room();
            if (row != null)
            {
                if (row["id"] != null && row["id"].ToString() != "")
                {
                    model.id = int.Parse(row["id"].ToString());
                }
                if (row["hotelid"] != null && row["hotelid"].ToString() != "")
                {
                    model.hotelid = int.Parse(row["hotelid"].ToString());
                }
                if (row["roomType"] != null)
                {
                    model.roomType = row["roomType"].ToString();
                }
                if (row["indroduce"] != null)
                {
                    model.indroduce = row["indroduce"].ToString();
                }
                if (row["roomPrice"] != null && row["roomPrice"].ToString() != "")
                {
                    model.roomPrice = decimal.Parse(row["roomPrice"].ToString());
                }
                if (row["salePrice"] != null && row["salePrice"].ToString() != "")
                {
                    model.salePrice = decimal.Parse(row["salePrice"].ToString());
                }
                if (row["facilities"] != null)
                {
                    model.facilities = row["facilities"].ToString();
                }
                if (row["createDate"] != null && row["createDate"].ToString() != "")
                {
                    model.createDate = DateTime.Parse(row["createDate"].ToString());
                }
                if (row["sortid"] != null && row["sortid"].ToString() != "")
                {
                    model.sortid = int.Parse(row["sortid"].ToString());
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
            strSql.Append("select *");
            strSql.Append(" FROM wx_hotel_room ");
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
            strSql.Append(" FROM wx_hotel_room ");
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
            strSql.Append("select count(1) FROM wx_hotel_room ");
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
            strSql.Append(")AS Row, T.*  from wx_hotel_room T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                strSql.Append(" WHERE " + strWhere);
            }
            strSql.Append(" ) TT");
            strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(strSql.ToString());
        }



        #endregion  BasicMethod
        #region  ExtensionMethod
        public DataSet GetList(int pageSize, int pageIndex, string strWhere, string filedOrder, out int recordCount)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM wx_hotel_room ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }

            recordCount = Convert.ToInt32(DbHelperSQL.GetSingle(PagingHelper.CreateCountingSql(strSql.ToString())));
            return DbHelperSQL.Query(PagingHelper.CreatePagingSql(recordCount, pageSize, pageIndex, strSql.ToString(), filedOrder));
        }

        public DataSet GetList(int hotelid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM wx_hotel_room  where hotelid='" + hotelid + "'  order by  createDate desc,id desc  ");

            return DbHelperSQL.Query(strSql.ToString());
        }

        public string GetRoomCode(int hotelid)
        {

            SqlParameter[] parameters = {
                    new SqlParameter("@HotelId", SqlDbType.Int)
            };
            parameters[0].Value = hotelid;

            SqlDataReader sr = DbHelperSQL.RunProcedure("usp_hotel_room_getCode", parameters);
            sr.Read();
            string roomCode = sr[0].ToString();
            sr.Close();
            return roomCode;
        }
        #endregion  ExtensionMethod
    }
}

