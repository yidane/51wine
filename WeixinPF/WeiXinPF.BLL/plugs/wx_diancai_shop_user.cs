using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WeiXinPF.BLL
{
    public class wx_diancai_shop_user
    {
        public DAL.wx_diancai_shop_user _dal = null;
        public wx_diancai_shop_user()
        {
            if (_dal == null)
            {
                _dal = new DAL.wx_diancai_shop_user();
            }
        }

        public int Add(Model.wx_diancai_shop_user model)
        {
            return _dal.Add(model);
        }

        public List<Model.wx_diancai_shop_user> GetModelList(string strWhere)
        {
            return _dal.GetModelList(strWhere);
        }

        public Model.wx_diancai_shop_user GetModel(int uid)
        {
            return _dal.GetModel(uid);
        }
    }
}
