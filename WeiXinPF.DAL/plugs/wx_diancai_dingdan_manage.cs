﻿using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using WeiXinPF.DBUtility;
using WeiXinPF.Common;//Please add references
namespace WeiXinPF.DAL
{
    /// <summary>
    /// 数据访问类:wx_diancai_dingdan_manage
    /// </summary>
    public partial class wx_diancai_dingdan_manage
    {
        public wx_diancai_dingdan_manage()
        { }
        #region  BasicMethod

        /// <summary>
        /// 得到最大ID
        /// </summary>
        public int GetMaxId()
        {
            return DbHelperSQL.GetMaxID("id", "wx_diancai_dingdan_manage");
        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from wx_diancai_dingdan_manage");
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
        public int Add(WeiXinPF.Model.wx_diancai_dingdan_manage model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into wx_diancai_dingdan_manage(");
            strSql.Append("shopinfoid,openid,wid,orderNumber,deskNumber,customerName,customerTel,address,oderTime,oderRemark,payAmount,payStatus,createDate)");
            strSql.Append(" values (");
            strSql.Append("@shopinfoid,@openid,@wid,@orderNumber,@deskNumber,@customerName,@customerTel,@address,@oderTime,@oderRemark,@payAmount,@payStatus,@createDate)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@shopinfoid", SqlDbType.Int,4),
					new SqlParameter("@openid", SqlDbType.VarChar,200),
					new SqlParameter("@wid", SqlDbType.VarChar,50),
					new SqlParameter("@orderNumber", SqlDbType.VarChar,200),
					new SqlParameter("@deskNumber", SqlDbType.VarChar,200),
					new SqlParameter("@customerName", SqlDbType.VarChar,200),
					new SqlParameter("@customerTel", SqlDbType.VarChar,200),
					new SqlParameter("@address", SqlDbType.VarChar,300),
					new SqlParameter("@oderTime", SqlDbType.DateTime),
					new SqlParameter("@oderRemark", SqlDbType.VarChar,300),
					new SqlParameter("@payAmount", SqlDbType.Float,8),
					new SqlParameter("@payStatus", SqlDbType.Int,4),
					new SqlParameter("@createDate", SqlDbType.DateTime)};
            parameters[0].Value = model.shopinfoid;
            parameters[1].Value = model.openid;
            parameters[2].Value = model.wid;
            parameters[3].Value = model.orderNumber;
            parameters[4].Value = model.deskNumber;
            parameters[5].Value = model.customerName;
            parameters[6].Value = model.customerTel;
            parameters[7].Value = model.address;
            parameters[8].Value = model.oderTime;
            parameters[9].Value = model.oderRemark;
            parameters[10].Value = model.payAmount;
            parameters[11].Value = model.payStatus;
            parameters[12].Value = model.createDate;

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
        public bool Update(WeiXinPF.Model.wx_diancai_dingdan_manage model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update wx_diancai_dingdan_manage set ");
            strSql.Append("shopinfoid=@shopinfoid,");
            strSql.Append("openid=@openid,");
            strSql.Append("wid=@wid,");
            strSql.Append("orderNumber=@orderNumber,");
            strSql.Append("deskNumber=@deskNumber,");
            strSql.Append("customerName=@customerName,");
            strSql.Append("customerTel=@customerTel,");
            strSql.Append("address=@address,");
            strSql.Append("oderTime=@oderTime,");
            strSql.Append("oderRemark=@oderRemark,");
            strSql.Append("payAmount=@payAmount,");
            strSql.Append("payStatus=@payStatus,");
            strSql.Append("createDate=@createDate");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
					new SqlParameter("@shopinfoid", SqlDbType.Int,4),
					new SqlParameter("@openid", SqlDbType.VarChar,200),
					new SqlParameter("@wid", SqlDbType.VarChar,50),
					new SqlParameter("@orderNumber", SqlDbType.VarChar,200),
					new SqlParameter("@deskNumber", SqlDbType.VarChar,200),
					new SqlParameter("@customerName", SqlDbType.VarChar,200),
					new SqlParameter("@customerTel", SqlDbType.VarChar,200),
					new SqlParameter("@address", SqlDbType.VarChar,300),
					new SqlParameter("@oderTime", SqlDbType.DateTime),
					new SqlParameter("@oderRemark", SqlDbType.VarChar,300),
					new SqlParameter("@payAmount", SqlDbType.Float,8),
					new SqlParameter("@payStatus", SqlDbType.Int,4),
					new SqlParameter("@createDate", SqlDbType.DateTime),
					new SqlParameter("@id", SqlDbType.Int,4)};
            parameters[0].Value = model.shopinfoid;
            parameters[1].Value = model.openid;
            parameters[2].Value = model.wid;
            parameters[3].Value = model.orderNumber;
            parameters[4].Value = model.deskNumber;
            parameters[5].Value = model.customerName;
            parameters[6].Value = model.customerTel;
            parameters[7].Value = model.address;
            parameters[8].Value = model.oderTime;
            parameters[9].Value = model.oderRemark;
            parameters[10].Value = model.payAmount;
            parameters[11].Value = model.payStatus;
            parameters[12].Value = model.createDate;
            parameters[13].Value = model.id;

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
            strSql.Append("delete from wx_diancai_dingdan_manage ");
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
            strSql.Append("delete from wx_diancai_dingdan_manage ");
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
        public WeiXinPF.Model.wx_diancai_dingdan_manage GetModel(int id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 id,shopinfoid,openid,wid,orderNumber,deskNumber,customerName,customerTel,address,oderTime,oderRemark,payAmount,payStatus,createDate from wx_diancai_dingdan_manage ");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)
			};
            parameters[0].Value = id;

            WeiXinPF.Model.wx_diancai_dingdan_manage model = new WeiXinPF.Model.wx_diancai_dingdan_manage();
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
        public WeiXinPF.Model.wx_diancai_dingdan_manage DataRowToModel(DataRow row)
        {
            WeiXinPF.Model.wx_diancai_dingdan_manage model = new WeiXinPF.Model.wx_diancai_dingdan_manage();
            if (row != null)
            {
                if (row["id"] != null && row["id"].ToString() != "")
                {
                    model.id = int.Parse(row["id"].ToString());
                }
                if (row["shopinfoid"] != null && row["shopinfoid"].ToString() != "")
                {
                    model.shopinfoid = int.Parse(row["shopinfoid"].ToString());
                }
                if (row["openid"] != null)
                {
                    model.openid = row["openid"].ToString();
                }
                if (row["wid"] != null && row["wid"].ToString() != "")
                {
                    model.wid = row["wid"].ToString();
                }
                if (row["orderNumber"] != null)
                {
                    model.orderNumber = row["orderNumber"].ToString();
                }
                if (row["deskNumber"] != null)
                {
                    model.deskNumber = row["deskNumber"].ToString();
                }
                if (row["customerName"] != null)
                {
                    model.customerName = row["customerName"].ToString();
                }
                if (row["customerTel"] != null)
                {
                    model.customerTel = row["customerTel"].ToString();
                }
                if (row["address"] != null)
                {
                    model.address = row["address"].ToString();
                }
                if (row["oderTime"] != null && row["oderTime"].ToString() != "")
                {
                    model.oderTime = DateTime.Parse(row["oderTime"].ToString());
                }
                if (row["oderRemark"] != null)
                {
                    model.oderRemark = row["oderRemark"].ToString();
                }
                if (row["payAmount"] != null && row["payAmount"].ToString() != "")
                {
                    model.payAmount = decimal.Parse(row["payAmount"].ToString());
                }
                if (row["payStatus"] != null && row["payStatus"].ToString() != "")
                {
                    model.payStatus = int.Parse(row["payStatus"].ToString());
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
            strSql.Append("select id,shopinfoid,openid,wid,orderNumber,deskNumber,customerName,customerTel,address,oderTime,oderRemark,payAmount,payStatus,createDate ");
            strSql.Append(" FROM wx_diancai_dingdan_manage ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }
        /// <summary>
        /// 获得商品列表
        /// </summary>
        public DataSet GetCommodityList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select a.id as id,shopinfoid,openid,wid,orderNumber,deskNumber,customerName,customerTel,address,oderTime,oderRemark,payAmount,payStatus,createDate,b.id AS cid  FROM wx_diancai_dingdan_manage AS a ");
            strSql.Append(" right join (select * from wx_diancai_dingdan_commodity where status=1 ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" and " + strWhere);
            }
            strSql.Append(" ) as b on a.id=b.dingId ");
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
            strSql.Append(" id,shopinfoid,openid,wid,orderNumber,deskNumber,customerName,customerTel,address,oderTime,oderRemark,payAmount,payStatus,createDate ");
            strSql.Append(" FROM wx_diancai_dingdan_manage ");
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
            strSql.Append("select count(1) FROM wx_diancai_dingdan_manage ");
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
            strSql.Append(")AS Row, T.*  from wx_diancai_dingdan_manage T ");
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
            parameters[0].Value = "wx_diancai_dingdan_manage";
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
        public DataSet GetList(int pageSize, int pageIndex, string strWhere, string filedOrder, out int recordCount)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select id,shopinfoid,openid,wid,orderNumber,deskNumber,customerName,customerTel,address,oderTime,oderRemark,payAmount,payStatus,'' as payStatusStr,createDate ");
            strSql.Append(" FROM wx_diancai_dingdan_manage ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }

            recordCount = Convert.ToInt32(DbHelperSQL.GetSingle(PagingHelper.CreateCountingSql(strSql.ToString())));
            return DbHelperSQL.Query(PagingHelper.CreatePagingSql(recordCount, pageSize, pageIndex, strSql.ToString(), filedOrder));
        }

        public DataSet GetListList(string openid)
        {
            StringBuilder strSql = new StringBuilder();
            //strSql.Append("select id,shopinfoid,openid,wid,orderNumber,deskNumber,customerName,customerTel,address,oderTime,oderRemark,payAmount,payStatus,createDate ");
            //strSql.Append(" FROM wx_diancai_dingdan_manage ");

            strSql.Append(@"SELECT  d.id ,
                                        shopinfoid ,
                                        s.hotelName ,
                                        openid ,
                                        d.wid ,
                                        orderNumber ,
                                        deskNumber ,
                                        customerName ,
                                        customerTel ,
                                        d.address ,
                                        oderTime ,
                                        oderRemark ,
                                        payAmount ,
                                        payStatus ,
                                        d.createDate
                                FROM    wx_diancai_dingdan_manage d
                                        LEFT JOIN wx_diancai_shopinfo s ON d.shopinfoid = s.id");

            //TODO:测试阶段，没有数据，不对OpenID过滤，后续必须要加上。
            //if (openid.Trim() != "")
            //{
            //    strSql.Append(" where openid='" + openid + "'");
            //}
            return DbHelperSQL.Query(strSql.ToString());
        }


        public bool Update(int id, decimal payAmount)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update  wx_diancai_dingdan_manage set payAmount=" + payAmount + "  ");
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


        public DataSet Getcaopin(string dingdan)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" select aa.id,aa.cpName as cpName,bb.price as price,bb.num as num,bb.totpric as totpric  from wx_diancai_caipin_manage  as aa ");
            strSql.Append(" right join (select * from wx_diancai_dingdan_caiping where dingId='" + dingdan + "') as bb on aa.id=bb.caiId ");

            return DbHelperSQL.Query(strSql.ToString());
        }


        public DataSet Getcaopin(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" select aa.id,aa.cpName as cpName,bb.price as price,bb.num as num,bb.totpric as totpric  from wx_diancai_caipin_manage  as aa ");
            strSql.Append(" right join (select * from wx_diancai_dingdan_caiping where dingId='" + id + "') as bb on aa.id=bb.caiId ");

            return DbHelperSQL.Query(strSql.ToString());
        }

        public DataSet Getcommodity(string cid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" select aa.id,aa.cpName as cpName,bb.price as price,bb.status as status from wx_diancai_caipin_manage  as aa ");
            strSql.Append(" right join (select * from wx_diancai_dingdan_commodity where id='" + cid + "') as bb on aa.id=bb.caiId ");

            return DbHelperSQL.Query(strSql.ToString());
        }

        public DataSet GetcommodityTable(string did)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" select aa.id,aa.cpName as cpName,bb.price as price,bb.status from wx_diancai_caipin_manage  as aa ");
            strSql.Append(" right join (select * from wx_diancai_dingdan_commodity where dingId='" + did + "') as bb on aa.id=bb.caiId ");

            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 获取订单详情
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public DataSet GetOrderDetail(int orderId)
        {
            var sqlBuilder = new StringBuilder();
            sqlBuilder.Append(@"SELECT  d.customerName ,
                                                d.customerTel ,
                                                d.orderNumber ,
                                                d.id AS OrderID ,
                                                d.payAmount ,
                                                d.oderTime ,
                                                s.hotelName ,
                                                s.dcRename ,
                                                s.xplace ,
                                                s.yplace ,
                                                s.tel ,
                                                s.hoteltimeBegin ,
                                                s.hoteltimeEnd ,
                                                s.hoteltimeBegin1 ,
                                                s.hoteltimeEnd1 ,
                                                s.hoteltimeBegin2 ,
                                                s.hoteltimeEnd2
                                        FROM    dbo.wx_diancai_dingdan_manage d
                                                LEFT JOIN dbo.wx_diancai_shopinfo s ON d.shopinfoid = s.id
                                        WHERE   d.id = @OrderID;

                                        SELECT  cp.cpName ,
                                                c.caiId ,
                                                c.identifyingcode ,
                                                c.price ,
                                                c.status
                                        FROM    [dbo].[wx_diancai_dingdan_commodity] c
                                                INNER JOIN [dbo].[wx_diancai_caipin_manage] cp ON c.caiId = cp.id
                                        WHERE   c.dingId = @OrderID");

            SqlParameter[] sqlparams =
                {
                    new SqlParameter(){ParameterName = "@OrderID",SqlDbType = SqlDbType.Int,SqlValue = orderId} 
                };

            return DbHelperSQL.Query(sqlBuilder.ToString(), sqlparams);
        }

        /// <summary>
        /// 获取订单菜品详情
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="caipinId"></param>
        /// <returns></returns>
        public DataSet GetOrderCaiPinDetail(int orderId, int caipinId)
        {
            return null;
        }


        public WeiXinPF.Model.wx_diancai_dingdan_manage GetModeldingdan(string dingdan)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 id,shopinfoid,openid,wid,orderNumber,deskNumber,customerName,customerTel,address,oderTime,oderRemark,payAmount,payStatus,createDate from wx_diancai_dingdan_manage ");
            strSql.Append(" where id=@dingdan");
            SqlParameter[] parameters = {
					new SqlParameter("@dingdan", SqlDbType.NVarChar,200)
			};
            parameters[0].Value = dingdan;

            WeiXinPF.Model.wx_diancai_dingdan_manage model = new WeiXinPF.Model.wx_diancai_dingdan_manage();
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


        public WeiXinPF.Model.wx_diancai_dingdan_manage GetModeldingdan(int id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 id,shopinfoid,openid,wid,orderNumber,deskNumber,customerName,customerTel,address,oderTime,oderRemark,payAmount,payStatus,createDate from wx_diancai_dingdan_manage ");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)
			};
            parameters[0].Value = id;

            WeiXinPF.Model.wx_diancai_dingdan_manage model = new WeiXinPF.Model.wx_diancai_dingdan_manage();
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


        public DataSet GetListshop(int shopid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select id,shopinfoid,openid,wid,orderNumber,deskNumber,customerName,customerTel,address,oderTime,oderRemark,payAmount,payStatus,createDate ");
            strSql.Append(" FROM wx_diancai_dingdan_manage where shopinfoid='" + shopid + "' order by  createDate desc,id desc  ");

            return DbHelperSQL.Query(strSql.ToString());
        }


        public bool Updatestatus(string id, string state)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update  wx_diancai_dingdan_manage set payStatus='" + state + "'  ");

            strSql.Append(" where id='" + id + "' ");

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

        public bool UpdateCommoditystatus(string cid, string status)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update  wx_diancai_dingdan_commodity set status='" + status + "'  ");

            strSql.Append(" where id='" + cid + "' ");

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

        public bool UpdateCommodityStatusByOrderId(string orderId, string status)
        {
            var strSql = new StringBuilder();
            strSql.Append("update  wx_diancai_dingdan_commodity set status='" + status + "'  ");

            strSql.Append(" where dingId=" + orderId);

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

        public bool UpdateCommoditystatus(string ccode, int status)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update  wx_diancai_dingdan_commodity set status='" + status + "'  ");

            strSql.Append(" where identifyingcode='" + ccode + "' ");

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

        public bool Delete(string dingdan)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from wx_diancai_dingdan_manage ");
            strSql.Append(" where   id='" + dingdan + "' ");


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



        #endregion  ExtensionMethod
    }
}
