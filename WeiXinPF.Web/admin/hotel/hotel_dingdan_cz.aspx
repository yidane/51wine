<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="hotel_dingdan_cz.aspx.cs" Inherits="WeiXinPF.Web.admin.hotel.hotel_dingdan_cz" %>

<%@ Import Namespace="WeiXinPF.Model.KNSHotel" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>订单处理</title>
    <script type="text/javascript" src="../../scripts/jquery/jquery-2.1.0.min.js"></script>
    <script type="text/javascript" src="../../scripts/jquery/Validform_v5.3.2_min.js"></script>
    <script type="text/javascript" src="../../scripts/lhgdialog/lhgdialog.js?skin=idialog"></script>
    <script type="text/javascript" src="../../scripts/datepicker/WdatePicker.js"></script>
    <script type="text/javascript" src="../../scripts/swfupload/swfupload.js"></script>
    <script type="text/javascript" src="../../scripts/swfupload/swfupload.queue.js"></script>
    <script type="text/javascript" src="../../scripts/swfupload/swfupload.handlers.js"></script>
    <script type="text/javascript" charset="utf-8" src="../../editor/kindeditor-min.js"></script>
    <script type="text/javascript" charset="utf-8" src="../../editor/lang/zh_CN.js"></script>
    <script type="text/javascript" src="../js/layout.js"></script>
    <link href="../skin/default/style.css" rel="stylesheet" type="text/css" />
    <%--    <link href="../skin/mystyle.css" rel="stylesheet" type="text/css" />--%>
    <link href="../../weixin/KNSHotel/css/orderstyle.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        $(function () {
            //初始化表单验证
            $("#form1").initValidform();

            //初始化上传控件
            $(".upload-img").each(function () {
                $(this).InitSWFUpload({ sendurl: "../../tools/upload_ajax.ashx", flashurl: "../../scripts/swfupload/swfupload.swf" });
            });
            $(".upload-album").each(function () {
                $(this).InitSWFUpload({ btntext: "批量上传", btnwidth: 66, single: false, water: true, thumbnail: true, filesize: "2048", sendurl: "../../tools/upload_ajax.ashx", flashurl: "../../scripts/swfupload/swfupload.swf", filetypes: "*.jpg;*.jpge;*.png;*.gif;" });
            });
            $(".attach-button").click(function () {
                showAttachDialog();
            });

            $("#chkIsRefund").click(function () {
                var button = $("#btnSaveRefund");
                if (!button.attr("disabled")) {

                    button.attr("disabled", true);
                } else {

                    button.attr("disabled", false);
                }
            });



        });
    </script>
    <script type="text/javascript">
        function parentToIndex(id) {
            parent.location.href = "/admin/Index.aspx?id=" + id;

        }



    </script>
    <style>
        a.shenghe {
            color: red;
        }

        #StatusType tr td {
            padding-right: 10px;
        }

        .text-danger {
            color: #FF0000;
        }

        .total-money {
            font-size: 20px;
        }

        .button {
            display: inline-block;
            padding: 6px 12px;
            margin-bottom: 0;
            font-size: 14px;
            font-weight: 400;
            line-height: 1.42857143;
            text-align: center;
            white-space: nowrap;
            vertical-align: middle;
            -ms-touch-action: manipulation;
            touch-action: manipulation;
            cursor: pointer;
            -webkit-user-select: none;
            -moz-user-select: none;
            -ms-user-select: none;
            user-select: none;
            background-image: none;
            border: 1px solid transparent;
            border-radius: 4px;
        }

        .button-primary {
            color: #fff;
            background-color: #337ab7;
            border-color: #2e6da4;
        }

            .button-primary.active, .button-primary:active, .open > .dropdown-toggle.button-primary {
                color: #fff;
                background-color: #286090;
                border-color: #204d74;
            }

            .button-primary:hover {
                color: #fff;
                background-color: #286090;
                border-color: #204d74;
            }



        .button-success {
            color: #fff;
            background-color: #5cb85c;
            border-color: #4cae4c;
        }

            .button-success.disabled, .button-success.disabled.active, .button-success.disabled.focus, .button-success.disabled:active, .button-success.disabled:focus, .button-success.disabled:hover, .button-success[disabled], .button-success[disabled].active, .button-success[disabled].focus, .button-success[disabled]:active, .button-success[disabled]:focus, .button-success[disabled]:hover, fieldset[disabled] .button-success, fieldset[disabled] .button-success.active, fieldset[disabled] .button-success.focus, fieldset[disabled] .button-success:active, fieldset[disabled] .button-success:focus, fieldset[disabled] .button-success:hover {
                background-color: #5cb85c;
                border-color: #4cae4c;
            }

        .button.disabled, .button[disabled], fieldset[disabled] .button {
            cursor: not-allowed;
            filter: alpha(opacity=65);
            -webkit-box-shadow: none;
            box-shadow: none;
            opacity: .65;
        }


        .button-success:hover {
            color: #fff;
            background-color: #449d44;
            border-color: #398439;
        }

        .button-success.active, .button-success:active, .open > .dropdown-toggle.button-success {
            color: #fff;
            background-color: #449d44;
            border-color: #398439;
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
    </style>

</head>
<body class="mainbody">
    <form id="form1" runat="server">


        <div class="location">
            <% if (isAdmin)
                {%>
            <a href="hotel_list.aspx" class="home"><i></i><span>酒店商户或门店管理</span></a>
            <i class="arrow"></i>
            <%}%>
            <%
                else
                {%>

            <%} %><span><a href="hotel_dingdan_manage.aspx?hotelid=<%=hotelid %>">订单管理</a></span>
            <i class="arrow"></i>
            <span>订单详情</span>
        </div>

        <!--/导航栏-->
        <div class="tab-content">
            <ul class="round">
                <li class="title"><span class="none smallspan">订单详情</span></li>
                <table width="100%" border="0" cellpadding="0" cellspacing="0" class="cpbiaoge">

                    <%=Dingdanlist %>
                </table>
            </ul>
            <ul class="round">
                <li class="title"><span class="none smallspan">订单信息</span></li>

                <table width="100%" border="0" cellpadding="0" cellspacing="0" class="cpbiaoge">
                    <%=Dingdanren %>
                </table>
            </ul>
        </div>

        <div class="tab-content">
            <% if (isAdmin)
                {%>
            <%--            <dl>--%>
            <%--                <dt>订单状态：</dt>--%>
            <%--                <dd><span><%=status.StatusName %></span>--%>
            <%--                    --%>
            <%----%>
            <%--                </dd>--%>
            <%--             </dl>--%>


            <%   if (orderStatus == HotelStatusManager.OrderStatus.Payed.StatusId)
                {%>

            <dl>
                <dt>是否退款：</dt>
                <dd>
                    <asp:CheckBox ID="chkIsRefund" runat="server" /></dd>
            </dl>

            <dl>
                <dt>退款金额：</dt>
                <dd>

                    <input id="txtAmount" type="text" class="input normal" runat="server" sucmsg=" " nullmsg="" datatype="n" />
                    <span class="Validform_checktip">注：默认为订单全额，可修改。*</span>

                </dd>
            </dl>
            <dl>
                <dt>备注：</dt>
                <dd>
                    <textarea name="remarks" rows="2" cols="20" id="remarks" datatype="*1-2000" sucmsg=" " nullmsg="" class="input" runat="server"></textarea>
                    <span class="Validform_checktip">*</span>
                </dd>
            </dl>
            <%}%>


            <%}%>
            <%
                else
                {%>


            <% if (orderStatus == HotelStatusManager.OrderStatus.Pending.StatusId)
                {%>
            <dl>
                <dt>订单状态：</dt>
                <dd>
                    <asp:RadioButtonList ID="StatusType" runat="server" RepeatDirection="Horizontal" CellPadding="10" CellSpacing="10">
                        <asp:ListItem Value="1">商家接收</asp:ListItem>
                        <asp:ListItem Value="1">商家拒绝</asp:ListItem>
                    </asp:RadioButtonList>


                </dd>

            </dl>

            <%}%>
            <%
                else
                {%>
            <dl>
                <dt>订单状态：</dt>
                <dd><span><%=status.StatusName %></span>


                </dd>

            </dl>
            <%} %>




            <%} %>

            <% if (orderStatus == HotelStatusManager.OrderStatus.Pending.StatusId
                               || orderStatus == HotelStatusManager.OrderStatus.Refused.StatusId
                               || orderStatus == HotelStatusManager.OrderStatus.Accepted.StatusId)
                {%>

            <dl>
                <dt>是否付款：</dt>
                <dd><span>未付款</span></dd>
            </dl>
            <%}%>

            <% else if (orderStatus == HotelStatusManager.OrderStatus.Refunding.StatusId
                         || orderStatus == HotelStatusManager.OrderStatus.Refunded.StatusId)
                {%>

            <dl>
                <dt>退款操作日期：</dt>
                <dd><span><%=tuidan.refundTime.ToString("yyyy/MM/dd") %></span></dd>
            </dl>
            <dl>
                <dt>退款操作人员：</dt>
                <dd><span><%=uName %>（<%=roleName %>）</span></dd>
            </dl>
            <dl>
                <dt>退款金额：</dt>
                <dd><span><%=tuidan.refundAmount %>元</span></dd>
            </dl>
            <%}%>
        </div>


        <% if (!isAdmin && orderStatus == HotelStatusManager.OrderStatus.Pending.StatusId)
            {%>

        <div style="width: 100%; text-align: center">
            <asp:Button ID="save_groupbase" runat="server" CssClass="button button-success " Text="提交" OnClick="save_groupbase_Click" />

        </div>
        <%}%>

        <%
            else if (isAdmin && orderStatus == HotelStatusManager.OrderStatus.Payed.StatusId)
            {%>

        <div style="width: 100%; text-align: center">
            <asp:Button ID="btnSaveRefund" runat="server" CssClass="button button-success " disabled="disabled" Text="提交" OnClick="btnSaveRefund_OnClick" />

        </div>
        <%}%>

        <%
            else
            {%>

        <div style="width: 100%; text-align: center">
            <% if (!isAdmin && orderStatus == HotelStatusManager.OrderStatus.Payed.StatusId)
                {%>
            <%=ordermsg %>
            <div class="alert alert-warning " role="alert" style="margin-top: 10px; text-align: left;">

                <strong>请注意!</strong>顾客到店完成验证码的验证并提供服务后，点击【订单完成】按钮。只有执行了订单完成操作的订单方可通过“服务凭据查询”功能与景区结算。
            </div>

            <asp:Button ID="btn_completed" runat="server" CssClass="button button-success " Text="订单完成"
                OnClientClick="return ConfirmPostBack('btn_completed', '请确认顾客已到店核销验证码，确定执行【订单完成】操作？');"
                OnClick="btn_completed_OnClick" />

            <%}%>



            <asp:Button ID="btn_return" runat="server" CssClass="button  button-primary " Text="返回" OnClick="btn_return_OnClick" />

        </div>
        <%}%>

        <input type="hidden" name="__EVENTTARGET" value="">
        <input type="hidden" name="__EVENTARGUMENT" value="">
    </form>
</body>
</html>
