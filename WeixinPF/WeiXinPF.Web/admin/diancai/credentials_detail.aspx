<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="credentials_detail.aspx.cs" Inherits="WeiXinPF.Web.admin.diancai.credentials_detail" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>服务凭据查询</title>
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
            color: red;
        }
    </style>
</head>
<body class="mainbody">
    <form id="form1" runat="server">
        <div class="location">
            <a href="javascript:;" class="home"><i></i><span>服务凭据管理</span></a>
            <i class="arrow"></i>
            <span>服务凭据查询</span>
        </div>
        <!--/导航栏-->

        <!--工具栏-->
        <div class="toolbar-wrap">
            <div id="floatHead" class="toolbar">
                <div class="l-list">
                    <div class="credentials-condition">
                        <span>订单关闭日期范围</span>
                        <asp:TextBox ID="startDate" runat="server"></asp:TextBox>
                        至
                        <asp:TextBox ID="endDate" runat="server"></asp:TextBox>
                    </div>
                    <div class="credentials-condition">
                        <span>订单支付金额</span>
                        <asp:TextBox ID="paidmin" runat="server"></asp:TextBox>至<asp:TextBox ID="paidmax" runat="server"></asp:TextBox>
                    </div>
                </div>
                <div class="r-list">
                    <div class="credentials-condition">
                        <span>订单编号</span>
                        <asp:TextBox ID="dingdanId" runat="server"></asp:TextBox>
                    </div>
                    <div class="credentials-condition">
                        <span>预约人</span>
                        <asp:TextBox ID="orderperson" runat="server"></asp:TextBox>
                    </div>
                </div>
                <div class="center-button">
                    <asp:Button ID="Button1" runat="server" Text="查询"></asp:Button>
                    <%--<asp:Button ID="Button2" runat="server" Text="导出Excel"></asp:Button>--%>
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
                            <th>订单编号</th>
                            <th>订单状态</th>
                            <th>订单关闭日期</th>
                            <th>预约人</th>
                            <th>总计</th>
                            <th></th>
                        </tr>
                    </thead>
                    <tbody class="ltbody">
            </HeaderTemplate>
            <ItemTemplate>
                <tr class="td_c">
                    <td>
                        <asp:HiddenField ID="HiddenField1" Value='<%#Eval("id")%>' runat="server" />
                        <%# Eval("orderNumber") %>
                    </td>
                    <td>
                        <%# Eval("payStatus") %>
                    </td>
                    <td>
                        <%# Eval("modifyTime") %>                        
                    </td>
                    <td>
                        <%# Eval("customerName") %>                        
                    </td>
                    <td>
                        <%# Eval("payAmount") %>                        
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td colspan="4"></td>
                    <td colspan="2">
                        <asp:Repeater runat="server" ID="rp">
                            <HeaderTemplate>
                                <table cellspacing="0" rules="all" border="1" id="CommodityList" style="border-collapse: collapse; width: 100%; margin: 1px 0px 5px 33px;" class="Repeater">
                                    <tr>
                                        <th scope="col">商品名称
                                        </th>
                                        <th scope="col">数量
                                        </th>
                                        <th scope="col">价格
                                        </th>
                                        <th scope="col">验证码
                                        </th>
                                        <th scope="col">验证码状态
                                        </th>
                                        <th scope="col">验证日期
                                        </th>
                                    </tr>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr>
                                    <td>
                                        <%# Eval("caiName") %>
                                    </td>
                                    <td>
                                        <%# Eval("number") %>
                                    </td>
                                    <td>
                                        <%# Eval("price") %>
                                    </td>
                                    <td>
                                        <%# Eval("identifyingcode") %>
                                    </td>
                                    <td>
                                        <%# Eval("status") %>
                                    </td>
                                    <td>
                                        <%# Eval("ModifyTime") %>
                                    </td>
                                </tr>
                            </ItemTemplate>
                            <FooterTemplate>
                                </table>
                            </FooterTemplate>
                        </asp:Repeater>
                    </td>
                </tr>
            </ItemTemplate>
            <FooterTemplate>
                <%#rptList.Items.Count == 0 ? "<tr><td align=\"center\" colspan=\"6\">暂无记录</td></tr>" : ""%>
                 </tbody>
                </table>
            </FooterTemplate>
        </asp:Repeater>

        <!--/列表-->

        <!--内容底部-->
        <div class="line20"></div>
        <div class="pagelist">
            <div class="footer-right">
                <span>总计</span><span><%=totalAmount %></span><span>元</span>
            </div>
            <div id="PageContent" runat="server" class="default"></div>
        </div>
    </form>
</body>
</html>

