using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MxWeiXinPF.Common;
using System.Data;

namespace MxWeiXinPF.Web.weixin.wqiche
{
    public partial class yhcx : WeiXinPage
    {
        protected int wid;
        protected string openid;
        protected Model.article_category acModel;
        protected void Page_Load(object sender, EventArgs e)
        {
            wid = MXRequest.GetQueryInt("wid");
            openid = MXRequest.GetQueryString("openid");
            if (!IsPostBack)
            {
                BLL.wx_wq_wzlx wzBll = new BLL.wx_wq_wzlx();
                Model.wx_wq_wzlx wzModel = wzBll.GetModelList("wid=" + wid)[0];

                BLL.article_category acBll = new BLL.article_category();
                DataTable ds = acBll.GetList(" wid=" + wid + " and id=" + wzModel.yhid);
                if (ds.Rows.Count > 0)
                    acModel = acBll.GetModel(MyCommFun.Obj2Int(wzModel.yhid));

                BLL.article aBll = new BLL.article();
                this.rptList.DataSource = aBll.GetList(" category_id=" + acModel.id);
                this.rptList.DataBind();
            }
        }
    }
}