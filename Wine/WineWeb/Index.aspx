<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="WineWeb.Index" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="Style/index.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div id="Container">
            <div id="Header">
                <div id="Logo">
                    <img src="Image/logo1.png" alt="" />
                </div>
                <div id="Headermenu">
                    <ul>
                        <li><a id="A1" href="Logout.aspx" runat="server"><span class="n4"></span>安全退出</a></li>
                    </ul>
                </div>
            </div>
            <div id="Toolbar">
                <div class="clock">
                    <img src="Image/clock.png" alt="" width="20" height="20" style="vertical-align: middle; padding-bottom: 3px;" />
                    <span id="datetime"></span>
                </div>
                <div style="width: auto; height: auto; overflow: hidden;">
                    <ul id="tabs">
                    </ul>
                </div>
            </div>
            <div id="Content">
                <div class="navigation">
                    <ul class="accordion">
                    </ul>
                </div>
                <div id="Frame_Container"></div>
            </div>
            <div id="Footer">
                <div style="width: 42%; text-align: left; float: left;">
                    &nbsp;<a href="javascript:void()"></a>
                    <span class="south-separator"></span>
                    <span class="south-separator"></span>
                </div>
                <div style="width: 16%; text-align: left; float: left;">
                    CopyRight © 2014-2014
                </div>
                <div style="width: 42%; text-align: right; float: left;">
                </div>
            </div>
        </div>
        <script src="Javascript/jquery.min.js" type="text/javascript"></script>
        <script src="Javascript/index.js" type="text/javascript"></script>
    </form>
</body>
</html>
