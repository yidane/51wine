﻿using System;
using System.Data;
using System.Collections.Generic;
using System.Transactions;
using System.Linq;
using WeiXinPF.Common;
using WeiXinPF.Model;
namespace WeiXinPF.BLL
{
    /// <summary>
    /// 订单管理
    /// </summary>
    public partial class wx_diancai_dingdan_manage
    {
        private readonly WeiXinPF.DAL.wx_diancai_dingdan_manage dal = new WeiXinPF.DAL.wx_diancai_dingdan_manage();
        public wx_diancai_dingdan_manage()
        { }
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
        public int Add(WeiXinPF.Model.wx_diancai_dingdan_manage model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(WeiXinPF.Model.wx_diancai_dingdan_manage model)
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
        public WeiXinPF.Model.wx_diancai_dingdan_manage GetModel(int id)
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
        /// 获得商品
        /// </summary>
        public DataSet GetCommodityList(string strWhere)
        {
            return dal.GetCommodityList(strWhere);
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
        public List<WeiXinPF.Model.wx_diancai_dingdan_manage> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<WeiXinPF.Model.wx_diancai_dingdan_manage> DataTableToList(DataTable dt)
        {
            List<WeiXinPF.Model.wx_diancai_dingdan_manage> modelList = new List<WeiXinPF.Model.wx_diancai_dingdan_manage>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                WeiXinPF.Model.wx_diancai_dingdan_manage model;
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

        public DataSet GetOrderList(int shopId, int pageSize, int pageIndex,
                                    DateTime beginDate, DateTime endDate, int payAmountMin,
                                    int payAmountMax, string orderNumber, string customerName, string customerTel, out int totalCount)
        {
            return dal.GetOrderList(shopId, pageSize, pageIndex, beginDate, endDate, payAmountMin, payAmountMax, orderNumber, customerName, customerTel, out totalCount);
        }

        public string GetOrderCaipinDetail(int orderId)
        {
            return dal.GetOrderCaipinDetail(orderId);
        }

        public List<wx_diancai_credentials_detail> GetCredentialsList(int shopid, string condition, string moduleName, out double totalAmount)
        {
            DataSet ds = dal.GetCredentialsList(shopid, condition, moduleName, out totalAmount);
            return ds.Tables[0].ToObject<wx_diancai_credentials_detail>().ToList();
        }

        public DataSet GetCredentialsCommodityList(int dingId, string moduleName)
        {
            return dal.GetCredentialsCommodityList(dingId, moduleName);
        }

        public DataSet GetPayList(string openid)
        {
            return dal.GetPayList(openid);
        }

        public DataSet GetMyOrderInShop(string openid, int shopid)
        {
            return dal.GetMyOrderInShop(openid, shopid);
        }

        public DataSet GetDingdanRefundDetail(int shopid, int dingdanid, string openid, int caiid)
        {
            return dal.GetDingdanRefundDetail(shopid, dingdanid, openid, caiid);
        }

        public void RefundDiancai(int shopinfiId, string openid, int wid, int refundAmount, int dingdanid, int caiid, List<Guid> caipinIdList)
        {
            dal.RefundDiancai(shopinfiId, openid, wid, refundAmount, dingdanid, caiid, caipinIdList);
        }

        public void PaySuccess(string orderNumber)
        {
            using (var scope = new TransactionScope())
            {
                dal.PaySuccess(orderNumber);
                dal.UpdateCommodityStatusByOrderCode(orderNumber, "1");

                scope.Complete();
            }
        }

        public void OrderFinish(int shopId,int orderId)
        {
            dal.OrderFinish(shopId, orderId);
        }

        public bool Update(int id, decimal payAmount)
        {
            return dal.Update(id, payAmount);
        }

        public DataSet Getcaopin(string dingdan)
        {
            return dal.Getcaopin(dingdan);
        }

        public void AfterVerification(int wid, int shopId, int orderID)
        {
            dal.AfterVerification(wid, shopId, orderID);
        }

        public DataSet Getcaopin(int id)
        {
            return dal.Getcaopin(id);
        }

        public DataSet Getcommodity(string cid)
        {
            return dal.Getcommodity(cid);
        }

        public DataSet GetcommodityTable(string did)
        {
            return dal.GetcommodityTable(did);
        }

        public DataSet GetOrderDetail(int orderId)
        {
            return dal.GetOrderDetail(orderId);
        }

        public WeiXinPF.Model.wx_diancai_dingdan_manage GetModeldingdan(string dingdan)
        {

            return dal.GetModeldingdan(dingdan);
        }

        public WeiXinPF.Model.wx_diancai_dingdan_manage GetModeldingdan(int id)
        {

            return dal.GetModeldingdan(id);
        }

        public DataSet GetListshop(int shopid)
        {
            return dal.GetListshop(shopid);
        }


        public bool Updatestatus(string id, string state)
        {
            return dal.Updatestatus(id, state);
        }
        public bool UpdateCommoditystatus(string cid, string status)
        {
            return dal.UpdateCommoditystatus(cid, status);
        }
        /// <summary>
        /// 修改订菜商品状态
        /// </summary>
        /// <param name="ccode">订单编号</param>
        /// <param name="status">状态类型</param>
        /// <returns></returns>
        public bool UpdateCommoditystatus(string ccode, int status)
        {
            bool result = dal.UpdateCommoditystatus(ccode, status);
            string id = GetCommodityList("identifyingcode='" + ccode + "'").Tables[0].Rows[0]["id"].ToString();
            DataSet dr = GetcommodityTable(id);
            DataTable dt = dr.Tables[0];
            int sum = 0;
            int paid = 0;
            if (dt.Rows.Count >= 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i]["status"].ToString() == "1")
                        paid++;
                    else if (dt.Rows[i]["status"].ToString() == "2")
                        sum++;
                }
                if (sum == dt.Rows.Count)
                {
                    Updatestatus(id, RestaurantCommodityStatus.Used.ToString());
                }
                if (paid == dt.Rows.Count)
                {
                    Updatestatus(id, RestaurantCommodityStatus.Paid.ToString());
                }
            }
            return result;
        }

        public bool UpdateCommodityStatusByOrderCode(string orderCode, string status)
        {
            return dal.UpdateCommodityStatusByOrderCode(orderCode, status);
        }

        public bool Delete(string dingdan)
        {

            return dal.Delete(dingdan);
        }

        #endregion  ExtensionMethod
    }
}

