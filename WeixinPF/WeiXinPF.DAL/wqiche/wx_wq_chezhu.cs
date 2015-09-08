using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using WeiXinPF.DBUtility;
using WeiXinPF.Common;//Please add references
namespace WeiXinPF.DAL
{
	/// <summary>
	/// 数据访问类:wx_wq_chezhu
	/// </summary>
	public partial class wx_wq_chezhu
	{
		public wx_wq_chezhu()
		{}
		#region  BasicMethod

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
		return DbHelperSQL.GetMaxID("Id", "wx_wq_chezhu"); 
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int Id)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from wx_wq_chezhu");
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
		public int Add(WeiXinPF.Model.wx_wq_chezhu model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into wx_wq_chezhu(");
			strSql.Append("cpNum,ppid,cxid,cxingid,Name,teltephone,spdate,gcdate,prevBxmoney,prevBxdate,prevNjdate,sort_id,createdate,prevBymoney,prevBydate,prevBylicheng,wid,openid)");
			strSql.Append(" values (");
			strSql.Append("@cpNum,@ppid,@cxid,@cxingid,@Name,@teltephone,@spdate,@gcdate,@prevBxmoney,@prevBxdate,@prevNjdate,@sort_id,@createdate,@prevBymoney,@prevBydate,@prevBylicheng,@wid,@openid)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@cpNum", SqlDbType.NVarChar,300),
					new SqlParameter("@ppid", SqlDbType.Int,4),
					new SqlParameter("@cxid", SqlDbType.Int,4),
					new SqlParameter("@cxingid", SqlDbType.Int,4),
					new SqlParameter("@Name", SqlDbType.VarChar,300),
					new SqlParameter("@teltephone", SqlDbType.VarChar,500),
					new SqlParameter("@spdate", SqlDbType.DateTime),
					new SqlParameter("@gcdate", SqlDbType.DateTime),
					new SqlParameter("@prevBxmoney", SqlDbType.Float,8),
					new SqlParameter("@prevBxdate", SqlDbType.DateTime),
					new SqlParameter("@prevNjdate", SqlDbType.DateTime),
					new SqlParameter("@sort_id", SqlDbType.Int,4),
					new SqlParameter("@createdate", SqlDbType.DateTime),
					new SqlParameter("@prevBymoney", SqlDbType.Float,8),
					new SqlParameter("@prevBydate", SqlDbType.DateTime),
					new SqlParameter("@prevBylicheng", SqlDbType.Float,8),
					new SqlParameter("@wid", SqlDbType.Int,4),
					new SqlParameter("@openid", SqlDbType.NVarChar,500)};
			parameters[0].Value = model.cpNum;
			parameters[1].Value = model.ppid;
			parameters[2].Value = model.cxid;
			parameters[3].Value = model.cxingid;
			parameters[4].Value = model.Name;
			parameters[5].Value = model.teltephone;
			parameters[6].Value = model.spdate;
			parameters[7].Value = model.gcdate;
			parameters[8].Value = model.prevBxmoney;
			parameters[9].Value = model.prevBxdate;
			parameters[10].Value = model.prevNjdate;
			parameters[11].Value = model.sort_id;
			parameters[12].Value = model.createdate;
			parameters[13].Value = model.prevBymoney;
			parameters[14].Value = model.prevBydate;
			parameters[15].Value = model.prevBylicheng;
			parameters[16].Value = model.wid;
			parameters[17].Value = model.openid;

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
		public bool Update(WeiXinPF.Model.wx_wq_chezhu model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update wx_wq_chezhu set ");
			strSql.Append("cpNum=@cpNum,");
			strSql.Append("ppid=@ppid,");
			strSql.Append("cxid=@cxid,");
			strSql.Append("cxingid=@cxingid,");
			strSql.Append("Name=@Name,");
			strSql.Append("teltephone=@teltephone,");
			strSql.Append("spdate=@spdate,");
			strSql.Append("gcdate=@gcdate,");
			strSql.Append("prevBxmoney=@prevBxmoney,");
			strSql.Append("prevBxdate=@prevBxdate,");
			strSql.Append("prevNjdate=@prevNjdate,");
			strSql.Append("sort_id=@sort_id,");
			strSql.Append("createdate=@createdate,");
			strSql.Append("prevBymoney=@prevBymoney,");
			strSql.Append("prevBydate=@prevBydate,");
			strSql.Append("prevBylicheng=@prevBylicheng,");
			strSql.Append("wid=@wid,");
			strSql.Append("openid=@openid");
			strSql.Append(" where Id=@Id");
			SqlParameter[] parameters = {
					new SqlParameter("@cpNum", SqlDbType.NVarChar,300),
					new SqlParameter("@ppid", SqlDbType.Int,4),
					new SqlParameter("@cxid", SqlDbType.Int,4),
					new SqlParameter("@cxingid", SqlDbType.Int,4),
					new SqlParameter("@Name", SqlDbType.VarChar,300),
					new SqlParameter("@teltephone", SqlDbType.VarChar,500),
					new SqlParameter("@spdate", SqlDbType.DateTime),
					new SqlParameter("@gcdate", SqlDbType.DateTime),
					new SqlParameter("@prevBxmoney", SqlDbType.Float,8),
					new SqlParameter("@prevBxdate", SqlDbType.DateTime),
					new SqlParameter("@prevNjdate", SqlDbType.DateTime),
					new SqlParameter("@sort_id", SqlDbType.Int,4),
					new SqlParameter("@createdate", SqlDbType.DateTime),
					new SqlParameter("@prevBymoney", SqlDbType.Float,8),
					new SqlParameter("@prevBydate", SqlDbType.DateTime),
					new SqlParameter("@prevBylicheng", SqlDbType.Float,8),
					new SqlParameter("@wid", SqlDbType.Int,4),
					new SqlParameter("@openid", SqlDbType.NVarChar,500),
					new SqlParameter("@Id", SqlDbType.Int,4)};
			parameters[0].Value = model.cpNum;
			parameters[1].Value = model.ppid;
			parameters[2].Value = model.cxid;
			parameters[3].Value = model.cxingid;
			parameters[4].Value = model.Name;
			parameters[5].Value = model.teltephone;
			parameters[6].Value = model.spdate;
			parameters[7].Value = model.gcdate;
			parameters[8].Value = model.prevBxmoney;
			parameters[9].Value = model.prevBxdate;
			parameters[10].Value = model.prevNjdate;
			parameters[11].Value = model.sort_id;
			parameters[12].Value = model.createdate;
			parameters[13].Value = model.prevBymoney;
			parameters[14].Value = model.prevBydate;
			parameters[15].Value = model.prevBylicheng;
			parameters[16].Value = model.wid;
			parameters[17].Value = model.openid;
			parameters[18].Value = model.Id;

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
			strSql.Append("delete from wx_wq_chezhu ");
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
			strSql.Append("delete from wx_wq_chezhu ");
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
		public WeiXinPF.Model.wx_wq_chezhu GetModel(int Id)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 Id,cpNum,ppid,cxid,cxingid,Name,teltephone,spdate,gcdate,prevBxmoney,prevBxdate,prevNjdate,sort_id,createdate,prevBymoney,prevBydate,prevBylicheng,wid,openid from wx_wq_chezhu ");
			strSql.Append(" where Id=@Id");
			SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.Int,4)
			};
			parameters[0].Value = Id;

