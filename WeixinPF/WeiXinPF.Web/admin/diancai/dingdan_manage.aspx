<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="dingdan_manage.aspx.cs" Inherits="WeiXinPF.Web.admin.diancai.dingdan_manage" %>

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

        $(function () {


        });

    </script>
    <style>
        a.shenghe {
            color: red;
        }

        .Width1 {
            width: 16.6%;
            text-align: right;
        }

        .Width2 {
            width: 33.3%;
            text-align: right;
        }

        .Width3 {
            width: 45%;
            padding: 5px 4px;
            min-height: 20px;
            line-height: 20px;
            border: 1px solid #d4d4d4;
            background: #fff;
            vertical-align: middle;
            color: #333;
            font-size: 100%;
        }
    </style>
</head>
<body class="mainbody">
    <form id="form1" runat="server">

        <div class="location">
               <% if (IsWeiXinCode())
                {%>
            <a    href="shop_list.aspx" class="home"><i></i><span>商户或门店列表</span></a>
              <i class="arrow"></i>
                <%}%>
                <%
                else
                {%>
               
                <%} %>
            <span>订单管理</span>
        </div>
        <table border="0" cellspacing="0" cellpadding="0" class="border-table" width="98%">
            <tr>
                <td align="right">交易时间
                </td>
                <td>
                    <asp:TextBox ID="txtbeginDate" runat="server" CssClass="input date Width3" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd HH:mm:ss'})" datatype="*1-50" errormsg="请选择正确的日期" sucmsg=" " nullmsg=" " />
                    至
                        <asp:TextBox ID="txtEndDate" runat="server" CssClass="input date Width3" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd HH:mm:ss'})" datatype="*1-50" errormsg="请选择正确的日期" sucmsg=" " nullmsg=" " /></td>
                <td align="right">交易金额</td>
                <td>
                    <asp:TextBox ID="txtPayAmountMin" runat="server" CssClass="Width3" datatype="n" sucmsg=" " Text="100" />
                    至
                        <asp:TextBox ID="txtPayAmountMax" runat="server" CssClass="Width3" datatype="n" sucmsg=" " Text="100" /></td>
                <td></td>
                <td></td>
            </tr>
            <tr>
                <td align="right">订单号</td>
                <td align="right">
                    <asp:TextBox ID="txtOrderNumber" runat="server" CssClass="input normal" datatype="*1-300" sucmsg=" " Text="" /></td>
                <td align="right">预约人</td>
                <td align="right">
                    <asp:TextBox ID="txtCustomerName" runat="server" CssClass="input normal" datatype="*1-300" sucmsg=" " Text="" /></td>
                <td align="right">预约电话</td>
                <td align="right">
                    <asp:TextBox ID="txtCustomerTel" runat="server" CssClass="input normal" datatype="*1-300" sucmsg=" " Text="" /></td>
            </tr>
        </table>
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
                            <th>序号</th>
                            <th>订单号</th>
                            <th>预订人</th>
                            <th>电话</th>
                            <th>预定时间</th>
                            <th>预订套餐</th>
                            <th>总价</th>
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
                        <%# Eval("OrderNo") %>
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
                        <%# Eval("createDate") %>                        
                    </td>
                    <td>
                        <%# Eval("detail") %>                        
                    </td>
                    <td>&yen; <%# Eval("payAmount") %>                        
                    </td>
                    <td>
                        <a href='dingdan_deal.aspx?id=<%#Eval("id") %>&shopid=<%=shopid %>'>操作</a>
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
    </form>
</body>
</html>
