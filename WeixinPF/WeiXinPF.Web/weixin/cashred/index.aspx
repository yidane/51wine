<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="WeiXinPF.Web.weixin.cashred.index" %>

<%@ Import Namespace="WeiXinPF.Common" %>
<!DOCTYPE html>

 <html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8">
    <meta name="viewport" content="width=device-width,height=device-height,inital-scale=1.0,maximum-scale=1.0,user-scalable=no;">
    <meta name="apple-mobile-web-app-capable" content="yes">
    <meta name="apple-mobile-web-app-status-bar-style" content="black">
    <meta name="format-detection" content="telephone=no">
    <meta name="description" content="微信">
    <meta http-equiv="Pragma" content="no-cache">
    <meta http-equiv="Cache-Control" content="no-cache">
    <meta http-equiv="Expires" content="0">
    <title>现金红包</title>
    <script src="http://libs.baidu.com/jquery/2.0.0/jquery.min.js" type="text/javascript"></script>
    <script src="../../scripts/jquery/alert.js" type="text/javascript"></script>
     <script type="text/javascript" src="../../scripts/jquery/jquery.query.js"></script>
     <script src="http://res.wx.qq.com/open/js/jweixin-1.0.0.js"></script>
    <link href="css/activity-style.css" rel="stylesheet" type="text/css">


    <script type="text/javascript">
        //jssdk
        var dataForWeixin = {
            appId: "<%=fxModel.appid%>",
            MsgImg: "<%=fxModel.fxImg%>",
            TLImg: "<%=fxModel.fxImg%>",
            url: "<%=fxModel.thisUrl%>",
            fxUrl: "<%=fxModel.fxUrl%>",
            title: "<%=fxModel.fxTitle%>",
            desc: "<%=fxModel.fxContent%>",
            timestamp: '<%=fxModel.timestamp%>',
            nonceStr: '<%=fxModel.nonce%>',
            signature: '<%=fxModel.signature%>',
            jsApiList: ["checkJsApi",
                    'onMenuShareTimeline',
                    'onMenuShareAppMessage',
                    'onMenuShareQQ',
                    'onMenuShareWeibo',
                    'hideMenuItems',
                    'showMenuItems',
                    'hideAllNonBaseMenuItem',
                    'showAllNonBaseMenuItem',
                    'translateVoice',
                    'startRecord',
                    'stopRecord',
                    'onRecordEnd',
                    'playVoice',
                    'pauseVoice',
                    'stopVoice',
                    'uploadVoice',
                    'downloadVoice',
                    'chooseImage',
                    'previewImage',
                    'uploadImage',
                    'downloadImage',
                    'getNetworkType',
                    'openLocation',
                    'getLocation',
                    'hideOptionMenu',
                    'showOptionMenu',
                    'closeWindow',
                    'scanQRCode',
                    'chooseWXPay',
                    'openProductSpecificView',
                    'addCard',
                    'chooseCard',
                    'openCard'],
            fakeid: "",
            callback: function () { }
        };
        wx.config({
            debug: false, // 开启调试模式,调用的所有api的返回值会在客户端alert出来，若要查看传入的参数，可以在pc端打开，参数信息会通过log打出，仅在pc端时才会打印。
            appId: dataForWeixin.appId, // 必填，公众号的唯一标识
            timestamp: dataForWeixin.timestamp, // 必填，生成签名的时间戳
            nonceStr: dataForWeixin.nonceStr, // 必填，生成签名的随机串
            signature: dataForWeixin.signature,// 必填，签名，见附录1
            jsApiList: dataForWeixin.jsApiList  // 必填，需要使用的JS接口列表，所有JS接口列表见附录2
        });
        wx.ready(function () {
            //在此输入各种API
            wx.showOptionMenu();

            //分享到朋友圈 onMenuShareTimeline
            wx.onMenuShareTimeline({
                title: dataForWeixin.title, // 分享标题
                link: dataForWeixin.fxUrl, // 分享链接
                imgUrl: dataForWeixin.MsgImg, // 分享图标
                success: function () {
                    // 用户确认分享后执行的回调函数
                    //fxJia();//分享+1
                },
                cancel: function () {
                    // 用户取消分享后执行的回调函数
                }
            });
            //分享给朋友onMenuShareAppMessage
            wx.onMenuShareAppMessage({
                title: dataForWeixin.title, // 分享标题
                desc: dataForWeixin.desc, // 分享描述
                link: dataForWeixin.fxUrl, // 分享链接
                imgUrl: dataForWeixin.TLImg, // 分享图标
                type: '', // 分享类型,music、video或link，不填默认为link
                dataUrl: '', // 如果type是music或video，则要提供数据链接，默认为空
                success: function () {
                    // 用户确认分享后执行的回调函数
                    //fxJia();//分享+1
                },
                cancel: function () {
                    // 用户取消分享后执行的回调函数
                }
            });
            //QQ
            wx.onMenuShareQQ({
                title: dataForWeixin.title, // 分享标题
                desc: dataForWeixin.desc, // 分享描述
                link: dataForWeixin.fxUrl, // 分享链接
                imgUrl: dataForWeixin.MsgImg,// 分享图标
                success: function () {
                    // 用户确认分享后执行的回调函数
                },
                cancel: function () {
                    // 用户取消分享后执行的回调函数
                }
            });
            //QQ微博
            wx.onMenuShareWeibo({
                title: dataForWeixin.title, // 分享标题
                desc: dataForWeixin.desc, // 分享描述
                link: dataForWeixin.fxUrl, // 分享链接
                imgUrl: dataForWeixin.TLImg, // 分享图标
                success: function () {
                    // 用户确认分享后执行的回调函数
                },
                cancel: function () {
                    // 用户取消分享后执行的回调函数
                }
            });
            // config信息验证后会执行ready方法，所有接口调用都必须在config接口获得结果之后，config是一个客户端的异步操作，
            //所以如果需要在页面加载时就调用相关接口，则须把相关接口放在ready函数中调用来确保正确执行。对于用户触发时才调用的接口，则可以直接调用，不需要放在ready函数中。
        });
        wx.error(function (res) {
            console.log(res);
          //  alert('验证失败');
            //alert(res);
            // config信息验证失败会执行error函数，如签名过期导致验证失败，具体错误信息可以打开config的debug模式查看，也可以在返回的res参数中查看，对于SPA可以在这里更新签名。
        });



    </script>
