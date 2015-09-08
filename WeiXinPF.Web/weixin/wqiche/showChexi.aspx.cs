using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WeiXinPF.Common;

namespace WeiXinPF.Web.weixin.wqiche
{
    public partial class showChexi : WeiXinPage
    {
        protected int wid;
        protected string openid;
        protected int pid;
        protected string pinpai;
        protected string ppLogo;

        protected void Page_Load(object sender, EventArgs e)
        {
            wid = MXRequest.GetQueryInt("wid");
            pid = MXRequest.GetQueryInt("pid");
            openid = MXRequest.GetQueryString("openid");
            if (!IsPostBack)
            {
                BLL.wx_wq_pinpai ppBll = new BLL.wx_wq_pinpai();
                BLL.wx_wq_chexi cxBll = new BLL.wx_wq_chexi();
                Model.wx_wq_pinpai ppModel = ppBll.GetModel(pid);
                ppLogo = ppModel.logo;
                pinpai = ppModel.name;
                this.rptList.DataSource = cxBll.GetList(string.Format(" wid={0} and pid={1} order by sort_id asc,id asc", wid, pid));
                this.rptList.DataBind();
            }
        }
    }
}