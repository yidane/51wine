<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="shop_user_list.aspx.cs" Inherits="WeiXinPF.Web.admin.diancai.shop_user_list" %>

<%@ Import Namespace="WeiXinPF.Common" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>管理员列表</title>
    <script type="text/javascript" src="../../scripts/jquery/jquery-1.10.2.min.js"></script>
    <script type="text/javascript" src="../../scripts/lhgdialog/lhgdialog.js?skin=idialog"></script>
    <script type="text/javascript" src="../js/layout.js"></script>
    <link href="../skin/default/style.css" rel="stylesheet" type="text/css" />
    <link href="../skin/mystyle.css" rel="stylesheet" type="text/css" />
</head>
<body class="mainbody">
    <form id="form1" runat="server">
        <!--导航栏-->
        <div class="location">
            <a href="shop_list.aspx" class="home"><i></i><span>点菜系统</span></a>
            <i class="arrow"></i>
            <span>管理员设置</span>
        </div>
        <div class="line10"></div>

        <!--工具栏-->
        <div class="toolbar-wrap">
            <div id="floatHead" class="toolbar">
                <div class="l-list">
                    <ul class="icon-list">
                        <li>
                            <a class="icon-btn add" href="shop_user_edit.aspx?action=<%=MXEnums.ActionEnum.Add %>&shopid=<%=shopid%>">
                                <i></i>
                                <span>新增管理人员</span>
                            </a>
                        </li>
                    </ul>
                </div>
            </div>
        </div>

        <!--列表-->
        <asp:Repeater ID="rptList" runat="server">
            <HeaderTemplate>
                <table width="100%" border="0" cellspacing="0" cellpadding="0" class="ltable">
                    <tr>
                        <th width="8%">选择</th>
                        <th align="left" width="10%">用户名</th>
                        <th align="left" width="12%">姓名</th>
                        <th align="left" width="12%">角色</th>
                        <th align="left" width="12%">电话</th>
                        <th align="left" width="10%">添加时间</th>
                        <th width="4%">状态</th>
                        <th width="10%">操作</th>
                    </tr>
            </HeaderTemplate>
            <ItemTemplate>
                <tr>
                    <td align="center">
                        <asp:CheckBox ID="chkId" CssClass="checkall" runat="server" Style="vertical-align: middle;" />
                        <asp:HiddenField ID="hidId" Value='<%#Eval("id")%>' runat="server" />
                    </td>
                    <td><%# Eval("user_name") %></td>
                    <td><%# Eval("real_name") %></td>
                    <td><%#new WeiXinPF.BLL.manager_role().GetTitle(Convert.ToInt32(Eval("role_id")))%></td>
                    <td><%# Eval("telephone") %></td>
                    <td><%#string.Format("{0:g}",Eval("add_time"))%></td>
                    <td align="center"><%#Eval("is_lock").ToString().Trim() == "0" ? "正常" : "禁用"%></td>
                    <td align="center">
                        <a href="shop_user_edit.aspx?action=<%#MXEnums.ActionEnum.Edit %>&id=<%#Eval("id")%>">修改</a>
                    </td>
                </tr>
            </ItemTemplate>
            <FooterTemplate>
                <%#rptList.Items.Count == 0 ? "<tr><td align=\"center\" colspan=\"10\">暂无记录</td></tr>" : ""%>
</table>
            </FooterTemplate>
        </asp:Repeater>
    </form>
</body>
</html>
