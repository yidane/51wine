using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MxWeiXinPF.Common;

namespace MxWeiXinPF.Web.weixin.zhanhui
{
    public partial class delc : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            MyCommFun.delCookie("gz_id");
            MyCommFun.delCookie("zs_id");
        }
    }
}