using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WeiXinPF.Common;

namespace WeiXinPF.Web.admin.diancai
{
    public partial class shop_user_list : Web.UI.ManagePage
    {
        protected int shopid = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            shopid = MXRequest.GetQueryInt("shopid");
            if (!IsPostBack)
            {
                RptBind();
            }
        }

        /// <summary>
        /// 绑定列表
        /// </summary>
        private void RptBind()
        {
            BLL.wx_diancai_admin dcBll = new BLL.wx_diancai_admin();
            List<Model.wx_diancai_admin> shopAdmins = dcBll.GetModelList("ShopId=" + shopid);

            BLL.manager managerBll = new BLL.manager();
            DataSet dsData = new DataSet();
            if (shopAdmins.Any())
            {
                string strWhere = string.Empty;
                for (int index = 0; index <= shopAdmins.Count - 1; index++)
                {
                    strWhere += "," + shopAdmins[index].ManagerId;
                }

                strWhere = "(" + strWhere.Substring(1) + ")";

                dsData = managerBll.GetList(0, "id in " + strWhere, string.Empty);
                rptList.DataSource = dsData;
            }
            else
            {
                rptList.DataSource = new List<Model.manager>();
            }

            rptList.DataBind();
        }
    }
}