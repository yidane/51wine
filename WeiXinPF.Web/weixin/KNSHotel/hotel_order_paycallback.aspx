<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="hotel_order_paycallback.aspx.cs" Inherits="WeiXinPF.Web.weixin.KNSHotel.hotel_order_paycallback" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <meta name="viewport" content="width=device-width,height=device-height,inital-scale=1.0,maximum-scale=1.0,user-scalable=no;">
    <meta name="apple-mobile-web-app-capable" content="yes">
    <meta name="apple-mobile-web-app-status-bar-style" content="black">
    <meta name="format-detection" content="telephone=no">
<%--    <script src="../../scripts/jquery/jquery-2.1.0.min.js"></script>--%>
<%--    <script src="js/paycallback.js"></script>--%>
     setTimeout(function () {
            document.location.href = "<%=newUrl%>";
        }, 500);
</head>
<body>
    <form id="form1" runat="server">
        <div id="paycallback">
            
        </div>

<%--        <script>--%>
<%--            $(document).ready(function () {--%>
<%--                var status = getQueryString("status");--%>
<%--                switch (status) {--%>
<%--                    case 'success':--%>
<%--                        wechatPayAfterSuccess();--%>
<%--                        break;--%>
<%----%>
<%--                    case 'fail':--%>
<%--                        wechatPayAfterFail();--%>
<%--                        break;--%>
<%----%>
<%--                    case 'cancel':--%>
<%--                        wechatPayAfterCancel();--%>
<%--                        break;--%>
<%----%>
<%--                    case 'wechatPayAfterComplete':--%>
<%--                        wechatPayAfterComplete();--%>
<%--                        break;--%>
<%----%>
<%--                    default:--%>
<%--                }--%>
<%--            });--%>
<%--        </script>--%>
    </form>
</body>
</html>
