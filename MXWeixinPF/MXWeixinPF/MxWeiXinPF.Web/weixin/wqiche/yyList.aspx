<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="yyList.aspx.cs" Inherits="MxWeiXinPF.Web.weixin.wqiche.yyList" %>

<%@ Import Namespace="MxWeiXinPF.Common" %>
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
    <link rel="stylesheet" type="text/css" href="css/datepicker.css" media="all" />
    <script type="text/javascript" src="js/jQuery.js"></script>
    <link rel="stylesheet" type="text/css" href="css/easy-responsive-tabs.css" media="all">
    <script type="text/javascript" src="js/easyResponsiveTabs.js"></script>
    <script src="../../scripts/jquery/alert.js"></script>
    <title>预约列表</title>

    <style>
        img {
            width: 100%!important;
        }
    </style>
</head>
<body onselectstart="return true;" ondragstart="return false;">

    <body onselectstart="return true;" ondragstart="return false;">
        <div id="bookBody" class="body">
            <header>
                <ul>
                    <li>
                        <img src="<%=img %>" style="width: 100%;" /></li>
                </ul>
            </header>

            <section>
                <div class="p_10">
                    <form id="form1" action="#" method="post">
                        <div id="horizontalTab">
                            <asp:Repeater ID="rptList" runat="server" OnItemDataBound="rptList_ItemDataBound">
                                <ItemTemplate>
                                    <ul class="resp-tabs-list">
                                        <li><%#MyCommFun.Obj2DateTime(Eval("createdate")).ToString("yyyy-MM-dd") %>订单详情  <a href="javascript:;" style="background-color: #ccc; float: right; margin-right: 0px; color: #000; line-height: 20px; border-radius: 8px; margin-top: 3px; padding: 0 5px;">
                                            <asp:Literal ID="litStatus" runat="server"></asp:Literal></a></li>
                                    </ul>
                                    <div class="resp-tabs-container">
                                        <div>
                                            <dl class="list_book">
                                                <dt class="ofh">
                                                    <label style="width: 150px;">&nbsp;</label>
                                                </dt>
                                                <dd class="tbox">
                                                    <div>
                                                        <label>联系人:</label>
                                                    </div>
                                                    <div>
                                                        <label><%#Eval("Name") %></label>
                                                    </div>
                                                </dd>
                                                <dd class="tbox">
                                                    <div>
                                                        <label>联系电话：</label>
                                                    </div>
                                                    <div><a href="tel:<%#Eval("telephone") %>" style="color: #000;"><%#Eval("telephone") %></a></div>
                                                </dd>
                                                <dd class="tbox">
                                                    <div>
                                                        <label>品牌/车系：</label>
                                                    </div>
                                                    <div>
                                                        <label><%#Eval("pinpai") %>/<%#Eval("chexi") %></label>
                                                    </div>
                                                </dd>
                                                <dd class="tbox">
                                                    <div>
                                                        <label>车型：</label>
                                                    </div>
                                                    <div>
                                                        <label></label>
                                                    </div>
                                                </dd>
                                                <asp:Literal ID="litShow" runat="server"></asp:Literal>
                                                <dd class="tbox">
                                                    <div>
                                                        <label>预约日期：</label>
                                                    </div>
                                                    <div>
                                                        <label><%#MyCommFun.Obj2DateTime(Eval("yydate")).ToString("yyyy-MM-dd") %> <%#Eval("yytime") %></label>
                                                    </div>
                                                </dd>
                                                <dd class="tbox">
                                                    <div>
                                                        <label>提交时间：</label>
                                                    </div>
                                                    <div>
                                                        <label><%#MyCommFun.Obj2DateTime(Eval("createdate")).ToString("yyyy年MM月dd日") %></label>
                                                    </div>
                                                </dd>
                                                <div>
                                                    <li class="box group_btn">
                                                        <div>
                                                        </div>
                                                        <div><a href="" onclick="delOrder(<%#Eval("id") %>)" id="showcard2" class="gray">删除订单</a></div>
                                                    </li>
                                                </div>
                                            </dl>
                                        </div>
                                    </div>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <%#rptList.Items.Count == 0 ? "<p>您还没有预约</p>" : ""%>
                                </FooterTemplate>
                            </asp:Repeater>
                        </div>
                        <script type="text/javascript">
                            $(document).ready(function () {
                                $('#horizontalTab').easyResponsiveTabs({
                                    type: 'accordion', //Types: default, vertical, accordion
                                    width: 'auto', //auto or any width like 600px
                                    fit: true,   // 100% fit in a container
                                    closed: 'accordion', // Start closed if in accordion view
                                    activate: function (event) { // Callback function if tab is switched
                                        var $tab = $(this);
                                        var $info = $('#tabInfo');
                                        var $name = $('span', $info); 
                                        $name.text($tab.text()); 
                                        $info.show();
                                    }
                                });
                            });

                            function delOrder(id) {
                                var res=window.confirm('确定要删除吗?');
                                if(res){
                                    $.post("cldata.ashx?myact=delorder", {wid:<%=wid%>,id:id}, function (data) { 
                                        setTimeout(function(){
                                            this.window.location.href="yyList.aspx?wid=<%=wid%>&openid=<%=openid%>&type=<%=type%>";
                                        },800); 
                                    }, "json");
                                }
                            }

                        </script>
                        <input type="hidden" name="__hash__" value="3e147248f46ca26e6927cf71981462a0_a3a75312995283de19e664247359bc76" />
                    </form>
                </div>
            </section>
        </div>

    </body>
    <div mark="stat_code" style="width: 0px; height: 0px; display: none;">
    </div>
</body>
</html>
