<%@ Page Title="" Language="C#" MasterPageFile="~/ListContent.Master" AutoEventWireup="true" CodeBehind="ProductList.aspx.cs" Inherits="Travel.Presentation.WebPlugin.ProductList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!--导航栏-->
    <div class="location">
        <a class="home"><i></i><span>单页列表</span></a>

    </div>
    <!--/导航栏-->

    <!--工具栏-->
    <div class="toolbar-wrap">
        <div id="floatHead" class="toolbar">
            <div class="l-list">
                <ul class="icon-list">
                    <li><a class="add" href="ProductEdit.aspx"><i></i><span>新增单页</span></a></li>
                    <li><a class="all" href="javascript:;" onclick="checkAll(this);"><i></i><span>全选</span></a></li>
                    <li>
                        <asp:LinkButton ID="btnDelete" runat="server" CssClass="del" OnClick="btnDelete_Click"><i></i><span>删除</span></asp:LinkButton></li>
                </ul>
            </div>
            <%--            <div class="r-list">
                <asp:TextBox ID="txtKeywords" runat="server" CssClass="keyword" />
                <asp:LinkButton ID="lbtnSearch" runat="server" CssClass="btn-search" OnClick="btnSearch_Click">查询</asp:LinkButton>
            </div>--%>
        </div>
    </div>
    <!--/工具栏-->

    <!--文字列表-->
    <asp:Repeater ID="rptList1" runat="server">
        <HeaderTemplate>
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="ltable">
                <tr>
                    <th width="6%">选择</th>
                    <th align="left">产品名称</th>
                    <th align="left">原产品名称</th>
                    <th align="left" width="129">产品类型</th>
                    <th align="left" width="65">产品编码</th>
                    <th align="left" width="110">产品价格</th>
                    <th align="left" width="65">是否组合产品</th>
                    <th align="left" width="65">是否自定义产品</th>
                    <th align="left" width="65">当前状态</th>
                    <th align="left" width="65">一级排序</th>
                    <th align="left" width="65">二级排序</th>
                    <th width="8%">操作</th>
                </tr>
        </HeaderTemplate>
        <ItemTemplate>
            <tr>
                <td align="center">
                    <asp:CheckBox ID="chkId" CssClass="checkall" runat="server" Style="vertical-align: middle;" /><asp:HiddenField ID="hdProductID" Value='<%#Eval("ProductID")%>' runat="server" />
                    <asp:HiddenField ID="hdProductPackageID" Value='<%#Eval("ProductPackageID")%>' runat="server" />
                    <asp:HiddenField ID="hdProductSource" Value='<%#Eval("ProductSource")%>' runat="server" />
                </td>
                <td><a href="ProductEdit.aspx?ProductID=<%#Eval("ProductID")%>&ProductPackageID=<%#Eval("ProductPackageID") %>&ProductSource=<%#Eval("ProductSource")%>"><%#Eval("ProductName")%></a></td>
                <td>
                    <%#Eval("OldProductName")%>
                </td>
                <td><%#Eval("ProductCategoryId")%></td>
                <td>
                    <%#Eval("ProductCode")%>
                </td>
                <td>
                    <%#Eval("ProductPrice")%>
                </td>
                <td>
                    <%#Eval("IsCombinedProduct")%>
                </td>
                <td>
                    <%#Eval("IsSelfDefinedProduct")%>
                </td>
                <td>
                    <%#Eval("CurrentStatus")%>
                </td>
                <td>
                    <%#Eval("FirstSort")%>
                </td>
                <td>
                    <%#Eval("SecondSort")%>
                </td>
            </tr>
        </ItemTemplate>
        <FooterTemplate>
            <%#rptList1.Items.Count == 0 ? "<tr><td align=\"center\" colspan=\"7\">暂无记录</td></tr>" : ""%>
</table>
        </FooterTemplate>
    </asp:Repeater>
    <!--/文字列表-->

    <!--内容底部-->
    <div class="line20"></div>
    <div class="pagelist">
        <div class="l-btns">
            <span>显示</span><asp:TextBox ID="txtPageNum" runat="server" CssClass="pagenum" onkeydown="return checkNumber(event);" AutoPostBack="True"></asp:TextBox><span>条/页</span>
        </div>
        <div id="PageContent" runat="server" class="default"></div>
    </div>
    <!--/内容底部-->
</asp:Content>
