<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="showChexi.aspx.cs" Inherits="MxWeiXinPF.Web.weixin.wqiche.showChexi" %>

<!DOCTYPE html>
<html>
<head>
    <meta charset="utf-8">
    <meta content="no-cache,must-revalidate" http-equiv="Cache-Control">
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8">
    <meta name="viewport" content="width=device-width,height=device-height,inital-scale=1.0,maximum-scale=1.0,user-scalable=no;">
    <meta name="apple-mobile-web-app-capable" content="yes">
    <meta name="apple-mobile-web-app-status-bar-style" content="black">
    <meta name="format-detection" content="telephone=no">
    <link rel="stylesheet" type="text/css" href="css/reset.css" media="all">
    <link rel="stylesheet" type="text/css" href="css/common.css" media="all">
    <script type="text/javascript" src="js/jquery152_min.js"></script>
    <script type="text/javascript" src="js/jquery_easing.js"></script>
    <script type="text/javascript" src="js/com_clanmo_gallery_min.js"></script>
    <script type="text/javascript" src="js/swipe.js"></script> 
    <link rel="stylesheet" type="text/css" href="css/easy-responsive-tabs.css" media="all">
    <script type="text/javascript" src="js/easyResponsiveTabs.js"></script>

    <title><%=pinpai %>-车型－车系</title>
    <style>
        img {
            width: 100%!important;
        }
    </style>
    <meta content="clickberry-extension-here">
</head>
<body onselectstart="return true;" ondragstart="return false;" class="portrait">
    <div class="header">
        <div class="logo">
            <%=pinpai %>
            <img src="<%=ppLogo %>" width="213" height="29" alt="" style="width: auto!important; max-width: 213px;">
        </div>
    </div>
    <span class="gradient_h_wbw"></span>
    <div class="top">
        <div id="roundabout_container" class="relative">
            <div id="roundabout__7f8fc196" class="ofh">
                <ul class="list_ul_top" style="left: 0px;">
                    <asp:Repeater ID="rptList" runat="server">
                        <ItemTemplate>
                            <li>
                                <a href=""><span><strong><%#Eval("name") %></strong></span></a>
                            </li>
                        </ItemTemplate>
                    </asp:Repeater>
                </ul>
            </div>
        </div>
        <span class="gradient_h_wbw"></span>
        <!--可以使用幻灯片-->
        <div class="image m_b_s">
            <img class="zeroborder scale" src="" height="150" alt="" style="width: 100%; max-height: 180px;">
        </div>
        <ul class="car_detail">
            <table>
                <tbody>
                    <tr>
                        <td colspan="2"></td>
                    </tr>
            </table>
        </ul>

    </div>
    <!-- 车系列表 begin -->
    <!--- 车型列表 begin -->
    <div class="main" style="padding-top: 10px;">
        <!--Horizontal Tab-->
        <div id="horizontalTab">
            <ul class="resp-tabs-list">
            </ul>
            <div class="resp-tabs-container">
            </div>
        </div>
    </div>

    <!-- 车型详情 begin -->
    <script type="text/javascript">
        $(document).ready(function () {
            $('#horizontalTab').easyResponsiveTabs({
                type: 'accordion', //Types: default, vertical, accordion           
                width: 'auto', //auto or any width like 600px
                fit: true,   // 100% fit in a container
                closed: 'accordion', // Start closed if in accordion view
                activate: function (event) { // Callback function if tab is switched
                    var $tab = $(this);
                    var $info = $('#tabInfo');
                    var $name = $('span', $info);

                    $name.text($tab.text());

                    $info.show();
                }
            });
        });
    </script>
    <!-- end -->
    <p>
        <br />
        <br />
        <br />
        <br />
    </p>

<%--    <span class="copyright">版权归我所有        </span>--%>
    <div mark="stat_code" style="width: 0px; height: 0px; display: none;">
    </div>

</body>
</html>
