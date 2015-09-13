<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="scenic_picture_list.aspx.cs" Inherits="WeiXinPF.Web.admin.scenic.scenic_picture_list" %>

<%@ Import Namespace="WeiXinPF.Common" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>景点-图片管理</title>
    <script type="text/javascript" src="../../scripts/jquery/jquery-1.10.2.min.js"></script>
    <script type="text/javascript" src="../../scripts/jquery/jquery.lazyload.min.js"></script>
    <script type="text/javascript" src="../../scripts/lhgdialog/lhgdialog.js?skin=idialog"></script>
    <script type="text/javascript" src="../js/layout.js"></script>
    <link href="../skin/default/style.css" rel="stylesheet" type="text/css" />
    <link href="../skin/mystyle.css" rel="stylesheet" type="text/css" />
    <link href="../../css/pagination.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript">
        $(function () {
            imgLayout();
            $(window).resize(function () {
                imgLayout();
            });
            //图片延迟加载
            $(".pic img").lazyload({ load: AutoResizeImage, effect: "fadeIn" });
            //点击图片链接
            $(".pic img").click(function () {
                $.dialog({ lock: true, title: "查看大图", content: "<img src=\"" + $(this).attr("src") + "\" />", padding: 0 });
            });
        });
        //排列图文列表
        function imgLayout() {
            var imgWidth = $(".imglist").width();
            var lineCount = Math.floor(imgWidth / 222);
            var lineNum = imgWidth % 222 / (lineCount - 1);
            $(".imglist ul").width(imgWidth + Math.ceil(lineNum));
            $(".imglist ul li").css("margin-right", parseFloat(lineNum));
        }
        //等比例缩放图片大小
        function AutoResizeImage(e, s) {
            var img = new Image();
            img.src = $(this).attr("src");
            var w = img.width;
            var h = img.height;
            var wRatio = w / h;
            if ((220 / wRatio) >= 165) {
                $(this).width(220); $(this).height(220 / wRatio);
            } else {
                $(this).width(165 * wRatio); $(this).height(165);
            }
        }
    </script>


</head>

<body class="mainbody">
    <form id="form1" runat="server">
        <!--导航栏-->
        <div class="location">
            <a href="scenic_list.aspx" class="home"><i></i><span>景区导览列表</span></a>
            <i class="arrow"></i>
            <a href="scenic_detail_list.aspx" class="home"><i></i><span>景点详情列表</span></a>
            <i class="arrow"></i>
            <span>图片管理</span>
        </div>
        <!--工具栏-->
        <div class="toolbar-wrap">
            <div id="floatHead" class="toolbar">
                <div class="l-list">
                    <ul class="icon-list">
                        <li><a class="icon-btn add" href="scenic_picture_edit.aspx?action=<%=MXEnums.ActionEnum.Add %>&detailId=<%=DetailId %>" id="itemAddButton"><i></i><span>新增图片</span></a></li>

                        <li><a class="all" href="javascript:;" onclick="checkAll(this);"><i></i><span>全选</span></a></li>
                        <li>
                            <asp:LinkButton ID="btnDelete" runat="server" CssClass="del" OnClientClick="return ExePostBack('btnDelete');" OnClick="btnDelete_Click"><i></i><span>删除</span></asp:LinkButton></li>
                    </ul>
                </div>
            </div>
        </div>
        <!--/工具栏-->

        <!--列表-->

        <asp:Repeater ID="rptList2" runat="server">
            <HeaderTemplate>
                <div class="imglist">
                    <ul>
            </HeaderTemplate>
            <ItemTemplate>
                <li>
                    <div class="details<%#Eval("PicPath").ToString() != "" ? "" : " nopic"%>">
                        <div class="check">
                            <asp:CheckBox ID="chkId" CssClass="checkall" runat="server" /><asp:HiddenField ID="hidId" Value='<%#Eval("Id")%>' runat="server" />
                        </div>
                        <%#Eval("PicPath").ToString() != "" ? "<div class=\"pic\"><img src=\"../skin/default/loadimg.gif\" data-original=\"" + Eval("PicPath") + "\" /></div><i class=\"absbg\"></i>" : ""%>
                        <h1><span><%#Eval("Name")%></span></h1>
                        <div class="remark" style="height: 20px;">
                            <%#Eval("Content").ToString() == "" ? "暂无描述说明..." : Eval("Content").ToString()%>
                        </div>
                        <div class="tools">
                            <asp:Label ID="Label1" runat="server" Text='<%#Eval("OrderNo")%>' Style="float: right;"></asp:Label>
                            <span style="float: right; line-height: 26px; font-size: 12px;">排序：</span>
                        </div>
                        <div class="foot">
                            <p class="time"><%#string.Format("{0:yyyy-MM-dd HH:mm:ss}", Eval("CreateDate"))%></p>
                            <a href="scenic_picture_edit.aspx?action=<%#MXEnums.ActionEnum.Edit %>&detailId=<%#this.DetailId %>&Id=<%#Eval("Id")%>" title="编辑" class="edit">编辑</a>
                        </div>
                    </div>
                </li>
            </ItemTemplate>
            <FooterTemplate>
                <%#rptList2.Items.Count == 0 ? "<div align=\"center\" style=\"font-size:12px;line-height:30px;color:#666;\">暂无记录</div>" : ""%>
  </ul>
</div>
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
