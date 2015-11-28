<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="Restaurant.Master" CodeBehind="diancai_orderDetail.aspx.cs" Inherits="WeiXinPF.Web.weixin.restaurant.diancai_orderDetail" %>


<asp:Content ID="h" ContentPlaceHolderID="head" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
    <meta name="viewport" content="width=device-width,height=device-height,inital-scale=1.0,maximum-scale=1.0,user-scalable=no;" />
    <%--    <meta name="apple-mobile-web-app-capable" content="yes" />
    <meta name="apple-mobile-web-app-status-bar-style" content="black" />
    <meta name="format-detection" content="telephone=no"/>
    <meta content="yes" name="apple-mobile-web-app-capable" />
    <meta content="black" name="apple-mobile-web-app-status-bar-style" />
    <meta content="telephone=no" name="format-detection" />
    <meta content="email=no" name="format-detection" />--%>

    <title>我的订单</title>
    <link href="css/diancai.css" rel="stylesheet" type="text/css" />
    <link href="css/swiper.min.css" rel="stylesheet" />
    <script src="js/zepto.min.js"></script>
    <script src="js/alert.js" type="text/javascript"></script>
    <script src="js/swiper.min.js"></script>
    <script charset="utf-8" src="http://map.qq.com/api/js?v=2.exp&libraries=geometry&key=XQSBZ-MYPKU-EJSVT-4XWSN-QWJXH-2TBDM"></script>

    <style>
        .table > thead > tr > th {
            vertical-align: bottom;
            border-bottom: 1px solid red;
        }

        .label-primary {
            background-color: #337ab7;
        }


        .label-info {
            background-color: #5bc0de;
        }

        .label-success {
            background-color: #5cb85c;
        }

        .label-danger {
            color: #d9534f;
        }

        .label {
            display: inline;
            padding: .2em .6em .3em;
            /*font-size: 75%;*/
            font-weight: 700;
            line-height: 1;
            /*color: #fff;*/
            text-align: center;
            white-space: nowrap;
            vertical-align: baseline;
            border-radius: .25em;
        }

        .alert-success {
            border-color: #d6e9c6;
        }

        .alert {
            padding-bottom: 10px;
            padding-right: 10px;
            padding-top: 10px;
            margin-bottom: 20px;
            border: 1px solid transparent;
            border-radius: 4px;
        }

        .top-alert {
            line-height: 8px;
            vertical-align: middle;
        }

        .top-alert-name {
            font-weight: 700;
        }

        .gpd-item-title {
            width: 100%;
            float: left;
            padding-top: 10px;
            height: 32px;
            line-height: 32px;
            margin-bottom: 10px;
            padding-bottom: 6px;
        }

            .gpd-item-title .detailicon-ticket, .gpd-item-title .gpd-item-title-name {
                float: left;
            }


        .text-danger {
            color: #c32d32;
        }

        .text-success {
            color: #3c763d;
        }

        .text-muted {
            color: #777;
        }

        .text-right {
            text-align: right;
        }


        .pull-left {
            float: left !important;
        }

        .pull-right {
            float: right !important;
        }

        .item-pullleft {
            margin-right: 1px;
        }

        .img-ercode {
            border: 1px;
            border-color: wheat;
            border-style: solid;
            padding: 10px;
        }

        .refund-button {
            display: block;
            margin-bottom: 0;
            font-size: 12px;
            font-weight: 400;
            text-align: center;
            white-space: nowrap;
            vertical-align: middle;
            cursor: pointer;
            border: 1px solid transparent;
            border-radius: 4px;
            background-color: green;
            max-width: 60px;
            padding-right: 1px;
        }

        .cp {
            width: 100%;
            text-align: left;
        }

            .cp .cc {
                text-align: center !important;
            }

            .cp .rr {
                text-align: right !important;
            }

            .cp th {
                background-color: #f9f9f9;
                font-size: 12px;
                padding: 8px 10px 8px 10px;
            }

            .cp td {
                border-top: 1px solid #E6E6E6;
                font-size: 14px;
                padding: 8px 1px 8px 1px;
            }

        .swiper-image {
            height: 220px;
        }

        .full-w {
            padding: 10px;
        }

        .butto-wapper {
            width: 100%;
            margin-top: 10px;
        }

            .butto-wapper a {
                display: inline-block;
                width: 45%;
                height: 30px;
                line-height: 30px;
                background-color: #c32d32;
                background-image: url(images/icon-xiangqing.png);
                background-repeat: no-repeat;
                background-position: 10px center;
                text-align: center;
                color: #fff;
            }

            .butto-wapper .btn-refund {
                margin-left: 10%;
                background-image: url(images/icon-tuikuan.png);
                background-color: #ccc;
            }
    </style>
    <script type="text/javascript">
        //获取当前位置的坐标
        function getLocation() {
            if (navigator.geolocation) {
                navigator.geolocation.getCurrentPosition(computeDistance);
            }
        }
        function computeDistance(position) {
            var lat = position.coords.latitude;
            var lng = position.coords.longitude;
            var currentLatlng = new qq.maps.LatLng(lat, lng);
            var poiLatlng = new qq.maps.LatLng(<%=lat %>, <%=lng %>);
            var distince = Math.round(qq.maps.geometry.spherical.computeDistanceBetween(currentLatlng, poiLatlng) / 1000);

            $("#detail_distince"  ).text(distince + 'km');

            var timer = setInterval(function () {
                     
                $("#detail_distince"  ).text('店铺距离：'+distince + 'km');

                clearInterval(timer);
            }, 500);
        }

        Zepto(function ($) {

            getLocation();

            $('.silde-background').each(function (i) {
                var id = $(this).attr("id");
                var swiper = new Swiper('#' + id + ' .swiper-container', {
                    loop: false,
                    pagination: '#' + id + ' .swiper-pagination ',
                    onTransitionEnd: function (swi) {
                        var current = swi.slides[swi.activeIndex];
                        var item = current.childNodes[0];
                        var caiId = $(item).attr("CaiID");
                        $("#itemkey_"+caiId).html($(item).attr("key"));
                        $("#itemStatus_"+caiId).html($(item).attr("status"));
                    }
                });
            });
            $(".gp-icons.gpd-up-icon").click(function () {
                var zThis = $(this);
                var zThisParent = zThis.parents(".gpd-item");
                zThisParent.toggleClass("gdp-curr");
                zThisParent.siblings(".gpd-item").removeClass("gdp-curr");
            });
        });
    </script>
