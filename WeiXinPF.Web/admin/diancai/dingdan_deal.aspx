<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="dingdan_deal.aspx.cs" Inherits="WeiXinPF.Web.admin.diancai.dingdan_deal" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
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

        .DingdanButton {
            border: 1px solid #3d810c;
            box-shadow: 0 1px 1px #aaa;
            -moz-box-shadow: 0 1px 1px #aaa;
            -webkit-box-shadow: 0 1px 1px #aaa;
            padding: 5px 20px;
            cursor: pointer;
            display: inline-block;
            text-align: center;
            vertical-align: bottom;
            overflow: visible;
            border-radius: 3px;
            -moz-border-radius: 3px;
            -webkit-border-radius: 3px;
            *zoom: 1;
            background-color: #f1f1f1;
            background-image: linear-gradient(bottom, #DCDADA 3%, #f9f9f9 97%, #fff 100%);
            background-image: -moz-linear-gradient(bottom, #DCDADA 3%, #f9f9f9 97%, #fff 100%);
            background-image: -webkit-linear-gradient(bottom, #DCDADA 3%, #f9f9f9 97%, #fff 100%);
            color: #000;
            border: 1px solid #AAA;
            font-size: 14px;
            line-height: 1.5;
            background-color: #16a0d3;
        }

        .button-success {
            color: #fff;
            background-color: #5cb85c;
            border-color: #4cae4c;
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
            <% if (IsWeiXinCode())
               {%>
            <a href="shop_list.aspx" class="home"><i></i><span>商户或门店列表</span></a>
            <i class="arrow"></i>
            <%}%>
            <%
               else
               {%>

            <%} %><span><a href="dingdan_manage.aspx?shopid=<%=shopid %>">订单管理</a></span>
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
                    <%=dingdanren %>
                </table>
            </ul>
        </div>
        <div style="width: 100%; text-align: center">
            <div class="alert alert-warning " role="alert" style="margin-top: 10px; text-align: left;">

                <strong>请注意!</strong>顾客到店完成验证码的验证并提供服务后，点击【订单完成】按钮。只有执行了订单完成操作的订单方可通过“服务凭据查询”功能与景区结算。
            </div>
            <asp:Button ID="btnFinish" runat="server" Text="订单完成" CssClass="DingdanButton button-success"
                OnClientClick="return ConfirmPostBack('btnFinish', '请确认顾客已到店核销验证码，确定执行【订单完成】操作？');"
                OnClick="btnFinish_OnClick" />
        </div>
        
        
        
        
        
        

        <input type="hidden" name="__EVENTTARGET" value="">
        <input type="hidden" name="__EVENTARGUMENT" value="">
    </form>
</body>
</html>