			WeiXinPF.Model.wx_wq_chezhu model=new WeiXinPF.Model.wx_wq_chezhu();
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
		public WeiXinPF.Model.wx_wq_chezhu DataRowToModel(DataRow row)
		{
			WeiXinPF.Model.wx_wq_chezhu model=new WeiXinPF.Model.wx_wq_chezhu();
			if (row != null)
			{
				if(row["Id"]!=null && row["Id"].ToString()!="")
				{
					model.Id=int.Parse(row["Id"].ToString());
				}
				if(row["cpNum"]!=null)
				{
					model.cpNum=row["cpNum"].ToString();
				}
				if(row["ppid"]!=null && row["ppid"].ToString()!="")
				{
					model.ppid=int.Parse(row["ppid"].ToString());
				}
				if(row["cxid"]!=null && row["cxid"].ToString()!="")
				{
					model.cxid=int.Parse(row["cxid"].ToString());
				}
				if(row["cxingid"]!=null && row["cxingid"].ToString()!="")
				{
					model.cxingid=int.Parse(row["cxingid"].ToString());
				}
				if(row["Name"]!=null)
				{
					model.Name=row["Name"].ToString();
				}
				if(row["teltephone"]!=null)
				{
					model.teltephone=row["teltephone"].ToString();
				}
				if(row["spdate"]!=null && row["spdate"].ToString()!="")
				{
					model.spdate=DateTime.Parse(row["spdate"].ToString());
				}
				if(row["gcdate"]!=null && row["gcdate"].ToString()!="")
				{
					model.gcdate=DateTime.Parse(row["gcdate"].ToString());
				}
				if(row["prevBxmoney"]!=null && row["prevBxmoney"].ToString()!="")
				{
					model.prevBxmoney=decimal.Parse(row["prevBxmoney"].ToString());
				}
				if(row["prevBxdate"]!=null && row["prevBxdate"].ToString()!="")
				{
					model.prevBxdate=DateTime.Parse(row["prevBxdate"].ToString());
				}
				if(row["prevNjdate"]!=null && row["prevNjdate"].ToString()!="")
				{
					model.prevNjdate=DateTime.Parse(row["prevNjdate"].ToString());
				}
				if(row["sort_id"]!=null && row["sort_id"].ToString()!="")
				{
					model.sort_id=int.Parse(row["sort_id"].ToString());
				}
				if(row["createdate"]!=null && row["createdate"].ToString()!="")
				{
					model.createdate=DateTime.Parse(row["createdate"].ToString());
				}
				if(row["prevBymoney"]!=null && row["prevBymoney"].ToString()!="")
				{
					model.prevBymoney=decimal.Parse(row["prevBymoney"].ToString());
				}
				if(row["prevBydate"]!=null && row["prevBydate"].ToString()!="")
				{
					model.prevBydate=DateTime.Parse(row["prevBydate"].ToString());
				}
				if(row["prevBylicheng"]!=null && row["prevBylicheng"].ToString()!="")
				{
					model.prevBylicheng=decimal.Parse(row["prevBylicheng"].ToString());
				}
				if(row["wid"]!=null && row["wid"].ToString()!="")
				{
					model.wid=int.Parse(row["wid"].ToString());
				}
				if(row["openid"]!=null)
				{
					model.openid=row["openid"].ToString();
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
			strSql.Append("select Id,cpNum,ppid,cxid,cxingid,Name,teltephone,spdate,gcdate,prevBxmoney,prevBxdate,prevNjdate,sort_id,createdate,prevBymoney,prevBydate,prevBylicheng,wid,openid ");
			strSql.Append(" FROM wx_wq_chezhu ");
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
			strSql.Append(" Id,cpNum,ppid,cxid,cxingid,Name,teltephone,spdate,gcdate,prevBxmoney,prevBxdate,prevNjdate,sort_id,createdate,prevBymoney,prevBydate,prevBylicheng,wid,openid ");
			strSql.Append(" FROM wx_wq_chezhu ");
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
			strSql.Append("select count(1) FROM wx_wq_chezhu ");
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
			strSql.Append(")AS Row, T.*  from wx_wq_chezhu T ");
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
			parameters[0].Value = "wx_wq_chezhu";
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
            strSql.Append("select a.*,(select name from wx_wq_pinpai p where p.id=a.ppid) as pinpai,(select name from wx_wq_chexi xi where xi.id=a.cxid) as chexi,(select name from wx_wq_chexing cx where cx.id=a.cxingid) as chexing from wx_wq_chezhu a ");
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

