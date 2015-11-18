<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="scenic_list.aspx.cs" Inherits="WeiXinPF.Web.admin.scenic.scenic_list" %>

<%@ Import Namespace="WeiXinPF.Common" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">

<head runat="server">
    <title>景区导览列表</title>
    <script type="text/javascript" src="../../scripts/jquery/jquery-1.10.2.min.js"></script>
    <script type="text/javascript" src="../../scripts/jquery/Validform_v5.3.2_min.js"></script>
    <script type="text/javascript" src="../../scripts/lhgdialog/lhgdialog.js?skin=idialog"></script>
    <script type="text/javascript" src="../js/layout.js"></script>
    <link href="../skin/default/style.css" rel="stylesheet" type="text/css" />
    <link href="../../css/pagination.css" rel="stylesheet" type="text/css" />
    <style>
        .btn-group, .btn-group-vertical {
            position: relative;
            display: inline-block;
            vertical-align: middle;
        }


            .btn-group > .btn:first-child:not(:last-child):not(.dropdown-toggle) {
                border-top-right-radius: 0;
                border-bottom-right-radius: 0;
            }

            .btn-group > .btn:not(:first-child):not(:last-child):not(.dropdown-toggle) {
                border-radius: 0;
            }

            .btn-group > .btn:last-child:not(:first-child), .btn-group > .dropdown-toggle:not(:first-child) {
                border-top-left-radius: 0;
                border-bottom-left-radius: 0;
            }

            .btn-group .btn + .btn, .btn-group .btn + .btn-group, .btn-group .btn-group + .btn, .btn-group .btn-group + .btn-group {
                margin-left: -2px;
            }

            .btn-group-vertical > .btn, .btn-group > .btn {
                position: relative;
                float: left;
            }

                .btn-group > .btn:first-child {
                    margin-left: 0;
                }

            .btn-group-vertical > .btn, .btn-group > .btn {
                position: relative;
                float: left;
            }

        .btn-default {
            color: #333 !important;
            background-color: #fff !important;
            border-color: #ccc !important;
        }

        .btn {
            display: inline-block;
            padding: 6px 12px;
            margin-bottom: 0;
            font-size: 14px;
            font-weight: 400;
            line-height: 1.42857143;
            text-align: center;
            white-space: nowrap;
            vertical-align: middle;
            -ms-touch-action: manipulation;
            touch-action: manipulation;
            cursor: pointer;
            -webkit-user-select: none;
            -moz-user-select: none;
            -ms-user-select: none;
            user-select: none;
            background-image: none;
            border: 1px solid transparent;
            border-radius: 4px;
        }

        .btn-group-vertical > .btn.active, .btn-group-vertical > .btn:active, .btn-group-vertical > .btn:focus, .btn-group-vertical > .btn:hover, .btn-group > .btn.active, .btn-group > .btn:active, .btn-group > .btn:focus, .btn-group > .btn:hover {
            z-index: 2;
        }

        .btn-default:hover {
            color: #333 !important;
            background-color: #e6e6e6 !important;
            border-color: #adadad !important;
        }

        .btn.focus, .btn:focus, .btn:hover {
            color: #333 !important;
            text-decoration: none !important;
        }
    </style>
</head>
<body class="mainbody">
    <form id="form1" runat="server">
        <!--导航栏-->
        <div class="location">
            <a href="javascript:;" class="home"><i></i><span>景区导览列表</span></a>
        </div>
        <!--/导航栏-->

        <!--工具栏-->
        <div class="toolbar-wrap" style="display: none">
            <div id="floatHead" class="toolbar">
                <div class="l-list">
                    <ul class="icon-list">
                        <li><a class="add" href="scenic_edit.aspx?action=<%=MXEnums.ActionEnum.Add %>"><i></i><span>新增景区导览</span></a></li>

                        <li><a class="all" href="javascript:;" onclick="checkAll(this);"><i></i><span>全选</span></a></li>
                        <li>
                            <asp:LinkButton ID="btnDelete" runat="server" CssClass="del" OnClientClick="return ExePostBack('btnDelete','删除该分类将不可恢复，是否继续？');" OnClick="btnDelete_Click"><i></i><span>删除</span></asp:LinkButton></li>
                    </ul>
                </div>
                <div class="r-list">
                    <asp:TextBox ID="txtKeywords" runat="server" CssClass="keyword" />
                    <asp:LinkButton ID="lbtnSearch" runat="server" CssClass="btn-search" OnClick="lbtnSearch_Click">查询</asp:LinkButton>
                </div>
            </div>
        </div>
        <!--/工具栏-->

        <!--文字列表-->
        <asp:Repeater ID="rptList" runat="server">
            <HeaderTemplate>
                <table width="100%" border="0" cellspacing="0" cellpadding="0" class="ltable">
                    <tr>
                        <th width="6%">选择</th>
                        <th>标题</th>
                        <th>使用地址</th>
                        <th width="12%">操作</th>
                    </tr>
            </HeaderTemplate>
            <ItemTemplate>
                <tr>
                    <td align="center">
                        <asp:CheckBox ID="chkId" CssClass="checkall" runat="server" Style="vertical-align: middle;" /><asp:HiddenField ID="hidId" Value='<%#Eval("id")%>' runat="server" />
                    </td>
                    <td style="text-align: center;"><%#Eval("Name") %></td>
                    <td><%=MyCommFun.getWebSite() %>/weixin/scenic/index.aspx?id=<%#Eval("Id") %></td>
                    <td style="text-align: center">
                            <a class="btn btn-default" style="display: none;" href="scenic_edit.aspx?action=<%#MXEnums.ActionEnum.Edit %>&id=<%#Eval("Id")%>">修改</a>
                        
                        <div class="btn-group" role="group" aria-label="...">
                            <a class="btn btn-default" href="scenic_detail_list.aspx?scenicId=<%#Eval("Id")%>">添加景点</a>
                        </div>
                    </td>
                </tr>
            </ItemTemplate>
            <FooterTemplate>
                <%#rptList.Items.Count == 0 ? "<tr><td align=\"center\" colspan=\"5\">暂无记录</td></tr>" : ""%>
            </table>
            </FooterTemplate>
        </asp:Repeater>
        <!--/文字列表-->
        <!--内容底部-->
        <div class="line20"></div>
        <div class="pagelist">
            <div class="l-btns">
                <span>显示</span>
                <asp:TextBox ID="txtPageNum" runat="server" CssClass="pagenum" onkeydown="return checkNumber(event);" OnTextChanged="txtPageNum_TextChanged" AutoPostBack="True"></asp:TextBox><span>条/页</span>
            </div>
            <div id="PageContent" runat="server" class="default"></div>
        </div>
        <!--/内容底部-->
    </form>
</body>
</html>
