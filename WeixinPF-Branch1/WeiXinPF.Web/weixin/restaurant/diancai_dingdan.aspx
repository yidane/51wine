<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="Restaurant.Master" CodeBehind="diancai_dingdan.aspx.cs" Inherits="WeiXinPF.Web.weixin.restaurant.diancai_dingdan" %>

<asp:Content ID="h" ContentPlaceHolderID="head" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8">
    <meta name="viewport" content="width=device-width,height=device-height,inital-scale=1.0,maximum-scale=1.0,user-scalable=no;">
    <meta name="apple-mobile-web-app-capable" content="yes">
    <meta name="apple-mobile-web-app-status-bar-style" content="black">
    <meta name="format-detection" content="telephone=no">
    <title><%=hotelName %></title>
    <link href="css/diancai.css" rel="stylesheet" type="text/css">
    <style>
</style>
</asp:Content>
<asp:Content ID="c" ContentPlaceHolderID="content" runat="server" class="mode_webapp">
    <div class="cardexplain" id="contact_info" runat="server">

        <ul class="round">
            <li class="title"><span class="none smallspan">订单详情</span></li>
            <table width="100%" border="0" cellpadding="0" cellspacing="0" class="cpbiaoge">

                <%=Dingdanlist %>
            </table>

        </ul>

        <ul class="round">
            <li class="title"><span class="none smallspan">订单信息</span></li>

            <table width="100%" border="0" cellpadding="0" cellspacing="0" class="cpbiaoge">
                <%=dingdanren %>
            </table>

        </ul>
        <div class="twobtn">
            <a id="showcard2" class="submit" href="javascript:history.go(-1);" style="display: none;" runat="server">返回</a>
        </div>


        <div class="clr"></div>
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
