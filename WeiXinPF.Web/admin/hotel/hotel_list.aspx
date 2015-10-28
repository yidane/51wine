﻿<%@ Import Namespace="WeiXinPF.Common" %>
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="hotel_list.aspx.cs" Inherits="WeiXinPF.Web.admin.hotel.hotel_list" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>微酒店</title>
    <script type="text/javascript" src="../../scripts/jquery/jquery-1.10.2.min.js"></script>
    <script type="text/javascript" src="../../scripts/lhgdialog/lhgdialog.js?skin=idialog"></script>
    <script type="text/javascript" src="../js/layout.js"></script>
    <link href="../skin/default/style.css" rel="stylesheet" type="text/css" />
    <link href="../skin/mystyle.css" rel="stylesheet" type="text/css" />
    <link href="../../css/pagination.css" rel="stylesheet" type="text/css" />
    <style>
        a.shenghe {
            color: red;
        }
    </style>
</head>
<body class="mainbody">
    <form id="form1" runat="server">
        <div class="location">
            <a href="javascript:;" class="home"><i></i><span>商户或门店列表</span></a>
        </div>
        <div class="toolbar-wrap">
            <div id="floatHead" class="toolbar">
                <div class="l-list">
                    <ul class="icon-list">
                        <li><a class="icon-btn add" href="hotel_register.aspx?action=<%=MXEnums.ActionEnum.Add %>" id="itemAddButton"><i></i><span>商户或门店入驻登记</span></a></li>
                        <li><a class="all" href="javascript:;" onclick="checkAll(this);"><i></i><span>全选</span></a></li>
                        <li>
                            <asp:LinkButton ID="btnDelete" runat="server" CssClass="del" OnClientClick="return ExePostBack('btnDelete');" OnClick="btnDelete_Click"><i></i><span>删除</span></asp:LinkButton></li>
                    </ul>
                </div>
                <div class="r-list">

                    <asp:TextBox ID="txtKeywords" runat="server" CssClass="keyword" />
                    <asp:LinkButton ID="lbtnSearch" runat="server" CssClass="btn-search" OnClick="btnSearch_Click">查询</asp:LinkButton>
                </div>
            </div>
        </div>



        <asp:Repeater ID="rptList" runat="server" OnItemCommand="rptList_ItemCommand" OnItemDataBound="rptList_ItemDataBound">
            <HeaderTemplate>
                <table width="100%" border="0" cellspacing="0" cellpadding="0" class="ltable">
                    <thead>
                        <tr>
                            <th>选择</th>
                            <th>编号</th>
                            <th>名称</th>
                            <th>运营人</th>
                            <th>公司电话</th>
                            <th>公司邮箱</th>
                            <th>操作</th>
                        </tr>
                    </thead>
                    <tbody class="ltbody">
            </HeaderTemplate>
            <ItemTemplate>
                <tr class="td_c">
                    <td>
                        <asp:CheckBox ID="chkId" CssClass="checkall" runat="server" Style="vertical-align: middle;" />
                        <asp:HiddenField ID="hidId" Value='<%#Eval("id")%>' runat="server" />
                    </td>
                    <td>
                        <%# Eval("HotelCode") %>
                    </td>
                    <td>
                        <%# Eval("hotelName") %>
                    </td>
                    <td>
                        <%# Eval("Operator") %>
                    </td>
                    <td>
                        <%# Eval("hotelPhone") %>
                    </td>
                    <td>
                        <%# Eval("noticeEmail") %>
                    </td>
                    <td>
                        <a href='hotel_register.aspx?hotelid=<%#Eval("id") %>&action=<%=MXEnums.ActionEnum.Edit.ToString() %>'>修改</a>
                        <a href='hotel_admin_list.aspx?hotelid=<%#Eval("id") %>'>管理员设置</a>
                        <a href="hotel_info.aspx?action=<%=MXEnums.ActionEnum.View.ToString() %>&hotelid=<%#Eval("id") %>">商户或门店信息查看</a>
                        <a href='hotel_room.aspx?hotelid=<%#Eval("id") %>&action=<%=MXEnums.ActionEnum.Audit.ToString() %>'>商品信息审核</a>
                        <a href='hotel_dingdan_manage.aspx?hotelid=<%#Eval("id") %>'>订单查看</a>
                    </td>
                </tr>
            </ItemTemplate>
            <FooterTemplate>
                <%#rptList.Items.Count == 0 ? "<tr><td align=\"center\" colspan=\"7\">暂无记录</td></tr>" : ""%>
                 </tbody>
                </table>
            </FooterTemplate>
        </asp:Repeater>
        <div class="line20"></div>
        <div class="pagelist">
            <div class="l-btns">
                <span>显示</span><asp:TextBox ID="txtPageNum" runat="server" CssClass="pagenum" onkeydown="return checkNumber(event);" OnTextChanged="txtPageNum_TextChanged" AutoPostBack="True"></asp:TextBox><span>条/页</span>
            </div>
            <div id="PageContent" runat="server" class="default"></div>
        </div>
    </form>
</body>
</html>
