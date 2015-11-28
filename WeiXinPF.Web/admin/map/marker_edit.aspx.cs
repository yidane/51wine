using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WeiXinPF.Common;

namespace WeiXinPF.Web.admin.map
{
    public partial class marker_edit : System.Web.UI.Page
    {
        private string _action = MXEnums.ActionEnum.Add.ToString();
        private int _id = 0;
        private BLL.wx_travel_marker _bll = new BLL.wx_travel_marker();
        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }
}