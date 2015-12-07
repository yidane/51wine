<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="hfszMgr.aspx.cs" Inherits="WeiXinPF.Web.admin.wqiche.hfszMgr" %>

<%@ Import Namespace="WeiXinPF.Common" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>汽车回复配置</title>
    <script type="text/javascript" src="../../scripts/jquery/jquery-1.10.2.min.js"></script>
    <script type="text/javascript" src="../../scripts/jquery/Validform_v5.3.2_min.js"></script>
    <script type="text/javascript" src="../../scripts/lhgdialog/lhgdialog.js?skin=idialog"></script>
    <script type="text/javascript" src="../../scripts/datepicker/WdatePicker.js"></script>
    <script type="text/javascript" src="../../scripts/swfupload/swfupload.js"></script>
    <script type="text/javascript" src="../../scripts/swfupload/swfupload.queue.js"></script>
    <script type="text/javascript" src="../../scripts/swfupload/swfupload.handlers.js"></script>
    <script type="text/javascript" charset="utf-8" src="../../editor/kindeditor-min.js"></script>
    <script type="text/javascript" charset="utf-8" src="../../editor/lang/zh_CN.js"></script>
    <script type="text/javascript" src="../js/layout.js"></script>
    <link href="../skin/default/style.css" rel="stylesheet" type="text/css" />
    <link href="../skin/mystyle.css" rel="stylesheet" type="text/css" /> 
</head>

<body class="mainbody">
    <form id="form1" runat="server">
        <!--导航栏-->
        <div class="location">
            <a href="javascript:void(0);" class="home"><i></i><span>汽车回复配置</span></a>
        </div>
        <div class="line10"></div>
        <!--/导航栏-->

        <!--内容-->
        <div class="content-tab-wrap">
            <div class="mytips">请将下面的链接地址复制微网站管理-分类管理</div>
            <div id="floatHead" class="content-tab">
                <div class="content-tab-ul-wrap">
                    <ul>
                        <li><a href="javascript:;" onclick="tabs(this);" class="selected">基本内容</a></li>
                    </ul>
                </div>
            </div>
        </div> 
        <div class="tab-content">
            <dl></dl>
            <dl>
                <dt>经销车型</dt>
                <dd>
                    <asp:Label ID="lblJxcx" runat="server" Text=""></asp:Label>
                </dd>
            </dl>
            <dl>
                <dt>销售顾问:</dt>
                <dd>
                    <asp:Label ID="lblXsgw" runat="server" Text=""></asp:Label>
                </dd>
            </dl>
            <dl>
                <dt>在线预约:</dt>
                <dd>
                    <asp:Label ID="lblZxyy" runat="server" Text=""></asp:Label>
                </dd>
            </dl>
            <dl>
                <dt>车主关怀：</dt>
                <dd>
                    <asp:Label ID="lblCzgh" runat="server" Text=""></asp:Label>
                </dd>
            </dl>
            <dl>
                <dt>实用工具:</dt>
                <dd>
                    <asp:Label ID="lblSygj" runat="server" Text=""></asp:Label>
                </dd>
            </dl>
            <dl>
                <dt>车型欣赏:</dt>
                <dd>
                    <asp:Label ID="lblCxxs" runat="server" Text=""></asp:Label>
                </dd>
            </dl>
            <dl>
                <dt>全景看车:</dt>
                <dd>
                    <asp:Label ID="lblQjkc" runat="server" Text=""></asp:Label>
                </dd>
            </dl>
        </div>
        <!--/内容-->

        <!--工具栏-->
        <div class="page-footer">
            <div class="btn-list">
                <asp:Button ID="btnSubmit" runat="server" Text="提交保存" CssClass="btn" />
                <a href="javascript:history.back(-1);"><span class="btn yellow">返回上一页</span></a>
            </div>
            <div class="clear"></div>
        </div>
        <!--/工具栏-->
    </form>
</body>
</html>
