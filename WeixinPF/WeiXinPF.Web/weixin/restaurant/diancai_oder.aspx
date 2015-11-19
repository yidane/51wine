<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="diancai_oder.aspx.cs" Inherits="WeiXinPF.Web.weixin.restaurant.diancai_oder" %>

<html>
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8">
    <meta name="viewport" content="width=device-width,height=device-height,inital-scale=1.0,maximum-scale=1.0,user-scalable=no;">
    <meta name="apple-mobile-web-app-capable" content="yes">
    <meta name="apple-mobile-web-app-status-bar-style" content="black">
    <meta name="format-detection" content="telephone=no">
    <meta content="yes" name="apple-mobile-web-app-capable" />
    <meta content="black" name="apple-mobile-web-app-status-bar-style" />
    <meta content="telephone=no" name="format-detection" />
    <meta content="email=no" name="format-detection" />
    <link href="css/orderlist.css" rel="stylesheet" />
    <script src="js/jquery.min.js" type="text/javascript"></script>
    <script src="js/alert.js" type="text/javascript"></script>
    <style>
        .Pay {
            float: left;
            left: 0px;
            width: 50%;
            text-align: center;
            background-color: white;
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

        .menu-active {
            color: white;
            background: -webkit-gradient(linear,left top,left bottom,from(#fe444a),to(#c32d32));
        }
    </style>
</head>
<body>
    <div class="personal_center">
        <ul id="myTab0">
            <%=menuStr %>
        </ul>
        <div class="personal_info">
            <div class="order_list" id="ResultList">
                <%=str %>
            </div>
        </div>
    </div>
    <script type="text/javascript">
        document.addEventListener('WeixinJSBridgeReady', function onBridgeReady() {
            WeixinJSBridge.call('hideToolbar');
        });
    </script>

</body>
</html>
