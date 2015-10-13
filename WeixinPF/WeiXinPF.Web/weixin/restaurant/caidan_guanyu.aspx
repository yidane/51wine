<%@ Page Language="C#" MasterPageFile="Restaurant.Master" AutoEventWireup="true" CodeBehind="caidan_guanyu.aspx.cs" Inherits="WeiXinPF.Web.weixin.restaurant.caidan_guanyu" %>

<asp:Content ID="h" ContentPlaceHolderID="head" runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
 <meta name="viewport" content="width=device-width,height=device-height,inital-scale=1.0,maximum-scale=1.0,user-scalable=no;">
<meta name="apple-mobile-web-app-capable" content="yes">
<meta name="apple-mobile-web-app-status-bar-style" content="black">
<meta name="format-detection" content="telephone=no">

<title><%=hotelName %></title>
<link href="css/diancai.css" rel="stylesheet" type="text/css" />
<script src="js/iscroll.js" type="text/javascript"></script>
<SCRIPT type=text/javascript>
    var myScroll;

    function loaded() {
        myScroll = new iScroll('wrapper', {
            snap: true,
            momentum: false,
            hScrollbar: false,
            onScrollEnd: function () {
                document.querySelector('#indicator > li.active').className = '';
                document.querySelector('#indicator > li:nth-child(' + (this.currPageX + 1) + ')').className = 'active';
            }
        });
    }
    document.addEventListener('DOMContentLoaded', loaded, false);
</SCRIPT>
<style>
</style>
</asp:Content>
<asp:Content ID="c" ContentPlaceHolderID="content" runat="server" class="mode_webapp">
<div class="cardexplain">
    <div class="detailcontent"><h2>公告</h2>
<div class="content">
    <%=notice %>
</div>
    </div>

<ul class="round">
    <li class="title"><span class="none smallspan">店铺信息</span></li>
        <table width="100%" border="0" cellpadding="0" cellspacing="0" class="cpbiaoge">
    <tr>
    <td>店铺分类：<%=kcType %></td>
    </tr>
    <%=yingye1 %>
    <%=yingye2 %>
    <%=yingye3 %>
           </table>
</ul>
    
    <ul class="round">
        <li class="tel"><a href="tel:<%=tel %>"><span><%=tel %></span></a></li>
<li class="addr"><a href="/weixin/map/map_address.html?type=catering&id=<%=shopid %>"><span><%=address %></span></a></li>
</ul>

<div class="detailcontent"><h2>详情介绍</h2>
        <div class="content">
           <br>
            <%=hotelintroduction %>
        </div>
    </div>
</div>
    <script src="js/shopCart.js" type="text/javascript"></script>
    <script src="js/BuyProducts.js" type="text/javascript"></script>
<script>
    var count = $("#thelist img").size();
    $("#thelist img").css("width", document.body.clientWidth);
    $("#scroller").css("width", document.body.clientWidth * count);
    setInterval(function () {
        myScroll.scrollToPage('next', 0, 400, count);
    }, 3500);
    window.onresize = function () {

        $("#thelist img").css("width", document.body.clientWidth);
        $("#scroller").css("width", document.body.clientWidth * count);
    }

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

