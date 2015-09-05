﻿/**************************************
 *
 * 作者：李朴
 * 公司:上海沐 雪 网络 科技有限公司
 * qq:23002807
 * website:http://uweixin.cn
 * taobao:https://item.taobao.com/item.htm?spm=686.1000925.0.0.5HYEHQ&id=520523216527  
 * createDate:2013-11-1
 * update:2014-12-30
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
	/// 短信发送信息
	/// </summary>
	public partial class wx_sms_info
	{
		private readonly MxWeiXinPF.DAL.wx_sms_info dal=new MxWeiXinPF.DAL.wx_sms_info();
		public wx_sms_info()
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
		public int  Add(MxWeiXinPF.Model.wx_sms_info model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(MxWeiXinPF.Model.wx_sms_info model)
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
		public MxWeiXinPF.Model.wx_sms_info GetModel(int id)
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
		public List<MxWeiXinPF.Model.wx_sms_info> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<MxWeiXinPF.Model.wx_sms_info> DataTableToList(DataTable dt)
		{
			List<MxWeiXinPF.Model.wx_sms_info> modelList = new List<MxWeiXinPF.Model.wx_sms_info>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				MxWeiXinPF.Model.wx_sms_info model;
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
        /// 是否存在该验证码
        /// </summary
        public bool ExistsYzm(string tel, string yzmCode)
        {
            return dal.ExistsYzm(tel, yzmCode);

        }

		#endregion  ExtensionMethod
	}
}
