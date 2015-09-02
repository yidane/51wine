using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MxWeiXinPF.Common;
namespace MxWeiXinPF.Web.weixin.wqiche
{
    public partial class qjtList : WeiXinPage
    {
        protected int wid;
        protected string openid;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                wid = MXRequest.GetQueryInt("wid");
                openid = MXRequest.GetQueryString("openid");
                BLL.wx_wq_panorama pBll = new BLL.wx_wq_panorama();
                this.rptView.DataSource = pBll.GetList("wid=" + wid);
                this.rptView.DataBind();
            }
        }
    }
}