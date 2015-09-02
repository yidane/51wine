<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="yyBaoyang.aspx.cs" Inherits="MxWeiXinPF.Web.weixin.wqiche.yyBaoyang" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <meta charset="utf-8" />
    <meta content="no-cache,must-revalidate" http-equiv="Cache-Control">
    <meta name="viewport" content="width=device-width,height=device-height,inital-scale=1.0,maximum-scale=1.0,user-scalable=no;">
    <meta name="apple-mobile-web-app-capable" content="yes">
    <meta name="apple-mobile-web-app-status-bar-style" content="black">
    <meta name="format-detection" content="telephone=no">
    <link rel="stylesheet" type="text/css" href="css/reset.css" media="all" />
    <link rel="stylesheet" type="text/css" href="css/common.css" media="all" />
    <link rel="stylesheet" type="text/css" href="css/datepicker_car.css" media="all" />
    <script src="js/jquery.js"></script>
    <script src="js/comm.js"></script>
    <script src="js/linkagesel-min.js"></script>
    <script type="text/javascript" src="js/jquery_ui.js"></script>
    <script type="text/javascript" src="js/category.js"></script>
    <script type="text/javascript" src="js/bootstrap-datepicker.js"></script>
    <title></title>
    <style>
        img {
            width: 100%!important;
        }
    </style>
