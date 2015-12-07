using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using WeiXinPF.DBUtility;
using WeiXinPF.Common;//Please add references
using Dapper;
using System.Linq;
using System.Collections.Generic;

namespace WeiXinPF.DAL
{
    /// <summary>
    /// 数据访问类:wx_diancai_shopinfo
    /// </summary>
    public partial class wx_diancai_shopinfo
    {
        public wx_diancai_shopinfo()
        { }
        #region  BasicMethod

        /// <summary>
        /// 得到最大ID
        /// </summary>
        public int GetMaxId()
        {
            return DbHelperSQL.GetMaxID("id", "wx_diancai_shopinfo");
        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from wx_diancai_shopinfo");
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
        public int Add(WeiXinPF.Model.wx_diancai_shopinfo model)
        {
            StringBuilder query = new StringBuilder();
            query.Append("insert into wx_diancai_shopinfo(");
            query.Append("wid,hotelName,hotelLogo,hoteltimeBegin,hoteltimeEnd,limiteOrder,dcRename,sendPrice,sendCost,freeSendcost,radius,sendArea,tel,address,personLimite,notice,hotelintroduction,email,emailpwd,stmp,css,createDate,kcType,miaoshu,xplace,yplace,hoteltimeBegin1,hoteltimeEnd1,hoteltimeBegin2,hoteltimeEnd2,Operator,Recommend,Mobile)");
            query.Append(" values (");
            query.Append("@wid,@hotelName,@hotelLogo,@hoteltimeBegin,@hoteltimeEnd,@limiteOrder,@dcRename,@sendPrice,@sendCost,@freeSendcost,@radius,@sendArea,@tel,@address,@personLimite,@notice,@hotelintroduction,@email,@emailpwd,@stmp,@css,@createDate,@kcType,@miaoshu,@xplace,@yplace,@hoteltimeBegin1,@hoteltimeEnd1,@hoteltimeBegin2,@hoteltimeEnd2,@Operator,@Recommend,@Mobile)");
            query.Append("Select @Id=Scope_Identity();");
            query.Append("Update dbo.wx_diancai_shopinfo Set ShopCode=Right('0000'+Cast(@Id As Varchar(10)),4) Where Id=@Id");

            using (IDbConnection db = DbFactory.GetOpenedConnection())
            {
                DynamicParameters dynamicParameters = new DynamicParameters();
                dynamicParameters.AddDynamicParams(model);
                dynamicParameters.Add("@Id", dbType: DbType.Int32, direction: ParameterDirection.Output);

                db.Execute(query.ToString(), dynamicParameters);

                return dynamicParameters.Get<int>("@Id");
            }
            //SqlParameter[] parameters = {
            //        new SqlParameter("@wid", SqlDbType.Int,4),
            //        new SqlParameter("@hotelName", SqlDbType.VarChar,500),
            //        new SqlParameter("@hotelLogo", SqlDbType.VarChar,200),
            //        new SqlParameter("@hoteltimeBegin", SqlDbType.DateTime),
            //        new SqlParameter("@hoteltimeEnd", SqlDbType.DateTime),
            //        new SqlParameter("@limiteOrder", SqlDbType.Bit,1),
            //        new SqlParameter("@dcRename", SqlDbType.VarChar,200),
            //        new SqlParameter("@sendPrice", SqlDbType.Float,8),
            //        new SqlParameter("@sendCost", SqlDbType.Float,8),
            //        new SqlParameter("@freeSendcost", SqlDbType.Int,4),
            //        new SqlParameter("@radius", SqlDbType.VarChar,200),
            //        new SqlParameter("@sendArea", SqlDbType.VarChar,200),
            //        new SqlParameter("@tel", SqlDbType.VarChar,100),
            //        new SqlParameter("@address", SqlDbType.VarChar,200),
            //        new SqlParameter("@personLimite", SqlDbType.Int,4),
            //        new SqlParameter("@notice", SqlDbType.VarChar,2000),
            //        new SqlParameter("@hotelintroduction", SqlDbType.VarChar,2000),
            //        new SqlParameter("@email", SqlDbType.VarChar,200),
            //        new SqlParameter("@emailpwd", SqlDbType.VarChar,100),
            //        new SqlParameter("@stmp", SqlDbType.VarChar,100),
            //        new SqlParameter("@css", SqlDbType.VarChar,200),
            //        new SqlParameter("@createDate", SqlDbType.DateTime),
            //        new SqlParameter("@kcType", SqlDbType.VarChar,4000),
            //        new SqlParameter("@miaoshu", SqlDbType.VarChar,200),
            //        new SqlParameter("@xplace", SqlDbType.Float,8),
            //        new SqlParameter("@yplace", SqlDbType.Float,8),
            //        new SqlParameter("@hoteltimeBegin1", SqlDbType.DateTime),
            //        new SqlParameter("@hoteltimeEnd1", SqlDbType.DateTime),
            //        new SqlParameter("@hoteltimeBegin2", SqlDbType.DateTime),
            //        new SqlParameter("@hoteltimeEnd2", SqlDbType.DateTime),
            //        new SqlParameter("@Operator", SqlDbType.VarChar),
            //        new SqlParameter("@Recommend", SqlDbType.Bit),
            //        new SqlParameter("@Mobile",SqlDbType.VarChar)
            //};
            //parameters[0].Value = model.wid;
            //parameters[1].Value = model.hotelName;
            //parameters[2].Value = model.hotelLogo;
            //parameters[3].Value = model.hoteltimeBegin;
            //parameters[4].Value = model.hoteltimeEnd;
            //parameters[5].Value = model.limiteOrder;
            //parameters[6].Value = model.dcRename;
            //parameters[7].Value = model.sendPrice;
            //parameters[8].Value = model.sendCost;
            //parameters[9].Value = model.freeSendcost;
            //parameters[10].Value = model.radius;
            //parameters[11].Value = model.sendArea;
            //parameters[12].Value = model.tel;
            //parameters[13].Value = model.address;
            //parameters[14].Value = model.personLimite;
            //parameters[15].Value = model.notice;
            //parameters[16].Value = model.hotelintroduction;
            //parameters[17].Value = model.email;
            //parameters[18].Value = model.emailpwd;
            //parameters[19].Value = model.stmp;
            //parameters[20].Value = model.css;
            //parameters[21].Value = model.createDate;
            //parameters[22].Value = model.kcType;
            //parameters[23].Value = model.miaoshu;
            //parameters[24].Value = model.xplace;
            //parameters[25].Value = model.yplace;
            //parameters[26].Value = model.hoteltimeBegin1;
            //parameters[27].Value = model.hoteltimeEnd1;
            //parameters[28].Value = model.hoteltimeBegin2;
            //parameters[29].Value = model.hoteltimeEnd2;
            //parameters[30].Value = model.Operator;
            //parameters[31].Value = model.Recommend;
            //parameters[32].Value = model.Mobile;

            //object obj = DbHelperSQL.GetSingle(strSql.ToString(), parameters);
            //if (obj == null)
            //{
            //    return 0;
            //}
            //else
            //{
            //    return Convert.ToInt32(obj);
            //}
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(WeiXinPF.Model.wx_diancai_shopinfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update wx_diancai_shopinfo set ");
            strSql.Append("wid=@wid,");
            strSql.Append("hotelName=@hotelName,");
            strSql.Append("hotelLogo=@hotelLogo,");
            strSql.Append("hoteltimeBegin=@hoteltimeBegin,");
            strSql.Append("hoteltimeEnd=@hoteltimeEnd,");
            strSql.Append("limiteOrder=@limiteOrder,");
            strSql.Append("dcRename=@dcRename,");
            strSql.Append("sendPrice=@sendPrice,");
            strSql.Append("sendCost=@sendCost,");
            strSql.Append("freeSendcost=@freeSendcost,");
            strSql.Append("radius=@radius,");
            strSql.Append("sendArea=@sendArea,");
            strSql.Append("tel=@tel,");
            strSql.Append("address=@address,");
            strSql.Append("personLimite=@personLimite,");
            strSql.Append("notice=@notice,");
            strSql.Append("hotelintroduction=@hotelintroduction,");
            strSql.Append("email=@email,");
            strSql.Append("emailpwd=@emailpwd,");
            strSql.Append("stmp=@stmp,");
            strSql.Append("css=@css,");
            //strSql.Append("createDate=@createDate,");
            strSql.Append("kcType=@kcType,");
            strSql.Append("miaoshu=@miaoshu,");
            strSql.Append("xplace=@xplace,");
            strSql.Append("yplace=@yplace,");
            strSql.Append("hoteltimeBegin1=@hoteltimeBegin1,");
            strSql.Append("hoteltimeEnd1=@hoteltimeEnd1,");
            strSql.Append("hoteltimeBegin2=@hoteltimeBegin2,");
            strSql.Append("hoteltimeEnd2=@hoteltimeEnd2,");
            strSql.Append("Operator=@Operator,");
            strSql.Append("Recommend=@Recommend,");
            strSql.Append("Mobile=@Mobile");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
                    new SqlParameter("@wid", SqlDbType.Int,4),
                    new SqlParameter("@hotelName", SqlDbType.VarChar,500),
                    new SqlParameter("@hotelLogo", SqlDbType.VarChar,200),
                    new SqlParameter("@hoteltimeBegin", SqlDbType.DateTime),
                    new SqlParameter("@hoteltimeEnd", SqlDbType.DateTime),
                    new SqlParameter("@limiteOrder", SqlDbType.Bit,1),
                    new SqlParameter("@dcRename", SqlDbType.VarChar,200),
                    new SqlParameter("@sendPrice", SqlDbType.Float,8),
                    new SqlParameter("@sendCost", SqlDbType.Float,8),
                    new SqlParameter("@freeSendcost", SqlDbType.Int,4),
                    new SqlParameter("@radius", SqlDbType.VarChar,200),
                    new SqlParameter("@sendArea", SqlDbType.VarChar,200),
                    new SqlParameter("@tel", SqlDbType.VarChar,100),
                    new SqlParameter("@address", SqlDbType.VarChar,200),
                    new SqlParameter("@personLimite", SqlDbType.Int,4),
                    new SqlParameter("@notice", SqlDbType.VarChar,2000),
                    new SqlParameter("@hotelintroduction", SqlDbType.VarChar,2000),
                    new SqlParameter("@email", SqlDbType.VarChar,200),
                    new SqlParameter("@emailpwd", SqlDbType.VarChar,100),
                    new SqlParameter("@stmp", SqlDbType.VarChar,100),
                    new SqlParameter("@css", SqlDbType.VarChar,200),
					//new SqlParameter("@createDate", SqlDbType.DateTime),
					new SqlParameter("@kcType", SqlDbType.VarChar,4000),
                    new SqlParameter("@miaoshu", SqlDbType.VarChar,200),
                    new SqlParameter("@xplace", SqlDbType.Float,8),
                    new SqlParameter("@yplace", SqlDbType.Float,8),
                    new SqlParameter("@hoteltimeBegin1", SqlDbType.DateTime),
                    new SqlParameter("@hoteltimeEnd1", SqlDbType.DateTime),
                    new SqlParameter("@hoteltimeBegin2", SqlDbType.DateTime),
                    new SqlParameter("@hoteltimeEnd2", SqlDbType.DateTime),
                    new SqlParameter("@Operator", SqlDbType.VarChar),
                    new SqlParameter("@Recommend", SqlDbType.Bit),
                    new SqlParameter("@Mobile",SqlDbType.VarChar),
                    new SqlParameter("@id", SqlDbType.Int,4)};
            parameters[0].Value = model.wid;
            parameters[1].Value = model.hotelName;
            parameters[2].Value = model.hotelLogo;
            parameters[3].Value = model.hoteltimeBegin;
            parameters[4].Value = model.hoteltimeEnd;
            parameters[5].Value = model.limiteOrder;
            parameters[6].Value = model.dcRename;
            parameters[7].Value = model.sendPrice;
            parameters[8].Value = model.sendCost;
            parameters[9].Value = model.freeSendcost;
            parameters[10].Value = model.radius;
            parameters[11].Value = model.sendArea;
            parameters[12].Value = model.tel;
            parameters[13].Value = model.address;
            parameters[14].Value = model.personLimite;
            parameters[15].Value = model.notice;
            parameters[16].Value = model.hotelintroduction;
            parameters[17].Value = model.email;
            parameters[18].Value = model.emailpwd;
            parameters[19].Value = model.stmp;
            parameters[20].Value = model.css;
            //parameters[21].Value = model.createDate;
            parameters[21].Value = model.kcType;
            parameters[22].Value = model.miaoshu;
            parameters[23].Value = model.xplace;
            parameters[24].Value = model.yplace;
            parameters[25].Value = model.hoteltimeBegin1;
            parameters[26].Value = model.hoteltimeEnd1;
            parameters[27].Value = model.hoteltimeBegin2;
            parameters[28].Value = model.hoteltimeEnd2;
            parameters[29].Value = model.Operator;
            parameters[30].Value = model.Recommend;
            parameters[31].Value = model.Mobile;
            parameters[32].Value = model.id;

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
            strSql.Append("delete from wx_diancai_shopinfo ");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
                    new SqlParameter("@id", SqlDbType.Int,4)
            };
            parameters[0].Value = id;

            int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);