</asp:Content>
<asp:Content ID="c" ContentPlaceHolderID="content" runat="server" class="mode_webapp">
    <div class="cardexplain" id="contact_info" runat="server" style="margin-bottom: 50px">
        <div class="alert alert-success top-alert" role="alert">
            <span style="float: left"><span>订单编号:</span> <%=OrderNumber.Trim() %></span>
            <span style="float: right"><span class="top-alert-name">总价  <strong style="color: red"><%=PayAmount.Trim() %>元</strong></span></span>
        </div>
        <section>
            <table width="100%" border="0" cellpadding="0" cellspacing="0" class="DishDetail table table-bordered">
                <thead>
                    <tr>
                        <%--<th class="col" style="width: 5%"></th>--%>
                        <th class="Col1" style="width: 35%">类型</th>
                        <th class="Col2" style="width: 30%">份数</th>
                        <th class="Col3" style="width: 35%">总价</th>
                        <%--<th class="Col4" style="width: 23%"></th>--%>
                    </tr>
                </thead>
            </table>
        </section>
        <div runat="server" id="detail">
        </div>
        <section class="gpd-item gdp-curr">
            <div class="gpd-item-title">
                <img class="detailicon-ticket" src="images/info.png" />
                <div class="gpd-item-title-name">联系人</div>
                <div class="gp-icons gpd-up-icon"></div>
            </div>
            <div class="gpd-content">
                <div style="width: 100%; float: left">
                    <p>联系人：<%=customeName %></p>
                    <p>联系电话:<%= customerTel%></p>
                </div>
            </div>
        </section>
        <section class="gpd-item">
            <div class="gpd-item-title">
                <img class="detailicon-ticket" src="images/info.png" />
                <div class="gpd-item-title-name">店铺信息</div>

                <div class="gp-icons gpd-up-icon"></div>
            </div>
            <div class="gpd-content">
                <div style="width: 78%; float: left">
                    <p><%=RestruantName %></p>
                    <p><%=RestruantLocation %></p>
                    <p class="distince" id="detail_distince"></p>
                </div>
                <div style="float: right">
                    <a href="tel:<%=RestruantPhone %>">
                        <img src="images/telephone.png" />
                    </a>
                </div>
            </div>
        </section>
        <%--<section class="gpd-item ">
            <div class="gpd-item-title">
                <img class="detailicon-ticket" src="images/info.png" />
                <div class="gpd-item-title-name">使用须知</div>
                <div class="gp-icons gpd-up-icon"></div>
            </div>
            <div class="gpd-content">
                <p>【营业时间】</p>
                <p>11:00-20:00</p>
                <p><span>【使用条件】</span></p>
                <p>· 无须预约，消费高峰时可能需要等位</p>
                <p>· 堂食外带均可，可以打包，打包费详情咨询商家</p>
                <p>· 提供免费wifi</p>
            </div>
        </section>
        <section class="gpd-item ">
            <div class="gpd-item-title">
                <img class="detailicon-ticket" src="images/undo.png" />
                <div class="gpd-item-title-name">退单规则</div>
                <div class="gp-icons gpd-up-icon"></div>
            </div>
            <div class="gpd-content">
            </div>
        </section>

        <section class="gpd-item" style="padding-bottom: 110px">
            <div class="gpd-item-title">
                <img class="detailicon-ticket" src="images/time.png" />
                <div class="gpd-item-title-name">套餐有效期</div>
                <div style="float: right" id="dateRange"><%=orderRange %></div>
            </div>
        </section>--%>
    </div>
</asp:Content>
