<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="orderDetail.aspx.cs" Inherits="WeiXinPF.Web.admin.wqiche.orderDetail"ValidateRequest="false" %>

<%@ Import Namespace="WeiXinPF.Common" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>编辑</title>
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
    <script type="text/javascript">
        $(function () {
            //初始化表单验证
            $("#form1").initValidform();

            //初始化编辑器
            var editor = KindEditor.create('.editor', {
                width: '75%',
                height: '100px',
                resizeType: 1,
                uploadJson: '../../tools/upload_ajax.ashx?action=EditorFile&IsWater=1',
                fileManagerJson: '../../tools/upload_ajax.ashx?action=ManagerFile',
                allowFileManager: true
            });
            var editorMini = KindEditor.create('.editor-mini', {
                width: '75%',
                height: '100px',
                resizeType: 1,
                uploadJson: '../../tools/upload_ajax.ashx?action=EditorFile&IsWater=1',
                items: [
                    'fontname', 'fontsize', '|', 'forecolor', 'hilitecolor', 'bold', 'italic', 'underline',
                    'removeformat', '|', 'justifyleft', 'justifycenter', 'justifyright', 'insertorderedlist',
                    'insertunorderedlist', '|', 'emoticons', 'image', 'link']
            });
        });
    </script>
</head>

<body class="mainbody">
    <form id="form1" runat="server">
        <!--导航栏-->
        <div class="location">
            <a href="yuyueMgr.aspx" class="home"><i></i><span>预约设置</span></a>
            <i class="arrow"></i>
            <a href="orderMgr.aspx?type=<%=type %>"><i></i><span>预约订单》<%=type==1?"预约保养":"预约试驾" %></span></a>
            <i class="arrow"></i>
            <span>订单详细</span>
        </div>
        <div class="line10"></div>
        <!--/导航栏-->

        <!--内容-->
        <div class="content-tab-wrap">
            <div id="floatHead" class="content-tab">
                <div class="content-tab-ul-wrap">
                    <ul>
                        <li><a href="javascript:;" onclick="tabs(this);" class="selected">基本内容</a></li>
                    </ul>
                </div>
            </div>
        </div>

        <div class="tab-content">
            <dl>
                <dt>预约项目：</dt>
                <dd>
                    <asp:Label ID="lblObj" runat="server" Text="Label"></asp:Label>
                </dd>
            </dl>
            <asp:Panel ID="pelShow" runat="server">
                <dl>
                    <dt>车牌：</dt>
                    <dd>
                        <asp:Label ID="lblCarnum" runat="server" Text="Label"></asp:Label>
                    </dd>
                </dl>
                <dl>
                    <dt>公里数：</dt>
                    <dd>
                        <asp:Label ID="lblKm" runat="server" Text="Label"></asp:Label>
                    </dd>
                </dl>
            </asp:Panel>
            <dl>
                <dt>预约人：</dt>
                <dd>
                    <asp:Label ID="lblYYPerson" runat="server" Text="Label"></asp:Label>
                </dd>
            </dl>
            <dl>
                <dt>电话：</dt>
                <dd>
                    <asp:Label ID="lblTelephone" runat="server" Text="Label"></asp:Label>
                </dd>
            </dl>
            <dl>
                <dt>备注：</dt>
                <dd>
                    <asp:Label ID="lblremark" runat="server" Text="Label"></asp:Label>
                </dd>
            </dl>
            <dl>
                <dt>下单时间：</dt>
                <dd>
                    <asp:Label ID="lblXdtime" runat="server" Text="Label"></asp:Label>
                </dd>
            </dl>
            <dl>
                <dt>预约时间：</dt>
                <dd>
                    <asp:Label ID="lblYytime" runat="server" Text="Label"></asp:Label>
                </dd>
            </dl>
            <dl>
                <dt>预约时间：</dt>
                <dd>
                    <div class="rule-single-select">
                        <asp:DropDownList ID="ddlStatus" runat="server">
                            <asp:ListItem>待回复</asp:ListItem>
                            <asp:ListItem>确认</asp:ListItem>
                            <asp:ListItem>拒绝</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </dd>
            </dl>
            <dl>
                <dt>客服备注：</dt>
                <dd>
                    <textarea id="txtfSummary" class="editor" style="visibility: hidden;" runat="server"></textarea>
                </dd>
            </dl>
        </div>
        <!--/内容-->

        <!--工具栏-->
        <div class="page-footer">
            <div class="btn-list">
                <asp:Button ID="btnSubmit" runat="server" Text="提交保存" CssClass="btn" OnClick="btnSubmit_Click" />
                <a href="javascript:history.back(-1);"><span class="btn yellow">返回上一页</span></a>
            </div>
            <div class="clear"></div>
        </div>
        <!--/工具栏-->
    </form>
</body>
</html>
