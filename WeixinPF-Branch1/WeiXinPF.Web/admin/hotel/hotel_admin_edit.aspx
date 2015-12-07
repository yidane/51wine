<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="hotel_admin_edit.aspx.cs" Inherits="WeiXinPF.Web.admin.hotel.hotel_admin_edit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>编辑管理员</title>
    <script type="text/javascript" src="../../scripts/jquery/jquery-1.10.2.min.js"></script>
    <script type="text/javascript" src="../../scripts/jquery/Validform_v5.3.2_min.js"></script>
    <script type="text/javascript" src="../../scripts/lhgdialog/lhgdialog.js?skin=idialog"></script>
    <script type="text/javascript" src="../js/layout.js"></script>
    <link href="../skin/default/style.css" rel="stylesheet" type="text/css" />
    <link href="../skin/mystyle.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        $(function () {
            $('#form1').initValidform();
        });
    </script>
</head>
<body class="mainbody">
    <form id="form1" runat="server">
        <!--导航栏-->
        <div class="location">
            <a href="hotel_list.aspx" class="home"><i></i><span>商户或门店列表</span></a>
            <i class="arrow"></i>
            <a href="hotel_admin_list.aspx?hotelid=<%=hotelid %>"><i></i><span>管理员列表</span></a>
            <i class="arrow"></i>
            <span>编辑管理员</span>
        </div>
        <div class="line10"></div>

        <!--内容-->
        <div class="content-tab-wrap">
            <div id="floatHead" class="content-tab">
                <div class="content-tab-ul-wrap">
                    <ul>
                        <li><a href="javascript:;" onclick="tabs(this);" class="selected">基本信息</a></li>
                    </ul>
                </div>
            </div>
        </div>
        <div class="tab-content">
            <dl>
                <dt>管理角色</dt>
                <dd>
                    <asp:Label ID="lblRoleName" runat="server" Text="" Font-Bold="true"></asp:Label>
                </dd>
            </dl>
            <dl>
                <dt>用户名</dt>
                <dd>
                    <asp:TextBox ID="txtUserName" runat="server" CssClass="input normal" datatype="/^[a-zA-Z0-9\-\_]{2,50}$/" nullmsg="请输入用户名" sucmsg=" " ajaxurl="../../tools/admin_ajax.ashx?action=manager_validate"></asp:TextBox>
                    <span class="Validform_checktip">*字母、下划线，不可修改</span></dd>
            </dl>
            <dl>
                <dt>密码</dt>
                <dd>
                    <asp:TextBox ID="txtPassword" runat="server" CssClass="input normal" datatype="*6-20" nullmsg="请设置密码" errormsg="密码范围在6-20位之间" sucmsg=" "></asp:TextBox>
                    <span class="Validform_checktip">*密码范围在6-20位之间。<asp:Literal ID="litpwdtip" runat="server"></asp:Literal></span></dd>
            </dl>

            <dl>
                <dt>姓名</dt>
                <dd>
                    <asp:TextBox ID="txtRealName" runat="server" CssClass="input normal" datatype="*1-50" nullmsg="请输入姓名" sucmsg=" "></asp:TextBox></dd>
            </dl>
            <dl>
                <dt>电话</dt>
                <dd>
                    <asp:TextBox ID="txtTelephone" runat="server" CssClass="input normal" datatype="*0-50" nullmsg=" " sucmsg=" "></asp:TextBox>
                </dd>
            </dl>
            <dl>
                <dt>邮箱</dt>
                <dd>
                    <asp:TextBox ID="txtEmail" runat="server" CssClass="input normal" datatype="*1-50" nullmsg="请输入邮箱" sucmsg=" "></asp:TextBox>
                    <span class="Validform_checktip"></span>
                </dd>
            </dl>
            <dl>
                <dt>是否启用</dt>
                <dd>
                    <div class="rule-multi-radio">
                        <asp:RadioButtonList ID="rblIsLock" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
                            <asp:ListItem Value="0" Selected="True">启用</asp:ListItem>
                            <asp:ListItem Value="1">不启用</asp:ListItem>
                        </asp:RadioButtonList>
                    </div>
                    <span class="Validform_checktip">*不启用则无法使用该账户登录</span>
                </dd>
            </dl>
            <dl>
                <dt>备注</dt>
                <dd>
                    <asp:TextBox ID="txtRemark" runat="server" CssClass="input normal" TextMode="MultiLine" Rows="4" datatype="*0-1500" nullmsg=" " sucmsg=" "></asp:TextBox>

                </dd>
            </dl>
        </div>
        <!--/内容-->

        <!--工具栏-->
        <div class="page-footer">
            <div class="btn-list">
                <asp:Button ID="btnSubmit" runat="server" Text="提交保存" CssClass="btn" OnClick="btnSubmit_Click" />
                <input name="btnReturn" type="button" value="返回上一页" class="btn yellow" onclick="javascript: history.back(-1);" />
            </div>
            <div class="clear"></div>
        </div>
        <!--/工具栏-->
    </form>
</body>
</html>
