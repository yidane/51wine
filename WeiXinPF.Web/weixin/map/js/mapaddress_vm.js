var mapViewModel = function (params, $doms) {
    //propertes
    var self = this;
    this.params = ko.observable(params);
    this.$doms = ko.observable($doms);
    this.info = ko.observable();
    this.isSetInfo = ko.observable(false);
    this.map = ko.observable();
    this.infoWin = ko.observable();
    this.startmarker = ko.observable();
    this.endmarker = ko.observable();
    this.service = ko.observable();
    this.polyline=ko.observable();

    //func
    this.getLocation = function () {
        if (navigator.geolocation) {
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

        self.initmap(point);
    };

    this.initmap = function (latLng) {
        var latlng = new qq.maps.LatLng(latLng.lat, latLng.lng);
        //map
        var map = new qq.maps.Map($("#container")[0], {
            center: latlng
           // zoom: 13
        });
        self.map(map);

        //info
        var infoWin = new qq.maps.InfoWindow({
            map: map
        });
        self.infoWin(infoWin);

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
            map: map,
            position: latlng
        });
        self.startmarker(marker);
        // marker.setTitle('teststsetset');
        //marker-click
        //qq.maps.event.addListener(marker, "click", function (e) {
        //
        //
        //
        //});

        var service = new qq.maps.DrivingService({
            complete: function (response) {
                self.drivingServiceComplete(response);
            }
        });
        self.service(service);

        self.getinfo();

        

      
    };

    this.clearOverlay=function(overlay){
        //清除地图上的marker

         if (overlay)
         {
             overlay.setMap(null);

         }
    };

    this.drivingServiceComplete = function (response) {
        var start = response.detail.start,
            end = response.detail.end;

     var   directions_routes = response.detail.routes;
        var routes_desc=[];
        //所有可选路线方案
        for(var i = 0;i < directions_routes.length; i++) {
            var route = directions_routes[i] ;
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
        self.clearOverlay(self.polyline());
        self.service().setLocation("新疆维吾尔自治区阿勒泰地区布尔津县");
        var policy = self.$doms().$policy.val();
        self.service().setPolicy(qq.maps.DrivingPolicy[policy]);
        self.service().search(self.startmarker().position,
            self.endmarker().position);
    };

    this.getinfo = function () {

        $.ajax({
            url: '../../WebServices/MapWebService.asmx/GetMapInfo',
            data: { id: self.params().id },
            type: 'post',
            dataType: 'json',
            success: function (json) {
                if (json.IsSuccess) {
                    self.info(json.Data);

                    var point = new qq.maps.LatLng(self.info().position.lat, self.info().position.lng);
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
                    // marker.setTitle('teststsetset');
                    //marker-click
                    qq.maps.event.addListener(marker, "click", function (e) {

                        if (!self.isSetInfo()) {
                            self.isSetInfo(true);
                            self.setinfo();
                        }
                        self.infoWin().open();
                    });

                    //完成后搜索         
                    self.searchRoad();

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


        content.show();


    }
};