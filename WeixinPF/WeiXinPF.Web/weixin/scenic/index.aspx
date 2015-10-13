<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="WeiXinPF.Web.weixin.scenic.index" %>

<%@ Import Namespace="WeiXinPF.Common" %>
<!DOCTYPE html>

<html>
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>景区导览</title>
    <meta id="eqMobileViewport" name="viewport" content="width=device-width,
          initial-scale=1.0,minimum-scale=1.0, maximum-scale=1.0, user-scalable=no">
    <meta name="apple-mobile-web-app-capable" content="yes">
    <meta name="apple-mobile-web-app-status-bar-style" content="black">
    <meta name="format-detection" content="telephone=no">
    <meta name="format-detection" content="email=no">
    <link href="css/index.css" rel="stylesheet" />
</head>
<body>
    <div class="container">
    </div>
    <script type="text/x-jquery-tmpl" id="tmpl">
        <section class="page bg1">
            <img alt="" src="{{:scenic.firstBgImg}}" style="position: absolute; top: 0; left: 0; width: 100%; height: 100%; z-index: -1" />
            <div class="circle">
                <img class="scenic-name" src="images/scenic_name.png" />
                <img class="stamp" src="images/stamp.png" />
                <img class="poetry" src="images/poetry.png" />
            </div>
            <span class="trigger">
                <svg width="100%" height="100%" viewBox="0 0 60 60" preserveAspectRatio="none">
                    <g class="icon">
                        <rect x="32.5" y="5.5" width="22" height="22" />
                        <rect x="4.5" y="5.5" width="22" height="22" />
                        <rect x="32.5" y="33.5" width="22" height="22" />
                        <rect x="4.5" y="33.5" width="22" height="22" />
                    </g>
                </svg>
            </span>
        </section>
        <section class="page bg2">
            <img alt="" src="{{:scenic.secondBgImg}}" style="position: absolute; top: 0; left: 0; width: 100%; height: 100%; z-index: -1" />
            <div class="info">
                {{:scenic.description}}
            </div>
            <ul class="scenic-list" data-bind="foreach: details">
                {{for details}}
                <li>
                    <a href="detail.aspx?id={{:id}}">
                        <div class="annular-round">
                            <img src="{{:cover}}" />
                        </div>
                        <span>{{:name}}</span>
                    </a>
                </li>
                {{/for}}
            </ul>
        </section>
    </script>
    <script src="../../scripts/jquery/jquery-2.1.0.min.js"></script>
    <script src="../../scripts/jsrender.min.js"></script>
    <script type="text/javascript">
        var hash = document.location.hash;
        function renderViewport() {
            var f = 1,
                e,
                g = $(document).width(),
                h = $(document).height();
            g / h >= 320 / 486 ? (f = h / 486) : (f = g / 320, e = (h / f - 486) / 2);
            $("#eqMobileViewport").attr("content", "width=320, initial-scale=" + f + ", maximum-scale=" + f + ", user-scalable=no");
        }

        function showPage() {
            if (!hash) {
                document.location.hash = "#2";
            }
            $(".bg2").addClass("slide-down").show();
        }

        $(function () {
            renderViewport();
            $.ajax({
                url: 'scenic.ashx',
                data: { action: 'GetScenic', id: <%=MyCommFun.RequestInt("id") %> },
                dataType: "json"
            }).done(function (res) {
                if (res && res.isSuccess) {
                    var outHtml = $("#tmpl").render(res.data);
                    $(".container").empty().html(outHtml);

                    if (hash != "#2") {
                        var autoDisplayNextPage = res.data.scenic.autoDisplayNextPage;
                        var delay = res.result.scenic.delay || 10;

                        if (autoDisplayNextPage) {
                            var timer = setTimeout(showPage, delay * 1000);
                            $(".trigger").on('click', function () {
                                if (timer) {
                                    clearTimeout(timer);
                                }
                                showPage();
                            });
                        }
                    }
                    else {
                        $('.bg1').hide();
                        $('.bg2').show();
                    }
                }
            }).fail(function () {
                alert('获取配置信息失败。');
            });
        });
    </script>
</body>
</html>
