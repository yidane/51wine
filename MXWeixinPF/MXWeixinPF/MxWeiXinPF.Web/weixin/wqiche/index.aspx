<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="MxWeiXinPF.Web.weixin.wqiche.index" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
    <title></title>
    <base href="." />
    <meta name="viewport" content="width=device-width,height=device-height,inital-scale=1.0,maximum-scale=1.0,user-scalable=no;" />
    <meta name="apple-mobile-web-app-capable" content="yes" />
    <meta name="apple-mobile-web-app-status-bar-style" content="black" />
    <meta name="format-detection" content="telephone=no" />
    <link href="css/cate23_0.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" href="css/reset.css" media="all" />
    <link rel="stylesheet" type="text/css" href="css/font-awesome.css" media="all" />
    <script type="text/javascript" src="js/maivl.js"></script>
    <script type="text/javascript" src="js/jQuery.js"></script>
    <script type="text/javascript" src="js/zepto.js"></script>
    <script type="text/javascript" src="js/swipe.js"></script>
</head>
<body onselectstart="return true;" ondragstart="return false;">
    <!--背景音乐-->
    <div class="body">
        <!--
    幻灯片管理
    -->
        <div style="-webkit-transform: translate3d(0,0,0);">
            <div id="banner_box" class="box_swipe" style="visibility: visible;">
                <ul style="list-style: none; width: 0px; transition: 0ms; -webkit-transition: 0ms; -webkit-transform: translate3d(0px, 0, 0);">
                    <asp:Repeater ID="imgList" runat="server">
                        <ItemTemplate>
                            <li style="width: 640px; display: table-cell; vertical-align: top;">
                                <a href="javascript:void(0)">
                                    <img src="<%#Eval("img_url") %>" style="width: 100%; max-height:200px;" />
                                </a>
                            </li>
                        </ItemTemplate>
                    </asp:Repeater>
                </ul>
                <ol>
                    <asp:Repeater ID="rptDian" runat="server">
                        <ItemTemplate>
                            <li class="<%#Container.ItemIndex==0?"on":"" %>"></li>
                        </ItemTemplate>
                    </asp:Repeater>
                </ol>
            </div>
        </div>
        <script>
            $(function () {
                new Swipe(document.getElementById('banner_box'), {
                    speed: 500,
                    auto: 3000,
                    callback: function () {
                        var lis = $(this.element).next("ol").children();
                        lis.removeClass("on").eq(this.index).addClass("on");
                    }
                });
            });
        </script>
        <br>
        <header>
            <div class="snower">
                <script type="text/javascript"></script>
            </div>
        </header>
        <section>
            <nav>
                <ul class="nav_links box">
                    <li><a href="cxXinshang.aspx?wid=<%=wid %>&openid=<%=openid %>">车型欣赏</a></li>
                    <li><a href="aboutwe.aspx?wid=<%=wid %>&openid=<%=openid %>">关于我们</a></li>
                    <li><a href="qjtList.aspx?wid=<%=wid %>&openid=<%=openid %>">全景看车</a></li>
                    <li><a href="syGongju.aspx?wid=<%=wid %>&openid=<%=openid %>">实用工具</a></li>
                </ul>
            </nav>
            <div>
                <ul class="ofh ul_list">
                    <asp:Literal ID="litMenu" runat="server"></asp:Literal>
                    <li><a href="yhcx.aspx?wid=<%=wid %>&openid=<%=openid %>" style="background-image: url(img/7.png); background-size: 100% 100%;">
                        <label>优惠促销</label>
                    </a></li>
                    <li><a href="zxdt.aspx?wid=<%=wid %>&openid=<%=openid %>" style="background-image: url(img/14.png); background-size: 100% 100%;">
                        <label>咨询动态</label>
                    </a></li>
                    <li><a href="ershouche.aspx?wid=<%=wid %>&openid=<%=openid %>" style="background-image: url(img/15.png); background-size: 100% 100%;">
                        <label>尊选二手车</label>
                    </a></li>
                    <ol style="background-image: url();"></ol>
                </ul>
            </div>
        </section>

        <%--<div class="copyright" style="text-align: center; padding: 10px 0">
            版权归我所有
        </div>--%>
    </div>
    <script src="js/jquery.min.js" type="text/javascript"></script>
    <script>
        function displayit(n) {
            for (i = 0; i < 4; i++) {
                if (i == n) {
                    var id = 'menu_list' + n;
                    if (document.getElementById(id).style.display == 'none') {
                        document.getElementById(id).style.display = '';
                        document.getElementById("plug-wrap").style.display = '';
                    } else {
                        document.getElementById(id).style.display = 'none';
                        document.getElementById("plug-wrap").style.display = 'none';
                    }
                } else {
                    if ($('#menu_list' + i)) {
                        $('#menu_list' + i).css('display', 'none');
                    }
                }
            }
        }
        function closeall() {
            var count = document.getElementById("top_menu").getElementsByTagName("ul").length;
            for (i = 0; i < count; i++) {
                document.getElementById("top_menu").getElementsByTagName("ul").item(i).style.display = 'none';
            }
            document.getElementById("plug-wrap").style.display = 'none';
        }

        document.addEventListener('WeixinJSBridgeReady', function onBridgeReady() {
            WeixinJSBridge.call('hideToolbar');
        });
    </script>
    <!-- share -->

    <script type="text/javascript">
        window.shareData = {
            "moduleName": "Index",
            "moduleID": "0",
            "imgUrl": "http://42.96.196.48/tpl/static/attachment/focus/default/../wedding/9.jpg",
            "timeLineLink": "http://42.96.196.48/index.php?g=Wap&m=Car&a=index&token=agpkhy1417835915",
            "sendFriendLink": "http://42.96.196.48/index.php?g=Wap&m=Car&a=index&token=agpkhy1417835915",
            "weiboLink": "http://42.96.196.48/index.php?g=Wap&m=Car&a=index&token=agpkhy1417835915",
            "tTitle": "你好，首页",
            "tContent": "你好，首页"
        };
    </script>

    <script>
        window.shareData.sendFriendLink = window.shareData.sendFriendLink.replace('http://42.96.196.48', 'http://42.96.196.48');
        document.addEventListener('WeixinJSBridgeReady', function onBridgeReady() {
            WeixinJSBridge.on('menu:share:appmessage', function (argv) {
                shareHandle('friend');
                WeixinJSBridge.invoke('sendAppMessage', {
                    "img_url": window.shareData.imgUrl,
                    "img_width": "640",
                    "img_height": "640",
                    "link": window.shareData.sendFriendLink,
                    "desc": window.shareData.tContent,
                    "title": window.shareData.tTitle
                }, function (res) {
                    _report('send_msg', res.err_msg);
                })
            });

            WeixinJSBridge.on('menu:share:timeline', function (argv) {
                shareHandle('frineds');
                WeixinJSBridge.invoke('shareTimeline', {
                    "img_url": window.shareData.imgUrl,
                    "img_width": "640",
                    "img_height": "640",
                    "link": window.shareData.sendFriendLink,
                    "desc": window.shareData.tContent,
                    "title": window.shareData.tTitle
                }, function (res) {
                    _report('timeline', res.err_msg);
                });
            });

            WeixinJSBridge.on('menu:share:weibo', function (argv) {
                shareHandle('weibo');
                WeixinJSBridge.invoke('shareWeibo', {
                    "content": window.shareData.tContent,
                    "url": window.shareData.sendFriendLink,
                }, function (res) {
                    _report('weibo', res.err_msg);
                });
            });
        }, false)

        function shareHandle(to) {
            var submitData = {
                module: window.shareData.moduleName,
                moduleid: window.shareData.moduleID,
                token: 'agpkhy1417835915',
                wecha_id: 'okiB6uA0hnixn13MUDRzxQNR9zaY',
                url: window.shareData.sendFriendLink,
                to: to
            };
            $.post('/index.php?g=Wap&m=Share&a=shareData&token=agpkhy1417835915&wecha_id=okiB6uA0hnixn13MUDRzxQNR9zaY', submitData, function (data) { }, 'json')
        }
    </script>
</body>
</html>
