<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="WeiXinPF.Web.admin.login" %>

<%--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">--%>
<%----%>
<%--<html xmlns="http://www.w3.org/1999/xhtml">--%>
<%--<head>--%>
<%--    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />--%>
<%--    <meta name="keywords" content="04e11d01b9df2865" />--%>
<%--    <title>管理员登录 版本号：<%=WeiXinPF.Common.MXKeys.ASSEMBLY_VERSION %> </title>--%>
<%--    <link href="skin/default/style.css" rel="stylesheet" type="text/css" />--%>
<%--    <script type="text/javascript" src="../scripts/jquery/jquery-1.10.2.min.js"></script>--%>
<%--    <script type="text/javascript">--%>
<%--        $(function () {--%>
<%--            //检测IE--%>
<%--            if ('undefined' == typeof (document.body.style.maxHeight)) {--%>
<%--                window.location.href = 'ie6update.html';--%>
<%--            }--%>
<%--        });--%>
<%--    </script>--%>
<%--</head>--%>
<%----%>
<%--<body class="loginbody">--%>
<%--    <form id="form1" runat="server">--%>
<%--        <div class="login-screen">--%>
<%--            <div class="login-icon">LOGO</div>--%>
<%--            <div class="login-form">--%>
<%--                <h1>系统管理登录</h1>--%>
<%--                <div class="control-group">--%>
<%--                    <asp:TextBox ID="txtUserName" runat="server" CssClass="login-field" placeholder="用户名" title="用户名"></asp:TextBox>--%>
<%--                    <label class="login-field-icon user" for="txtUserName"></label>--%>
<%--                </div>--%>
<%--                <div class="control-group">--%>
<%--                    <asp:TextBox ID="txtPassword" runat="server" CssClass="login-field" TextMode="Password" placeholder="密码" title="密码"></asp:TextBox>--%>
<%--                    <label class="login-field-icon pwd" for="txtPassword"></label>--%>
<%--                </div>--%>
<%--                <div>--%>
<%--                    <asp:Button ID="btnSubmit" runat="server" Text="登 录" CssClass="btn-login" OnClick="btnSubmit_Click" /></div>--%>
<%--                <span class="login-tips"><i></i><b id="msgtip" runat="server">请输入用户名和密码</b></span>--%>
<%--            </div>--%>
<%--            <i class="arrow">箭头</i>--%>
<%--        </div>--%>
<%--    </form>--%>
<%--</body>--%>
<%--</html>--%>



<!DOCTYPE HTML PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>喀纳斯景区服务号后台管理系统</title>

    <link href="../css/login.css" rel="stylesheet" />
    <script src="../scripts/jquery/jquery-1.10.2.min.js"></script>
        <script src="../scripts/cloud.js"></script>

    <script language="javascript">
        var $userName ;
        var $password ;
        $(function () {
             $userName = $("#txtUserName");
             $password = $("#txtPassword");
            
             

            //默认用户名输入框获得焦点。
            $userName[0].focus();
            
            $('.loginbox').css({ 'position': 'absolute', 'left': ($(window).width() - 692) / 2 });
            $(window).resize(function () {
                $('.loginbox').css({ 'position': 'absolute', 'left': ($(window).width() - 692) / 2 });
            });

        });

 
    </script>
    <style>
        .flat-left {
            float: left;
        }
    </style>

</head>

<body style="background-color: #1c77ac; background-image: url(../images/light.png); background-repeat: no-repeat; background-position: center top; overflow: hidden;">

   

        <div id="mainBody">
            <div id="cloud1" class="cloud"></div>
            <div id="cloud2" class="cloud"></div>
        </div>


        <div class="logintop">
            <span>喀纳斯景区服务号后台管理系统</span>
            <ul>
                <li><a href="#">帮助</a></li>
                <li><a href="#">关于</a></li>
            </ul>
        </div>
        <form id="form1" runat="server"  autocomplete="off">
        <div class="loginbody">

            <span class="systemlogo"></span>

            <div class="loginbox">

                <ul>
                    <li>
                        <input name="" type="text" runat="server"  id="txtUserName" class="loginuser" value="" placeholder="用户名" /></li>
                    <li>
                        <input name="" type="password" runat="server" id="txtPassword" class="loginpwd" value="" placeholder="密码" /></li>
                    <li>

                        <asp:Button ID="btnSubmit" runat="server" Text="登 录" CssClass="loginbtn flat-left" OnClick="btnSubmit_Click" />
                        <label>
                          <!--  <input name="" id="chkRemember" type="checkbox" value="" checked="checked" runat="server" />记住密码-->
                        <span class="login-tips flat-left"><i></i><b id="msgtip" runat="server">请输入用户名和密码</b></span></label>
                    </li>
                </ul>


            </div>

        </div>

             </form>

        <div class="loginbm">版权所有  2015  喀纳斯景区管理委员会 </div>

   
</body>

</html>
