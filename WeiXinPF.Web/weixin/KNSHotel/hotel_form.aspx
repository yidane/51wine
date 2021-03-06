﻿<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="KNSHotelMasterPage.Master" CodeBehind="hotel_form.aspx.cs" Inherits="WeiXinPF.Web.weixin.KNSHotel.hotel_form" %>

<asp:Content ID="h" ContentPlaceHolderID="head" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8">
    <title>在线预订</title>
    <meta name="viewport" content="width=device-width,height=device-height,inital-scale=1.0,maximum-scale=1.0,user-scalable=no;">
    <meta name="apple-mobile-web-app-capable" content="yes">
    <meta name="apple-mobile-web-app-status-bar-style" content="black">
    <meta name="format-detection" content="telephone=no">
    <link href="css/hotels.css?2013.11.19" rel="stylesheet" type="text/css">

    <link href="../../scripts/bootstrap/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="../../scripts/bootstrap/datetimepicker/css/bootstrap-datetimepicker.min.css" rel="stylesheet" type="text/css" />

    <link href="css/mystyle.css" rel="stylesheet" type="text/css">
    <script src="js/iscroll.js" type="text/javascript"></script>
    <script type="text/javascript" src="../../scripts/bootstrap/js/bootstrap.min.js"></script>
    <script type="text/javascript" src="../../scripts/bootstrap/datetimepicker/js/bootstrap-datetimepicker.min.js"></script>
    <script type="text/javascript" src="../../scripts/bootstrap/datetimepicker/js/locales/bootstrap-datetimepicker.zh-CN.js"></script>
    <style>
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
            margin-bottom: 11px;
            border: 1px solid transparent;
            border-radius: 10px;
        }

        .header-img {
            max-height: 200px;
        }
    </style>
    <script type="text/javascript">
        var myScroll;

        function loaded() {
            myScroll = new iScroll('wrapper', {
                snap: true,
                momentum: false,
                hScrollbar: false,
                onScrollEnd: function () {
                    document.querySelector('#indicator > li.active').className = '';
                    document.querySelector('#indicator > li:nth-child(' + (this.currPageX +1) + ')').className = 'active';                   
                }
            });


        }

        document.addEventListener('DOMContentLoaded', loaded, false);
    </script>
</asp:Content>

