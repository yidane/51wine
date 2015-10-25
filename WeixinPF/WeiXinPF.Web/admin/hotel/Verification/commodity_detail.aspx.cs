using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WeiXinPF.Common;

namespace WeiXinPF.Web.admin.hotel.Verification
{
    using System.Globalization;
    using System.Transactions;

    using WeiXinPF.Application.DomainModules.IdentifyingCode.Service;
    using WeiXinPF.Model.KNSHotel;

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
        public int hotelid = 0;
        public string openid = "";

        public int wid = 0;

        public const string ModuleName = "hotel";
        protected void Page_Load(object sender, EventArgs e)
        {
            ids = MyCommFun.RequestInt("id");
            id = MyCommFun.QueryString("id");
            cid = MyCommFun.QueryString("cid");
            hotelid = MyCommFun.RequestInt("shopid") == 0 ? this.GetHotelId() : MyCommFun.RequestInt("shopid");
            wid = MyCommFun.RequestInt("wid") == 0 ? this.GetWeiXinCode().id : MyCommFun.RequestInt("wid");
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
            const string status = "2";

            Guid identifyingCodeId;

            if (Guid.TryParse(this.cid, out identifyingCodeId))
            {
                var identifyingCodeObject = IdentifyingCodeService.GetIdentifyingCodeInfoByIdentifyingCodeId(identifyingCodeId, ModuleName, this.wid);

                if (identifyingCodeObject != null && identifyingCodeObject.ShopId.Equals(this.hotelid.ToString(CultureInfo.InvariantCulture)))
                {
                    identifyingCodeObject.Status = 2;
                    identifyingCodeObject.ModifyTime = DateTime.Now;

                    IdentifyingCodeService.ModifyIdentifyingCodeInfo(identifyingCodeObject);
                    
                    AddAdminLog(MXEnums.ActionEnum.Edit.ToString(), "修改支付状态，主键为" + id); //记录日志
                    //JscriptMsg("修改成功！", "dingdan_confirm.aspx?shopid=" + shopid + "", "Success");
                    //Response.Redirect("dingdan_confirm.aspx?shopid=" + shopid + "");
                    Response.Write("<script language='javascript' type='text/javascript'>alert('修改成功！');location.href = 'dingdan_confirm.aspx?shopid=" + hotelid + "';</script>");

                }
            }                       
        }

        public void List(int ids)
        {
            //订单
            Dingdanlist = "";
            dingdanren = "";

            var identifyingCodeDetails = IdentifyingCodeService.GetIdentifyingCodeDetailById(cid, "hotel");

            if (identifyingCodeDetails != null && identifyingCodeDetails.Any())
            {
                decimal amount = 0;


                if (identifyingCodeDetails.FirstOrDefault().Status == 2)
                {
                    save_groupbase.Text = "已验证";
                    save_groupbase.Enabled = false;
                    save_groupbase.Style.Value = "";
                }
                Dingdanlist += "<tr><th>商品名称</th><th class=\"cc\">单价</th><th class=\"cc\">购买数量</th><th class=\"rr\">价格</th> </tr>";
                foreach (var item in identifyingCodeDetails)
                {
                    Dingdanlist += " <tr><td>" + item.ProductName + "</td>";
                    Dingdanlist += "<td class=\"cc\">" + item.Price + "</td>";
                    Dingdanlist += "<td class=\"cc\">" + 1 + "</td>";
                    Dingdanlist += "<td class=\"rr\">￥" + item.Price + "</td></tr>";
                    amount += Convert.ToDecimal(item.Price);
                }

                sjopmodel = shopinfo.GetModel(hotelid);//配送费
                Dingdanlist += "<tr><td>总计：</td><td ></td><td ></td><td class=\"rr\">￥" + amount + "</td></tr>";
            }

            var hotelOrder = new BLL.wx_hotel_dingdan().GetModel(int.Parse(id));
            //manage = managebll.GetModeldingdan(id);
            //订单信息
            if (hotelOrder != null)
            {
                dingdanren += "<tr><td width=\"70\">订单编号： " + hotelOrder.OrderNumber + "</td></tr>";
                dingdanren += "<tr> <td>下单时间：" + hotelOrder.orderTime + "</td></tr>";
                dingdanren += "<tr><td>联系人：" + hotelOrder.oderName + "</td></tr>";
                dingdanren += "<tr><td>联系电话：" + hotelOrder.tel + "</td></tr>";
                dingdanren += "<tr><td>备注 ：" + hotelOrder.remark + "</td></tr>";
                dingdanren += "<tr><td>订单状态：<em  style='width:70px;' class='ok'>" + HotelStatusManager.OrderStatus.GetStatusDict(hotelOrder.orderStatus.Value).StatusName + "</em></td></tr>";
            }
            else
            {
                dingdanren += "<tr><td width=\"70\">订单编号：</td></tr>";
                dingdanren += "<tr> <td>下单时间：</td></tr>";
                dingdanren += "<tr><td>联系人：</td></tr>";
                dingdanren += "<tr><td>联系电话：</td></tr>";
                dingdanren += "<tr><td>备注 ：</td></tr>";
                dingdanren += "<tr><td>订单状态：<em  style='width:70px;' class='no'>未使用</em></td></tr>";

            }


            dingdanren += "<tr><td>商家留言：</td></tr> <tr> <td></td></tr>";
        }
    }
}