</head>
<body onselectstart="return true;" ondragstart="return false;">
    <style>
        #bookBody .pb_5 {
            padding-bottom: 10px!important;
        }
    </style>

    <script>
        $().ready(function () {
            var nowTemp = new Date();
            var now = new Date(nowTemp.getFullYear(), nowTemp.getMonth(), nowTemp.getDate(), 0, 0, 0, 0);
            var ndate = $('#dateline').datepicker({
                format: "yyyy-mm-dd",
                onRender: function (date) {
                    return date.valueOf() < now.valueOf() ? 'disabled' : '';
                }
            }).on("changeDate", function (date) {
                ndate.datepicker('hide');
            });

            //初始化品牌车系
            var pinpai= $("#pid").val();
            getChexi(pinpai);

            //品牌得到车系
            $("#pid").change(function () {
                var pid = $(this).val();
                getChexi(pid);
            })
        });

        function getChexi(pinpai){
            $.post("cldata.ashx?myact=chexi", { pid: pinpai,wid:<%=wid%> }, function (data) {
                var str="";
                for (var i = 0; i < data.length; i++) {
                    str+="<option value='"+data[i].Id+"'>"+data[i].Name+"</option><br/>";
                }
                $("#xid").html(str);
            },"json");
        }

        function submit() {
            var form = document.getElementById("form1");
            var obj = { truename: '', tel: '', dateline: '', xid: '', pid: '', type: '', timepart: '', info: '', carnum: '', km: '' };

            if ('undefined' !== typeof (form.truename)) {
                obj.truename = form.truename.value;
                if (obj.truename != null && obj.truename.length <= 0) {
                    alert("请输入您的真实姓名"); return;
                }
            }
            if ('undefined' !== typeof (form.tel)) {
                obj.tel = form.tel.value;
                if (obj.tel != null && obj.tel.length <= 7) {
                    alert("请输入正确的电话号码"); return;
                }
            }
            if ('undefined' !== typeof (form.dateline)) {
                obj.dateline = form.dateline.value;
                if (obj.dateline != null && obj.dateline.length <= 7) {
                    alert("请输入预约日期"); return;
                }
            }
            if ('undefined' !== typeof (form.pid)) {
                obj.pid = form.pid.value;
            }
            if ('undefined' !== typeof (form.carnum)) {
                obj.carnum = form.carnum.value;
            }
            if ('undefined' !== typeof (form.km)) {
                obj.km = form.km.value;
            }
            if ('undefined' !== typeof (form.type)) {
                obj.type = form.type.value;
            }
            if ('undefined' !== typeof (form.xid)) {
                obj.xid = form.xid.value;
            }

            if ('undefined' !== typeof (form.timepart)) {
                obj.timepart = form.timepart.value;
            }

            if ('undefined' !== typeof (form.info)) {
                obj.info = form.info.value;
            }
            var obj = {
                truename: form.truename.value,
                tel: form.tel.value,
                dateline: form.dateline.value,
                xid: form.xid.value,//车系
                pid: form.pid.value,          //品牌
                type: form.type.value,
                carnum: form.carnum.value,
                km: form.km.value,
                timepart: form.timepart.value,
                info: form.info.value,
                wid:<%=wid%>,
                openid:'<%=openid%>'
            }

            $.post("cldata.ashx?myact=addorder", obj,
                function (data) {
                    if ('0' == data.errno) {
                        alert('添加成功!'); 
                        setTimeout(function () {
                            jumpurl("yyList.aspx?wid=<%=wid%>&openid=<%=openid%>&type=<%=type%>");
                        }, 1500);
                        return;
                    } else {
                        alert('添加失败!');
                    }
                },
            "json");

            } 
            function jumpurl(url) {
                window.location.href = url;
            }
    </script>
    <body onselectstart="return true;" ondragstart="return false;">
        <div id="bookBody" class="body">
            <header>
                <ul>
                    <li>
                        <img src="<%=yyModel.coverpic %>" style="width: 100%;" /></li>
                </ul>
            </header>
            <section>
                <div class="pt_5 pb_5 pl_10 pr_10">
                    <dl class="list_book list_book_my">
                        <dd class="tbox">
                            <div>
                                <label>我的订单</label>
                            </div>
                            <div class="align_right"><a href="yyList.aspx?wid=<%=wid %>&openid=<%=openid %>&type=<%=type %>"><%=ddnum %></a></div>
                        </dd>
                    </dl>
                </div>
                <div class="pb_5 pl_10 pr_10">
                    <dl class="list_book list_book_des">
                        <dd>
                            <div>
                                <label>订单说明</label>
                            </div>
                            <div style="word-break: break-all; word-wrap: nowrap; white-space: normal; padding: 15px 0;"><%=yyModel.remark %></div>
                        </dd>
                    </dl>
                </div>
                <div class="pb_5 pl_10 pr_10">
                    <dl class="list_book list_book_contact">
                        <dd>
                            <div>
                                <a href="">
                                    <span>地址：<%=yyModel.address %></span>
                                </a>
                            </div>
                            <div style="padding-top: 3px;"><a href="tel:<%=yyModel.telephone %>"><span>订单电话： <%=yyModel.telephone %></span></a></div>
                        </dd>
                    </dl>
                </div>

                <div class="pb_5 pl_10 pr_10">
                    <dl class="list_book list_book_my">
                        <dd class="round roundyellow">
                            <div>
                                <a href=""><span>请完善个人资料再填表单</span></a>
                            </div>
                        </dd>
                    </dl>
                </div>
                <div class="pb_5 pl_10 pr_10">
                    <form id="form1" action="javascript:submit();" method="post">
                        <input type="hidden" name="type" id="type" value="<%=type %>" />
                        <dl class="list_book">
                            <dt>
                                <label>请认真填写表单</label></dt>
                            <dd class="tbox">
                                <div>
                                    <label>联系人:</label>
                                </div>
                                <div>
                                    <input type="text" name="truename" placeholder="请输入您的真实姓名" value="" />
                                </div>
                            </dd>
                            <dd class="tbox">
                                <div>
                                    <label>联系电话：</label>
                                </div>
                                <div>
                                    <input type="tel" name="tel" placeholder="请输入您的电话" value="" />
                                </div>
                            </dd>

                            <dd class="tbox">
                                <div>
                                    <label>品牌/车系</label>
                                </div>
                                <div>
                                    <select name="pid" id="pid" class="input-medium" data-rule-required="true">
                                        <asp:Repeater ID="rptPinpai" runat="server">
                                            <ItemTemplate>
                                                <option value="<%#Eval("id") %>"><%#Eval("name") %></option>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </select>
                                    <select name="xid" id="xid" class="input-medium" data-rule-required="true"></select>
                                </div>
                            </dd>
                            <asp:Literal ID="litShow" runat="server"></asp:Literal>
                            <dd class="tbox">
                                <div>
                                    <label>预约日期：</label>
                                </div>
                                <div>
                                    <input type="text" name="dateline" id="dateline" readonly="readonly" placeholder="" value="" />
                                </div>
                            </dd>
                            <dd class="tbox">
                                <div>
                                    <label>预约时间：</label>
                                </div>
                                <div>
                                    <select name="timepart">
                                        <option value="0:00-1:00">0:00-1:00</option>
                                        <option value="1:00-2:00">1:00-2:00</option>
                                        <option value="2:00-3:00">2:00-3:00</option>
                                        <option value="3:00-4:00">3:00-4:00</option>
                                        <option value="4:00-5:00">4:00-5:00</option>
                                        <option value="5:00-6:00">5:00-6:00</option>
                                        <option value="6:00-7:00">6:00-7:00</option>
                                        <option value="7:00-8:00">7:00-8:00</option>
                                        <option value="8:00-9:00">8:00-9:00</option>
                                        <option value="9:00-10:00">9:00-10:00</option>
                                        <option value="10:00-11:00">10:00-11:00</option>
                                        <option value="11:00-12:00">11:00-12:00</option>
                                        <option value="12:00-13:00">12:00-13:00</option>
                                        <option value="13:00-14:00">13:00-14:00</option>
                                        <option value="14:00-15:00">14:00-15:00</option>
                                        <option value="15:00-16:00">15:00-16:00</option>
                                        <option value="16:00-17:00">16:00-17:00</option>
                                        <option value="17:00-18:00">17:00-18:00</option>
                                        <option value="18:00-19:00">18:00-19:00</option>
                                        <option value="19:00-20:00">19:00-20:00</option>
                                        <option value="20:00-21:00">20:00-21:00</option>
                                        <option value="21:00-22:00">21:00-22:00</option>
                                        <option value="22:00-23:00">22:00-23:00</option>
                                        <option value="23:00-24:00">23:00-24:00</option>
                                    </select>
                                </div>
                            </dd>
                            <dd class="tbox">
                                <div>
                                    <label>备注：</label>
                                </div>
                                <div>
                                    <textarea name="info" placeholder="请输入备注信息" style="height: 80px;"></textarea>
                                </div>
                            </dd>
                        </dl>
                        <div style="margin: 10px; text-align: center;">
                            <input type="submit" value="提交订单" class="btn_submit" style="margin: 10px auto; text-align: center; cursor: pointer" />
                            
                        </div>
                    </form>
                </div>
            </section>
<%--            <span class="copyright" style="text-align: center;">版权归我所有        </span>--%>
        </div>
    </body>
</html>
