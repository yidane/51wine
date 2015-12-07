<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="Restaurant.Master" CodeBehind="diancai_OrderList.aspx.cs" Inherits="WeiXinPF.Web.weixin.restaurant.diancai_OrderList" %>

<asp:Content ID="h" ContentPlaceHolderID="head" runat="server">
   <meta http-equiv="Content-Type" content="text/html; charset=UTF-8">
    <meta name="viewport" content="width=device-width,height=device-height,inital-scale=1.0,maximum-scale=1.0,user-scalable=no;">
    <meta name="apple-mobile-web-app-capable" content="yes">
    <meta name="apple-mobile-web-app-status-bar-style" content="black">
    <meta name="format-detection" content="telephone=no">
<title><%=hotelName %></title>
<link href="css/diancai.css" rel="stylesheet" type="text/css">
<script src="js/alert.js" type="text/javascript"></script>
<style>
</style>
</asp:Content>
<asp:Content ID="c" ContentPlaceHolderID="content" runat="server" class="mode_webapp">

<div class="cardexplain"> 

<!--超过预订时间3天后自动删掉预订记录，免得占服务器资源！-->
<%=str %>
</div>  
      
    <script src="js/shopCart.js" type="text/javascript"></script>
    <script src="js/BuyProducts.js" type="text/javascript"></script>
<script>
    window.onload = function () {
        cart.getFromCache();
        showProductsInShopCart();
    }

    var cart = new OAK.Shop.Cart(<%=shopid %>);
    function showProductsInShopCart() {
        showShopCartProductsNumber();
    }

</script>
<script type="text/javascript">


    document.addEventListener('WeixinJSBridgeReady', function onBridgeReady() {
        WeixinJSBridge.call('hideToolbar');
    });
</script>

</asp:Content>

