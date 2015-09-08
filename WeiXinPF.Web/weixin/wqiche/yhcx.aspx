<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="yhcx.aspx.cs" Inherits="WeiXinPF.Web.weixin.wqiche.yhcx" %>

<%@ Import Namespace="WeiXinPF.Common" %>

<!DOCTYPE html>
<html>
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8">
    <meta charset="utf-8">
    <meta content="no-cache,must-revalidate" http-equiv="Cache-Control">
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8">
    <meta name="viewport" content="width=device-width,height=device-height,inital-scale=1.0,maximum-scale=1.0,user-scalable=no;">
    <meta name="apple-mobile-web-app-capable" content="yes">
    <meta name="apple-mobile-web-app-status-bar-style" content="black">
    <meta name="format-detection" content="telephone=no">
    <link rel="stylesheet" type="text/css" href="css/pigcms-ui-1-1.css" media="all">
    <link rel="stylesheet" type="text/css" href="css/common.css" media="all">
    <link rel="stylesheet" type="text/css" href="css/car_reset.css" media="all">
    <link rel="stylesheet" type="text/css" href="css/list-8.css" media="all">
    <link rel="stylesheet" type="text/css" href="css/menu-2.css" media="all">
    <script type="text/javascript" src="js/jQuery.js"></script>
    <title>商城</title>
</head>
<body onselectstart="return true;" ondragstart="return false;">
    <div class="body">
        <footer class="nav_footer">
            <ul class="box">
                <li><a href="javascript:history.go(-1);">返回</a></li>
                <li><a href="javascript:history.go(1);">前进</a></li>
                <li><a href="index.aspx?wid=<%=wid %>&openid=<%=openid %>">首页</a></li>
                <li><a href="javascript:location.reload();">刷新</a></li>
            </ul>
        </footer>

        <header>
            <div class="head_news">
                <a href="">
                    <ul>
                        <li>
                            <img src="<%=acModel.img_url %>" style="width: 100%; max-height: 400px;"></li>
                    </ul>
                    <ol><%=acModel.title %></ol>
                </a>
            </div>
        </header>
        <p></p>
        <section>

            <ul class="list_ul list_ul_news">
                <asp:Repeater ID="rptList" runat="server">
                    <ItemTemplate>
                        <li>
                            <a class="tbox" href="wzDetail.aspx?id=<%#Eval("id") %>&wid=<%=wid %>&openid=<%=openid %>">
                                <div>
                                    <img src="<%#Eval("img_url") %>" style="width: 60px!important; height: 60px;" />
                                </div>
                                <div>
                                    <p><%#Eval("title") %></p>
                                    <p><%#MyCommFun.Obj2DateTime(Eval("add_time")).ToString("yyyy年MM月dd日") %></p>
                                </div>
                            </a>
                        </li>
                    </ItemTemplate>
                </asp:Repeater>
            </ul>
        </section>
    </div>
    <div mark="stat_code" style="width: 0px; height: 0px; display: none;">
    </div>

</body>
</html>
