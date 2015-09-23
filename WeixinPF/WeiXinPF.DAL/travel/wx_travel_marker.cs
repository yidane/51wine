using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using WeiXinPF.DBUtility;

namespace WeiXinPF.DAL
{
    public class wx_travel_marker
    {

        public Model.wx_travel_marker GetModel(int id)
        {
            using (IDbConnection db = DbFactory.GetOpenedConnection())
            {
                return db.Query<Model.wx_travel_marker>("Select * From [dbo].[wx_travel_marker] Where Id=@Id",
                    new { Id = id }
                ).FirstOrDefault();
            }
        }

        public List<Model.wx_travel_marker> GetModelList(string strWhere)
        {
            if (!string.IsNullOrEmpty(strWhere))
            {
                strWhere = "Where " + strWhere;
            }

            using (IDbConnection db = DbFactory.GetOpenedConnection())
            {
                return db.Query<Model.wx_travel_marker>("Select * From [dbo].[wx_travel_marker] " + strWhere).ToList();
            }
        }
    }
}
