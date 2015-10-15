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
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <a href="../restaurant/diancai_oder.aspx?openid=<%=openid %>">
                <div style="width: 100%; padding: 5px;"></div>
            </a>
        </div>
    </form>
</body>
</html>
