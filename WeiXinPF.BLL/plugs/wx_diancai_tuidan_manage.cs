using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace WeiXinPF.BLL
{
    public class wx_diancai_tuidan_manage
    {
        private readonly WeiXinPF.DAL.wx_diancai_tuidan_manage dal = new WeiXinPF.DAL.wx_diancai_tuidan_manage();
        public void AddRefundModel(List<Model.wx_diancai_tuidan_manage> modelList)
        {
            dal.AddRefundModel(modelList);
        }
    }
}
