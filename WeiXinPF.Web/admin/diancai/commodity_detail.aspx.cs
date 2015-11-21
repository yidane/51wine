using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WeiXinPF.Common;
using WeiXinPF.Web.weixin.restaurant;

namespace WeiXinPF.Web.admin.diancai
{
    using System.Transactions;

    using WeiXinPF.Application.DomainModules.IdentifyingCode.Service;

    public partial class commodity_detail : Web.UI.ManagePage
    {
        BLL.wx_diancai_dingdan_manage managebll = new BLL.wx_diancai_dingdan_manage();
        Model.wx_diancai_dingdan_manage manage = new Model.wx_diancai_dingdan_manage();
        BLL.wx_diancai_shopinfo shopinfo = new BLL.wx_diancai_shopinfo();
        Model.wx_diancai_shopinfo sjopmodel = new Model.wx_diancai_shopinfo();

        public string Dingdanlist = "";
        public string dingdanren = "";
        public string id = "";
        public string cid = "";
        public int ids = 0;
        public int shopid = 0;
        public string openid = "";
        public int wid = 0;

        public const string ModuleName = "restaurant";
        protected void Page_Load(object sender, EventArgs e)
        {
            ids = MyCommFun.RequestInt("id");
            id = MyCommFun.QueryString("id");
            cid = MyCommFun.QueryString("cid");
            shopid = GetShopId();
            wid = this.GetWeiXinCode().id;
            if (!IsPostBack)
            {

                if (ids != 0)
                {
                    List(ids);
                }
            }
        }

        protected void save_groupbase_Click(object sender, EventArgs e)
        {
            var status = StatusManager.DishStatus.Used.StatusID;

            Guid identifyingCodeId;

            if (Guid.TryParse(cid, out identifyingCodeId))
            {
                var identifyingCodeObject = IdentifyingCodeService.GetIdentifyingCodeInfoByIdentifyingCodeId(identifyingCodeId, ModuleName, wid);

                if (identifyingCodeObject != null && identifyingCodeObject.ShopId.Equals(shopid.ToString()))
                {
                    identifyingCodeObject.Status = StatusManager.DishStatus.Used.StatusID;
                    identifyingCodeObject.ModifyTime = DateTime.Now;

                    using (var scope = new TransactionScope())
                    {
                        IdentifyingCodeService.ModifyIdentifyingCodeInfo(identifyingCodeObject);
                        managebll.AfterVerification(wid,shopid, int.Parse(identifyingCodeObject.OrderId));

                        scope.Complete();
                    }
                    manage = managebll.GetModel(MyCommFun.Str2Int(id));
                    BLL.wx_diancai_member menbll = new BLL.wx_diancai_member();
                    if (status == StatusManager.DishStatus.Used.StatusID)
                    {

                        menbll.Update(manage.openid);
                    }
                    if (status == StatusManager.DishStatus.PreRefund.StatusID || status == StatusManager.DishStatus.Refund.StatusID)
                    {

                        menbll.Updatefail(manage.openid);
                    }


                    AddAdminLog(MXEnums.ActionEnum.Edit.ToString(), "修改支付状态，主键为" + id); //记录日志
                    //JscriptMsg("修改成功！", "dingdan_confirm.aspx?shopid=" + shopid + "", "Success");
                    //Response.Redirect("dingdan_confirm.aspx?shopid=" + shopid + "");
                    Response.Write("<script language='javascript' type='text/javascript'>alert('核销成功！');location.href = 'dingdan_confirm.aspx?shopid=" + shopid + "';</script>");

                }
            }                       
        }

        public void List(int ids)
        {

            //订单

            Dingdanlist = "";
            dingdanren = "";

            DataSet dr = managebll.Getcommodity(cid);
            if (dr.Tables[0].Rows.Count > 0)
            {
                decimal amount = 0;


                if (dr.Tables[0].Rows[0]["status"].ToString() == "2")
                {
                    save_groupbase.Text = "已验证";
                    save_groupbase.Enabled = false;
                    save_groupbase.Style.Value = "";
                }
                Dingdanlist += "<tr><th>套餐名称</th><th class=\"cc\">购买份数</th><th class=\"cc\">单价</th><th class=\"rr\">总价</th> </tr>";
                for (int i = 0; i < dr.Tables[0].Rows.Count; i++)
                {
                    Dingdanlist += " <tr><td  class=\"cc\">" + dr.Tables[0].Rows[i]["cpName"] + "</td>";
                    Dingdanlist += "<td class=\"cc\">" + 1 + "</td>";
                    Dingdanlist += "<td class=\"cc\">" + dr.Tables[0].Rows[i]["price"] + "</td>";                    
                    Dingdanlist += "<td class=\"rr\">￥" + dr.Tables[0].Rows[i]["price"] + "</td></tr>";
                    amount += Convert.ToDecimal(dr.Tables[0].Rows[i]["price"]);
                }

                Dingdanlist += "<tr><td></td><td ></td><td ></td><td class=\"rr\" style=\"color: red; font-weight:bold;\">总计：￥" + amount + "</td></tr>";

            }


            manage = managebll.GetModeldingdan(id);
            //订单信息
            if (manage != null)
            {
                dingdanren += "<tr><td width=\"70\">订单编号： " + manage.orderNumber + "</td></tr>";
                dingdanren += "<tr> <td>预订日期：" + manage.oderTime + "</td></tr>";
                dingdanren += "<tr><td>预约人：" + manage.customerName + "</td></tr>";
                dingdanren += "<tr><td>电话：" + manage.customerTel + "</td></tr>";
                //dingdanren += "<tr><td>地址：" + manage.address + "</td></tr>";
                //dingdanren += "<tr><td>备注 ：" + manage.oderRemark + "</td></tr>";
                if (manage.payStatus == StatusManager.PayStatus.Payed.StatusID)
                {
                    dingdanren += "<tr><td>订单状态：<em  style='width:70px;' class='ok'>等待使用</em></td></tr>";
                }
                else if (manage.payStatus == StatusManager.PayStatus.PartUsed.StatusID)
                {
                    dingdanren += "<tr><td>订单状态：<em  style='width:70px;' class='ok'>部分使用</em></td></tr>";
                }
                else if (manage.payStatus == StatusManager.PayStatus.PartRefund.StatusID)
                {
                    dingdanren += "<tr><td>订单状态：<em  style='width:70px;' class='ok'>部分退款</em></td></tr>";
                }
                else
                {
                    dingdanren += "<tr><td>订单状态：<em  style='width:70px;' class='no'>未使用</em></td></tr>";
                }
            }
            else
            {
                dingdanren += "<tr><td width=\"70\">订单编号：</td></tr>";
                dingdanren += "<tr> <td>预订日期：</td></tr>";
                dingdanren += "<tr><td>预约人：</td></tr>";
                dingdanren += "<tr><td>电话：</td></tr>";
                //dingdanren += "<tr><td>地址：</td></tr>";
                //dingdanren += "<tr><td>备注 ：</td></tr>";


                dingdanren += "<tr><td>订单状态：<em  style='width:70px;' class='no'>未使用</em></td></tr>";

            }
        }
    }
}