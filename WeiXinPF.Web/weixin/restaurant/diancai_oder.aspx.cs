using WeiXinPF.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WeiXinPF.Web.weixin.restaurant
{
    public partial class diancai_oder : WeiXinPage
    {
        BLL.wx_diancai_dingdan_manage managebll = new BLL.wx_diancai_dingdan_manage();
        Model.wx_diancai_dingdan_manage manage = new Model.wx_diancai_dingdan_manage();
        public string str = "";
        public string openId = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                openId = MyCommFun.QueryString("openid");
                var type = MyCommFun.QueryString("type");
                if (!string.IsNullOrEmpty(openId))
                {
                    if (string.Equals(type, "Pay", StringComparison.CurrentCultureIgnoreCase))
                    {
                        GetRefund(openId);
                    }
                    else
                    {
                        GetPay(openId);
                    }
                }
            }
        }

        public void GetPay(string openID)
        {
            DataSet dr = managebll.GetListList(openID);
            if (dr.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < dr.Tables[0].Rows.Count; i++)
                {
                    str += "<ul class=\"round\">";
                    str += "<li class=\"title\"><a href=\"diancai_orderDetail.aspx?shopid=" + dr.Tables[0].Rows[i]["shopinfoid"].ToString() + "&dingdan=" + dr.Tables[0].Rows[i]["id"].ToString() + "&openid=" + openid + "\"><span>" + dr.Tables[0].Rows[i]["oderTime"].ToString() + "<img src=\"images\\tel.png\" class=\"HomeImage\"></img>" + dr.Tables[0].Rows[i].Field<string>("hotelName") + "</span></a></li>";
                    str += " <table width=\"100%\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" class=\"cpbiaoge\">";
                    str += "<tr><th>订单编号</th>";
                    str += "<th width=\"70\" class=\"cc\">订单金额</th><th width=\"55\" class=\"cc\">订单状态</th></tr>";
                    str += "<tr><td>" + dr.Tables[0].Rows[i]["orderNumber"].ToString() + "</td><td class=\"cc\">" + dr.Tables[0].Rows[i]["payAmount"].ToString() + "元</td>";
                    str += "<td class=\"cc\"> ";
                    if (dr.Tables[0].Rows[i]["payStatus"].ToString() == "1")
                    {
                        str += "<em class=\"ok\">部分使用</em>";
                    }
                    else if (dr.Tables[0].Rows[i]["payStatus"].ToString() == "2")
                    {
                        str += "<em class=\"error\">全部使用</em>";
                    }
                    else
                    {
                        str += "<em class=\"no\">未处理</em>";
                    }
                    str += " </td></tr></table></ul>";
                }
            }
        }

        public void GetRefund(string openID)
        {
            DataSet dr = managebll.GetListList(openID);
            if (dr.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < dr.Tables[0].Rows.Count; i++)
                {
                    str += "<ul class=\"round\">";
                    str += "<li class=\"title\"><a href=\"diancai_orderDetail.aspx?shopid=" + dr.Tables[0].Rows[i]["shopinfoid"].ToString() + "&dingdan=" + dr.Tables[0].Rows[i]["id"].ToString() + "&openid=" + openid + "\"><span>" + dr.Tables[0].Rows[i]["oderTime"].ToString() + "<img src=\"images\\tel.png\" class=\"HomeImage\"></img>" + dr.Tables[0].Rows[i].Field<string>("hotelName") + "</span></a></li>";
                    str += " <table width=\"100%\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" class=\"cpbiaoge\">";
                    str += "<tr><th>订单编号</th>";
                    str += "<th width=\"70\" class=\"cc\">订单金额</th><th width=\"55\" class=\"cc\">订单状态</th></tr>";
                    str += "<tr><td>" + dr.Tables[0].Rows[i]["orderNumber"].ToString() + "</td><td class=\"cc\">" + dr.Tables[0].Rows[i]["payAmount"].ToString() + "元</td>";
                    str += "<td class=\"cc\"> ";
                    if (dr.Tables[0].Rows[i]["payStatus"].ToString() == "1")
                    {
                        str += "<em class=\"ok\">部分使用</em>";
                    }
                    else if (dr.Tables[0].Rows[i]["payStatus"].ToString() == "2")
                    {
                        str += "<em class=\"error\">全部使用</em>";
                    }
                    else
                    {
                        str += "<em class=\"no\">未处理</em>";
                    }
                    str += " </td></tr></table></ul>";
                }
            }
        }
    }
}



