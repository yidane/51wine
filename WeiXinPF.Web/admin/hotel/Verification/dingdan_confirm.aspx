<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="dingdan_confirm.aspx.cs" Inherits="WeiXinPF.Web.admin.hotel.Verification.dingdan_confirm" %>

<!DOCTYPE html>

<html>
<head id="Head1" runat="server">
    <title>订单管理</title>
    <script type="text/javascript" src="../../../scripts/jquery/jquery-1.10.2.min.js"></script>
    <script type="text/javascript" src="../../../scripts/lhgdialog/lhgdialog.js?skin=idialog"></script>
    <script type="text/javascript" src="../../js/layout.js"></script>
    <link href="../../skin/default/style.css" rel="stylesheet" type="text/css" />
    <link href="../../skin/mystyle.css" rel="stylesheet" type="text/css" />
    <link href="../../../css/pagination.css" rel="stylesheet" type="text/css" />

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

        .SearchBox {
            float: left;
            padding: 0 5px;
            width: 410px;
            height: 30px;
            line-height: 28px;
            font-size: 12px;
            border: 1px solid #dbdbdb;
            color: #444;
        }

        .SearchButton {
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

        .SearchDiv {
            position: absolute;
            float: left;
            left: -205px;
            margin-left: 45%;
            width: 500px;
            text-align: left;
            margin-top: 100px;
        }
    </style>
</head>
<body class="mainbody">
    <form id="form1" runat="server">

        <div class="location">
           <% if (IsWeiXinCode())
                {%>
            <a    href="../hotel_list.aspx" class="home"><i></i><span>商户或门店列表</span></a>
              <i class="arrow"></i>
                <%}%>
                <%
                else
                {%>
               
                <%} %>
            <span>服务码验证</span>
        </div>
        <!--/导航栏-->
        <div class="mytips" style="display: none;">
            1、确认订单后，用户不能再取消该订单；被设置为无效的订单，将成为失败订单！
             <br />
            2、交易成功的订单会计入销售统计；交易失败的订单不会计入销售统计，多次交易失败的客户建议加入黑名单！
        </div>
        <!--/验证内容-->
        <div class="SearchDiv">
            <asp:TextBox ID="confirmnumber" runat="server" CssClass="SearchBox" placeholder="请输入验证码"></asp:TextBox>
            <asp:Button ID="confirm_dingdan" name="confirm_dingdan" runat="server" Text="验 证" class="SearchButton" OnClick="confirm_dingdan_Click" />
        </div>
    </form>
</body>
</html>
