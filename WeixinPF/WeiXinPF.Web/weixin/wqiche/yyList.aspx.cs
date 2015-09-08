using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WeiXinPF.Common;
using System.Data;

namespace WeiXinPF.Web.weixin.wqiche
{
    public partial class yyList : WeiXinPage
    {
        protected int type;
        protected int wid;
        protected string openid;
        protected string img;
        protected void Page_Load(object sender, EventArgs e)
        {
            wid = MXRequest.GetQueryInt("wid");
            type = MXRequest.GetQueryInt("type");
            openid = MXRequest.GetQueryString("openid");
            if (!IsPostBack)
            {
                BLL.wx_wq_yyOrder yoBll = new BLL.wx_wq_yyOrder();
                BLL.wx_wq_yuyue yyBll = new BLL.wx_wq_yuyue();
                img = yyBll.GetModelList("type=" + type + " and wid=" + wid)[0].headpic;
                DataSet yyList = yoBll.GetList(string.Format(" wid={0} and openid='{1}' and yid={2}", wid, openid, type));
                this.rptList.DataSource = yyList;
                this.rptList.DataBind();
            }
        }

        protected void rptList_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                DataRowView drv = (DataRowView)e.Item.DataItem;
                Literal ltr = e.Item.FindControl("litShow") as Literal;
                Literal litStatus = e.Item.FindControl("litStatus") as Literal;
                string km = drv["km"].ToString();
                string carnum = drv["carnum"].ToString();
                int yid = MyCommFun.Obj2Int(drv["id"]);
                string orderStatus = drv["ddstatus"].ToString();
                if (type == 1)//保养
                {
                    ltr.Text = "<dd class=\"tbox\"><div><label>车牌：</label></div>" +
                    "<div><label>" + carnum + "</label></div></dd>" +
                     "<dd class=\"tbox\"><div><label>公里：</label></div><div><label>" + km + "</label></div></dd>";

                }

                if (orderStatus == "待回复")
                {
                    litStatus.Text = "<em class=\"no\">等待客服回电</em>";

                }
                else if (orderStatus == "拒绝")
                {
                    litStatus.Text = "<em class=\"red\">您的请求被拒绝</em>";
                }
                else if (orderStatus == "确认")
                {
                    litStatus.Text = "<em class=\"green\">客服已回复</em>";
                }


            }
        }
    }
}