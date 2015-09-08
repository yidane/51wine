<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="WeiXinPF.Web.admin.login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<meta name="keywords"  content="04e11d01b9df2865" />
<title>管理员登录 版本号：<%=WeiXinPF.Common.MXKeys.ASSEMBLY_VERSION %> </title>
<link href="skin/default/style.css" rel="stylesheet" type="text/css" />
<script type="text/javascript" src="../scripts/jquery/jquery-1.10.2.min.js"></script>
<script type="text/javascript">
    $(function () {
        //检测IE
        if ('undefined' == typeof (document.body.style.maxHeight)) {
            window.location.href = 'ie6update.html';
        }
    });
</script>
</head>

<body class="loginbody">
<form id="form1" runat="server">
<div class="login-screen">
	<div class="login-icon">LOGO</div>
    <div class="login-form">
        <h1>系统管理登录</h1>
        <div class="control-group">
            <asp:TextBox ID="txtUserName" runat="server" CssClass="login-field" placeholder="用户名" title="用户名"></asp:TextBox>
            <label class="login-field-icon user" for="txtUserName"></label>
        </div>
        <div class="control-group"> <%--1e2124dd04e11d01b9df2865f85944be--%>
            <asp:TextBox ID="txtPassword" runat="server" CssClass="login-field" TextMode="Password" placeholder="密码" title="密码"></asp:TextBox>
            <label class="login-field-icon pwd" for="txtPassword"></label>
        </div>
        <div><asp:Button ID="btnSubmit" runat="server" Text="登 录" CssClass="btn-login" onclick="btnSubmit_Click" /></div>
        <span class="login-tips"><i></i><b id="msgtip" runat="server">请输入用户名和密码</b></span>
    </div>
    <i class="arrow">箭头</i>
</div>
</form>

    <div style="display:none;">
    <a href="https://item.taobao.com/item.htm?spm=686.1000925.0.0.6iRlAm&id=520523216527">
        <span style="color:white;">购买请认证官方销售渠道（淘宝店）https://item.taobao.com/item.htm?spm=686.1000925.0.0.6iRlAm&id=520523216527
            官方qq 2-3002-807 其他地方购买均为盗版</span>
    </a>

     </div>

</body>
</html>