</head>
<body class="activity-coupon-winning">
    <form id="form1" runat="server">
        <asp:HiddenField ID="hidStatus" runat="server" EnableViewState="false" />
        <asp:HiddenField ID="hidErrInfo" runat="server" EnableViewState="false" />
        <asp:HiddenField ID="hidAwardId" runat="server" EnableViewState="false" Value="0" />

        <div class="main">
            <div class="banner">
                <a href="javascript:;">
                    <asp:Image ID="zjpic" runat="server" ImageUrl="images/hongbao.jpg" EnableViewState="false" />
                </a>
            </div>
            <div class="content" style="margin-top: -5px">
                <div class="boxcontent boxwhite" id="zjl" style="display: none;">
                    <div class="box">
                        <div class="title-red"><span>中奖了</span></div>
                        <div class="Detail">
                            <p>你抢到：<span class="red"><asp:Literal ID="litJiangPing" runat="server" EnableViewState="false"></asp:Literal></span></p>

                            <p>SN码：<span class="red" id="sncode"><asp:Literal ID="litSn" runat="server" EnableViewState="false"></asp:Literal></span></p>
                            <p class="red">
                                <asp:Literal ID="litsuccTip" runat="server" EnableViewState="false"></asp:Literal>
                            </p>

                            <p>
                                <input name="" class="px" id="tel" value="" type="text" placeholder="用户请输入您的手机号">
                            </p>


                            <p>
                                <input name="" class="px" id="parssword" type="password" value="" placeholder="商家输入兑奖密码">
                            </p>

                            <p>
                                <input class="pxbtn" name="提 交" id="save-btn" type="button" value="用户提交">
                            </p>
                        </div>
                    </div>
                </div>


                 <div class="boxcontent boxwhite" id="result" >
                    <div class="box">
                        <div class="title-red"><span><asp:Literal ID="litTitle" runat="server" EnableViewState="false" Text="谢谢参与"></asp:Literal></span></div>
                        <div class="Detail">
                           <p><span class="red"><asp:Literal ID="litMoney" runat="server" EnableViewState="false" Text="谢谢参与"></asp:Literal></span></p>
                        </div>


                    </div>
                </div>
                <div class="boxcontent boxwhite">
                    <div class="box">
                        <div class="title-orange">活动说明：</div>
                        <div class="Detail">
                            <p>
                                <asp:Literal ID="litActionRemark" runat="server" EnableViewState="false"></asp:Literal>
                            </p>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>


