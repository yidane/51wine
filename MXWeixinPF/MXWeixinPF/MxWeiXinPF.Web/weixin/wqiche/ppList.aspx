<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ppList.aspx.cs" Inherits="MxWeiXinPF.Web.weixin.wqiche.ppList" %>


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
    <script type="text/javascript" src="js/jQuery.js"></script>
    <script type="text/javascript" src="js/jquery_easing.js"></script>
    <script type="text/javascript" src="js/com_clanmo_gallery_min.js"></script>
    <script type="text/javascript" src="js/swipe.js"></script>
    <link rel="stylesheet" type="text/css" href="css/easy-responsive-tabs.css" media="all">
    <script type="text/javascript" src="js/easyResponsiveTabs.js"></script>

    <title>全部车型－品牌</title>
    <style>
        .main img {
            width: 100%!important;
        }

        .logohead {
            padding: 5px;
            text-align: center;
        }
    </style>
    <meta content="clickberry-extension-here">
</head>
<body onselectstart="return true;" ondragstart="return false;" class="portrait">



    <div class="header">
        <div class="logohead">
            <h1>品牌列表</h1>
        </div>
        <div style="clear: both;"></div>
    </div>
    <span class="gradient_h_wbw"></span>
    <div class="main" style="padding-top: 10px;">
        <div id="horizontalTab">
            <asp:Repeater ID="rptList" runat="server">
                <ItemTemplate>
                    <ul class="resp-tabs-list">
                        <li>
                            <img src="<%#Eval("logo") %>" width="140" height="29" alt="" style="width: auto!important; max-width: 180px;">
                            <%#Eval("name") %> </li>
                    </ul>
                    <div class="resp-tabs-container">
                        <div>
                            <div style="margin: 5px; border: 1px solid #ccc; line-height: 35px; text-indent: 10px; border-radius: 5px; background: -webkit-gradient(linear, 0 0, 0 100%, from(#fff), to(#eee)); -webkit-box-shadow: 0 1px 1px #fff;">
                                <a href="showChexi.aspx?wid=<%=wid %>&pid=<%#Eval("id") %>" style="color: #666; display: block; text-align: center">查看车系</a>
                            </div>
                            <p style="padding: 15px;">
                                <%#Eval("remark") %>                   
                            </p>
                            <br />
                            <div style="margin: 5px; border: 1px solid #ccc; line-height: 35px; text-indent: 10px; border-radius: 5px; background: -webkit-gradient(linear, 0 0, 0 100%, from(#fff), to(#eee)); -webkit-box-shadow: 0 1px 1px #fff;">
                                <a href="<%#Eval("website") %> " style="color: #666; display: block; text-align: center"><%#Eval("name") %>  官网</a>
                            </div>
                        </div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div> 
    </div>

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
    </p> 
<%--    <span class="copyright">版权归我所有        </span>--%>
    <div mark="stat_code" style="width: 0px; height: 0px; display: none;"></div>

</body>
</html>
