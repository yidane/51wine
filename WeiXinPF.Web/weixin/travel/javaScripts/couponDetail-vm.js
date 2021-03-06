﻿
var DetailViewModel = function ($domParam, param) {
    var self = this;
    this.$DomParm = ko.observable($domParam);
    this.param = ko.observable(param);
    this.coupon = ko.observable();
    this.status = ko.observable("unExpired");
    this.couponStatus = ko.pureComputed({
        read: function () {
            return self.status();
        },
        write: function (value) {
            switch (value) {
                case 0:
                    self.status("unExpired");
                    break;
                case 1:
                    self.status("used");
                    break;
            }
        },
        owner: this
    });
    this.profitStatus = ko.pureComputed(function () {
        var result = "";
        if (self.couponStatus() == "used") {
            result = "stamp-used";
        }
        else if (self.couponStatus() == "expired") {
            result = "stamp-overdue";
        }
        return result;
    }, this);
    this.getCoupon = function () {
        $.getJSON("../WebService/CouponWebService.asmx/GetCoupon",
            {
                openId: self.param().openId,
                couponUsageId: self.param().couponUsageId
            })
            .done(function (json) {
                if (json.IsSuccess) {
                    self.coupon(json.Data.coupon);
                    self.couponStatus(json.Data.coupon.status);
                    self.$DomParm().$qrcode.qrcode({ width: 130, height: 130, text: json.Data.coupon.couponId });
                    $("#qrcode").append(self.$DomParm().$qrcode);
                    // self.param().openId = json.Data.user.openid;
                }
            }).fail(
            function (jqxhr, textStatus, error) {
                var err = textStatus + ", " + error;
                console.log("Request Failed: " + err);
            });
    };

    this.btnUseClick = function ($confirm, $main) {
        $main.removeClass("animated bounceOut").addClass("animated bounceIn");
        $confirm.css("background-color", "rgba(0,0,0,.75)").show();
        //s.on("click", function () {

        //}), a.on("click", function () {

        //}), f.on("click", function () {
        //    $confirm.css("background-color", "rgba(0,0,0,0)"), $confirm.hide();
        //    var n = e(this);
        //    if (t.Isrequest)return !1;
        //    t.Isrequest = !0, e("#loadingTips").text(""), e.ajax({
        //        type: "GET",
        //        url: params.TmpVerifyUrl,
        //        data: {},
        //        dataType: "json",
        //        success: function (n) {
        //            n.error_code == 0 ? (s.remove(), e("body").css("padding-bottom", 0), l.show()) : (t.Isrequest = !1, c.info(n.error_msg), n.error_msg == "您已使用过本优惠券!" && (s.remove(), e("body").css("padding-bottom", 0), l.show()))
        //        },
        //        error: function (e) {
        //            t.Isrequest = !1, c.info(e)
        //        },
        //        complete: function () {
        //            e("#loadingBox").hide()
        //        }
        //    })
        //})

    };
    this.btnCancelClick = function ($confirm, $main) {
        $main.removeClass("animated bounceIn").addClass("animated bounceOut");
        
            $('#confirm-cover').css("background-color", "rgba(0,0,0,0)").hide();
        

    };

    this.btnSureClick = function ($confirm, data, event) {
        $.getJSON("../WebService/CouponWebService.asmx/UseCoupon",
           {
               openId: self.param().openId,
               couponUsageId: self.param().couponUsageId
           })
           .done(function (json) {
               if (json.IsSuccess) {
                   $confirm.css("background-color", "rgba(0,0,0,0)").hide();
                   self.couponStatus(1);
               }
           }).fail(
           function (jqxhr, textStatus, error) {
               var err = textStatus + ", " + error;
               console.log("Request Failed: " + err);
           });

    };

    this.btnGobackClick = function () {
        window.location.href = 'MyCoupons.html';//?code='+self.param().access_code;
        // window.location.href='https://open.weixin.qq.com/connect/oauth2/authorize?appid=wxdd6127bdb5e7611c&redirect_uri=http%3A%2F%2Fwww.cloudorg.com.cn%2Ftravel%2FCoupons%2FMyCoupons.html&response_type=code&scope=snsapi_base&state=STATE%23wechat_redirect&connect_redirect=1#wechat_redirect';
        //window.history.back();
    };

    this.getCoupon();
};