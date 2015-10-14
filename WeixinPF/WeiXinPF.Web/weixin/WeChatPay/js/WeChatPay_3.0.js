var WeChatPayIsReady = true;
var wid = GetQueryString("wid");
if (wid == "" || wid == undefined) {
    alert("不存在参数wid");
}

var UnifiedOrderRequest = {
    wid: 0,
    body: "",
    attach: "",
    out_trade_no: "",
    total_fee: 0,
    openid: ""
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
        data: { wid: wid, url: document.location.href },
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

function Pay(body, attach, out_trade_no, total_fee, openid) {
    if (WeChatPayIsReady) {
        UnifiedOrderRequest.wid = wid;
        UnifiedOrderRequest.body = body;
        UnifiedOrderRequest.attach = attach;
        UnifiedOrderRequest.out_trade_no = out_trade_no;
        UnifiedOrderRequest.total_fee = total_fee;
        UnifiedOrderRequest.openid = openid;

        var payResult = false;
        $.ajax({
            url: "../WeChatPay/WeChatService.asmx/UnifiedOrder",
            type: "post",
            dataType: "json",
            async: false,
            contentType: "application/json; charset=utf-8",
            data: "{ request:" + JSON.stringify(UnifiedOrderRequest) + "}",
            success: function (result) {
                if (result.IsSuccess) {
                    alert(result.IsSuccess);
                    wx.chooseWXPay({
                        timestamp: result.Data.timeStamp, // 支付签名时间戳，注意微信jssdk中的所有使用timestamp字段均为小写。但最新版的支付后台生成签名使用的timeStamp字段名需大写其中的S字符
                        nonceStr: result.Data.nonceStr, // 支付签名随机串，不长于 32 位
                        package: result.Data.package, // 统一支付接口返回的prepay_id参数值，提交格式如：prepay_id=***）
                        signType: 'MD5', // 签名方式，默认为'SHA1'，使用新版支付需传入'MD5'
                        paySign: result.Data.paySign, // 支付签名
                        success: function (res) {

                            payResult = true;
                        },
                        fail: function (res) {
                            //OrderRelease(result.Data.orderId);
                        },
                        cancel: function (res) {
                            //OrderRelease(result.Data.orderId);
                        },
                        complete: function (res) {

                        }
                    });
                }
            },
            error: function (error) {
            }
        });

        return payResult;
    } else {
        alert("支付状态正在初始化，请稍后再试");
    }
}