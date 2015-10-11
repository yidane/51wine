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
<%=status %>
    <tr>
    <td>店铺分类：<%=kcType %></td>
    </tr>
    <%=yingye1 %>
    <%=yingye2 %>
    <%=yingye3 %>
     <tr>
    <td>起送价格：<%=sendPrice %>元</td>
  </tr>
   <tr>
    <td>服务半径：<%=radius %>千米</td>
  </tr>
         <tr>
    <td valign="top">配送区域：<%=sendArea %></td>
  </tr>
           </table>
</ul>
    
    <ul class="round">
        <li class="tel"><a href="tel:<%=tel %>"><span><%=tel %></span></a></li>
<li class="addr"><a href="http://api.map.baidu.com/marker?location=<%=xplace %>,<%=yplace %>&amp;title=<%=hotelName %>&amp;content=<%=address %>&amp;output=html"><span><%=address %></span></a></li>
        <li class="manage"><a href="diancai_Login.aspx?shopid=<%=shopid %>"><span>订单管理</span></a></li>
</ul>

<div class="detailcontent"><h2>详情介绍</h2>
        <div class="content">
           <br>
            <%=hotelintroduction %>
        </div>
    </div>
</div>
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

</script>
<script type="text/javascript">
    document.addEventListener('WeixinJSBridgeReady', function onBridgeReady() {
        WeixinJSBridge.call('hideToolbar');
    });
</script>
</asp:Content>

