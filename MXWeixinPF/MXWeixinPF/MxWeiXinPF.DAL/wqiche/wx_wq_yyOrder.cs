using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using MxWeiXinPF.DBUtility;
using MxWeiXinPF.Common;//Please add references
namespace MxWeiXinPF.DAL
{
	/// <summary>
	/// 数据访问类:wx_wq_yyOrder
	/// </summary>
	public partial class wx_wq_yyOrder
	{
		public wx_wq_yyOrder()
		{}
		#region  BasicMethod

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
		return DbHelperSQL.GetMaxID("Id", "wx_wq_yyOrder"); 
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int Id)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from wx_wq_yyOrder");
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
		public int Add(MxWeiXinPF.Model.wx_wq_yyOrder model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into wx_wq_yyOrder(");
			strSql.Append("sort_id,Name,telephone,yydate,yytime,ddstatus,createdate,remark,kfremark,yid,pid,xingid,xid,openid,wid,carnum,km)");
			strSql.Append(" values (");
			strSql.Append("@sort_id,@Name,@telephone,@yydate,@yytime,@ddstatus,@createdate,@remark,@kfremark,@yid,@pid,@xingid,@xid,@openid,@wid,@carnum,@km)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@sort_id", SqlDbType.Int,4),
					new SqlParameter("@Name", SqlDbType.VarChar,300),
					new SqlParameter("@telephone", SqlDbType.VarChar,300),
					new SqlParameter("@yydate", SqlDbType.DateTime),
					new SqlParameter("@yytime", SqlDbType.VarChar,300),
					new SqlParameter("@ddstatus", SqlDbType.VarChar,300),
					new SqlParameter("@createdate", SqlDbType.DateTime),
					new SqlParameter("@remark", SqlDbType.VarChar,2000),
					new SqlParameter("@kfremark", SqlDbType.VarChar,2000),
					new SqlParameter("@yid", SqlDbType.Int,4),
					new SqlParameter("@pid", SqlDbType.Int,4),
					new SqlParameter("@xingid", SqlDbType.Int,4),
					new SqlParameter("@xid", SqlDbType.Int,4),
					new SqlParameter("@openid", SqlDbType.VarChar,800),
					new SqlParameter("@wid", SqlDbType.Int,4),
					new SqlParameter("@carnum", SqlDbType.NVarChar,500),
					new SqlParameter("@km", SqlDbType.Float,8)};
			parameters[0].Value = model.sort_id;
			parameters[1].Value = model.Name;
			parameters[2].Value = model.telephone;
			parameters[3].Value = model.yydate;
			parameters[4].Value = model.yytime;
			parameters[5].Value = model.ddstatus;
			parameters[6].Value = model.createdate;
			parameters[7].Value = model.remark;
			parameters[8].Value = model.kfremark;
			parameters[9].Value = model.yid;
			parameters[10].Value = model.pid;
			parameters[11].Value = model.xingid;
			parameters[12].Value = model.xid;
			parameters[13].Value = model.openid;
			parameters[14].Value = model.wid;
			parameters[15].Value = model.carnum;
			parameters[16].Value = model.km;

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
		public bool Update(MxWeiXinPF.Model.wx_wq_yyOrder model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update wx_wq_yyOrder set ");
			strSql.Append("sort_id=@sort_id,");
			strSql.Append("Name=@Name,");
			strSql.Append("telephone=@telephone,");
			strSql.Append("yydate=@yydate,");
			strSql.Append("yytime=@yytime,");
			strSql.Append("ddstatus=@ddstatus,");
			strSql.Append("createdate=@createdate,");
			strSql.Append("remark=@remark,");
			strSql.Append("kfremark=@kfremark,");
			strSql.Append("yid=@yid,");
			strSql.Append("pid=@pid,");
			strSql.Append("xingid=@xingid,");
			strSql.Append("xid=@xid,");
			strSql.Append("openid=@openid,");
			strSql.Append("wid=@wid,");
			strSql.Append("carnum=@carnum,");
			strSql.Append("km=@km");
			strSql.Append(" where Id=@Id");
			SqlParameter[] parameters = {
					new SqlParameter("@sort_id", SqlDbType.Int,4),
					new SqlParameter("@Name", SqlDbType.VarChar,300),
					new SqlParameter("@telephone", SqlDbType.VarChar,300),
					new SqlParameter("@yydate", SqlDbType.DateTime),
					new SqlParameter("@yytime", SqlDbType.VarChar,300),
					new SqlParameter("@ddstatus", SqlDbType.VarChar,300),
					new SqlParameter("@createdate", SqlDbType.DateTime),
					new SqlParameter("@remark", SqlDbType.VarChar,2000),
					new SqlParameter("@kfremark", SqlDbType.VarChar,2000),
					new SqlParameter("@yid", SqlDbType.Int,4),
					new SqlParameter("@pid", SqlDbType.Int,4),
					new SqlParameter("@xingid", SqlDbType.Int,4),
					new SqlParameter("@xid", SqlDbType.Int,4),
					new SqlParameter("@openid", SqlDbType.VarChar,800),
					new SqlParameter("@wid", SqlDbType.Int,4),
					new SqlParameter("@carnum", SqlDbType.NVarChar,500),
					new SqlParameter("@km", SqlDbType.Float,8),
					new SqlParameter("@Id", SqlDbType.Int,4)};
			parameters[0].Value = model.sort_id;
			parameters[1].Value = model.Name;
			parameters[2].Value = model.telephone;
			parameters[3].Value = model.yydate;
			parameters[4].Value = model.yytime;
			parameters[5].Value = model.ddstatus;
			parameters[6].Value = model.createdate;
			parameters[7].Value = model.remark;
			parameters[8].Value = model.kfremark;
			parameters[9].Value = model.yid;
			parameters[10].Value = model.pid;
			parameters[11].Value = model.xingid;
			parameters[12].Value = model.xid;
			parameters[13].Value = model.openid;
			parameters[14].Value = model.wid;
			parameters[15].Value = model.carnum;
			parameters[16].Value = model.km;
			parameters[17].Value = model.Id;

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
			strSql.Append("delete from wx_wq_yyOrder ");
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
			strSql.Append("delete from wx_wq_yyOrder ");
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
		public MxWeiXinPF.Model.wx_wq_yyOrder GetModel(int Id)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 Id,sort_id,Name,telephone,yydate,yytime,ddstatus,createdate,remark,kfremark,yid,pid,xingid,xid,openid,wid,carnum,km from wx_wq_yyOrder ");
			strSql.Append(" where Id=@Id");
			SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.Int,4)
			};
			parameters[0].Value = Id;

