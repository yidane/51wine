using WeiXinPF.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WeiXinPF.Web.weixin.zhanhui
{
    public partial class zsReg : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string gzid_pd = MyCommFun.getCookie("zs_id");
                BLL.wx_zh_gz gzBll = new BLL.wx_zh_gz();
                if (gzid_pd != "")
                {
                    if (gzBll.GetRecordCount("id='" + gzid_pd + "'") > 0)
                        Response.Redirect("zsReg_ret.aspx?zsid=" + gzid_pd);
                }

            }
        }
    }
}