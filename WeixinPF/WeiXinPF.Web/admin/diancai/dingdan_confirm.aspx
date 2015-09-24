<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="dingdan_confirm.aspx.cs" Inherits="WeiXinPF.Web.admin.diancai.dingdan_confirm" %>

<!DOCTYPE html>

<html >
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
        color:red;
        
        }
    </style>
</head>
<body class="mainbody">
    <form id="form1" runat="server">

          <div class="location">
            <a href="shop_list.aspx" class="home"><i></i><span>点菜系统</span></a>
            <i  href="dingdan_manage.aspx?shopid=<%=shopid %>" class="arrow"></i><span>订单管理</span>
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
        <div class="">
    </form>
</body>
</html>
