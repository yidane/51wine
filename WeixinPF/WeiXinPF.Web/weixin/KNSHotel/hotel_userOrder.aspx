<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="hotel_userOrder.aspx.cs" Inherits="WeiXinPF.Web.weixin.KNSHotel.hotel_userOrder" %>


<html>
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8">
    <title>在线预订管理</title>
    <meta name="viewport" content="width=device-width,height=device-height,inital-scale=1.0,maximum-scale=1.0,user-scalable=no;">
    <meta name="apple-mobile-web-app-capable" content="yes">
    <meta name="apple-mobile-web-app-status-bar-style" content="black">
    <meta name="format-detection" content="telephone=no">
    <link href="css/hotels.css" rel="stylesheet" type="text/css">
</head>

<body id="hotelslist" class="mode_webapp">
    <div class="menu_header">
        <div class="menu_top">
            <%=menuStr %>
            <%--             <a class="Pay menu-active" href="hotel_order.aspx?openid=<%=openid %>&hotelid=<%=hotelid%>&type=all">全部</a>--%>
            <%--            <a class="Pay" href="hotel_order.aspx?openid=<%=openid %>&hotelid=<%=hotelid%>&type=pay">已付款</a>--%>
            <%--            <a class="Refund" href="hotel_order.aspx?openid=<%=openid %>&hotelid=<%=hotelid%>&type=refund">退单</a>--%>
        </div>
    </div>
    <%--<div class="qiandaobanner">  <a href="hotel_order_onlin.aspx?openid=<%=openid %>" > <img   src="<%=image %>"  /></a> </div>--%>
    <div class="cardexplain" style="margin-top: 45px">

        <!--超过预订时间3天后自动删掉预订记录，免得占服务器资源！-->


        <%=order %>
    </div>
    <script>
        function dourl(url) {
            location.href = url;
        }
    </script>
    <script src="js/plugback.js" type="text/javascript"></script>
    <%----%>
    <%--<div class="footermenu">--%>
    <%--    <ul>--%>
    <%--    <li>--%>
    <%--            <a href="javascript:history.go(-1);">--%>
    <%--            <img src="images/9uKCykhUSh.png">--%>
    <%--            <p>返回</p>--%>
    <%--            </a>--%>
    <%--        </li>--%>
    <%--    <li>--%>
    <%--            <a href="index.aspx?openid=<%=openid %>&hotelid=<%=hotelid%>">--%>
    <%--            <img src="images/3YQLfzfunJ.png">--%>
    <%--            <p>首页</p>--%>
    <%--            </a>--%>
    <%--        </li>--%>
    <%--        <li>--%>
    <%--            <a  href="hotel_detail.aspx?openid=<%=openid %>&hotelid=<%=hotelid%>">--%>
    <%--            <img src="images/xxyX63YryG.png">--%>
    <%--            <p>关于</p>--%>
    <%--            </a>--%>
    <%--        </li>--%>
    <%--        <li>--%>
    <%--            <a class="active" href="hotel_order.aspx?openid=<%=openid %>&hotelid=<%=hotelid%>">--%>
    <%--            <span class="num" ><%=numdingdan %></span>  <img src="images/s22KaR0Wtc.png">--%>
    <%--            <p>订单</p>--%>
    <%--            </a>--%>
    <%--        </li>--%>
    <%--    </ul>--%>
    <%--</div>--%>
    <script type="text/javascript">
        document.addEventListener('WeixinJSBridgeReady', function onBridgeReady() {
            WeixinJSBridge.call('hideToolbar');
        });
    </script>

</body>
</html>
