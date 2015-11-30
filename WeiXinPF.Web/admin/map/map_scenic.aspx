<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="map_scenic.aspx.cs" Inherits="WeiXinPF.Web.admin.map.map_scenic" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>景区导航设置</title>
    <link rel="stylesheet" href="http://cdn.leafletjs.com/leaflet-1.0.0-b1/leaflet.css" />

    <link href="//cdn.bootcss.com/font-awesome/4.4.0/css/font-awesome.css" rel="stylesheet" />
    <link rel="stylesheet" href="../../weixin/map/css/L.Control.Locate.min.css" />
    <!--[if lt IE 9]>
    <link rel="stylesheet" href="../../weixin/map/css/L.Control.Locate.ie.min.css"/>
    <![endif]-->
    <link href="../../scripts/lhgdialog/skins/idialog.css" rel="stylesheet" />
    <link href="../../weixin/map/css/mapScenic.css" rel="stylesheet" />
    <style>
        html, body {
            width: 100%;
            height: 100%;
            overflow: hidden;
        }

        .mainbody {
            padding: 10px 15px;
        }

        #map {
            height: 92%;
            width: 98%;
        }


        .tip-bottom ul li {
            width: 33.33333%;
        }

        .location {
            padding-bottom: 9px;
            border-bottom: solid 1px #e1e1e1;
            height: 32px;
            line-height: 24px;
            font-size: 12px;
            color: #333;
        }

            .location a.back {
                margin-right: 15px;
            }

            .location a {
                display: inline-block;
                color: #333;
                text-decoration: none;
            }

                .location a.home i {
                    background-position: -28px 0;
                }

                .location a i {
                    display: inline-block;
                    margin-right: 5px;
                    width: 14px;
                    height: 14px;
                    text-indent: -999em;
                    background: url(../skin/default/skin_icons.png) no-repeat;
                    vertical-align: middle;
                }
    </style>
