using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MxWeiXinPF.Common;

namespace MxWeiXinPF.Web.weixin.wqiche
{
    public partial class wzDetail : WeiXinPage
    {
        protected int wid;
        protected string openid;
        protected int id;
        protected Model.article aModel;
        protected void Page_Load(object sender, EventArgs e)
        {
            id = MXRequest.GetQueryInt("id");
            wid = MXRequest.GetQueryInt("wid");
            openid = MyCommFun.RequestOpenid();

            if (!IsPostBack)
            {
                BLL.article aBll = new BLL.article();
                if (id > 0)
                    aModel = aBll.GetModel(id);
            }
        }
    }
}