<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="KNSHotelMasterPage.Master" CodeBehind="hotel_order_xianshi.aspx.cs" Inherits="WeiXinPF.Web.weixin.KNSHotel.hotel_order_xianshi" %>

<asp:Content ID="h" ContentPlaceHolderID="head" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8">
    <title>在线预订成功</title>
    <meta name="viewport" content="width=device-width,height=device-height,inital-scale=1.0,maximum-scale=1.0,user-scalable=no;">
    <meta name="apple-mobile-web-app-capable" content="yes">
    <meta name="apple-mobile-web-app-status-bar-style" content="black">
    <meta name="format-detection" content="telephone=no">
    <link href="css/hotels.css" rel="stylesheet" type="text/css">
    <link href="../../scripts/swiper/swiper.min.css" rel="stylesheet" />
    <script src="../../scripts/swiper/swiper.min.js"></script>
    <%--    <script src="http://libs.baidu.com/jquery/2.0.0/jquery.min.js" type="text/javascript"></script>--%>
    <script src="../../scripts/jquery/jquery-2.1.0.min.js"></script>
    <script src="../../scripts/jquery/jquery.qrcode-0.12.0.min.js"></script>
    <style>
        .silde-center {
            /*margin-bottom: 30px;*/
            /*margin-top: 25px;*/
            text-align: center;
        }

        .silde-background
        {
        padding: 10px 0;
        }
          .text-danger {
            color: #a94442;
        }

        .text-success {
            color: #3c763d;
        }

        .text-muted {
            color: #777;
        }

        .text-right {
            text-align: right;
        }


        .pull-left {
            float: left !important;
        }

        .pull-right {
            float: right !important;
        }
    </style>
</asp:Content>

<asp:Content ID="c" ContentPlaceHolderID="content" runat="server" class="mode_webapp">
    <div class="qiandaobanner">
        <a href="index.php?ac=hotelslist&amp;c=o99epjjmjWhMPNzoQbo9r6DAEYds&amp;tid=1563">
            <img src="http://img.ishangtong.com/images/RTqs2yHIc9.jpg" /></a>
    </div>
    <div class="cardexplain">
       

        <ul class="round">
            <li class="title"><span class="none"><%=createtime %> 订单详情<%=zhuangtai %></span></li>
            <li class="dandanb">
                <div class="silde-background">



                    <div class="swiper-container silde-center gpd-ablum swiper-container-horizontal">
                        <div class="swiper-wrapper " style="transition-duration: 0ms; transform: translate3d(0px, 0px, 0px);" id="swiperContain">
                            <div class="swiper-slide">
                                <input type="hidden" value="1">
                                <input type="hidden" value="未使用">
                            </div>
                            <div class="swiper-slide">
                                <input type="hidden" value="2">
                                <input type="hidden" value="已使用">
                            </div>
                            <div class="swiper-slide">
                                <input type="hidden" value="2">
                                <input type="hidden" value="已失效">
                            </div>
                        </div>
                         <!-- Add Pagination -->
                        <div class="swiper-pagination" id="swiperFooter">
                        </div>
                        <!-- Add Arrows -->
                <div class="swiper-button-next"></div>
                <div class="swiper-button-prev"></div>
                    </div>
                </div>
                <div class="full-w">
                    <p class="text-uppercase">
                        验证码：<strong id="ticketCode" class="text-danger item-pullleft"></strong><strong class="text-danger pull-right item-pullleft">未使用</strong>
                    </p>
                </div>
            </li>
            <li class="dandanb">
                <table width="100%" border="0" cellspacing="0" cellpadding="0" class="kuang">
                    <tr>
                        <th>联系人</th>
                        <td class="userinfo"><%=truename %></td>
                    </tr>
                </table>
            </li>
            <li class="dandanb"><a href="tel:12345678977"><span>
                <table width="100%" border="0" cellspacing="0" cellpadding="0" class="kuang">
                    <tr>
                        <th>联系电话</th>
                        <td class="userinfo"><%=tel %></td>
                    </tr>
                </table>
            </span></a>
            </li>
            <li class="dandanb">
                <table width="100%" border="0" cellspacing="0" cellpadding="0" class="kuang">
                    <tr>
                        <th>入住时间</th>
                        <td class="userinfo"><%=rztime %></td>
                    </tr>
                </table>
            </li>
            <li class="dandanb">
                <table width="100%" border="0" cellspacing="0" cellpadding="0" class="kuang">
                    <tr>
                        <th>离店时间</th>
                        <td class="userinfo"><%=ldtime %></td>
                    </tr>
                </table>
            </li>
            <li class="dandanb">
                <table width="100%" border="0" cellspacing="0" cellpadding="0" class="kuang">
                    <tr>
                        <th>房间类型</th>
                        <td class="userinfo"><%=roomtype %></td>
                    </tr>
                </table>
            </li>
            <li class="dandanb">
                <table width="100%" border="0" cellspacing="0" cellpadding="0" class="kuang">
                    <tr>
                        <th>预订数量</th>
                        <td class="userinfo"><%=num %>间</td>
                    </tr>
                </table>
            </li>
            <li class="nob">
                <table width="100%" border="0" cellspacing="0" cellpadding="0" class="kuang">
                    <tr>
                        <th>原价</th>
                        <td class="userinfo">￥<%=yuanjia %></td>
                    </tr>
                </table>
            </li>
            <li class="nob">
                <table width="100%" border="0" cellspacing="0" cellpadding="0" class="kuang">
                    <tr>
                        <th>现价</th>
                        <td class="userinfo price">￥<%=price %></td>
                    </tr>
                </table>
            </li>
            <li class="nob">
                <table width="100%" border="0" cellspacing="0" cellpadding="0" class="kuang">
                    <tr>
                        <th>为你节省</th>
                        <td class="userinfo price2" id="price3">￥<%=jiesheng %></td>
                    </tr>
                </table>
            </li>


            <li class="dandanb">
                <table width="100%" border="0" cellspacing="0" cellpadding="0" class="kuang">
                    <tr>
                        <th valign="top" class="thtop">备注</th>
                        <td class="userinfo pm"><%=beizhu %></td>
                    </tr>
                </table>
            </li>
        </ul>
        <input type="hidden" name="formhash" id="formhash" value="77ee642e" />
        <%--<ul class="round">
