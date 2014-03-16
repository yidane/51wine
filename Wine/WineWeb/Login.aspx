<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="WineWeb.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>用户登录</title>
    <style type="text/css">
        body, div, p, input, a {
            margin: 0;
            padding: 0;
            border: 0;
        }

        a, input {
            outline: none;
        }

        .mb05 {
            margin-bottom: 5px;
        }

        body {
            background: #E5E8F1;
        }

        .loginBox {
            width: 530px;
            height: 390px;
            background: url(Image/login_bg.gif) 0 0 no-repeat;
            position: absolute;
            top: 50%;
            left: 50%;
            margin: -195px 0 0 -265px;
        }

        .login {
            width: 530px;
            height: 390px;
            position: relative;
        }

        .author {
            width: 280px;
            height: 45px;
            position: absolute;
            left: 166px;
            top: 207px;
        }

        .autInput {
            width: 158px;
            height: 45px;
            float: left;
        }

            .autInput p {
                width: 160px;
                height: 20px;
                font-size: 13px;
            }

                .autInput p label {
                    float: left;
                    height: 20px;
                    line-height: 20px;
                    width: 40px;
                }

                .autInput p input {
                    width: 114px;
                    height: 20px;
                    line-height: 20px;
                    float: left;
                    padding: 0 3px;
                    background-color: #292929;
                    color: #fff;
                }

        .autBtn {
            width: 104px;
            height: 18px;
            line-height: 18px;
            float: right;
            padding-top: 27px;
        }

            .autBtn input {
                width: 47px;
                height: 18px;
                float: left;
                margin-right: 4px;
                background: url(Image/button.gif) 0 0 no-repeat;
                cursor: pointer;
                font-size: 12px;
                color: #fff;
            }

                .autBtn input:hover {
                    background-position: 0 -18px;
                }

        .title {
            position: absolute;
            top: 29px;
            left: 68px;
            width: 360px;
            height: 48px;
            line-height: 48px;
            font-family: "微软雅黑";
            font-size: 26px;
            color: #fff;
            text-align: center;
        }
    </style>
</head>
<body>
    <form id="Form1" runat="server">
        <div class="loginBox">
            <div class="login">
                <div class="title">某某管理系统</div>
                <div class="author">
                    <div class="autInput">
                        <p class="mb05">
                            <label>用户：</label><asp:TextBox ID="LoginName" runat="server"></asp:TextBox>
                        </p>
                        <p>
                            <label>密码：</label><asp:TextBox ID="Password" TextMode="Password" runat="server"></asp:TextBox>
                        </p>
                    </div>
                    <div class="autBtn">
                        <asp:Button runat="server" ID="LoginIn" OnClick="LoginIn_Click" Text="登录" />
                    </div>
                </div>
            </div>
        </div>
    </form>
    <script src="Script/jquery.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        function LoginIn() {
            document.location.href = "Index.aspx"
        }
    </script>
</body>
</html>
