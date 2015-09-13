using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace WeiXinPF.BLL
{
    public class wx_travel_scenicDetail
    {
        private DAL.wx_travel_scenicDetail _dal = null;

        public wx_travel_scenicDetail()
        {
            if (_dal == null)
            {
                _dal = new DAL.wx_travel_scenicDetail();
            }
        }

        public int Add(Model.wx_travel_scenicDetail model)
        {
            return _dal.Add(model);
        }

        public bool Update(Model.wx_travel_scenicDetail model)
        {
            return _dal.Update(model);
        }

        public bool Delete(int id)
        {
            return _dal.Delete(id);
        }

        public Model.wx_travel_scenicDetail GetModel(int id)
        {
            return _dal.GetModel(id);
        }

        public List<Model.wx_travel_scenicDetail> GetModelByScenicId(int scenicId)
        {
            return _dal.GetModelByScenicId(scenicId);
        }

        public DataSet GetPageList(int pageSize, int pageIndex, string strWhere, string filedOrder, out int recordCount)
        {
            return _dal.GetPageList(pageSize, pageIndex, strWhere, filedOrder, out recordCount);
        }

    }
}
