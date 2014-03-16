$(function () {
    Layout();
    GetUserMenu();
    Clock();
});

//设置主区域的高度
function Layout() {
    SetContentHeight();
    $(window).resize(SetContentHeight);
    function SetContentHeight() {
        var windowHeight = $(window).height();
        var fixHeight = 122;
        $("#Content").height(windowHeight - fixHeight);
    }
}

function ResizeNav() {
    SetSubMenuHeight();
    $(window).resize(SetSubMenuHeight);
    function SetSubMenuHeight() {
        var accordion_head = $('.accordion > li > a');
        var accordion_body = $('.accordion li > .sub-menu');
        var fixHeight = 19;//.sub-menu的padding值
        $(".sub-menu").css('height', $(".navigation").height() - fixHeight - accordion_head.length * accordion_head.height() - accordion_head.length + 'px');
        accordion_body.hide();
        accordion_head.first().addClass('active').next().slideDown('normal');
        accordion_head.on('click', function (event) {
            event.preventDefault();
            if ($(this).attr('class') != 'active') {
                accordion_body.slideUp('normal');
                $(this).next().stop(true, true).slideToggle('normal');
                accordion_head.removeClass('active');
                $(this).addClass('active');
            }
        });
    }
}

//获取菜单
function GetUserMenu() {
    $.ajax({
        url: "Services/UserService.asmx/InitUserMenu",
        dataType: "json",
        type: "post",
        async: false,
        success: function (result) {
            if (result.IsSuccess && result.Data != null) {
                var html = "";
                $.each(result.Data, function (index, menu) {
                    if (index == 0) {
                        html += ' <li><a style="border-top: 0px;" ><img src="Image/' + menu.Image + '" alt="" width="32px" height="32px">' + menu.Caption + '</a>'
                    }
                    else {
                        html += ' <li><a><img src="Image/' + menu.Image + '" alt="" width="32px" height="32px">' + menu.Caption + '</a>'
                    }

                    html += TranslateSubMenu(menu.SubMenu);
                    html += '</li>'
                });
                $(".accordion").empty().html(html);

                //事件注册
                $(".sub-menu div").click(function () {
                    $('.sub-menu div').removeClass("selected")
                    $(this).addClass("selected");
                }).hover(function () {
                    $(this).addClass("hover");
                }, function () {
                    $(this).removeClass("hover");
                });

                //SubMenu高度设置
                ResizeNav();
            }
            else {
                alert(result.Message);
            }
        },
        error: function (a, b, c) {
            alert("系统错误！");
        }
    });
}

function TranslateSubMenu(subMenu) {
    var html = '<div class="sub-menu">';
    $.each(subMenu, function (i, m) {
        html += "<div onclick=\"AddTab('" + m.MenuId + "','" + m.Url + "','" + m.Caption + "','" + m.Image + "', true);\">";
        html += '<img src="Image/' + m.Image + '" alt="" width="32px" height="32px" />' + m.Caption + '</div>'
    });
    html += '</div>'
    return html;
}

//添加tab
function AddTab(tabid, url, name, img, isClose) {
    if (url == undefined && url == null) {
        return;
    }

    var tabs_container = $("#tabs");
    var frame_container = $("#Frame_Container");
    if (document.getElementById("tab_" + tabid) == null) {
        tabs_container.find("li").removeClass("selected");
        frame_container.find("iframe").hide();

        //增加Tab
        if (isClose) {
            tabs_container.append('<li id="tab_' + tabid + '" class="selected" ><span title="' + name + '" onclick="' + "AddTab('" + tabid + "','" + url + "','" + name + "','" + img + "', true);" + '"><img src="Image/' + img + '" alt="" />' + name + '</span><a class="tab-close" onclick="RemoveTab(\'' + tabid + '\')"></a></li>');
        }
        else {
            tabs_container.append('<li id="tab_' + tabid + '" class="selected" ><span title="' + name + '" onclick="' + "AddTab('" + tabid + "','" + url + "','" + name + "','" + img + "', true);" + '"><img src="Image/' + img + '" alt="" />' + name + '</span></li>');
        }

        //增加页面
        frame_container.append('<iframe id="iframe_' + tabid + '" name="iframe_' + tabid + '" height="100%" width="100%" src="' + url + '" frameBorder="0"></iframe>');
    }
    else {
        tabs_container.find("li").removeClass("selected");
        frame_container.find("iframe").hide().find("#iframe_" + tabid).show();
        tabs_container.find("#tab_" + tabid).addClass("selected");
        frame_container.find("#iframe_" + tabid).show();
    }
}

//删除tab 
function RemoveTab(tabid) {
    var tabs_container = $("#tabs");
    var frame_container = $("#Frame_Container");

    tabs_container.find("#tab_" + tabid).remove();
    frame_container.find("#iframe_" + tabid).remove();

    tabs_container.find("li").last().addClass("selected");
    tabs_container.find("li").last().show();
}

//时钟
function Clock() {
    var now = new Date();
    var year = now.getFullYear();
    var month = now.getMonth();
    var date = now.getDate();
    var day = now.getDay();
    var hour = now.getHours();
    var minu = now.getMinutes();
    var sec = now.getSeconds();
    var week;
    month = month + 1;
    if (month < 10) month = "0" + month;
    if (date < 10) date = "0" + date;
    if (hour < 10) hour = "0" + hour;
    if (minu < 10) minu = "0" + minu;
    if (sec < 10) sec = "0" + sec;
    var arr_week = new Array("星期日", "星期一", "星期二", "星期三", "星期四", "星期五", "星期六");
    week = arr_week[day];
    var time = "";
    time = year + "年" + month + "月" + date + "日" + " " + hour + ":" + minu + ":" + sec;

    $("#datetime").text(time);
    var timer = setTimeout("Clock()", 1000);
}