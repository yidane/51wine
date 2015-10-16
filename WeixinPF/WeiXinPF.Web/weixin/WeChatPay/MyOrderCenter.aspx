<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MyOrderCenter.aspx.cs" Inherits="WeiXinPF.Web.weixin.WeChatPay.MyOrderCenter" %>

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
    <style type="text/css">
        .Order {
            display: block;
            background-color: red;
            height: 100px;
            width: 100%;
            margin-top: 10px;
        }

        .OrderText {
            text-align: center;
            width: 100%;
            height: 100px;
            line-height: 100px;
            font-size: 2em;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <a class="Order" href="../restaurant/diancai_oder.aspx?openid=<%=openid %>">
                <div class="OrderText">餐饮订单</div>
            </a>
            <a class="Order" href="../restaurant/diancai_oder.aspx?openid=<%=openid %>">
                <div class="OrderText">餐饮订单</div>
            </a>
            <a class="Order" href="../restaurant/diancai_oder.aspx?openid=<%=openid %>">
                <div class="OrderText">餐饮订单</div>
            </a>
        </div>
    </form>
</body>
</html>
