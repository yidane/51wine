<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="WeiXinPF.Web.admin.index" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>后台管理中心 版本号：<%=WeiXinPF.Common.MXKeys.ASSEMBLY_VERSION %></title>
    <link href="../scripts/bootstrap/css/bootstrap.min.css" />
    <link href="skin/default/style.css" rel="stylesheet" type="text/css" />
    <link href="../css/font-awesome/css/font-awesome.min.css" rel="stylesheet" />
    <link href="skin/notify.css" rel="stylesheet" />

    <script type="text/javascript" src="../scripts/jquery/jquery-1.10.2.min.js"></script>
    <script type="text/javascript" src="../scripts/jquery/jquery.nicescroll.js"></script>
    <script type="text/javascript" src="../scripts/lhgdialog/lhgdialog.js?skin=idialog"></script>
    <script type="text/javascript" src="../scripts/bootstrap/js/bootstrap.min.js"></script>

    <script type="text/javascript" src="js/layout.js"></script>

    <script type="text/javascript">
        //页面加载完成时
        $(function () {
            //检测IE
            if ('undefined' == typeof (document.body.style.maxHeight)) {
                window.location.href = 'ie6update.html';
            }
            loadMenuTree(true); //加载管理首页左边导航菜单
        });

        //页面尺寸改变时
        $(window).resize(function () {
            navresize();
        });
        //加载管理首页左边导航菜单
        function loadMenuTree(_islink) {
            //判断是否跳转链接
            var islink = false;
            if (arguments.length == 1 && _islink) {
                islink = true;
            }
            //发送AJAX请求
            $.ajax({
                type: "post",
                url: "../tools/admin_ajax.ashx?action=get_navigation_list&time=" + Math.random(),
                dataType: "html",
                success: function (data, textStatus) {
                    //将得到的数据插件到页面中
                    //alert(data);
                    $("#sidebar-nav .list-box").html(data);
                    $("#pop-menu .list-box").html(data);
                    //初始化导航菜单
                    initMenuTree(islink);
                    initPopMenuTree();
                    //设置左边导航滚动条
                    $("#sidebar-nav").niceScroll({ touchbehavior: false, cursorcolor: "#7C7C7C", cursoropacitymax: 0.6, cursorwidth: 5 });
                    $("#pop-menu .list-box").niceScroll({ touchbehavior: false, cursorcolor: "#7C7C7C", cursoropacitymax: 0.6, cursorwidth: 5 });
                    $("#pop-menu .list-box").getNiceScroll().hide();
                    //设置主导航菜单显示和隐藏
                    navresize();
                }
            });
        }
        //初始化导航菜单
        function initMenuTree(islink) {
            //先清空NAV菜单内容
            $("#nav").html('');
            $("#sidebar-nav .list-box .list-group").each(function (i) {
                //添加菜单导航
                var navHtml = $('<li><i class="icon-' + i + '"></i><span>' + $(this).attr("name") + '</span></li>').appendTo($("#nav"));
                //默认选中第一项
                if (i == 0) {
                    $(this).show();
                    navHtml.addClass("selected");
                }
                //为菜单添加事件
                navHtml.click(function () {
                    $("#nav li").removeClass("selected");
                    $(this).addClass("selected");
                    $("#sidebar-nav .list-box .list-group").hide();
                    $("#sidebar-nav .list-box .list-group").eq($("#nav li").index($(this))).show();
                });
                //为H2添加事件
                $(this).children("h2").click(function () {
                    if ($(this).next("ul").css('display') != 'none') {
                        return false;
                    }
                    $(this).siblings("ul").slideUp(300);
                    $(this).next("ul").slideDown(300);
                    //展开第一个菜单1—e2124dd04e11d0-1b9df2865f85944-be 
                    if ($(this).next("ul").children("li").first().children("ul").length > 0) {
                        //$(this).next("ul").children("li").first().children("a").children(".expandable").last().removeClass("close");
                        //$(this).next("ul").children("li").first().children("a").children(".expandable").last().addClass("open");
                        //$(this).next("ul").children("li").first().children("ul").first().show();
                    }
                });

                //首先隐藏所有的UL
                $(this).find("ul").hide();
                //绑定树菜单事件.开始
                $(this).find("ul").each(function (j) { //遍历所有的UL
                    //遍历UL第一层LI
                    $(this).children("li").each(function () {
                        liObj = $(this);
                        //插入选中的三角
                        var spanObj = liObj.children("a").children("span");
                        $('<div class="arrow"></div>').insertBefore(spanObj); //插入到span前面
                        //判断是否有子菜单和设置距左距离
                        var parentExpandableLen = liObj.parent().parent().children("a").children(".expandable").length; //父节点的左距离
                        if (liObj.children("ul").length > 0) { //如果有下级菜单
                            //删除链接，防止跳转
                            liObj.children("a").removeAttr("href");
                            //更改样式
                            liObj.children("a").addClass("pack");
                            //设置左距离
                            var lastExpandableObj;
                            for (var n = 0; n <= parentExpandableLen; n++) { //注意<=
                                lastExpandableObj = $('<div class="expandable"></div>').insertBefore(spanObj); //插入到span前面
                            }
                            //设置最后一个为闭合+号
                            lastExpandableObj.addClass("close");
                            //创建和设置文件夹图标
                            $('<div class="folder close"></div>').insertBefore(spanObj); //插入到span前面
                            //隐藏下级的UL
                            liObj.children("ul").hide();
                            //绑定单击事件
                            liObj.children("a").click(function () {
                                //搜索所有同级LI且有子菜单的左距离图标为+号及隐藏子菜单  1e2124dd04e11--d01b9df2865f85944be 
                                $(this).parent().siblings().each(function () {
                                    //alert($(this).html());
                                    if ($(this).children("ul").length > 0) {
                                        //设置自身的左距离图标为+号
                                        $(this).children("a").children(".expandable").last().removeClass("open");
                                        $(this).children("a").children(".expandable").last().addClass("close");
                                        //隐藏自身子菜单
                                        $(this).children("ul").slideUp(300);
                                    }
                                });

                                //设置自身的左距离图标为-号
                                $(this).children(".expandable").last().removeClass("close");
                                $(this).children(".expandable").last().addClass("open");
                                //显示自身父节点的UL子菜单
                                $(this).parent().children("ul").slideDown(300);
                            });
                        } else {
                            //设置左距离
                            for (var n = 0; n < parentExpandableLen; n++) {
                                $('<div class="expandable"></div>').insertBefore(spanObj); //插入到span前面
                            }
                            //创建和设置文件夹图标
                            $('<div class="folder open"></div>').insertBefore(spanObj); //插入到span前面
                            //绑定单击事件
                            liObj.children("a").click(function () {
                                //删除所有的选中样式
                                $("#sidebar-nav .list-box .list-group ul li a").removeClass("selected");
                                //自身添加样式
                                $(this).addClass("selected");
                                //保存到cookie
                                addCookie("dt_manage_navigation_cookie", $(this).attr("navid"), 240);
                            });
                        }
                    });
                    //显示第一个UL
                    if (j == 0) {
                        $(this).show();
                        //展开第一个菜单
                        if ($(this).children("li").first().children("ul").length > 0) {
                            $(this).children("li").first().children("a").children(".expandable").last().removeClass("close");
                            $(this).children("li").first().children("a").children(".expandable").last().addClass("open");
                            $(this).children("li").first().children("ul").show();
                        }
                    }
                });
                //绑定树菜单事件.结束
            });
            //定位或跳转到相应的菜单
            linkMenuTree(islink);
        }
        //定位或跳转到相应的菜单
        function linkMenuTree(islink, navid) {
            //读取Cookie,如果存在该ID则定位到对应的导航
            var cookieObj;
            var argument = arguments.length;
            if (argument == 2) {
                cookieObj = $("#sidebar-nav").find('a[navid="' + navid + '"]');
            } else {
                cookieObj = $("#sidebar-nav").find('a[navid="' + getCookie("dt_manage_navigation_cookie") + '"]');
            }
            if (cookieObj.length > 0) {
                //显示所在的导航和组
                var indexNum = $("#sidebar-nav .list-box .list-group").index(cookieObj.parents(".list-group"));
                $("#nav li").removeClass("selected");
                $("#nav li").eq(indexNum).addClass("selected");
                cookieObj.parents(".list-group").siblings().hide();
                cookieObj.parents(".list-group").show();
                //遍历所有的LI父节点
                cookieObj.parents("li").each(function () {
                    //搜索所有同级LI且有子菜单的左距离图标为+号及隐藏子菜单
                    $(this).siblings().each(function () {
                        if ($(this).children("ul").length > 0) {
                            //设置自身的左距离图标为+号
                            $(this).children("a").children(".expandable").last().removeClass("open");
                            $(this).children("a").children(".expandable").last().addClass("close");
                            //隐藏自身子菜单
                            $(this).children("ul").hide();
                        }
                    });
                    //设置自身的左距离图标为-号
                    if ($(this).children("ul").length > 0) {
                        $(this).children("a").children(".expandable").last().removeClass("close");
                        $(this).children("a").children(".expandable").last().addClass("open");
                    }
                    //显示自身的UL
                    $(this).children("ul").show();
                });
                //显示最后一个父节点UL，隐藏兄弟UL
                cookieObj.parents("ul").eq(-1).show();
                cookieObj.parents("ul").eq(-1).siblings("ul").hide();
                //删除所有的选中样式
                $("#sidebar-nav .list-box .list-group ul li a").removeClass("selected");
                //自身添加样式
                cookieObj.addClass("selected");
                //检查是否需要保存到cookie
                if (argument == 2) {
                    addCookie("dt_manage_navigation_cookie", navid, 240);
                }
                //检查是否需要跳转链接
                if (islink == true) {
                    frames["mainframe"].location.href = cookieObj.attr("href");
                }
            } else if (argument == 2) {
                //删除所有的选中样式
                $("#sidebar-nav .list-box .list-group ul li a").removeClass("selected");
                //保存到cookie
                addCookie("dt_manage_navigation_cookie", "", 240);
            }
        }
        //初始化快捷导航菜单
        function initPopMenuTree() {
            var divWidth = $("#pop-menu .list-box .list-group").length * $("#pop-menu .list-box .list-group").outerWidth();
            var divHeight = $(window).height() * 0.6;
            //如果计算的宽度大于浏览器当前窗口可视宽度
            if (divWidth > ($(window).width() - 60)) {
                divWidth = $(window).width() - 60;
            }
            //计算实际的高度
            var groupHeight = 0;
            $("#pop-menu .list-box .list-group").each(function () {
                if ($(this).height() > groupHeight) {
                    groupHeight = $(this).height();
                }
            });
            if (divHeight > groupHeight) {
                divHeight = groupHeight;
            }
            $("#pop-menu .list-box .list-group").height(groupHeight);
            $("#pop-menu .pop-box").width(divWidth);
            $("#pop-menu .pop-box").height(divHeight);

            //遍历及加载事件
            $("#pop-menu .pop-box .list-box li").each(function () {
                var linkObj = $(this).children("a");
                linkObj.removeAttr("href");
                if ($(this).children("ul").length > 0) { //如果无下级菜单
                    linkObj.addClass("nolink");
                } else {
                    linkObj.addClass("link");
                    linkObj.click(function () {
                        linkMenuTree(true, linkObj.attr("navid")); //加载函数
                    });
                }
            });
        }
        //快捷菜单的显示与隐藏
        function triggerMenu(isShow) {
            if (isShow) {
                $("#pop-menu .list-box").getNiceScroll().show();
                $("#pop-menu").css("visibility", "visible");
            } else {
                $("#pop-menu .list-box").getNiceScroll().hide();
                $("#pop-menu").css("visibility", "hidden");
            }
        }
        //导航菜单显示和隐藏
        function navresize() {
            var docWidth = $(document).width();
            if (docWidth < 1004) {
                $(".nav li span").hide();
            } else {
                $(".nav li span").show();
            }
        }

        var timer;
        //点击消息弹窗
        function openmsg() {
            var count = $("#msg_count").text();
            if (count && count > 0) {
                $.dialog({
                    id: 'msg',
                    init: function () {
                        var that = this, i = 10;
                        var fn = function () {
                            that.title('消息提醒(' + i + '秒后关闭)');
                            !i && that.close();
                            i--;
                        };
                        timer = setInterval(fn, 1000);
                        fn();
                    },
                    close: function () {
                        clearInterval(timer);
                    },
                    //                    title: '消息提醒',
                    content: '喀纳斯山庄于2015/10/17 15:19:00提交商品编号为：1003，名称为：商务标准间的商品请您审核！',
                    width: 310,

                    left: '99.5%',
                    top: '100%',

                    fixed: true,
                    drag: false,
                    min: false,
                    max: false,
                    resize: false,

                    button: [
                            {
                                name: '马上审核',
                                callback: function () {
                                    this.content('你同意了').time(2);
                                    return false;
                                },
                                focus: true
                            }
                    ]
                });
            }
        }
    </script>
