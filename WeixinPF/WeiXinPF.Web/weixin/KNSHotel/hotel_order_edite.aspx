﻿<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="KNSHotelMasterPage.Master" CodeBehind="hotel_order_edite.aspx.cs" Inherits="WeiXinPF.Web.weixin.KNSHotel.hotel_order_edite" %>

<asp:Content ID="h" ContentPlaceHolderID="head" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8">
    <title>在线预订未处理</title>
    <meta name="viewport" content="width=device-width,height=device-height,inital-scale=1.0,maximum-scale=1.0,user-scalable=no;">
    <meta name="apple-mobile-web-app-capable" content="yes">
    <meta name="apple-mobile-web-app-status-bar-style" content="black">
    <meta name="format-detection" content="telephone=no">
    <link href="css/hotels.css" rel="stylesheet" type="text/css">
    <script src="http://libs.baidu.com/jquery/2.0.0/jquery.min.js" type="text/javascript"></script>

    <link href="../../scripts/bootstrap/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="../../scripts/bootstrap/datetimepicker/css/bootstrap-datetimepicker.min.css" rel="stylesheet" type="text/css" />

    <link href="css/mystyle.css" rel="stylesheet" type="text/css">

      <script type="text/javascript" src="../../scripts/bootstrap/js/bootstrap.min.js"></script>
    <script type="text/javascript" src="../../scripts/bootstrap/datetimepicker/js/bootstrap-datetimepicker.min.js"></script>
    <script type="text/javascript" src="../../scripts/bootstrap/datetimepicker/js/locales/bootstrap-datetimepicker.zh-CN.js"></script>

</asp:Content>

<asp:Content ID="c" ContentPlaceHolderID="content" runat="server" class="mode_webapp">
    <div class="qiandaobanner">
        <img src="<%=image %>" /> 
    </div>
    
    <div class="header">
        <span class="pCount"></span>
        <label><i>共计：</i><b id="totalPrice" class="duiqi"><%=totalPrice %></b><b class="duiqi">元</b></label>
    </div>
    <div class="cardexplain">
        <ul class="round">
            <li class="title mb"><span class="none">订单号：<%=OrderNumber %><%=zhuangtai %></span></li>
            <li class="nob">
                <div class="beizhu">此订单可重新修改后再提交！</div>
            </li>
            <li class="nob">
                <table width="100%" border="0" cellspacing="0" cellpadding="0" class="kuang">
                    <tr>
                        <th>预订人</th>
                        <td>
                            <input name="truename" type="text" class="px" id="truename" value="<%=truename %>" placeholder="请输入您的真实姓名"/></td>
                    </tr>
                </table>
            </li>
            <li class="nob">
                <table width="100%" border="0" cellspacing="0" cellpadding="0" class="kuang">
                    <tr>
                        <th>身份证号</th>
                        <td>
                            <input name="identityNumber" class="px" id="identityNumber" value="<%=IdentityNumber %>" type="text" placeholder="请输入您的身份证号码"/></td>
                    </tr>
                </table>
            </li>
            <li class="nob">
                <table width="100%" border="0" cellspacing="0" cellpadding="0" class="kuang">
                    <tr>
                        <th>联系电话</th>
                        <td>
                            <input name="tel" class="px" id="tel" value="<%=tel %>" type="text" placeholder="请输入您的电话"/></td>
                    </tr>
                </table>
            </li>
            <li class="nob">
                <table width="100%" border="0" cellspacing="0" cellpadding="0" class="kuang">
                    <tr>
                        <th>到店日期</th>
                        <td>
                            <input name="dateline" class="px datetimepicker" id="dateline" value="<%=dateline %>" type="text" placeholder="到店日期"/>
                        </td>
                    </tr>
                </table>
            </li>
             <li class="nob">
                <table width="100%" border="0" cellspacing="0" cellpadding="0" class="kuang">
                    <tr>
                        <th>离店日期</th>
                        <td>
                            <input name="leaveTime" class="px datetimepicker" id="leaveTime" value="<%=leaveTime %>" type="text" placeholder="离店日期"/>
                        </td>
                    </tr>
                </table>
            </li>
            <input type="hidden" name="formhash" id="formhash" value="77ee642e" />
            <li class="nob">
                <table width="100%" border="0" cellspacing="0" cellpadding="0" class="kuang">
                    <tr>    
                        <th>房型</th>
                        <td><%=roomtype %></td>
                    </tr>
                </table>
            </li>

            <li class="nob">
                <table width="100%" border="0" cellspacing="0" cellpadding="0" class="kuang">
                    <tr>
                        <th>数量</th>
                        <td>
                            <select name="nums" class="dropdown-select" id="nums" onchange="dothis(<%=nums %>)">
                                <option value="1">1间</option>
                                <option value="2">2间</option>
                                <option value="3">3间</option>
                                <option value="4">4间</option>
                                <option value="5">5间</option>
                                <option value="6">6间</option>
                                <option value="7">7间</option>
                                <option value="8">8间</option>
                                <option value="9">9间</option>
                                <option value="10">10间</option>
                                <option value="11">11间</option>
                                <option value="12">12间</option>
                                <option value="13">13间</option>
                                <option value="14">14间</option>
                                <option value="15">15间</option>
                                <option value="16">16间</option>
                                <option value="17">17间</option>
                                <option value="18">18间</option>
                                <option value="19">19间</option>
                                <option value="20">20间</option>
                            </select></td>
                    </tr>
                </table>
            </li>
            <li class="nob">
                <table width="100%" border="0" cellspacing="0" cellpadding="0" class="kuang">
                    <tr>
                        <th>原价</th>
                        <td class="userinfo" id="price1">￥<%=yuanjia %></td>
                    </tr>
                </table>
            </li>
            <li class="nob">
                <table width="100%" border="0" cellspacing="0" cellpadding="0" class="kuang">
                    <tr>
                        <th>现价</th>
                        <td class="userinfo price" id="price2">￥<%=price %></td>
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


            <li class="nob">
                <table width="100%" border="0" cellspacing="0" cellpadding="0" class="kuang">
                    <tr>
                        <th valign="top" class="thtop">备注</th>
                        <td>
                            <textarea name="info" class="pxtextarea" style="height: 99px; overflow-y: visible" id="info" runat="server" placeholder="请输入备注信息">4534</textarea></td>
                    </tr>
                </table>
            </li>
        </ul>

        <div class="footReturn">
            <ul>
                <li class="footerbtn"><a id="showcard2" class="del right3">取消订单</a></li>
                <li class="footerbtn"><a id="showcard" class="submit left3">提交订单</a></li>
            </ul>
            <div class="clr"></div>
            <div class="window" id="windowcenter">
                <div id="title" class="wtitle">操作成功<span class="close" id="alertclose"></span></div>
                <div class="content">
                    <div id="txt"></div>
                </div>
            </div>
        </div>



