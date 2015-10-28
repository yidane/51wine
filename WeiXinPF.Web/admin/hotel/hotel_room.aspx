<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="hotel_room.aspx.cs" ValidateRequest="false" Inherits="WeiXinPF.Web.admin.hotel.hotel_room" %>
<%@ Import Namespace="WeiXinPF.Common" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>房间类型信息</title>
    <script type="text/javascript" src="../../scripts/jquery/jquery-1.10.2.min.js"></script>
    <script type="text/javascript" src="../../scripts/lhgdialog/lhgdialog.js?skin=idialog"></script>
    <script type="text/javascript" src="../js/layout.js"></script>
    <link href="../skin/default/style.css" rel="stylesheet" type="text/css" />
    <link href="../skin/mystyle.css" rel="stylesheet" type="text/css" />
    <link href="../../css/pagination.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .badge {
            display: inline-block;
            min-width: 50px;
            padding: 5px 8px;
            font-size: 10px;
            line-height: 1;
            color: #fff;
            text-align: center;
            white-space: nowrap;
            vertical-align: baseline;
            background-color: #777;
            border-radius: 10px;
        }

        .sbumit {
            background-color: #5bc0de;
        }

        .agree {
            background-color: #428bca;
        }

        .refuse {
            background-color: #d9534f;
        }

        .publish {
            background-color: #5cb85c;
        }
    </style>
</head>
<body class="mainbody">
    <form id="form1" runat="server">
        <div class="location" runat="server" id="divLocation">
        </div>
        <div class="toolbar-wrap">
            <div id="floatHead" class="toolbar">
                <div class="l-list">
                    <ul class="icon-list">
                        <li runat="server" id="barAdd">
                            <a class="icon-btn add" href="hotel_room_info.aspx?action=<%=MXEnums.ActionEnum.Add %>&hotelid=<%=hotelid %>">
                                <i></i>
                                <span>新增商品</span>
                            </a>
                        </li>
                        <li><a class="all" href="javascript:;" onclick="checkAll(this);"><i></i><span>全选</span></a></li>
                        <li runat="server" id="barDelete">
                            <asp:LinkButton ID="btnDelete" runat="server" CssClass="del" OnClientClick="return ExePostBack('btnDelete');" OnClick="btnDelete_Click">
                                <i></i><span>删除</span>
                            </asp:LinkButton>
                        </li>
                        <li runat="server" id="barAgree">
                            <asp:LinkButton ID="btnAgree" runat="server" OnClientClick="return ExePostBack('btnAgree', '是否确认通过审核？');" OnClick="btnAgree_Click">审核通过</asp:LinkButton>
                        </li>

                        <li runat="server" id="barRefuse">
                            <asp:LinkButton ID="btnRefuse" runat="server" OnClientClick="return ExePostBack('btnRefuse', '是否确认拒绝申请？');" OnClick="btnRefuse_Click">审核不通过</asp:LinkButton>
                        </li>
                    </ul>
                </div>
                <div class="r-list">
                    <asp:TextBox ID="txtKeywords" runat="server" CssClass="keyword" />
                    <asp:LinkButton ID="lbtnSearch" runat="server" CssClass="btn-search" OnClick="btnSearch_Click">查询</asp:LinkButton>
                </div>
            </div>
        </div>
        <asp:Repeater ID="rptList" runat="server" OnItemCommand="rptList_ItemCommand">
            <HeaderTemplate>
                <table width="100%" border="0" cellspacing="0" cellpadding="0" class="ltable">
                    <thead>
                        <tr>
                            <th>选择</th>
                            <th>商品编号</th>
                            <th>商品名称</th>
                            <th>原价</th>
                            <th>优惠价</th>
                            <th>审核状态</th>
                            <th>操作</th>
                        </tr>
                    </thead>
                    <tbody class="ltbody">
            </HeaderTemplate>
            <ItemTemplate>
                <tr class="td_c">
                    <td>
                        <asp:CheckBox ID="chkId" CssClass="checkall" runat="server" Style="vertical-align: middle;" />
                        <asp:HiddenField ID="hidId" Value='<%# Eval("id")%>' runat="server" />
                    </td>
                    <td><%# Eval("RoomCode") %></td>
                    <td><%# Eval("roomType") %></td>
                    <td><%# Eval("roomPrice") %></td>
                    <td><%# Eval("salePrice") %></td>
                    <td><%# Eval("StatusStr") %></td>
                    <td>
                        <asp:LinkButton ID="lbtView" runat="server" CommandName="View" CommandArgument='<%#Eval("id") %>'>查看</asp:LinkButton>
                        <asp:LinkButton ID="lbtEdit" runat="server" CommandName="Edit" CommandArgument='<%#Eval("id")%>'>修改</asp:LinkButton>
                        <asp:LinkButton ID="lbtAudit" runat="server" CommandName="Audit" CommandArgument='<%#Eval("id")%>'>审核</asp:LinkButton>
                        <asp:LinkButton ID="lbtPublish" runat="server" CommandName="Publish" CommandArgument='<%#Eval("id")%>'>发布</asp:LinkButton>
                        <asp:LinkButton ID="lbtSoldOut" runat="server" CommandName="SoldOut" CommandArgument='<%#Eval("id")%>'>下架</asp:LinkButton>
                    </td>
                </tr>
            </ItemTemplate>
            <FooterTemplate>
                <%#rptList.Items.Count == 0 ? "<tr><td align=\"center\" colspan=\"12\">暂无记录</td></tr>" : ""%>
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
