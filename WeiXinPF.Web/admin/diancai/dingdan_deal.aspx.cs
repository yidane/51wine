﻿using WeiXinPF.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WeiXinPF.Web.admin.diancai
{
    public partial class dingdan_deal : Web.UI.ManagePage
    {
        BLL.wx_diancai_dingdan_manage managebll = new BLL.wx_diancai_dingdan_manage();
        Model.wx_diancai_dingdan_manage manage = new Model.wx_diancai_dingdan_manage();

        public string Dingdanlist = "";
        public string dingdanren = "";
        BLL.wx_diancai_shopinfo shopinfo = new BLL.wx_diancai_shopinfo();
        Model.wx_diancai_shopinfo sjopmodel = new Model.wx_diancai_shopinfo();
        public string id = "";
        public int ids = 0;
        public int shopid = 0;
        public string openid = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            ids = MyCommFun.RequestInt("id");
            id = MyCommFun.QueryString("id");
            shopid = MyCommFun.RequestInt("shopid");
            if (Request.Form["__EVENTTARGET"] == "btnFinish")
            {
                // Fire event
                OnOrderFinishClick(this, new EventArgs());
            }
            if (!IsPostBack)
            {
                if (ids != 0)
                {
                    List(ids);
                }
            }
        }

        public void List(int ids)
        {

            //订单

            Dingdanlist = "";
            dingdanren = "";

            DataSet dr = managebll.Getcaopin(id);
            if (dr.Tables[0].Rows.Count > 0)
            {
                decimal amount = 0;

                Dingdanlist += "<tr><th>商品信息名称</th><th class=\"cc\">单价</th><th class=\"cc\">购买份数</th><th class=\"rr\">总价</th> </tr>";
                for (int i = 0; i < dr.Tables[0].Rows.Count; i++)
                {
                    Dingdanlist += " <tr><td>" + dr.Tables[0].Rows[i]["cpName"] + "</td>";
                    Dingdanlist += "<td class=\"cc\">￥" + dr.Tables[0].Rows[i]["price"] + "</td>";
                    Dingdanlist += "<td class=\"cc\">" + dr.Tables[0].Rows[i]["num"] + "</td>";
                    Dingdanlist += "<td class=\"rr\">￥" + dr.Tables[0].Rows[i]["totpric"] + "</td></tr>";
                    amount += Convert.ToDecimal(dr.Tables[0].Rows[i]["totpric"]);
                }

                sjopmodel = shopinfo.GetModel(shopid);//配送费
                decimal zongji = amount + Convert.ToDecimal(sjopmodel.sendCost);
                if (sjopmodel != null)
                {
                    Dingdanlist += "<tr><td>商品总费</td><td class=\"cc\">￥" + amount + "</td>  <td class=\"cc\" ></td><td class=\"rr\" ></td></tr>";
                }
                else
                {
                    Dingdanlist += "<tr><td>商品总费</td><td class=\"cc\">￥" + amount + "</td>  <td class=\"cc\" ></td><td class=\"rr\" ></td></tr>";
                }
                Dingdanlist += "<tr><td></td><td ></td><td ></td><td class=\"rr\">总计：<span class='text-danger'>￥" + zongji + "</span></td></tr>";
            }


            manage = managebll.GetModeldingdan(id);
            //订单信息
            if (manage != null)
            {
                dingdanren += "<tr><td width=\"70\">订单编号： " + manage.orderNumber + "</td></tr>";
                dingdanren += "<tr> <td>预约日期：" + manage.oderTime + "</td></tr>";
                dingdanren += "<tr><td>预约人：" + manage.customerName + "</td></tr>";
                dingdanren += "<tr><td>电话：" + manage.customerTel + "</td></tr>";

                switch (manage.payStatus)
                {
                    case 1:
                        dingdanren += "<tr><td>订单状态：<em  style='width:70px;' class='ok'>等待使用</em></td></tr>";
                        break;
                    case 2:
                    case 4: //部分退款
                        dingdanren += "<tr><td>订单状态：<em  style='width:70px;' class='ok'>部分使用</em></td></tr>";
                        break;
                    case 3:
                    case 5://全部退款
                        dingdanren += "<tr><td>订单状态：<em  style='width:70px;' class='ok'>全部使用</em></td></tr>";
                        this.btnFinish.Visible = false;
                        break;
                    default:
                        dingdanren += "<tr><td>订单状态：<em  style='width:70px;' class='no'>未处理</em></td></tr>";
                        break;
                }
            }
            else
            {
                dingdanren += "<tr><td width=\"70\">订单编号：</td></tr>";
                dingdanren += "<tr> <td>下单时间：</td></tr>";
                dingdanren += "<tr><td>联系人：</td></tr>";
                dingdanren += "<tr><td>联系电话：</td></tr>";
                dingdanren += "<tr><td>地址：</td></tr>";
                dingdanren += "<tr><td>备注 ：</td></tr>";

                dingdanren += "<tr><td>订单状态：<em  style='width:70px;' class='no'>未处理</em></td></tr>";
            }
            //            dingdanren += "<tr><td>商家留言：</td></tr> <tr> <td></td></tr>";
        }

        public void OnOrderFinishClick(object sender, EventArgs e)
        {

        }
    }
}