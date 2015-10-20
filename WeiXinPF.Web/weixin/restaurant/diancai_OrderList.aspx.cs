namespace WeiXinPF.Web.weixin.restaurant
{
    using System;
    using System.Data;

    using WeiXinPF.Common;

    public partial class diancai_OrderList : WeiXinPage
    {
      
        public int shopid = 0;
        public string openid = "";
        BLL.wx_diancai_dingdan_manage managebll= new BLL.wx_diancai_dingdan_manage();
        Model.wx_diancai_dingdan_manage manage = new Model.wx_diancai_dingdan_manage();
        public  string str = "";
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
                if (this.openid!="")
                {
                    this.List(this.openid);
                }

             }
           }


        public void List(string openid)
        {

            DataSet dr = this.managebll.GetMyOrderInShop(openid, this.shopid);
            if(dr.Tables[0].Rows.Count>0)
            {
                for (int i = 0; i < dr.Tables[0].Rows.Count;i++ )
                {
                    this.str += "<ul class=\"round\">";
                    this.str += "<li class=\"title\"><a href=\"diancai_dingdan.aspx?shopid=" + this.shopid + "&dingdan=" + dr.Tables[0].Rows[i]["id"].ToString() + "&openid=" + openid + "\"><span>" + dr.Tables[0].Rows[i]["oderTime"].ToString() + " </span></a></li>";
                    this.str+=" <table width=\"100%\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" class=\"cpbiaoge\">";
                    this.str += "<tr><th>订单编号</th>";
                    this.str += "<th width=\"70\" class=\"cc\">订单金额</th><th width=\"55\" class=\"cc\">订单状态</th></tr>";
                    this.str += "<tr><td>" + dr.Tables[0].Rows[i]["orderNumber"].ToString() + "</td><td class=\"cc\">" + dr.Tables[0].Rows[i]["payAmount"].ToString() + "元</td>";
                    this.str += "<td class=\"cc\"> ";
                    if (dr.Tables[0].Rows[i]["payStatus"].ToString() == "1")
                    {
                        this.str += "<em class=\"ok\">成功</em>";
                    }
                    else if (dr.Tables[0].Rows[i]["payStatus"].ToString() == "2")
                    {
                        this.str += "<em class=\"error\">失败</em>";
                    }
                    else
                    {
                        this.str += "<em class=\"no\">未处理</em>";
                    }
                    this.str+=" </td></tr></table></ul>";
                }
            }

        }


        }
      }

    

   