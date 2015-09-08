using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using WeiXinPF.DBUtility;
using WeiXinPF.Common;//Please add references
namespace WeiXinPF.DAL
{
	/// <summary>
	/// 数据访问类:wx_xjhongbao_action
	/// </summary>
	public partial class wx_xjhongbao_action
	{
		public wx_xjhongbao_action()
		{}
        #region  BasicMethod

        /// <summary>
        /// 得到最大ID
        /// </summary>
        public int GetMaxId()
        {
            return DbHelperSQL.GetMaxID("id", "wx_xjhongbao_action");
        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from wx_xjhongbao_action");
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
        public int Add(WeiXinPF.Model.wx_xjhongbao_action model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into wx_xjhongbao_action(");
            strSql.Append("wid,hbType,act_name,moneyType,min_value,max_value,totalMoney,wishing,nick_name,send_name,logo_imgurl,client_ip,share_content,share_imgurl,share_url,beginDate,endDate,actPic,totalLqMoney,sort_id,remark,createDate,lqType,keywords)");
            strSql.Append(" values (");
            strSql.Append("@wid,@hbType,@act_name,@moneyType,@min_value,@max_value,@totalMoney,@wishing,@nick_name,@send_name,@logo_imgurl,@client_ip,@share_content,@share_imgurl,@share_url,@beginDate,@endDate,@actPic,@totalLqMoney,@sort_id,@remark,@createDate,@lqType,@keywords)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@wid", SqlDbType.Int,4),
					new SqlParameter("@hbType", SqlDbType.Int,4),
					new SqlParameter("@act_name", SqlDbType.VarChar,32),
					new SqlParameter("@moneyType", SqlDbType.Int,4),
					new SqlParameter("@min_value", SqlDbType.Int,4),
					new SqlParameter("@max_value", SqlDbType.Int,4),
					new SqlParameter("@totalMoney", SqlDbType.Int,4),
					new SqlParameter("@wishing", SqlDbType.VarChar,128),
					new SqlParameter("@nick_name", SqlDbType.VarChar,32),
					new SqlParameter("@send_name", SqlDbType.VarChar,32),
					new SqlParameter("@logo_imgurl", SqlDbType.VarChar,128),
					new SqlParameter("@client_ip", SqlDbType.VarChar,100),
					new SqlParameter("@share_content", SqlDbType.VarChar,100),
					new SqlParameter("@share_imgurl", SqlDbType.VarChar,300),
					new SqlParameter("@share_url", SqlDbType.VarChar,200),
					new SqlParameter("@beginDate", SqlDbType.DateTime),
					new SqlParameter("@endDate", SqlDbType.DateTime),
					new SqlParameter("@actPic", SqlDbType.VarChar,800),
					new SqlParameter("@totalLqMoney", SqlDbType.Int,4),
					new SqlParameter("@sort_id", SqlDbType.Int,4),
					new SqlParameter("@remark", SqlDbType.VarChar,2000),
					new SqlParameter("@createDate", SqlDbType.DateTime),
					new SqlParameter("@lqType", SqlDbType.Int,4),
					new SqlParameter("@keywords", SqlDbType.VarChar,100)};
            parameters[0].Value = model.wid;
            parameters[1].Value = model.hbType;
            parameters[2].Value = model.act_name;
            parameters[3].Value = model.moneyType;
            parameters[4].Value = model.min_value;
            parameters[5].Value = model.max_value;
            parameters[6].Value = model.totalMoney;
            parameters[7].Value = model.wishing;
            parameters[8].Value = model.nick_name;
            parameters[9].Value = model.send_name;
            parameters[10].Value = model.logo_imgurl;
            parameters[11].Value = model.client_ip;
            parameters[12].Value = model.share_content;
            parameters[13].Value = model.share_imgurl;
            parameters[14].Value = model.share_url;
            parameters[15].Value = model.beginDate;
            parameters[16].Value = model.endDate;
            parameters[17].Value = model.actPic;
            parameters[18].Value = model.totalLqMoney;
            parameters[19].Value = model.sort_id;
            parameters[20].Value = model.remark;
            parameters[21].Value = model.createDate;
            parameters[22].Value = model.lqType;
            parameters[23].Value = model.keywords;

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
        public bool Update(WeiXinPF.Model.wx_xjhongbao_action model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update wx_xjhongbao_action set ");
            strSql.Append("wid=@wid,");
            strSql.Append("hbType=@hbType,");
            strSql.Append("act_name=@act_name,");
            strSql.Append("moneyType=@moneyType,");
            strSql.Append("min_value=@min_value,");
            strSql.Append("max_value=@max_value,");
            strSql.Append("totalMoney=@totalMoney,");
            strSql.Append("wishing=@wishing,");
            strSql.Append("nick_name=@nick_name,");
            strSql.Append("send_name=@send_name,");
            strSql.Append("logo_imgurl=@logo_imgurl,");
            strSql.Append("client_ip=@client_ip,");
            strSql.Append("share_content=@share_content,");
            strSql.Append("share_imgurl=@share_imgurl,");
            strSql.Append("share_url=@share_url,");
            strSql.Append("beginDate=@beginDate,");
            strSql.Append("endDate=@endDate,");
            strSql.Append("actPic=@actPic,");
            strSql.Append("totalLqMoney=@totalLqMoney,");
            strSql.Append("sort_id=@sort_id,");
            strSql.Append("remark=@remark,");
            strSql.Append("createDate=@createDate,");
            strSql.Append("lqType=@lqType,");
            strSql.Append("keywords=@keywords");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
					new SqlParameter("@wid", SqlDbType.Int,4),
					new SqlParameter("@hbType", SqlDbType.Int,4),
					new SqlParameter("@act_name", SqlDbType.VarChar,32),
					new SqlParameter("@moneyType", SqlDbType.Int,4),
					new SqlParameter("@min_value", SqlDbType.Int,4),
					new SqlParameter("@max_value", SqlDbType.Int,4),
					new SqlParameter("@totalMoney", SqlDbType.Int,4),
					new SqlParameter("@wishing", SqlDbType.VarChar,128),
					new SqlParameter("@nick_name", SqlDbType.VarChar,32),
					new SqlParameter("@send_name", SqlDbType.VarChar,32),
					new SqlParameter("@logo_imgurl", SqlDbType.VarChar,128),
					new SqlParameter("@client_ip", SqlDbType.VarChar,100),
					new SqlParameter("@share_content", SqlDbType.VarChar,100),
					new SqlParameter("@share_imgurl", SqlDbType.VarChar,300),
					new SqlParameter("@share_url", SqlDbType.VarChar,200),
					new SqlParameter("@beginDate", SqlDbType.DateTime),
					new SqlParameter("@endDate", SqlDbType.DateTime),
					new SqlParameter("@actPic", SqlDbType.VarChar,800),
					new SqlParameter("@totalLqMoney", SqlDbType.Int,4),
					new SqlParameter("@sort_id", SqlDbType.Int,4),
					new SqlParameter("@remark", SqlDbType.VarChar,2000),
					new SqlParameter("@createDate", SqlDbType.DateTime),
					new SqlParameter("@lqType", SqlDbType.Int,4),
					new SqlParameter("@keywords", SqlDbType.VarChar,100),
					new SqlParameter("@id", SqlDbType.Int,4)};
            parameters[0].Value = model.wid;
            parameters[1].Value = model.hbType;
            parameters[2].Value = model.act_name;
            parameters[3].Value = model.moneyType;
            parameters[4].Value = model.min_value;
            parameters[5].Value = model.max_value;
            parameters[6].Value = model.totalMoney;
            parameters[7].Value = model.wishing;
            parameters[8].Value = model.nick_name;
            parameters[9].Value = model.send_name;
            parameters[10].Value = model.logo_imgurl;
            parameters[11].Value = model.client_ip;
            parameters[12].Value = model.share_content;
            parameters[13].Value = model.share_imgurl;
            parameters[14].Value = model.share_url;
            parameters[15].Value = model.beginDate;
            parameters[16].Value = model.endDate;
            parameters[17].Value = model.actPic;
            parameters[18].Value = model.totalLqMoney;
            parameters[19].Value = model.sort_id;
            parameters[20].Value = model.remark;
            parameters[21].Value = model.createDate;
            parameters[22].Value = model.lqType;
            parameters[23].Value = model.keywords;
            parameters[24].Value = model.id;

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
            strSql.Append("delete from wx_xjhongbao_action ");
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
            strSql.Append("delete from wx_xjhongbao_action ");
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
        public WeiXinPF.Model.wx_xjhongbao_action GetModel(int id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 id,wid,hbType,act_name,moneyType,min_value,max_value,totalMoney,wishing,nick_name,send_name,logo_imgurl,client_ip,share_content,share_imgurl,share_url,beginDate,endDate,actPic,totalLqMoney,sort_id,remark,createDate,lqType,keywords from wx_xjhongbao_action ");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)
			};
            parameters[0].Value = id;

            WeiXinPF.Model.wx_xjhongbao_action model = new WeiXinPF.Model.wx_xjhongbao_action();
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
        public WeiXinPF.Model.wx_xjhongbao_action DataRowToModel(DataRow row)
        {
            WeiXinPF.Model.wx_xjhongbao_action model = new WeiXinPF.Model.wx_xjhongbao_action();
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
                if (row["hbType"] != null && row["hbType"].ToString() != "")
                {
                    model.hbType = int.Parse(row["hbType"].ToString());
                }
                if (row["act_name"] != null)
                {
                    model.act_name = row["act_name"].ToString();
                }
                if (row["moneyType"] != null && row["moneyType"].ToString() != "")
                {
                    model.moneyType = int.Parse(row["moneyType"].ToString());
                }
                if (row["min_value"] != null && row["min_value"].ToString() != "")
                {
                    model.min_value = int.Parse(row["min_value"].ToString());
                }
                if (row["max_value"] != null && row["max_value"].ToString() != "")
                {
                    model.max_value = int.Parse(row["max_value"].ToString());
                }
                if (row["totalMoney"] != null && row["totalMoney"].ToString() != "")
                {
                    model.totalMoney = int.Parse(row["totalMoney"].ToString());
                }
                if (row["wishing"] != null)
                {
                    model.wishing = row["wishing"].ToString();
                }
                if (row["nick_name"] != null)
                {
                    model.nick_name = row["nick_name"].ToString();
                }
                if (row["send_name"] != null)
                {
                    model.send_name = row["send_name"].ToString();
                }
                if (row["logo_imgurl"] != null)
                {
                    model.logo_imgurl = row["logo_imgurl"].ToString();
                }
                if (row["client_ip"] != null)
                {
                    model.client_ip = row["client_ip"].ToString();
                }
                if (row["share_content"] != null)
                {
                    model.share_content = row["share_content"].ToString();
                }
                if (row["share_imgurl"] != null)
                {
                    model.share_imgurl = row["share_imgurl"].ToString();
                }
                if (row["share_url"] != null)
                {
                    model.share_url = row["share_url"].ToString();
                }
                if (row["beginDate"] != null && row["beginDate"].ToString() != "")
                {
                    model.beginDate = DateTime.Parse(row["beginDate"].ToString());
                }
                if (row["endDate"] != null && row["endDate"].ToString() != "")
                {
                    model.endDate = DateTime.Parse(row["endDate"].ToString());
                }
                if (row["actPic"] != null)
                {
                    model.actPic = row["actPic"].ToString();
                }
                if (row["totalLqMoney"] != null && row["totalLqMoney"].ToString() != "")
                {
                    model.totalLqMoney = int.Parse(row["totalLqMoney"].ToString());
                }
                if (row["sort_id"] != null && row["sort_id"].ToString() != "")
                {
                    model.sort_id = int.Parse(row["sort_id"].ToString());
                }
                if (row["remark"] != null)
                {
                    model.remark = row["remark"].ToString();
                }
                if (row["createDate"] != null && row["createDate"].ToString() != "")
                {
                    model.createDate = DateTime.Parse(row["createDate"].ToString());
                }
                if (row["lqType"] != null && row["lqType"].ToString() != "")
                {
                    model.lqType = int.Parse(row["lqType"].ToString());
                }
                if (row["keywords"] != null)
                {
                    model.keywords = row["keywords"].ToString();
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
            strSql.Append("select id,wid,hbType,act_name,moneyType,min_value,max_value,totalMoney,wishing,nick_name,send_name,logo_imgurl,client_ip,share_content,share_imgurl,share_url,beginDate,endDate,actPic,totalLqMoney,sort_id,remark,createDate,lqType,keywords ");
            strSql.Append(" FROM wx_xjhongbao_action ");
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
            strSql.Append(" id,wid,hbType,act_name,moneyType,min_value,max_value,totalMoney,wishing,nick_name,send_name,logo_imgurl,client_ip,share_content,share_imgurl,share_url,beginDate,endDate,actPic,totalLqMoney,sort_id,remark,createDate,lqType,keywords ");
            strSql.Append(" FROM wx_xjhongbao_action ");
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
            strSql.Append("select count(1) FROM wx_xjhongbao_action ");
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
            strSql.Append(")AS Row, T.*  from wx_xjhongbao_action T ");
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
            parameters[0].Value = "wx_xjhongbao_action";
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
            strSql.Append("select *,status_s=''  from wx_xjhongbao_action a ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where  " + strWhere);
            }
            recordCount = Convert.ToInt32(DbHelperSQL.GetSingle(PagingHelper.CreateCountingSql(strSql.ToString())));
            return DbHelperSQL.Query(PagingHelper.CreatePagingSql(recordCount, pageSize, pageIndex, strSql.ToString(), filedOrder));
        }


        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int id,int wid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from wx_xjhongbao_action");
            strSql.Append(" where id=@id and wid=@wid");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4),
                    new SqlParameter("@wid", SqlDbType.Int,4)
			};
            parameters[0].Value = id;
            parameters[1].Value = wid;
            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 是否存在关注红包记录
        /// </summary>
        public bool ExistsSubscribeAction(int wid)
        {
            DateTime now=DateTime.Now;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from wx_xjhongbao_action");
            strSql.Append(" where hbType=0 and wid=@wid and beginDate<='" + now.ToString() + "' and  endDate>='" + now.ToString() + "'");
            SqlParameter[] parameters = {
                    new SqlParameter("@wid", SqlDbType.Int,4)
			};
            parameters[0].Value = wid;
            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 是否存在关键词红包
        /// </summary>
        /// <param name="wid"></param>
        /// <param name="keywords"></param>
        /// <returns></returns>
        public bool ExistsKeyWordsAction(int wid,string keywords)
        {
            DateTime now = DateTime.Now;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from wx_xjhongbao_action");
            strSql.Append(" where hbType=1 and wid=@wid and keywords=@keywords  and beginDate<='" + now.ToString() + "' and  endDate>='" + now.ToString() + "'");
            SqlParameter[] parameters = {
                    new SqlParameter("@wid", SqlDbType.Int,4),
                    new SqlParameter("@keywords", SqlDbType.VarChar,100)
			};
            parameters[0].Value = wid;
            parameters[1].Value = keywords;
            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }



        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public WeiXinPF.Model.wx_xjhongbao_action GetGZHBModelByWid(int wid)
        {
            DateTime now = DateTime.Now;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 id,wid,hbType,act_name,keywords,moneyType,min_value,max_value,totalMoney,wishing,nick_name,send_name,logo_imgurl,client_ip,share_content,share_imgurl,share_url,beginDate,endDate,actPic,totalLqMoney,sort_id,remark,createDate,lqType from wx_xjhongbao_action ");
            strSql.Append(" where hbType=0 and wid=@wid and beginDate<='" + now.ToString() + "' and  endDate>='" + now.ToString() + "'");
            SqlParameter[] parameters = {
                    new SqlParameter("@wid", SqlDbType.Int,4)
			};
            parameters[0].Value = wid;
            WeiXinPF.Model.wx_xjhongbao_action model = new WeiXinPF.Model.wx_xjhongbao_action();
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
        /// 得到一个关键词对象实体
        /// </summary>
        public WeiXinPF.Model.wx_xjhongbao_action GetKeyWordsModelByWid(int wid,string keywords)
        {
            DateTime now = DateTime.Now;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 id,wid,hbType,act_name,keywords,moneyType,min_value,max_value,totalMoney,wishing,nick_name,send_name,logo_imgurl,client_ip,share_content,share_imgurl,share_url,beginDate,endDate,actPic,totalLqMoney,sort_id,remark,createDate,lqType from wx_xjhongbao_action ");
            strSql.Append(" where hbType=1 and wid=@wid and keywords=@keywords  and beginDate<='" + now.ToString() + "' and  endDate>='" + now.ToString() + "'");
            SqlParameter[] parameters = {
                    new SqlParameter("@wid", SqlDbType.Int,4),
                    new SqlParameter("@keywords", SqlDbType.VarChar,100)
			};
            parameters[0].Value = wid;
            parameters[1].Value = keywords;
            WeiXinPF.Model.wx_xjhongbao_action model = new WeiXinPF.Model.wx_xjhongbao_action();
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
        /// 得到一个对象实体(网页版的现金红包)
        /// </summary>
        public WeiXinPF.Model.wx_xjhongbao_action GetPageHongBaoModel(int id, int wid)
        {
            DateTime now = DateTime.Now;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 id,wid,hbType,act_name,moneyType,min_value,max_value,totalMoney,wishing,nick_name,send_name,logo_imgurl,client_ip,share_content,share_imgurl,share_url,beginDate,endDate,actPic,totalLqMoney,sort_id,remark,createDate,lqType,keywords from wx_xjhongbao_action ");
            strSql.Append(" where hbType=2 and id=@id and wid=@wid  and beginDate<='" + now.ToString() + "' and  endDate>='" + now.ToString() + "'");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4),
                    new SqlParameter("@wid", SqlDbType.Int,4)
			};
            parameters[0].Value = id;
            parameters[1].Value = wid;
            WeiXinPF.Model.wx_xjhongbao_action model = new WeiXinPF.Model.wx_xjhongbao_action();
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

