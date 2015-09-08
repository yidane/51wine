<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="syGongju.aspx.cs" Inherits="WeiXinPF.Web.weixin.wqiche.syGongju" %>


<!DOCTYPE html>
<html>
<head>
    <meta charset="utf-8" />
    <meta content="no-cache,must-revalidate" http-equiv="Cache-Control">
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8">
    <meta name="viewport" content="width=device-width,height=device-height,inital-scale=1.0,maximum-scale=1.0,user-scalable=no;">
    <meta name="apple-mobile-web-app-capable" content="yes">
    <meta name="apple-mobile-web-app-status-bar-style" content="black">
    <meta name="format-detection" content="telephone=no">
    <link rel="stylesheet" type="text/css" href="css/reset.css" media="all" />
    <link rel="stylesheet" type="text/css" href="css/common.css" media="all" />
    <title>实用工具</title>
    <style>
        img {
            width: 100%!important;
        }
    </style>
</head>
<body onselectstart="return true;" ondragstart="return false;">
    <body class="portrait">
        <div class="header">
            <div class="logo">
                <img src="img/tool-box-preferences.png" width="120" height="100" alt="" style="width: auto!important; max-width: 250px;" />
            </div>
            <div style="clear: both;"></div>
        </div>
        <span class="gradient_h_wbw"></span>
        <div class="top">
            <div id="roundabout_container" class="relative">
            </div>
            <span class="gradient_h_wbw"></span>
        </div>
        <div class="main" style="padding-top: 10px;">
            <ul class="list_ul_common">
                <asp:Repeater ID="rptList" runat="server">
                    <ItemTemplate>
                        <li>
                            <a href="<%#Eval("url") %>">
                                <div>
                                    <p><%#Eval("Name") %></p>
                                </div>
                            </a>
                        </li>
                    </ItemTemplate>
                </asp:Repeater>
            </ul>
        </div>
<%--        <span class="copyright">版权归我所有        </span>--%>
    </body>
</html>
