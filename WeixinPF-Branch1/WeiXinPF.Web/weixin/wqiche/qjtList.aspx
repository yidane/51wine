<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="qjtList.aspx.cs" Inherits="WeiXinPF.Web.weixin.wqiche.qjtList" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8">
    <title>汽车全景图列表</title>
    <meta name="viewport" content="width=device-width, initial-scale=1.0, minimum-scale=0.5, maximum-scale=2.0, user-scalable=yes" />
    <meta name="apple-mobile-web-app-capable" content="yes">
    <meta name="apple-mobile-web-app-status-bar-style" content="black">
    <meta name="format-detection" content="telephone=no">
    <meta charset="utf-8">
    <link href="css/news.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" href="css/plugmenu.css">
</head>

<body id="listhome1">
    <div class="Listpage">
        <div id="todayList">
            <ul class="todayList">
                <asp:Repeater ID="rptView" runat="server">
                    <ItemTemplate>
                        <li>
                            <a href="qjt.aspx?id=<%#Eval("id") %>&wid=<%=wid %>&openid=<%=openid %>">
                                <div class="img">
                                    <img src="<%#Eval("pri_front") %>">
                                </div>
                                <h2><%#Eval("jdname") %></h2>
                                <p class="onlyheight"><%#Eval("remark") %></p>
                                <div class="commentNum"></div>
                            </a>
                        </li>
                    </ItemTemplate>
                </asp:Repeater>
            </ul>
        </div>
    </div> 
</body>
</html>
