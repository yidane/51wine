﻿using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using WeiXinPF.DBUtility;//Please add references
namespace WeiXinPF.DAL
{
	/// <summary>
	/// 数据访问类:wx_crm_setting
	/// </summary>
	public partial class wx_crm_setting
	{
		public wx_crm_setting()
		{}
		#region  BasicMethod

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
		return DbHelperSQL.GetMaxID("id", "wx_crm_setting"); 
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int id)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from wx_crm_setting");
			strSql.Append(" where id=@id");
			SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)
			};
			parameters[0].Value = id;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(WeiXinPF.Model.wx_crm_setting model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into wx_crm_setting(");
			strSql.Append("wid,groupCount,openidCount,groupSynDate,personSynDate)");
			strSql.Append(" values (");
			strSql.Append("@wid,@groupCount,@openidCount,@groupSynDate,@personSynDate)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@wid", SqlDbType.Int,4),
					new SqlParameter("@groupCount", SqlDbType.Int,4),
					new SqlParameter("@openidCount", SqlDbType.Int,4),
					new SqlParameter("@groupSynDate", SqlDbType.DateTime),
					new SqlParameter("@personSynDate", SqlDbType.DateTime)};
			parameters[0].Value = model.wid;
			parameters[1].Value = model.groupCount;
			parameters[2].Value = model.openidCount;
			parameters[3].Value = model.groupSynDate;
			parameters[4].Value = model.personSynDate;

			object obj = DbHelperSQL.GetSingle(strSql.ToString(),parameters);
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
		public bool Update(WeiXinPF.Model.wx_crm_setting model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update wx_crm_setting set ");
			strSql.Append("wid=@wid,");
			strSql.Append("groupCount=@groupCount,");
			strSql.Append("openidCount=@openidCount,");
			strSql.Append("groupSynDate=@groupSynDate,");
			strSql.Append("personSynDate=@personSynDate");
			strSql.Append(" where id=@id");
			SqlParameter[] parameters = {
					new SqlParameter("@wid", SqlDbType.Int,4),
					new SqlParameter("@groupCount", SqlDbType.Int,4),
					new SqlParameter("@openidCount", SqlDbType.Int,4),
					new SqlParameter("@groupSynDate", SqlDbType.DateTime),
					new SqlParameter("@personSynDate", SqlDbType.DateTime),
					new SqlParameter("@id", SqlDbType.Int,4)};
			parameters[0].Value = model.wid;
			parameters[1].Value = model.groupCount;
			parameters[2].Value = model.openidCount;
			parameters[3].Value = model.groupSynDate;
			parameters[4].Value = model.personSynDate;
			parameters[5].Value = model.id;

			int rows=DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
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
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from wx_crm_setting ");
			strSql.Append(" where id=@id");
			SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)
			};
			parameters[0].Value = id;

			int rows=DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
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
		public bool DeleteList(string idlist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from wx_crm_setting ");
			strSql.Append(" where id in ("+idlist + ")  ");
			int rows=DbHelperSQL.ExecuteSql(strSql.ToString());
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
		public WeiXinPF.Model.wx_crm_setting GetModel(int id)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 id,wid,groupCount,openidCount,groupSynDate,personSynDate from wx_crm_setting ");
			strSql.Append(" where id=@id");
			SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)
			};
			parameters[0].Value = id;

			WeiXinPF.Model.wx_crm_setting model=new WeiXinPF.Model.wx_crm_setting();
			DataSet ds=DbHelperSQL.Query(strSql.ToString(),parameters);
			if(ds.Tables[0].Rows.Count>0)
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
		public WeiXinPF.Model.wx_crm_setting DataRowToModel(DataRow row)
		{
			WeiXinPF.Model.wx_crm_setting model=new WeiXinPF.Model.wx_crm_setting();
			if (row != null)
			{
				if(row["id"]!=null && row["id"].ToString()!="")
				{
					model.id=int.Parse(row["id"].ToString());
				}
				if(row["wid"]!=null && row["wid"].ToString()!="")
				{
					model.wid=int.Parse(row["wid"].ToString());
				}
				if(row["groupCount"]!=null && row["groupCount"].ToString()!="")
				{
					model.groupCount=int.Parse(row["groupCount"].ToString());
				}
				if(row["openidCount"]!=null && row["openidCount"].ToString()!="")
				{
					model.openidCount=int.Parse(row["openidCount"].ToString());
				}
				if(row["groupSynDate"]!=null && row["groupSynDate"].ToString()!="")
				{
					model.groupSynDate=DateTime.Parse(row["groupSynDate"].ToString());
				}
				if(row["personSynDate"]!=null && row["personSynDate"].ToString()!="")
				{
					model.personSynDate=DateTime.Parse(row["personSynDate"].ToString());
				}
			}
			return model;
		}

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetList(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select id,wid,groupCount,openidCount,groupSynDate,personSynDate ");
			strSql.Append(" FROM wx_crm_setting ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			return DbHelperSQL.Query(strSql.ToString());
		}

		/// <summary>
		/// 获得前几行数据
		/// </summary>
		public DataSet GetList(int Top,string strWhere,string filedOrder)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select ");
			if(Top>0)
			{
				strSql.Append(" top "+Top.ToString());
			}
			strSql.Append(" id,wid,groupCount,openidCount,groupSynDate,personSynDate ");
			strSql.Append(" FROM wx_crm_setting ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			strSql.Append(" order by " + filedOrder);
			return DbHelperSQL.Query(strSql.ToString());
		}

		/// <summary>
		/// 获取记录总数
		/// </summary>
		public int GetRecordCount(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) FROM wx_crm_setting ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
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
			StringBuilder strSql=new StringBuilder();
			strSql.Append("SELECT * FROM ( ");
			strSql.Append(" SELECT ROW_NUMBER() OVER (");
			if (!string.IsNullOrEmpty(orderby.Trim()))
			{
				strSql.Append("order by T." + orderby );
			}
			else
			{
				strSql.Append("order by T.id desc");
			}
			strSql.Append(")AS Row, T.*  from wx_crm_setting T ");
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

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool ExistsWid(int wid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from wx_crm_setting");
            strSql.Append(" where wid=@wid");
            SqlParameter[] parameters = {
					new SqlParameter("@wid", SqlDbType.Int,4)
			};
            parameters[0].Value = wid;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }

        

        /// <summary>
        /// 修改一列数据
        /// </summary>
        public void UpdateField(int id, string strValue)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update wx_crm_setting  set " + strValue);
            strSql.Append(" where id=" + id);
            DbHelperSQL.ExecuteSql(strSql.ToString());
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public WeiXinPF.Model.wx_crm_setting GetModelByWid(int wid)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 id,wid,groupCount,openidCount,groupSynDate,personSynDate from wx_crm_setting ");
            strSql.Append(" where wid=@wid");
            SqlParameter[] parameters = {
					new SqlParameter("@wid", SqlDbType.Int,4)
			};
            parameters[0].Value = wid;

            WeiXinPF.Model.wx_crm_setting model = new WeiXinPF.Model.wx_crm_setting();
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
