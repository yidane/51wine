using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WeiXinPF.Common;

namespace WeiXinPF.Web.weixin.wqiche
{
    public partial class czInfo : WeiXinPage
    {
        protected string openid;
        protected int wid;
        protected string spdate = "";
        protected string gcdate = "";
        protected Model.wx_wq_chezhu czModel;
        protected void Page_Load(object sender, EventArgs e)
        {
            wid = MXRequest.GetQueryInt("wid");
            openid = MXRequest.GetQueryString("openid");

            if (!IsPostBack)
            {
                showInfo();
            }
        }

        void showInfo()
        {
            BLL.wx_wq_pinpai ppBll = new BLL.wx_wq_pinpai();

            BLL.wx_wq_chezhu czBll = new BLL.wx_wq_chezhu();
            IList<Model.wx_wq_chezhu> czList = czBll.GetModelList(" openid='" + openid + "'");
            if (czList.Count > 0)
            {
                czModel = czList[0];
            }
            else
            {
                czModel = new Model.wx_wq_chezhu();
            }
            this.rptPinpai.DataSource = ppBll.GetModelList("wid=" + wid);
            this.rptPinpai.DataBind();
            if (czModel.gcdate != null)
                gcdate = MyCommFun.Obj2DateTime(czModel.gcdate).ToString("yyyy-MM-dd");
            else
                gcdate = DateTime.Now.ToString("yyyy-MM-dd");

            if (czModel.spdate != null)
                spdate = MyCommFun.Obj2DateTime(czModel.spdate).ToString("yyyy-MM-dd");
            else
                spdate = DateTime.Now.ToString("yyyy-MM-dd");
        }

        protected void rptPinpai_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Model.wx_wq_pinpai drv = (Model.wx_wq_pinpai)e.Item.DataItem;
                Literal ltr = e.Item.FindControl("litShow") as Literal;
                string select = "";
                if (drv.Id == czModel.ppid)
                {
                    select += "selected='selected'";
                }
                ltr.Text = "<option value=\"" + drv.Id + "\" " + select + ">" + drv.name + "</option>";

            }
        }
    }
}