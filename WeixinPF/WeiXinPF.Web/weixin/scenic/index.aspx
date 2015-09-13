<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="WeiXinPF.Web.weixin.scenic.index" %>

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
        <section class="page bg1" data-bind="with: scenic">
            <img alt="" data-bind="attr: { 'src': firstBgImg }" style="position: absolute; top: 0; left: 0; width: 100%; height: 100%; z-index: -1" />
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
        <section class="page bg2" style="display: none;">
            <ul class="scenic-list">
                <li>
                    <a href="detail.aspx">
                        <div class="annular-round">
                            <img src="images/scenic1.jpg" />
                        </div>
                        <span>卧龙湾</span>
                    </a>
                </li>
                <li>
                    <a href="detail.aspx">
                        <div class="annular-round">
                            <img src="images/scenic2.jpg" />
                        </div>
                        <span>月亮湾</span>
                    </a>
                </li>
                <li>
                    <a href="detail.aspx">
                        <div class="annular-round">
                            <img src="images/scenic3.jpg" />
                        </div>
                        <span>神仙湾</span>
                    </a>
                </li>
                <li>
                    <a href="detail.aspx">
                        <div class="annular-round">
                            <img src="images/scenic4.jpg" />
                        </div>
                        <span>观鱼台</span>
                    </a>
                </li>
            </ul>
            <div class="info"><span style="font-size: 16px; font-weight: bold;">&nbsp;&nbsp;&nbsp;&nbsp;喀纳斯</span>，一个美丽富饶、神秘莫测的地方，在这里壮观的冰川映衬着宁静的湖水、茫茫的草原包容着幽深的原始森林。神秘的湖怪、古朴的土瓦人、变换的湖水、眩人的风景会让人痴迷。生活在都市的你，可以适时的停下脚步，背上背包，在这片净土上和自己的心来一次徒步之旅。</div>
        </section>
    </div>
    <script src="../../scripts/jquery/jquery-2.1.0.min.js"></script>
    <script src="../../scripts/knockout/knockout-3.3.0.js"></script>
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
                document.location.hash = "page2";
            }
            $(".bg2").addClass("slide-down").show();
        }

        $(function () {
            var scenicViewModel = new ScenicViewModel();
            ko.applyBindings(scenicViewModel);
        });


        var ScenicViewModel = function () {
            var self = this;
            this.scenic = ko.observable();
            this.details = ko.observableArray();

            var loadData = function () {
                $.getJSON('scenic.ashx', {
                    action: 'GetScenic', id: 1
                }).done(function (res) {
                    if (res && res.success) {
                        self.scenic(res.result.scenic);
                        self.details(res.result.details);
                    };
                }).fail(function (a, b, c) {
                    alert(1);
                });
            }
            loadData();
        }
    </script>
</body>
</html>