</head>

<body class="indexbody skin-blue">
    <form id="form1" runat="server">
        <!--全局菜单-->
        <a class="btn-paograms" onclick="triggerMenu(true);"></a>
        <div id="pop-menu" class="pop-menu">
            <div class="pop-box">
                <h1 class="title"><i></i>导航菜单</h1>
                <i class="close" onclick="triggerMenu(false);">关闭</i>
                <div class="list-box"></div>
            </div>
            <i class="arrow">箭头</i>
        </div>
        <!--/全局菜单-->

        <div class="header main-header">
            <div class="header-box navbar ">
                <a href="index.aspx" id="indexUrl" runat="server"><span class="logo"></span></a>
                <ul id="nav" class="nav"></ul>
                <div class=" navbar-custom-menu">

                    <ul class="nav navbar-nav">
                        <li class="dropdown messages-menu " id="menu_msg">
                            <a href="#" class="dropdown-toggle" data-toggle="dropdown" aria-expanded="false">
                                <i class="fa fa-envelope-o"></i>
                                <span class="label label-warning" data-bind="text: shortmsgCount">0</span>
                            </a>
                            <ul class="dropdown-menu">
                                <li class="header" data-bind="text: '您有' + shortmsgCount() + '条消息提醒'">您有0条消息提醒</li>
                                <li>
                                    <ul class="menu" data-bind="foreach: shotmsgList">
                                        <li>
                                            <a href="#">
                                                <h4>
                                                    <!--ko text:msg.title-->
                                                    <!--/ko-->

                                                    <small><i class="fa fa-clock-o"></i>
                                                        <!--ko text:msg.createTime-->
                                                        <!--/ko-->
                                                    </small>
                                                </h4>
                                                <p>

                                                    <!--ko text:msg.content-->
                                                    <!--/ko-->

                                                    <span style="color: #3c8dbc" data-bind="
    visible: msg.isShowButton,
    click: $parent.shortmsgViewDetail
                                                        , text: msg.buttonText"></span>






                                                </p>
                                            </a>
                                        </li>
                                    </ul>
                                </li>
