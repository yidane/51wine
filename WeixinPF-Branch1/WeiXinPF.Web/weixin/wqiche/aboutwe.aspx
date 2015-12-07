<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="aboutwe.aspx.cs" Inherits="WeiXinPF.Web.weixin.wqiche.aboutwe" %>

<!DOCTYPE html>
<html>
<head>
    <meta charset="utf-8" />
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8">
    <meta name="viewport" content="width=device-width,height=device-height,inital-scale=1.0,maximum-scale=1.0,user-scalable=no;">
    <meta name="apple-mobile-web-app-capable" content="yes">
    <meta name="apple-mobile-web-app-status-bar-style" content="black">
    <meta name="format-detection" content="telephone=no">
    <link rel="stylesheet" type="text/css" href="css/reset.css" media="all" />
    <link rel="stylesheet" type="text/css" href="css/common.css" media="all" />
    <link rel="stylesheet" type="text/css" href="css/peak-3.css" media="all" />
    <link rel="stylesheet" type="text/css" href="css/home-menu.css" media="all" />
    <script type="text/javascript" src="js/jQuery.js"></script>
    <title>关于我们</title>
    <style>
        img {
            width: 100%!important;
        }
    </style>
</head>
<body onselectstart="return true;" ondragstart="return false;">
    <div class="body" style="padding-bottom: 60px;"> 
        <section class="news_article">
            <header>
                <h3 style="font-size: 14px;">联系我们</h3>
                <small class="gray"><%=wsModel.companyName %></small>
                <p>
                    <img src="<%=wsModel.bgPic %>" width="180" height="50" alt="" style="width: auto!important; max-width: 240px;">
                </p>
            </header>
            <article>
                <p>
                    <div class="box_map">
                        <a href="javascript:void(0);">
                            <div class="map_area">
                                <span>地址： <%=wsModel.addr %></span>
                            </div>
                        </a>
                    </div>
                    <p>
                        <strong><span style="color: #009900; font-size: 16px;">免费热线：<%=wsModel.phone %></span></strong><br />
                        公司简介：<br />
                        <%=wsModel.wBrief %>
                    </p>
                </p>
            </article>
        </section>
        <div style="padding-bottom: 0!important;">
            <a href="javascript:window.scrollTo(0,0);" style="font-size: 12px; margin: 5px auto; display: block; color: #fff; text-align: center; line-height: 35px; background: #333; margin-bottom: -10px;">返回顶部</a>
        </div>
    </div>
    <div mark="stat_code" style="width: 0px; height: 0px; display: none;">
    </div>
</body>
</html>
