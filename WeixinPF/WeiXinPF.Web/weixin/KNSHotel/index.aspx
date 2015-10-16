<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="KNSHotelMasterPage.Master" CodeBehind="index.aspx.cs" Inherits="WeiXinPF.Web.weixin.KNSHotel.index" %>


<asp:Content ID="h" ContentPlaceHolderID="head" runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=UTF-8">
<title>在线预订</title>
<meta name="viewport" content="width=device-width,height=device-height,inital-scale=1.0,maximum-scale=1.0,user-scalable=no;">
<meta name="apple-mobile-web-app-capable" content="yes">
<meta name="apple-mobile-web-app-status-bar-style" content="black">
<meta name="format-detection" content="telephone=no">
<link href="css/hotels.css" rel="stylesheet" type="text/css">
</asp:Content>


<asp:Content ID="c" ContentPlaceHolderID="content" runat="server" class="mode_webapp">
<div class="qiandaobanner">  <a href="hotel_order_onlin.aspx?openid=<%=openid %>" > <img   src="<%=image %>"  /></a> </div>
<div class="cardexplain"> 

  <!--普通用户登录时显示-->
<%=numdingdan %>  

<!--商家房价及类型-->
<ul class="round">
<%=yuding %>       
</ul>

<!--后台可控制是否显示-->
<div class="detailcontent"><h2>订单说明</h2>
  <div class="content">
  <span style="color:#444444;font-family:'Microsoft YaHei', Helvitica, Verdana, Arial, san-serif;font-size:14px;font-weight:bold;line-height:21px;background-color:#FCFCFC;"><%=shuoming %></span></div>
  </div>
   
<!--后台可控制是否显示-->
<ul class="round">
<li class="addr"><a href="http://api.map.baidu.com/marker?location=<%=xplace %>,<%=yplace %>&amp;title=<%=dizhi %>&amp;content=<%=dizhi %>&amp;output=html"><span><%=dizhi %></span></a></li>
<li class="tel"><a href="tel:<%=tel %>"><span><%=tel %> 电话预订</span></a></li>
<li class="detail"><a href="hotel_detail.aspx?openid=<%=openid%>&hotelid=<%=hotelid %>"><span>查看商家详情</span></a></li>
</ul>
</div>
<script src="js/plugback.js" type="text/javascript" ></script>
<script type="text/javascript">
    document.addEventListener('WeixinJSBridgeReady', function onBridgeReady() {
        WeixinJSBridge.call('hideToolbar');
    });
</script>

</asp:Content>

