//微信支付成功
function wechatPayAfterSuccess(param) {

    $("#paycallback").val("支付成功！");

    var dingdanidnum =getQueryString('dingdanidnum') ;
    var openid = getQueryString('openid');
    var hotelid = getQueryString('hotelid');
    var roomid = getQueryString('roomid');

    var hotelUrl = getRootPath() + '/weixin/hotel/';
    var ajaxUrl = hotelUrl + 'hotel_info.ashx';

    window.location.href = hotelUrl + "hotel_order.aspx?openid=" + openid +
                 "&hotelid=" + hotelid + "&roomid=" + roomid;
    
//    var submitData = {
//
//        dingdanidnum: dingdanidnum,
//        myact: "dingdanPayed"
//    };
//    $.post(ajaxUrl, submitData,
//         function (data) {
//             if (data.ret == "ok") {
//                 alert(data.content);
//
//                 window.location.href = hotelUrl + "hotel_order.aspx?openid=" + openid +
//                     "&hotelid=" + hotelid + "&roomid="+roomid;
//
//             } else { alert(data.content); }
//         },
//   "json");
     
}

//微信支付失败
function wechatPayAfterFail(param) {
    $("#paycallback").val("支付失败！");

    var dingdanidnum = getQueryString('dingdanidnum');
    var openid = getQueryString('openid');
    var hotelid = getQueryString('hotelid');
    var roomid = getQueryString('roomid');

    var hotelUrl = getRootPath() + '/weixin/hotel/'; 

    window.location.href = hotelUrl + "hotel_order_xianshi.aspx?dingdanid="+dingdanidnum +"&openid=" + openid +
                 "&hotelid=" + hotelid + "&roomid=" + roomid;
}

//微信支付取消
function wechatPayAfterCancel(param) {
    $("#paycallback").val("支付取消！");

    var dingdanidnum = getQueryString('dingdanidnum');
    var openid = getQueryString('openid');
    var hotelid = getQueryString('hotelid');
    var roomid = getQueryString('roomid');

    var hotelUrl = getRootPath() + '/weixin/hotel/'; 

    window.location.href = hotelUrl + "hotel_order_xianshi.aspx?dingdanid=" + dingdanidnum + "&openid=" + openid +
                 "&hotelid=" + hotelid + "&roomid=" + roomid;
}
 
//微信支付完成
function wechatPayAfterComplete(param) {
    $("#paycallback").val("支付完成！");

    var dingdanidnum = getQueryString('dingdanidnum');
    var openid = getQueryString('openid');
    var hotelid = getQueryString('hotelid');
    var roomid = getQueryString('roomid');

    var hotelUrl = getRootPath() + '/weixin/hotel/';
    var ajaxUrl = hotelUrl + 'hotel_info.ashx';

    window.location.href = hotelUrl + "hotel_order.aspx?openid=" + openid +
                 "&hotelid=" + hotelid + "&roomid=" + roomid;
}


//获取项目根路径
function getRootPath(hasProject){
    //获取当前网址，如： http://localhost:8083/uimcardprj/share/meun.jsp
    var curWwwPath=window.document.location.href;
    //获取主机地址之后的目录，如： uimcardprj/share/meun.jsp
    var pathName=window.document.location.pathname;
    var pos=curWwwPath.indexOf(pathName);
    //获取主机地址，如： http://localhost:8083
    var localhostPaht = curWwwPath.substring(0, pos);
    var result = localhostPaht;
    if (hasProject) {
        //获取带"/"的项目名，如：/uimcardprj
        var projectName = pathName.substring(0, pathName.substr(1).indexOf('/') + 1);
        result += projectName;
    }
    
    return (result);
}

//获取参数值
function getQueryString(name) {
    var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)", "i");
    var r = window.location.search.substr(1).match(reg);
    if (r != null) return unescape(r[2]); return null;
}