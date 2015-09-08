/**************************************
 *
 * 作者：李~~朴
 * 公司:上·海沐~雪网·络·科·技有·限·公~司
 * qq:2~3~0~0~2~8~0~7
 * website:http://uweixin.cn
 * taobao:https://item.taobao.com/item.htm?spm=686.1000925.0.0.5HYEHQ&id=520523216527  
 * createDate:2015-8-3
 * update:2015-8-3
 * 
 ***********************************/

using System;
using System.Data;
using System.Collections.Generic;
using WeiXinPF.Common;
using WeiXinPF.Model;
namespace WeiXinPF.BLL
{
	/// <summary>
	/// 现金红包活动表
	/// </summary>
	public partial class wx_xjhongbao_action
	{
		private readonly WeiXinPF.DAL.wx_xjhongbao_action dal=new WeiXinPF.DAL.wx_xjhongbao_action();
		public wx_xjhongbao_action()
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
		public int  Add(WeiXinPF.Model.wx_xjhongbao_action model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(WeiXinPF.Model.wx_xjhongbao_action model)
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
		public bool DeleteList(string idlist )
		{
			return dal.DeleteList(idlist );
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public WeiXinPF.Model.wx_xjhongbao_action GetModel(int id)
		{
			
			return dal.GetModel(id);
		}

		 

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
		public DataSet GetList(int Top,string strWhere,string filedOrder)
		{
			return dal.GetList(Top,strWhere,filedOrder);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<WeiXinPF.Model.wx_xjhongbao_action> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<WeiXinPF.Model.wx_xjhongbao_action> DataTableToList(DataTable dt)
		{
			List<WeiXinPF.Model.wx_xjhongbao_action> modelList = new List<WeiXinPF.Model.wx_xjhongbao_action>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				WeiXinPF.Model.wx_xjhongbao_action model;
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
			return dal.GetListByPage( strWhere,  orderby,  startIndex,  endIndex);
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

        /// <summary>
        /// 获得查询分页数据
        /// </summary>
        public DataSet GetList(int pageSize, int pageIndex, string strWhere, string filedOrder, out int recordCount)
        {
            return dal.GetList(pageSize, pageIndex, strWhere, filedOrder, out recordCount);
        }
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int id,int wid)
        {
            return dal.Exists(id,wid);
        }

        /// <summary>
        /// 是否存在关注红包
        /// </summary>
        /// <param name="wid"></param>
        /// <returns></returns>
        public bool ExistsSubscribeAction(int wid)
        {
            return dal.ExistsSubscribeAction(wid);

        }

        /// <summary>
        /// 是否存在关键词红包
        /// </summary>
        /// <param name="wid"></param>
        /// <param name="keywords"></param>
        /// <returns></returns>
        public bool ExistsKeyWordsAction(int wid, string keywords)
        {
            return dal.ExistsKeyWordsAction(wid, keywords);

        }


        /// <summary>
        /// 得到关注红包实体
        /// </summary>
        public WeiXinPF.Model.wx_xjhongbao_action GetGZHBModelByWid(int wid)
        {

            return dal.GetGZHBModelByWid(wid);
        }

        /// <summary>
        /// 获取关键词红包
        /// </summary>
        /// <param name="wid"></param>
        /// <param name="keywords"></param>
        /// <returns></returns>
        public WeiXinPF.Model.wx_xjhongbao_action GetKeyWordsModelByWid(int wid, string keywords)
        {
            return dal.GetKeyWordsModelByWid(  wid,   keywords);
        }

        /// <summary>
        /// 取网页版的现金红包
        /// </summary>
        /// <param name="id"></param>
        /// <param name="wid"></param>
        /// <returns></returns>
        public WeiXinPF.Model.wx_xjhongbao_action GetPageHongBaoModel(int id, int wid)
        {
            return dal.GetPageHongBaoModel(id, wid);
        }
		#endregion  ExtensionMethod
	}
}

