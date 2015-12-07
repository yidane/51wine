using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using WeiXinPF.DBUtility;
using WeiXinPF.Common;//Please add references
namespace WeiXinPF.DAL
{
	/// <summary>
	/// 数据访问类:wx_zh_zs
	/// </summary>
	public partial class wx_zh_zs
	{
		public wx_zh_zs()
		{}
		#region  BasicMethod

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
		return DbHelperSQL.GetMaxID("id", "wx_zh_zs"); 
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int id)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from wx_zh_zs");
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
		public int Add(WeiXinPF.Model.wx_zh_zs model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into wx_zh_zs(");
			strSql.Append("company_name,linkman,tel,mobile,fax,email,qq,type,sq_area,createdate,sort_id)");
			strSql.Append(" values (");
			strSql.Append("@company_name,@linkman,@tel,@mobile,@fax,@email,@qq,@type,@sq_area,@createdate,@sort_id)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@company_name", SqlDbType.NVarChar,200),
					new SqlParameter("@linkman", SqlDbType.NVarChar,200),
					new SqlParameter("@tel", SqlDbType.NVarChar,200),
					new SqlParameter("@mobile", SqlDbType.NVarChar,200),
					new SqlParameter("@fax", SqlDbType.NVarChar,200),
					new SqlParameter("@email", SqlDbType.NVarChar,300),
					new SqlParameter("@qq", SqlDbType.NVarChar,200),
					new SqlParameter("@type", SqlDbType.NVarChar,400),
					new SqlParameter("@sq_area", SqlDbType.NVarChar,400),
					new SqlParameter("@createdate", SqlDbType.DateTime),
					new SqlParameter("@sort_id", SqlDbType.Int,4)};
			parameters[0].Value = model.company_name;
			parameters[1].Value = model.linkman;
			parameters[2].Value = model.tel;
			parameters[3].Value = model.mobile;
			parameters[4].Value = model.fax;
			parameters[5].Value = model.email;
			parameters[6].Value = model.qq;
			parameters[7].Value = model.type;
			parameters[8].Value = model.sq_area;
			parameters[9].Value = model.createdate;
			parameters[10].Value = model.sort_id;

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
		public bool Update(WeiXinPF.Model.wx_zh_zs model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update wx_zh_zs set ");
			strSql.Append("company_name=@company_name,");
			strSql.Append("linkman=@linkman,");
			strSql.Append("tel=@tel,");
			strSql.Append("mobile=@mobile,");
			strSql.Append("fax=@fax,");
			strSql.Append("email=@email,");
			strSql.Append("qq=@qq,");
			strSql.Append("type=@type,");
			strSql.Append("sq_area=@sq_area,");
			strSql.Append("createdate=@createdate,");
			strSql.Append("sort_id=@sort_id");
			strSql.Append(" where id=@id");
			SqlParameter[] parameters = {
					new SqlParameter("@company_name", SqlDbType.NVarChar,200),
					new SqlParameter("@linkman", SqlDbType.NVarChar,200),
					new SqlParameter("@tel", SqlDbType.NVarChar,200),
					new SqlParameter("@mobile", SqlDbType.NVarChar,200),
					new SqlParameter("@fax", SqlDbType.NVarChar,200),
					new SqlParameter("@email", SqlDbType.NVarChar,300),
					new SqlParameter("@qq", SqlDbType.NVarChar,200),
					new SqlParameter("@type", SqlDbType.NVarChar,400),
					new SqlParameter("@sq_area", SqlDbType.NVarChar,400),
					new SqlParameter("@createdate", SqlDbType.DateTime),
					new SqlParameter("@sort_id", SqlDbType.Int,4),
					new SqlParameter("@id", SqlDbType.Int,4)};
			parameters[0].Value = model.company_name;
			parameters[1].Value = model.linkman;
			parameters[2].Value = model.tel;
			parameters[3].Value = model.mobile;
			parameters[4].Value = model.fax;
			parameters[5].Value = model.email;
			parameters[6].Value = model.qq;
			parameters[7].Value = model.type;
			parameters[8].Value = model.sq_area;
			parameters[9].Value = model.createdate;
			parameters[10].Value = model.sort_id;
			parameters[11].Value = model.id;

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
			strSql.Append("delete from wx_zh_zs ");
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
			strSql.Append("delete from wx_zh_zs ");
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
		public WeiXinPF.Model.wx_zh_zs GetModel(int id)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 id,company_name,linkman,tel,mobile,fax,email,qq,type,sq_area,createdate,sort_id from wx_zh_zs ");
			strSql.Append(" where id=@id");
			SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)
			};
			parameters[0].Value = id;

			WeiXinPF.Model.wx_zh_zs model=new WeiXinPF.Model.wx_zh_zs();
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
		public WeiXinPF.Model.wx_zh_zs DataRowToModel(DataRow row)
		{
			WeiXinPF.Model.wx_zh_zs model=new WeiXinPF.Model.wx_zh_zs();
			if (row != null)
			{
				if(row["id"]!=null && row["id"].ToString()!="")
				{
					model.id=int.Parse(row["id"].ToString());
				}
				if(row["company_name"]!=null)
				{
					model.company_name=row["company_name"].ToString();
				}
				if(row["linkman"]!=null)
				{
					model.linkman=row["linkman"].ToString();
				}
				if(row["tel"]!=null)
				{
					model.tel=row["tel"].ToString();
				}
				if(row["mobile"]!=null)
				{
					model.mobile=row["mobile"].ToString();
				}
				if(row["fax"]!=null)
				{
					model.fax=row["fax"].ToString();
				}
				if(row["email"]!=null)
				{
					model.email=row["email"].ToString();
				}
				if(row["qq"]!=null)
				{
					model.qq=row["qq"].ToString();
				}
				if(row["type"]!=null)
				{
					model.type=row["type"].ToString();
				}
				if(row["sq_area"]!=null)
				{
					model.sq_area=row["sq_area"].ToString();
				}
				if(row["createdate"]!=null && row["createdate"].ToString()!="")
				{
					model.createdate=DateTime.Parse(row["createdate"].ToString());
				}
				if(row["sort_id"]!=null && row["sort_id"].ToString()!="")
				{
					model.sort_id=int.Parse(row["sort_id"].ToString());
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
			strSql.Append("select id,company_name,linkman,tel,mobile,fax,email,qq,type,sq_area,createdate,sort_id ");
			strSql.Append(" FROM wx_zh_zs ");
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
			strSql.Append(" id,company_name,linkman,tel,mobile,fax,email,qq,type,sq_area,createdate,sort_id ");
			strSql.Append(" FROM wx_zh_zs ");
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
			strSql.Append("select count(1) FROM wx_zh_zs ");
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
			strSql.Append(")AS Row, T.*  from wx_zh_zs T ");
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
			parameters[0].Value = "wx_zh_zs";
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
        /// 获得查询分页数据
        /// </summary>
        public DataSet GetList(int pageSize, int pageIndex, string strWhere, string filedOrder, out int recordCount)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select a.* from wx_zh_zs a ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where  " + strWhere);
            }
            recordCount = Convert.ToInt32(DbHelperSQL.GetSingle(PagingHelper.CreateCountingSql(strSql.ToString())));
            return DbHelperSQL.Query(PagingHelper.CreatePagingSql(recordCount, pageSize, pageIndex, strSql.ToString(), filedOrder));
        }
		#endregion  ExtensionMethod
	}
}

