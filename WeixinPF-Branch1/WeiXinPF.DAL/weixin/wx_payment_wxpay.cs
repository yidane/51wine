using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using WeiXinPF.DBUtility;//Please add references
namespace WeiXinPF.DAL
{
	/// <summary>
	/// 数据访问类:wx_payment_wxpay
	/// </summary>
	public partial class wx_payment_wxpay
	{
		public wx_payment_wxpay()
		{}
        #region  BasicMethod

        /// <summary>
        /// 得到最大ID
        /// </summary>
        public int GetMaxId()
        {
            return DbHelperSQL.GetMaxID("id", "wx_payment_wxpay");
        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from wx_payment_wxpay");
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
        public int Add(WeiXinPF.Model.wx_payment_wxpay model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into wx_payment_wxpay(");
            strSql.Append("wid,mch_id,paykey,certInfoPath,cerInfoPwd,remark,quicklyFH,createDate)");
            strSql.Append(" values (");
            strSql.Append("@wid,@mch_id,@paykey,@certInfoPath,@cerInfoPwd,@remark,@quicklyFH,@createDate)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@wid", SqlDbType.Int,4),
					new SqlParameter("@mch_id", SqlDbType.VarChar,200),
					new SqlParameter("@paykey", SqlDbType.VarChar,500),
					new SqlParameter("@certInfoPath", SqlDbType.VarChar,1000),
					new SqlParameter("@cerInfoPwd", SqlDbType.VarChar,100),
					new SqlParameter("@remark", SqlDbType.VarChar,2000),
					new SqlParameter("@quicklyFH", SqlDbType.Bit,1),
					new SqlParameter("@createDate", SqlDbType.DateTime)};
            parameters[0].Value = model.wid;
            parameters[1].Value = model.mch_id;
            parameters[2].Value = model.paykey;
            parameters[3].Value = model.certInfoPath;
            parameters[4].Value = model.cerInfoPwd;
            parameters[5].Value = model.remark;
            parameters[6].Value = model.quicklyFH;
            parameters[7].Value = model.createDate;

            object obj = DbHelperSQL.GetSingle(strSql.ToString(), parameters);
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
        /// 更新一条数据
        /// </summary>
        public bool Update(WeiXinPF.Model.wx_payment_wxpay model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update wx_payment_wxpay set ");
            strSql.Append("wid=@wid,");
            strSql.Append("mch_id=@mch_id,");
            strSql.Append("paykey=@paykey,");
            strSql.Append("certInfoPath=@certInfoPath,");
            strSql.Append("cerInfoPwd=@cerInfoPwd,");
            strSql.Append("remark=@remark,");
            strSql.Append("quicklyFH=@quicklyFH,");
            strSql.Append("createDate=@createDate");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
					new SqlParameter("@wid", SqlDbType.Int,4),
					new SqlParameter("@mch_id", SqlDbType.VarChar,200),
					new SqlParameter("@paykey", SqlDbType.VarChar,500),
					new SqlParameter("@certInfoPath", SqlDbType.VarChar,1000),
					new SqlParameter("@cerInfoPwd", SqlDbType.VarChar,100),
					new SqlParameter("@remark", SqlDbType.VarChar,2000),
					new SqlParameter("@quicklyFH", SqlDbType.Bit,1),
					new SqlParameter("@createDate", SqlDbType.DateTime),
					new SqlParameter("@id", SqlDbType.Int,4)};
            parameters[0].Value = model.wid;
            parameters[1].Value = model.mch_id;
            parameters[2].Value = model.paykey;
            parameters[3].Value = model.certInfoPath;
            parameters[4].Value = model.cerInfoPwd;
            parameters[5].Value = model.remark;
            parameters[6].Value = model.quicklyFH;
            parameters[7].Value = model.createDate;
            parameters[8].Value = model.id;

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
        /// 删除一条数据
        /// </summary>
        public bool Delete(int id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from wx_payment_wxpay ");
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
            strSql.Append("delete from wx_payment_wxpay ");
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
        public WeiXinPF.Model.wx_payment_wxpay GetModel(int id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 id,wid,mch_id,paykey,certInfoPath,cerInfoPwd,remark,quicklyFH,createDate from wx_payment_wxpay ");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)
			};
            parameters[0].Value = id;

            WeiXinPF.Model.wx_payment_wxpay model = new WeiXinPF.Model.wx_payment_wxpay();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                return DataRowToModel(ds.Tables[0].Rows[0]);
            }
            else
            {
                return null;
            }
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public WeiXinPF.Model.wx_payment_wxpay DataRowToModel(DataRow row)
        {
            WeiXinPF.Model.wx_payment_wxpay model = new WeiXinPF.Model.wx_payment_wxpay();
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
                if (row["mch_id"] != null)
                {
                    model.mch_id = row["mch_id"].ToString();
                }
                if (row["paykey"] != null)
                {
                    model.paykey = row["paykey"].ToString();
                }
                if (row["certInfoPath"] != null)
                {
                    model.certInfoPath = row["certInfoPath"].ToString();
                }
                if (row["cerInfoPwd"] != null)
                {
                    model.cerInfoPwd = row["cerInfoPwd"].ToString();
                }
                if (row["remark"] != null)
                {
                    model.remark = row["remark"].ToString();
                }
                if (row["quicklyFH"] != null && row["quicklyFH"].ToString() != "")
                {
                    if ((row["quicklyFH"].ToString() == "1") || (row["quicklyFH"].ToString().ToLower() == "true"))
                    {
                        model.quicklyFH = true;
                    }
                    else
                    {
                        model.quicklyFH = false;
                    }
                }
                if (row["createDate"] != null && row["createDate"].ToString() != "")
                {
                    model.createDate = DateTime.Parse(row["createDate"].ToString());
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
            strSql.Append("select id,wid,mch_id,paykey,certInfoPath,cerInfoPwd,remark,quicklyFH,createDate ");
            strSql.Append(" FROM wx_payment_wxpay ");
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
            strSql.Append(" id,wid,mch_id,paykey,certInfoPath,cerInfoPwd,remark,quicklyFH,createDate ");
            strSql.Append(" FROM wx_payment_wxpay ");
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
            strSql.Append("select count(1) FROM wx_payment_wxpay ");
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
            strSql.Append(")AS Row, T.*  from wx_payment_wxpay T ");
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
            parameters[0].Value = "wx_payment_wxpay";
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

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public WeiXinPF.Model.wx_payment_wxpay GetModelByWid(int wid)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 *  from wx_payment_wxpay ");
            strSql.Append(" where wid=@wid");
            SqlParameter[] parameters = {
					new SqlParameter("@wid", SqlDbType.Int,4)
			};
            parameters[0].Value = wid;

            WeiXinPF.Model.wx_payment_wxpay model = new WeiXinPF.Model.wx_payment_wxpay();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                return DataRowToModel(ds.Tables[0].Rows[0]);
            }
            else
            {
                return null;
            }
        }

		#endregion  ExtensionMethod
	}
}

