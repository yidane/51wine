using System.Text;
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
        public string str = "";
        public string openId = string.Empty;
        public string menuStr = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                openId = MyCommFun.QueryString("openid");
                var type = MyCommFun.QueryString("type");
                if (!string.IsNullOrEmpty(openId))
                {
                    if (!string.Equals(type, "Pay", StringComparison.CurrentCultureIgnoreCase))
                    {
                        menuStr = string.Format(@"<li class='normal'><a href='diancai_oder.aspx?openid={0}&type=pay'>已付款</a></li>
                                                        <li class='active'><a href='diancai_oder.aspx?openid={0}&type=refund'>退单</a></li>", openId);
                        GetRefund(openId);
                    }
                    else
                    {
                        menuStr = string.Format(@"<li class='active'><a href='diancai_oder.aspx?openid={0}&type=pay'>已付款</a></li>
                                                        <li class='normal'><a href='diancai_oder.aspx?openid={0}&type=refund'>退单</a></li>", openId);
                        GetPay(openId);
                    }
                }
            }
        }

        public void GetPay(string openID)
        {
            var managebll = new BLL.wx_diancai_dingdan_manage();
            DataSet dr = managebll.GetPayList(openID);
            if (dr.Tables[0].Rows.Count > 0)
            {
                var detailBuilder = new StringBuilder();
                for (int i = 0; i < dr.Tables[0].Rows.Count; i++)
                {
                    var payStatus = dr.Tables[0].Rows[i]["payStatus"].ToString();
                    string payStatusText;
                    string payStatusCss = string.Empty;
                    switch (payStatus)
                    {
                        case "1":
                            payStatusText = "等待使用";
                            break;
                        case "2":
                        case "4": //部分退款
                            payStatusText = "未使用";
                            payStatusCss = "status-refundPart";
                            break;
                        case "3":
                            payStatusText = "已使用";
                            //payStatusCss = "status-refundAll";
                            break;
                        case "5"://全部退款
                            payStatusText = "未使用";
                            payStatusCss = "status-refundAll";
                            break;
                        default:
                            payStatusText = "未处理";
                            break;
                    }

                    detailBuilder.Append("<ul>");
                    detailBuilder.Append("<li>");
                    detailBuilder.AppendFormat("<a href=\"diancai_orderDetail.aspx?wid={0}&shopid={1}&dingdan={2}&openid={3}\">",
                                                                    dr.Tables[0].Rows[i]["wid"].ToString(),
                                                                    dr.Tables[0].Rows[i]["shopinfoid"].ToString(),
                                                                    dr.Tables[0].Rows[i]["id"].ToString(),
                                                                    openId
                                                                  );
                    detailBuilder.Append("<div class=\"info_01\">");

                    if (!string.IsNullOrEmpty(payStatusCss))
                        detailBuilder.AppendFormat("<i class=\"{0} i-status\"></i>", payStatusCss);

                    detailBuilder.AppendFormat("<h3>{0}</h3>", dr.Tables[0].Rows[i].Field<string>("hotelName"));
                    detailBuilder.AppendFormat("<p>实付<b>￥{0}</b>共<b>{1}</b>件商品</p>", dr.Tables[0].Rows[i]["payAmount"].ToString(), dr.Tables[0].Rows[i]["OrderCount"].ToString());
                    detailBuilder.Append("<span class=\"wave_blue_icon\"></span>");
                    detailBuilder.Append("</div>");
                    detailBuilder.Append("<div class=\"info_02\">");
                    detailBuilder.Append("<dl>");
                    detailBuilder.AppendFormat("<dd><b class=\"i_gray_icon\"></b>订单编号 {0}</dd>", dr.Tables[0].Rows[i]["orderNumber"].ToString());
                    detailBuilder.AppendFormat("<dd><b class=\"time_gray_icon\"></b>购票日期 {0}</dd>", dr.Tables[0].Rows[i]["oderTime"].ToString());
                    //此处应该有购票日期
                    detailBuilder.Append("</dl>");
                    detailBuilder.Append("</div>");
                    detailBuilder.Append("<div class=\"info_03\">");
                    detailBuilder.AppendFormat("<span>{0}</span>", payStatusText);
                    detailBuilder.Append("</div>");
                    detailBuilder.Append("</a>");
                    detailBuilder.Append("</li>");
                    detailBuilder.Append("</ul>");
                }

                str = detailBuilder.ToString();
            }
        }

        public void GetRefund(string openID)
        {
            var managebll = new BLL.wx_diancai_tuidan_manage();
            DataSet dr = managebll.GetRefundList(openID);

            if (dr.Tables[0].Rows.Count > 0)
            {
                var builder = new StringBuilder();
                for (int i = 0; i < dr.Tables[0].Rows.Count; i++)
                {
                    builder.Append("<ul>");
                    builder.Append("<li>");
                    builder.AppendFormat("<a href=\"diancai_RefundOrderDetail.aspx?wid={0}&shopid={1}&dingdan={2}&refundCode={3}&openid={4}\">",
                                                                            dr.Tables[0].Rows[i]["wid"].ToString(),
                                                                            dr.Tables[0].Rows[i]["shopinfoid"].ToString(),
                                                                            dr.Tables[0].Rows[i]["dingdan"].ToString(),
                                                                            dr.Tables[0].Rows[i]["refundCode"].ToString(),
                                                                            openId);
                    builder.Append("<div class=\"info_01\">");
                    builder.AppendFormat("<h3>{0}</h3>", dr.Tables[0].Rows[i].Field<string>("hotelName"));
                    builder.AppendFormat("<p>退款<b>￥{0}</b>共<b>{1}</b>件商品</p>", dr.Tables[0].Rows[i]["refundAmount"].ToString(), dr.Tables[0].Rows[i]["RefundCount"].ToString());
                    builder.Append("<span class=\"wave_blue_icon\"></span>");
                    builder.Append("</div>");
                    builder.Append("<div class=\"info_02\">");
                    builder.Append("<dl>");
                    builder.AppendFormat("<dd><b class=\"i_gray_icon\"></b>订单编号 {0}</dd>", dr.Tables[0].Rows[i]["orderNumber"].ToString());
                    builder.AppendFormat("<dd><b class=\"i_gray_icon\"></b>退单编号 {0}</dd>", dr.Tables[0].Rows[i]["refundCode"].ToString());
                    builder.AppendFormat("<dd><b class=\"time_gray_icon\"></b>退单日期 {0}</dd>", dr.Tables[0].Rows[i]["createDate"].ToString());
                    //此处应该有购票日期
                    builder.Append("</dl>");
                    builder.Append("</div>");
                    builder.Append("<div class=\"info_03\">");
                    var refundStatus = Convert.ToInt32(dr.Tables[0].Rows[i]["refundStatus"]);
                    var statusDict = StatusManager.DishStatus.GetStatusDict(refundStatus);

                    builder.AppendFormat("<span>{0}</span>", statusDict.StatusName);
                    builder.Append("</div>");
                    builder.Append("</a>");
                    builder.Append("</li>");
                    builder.Append("</ul>");
                }

                str = builder.ToString();
            }
        }
    }
}