<li class="title"><span class="none">商家留言</span></li>
<li>
<span class="green none"><p class="time">时间：06月17日 16:43</p></span>
</li>
</ul>--%>
        <div class="footReturn">
            <a id="showcard" class="del">删除此订单</a>
            <div class="window" id="windowcenter">
                <div id="title" class="wtitle">删除成功<span class="close" id="alertclose"></span></div>
                <div class="content">
                    <div id="txt"></div>
                </div>
            </div>
        </div>
        <input type="hidden" runat="server" id="dingdanidnum" value="" />
        <script type="text/javascript">
            var oLay = document.getElementById("overlay");
            $(document).ready(function () {

                //------todo:判断订单详情状态为已支付
                renderQrcode();
                initswiper();
                //------

                $("#showcard").click(function () {


                    var dingdanidnum = document.getElementById('dingdanidnum').value;

                    var submitData = {

                        dingdanidnum: dingdanidnum,
                        myact: "dingdandelete"
                    };
                    $.post('hotel_info.ashx', submitData,
                     function (data) {
                         if (data.ret == "ok") {
                             alert(data.content);

                             window.location.href = "hotel_order.aspx?openid=<%=openid%>&hotelid=<%=hotelid%>&roomid=<%=roomid%>";

                         } else { alert(data.content); }
                     },
                "json");

                    oLay.style.display = "block";

                });
            });

             $("#windowclosebutton").click(function () {
                 $("#windowcenter").slideUp(500);
                 oLay.style.display = "none";

             });
             $("#alertclose").click(function () {
                 $("#windowcenter").slideUp(500);
                 oLay.style.display = "none";

             });

             function alert(title) {

                 $("#windowcenter").slideToggle("slow");
                 $("#txt").html(title);
                 //$("#windowcenter").hide("slow"); 
                 setTimeout('$("#windowcenter").slideUp(500)', 4000);
             }

             function initswiper() {
                 var swiper = new Swiper('.swiper-container', {
//                     autoplay: 3000,
                     loop: false,
                      nextButton: '.swiper-button-next',
                             prevButton: '.swiper-button-prev',
                     pagination: '.swiper-pagination',
                     paginationElement : 'li',
                     onTransitionEnd: function (swi) {
                         var current = swi.slides[swi.activeIndex];
                         var code=current.children[0].value;
                         $("#ticketCode").html(code);
                     }
                 });
             }

             function renderQrcode() {
                 $(".swiper-container .swiper-slide").each(function () {
//                     var t = $(this).find('input:hidden').val();
                      var t = $(this).html();
                     $(this).qrcode({
                         "size": 200,
                         "color": "#3a3",
                         "text": t
                     });
                 });
             }

        </script>
    </div>
    <script src="index/js/plugback.js" type="text/javascript" type="text/javascript"></script>
</asp:Content>
