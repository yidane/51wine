using System;
using System.Collections.Generic;
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
        public Model.wx_travel_scenicDetail GetModel(int id)
        {
            return _dal.GetModel(id);
        }

    }
}
