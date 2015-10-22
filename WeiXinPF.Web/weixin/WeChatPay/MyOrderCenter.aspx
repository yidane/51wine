<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MyOrderCenter.aspx.cs" Inherits="WeiXinPF.Web.weixin.WeChatPay.MyOrderCenter" %>

<%@ Import Namespace="WeiXinPF.Common" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
    <meta name="viewport" content="width=device-width,height=device-height,inital-scale=1.0,maximum-scale=1.0,user-scalable=no;" />
    <meta name="apple-mobile-web-app-capable" content="yes" />
    <meta name="apple-mobile-web-app-status-bar-style" content="black" />
    <meta name="format-detection" content="telephone=no" />
    <link href="../../scripts/swiper/swiper.min.css" rel="stylesheet" />
    <style type="text/css">
        * {
            margin: 0;
            outline: 0;
            padding: 0;
            -webkit-tap-highlight-color: rgba(0,0,0,0);
        }

        a {
            text-decoration: none;
            -webkit-tap-highlight-color: rgba(0,0,0,.35);
        }

        html {
            height: 100%;
            -webkit-text-size-adjust: 100%;
        }

        body {
            width: 100%;
            height: 100%;
            font-family: 'Microsoft Yahei',Tahoma,Helvetica,Arial,sans-serif;
            font-size: 62.5%;
            font-weight: 300;
            line-height: 1.231;
            position: relative;
            text-align: center;
        }

        ul, li {
            list-style: none;
        }

        a {
            text-decoration: none;
            color: #000;
            -webkit-tap-highlight-color: transparent;
        }

            a:visited {
                color: #000;
            }

        .container {
            width: 100%;
            height: 100%;
        }

        .banner {
            width: 100%;
            height: 200px;
        }

            .banner img {
                width: 100%;
                height: 200px;
            }

        .list {
            width: 100%;
            padding: 10px;
            box-sizing: border-box;
        }

            .list .item {
                display: block;
                width: 100%;
                height: 96px;
                margin-bottom: 15px;
                box-shadow: 0 0 5px rgba(0,0,0,0.15);
                overflow: hidden;
            }

                .list .item .img {
                    float: left;
                    width: 60%;
                    height: 96px;
                }

                    .list .item .img img {
                        width: 100%;
                        height: 100%;
                    }

                .list .item .text {
                    display: block;
                    margin-left: 60%;
                    height: 96px;
                    line-height: 96px;
                    font-size: 24px;
                    text-align: center;
                    background: -webkit-gradient(linear,left top,left bottom,from(#fff),to(#cdcdcd));
                }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <div class="swiper-container banner">
                <div class="swiper-wrapper">
                    <div class="swiper-slide">
                        <img class="" src="images/1.jpg" />
                    </div>
                    <div class="swiper-slide">
                        <img class="" src="images/2.jpg" />
                    </div>
                    <div class="swiper-slide">
                        <img class="" src="images/3.jpg" />
                    </div>
                    <div class="swiper-slide">
                        <img class="" src="images/4.jpg" />
                    </div>
                </div>
                <div class="swiper-pagination"></div>
            </div>
            <div class="list">
                <a class="item" href="<%=MyCommFun.getWebSite()%>/travel/order/myorderlist.html">
                    <span class="img">
                        <img src="images/ticket.png" />
                    </span>
                    <span class="text">门票订单</span>
                </a>
                <a class="item">
                    <span class="img">
                        <img src="images/hotel.png" />
                    </span>
                    <span class="text">住宿订单</span>
                </a>
                <a class="item" href="../restaurant/diancai_oder.aspx?openid=<%=openid %>">
                    <span class="img">
                        <img src="images/catering.png" />
                    </span>
                    <span class="text">餐饮订单</span>
                </a>
            </div>
        </div>
        <%--<div>
            <a class="Order" href="../restaurant/diancai_oder.aspx?openid=<%=openid %>">
                <div class="OrderText">门票订单</div>
            </a>
            <a class="Order" href="../restaurant/diancai_oder.aspx?openid=<%=openid %>">
                <div class="OrderText">酒店订单</div>
            </a>
            <a class="Order" href="../restaurant/diancai_oder.aspx?openid=<%=openid %>">
                <div class="OrderText">餐饮订单</div>
            </a>
        </div>--%>
    </form>
    <script src="../../scripts/jquery/zepto.min.js"></script>
    <script src="../../scripts/swiper/swiper.min.js"></script>
    <script>
        $(function () {
            var swiper = new Swiper('.swiper-container', {
                autoplay: 3000,
                loop: true,
                pagination: '.swiper-pagination',
            });
        })
    </script>
</body>
</html>
