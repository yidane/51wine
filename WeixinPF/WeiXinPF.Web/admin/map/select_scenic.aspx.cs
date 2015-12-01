using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WeiXinPF.Common;

namespace WeiXinPF.Web.admin.map
{
    public partial class select_scenic : Web.UI.ManagePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadScenic();
            }
        }

        private void LoadScenic()
        {
            int wid = GetWeiXinCode().id;

            List<Model.wx_travel_scenicDetail> scenics = new BLL.wx_travel_scenicDetail().GetModelByWid(wid);

            rpScenic.DataSource = scenics;
            rpScenic.DataBind();
        }
    }
}