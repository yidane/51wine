<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="hotel_userOrder.aspx.cs" Inherits="WeiXinPF.Web.weixin.KNSHotel.hotel_userOrder" %>


<html>
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8">
    <title>在线预订管理</title>
    <meta name="viewport" content="width=device-width,height=device-height,inital-scale=1.0,maximum-scale=1.0,user-scalable=no;">
    <meta name="apple-mobile-web-app-capable" content="yes">
    <meta name="apple-mobile-web-app-status-bar-style" content="black">
    <meta name="format-detection" content="telephone=no">
<%--    <link href="css/hotels.css" rel="stylesheet" type="text/css">--%>
     <link href="../restaurant/css/orderlist.css" rel="stylesheet" />
     <style>
         .personal_center #myTab0 li {
             width: 33.333333333%;
         }
     </style>
</head>

<body id="hotelslist" class="mode_webapp">
 <div class="personal_center">
        <ul id="myTab0">
            <%=menuStr %>
        </ul>
        <div class="personal_info">
            <div class="order_list" id="ResultList">
                <%=order %>
            </div>
        </div>
    </div>



<%--    <div class="menu_header">--%>
<%--        <div class="menu_top">--%>
<%--            <%=menuStr %>--%>
<%--               </div>--%>
<%--    </div>--%>
<%--    $1$<div class="qiandaobanner">  <a href="hotel_order_onlin.aspx?openid=<%=openid %>" > <img   src="<%=image %>"  /></a> </div>#1#--%>
<%--    <div class="cardexplain" style="margin-top: 45px">--%>
<%----%>
<%--        <!--超过预订时间3天后自动删掉预订记录，免得占服务器资源！-->--%>
<%----%>
<%----%>
<%--        <%=order %>--%>
<%--    </div>--%>
    <script>
        function dourl(url) {
            location.href = url;
        }
    </script>
    <script src="js/plugback.js" type="text/javascript"></script>
   
    <script type="text/javascript">
        document.addEventListener('WeixinJSBridgeReady', function onBridgeReady() {
            WeixinJSBridge.call('hideToolbar');
        });
    </script>

</body>
</html>
