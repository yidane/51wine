<%@ Page Title="" Language="C#" MasterPageFile="~/ListContent.Master" AutoEventWireup="true" CodeBehind="OrderStatistics.aspx.cs" Inherits="Travel.Presentation.WebPlugin.OrderStatistics" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!--导航栏-->
    <div class="location">
        <a class="home"><i></i><span>内容列表</span></a>
    </div>
    <!--/导航栏-->

    <!--工具栏-->
    <div class="toolbar-wrap">
        <!-- 查询面板 S -->
        <div class="query-panel">
            <div class="tab-subnav">
                <a href='#' data-target="#batchQuery" class="on">批量订单查询</a>
            </div>
            <input type="hidden" id="token" name="ecc_csrf_token" value="7f625c239ff4beeb0a42a8b084df515e">

            <div class="tab-subcon">
                <!-- 批量订单查询 S -->
                <div class="" id="batchQuery">

                    <div class="form-group">
                        <div class="form-item">
                            <label class="label" for="">交易时间：</label>
                            <div class="input-date">
                                <span class="btn datepicker-switch">
                                    <asp:TextBox ID="txtbeginDate" runat="server" CssClass="input date" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd HH:mm:ss'})" datatype="*1-50" errormsg="请选择正确的日期" sucmsg=" " nullmsg=" " />
                                    <i>开始时间</i>
                                </span>
                            </div>
                            <span class="gaps">到</span>
                            <div class="input-date">
                                <span class="btn datepicker-switch">
                                    <asp:TextBox ID="txtendDate" runat="server" CssClass="input date" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd HH:mm:ss'})" datatype="*1-50" errormsg="请选择正确的日期" sucmsg=" " nullmsg=" " />
                                    <i>结束时间</i>
                                </span>
                                <span class="tips-error hide" id="ErrTipTime"></span>
                            </div>
                        </div>
                    </div>
                    <div class="form-group more hide">
                        <div class="form-item">
                            <label class="label" for="">交易状态：</label>
                            <asp:DropDownList ID="ddlCategoryStatus" runat="server"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="form-group more hide">
                        <div class="form-item">
                            <label class="label" for="">票种类：</label>
                            <asp:DropDownList ID="ddlTicketCategory" runat="server"></asp:DropDownList>
                        </div>
                    </div>

                    <div class="form-group more hide">
                        <div class="form-item">
                            <label class="label" for="">交易金额：</label>
                            <asp:TextBox runat="server" ID="txtMinAmount"></asp:TextBox>
                            <span class="gaps">到</span>
                            <asp:TextBox runat="server" ID="txtMaxAmount"></asp:TextBox>
                        </div>
                    </div>

                    <div class="form-group">
                        <div class="form-item">
                            <a class="btn btn-primary" id="batchQueryButton" runat="server" onserverclick="btnSearch_Click">查询</a>
                            <a href="#" id="moreSearch" onclick="AdvanceSearch()">显示高级选项</a>
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
                    <th align="center" width="8%">票名称</th>
                    <th align="center" width="8%">票数量</th>
                    <th width="8%">票单价</th>
                    <th width="8%">票总价</th>
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
                <td><%#Eval("TicketCount")%></td>
                <td><%#Eval("TicketPrice")%></td>
                <td><%#Eval("TotalPrice")%></td>
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
            if ($(".more").hasClass('hide')) {
                $(".more").removeClass('hide');
                $("#moreSearch").text('隐藏高级选项');
            } else {
                $(".more").addClass('hide');
                $("#moreSearch").text('显示高级选项');
            }
        }
    </script>
</asp:Content>
