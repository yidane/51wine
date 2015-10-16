using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WeiXinPF.Model;

namespace WeiXinPF.BLL
{
    public class wx_diancai_admin
    {
        public DAL.wx_diancai_admin _dal = null;
        public wx_diancai_admin() {
            if (_dal == null)
            {
                _dal = new DAL.wx_diancai_admin();
            }
        }


        public int Add(Model.wx_diancai_admin model)
        {
            return _dal.Add(model);
        }

        public Model.wx_diancai_admin GetModel(int uid)
        {
            return _dal.GetModel(uid);
        }

        public List<Model.wx_diancai_admin> GetModelList(string strWhere)
        {
            return _dal.GetModelList(strWhere);
        }
    }
}
