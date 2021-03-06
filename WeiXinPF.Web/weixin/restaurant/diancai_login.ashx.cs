﻿using WeiXinPF.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WeiXinPF.Web.weixin.WeChatPay;
using WeiXinPF.WeiXinComm;

namespace WeiXinPF.Web.weixin.restaurant
{
    using System.Transactions;
    using System.Web.Util;

    using WeiXinPF.Application.DomainModules.IdentifyingCode.Service;
    using WeiXinPF.Infrastructure.DomainDataAccess.IdentifyingCode;
    using WeiXinPF.Model;
    using WeiXinPF.Web.weixin.product;
    using WeiXinPF.Web.weixin.qiangpiao;

    using wx_diancai_caipin_manage = WeiXinPF.BLL.wx_diancai_caipin_manage;
    using wx_diancai_shopinfo = WeiXinPF.BLL.wx_diancai_shopinfo;

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

                if (order != null)
                {
                    //this.jsonDict.Add("ret", "ok");
                    //this.jsonDict.Add("content", orderProcessResult.Message);
                    //this.jsonDict.Add("orderid", order.id.ToString());
                    //this.jsonDict.Add("ordercode", order.orderNumber);
                    //this.jsonDict.Add("openid", openid);
                    //this.jsonDict.Add("payamount", order.payAmount.ToString());
                    //this.jsonDict.Add("shopname", new BLL.wx_diancai_shopinfo().GetModel(this.shopid).hotelName);
                    //this.jsonDict.Add("wid", order.wid.ToString());
                    //this.jsonDict.Add("orderNumber", order.orderNumber);
                    //context.Response.Write(MyCommFun.getJsonStr(this.jsonDict));

                    var shopInfo = new BLL.wx_diancai_shopinfo().GetModel(this.shopid);
                    var entity = new UnifiedOrderEntity
                    {
                        OrderId = order.id.ToString(),
                        wid = order.wid,
                        total_fee = order.payAmount == null ? 0 : (int)order.payAmount,
                        out_trade_no = order.orderNumber,
                        openid = openid,
                        body =string.Format("订单编号{0} {1}",order.orderNumber, shopInfo.hotelName),
                        PayModuleID = (int)PayModuleEnum.Restaurant,
                        PayComplete = "../restaurant/AfterPay.aspx"
                    };

                    entity.Extra.Add("content", orderProcessResult.Message);
                    entity.Extra.Add("shopname", new BLL.wx_diancai_shopinfo().GetModel(this.shopid).hotelName);
                    entity.Extra.Add("shopid", shopid.ToString());
                    entity.Extra.Add("wid", shopInfo.wid.ToString());

                    var ticket = EncryptionManager.CreateIV();
                    var payData = EncryptionManager.AESEncrypt(entity.ToJson(), ticket);

                    context.Response.Write(AjaxResult.Success(PayHelper.GetPayUrl(payData, ticket)));
                }

                context.Response.End();
            }
            else if (_action == "productDetail")
            {
                var productId = MyCommFun.QueryString("productId");


                if (!string.IsNullOrEmpty(productId))
                {
                    var model = new BLL.wx_diancai_caipin_manage().GetModel(int.Parse(productId));
                    String useRange = string.Empty;
                    if (model.beginDate.HasValue || model.endDate.HasValue)
                    {
                        useRange = string.Format("{0}至{1}",
                        model.beginDate.HasValue ? model.beginDate.Value.ToString("yyyy-MM-dd") : string.Empty,
                        model.endDate.HasValue ? model.endDate.Value.ToString("yyyy-MM-dd") : string.Empty);
                    }
                    var data = new
                    {
                        shopIntroduction = model.shopIntroduction,
                        detailContent = model.detailContent,
                        instructions = model.instructions,
                        chargeback = model.chargeback,
                        useRange= useRange

                    };
                    context.Response.Write(AjaxResult.Success(data).ToCamelString());

                }

                context.Response.End();
            }
            else if (_action == "caipinDetail")
            {
                var caipinId = MyCommFun.RequestInt("caipinId");
                var caipinModel = new BLL.wx_diancai_caipin_manage().GetModel(caipinId);

                String useRange = string.Empty;
                if (caipinModel.beginDate.HasValue || caipinModel.endDate.HasValue)
                {
                    useRange = string.Format("{0}至{1}",
                                                                    caipinModel.beginDate.HasValue ? caipinModel.beginDate.Value.ToString("yyyy-MM-dd") : string.Empty,
                                                                    caipinModel.endDate.HasValue ? caipinModel.endDate.Value.ToString("yyyy-MM-dd") : string.Empty);
                }

                String jianjie = caipinModel.shopIntroduction;
                String tuidanRule = caipinModel.chargeback;
                string pictureUrl = caipinModel.picUrl;

                var result = new
                {
                    Name = caipinModel.cpName,
                    Url = MapUrl(pictureUrl),
                    Range = useRange,
                    Intruduce = jianjie,
                    Rule = tuidanRule,
                    suoming=caipinModel.detailContent,
                    shopIntroduction=caipinModel.shopIntroduction
                };

                context.Response.Write(AjaxResult.Success(result));
                context.Response.End();
            }
        }

        public static string GetHostPort()
        {
            if (HttpContext.Current.Request.Url.Port == 80)
                return string.Format("http://{0}{1}/",
                HttpContext.Current.Request.Url.Host,
                HttpContext.Current.Request.ApplicationPath.TrimEnd('/'));
            else
                return string.Format("http://{0}:{1}{2}/",
                    HttpContext.Current.Request.Url.Host,
                    HttpContext.Current.Request.Url.Port,
                    HttpContext.Current.Request.ApplicationPath.TrimEnd('/'));
        }

        /// <summary>
        /// 得到完整路径
        /// </summary>
        /// <param name="url">相对url</param>
        /// <returns></returns>
        public static string MapUrl(string url)
        {
            return string.Format("{0}{1}", GetHostPort(), url);
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
                wid = new BLL.wx_diancai_shopinfo().GetModel(this.shopid).wid.Value,
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
                                var iCode = new IdentifyingCodeInfo()
                                {
                                    IdentifyingCodeId = Guid.NewGuid(),
                                    CreateTime = DateTime.Now,
                                    IdentifyingCode = string.Empty,
                                    ModifyTime = DateTime.Now,
                                    ModuleName = "restaurant",
                                    OrderCode = order.orderNumber,
                                    OrderId = order.id.ToString(),
                                    ProductCode = item.caiId.ToString(),
                                    ProductId = item.caiId.ToString(),
                                    ShopId = order.shopinfoid.ToString(),
                                    Wid = order.wid,
                                    Status = 0
                                };
                                IdentifyingCodeService.AddIdentifyingCode(iCode);
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
            catch (Exception ex)
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