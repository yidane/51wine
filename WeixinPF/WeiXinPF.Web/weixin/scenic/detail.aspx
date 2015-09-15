<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="detail.aspx.cs" Inherits="WeiXinPF.Web.weixin.scenic.detail" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0, user-scalable=no, minimum-scale=1.0, maximum-scale=1.0">
    <title><%=Detail.Name %></title>
    <style>
        * {
            margin: 0;
            padding: 0;
        }

        html {
            font-size: 62.5%;
            -webkit-text-size-adjust: 100%;
            text-size-adjust: 100%;
        }

        body {
        }

        a {
            -webkit-tap-highlight-color: transparent;
        }

        .bg {
            position: fixed;
            width: 100%;
            height: 100%;
            top: 0;
            left: 0;
            z-index: -1;
        }

            .bg img {
                width: 100%;
                height: 100%;
                position: absolute;
                top: 0;
                left: 0;
                z-index: 0;
            }

        .progress {
            width: 100%;
            position: relative;
        }

            .progress img {
                width: 100%;
            }

        .container {
            padding: 15px;
        }

        .caption {
            height: 40px;
            overflow: hidden;
            position: relative;
        }

            .caption .listen {
                position: absolute;
                right: 0;
                top: 0;
                width: 40px;
                height: 40px;
                background: url(images/listen.png) 0 0 no-repeat;
                background-size: 40px 40px;
                cursor: pointer;
            }

                .caption .listen.close {
                    background-image: url(images/listen_close.png);
                }

            .caption .title {
                line-height: 30px;
                margin-top: 5px;
                padding: 0 10px;
                font-size: 1.625rem;
                font-weight: bold;
                color: #fff;
                border-left: 4px solid #f00;
            }

        .imgbox {
            width: 100%;
            position: relative;
            margin: 15px 0 30px !important;
            height: 220px;
            border: 2px solid #fff;
            border-radius: 6px;
        }

        .box-item {
            width: 100%;
            height: 100%;
            border-radius: 6px;
            -moz-box-sizing: border-box;
            -webkit-box-sizing: border-box;
            box-sizing: border-box;
            position: absolute;
            /*opacity: 0.6;*/
        }

        /*.imgbox .box-item:nth-child(1) {
            left: -10px;
            top: 0;
        }

        .imgbox .box-item:nth-child(2) {
            left: -5px;
            top: 5px;
        }

        .imgbox .box-item:nth-child(3) {
            left: 0;
            top: 10px;
        }

        .imgbox .box-item:nth-child(4) {
            left: 5px;
            top: 15px;
        }

        .imgbox .box-item:last-child {
            opacity: 1;
        }*/

        .imgbox img {
            width: 100%;
            height: 100%;
            border-radius: 6px;
        }

        .produce {
            position: relative;
            line-height: 1.5;
            font-size: 1.4rem;
            color: #D4D2FF;
            padding: 10px;
        }

            .produce p {
                white-space: pre-wrap;
            }

            .produce .border-left-top {
                position: absolute;
                width: 60px;
                height: 60px;
                top: 0;
                left: -2px;
                background: url(images/border-left-top.png) 0 0 no-repeat;
            }

            .produce .border-right-bottom {
                position: absolute;
                width: 60px;
                height: 60px;
                right: 0;
                bottom: -2px;
                background: url(images/border-right-bottom.png) 0 0 no-repeat;
            }

        @media screen and (min-width: 375px) {
            .imgbox {
                height: 260px;
            }
        }

        @media screen and (min-width: 414px) {
            .imgbox {
                height: 290px;
            }
        }
    </style>
    <link href="css/swiper.min.css" rel="stylesheet" />
</head>
<body>
    <div class="bg">
        <img src="<%=Detail.BackgroundImage %>" alt="">
    </div>
    <div class="container">
        <div class="progress">
            <img src="images/step4.png" alt="">
        </div>
        <div class="caption">
            <div class="listen" onclick="music.pause"></div>
            <div class="title"><%=Detail.Name %></div>
        </div>
        <asp:Repeater ID="rpList" runat="server">
            <HeaderTemplate>
                <div class="imgbox swiper-container">
                    <div class="swiper-wrapper">
            </HeaderTemplate>
            <ItemTemplate>
                <div class="box-item swiper-slide">
                    <img src="<%#Eval("PicPath") %>" alt="">
                </div>
            </ItemTemplate>
            <FooterTemplate>
                </div>
                     <div class="swiper-pagination"></div>
                </div>
            </FooterTemplate>
        </asp:Repeater>
        <div class="produce">
            <span class="border-left-top"></span>
            <span class="border-right-bottom"></span>
            <div id="divContent" runat="server">
            </div>
        </div>
    </div>
    <script src="../../scripts/jquery/jquery-2.1.0.min.js"></script>
    <script src="js/swiper.min.js"></script>
    <script type="text/javascript">
        var music = {
            play: function () {
                if (!music.player) {
                    music.player = new Audio();
                    music.player.src = '<%=Detail.Audio%>';
                    music.player.loop = <%=Detail.LoopAudio.ToString().ToLower()%>;
                }
                music.player && music.player.play();
            },
            pause: function () {
                if (music.player) {
                    music.player.pause();
                }
            }
        }
        $(function () {
            var autoplay=<%=Detail.AutoAudio.ToString().ToLower()%>;
            if(autoplay){
                music.play();
            }else{
                $this.addClass('close');
            }
            $('.listen').on('click', function () {
                var $this = $(this);
                if ($this.hasClass('close')) {
                    music.play();
                    $this.removeClass('close');
                }
                else {
                    music.pause();
                    $this.addClass('close');
                }
            });

            var swiper = new Swiper('.swiper-container', {
                autoplay: 5000, loop: true,
                pagination: '.swiper-pagination'
            });
        });
    </script>
</body>
</html>
