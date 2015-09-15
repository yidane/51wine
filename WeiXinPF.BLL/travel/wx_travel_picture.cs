using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using WeiXinPF.Common;

namespace WeiXinPF.BLL
{
    public class wx_travel_picture
    {
        private DAL.wx_travel_picture _dal = null;

        public wx_travel_picture()
        {
            if (_dal == null)
            {
                _dal = new DAL.wx_travel_picture();
            }
        }


        public int Add(Model.wx_travel_picture model)
        {
            return _dal.Add(model);
        }

        public bool Update(Model.wx_travel_picture model)
        {
            return _dal.Update(model);
        }

        public bool Delete(int id)
        {
            return _dal.Delete(id);
        }

        public Model.wx_travel_picture GetModel(int id)
        {
            return _dal.GetModel(id);
        }

        public DataSet GetPageList(int pageSize, int pageIndex, string strWhere, string filedOrder, out int recordCount)
        {
            return _dal.GetPageList(pageSize, pageIndex, strWhere, filedOrder, out recordCount);
        }

        public List<Model.wx_travel_picture> GetModelList(string strWhere)
        {
            return _dal.GetModelList(strWhere);
        }
    }
}
