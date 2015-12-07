using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WeiXinPF.BLL
{
    public class wx_hotel_user
    {

        public DAL.wx_hotel_user _dal = null;
        public wx_hotel_user()
        {
            if (_dal == null)
            {
                _dal = new DAL.wx_hotel_user();
            }
        }

        public int Add(Model.wx_hotel_user model)
        {
            return _dal.Add(model);
        }

        public List<Model.wx_hotel_user> GetModelList(string strWhere)
        {
            return _dal.GetModelList(strWhere);
        }

        public Model.wx_hotel_user GetModel(int uid)
        {
            return _dal.GetModel(uid);
        }
    }
}
