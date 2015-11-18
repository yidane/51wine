<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="center.aspx.cs" Inherits="WeiXinPF.Web.admin.center" %>

<%@ Import Namespace="WeiXinPF.Common" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>管理首页</title>
    <%--        <link href="skin/default/style.css" rel="stylesheet" type="text/css" />--%>
    <link href="skin/base.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../scripts/jquery/jquery-1.10.2.min.js"></script>
    <script type="text/javascript" src="js/layout.js"></script>

    <link href="../scripts/bootstrap/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../css/font-awesome/css/font-awesome.min.css" rel="stylesheet" />
    <style type="text/css">
        .ol-decimal {
            list-style-type: decimal;
        }

        .location {
            padding-bottom: 9px;
            border-bottom: solid 1px #e1e1e1;
            height: 32px;
            line-height: 24px;
            font-size: 12px;
            color: #333;
        }

            .location a {
                display: inline-block;
                color: #333;
                text-decoration: none;
            }

                .location a:hover {
                    color: #0065D9;
                    text-decoration: none;
                }

                .location a i {
                    display: inline-block;
                    margin-right: 5px;
                    width: 14px;
                    height: 14px;
                    text-indent: -999em;
                    background: url(skin/default/skin_icons.png) no-repeat;
                    vertical-align: middle;
                    *text-indent: 0; /*float:left;*margin-top:5px;*/
                }

                .location a.back {
                    margin-right: 15px;
                }

                    .location a.back i {
                        background-position: 0 0;
                    }

                .location a.home i {
                    background-position: -28px 0;
                }

            .location span {
                display: inline-block;
                vertical-align: middle;
            }

            .location .arrow {
                display: inline-block;
                margin: auto 3px;
                width: 14px;
                height: 14px;
                background: url(skin/default/skin_icons.png) no-repeat -56px 0;
                vertical-align: middle;
            }

        .nlist-1 {
            line-height: 45px;
            color: #444;
            font-size: 12px;
        }

            .nlist-1 ul {
                padding: 0 0 0 20px;
            }

                .nlist-1 ul:after {
                    clear: both;
                    content: ".";
                    display: block;
                    height: 0;
                    visibility: hidden;
                }

                .nlist-1 ul li {
                    float: left;
                    margin-right: 10px;
                    width: 32%;
                    overflow: hidden;
                    white-space: nowrap;
                    text-overflow: ellipsis;
                }


        .text-indet {
            padding-left: 25px;
        }
    </style>
    <style>
        /*metro*/
        .tile {
            width: 150px;
            height: 150px;
            /*display: block;*/
            float: left;
            margin: 5px;
            /*background-color: #eeeeee;*/
            box-shadow: inset 0 0 1px #FFFFCC;
            /*cursor: pointer;*/
            position: relative;
            overflow: hidden;
            -webkit-user-select: none;
            -ms-user-select: none;
            user-select: none;
        }

        .tile-title {
            cursor: pointer;
            width: 100%;
        }

        .fg-white {
            color: #ffffff !important;
        }

        .bg-black {
            background: #000000 !important;
        }

        .bg-white {
            background: #ffffff !important;
        }

        .bg-lime {
            background: #a4c400 !important;
        }

        .bg-green {
            background: #60a917 !important;
        }

        .bg-emerald {
            background: #008a00 !important;
        }

        .bg-teal {
            background: #00aba9 !important;
        }

        .bg-cyan {
            background: #1ba1e2 !important;
        }

        .bg-cobalt {
            background: #0050ef !important;
        }

        .bg-indigo {
            background: #6a00ff !important;
        }

        .bg-violet {
            background: #aa00ff !important;
        }

        .bg-pink {
            background: #dc4fad !important;
        }

        .bg-magenta {
            background: #d80073 !important;
        }

        .bg-crimson {
            background: #a20025 !important;
        }

        .bg-red {
            background: #ce352c !important;
        }

        .bg-orange {
            background: #fa6800 !important;
        }

        .bg-amber {
            background: #f0a30a !important;
        }

        .bg-yellow {
            background: #e3c800 !important;
        }

        .bg-brown {
            background: #825a2c !important;
        }

        .bg-olive {
            background: #6d8764 !important;
        }

        .bg-steel {
            background: #647687 !important;
        }

        .bg-mauve {
            background: #76608a !important;
        }

        .bg-taupe {
            background: #87794e !important;
        }

        .bg-gray {
            background: #555555 !important;
        }

        .bg-dark {
            background: #333333 !important;
        }

        .bg-darker {
            background: #222222 !important;
        }

        .bg-transparent {
            background: transparent !important;
        }

        .bg-darkBrown {
            background: #63362f !important;
        }

        .bg-darkCrimson {
            background: #640024 !important;
        }

        .bg-darkMagenta {
            background: #81003c !important;
        }

        .bg-darkIndigo {
            background: #4b0096 !important;
        }

        .bg-darkCyan {
            background: #1b6eae !important;
        }

        .bg-darkCobalt {
            background: #00356a !important;
        }

        .bg-darkTeal {
            background: #004050 !important;
        }

        .bg-darkEmerald {
            background: #003e00 !important;
        }

        .bg-darkGreen {
            background: #128023 !important;
        }

        .bg-darkOrange {
            background: #bf5a15 !important;
        }

        .bg-darkRed {
            background: #9a1616 !important;
        }

        .bg-darkPink {
            background: #9a165a !important;
        }

        .bg-darkViolet {
            background: #57169a !important;
        }

        .bg-darkBlue {
            background: #16499a !important;
        }

        .bg-lightBlue {
            background: #4390df !important;
        }

        .bg-lightRed {
            background: #da5a53 !important;
        }

        .bg-lightGreen {
            background: #7ad61d !important;
        }

        .bg-lighterBlue {
            background: #00ccff !important;
        }

        .bg-lightTeal {
            background: #45fffd !important;
        }

        .bg-lightOlive {
            background: #78aa1c !important;
        }

        .bg-lightOrange {
            background: #ffc194 !important;
        }

        .bg-lightPink {
            background: #f472d0 !important;
        }

        .bg-grayDark {
            background: #333333 !important;
        }

        .bg-grayDarker {
            background: #222222 !important;
        }

        .bg-grayLight {
            background: #999999 !important;
        }

        .bg-grayLighter {
            background: #eeeeee !important;
        }

        .bg-blue {
            background: #00aff0 !important;
        }

        .fg-black {
            color: #000000 !important;
        }

        .fg-white {
            color: #ffffff !important;
        }

        .fg-lime {
            color: #a4c400 !important;
        }

        .fg-green {
            color: #60a917 !important;
        }

        .fg-emerald {
            color: #008a00 !important;
        }

        .fg-teal {
            color: #00aba9 !important;
        }

        .fg-cyan {
            color: #1ba1e2 !important;
        }

        .fg-cobalt {
            color: #0050ef !important;
        }

        .fg-indigo {
            color: #6a00ff !important;
        }

        .fg-violet {
            color: #aa00ff !important;
        }

        .fg-pink {
            color: #dc4fad !important;
        }

        .fg-magenta {
            color: #d80073 !important;
        }

        .fg-crimson {
            color: #a20025 !important;
        }

        .fg-red {
            color: #ce352c !important;
        }

        .fg-orange {
            color: #fa6800 !important;
        }

        .fg-amber {
            color: #f0a30a !important;
        }

        .fg-yellow {
            color: #e3c800 !important;
        }

        .fg-brown {
            color: #825a2c !important;
        }

        .fg-olive {
            color: #6d8764 !important;
        }

        .fg-steel {
            color: #647687 !important;
        }

        .fg-mauve {
            color: #76608a !important;
        }

        .fg-taupe {
            color: #87794e !important;
        }

        .fg-gray {
            color: #555555 !important;
        }

        .fg-dark {
            color: #333333 !important;
        }

        .fg-darker {
            color: #222222 !important;
        }

        .fg-transparent {
            color: transparent !important;
        }

        .fg-darkBrown {
            color: #63362f !important;
        }

        .fg-darkCrimson {
            color: #640024 !important;
        }

        .fg-darkMagenta {
            color: #81003c !important;
        }

        .fg-darkIndigo {
            color: #4b0096 !important;
        }

        .fg-darkCyan {
            color: #1b6eae !important;
        }

        .fg-darkCobalt {
            color: #00356a !important;
        }

        .fg-darkTeal {
            color: #004050 !important;
        }

        .fg-darkEmerald {
            color: #003e00 !important;
        }

        .fg-darkGreen {
            color: #128023 !important;
        }

        .fg-darkOrange {
            color: #bf5a15 !important;
        }

        .fg-darkRed {
            color: #9a1616 !important;
        }

        .fg-darkPink {
            color: #9a165a !important;
        }

        .fg-darkViolet {
            color: #57169a !important;
        }

        .fg-darkBlue {
            color: #16499a !important;
        }

        .fg-lightBlue {
            color: #4390df !important;
        }

        .fg-lighterBlue {
            color: #00ccff !important;
        }

        .fg-lightTeal {
            color: #45fffd !important;
        }

        .fg-lightOlive {
            color: #78aa1c !important;
        }

        .fg-lightOrange {
            color: #ffc194 !important;
        }

        .fg-lightPink {
            color: #f472d0 !important;
        }

        .fg-lightRed {
            color: #da5a53 !important;
        }

        .fg-lightGreen {
            color: #7ad61d !important;
        }

        .fg-grayDark {
            color: #333333 !important;
        }

        .fg-grayDarker {
            color: #222222 !important;
        }

        .fg-grayLight {
            color: #999999 !important;
        }

        .fg-grayLighter {
            color: #eeeeee !important;
        }

        .fg-blue {
            color: #00aff0 !important;
        }

        .tile-area-scheme-dark [class*=tile] {
            outline-color: #373737;
        }

        .tile-content:first-child {
            display: block;
        }

        .tile-content {
            position: absolute;
            top: 0;
            left: 0;
            width: 100%;
            height: inherit;
            overflow: hidden;
            /*display: none;*/
        }

            .tile-content.iconic .icon {
                position: absolute;
                width: 64px;
                height: 64px;
                font-size: 64px;
                top: 50%;
                margin-top: -32px;
                left: 50%;
                margin-left: -32px;
                text-align: center;
            }

        [class^="mif-"], [class*=" mif-"] {
            display: inline-block;
            font: normal normal normal 1.5em/1;
            font-family: metro, "Segoe UI", "Open Sans", serif;
            line-height: 1;
            text-rendering: auto;
            -webkit-font-smoothing: antialiased;
            -moz-osx-font-smoothing: grayscale;
            -webkit-transform: translate(0, 0);
            transform: translate(0, 0);
            vertical-align: middle;
            position: static;
        }



        .tile .tile-label {
            position: absolute;
            bottom: 0;
            left: .625rem;
            padding: .425rem .25rem;
            z-index: 999;
        }

        .tile .tile-step {
            position: absolute;
            top: 0;
            left: .625rem;
            padding: .425rem .25rem;
            /*z-index: 999;*/
            width: 100%;
        }

            .tile .tile-step strong {
                float: left;
            }

        .metro-height {
            height: 200px;
        }

        .btn-primary .badge {
            color: #337ab7;
            background-color: #fff;
        }

        .btn .badge {
            margin-left: 2px;
        }

        .tile-step-icon {
            position: absolute;
            right: -50px;
            top: -50px;
            float: right;
            background-color: #f0ad4e;
            width: 100px;
            height: 100px;
            transform: rotate(45deg);
            color: white;
        }

            .tile-step-icon:hover, .tile-step-icon:focus {
                color: #fff;
                background-color: #ec971f;
                border-color: #d58512;
            }

            .tile-step-icon.focus, .tile-step-icon:focus, .tile-step-icon:hover {
                color: #333;
                text-decoration: none;
            }

            .tile-step-icon i {
                position: absolute;
                left: 45px;
                bottom: 14px;
                transform: rotate(-45deg);
            }


        .btn-primary:hover, .btn-primary:focus, .btn-primary:active, .btn-primary.active, .open .dropdown-toggle.btn-primary {
            color: #fff;
            background-color: #428bca;
            border-color: #357ebd;
        }

        .btn {
            border: 0 solid transparent;
        }
    </style>
