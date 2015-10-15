var mapViewModel = function (params, $doms) {
    //propertes
    var self = this;
    this.params = ko.observable(params);
    this.$doms = ko.observable($doms);
    this.startlocation = ko.observable('当前位置');
    this.formshow = ko.observable(false);
    this.endlocation = ko.observable('');
    this.info = ko.observable();
    this.isSetInfo = ko.observable(false);
    this.map = ko.observable();
    this.infoWin = ko.observable();
    this.startmarker = ko.observable();
    this.endmarker = ko.observable();
    this.service = ko.observable();
    this.searchservice = ko.observable();
    this.polyline = ko.observable();
    this.localinfo = ko.observable({
        latlng: {lat: 48.6589239484962, lng: 87.04087972640991},
        location: '新疆维吾尔自治区',
        locationdetail: '新疆维吾尔自治区阿勒泰地区布尔津县'
    });
    this.seachType = ko.observable("start");
    this.statusbox = {
        text: ko.observable('亲，出错了！'), show: ko.observable(false), close: function () {
            self.statusbox.show(false);

        }
    };
    this.markers = ko.observable();
    this.menuType=ko.observable('locale');

    this.init = function () {

        //self.initmap();
        //self.getLocation();
        //当前位子
        if (self.params().uselocale) {
            self.getLocation();
            self.togglesearchroad();
            self.menuType('locale');
        }
        else {
            if (!self.map()) {
                var point = new qq.maps.LatLng(48.65916135658294, 87.03942060470581);
                self.initmap(point);
            }
        }
    };
    this.togglesearchroad = function () {
        self.formshow(!self.formshow());
    };

    //func
    this.getLocation = function () {
        if (navigator.geolocation) {
            //self.$doms().$txtbegin.val('当前位置');
            navigator.geolocation.getCurrentPosition(self.showPosition);
        } else {
            x.innerHTML = "浏览器不支持定位.";
        }
    };
    this.showPosition = function (position) {
        var lat = position.coords.latitude;
        var lng = position.coords.longitude;
        //todo 移除测试代码
        var point = new qq.maps.LatLng(lat, lng);
        //  var point = new qq.maps.LatLng(48.65916135658294, 87.03942060470581);

        //如果地图没初始化
        if (!self.map()) {
            self.initmap(point);
        }
        if (point) {
            self.map().setCenter(point);
            self.startlocation('当前位置');
        }
        //如果初始了点则添加Marker
        self.setstartmarker(point);
    };

    this.initmap = function () {
        var map = new qq.maps.Map($("#container")[0], {
            zoomControlOptions: {
                position: qq.maps.ControlPosition.LEFT_TOP
            }
        });
        qq.maps.event.addListener(map, "click", function (e) {
            if (self.markers()) {
                for (var i = 0; i < self.markers().length; i++) {
                    var data = self.markers()[i];
                    if (data.infoWin) {
                        data.infoWin.close();
                    }


                }
            }


            //content.show();
        });
        self.map(map);

        //info
        var infoWin = new qq.maps.InfoWindow({
            map: map
        });
        self.infoWin(infoWin);


        //导航路线
        self.adddirveservice();


        //搜索服务
        self.addsearchservice(map);

        //添加autocomplete
        self.addAutocomplete(self.searchservice());


        if (self.params().id) {
            self.getinfo();
        }
        if (self.params().showallmarker) {
            self.getmarkers(self.params().wid);
            self.trggleMenuType('showallmarker');
        }
    };
    this.adddirveservice = function () {
        //导航路线
        var drivingservice = new qq.maps.DrivingService({
            complete: function (response) {
                self.drivingServiceComplete(response);
            }
        });
        self.service(drivingservice);
    };
    this.addsearchservice = function (map) {
        //搜索服务
        var searchService = new qq.maps.SearchService({
            map: map,
            pageCapacity: 1,
            complete: function (res) {
                if (res && res.detail && res.detail.pois) {
                    if (res.detail.pois.length > 0) {
                        var firstpoi = res.detail.pois[0];
                        if (firstpoi) {
                            self.setmaker(firstpoi);
                        }
                    }
                }
            }
        });
        searchService.setLocation(self.localinfo().locationdetail);
        self.searchservice(searchService);
    };
    this.addAutocomplete = function (searchService) {
        var apstart = new qq.maps.place.Autocomplete(self.$doms().$txtbegin[0], {

            location: self.localinfo().location
        });
        qq.maps.event.addListener(apstart, "confirm", function (res) {
            self.seachType('start');
            searchService.search(res.value);
        });
        var apend = new qq.maps.place.Autocomplete(self.$doms().$txtend[0], {

            location: self.localinfo().location
        });
        qq.maps.event.addListener(apend, "confirm", function (res) {
            self.seachType('end');
            searchService.search(res.value);
        });
    };

    this.clearOverlay = function (overlay) {
        //清除地图上的marker

        if (overlay) {
            overlay.setMap(null);

        }
    };

    this.drivingServiceComplete = function (response) {
        var start = response.detail.start,
            end = response.detail.end;

        var directions_routes = response.detail.routes;
        var routes_desc = [];
        //所有可选路线方案
        for (var i = 0; i < directions_routes.length; i++) {
            var route = directions_routes[i];
            //调整地图窗口显示所有路线
            self.map().fitBounds(response.detail.bounds);
            //所有路程信息
            //for(var j = 0 ; j < legs.length; j++){ 
            var polyline = new qq.maps.Polyline(
                {
                    path: route.path,
                    strokeColor: '#3893F9',
                    strokeWeight: 6,
                    map: self.map()
                }
            );
            self.polyline(polyline);
        }
    };

    this.searchRoad = function () {
        if (!self.startmarker()) {
            self.statusbox.text('亲，请输入或确认起点！');
            self.statusbox.show(true);
            return;
        }
        if (!self.endmarker()) {
            self.statusbox.text('亲，请输入或确认终点！');
            self.statusbox.show(true);
            return;
        }
        self.clearOverlay(self.polyline());
        self.service().setLocation("新疆维吾尔自治区阿勒泰地区布尔津县");
        var policy = self.$doms().$policy.val();
        self.service().setPolicy(qq.maps.DrivingPolicy[policy]);
        self.service().search(self.startmarker().position,
            self.endmarker().position);
    };
    this.getmarkers = function (wid) {
        $.ajax({
            url: '../../WebServices/MapWebService.asmx/GetMarkers',
            data: {wid: wid},
            type: 'post',
            dataType: 'json',
            success: function (json) {
                if (json.success) {
                    var data = [];
                    for (var i = 0; i < json.result.length; i++) {
                        var item = json.result[i];
                        item.address = item.description;
                        item.position = {lat: item.lat, lng: item.lng};
                        data.push(item);
                    }
                    self.markers(data);
                    self.addmarkers();
                }
            },
            error: function (a, b, c) {
                var err = textStatus + ", " + error;
                console.log("Request Failed: " + err);
            }
        });
    };
    this.addmarkers = function () {
        var map = self.map();
        for (var i = 0; i < self.markers().length; i++) {
            var data = self.markers()[i];
            var point = new qq.maps.LatLng(data.lat, data.lng);
            var marker = new qq.maps.Marker({

                map: self.map(),
                position: point
            });
            data.target = marker;
            //info
            var infoWin = new qq.maps.InfoWindow({
                map: map
            });
            data.infoWin = infoWin;
            //self.infoWin(infoWin);


            qq.maps.event.addListener(marker, "click", function (e) {
                var marker = e.target;
                for (var i = 0; i < self.markers().length; i++) {
                    var data = self.markers()[i];
                    if (data.infoWin) {
                        data.infoWin.close();
                    }
                    if (data.target == marker) {
                        self.info(data);
                        var point = new qq.maps.LatLng(self.info().position.lat, self.info().position.lng);
                        data.infoWin.setPosition(point);
                        var content = self.$doms().$info_pop;
                        // var content=  G('inf o_pop');
                        data.infoWin.setContent(content[0]);

                        data.infoWin.open();
                    }

                }


                //content.show();
            });
        }
        if (self.markers()) {
            var lastmarker = self.markers()[self.markers().length - 1];
            map.panTo(new qq.maps.LatLng(lastmarker.lat, lastmarker.lng));
            map.zoomTo(11);
        }
    };

    this.getinfo = function () {

        $.ajax({
            url: '../../WebServices/MapWebService.asmx/GetMapInfo',
            data: {id: self.params().id,type:self.params().type},
            type: 'post',
            dataType: 'json',
            success: function (json) {
                if (json.IsSuccess) {
                    self.info(json.Data);
                    self.endlocation(json.Data.name);
                    var point = new qq.maps.LatLng(self.info().position.lat, self.info().position.lng);
                    var anchor = new qq.maps.Point(16, 16),
                        size = new qq.maps.Size(32, 37),
                        end_icon = new qq.maps.MarkerImage(
                            'http://s.map.qq.com/themes/default/img/busmapicon.png',
                            size,
                            new qq.maps.Point(32, 0),
                            anchor
                        );

                    //marker
                    var marker = new qq.maps.Marker({
                        icon: end_icon,
                        map: self.map(),
                        position: point
                    });
                    self.endmarker(marker);
                    // marker.setTitle('teststsetset');
                    //marker-click
                    qq.maps.event.addListener(self.endmarker(), "click", function (e) {

                        if (!self.isSetInfo()) {
                            self.isSetInfo(true);
                            self.setinfo();
                        }
                        self.infoWin().open();
                        var content = self.$doms().$info_pop;
                        content.show();
                    });

                   if(self.params().uselocale)
                   {
                       //完成后搜索
                       self.searchRoad();
                   }
                    else
                   {
                        self.map().panTo(point);
                       self.map().zoomTo(18);
                   }

                    if (!self.isSetInfo()) {
                        self.isSetInfo(true);
                        self.setinfo();
                    }
                }
            },
            error: function (a, b, c) {
                var err = textStatus + ", " + error;
                console.log("Request Failed: " + err);
            }
        });


    };


    this.setinfo = function () {


        // infoWin.open();

        //            var content = '<div style="margin:0;line-height:20px;padding:2px;">'
        //                        + '<p>名称：' + info.name + '</p>'
        //                        + '<p>导航：' + info.navigate + '</p>'
        //                    + '<p>进度：' + info.position.lng + '</p>'
        //                    + '<p>维度：' + info.position.lat + '</p>'
        //                    + '</div>';

        var content = self.$doms().$info_pop;
        // var content=  G('inf o_pop');


        var point = new qq.maps.LatLng(self.info().position.lat, self.info().position.lng);
        self.infoWin().setContent(content[0]);
        self.infoWin().setPosition(point);

        //settitle
        document.title = self.info().name;


        //content.show();


    };
    this.setmaker = function (firstpoi) {
        var point = firstpoi.latLng;
        if (self.seachType() == 'start') {
            self.setstartmarker(point);
        }
        else {
            self.setendmarker(point);
        }
    };

    this.setstartmarker = function (point) {
        self.clearOverlay(self.startmarker());
        //   self.map().setCenter(point);
        var anchor = new qq.maps.Point(6, 6),
            size = new qq.maps.Size(32, 37),
            start_icon = new qq.maps.MarkerImage(
                'http://s.map.qq.com/themes/default/img/busmapicon.png',
                size,
                new qq.maps.Point(0, 0),
                anchor
            );
        //marker
        var marker = new qq.maps.Marker({
            icon: start_icon,
            map: self.map(),
            position: point
        });
        self.startmarker(marker);
    };
    this.setendmarker = function (point) {
        self.clearOverlay(self.endmarker());
        var anchor = new qq.maps.Point(6, 6),
            size = new qq.maps.Size(32, 37),
            end_icon = new qq.maps.MarkerImage(
                'http://s.map.qq.com/themes/default/img/busmapicon.png',
                size,
                new qq.maps.Point(32, 0),
                anchor
            );

        //marker
        var marker = new qq.maps.Marker({
            icon: end_icon,
            map: self.map(),
            position: point
        });
        self.endmarker(marker);
    };

    this.trggleMenuType=function(data){
self.menuType(data);
    };
};