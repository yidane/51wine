<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="diancai_RefundOrderDetail.aspx.cs" Inherits="WeiXinPF.Web.weixin.restaurant.diancai_RefundOrderDetail" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8">
    <meta name="viewport" content="width=device-width,height=device-height,inital-scale=1.0,maximum-scale=1.0,user-scalable=no;">
    <meta name="apple-mobile-web-app-capable" content="yes">
    <meta name="apple-mobile-web-app-status-bar-style" content="black">
    <meta name="format-detection" content="telephone=no">
    <title><%=RestruantName %>餐饮订单</title>
    <link href="css/diancai.css" rel="stylesheet" type="text/css">
    <link href="css/swiper.min.css" rel="stylesheet" />
    <script src="js/zepto.min.js"></script>
    <script src="js/alert.js" type="text/javascript"></script>
    <script src="js/swiper.min.js"></script>
    <style>
        /*.table-bordered {*/
        /*border: 1px solid #ddd;*/
        /*}*/
        /*.table-bordered>thead>tr>th {*/
        /*border: 1px solid #ddd;*/
        /*}*/
        .table > thead > tr > th {
            vertical-align: bottom;
            border-bottom: 2px solid #ddd;
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
            color: #3c763d;
            background-color: #dff0d8;
            border-color: #d6e9c6;
        }

        .alert {
            padding: 15px;
            margin-bottom: 20px;
            border: 1px solid transparent;
            border-radius: 4px;
        }

        .top-alert {
            height: 32px;
            line-height: 32px;
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
        }

            .gpd-item-title .detailicon-ticket, .gpd-item-title .gpd-item-title-name {
                float: left;
            }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="menu_header">
            <div class="menu_topbar">
                <strong style="float: left; margin-left: 80px">餐饮订单</strong>
                <span class="head_btn_left"><a href="javascript:history.go(-1);"><span>返回</span></a><b></b></span>
                <a class="head_btn_right" href="../map/poilist.html?type=catering&wid=<%=wid %>">
                    <span><i class="menu_header_home"></i></span><b></b>
                </a>
            </div>
        </div>

        <div id="contact_info" class="cardexplain" style="margin-top: 50px;">
            <div class="alert alert-success top-alert" role="alert" style="margin-bottom: 0;">
                <span style="float: left"><span class="top-alert-name">订单编号：</span> <%=OrderNumber %></span>
                <span style="float: right"><span class="top-alert-name">总价：</span>
                    <span class="label label-danger"><%=PayAmount %>元</span></span>
            </div>

            <section class="gpd-item  gdp-curr">
                <div class="gpd-item-title">
                    <img class="detailicon-ticket" src="images/info.png" />
                    <div class="gpd-item-title-name">审核意见</div>
                    <div class="gp-icons gpd-up-icon"></div>
                </div>
                <div class="gpd-content">
                    <div runat="server" id="refundReason">
                    </div>
                </div>
            </section>

            <section class="gpd-item">
                <div class="gpd-item-title">
                    <img class="detailicon-ticket" src="images/info.png" />
                    <div class="gpd-item-title-name">详细信息</div>
                    <div class="gp-icons gpd-up-icon"></div>
                </div>
                <div class="gpd-content">
                    <div runat="server" id="detail">
                    </div>
                </div>
            </section>

            <section class="gpd-item">
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
                </div>
            </section>
        </div>
    </form>
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
            $(".gp-icons.gpd-up-icon").click(function () {
                var zThis = $(this);
                var zThisParent = zThis.parents(".gpd-item");
                zThisParent.toggleClass("gdp-curr");
                zThisParent.siblings(".gpd-item").removeClass("gdp-curr");
            });
        });
    </script>
</body>
</html>
