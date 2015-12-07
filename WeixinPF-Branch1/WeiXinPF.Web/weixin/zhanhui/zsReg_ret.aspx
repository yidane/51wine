<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="zsReg_ret.aspx.cs" Inherits="WeiXinPF.Web.weixin.zhanhui.zsReg_ret" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <meta name="viewport" content="width=device-width,height=device-height,inital-scale=1.0,maximum-scale=1.0,user-scalable=no;" />
    <meta name="apple-mobile-web-app-capable" content="yes" />
    <meta name="apple-mobile-web-app-status-bar-style" content="black" />
    <meta name="format-detection" content="telephone=no" />
    <meta name="Author" content="Power by:JerryYan QQ:855222 JerrySoft Framework 1.1" />
    <style type="text/css">
        body {
        }

        p {
            font-size: 13px;
            line-height: 18px;
        }

        h4 {
            margin: 10px 0px;
        }

        * {
            margin: 0;
            outline: 0;
            padding: 0;
            font-size: 100%;
            -webkit-tap-highlight-color: rgba(0, 0, 0, 0);
        }

        .c_back {
            width: 90%;
            margin: 0px auto;
        }
    </style>
</head>
<body>
    <img src="images/banner.jpg" width="100%" />
    <div class="c_back">
        <h4>尊敬的<%=name %>：</h4>
        <p>将报名表 下载填写好后，传真至 021-51714666</p>
        <p>展商报名表下载地址:</p>
        <p><a href="http://pan.baidu.com/s/1jG5o8Ou">http://pan.baidu.com/s/1jG5o8Ou</a></p> 
    </div>
</body>
</html>
