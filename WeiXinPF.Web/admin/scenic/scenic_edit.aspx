<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="scenic_edit.aspx.cs" Inherits="WeiXinPF.Web.admin.scenic.scenic_edit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>景区导览编辑</title>
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
    <script type="text/javascript">
        $(function () {
            //初始化表单验证
            $("#form1").initValidform();

            //初始化上传控件
            $(".upload-img").each(function () {
                $(this).InitSWFUpload({ sendurl: "../../tools/upload_ajax.ashx", flashurl: "../../scripts/swfupload/swfupload.swf", filetypes: "*.jpg;*.jpge;*.png;*.gif;*.mp3;" });
            });
        });

    </script>
</head>
<body>
    <form id="form1" runat="server">
        <!--导航栏-->
        <div class="location">
            <a href="scenic_list.aspx" class="home"><i></i><span>杂志列表</span></a>
            <i class="arrow"></i>
            <span>景区导览编辑</span>
        </div>
        <div class="line10"></div>
        <!--/导航栏-->
        <!--内容-->
        <div class="content-tab-wrap">
            <div id="floatHead" class="content-tab">
                <div class="content-tab-ul-wrap">
                    <ul>
                        <li><a href="javascript:;" onclick="tabs(this);" class="selected">内容</a></li>
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
                <dt>首页背景图片</dt>
                <dd>
                    <asp:TextBox ID="txtFirstBgImg" runat="server" CssClass="input normal upload-path" />
                    <div class="upload-box upload-img"></div>
                    <%--<span class="Validform_checktip"></span>--%>
                </dd>
            </dl>
            <dl>
                <dt>景区标识图片</dt>
                <dd>
                    <asp:TextBox ID="txtIdentifyImg" runat="server" CssClass="input normal upload-path" />
                    <div class="upload-box upload-img"></div>
                    <%--<span class="Validform_checktip"></span>--%>
                </dd>
            </dl>
            <dl>
                <dt>景区标识显示动画</dt>
                <dd>
                    <div class="rule-multi-radio">
                        <asp:RadioButtonList ID="rblDiaplayAction" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
                            <asp:ListItem Value="fadeIn" Selected="True">淡入</asp:ListItem>
                            <asp:ListItem Value="fadeIn">左侧滚入</asp:ListItem>
                            <asp:ListItem Value="fadeIn">右侧滚入</asp:ListItem>
                        </asp:RadioButtonList>
                    </div>
                </dd>
            </dl>
            <dl>
                <dt>第二页背景图片</dt>
                <dd>
                    <asp:TextBox ID="txtSecondBgImg" runat="server" CssClass="input normal upload-path" />
                    <div class="upload-box upload-img"></div>
                    <span class="Validform_checktip"></span>
                </dd>
            </dl>
            <dl>
                <dt>是否自动显示第二页：
                </dt>
                <dd>
                    <div class="rule-multi-radio">
                        <asp:RadioButtonList ID="rblAutoDisplayNextPage" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
                            <asp:ListItem Value="1" Selected="True">是</asp:ListItem>
                            <asp:ListItem Value="0">否</asp:ListItem>
                        </asp:RadioButtonList>
                    </div>
                </dd>
            </dl>
            <dl>
                <dt>延迟显示时间</dt>
                <dd>
                    <asp:TextBox ID="txtDelay" runat="server" datatype="n" CssClass="input normal" />
                    <span class="Validform_checktip">*以秒为单位</span>
                </dd>
            </dl>
            <dl>
                <dt>景区描述</dt>
                <dd>
                    <asp:TextBox runat="server" ID="txtDescription" TextMode="MultiLine" Height="75"></asp:TextBox>
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
