<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="KNSHotelMasterPage.Master" CodeBehind="hotel_order.aspx.cs" Inherits="WeiXinPF.Web.weixin.KNSHotel.hotel_order" %>

<asp:Content ID="h" ContentPlaceHolderID="head" runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=UTF-8">
<title>在线预订管理</title>
<meta name="viewport" content="width=device-width,height=device-height,inital-scale=1.0,maximum-scale=1.0,user-scalable=no;">
<meta name="apple-mobile-web-app-capable" content="yes">
<meta name="apple-mobile-web-app-status-bar-style" content="black">
<meta name="format-detection" content="telephone=no">
<link href="css/hotels.css" rel="stylesheet" type="text/css">
  <style>
          .header-img {
            height: 200px;
        }
    </style>
</asp:Content>

<asp:Content ID="c" ContentPlaceHolderID="content" runat="server" class="mode_webapp">
<div class="qiandaobanner"> 
<%--     <a href="hotel_order_onlin.aspx?openid=<%=openid %>" >--%>
          <img  class="header-img"    src="<%=image %>"  />
<%--     </a>--%>

</div>
<div class="cardexplain">

<!--超过预订时间3天后自动删掉预订记录，免得占服务器资源！-->  

    
<%=order %>

    

</div>
<script>
    function dourl(url) {
        location.href = url;
    }
</script>
<script src="js/plugback.js" type="text/javascript" ></script>
<script type="text/javascript">
    document.addEventListener('WeixinJSBridgeReady', function onBridgeReady() {
        WeixinJSBridge.call('hideToolbar');
    });
</script>

</asp:Content>
