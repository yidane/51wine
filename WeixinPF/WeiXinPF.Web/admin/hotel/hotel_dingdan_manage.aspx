<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="hotel_dingdan_manage.aspx.cs" Inherits="WeiXinPF.Web.admin.hotel.hotel_dingdan_manage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>在线预定管理</title>
    <script type="text/javascript" src="../../scripts/jquery/jquery-1.10.2.min.js"></script>
    <script type="text/javascript" src="../../scripts/lhgdialog/lhgdialog.js?skin=idialog"></script>
    <script type="text/javascript" src="../../scripts/datepicker/WdatePicker.js"></script>
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

        .status {
            border-radius: 3px;
            color: #FFF;
            font-size: 12px;
            line-height: 12px;
            padding: 2px 4px;
            text-shadow: 0 0 #FFFFFF;
            font-style: normal;
        }

        em.ok {
            background-color: #1CC200;
            color: #FFF !important;
        }

        em.error {
            background-color: #FF6600;
            color: #FFF !important;
        }

        em.no {
            background-color: #BBBBBB;
            color: #FFF !important;
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

        .single-select .boxwrap {
            width: 179px;
        }
    </style>
</head>
<body class="mainbody">
    <form id="form1" runat="server">
        <div class="location">
            <% if (IsWeiXinCode())
                {%>
            <a href="hotel_list.aspx" class="home"><i></i><span>酒店商户或门店管理</span></a>
            <i class="arrow"></i>
            <%}%>
            <span>订单管理 </span>
        </div>

        <table border="0" cellspacing="0" cellpadding="0" class="border-table" width="98%">
            <tr>
                <td align="right" style="width: 15%">订单号：</td>
                <td align="left" style="width: 35%">
                    <asp:TextBox ID="txtOrderNumber" Width="170px" runat="server" placeholder="订单号" CssClass="input normal" datatype="*1-300" sucmsg=" " Text="" />

                </td>

               
                
                 <td align="right" style="width: 15%">交易时间
                </td>
                <td align="left" style="width: 35%">
                    <div class="input-date">
                        <asp:TextBox ID="txtbeginDate" runat="server" placeholder="开始时间" CssClass="input date" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd '})" datatype="*1-50" errormsg="请选择正确的日期" sucmsg=" " nullmsg=" " />
                        <i>开始时间</i>
                    </div>
                    到
                  
                    <div class="input-date">
                        <asp:TextBox ID="txtendDate" runat="server"  placeholder="结束时间" CssClass="input date" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd '})" datatype="*1-50" errormsg="请选择正确的日期" sucmsg=" " nullmsg=" " />
                        <i>结束时间</i>
                    </div>
                </td>
            </tr>




            <tr>
                <td align="right" style="width: 15%">订单状态：</td>
                <td align="left" style="width: 35%">
                    <div class="rule-single-select">
                        <asp:DropDownList runat="server" ID="ddlOrderStatus" Width="170px">
                            <%--                            <asp:ListItem Value="-1">请选择</asp:ListItem> --%>
                        </asp:DropDownList>
                    </div>
                </td>

                <td align="right" style="width: 15%">交易金额</td>
                <td align="left" style="width: 35%">
                    <asp:TextBox ID="txtPayAmountMin" Width="170px" runat="server" placeholder="最小金额" CssClass="input txt" datatype="n" sucmsg=" " Text="" />
                    至
                        <asp:TextBox ID="txtPayAmountMax" Width="170px" runat="server" placeholder="最大金额" CssClass="input txt" datatype="n" sucmsg=" " Text="" /></td>


            </tr>

        </table>
        <div style="width: 100%; text-align: center;padding: 10px 0;">
            <asp:Button ID="btnSearch" runat="server" Text="查询" CssClass="DingdanButton" OnClick="btnSearch_Click" />
            <asp:Button ID="btnExport" runat="server" Text="导出Excel" CssClass="DingdanButton" OnClick="btnExport_OnClick" />
        </div>




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
        <asp:Repeater ID="rptList" runat="server">
            <HeaderTemplate>
                <table width="100%" border="0" cellspacing="0" cellpadding="0" class="ltable">
                    <thead>
                        <tr>

                            <th>选择</th>
                            <th>序号</th>
                            <th>订单编号</th>
                            <th>订单状态</th>
                            <th>是否退单</th>
                            <th>商户或门店名称</th>
                            <th>预定人</th>
                            <th>交易日期</th>
                            <th>商品名称</th>
                            <th>购买数量</th>
                            <th>商品价格</th>

                            <th>入住/离店时间</th>
                            <th>支付金额</th>


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
                        <%# Eval("orderno") %>
                    </td>
                    <td>
                        <%# Eval("orderNumber") %>
                    </td>
                    <td>
                        <%# Eval("payStatusStr") %>
                    </td>
                    <td>
                        <%# Eval("isRefund") %>
                    </td>
                    <td>
                        <%# Eval("hotelName") %>
                    </td>
                    <td>
                        <%# Eval("oderName") %>
                    </td>
                    <td>
                        <%# string.Format("{0:yyyy/MM/dd}",  Eval("createDate")) %>
                    </td>
                    <td>
                        <%# Eval("roomType") %>
                    </td>
                    <td>
                        <%# Eval("orderNum") %>
                    </td>
                    <td>
                        <%# Eval("price") %>
                    </td>
                    <td>
                        <%# string.Format("{0:yyyy/MM/dd}", Eval("arriveTime")) %>
                        <br />
                        <%# string.Format("{0:yyyy/MM/dd}",  Eval("leaveTime"))  %>
                    </td>



                    <td>
                        <span style="color: red; font-weight: bold;"><%# Eval("totalPrice") %></span>

                    </td>


                    <td>
                        <a href='hotel_dingdan_cz.aspx?id=<%#Eval("id") %>&hotelid=<%=hotelid %>'>操作</a>

                    </td>
                </tr>
            </ItemTemplate>
            <FooterTemplate>
                <%#rptList.Items.Count == 0 ? "<tr><td align=\"center\" colspan=\"14\">暂无记录</td></tr>" : ""%>
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
    <p>
        &nbsp;
    </p>
</body>
</html>
