using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WeiXinPF.Common;

namespace WeiXinPF.Web.weixin.wqiche
{
    public partial class xiaoshouMgr : WeiXinPage
    {
        protected int wid;
        protected string openid;
        protected void Page_Load(object sender, EventArgs e)
        {
            wid = MXRequest.GetQueryInt("wid");
            openid = MyCommFun.RequestOpenid();

            if (!IsPostBack)
            {
                BLL.wx_wq_xiaoshou xsBll = new BLL.wx_wq_xiaoshou();
                this.rptList.DataSource = xsBll.GetList(" wid=" + wid + " order by sort_id asc,id asc");
                this.rptList.DataBind();
            }
        }
    }
}