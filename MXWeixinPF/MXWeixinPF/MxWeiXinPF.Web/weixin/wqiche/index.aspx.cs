using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MxWeiXinPF.Common;
using System.Text;

namespace MxWeiXinPF.Web.weixin.wqiche
{
    public partial class index : WeiXinPage
    {
        protected int wid;
        protected string openid;
        protected Model.wx_wq_fuhuiSys fhModel;
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
            BLL.wx_wq_fuhuiSys fhBll = new BLL.wx_wq_fuhuiSys();
            BLL.article aBll = new BLL.article();
            BLL.wx_userweixin uw = new BLL.wx_userweixin();
            this.Title = uw.GetModel(wid).wxName;

            //首页幻灯片
            this.imgList.DataSource = aBll.GetList(10, " wid=" + wid, "id asc,add_time asc");
            this.imgList.DataBind();
            this.rptDian.DataSource = aBll.GetList(10, " wid=" + wid, "id asc,add_time asc");
            this.rptDian.DataBind();

            fhModel = fhBll.GetModelList("wid=" + wid)[0];
            StringBuilder sb = new StringBuilder();
            string canshu = "?wid=" + wid + "&openid=" + openid;

            //经销车型
            string jxcxUrl = fhModel.jscxurl == "" ? "ppList.aspx" + canshu : fhModel.jscxurl + canshu;
            sb.Append("<li><a href=\"" + jxcxUrl + "\" style=\"background-image: url(" + fhModel.jscxpic + ");" + "background-size: 100% 100%;\"><label>" + fhModel.jscx + "</label></a></li>");

            //销售顾问
            string xsgwUrl = fhModel.xsgwurl == "" ? "xiaoshouMgr.aspx" + canshu : fhModel.xsgw + canshu;
            sb.Append("<li><a href=\"" + xsgwUrl + "\" style=\"background-image: url(" + fhModel.xsgwpic + ");" + "background-size: 100% 100%;\"><label>" + fhModel.xsgw + "</label></a></li>");

            //在线预约
            string zxyyUrl = fhModel.zxyyurl == "" ? "yyBaoyang.aspx" + canshu + "&type=2" : fhModel.zxyy + canshu + "&type=2";
            sb.Append("<li><a href=\"" + zxyyUrl + "\" style=\"background-image: url(" + fhModel.zxyypic + ");" + "background-size: 100% 100%;\"><label>" + fhModel.zxyy + "</label></a></li>");

            //车主关怀
            string czghUrl = fhModel.czghurl == "" ? "czGuanhuai.aspx" + canshu : fhModel.czgh + canshu;
            sb.Append("<li><a href=\"" + czghUrl + "\" style=\"background-image: url(" + fhModel.czghpic + ");" + "background-size: 100% 100%;\"><label>" + fhModel.czgh + "</label></a></li>");

            //实用工具
            string sygjUrl = fhModel.sygjurl == "" ? "syGongju.aspx" + canshu : fhModel.sygj + canshu;
            sb.Append("<li><a href=\"" + sygjUrl + "\" style=\"background-image: url(" + fhModel.sygjpic + ");" + "background-size: 100% 100%;\"><label>" + fhModel.sygj + "</label></a></li>");


            this.litMenu.Text = sb.ToString();
        }

    }
}