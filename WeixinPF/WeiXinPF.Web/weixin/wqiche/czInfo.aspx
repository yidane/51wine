<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="czInfo.aspx.cs" Inherits="WeiXinPF.Web.weixin.wqiche.czInfo" %>

<%@ Import Namespace="WeiXinPF.Common" %>

<!DOCTYPE html>
<html>
<head>
    <meta charset="utf-8" />
    <meta content="no-cache,must-revalidate" http-equiv="Cache-Control">
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8">
    <meta name="viewport" content="width=device-width,height=device-height,inital-scale=1.0,maximum-scale=1.0,user-scalable=no">
    <meta name="apple-mobile-web-app-capable" content="yes">
    <meta name="apple-mobile-web-app-status-bar-style" content="black">
    <meta name="format-detection" content="telephone=no">
    <link rel="stylesheet" type="text/css" href="css/reset.css" media="all" />
    <link rel="stylesheet" type="text/css" href="css/common.css" media="all" />
    <link rel="stylesheet" type="text/css" href="css/datepicker_car.css" media="all" />
    <script src="js/jquery.js"></script>
    <script src="js/jquery_ui.js"></script>
    <script src="js/comm.js"></script>
    <script src="js/linkagesel-min.js"></script>
    <script type="text/javascript" src="js/category.js"></script>
    <script type="text/javascript" src="js/bootstrap-datepicker.js"></script>
    <title>车主信息</title>
    <meta content="text/html; charset=utf-8" http-equiv="Content-Type" />
    <meta content="width=device-width, initial-scale=1.0, minimum-scale=1.0, maximum-scale=1.0, user-scalable=no" name="viewport">
    <style>
        img {
            width: 100%!important;
        }

        .spanfont {
            color: #000;
            font-weight: bold;
        }
    </style>
