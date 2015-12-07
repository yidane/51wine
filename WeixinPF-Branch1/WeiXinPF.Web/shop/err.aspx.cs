using MxWeiXinPF.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MxWeiXinPF.Web.shop
{
    public partial class err : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string err = MyCommFun.QueryString("err");
            literr.Text = err;
        }
    }
}