<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WeChatPay.aspx.cs" Inherits="WeiXinPF.Web.weixin.WeChatPay.WeChatPay" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>微支付</title>
    <meta http-equiv="Content-type" content="text/html; charset=GBK" />
    <meta content="application/xhtml+xml;charset=GBK" http-equiv="Content-Type" />
    <meta content="telephone=no, address=no" name="format-detection" />
    <meta content="width=device-width, initial-scale=1.0, minimum-scale=1.0, maximum-scale=1.0, user-scalable=no" name="viewport" />
    <script src="js/zepto.min.js"></script>
    <script src="http://res.wx.qq.com/open/js/jweixin-1.0.0.js"></script>
    <script type="text/javascript">
        var WeChatPayIsReady = true;
        var completeUrl = "<%=PayComplete%>";

        function Pay() {
            var unifiedOrderRequest = {
                orderId: "<%=OrderID%>",
                wid: parseInt(<%=wid%>),
                body: "<%=body%>",
                payModuleID: "<%=payModuleID%>",
                out_trade_no: "<%=out_trade_no%>",
                total_fee: parseInt(<%=total_fee%>),
                openid: "<%=openid%>"
            };

            function WeChatPay(UnifiedOrderRequest, beforePay, paySuccess, payFail, payCancel, payComplete) {
                alert("WeChatPay");
                $.ajax({
                    url: "../WeChatPay/WeChatService.asmx/UnifiedOrder",
                    type: "post",
                    dataType: "json",
                    async: false,
                    data: { request: JSON.stringify(UnifiedOrderRequest) },
                    success: function (result) {
                        if (result != null && result.IsSuccess) {
                            wx.chooseWXPay({
                                timestamp: result.Data.timeStamp,
                                nonceStr: result.Data.nonceStr,
                                package: result.Data.package,
                                signType: 'MD5',
                                paySign: result.Data.paySign,
                                success: function (res) {
                                    //document.location.href = paySuccess;
                                    completeUrl = completeUrl + "success";
                                    document.location.href = completeUrl;
                                },
                                fail: function (res) {
                                    //document.location.href = payFail;
                                    completeUrl = completeUrl + "fail";
                                    document.location.href = completeUrl;
                                },
                                cancel: function (res) {
                                    //document.location.href = payCancel;
                                    completeUrl = completeUrl + "cancel";
                                    document.location.href = completeUrl;
                                },
                                complete: function (res) {
                                    //不做处理
                                }
                            });
                        }
                    },
                    error: function (error) {
                    }
                });
            }

            WeChatPay(unifiedOrderRequest, '<%=BeforePay%>', '<%=PaySuccess%>', '<%=PayFail%>', '<%=PayCancel%>', '<%=PayComplete%>');
        }

        $(document).ready(function () {
            $.ajax({
                url: "../WeChatPay/WeChatService.asmx/WeChatConfigInit",
                type: "post",
                dataType: "json",
                data: { "wid": parseInt(<%=wid%>), "url": document.location.href },
                success: function (result) {
                    if (result.IsSuccess) {
                        wx.config({
                            debug: true,
                            appId: result.Data.appId,
                            timestamp: result.Data.timestamp,
                            nonceStr: result.Data.nonceStr,
                            signature: result.Data.signature,
                            jsApiList: [
                                'chooseWXPay',
                                'getNetworkType',
                                'onMenuShareAppMessage',
                                'onMenuShareTimeline',
                                'onMenuShareQQ',
                                'hideOptionMenu'
                            ]
                        });
                        wx.ready(function () {

                            WeChatPayIsReady = true;
                            Pay();
                            //wx.hideOptionMenu();

                            //wx.onMenuShareAppMessage({
                            //    title: window.share.title,
                            //    desc: window.share.desc,
                            //    link: window.share.link,
                            //    imgUrl: window.share.imgUrl,
                            //    success: function () {
                            //        //成功回调
                            //        window.share.appcallback();

                            //    }
                            //});

                            //wx.onMenuShareTimeline({
                            //    title: window.share.title,
                            //    link: window.share.link,
                            //    imgUrl: window.share.imgUrl
                            //});

                            //wx.onMenuShareQQ({
                            //    title: window.share.title,
                            //    desc: window.share.desc,
                            //    link: window.share.link,
                            //    imgUrl: window.share.imgUrl
                            //});

                            //wx.error(function (res) {
                            //    alert(res.err_code + "______" + res.err_desc + "______" + res.err_msg);
                            //});
                        });
                    }
                },
                error: function () {

                }
            });
        });
    </script>
</head>
<body>
</body>
</html>
