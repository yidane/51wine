var wxsdk = wxsdk || {};
(function ($, window) {

    var jsApiList = [
        'checkJsApi',
        'chooseImage',
        'previewImage',
        'uploadImage',
        'downloadImage',
        'onMenuShareTimeline',
        'onMenuShareAppMessage',
        'hideAllNonBaseMenuItem',
        'showMenuItems'
    ];

    //注册方法
    wxsdk.initWxConfig = function (debug) {
        $.ajax({
            url: '../../WebServices/WeChatWebService.asmx/WeChatConfigInit',
            data: {
                url: document.location.href,
                wid: wid
            },
            async: false,
            dataType: 'json',
            success: function (res) {
                if (res && res.IsSuccess) {
                    var wxConfig = res.Data;
                    wxConfig.debug = debug;
                    wxConfig.jsApiList = jsApiList;
                    wx.config(wxConfig);
                    wxsdk.checkJsApi();
                } else {
                    alert('注册微信方法失败');
                }
            },
            error: function (xmlhttp, errMsg, status) {
                alert(xmlhttp.responseText);
                $("#error").html(xmlhttp.response);
            }
        });
    }

    wxsdk.checkJsApi = function () {
        wx.ready(function () {
            wx.checkJsApi({
                jsApiList: jsApiList,
                success: function (res) {
                    var strResult = res.errMsg;
                    if (strResult.indexOf(':ok') < 0) {
                        alert('您当前的微信版本不支持，请确保您的微信为最新版本！');
                    }
                }
            });
        });

        wx.error(function (res) {
            alert(JSON.stringify(res));
        });
    }

    //选择图片
    wxsdk.chooseImage = function (options) {
        wx.ready(function () {
            wx.chooseImage({
                count: 1,
                success: options.success
            });
        });
    }

    //上传图片
    wxsdk.uploadImage = function (options) {
        wx.uploadImage({
            localId: options.localId,
            isShowProgressTips: 0,
            success: options.success
        });
    }

    //下载图片
    wxsdk.downloadImage = function (options) {
        wx.downloadImage({
            serverId: options.serverId,
            success: options.success,
            fail: function (res) {
                alert(JSON.stringify(res));
            }
        });
    }

    //分享消息
    wxsdk.shareMessage = function (message) {
        wx.ready(function () {
            //监听“分享给朋友”，按钮点击、自定义分享内容及分享结果接口
            wx.onMenuShareAppMessage({
                title: message.title,
                link: message.link,
                imgUrl: message.imgUrl,
                fail: function () {
                    alert("分享失败！");
                }
            });

            //监听“分享到朋友圈”按钮点击、自定义分享内容及分享结果接口
            wx.onMenuShareTimeline({
                title: message.title,
                link: message.link,
                imgUrl: message.imgUrl,
                fail: function () {
                    alert("分享失败！");
                }
            });
        });
    }

    wxsdk.hideAllNonBaseMenuItem = function () {
        wx.ready(function () {
            wx.hideAllNonBaseMenuItem();
        });
    };

    wxsdk.showShareMenu = function () {
        wx.ready(function () {
            wx.showMenuItems({
                menuList: [
                    'menuItem:share:appMessage',
                    'menuItem:share:timeline'
                ]
            });
        });
    }
})($, window);