            //删除wx_diancai_shoppic表
            string wx_diancai_shoppic = "delete from  wx_diancai_shoppic where shopid='" + id + "' ";
            DbHelperSQL.ExecuteSql(wx_diancai_shoppic.ToString());

            //删除wx_diancai_shop_setup表   wx_diancai_shop_advertisement
            string wx_diancai_shop_setup = "delete from  wx_diancai_shop_setup where shopid='" + id + "'";
            DbHelperSQL.ExecuteSql(wx_diancai_shop_setup.ToString());

            //删除wx_diancai_dingdan_manage表
            string wx_diancai_dingdan_manage = "delete from  wx_diancai_dingdan_manage where shopinfoid='" + id + "'";
            DbHelperSQL.ExecuteSql(wx_diancai_dingdan_manage.ToString());

            //删除wx_diancai_caipin_category表
            string wx_diancai_caipin_category = "delete from  wx_diancai_caipin_category where shopid='" + id + "'";
            DbHelperSQL.ExecuteSql(wx_diancai_caipin_category.ToString());


            //删除wx_diancai_caipin_manage 表
            string wx_diancai_caipin_manage = "delete from  wx_diancai_caipin_manage where shopid='" + id + "'";
            DbHelperSQL.ExecuteSql(wx_diancai_caipin_manage.ToString());

