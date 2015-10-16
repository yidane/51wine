<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WeChatPay.aspx.cs" Inherits="WeiXinPF.Web.weixin.WeChatPay.WeChatPay" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <script src="js/jquery-2.1.0.min.js"></script>
    <script src="http://res.wx.qq.com/open/js/jweixin-1.0.0.js"></script>
    <script type="text/javascript">
        var WeChatPayIsReady = true;
        //var wid = GetQueryString("wid");
        //if (wid == "" || wid == undefined) {
        //    alert("不存在参数wid");
        //}

        var PayManager =
                {
                    Pay: function () {
                        WeChatPayIsReady = true;
                        alert(WeChatPayIsReady);
                        if (WeChatPayIsReady) {
                            alert("pay");
                            var payResult = false;
                            $.ajax({
                                url: "../WeChatPay/WeChatService.asmx/UnifiedOrder",
                                type: "post",
                                dataType: "json",
                                async: false,
                                data: { request: JSON.stringify(UnifiedOrderRequest) },
                                success: function (result) {
                                    if (result != null && result.IsSuccess) {
                                        alert("prepay");
                                        wx.chooseWXPay({
                                            timestamp: result.Data.timeStamp,
                                            nonceStr: result.Data.nonceStr,
                                            package: result.Data.package,
                                            signType: 'MD5',
                                            paySign: result.Data.paySign,
                                            success: function (res) {
                                                payResult = true;
                                                if (typeof UnifiedOrderRequest.afterSuccess === "function") {
                                                    UnifiedOrderRequest.afterSuccess(result.Data.prepayid);
                                                }
                                            },
                                            fail: function (res) {
                                                if (typeof UnifiedOrderRequest.afterFail === "function") {
                                                    UnifiedOrderRequest.afterFail(result.Data.prepayid);
                                                }
                                            },
                                            cancel: function (res) {
                                                if (typeof UnifiedOrderRequest.afterCancel === "function") {
                                                    UnifiedOrderRequest.afterCancel(result.Data.prepayid);
                                                }
                                            },
                                            complete: function (res) {
                                                if (typeof UnifiedOrderRequest.afterComplete === "function") {
                                                    UnifiedOrderRequest.afterComplete(result.Data.prepayid);
                                                }
                                            }
                                        });
                                    }
                                },
                                error: function (error) {
                                    alert(error);
                                }
                            });

                            return payResult;
                        } else {
                            alert("支付状态正在初始化，请稍后再试");
                        }
                    }
                };

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

        function GetQueryString(name) {
            var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)", "i");
            var r = window.location.search.substr(1).match(reg);
            if (r != null) return r[2]; return null;
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
                            PayManager.Pay();
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
</html>
