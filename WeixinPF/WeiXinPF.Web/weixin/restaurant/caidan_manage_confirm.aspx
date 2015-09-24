<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="caidan_manage_confirm.aspx.cs" Inherits="WeiXinPF.Web.weixin.restaurant.caidan_manage_confirm" %>

<html>
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8">
    <title><%=hotelName %></title>

    <meta name="viewport" content="width=device-width,height=device-height,inital-scale=1.0,maximum-scale=1.0,user-scalable=no;">
    <meta name="apple-mobile-web-app-capable" content="yes">
    <meta name="apple-mobile-web-app-status-bar-style" content="black">
    <meta name="format-detection" content="telephone=no">
    <link href="css/diancai.css" rel="stylesheet" type="text/css">
    <style>
</style>
    <script src="http://res.wx.qq.com/open/js/jweixin-1.0.0.js" type="text/javascript"></script>
    <script type="text/javascript">
        function useQrcode() {
            wx.scanQRCode({
                needResult: 1, // 默认为0，扫描结果由微信处理，1则直接返回扫描结果，
                scanType: ["qrCode", "barCode"], // 可以指定扫二维码还是一维码，默认二者都有
                success: function (res) {
                    var result = res.resultStr; // 当needResult 为 1 时，扫码返回的结果
                }
            });
        }
    </script>
    <style type="text/css">
        .px[type="text"] {
            width: 100%;
            box-sizing: border-box;
            -webkit-box-sizing: border-box;
            -moz-box-sizing: border-box;
            line-height: 40px;
            margin: 2px;
        }

        .qrcode {
            float: right;
            position: relative;
            top: -40px;
        }
    </style>
</head>

<body id="onlinebooking-list">
    <div class="qiandaobanner">
        <img src="images/RTqs2yHIc9.jpg">
    </div>
    <div class="cardexplain">

        <!--超过预订时间3天后自动删掉预订记录，免得占服务器资源！-->
        <div class="round">

            <input name="" type="text" class="px" id="verificationcode" value="" placeholder="请输入验证码">
            <a href="javascript:useQrcode()" class="qrcode">
                <img src="images/manage.png" /></a>
            <a id="showcard" class="submit" href="javascript:void(0)">验 证</a>

        </div>
    </div>
    <!--页码-->

    <!--页码-->
</body>
</html>
