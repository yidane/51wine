<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="diancai_dingdanRefund_manage.aspx.cs" Inherits="WeiXinPF.Web.admin.diancai.diancai_dingdanRefund_manage" %>

<!DOCTYPE html>

<html>
<head id="Head1" runat="server">
    <title>订单管理</title>
    <script type="text/javascript" src="../../scripts/jquery/jquery-1.10.2.min.js"></script>
    <script type="text/javascript" src="../../scripts/lhgdialog/lhgdialog.js?skin=idialog"></script>
    <script type="text/javascript" src="../js/layout.js"></script>
    <link href="../skin/default/style.css" rel="stylesheet" type="text/css" />
    <link href="../skin/mystyle.css" rel="stylesheet" type="text/css" />
    <link href="../../css/pagination.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../../scripts/datepicker/WdatePicker.js"></script>
    <script type="text/javascript">
        function parentToIndex(id) {
            parent.location.href = "/admin/Index.aspx?id=" + id;

        }
    </script>
    <style>
        a.shenghe {
            color: red;
        }

        .DingdanButton {
            border: 1px solid #3d810c;
            box-shadow: 0 1px 1px #aaa;
            -moz-box-shadow: 0 1px 1px #aaa;
            -webkit-box-shadow: 0 1px 1px #aaa;
            padding: 5px 20px;
            cursor: pointer;
            display: inline-block;
            text-align: center;
            vertical-align: bottom;
            overflow: visible;
            border-radius: 3px;
            -moz-border-radius: 3px;
            -webkit-border-radius: 3px;
            *zoom: 1;
            background-color: #f1f1f1;
            background-image: linear-gradient(bottom, #DCDADA 3%, #f9f9f9 97%, #fff 100%);
            background-image: -moz-linear-gradient(bottom, #DCDADA 3%, #f9f9f9 97%, #fff 100%);
            background-image: -webkit-linear-gradient(bottom, #DCDADA 3%, #f9f9f9 97%, #fff 100%);
            color: #000;
            border: 1px solid #AAA;
            font-size: 14px;
            line-height: 1.5;
            background-color: #16a0d3;
        }
    </style>
</head>
<body class="mainbody">
    <form id="form1" runat="server">

        <div class="location">
            <% if (IsWeiXinCode())
               {%>
            <a href="#" class="home"><i></i><span>商户或门店列表</span></a>
            <i class="arrow"></i>
            <%}%>
            <%
               else
               {%>

            <%} %>
            <span>订单管理</span>
        </div>
        <div class="line10"></div>
        <!--/导航栏-->
        <%--         <div class="mytips">        
            1、确认订单后，用户不能再取消该订单；被设置为无效的订单，将成为失败订单！
             <br />
            2、交易成功的订单会计入销售统计；交易失败的订单不会计入销售统计，多次交易失败的客户建议加入黑名单！
         </div>--%>
        <%--<div class="tab-content">--%>
        <table border="0" cellspacing="0" cellpadding="0" class="border-table" width="98%">
            <tr>
                <td align="right">退单时间
                </td>
                <td>
                    <asp:TextBox ID="txtbeginDate" runat="server" CssClass="input date" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd HH:mm:ss'})" datatype="*1-50" errormsg="请选择正确的日期" sucmsg=" " nullmsg=" " />
                    至
                        <asp:TextBox ID="txtEndDate" runat="server" CssClass="input date" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd HH:mm:ss'})" datatype="*1-50" errormsg="请选择正确的日期" sucmsg=" " nullmsg=" " /></td>
                <td align="right">退单号</td>
                <td>
                    <asp:TextBox ID="txtRefundNo" runat="server" CssClass="input normal" datatype="n" sucmsg=" " Text="100" /></td>
                <td align="right">订单号</td>
                <td>
                    <asp:TextBox ID="txtOrderNumber" runat="server" CssClass="input normal" datatype="n" sucmsg=" " Text="100" />
                </td>
            </tr>
            <tr>
                <td align="right">退单状态</td>
                <td>
                    <%--<div class="rule-single-select">--%>
                    <asp:DropDownList runat="server" ID="dboRefundStatus" CssClass="select" Width="293">
                        <asp:ListItem Text="退款审核中" Value="3"></asp:ListItem>
                        <asp:ListItem Text="已退款" Value="4"></asp:ListItem>
                        <asp:ListItem Text="审核不通过" Value="5"></asp:ListItem>
                    </asp:DropDownList>
                    <%--</div>--%>
                </td>
                <td align="right">退单人</td>
                <td>
                    <asp:TextBox ID="txtCustomerName" runat="server" CssClass="input normal" datatype="*1-300" sucmsg=" " Text="" /></td>
                <td align="right">退单电话</td>
                <td>
                    <asp:TextBox ID="txtCustomerTel" runat="server" CssClass="input normal" datatype="*1-300" sucmsg=" " Text="" /></td>
            </tr>
        </table>
        <div style="width: 100%; text-align: center">
            <asp:Button ID="btnSearch" runat="server" Text="查询" CssClass="DingdanButton" OnClick="btnSearch_Click" />
            <asp:Button ID="btnExport" runat="server" Text="导出Excel" CssClass="DingdanButton" OnClick="btnExport_OnClick" />
        </div>
        <!--工具栏-->
        <div class="toolbar-wrap">
            <div id="floatHead" class="toolbar">
                <div class="l-list">
                    <ul class="icon-list">
                        <li><a class="all" href="javascript:;" onclick="checkAll(this);"><i></i><span>全选</span></a></li>
                        <li>
                            <asp:LinkButton ID="btnDelete" runat="server" CssClass="del" OnClientClick="return ExePostBack('btnDelete');" OnClick="btnDelete_Click"><i></i><span>删除</span></asp:LinkButton></li>
                    </ul>
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
                            <th>序号</th>
                            <th>退单号</th>
                            <th>订单号</th>
                            <th>预订人</th>
                            <th>电话</th>
                            <th>预定时间</th>
                            <th>退单商品</th>
                            <th>退款总额</th>
                            <th>退单状态</th>
                            <th>操作</th>
                        </tr>
                    </thead>
                    <tbody class="ltbody">
            </HeaderTemplate>
            <ItemTemplate>
                <tr class="td_c">
                    <td>
                        <asp:CheckBox ID="chkId" CssClass="checkall" runat="server" Style="vertical-align: middle;" />
                    </td>
                    <td>
                        <%# Eval("OrderNo") %>
                    </td>
                    <td>
                        <%# Eval("RefundNumber") %>
                    </td>
                    <td>
                        <%# Eval("orderNumber") %>
                    </td>
                    <td>
                        <%# Eval("customerName") %>
                    </td>
                    <td>
                        <%# Eval("customerTel") %>                        
                    </td>
                    <td>
                        <%# Eval("RefundTime") %>                        
                    </td>
                    <td>
                        <%# Eval("detail") %>                        
                    </td>
                    <td>&yen; <%# Eval("RefundAmount") %>                      
                    </td>
                    <td>
                        <%# Eval("refundStatusDesc") %>                        
                    </td>
                    <td>
                        <a href='diangdan_refundDetail.aspx?shopid=<%#Eval("shopid") %>&dingdanid=<%#Eval("DingdanID") %>&id=<%#Eval("RefundNumber") %>'>操作</a>
                    </td>
                </tr>
            </ItemTemplate>
            <FooterTemplate>
                <%#rptList.Items.Count == 0 ? "<tr><td align=\"center\" colspan=\"11\">暂无记录</td></tr>" : ""%>
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
        
        <asp:HiddenField runat="server" ID="total" Value="0"/>
    </form>
</body>
</html>