<%--        <input type="hidden" runat="server" id="roomidnum" value="" />
        <input type="hidden" runat="server" id="hotelidnum" value="" />
        <input type="hidden" runat="server" id="openidnum" value="" />
        <input type="hidden" runat="server" id="dingdanidnum" value="" />

        <input type="hidden" runat="server" id="yuanjianum" value="" />
        <input type="hidden" runat="server" id="xianjianum" value="" />
        <input type="hidden" runat="server" id="jsnum" value="" />--%>
        <script type="text/javascript">
            function dothis(nums) {
                var str2 = <%=price %> * nums;

                $("#price3").text("￥" + (<%=yuanjia %>-<%=price %>) * nums);
                $("#totalPrice").html(str2);
                
            }

            var oLay = document.getElementById("overlay");
            $(document).ready(function () {


                $('.datetimepicker').datetimepicker({
                    minView: "month", //选择日期后，不会再跳转去选择时分秒 
                    format: "yyyy/mm/dd", //选择日期后，文本框显示的日期格式 
                    language: 'zh-CN', //汉化 
                    autoclose: true //选择日期后自动关闭 
                });

                $("#showcard").click(function () {
                    var truename = document.getElementById('truename').value;
                    var tel = document.getElementById('tel').value;
                    var dateline = document.getElementById('dateline').value;
                    var leaveTime = document.getElementById('leaveTime').value;

                    //var roomType = document.getElementById('roomtypenum').value;
                    var openid = '<%=string.IsNullOrEmpty(openid) ? "''" : openid %>';
                    var roomid = <%=roomid %>;
                    var hotelid = <%=hotelid %>;

                    var nums = document.getElementById('nums').value;
                    var xianjianum = <%=price %>;
                    var yuanjianum = <%=yuanjia %>;
                    var dingdanidnum = <%=dingdanid %>;
                    var info = document.getElementById('info').value;
                    var identitiyNumber =document.getElementById('identityNumber').value;


                    var submitData = {
                        truename: truename,
                        tel: tel,
                        dateline: dateline,
                        openid: openid,
                        roomid: roomid,
                        hotelid: hotelid,
                        nums: nums,
                        xianjianum: xianjianum,
                        yuanjianum: yuanjianum,
                        info: info,
                        dingdanidnum: dingdanidnum,
                        identityNumber: identitiyNumber,
                        myact: "dingdanedite"
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




        $("#showcard2").click(function () {


            var dingdanidnum = <%=dingdanid %>;



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

         setTimeout('$("#windowcenter").slideUp(500)', 4000);
     }

        </script>
    </div>
</asp:Content>
