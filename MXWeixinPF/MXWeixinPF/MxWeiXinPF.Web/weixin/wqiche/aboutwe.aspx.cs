using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MxWeiXinPF.Common;

namespace MxWeiXinPF.Web.weixin.wqiche
{
    public partial class aboutwe : WeiXinPage
    {
        protected int wid;
        protected string openid;
        protected Model.wx_wsite_setting wsModel;
        protected void Page_Load(object sender, EventArgs e)
        {
            wid = MXRequest.GetQueryInt("wid");
            openid = MyCommFun.RequestOpenid();

            if (!IsPostBack)
            {
                BLL.wx_wsite_setting ws = new BLL.wx_wsite_setting();
                wsModel = ws.GetModelByWid(wid);
            }
        }
    }
}