using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MxWeiXinPF.Common;

namespace MxWeiXinPF.Web.admin.magazine
{
    public partial class animate : System.Web.UI.Page
    {
        protected int iid;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                iid = MyCommFun.RequestInt("iid");
            }
        }
    }
}