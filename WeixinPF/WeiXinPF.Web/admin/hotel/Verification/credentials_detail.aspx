<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="credentials_detail.aspx.cs" Inherits="WeiXinPF.Web.admin.hotel.Verification.credentials_detail" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>服务凭据查询</title>
    <script type="text/javascript" src="../../../scripts/jquery/jquery-1.10.2.min.js"></script>
    <script type="text/javascript" src="../../../scripts/lhgdialog/lhgdialog.js?skin=idialog"></script>
    <script type="text/javascript" src="../../js/layout.js"></script>
    <script type="text/javascript" src="../../../scripts/datepicker/WdatePicker.js"></script>
    <link href="../../skin/default/style.css" rel="stylesheet" type="text/css" />
    <link href="../../skin/mystyle.css" rel="stylesheet" type="text/css" />
    <link href="../../../css/pagination.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .label-left {
            width: 200px;
        }

        .serchbtn {
            margin: auto;
        }

        .center-button {
            text-align: center;
        }

        .l-list {
        }

        .increase, .reduce {
            display: inline-block;
            width: 35px;
            height: 38px;
            vertical-align: -2px;
        }

        .ico_increase, .ico_reduce {
            position: relative;
            display: block;
            width: 25px;
            height: 25px;
            margin: 6px 0 0 5px;
            background: #D00A0A;
            border-radius: 50%;
            text-indent: -9999px;
        }

        .ico_increase, .ico_reduce {
            background: #D00A0A;
        }

            .ico_increase:after, .ico_reduce:after {
                position: absolute;
                top: 11px;
                left: 5px;
                content: "";
                display: block;
                width: 15px;
                height: 3px;
                background: #FFFFFF;
            }

            .ico_increase:before {
                position: absolute;
                top: 5px;
                left: 11px;
                content: "";
                display: block;
                width: 3px;
                height: 15px;
                background: #FFFFFF;
            }
    </style>
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
            <span>服务凭据查询</span>
        </div>
        <!--/导航栏-->

        <!--工具栏-->
        <div class="toolbar-wrap">
            <div id="floatHead" class="toolbar">
                <table border="0" cellspacing="0" cellpadding="0" class="border-table" width="98%">
                    <tr>
                        <td align="right">订单关闭日期范围
                        </td>
                        <td>
                            <asp:TextBox ID="startDate" runat="server" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd'})" datatype="*1-50" errormsg="请选择正确的日期" sucmsg=" " nullmsg=" "></asp:TextBox>
                            至
                                <asp:TextBox ID="endDate" runat="server" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd'})" datatype="*1-50" errormsg="请选择正确的日期" sucmsg=" " nullmsg=" "></asp:TextBox>
                            <td align="right">订单编号</td>
                            <td>
                                <asp:TextBox ID="dingdanId" runat="server"></asp:TextBox>
                                <td align="right">预约人</td>
                                <td>
                                    <asp:TextBox ID="orderperson" runat="server"></asp:TextBox>
                                </td>
                    </tr>
                    <tr>
                        <td align="right">订单支付金额</td>
                        <td>
                            <asp:TextBox ID="paidmin" runat="server"></asp:TextBox>
                            至
                            <asp:TextBox ID="paidmax" runat="server"></asp:TextBox>
                        </td>
                        <td align="right"></td>
                        <td>
                            <td align="right"></td>
                            <td>
                    </tr>
                </table>

                <div class="center-button">
                    <asp:Button ID="serch" runat="server" Text="查询" CssClass="DingdanButton" OnClick="serch_OnClick"></asp:Button>
                    <asp:Button ID="btnExport" runat="server" Text="导出Excel" CssClass="DingdanButton" OnClick="btnExport_OnClick" />

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
                            <th style="width: 250px">订单编号</th>
                            <th style="width: 100px">订单状态</th>
                            <th style="width: 100px">订单关闭日期</th>
                            <th style="width: 100px">预约人</th>
                            <th style="width: 50px">数量</th>
                            <th style="width: 100px">商品名称</th>
                            <th style="width: 100px">入住日期</th>
                            <th style="width: 100px">离店日期</th>
                            <th style="width: 100px">商品单价</th>
                            <th style="width: 100px">支付总额</th>
                            <th style="width: 100px">退款总额</th>
                            <th style="width: 100px">实际交易总额</th>
                        </tr>
                    </thead>
                    <tbody class="ltbody">
            </HeaderTemplate>
            <ItemTemplate>
                <tr class="td_c">
                    <td >
                        <asp:HiddenField ID="HiddenField1" Value='<%#Eval("Id")%>' runat="server" />
                        <a id="notdisplay<%#Eval("Id")%>" href="javascript:f_NotDisplay(<%#Eval("Id")%>);" class="reduce" style="display: none;"><b class="ico_reduce">加一份</b></a>
                        <a id="display<%#Eval("Id")%>" href="javascript:f_Display(<%#Eval("Id")%>);" class="increase"><b class="ico_increase">加一份</b></a>

                        <%# Eval("OrderNumber") %>
                    </td>
                    <td >
                        <%# Eval("PayStatus") %>
                    </td>
                    <td >
                        <%# Eval("ModifyTime") %>                        
                    </td>
                    <td >
                        <%# Eval("CustomerName") %>                        
                    </td>
                    <td >
                        <%# Eval("Number") %>                        
                    </td>
                    <td >
                        <%# Eval("ProductName") %>                        
                    </td>
                    <td>
                        <%# Eval("ArriveTime").ToString().Substring(0,10) %>                        
                    </td>
                    <td >
                        <%# Eval("LeaveTime").ToString().Substring(0,10) %>                        
                    </td>
                    <td >
                        <%# Eval("Price") %>                        
                    </td>
                    <td >
                        <%# Eval("PayAmount") %> 元                        
                    </td>
                    <td>
                        <%# Eval("RefundAmount") %>元
                    </td>
                    <td>
                        <%# Eval("RealAmount") %>元
                    </td>
                </tr>
                <tr class="rpSubMenu<%#Eval("Id")%>" style="display: none;">
                    <td colspan="9"></td>
                    <td colspan="3">
                        <asp:Repeater runat="server" ID="rp">
                            <HeaderTemplate>
                                <table cellspacing="0" rules="all" border="1" id="CommodityList" style="border-collapse: collapse; width: 90%; margin: 1px 0px 5px 33px;" class="Repeater">
                                    <tr>
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
                                        <%# Eval("IdentifyingCode") %>
                                    </td>
                                    <td>
                                        <%# Eval("Status") %>
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
                <%#rptList.Items.Count == 0 ? "<tr><td align=\"center\" colspan=\"13\">暂无记录</td></tr>" : ""%>
                 </tbody>
                </table>
            </FooterTemplate>
        </asp:Repeater>

        <!--/列表-->

        <!--内容底部-->
        <div class="line20"></div>
        <div class="pagelist">
            <div style="float: right">
                <span>总计实际交易金额</span><span style="color: #ff0000"><%=totalAmount %></span><span>元</span>
            </div>
            <div id="PageContent" runat="server" class="default"></div>
        </div>
    </form>

    <script type="text/javascript">
        function f_NotDisplay(e) {
            $(".rpSubMenu" + e).attr("style", "display: none;");
            $("#notdisplay" + e).attr("style", "display: none;");
            $("#display" + e).attr("style", "");
        }

        function f_Display(e) {
            $(".rpSubMenu" + e).attr("style", "");
            $("#notdisplay" + e).attr("style", "");
            $("#display" + e).attr("style", "display: none;");
        }
    </script>
</body>
</html>

