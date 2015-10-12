<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="diancai_refund.aspx.cs" Inherits="WeiXinPF.Web.weixin.restaurant.diancai_refund" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8">
    <meta name="viewport" content="width=device-width,height=device-height,inital-scale=1.0,maximum-scale=1.0,user-scalable=no;">
    <meta name="apple-mobile-web-app-capable" content="yes">
    <meta name="apple-mobile-web-app-status-bar-style" content="black">
    <meta name="format-detection" content="telephone=no">
    <title><%=RestruantName %>餐饮订单</title>
    <link href="css/diancai.css" rel="stylesheet" type="text/css">
    <link href="css/swiper.min.css" rel="stylesheet" />
    <script src="js/jquery.min.js" type="text/javascript"></script>
    <script src="js/alert.js" type="text/javascript"></script>
    <script src="js/swiper.min.js"></script>
    <script src="js/zepto.min.js"></script>
    <style type="text/css">
        .gpd-ticket {
            margin-left: 50%;
            left: -60px;
            border: 0;
            -moz-border-radius: 5px;
            -webkit-border-radius: 5px;
            border-radius: 5px;
            position: fixed;
            bottom: 0px;
            height: 27px;
            width: 120px;
            z-index: 9999;
            background-color: #62b900;
        }
    </style>
</head>
<body class="mode_webapp">
    <form id="form1" runat="server">
        <div class="menu_header">
            <div class="menu_topbar">
                <strong style="float: left; margin-left: 80px">餐饮订单</strong>
                <span class="head_btn_left"><a href="javascript:history.go(-1);"><span>返回</span></a><b></b></span>
                <a class="head_btn_right" href="caidan_shangjia.aspx?shopid=4&openid=1">
                    <span><i class="menu_header_home"></i></span><b></b>
                </a>
            </div>
        </div>

        <div class="contact-info" style="margin-top: 10px;">
            <ul>
                <li class="title">退款申请</li>
                <li>
                    <table style="padding: 0; margin: 0; width: 100%;">
                        <tbody>
                            <tr>
                                <td style="text-align: right; width: 140px">
                                    <label for="wxname" class="ui-input-text">订单号：</label></td>
                                <td>
                                    <%=OrderNumber %>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: right; width: 140px">
                                    <label for="username" class="ui-input-text">类型：</label></td>
                                <td>
                                    <%=CaiPinName %>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: right; width: 140px">
                                    <label for="price" class="ui-input-text">单价：</label></td>
                                <td>
                                    <%=Price %>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: right; width: 140px">
                                    <label for="NoUseCount" class="ui-input-text">未使用总数：</label></td>
                                <td>
                                    <%=NoUseCount %>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: right; width: 140px">
                                    <label for="RefundCount" class="ui-input-text">退票数：</label></td>
                                <td>
                                    <span  id="spanRefundCount" runat="server" enableviewstate="True"><%=NoUseCount %></span>
                                </td>
                            </tr>
                            <tr id="Tr1">
                                <td colspan="3" id="Td1" class="cart-editalertinfo" style="text-align: right">退款总额：<span id="spanRefundAmount" runat="server" enableviewstate="True">123</span>元
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </li>
            </ul>
            <asp:Button runat="server" ID="btnRefund" Text="提交" OnClick="btnRefund_Click" CssClass="gpd-ticket" />
        </div>
    </form>
</body>
</html>