</head>
<body onselectstart="return true;" ondragstart="return false;">
    <script type="text/javascript">
        $(document).ready(function () { 

            var now = new Date();
            var nowArr = [now.getFullYear(), now.getMonth() + 1, now.getDate()];
            $("#dpd1,#dpd2").each(function (k, v) { 
                var ndate = $(v).datepicker({
                    format: "yyyy-mm-dd",
                    onRender: function (date) {
                        return date.valueOf() > now.valueOf() ? 'disabled' : '';
                    }
                }).on("changeDate", function (date) {
                    ndate.datepicker('hide');
                }); 
            });  
             
            //初始化品牌-车系-车型
            var pinpai = $("#car_model").val();
            getChexi(pinpai);   

            //品牌得到车系
            $("#car_model").change(function () {
                var pid = $(this).val();
                getChexi(pid); 
            })

            //车系得到车型
            $("#car_type").change(function () {
                var xid = $(this).val();
                getChexing(xid); 
            })

        });

        //根据品牌得到车系
        function getChexi(pinpai){
            $.post("cldata.ashx?myact=chexi", { pid: pinpai,wid:<%=wid%> }, function (data) { 
                var str='';
                var flag=false;
                if('<%=czModel.cxid%>'=='0'){
                    str+="<option value=''>请选择车系.....</option>";
                }

                for (var i = 0; i < data.length; i++) {
                    if(data[i].Id=='<%=czModel.cxid%>'){
                        str+="<option value='"+data[i].Id+"' selected='selected'>"+data[i].Name+"</option><br/>"; 
                        flag=true;
                    }else{
                        str+="<option value='"+data[i].Id+"'>"+data[i].Name+"</option><br/>"; 
                    }
                }

                if(data.length>0){
                    if('<%=czModel.cxingid%>'=='0'){ 
                        getChexing(0);
                    }else if(flag){
                        getChexing(<%=czModel.cxid%>);
                    }else{
                        getChexing(data[0].Id);
                    }
            }

                $("#car_type").html(str);
            },"json");
    }

    //根据车系得到车型
    function getChexing(xid){
        str='';
        if(xid==0){
            str+="<option value=''>请选择车型.....</option>";
        }

        $.post("cldata.ashx?myact=chexing", { xid: xid,wid:<%=wid%> }, function (data) {
            var str='';
            if(data!=null&&data.length>0){
                for (var i = 0; i < data.length; i++) { 
                    if(data[i].Id=='<%=czModel.cxingid%>'){
                        str+="<option value='"+data[i].Id+"' selected='selected'>"+data[i].Name+"</option><br/>"; 
                    }else{
                        str+="<option value='"+data[i].Id+"'>"+data[i].Name+"</option><br/>"; 
                    }
                }
            }else{
                str+="<option value=''>暂无车型</option>";
            }
            $("#car_xing").html(str);
        },"json");
    } 

    function checkInfo() {
            
        var form = document.getElementById("form1");
        var obj = {
            car_model: form.car_model.value, 
            car_type: form.car_type.value,
            car_xing: form.car_xing.value,
            car_no: trim(form.car_no.value),
            tel:form.user_tel.value,
            car_userName: trim(form.car_userName.value),
            car_startTime: form.car_startTime.value,
            car_buyTime: form.car_buyTime.value,
            wid:<%=wid%>,
            openid:'<%=openid%>'
        } 
        if (obj.car_no.length <= 0) {
            alert("请输入车牌号码"); return false;
        }
        if (obj.car_userName.length <= 0) {
            alert("请输入车主姓名"); return false;
        }
        $.post("cldata.ashx?myact=addChezhu",obj,function(data){ 
            alert(data.content);
        },"json");
            

           
    }

    function trim(str) {
        str = str.length ? str : "";
        return str.replace(/^\s+|\s+$/gi, "");
    }
    function validNo(str) {
        str += str;
        var yes = (/^\d+$/g.test(str));
        return yes;
    }

    </script>

    <body onselectstart="return true;" ondragstart="return false;">
        <div id="bookBody" class="body">
            <section>
                <form id="form1" action="javascript:checkInfo()" method="post" enctype="multipart/form-data">
                    <input type="hidden" name="wecha_id" value="okiB6uA0hnixn13MUDRzxQNR9zaY" />
                    <input type="hidden" name="token" value="agpkhy1417835915" />
                    <div class="pb_5 pl_10 pr_10 pt_10">
                        <dl class="list_book">
                            <dt>
                                <label>添加车型/车系</label></dt>
                            <dd>
                                <div>
                                    <!--品牌-->
                                    <select name="car_model" id="car_model">
                                        <option value="">请选择品牌..</option>
                                        <asp:Repeater ID="rptPinpai" runat="server" OnItemDataBound="rptPinpai_ItemDataBound">
                                            <ItemTemplate>
                                                <asp:Literal ID="litShow" runat="server"></asp:Literal>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </select>
                                    <!--车系-->
                                    <select name="car_type" id="car_type"></select>
                                    <!--车型-->
                                    <select name="car_xing" id="car_xing"></select>
                                </div>
                            </dd>
                        </dl>
                    </div>
                    <!--dd-->
                    <div class="pb_5 pl_10 pr_10">
                        <dl class="list_book">
                            <dt>
                                <label>基本信息</label></dt>
                            <dd class="tbox">
                                <div>
                                    <label>车牌号码</label>
                                </div>
                                <div>
                                    <input type="text" name="car_no" placeholder="如：皖A88888" maxlength="15" value="<%=czModel.cpNum %>" />
                                </div>
                            </dd>
                            <dd class="tbox">
                                <div>
                                    <label>车主姓名</label>
                                </div>
                                <div>
                                    <input type="text" name="car_userName" placeholder="如：韦强" maxlength="15" value="<%=czModel.Name %>" />
                                </div>
                            </dd>
                            <dd class="tbox">
                                <div>
                                    <label>车主号码</label>
                                </div>
                                <div>
                                    <input type="text" name="user_tel" placeholder="请填写您的电话号码" maxlength="11" value="<%=czModel.teltephone %>" />
                                </div>
                            </dd>
                            <dd class="tbox">
                                <div>
                                    <label>上牌时间</label>
                                </div>
                                <div>
                                    <input type="text" name="car_startTime" id="dpd1" readonly="readonly" value="<%=spdate %>" />
                                </div>
                            </dd>
                            <dd class="tbox">
                                <div>
                                    <label>购车时间</label>
                                </div>
                                <div>
                                    <input type="text" name="car_buyTime" id="dpd2" readonly="readonly" value="<%=gcdate %>" />
                                </div>
                            </dd>

                        </dl>
                    </div>
                    <!--ee-->
                    <div class="pb_5 pl_10 pr_10">
                        <dl class="list_book">
                            <dt>
                                <label>保险信息</label></dt>
                            <dd class="tbox">
                                <div>
                                    <label>上次保险日期</label>
                                </div>
                                <div>
                                    <span class="spanfont"><%=MyCommFun.Obj2DateTime(czModel.prevBxdate).ToString("yyyy-MM-dd") %></span>
                                </div>
                            </dd>
                            <dd class="tbox">
                                <div>
                                    <label>上次保险费用</label>
                                </div>
                                <div style="width: 100%;">
                                    <span class="spanfont"><%=czModel.prevBxmoney %></span><i>(RMB)</i>
                                </div>
                                <div></div>
                            </dd>
                            <dd class="tbox">
                                <div>
                                    <label>上次年检时间</label>
                                </div>
                                <div style="width: 100%;">
                                    <span class="spanfont"><%=MyCommFun.Obj2DateTime(czModel.prevNjdate).ToString("yyyy-MM-dd") %></span>
                                </div>
                                <div></div>
                            </dd>
                        </dl>
                    </div>

                    <!--rr-->
                    <div class="pb_5 pl_10 pr_10">
                        <dl class="list_book">
                            <dt>
                                <label>保养信息</label></dt>
                            <dd class="tbox">
                                <div>
                                    <label>上次保养里程</label>
                                </div>
                                <div style="width: 100%;">
                                    <span class="spanfont"><%=czModel.prevBylicheng %></span><i>(公里)</i>
                                </div>
                                <div>
                                    <pre></pre>
                                </div>
                            </dd>
                            <dd class="tbox">
                                <div>
                                    <label>上次保养时间</label>
                                </div>
                                <div>
                                    <span class="spanfont"><%=MyCommFun.Obj2DateTime(czModel.prevBydate).ToString("yyyy-MM-dd") %></span>
                                </div>
                            </dd>
                            <dd class="tbox">
                                <div>
                                    <label>上次保养费用</label>
                                </div>
                                <div style="width: 100%;">
                                    <span class="spanfont"><%=czModel.prevBymoney %></span><i>(RMB)</i>
                                </div>
                                <div></div>
                            </dd>
                        </dl>
                    </div>

                    <div style="margin: 10px; text-align: center;">
                        <input type="submit" value="确定" class="btn_submit">
                        <br>
                        <br>
                    </div>
                    <input type="hidden" name="__hash__" value="11d278991823d823e4af73976589741f_b8d95804e937a3a63ef779a9446770de" />
                </form>
            </section>
            <%--<span class="copyright">版权归我所有        </span>--%>
        </div>

    </body>
</body>
</html>
