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
                color: #fff;
                text-decoration: none;
            }

            .tile-step-icon i {
                position: absolute;
                left: 45px;
                bottom: 14px;
                transform: rotate(-45deg);
            }


        /*.btn-primary:hover, .btn-primary:focus, .btn-primary:active, .btn-primary.active, .open .dropdown-toggle.btn-primary {
            color: #fff;
            background-color: #428bca;
            border-color: #357ebd;
        }*/

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

        <% if (_userType == "HotelAdmin")
            {%>
        <ol class="ol-decimal text-indet">
            <li>
                <strong class="tile-title" data-toggle="collapse" href="#collapseLi1" aria-expanded="true" aria-controls="collapseLi1">信息初始化</strong>
                <div class="in" id="collapseLi1" aria-expanded="true">
                    <div class="well metro-height">

                        <div class="tile btn btn-primary" data-role="tile"
                            onclick="javascript:jumpUrl('hotel_info');"
                            style="opacity: 1; transform: scale(1); transition: 0.3s;">
                            <p class="tile-step">
                                <strong>STEP<span class="badge">1</span></strong>
                            </p>
                            <div class="tile-content iconic">
                                <span class="icon fa fa-credit-card"></span>
                            </div>
                            <span class="tile-label">
                                <strong>商户或门店设置</strong>
                            </span>
                            <a href="#" class="tile-step-icon" onclick="javascript:jumpdirect('usehelp/酒店管理员/商户或门店设置.htm');">
                                <i class="fa fa-question fa-lg"></i>
                            </a>

                        </div>


                        <div class="tile btn btn-primary" data-role="tile"
                            onclick="javascript:jumpUrl('hotel_room');"
                            style="opacity: 1; transform: scale(1); transition: 0.3s;">
                            <p class="tile-step">
                                <strong>STEP<span class="badge">2</span></strong>
                            </p>
                            <div class="tile-content iconic">
                                <span class="icon fa fa-bed"></span>
                            </div>
                            <span class="tile-label">
                                <strong>商品管理</strong>
                            </span>
                            <a href="#" class="tile-step-icon" onclick="javascript:jumpdirect('usehelp/酒店管理员/商品管理.htm');">
                                <i class="fa fa-question fa-lg"></i>
                            </a>

                        </div>
                    </div>
                </div>
            </li>

            <li>
                <strong class="tile-title" data-toggle="collapse" href="#collapseLi2"
                    aria-expanded="true" aria-controls="collapseLi2">用户下单了，要怎么做？</strong>
                <div class="in" id="collapseLi2" aria-expanded="true">
                    <div class="well metro-height">
                        <div class="tile btn btn-primary" data-role="tile"
                            onclick="javascript:jumpUrl('hotel_dingdan_manage');"
                            style="opacity: 1; transform: scale(1); transition: 0.3s;">

                            <div class="tile-content iconic">
                                <span class="icon fa fa-file-text"></span>
                            </div>
                            <span class="tile-label">
                                <strong>订单管理</strong>
                            </span>
                            <a href="#" class="tile-step-icon" onclick="javascript:jumpdirect('usehelp/酒店管理员/订单管理.htm');">
                                <i class="fa fa-question fa-lg"></i>
                            </a>

                        </div>

                    </div>
                </div>
            </li>

            <li>
                <strong class="tile-title" data-toggle="collapse" href="#collapseLi3"
                    aria-expanded="true" aria-controls="collapseLi3">用户到店了，要怎么做？</strong>
                <div class="in" id="collapseLi3" aria-expanded="true">
                    <div class="well metro-height">
                        <div class="tile btn btn-primary" data-role="tile"
                            onclick="javascript:jumpUrl('dingdan_confirm');"
                            style="opacity: 1; transform: scale(1); transition: 0.3s;">

                            <div class="tile-content iconic">
                                <span class="icon fa fa-qrcode"></span>
                            </div>
                            <span class="tile-label">
                                <strong>服务码验证</strong>
                            </span>
                            <a href="#" class="tile-step-icon" onclick="javascript:jumpdirect('usehelp/酒店管理员/订单管理.htm');">
                                <i class="fa fa-question fa-lg"></i>
                            </a>

                        </div>

                    </div>
                </div>
            </li>

            <li>
                <strong class="tile-title" data-toggle="collapse" href="#collapseLi4"
                    aria-expanded="true" aria-controls="collapseLi4">如何和景区结算？</strong>
                <div class="in" id="collapseLi4" aria-expanded="true">
                    <div class="well metro-height">
                        <div class="tile btn btn-primary" data-role="tile"
                            onclick="javascript:jumpUrl('hotel_dingdan_manage');"
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
                            <a href="#" class="tile-step-icon" onclick="javascript:jumpdirect('usehelp/酒店管理员/订单管理.htm');">
                                <i class="fa fa-question fa-lg"></i>
                            </a>

                        </div>
                        <div class="tile btn btn-primary" data-role="tile"
                            onclick="javascript:jumpUrl('hotelcredentials_detail');"
                            style="opacity: 1; transform: scale(1); transition: 0.3s;">
                            <p class="tile-step">
                                <strong>STEP<span class="badge">2</span></strong>
                            </p>
                            <div class="tile-content iconic">
                                <span class="icon fa fa-qrcode"></span>
                            </div>
                            <span class="tile-label">
                                <strong>服务凭据查询</strong>
                            </span>
                            <a href="#" class="tile-step-icon" onclick="javascript:jumpdirect('usehelp/酒店管理员/服务凭据查询.htm');">
                                <i class="fa fa-question fa-lg"></i>
                            </a>

                        </div>
                    </div>
                </div>
            </li>

            <li>
                <strong class="tile-title" data-toggle="collapse" href="#collapseLi5"
                    aria-expanded="true" aria-controls="collapseLi5">如何设置管理员</strong>
                <div class="in" id="collapseLi5" aria-expanded="true">
                    <div class="well metro-height">
                        <div class="tile btn btn-primary" data-role="tile"
                            onclick="javascript:jumpUrl('hotel_user');"
                            style="opacity: 1; transform: scale(1); transition: 0.3s;">

                            <div class="tile-content iconic">
                                <span class="icon fa fa-cog"></span>
                            </div>
                            <span class="tile-label">
                                <strong>管理员设置</strong>
                            </span>
                            <a href="#" class="tile-step-icon" onclick="javascript:jumpdirect('usehelp/酒店管理员/管理员设置.htm');">
                                <i class="fa fa-question fa-lg"></i>
                            </a>

                        </div>

                    </div>
                </div>
            </li>
            <li>
                <strong class="tile-title" data-toggle="collapse" href="#collapseLi6"
                    aria-expanded="true" aria-controls="collapseLi6">如何修改密码？</strong>
                <div class="in" id="collapseLi6" aria-expanded="true">
                    <div class="well metro-height">
                        <div class="tile btn btn-primary" data-role="tile"
                            onclick="javascript:jumpdirect('manager/manager_pwd.aspx');"
                            style="opacity: 1; transform: scale(1); transition: 0.3s;">

                            <div class="tile-content iconic">
                                <span class="icon fa fa-unlock-alt"></span>
                            </div>
                            <span class="tile-label">
                                <strong>密码修改</strong>
                            </span>
                            <a href="#" class="tile-step-icon" onclick="javascript:jumpdirect('usehelp/酒店管理员/密码修改.htm');">
                                <i class="fa fa-question fa-lg"></i>
                            </a>

                        </div>

                    </div>
                </div>
            </li>
        </ol>
        <%   } %>

        <% else if (_userType == "ShopAdmin")
            {%>
        <ol class="ol-decimal text-indet">
            <li>
                <strong class="tile-title" data-toggle="collapse" href="#collapseLi1" aria-expanded="true" aria-controls="collapseLi1">信息初始化</strong>
                <div class="in" id="collapseLi1" aria-expanded="true">
                    <div class="well metro-height">
                        <div class="tile btn btn-primary" data-role="tile"
                            onclick="javascript:jumpUrl('shop_add');"
                            style="opacity: 1; transform: scale(1); transition: 0.3s;">
                            <p class="tile-step">
                                <strong>STEP<span class="badge">1</span></strong>
                            </p>
                            <div class="tile-content iconic">
                                <span class="icon fa fa-credit-card"></span>
                            </div>
                            <span class="tile-label">
                                <strong>商户或门店设置</strong>
                            </span>
                            <a href="#" class="tile-step-icon" onclick="javascript:jumpdirect('usehelp/餐饮管理员/商户或门店设置.htm');">
                                <i class="fa fa-question fa-lg"></i>
                            </a>

                        </div>
                        <div class="tile btn btn-primary" data-role="tile"
                            onclick="javascript:jumpUrl('caipin_category');"
                            style="opacity: 1; transform: scale(1); transition: 0.3s;">
                            <p class="tile-step">
                                <strong>STEP<span class="badge">2</span></strong>
                            </p>
                            <div class="tile-content iconic">
                                <span class="icon fa fa-cubes"></span>
                            </div>
                            <span class="tile-label">
                                <strong>商品分类管理</strong>
                            </span>
                            <a href="#" class="tile-step-icon" onclick="javascript:jumpdirect('usehelp/餐饮管理员/商品分类管理.htm');">
                                <i class="fa fa-question fa-lg"></i>
                            </a>

                        </div>
                        <div class="tile btn btn-primary" data-role="tile"
                            onclick="javascript:jumpUrl('caipin_manage');"
                            style="opacity: 1; transform: scale(1); transition: 0.3s;">
                            <p class="tile-step">
                                <strong>STEP<span class="badge">3</span></strong>
                            </p>
                            <div class="tile-content iconic">
                                <span class="icon fa fa-cutlery"></span>
                            </div>
                            <span class="tile-label">
                                <strong>商品管理</strong>
                            </span>
                            <a href="#" class="tile-step-icon" onclick="javascript:jumpdirect('usehelp/餐饮管理员/商品管理.htm');">
                                <i class="fa fa-question fa-lg"></i>
                            </a>

                        </div>
                    </div>
                </div>
            </li>

            <li>
                <strong class="tile-title" data-toggle="collapse" href="#collapseLi2"
                    aria-expanded="true" aria-controls="collapseLi2">用户下单了，要怎么做？</strong>
                <div class="in" id="collapseLi2" aria-expanded="true">
                    <div class="well metro-height">
                        <div class="tile btn btn-primary" data-role="tile"
                            onclick="javascript:jumpUrl('dingdan_manage');"
                            style="opacity: 1; transform: scale(1); transition: 0.3s;">

                            <div class="tile-content iconic">
                                <span class="icon fa fa-file-text"></span>
                            </div>
                            <span class="tile-label">
                                <strong>订单管理</strong>
                            </span>
                            <a href="#" class="tile-step-icon" onclick="javascript:jumpdirect('usehelp/餐饮管理员/订单管理.htm');">
                                <i class="fa fa-question fa-lg"></i>
                            </a>

                        </div>

                    </div>
                </div>
            </li>
            <li>
                <strong class="tile-title" data-toggle="collapse" href="#collapseLi8"
                    aria-expanded="true" aria-controls="collapseLi8">用户退单了，要怎么做？</strong>
                <div class="in" id="collapseLi8" aria-expanded="true">
                    <div class="well metro-height">
                        <div class="tile btn btn-primary" data-role="tile"
                            onclick="javascript:jumpUrl('dingdanRefund_manage');"
                            style="opacity: 1; transform: scale(1); transition: 0.3s;">

                            <div class="tile-content iconic">
                                <span class="icon fa fa-hand-o-left"></span>
                            </div>
                            <span class="tile-label">
                                <strong>退单管理</strong>
                            </span>
                            <a href="#" class="tile-step-icon" onclick="javascript:jumpdirect('usehelp/餐饮管理员/退单管理.htm');">
                                <i class="fa fa-question fa-lg"></i>
                            </a>

                        </div>

                    </div>
                </div>
            </li>

          
                <li>
                    <strong class="tile-title" data-toggle="collapse" href="#collapseLi3"
                        aria-expanded="true" aria-controls="collapseLi3">用户到店了，要怎么做？</strong>
                    <div class="in" id="collapseLi3" aria-expanded="true">
                        <div class="well metro-height">
                            <div class="tile btn btn-primary" data-role="tile"
                                onclick="javascript:jumpUrl('dingdanconfirm');"
                                style="opacity: 1; transform: scale(1); transition: 0.3s;">

                                <div class="tile-content iconic">
                                    <span class="icon fa fa-qrcode"></span>
                                </div>
                                <span class="tile-label">
                                    <strong>服务码验证</strong>
                                </span>
                                <a href="#" class="tile-step-icon" onclick="javascript:jumpdirect('usehelp/餐饮管理员/服务码验证.htm');">
                                    <i class="fa fa-question fa-lg"></i>
                                </a>

                            </div>

                        </div>
                    </div>
                </li>

                <li>
                    <strong class="tile-title" data-toggle="collapse" href="#collapseLi4"
                        aria-expanded="true" aria-controls="collapseLi4">如何和景区结算？</strong>
                    <div class="in" id="collapseLi4" aria-expanded="true">
                        <div class="well metro-height">
                            <div class="tile btn btn-primary" data-role="tile"
                                onclick="javascript:jumpUrl('dingdan_manage');"
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
                                <a href="#" class="tile-step-icon" onclick="javascript:jumpdirect('usehelp/餐饮管理员/订单管理.htm');">
                                    <i class="fa fa-question fa-lg"></i>
                                </a>

                            </div>
                            <div class="tile btn btn-primary" data-role="tile"
                                onclick="javascript:jumpUrl('diancai_credentials_detail');"
                                style="opacity: 1; transform: scale(1); transition: 0.3s;">
                                <p class="tile-step">
                                    <strong>STEP<span class="badge">2</span></strong>
                                </p>
                                <div class="tile-content iconic">
                                    <span class="icon fa fa-qrcode"></span>
                                </div>
                                <span class="tile-label">
                                    <strong>服务凭据查询</strong>
                                </span>
                                <a href="#" class="tile-step-icon" onclick="javascript:jumpdirect('usehelp/餐饮管理员/服务凭据查询.htm');">
                                    <i class="fa fa-question fa-lg"></i>
                                </a>

                            </div>
                        </div>
                    </div>
                </li>

                <li>
                    <strong class="tile-title" data-toggle="collapse" href="#collapseLi5"
                        aria-expanded="true" aria-controls="collapseLi5">如何设置管理员</strong>
                    <div class="in" id="collapseLi5" aria-expanded="true">
                        <div class="well metro-height">
                            <div class="tile btn btn-primary" data-role="tile"
                                onclick="javascript:jumpUrl('sysadmin');"
                                style="opacity: 1; transform: scale(1); transition: 0.3s;">

                                <div class="tile-content iconic">
                                    <span class="icon fa fa-cog"></span>
                                </div>
                                <span class="tile-label">
                                    <strong>管理员设置</strong>
                                </span>
                                <a href="#" class="tile-step-icon" onclick="javascript:jumpdirect('usehelp/餐饮管理员/管理员设置.htm');">
                                    <i class="fa fa-question fa-lg"></i>
                                </a>

                            </div>

                        </div>
                    </div>
                </li>
                <li>
                    <strong class="tile-title" data-toggle="collapse" href="#collapseLi6"
                        aria-expanded="true" aria-controls="collapseLi6">如何修改密码？</strong>
                    <div class="in" id="collapseLi6" aria-expanded="true">
                        <div class="well metro-height">
                            <div class="tile btn btn-primary" data-role="tile"
                                onclick="javascript:jumpdirect('manager/manager_pwd.aspx');"
                                style="opacity: 1; transform: scale(1); transition: 0.3s;">

                                <div class="tile-content iconic">
                                    <span class="icon fa fa-unlock-alt"></span>
                                </div>
                                <span class="tile-label">
                                    <strong>密码修改</strong>
                                </span>
                                <a href="#" class="tile-step-icon" onclick="javascript:jumpdirect('usehelp/餐饮管理员/密码修改.htm');">
                                    <i class="fa fa-question fa-lg"></i>
                                </a>

                            </div>

                        </div>
                    </div>
                </li>
        </ol>
        <%   } %>

        <% else if (_userType == "ScenicAdmin")
            {%>
        <ol class="ol-decimal text-indet">
            <li>
                <strong class="tile-title" data-toggle="collapse"
                     href="#collapseLi1" aria-expanded="true" aria-controls="collapseLi1">如何设置公众号自动消息回复？</strong>
                <div class="in" id="collapseLi1" aria-expanded="true">
                    <div class="well metro-height">
                        <div class="tile btn btn-primary" data-role="tile"
                            onclick="javascript:jumpUrl('weixin_huifu_subscibe');"
                            style="opacity: 1; transform: scale(1); transition: 0.3s;">
                            
                            <div class="tile-content iconic">
                                <span class="icon fa fa-eye"></span>
                            </div>
                            <span class="tile-label">
                                <strong>关注时回复</strong>
                            </span>
                            <a href="#" class="tile-step-icon" onclick="javascript:jumpdirect('usehelp/景区管理员/关注时回复.htm');">
                                <i class="fa fa-question fa-lg"></i>
                            </a>

                        </div>
                        <div class="tile btn btn-primary" data-role="tile"
                            onclick="javascript:jumpUrl('weixin_huifu_default');"
                            style="opacity: 1; transform: scale(1); transition: 0.3s;">
                           
                            <div class="tile-content iconic">
                                <span class="icon fa fa-file-code-o"></span>
                            </div>
                            <span class="tile-label">
                                <strong>默认回复</strong>
                            </span>
                            <a href="#" class="tile-step-icon" onclick="javascript:jumpdirect('usehelp/景区管理员/默认回复.htm');">
                                <i class="fa fa-question fa-lg"></i>
                            </a>

                        </div>
                        <div class="tile btn btn-primary" data-role="tile"
                            onclick="javascript:jumpUrl('weixin_huifu_wenben');"
                            style="opacity: 1; transform: scale(1); transition: 0.3s;">
                            
                            <div class="tile-content iconic">
                                <span class="icon fa fa-file-word-o"></span>
                            </div>
                            <span class="tile-label">
                                <strong>文本回复</strong>
                            </span>
                            <a href="#" class="tile-step-icon" onclick="javascript:jumpdirect('usehelp/景区管理员/文本回复.htm');">
                                <i class="fa fa-question fa-lg"></i>
                            </a>

                        </div>
                        <div class="tile btn btn-primary" data-role="tile"
                            onclick="javascript:jumpUrl('weixin_huifu_tuwen');"
                            style="opacity: 1; transform: scale(1); transition: 0.3s;">
                            
                            <div class="tile-content iconic">
                                <span class="icon fa fa-file-photo-o"></span>
                            </div>
                            <span class="tile-label">
                                <strong>图文回复</strong>
                            </span>
                            <a href="#" class="tile-step-icon" onclick="javascript:jumpdirect('usehelp/景区管理员/图文回复.htm');">
                                <i class="fa fa-question fa-lg"></i>
                            </a>

                        </div>
                        <div class="tile btn btn-primary" data-role="tile"
                            onclick="javascript:jumpUrl('weixin_huifu_yuyin');"
                            style="opacity: 1; transform: scale(1); transition: 0.3s;">
                           
                            <div class="tile-content iconic">
                                <span class="icon fa fa-file-audio-o"></span>
                            </div>
                            <span class="tile-label">
                                <strong>语音回复</strong>
                            </span>
                            <a href="#" class="tile-step-icon" onclick="javascript:jumpdirect('usehelp/景区管理员/语音回复.htm');">
                                <i class="fa fa-question fa-lg"></i>
                            </a>

                        </div>
                    </div>
                </div>
            </li>

            <li>
                <strong class="tile-title" data-toggle="collapse" href="#collapseLi2"
                    aria-expanded="true" aria-controls="collapseLi2">菜单设置</strong>
                <div class="in" id="collapseLi2" aria-expanded="true">
                    <div class="well metro-height">
                        <div class="tile btn btn-primary" data-role="tile"
                            onclick="javascript:jumpUrl('weixin_caidan_setting');"
                            style="opacity: 1; transform: scale(1); transition: 0.3s;">

                            <div class="tile-content iconic">
                                <span class="icon fa fa-book"></span>
                            </div>
                            <span class="tile-label">
                                <strong>菜单设置</strong>
                            </span>
                            <a href="#" class="tile-step-icon" onclick="javascript:jumpdirect('usehelp/景区管理员/菜单设置.htm');">
                                <i class="fa fa-question fa-lg"></i>
                            </a>

                        </div>

                    </div>
                </div>
            </li>
            

            
                <li>
                    <strong class="tile-title" data-toggle="collapse" href="#collapseLi3"
                        aria-expanded="true" aria-controls="collapseLi3">如何在公众号景区导览中添加新的景点？</strong>
                    <div class="in" id="collapseLi3" aria-expanded="true">
                        <div class="well metro-height">
                            <div class="tile btn btn-primary" data-role="tile"
                                onclick="javascript:jumpUrl('scenic');"
                                style="opacity: 1; transform: scale(1); transition: 0.3s;">

                                <div class="tile-content iconic">
                                    <span class="icon fa fa-image"></span>
                                </div>
                                <span class="tile-label">
                                    <strong>景区导览</strong>
                                </span>
                                <a href="#" class="tile-step-icon" onclick="javascript:jumpdirect('usehelp/景区管理员/景区导览.htm');">
                                    <i class="fa fa-question fa-lg"></i>
                                </a>

                            </div>

                        </div>
                    </div>
                </li>

                <li>
                    <strong class="tile-title" data-toggle="collapse" href="#collapseLi4"
                        aria-expanded="true" aria-controls="collapseLi4">如何为公众号增加摇一摇活动？</strong>
                    <div class="in" id="collapseLi4" aria-expanded="true">
                        <div class="well metro-height">
                            <div class="tile btn btn-primary" data-role="tile"
                                onclick="javascript:jumpUrl('shakeLuckyMoney');"
                                style="opacity: 1; transform: scale(1); transition: 0.3s;">
                                <p class="tile-step">
                                    <strong>STEP<span class="badge">1</span></strong>
                                </p>
                                <div class="tile-content iconic">
                                    <span class="icon fa fa-hand-rock-o"></span>
                                </div>
                                <span class="tile-label">
                                    <strong>摇一摇</strong>
                                </span>
                                <a href="#" class="tile-step-icon" onclick="javascript:jumpdirect('usehelp/景区管理员/摇一摇.htm');">
                                    <i class="fa fa-question fa-lg"></i>
                                </a>

                            </div>
                            <div class="tile btn btn-primary" data-role="tile"
                                onclick="javascript:jumpUrl('weixin_caidan_setting');"
                                style="opacity: 1; transform: scale(1); transition: 0.3s;">
                                <p class="tile-step">
                                    <strong>STEP<span class="badge">2</span></strong>
                                </p>
                                <div class="tile-content iconic">
                                    <span class="icon fa fa-book"></span>
                                </div>
                                <span class="tile-label">
                                    <strong>菜单设置</strong>
                                </span>
                                <a href="#" class="tile-step-icon" onclick="javascript:jumpdirect('usehelp/景区管理员/菜单设置.htm');">
                                    <i class="fa fa-question fa-lg"></i>
                                </a>

                            </div>
                        </div>
                    </div>
                </li>
             <li>
                    <strong class="tile-title" data-toggle="collapse" href="#collapseLi5"
                        aria-expanded="true" aria-controls="collapseLi5">如何查询、统计门票订单？</strong>
                    <div class="in" id="collapseLi5" aria-expanded="true">
                        <div class="well metro-height">
                            <div class="tile btn btn-primary" data-role="tile"
                                onclick="javascript:jumpUrl('dingdantongji');"
                                style="opacity: 1; transform: scale(1); transition: 0.3s;">
                              
                                <div class="tile-content iconic">
                                    <span class="icon fa fa-bar-chart"></span>
                                </div>
                                <span class="tile-label">
                                    <strong>门票订单统计</strong>
                                </span>
                                <a href="#" class="tile-step-icon" onclick="javascript:jumpdirect('usehelp/景区管理员/门票订单统计.htm');">
                                    <i class="fa fa-question fa-lg"></i>
                                </a>

                            </div>
        
                        </div>
                    </div>
                </li>
             <li>
                    <strong class="tile-title" data-toggle="collapse" href="#collapseLi6"
                        aria-expanded="true" aria-controls="collapseLi6">如何管理酒店商户或门店？</strong>
                    <div class="in" id="collapseLi6" aria-expanded="true">
                        <div class="well metro-height">
                            <div class="tile btn btn-primary" data-role="tile"
                                onclick="javascript:jumpUrl('wHotel');"
                                style="opacity: 1; transform: scale(1); transition: 0.3s;">
                              
                                <div class="tile-content iconic">
                                    <span class="icon fa fa-bed"></span>
                                </div>
                                <span class="tile-label">
                                    <strong>酒店管理</strong>
                                </span>
                                <a href="#" class="tile-step-icon" onclick="javascript:jumpdirect('usehelp/景区管理员/酒店管理.htm');">
                                    <i class="fa fa-question fa-lg"></i>
                                </a>

                            </div>
        
                        </div>
                    </div>
                </li>
             <li>
                    <strong class="tile-title" data-toggle="collapse" href="#collapseLi7"
                        aria-expanded="true" aria-controls="collapseLi7">如何管理餐饮商户或门店？</strong>
                    <div class="in" id="collapseLi7" aria-expanded="true">
                        <div class="well metro-height">
                            <div class="tile btn btn-primary" data-role="tile"
                                onclick="javascript:jumpUrl('diancai');"
                                style="opacity: 1; transform: scale(1); transition: 0.3s;">
                              
                                <div class="tile-content iconic">
                                    <span class="icon fa fa-cutlery"></span>
                                </div>
                                <span class="tile-label">
                                    <strong>餐饮管理</strong>
                                </span>
                                <a href="#" class="tile-step-icon" onclick="javascript:jumpdirect('usehelp/景区管理员/餐饮管理.htm');">
                                    <i class="fa fa-question fa-lg"></i>
                                </a>

                            </div>
        
                        </div>
                    </div>
                </li>
                
                <li>
                    <strong class="tile-title" data-toggle="collapse" href="#collapseLi8"
                        aria-expanded="true" aria-controls="collapseLi8">如何修改密码？</strong>
                    <div class="in" id="collapseLi8" aria-expanded="true">
                        <div class="well metro-height">
                            <div class="tile btn btn-primary" data-role="tile"
                                onclick="javascript:jumpdirect('manager/manager_pwd.aspx');"
                                style="opacity: 1; transform: scale(1); transition: 0.3s;">

                                <div class="tile-content iconic">
                                    <span class="icon fa fa-unlock-alt"></span>
                                </div>
                                <span class="tile-label">
                                    <strong>密码修改</strong>
                                </span>
                                <a href="#" class="tile-step-icon" onclick="javascript:jumpdirect('usehelp/景区管理员/密码修改.htm');">
                                    <i class="fa fa-question fa-lg"></i>
                                </a>

                            </div>

                        </div>
                    </div>
                </li>
        </ol>
        <%   } %>
    </form>
    <script src="../scripts/bootstrap/js/bootstrap.min.js"></script>
    <script type="text/javascript">
        function jumpUrl(navid) {
//            alert(navid);
            parent.linkMenuTree(true, navid);
            $(top.document).find('.main-sidebar .list-group').find('a').each(function() {
                var a = $(this);
                if (a.attr('navid') && a.attr('navid') == navid) {
//                    console.log(a);
                    jumpdirect(a.attr('href'));
                }
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
</body>
</html>
