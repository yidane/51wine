using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WeiXinPF.Common;

namespace WeiXinPF.Web.admin.order
{
    /// <summary>
    /// clStock 的摘要说明
    /// </summary>
    public class clStock : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/json";
            string action = MXRequest.GetQueryString("myact");
            if (action == "upStock")
            { 
                BLL.wx_shop_product spBll = new BLL.wx_shop_product();
                BLL.orders oBll = new BLL.orders();
                try
                {
                    string orderno = MyCommFun.QueryString("order_no");
                    //根据订单号得到订单
                    Model.orders oModel = oBll.GetModelByNo(orderno);
                    //得到所有订购商品
                    List<Model.order_goods> ogList = oModel.order_goods;

                    //根据订购商品的数量修改库存
                    foreach (Model.order_goods item in ogList)
                    {
                        Model.wx_shop_product spModel = spBll.GetModel(item.goods_id);
                        spModel.stock = spModel.stock - item.quantity;
                        spBll.Update(spModel);
                    } 
                    context.Response.Write("{\"status\": 1, \"msg\": \"库存操作成功！\"}");
                    return;
                }
                catch (Exception)
                {
                    context.Response.Write("{\"status\": 0, \"msg\": \"库存操作失败！\"}");
                    return;
                }


            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}