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

        .silde-background {
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

        .alert-success {
            color: #3c763d;
            background-color: #dff0d8;
            border-color: #d6e9c6;
        }

        .alert-danger {
            color: #a94442;
            background-color: #f2dede;
            border-color: #ebccd1;
        }

        .alert-warning {
            color: #8a6d3b;
            background-color: #fcf8e3;
            border-color: #faebcc;
        }

        .alert-info {
            color: #31708f;
            background-color: #d9edf7;
            border-color: #bce8f1;
        }

        .alert {
            padding: 15px;
            margin-bottom: 20px;
            border: 1px solid transparent;
            border-radius: 10px;
        }

        .top-alert {
            height: 32px;
            line-height: 32px;
            vertical-align: middle;
        }

        .top-alert-name {
            font-weight: 700;
        }

        .label-primary {
            background-color: #337ab7;
        }


        .label-info {
            background-color: #5bc0de;
        }

        .label-success {
            background-color: #5cb85c;
        }

        .label-danger {
            color: #d9534f;
        }

        .label {
            display: inline;
            padding: .2em .6em .3em;
            /*font-size: 75%;*/
            font-weight: 700;
            line-height: 1;
            /*color: #fff;*/
            text-align: center;
            white-space: nowrap;
            vertical-align: baseline;
            border-radius: .25em;
        }


        .swiper-image {
            height: 230px;
        }
    </style>
</asp:Content>

<asp:Content ID="c" ContentPlaceHolderID="content" runat="server" class="mode_webapp">
<%--    <div class="qiandaobanner">--%>
<%--        <a href="index.php?ac=hotelslist&amp;c=o99epjjmjWhMPNzoQbo9r6DAEYds&amp;tid=1563">--%>
<%--            <img src="http://img.ishangtong.com/images/RTqs2yHIc9.jpg" /></a>--%>
<%--    </div>--%>
    <div class="cardexplain">
        <%=AlertOrderMsg %>
        <%--        <div class="alert alert-success top-alert" role="alert">--%>
        <%--            <span style="float: left"><span class="top-alert-name">订单编号：</span> <%=OrderNumber %></span>--%>
        <%--            <span style="float: right"><span class="top-alert-name">总价：</span>--%>
        <%--                <span class="label label-danger"><%=PayAmount %>元</span></span>--%>
        <%--        </div>--%>
        
                    <div class="footer-money">

                                    <p>
                                            <span class=" color-red">已优惠</span>
                                                                                       <span  id="jiesheng" class="color-red discount-money">￥<%=totaljiesheng %></span>
                                    <span>共</span>
                                    <span  id="price" class="  total-money color-red">￥<%=totalPrice %></span>
                                    <span  id="yuanjia" class="  cost-money">￥<%=totalyuanjia %></span>

                                    </p>

                                </div>
        <ul class="round">
            <li class="title"><span class="none">订单号：<%=OrderNumber %><%=zhuangtai %></span></li>
            <li class="dandanb" style="display: <% if (isShowQRCode)
                {%>
            block; <%}%>
                <%
                else
                {%>
               none; <%} %>">
                <div class="silde-background">



                    <div class="swiper-container silde-center gpd-ablum swiper-container-horizontal">
                        <div class="swiper-wrapper " style="transition-duration: 0ms; transform: translate3d(0px, 0px, 0px);" id="swiperContain">
                            <%=VerificationCode %>

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
                        验证码：<strong id="ticketCode" class="text-danger item-pullleft"></strong><strong id="ticket_status" class="pull-right item-pullleft"></strong>
                    </p>
                </div>
            </li>
            <li class="dandanb">
                <table width="100%" border="0" cellspacing="0" cellpadding="0" class="kuang">
                    <tr>
                        <th>预定人</th>
                        <td class="userinfo"><%=truename %></td>
                    </tr>
                </table>
            </li>
              <li class="dandanb"><a href="tel:<%=tel %>"><span>
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
                        <th>身份证号</th>
                       <td class="userinfo"><%=IdentityNumber %></td>
                </table>
            </li>
          
            <li class="dandanb">
                <table width="100%" border="0" cellspacing="0" cellpadding="0" class="kuang">
                    <tr>
                        <th>到店日期</th>
                        <td class="userinfo"><%=rztime %></td>
                    </tr>
                </table>
            </li>
            <li class="dandanb">
                <table width="100%" border="0" cellspacing="0" cellpadding="0" class="kuang">
                    <tr>
                        <th>离店日期</th>
                        <td class="userinfo"><%=ldtime %></td>
                    </tr>
                </table>
            </li>
            <li class="dandanb">
                <table width="100%" border="0" cellspacing="0" cellpadding="0" class="kuang">
                    <tr>
                        <th>房型</th>
                        <td class="userinfo"><%=roomtype %></td>
                    </tr>
                </table>
            </li>
            <li class="dandanb">
                <table width="100%" border="0" cellspacing="0" cellpadding="0" class="kuang">
                    <tr>
                        <th>数量</th>
                        <td class="userinfo"><%=num %>间</td>
                    </tr>
                </table>
            </li>
            <li class="nob" style="display: none">
                <table width="100%" border="0" cellspacing="0" cellpadding="0" class="kuang">
                    <tr>
                        <th>原价</th>
                        <td class="userinfo">￥<%=yuanjia %></td>
                    </tr>
                </table>
            </li>
            <li class="nob" >
                <table width="100%" border="0" cellspacing="0" cellpadding="0" class="kuang">
                    <tr>
                        <th>单价</th>
                        <td class="userinfo price">￥<%=price %></td>
                    </tr>
                </table>
            </li>
            <li class="nob" style="display: none">
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
            <% if (isShowContent)
                {%>
            <li class="addr">
                <a href="/weixin/map/map_address.html?type=hotel&id=<%=hotelid %>">
                <span><%=address %></span></a></li>
            <li class="tel"><a href="tel:<%=hotelTel %>"><span><%=hotelTel %> 电话预订</span></a></li>
            <%}%>
            <%
                else
                {%>

            <%} %>
        </ul>



        <% if (isShowContent)
            {%>

        <div id="detail_content">
            <div class="detailcontent">
                <h2 class="gpd-item-title">
                    <img class="detailicon-ticket" src="../restaurant/images/info.png" />
                    <span class="gpd-item-title-name">使用须知</span>
                    <span class="gp-icons gpd-up-icon"></span>

                </h2>

                <div class="content gpd-content">
                    <%=UseInstruction %>
                </div>
            </div>

            <div class="detailcontent">
                <h2 class="gpd-item-title">
                    <img class="detailicon-ticket" src="../restaurant/images/info.png" />
                    <span class="gpd-item-title-name">商家介绍</span>
                    <span class="gp-icons gpd-up-icon"></span></h2>
                <div class="content gpd-content">
                    <span style="color: #444444; font-family: 'Microsoft YaHei', Helvitica, Verdana, Arial, san-serif; font-size: 14px; font-weight: bold; line-height: 21px; background-color: #FCFCFC;">
                        <%=jieshao %>
                    </span>
                </div>
            </div>

            <div class="detailcontent">
                <h2 class="gpd-item-title">
                    <img class="detailicon-ticket" src="../restaurant/images/undo.png" />
                    <span class="gpd-item-title-name">退单规则</span>
                    <span class="gp-icons gpd-up-icon"></span>

                </h2>

                <div class="content gpd-content">
                    <%=RefundRule %>
                </div>
            </div>
        </div>

        <%}%>
        <%
            else
            {%>

        <%} %>




        <input type="hidden" name="formhash" id="formhash" value="77ee642e" />
        <div class="footReturn">
            <% if (BtnShowStatus == 1)
                {%>
            <a id="showcard" class="del">取消订单</a>
            <%}%>
            <% else if (BtnShowStatus == 2)
                {%>
            <ul>
                <li class="footerbtn"><a id="showcard" class="del right3">取消订单</a></li>
                <li class="footerbtn"><a id="btn_payment" class="submit left3">微信支付</a></li>
            </ul>
            <div class="clr"></div>

            <%}%>
            <%
                else
                {%>

            <%} %>


            <div class="window" id="windowcenter">
                <div id="title" class="wtitle">删除成功<span class="close" id="alertclose"></span></div>
                <div class="content">
                    <div id="txt"></div>
                </div>
            </div>
        </div>
        <input type="hidden" runat="server" id="dingdanidnum" value="" />
        <script type="text/javascript">
             var clickedBtn = false;
            var oLay = document.getElementById("overlay");
            //            <strong id="ticket_status" class="text-danger pull-right item-pullleft">未使用</strong>
            var statusArr = [
            { status: 1, text: '未使用', css: ' #777' },
            { status: 2, text: '已使用', css: '#3c763d' },
            { status: 3, text: '已失效', css: '#a94442' }];


            $(document).ready(function () {

                //------todo:判断订单详情状态为已支付
                renderQrcode();
                initswiper();
                //------

                //显影
                $(".gpd-item-title").click(function () {
                    var zThis = $(this);
                    var zThisParent = zThis.parents(".detailcontent");
                    zThisParent.toggleClass("gdp-curr");
                    zThisParent.siblings(".detailcontent").removeClass("gdp-curr");
                });

                $("#showcard").click(function () {
                    var dingdanidnum = document.getElementById('<%= dingdanidnum.ClientID %>').value;
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



                $("#btn_payment").click(function () {
                    btn_paymentclick();
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
                    paginationElement: 'li',
                    onTransitionEnd: function (swi) {
                        var current = swi.slides[swi.activeIndex];
                        var t = $(current.children[0]);
                        setCodeVal(t);
                    }
                });
            }

             function setCodeVal(t) {
                 var code = '';
                 if (t.is('img')) {
                     code = t.attr('value');
                 } else {
                     code = t.val();
                 }

                 $("#ticketCode").html(code);
                 setStatus(t.attr('status'));
             }

             function renderQrcode() {
                $(".swiper-container .swiper-slide").each(function () {
                    var t = $(this).find('input:hidden').val();
                    //                      var t = $(this).html();
                    if (t) {
                        $(this).qrcode({
                            "size": 200,
                            "color": "#3a3",
                            "text": t
                        });
                    }
                   
                });
                var first = $(".swiper-container .swiper-slide").eq(0).children().first();

                setCodeVal(first); 

            }

            function getStatus(statusid) {
                for (var i = 0; i < statusArr.length; i++) {
                    if (statusArr[i].status == statusid) {
                        return statusArr[i];
                    }
                }
            }

            function setStatus(statusid) {
                var status = getStatus(statusid);
                if (status) {
                    $("#ticket_status").text(status.text);
                    $("#ticket_status").css('color', status.css); 

                }

            }



            //支付点击
            function btn_paymentclick() {


                if (clickedBtn) {
                    alert('请勿重复提交！谢谢！');
                    return false;
                } else {
                    clickedBtn = true;
                }

                var dingdanidnum = document.getElementById('<%= dingdanidnum.ClientID %>').value; 


                var submitData = {

                    dingdanidnum: dingdanidnum,
                    myact: "dingdanPaying"
                };
                $.post('hotel_info.ashx', submitData,
                    function (result) {
                        if (result.IsSuccess) { 
                      
                            document.location.href = result.Data; 
                        } else {
                            alert(data.content); }
                    },
                    "json");


              
                oLay.style.display = "block";
            }



            //获取项目根路径
            function getRootPath(hasProject) {
                //获取当前网址，如： http://localhost:8083/uimcardprj/share/meun.jsp
                var curWwwPath = window.document.location.href;
                //获取主机地址之后的目录，如： uimcardprj/share/meun.jsp
                var pathName = window.document.location.pathname;
                var pos = curWwwPath.indexOf(pathName);
                //获取主机地址，如： http://localhost:8083
                var localhostPaht = curWwwPath.substring(0, pos);
                var result = localhostPaht;
                if (hasProject) {
                    //获取带"/"的项目名，如：/uimcardprj
                    var projectName = pathName.substring(0, pathName.substr(1).indexOf('/') + 1);
                    result += projectName;
                }

                return (result);
            }

        </script>
    </div>

</asp:Content>
