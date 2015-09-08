using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using WeiXinPF.DBUtility;
using WeiXinPF.Common;//Please add references
namespace WeiXinPF.DAL
{
	/// <summary>
	/// 数据访问类:wx_wq_chexing
	/// </summary>
	public partial class wx_wq_chexing
	{
		public wx_wq_chexing()
		{}
		#region  BasicMethod

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
		return DbHelperSQL.GetMaxID("Id", "wx_wq_chexing"); 
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int Id)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from wx_wq_chexing");
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
		public int Add(WeiXinPF.Model.wx_wq_chexing model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into wx_wq_chexing(");
			strSql.Append("xid,dwNum,Name,niankuan,sort_id,zdPrice,jxsPrice,pic,qjid,pailiang,biansuxiang,createdate,wid,pid)");
			strSql.Append(" values (");
			strSql.Append("@xid,@dwNum,@Name,@niankuan,@sort_id,@zdPrice,@jxsPrice,@pic,@qjid,@pailiang,@biansuxiang,@createdate,@wid,@pid)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@xid", SqlDbType.Int,4),
					new SqlParameter("@dwNum", SqlDbType.Int,4),
					new SqlParameter("@Name", SqlDbType.VarChar,300),
					new SqlParameter("@niankuan", SqlDbType.VarChar,300),
					new SqlParameter("@sort_id", SqlDbType.Int,4),
					new SqlParameter("@zdPrice", SqlDbType.NVarChar,100),
					new SqlParameter("@jxsPrice", SqlDbType.NVarChar,100),
					new SqlParameter("@pic", SqlDbType.VarChar,500),
					new SqlParameter("@qjid", SqlDbType.Int,4),
					new SqlParameter("@pailiang", SqlDbType.VarChar,300),
					new SqlParameter("@biansuxiang", SqlDbType.VarChar,300),
					new SqlParameter("@createdate", SqlDbType.DateTime),
					new SqlParameter("@wid", SqlDbType.Int,4),
					new SqlParameter("@pid", SqlDbType.Int,4)};
			parameters[0].Value = model.xid;
			parameters[1].Value = model.dwNum;
			parameters[2].Value = model.Name;
			parameters[3].Value = model.niankuan;
			parameters[4].Value = model.sort_id;
			parameters[5].Value = model.zdPrice;
			parameters[6].Value = model.jxsPrice;
			parameters[7].Value = model.pic;
			parameters[8].Value = model.qjid;
			parameters[9].Value = model.pailiang;
			parameters[10].Value = model.biansuxiang;
			parameters[11].Value = model.createdate;
			parameters[12].Value = model.wid;
			parameters[13].Value = model.pid;

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
		public bool Update(WeiXinPF.Model.wx_wq_chexing model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update wx_wq_chexing set ");
			strSql.Append("xid=@xid,");
			strSql.Append("dwNum=@dwNum,");
			strSql.Append("Name=@Name,");
			strSql.Append("niankuan=@niankuan,");
			strSql.Append("sort_id=@sort_id,");
			strSql.Append("zdPrice=@zdPrice,");
			strSql.Append("jxsPrice=@jxsPrice,");
			strSql.Append("pic=@pic,");
			strSql.Append("qjid=@qjid,");
			strSql.Append("pailiang=@pailiang,");
			strSql.Append("biansuxiang=@biansuxiang,");
			strSql.Append("createdate=@createdate,");
			strSql.Append("wid=@wid,");
			strSql.Append("pid=@pid");
			strSql.Append(" where Id=@Id");
			SqlParameter[] parameters = {
					new SqlParameter("@xid", SqlDbType.Int,4),
					new SqlParameter("@dwNum", SqlDbType.Int,4),
					new SqlParameter("@Name", SqlDbType.VarChar,300),
					new SqlParameter("@niankuan", SqlDbType.VarChar,300),
					new SqlParameter("@sort_id", SqlDbType.Int,4),
					new SqlParameter("@zdPrice", SqlDbType.NVarChar,100),
					new SqlParameter("@jxsPrice", SqlDbType.NVarChar,100),
					new SqlParameter("@pic", SqlDbType.VarChar,500),
					new SqlParameter("@qjid", SqlDbType.Int,4),
					new SqlParameter("@pailiang", SqlDbType.VarChar,300),
					new SqlParameter("@biansuxiang", SqlDbType.VarChar,300),
					new SqlParameter("@createdate", SqlDbType.DateTime),
					new SqlParameter("@wid", SqlDbType.Int,4),
					new SqlParameter("@pid", SqlDbType.Int,4),
					new SqlParameter("@Id", SqlDbType.Int,4)};
			parameters[0].Value = model.xid;
			parameters[1].Value = model.dwNum;
			parameters[2].Value = model.Name;
			parameters[3].Value = model.niankuan;
			parameters[4].Value = model.sort_id;
			parameters[5].Value = model.zdPrice;
			parameters[6].Value = model.jxsPrice;
			parameters[7].Value = model.pic;
			parameters[8].Value = model.qjid;
			parameters[9].Value = model.pailiang;
			parameters[10].Value = model.biansuxiang;
			parameters[11].Value = model.createdate;
			parameters[12].Value = model.wid;
			parameters[13].Value = model.pid;
			parameters[14].Value = model.Id;

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
			strSql.Append("delete from wx_wq_chexing ");
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
			strSql.Append("delete from wx_wq_chexing ");
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
		public WeiXinPF.Model.wx_wq_chexing GetModel(int Id)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 Id,xid,dwNum,Name,niankuan,sort_id,zdPrice,jxsPrice,pic,qjid,pailiang,biansuxiang,createdate,wid,pid from wx_wq_chexing ");
			strSql.Append(" where Id=@Id");
			SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.Int,4)
			};
			parameters[0].Value = Id;

