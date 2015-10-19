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
    public partial class index : WeiXinPage
    {

        public int shopid = 0;
        BLL.wx_diancai_caipin_category categorybll = new BLL.wx_diancai_caipin_category();
        Model.wx_diancai_caipin_category categorymodel = new Model.wx_diancai_caipin_category();

        BLL.wx_diancai_caipin_manage managebll = new BLL.wx_diancai_caipin_manage();

        BLL.wx_diancai_shopinfo shopBll = new BLL.wx_diancai_shopinfo();
        Model.wx_diancai_shopinfo shopinfo = new Model.wx_diancai_shopinfo();
        public string cateString = "";
        public string manageString = "";
        public int categoryid = 0;

        public string categories = "";

        public string hotelName = "";

        public string openid = "";
        public string rename = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                shopid = MyCommFun.RequestInt("shopid");
                categoryid = MyCommFun.RequestInt("categoryid");
                openid = MyCommFun.RequestOpenid();

                if (shopid == 0 || openid == "")
                {
                    MessageBox.ResponseScript(this, "链接参数有问题！");
                    return;
                }

                BindLeftCate();
            }



        }

        /// <summary>
        /// 绑定左边的分类信息
        /// </summary>
        protected void BindLeftCate()
        {

            shopinfo = shopBll.GetModel(shopid);

            if (shopinfo == null)
            {
                return;
            }
            rename = shopinfo.dcRename;
            hotelName = shopinfo.hotelName;
            DataSet category1 = categorybll.GetList(shopid);
            if (category1 == null || category1.Tables.Count <= 0 || category1.Tables[0].Rows.Count <= 0)
            {
                return;
            }

            if (categoryid == 0)
            {
                if (category1 != null && category1.Tables.Count > 0 && category1.Tables[0].Rows.Count > 0)
                {
                    categoryid = MyCommFun.Obj2Int(category1.Tables[0].Rows[0]["id"]);
                }
            }


            manageString = "";
            cateString = "";
            categories = "{";
            if (category1.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < category1.Tables[0].Rows.Count; i++)
                {
                    if (categoryid == Convert.ToInt32(category1.Tables[0].Rows[i]["id"].ToString()))//选择的一条
                    {
                        cateString += "<dd class=\"active\"><a style=\"width: 100%;\" href=\"index.aspx?categoryid=" + category1.Tables[0].Rows[i]["id"].ToString() + "&openid=" + openid + "&shopid=" + shopid + "\">" + category1.Tables[0].Rows[i]["categoryName"].ToString() + "</a></dd>";

                    }
                    else
                    {
                        cateString += "<dd ><a style=\"width: 100%;\" href=\"index.aspx?categoryid=" + category1.Tables[0].Rows[i]["id"].ToString() + "&openid=" + openid + "&shopid=" + shopid + "\">" + category1.Tables[0].Rows[i]["categoryName"].ToString() + "</a></dd>";
                    }


                    categories += "\"" + category1.Tables[0].Rows[i]["id"].ToString() + "\"" + ":" + "\"" + category1.Tables[0].Rows[i]["categoryName"].ToString() + "\"" + ",";

                }
            }
            categories = categories.Substring(0, categories.Length - 1);
            categories += "}";


            //菜品
            DataSet manage1 = managebll.GetList(categoryid);//分类id
            if (manage1.Tables[0].Rows.Count > 0)
            {
                for (int j = 0; j < manage1.Tables[0].Rows.Count; j++)
                {
                    manageString += "<dd> ";
                    manageString += "<span class=\"count_zero\" id=\"num_" + manage1.Tables[0].Rows[j]["id"].ToString() + "_1\" onClick=\"addProduct('" + manage1.Tables[0].Rows[j]["id"].ToString() + "','1','" + manage1.Tables[0].Rows[j]["cpName"].ToString() + "','" + manage1.Tables[0].Rows[j]["zkPrice"].ToString() + "','" + categoryid + "',1);\">0</span>";
                    manageString += " <div class=\"tupian\"><img src=" + manage1.Tables[0].Rows[j]["picUrl"].ToString() + " onClick=\"htmlit('" + manage1.Tables[0].Rows[j]["picUrl"].ToString() + "','" + manage1.Tables[0].Rows[j]["cpName"].ToString() + "'," + manage1.Tables[0].Rows[j]["id"].ToString() + ")\">";
                    manageString += "<a href=\"javascript:htmlit('" + manage1.Tables[0].Rows[j]["picUrl"].ToString() + "','" + manage1.Tables[0].Rows[j]["cpName"].ToString() + "'," + manage1.Tables[0].Rows[j]["id"].ToString() + ");\" class=\"add\" data-foodid=\"" + manage1.Tables[0].Rows[j]["id"].ToString() + "_1\">";
                    manageString += "<h3>" + manage1.Tables[0].Rows[j]["cpName"].ToString() + "</h3>";
                    manageString += " <em>" + manage1.Tables[0].Rows[j]["zkPrice"].ToString() + "元/件<del>" + manage1.Tables[0].Rows[j]["cpPrice"].ToString() + "元/件</del></em>";
                    manageString += "  <p class=\"dpNum\">" + manage1.Tables[0].Rows[j]["scan"].ToString() + "</p></a> ";
                    manageString += " <a href=\"javascript:addProduct('" + manage1.Tables[0].Rows[j]["id"].ToString() + "','1','" + manage1.Tables[0].Rows[j]["cpName"].ToString() + "','" + manage1.Tables[0].Rows[j]["zkPrice"].ToString() + "','" + categoryid + "',1);\" class=\"action plus\" id=\"add_" + manage1.Tables[0].Rows[j]["id"].ToString() + "_1\"><b>加一份</b></a>";
                    manageString += " <a href=\"javascript:reduceProduct('" + manage1.Tables[0].Rows[j]["id"].ToString() + "','1',1);\" class=\"action reduce\" id=\"del_" + manage1.Tables[0].Rows[j]["id"].ToString() + "_1\" style=\"display:none;\"><b>减一份</b></a>";
                    manageString += " </div></dd>";
                }

            }

        }

    }
}