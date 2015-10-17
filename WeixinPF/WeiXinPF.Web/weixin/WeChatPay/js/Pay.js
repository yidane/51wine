/// <reference path="jquery-2.1.0.min.js" />

function Pay(url, wid, body, attach, out_trade_no, total_fee, openid, afterSuccess, afterFail, afterCancel, afterComplete) {
    var select = $("<div/>").appendTo($("body"));
    var unifiedOrderRequest =
    {
        wid: wid,
        body: body,
        attach: attach,
        out_trade_no: out_trade_no,
        total_fee: total_fee,
        openid: openid,
        afterSuccess: afterSuccess,
        afterFail: afterFail,
        afterCancel: afterCancel,
        afterComplete: afterComplete
    };

    //$(select).attr("src", url + "?payData=" + JSON.stringify(unifiedOrderRequest));
    $(select).load(url, { "payData": JSON.stringify(unifiedOrderRequest) }, function () {
        //加载完毕之后干啥。。。
    });
}