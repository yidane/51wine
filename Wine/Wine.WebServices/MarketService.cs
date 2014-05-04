using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wine.Infrastructure.Model.Commodity;
using Wine.Infrastructure.Model.Orders;
using Wine.Infrastructure.Model.User;
using Wine.WebFacade.Commodity;
using Wine.WebFacade.Orders;

namespace Wine.WebServices
{
    public class MarketService
    {
        public List<Goods> QueryWineList(int wineType, int id, out int sum, out GoodsType goodsType)
        {
            goodsType = new GoodsTypeFacade().QueryAllGoodsType().Find(type => type.TypeID == wineType);
            var goodsList = new GoodsFacade().QueryGoods(wineType, id, out sum);
            return goodsList ?? new List<Goods>();
        }

        public Goods QueryGoodsDetail(int id)
        {
            return new GoodsFacade().QueryGood(id);
        }

        public bool CommitOrder(Order order)
        {
            return new OrderFacade().CommitOrder(order);
        }

        public string QueryMyOrder(object customerSession)
        {
            try
            {
                if (customerSession == null)
                    return new WebServiceResult<object>(false, null, "登录已超时").ToString();
                var customer = customerSession as Customer;

                var orderList = new OrderFacade().QueryMyOrder(customer);
                var outputString = string.Empty;
                if (orderList == null || orderList.Count == 0)
                    return string.Empty;

                orderList.ForEach(order => outputString = outputString + OrderToString(order));
                return new WebServiceResult<object>(true, outputString, string.Empty).ToString();
            }
            catch (Exception ex)
            {
                return new WebServiceResult<object>(false, null, ex.Message).ToString();
            }
        }

        private string OrderToString(Order order)
        {
            var output = @"<li>
                    <div class='info'>
                        <span class='shipped'></span>
                        <a class='touch' title='' target=''>
                            <p class='id'>订单号：{0}</p>
                            <p class='price'>订单金额：<strong>&yen;&nbsp;{1}</strong></p>
                            <p class='price'>运费：<strong>&yen;&nbsp;10</strong></p>
                            <p class='date'>下单时间：{2}</p>
                            <div class='total clearfix'>
                                <p class='package'>1个包裹</p>
                                <p class='num-pro'>共1件商品</p>
                            </div>
                            <span class='more'><span class='inner'></span></span>
                        </a>
                    </div>
                    <div class='product clearfix'>
                        <a class=''>
                            <img src='{3}' alt='' /></a>
                    </div>
                </li>";
            string url = string.Empty;
            if (order.Details != null && order.Details.Count > 0)
                url = order.Details[0].PictureUrl;

            string[] args = { order.OrderNO, order.TotalPrice.ToString("0.00"), order.OrderTime.ToString(), url };
            return string.Format(output, args);
        }
    }
}