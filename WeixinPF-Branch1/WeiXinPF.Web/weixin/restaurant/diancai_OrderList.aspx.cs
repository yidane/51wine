using System.Text;

namespace WeiXinPF.Web.weixin.restaurant
{
    using System;
    using System.Data;

    using WeiXinPF.Common;

    public partial class diancai_OrderList : WeiXinPage
    {

        public int shopid = 0;
        public string openid = "";
        BLL.wx_diancai_dingdan_manage managebll = new BLL.wx_diancai_dingdan_manage();
        Model.wx_diancai_dingdan_manage manage = new Model.wx_diancai_dingdan_manage();
        public string str = "";
        BLL.wx_diancai_shopinfo shopBll = new BLL.wx_diancai_shopinfo();
        Model.wx_diancai_shopinfo shopinfo = new Model.wx_diancai_shopinfo();
        public string hotelName = "";
        public string rename = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {

                this.shopid = MyCommFun.RequestInt("shopid");

                this.openid = MyCommFun.QueryString("openid");

                this.shopinfo = this.shopBll.GetModel(this.shopid);
                this.hotelName = this.shopinfo.hotelName;
                this.rename = this.shopinfo.dcRename;
                if (this.openid != "")
                {
                    this.List(this.openid);
                }

            }
        }


        public void List(string openid)
        {

            DataSet dr = this.managebll.GetMyOrderInShop(openid, this.shopid);
            if (dr.Tables[0].Rows.Count > 0)
            {
                var detailBuilder = new StringBuilder();
                for (int i = 0; i < dr.Tables[0].Rows.Count; i++)
                {
                    detailBuilder.Append("<ul class=\"round\">");
                    detailBuilder.AppendFormat("<li class=\"title\"><a href=\"diancai_orderDetail.aspx?wid={0}&shopid={1}&dingdan={2}&openid={3}\"><span>{4} {5}</span></a></li>",
                                                                            dr.Tables[0].Rows[i]["wid"].ToString(),
                                                                            this.shopid,
                                                                            dr.Tables[0].Rows[i]["id"].ToString(),
                                                                            this.openid,
                                                                            dr.Tables[0].Rows[i]["oderTime"].ToString(),
                                                                            dr.Tables[0].Rows[i].Field<string>("hotelName"));
                    detailBuilder.Append(" <table width=\"100%\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" class=\"cpbiaoge\">");
                    detailBuilder.Append("<tr><th>订单编号</th>");
                    detailBuilder.Append("<th width=\"70\" class=\"cc\">订单金额</th><th width=\"60\" class=\"cc\">订单状态</th></tr>");
                    detailBuilder.Append("<tr><td>" + dr.Tables[0].Rows[i]["orderNumber"].ToString() + "</td><td class=\"cc\">" + dr.Tables[0].Rows[i]["payAmount"].ToString() + "元</td>");
                    detailBuilder.Append("<td class=\"cc\">");
                    var payStatus = dr.Tables[0].Rows[i]["payStatus"].ToString();
                    switch (payStatus)
                    {
                        case "1":
                            detailBuilder.Append("<em class=\"ok\">等待使用</em>");
                            break;
                        case "2":
                        case "4": //部分退款
                            detailBuilder.Append("<em class=\"ok\">部分使用</em>");
                            break;
                        case "3":
                        case "5"://全部退款
                            detailBuilder.Append("<em class=\"error\">全部使用</em>");
                            break;
                        default:
                            detailBuilder.Append("<em class=\"no\">未处理</em>");
                            break;
                    }
                    detailBuilder.Append(" </td></tr></table></ul>");
                }

                str = detailBuilder.ToString();
            }
        }
    }
}