</head>
<body class="mainbody">
    <div class="location">
        <a class="home" href="javascript:;"><i></i><span>景区导航设置</span></a>
    </div>
    <div id="map"></div>
    <div style="display: none">
        <div class="marker-list" data-bind="foreach: markers">
            <div class="marker" data-bind="click: $root.showtip">
                <i class="point"></i>
                <div data-bind="template: { name: 'marker-template', data: $data }"></div>
            </div>
        </div>


    </div>
    <div style="display: none" id="div_newMarker" data-bind="template: { name: 'marker-template', data: newMarker }">
    </div>

    <script type="text/html" id="marker-template">
        <div class="tip" data-bind="attr: { id: 'tip_' + id }">
            <div class="tip-title">
                <h3 data-bind="text: name"></h3>

            </div>
            <div class="tip-content" data-bind="text: remark"></div>
            <div class="tip-bottom">
                <ul>
                    <li data-bind=" enable: id <= 0">
                        <a href="#" data-bind="attr: { 'data-left': left, 'data-top': top }"
                            onclick="addRemark($(this).attr('data-left'),$(this).attr('data-top'))">
                            <i class="fa fa-plus fa-lg" style="color: #1d9d74; margin-right: 5px;"></i>
                            <span>添加</span>
                        </a>
                    </li>
                    <li data-bind=" enable: id > 0">
                        <a href="#" data-bind="attr: { 'data-id': id }"
                            onclick="editRemark($(this).attr('data-id'))">
                            <i class="fa fa-edit fa-lg" style="color: #337ab7; margin-right: 5px;"></i><span>修改</span>
                        </a>
                    </li>
                    <li data-bind=" enable: id > 0">
                        <a href="#"
                            data-bind="attr: { 'data-id': id, 'data-name': name }"
                            onclick="deleteRemark($(this).attr('data-id'), $(this).attr('data-name'))">
                            <i class="fa fa-trash fa-lg" style="color: #c9302c; margin-right: 5px;"></i><span>删除</span>
                        </a>
                    </li>
                </ul>
            </div>

        </div>
    </script>


    <script src="http://cdn.leafletjs.com/leaflet-1.0.0-b1/leaflet.js"></script>
    <script src="../../weixin/map/js/bouncemarker.js"></script>
    <script src="../../scripts/jquery/jquery-2.1.0.min.js"></script>
    <script src="../../scripts/lhgdialog/lhgdialog.js"></script>
    <script src="../../scripts/knockout/knockout-3.3.0.js"></script>

    <script type="text/javascript">
        var ViewModel = function (params) {
            var self = this;
            this.params = ko.observable(params);
            this.map = ko.observable();
            this.mapborder = ko.observable();
            this.markers = ko.observableArray();
            this.newMarker = ko.observable({
                id: 0,
                name: '',
                remark: '',
                left: 0,
                top: 0
            });



            this.init = function () {
                self.initmap(40.0625, 116.34375);
                var imageurl = "/weixin/map/images/map.jpg";
                self.addtileLayer();//添加缩放图层
                self.bindevent();//添加绑定事件

                this.loadData();
            };

            this.initmap = function (lat, lng) {
                var mapMinZoom = 5;
                var mapMaxZoom = 5;
                var map = L.map('map', {
                    maxZoom: mapMaxZoom,
                    minZoom: mapMinZoom,
                    attributionControl: false,
                    crs: L.CRS.Simple
                });
                var mapsize = self.getMapSize();
                var _mapCenter = map.unproject([1612, 1462], mapMaxZoom);
                map.setView(_mapCenter, mapMinZoom + 1);
                self.map(map);

            };
            this.getMapSize = function () {
                return { height: 6068 / 2, width: 8858 / 2 };
            };
            this.getborderlatlng = function () {
                return { lat: 39.99848, lng: 116.3469 };
            };

            this.addtileLayer = function () {
                var map = self.map();
                var mapsize = self.getMapSize();
                var mapMinZoom = map.getMinZoom();
                var mapMaxZoom = map.getMaxZoom();
                var mapBounds;
                if (!self.mapborder()) {
                    var southWest = map.unproject([0, mapsize.height], map.getMaxZoom());
                    var northEast = map.unproject([mapsize.width, 0], map.getMaxZoom());

                    mapBounds = new L.LatLngBounds(southWest, northEast);
                    self.setborder(mapBounds);
                }
                else {
                    mapBounds = self.mapborder();
                }
                L.tileLayer('../../weixin/map/images/map/{z}/{x}/{y}.png', {
                    minZoom: mapMinZoom, maxZoom: mapMaxZoom,
                    bounds: mapBounds,noWrap: true
                }).addTo(map);


            };
            this.addScenicLayer = function (imageurl) {

                var map = self.map();
                var mapsize = self.getMapSize();
                var latlng = self.getborderlatlng();


                var southWest = map.unproject([0, mapsize.height], map.getMaxZoom());
                var northEast = map.unproject([mapsize.width, 0], map.getMaxZoom());

               
                var bounds = new L.LatLngBounds(southWest, northEast);
                self.mapborder(bounds);
                self.setborder(bounds);
                new L.ImageOverlay(imageurl, bounds).addTo(map);
            };
            this.setborder = function (bounds) {
                var map = self.map();
                map.setMaxBounds(bounds);
                //阻止拖出边界
                map.on('drag', function () {
                    map.panInsideBounds(bounds, { animate: true });
                });
            };
            this.bindevent = function () {
                var map = self.map();
                map
                        .on('zoom', function (event) {
                            console.log("map zoom :", event);
                        })
                        .on("locationfound", function (event) {

                            console.log("locationfound :", event);
                        })
                        .on("locationerror", function (event) {

                            console.log("locationerror :", event);
                        })
                        .on("popupopen", function (spot) {
                            console.log(spot);

                            //修改地图包样式.  这并不完美..

                            setTimeout(function () {
                                $('.leaflet-popup-tip').addClass('has_point_detail');
                            }, 200);

                        })
                        .on('click', function (event) {
                            var map = self.map();


                            console.log("map click :", event);
                            var point = map.project(event.latlng);
                            console.log(point);
                            //                            console.log(event.containerPoint);

                            //                            var marker = L.marker(event.latlng).addTo(map);
                            var newMarker = self.newMarker();
                            newMarker.name = '提示';
                            newMarker.remark = '请点击[添加]按钮添加景点';
                            newMarker.left = point.x;
                            newMarker.top = point.y;
                            self.newMarker(newMarker);
                            var content = $('#div_newMarker').html();
                            var popup = self.getPopup(content);
                            popup
                                .setLatLng(event.latlng)
//                                .setContent('<p>Hello world!<br />This is a nice popup.</p>')
                                .openOn(map);
                        });
            };



            this.loadData = function () {
                $.ajax({
                    url: '/WebServices/MapWebService.asmx/GetMarkers',
                    dataType: "json",
                    data: { wid: self.params().wid },
                    success: function (res) {
                        if (res.isSuccess) {
                            console.log(res.data);
                            self.markers(res.data);
                            self.addmarkers();

                        } else {
                            console.log(res.message);
                        }
                    }
                });
            };



            
            self.addmarkers = function () {
                var map = self.map();
                for (var i = 0; i < self.markers().length; i++) {
                    var data = self.markers()[i];
                    var latlng = map.unproject([data.left, data.top], map.getMaxZoom());
                    var marker = L.marker(latlng, {
                        bounceOnAdd: true,
                        bounceOnAddOptions: { duration: 400 * (i + 1) + 500, height: 100 },
                        bounceOnAddCallback: function () { console.log("done"); }
                    }).addTo(map);
                    marker.on('click', function (e) {
                        e.target.bounce({ height: 100 });
                    });

                    //                    var marker = L.marker(self.getLatLng(data.left,data.top)).addTo(map);
                    var content = $('#tip_' + data.id).html();
                    var popup = self.getPopup(content);
                    marker.bindPopup(popup);
                }
            };
            self.getPopup = function (content) {

                var popup = new L.popup({
                    className: 'tip',
                    closeButton: false
                });
                popup.setContent(content);
                return popup;
            };
            self.getLatLng = function (x, y) {
                return self._XYToLatLng(x, y);
            };
            self._XYToLatLng = function (x, y) {
                if (x instanceof Array)
                    y = x[1], x = x[0];
                //                var OE.Scenic.originalZoom=10;
                var originalZoom = 10 * 1.0;
                var maxSize = Math.pow(2, originalZoom + 8) * 1.0;
                var mapsize = self.getMapSize(),
                        lat = (y * mapsize.height + (maxSize - mapsize.height) / 2) / maxSize,
                        lng = (x * mapsize.width + (maxSize - mapsize.width) / 2) / maxSize;
                return [-256 * lat, 256 * lng];
            };

            self.jumpUrl = function (url) {
                document.location.href = url;
            };


            this.init();

        };
        function addRemark(left, top) {


            var url = 'marker_edit.aspx?action=Add&top=' + top + '&left=' + left;
            jumpdirect(url);
        }
        function editRemark(id) {

            var url = 'marker_edit.aspx?action=Edit&id=' + id;
            jumpdirect(url);
        }
        function deleteRemark(id, name) {
            console.log(id);
            $.dialog.confirm('确认删除[' + name + ']吗？', function () {
                $.ajax({
                    url: '/WebServices/MapWebService.asmx/DeleteMarker',
                    dataType: "json",
                    data: { id: id },
                    success: function (res) {
                        if (res.isSuccess) {

                            jumpdirect("map_scenic.aspx");

                        } else {
                            console.log(res.message);
                        }
                    }
                });
            });

        }

        function jumpdirect(url) {
            top.frames["mainframe"].location.href = url;


            var e = window.event || event;

            if (e.stopPropagation) { //如果提供了事件对象，则这是一个非IE浏览器
                e.stopPropagation();
            } else {
                //兼容IE的方式来取消事件冒泡
                window.event.cancelBubble = true;
            }
        }

    </script>


    <script>
        var params = { wid: 0 };
        $(function () {

            $('.content').scrollTop(100);
            $('.content').scrollLeft(400);
            var wid = getQueryString('wid');
            if (wid) {
                params.wid = wid;
            }

            var viewModel = new ViewModel(params);
            ko.applyBindings(viewModel);
        });
        function getQueryString(name) {
            var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)", "i");
            var r = window.location.search.substr(1).match(reg);
            if (r != null) return (r[2]);
            return null;
        }



    </script>
</body>
</html>