            //删除wx_diancai_desknum 表
            string wx_diancai_desknum = "delete from  wx_diancai_desknum where shopid='" + id + "'";
            DbHelperSQL.ExecuteSql(wx_diancai_desknum.ToString());

            //删除wx_diancai_dianyuan表
            string wx_diancai_dianyuan = "delete from  wx_diancai_dianyuan where shopid='" + id + "'";
            DbHelperSQL.ExecuteSql(wx_diancai_dianyuan.ToString());

            //删除wx_diancai_form_control
            string wx_diancai_form_control = "delete from  wx_diancai_form_control where shopinfoId='" + id + "'";
            DbHelperSQL.ExecuteSql(wx_diancai_form_control.ToString());

            //删除wx_diancai_form_result表   wx_diancai_dingdan_caiping
            string wx_diancai_form_result = "delete from  wx_diancai_form_result where shopinfoId='" + id + "'";
            DbHelperSQL.ExecuteSql(wx_diancai_form_result.ToString());

            //删除wx_diancai_blacklist表 
            string wx_diancai_blacklist = "delete from  wx_diancai_blacklist where shopid='" + id + "'";
            DbHelperSQL.ExecuteSql(wx_diancai_blacklist.ToString());

            //删除wx_diancai_member表
            string wx_diancai_member = "delete from  wx_diancai_member where shopid='" + id + "'";
            DbHelperSQL.ExecuteSql(wx_diancai_member.ToString());

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
            strSql.Append("delete from wx_diancai_shopinfo ");
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
        public WeiXinPF.Model.wx_diancai_shopinfo GetModel(int id)
        {

            //StringBuilder strSql = new StringBuilder();
            //strSql.Append("select  top 1 id,wid,hotelName,hotelLogo,hoteltimeBegin,hoteltimeEnd,limiteOrder,dcRename,sendPrice,sendCost,freeSendcost,radius,sendArea,tel,address,personLimite,notice,hotelintroduction,email,emailpwd,stmp,css,createDate,kcType,miaoshu,xplace,yplace,hoteltimeBegin1,hoteltimeEnd1,hoteltimeBegin2,hoteltimeEnd2,Operator,Recommend,Mobile from wx_diancai_shopinfo ");
            //strSql.Append(" where id=@id");
            //SqlParameter[] parameters = {
            //        new SqlParameter("@id", SqlDbType.Int,4)
            //};
            //parameters[0].Value = id;

            //WeiXinPF.Model.wx_diancai_shopinfo model = new WeiXinPF.Model.wx_diancai_shopinfo();
            //DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            //if (ds.Tables[0].Rows.Count > 0)
            //{
            //    return DataRowToModel(ds.Tables[0].Rows[0]);
            //}
            //else
            //{
            //    return null;
            //}
            using (IDbConnection db = DbFactory.GetOpenedConnection())
            {
                string query = "Select * From wx_diancai_shopinfo Where Id=@Id";
                return db.Query<Model.wx_diancai_shopinfo>(query, new { Id = id }).FirstOrDefault();
            }
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public WeiXinPF.Model.wx_diancai_shopinfo DataRowToModel(DataRow row)
        {
            WeiXinPF.Model.wx_diancai_shopinfo model = new WeiXinPF.Model.wx_diancai_shopinfo();
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
                if (row["hotelName"] != null)
                {
                    model.hotelName = row["hotelName"].ToString();
                }
                if (row["hotelLogo"] != null)
                {
                    model.hotelLogo = row["hotelLogo"].ToString();
                }
                if (row["limiteOrder"] != null && row["limiteOrder"].ToString() != "")
                {
                    if ((row["limiteOrder"].ToString() == "1") || (row["limiteOrder"].ToString().ToLower() == "true"))
                    {
                        model.limiteOrder = true;
                    }
                    else
                    {
                        model.limiteOrder = false;
                    }
                }
                if (row["dcRename"] != null)
                {
                    model.dcRename = row["dcRename"].ToString();
                }
                if (row["sendPrice"] != null && row["sendPrice"].ToString() != "")
                {
                    model.sendPrice = decimal.Parse(row["sendPrice"].ToString());
                }
                if (row["sendCost"] != null && row["sendCost"].ToString() != "")
                {
                    model.sendCost = decimal.Parse(row["sendCost"].ToString());
                }
                if (row["freeSendcost"] != null && row["freeSendcost"].ToString() != "")
                {
                    model.freeSendcost = int.Parse(row["freeSendcost"].ToString());
                }
                if (row["radius"] != null)
                {
                    model.radius = row["radius"].ToString();
                }
                if (row["sendArea"] != null)
                {
                    model.sendArea = row["sendArea"].ToString();
                }
                if (row["tel"] != null)
                {
                    model.tel = row["tel"].ToString();
                }
                if (row["address"] != null)
                {
                    model.address = row["address"].ToString();
                }
                if (row["personLimite"] != null && row["personLimite"].ToString() != "")
                {
                    model.personLimite = int.Parse(row["personLimite"].ToString());
                }
                if (row["notice"] != null)
                {
                    model.notice = row["notice"].ToString();
                }
                if (row["hotelintroduction"] != null)
                {
                    model.hotelintroduction = row["hotelintroduction"].ToString();
                }
                if (row["email"] != null)
                {
                    model.email = row["email"].ToString();
                }
                if (row["emailpwd"] != null)
                {
                    model.emailpwd = row["emailpwd"].ToString();
                }
                if (row["stmp"] != null)
                {
                    model.stmp = row["stmp"].ToString();
                }
                if (row["css"] != null)
                {
                    model.css = row["css"].ToString();
                }
                if (row["createDate"] != null && row["createDate"].ToString() != "")
                {
                    model.createDate = DateTime.Parse(row["createDate"].ToString());
                }
                if (row["kcType"] != null)
                {
                    model.kcType = row["kcType"].ToString();
                }
                if (row["miaoshu"] != null)
                {
                    model.miaoshu = row["miaoshu"].ToString();
                }
                if (row["xplace"] != null && row["xplace"].ToString() != "")
                {
                    model.xplace = decimal.Parse(row["xplace"].ToString());
                }
                if (row["yplace"] != null && row["yplace"].ToString() != "")
                {
                    model.yplace = decimal.Parse(row["yplace"].ToString());
                }

                model.hoteltimeBegin = ToDateTime(row["hoteltimeBegin"]);
                model.hoteltimeEnd = ToDateTime(row["hoteltimeEnd"]);
                model.hoteltimeBegin1 = ToDateTime(row["hoteltimeBegin1"]);
                model.hoteltimeEnd1 = ToDateTime(row["hoteltimeEnd1"]);
                model.hoteltimeBegin2 = ToDateTime(row["hoteltimeBegin2"]);
                model.hoteltimeEnd2 = ToDateTime(row["hoteltimeEnd2"]);

                if (row["hoteltimeEnd2"] != null && row["hoteltimeEnd2"].ToString() != "")
                {
                    model.hoteltimeEnd2 = DateTime.Parse(row["hoteltimeEnd2"].ToString());
                }

                if (row["Operator"] != null && row["Operator"].ToString() != "")
                {
                    model.Operator = row["Operator"].ToString();
                }

                if (row["Recommend"] != null && row["Recommend"].ToString() != "")
                {
                    model.Recommend = row.Field<bool>("Recommend");
                }

                if (row["Mobile"] != null && row["Mobile"].ToString() != "")
                {
                    model.Mobile = row["Mobile"].ToString();
                }
            }
            return model;
        }

