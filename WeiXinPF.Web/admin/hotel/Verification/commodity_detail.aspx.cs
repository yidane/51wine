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

    using WeiXinPF.Application.DomainModules.IdentifyingCode.Service;
    using WeiXinPF.Model.KNSHotel;

    public partial class commodity_detail : Web.UI.ManagePage
    {
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
            hotelid = this.GetHotelId();
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
                    Response.Write("<script language='javascript' type='text/javascript'>alert('核销成功！');location.href = 'dingdan_confirm.aspx?shopid=" + hotelid + "';</script>");

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
                Dingdanlist += "<tr><th>商品名称</th><th class=\"cc\">购买数量</th><th class=\"cc\">单价</th><th class=\"cc\">入住时间</th><th class=\"cc\">离店时间</th></tr>";
                foreach (var item in identifyingCodeDetails)
                {
                    Dingdanlist += " <tr><td class=\"cc\">" + item.ProductName + "</td>";
                    Dingdanlist += "<td class=\"cc\">" + item.Number + "</td>";
                    Dingdanlist += "<td class=\"cc\">" + item.Price + "</td>";                    
                    Dingdanlist += "<td class=\"cc\">" + item.ArriveTime + "</td>";
                    Dingdanlist += "<td class=\"cc\">" + item.LeaveTime + "</td></tr>";
                    amount += Convert.ToDecimal(item.TotelPrice);
                }
                Dingdanlist += "<tr><td></td><td ></td><td ></td><td ></td><td class=\"rr\" style=\"color: red; font-weight:bold;\">支付总计：￥" + amount + "</td></tr>";
            }
            
            var hotelOrder = new BLL.wx_hotel_dingdan().GetModel(int.Parse(id));
            
            //订单信息
            if (hotelOrder != null)
            {
                dingdanren += "<tr><td width=\"70\">订单编号： " + hotelOrder.OrderNumber + "</td></tr>";
                dingdanren += "<tr> <td>交易日期：" + hotelOrder.orderTime + "</td></tr>";
                dingdanren += "<tr><td>预定人：" + hotelOrder.oderName + "</td></tr>";
                dingdanren += "<tr><td>电话：" + hotelOrder.tel + "</td></tr>";
            }
            else
            {
                dingdanren += "<tr><td width=\"70\">订单编号：</td></tr>";
                dingdanren += "<tr> <td>交易日期：</td></tr>";
                dingdanren += "<tr><td>预定人：</td></tr>";
                dingdanren += "<tr><td>电话：</td></tr>";
            }
        }
    }
}