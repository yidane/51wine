using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WeiXinPF.Common;

namespace WeiXinPF.Web.weixin.wqiche
{
    public partial class qjt : WeiXinPage
    {
        protected string openid;
        protected int wid;
        protected int fid;
        protected int id;
        protected string title;
        protected void Page_Load(object sender, EventArgs e)
        {
            wid = MyCommFun.RequestInt("wid");
            id = MyCommFun.RequestInt("id");
            openid = MyCommFun.QueryString("openid");
            if (!IsPostBack)
            {
                BLL.wx_wq_panorama pBll = new BLL.wx_wq_panorama();
                Model.wx_wq_panorama pModel = null; 
                pModel = pBll.GetModel(id);
                title = pModel.jdname;
                this.Title = title;
            }
        }

    }
}