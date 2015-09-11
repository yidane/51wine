using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace WeiXinPF.BLL
{
    public class wx_travel_scenicSetting
    {
        private DAL.wx_travel_scenicSetting _dal = null;

         public wx_travel_scenicSetting()
        {
            if (_dal == null)
            {
                _dal = new DAL.wx_travel_scenicSetting();
            }
        }
        /// <summary>
        /// 增加一条数据
        /// </summary>
         public int Add(Model.wx_travel_scenicSetting model)
        {
            return _dal.Add((model));
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
         public bool Update(Model.wx_travel_scenicSetting model)
        {
            return _dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int id)
        {
            return _dal.Delete(id);
        }

        /// <summary>
        /// 批量删除数据
        /// </summary>
        public bool DeleteList(string idlist)
        {
            return _dal.DeleteList(idlist);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Model.wx_travel_scenicSetting GetModel(int id)
        {
            return _dal.GetModel(id);
        }

        public DataSet GetPageList(int pageSize, int pageIndex, string strWhere, string filedOrder, out int recordCount)
        {
            return _dal.GetPageList(pageSize, pageIndex, strWhere, filedOrder, out recordCount);
        }

        public Model.wx_travel_scenicSetting GetModelByScenicId(int scenicId)
        {
            return _dal.GetModelByScenicId(scenicId);
        }
    }
}
