/**************************************
 *
 * 作者：李~朴
 * 公司:上·海沐·雪·网·络·科·技·有·限·公·司
 * qq:23002807
 * website:http://uweixin.cn
 * taobao:https://item.taobao.com/item.htm?spm=686.1000925.0.0.5HYEHQ&id=520523216527  
 * createDate:2015-8-3
 * update:2015-8-3
 * 
 ***********************************/


using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using WeiXinPF.DBUtility;
using WeiXinPF.Common; 
namespace WeiXinPF.DAL
{
	/// <summary>
	/// 数据访问类:wx_xjhongbao_lqinfo
	/// </summary>
	public partial class wx_xjhongbao_lqinfo
	{
		public wx_xjhongbao_lqinfo()
		{}
        #region  BasicMethod

        /// <summary>
        /// 得到最大ID
        /// </summary>
        public int GetMaxId()
        {
            return DbHelperSQL.GetMaxID("id", "wx_xjhongbao_lqinfo");
        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from wx_xjhongbao_lqinfo");
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
        public int Add(WeiXinPF.Model.wx_xjhongbao_lqinfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into wx_xjhongbao_lqinfo(");
            strSql.Append("wid,actionId,openid,userNick,total_amount,Send_time,mch_billno,mch_id,detail_id,hbstatus,send_type,hb_type,reason,Refund_time,createDate,remark)");
            strSql.Append(" values (");
            strSql.Append("@wid,@actionId,@openid,@userNick,@total_amount,@Send_time,@mch_billno,@mch_id,@detail_id,@hbstatus,@send_type,@hb_type,@reason,@Refund_time,@createDate,@remark)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@wid", SqlDbType.Int,4),
					new SqlParameter("@actionId", SqlDbType.Int,4),
					new SqlParameter("@openid", SqlDbType.VarChar,300),
					new SqlParameter("@userNick", SqlDbType.VarChar,400),
					new SqlParameter("@total_amount", SqlDbType.Int,4),
					new SqlParameter("@Send_time", SqlDbType.DateTime),
					new SqlParameter("@mch_billno", SqlDbType.VarChar,28),
					new SqlParameter("@mch_id", SqlDbType.VarChar,32),
					new SqlParameter("@detail_id", SqlDbType.VarChar,32),
					new SqlParameter("@hbstatus", SqlDbType.VarChar,16),
					new SqlParameter("@send_type", SqlDbType.VarChar,32),
					new SqlParameter("@hb_type", SqlDbType.VarChar,32),
					new SqlParameter("@reason", SqlDbType.VarChar,2000),
					new SqlParameter("@Refund_time", SqlDbType.DateTime),
					new SqlParameter("@createDate", SqlDbType.DateTime),
					new SqlParameter("@remark", SqlDbType.VarChar,2000)};
            parameters[0].Value = model.wid;
            parameters[1].Value = model.actionId;
            parameters[2].Value = model.openid;
            parameters[3].Value = model.userNick;
            parameters[4].Value = model.total_amount;
            parameters[5].Value = model.Send_time;
            parameters[6].Value = model.mch_billno;
            parameters[7].Value = model.mch_id;
            parameters[8].Value = model.detail_id;
            parameters[9].Value = model.hbstatus;
            parameters[10].Value = model.send_type;
            parameters[11].Value = model.hb_type;
            parameters[12].Value = model.reason;
            parameters[13].Value = model.Refund_time;
            parameters[14].Value = model.createDate;
            parameters[15].Value = model.remark;

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
        public bool Update(WeiXinPF.Model.wx_xjhongbao_lqinfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update wx_xjhongbao_lqinfo set ");
            strSql.Append("wid=@wid,");
            strSql.Append("actionId=@actionId,");
            strSql.Append("openid=@openid,");
            strSql.Append("userNick=@userNick,");
            strSql.Append("total_amount=@total_amount,");
            strSql.Append("Send_time=@Send_time,");
            strSql.Append("mch_billno=@mch_billno,");
            strSql.Append("mch_id=@mch_id,");
            strSql.Append("detail_id=@detail_id,");
            strSql.Append("hbstatus=@hbstatus,");
            strSql.Append("send_type=@send_type,");
            strSql.Append("hb_type=@hb_type,");
            strSql.Append("reason=@reason,");
            strSql.Append("Refund_time=@Refund_time,");
            strSql.Append("createDate=@createDate,");
            strSql.Append("remark=@remark");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
					new SqlParameter("@wid", SqlDbType.Int,4),
					new SqlParameter("@actionId", SqlDbType.Int,4),
					new SqlParameter("@openid", SqlDbType.VarChar,300),
					new SqlParameter("@userNick", SqlDbType.VarChar,400),
					new SqlParameter("@total_amount", SqlDbType.Int,4),
					new SqlParameter("@Send_time", SqlDbType.DateTime),
					new SqlParameter("@mch_billno", SqlDbType.VarChar,28),
					new SqlParameter("@mch_id", SqlDbType.VarChar,32),
					new SqlParameter("@detail_id", SqlDbType.VarChar,32),
					new SqlParameter("@hbstatus", SqlDbType.VarChar,16),
					new SqlParameter("@send_type", SqlDbType.VarChar,32),
					new SqlParameter("@hb_type", SqlDbType.VarChar,32),
					new SqlParameter("@reason", SqlDbType.VarChar,2000),
					new SqlParameter("@Refund_time", SqlDbType.DateTime),
					new SqlParameter("@createDate", SqlDbType.DateTime),
					new SqlParameter("@remark", SqlDbType.VarChar,2000),
					new SqlParameter("@id", SqlDbType.Int,4)};
            parameters[0].Value = model.wid;
            parameters[1].Value = model.actionId;
            parameters[2].Value = model.openid;
            parameters[3].Value = model.userNick;
            parameters[4].Value = model.total_amount;
            parameters[5].Value = model.Send_time;
            parameters[6].Value = model.mch_billno;
            parameters[7].Value = model.mch_id;
            parameters[8].Value = model.detail_id;
            parameters[9].Value = model.hbstatus;
            parameters[10].Value = model.send_type;
            parameters[11].Value = model.hb_type;
            parameters[12].Value = model.reason;
            parameters[13].Value = model.Refund_time;
            parameters[14].Value = model.createDate;
            parameters[15].Value = model.remark;
            parameters[16].Value = model.id;

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
            strSql.Append("delete from wx_xjhongbao_lqinfo ");
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
            strSql.Append("delete from wx_xjhongbao_lqinfo ");
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
        public WeiXinPF.Model.wx_xjhongbao_lqinfo GetModel(int id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 id,wid,actionId,openid,userNick,total_amount,Send_time,mch_billno,mch_id,detail_id,hbstatus,send_type,hb_type,reason,Refund_time,createDate,remark from wx_xjhongbao_lqinfo ");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)
			};
            parameters[0].Value = id;

            WeiXinPF.Model.wx_xjhongbao_lqinfo model = new WeiXinPF.Model.wx_xjhongbao_lqinfo();
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
        public WeiXinPF.Model.wx_xjhongbao_lqinfo DataRowToModel(DataRow row)
        {
            WeiXinPF.Model.wx_xjhongbao_lqinfo model = new WeiXinPF.Model.wx_xjhongbao_lqinfo();
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
                if (row["actionId"] != null && row["actionId"].ToString() != "")
                {
                    model.actionId = int.Parse(row["actionId"].ToString());
                }
                if (row["openid"] != null)
                {
                    model.openid = row["openid"].ToString();
                }
                if (row["userNick"] != null)
                {
                    model.userNick = row["userNick"].ToString();
                }
                if (row["total_amount"] != null && row["total_amount"].ToString() != "")
                {
                    model.total_amount = int.Parse(row["total_amount"].ToString());
                }
                if (row["Send_time"] != null && row["Send_time"].ToString() != "")
                {
                    model.Send_time = DateTime.Parse(row["Send_time"].ToString());
                }
                if (row["mch_billno"] != null)
                {
                    model.mch_billno = row["mch_billno"].ToString();
                }
                if (row["mch_id"] != null)
                {
                    model.mch_id = row["mch_id"].ToString();
                }
                if (row["detail_id"] != null)
                {
                    model.detail_id = row["detail_id"].ToString();
                }
                if (row["hbstatus"] != null)
                {
                    model.hbstatus = row["hbstatus"].ToString();
                }
                if (row["send_type"] != null)
                {
                    model.send_type = row["send_type"].ToString();
                }
                if (row["hb_type"] != null)
                {
                    model.hb_type = row["hb_type"].ToString();
                }
                if (row["reason"] != null)
                {
                    model.reason = row["reason"].ToString();
                }
                if (row["Refund_time"] != null && row["Refund_time"].ToString() != "")
                {
                    model.Refund_time = DateTime.Parse(row["Refund_time"].ToString());
                }
                if (row["createDate"] != null && row["createDate"].ToString() != "")
                {
                    model.createDate = DateTime.Parse(row["createDate"].ToString());
                }
                if (row["remark"] != null)
                {
                    model.remark = row["remark"].ToString();
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
            strSql.Append("select id,wid,actionId,openid,userNick,total_amount,Send_time,mch_billno,mch_id,detail_id,hbstatus,send_type,hb_type,reason,Refund_time,createDate,remark ");
            strSql.Append(" FROM wx_xjhongbao_lqinfo ");
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
            strSql.Append(" id,wid,actionId,openid,userNick,total_amount,Send_time,mch_billno,mch_id,detail_id,hbstatus,send_type,hb_type,reason,Refund_time,createDate,remark ");
            strSql.Append(" FROM wx_xjhongbao_lqinfo ");
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
            strSql.Append("select count(1) FROM wx_xjhongbao_lqinfo ");
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
            strSql.Append(")AS Row, T.*  from wx_xjhongbao_lqinfo T ");
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
            parameters[0].Value = "wx_xjhongbao_lqinfo";
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
            strSql.Append("select * from wx_xjhongbao_lqinfo ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where  " + strWhere);
            }
            recordCount = Convert.ToInt32(DbHelperSQL.GetSingle(PagingHelper.CreateCountingSql(strSql.ToString())));
            return DbHelperSQL.Query(PagingHelper.CreatePagingSql(recordCount, pageSize, pageIndex, strSql.ToString(), filedOrder));
        }



        /// <summary>
        /// 如果活动为一次性获奖情况， 判断用户是否获得过该活动的红包，如果有（并且获得的红包金额大于0），则返回true
        ///如果活动为每天一次获奖机会，判断用户当天是否获得过该活动的红包，如果有（并且获得的红包金额大于0），则返回true
        ///add by 李·朴 
        ///datetime:2015-8-3
        /// </summary>
        /// <param name="actionId">活动的id</param>
        /// <param name="openid">微信用户</param>
        /// <param name="isOnlyOne">一次性true,每天一次false</param>
        /// <returns>存在则返回true,不存在返回false</returns>
        public bool ExistsOpenid(int actionId,string openid,bool isOnlyOne)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(*) from wx_xjhongbao_lqinfo");
            if (isOnlyOne)
            {
                strSql.Append(" where actionId=@actionId and   openid=@openid and total_amount>0 and hbstatus!='FAILED'");
            }
            else
            {//每天一次的情况
                DateTime min_today = DateTime.Parse(DateTime.Now.ToString());
                DateTime max_today = min_today.AddDays(1);

                strSql.Append(" where actionId=@actionId and openid=@openid and total_amount>0 and   hbstatus!='FAILED' and  ( Send_time>='" + min_today.ToString() + "' and Send_time<'" + max_today.ToString() + "')");
            }
            SqlParameter[] parameters = {
                    new SqlParameter("@actionId", SqlDbType.Int,4),
                     new SqlParameter("@openid", SqlDbType.VarChar,300)
			};
            parameters[0].Value = actionId;
            parameters[1].Value = openid;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }


		#endregion  ExtensionMethod
	}
}

