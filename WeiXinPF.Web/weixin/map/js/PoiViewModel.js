﻿//增加format扩展
String.prototype.format = function (args) {
    var result = this;
    if (arguments.length > 0) {
        if (arguments.length == 1 && typeof (args) == "object") {
            for (var key in args) {
                if (args[key] != undefined) {
                    var reg = new RegExp("({" + key + "})", "g");
                    result = result.replace(reg, args[key]);
                }
            }
        }
        else {
            for (var i = 0; i < arguments.length; i++) {
                if (arguments[i] != undefined) {
                    var reg = new RegExp("({)" + i + "(})", "g");
                    result = result.replace(reg, arguments[i]);
                }
            }
        }
    }
    return result;
}

var poiOptions = {
    scenic: {
        name: '景点列表',
        ajaxUrl: '/WebServices/MapWebService.asmx/GetScenics'
    },
    catering: {
        name: '周边美食',
        ajaxUrl: '/WebServices/MapWebService.asmx/GetCateringShops',
        detailUrl: '/weixin/restaurant/index.aspx?wid={wid}&shopid={id}'//&openid={openId}'
    },
    hotel: {
        name: '周边酒店',
        ajaxUrl: '/WebServices/MapWebService.asmx/GetHotels',
        detailUrl: '/weixin/knshotel/index.aspx?wid={wid}&hotelid={id}'//&openid={openId}'
    }
}



var PoiViewModel = function (param) {
    var self = this;

    this.title = ko.observable();
    this.keywords = ko.observable('');
    this.pois = ko.observableArray();
    this.param = param;
    this.options = null;

    this.actionName = ko.pureComputed(function () {
        if (self.param.type == 'scenic') {
            return '介绍';
        }
        return '预定';
    }, this);

    this.actionCss = ko.pureComputed(function () {
        if (self.param.type == 'scenic') {
            return 'fa fa-bullhorn';
        }
        return 'fa fa-money';
    }, this);



    var init = function () {
        self.options = poiOptions[self.param.type];
        if (!self.options) return;

        //设置标题
        document.title = self.options.name;
        self.title(self.options.name);
        loadData();
    }

    var loadData = function () {
        $.ajax({
            url: self.options.ajaxUrl,
            dataType: "json",
            data: { wid: self.param.wid, keywords: self.keywords() }
        }).done(function (res) {
            if (res.isSuccess) {
                self.pois(res.data);
            }
            else {
                alert(res.message);
            }
        }).fail(function (a, b, c) {
            alert(JSON.stringify(a));
            alert(b)
        });
    };

    self.search = function () {
        loadData();
    };

    self.book = function (poi) {
        if (poi.url) {
            document.location.href = poi.url;
        }
        else {
            document.location.href = self.options.detailUrl.format({ wid: self.param.wid, id: poi.id });//, openId: self.param.openId });
        }
    }

    self.go = function (poi) {
        document.location.href = 'map_address.html?type={type}&id={id}'.format({ type: self.param.type, id: poi.id });
    }

    init();
}