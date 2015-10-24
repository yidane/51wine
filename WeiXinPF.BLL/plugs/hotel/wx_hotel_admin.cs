using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WeiXinPF.BLL
{
    public class wx_hotel_admin
    {

        public DAL.wx_hotel_admin _dal = null;
        public wx_hotel_admin()
        {
            if (_dal == null)
            {
                _dal = new DAL.wx_hotel_admin();
            }
        }


        public int Add(Model.wx_hotel_admin model)
        {
            return _dal.Add(model);
        }

        public Model.wx_hotel_admin GetModel(int uid)
        {
            return _dal.GetModel(uid);
        }

        public List<Model.wx_hotel_admin> GetModelList(string strWhere)
        {
            return _dal.GetModelList(strWhere);
        }
    }
}
