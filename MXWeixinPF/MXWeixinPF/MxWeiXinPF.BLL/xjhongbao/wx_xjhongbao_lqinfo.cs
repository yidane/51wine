/**************************************
 *
 * 作者：李~~朴
 * 公司:上·海沐~雪网·络·科·技有·限·公·司
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
using MxWeiXinPF.Common;
using MxWeiXinPF.Model;
namespace MxWeiXinPF.BLL
{
	/// <summary>
	/// 现金红包领取记录表
	/// </summary>
	public partial class wx_xjhongbao_lqinfo
	{
		private readonly MxWeiXinPF.DAL.wx_xjhongbao_lqinfo dal=new MxWeiXinPF.DAL.wx_xjhongbao_lqinfo();
		public wx_xjhongbao_lqinfo()
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
		public int  Add(MxWeiXinPF.Model.wx_xjhongbao_lqinfo model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(MxWeiXinPF.Model.wx_xjhongbao_lqinfo model)
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
		public MxWeiXinPF.Model.wx_xjhongbao_lqinfo GetModel(int id)
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
		public List<MxWeiXinPF.Model.wx_xjhongbao_lqinfo> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<MxWeiXinPF.Model.wx_xjhongbao_lqinfo> DataTableToList(DataTable dt)
		{
			List<MxWeiXinPF.Model.wx_xjhongbao_lqinfo> modelList = new List<MxWeiXinPF.Model.wx_xjhongbao_lqinfo>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				MxWeiXinPF.Model.wx_xjhongbao_lqinfo model;
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
        /// 如果活动为一次性获奖情况， 判断用户是否获得过该活动的红包，如果有（并且获得的红包金额大于0），则返回true
        ///如果活动为每天一次获奖机会，判断用户当天是否获得过该活动的红包，如果有（并且获得的红包金额大于0），则返回true
        ///add by 李·朴  @沐……雪
        ///datetime:2015-8-3
        /// </summary>
        /// <param name="actionId">活动的id</param>
        /// <param name="openid">微信用户</param>
        /// <param name="isOnlyOne">一次性true,每天一次false</param>
        /// <returns>存在则返回true,不存在返回false</returns>
        public bool ExistsOpenid(int actionId, string openid, bool isOnlyOne)
        {
            return dal.ExistsOpenid(actionId, openid, isOnlyOne);
        }
		#endregion  ExtensionMethod
	}
}

