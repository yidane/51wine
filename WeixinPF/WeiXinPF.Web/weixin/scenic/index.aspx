<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="WeiXinPF.Web.weixin.scenic.index" %>

<%@ Import Namespace="WeiXinPF.Common" %>
<!DOCTYPE html>

<html>
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>景区导览</title>
    <meta id="eqMobileViewport" name="viewport" content="width=device-width,initial-scale=1.0,minimum-scale=1.0, maximum-scale=1.0, user-scalable=no">
    <link href="../../scripts/swiper/swiper.min.css" rel="stylesheet" />
    <link href="../../scripts/swiper/animate.min.css" rel="stylesheet" />
    <link href="css/index.css" rel="stylesheet" />
</head>
<body>
    <div class="swiper-container" id="swiper-outter">
        <div class="swiper-wrapper">
            <section class="swiper-slide" id="swiper-slide1" data-hash="1">
                <div class="circle ani" swiper-animate-effect="zoomIn" swiper-animate-duration="4s">
                    <img class="scenic-name" src="images/scenic_name.png" />
                    <img class="stamp" src="images/stamp.png" />
                    <img class="poetry" src="images/poetry.png" />
                </div>
            </section>
            <section class="swiper-slide" id="swiper-slide2" data-hash="2">
                <div class="info" id="info">
                </div>
                <div class="swiper-container" id="swiper-inner">
                    <div class="swiper-wrapper">
                    </div>
                    <div class="btn-prev-inner">
                        <img src="images/btn_arrow.png" alt="" class="small-botton prev" />
                    </div>
                    <div class="btn-next-inner">
                        <img src="images/btn_arrow.png" alt="" class="small-botton next" />
                    </div>
                </div>
            </section>
        </div>
        <a id="btnNext">
            <img src="images/btn_arrow.png" alt="" class="bottom" />
        </a>
    </div>
    <script src="../../scripts/jquery/jquery-2.1.0.min.js"></script>
    <script src="../../scripts/swiper/swiper.min.js"></script>
    <script src="../../scripts/swiper/swiper.animate1.0.2.min.js"></script>
    <script type="text/javascript">
        function initSwiper() {
            var mySwiper1 = new Swiper('#swiper-outter', {
                direction: "vertical",
                speed: 1000,
                hashnav: true,
                nextButton: '#btnNext',
                lazyLoading: true,
                lazyLoadingInPrevNext: true,
                lazyLoadingOnTransitionStart: true,
                onInit: function (swiper) {
                    swiperAnimateCache(swiper);
                    swiperAnimate(swiper);
                },
                onSlideChangeEnd: function (swiper) {
                    swiperAnimate(swiper);
                },
                onTransitionEnd: function (swiper) {
                    swiperAnimate(swiper);
                }
            });
            var mySwiper2 = new Swiper('#swiper-inner', {
                speed: 1000,
                heigth: 340,
                prevButton: '.btn-prev-inner',
                nextButton: '.btn-next-inner',
                lazyLoading: true,
                lazyLoadingInPrevNext: true,
                lazyLoadingOnTransitionStart: true
            });
        }

        $(function () {
            $.ajax({
                url: 'scenic.ashx?rid=' + Math.random(),
                data: { action: 'GetScenic', id: <%=MyCommFun.RequestInt("id") %> },
                dataType: "json"
            }).done(function (res) {
                if (res && res.isSuccess) {
                    var data = res.data;
                    $("#swiper-slide1").css("background-image", "url(" + data.scenic.firstBgImg + ")");
                    $("#swiper-slide2").css("background-image", "url(" + data.scenic.secondBgImg + ")");
                    $("#info").html(data.scenic.description);

                    var splitGroupNumber = Math.ceil(data.details.length / 4);
                    var htmlContainer = [];
                    for (var i = 1; i <= splitGroupNumber; i++) {
                        var start = (i - 1) * 4;
                        var details = data.details.slice(start, start + 4);

                        htmlContainer.push('<div class="swiper-slide">');
                        htmlContainer.push('<ul class="scenic-list">');
                        for (var index = 0; index < details.length; index++) {
                            var detail = details[index];
                            htmlContainer.push('<li>');
                            htmlContainer.push('    <a href="detail.aspx?id=' + detail.id + '">');
                            htmlContainer.push('        <div class="annular-round">');
                            htmlContainer.push('            <img src="' + detail.cover + '" />');
                            htmlContainer.push('        </div>');
                            htmlContainer.push('        <span>' + detail.name + '</span>');
                            htmlContainer.push('    </a>');
                            htmlContainer.push('</li>');
                        }
                        htmlContainer.push('</div>');
                        htmlContainer.push('</ul>');
                    }
                    $("#swiper-inner>.swiper-wrapper").html(htmlContainer.join(''));

                    initSwiper();
                }
            }).fail(function () {
                alert('获取配置信息失败。');
            });
        });
    </script>
</body>
</html>

