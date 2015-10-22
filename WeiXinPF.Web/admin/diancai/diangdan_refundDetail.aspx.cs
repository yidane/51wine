using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WeiXinPF.BLL;
using WeiXinPF.Common;
using WeiXinPF.Web.weixin.restaurant;

namespace WeiXinPF.Web.admin.diancai
{
    public partial class diangdan_refundDetail : Web.UI.ManagePage
    {
        public string Dingdanlist = "";
        public string dingdanren = "";
        public string refundCode = string.Empty;
        public int orderID = 0;
        public int shopid = 0;
        public string openid = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            refundCode = MyCommFun.QueryString("id");
            orderID = MyCommFun.RequestInt("dingdanid");
            shopid = MyCommFun.RequestInt("shopid");
            if (!IsPostBack)
            {
                if (orderID > 0 && !string.IsNullOrEmpty(refundCode))
                {
                    List();
                }
            }
        }

        public void List()
        {
            //退单
            Dingdanlist = "";
            dingdanren = "";

            DataSet result = new wx_diancai_tuidan_manage().GetRefundDetailWithOrderDetail(shopid, orderID, refundCode);
            if (result.Tables[0].Rows.Count > 0)
            {
                //退单详情
                var dingdanStringBuilder = new StringBuilder();

                if (result.Tables[0].Rows.Count > 0 && result.Tables[0].Rows.Count > 0)
                {
                    //重新计算订单详情
                    int refundTotal = Convert.ToInt32(result.Tables[0].Rows[0]["refundAmount"]);
                    var newDataTable = new DataTable();
                    newDataTable.Columns.Add("Name");
                    newDataTable.Columns.Add("BuyCount");
                    newDataTable.Columns.Add("BuyPrice");
                    newDataTable.Columns.Add("Total");
                    newDataTable.PrimaryKey = new DataColumn[] { newDataTable.Columns[0] };

                    foreach (DataRow row in result.Tables[0].Rows)
                    {
                        var currentRow = newDataTable.Rows.Find(row["cpName"]);
                        if (currentRow != null) continue;
                        currentRow = newDataTable.NewRow();
                        currentRow["Name"] = row["cpName"];
                        currentRow["BuyPrice"] = row["price"];
                        newDataTable.Rows.Add(currentRow);
                    }

                    foreach (DataRow row in newDataTable.Rows)
                    {
                        var currentRows = result.Tables[0].Select(string.Format("cpName='{0}'", row["Name"]));
                        row["BuyCount"] = currentRows.Length;
                        row["Total"] = Convert.ToDouble(row["BuyPrice"]) * currentRows.Length;
                    }


                    dingdanStringBuilder.AppendFormat("<tr><th>商品信息名称</th><th class=\"cc\">单价</th><th class=\"cc\">购买份数</th><th class=\"rr\">总价</th> </tr>");
                    foreach (DataRow row in newDataTable.Rows)
                    {
                        dingdanStringBuilder.AppendFormat("<tr><td>{0}</td>", row["Name"]);
                        dingdanStringBuilder.AppendFormat("<td class=\"cc\">{0}</td>", row["BuyPrice"]);
                        dingdanStringBuilder.AppendFormat("<td class=\"cc\">{0}</td>", row["BuyCount"]);
                        dingdanStringBuilder.AppendFormat("<td class=\"rr\">￥{0}</td></tr>", row["Total"]);
                    }

                    dingdanStringBuilder.AppendFormat("<tr><td></td><td ></td><td ></td><td class=\"rr\">总计：<span class='text-danger'>￥{0}</span></td></tr>", refundTotal / 100);

                    Dingdanlist = dingdanStringBuilder.ToString();
                }

                //退单信息
                var tuidanBuilder = new StringBuilder();
                if (result.Tables.Count > 1 && result.Tables[1].Rows.Count > 0)
                {
                    tuidanBuilder.AppendFormat("<tr><td width=\"70\">退单编号： {0}</td></tr>", result.Tables[1].Rows[0]["refundCode"]);
                    tuidanBuilder.AppendFormat("<tr> <td>退单日期：{0}</td></tr>", result.Tables[1].Rows[0]["createDate"]);

                    var url = string.Format("dingdan_deal.aspx?id={0}&shopid={1}", result.Tables[1].Rows[0]["dingdanid"], result.Tables[1].Rows[0]["shopinfoid"]);
                    tuidanBuilder.AppendFormat("<tr> <td>订单号：{0}<a href='{1}' class='btn btn-primary btn-lg active' role='button'>订单查看</a></td></tr>", result.Tables[1].Rows[0]["orderNumber"], url);
                    tuidanBuilder.AppendFormat("<tr><td>退单人：{0}</td></tr>", result.Tables[1].Rows[0]["customerName"]);
                    tuidanBuilder.AppendFormat("<tr><td>电话：{0}</td></tr>", result.Tables[1].Rows[0]["customerTel"]);
                    var refundStatus = Convert.ToInt32(result.Tables[1].Rows[0]["refundStatus"]);

                    tuidanBuilder.AppendFormat("<tr><td>退单状态：<em  style='width:70px;' class='{0}'>{1}</em></td></tr>", "ok", StatusManager.DishStatus.GetStatusDict(refundStatus).StatusName);
                }
                else
                {
                    tuidanBuilder.AppendFormat("<tr><td width=\"70\">退单编号：</td></tr>");
                    tuidanBuilder.AppendFormat("<tr> <td>下单时间：</td></tr>");
                    tuidanBuilder.AppendFormat("<tr><td>联系人：</td></tr>");
                    tuidanBuilder.AppendFormat("<tr><td>联系电话：</td></tr>");
                    tuidanBuilder.AppendFormat("<tr><td>地址：</td></tr>");
                    tuidanBuilder.AppendFormat("<tr><td>备注 ：</td></tr>");

                    tuidanBuilder.AppendFormat("<tr><td>退单状态：<em  style='width:70px;' class='no'>未处理</em></td></tr>");
                }

                dingdanren = tuidanBuilder.ToString();
            }
        }

        protected void btnAgreeRefund_Click(object sender, EventArgs e)
        {
            try
            {
                

                var shopInfo = new BLL.wx_diancai_shopinfo().GetModel(shopid);
                var wxModel = new BLL.wx_userweixin().GetModel((int)shopInfo.wid);
                var certificatePath = string.Format(@"{0}{1}\{2}\apiclient_cert.p12", AppDomain.CurrentDomain.BaseDirectory, "Certificaters", wxModel.AppId);
                if (System.IO.File.Exists(certificatePath))
                {

                }
            }
            catch (Exception exception)
            {

            }
        }

        protected void btnDisAgreeRefund_Click(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception exception)
            {

            }
        }
    }
}