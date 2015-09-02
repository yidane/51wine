<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="cxXinshang.aspx.cs" Inherits="MxWeiXinPF.Web.weixin.wqiche.cxXinshang" %>

<html>
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8">
    <title></title>
    <meta name="viewport" content="width=device-width,height=device-height,inital-scale=1.0,maximum-scale=1.0,user-scalable=no;">
    <meta name="apple-mobile-web-app-capable" content="yes">
    <meta name="apple-mobile-web-app-status-bar-style" content="black">
    <meta name="format-detection" content="telephone=no">
    <meta charset="utf-8">
    <link href="css/photo.css" rel="stylesheet" type="text/css" />
    <link href="css/photoswipe.css" type="text/css" rel="stylesheet" />
    <script src="js/klass.min.js" type="text/javascript"></script>
    <script src="js/code.photoswipe-3.0.5.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        (function (window, PhotoSwipe) {
            document.addEventListener('DOMContentLoaded', function () {
                var
                options = {},
                instance = PhotoSwipe.attach(window.document.querySelectorAll('#Gallery a'), options);
            }, false);
        }(window, window.Code.PhotoSwipe));
    </script>
</head>

<body id="photo">
    <div class="qiandaobanner">
        <a>
            <img src="img/banner.jpg"></a>
    </div>
    <div id="main" role="main">
        <ul id="Gallery" class="gallery">
            <asp:Repeater ID="rptList" runat="server">
                <ItemTemplate>
                    <li><a href="<%#Eval("photoPic") %>">
                        <img src="<%#Eval("photoPic") %>" alt="  " /></a>
                    </li>
                </ItemTemplate>
            </asp:Repeater> 
        </ul> 
    </div> 
    <!--jquery.min-->
    <script src="js/jquery.min.js" type="text/javascript"></script>
    <!--下面是瀑布流js-->
    <script src="js/jquery.imagesloaded.js" type="text/javascript"></script>
    <script src="js/jquery.wookmark.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        (function ($) {
            $('#Gallery').imagesLoaded(function () {
                // Prepare layout options.
                var options = {
                    autoResize: true, // This will auto-update the layout when the browser window is resized.
                    container: $('#main'), // Optional, used for some extra CSS styling
                    offset: 4, // Optional, the distance between grid items
                    itemWidth: 150 // Optional, the width of a grid item
                };

                // Get a reference to your grid items.
                var handler = $('#Gallery li'); 
                // Call the layout function.
                handler.wookmark(options); 
            });
        })(jQuery);
    </script>
</body>
</html>
