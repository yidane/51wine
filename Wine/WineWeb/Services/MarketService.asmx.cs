using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using Wine.Infrastructure.Model.Orders;
using Wine.WebFacade.Orders;

namespace WineWeb.Services
{
    /// <summary>
    /// MarketService 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
    // [System.Web.Script.Services.ScriptService]
    public class MarketService : System.Web.Services.WebService
    {
        [WebMethod(EnableSession = true)]
        public void QueryOrderList(int page, int rows)
        {
            if (Session["LoginUser"] != null)
            {
                var result = new Wine.WebFacade.Orders.OrderFacade().QueryAllOrderByPage(page, rows);
                if (result == null)
                    result = new List<Order>();
                var total = result.Count;
                var rowList = new List<OrderView>();
                result.ForEach(order => rowList.Add(new OrderView(order)));
                var json = new
                {
                    total = total,
                    rows = rowList
                };

                Context.Response.ContentType = "json";
                Context.Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(json));
            }
        }

        [WebMethod(EnableSession = true)]
        public void CloseOrder(int customerId, int orderId)
        {
            if (Session["LoginUser"] != null)
            {
                var result = new Wine.WebFacade.Orders.OrderFacade().CloseOrder(customerId, orderId);
                Context.Response.ContentType = "json";
                var output = new
                {
                    IsSucceed = true,
                    Result = result
                };
                Context.Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(output));
            }
        }
    }

    public class OrderView
    {
        public OrderView(Order order)
        {
            if (order != null)
            {
                this.OrderID = order.OrderID;
                this.OrderStatus = order.OrderStatus.ToString();
                this.ConsigneeMobile = order.ConsigneeMobile;
                this.Adress = order.Adress;
                this.ConsigneeUserName = order.ConsigneeUserName;
                this.TotalPrice = order.TotalPrice.ToString("0.00");
                this.CustomerID = order.OrderOwnerID;

                if (order.Details != null && order.Details.Count > 0)
                {
                    var detail = order.Details[0];
                    this.GoodsName = detail.GoodsName;
                    this.GoodsPrice = detail.GoodsPrice.ToString("0.00");
                    this.GoodsCount = detail.GoodsCount.ToString();
                }
            }
        }

        public int OrderID { get; set; }
        public int CustomerID { get; set; }
        public string OrderStatus { get; set; }
        public string Adress { get; set; }
        public string ConsigneeUserName { get; set; }
        public string ConsigneeMobile { get; set; }
        public string GoodsName { get; set; }
        public string GoodsPrice { get; set; }
        public string GoodsCount { get; set; }
        public string TotalPrice { get; set; }
    }
}
