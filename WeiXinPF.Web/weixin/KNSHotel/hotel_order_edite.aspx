<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="KNSHotelMasterPage.Master" CodeBehind="hotel_order_edite.aspx.cs" Inherits="WeiXinPF.Web.weixin.KNSHotel.hotel_order_edite" %>

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
    <style>
          .header-img {
            max-height: 150px;
        }
    </style>
</asp:Content>

<asp:Content ID="c" ContentPlaceHolderID="content" runat="server" class="mode_webapp">
    <div class="qiandaobanner">
        <img class="header-img" src="<%=image %>" /> 
    </div>
    

    <div class="cardexplain">
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
                            <input name="arriveTime" class="px datetimepicker" id="arriveTime"  readonly="readonly"  value="<%=arriveTime %>" type="text" placeholder="到店日期"/>
                        </td>
                    </tr>
                </table>
            </li>
             <li class="nob">
                <table width="100%" border="0" cellspacing="0" cellpadding="0" class="kuang">
                    <tr>
                        <th>离店日期</th>
                        <td>
                            <input name="leaveTime" class="px datetimepicker" id="leaveTime"  readonly="readonly"  value="<%=leaveTime %>" type="text" placeholder="离店日期"/>
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
                             <select name="orderNum" class="dropdown-select" id="orderNum" onchange="javascript:dothis(this.value);">
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
                        <td class="userinfo price" id="price2">￥<%=price %></td>
                    </tr>
                </table>
            </li>

            <li class="nob">
                <table width="100%" border="0" cellspacing="0" cellpadding="0" class="kuang">
                    <tr>
                        <th valign="middle" class="thtop">备注</th>
                        <td>
                            <textarea name="info" class="pxtextarea" style="height: 99px; overflow-y: visible" id="info" runat="server" placeholder="请输入备注信息"></textarea></td>
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
                <div id="title" class="wtitle">提示<span class="close" id="alertclose"></span></div>
                <div class="content">
                    <div id="txt"></div>
                </div>
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
                            var xianjia = <%=price %>  ;
                            var jiesheng = <%=jiesheng %>  ;

                        var str1 = yuanjia * nums*timespan;
                                        var str2 = xianjia * nums*timespan;
                                        var str3 = jiesheng * nums*timespan;
                                        $("#yuanjia").text("￥" + str1);
                                        $("#price").text("￥" + str2);
                                        $("#jiesheng").text("￥" + str3);
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

                    var nums=<%=nums%>;
                    $("#orderNum").val(nums);

                    $('.datetimepicker').datetimepicker({
                        minView: "month", //选择日期后，不会再跳转去选择时分秒
                        format: "yyyy/mm/dd", //选择日期后，文本框显示的日期格式
                        language: 'zh-CN', //汉化
                        autoclose: true //选择日期后自动关闭
                    })  .on('changeDate', function(ev){
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

                                        if ($("#truename").val() == '') { alert('名字不能为空'); return; }
                                        if ($("#identityNumber").val() == '') { alert('身份证号码不能为空'); return; }
                                        if ($("#tel").val() == '') { alert('电话不能为空'); return; }
                                        if ($("#arriveTime").val() == '') { alert('请选择入住时间'); return; }
                                        if ($("#leaveTime").val() == '') { alert('请选择离店时间'); return; }


                           
                                        if (clickedBtn) {
                                            alert('请勿重复提交！谢谢！');
                                            return false;
                                        } else {
                                            clickedBtn = true;
                                        }

                        var truename = document.getElementById('truename').value;
                        var tel = document.getElementById('tel').value;
                        var arriveTime = document.getElementById('arriveTime').value;
                        var leaveTime = document.getElementById('leaveTime').value;

                        //var roomType = document.getElementById('roomtypenum').value;
                        var openid = '<%=string.IsNullOrEmpty(openid) ? "''" : openid %>';
                        var roomid = <%=roomid %>;
                        var hotelid = <%=hotelid %>;

                        var nums = document.getElementById('orderNum').value;
                        var xianjianum = <%=price %>;
                        var yuanjianum = <%=yuanjia %>;
                        var dingdanidnum = <%=dingdanid %>;
                        var info = document.getElementById('ctl00_content_info').value;
                        var identitiyNumber =document.getElementById('identityNumber').value;


                        var submitData = {
                            truename: truename,
                            tel: tel,
                            arriveTime: arriveTime,
                            leaveTime:leaveTime,
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
                if (clickedBtn) {
                    alert('请勿重复提交！谢谢！');
                    return false;
                } else {
                    clickedBtn = true;
                }

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
</asp:Content>
