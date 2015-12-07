using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WeiXinPF.Common;

namespace WeiXinPF.Web.weixin.wqiche
{
    public partial class yyBaoyang : WeiXinPage
    {
        protected int wid;
        protected string openid;
        protected string type;//1,保养2，试驾
        protected string img;
        protected int ddnum;
        protected Model.wx_wq_yuyue yyModel;
        protected void Page_Load(object sender, EventArgs e)
        {
            wid = MXRequest.GetQueryInt("wid");
            openid = MXRequest.GetQueryString("openid");
            type = MXRequest.GetQueryString("type");
            if (!IsPostBack)
            {
                showInfo();
            }
        }

        void showInfo()
        {
            BLL.wx_wq_pinpai ppBll = new BLL.wx_wq_pinpai();
            BLL.wx_wq_yuyue yyBll = new BLL.wx_wq_yuyue();
            BLL.wx_wq_yyOrder yoBll = new BLL.wx_wq_yyOrder();
            yyModel = yyBll.GetModelList("type=" + type + " and wid=" + wid)[0];
            ddnum = yoBll.GetRecordCount("yid=" + type + " and wid=" + wid + " and openid='" + openid + "'");
            this.rptPinpai.DataSource = ppBll.GetList("wid=" + wid);
            this.rptPinpai.DataBind();
            if (type == "1")
            {
                this.Title = "预约保养";
                this.litShow.Text = "<dd class=\"tbox\">" +
          "<div><label>车牌</label></div><div>" +
         "<input type=\"text\" name=\"carnum\" id=\"carnum\" value=\"\" placeholder=\"如：京M 88888\"/></div></dd>" +
          "<dd class=\"tbox\"><div><label>公里数</label></div><div>" +
          "<input type=\"number\" name=\"km\" id=\"km\"  value=\"\" placeholder=\"请输入您的公里数\"  /></div></dd>";
            }
            else
            {
                this.Title = "预约试驾";
                this.litShow.Text = "<input type=\"hidden\" name=\"carnum\" id=\"carnum\" value=\"\" placeholder=\"如：京M 88888\"/><br/>" +
                    "<input type=\"hidden\" name=\"km\" id=\"km\"  value=\"\" placeholder=\"请输入您的公里数\"  />";
            }
        }
    }
}