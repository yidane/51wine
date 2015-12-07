using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WeiXinPF.BLL
{
    public class wx_travel_marker
    {
        private DAL.wx_travel_marker _dal = null;
        public wx_travel_marker()
        {
            if (_dal == null)
            {
                _dal = new DAL.wx_travel_marker();
            }
        }

        public int Add(Model.wx_travel_marker model)
        {
            return _dal.Add(model);
        }

        public bool Update(Model.wx_travel_marker model)
        {
            return _dal.Update(model);
        }

        public bool Delete(int id)
        {
            return _dal.Delete(id);
        }

        public Model.wx_travel_marker GetModel(int id)
        {
            return _dal.GetModel(id);
        }

        public List<Model.wx_travel_marker> GetModelList(string strWhere)
        {
            return _dal.GetModelList(strWhere);
        }

        
    }
}
