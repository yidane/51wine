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
    <script src="js/zepto.min.js"></script>
    <script src="js/swiper.min.js"></script>
    <script src="js/alert.js" type="text/javascript"></script>
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

        .Orderchange {
            right: 0;
            top: 50%;
            margin-top: -16px;
            line-height: 30px;
        }

            .Orderchange .count {
                display: inline-block;
                width: 28px;
                height: 28px;
                border-radius: 4px;
                background: #697077;
                line-height: 28px;
                text-align: center;
                color: #FFFFFF;
                text-shadow: 0 1px 2px rgba(0,0,0,0.2);
                box-shadow: 0 1px 2px rgba(0,0,0,0.3) inset;
                font-size: 18px;
            }

            .Orderchange .increase, .Orderchange .reduce {
                display: inline-block;
                width: 35px;
                height: 38px;
                vertical-align: -2px;
            }

            .Orderchange .ico_increase, .Orderchange .ico_reduce {
                position: relative;
                display: block;
                width: 25px;
                height: 25px;
                margin: 6px 0 0 5px;
                background: #D00A0A;
                border-radius: 50%;
                text-indent: -9999px;
            }

            .Orderchange .ico_reduce {
                background: #D00A0A;
            }

                .Orderchange .ico_increase:after, .Orderchange .ico_reduce:after {
                    position: absolute;
                    top: 11px;
                    left: 5px;
                    content: "";
                    display: block;
                    width: 15px;
                    height: 3px;
                    background: #FFFFFF;
                }

            .Orderchange .ico_increase:before {
                position: absolute;
                top: 5px;
                left: 11px;
                content: "";
                display: block;
                width: 3px;
                height: 15px;
                background: #FFFFFF;
            }

        .Amount {
            width: 70px;
            background-color: white;
            border: 0px;
            color: red;
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
                                    <%=Price %>元
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
                                <td class="Orderchange">
                                    <a href="javascript:addProduct()" class="increase"><b class="ico_increase">加一份</b></a>
                                    <asp:TextBox runat="server" ID="txtRefundCount" Text="1" CssClass="count" BorderWidth="0"></asp:TextBox>
                                    <a href="javascript:reduceProduct()" class="reduce"><b class="ico_reduce">减一份</b></a>
                                </td>
                            </tr>
                            <tr id="Tr1">
                                <td colspan="3" id="Td1" class="cart-editalertinfo" style="text-align: right">退款总额：<asp:TextBox runat="server" ID="txtRefundAmount" Enabled="False" CssClass="Amount" Text="0元"></asp:TextBox>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </li>
            </ul>
            <asp:Button runat="server" ID="btnRefund" Text="提交" OnClick="btnRefund_Click" CssClass="gpd-ticket" />
            <asp:HiddenField runat="server" ID="caiidList" />
        </div>

        <script type="text/javascript">
            $(document).ready(function () {
                // 仅能输入数字
                function isNumber(keyCode) {
                    // 数字
                    if (keyCode >= 48 && keyCode <= 57) return true;
                    // 小数字键盘
                    if (keyCode >= 96 && keyCode <= 105) return true;
                    // Backspace键
                    if (keyCode == 8) return true;
                    return false;
                }

                $("#txtRefundCount").on("keydown", function (e) {
                    var keyCode = e.keyCode;
                    return isNumber(keyCode);
                });

                $("#txtRefundCount").change(function () {
                    if ($(this).val() > parseInt(<%=NoUseCount%>)) {
                        $(this).val(parseInt(<%=NoUseCount%>));
                    }

                    if ($(this).val() < 0) {
                        $(this).val(0);
                    }
                });
            });

            var noUseCount = parseInt(<%=NoUseCount %>);
            var price = parseFloat(<%=Price%>);

            function addProduct() {
                var currentCount = parseInt($("#txtRefundCount").val());
                if (currentCount < noUseCount) {
                    currentCount = currentCount + 1;
                    $("#txtRefundCount").val(currentCount);
                    $("#txtRefundAmount").val(accMul(currentCount, price) + "元");
                }
            }

            function reduceProduct() {
                var currentCount = parseInt($("#txtRefundCount").val());
                if (currentCount > 1) {
                    currentCount = currentCount - 1;
                    $("#txtRefundCount").val(currentCount);
                    $("#txtRefundAmount").val(accMul(currentCount, price) + "元");
                }
            }

            function accMul(arg1, arg2) {
                var m = 0, s1 = arg1.toString(), s2 = arg2.toString();
                try {
                    m += s1.split('.')[1].length;
                } catch (e) { }
                try {
                    m += s2.split('.')[1].length;
                } catch (e) { }
                return Number(s1.replace('.', '')) * Number(s2.replace('.', '')) / Math.pow(10, m);
            }
        </script>
    </form>
</body>
</html>
