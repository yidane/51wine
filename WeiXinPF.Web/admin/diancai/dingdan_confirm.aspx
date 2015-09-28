<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="dingdan_confirm.aspx.cs" Inherits="WeiXinPF.Web.admin.diancai.dingdan_confirm" %>

<!DOCTYPE html>

<html>
<head id="Head1" runat="server">
    <title>订单管理</title>
    <script type="text/javascript" src="../../scripts/jquery/jquery-1.10.2.min.js"></script>
    <script type="text/javascript" src="../../scripts/lhgdialog/lhgdialog.js?skin=idialog"></script>
    <script type="text/javascript" src="../js/layout.js"></script>
    <link href="../skin/default/style.css" rel="stylesheet" type="text/css" />
    <link href="../skin/mystyle.css" rel="stylesheet" type="text/css" />
    <link href="../../css/pagination.css" rel="stylesheet" type="text/css" />

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

        .submit {
            background-color: #c32d32;
            padding: 10px 20px;
            font-size: 16px;
            text-decoration: none;
            border: 1px solid #9C1402;
            background: -webkit-gradient(linear,left top,left bottom,from(#fe444a),to(#c32d32));
            box-shadow: 0 1px 0 #FF9D9D inset, 0 1px 2px rgba(0, 0, 0, 0.5);
            border-radius: 5px;
            color: #ffffff;
            display: block;
            text-align: center;
            text-shadow: 0 1px rgba(0, 0, 0, 0.2);
        }

            .submit:active {
                padding-bottom: 9px;
                padding-left: 20px;
                padding-right: 20px;
                padding-top: 11px;
                top: 0px;
                background: -webkit-gradient(linear,left top,left bottom,from(#c32d32),to(#fe444a));
                box-shadow: 0 1px 0 #FF9D9D inset, 0 1px 2px rgba(0, 0, 0, 0.5);
            }
    </style>
</head>
<body class="mainbody">
    <form id="form1" runat="server">

        <div class="location">
            <a href="shop_list.aspx" class="home"><i></i><span>点菜系统</span></a>
            <i href="dingdan_manage.aspx?shopid=<%=shopid %>" class="arrow"></i><span>订单管理</span>
            <i class="arrow"></i>
            <span>订单验证</span>
        </div>
        <!--/导航栏-->
        <div class="mytips">
            1、确认订单后，用户不能再取消该订单；被设置为无效的订单，将成为失败订单！
             <br />
            2、交易成功的订单会计入销售统计；交易失败的订单不会计入销售统计，多次交易失败的客户建议加入黑名单！
        </div>
        <!--/验证内容-->
        <div class="content">
            <%--<input id="confirmnumber" type="text" value="" placeholder="请输入订单号" />--%>
            <asp:TextBox ID="confirmnumber" runat="server" placeholder="请输入订单号"></asp:TextBox>
            <%--<a id="btnconfirm" class="submit" href="javascript:void(0)">验 证</a>--%>
            <%--<input type="submit" name="confirm_dingdan" value="验 证" id="confirm_dingdan" class="btn">--%>
            <asp:Button ID="confirm_dingdan" name="confirm_dingdan" runat="server" Text="验 证" class="btn" OnClick="confirm_dingdan_Click"/>
        </div>
    </form>
</body>
</html>
