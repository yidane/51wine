
var MyCouponsViewModel= function ($domParam, param) {
  var self=this;
    this.$DomParm = ko.observable($domParam);
    this.param = ko.observable(param);
   // this.user = ko.observable();
    this.openId = ko.observable("");
    this.expiredCoupons=ko.observableArray();
    this.unExpiredCoupons=ko.observableArray();
    this.usedCoupons=ko.observableArray();
    this.selectedType=ko.observable("unExpired");

    this.getCoupons=function(){
        $.getJSON("../WebService/CouponWebService.asmx/GetCouponList", {access_code:self.param().access_code})
            .done(function (json) {
                if (json.IsSuccess) {
                    self.expiredCoupons(json.Data.lists.ExpiredCoupons);
                    self.unExpiredCoupons(json.Data.lists.UnExpiredCoupons);
                    self.usedCoupons(json.Data.lists.UsedCoupons);
                    self.openId(json.Data.user.openid);
                }
            }).fail(
            function (jqxhr, textStatus, error) {
                var err = textStatus + ", " + error;
                console.log("Request Failed: " + err);
            });
    };
    this.changeCoupon=function(data){
        self.selectedType(data);
        //var t=$(".coupons-list")
        //if (t.length > 0)for (var n = 0; n < t.length; n++)(function (n) {
        //    setTimeout(function () {
        //        e(t[n]).addClass("fadeInRight")
        //    }, n * 300)
        //}
    };

    //执行获取数据
    self.getCoupons();
};