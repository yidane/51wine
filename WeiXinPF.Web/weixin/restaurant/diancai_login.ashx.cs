using WeiXinPF.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WeiXinPF.Web.weixin.restaurant
{
    using System.Transactions;
    using System.Web.Util;

    using WeiXinPF.Model;
    using WeiXinPF.Web.weixin.product;
    using WeiXinPF.Web.weixin.qiangpiao;

    using wx_diancai_caipin_manage = WeiXinPF.BLL.wx_diancai_caipin_manage;

    /// <summary>
    /// diancai_login 的摘要说明
    /// 李 朴
    /// http://uweixin.cn
    /// </summary>
    public class diancai_login : IHttpHandler
    {
        private string goodsData { get; set; }
        private string openid { get; set; }
        private int shopid { get; set; }

        Dictionary<string, string> jsonDict = new Dictionary<string, string>();

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/json";
            string _action = MyCommFun.QueryString("myact");
            string username = MyCommFun.QueryString("username");
            string parssword = MyCommFun.QueryString("parssword");
            string id = MyCommFun.QueryString("id");
            openid = MyCommFun.QueryString("openid");
            string state = MyCommFun.QueryString("state");
            goodsData = QueryString("goodsData");
            shopid = MyCommFun.RequestInt("shopid");


            BLL.wx_diancai_dianyuan dianyuanbll = new BLL.wx_diancai_dianyuan();
            BLL.wx_diancai_dingdan_manage manage = new BLL.wx_diancai_dingdan_manage();
            Model.wx_diancai_dingdan_manage managemodel = new Model.wx_diancai_dingdan_manage();


            if (_action == "login")
            {
                if (dianyuanbll.Exists(username, parssword))
                {
                    jsonDict.Add("ret", "ok");
                    jsonDict.Add("content", "登录成功！");

                    context.Response.Write(MyCommFun.getJsonStr(jsonDict));
                }
                else
                {
                    jsonDict.Add("ret", "fail");
                    jsonDict.Add("content", "密码错误！");

                    context.Response.Write(MyCommFun.getJsonStr(jsonDict));
                }
            }

            else if (_action == "setstatus")
            {
                //id

                if (manage.Updatestatus(id, state))
                {
                    managemodel = manage.GetModel(MyCommFun.Str2Int(id));
                    BLL.wx_diancai_member menbll = new BLL.wx_diancai_member();
                    if (state == "1")
                    {

                        menbll.Update(managemodel.openid);
                    }
                    if (state == "2")
                    {

                        menbll.Updatefail(managemodel.openid);
                    }

                    jsonDict.Add("ret", "ok");
                    jsonDict.Add("content", "提交成功！");
                    context.Response.Write(MyCommFun.getJsonStr(jsonDict));
                }

            }
            else if (_action == "addcaidan")
            {
                var orderProcessResult = this.ProcessOrder();

                if (!orderProcessResult.IsSuccess)
                {
                    this.jsonDict.Add("ret", "err");
                    this.jsonDict.Add("content", orderProcessResult.Message);
                    context.Response.Write(MyCommFun.getJsonStr(jsonDict));
                    return;
                }

                var order = orderProcessResult.BusinessData as Model.wx_diancai_dingdan_manage;

                // 支付处理程序，可在前台处理，参数通过完成订单后的返回参数传递

                // 完成订单处理
                this.jsonDict.Add("ret", "ok");
                this.jsonDict.Add("content", orderProcessResult.Message);
                context.Response.Write(MyCommFun.getJsonStr(this.jsonDict));
                context.Response.End();
            }else if (_action == "afterpayment")
            {
                var afterPaymentProcessResult = this.AfterPaymentProcess();

                if (!afterPaymentProcessResult.IsSuccess)
                {
                    this.jsonDict.Add("ret", "err");
                    this.jsonDict.Add("content", afterPaymentProcessResult.Message);
                    context.Response.Write(MyCommFun.getJsonStr(this.jsonDict));
                    return;
                }
                else
                {
                    this.jsonDict.Add("ret", "ok");
                    this.jsonDict.Add("content", afterPaymentProcessResult.Message);
                    context.Response.Write(MyCommFun.getJsonStr(this.jsonDict));
                    context.Response.End();
                }
            }
        }

        #region 处理订单

        /// <summary>
        /// The process order.
        /// </summary>
        /// <param name="context">
        /// The context.
        /// </param>
        /// <returns>
        /// The <see cref="ProcessResult"/>.
        /// </returns>
        private ProcessResult ProcessOrder()
        {
            if (this.IsParametersEmpty())
            {
                return new ProcessResult() { IsSuccess = false, Message = "订单提交失败,参数为空值！" };
            }

            var memberResult = this.ProcessMemberInfo();
            if (!memberResult.IsSuccess)
            {
                return memberResult;
            }

            return this.SaveOrder();            
        }

        /// <summary>
        /// The validate parameters.
        /// </summary>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        private bool IsParametersEmpty()
        {
            return string.IsNullOrEmpty(this.goodsData) || string.IsNullOrEmpty(this.openid);
        }

        /// <summary>
        /// The process member info.
        /// </summary>
        /// <returns>
        /// The <see cref="ProcessResult"/>.
        /// </returns>
        private ProcessResult ProcessMemberInfo()
        {
            var menberbll = new BLL.wx_diancai_member();
            var member = menberbll.GetModel(shopid, openid);

            if (member == null)
            {
                member = new Model.wx_diancai_member
                {
                    shopid = this.shopid,
                    openid = this.openid,
                    Name = MyCommFun.QueryString("name"),
                    memberName = MyCommFun.QueryString("name"),
                    menberTel = MyCommFun.QueryString("phone"),
                    memberAddress = string.Empty,
                    successDingdan = 0,
                    failDingdan = 0,
                    cancelDingdan = 0,
                    zongjifen = 0,
                    zongcje = 0,
                    status = 1,
                    createDate = DateTime.Now
                };

                menberbll.Add(member);
            }
            else
            {
                if (member.status.Value == 0)
                {
                    return new ProcessResult() { IsSuccess = false, Message = "您处于黑名单里,不能下单！" };
                }

                member.Name = MyCommFun.QueryString("name");
                member.memberName = MyCommFun.QueryString("name");
                member.menberTel = MyCommFun.QueryString("phone");

                menberbll.Update(member);
            }

            return new ProcessResult() { IsSuccess = true, Message = string.Empty };
        }        

        /// <summary>
        /// The create order.
        /// </summary>
        /// <returns>
        /// The <see cref="wx_diancai_dingdan_manage"/>.
        /// </returns>
        private Model.wx_diancai_dingdan_manage CreateOrder()
        {
            var arrGoods = this.goodsData.Split(';');

            if (arrGoods.Length < 1)
            {
                return null;
            }

            var order = new Model.wx_diancai_dingdan_manage
            {
                shopinfoid = this.shopid,
                openid = this.openid,
                orderNumber = Utils.Number(13),
                deskNumber = string.Empty,
                customerName = MyCommFun.QueryString("name"),
                customerTel = MyCommFun.QueryString("phone"),
                address = MyCommFun.QueryString("address"),
                oderRemark = MyCommFun.QueryString("oderRemark"),
                payStatus = 0,
                oderTime = DateTime.Now,
                createDate = DateTime.Now,
                payAmount = 0
            };

            for (var i = 0; i < arrGoods.Length - 1; i++)
            {
                var sAr = arrGoods[i].Split(',');
                order.payAmount += Convert.ToInt32(sAr[1]) * Convert.ToDecimal(sAr[2]); // 总价
            }

            order.id = new BLL.wx_diancai_dingdan_manage().Add(order);

            return order;
        }

        /// <summary>
        /// The save order.
        /// </summary>
        /// <returns>
        /// The <see cref="ProcessResult"/>.
        /// </returns>
        private ProcessResult SaveOrder()
        {
            var bll = new BLL.wx_diancai_dingdan_caiping();
            Model.wx_diancai_dingdan_manage order = null;

            try
            {
                using (var scope = new TransactionScope())
                {
                    order = this.CreateOrder();
                    if (order != null)
                    {
                        var goodsInOrder = this.CreateGoodsInOrder(order);

                        if (goodsInOrder == null)
                        {
                            return new ProcessResult() { IsSuccess = false, Message = "所选择的商品有误" };
                        }

                        foreach (var item in goodsInOrder)
                        {
                            bll.Add(item);

                            for (var i = 0; i < item.num; i++)
                            {
                                bll.AddCommodity(item);
                            }
                        }
                    }
                    else
                    {
                        return new ProcessResult() { IsSuccess = false, Message = "所选择的商品有误" };
                    }

                    scope.Complete();
                }
            }
            catch (Exception)
            {
                return new ProcessResult() { IsSuccess = false, Message = "保存订单出错" };
            }

            return new ProcessResult() { IsSuccess = true, Message = "订单提交成功！请到订单查看！", BusinessData = order };
        }

        /// <summary>
        /// The create goods in order.
        /// </summary>
        /// <param name="order">
        /// The order.
        /// </param>
        /// <returns>
        /// The <see cref="IEnumerable"/>.
        /// </returns>
        private IEnumerable<Model.wx_diancai_dingdan_caiping> CreateGoodsInOrder(Model.wx_diancai_dingdan_manage order)
        {
            var arrGoods = this.goodsData.Split(';');
            var allGoods = new List<Model.wx_diancai_dingdan_caiping>();

            if (arrGoods.Length < 1)
            {
                return null;
            }

            for (var i = 0; i < arrGoods.Length - 1; i++)
            {
                var sAr = arrGoods[i].Split(',');

                allGoods.Add(new wx_diancai_dingdan_caiping()
                                 {
                                     dingId = order.id,
                                     caiId = Convert.ToInt32(sAr[0]), // 菜品ID
                                     num = Convert.ToInt32(sAr[1]), // 菜品件数
                                     price = Convert.ToDecimal(sAr[2]), // 菜品单价
                                     totpric = Convert.ToInt32(sAr[1]) * Convert.ToDecimal(sAr[2]) // 总价
                                 });
            }

            return allGoods;
        }

        #endregion

        #region 支付成功后处理程序

        /// <summary>
        /// The after payment process.
        /// </summary>
        /// <returns>
        /// The <see cref="ProcessResult"/>.
        /// </returns>
        private ProcessResult AfterPaymentProcess()
        {
            // 微信支付id
            var wid = MyCommFun.QueryString("wid");

            // 订单id
            var orderid = MyCommFun.QueryString("orderid");

            if (!string.IsNullOrEmpty(wid) && !string.IsNullOrEmpty(orderid))
            {
                var bll = new BLL.wx_diancai_dingdan_manage();
                var order = bll.GetModel(int.Parse(orderid));

                if (order != null)
                {
                    order.wid = wid;
                    order.payStatus = 1;
                    order.oderTime = DateTime.Now;

                    try
                    {
                        using (var scope = new TransactionScope())
                        {
                            bll.Update(order);
                            bll.UpdateCommodityStatusByOrderId(orderid, "1");

                            scope.Complete();
                        }
                    }
                    catch (Exception)
                    {
                        return new ProcessResult() { IsSuccess = false, Message = "保存支付信息失败" };
                    }                    
                }
                else
                {
                    return new ProcessResult() { IsSuccess = false, Message = "无效的订单号" };
                }
            }
            else
            {
                return new ProcessResult() { IsSuccess = false, Message = "参数不能为空" };
            }

            return new ProcessResult() { IsSuccess = true, Message = string.Empty };
        }

        #endregion

        public static string QueryString(string param)
        {
            if (HttpContext.Current.Request[param] == null || HttpContext.Current.Request[param].ToString().Trim() == "")
            {
                return "";
            }
            string ret = HttpContext.Current.Request[param].ToString().Trim();
            return ret;
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        private class ProcessResult
        {
            public bool IsSuccess { get; set; }

            public string Message { get; set; }

            public object BusinessData { get; set; }
        }
    }
}