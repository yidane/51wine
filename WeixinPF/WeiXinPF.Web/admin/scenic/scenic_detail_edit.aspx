<%@ Page Language="C#" AutoEventWireup="true" ValidateRequest="false" CodeBehind="scenic_detail_edit.aspx.cs" Inherits="WeiXinPF.Web.admin.scenic.scenic_detail_edit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <script type="text/javascript" src="../../scripts/jquery/jquery-1.10.2.min.js"></script>
    <script type="text/javascript" src="../../scripts/jquery/Validform_v5.3.2_min.js"></script>
    <script type="text/javascript" src="../../scripts/lhgdialog/lhgdialog.js?skin=idialog"></script>
    <script type="text/javascript" src="../../scripts/swfupload/swfupload.js"></script>
    <script type="text/javascript" src="../../scripts/swfupload/swfupload.queue.js"></script>
    <script type="text/javascript" src="../../scripts/swfupload/swfupload.handlers.js"></script>
    <script type="text/javascript" charset="utf-8" src="../../editor/kindeditor-min.js"></script>
    <script type="text/javascript" charset="utf-8" src="../../editor/lang/zh_CN.js"></script>
    <script type="text/javascript" src="../js/layout.js"></script>
    <link href="../skin/default/style.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        $(function () {
            //初始化表单验证
            $("#form1").initValidform();

            //初始化上传控件
            $(".upload-img").each(function () {
                $(this).InitSWFUpload({ sendurl: "../../tools/upload_ajax.ashx", flashurl: "../../scripts/swfupload/swfupload.swf", filetypes: "*.jpg;*.jpge;*.png;*.gif;*.mp3;" });
            });

            var editor = KindEditor.create('.editor', {
                width: '80%',
                height: '250px',
                resizeType: 1,
                uploadJson: '../../tools/upload_ajax.ashx?action=EditorFile&IsWater=1'
            });
        });

    </script>
</head>
<body class="mainbody">
    <form id="form1" runat="server">
        <!--导航栏-->
        <div class="location">
            <a href="scenic_list.aspx" class="home"><i></i><span>景区导览列表</span></a>
            <i class="arrow"></i>
            <a href="scenic_detail_list.aspx?scenicId=<%=ScenicId %>"><i></i><span>景点列表</span></a>
            <i class="arrow"></i>
            <span>景点详情编辑</span>
        </div>
        <div class="line10"></div>
        <!--/导航栏-->
        <!--内容-->
        <div class="content-tab-wrap">
            <div id="floatHead" class="content-tab">
                <div class="content-tab-ul-wrap">
                    <ul>
                        <li><a href="javascript:;" onclick="tabs(this);" class="selected">景点详情</a></li>
                    </ul>
                </div>
            </div>
        </div>
        <div class="tab-content">
            <dl>
                <dt>名称</dt>
                <dd>
                    <asp:TextBox ID="txtName" runat="server" CssClass="input normal" datatype="*2-100" sucmsg=" " />
                    <span class="Validform_checktip">*名称最多100个字符</span>
                </dd>
            </dl>
            <dl>
                <dt>封面图片</dt>
                <dd>
                    <asp:TextBox ID="txtCover" runat="server" datatype="*2-100" CssClass="input normal upload-path" />
                    <div class="upload-box upload-img"></div>
                    <%--<span class="Validform_checktip"></span>--%>
                </dd>
            </dl>
            <dl>
                <dt>首页背景图片</dt>
                <dd>
                    <asp:TextBox ID="txtBackgroundImage" runat="server" datatype="*2-100" CssClass="input normal upload-path" />
                    <div class="upload-box upload-img"></div>
                    <%--<span class="Validform_checktip"></span>--%>
                </dd>
            </dl>
            <dl>
                <dt>摘要</dt>
                <dd>
                    <asp:TextBox ID="txtDigest" runat="server" CssClass="input normal" />
                </dd>
            </dl>
            <dl>
                <dt>正文</dt>
                <dd>
                    <textarea id="txtContent" class="editor" style="visibility: hidden;" runat="server"></textarea>
                    <%--<asp:TextBox ID="txtContent" runat="server" CssClass="input normal" />--%>
                </dd>
            </dl>
            <dl>
                <dt>音频文件</dt>
                <dd>
                    <asp:TextBox ID="txtAudio" runat="server" datatype="*2-100" CssClass="input normal upload-path" />
                    <div class="upload-box upload-img"></div>
                    <%--<span class="Validform_checktip"></span>--%>
                </dd>
            </dl>
            <dl>
                <dt>是否自动播放音频文件</dt>
                <dd>
                    <div class="rule-multi-radio">
                        <asp:RadioButtonList ID="rblAutoAudio" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
                            <asp:ListItem Value="1" Selected="True">是</asp:ListItem>
                            <asp:ListItem Value="0">否</asp:ListItem>
                        </asp:RadioButtonList>
                    </div>
                </dd>
            </dl>
            <dl>
                <dt>是否循环播放音频文件</dt>
                <dd>
                    <div class="rule-multi-radio">
                        <asp:RadioButtonList ID="rblLoopAudio" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
                            <asp:ListItem Value="1" Selected="True">是</asp:ListItem>
                            <asp:ListItem Value="0">否</asp:ListItem>
                        </asp:RadioButtonList>
                    </div>
                </dd>
            </dl>
        </div>
        <!--/内容-->
        <!--工具栏-->
        <div class="page-footer">
            <div class="btn-list">
                <asp:Button ID="btnSubmit" runat="server" Text="提交保存" CssClass="btn" OnClick="btnSubmit_Click" />
                <a href="scenic_list.aspx"><span class="btn yellow">返回上一页</span></a>
            </div>
            <div class="clear"></div>
        </div>
        <!--/工具栏-->
    </form>
</body>
</html>
