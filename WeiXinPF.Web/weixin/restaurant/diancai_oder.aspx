<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="diancai_oder.aspx.cs" Inherits="WeiXinPF.Web.weixin.restaurant.diancai_oder" %>

<html>
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8">
    <meta name="viewport" content="width=device-width,height=device-height,inital-scale=1.0,maximum-scale=1.0,user-scalable=no;">
    <meta name="apple-mobile-web-app-capable" content="yes">
    <meta name="apple-mobile-web-app-status-bar-style" content="black">
    <meta name="format-detection" content="telephone=no">
    <link href="css/diancai.css" rel="stylesheet" type="text/css">
    <script src="js/jquery.min.js" type="text/javascript"></script>
    <script src="js/alert.js" type="text/javascript"></script>
    <style>
        .Pay {
            float: left;
            left: 0px;
            width: 50%;
            text-align: center;
            background-color: red;
            border: 0;
            margin: 0;
        }

        .Refund {
            float: right;
            width: 50%;
            text-align: center;
            background-color: white;
            border: 0;
            margin: 0;
        }

        .menu_top {
            color: #fff;
            /*background-color: #c32d32;
            background: -webkit-gradient(linear,left top,left bottom,from(#fe444a),to(#c32d32));
            border-bottom: 1px solid #700d00;*/
            padding: 0 10px 0 10px;
            box-shadow: 0 0 5px #333;
            border-top: 0 none;
            position: fixed;
            top: 0;
            left: 0;
            width: 100%;
            height: 40px;
            line-height: 40px;
            text-align: center;
        }
    </style>
</head>
<body class="mode_webapp">
    <div class="menu_header">
        <div class="menu_top">
            <a class="Pay" href="diancai_oder.aspx?openid=<%=openid %>&type=pay">已付款</a>
            <a class="Refund" href="diancai_oder.aspx?openid=<%=openid %>&type=refund">退单</a>
        </div>
    </div>

    <div class="cardexplain">

        <!--超过预订时间3天后自动删掉预订记录，免得占服务器资源！-->
        <%=str %>
    </div>



    <!--页码-->



    <%--<div class="footermenu">
        <ul>
            <li>
                <a href="caidan_guanyu.aspx?shopid=<%=shopid %>&openid=<%=openid %>">
                    <img src="images/xxyX63YryG.png">
                    <p>关于</p>
                </a>
            </li>
            <li>
                <a href="index.aspx?shopid=<%=shopid %>&openid=<%=openid %>">
                    <img src="images/Lngjm86JQq.png">
                    <p>点单</p>
                </a>
            </li>
            <li>
                <a href="diancai_shoppingCart.aspx?shopid=<%=shopid %>&openid=<%=openid %>">
                    <span class="num" id="cartN2">0</span>
                    <img src="images/2yFKO6TwKI.png">
                    <p>购物车</p>
                </a>
            </li>
            <li>
                <a class="active" href="diancai_oder.aspx?shopid=<%=shopid %>&openid=<%=openid %>">
                    <img src="images/s22KaR0Wtc.png">
                    <p>订单</p>
                </a>
            </li>
            <li>
                <a href="diancai_geren.aspx?shopid=<%=shopid %>&openid=<%=openid %>">
                    <img src="images/J0uZbXQWvJ.png">
                    <p>我的</p>
                </a>
            </li>
        </ul>
    </div>--%>

    <script type="text/javascript">


        document.addEventListener('WeixinJSBridgeReady', function onBridgeReady() {
            WeixinJSBridge.call('hideToolbar');
        });
    </script>

</body>
</html>