			MxWeiXinPF.Model.wx_wq_yyOrder model=new MxWeiXinPF.Model.wx_wq_yyOrder();
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
		public MxWeiXinPF.Model.wx_wq_yyOrder DataRowToModel(DataRow row)
		{
			MxWeiXinPF.Model.wx_wq_yyOrder model=new MxWeiXinPF.Model.wx_wq_yyOrder();
			if (row != null)
			{
				if(row["Id"]!=null && row["Id"].ToString()!="")
				{
					model.Id=int.Parse(row["Id"].ToString());
				}
				if(row["sort_id"]!=null && row["sort_id"].ToString()!="")
				{
					model.sort_id=int.Parse(row["sort_id"].ToString());
				}
				if(row["Name"]!=null)
				{
					model.Name=row["Name"].ToString();
				}
				if(row["telephone"]!=null)
				{
					model.telephone=row["telephone"].ToString();
				}
				if(row["yydate"]!=null && row["yydate"].ToString()!="")
				{
					model.yydate=DateTime.Parse(row["yydate"].ToString());
				}
				if(row["yytime"]!=null)
				{
					model.yytime=row["yytime"].ToString();
				}
				if(row["ddstatus"]!=null)
				{
					model.ddstatus=row["ddstatus"].ToString();
				}
				if(row["createdate"]!=null && row["createdate"].ToString()!="")
				{
					model.createdate=DateTime.Parse(row["createdate"].ToString());
				}
				if(row["remark"]!=null)
				{
					model.remark=row["remark"].ToString();
				}
				if(row["kfremark"]!=null)
				{
					model.kfremark=row["kfremark"].ToString();
				}
				if(row["yid"]!=null && row["yid"].ToString()!="")
				{
					model.yid=int.Parse(row["yid"].ToString());
				}
				if(row["pid"]!=null && row["pid"].ToString()!="")
				{
					model.pid=int.Parse(row["pid"].ToString());
				}
				if(row["xingid"]!=null && row["xingid"].ToString()!="")
				{
					model.xingid=int.Parse(row["xingid"].ToString());
				}
				if(row["xid"]!=null && row["xid"].ToString()!="")
				{
					model.xid=int.Parse(row["xid"].ToString());
				}
				if(row["openid"]!=null)
				{
					model.openid=row["openid"].ToString();
				}
				if(row["wid"]!=null && row["wid"].ToString()!="")
				{
					model.wid=int.Parse(row["wid"].ToString());
				}
				if(row["carnum"]!=null)
				{
					model.carnum=row["carnum"].ToString();
				}
				if(row["km"]!=null && row["km"].ToString()!="")
				{
					model.km=decimal.Parse(row["km"].ToString());
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
            strSql.Append("select Id,sort_id,Name,telephone,yydate,yytime,(select name from wx_wq_pinpai p where p.id=a.pid) as pinpai,(select name from wx_wq_chexi x where x.id=a.xid) as chexi,ddstatus,createdate,remark,kfremark,yid,pid,xingid,xid,openid,wid,carnum,km ");
			strSql.Append(" FROM wx_wq_yyOrder a");
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
			strSql.Append(" Id,sort_id,Name,telephone,yydate,yytime,ddstatus,createdate,remark,kfremark,yid,pid,xingid,xid,openid,wid,carnum,km ");
			strSql.Append(" FROM wx_wq_yyOrder ");
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
			strSql.Append("select count(1) FROM wx_wq_yyOrder ");
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
			strSql.Append(")AS Row, T.*  from wx_wq_yyOrder T ");
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
			parameters[0].Value = "wx_wq_yyOrder";
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
            strSql.Append("select a.*,(select name from wx_wq_pinpai p where p.id=a.pid) as pinpai,(select name from wx_wq_chexi x where x.id=a.xid) as chexi from wx_wq_yyOrder a ");
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

