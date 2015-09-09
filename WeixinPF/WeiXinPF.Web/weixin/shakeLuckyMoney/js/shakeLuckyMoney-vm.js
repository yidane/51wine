



var LuckyMoneyViewModel = function ($domParam, param) {
    var self = this;
    this.$DomParm = ko.observable($domParam);
    this.param = ko.observable(param);
    this.shakeObj = ko.observable();
    this.stage = ko.observable("hand");
    this.info = ko.observable();
    this.coupon = ko.observable();
    this.user = ko.observable();
    this.status = ko.observable("立即领取");
    this.couponStatus = ko.pureComputed({
        read: function () {
            return self.status();
        },
        write: function (value) {
            switch (value) {
                case 2:
                    self.status("已参与");
                    break;
                case 3:
                    self.status("立即领取");
                    break;
                case 4:
                    self.status("领取成功");
                    break;
            }
        },
        owner: this
    });
    this.expired=ko.observable();

    this.wxConfig = ko.observable();
    this.jsApiList = ko.observableArray(["hideAllNonBaseMenuItem", "showMenuItems", "onMenuShareAppMessage", "addCard"]);


    this.init = function () {
        self.initWeChat(false);
        self.initShake();
        //$("#div_content").show();
        //$('#loadingBox').hide();
        //self.param().onAfterLoadData();
    };

    //------------wechat
    this.initWeChat = function (debug) {
        $.getJSON('WebService/WeChatWebService.asmx/WeChatConfigInit', { url: document.location.href })
          .done(function (json) {
              if (json.IsSuccess) {
                  var wxConfig = json.Data;
                  wxConfig.debug = debug;
                  wxConfig.jsApiList = self.jsApiList();
                  self.wxConfig(wxConfig);
                  wx.config(wxConfig);
                  self.onWeChatReady();
              }
          }).fail(
          function (jqxhr, textStatus, error) {
              var err = textStatus + ", " + error;
              console.log("Request Failed: " + err);
          });





    };
    this.onWeChatReady = function () {
        wx.ready(function () {
            //wx.checkJsApi({
            //    jsApiList: [
            //      'getNetworkType',
            //      'previewImage'
            //    ],
            //    success: function (res) {
            //        alert(JSON.stringify(res));
            //    }
            //});
            self.hideWeChatBtn();
            self.onMenuShareAppMessage();


        });
        wx.error(function (res) {
            console.log(res);
            //alert(res);

        });
    };
    this.hideWeChatBtn = function () {
        wx.hideAllNonBaseMenuItem();
        wx.showMenuItems({
            menuList: [
            "menuItem:share:appMessage"
            ] // 要显示的菜单项，所有menu项见附录3
        });

    };
    this.onMenuShareAppMessage = function () {
        var shareData = {
            title: '摇一摇得红包',
            desc: '一起来摇红包吧！',
            link: document.location.href,
            imgUrl: 'http://mmbiz.qpic.cn/mmbiz/icTdbqWNOwNRt8Qia4lv7k3M9J1SKqKCImxJCt7j9rHYicKDI45jRPBxdzdyREWnk0ia0N5TMnMfth7SdxtzMvVgXg/0'
        };
        wx.onMenuShareAppMessage(shareData);

    };

    //------------shake
    this.initShake = function () {
        var yy = new mobilePhoneShake({
            speed: 1000,//阀值，值越小，能检测到摇动的手机摆动幅度越小
            callback: function (x, y, z) {//将设备放置在水平表面，屏幕向上，则其x,y,z信息如下：{x: 0,y: 0,z: 9.81};
                self.afterShake();
                self.shakeObj().stop();
                self.shakeObj(null);
            },
            onchange: function (x, y, z) {
                //document.getElementById("msg").innerHTML="x:"+x+"<br>y:"+y+"<br>z:"+z;
            }
        });

        yy.start();
        self.shakeObj(yy);
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
        
        $.getJSON("WebService/CouponWebService.asmx/GetCouponBaseInfo", { aid: self.param().aid })
           .done(function (json) {
               if (json.IsSuccess) {
                   self.info(json.Data);
               }
               else {
                   console.log(json.Message);
               }
           }).fail(
           function (jqxhr, textStatus, error) {
               var err = textStatus + ", " + error;
               console.log("Request Failed: " + err);
           });
    };

    this.getRandomCoupon = function () {
        $.getJSON("WebService/CouponWebService.asmx/GetRandomCoupon", { code: self.param().access_code })
           .done(function (json) {
               if (json.IsSuccess) {
                   self.coupon(json.Data.coupon);
                   if(json.Data.coupon)
                   {
                       self.couponStatus(json.Data.coupon.status);
                       if(json.Data.coupon.status!=3)
                       {

                               self.jumpToMyCoupons(3000);

                       }
                   }
                   else
                   {
                       self.expired(1);
                   }

                   self.user(json.Data.user);
                   //alert(json.Data);
                   //alert(json.Data.openId);
                   setTimeout(function () {
                       self.$DomParm().$endMp3[0].play();

                   }, 1000);
               }
               else {
                   console.log(json.Message);
                  // alert(json.Message);
               }
           }).fail(
           function (jqxhr, textStatus, error) {
               var err = textStatus + ", " + error;
               console.log("Request Failed: " + err);
           });

       
    };

    this.receiveCoupon = function () {
        $.getJSON("WebService/CouponWebService.asmx/ReceiveCoupon", {
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




