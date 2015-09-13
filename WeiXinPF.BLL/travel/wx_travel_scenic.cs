using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using WeiXinPF.Common;

namespace WeiXinPF.BLL
{
    public class wx_travel_scenic
    {
        private DAL.wx_travel_scenic _dal = null;

        public wx_travel_scenic()
        {
            if (_dal == null)
            {
                _dal = new DAL.wx_travel_scenic();
            }
        }
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Model.wx_travel_scenic model)
        {
            return _dal.Add((model));
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Model.wx_travel_scenic model)
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
        public bool DeleteList(int[] idlist)
        {
            return _dal.DeleteList(idlist);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Model.wx_travel_scenic GetModel(int id)
        {
            return _dal.GetModel(id);
        }

        public DataSet GetPageList(int pageSize, int pageIndex, string strWhere, string filedOrder, out int recordCount)
        {
            return _dal.GetPageList(pageSize, pageIndex, strWhere, filedOrder, out recordCount);
        }
    }
}
