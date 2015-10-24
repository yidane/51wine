<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="diangdan_refundDetail.aspx.cs" Inherits="WeiXinPF.Web.admin.diancai.diangdan_refundDetail" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>订单详情</title>
    <script type="text/javascript" src="../../scripts/jquery/jquery-1.10.2.min.js"></script>
    <script type="text/javascript" src="../../scripts/lhgdialog/lhgdialog.js?skin=idialog"></script>
    <script type="text/javascript" src="../js/layout.js"></script>
    <link href="../skin/default/style.css" rel="stylesheet" type="text/css" />
    <link href="../skin/mystyle.css" rel="stylesheet" type="text/css" />
    <%-- <link href="../../css/pagination.css" rel="stylesheet" type="text/css" />--%>
    <link href="../../weixin/diancai/css/diancai.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        function parentToIndex(id) {
            parent.location.href = "/admin/Index.aspx?id=" + id;
        }

        $(function () {
        });

    </script>
    <style>
        a.shenghe {
            color: red;
        }

        .text-danger {
            color: #a94442;
        }

        .btn-success:hover {
            color: #fff;
            background-color: #449d44;
            border-color: #398439;
        }

        .btn.focus, .btn:focus, .btn:hover {
            color: #333;
            text-decoration: none;
        }

        .btn-success {
            color: #fff;
            background-color: #5cb85c;
            border-color: #4cae4c;
        }

        .btn {
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
    </style>
</head>
<body class="mainbody">
    <form id="form1" runat="server">
        <div class="location">
            <% if (IsWeiXinCode())
               {%>
            <a href="#" class="home"><i></i><span>商户或门店列表</span></a>
            <i class="arrow"></i>
            <%}%>
            <%
               else
               {%>

            <%} %> <span><a href="dingdan_manage.aspx?shopid=<%=shopid %>">退单管理</a></span>
            <i class="arrow"></i>
            <span>退单详情</span>
        </div>

        <!--/导航栏-->
        <div class="tab-content">
            <ul class="round">
                <li class="title"><span class="none smallspan">退单详情</span></li>
                <table width="100%" border="0" cellpadding="0" cellspacing="0" class="cpbiaoge">
                    <%=Dingdanlist %>
                </table>
            </ul>

            <ul class="round">
                <li class="title"><span class="none smallspan">退单信息</span></li>
                <table width="100%" border="0" cellpadding="0" cellspacing="0" class="cpbiaoge">
                    <%=dingdanren %>
                </table>
            </ul>
        </div>

        <div class="tab-content">
            <dl>
                <dt>不同意退单理由：</dt>
                <dd>
                    <textarea name="reason" rows="2" cols="20" id="instructions" datatype="*1-1000" sucmsg=" " nullmsg="" class="input" runat="server"></textarea>
                </dd>
            </dl>
        </div>

        <div class="tab-content" style="display: none">
            <dl>
                <dt>状态调整为：</dt>
                <dd>
                    <asp:DropDownList ID="ddlStatusType" runat="server">
                        <asp:ListItem Text="退款审核中" Value="2"></asp:ListItem>
                        <asp:ListItem Text="已退款" Value="3"></asp:ListItem>
                        <asp:ListItem Text="退款失败" Value="4"></asp:ListItem>
                    </asp:DropDownList>
                </dd>
            </dl>
        </div>

        <div class="page-footer">
            <div class="btn-list" style="text-align: center">
                <asp:Button ID="btnAgreeRefund" runat="server" CssClass="btn" Text="同意退单" OnClick="btnAgreeRefund_Click" Enabled="False" Visible="False" />
                <asp:Button ID="btnDisAgreeRefund" runat="server" CssClass="btn" Text="不同意退单" OnClick="btnDisAgreeRefund_Click" Enabled="False" Visible="False" />
            </div>
            <div class="clear"></div>
        </div>
    </form>
</body>
</html>
