using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WeiXinPF.BLL
{
    public class wx_travel_marker
    {
        private DAL.wx_travel_marker _bll = null;
        public wx_travel_marker()
        {
            if (_bll == null)
            {
                _bll = new DAL.wx_travel_marker();
            }
        }

        public Model.wx_travel_marker GetModel(int id)
        {
            return _bll.GetModel(id);
        }

        public List<Model.wx_travel_marker> GetModelList(string strWhere)
        {
            return _bll.GetModelList(strWhere);
        }
    }
}
