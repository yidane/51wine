<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="diancai_orderDetail.aspx.cs" Inherits="WeiXinPF.Web.weixin.restaurant.diancai_orderDetail" %>

<!DOCTYPE>
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8">
    <meta name="viewport" content="width=device-width,height=device-height,inital-scale=1.0,maximum-scale=1.0,user-scalable=no;">
    <meta name="apple-mobile-web-app-capable" content="yes">
    <meta name="apple-mobile-web-app-status-bar-style" content="black">
    <meta name="format-detection" content="telephone=no">
    <title><%=RestruantName %>餐饮订单</title>
    <link href="css/diancai.css" rel="stylesheet" type="text/css">
    <link href="css/swiper.min.css" rel="stylesheet" />
    <script src="js/jquery.min.js" type="text/javascript"></script>
    <script src="js/alert.js" type="text/javascript"></script>
    <script src="js/swiper.min.js"></script>
    <script src="js/zepto.min.js"></script>
</head>
<body class="mode_webapp">
    <form id="form1" runat="server">
        <div class="menu_header">
            <div class="menu_topbar">
                <strong style="float: left; margin-left: 80px">餐饮订单</strong>
                <span class="head_btn_left"><a href="javascript:history.go(-1);"><span>返回</span></a><b></b></span>
                <a class="head_btn_right" href="caidan_shangjia.aspx?shopid=4&openid=1">
                    <span><i class="menu_header_home"></i></span><b></b>
                </a>
            </div>
        </div>

        <div id="contact_info" class="cardexplain">
            <section>
                <div style="float: left">订单编号123</div>
                <div style="float: right"><strong style="font-size: 1.2em; right: 2px">总价 </strong>68元</div>
            </section>
            <section>
                <table width="100%" border="0" cellpadding="0" cellspacing="0" class="DishDetail">
                    <tr>
                        <td class="Col1" style="width: 33%">类型</td>
                        <td class="Col2" style="width: 17%">份数</td>
                        <td class="Col3" style="width: 25%">总价</td>
                        <td class="Col4" style="width: 23%"></td>
                    </tr>
                </table>
            </section>
            <div>
                <table width="100%" border="0" cellpadding="0" cellspacing="0" class="DishDetail">
                    <tr>
                        <td class="Col1" style="width: 33%">脆鸡八分堡套餐</td>
                        <td class="Col2" style="width: 17%">1</td>
                        <td class="Col3" style="width: 25%">25元</td>
                        <td class="Col4" style="width: 23%">
                            <asp:Button ID="Button1" runat="server" Text="申请退款" />
                        </td>
                    </tr>
                </table>
                <div class="silde-background">
                    <div class="swiper-container gpd-ablum swiper-container-horizontal">
                        <div class="swiper-wrapper silde-center" style="transition-duration: 0ms; transform: translate3d(0px, 0px, 0px);" id="swiperContain">
                            <div class='swiper-slide'>
                                <img id='123' class='img-border' src="ErCodeHandler.ashx?key=123123123">
                            </div>
                            <div class='swiper-slide'>
                                <img id='Img1' class='img-border' src="ErCodeHandler.ashx?key=123123123">
                            </div>
                            <div class='swiper-slide'>
                                <img id='Img2' class='img-border' src="ErCodeHandler.ashx?key=123123123">
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <section>
                <table width="100%" border="0" cellpadding="0" cellspacing="0" class="cpbiaoge">
                    <tr>
                        <td class="cc" style="width: 33%">脆鸡八分堡套餐</td>
                        <td class="cc" style="width: 17%">1</td>
                        <td class="cc" style="width: 25%">25元</td>
                        <td class="cc" style="width: 23%">
                            <asp:Button ID="Button2" runat="server" Text="申请退款" />
                        </td>
                    </tr>
                </table>
                <div class="silde-background">
                    <div class="swiper-container gpd-ablum swiper-container-horizontal">
                        <div class="swiper-wrapper silde-center" style="transition-duration: 0ms; transform: translate3d(0px, 0px, 0px);" id="Div1">
                            <div class='swiper-slide'>
                                <img id='Img3' class='img-border' src="ErCodeHandler.ashx?key=123123123">
                            </div>
                            <div class='swiper-slide'>
                                <img id='Img4' class='img-border' src="ErCodeHandler.ashx?key=123123123">
                            </div>
                            <div class='swiper-slide'>
                                <img id='Img5' class='img-border' src="ErCodeHandler.ashx?key=123123123">
                            </div>
                        </div>
                    </div>
                </div>
            </section>
            <section>
                <div>
                    联系人
                        <hr />
                    <div>
                        联系人：张某
                    </div>
                    <div>
                        联系电话:18910051235
                    </div>
                </div>
            </section>
            <section>
                店铺信息
                    <hr />
                <div style="width: 78%; float: left">
                    喀纳斯FKC
                </div>
                <div style="float: right">
                    <a href="tel:15201261252">
                        <img src="images/2yFKO6TwKI.png" />
                    </a>
                </div>
            </section>
            <section class="gpd-item">
                <div class="gpd-item-title">
                    <img class="detailicon-ticket" src="images/info.png" />使用须知<div class="gp-icons gpd-up-icon"></div>
                </div>
                <div class="gpd-content">
                    <p>【取票方式】</p>
                    <p>喀纳斯景区内所有景点，注：家房屋需单独购票</p>
                    <p><span>【购票条件】</span></p>
                    <p>特惠半价票，适用于1.2米以下的儿童以及65-70岁老人</p>
                    <p>【入园】</p>
                    <p>微信购票后凭电子票二维码扫码入园</p>
                    <p>【发票】</p>
                    <p>微信购票暂不支持开具发票，敬请期待后续优化</p>
                </div>
            </section>

            <section class="gpd-item ">
                <div class="gpd-item-title">
                    <img class="detailicon-ticket" src="images/undo.png" />退单规则<div class="gp-icons gpd-up-icon"></div>
                </div>
                <div class="gpd-content">
                    <p>1、支付成功后，可在票据有效期内申请退票，过期则视为作废不予受理退票</p>
                    <p>2、可在我的订单中申请退票，申请后会先审核</p>
                    <p>3、工作人员会在1~2个工作日内处理您的退票申请</p>
                    <p>4、审核通过后，支付款额会自动退回微信钱包</p>
                    <p>5、微信门票一经扫码入园后，不予退票，如二进票入园一次后即不予退票</p>
                </div>
            </section>

            <section class="gpd-item ">
                <div class="gpd-item-title">
                    <img class="detailicon-ticket" src="images/time.png" />订单有效期 <span style="float: right" id="dateRange"></span>
                    <div class="line-title"></div>
                </div>
            </section>
        </div>

        <script type="text/javascript">
            var swiper = new Swiper('.swiper-container', {
                loop: false,
                pagination: '.swiper-pagination',
                onTransitionEnd: function (swi) {
                    var current = swi.slides[swi.activeIndex];
                    var ticketid = current.childNodes[0].id;
                    $("#ticketCode").html(ticketid.split('_')[0]);
                    $("#ticketStatus").html(ticketid.split('_')[1]);
                }
            });

            Zepto(function ($) {
                $(".gp-icons.gpd-up-icon").click(function () {
                    var zThis = $(this);
                    var zThisParent = zThis.parents(".gpd-item");
                    zThisParent.toggleClass("gdp-curr");
                    zThisParent.siblings(".gpd-item").removeClass("gdp-curr");
                });
            });
        </script>

    </form>
</body>
