using System;
using System.Data;
using System.Collections.Generic;
using WeiXinPF.Common;
using WeiXinPF.Model;
using WeiXinPF.Model.KNSHotel;

namespace WeiXinPF.BLL
{
	/// <summary>
	/// 数据表5
	/// </summary>
	public partial class wx_hotel_dingdan
	{
		private readonly WeiXinPF.DAL.wx_hotel_dingdan dal=new WeiXinPF.DAL.wx_hotel_dingdan();
		public wx_hotel_dingdan()
		{}
        #region  BasicMethod

        /// <summary>
        /// 得到最大ID
        /// </summary>
        public int GetMaxId()
        {
            return dal.GetMaxId();
        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int id)
        {
            return dal.Exists(id);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(WeiXinPF.Model.wx_hotel_dingdan model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(WeiXinPF.Model.wx_hotel_dingdan model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int id)
        {

            return dal.Delete(id);
        }
        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool DeleteList(string idlist)
        {
            return dal.DeleteList(idlist);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public WeiXinPF.Model.wx_hotel_dingdan GetModel(int id)
        {

            return dal.GetModel(id);
        }

        /// <summary>
        /// 得到一个对象实体，从缓存中
        /// </summary>


        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            return dal.GetList(strWhere);
        }
        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            return dal.GetList(Top, strWhere, filedOrder);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<WeiXinPF.Model.wx_hotel_dingdan> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<WeiXinPF.Model.wx_hotel_dingdan> DataTableToList(DataTable dt)
        {
            List<WeiXinPF.Model.wx_hotel_dingdan> modelList = new List<WeiXinPF.Model.wx_hotel_dingdan>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                WeiXinPF.Model.wx_hotel_dingdan model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = dal.DataRowToModel(dt.Rows[n]);
                    if (model != null)
                    {
                        modelList.Add(model);
                    }
                }
            }
            return modelList;
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetAllList()
        {
            return GetList("");
        }

        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public int GetRecordCount(string strWhere)
        {
            return dal.GetRecordCount(strWhere);
        }
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
        {
            return dal.GetListByPage(strWhere, orderby, startIndex, endIndex);
        }
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        //public DataSet GetList(int PageSize,int PageIndex,string strWhere)
        //{
        //return dal.GetList(PageSize,PageIndex,strWhere);
        //}

        #endregion  BasicMethod
		#region  ExtensionMethod
        public DataSet GetList(int pageSize, int pageIndex, string strWhere, string filedOrder, out int recordCount)
        {
            return dal.GetList(pageSize, pageIndex, strWhere, filedOrder, out recordCount);
        }
        public DataSet GetList(string openid,int hotelid,string type="all")
        {
            DataSet result = null;
            if (type=="all")
            {
                result= dal.GetListWithSql(openid, hotelid, "", " order by orderTime  desc ");
            }
            else if (type== "pay")
            {
                
                result = dal.GetListWithSql(openid, hotelid, " and   orderStatus =3", " order by orderTime   desc");
            }
            else if (type== "refund")
            {
                result = dal.GetListWithSql(openid, hotelid, " and   orderStatus =7", " order by orderTime  desc ");
            }
            

            return result;
        }

        public bool Updatehotel(WeiXinPF.Model.wx_hotel_dingdan model)
        {
            return dal.Updatehotel(model);
        }

        public bool Update(int dingdanid, string status)
        {
            return dal.Update(dingdanid, status);
        }

        public bool Update(int dingdanid)
        {
            return dal.Update(dingdanid);
        }
		#endregion  ExtensionMethod

        /// <summary>
        /// 获取用户订单
        /// </summary>
        /// <param name="openid"></param>
        /// <param name="wid"></param>
        /// <param name="type"></param>
        /// <returns></returns>
	    public DataSet GetUserOrderList(string openid, int wid, string type)
	    {
            DataSet result = null;
            if (type == "all")
            {
                result = dal.GetUserOrderList(openid, wid,"", " order by orderTime  desc ");
            }
            else if (type == "pay")
            {

                result = dal.GetUserOrderList(openid, wid, " orderStatus =3", " order by orderTime desc ");
            }
            else if (type == "refund")
            {
                result = dal.GetUserOrderList(openid, wid, " orderStatus =7", " order by orderTime  desc ");
            }


            return result;
        }


       /// <summary>
       /// 获取用户保存过的信息
       /// </summary>
       /// <param name="openid"></param>
       /// <returns></returns>
        public WeiXinPF.Model.wx_hotel_dingdan GetLastUserModel(string openid)
        {

            return dal.GetLastUserModel(openid);
        }

        /// <summary>
        /// 支付完成后更新状态
        /// </summary>
        /// <param name="outTradeNo">订单号</param>
	    public void PaySuccess(string outTradeNo)
        {
            var model = this.GetModel(outTradeNo);
            if (model!=null)
            {
                this.Update(model.id,  HotelStatusManager.OrderStatus.Payed.StatusId.ToString() );
            }
	        
	    }

        /// <summary>
        /// 根据订单号获取模型
        /// </summary>
        /// <param name="outTradeNo"></param>
        /// <returns></returns>
	    private WeiXinPF.Model.wx_hotel_dingdan GetModel(string outTradeNo)
	    {
            return dal.GetModel(outTradeNo);
        }

        /// <summary>
        /// 获取微信所必须的退单参数
        /// </summary>
        /// <param name="wid"></param>
        /// <param name="hotelid"></param>
        /// <param name="dingdanId"></param>
        /// <param name="modelName"></param>
        /// <returns></returns>
        public DataSet GetWeChatRefundParams(int wid, int hotelid, int dingdanId,string refundCode, string modelName= "Hotel")
        {
            return dal.GetWeChatRefundParams(wid, hotelid, dingdanId, refundCode, modelName);
 
        }

        /// <summary>
        /// 退单完成后改变订单状态
        /// </summary>
        /// <param name="refundCode">订单号</param>
	    public void RefundComplete(string outTradeNo)
	    {
            var model = this.GetModel(outTradeNo);
            if (model != null)
            {
                this.Update(model.id, HotelStatusManager.OrderStatus.Refunded.StatusId.ToString());
            }
        }
	}
}

