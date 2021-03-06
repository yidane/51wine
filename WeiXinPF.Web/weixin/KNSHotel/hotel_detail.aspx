﻿<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="KNSHotelMasterPage.Master" CodeBehind="hotel_detail.aspx.cs" Inherits="WeiXinPF.Web.weixin.KNSHotel.hotel_detail" %>

<asp:Content ID="h" ContentPlaceHolderID="head" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8">
    <title>ddd</title>
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
    <script src="js/iscroll.js" type="text/javascript"></script>
    <script type="text/javascript">
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
    </script>

</asp:Content>

<asp:Content ID="c" ContentPlaceHolderID="content" runat="server" class="mode_webapp">
    <div class="banner">
        <div id="wrapper">
            <div id="scroller">
                <ul id="thelist">
                    <%=tupian %>
                </ul>
            </div>
        </div>
        <div id="nav">
            <div id="prev" onclick="myScroll.scrollToPage('prev', 0,400,3);return false">&larr; prev</div>
            <ul id="indicator">
                <%=tabid %>
            </ul>
            <div id="next" onclick="myScroll.scrollToPage('next', 0);return false">next &rarr;</div>
        </div>
        <div class="clr"></div>
    </div>

    <script>


        var count = document.getElementById("thelist").getElementsByTagName("img").length;


        for (i = 0; i < count; i++) {
            document.getElementById("thelist").getElementsByTagName("img").item(i).style.cssText = " width:" + document.body.clientWidth + "px";

        }

        document.getElementById("scroller").style.cssText = " width:" + document.body.clientWidth * count + "px";


        setInterval(function () {
            myScroll.scrollToPage('next', 0, 400, count);
        }, 3500);

        window.onresize = function () {
            for (i = 0; i < count; i++) {
                document.getElementById("thelist").getElementsByTagName("img").item(i).style.cssText = " width:" + document.body.clientWidth + "px";

            }

            document.getElementById("scroller").style.cssText = " width:" + document.body.clientWidth * count + "px";
        }

    </script>
    <div class="cardexplain">


        <ul class="round">
            <li class="addr"><a href="/weixin/map/map_address.html?type=hotel&id=<%=hotelid %>"><span><%=address %></span></a></li>
            <li class="tel"><a href="tel:<%=tel %>"><span><%=tel %> 电话预订</span></a></li>
        </ul>
        <div class="detailcontent">
            <h2>商家介绍</h2>
            <div class="content">
                <span style="color: #444444; font-family: 'Microsoft YaHei', Helvitica, Verdana, Arial, san-serif; font-size: 14px; font-weight: bold; line-height: 21px; background-color: #FCFCFC;"><%=jieshao %></span>
            </div>
        </div>
    </div>
    <script src="js/plugback.js" type="text/javascript"></script>
    <script type="text/javascript">
        document.addEventListener('WeixinJSBridgeReady', function onBridgeReady() {
            WeixinJSBridge.call('hideToolbar');
        });
    </script>

</asp:Content>
