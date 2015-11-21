<%@ Page Title="" Language="C#" MasterPageFile="~/ListContent.Master" AutoEventWireup="true" CodeBehind="OrderStatistics.aspx.cs" Inherits="Travel.Presentation.WebPlugin.OrderStatistics" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <style type="text/css">
        .d {
        }

        .dHide {
            /*visibility: hidden;*/
        }

        .shownone {
            display: none !important;
        }
    </style>
    <!--导航栏-->
    <div class="location">
        <a class="home"><i></i><span>内容列表</span></a>
    </div>
    <!--/导航栏-->

    <!--工具栏-->
    <div class="tab-content">
        <!-- 查询面板 S -->
        <div class="query-panel">
            <div class="tab-subnav">
                <a href='#' data-target="#batchQuery" class="on">批量订单查询</a>
            </div>
            <div class="tab-subcon">
                <!-- 批量订单查询 S -->
                <div class="" id="batchQuery">
                    <dl>
                        <dt>开始时间：</dt>
                        <dd>
                            <div class="input-date">
                                <asp:TextBox ID="txtbeginDate" runat="server" CssClass="input date" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd'})" datatype="*1-50" errormsg="请选择正确的日期" sucmsg=" " nullmsg=" " />
                                <i>日期</i>
                            </div>
                            <span class="gaps">到</span>
                            <div class="input-date">
                                <asp:TextBox ID="txtendDate" runat="server" CssClass="input date" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd'})" datatype="*1-50" errormsg="请选择正确的日期" sucmsg=" " nullmsg=" " />
                                <i>日期</i>
                            </div>
                        </dd>
                    </dl>
                    <dl class="d dHide">
                        <dt>交易状态：</dt>
                        <dd>
                            <div class="rule-single-select">
                                <asp:DropDownList ID="ddlCategoryStatus" runat="server"></asp:DropDownList>
                            </div>
                        </dd>
                    </dl>
                    <dl class="d dHide">
                        <dt>票种类：</dt>
                        <dd>
                            <div class="rule-single-select">
                                <asp:DropDownList ID="ddlTicketCategory" runat="server" datatype="*" sucmsg=" "></asp:DropDownList>
                            </div>
                        </dd>
                    </dl>
                    <dl class="d dHide shownone">
                        <dt>交易金额：</dt>
                        <dd>
                            <asp:TextBox runat="server" ID="txtMinAmount" CssClass="input"></asp:TextBox>
                            <span class="gaps">到</span>
                            <asp:TextBox runat="server" ID="txtMaxAmount" CssClass="input"></asp:TextBox>
                        </dd>
                    </dl>
                    <div class="form-group">
                        <div class="form-item">
                            <a class="btn" id="batchQueryButton" runat="server" onserverclick="btnSearch_Click">查询</a>
                            <a class="btn" id="btnDownloadSearchResult" runat="server" onserverclick="btnDownloadSearchResult_Click">下载</a>
                            <%--<a href="#" id="moreSearch" onclick="AdvanceSearch()">显示高级选项</a>--%>
                        </div>
                    </div>
                </div>
                <!-- 批量订单查询 E -->
            </div>
        </div>
        <!-- 查询面板 E -->
    </div>
    <!--/工具栏-->

    <!--文字列表-->
    <asp:Repeater ID="rptList" runat="server">
        <HeaderTemplate>
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="ltable">
                <tr>
                    <th width="180">订单号</th>
                    <th align="left" width="22%">交易时间</th>
                    <th align="left" width="12%">联系人</th>
                    <th align="left" width="20%">联系电话</th>
                    <th align="left" width="22%">联系人身份证</th>
                    <th align="left" width="14%">订单状态</th>
                    <th align="center" width="180">票名称</th>
                    <th align="center" width="8%">票数量</th>
                    <th align="center" width="8%">票单价(元)</th>
                    <th align="center" width="8%">票总价(元)</th>
                </tr>
        </HeaderTemplate>
        <ItemTemplate>
            <tr>
                <td><%#Eval("TradeNo") %></td>
                <td><%#Eval("TradeTime") %></td>
                <td><%#Eval("ContactPersonName")%></td>
                <td><%#Eval("MobilePhoneNumber")%></td>
                <td><%#Eval("IdentityCardNumber") %></td>
                <td><%#Eval("OrderStatus")%></td>
                <td><%#Eval("TicketCategoryName")%></td>
                <td align="center"><%#Eval("TicketCount")%></td>
                <td align="center"><%#Eval("TicketPrice")%></td>
                <td align="center"><%#Eval("TotalPrice")%></td>
            </tr>
        </ItemTemplate>
        <FooterTemplate>
            <%#rptList.Items.Count == 0 ? "<tr><td align=\"center\" colspan=\"8\">暂无记录</td></tr>" : ""%>
            </table>
        </FooterTemplate>
    </asp:Repeater>
    <!--/文字列表-->
    <!--内容底部-->
    <div class="line20"></div>
    <div class="pagelist">
        <div class="l-btns">
            <span>显示</span><asp:TextBox ID="txtPageNum" runat="server" CssClass="pagenum" onkeydown="return checkNumber(event);" OnTextChanged="txtPageNum_TextChanged" AutoPostBack="True"></asp:TextBox><span>条/页</span>
        </div>
        <div id="PageContent" runat="server" class="default"></div>
    </div>
    <!--/内容底部-->

    <script language="javascript">
        function AdvanceSearch() {
            if ($(".d").hasClass('dHide')) {
                $(".d").removeClass('dHide');
                $("#moreSearch").text('隐藏高级选项');
            } else {
                $(".d").addClass('dHide');
                $("#moreSearch").text('显示高级选项');
            }
        }
    </script>
</asp:Content>
