<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MyOrderCenter.aspx.cs" Inherits="WeiXinPF.Web.weixin.WeChatPay.MyOrderCenter" %>

<%@ Import Namespace="WeiXinPF.Common" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <meta name="viewport" content="width=device-width,height=device-height,inital-scale=1.0,maximum-scale=1.0,user-scalable=no" />
    <meta name="apple-mobile-web-app-capable" content="yes" />
    <meta name="apple-mobile-web-app-status-bar-style" content="black" />
    <meta name="format-detection" content="telephone=no" />
    <link href="css/animate.css" rel="stylesheet" />
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

        .order {
            position: relative;
            display: block;
            width: 100%;
            height: 33.3333333333%;
            padding: 2px;
            -moz-box-sizing: border-box;
            -webkit-box-sizing: border-box;
            box-sizing: border-box;
            background: rgba(0,0,0,0.15);
            z-index: 3;
        }

            .order:nth-child(2) {
                z-index: 2;
                -webkit-animation-delay: 0.3s;
                animation-delay: 0.3s;
            }

            .order:nth-child(3) {
                z-index: 1;
                -webkit-animation-delay: 0.6s;
                animation-delay: 0.6s;
            }

        .order-cover {
            width: 100%;
            height: 100%;
            border-radius: 5px;
            box-shadow: 0 0 20px rgba(0,0,0,.15);
        }

            .order-cover img {
                width: 100%;
                height: 100%;
                border-radius: 5px;
            }

        .circle-text {
            position: absolute;
            width: 100px;
            height: 100px;
            top: 50%;
            left: 50%;
            margin-top: -50px;
            margin-left: -50px;
            /*-webkit-animation-delay: 1s !important;
            animation-delay: 1s !important;*/
        }

            .circle-text > img {
                width: 100%;
                height: 100%;
            }

        .text {
            position: absolute;
            width: 170px;
            height: 50px;
            bottom: 10px;
            left: 50%;
            -webkit-transform: translate3d(-50%,0,0);
            transform: translate3d(-50%,0,0);
        }
    </style>
</head>
<body>
    <%--<form id="form1" runat="server">--%>
    <div class="container">
        <a class="order animated fadeInDown" href="../travel/order/myorderlist.html">
            <div class="order-cover">
                <img src="images/bg_play.png" />
            </div>
            <div class="circle-text">
                <img src="images/circle_text_play.png" />
            </div>
            <div class="text">
                <img src="images/text_play.png" />
            </div>
        </a>
        <a class="order animated fadeInDown" href="../KNSHotel/hotel_userOrder.aspx?openid=<%=openid %>&type=all&wid=1">
            <div class="order-cover">
                <img src="images/bg_reside.png" />
            </div>
            <div class="circle-text">
                <img src="images/circle_text_reside.png" />
            </div>
            <div class="text">
                <img src="images/text_reside.png" />
            </div>
        </a>
        <a class="order animated fadeInDown" href="../restaurant/diancai_oder.aspx?openid=<%=openid %>&type=pay">
            <div class="order-cover">
                <img src="images/bg_eat.png" />
            </div>
            <div class="circle-text">
                <img src="images/circle_text_eat.png" />
            </div>
            <div class="text">
                <img src="images/text_eat.png" />
            </div>
        </a>
    </div>
</body>
</html>