<%--                                <li class="footer">--%>
<%--                                    <a href="#" data-bind="click: $root.LoadMessage">查看所有消息</a>--%>
<%--                                </li>--%>
                            </ul>
                        </li>

                        <li class="dropdown dropdown-nochild user user-menu">
                            <a href="#" class="dropdown-toggle" data-toggle="dropdown">
                                <i class="user-image"></i>
                                <!--<img src="/images/avatar.png" class="user-image" alt="User Image">-->
                                <span class="hidden-xs">您好，<%=admin_info.real_name %><br />
                                    <%=new WeiXinPF.BLL.manager_role().GetTitle(admin_info.role_id) %></span>
                                <!--<i  class="user-icon-dropdown"></i>-->
                                <i class="user-icon-dropdown fa fa-angle-down fa-2x"></i>
                            </a>
                            <ul class="dropdown-menu">
                                <div class="arrow-background"></div>
                                <div class="arrow"></div>
                                <!-- User image -->
                                <li id="mygzh" runat="server">
                                    <a navid="list_weixin" href="weixin/myweixinlist.aspx" target="mainframe" class="item selected">我的公众帐号
                                    </a>
                                </li>
                                <li><a class="item" href="center.aspx" target="mainframe">管理中心</a></li>
                                <li><a class="item" onclick="linkMenuTree(false, '');" href="manager/manager_pwd.aspx" target="mainframe">修改密码</a></li>
                                <li>
                                    <asp:LinkButton ID="lbtnExit" class="item" runat="server" OnClick="lbtnExit_Click">注销登录</asp:LinkButton></li>

                            </ul>
                        </li>
                        <!--<li>-->
                        <!--<a href="#" data-toggle="control-sidebar">-->
                        <!--<i class="fa fa-angle-down fa-2x"></i></a>-->
                        <!--</li>-->
                    </ul>


                    <%--<div class="dropdown messages-menu">--%>
                    <%--<a href="#" class="dropdown-toggle" title="您有0条新消息" data-toggle="dropdown" aria-expanded="false" onclick="javascript:openmsg()">--%>
                    <%--<i class="fa fa-envelope-o fa-lg"></i>--%>
                    <%--<span id="msg_count" class="label label-danger">1</span>--%>


                    <%--</a>--%>
                    <%----%>
                    <%--</div>--%>
                </div>
            </div>
        </div>

        <div class="main-sidebar">
            <div id="sidebar-nav" class="sidebar-nav">
                <div class="list-box"></div>
            </div>
        </div>

        <div class="main-container">
            <iframe id="mainframe" name="mainframe" frameborder="0" src="center.aspx"></iframe>
        </div>
    </form>
    <script src="../scripts/knockout/knockout-3.3.0.js" type="text/javascript"></script>
    <script src="js/message-vm.js" type="text/javascript"></script>
    <script type="text/javascript">
        var systemMenu;
        var myMessageDetailViewModel;
        $(function () {
            userMenuWidth();


            if ($.isEmptyObject(myMessageDetailViewModel)) {
                myMessageDetailViewModel = new MessageViewModel();
                ko.applyBindings(myMessageDetailViewModel, document.getElementById("menu_msg"));
                myMessageDetailViewModel.loadData();
            }

            
        });

        function userMenuWidth() {
            var liUser = $(".user.user-menu");
            var userWidth = liUser.width() - 21;
            var ulMenu = liUser.find(".dropdown-menu");
            ulMenu.css('right', userWidth / 2 - ulMenu.width() / 2);
            ulMenu.find(".arrow-background").width(userWidth);
            var arrow = ulMenu.find(".arrow");
            arrow.css('right', userWidth / 2 - ulMenu.width() / 2 + arrow.width() / 2);
        }


    </script>
</body>
</html>
