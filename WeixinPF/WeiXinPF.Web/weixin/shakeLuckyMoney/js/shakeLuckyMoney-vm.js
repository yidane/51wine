



var LuckyMoneyViewModel = function ($domParam, param) {
    var self = this;
    this.$DomParm = ko.observable($domParam);
    this.param = ko.observable(param);
    this.shakeObj = ko.observable();
    this.wechatDebug = ko.observable(false);
    this.stage = ko.observable("hand");
    this.itemList = ko.observableArray();
    this.info = ko.observable();
    this.coupon = ko.observable();
    this.user = ko.observable();
    this._message = ko.observable('');
    this.message = ko.pureComputed({
        read: function () {

            if (!self._message() && self.info()) {
                self._message(self.info().cfcjhf);
            }

            return self._message();
        },
        write: function (value) {
            
                
            self._message(value);
        },
        owner: this
    });
    this.status = ko.observable("已参与");
    this._couponStatus=ko.observable("notimes");
    this.couponStatus = ko.pureComputed({
        read: function () {
            return self._couponStatus();
        },
        write: function (value) {
            switch (value) {

                case 'nomore':
                    self.status('奖品已领完');
                    break;
                case 'end':
                    self.status('活动已结束');
                    break;
                case 'nostart':
                    self.status('活动未开始');
                    break;
                //case 3:
                //    self.status("立即领取");
                //    break;
                case 'success':
                    self.status("领取成功");
                    break;
                case 'notimes':
                default :
                    self.status("已参与");
            }
            self._couponStatus(value);
        },
        owner: this
    });
    this.expired=ko.observable();

    this.wxConfig = ko.observable();
    this.jsApiList = ko.observableArray(["hideAllNonBaseMenuItem", "showMenuItems", "onMenuShareAppMessage", "addCard"]);


    this.init = function () {
        //self.initWeChat();
        self.initShake();
        self.getCouponBaseInfo();
        //$("#div_content").show();
        //$('#loadingBox').hide();
        //self.param().onAfterLoadData();
    };

    //------------wechat
   
    this.configWeChat = function (wxConfig) {
        wxConfig.debug = self.wechatDebug();
        wxConfig.jsApiList = self.jsApiList();
        self.wxConfig(wxConfig);
        wx.config(wxConfig);
        self.onWeChatReady(wxConfig);
    };

    this.onWeChatReady = function (wxConfig) {
        wx.ready(function () {
            //wx.checkJsApi({
            //    jsApiList: wxConfig.jsApiList,
            //    success: function (res) {
            //        alert(JSON.stringify(res));
            //    }
            //});

            self.hideWeChatBtn();
            

          var  data = {
                "imgUrl": wxConfig.fxImg,
                "timeLineLink": wxConfig.fxUrl + "&is_share=1",
                "sendFriendLink": wxConfig.fxUrl + "&is_share=1",
                "weiboLink": wxConfig.fxUrl + "&is_share=1",
                "tTitle": wxConfig.fxTitle,
                "tContent": "请关注后，再来抽奖。" + wxConfig.fxContent,
                "fTitle":  wxConfig.fxTitle,
                "fContent": "请关注后，再来抽奖。" + wxConfig.fxContent,
                "wTitle": wxConfig.fxTitle,
                "wContent": "请关注后，再来抽奖。" + wxConfig.fxContent
            };

            self.onShare(data);


        });
        wx.error(function (res) {
            console.log(res);
            //alert(res);

        });
    };
    //按钮显影
    this.hideWeChatBtn = function () {
        wx.hideAllNonBaseMenuItem();
        wx.showMenuItems({
            menuList: [
                "menuItem:share:appMessage"
            ] // 要显示的菜单项，所有menu项见附录3
        });



    };
    //分享
    this.onShare = function(data) {
        self.onMenuShareAppMessage(data);
        self.onMenuShareTimeline(data);
        self.onMenuShareWeibo(data);
    };
    // 发送给好友
    this.onMenuShareAppMessage = function (data) {
      
        var shareData = {
            title: data.fTitle,
            desc: data.fContent,
            link: data.sendFriendLink,
            imgUrl: data.imgUrl
        };
        wx.onMenuShareAppMessage(shareData);

    };
    // 分享到朋友圈
    this.onMenuShareTimeline = function (data) {
      
        var shareData = {
            title: data.tTitle,
            desc: data.tContent,
            link: data.timeLineLink,
            imgUrl: data.imgUrl
        };
        wx.onMenuShareTimeline(shareData);
    };
    // 分享到微博
    this.onMenuShareWeibo = function (data) {
       
        var shareData = {
            title: data.wTitle,
            desc: data.wContent,
            link: data.weiboLink,
            imgUrl: data.imgUrl
        };
        wx.onMenuShareWeibo(shareData);
    };

    //------------shake
    this.initShake = function () {
//        var yy = new mobilePhoneShake({
//            speed: 2000,//阀值，值越小，能检测到摇动的手机摆动幅度越小
//            callback: function (x, y, z) {//将设备放置在水平表面，屏幕向上，则其x,y,z信息如下：{x: 0,y: 0,z: 9.81};
//                self.afterShake();
//                self.shakeObj().stop();
//                self.shakeObj(null);
//            },
//            onchange: function (x, y, z) {
//                //document.getElementById("msg").innerHTML="x:"+x+"<br>y:"+y+"<br>z:"+z;
//            }
//        });
//
//        yy.start();
        //        self.shakeObj(yy);


        //create a new instance of shake.js.
        var myShakeEvent = new Shake({
            threshold: 15
        });

        // start listening to device motion
        myShakeEvent.start();

        // register a shake event
        window.addEventListener('shake', function() {
            //shake event callback
            self.afterShake();
        }, false);

        
    };
    this.afterShake = function () {

        self.$DomParm().$musicBox.bind('ended', self.musicEnded);

        self.$DomParm().$musicBox[0].play();

    };
    this.musicEnded = function () {
        //显影
        // self.stage("money");
        // alert(1);
        self.getRandomCoupon();



        ////动画开始时隐藏
        //self.$DomParm().$money.bind('animationstart', function () {
        //    self.$DomParm().$hand.addClass("item-hidden");
        //});


    };

    this.getCouponBaseInfo = function() {

        $.getJSON("../../WebServices/CouponWebService.asmx/GetBaseCouponInfo",
            {
                aid: self.param().aid,
                wid: self.param().wid,
                code: self.param().access_code,
                url: document.location.href
                
            })
            .done(function (json) {
                console.log(json);
                if (json.IsSuccess) {
                    document.title = json.Data.info.actName;
                    self.user(json.Data.user);
                    self.info(json.Data.info);
                    self.itemList(json.Data.itemlist);

                    json.Data.signature.fxImg = getBaseUrl() + 'images/shareLuckyMoney.png';

                    self.configWeChat(json.Data.signature);
                }
                else {
                    if(json.MessageType!='system'){
                        if(json.MessageType=='41008')
                        {
                            window.location.href=json.Message;
                            return;
                        }


                        self.couponStatus(json.MessageType);
                        setTimeout(function () {
                            self.$DomParm().$endMp3[0].play();

                        }, 1000);
                    }

                    console.log(json.Message);
                }
            }).fail(
            function (jqxhr, textStatus, error) {
                var err = textStatus + ", " + error;
                console.log("Request Failed: " + err);
            });
    };

    this.getRandomCoupon = function () {
        $.getJSON("../../WebServices/CouponWebService.asmx/GetRandomCoupon",
            {
                aid: self.param().aid,
                wid: self.param().wid,
                url:document.location.href,
                openid: self.user()?self.user().openid:''
              //  openid:'obzTsw4p1nhpl97G1xJwKicDNsiQ'
            })
            .done(function (json) {
                console.log(json);
                if (json.IsSuccess) {
                    self.couponStatus(json.MessageType);
                    self.coupon(json.Data.coupon);
                    if(json.Data.coupon)
                    {
                        //self.couponStatus(json.Data.coupon.status);
                        if(json.MessageType=='success')
                        {

                           // self.jumpToMyCoupons(3000);

                        }
                    }



                    setTimeout(function () {
                        self.$DomParm().$endMp3[0].play();

                    }, 1000);
                }
                else {
                    if(json.MessageType!='system'){
                     if(json.MessageType=='41008')
                     {
                         window.location.href=json.Message;
                         return;
                     }
                        
                        else if (json.MessageType == 'notimes') {
                            self.message(json.Message);
                     }
                        self.coupon(json.Data.coupon);

                        self.couponStatus(json.MessageType);
                        setTimeout(function () {
                            self.$DomParm().$endMp3[0].play();

                        }, 1000);
                    }
                   

                    console.log(json.Message+','+json.MessageType);
                    // alert(json.Message);
                }
            }).fail(
            function (jqxhr, textStatus, error) {
                var err = textStatus + ", " + error;
                console.log("Request Failed: " + err);
            });


    };

    this.receiveCoupon = function () {
        $.getJSON("../../WebServices/CouponWebService.asmx/ReceiveCoupon", {
            openId: self.user().openid,
            couponId: self.coupon().couponId
        })
            .done(function (json) {
                if (json.IsSuccess) {
                    self.couponStatus(4);
                    //self.$DomParm().$receiveBtn.val("领取成功!");
                    //self.$DomParm().$receiveBtn.attr("disabled", "disabled");
                    self.jumpToMyCoupons(1000);
                }
            }).fail(
            function (jqxhr, textStatus, error) {
                var err = textStatus + ", " + error;
                console.log("Request Failed: " + err);
            });
    };

    this.jumpToMyCoupons=function(time){
        setTimeout(function () {
            var url = 'Coupons/MyCoupons.html';//?code=' + self.param().access_code;
            window.location.href = url;
        }, time);
    };

    //最后执行初始化
    self.init();
};




