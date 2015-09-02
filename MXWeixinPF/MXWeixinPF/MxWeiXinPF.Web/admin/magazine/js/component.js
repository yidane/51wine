/// <reference path="jquery-2.1.1.min.js" />
/*if(window.location.protocol == 'file:'){
  alert('To test this demo properly please use a local server such as XAMPP or WAMP. See README.md for more details.');
}*/

var resizeableImage = function () {

    // Some variable and settings
    var $container, image_target,
        orig_src = new Image(),
        newimg = new Image(),
        event_state = {},
        constrain = false,
        min_width = 12, // Change as required
        min_height = 12,
        max_width = 800, // Change as required
        max_height = 900,
        df_width = 60, // Change as required
        df_height = 60,
        resize_canvas = document.createElement('canvas'),
        cot_left = $(".component").offset().left,
        cot_top = $(".component").offset().top;

    init = function () {

        // Assign the container to a variable   
        //初始化大小
        var width = df_width, height;

        //初始化所有的动画图片
        $.post("../magazine/getdata.ashx?myact=getAm", { iid: $("#g_iid").val() }, function (data) {
            if (data.res != 0)
                $("#mzimg").attr("src", data.img);
   
            if (data.res == 1) {//修改动画图片  
                for (var i = 0; i < data.content.length; i++) {
                    var curItem = data.content[i];
                    //右框显示详情
                    $("div.addEle").before(pjDl(curItem, ''));

                    //height = width / $itme.width() * $itme.height();
                    var curImg = $('<img class="resize-image" src="' + curItem.img + '" alt="" id="i_' + curItem.id + '" />');

                    $(".component").append(curImg);

                    //定大小
                    resizeImage(curImg, curItem.width, curItem.height);

                    //定父级
                    curImg.wrap('<div class="resize-container"></div>')
                    .after('<span class="resize-handle resize-handle-se"></span>');
                    $container = curImg.parent(".resize-container");

                    //定位置
                    $container.offset({
                        'left': parseInt(curItem.x_lc) + cot_left,
                        'top': parseInt(curItem.y_lc) + cot_top
                    });

                    // Add events  
                    $container.on('mousedown touchstart', '.resize-handle', startResize);
                    $container.on('mousedown touchstart', 'img', startMoving);
                    curImg.siblings(".resize-handle").on("mouseover", tabObj);
                    curImg.siblings(".resize-handle").on("mouseout", cancelOver);
                    $(".resize-image").on("mouseover", tabObj);
                    $(".resize-image").on("mouseout", cancelOver);
                }
            } else {    //添加动画图片

            }


        }, "json");

        //右边数据调整
        rightDl();

        //添加元素
        addEle();
    };

    //添加元素
    addEle = function () {
        $("div.addEle").click(function () {
            var str = '<img src="img/icon-add.png" style="vertical-align: middle;"/><span class="newimg">添加图片</span>';
            $(this).before(pjDl(null, str));
        })
    }

    //拼接dl
    pjDl = function (param, imghtml) {
        var e_wid = '', e_img = '', e_hei = '', e_x = '', e_y = '', e_ss = '', e_cs = '', e_tp = '', e_id = $(".animate_dl>dl").length + 1;

        if (param != null) {
            e_img = param.img;
            e_wid = param.width;
            e_hei = param.height;
            e_x = param.x_lc;
            e_y = param.y_lc;
            e_ss = param.s_sec;
            e_cs = param.c_sec;
            e_tp = param.type;
            e_id = param.id;
        }

        var am_type = [
            '<option value="none">无</option>',
            '<option value="dr">淡入</option>',
            '<option value="xz">下坠</option>',
            '<option value="sf">上浮</option>',
            '<option value="zr">左入</option>',
            '<option value="yr">右入</option>'
        ]
        //判断图片是否已有
        var ih = '<img src="' + e_img + '" width="90%" />' +
                        '<span class="tm_bz" style="width: 90%;">重新上传</span>';
        if (imghtml != '' || imghtml == null) {
            ih = imghtml;
        }

        var str = '<dl id="' + e_id + '">' +
                    '<dt>' + ih +
        '</dt>' +
        '<dd>' +
            '<span class="Validform_checktip">宽</span>' +
            '<input type="text" class="input small am_width" value="' + e_wid + '" />' +
            '<span class="Validform_checktip">高</span>' +
            '<input type="text" class="input small am_height" value="' + e_hei + '" />' +
        '</dd>' +
        '<dd>' +
            '<span class="Validform_checktip">横向位置：</span>' +
            '<input type="text" class="input small am_x" value="' + e_x + '" />' +
        '</dd>' +
        '<dd>' +
            '<span class="Validform_checktip">纵向位置：</span>' +
            '<input type="text" class="input small am_y" value="' + e_y + '" />' +
        '</dd>' +
        '<dd>' +
            '<span class="Validform_checktip">动画效果：</span>' +
            '<select>';

        //根据数据选中
        for (var i = 0; i < am_type.length; i++) {
            if (e_tp != '' && am_type[i].indexOf(e_tp) > 0) {
                str += am_type[i].replace('<option', '<option selected="selected" ');
                continue;
            }
            str += am_type[i];
        }

        str += '</select>' +
       '</dd>' +
       '<dd>' +
           '<span class="Validform_checktip">开始时间：</span>' +
           '<input type="text" class="input small" value="' + e_ss + '" />秒' +
       '</dd>' +
       '<dd>' +
           '<span class="Validform_checktip">动画持续：</span>' +
           '<input type="text" class="input small" value="' + e_cs + '" />秒' +
       '</dd><dd class="rem_dl">×</dd></dl>';

        return str;
    }

    //右边数据调整
    rightDl = function () {
        var component = $(".component");
        var os_left = component.offset().left;
        var os_top = component.offset().top;
        var c_width = component.width();
        var c_height = component.height();

        //宽度调整
        $(document).on("blur", ".am_width", function (e) {
            var curWidth = $(this).val();
            var curId = $(this).parent().parent().attr("id");

            var curItem = $("#i_" + curId);
            var width = curWidth;
            var height = width / curItem.width() * curItem.height();

            if (width > min_width && height > min_height && width < max_width && height < max_height) {
                resizeImage(curItem, width, height);
                $(this).siblings(".am_height").val(Math.round(height));
            }
        });

        //高度调整
        $(document).on("blur", ".am_height", function () {
            var curHeight = $(this).val();
            var curId = $(this).parent().parent().attr("id");

            var curItem = $("#i_" + curId);
            var height = curHeight;
            var width = height / curItem.height() * curItem.width();

            if (width > min_width && height > min_height && width < max_width && height < max_height) {
                resizeImage(curItem, width, height);
                $(this).siblings(".am_width").val(Math.round(width));
            }
        })

        //横向坐标调整
        $(document).on("blur", ".am_x", function () {
            var curId = $(this).parent().parent().attr("id");
            var curItem = $("#i_" + curId);

            var cur_x = $(this).val();
            var max_x = c_width - curItem.parent().width();

            if (cur_x <= max_x) {
                var left = parseInt(cur_x) + os_left;
                curItem.parent().offset({ 'left': left });
            }
        });

        //纵向坐标调整
        $(document).on("blur", ".am_y", function () {
            var curId = $(this).parent().parent().attr("id");
            var curItem = $("#i_" + curId);

            var cur_y = $(this).val();
            var max_y = c_height - curItem.parent().height();

            if (cur_y <= max_y) {
                var top = parseInt(cur_y) + os_top;
                curItem.parent().offset({ 'top': top });
            }
        });

        //重新上传图片
        $(document).on("click", ".tm_bz", function () {
            var curId = $(this).parent().parent().attr("id");
            var curItem = $("#i_" + curId);
            $("#upload").click();
        });
        $(document).on("change", "#upload", function () {
            //取消dl鼠标离开事件，以免冲突
            $(document).off("mouseout", ".animate_dl>dl", close_dl);

            var thi = $(this);
            var curImg = $("img.cur");
            //服务器上传实现预览
            window.ajax2("getdata.ashx?myact=uploadImg", {
                type: "POST",
                async: true,
                data: { header_img_id: thi.get(0).files[0] },
                timeout: 10000 * 6,
                callback: function (data) {
                    if (data.res == 1) {
                        $("dl.redbor dt>img").attr("src", data.newPhotoUrl);
                        newimg.src = data.newPhotoUrl;
                        //图片加载成功回调函数
                        newimg.onload = function () {
                            var n_height = curImg.width() / this.width * this.height;
                            curImg.attr("src", data.newPhotoUrl);
                            resizeImage(curImg, curImg.width(), n_height);
                            $(".redbor dd>input.am_width").val(Math.round(curImg.width()));
                            $(".redbor dd>input.am_height").val(Math.round(n_height));
                        }

                        //取消dl鼠标离开事件，以免冲突
                        $(document).on("mouseout", ".animate_dl>dl", close_dl);
                    } else {
                        alert(data.content);
                    }
                },
            });
        });

        //新添加图片
        $(document).on("click", ".newimg", function () {
            $("#newUpl").click();
        });
        $(document).on("change", "#newUpl", function (e) {
            //取消dl鼠标离开事件，以免冲突
            $(document).off("mouseout", ".animate_dl>dl", close_dl);

            //服务器上传实现预览
            window.ajax2("getdata.ashx?myact=uploadImg", {
                type: "POST",
                async: true,
                data: { header_img_id: e.target.files[0] },
                timeout: 10000 * 6,
                callback: function (data) {
                    if (data.res == 1) {
                        var curImg = $('<img class="resize-image" src="' + data.newPhotoUrl + '" alt="" id="i_' + $(".animate_dl>dl").length + '" />');
                        $(".component").append(curImg);
                        //定父级
                        curImg.wrap('<div class="resize-container"></div>')
                        .after('<span class="resize-handle resize-handle-se"></span>');
                        $container = curImg.parent(".resize-container");
                        curImg.attr("src", data.newPhotoUrl);


                        $("dl.redbor dt").html('<img src="' + data.newPhotoUrl + '" width="90%" />' +
                        '<span class="tm_bz" style="width: 90%;">重新上传</span>');

                        newimg.src = data.newPhotoUrl;
                        //图片加载成功回调函数
                        newimg.onload = function () {
                            var n_height = df_width / this.width * this.height;
                            resizeImage(curImg, df_width, n_height);
                            $(".redbor dd>input.am_width").val(Math.round(curImg.width()));
                            $(".redbor dd>input.am_height").val(Math.round(n_height));
                        }

                        $container.on('mousedown touchstart', '.resize-handle', startResize);
                        $container.on('mousedown touchstart', 'img', startMoving);
                        curImg.siblings(".resize-handle").on("mouseover", tabObj);
                        curImg.siblings(".resize-handle").on("mouseout", cancelOver);
                        $(".resize-image").on("mouseover", tabObj);
                        $(".resize-image").on("mouseout", cancelOver);

                        //恢复dl鼠标离开事件
                        $(document).on("mouseout", ".animate_dl>dl", close_dl);
                    } else {
                        alert(data.content);
                    }
                },
            });
        });

        //dl悬浮触发
        $(document).on("mouseover", ".animate_dl>dl", open_dl);

        //dl浮开触发
        $(document).on("mouseout", ".animate_dl>dl", close_dl);
    }

    open_dl = function () {
        $(this).addClass("redbor");
        $("#i_" + this.id).addClass("cur").parent().siblings().find(".resize-image").removeClass("cur");
    }
    close_dl = function () {
        $(this).removeClass("redbor");
    }

    //切换焦点对象
    tabObj = function (e) {
        thi = e.target;
        if (thi.localName != 'img')//span
            image_target = $(thi).siblings(".resize-image:first").get(0);
        else                       //img
            image_target = thi;

        orig_src.src = image_target.src;

        try {
            $("#" + $(image_target).attr("id").split('_')[1]).addClass("redbor");
            //显示删除按钮
            $("dd.rem_dl").show().parent().siblings().find("dd.rem_dl").hide();
        } catch (e) { }

        $(image_target).addClass("cur").parent().siblings().find(".resize-image").removeClass("cur");
        $container = $(image_target).parent(".resize-container");
    }

    //取消悬浮
    cancelOver = function (e) {
        thi = e.target;
        var dlid;
        if (thi.localName != 'img')//span 
            dlid = $(thi).siblings("img.resize-image").attr("id").split('_')[1];
        else
            dlid = thi.id.split('_')[1];

        $("#" + dlid).removeClass("redbor");
        //隐藏删除按钮
        $("dd.rem_dl").hide();
    }

    //就绪等比例缩放
    startResize = function (e) {
        //取消其他图片的悬浮事件，以免冲突
        $(this).parent().siblings().children(".resize-image").off("mouseover");

        e.preventDefault();
        e.stopPropagation();
        saveEventState(e);

        $(document).on('mousemove touchmove', resizing);
        $(document).on('mouseup touchend', endResize);
    };

    //结束等比例缩放 
    endResize = function (e) {
        //重新给其他图片焦点
        $(".resize-image").on("mouseover", tabObj);

        e.preventDefault();
        $(document).off('mouseup touchend', endResize);
        $(document).off('mousemove touchmove', resizing);

        //右边显示详情
        toDl();
    };

    //保存事件状态
    saveEventState = function (e) {
        // Save the initial event details and container state
        event_state.container_width = $container.width();
        event_state.container_height = $container.height();
        event_state.container_left = $container.offset().left;
        event_state.container_top = $container.offset().top;
        event_state.mouse_x = (e.clientX || e.pageX || e.originalEvent.touches[0].clientX) + $(window).scrollLeft();
        event_state.mouse_y = (e.clientY || e.pageY || e.originalEvent.touches[0].clientY) + $(window).scrollTop();

        // This is a fix for mobile safari
        // For some reason it does not allow a direct copy of the touches property
        if (typeof e.originalEvent.touches !== 'undefined') {
            event_state.touches = [];
            $.each(e.originalEvent.touches, function (i, ob) {
                event_state.touches[i] = {};
                event_state.touches[i].clientX = 0 + ob.clientX;
                event_state.touches[i].clientY = 0 + ob.clientY;
            });
        }
        event_state.evnt = e;
    };

    //图片等比例缩放
    resizing = function (e) {

        var mouse = {}, width, height, left, top, offset = $container.offset();
        mouse.x = (e.clientX || e.pageX || e.originalEvent.touches[0].clientX) + $(window).scrollLeft();
        mouse.y = (e.clientY || e.pageY || e.originalEvent.touches[0].clientY) + $(window).scrollTop();

        // Position image differently depending on the corner dragged and constraints
        //右下角
        if ($(event_state.evnt.target).hasClass('resize-handle-se')) {
            width = mouse.x - event_state.container_left;
            height = mouse.y - event_state.container_top;
            left = event_state.container_left;
            top = event_state.container_top;
        }
        //else if ($(event_state.evnt.target).hasClass('resize-handle-sw')) {
        //    width = event_state.container_width - (mouse.x - event_state.container_left);
        //    height = mouse.y - event_state.container_top;
        //    left = mouse.x;
        //    top = event_state.container_top;
        //} else if ($(event_state.evnt.target).hasClass('resize-handle-nw')) {
        //    width = event_state.container_width - (mouse.x - event_state.container_left);
        //    height = event_state.container_height - (mouse.y - event_state.container_top);
        //    left = mouse.x;
        //    top = mouse.y;
        //    if (constrain || e.shiftKey) {
        //        top = mouse.y - ((width / orig_src.width * orig_src.height) - height);
        //    }
        //} else if ($(event_state.evnt.target).hasClass('resize-handle-ne')) {
        //    width = mouse.x - event_state.container_left;
        //    height = event_state.container_height - (mouse.y - event_state.container_top);
        //    left = event_state.container_left;
        //    top = mouse.y;
        //    if (constrain || e.shiftKey) {
        //        top = mouse.y - ((width / orig_src.width * orig_src.height) - height);
        //    }
        //}


        // Optionally maintain aspect ratio
        //按住shift键图片等比例缩放
        //if (constrain || e.shiftKey) { 
        height = width / orig_src.width * orig_src.height;
        // console.log("shift");
        //} 

        if (width > min_width && height > min_height && width < max_width && height < max_height) {
            // To improve performance you might limit how often resizeImage() is called
            resizeImage($(image_target), width, height);
            // Without this Firefox will not re-calculate the the image dimensions until drag end
            $container.offset({ 'left': left, 'top': top });
        }
    }

    resizeImage = function ($img, width, height) {

        //resize_canvas.width = width;
        //resize_canvas.height = height;
        //resize_canvas.getContext('2d').drawImage(i_src, 0, 0, width, height);
        //$img.attr('src', resize_canvas.toDataURL("image/png"));

        $img.css({ width: width, height: height });
    };

    //等待移动
    startMoving = function (e) {

        //取消其他图片的悬浮事件，以免冲突
        $(this).parent().siblings().children(".resize-image").off("mouseover");
        //取消右下角悬浮
        $(".resize-handle").off("mouseover");

        e.preventDefault();
        e.stopPropagation();
        saveEventState(e);

        $(document).on('mousemove touchmove', moving);
        $(document).on('mouseup touchend', endMoving);
    };

    //结束图片拖放移动
    endMoving = function (e) {
        //恢复其他图片焦点
        $(".resize-image").on("mouseover", tabObj);
        //恢复右下角悬浮事件
        $(".resize-handle").on("mouseover", tabObj);

        e.preventDefault();
        e.stopPropagation();
        $(document).off('mouseup touchend', endMoving);//结束事件
        $(document).off('mousemove touchmove', moving);

        //右边显示详情
        toDl();
    };

    //将图片详情显示在右边
    toDl = function () {
        var $curimg = $("img.cur:first"); //当前操作的图片
        var $waiBox = $("div.component");
        var x = $curimg.offset().left - $waiBox.offset().left;
        var y = $curimg.offset().top - $waiBox.offset().top;
        try {
            var imgid = $curimg.attr("id").split('_')[1];
            var dl = $("#" + imgid);
            dl.find(".am_x").val(x);
            dl.find(".am_y").val(y);
            dl.find(".am_height").val($curimg.height());
            dl.find(".am_width").val($curimg.width());
        } catch (e) {

        }
    }

    //控制拖动范围
    getXY = function (x_mouse, y_mouse) {

        var cot_height = $(".component").height();
        var cot_widht = $(".component").width();

        //可移动min,max的x距离
        if (x_mouse < cot_left)
            x_mouse = cot_left;
        if (x_mouse > (cot_left + cot_widht - image_target.width))
            x_mouse = cot_left + cot_widht - image_target.width;

        if (y_mouse < cot_top)
            y_mouse = cot_top;
        if (y_mouse > (cot_top + cot_height - image_target.height))
            y_mouse = cot_top + cot_height - image_target.height;

        $container.offset({
            'left': x_mouse,
            'top': y_mouse
        });
    }

    //图片拖动
    moving = function (e) {
        var mouse = {}, touches;
        e.preventDefault(); //阻止元素默认事件
        e.stopPropagation();//防止事件冒泡

        touches = e.originalEvent.touches;

        mouse.x = (e.clientX || e.pageX || touches[0].clientX) + $(window).scrollLeft();
        mouse.y = (e.clientY || e.pageY || touches[0].clientY) + $(window).scrollTop();

        var x_mouse = (mouse.x - (event_state.mouse_x - event_state.container_left));
        var y_mouse = (mouse.y - (event_state.mouse_y - event_state.container_top));

        //控制图片拖动范围，并移动
        getXY(x_mouse, y_mouse);

        // Watch for pinch zoom gesture while moving
        if (event_state.touches && event_state.touches.length > 1 && touches.length > 1) {
            var width = event_state.container_width, height = event_state.container_height;
            var a = event_state.touches[0].clientX - event_state.touches[1].clientX;
            a = a * a;
            var b = event_state.touches[0].clientY - event_state.touches[1].clientY;
            b = b * b;
            var dist1 = Math.sqrt(a + b);

            a = e.originalEvent.touches[0].clientX - touches[1].clientX;
            a = a * a;
            b = e.originalEvent.touches[0].clientY - touches[1].clientY;
            b = b * b;
            var dist2 = Math.sqrt(a + b);

            var ratio = dist2 / dist1;

            width = width * ratio;
            height = height * ratio;
            // To improve performance you might limit how often resizeImage() is called
            resizeImage($(image_target), width, height);
        }
    };

    init();
};

// Kick everything off with the target image
resizeableImage();