using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using WeiXinPF.DBUtility;
using WeiXinPF.Common;//Please add references
namespace WeiXinPF.DAL
{
	/// <summary>
	/// 数据访问类:wx_wq_yuyue
	/// </summary>
	public partial class wx_wq_yuyue
	{
		public wx_wq_yuyue()
		{}
		#region  BasicMethod

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
		return DbHelperSQL.GetMaxID("Id", "wx_wq_yuyue"); 
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int Id)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from wx_wq_yuyue");
			strSql.Append(" where Id=@Id");
			SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.Int,4)
			};
			parameters[0].Value = Id;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(WeiXinPF.Model.wx_wq_yuyue model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into wx_wq_yuyue(");
			strSql.Append("Name,telephone,coverpic,address,lngX,latY,headpic,remark,sort_id,createdate,wid,type)");
			strSql.Append(" values (");
			strSql.Append("@Name,@telephone,@coverpic,@address,@lngX,@latY,@headpic,@remark,@sort_id,@createdate,@wid,@type)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@Name", SqlDbType.VarChar,4000),
					new SqlParameter("@telephone", SqlDbType.VarChar,300),
					new SqlParameter("@coverpic", SqlDbType.VarChar,800),
					new SqlParameter("@address", SqlDbType.VarChar,500),
					new SqlParameter("@lngX", SqlDbType.Float,8),
					new SqlParameter("@latY", SqlDbType.Float,8),
					new SqlParameter("@headpic", SqlDbType.VarChar,500),
					new SqlParameter("@remark", SqlDbType.VarChar,2000),
					new SqlParameter("@sort_id", SqlDbType.Int,4),
					new SqlParameter("@createdate", SqlDbType.DateTime),
					new SqlParameter("@wid", SqlDbType.Int,4),
					new SqlParameter("@type", SqlDbType.NVarChar,300)};
			parameters[0].Value = model.Name;
			parameters[1].Value = model.telephone;
			parameters[2].Value = model.coverpic;
			parameters[3].Value = model.address;
			parameters[4].Value = model.lngX;
			parameters[5].Value = model.latY;
			parameters[6].Value = model.headpic;
			parameters[7].Value = model.remark;
			parameters[8].Value = model.sort_id;
			parameters[9].Value = model.createdate;
			parameters[10].Value = model.wid;
			parameters[11].Value = model.type;

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
		public bool Update(WeiXinPF.Model.wx_wq_yuyue model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update wx_wq_yuyue set ");
			strSql.Append("Name=@Name,");
			strSql.Append("telephone=@telephone,");
			strSql.Append("coverpic=@coverpic,");
			strSql.Append("address=@address,");
			strSql.Append("lngX=@lngX,");
			strSql.Append("latY=@latY,");
			strSql.Append("headpic=@headpic,");
			strSql.Append("remark=@remark,");
			strSql.Append("sort_id=@sort_id,");
			strSql.Append("createdate=@createdate,");
			strSql.Append("wid=@wid,");
			strSql.Append("type=@type");
			strSql.Append(" where Id=@Id");
			SqlParameter[] parameters = {
					new SqlParameter("@Name", SqlDbType.VarChar,4000),
					new SqlParameter("@telephone", SqlDbType.VarChar,300),
					new SqlParameter("@coverpic", SqlDbType.VarChar,800),
					new SqlParameter("@address", SqlDbType.VarChar,500),
					new SqlParameter("@lngX", SqlDbType.Float,8),
					new SqlParameter("@latY", SqlDbType.Float,8),
					new SqlParameter("@headpic", SqlDbType.VarChar,500),
					new SqlParameter("@remark", SqlDbType.VarChar,2000),
					new SqlParameter("@sort_id", SqlDbType.Int,4),
					new SqlParameter("@createdate", SqlDbType.DateTime),
					new SqlParameter("@wid", SqlDbType.Int,4),
					new SqlParameter("@type", SqlDbType.NVarChar,300),
					new SqlParameter("@Id", SqlDbType.Int,4)};
			parameters[0].Value = model.Name;
			parameters[1].Value = model.telephone;
			parameters[2].Value = model.coverpic;
			parameters[3].Value = model.address;
			parameters[4].Value = model.lngX;
			parameters[5].Value = model.latY;
			parameters[6].Value = model.headpic;
			parameters[7].Value = model.remark;
			parameters[8].Value = model.sort_id;
			parameters[9].Value = model.createdate;
			parameters[10].Value = model.wid;
			parameters[11].Value = model.type;
			parameters[12].Value = model.Id;

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
		public bool Delete(int Id)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from wx_wq_yuyue ");
			strSql.Append(" where Id=@Id");
			SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.Int,4)
			};
			parameters[0].Value = Id;

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
		public bool DeleteList(string Idlist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from wx_wq_yuyue ");
			strSql.Append(" where Id in ("+Idlist + ")  ");
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
		public WeiXinPF.Model.wx_wq_yuyue GetModel(int Id)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 Id,Name,telephone,coverpic,address,lngX,latY,headpic,remark,sort_id,createdate,wid,type from wx_wq_yuyue ");
			strSql.Append(" where Id=@Id");
			SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.Int,4)
			};
			parameters[0].Value = Id;

			WeiXinPF.Model.wx_wq_yuyue model=new WeiXinPF.Model.wx_wq_yuyue();
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
		public WeiXinPF.Model.wx_wq_yuyue DataRowToModel(DataRow row)
		{
			WeiXinPF.Model.wx_wq_yuyue model=new WeiXinPF.Model.wx_wq_yuyue();
			if (row != null)
			{
				if(row["Id"]!=null && row["Id"].ToString()!="")
				{
					model.Id=int.Parse(row["Id"].ToString());
				}
				if(row["Name"]!=null)
				{
					model.Name=row["Name"].ToString();
				}
				if(row["telephone"]!=null)
				{
					model.telephone=row["telephone"].ToString();
				}
				if(row["coverpic"]!=null)
				{
					model.coverpic=row["coverpic"].ToString();
				}
				if(row["address"]!=null)
				{
					model.address=row["address"].ToString();
				}
				if(row["lngX"]!=null && row["lngX"].ToString()!="")
				{
					model.lngX=decimal.Parse(row["lngX"].ToString());
				}
				if(row["latY"]!=null && row["latY"].ToString()!="")
				{
					model.latY=decimal.Parse(row["latY"].ToString());
				}
				if(row["headpic"]!=null)
				{
					model.headpic=row["headpic"].ToString();
				}
				if(row["remark"]!=null)
				{
					model.remark=row["remark"].ToString();
				}
				if(row["sort_id"]!=null && row["sort_id"].ToString()!="")
				{
					model.sort_id=int.Parse(row["sort_id"].ToString());
				}
				if(row["createdate"]!=null && row["createdate"].ToString()!="")
				{
					model.createdate=DateTime.Parse(row["createdate"].ToString());
				}
				if(row["wid"]!=null && row["wid"].ToString()!="")
				{
					model.wid=int.Parse(row["wid"].ToString());
				}
				if(row["type"]!=null)
				{
					model.type=row["type"].ToString();
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
			strSql.Append("select Id,Name,telephone,coverpic,address,lngX,latY,headpic,remark,sort_id,createdate,wid,type ");
			strSql.Append(" FROM wx_wq_yuyue ");
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
			strSql.Append(" Id,Name,telephone,coverpic,address,lngX,latY,headpic,remark,sort_id,createdate,wid,type ");
			strSql.Append(" FROM wx_wq_yuyue ");
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
			strSql.Append("select count(1) FROM wx_wq_yuyue ");
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
				strSql.Append("order by T.Id desc");
			}
			strSql.Append(")AS Row, T.*  from wx_wq_yuyue T ");
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
			parameters[0].Value = "wx_wq_yuyue";
			parameters[1].Value = "Id";
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
            strSql.Append("select a.* from wx_wq_yuyue a ");
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