<asp:Content ID="c" ContentPlaceHolderID="content" runat="server" class="mode_webapp">
    <div class="banner">
        <div id="wrapper">
            <div id="scroller">
                <ul id="thelist">
                    <%=tupian %>
                </ul>
            </div>
        </div>
        <div id="nav">
            <div id="prev" onclick="myScroll.scrollToPage('prev', 0,400,2);return false; ">&larr; prev</div>
            <ul id="indicator">
                <%=tabid %>
            </ul>
            <div id="next" onclick="myScroll.scrollToPage('next', 0);return false; ">next &rarr;</div>
        </div>
        <div class="clr"></div>
    </div>


    <div class="cardexplain">


        <!--商家房价及类型-->
        <ul class="round">
            <li class="biaotou bradius pad">
                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td>类型</td>
                        <td class="yuanjia">原价</td>
                        <td class="youhuijia">优惠价</td>
                    </tr>
                </table>
            </li>
            <li><span class="noneorder">
                <table class="jiagebiao" width="100%" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td><%=roomtype %></td>
                        <td class="yuanjia">￥<%=yuanjia %></td>
                        <td class="youhuijia">￥<%=xianjia %></td>
                    </tr>
                </table>
            </span>
            </li>

        </ul>

        <!--后台可控制是否显示-->


        <div class="detailcontent">
            <h2 class="gpd-item-title">
                <img class="detailicon-ticket" src="../restaurant/images/info.png" />
                <span class="gpd-item-title-name">商品说明</span>
                <span class="gp-icons gpd-up-icon"></span>
            </h2>
            <div class="content  gpd-content"><%=peitao %> </div>
        </div>

        <div class="detailcontent">
            <h2 class="gpd-item-title">
                <img class="detailicon-ticket" src="../restaurant/images/info.png" />
                <span class="gpd-item-title-name">使用须知</span>
                <span class="gp-icons gpd-up-icon"></span>

            </h2>

            <div class="content gpd-content">
                <%=UseInstruction %>
                <%--                <p>【取票方式】</p>--%>
                <%--                <p>喀纳斯景区内所有景点，注：家房屋需单独购票</p>--%>
                <%--                <p><span>【购票条件】</span></p>--%>
                <%--                <p>特惠半价票，适用于1.2米以下的儿童以及65-70岁老人</p>--%>
                <%--                <p>【入园】</p>--%>
                <%--                <p>微信购票后凭电子票二维码扫码入园</p>--%>
                <%--                <p>【发票】</p>--%>
                <%--                <p>微信购票暂不支持开具发票，敬请期待后续优化</p>--%>
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
                <%--                <p>1、支付成功后，可在票据有效期内申请退票，过期则视为作废不予受理退票</p>--%>
                <%--                <p>2、可在我的订单中申请退票，申请后会先审核</p>--%>
                <%--                <p>3、工作人员会在1~2个工作日内处理您的退票申请</p>--%>
                <%--                <p>4、审核通过后，支付款额会自动退回微信钱包</p>--%>
                <%--                <p>5、微信门票一经扫码入园后，不予退票，如二进票入园一次后即不予退票</p>--%>
            </div>
        </div>


        <ul class="round">
            <li class="tel"><a href="tel:<%=hoteltel %>"><span><%=hoteltel %> 电话预订</span></a></li>
        </ul>



        <input type="hidden" name="formhash" id="formhash" value="7de8fa52" />
        <input type="hidden" name="issub" id="issub" value="0" />

        <div class='alert alert-warning' role='alert'>
            <strong>提示</strong> 填写入住其中一人的真实信息即可。
        </div>
        <div class="footer-money">

            <p>
                <span class=" color-red">已优惠</span>
                <span id="price3" class="color-red discount-money">￥0</span>
                <span>共</span>
                <span id="price" class="  total-money color-red">￥0</span>
                <span id="yuanjia" class="  cost-money">￥0</span>

            </p>

        </div>
        <ul class="round">
            <li class="title mb"><span class="none">请认真填写在线订单</span></li>
            <li class="nob">
                <table width="100%" border="0" cellspacing="0" cellpadding="0" class="kuang">
                    <tr>
                        <th>预订人</th>
                        <td>
                            <input name="oderName" type="text" class="px" id="oderName" runat="server" value="" placeholder="请输入您的真实姓名" /></td>
                    </tr>
                </table>
            </li>
            <li class="nob">
                <table width="100%" border="0" cellspacing="0" cellpadding="0" class="kuang">
                    <tr>
                        <th>身份证号</th>
                        <td>
                            <input name="identityNumber" type="text" class="px" runat="server" onblur="checkidcard()" required id="identityNumber" value="" placeholder="请输入您的身份证号" /></td>
                    </tr>
                </table>
            </li>
            <li class="nob">
                <table width="100%" border="0" cellspacing="0" cellpadding="0" class="kuang">
                    <tr>
                        <th>联系电话</th>
                        <td>
                            <input name="tel" type="text" class="px" id="tel" runat="server" value="" onblur="checkphone()" required placeholder="请输入您的电话" /></td>
                    </tr>
                </table>
            </li>
            <li class="nob">
                <table width="100%" border="0" cellspacing="0" cellpadding="0" class="kuang">
                    <tr>
                        <th>入住时间</th>
                        <td>
                            <input name="arriveTime" class="px datetimepicker" id="arriveTime" value="" readonly="readonly" type="text" placeholder="入住时间" />
                        </td>
                    </tr>
                </table>
            </li>
            <li class="nob">
                <table width="100%" border="0" cellspacing="0" cellpadding="0" class="kuang">
                    <tr>
                        <th>离店时间</th>
                        <td>
                            <input name="leaveTime" class="px datetimepicker" id="leaveTime" value="" readonly="readonly" type="text" placeholder="离店时间" />
                        </td>
                    </tr>
                </table>
            </li>
            <li class="nob">
                <table width="100%" border="0" cellspacing="0" cellpadding="0" class="kuang">
                    <tr>
                        <th>类型</th>
                        <td>
                            <input class="px" id="roomtype" value="<%=roomtype %>" type="text" readonly="true" /></td>
                    </tr>
                </table>
            </li>
            <li class="nob">
                <table width="100%" border="0" cellspacing="0" cellpadding="0" class="kuang">
                    <tr>
                        <th>预订数量</th>
                        <td>
                            <select name="orderNum" class="dropdown-select" id="orderNum" onchange="dothis(this.value)">
                                <option value="1">1</option>
                                <option value="2">2</option>
                                <option value="3">3</option>
                                <option value="4">4</option>
                                <option value="5">5</option>
                                <option value="6">6</option>
                                <option value="7">7</option>
                                <option value="8">8</option>
                                <option value="9">9</option>
                                <option value="10">10</option>
                                <option value="11">11</option>
                                <option value="12">12</option>
                                <option value="13">13</option>
                                <option value="14">14</option>
                                <option value="15">15</option>
                                <option value="16">16</option>
                                <option value="17">17</option>
                                <option value="18">18</option>
                                <option value="19">19</option>
                                <option value="20">20</option>
                            </select></td>
                    </tr>
                </table>
            </li>
            <li class="nob">
                <table width="100%" border="0" cellspacing="0" cellpadding="0" class="kuang">
                    <tr>
                        <th>单价</th>
                        <td class="userinfo price">￥<%=xianjia %></td>
                    </tr>
                </table>
            </li>




            <li class="nob">
                <table width="100%" border="0" cellspacing="0" cellpadding="0" class="kuang">
                    <tr>
                        <th valign="middle" class="thtop">备注</th>
                        <td>
                            <textarea name="remark" class="pxtextarea" style="height: 99px; overflow-y: visible" id="remark" placeholder="请输入备注信息"></textarea></td>
                    </tr>
                </table>
            </li>
        </ul>



        <div class="footReturn">
            <a id="showcard" class="submit">提交订单</a>
            <div class="window" id="windowcenter">
                <div id="title" class="wtitle">提示<span class="close" id="alertclose"></span></div>
                <div class="content">
                    <div id="txt"></div>
                </div>
            </div>

        </div>

        <script type="text/javascript">
            var clickedBtn = false;
            function dothis(nums) {
                if(checkValue())
                {
                    var arriveTime=$("#arriveTime").val();
                    var leaveTime=$("#leaveTime").val();
                    var span=Date.parse(leaveTime)-Date.parse(arriveTime);
                    var days=Math.floor(span/(24*3600*1000));
                    changeprice(nums,days);

                }

            }

            function changeprice(nums,timespan)
            {
                if(!nums||nums<=0)
                {
                    //  alert('请选择数量');
                    return;
                }
                if(!timespan||timespan<=0)
                {
                    alert("离店时间不能小于入住时间！");
                    return ;
                }
                var yuanjia = <%=yuanjia %>  ;
                var xianjia = <%=xianjia %>  ;
                var price3 = <%=price3 %>  ;

                var str1 = yuanjia * nums*timespan;
                var str2 = xianjia * nums*timespan;
                var str3 = price3 * nums*timespan;
                $("#yuanjia").text("￥" + str1);
                $("#price").text("￥" + str2);
                $("#price3").text("￥" + str3);
            }



            function checkValue(){
                var result =false;
                var arriveTime=$("#arriveTime").val();
                var leaveTime=$("#leaveTime").val();
                var orderNum=$("#orderNum").val();
                if(arriveTime&&leaveTime&&orderNum)
                {
                    result=true;
                }
                return result;
            }

            function changePriceUseDate(arriveTime,leaveTime)
            {
                if(checkValue())
                {
                    var span=Date.parse(leaveTime)-Date.parse(arriveTime);
                    var days=Math.floor(span/(24*3600*1000));
                    var orderNum=$("#orderNum").val();

                    changeprice(orderNum,days);
                }



            }

            function checkDate(dateVal)
            {
                var result=false;
                var date=  Date.parse(dateVal);
                var now=new Date();
                if(date>addDate(now,-1))
                {
                    result=true;
                }

                return result;
            }

            function addDate(date,days){ 
                var val=  date.setDate(date.getDate()+days); 
                return val; 
            }

            var oLay = document.getElementById("overlay");
            $(document).ready(function () {



                $('.datetimepicker').datetimepicker({
                    minView: "month", //选择日期后，不会再跳转去选择时分秒 
                    format: "yyyy/mm/dd", //选择日期后，文本框显示的日期格式 
                    language: 'zh-CN', //汉化 
                    autoclose: true //选择日期后自动关闭
                })
                .on('changeDate', function(ev){
                    if(ev.currentTarget.id=='arriveTime')
                    {
                        var arriveTime=$(ev.currentTarget).val();
                        if( checkDate(arriveTime))
                        {
                            var leaveTime=$("#leaveTime").val();
                            changePriceUseDate(arriveTime,leaveTime);
                        }
                        else{
                            alert("入住时间不能小于今天！");
                            return false;
                        }

                    }
                    else  if(ev.currentTarget.id=='leaveTime')
                    {

                        var leaveTime=$(ev.currentTarget).val();
                        if( checkDate(leaveTime))
                        {
                            var arriveTime=$("#arriveTime").val();
                            changePriceUseDate(arriveTime,leaveTime);
                        }else{
                            alert("离店时间不能小于今天！");
                            return false;
                        }

                    }
                });
                //显影
                $(".gpd-item-title").click(function () {
                    var zThis = $(this);
                    var zThisParent = zThis.parents(".detailcontent");
                    zThisParent.toggleClass("gdp-curr");
                    zThisParent.siblings(".detailcontent").removeClass("gdp-curr");
                });


                $("#showcard").click(function () {

                    
                 

                    var arriveTime=$("#arriveTime").val();
                    var leaveTime=$("#leaveTime").val();
                    if(!checkDate(arriveTime))
                    {
                        alert("入住时间不能小于今天！");
                        return false;
                    }

                    if(!checkDate(leaveTime))
                    {
                        alert("离店时间不能小于今天！");
                        return false;
                    }

                    if (arriveTime >= leaveTime)
                    {
                        alert("离店时间不能小于入住时间！");
                        return false;
                       
                    }
                    if ($('#<%= oderName.ClientID %>').val() == '') { alert('名字不能为空'); return; }
                    //验证身份证
                    if(!checkidcard())
                    {
                        return;
                    }


                    //验证电话
                    if(!checkphone())
                    {
                        return;
                    }
                 
                    if ($("#arriveTime").val() == '') { alert('请选择入住时间'); return; }
                    if ($("#leaveTime").val() == '') { alert('请选择离店时间'); return; }



                   

                  

                       
                    if (clickedBtn) {
                        alert('请勿重复提交！谢谢！');
                        return false;
                    } else {
                        clickedBtn = true;
                    }

                   
                    var oderName = document.getElementById('<%= oderName.ClientID %>').value;
                    var identityNumber = document.getElementById('<%= identityNumber.ClientID %>').value;
                    var tel = document.getElementById('<%= tel.ClientID %>').value;
                    var arriveTime = document.getElementById('arriveTime').value;
                    var leaveTime = document.getElementById('leaveTime').value;

                    var roomType = '<%=roomtype %>';
                    var openid = '<%= string.IsNullOrEmpty(openid) ? "''" : openid  %>';
                    var roomid = <%=roomid %>;
                    var hotelid = <%=hotelid %>;

                    var orderNum = document.getElementById('orderNum').value;
                    var price = <%=xianjia %>;
                    var yuanjia = <%=yuanjia %>;
                    var remark = document.getElementById('remark').value;


                    var submitData = {
                        oderName: oderName,
                        identityNumber: identityNumber,
                        tel: tel,
                        arriveTime: arriveTime,
                        leaveTime: leaveTime,
                        orderNum: orderNum,
                        roomType: roomType,
                        openid: openid,
                        roomid: roomid,
                        hotelid: hotelid,
                        price: price,
                        yuanjia: yuanjia,
                        remark: remark,
                        myact: "dingdan"
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


             var count = $("#thelist img").size();
             $("#thelist img").css("width", document.body.clientWidth);
             $("#scroller").css("width", document.body.clientWidth * count);
             setInterval(function () {
                 myScroll.scrollToPage('next', 0, 400, count);
             }, 3500);
             window.onresize = function () {

                 $("#thelist img").css("width", document.body.clientWidth);
                 $("#scroller").css("width", document.body.clientWidth * count);
             };



             function checkphone()
             {
                 var result=true;
                 //验证电话
                 
                 var zPhone = $('#<%= tel.ClientID %>');
                 var zPhoneVal = zPhone.val();
                 if (zPhoneVal == "") {
                     alert('电话不能为空');
                     result=false;
                 }
                 if (!(VI.regPhone.test(zPhoneVal))) {
                     alert('电话号码错误');
                     result=false;

                 }
                 return result;
             }


             function checkidcard()
             {
                 var result=true;
                 //验证身份证

                 var zIDCard = $('#<%= identityNumber.ClientID %>');
                 var zIDCardVal = zIDCard.val();
                 if (zIDCardVal == "") {
                     alert('身份证号码不能为空');
                     result=false;
                 }
                 if (!VI.idCard(zIDCardVal)) {
                     alert('身份证号错误');
                     result=false;

                 }

                 return result;
             }


             var VI = {
               
                 regPhone: /^1[3|4|5|8|9][0-9]\d{8}$/ 
             };

             VI.idCard = function (str) {
                 var wi = [7, 9, 10, 5, 8, 4, 2, 1, 6, 3, 7, 9, 10, 5, 8, 4, 2],
                     xi = [1, 0, "X", 9, 8, 7, 6, 5, 4, 3, 2],
                     pi = [11, 12, 13, 14, 15, 21, 22, 23, 31, 32, 33, 34, 35, 36, 37, 41, 42, 43, 44, 45, 46, 50, 51, 52, 53, 54, 61, 62, 63, 64, 65, 71, 81, 82, 91];

                 if (str.match(/^\d{14,17}(\d|X)$/gi) == null) return false;

                 var brithday18 = function (tmpl) {
                     var year = parseFloat(tmpl.substr(6, 4));
                     var month = parseFloat(tmpl.substr(10, 2));
                     var day = parseFloat(tmpl.substr(12, 2));
                     var checkDay = new Date(year, month - 1, day);
                     var nowDay = new Date();
                     if (1900 <= year && year <= nowDay.getFullYear() && month == (checkDay.getMonth() + 1) && day == checkDay.getDate()) {
                         return true;
                     }
                     ;

                     return false;
                 };

                 var brithday15 = function (tmpl) {
                     var year = parseFloat(tmpl.substr(6, 2));
                     var month = parseFloat(tmpl.substr(8, 2));
                     var day = parseFloat(tmpl.substr(10, 2));
                     var checkDay = new Date(year, month - 1, day);
                     if (month == (checkDay.getMonth() + 1) && day == checkDay.getDate()) {
                         return true;
                     }
                     ;

                     return false;
                 };

                 var validate = function (tmpl) {

                     var aIdCard = tmpl.split("");
                     var sum = 0;
                     for (var i = 0; i < wi.length; i++) {
                         sum += wi[i] * aIdCard[i]; //线性加权求和  
                     }
                     ;
                     var index = sum % 11; //求模，可能为0~10,可求对应的校验码是否于身份证的校验码匹配  
                     if (xi[index] == aIdCard[17].toUpperCase()) {
                         return true;
                     }
                     ;

                     return false;
                 };

                 var province = function (tmpl) {
                     var p2 = tmpl.substr(0, 2);
                     for (var i = 0; i < pi.length; i++) {
                         if (pi[i] == p2) {
                             return true;
                         };
                     };

                     return false;
                 };

                 if (str.length == 18 && province(str) && brithday18(str) && validate(str)) return true;

                 if (str.length == 15 && province(str) && brithday15(str)) return true;

                 return false;
             };

        </script>
    </div>
    <script type="text/javascript">
        document.addEventListener('WeixinJSBridgeReady', function onBridgeReady() {
            WeixinJSBridge.call('hideToolbar');
        });
    </script>

</asp:Content>
