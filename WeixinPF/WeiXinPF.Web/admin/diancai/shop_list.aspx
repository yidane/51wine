<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="shop_list.aspx.cs" Inherits="WeiXinPF.Web.admin.diancai.shop_list" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>商户或门店列表</title>
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
    </script>
    <style>
        a.shenghe {
            color: red;
        }

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
            <a href="javascript:;" class="home"><i></i><span>商户或门店列表</span></a>
        </div>
        <!--/导航栏-->

        <!--工具栏-->
        <div class="toolbar-wrap">
            <div id="floatHead" class="toolbar">
                <div class="l-list">
                    <ul class="icon-list">
                        <li><a class="icon-btn add" href="shop_register.aspx?action=<%=WeiXinPF.Common.MXEnums.ActionEnum.Add %>" id="itemAddButton"><i></i><span>商户或门店入驻登记</span></a></li>
                        <%-- <li><a class="icon-btn add" href="shop_setup.aspx?action=<%=WeiXinPF.Common.MXEnums.ActionEnum.Add %>&type=add"  id="itemButton"><i></i><span>商城设置</span></a></li>--%>
                        <li><%--<a class="icon-btn add" href="caidan_blacklist.aspx" id="itemBlackButton"><i></i><span>黑名单</span></a>--%></li>

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
        <!--/工具栏-->

        <!--列表-->

        <asp:Repeater ID="rptList" runat="server" OnItemCommand="rptList_ItemCommand" OnItemDataBound="rptList_ItemDataBound">
            <HeaderTemplate>
                <table width="100%" border="0" cellspacing="0" cellpadding="0" class="ltable">
                    <thead>
                        <tr>
                            <th>选择</th>
                            <th>商家名称</th>

                            <th>类型</th>
                            <%--<th >查看</th>--%>
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
                        <%# Eval("hotelName") %>
                    </td>
                    <td>
                        <%# Eval("kcType") %>
                        
                    </td>
                    <%--<td>
                     <a href="javascript:;"><%=yuming%>/weixin/diancai/index.aspx?wid=<%=wid %>&shopid=<%#Eval("id") %></a>
                    </td>--%>
                    <td>
                        <div class="btn-group" role="group" aria-label="...">
                            <a class="btn btn-default" href="shop_register.aspx?shopid=<%#Eval("id") %>&action=<%# WeiXinPF.Common.MXEnums.ActionEnum.Edit.ToString() %>">修改</a>
                            <a class="btn btn-default" href="shop_admin_list.aspx?shopid=<%#Eval("id") %>">管理员设置</a>
                            <a class="btn btn-default" href="shop_add.aspx?shopid=<%#Eval("id") %>&type=edite">商户或门店信息查看</a>
                            <a class="btn btn-default" href="caipin_manage.aspx?shopid=<%#Eval("id") %>">商品信息查看</a>
                            <a class="btn btn-default" href="dingdan_manage.aspx?shopid=<%#Eval("id") %>">订单查看</a>
                            <a class="btn btn-default" href="diancai_dingdanRefund_manage.aspx?shopid=<%#Eval("id") %>">退单查看</a>
                            <a class="btn btn-default" href="credentials_detail.aspx?shopid=<%#Eval("id") %>">服务凭据查看</a>
                            <%--<a  href='shop_setup.aspx?shopid=<%#Eval("id") %>&type=add'  >商城设置</a>--%>
                            <%--<a  href='caipin_category.aspx?shopid=<%#Eval("id") %>' ></a>--%>
                            <%--<a  href='desk_number.aspx?shopid=<%#Eval("id") %>' >桌号设置</a>--%>
                            <%--<a  href='caidan_member_manage.aspx?shopid=<%#Eval("id") %>' >会员管理</a>--%>
                            <%--<a  href='dianyuan_manage.aspx?shopid=<%#Eval("id") %>' >店员管理</a>--%>
                            <%--<a  href='caidan_baobiao.aspx?shopid=<%#Eval("id") %>' >统计图表</a>--%>
                            <%--<a  href='message_edite.aspx?shopid=<%#Eval("id") %>' >打印机设置</a>--%>
                            <%--<a  href='diancai_form.aspx?shopid=<%#Eval("id") %>' >表单设计</a>--%>
                            <%--<a  href='../../weixin/diancai/diancai_Login.aspx?shopid=<%#Eval("id") %>' target="_blank" >手机端管理</a>--%>
                        </div>

                    </td>
                </tr>
            </ItemTemplate>
            <FooterTemplate>
                <%#rptList.Items.Count == 0 ? "<tr><td align=\"center\" colspan=\"7\">暂无记录</td></tr>" : ""%>
                 </tbody>
                </table>
            </FooterTemplate>
        </asp:Repeater>

        <!--/列表-->

        <!--内容底部-->
        <div class="line20"></div>
        <div class="pagelist">
            <div class="l-btns">
                <span>显示</span><asp:TextBox ID="txtPageNum" runat="server" CssClass="pagenum" onkeydown="return checkNumber(event);" OnTextChanged="txtPageNum_TextChanged" AutoPostBack="True"></asp:TextBox><span>条/页</span>
            </div>
            <div id="PageContent" runat="server" class="default"></div>
        </div>
        <!--/内容底部-->
    </form>
</body>
</html>