        public DateTime? ToDateTime(object obj)
        {
            if (obj == null || obj == DBNull.Value)
                return null;
            DateTime datetimeOut = DateTime.Now;
            if (DateTime.TryParse(obj.ToString(), out datetimeOut))
            {
                return datetimeOut;
            }
            return null;
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Model.wx_diancai_shopinfo> GetList(string strWhere)
        {
            string query = "Select * FROM wx_diancai_shopinfo";
            if (strWhere.Trim() != "")
            {
                query += " where " + strWhere;
            }
            using (IDbConnection db = DbFactory.GetOpenedConnection())
            {
                return db.Query<Model.wx_diancai_shopinfo>(query).ToList();
            }
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
            strSql.Append(" id,wid,hotelName,hotelLogo,hoteltimeBegin,hoteltimeEnd,limiteOrder,dcRename,sendPrice,sendCost,freeSendcost,radius,sendArea,tel,address,personLimite,notice,hotelintroduction,email,emailpwd,stmp,css,createDate,kcType,miaoshu,xplace,yplace,hoteltimeBegin1,hoteltimeEnd1,hoteltimeBegin2,hoteltimeEnd2,Operator,Recommend,Mobile,ShopCode ");
            strSql.Append(" FROM wx_diancai_shopinfo ");
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
            strSql.Append("select count(1) FROM wx_diancai_shopinfo ");
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
            strSql.Append(")AS Row, T.*  from wx_diancai_shopinfo T ");
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
            parameters[0].Value = "wx_diancai_shopinfo";
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
            strSql.Append("select id,wid,hotelName,hotelLogo,hoteltimeBegin,hoteltimeEnd,limiteOrder,dcRename,sendPrice,sendCost,freeSendcost,radius,sendArea,tel,address,personLimite,notice,hotelintroduction,email,emailpwd,stmp,css,createDate,kcType,miaoshu,xplace,yplace,hoteltimeBegin1,hoteltimeEnd1,hoteltimeBegin2,hoteltimeEnd2,Operator,Recommend,Mobile,ShopCode ");
            strSql.Append(" FROM wx_diancai_shopinfo ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }

            recordCount = Convert.ToInt32(DbHelperSQL.GetSingle(PagingHelper.CreateCountingSql(strSql.ToString())));
            return DbHelperSQL.Query(PagingHelper.CreatePagingSql(recordCount, pageSize, pageIndex, strSql.ToString(), filedOrder));
        }



        public DataSet GetList()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select id,wid,hotelName,hotelLogo,hoteltimeBegin,hoteltimeEnd,limiteOrder,dcRename,sendPrice,sendCost,freeSendcost,radius,sendArea,tel,address,personLimite,notice,hotelintroduction,email,emailpwd,stmp,css,createDate,kcType,miaoshu,xplace,yplace,hoteltimeBegin1,hoteltimeEnd1,hoteltimeBegin2,hoteltimeEnd2,Operator,Recommend,Mobile,ShopCode ");
            strSql.Append(" FROM wx_diancai_shopinfo ");

            return DbHelperSQL.Query(strSql.ToString());
        }


        #endregion  ExtensionMethod
    }
}

