var mk = mk || {};
(function ($) {
    var ui = {
        crop_container: "#crop_container",
        crop_layer_body: '.crop-layer-body',
        crop_img: "#crop_img",
        preview_img: "#preview_img",
        preview_size: { width: 100, height: 100 },
        showMask: function (msg) {
            $("#mask").show();
        },
        hideMask: function () {
            $("#mask").hide();
        }
    }

    mk.cropFormServer = function () {
        var formData = getCoordinate();
        if (!formData) {
            return;
        }

        formData.imgName = ui.image.imgName;

        $.ajax({
            url: '../../WebServices/MonsterWebService.asmx/CropImage',
            data: formData,
            type: 'post',
            dataType: 'json',
            success: function (res) {
                if (res && res.IsSuccess) {
                    showPhoto(res.Data);
                }
                else {
                    alert(res.Message);
                    ui.hideMask();
                }
            },
            error: function (a, b, c) {
                //$("#token").html(a.response);
            }
        });
    }

    mk.chooseImage = function () {
        wxsdk.chooseImage({
            success: function (chooseResult) {
                ui.showMask();
                var localId = chooseResult.localIds[0];
                wxsdk.uploadImage({
                    localId: localId,
                    success: function (uploadResult) {
                        var serverId = uploadResult.serverId;
                        downImageFromServer(serverId);
                    }
                });
            }
        });
    }

    mk.getInfo = function(aid,wid,code) {
        $.getJSON("../../WebServices/MonsterWebService.asmx/GetBaseInfo",
           {
               aid: aid,
               wid: wid

           })
           .done(function (json) {
               console.log(json);
               if (json.IsSuccess) {
                   document.title = json.Data.actName;
                   $("#msg_monster").text(json.Data.actContent);
                   if (json.Data.status_s == "nostart") {
                       $(".div-nostart").show();
                        
                       $(".container").hide();
                       $("#crop_container").hide();
                   }
                   else if (json.Data.status_s == "end") {
                       $(".div-expired").show();
                       $(".container").hide();

                       $("#crop_container").hide();
                   } else {
                       $(".container").show();
                   }
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


    var showPhoto = function (img) {
        var url = 'ShowPhoto.html?img={0}&scenic={1}&wid={2}&aid={3}';
        document.location.href = formatString(url, img, getRandom(2),wid,aid);
    }


    //设置图片尺寸
    var setZoom = function (url) {
        var image = new Image();
        var crop_height = 300;
        image.onload = function () {
            var n = $(ui.crop_layer_body).width();
            var p = $(ui.crop_layer_body).height();
            var o = image.width / n;
            var l = image.height / p;

            if (o < 1 && l < 1) {
                o = 1;
                l = 1;
            } else {
                if (o > 1 || l > 1) {
                    var m = o >= l ? o : l;
                    o = m;
                    l = m;
                    if (ui.jsScropApi) {
                        ui.jsScropApi.destroy();
                    }
                    $(ui.crop_img).attr("src", image.src);
                    $(ui.preview_img).attr("src", image.src);
                    $(ui.crop_img).width(image.width / o);
                    $(ui.crop_img).height(image.height / l);

                    ui.zoom = {
                        wScale: o,
                        hScale: l
                    }
                    crop();
                }
            }
        }

        image.setAttribute("src", url);
    }

    //设置预览效果
    var setPreview = function (c) {
        if (parseInt(c.w) > 0) {
            var rx = ui.preview_size.width / c.w;
            var ry = ui.preview_size.height / c.h;

            $(ui.preview_img).css({
                width: Math.round(rx * $(ui.crop_img).width()) + 'px',
                height: Math.round(ry * $(ui.crop_img).height()) + 'px',
                marginLeft: '-' + Math.round(rx * c.x) + 'px',
                marginTop: '-' + Math.round(ry * c.y) + 'px'
            });
        }
    }

    //设置裁剪坐标
    var setCoordinate = function (c) {
        ui.coordinate = c;
    }

    //获取裁剪坐标
    var getCoordinate = function () {
        if (ui.coordinate) {
            ui.coordinate.x *= ui.zoom.wScale;
            ui.coordinate.y *= ui.zoom.hScale;
            ui.coordinate.w *= ui.zoom.wScale;
            ui.coordinate.h *= ui.zoom.hScale;
            return {
                x: Math.round(ui.coordinate.x),
                y: Math.round(ui.coordinate.y),
                width: Math.round(ui.coordinate.w),
                height: Math.round(ui.coordinate.h)
            }
        }
        return null;
    }

    //设置裁剪
    var crop = function () {

        var x = ($(ui.crop_img).width() - 150) / 2;
        var x2 = x + 150;
        var y = 50;
        var y2 = 200;

        $(ui.crop_img).Jcrop({
            bgColor: "#fff",
            minSize: [150, 150],
            maxSize: [350, 350],
            setSelect: [x, y, x2, y2],
            onChange: function (c) {
                setPreview(c);
                setCoordinate(c);
            },
            onSelect: function (c) {
                setPreview(c);
                setCoordinate(c);
            },
            aspectRatio: 1
        }, function () {
            ui.jsScropApi = this;
        });
    }
    var showCrop = function (image) {
        ui.image = image;
        setZoom(ui.image.imgUrl);
        ui.hideMask();
        $(ui.crop_container).show();
    }

    var downImageFromServer = function (serverId) {
        $.ajax({
            url: '../../WebServices/MonsterWebService.asmx/DownLoadImage',
            data: { mediaId: serverId },
            type: 'post',
            dataType: 'json',
            success: function (res) {
                if (res && res.IsSuccess) {
                    showCrop(res.Data);
                }
                else {
                    alert(res.Message);
                    ui.hideMask();
                }
            },
            error: function (a, b, c) {
                $("#token").html(a.response);
            }
        });
    }
})(jQuery);

function getRandom(seed) {
    return Math.round(Math.random() * seed);
}

function formatString() {
    if (arguments.length < 1) {
        return null;
    }

    var str = arguments[0];

    for (var i = 1; i < arguments.length; i++) {
        var placeHolder = '{' + (i - 1) + '}';
        str = str.replace(placeHolder, arguments[i]);
    }

    return str;
};

$(function () {
   
    mk.getInfo(aid, wid, code);
    wxsdk.initWxConfig(false);
    $("#chooseImage").on('click', mk.chooseImage);
    $("#btnSure").on('click', mk.cropFormServer);
    $("#btnReselect").on('click', mk.chooseImage);
});