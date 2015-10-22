<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WeChatPay.aspx.cs" Inherits="WeiXinPF.Web.weixin.WeChatPay.WeChatPay" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>微支付</title>
    <meta http-equiv="Content-type" content="text/html; charset=GBK"/>
    <meta content="application/xhtml+xml;charset=GBK" http-equiv="Content-Type"/>
    <meta content="telephone=no, address=no" name="format-detection"/>
    <meta content="width=device-width, initial-scale=1.0, minimum-scale=1.0, maximum-scale=1.0, user-scalable=no" name="viewport"/>
    <script src="js/zepto.min.js"></script>
    <script src="http://res.wx.qq.com/open/js/jweixin-1.0.0.js"></script>
    <script type="text/javascript">
        var WeChatPayIsReady = true;

        var UnifiedOrderRequest = {
            wid: parseInt(<%=wid%>),
            body: "<%=body%>",
            attach: "<%=attach%>",
            out_trade_no: "<%=out_trade_no%>",
            total_fee: parseInt(<%=total_fee%>),
            openid: "<%=openid%>",
            afterSuccess: null,
            afterFail: null,
            afterCancel: null,
            afterComplete: null
        };
    </script>

    <style type="text/css">
        * {
            margin: 0px;
            padding: 0px;
            -webkit-box-sizing: border-box;
        }

        .body {
            text-align: center;
            width: 100%;
            padding: 60px 20px;
        }

            .body .ordernum {
                font-size: 14px;
                line-height: 30px;
            }

            .body .money {
                font-size: 20px;
                font-weight: bold;
                line-height: 60px;
            }

            .body .time {
                font-size: 16px;
                font-weight: bold;
                line-height: 30px;
            }

            .body .btn {
                display: block;
                background: #25a52e;
                text-decoration: none;
                border-radius: 2px;
                color: #fff;
                height: 44px;
                line-height: 44px;
                font-size: 18px;
                margin-top: 20px;
            }
    </style>
</head>
<body>
    <form runat="server">
        <section class="body">
            <div class="ordernum">订单号：<asp:Literal ID="litout_trade_no" runat="server" EnableViewState="false"></asp:Literal></div>
            <div class="money">共计金额￥<asp:Literal ID="litMoney" runat="server" EnableViewState="false"></asp:Literal></div>
            <div class="time">下单时间：<asp:Literal ID="litDate" runat="server" EnableViewState="false"></asp:Literal></div>
            <div class="paytype" style="display: none;">支付方式：微信支付</div>
            <a href="javascript:void(0);" class="btn" id="getBrandWCPayRequest">确认支付</a>
            <a href="javascript:void(0);" class="btn" id="reload" onclick="javascript:location.href=location.href>'">刷新</a>
        </section>
    </form>
</body>
</html>