</head>

<body class="mainbody">
    <form id="form1" runat="server">
        <!--导航栏-->
        <div class="location">
            <a class="home"><i></i><span>管理中心</span></a>

        </div>
        <!--/导航栏-->

        <!--内容-->
        <div class="line10"></div>
        <div class="nlist-1">
            <ul>
                <li>本次登录IP：<asp:Literal ID="litIP" runat="server" Text="-" /></li>
                <li>上次登录IP：<asp:Literal ID="litBackIP" runat="server" Text="-" /></li>
                <li>上次登录时间：<asp:Literal ID="litBackTime" runat="server" Text="-" /></li>
            </ul>
        </div>
        <div class="line10"></div>

        <%--<div class="nlist-2">
            <h3><i></i>站点信息</h3>
            <ul>
                <li>站点名称：<%=siteConfig.webname %></li>
                <li>公司名称：<%=siteConfig.webcompany %></li>
                <li>系统版本：V<%=Utils.GetVersion()%></li>
                <li>升级通知： <asp:Literal ID="LitUpgrade" runat="server" /> </li>
            </ul>
            <h3><i class="msg"></i>官方消息</h3>
            <ul>
               <asp:Literal ID="LitNotice" runat="server" /> 
            </ul>
        </div>--%>
        <!--/内容-->

        <div class="page-header">
            <h4><span class="text-success"><i class="fa fa-info-circle"></i></span>
                首次使用，别担心，我们教您用！</h4>
            <p class="text-muted text-indet">
                注：点击如下步骤可直接进入操作界面进行操作，点击步骤上
            <span class="label label-warning"><i class="fa fa-question fa-lg"></i></span>
                则显示该步骤相关界面的详细操作说明！

            </p>
        </div>


        <ol class="ol-decimal text-indet">
            <li>
                <strong class="tile-title" data-toggle="collapse" href="#collapseLi1" aria-expanded="true" aria-controls="collapseLi1">信息初始化</strong>
                <div class="in" id="collapseLi1" aria-expanded="true">
                    <div class="well metro-height">
                        <button class="tile btn btn-primary" data-role="tile"
                            onclick="alert(2)"
                            style="opacity: 1; transform: scale(1); transition: 0.3s;">
                            <p class="tile-step">
                                <strong>STEP<span class="badge">1</span></strong>
                            </p>
                            <div class="tile-content iconic">
                                <span class="icon fa fa-credit-card"></span>
                            </div>
                            <span class="tile-label">
                                <strong>商家或门店设置</strong>
                            </span>
                            <a href="#" class="tile-step-icon" onclick="javascript:alert(1)">
                                <i class="fa fa-question fa-lg"></i>
                            </a>

                        </button>
                        <button class="tile btn btn-primary" data-role="tile"
                            onclick="alert(2)"
                            style="opacity: 1; transform: scale(1); transition: 0.3s;">
                            <p class="tile-step">
                                <strong>STEP<span class="badge">2</span></strong>
                            </p>
                            <div class="tile-content iconic">
                                <span class="icon fa fa-cubes"></span>
                            </div>
                            <span class="tile-label">
                                <strong>商品管理</strong>
                            </span>
                            <a href="#" class="tile-step-icon" onclick="javascript:alert(1)">
                                <i class="fa fa-question fa-lg"></i>
                            </a>

                        </button>
                    </div>
                </div>
            </li>

            <li>
                <strong class="tile-title" data-toggle="collapse" href="#collapseLi2"
                    aria-expanded="true" aria-controls="collapseLi2">用户下单了，要怎么做？</strong>
                <div class="in" id="collapseLi2" aria-expanded="true">
                    <div class="well metro-height">
                        <button class="tile btn btn-primary" data-role="tile"
                            onclick="alert(2)"
                            style="opacity: 1; transform: scale(1); transition: 0.3s;">
                           
                            <div class="tile-content iconic">
                                <span class="icon fa fa-file-text"></span>
                            </div>
                            <span class="tile-label">
                                <strong>订单管理</strong>
                            </span>
                            <a href="#" class="tile-step-icon" onclick="javascript:alert(1)">
                                <i class="fa fa-question fa-lg"></i>
                            </a>

                        </button>

                    </div>
                </div>
            </li>

            <li>
                <strong class="tile-title" data-toggle="collapse" href="#collapseLi3"
                    aria-expanded="true" aria-controls="collapseLi3">用户到店了，要怎么做？</strong>
                <div class="in" id="collapseLi3" aria-expanded="true">
                    <div class="well metro-height">
                        <button class="tile btn btn-primary" data-role="tile"
                            onclick="alert(2)"
                            style="opacity: 1; transform: scale(1); transition: 0.3s;">
                           
                            <div class="tile-content iconic">
                                <span class="icon fa fa-barcode"></span>
                            </div>
                            <span class="tile-label">
                                <strong>服务码验证</strong>
                            </span>
                            <a href="#" class="tile-step-icon" onclick="javascript:alert(1)">
                                <i class="fa fa-question fa-lg"></i>
                            </a>

                        </button>

                    </div>
                </div>
            </li>
            
            <li>
                <strong class="tile-title" data-toggle="collapse" href="#collapseLi4"
                    aria-expanded="true" aria-controls="collapseLi4">如何和景区结算？</strong>
                <div class="in" id="collapseLi4" aria-expanded="true">
                    <div class="well metro-height">
                        <button class="tile btn btn-primary" data-role="tile"
                            onclick="alert(2)"
                            style="opacity: 1; transform: scale(1); transition: 0.3s;">
                             <p class="tile-step">
                                <strong>STEP<span class="badge">1</span></strong>
                            </p>
                            <div class="tile-content iconic">
                                <span class="icon fa fa-file-text"></span>
                            </div>
                            <span class="tile-label">
                                <strong>订单管理</strong>
                            </span>
                            <a href="#" class="tile-step-icon" onclick="javascript:alert(1)">
                                <i class="fa fa-question fa-lg"></i>
                            </a>

                        </button>
                        <button class="tile btn btn-primary" data-role="tile"
                            onclick="alert(2)"
                            style="opacity: 1; transform: scale(1); transition: 0.3s;">
                            <p class="tile-step">
                                <strong>STEP<span class="badge">2</span></strong>
                            </p>
                            <div class="tile-content iconic">
                                <span class="icon fa fa-barcode"></span>
                            </div>
                            <span class="tile-label">
                                <strong>服务验证查询</strong>
                            </span>
                            <a href="#" class="tile-step-icon" onclick="javascript:alert(1)">
                                <i class="fa fa-question fa-lg"></i>
                            </a>

                        </button>
                    </div>
                </div>
            </li>
            
             <li>
                <strong class="tile-title" data-toggle="collapse" href="#collapseLi5"
                    aria-expanded="true" aria-controls="collapseLi5">如何设置管理员</strong>
                <div class="in" id="collapseLi5" aria-expanded="true">
                    <div class="well metro-height">
                        <button class="tile btn btn-primary" data-role="tile"
                            onclick="alert(2)"
                            style="opacity: 1; transform: scale(1); transition: 0.3s;">
                            
                            <div class="tile-content iconic">
                                <span class="icon fa fa-cog"></span>
                            </div>
                            <span class="tile-label">
                                <strong>管理员设置</strong>
                            </span>
                            <a href="#" class="tile-step-icon" onclick="javascript:alert(1)">
                                <i class="fa fa-question fa-lg"></i>
                            </a>

                        </button>
                       
                    </div>
                </div>
            </li>
             <li>
                <strong class="tile-title" data-toggle="collapse" href="#collapseLi6"
                    aria-expanded="true" aria-controls="collapseLi6">如何修改密码？</strong>
                <div class="in" id="collapseLi6" aria-expanded="true">
                    <div class="well metro-height">
                        <button class="tile btn btn-primary" data-role="tile"
                            onclick="alert(2)"
                            style="opacity: 1; transform: scale(1); transition: 0.3s;">
                           
                            <div class="tile-content iconic">
                                <span class="icon fa fa-unlock-alt"></span>
                            </div>
                            <span class="tile-label">
                                <strong>密码修改</strong>
                            </span>
                            <a href="#" class="tile-step-icon" onclick="javascript:alert(1)">
                                <i class="fa fa-question fa-lg"></i>
                            </a>

                        </button>
                       
                    </div>
                </div>
            </li>
        </ol>

    </form>
    <script src="../scripts/bootstrap/js/bootstrap.min.js"></script>
</body>
</html>
