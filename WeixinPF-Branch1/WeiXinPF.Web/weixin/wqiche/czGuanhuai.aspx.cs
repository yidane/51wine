using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WeiXinPF.Common;

namespace WeiXinPF.Web.weixin.wqiche
{
    public partial class czGuanhuai : WeiXinPage
    {
        protected string openid;
        protected int wid;
        protected Model.wx_wq_czgh ghModel;
        protected void Page_Load(object sender, EventArgs e)
        {
            wid = MXRequest.GetQueryInt("wid");
            openid = MXRequest.GetQueryString("openid");
            if (!IsPostBack)
            {
                BLL.wx_wq_czgh ghBll = new BLL.wx_wq_czgh();
                ghModel = ghBll.GetModelList("wid=" + wid)[0];
            }
        }
    }
}