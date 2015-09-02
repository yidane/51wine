using MxWeiXinPF.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MxWeiXinPF.Web.weixin.zhanhui
{
    public partial class gzReg : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string gzid_pd = MyCommFun.getCookie("gz_id");
                BLL.wx_zh_gz gzBll = new BLL.wx_zh_gz();
                if (gzid_pd != "")
                {
                    if (gzBll.GetRecordCount("id='" + gzid_pd + "'") > 0)
                        Response.Redirect("gzReg_ret.aspx?gzid=" + gzid_pd);
                }

            }
        }
    }
}