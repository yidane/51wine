<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="end.aspx.cs" Inherits="WeiXinPF.Web.weixin.photo.end" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
     <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width,height=device-height,inital-scale=1.0,maximum-scale=1.0,user-scalable=no;">
    <meta name="apple-mobile-web-app-capable" content="yes">
    <meta name="apple-mobile-web-app-status-bar-style" content="black">
    <meta name="format-detection" content="telephone=no"> 
    <title>湖怪活动结束</title>
    <link href="../ggk/css/activity_style.css" rel="stylesheet" type="text/css">
</head>
<body>
    <form id="form1" runat="server">
        <div class="main">
            <div class="banner">
              <img src="images/活动结束.png" /></div>
            <div class="content" style="margin-top: 10px">
                <div class="boxcontent boxyellow">
                    <div class="box">
                        <div class="title-red">活动结束说明：</div>
                        <div class="Detail">
                            <p> <asp:Literal ID="litEndNotice" runat="server" EnableViewState="false" Text="亲，本次活动已经结束！"></asp:Literal>  </p>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
