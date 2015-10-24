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
                                    completeUrl = completeUrl + "success";
                                    document.location.href = completeUrl;
                                },
                                fail: function (res) {
                                    completeUrl = completeUrl + "fail";
                                    document.location.href = completeUrl;
                                },
                                cancel: function (res) {
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
                            debug: false,
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

    <style type="text/css">
        .spinner {
            margin: 100px auto 0;
            width: 100%;
            text-align: center;
            position: absolute;
            top: 50%;
            margin-top: -50px;
        }

            .spinner > div {
                width: 20px;
                height: 20px;
                background-color: #67CF22;
                border-radius: 100%;
                display: inline-block;
                -webkit-animation: bouncedelay 1.4s infinite ease-in-out;
                animation: bouncedelay 1.4s infinite ease-in-out;
                /* Prevent first frame from flickering when animation starts */
                -webkit-animation-fill-mode: both;
                animation-fill-mode: both;
            }

            .spinner .bounce1 {
                -webkit-animation-delay: -0.32s;
                animation-delay: -0.32s;
            }

            .spinner .bounce2 {
                -webkit-animation-delay: -0.16s;
                animation-delay: -0.16s;
            }

        @-webkit-keyframes bouncedelay {
            0%, 80%, 100% {
                -webkit-transform: scale(0.0);
            }

            40% {
                -webkit-transform: scale(1.0);
            }
        }

        @keyframes bouncedelay {
            0%, 80%, 100% {
                transform: scale(0.0);
                -webkit-transform: scale(0.0);
            }

            40% {
                transform: scale(1.0);
                -webkit-transform: scale(1.0);
            }
        }
    </style>
</head>
<body>
    <div class="spinner">
        <div class="bounce1"></div>
        <div class="bounce2"></div>
        <div class="bounce3"></div>
    </div>
</body>
</html>