			WeiXinPF.Model.wx_wq_chexing model=new WeiXinPF.Model.wx_wq_chexing();
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
		public WeiXinPF.Model.wx_wq_chexing DataRowToModel(DataRow row)
		{
			WeiXinPF.Model.wx_wq_chexing model=new WeiXinPF.Model.wx_wq_chexing();
			if (row != null)
			{
				if(row["Id"]!=null && row["Id"].ToString()!="")
				{
					model.Id=int.Parse(row["Id"].ToString());
				}
				if(row["xid"]!=null && row["xid"].ToString()!="")
				{
					model.xid=int.Parse(row["xid"].ToString());
				}
				if(row["dwNum"]!=null && row["dwNum"].ToString()!="")
				{
					model.dwNum=int.Parse(row["dwNum"].ToString());
				}
				if(row["Name"]!=null)
				{
					model.Name=row["Name"].ToString();
				}
				if(row["niankuan"]!=null)
				{
					model.niankuan=row["niankuan"].ToString();
				}
				if(row["sort_id"]!=null && row["sort_id"].ToString()!="")
				{
					model.sort_id=int.Parse(row["sort_id"].ToString());
				}
				if(row["zdPrice"]!=null)
				{
					model.zdPrice=row["zdPrice"].ToString();
				}
				if(row["jxsPrice"]!=null)
				{
					model.jxsPrice=row["jxsPrice"].ToString();
				}
				if(row["pic"]!=null)
				{
					model.pic=row["pic"].ToString();
				}
				if(row["qjid"]!=null && row["qjid"].ToString()!="")
				{
					model.qjid=int.Parse(row["qjid"].ToString());
				}
				if(row["pailiang"]!=null)
				{
					model.pailiang=row["pailiang"].ToString();
				}
				if(row["biansuxiang"]!=null)
				{
					model.biansuxiang=row["biansuxiang"].ToString();
				}
				if(row["createdate"]!=null && row["createdate"].ToString()!="")
				{
					model.createdate=DateTime.Parse(row["createdate"].ToString());
				}
				if(row["wid"]!=null && row["wid"].ToString()!="")
				{
					model.wid=int.Parse(row["wid"].ToString());
				}
				if(row["pid"]!=null && row["pid"].ToString()!="")
				{
					model.pid=int.Parse(row["pid"].ToString());
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
			strSql.Append("select Id,xid,dwNum,Name,niankuan,sort_id,zdPrice,jxsPrice,pic,qjid,pailiang,biansuxiang,createdate,wid,pid ");
			strSql.Append(" FROM wx_wq_chexing ");
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
			strSql.Append(" Id,xid,dwNum,Name,niankuan,sort_id,zdPrice,jxsPrice,pic,qjid,pailiang,biansuxiang,createdate,wid,pid ");
			strSql.Append(" FROM wx_wq_chexing ");
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
			strSql.Append("select count(1) FROM wx_wq_chexing ");
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
			strSql.Append(")AS Row, T.*  from wx_wq_chexing T ");
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
			parameters[0].Value = "wx_wq_chexing";
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
            strSql.Append("select a.*,(select name from wx_wq_pinpai p where p.id=a.pid) as pinpai,(select name from wx_wq_chexi x where x.id=a.xid) as chexi from wx_wq_chexing a ");
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

