<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="chexing_edit.aspx.cs" Inherits="WeiXinPF.Web.admin.wqiche.chexing_edit" ValidateRequest="false"%>

<%@ Import Namespace="WeiXinPF.Common" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>编辑车型</title>
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

            //初始化上传控件
            $(".upload-img").each(function () {
                $(this).InitSWFUpload({ sendurl: "../../tools/upload_ajax.ashx", flashurl: "../../scripts/swfupload/swfupload.swf" });
            });
            $(".upload-album").each(function () {
                $(this).InitSWFUpload({ btntext: "批量上传", btnwidth: 66, single: false, water: true, thumbnail: true, filesize: "2048", sendurl: "../../tools/upload_ajax.ashx", flashurl: "../../scripts/swfupload/swfupload.swf", filetypes: "*.jpg;*.jpge;*.png;*.gif;" });
            });
            $(".attach-btn").click(function () {
                showAttachDialog();
            });

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
            <a href="chexingMgr.aspx" class="home"><i></i><span>车型管理</span></a>
            <i class="arrow"></i>
            <span>编辑车型</span>
        </div>
        <div class="line10"></div>
        <!--/导航栏-->

        <!--内容-->
        <div class="content-tab-wrap">
            <div id="floatHead" class="content-tab">
                <div class="content-tab-ul-wrap">
                    <ul>
                        <li><a href="javascript:;" onclick="tabs(this);" class="selected">基本内容</a></li>
                        <li><a href="javascript:;" onclick="tabs(this);">其他</a></li>
                    </ul>
                </div>
            </div>
        </div>

        <div class="tab-content">
            <dl>
                <dt>车型名称：</dt>
                <dd>
                    <asp:TextBox ID="txtName" runat="server" CssClass="input normal" datatype="*1-500" sucmsg=" " Text="" />
                    <span class="Validform_checktip">*</span>
                </dd>
            </dl>
            <dl>
                <dt>品牌/车系：</dt>
                <dd>
                    <div class="rule-single-select">
                        <asp:DropDownList ID="ddlPinpai" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlPinpai_SelectedIndexChanged"></asp:DropDownList>
                    </div>
                    <div class="rule-single-select">
                        <asp:DropDownList ID="ddlChexi" runat="server" datatype="*"></asp:DropDownList>
                    </div>
                    <span class="Validform_checktip">选择品牌，车系会自动出来</span>
                </dd>
            </dl>

            <dl>
                <dt>显示顺序：</dt>
                <dd>
                    <asp:TextBox ID="txtSort_id" runat="server" CssClass="input small" datatype="n" sucmsg=" " Text="99" />
                    <span class="Validform_checktip"></span>
                </dd>
            </dl>
            <dl>
                <dt>指导价：</dt>
                <dd>
                    <asp:TextBox ID="txtZdprice" runat="server" CssClass="input normal" datatype="*1-500" sucmsg=" " Text="" />
                    <span class="Validform_checktip">*(万元) 如: 23.99-25.99</span>
                </dd>
            </dl>
            <dl>
                <dt>经销商报价：</dt>
                <dd>
                    <asp:TextBox ID="txtJxsPrice" runat="server" CssClass="input normal" datatype="*1-500" sucmsg=" " Text="" />
                    <span class="Validform_checktip">*(万元) 如:19.24-35.59</span>
                </dd>
            </dl>
            <dl>
                <dt>图片：</dt>
                <dd>
                    <asp:Image ID="imgPic" runat="server" ImageUrl="~/images/noneimg.jpg" Style="max-height: 80px;" />
                    <asp:TextBox ID="txtPic" runat="server" CssClass="input normal upload-path" />
                    <div class="upload-box upload-img"></div>
                    <span class="Validform_checktip">（尺寸：宽720像素，高400像素） 小于200k;</span>
                </dd>
            </dl>
        </div>

        <div class="tab-content" style="display: none">
            <dl>
                <dt>挡位个数：</dt>
                <dd>
                    <asp:TextBox ID="txtDwnum" runat="server" CssClass="input normal" datatype="*0-500" sucmsg=" " Text="" />
                    <span class="Validform_checktip">*</span>
                </dd>
            </dl>
            <dl>
                <dt>排量：</dt>
                <dd>
                    <asp:TextBox ID="txtPailiang" runat="server" CssClass="input normal" datatype="*0-500" sucmsg=" " Text="" />
                    <span class="Validform_checktip">请选择:</span>
                    <div class="rule-single-select">
                        <asp:DropDownList ID="ddlPltype" runat="server">
                            <asp:ListItem>T</asp:ListItem>
                            <asp:ListItem>L</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </dd>
            </dl>
            <dl>
                <dt>变速箱：</dt>
                <dd>
                    <div class="rule-single-select">
                        <asp:DropDownList ID="ddlBsx" runat="server">
                            <asp:ListItem>自动变速箱（AT）</asp:ListItem>
                            <asp:ListItem>手动变速箱（MT）</asp:ListItem>
                            <asp:ListItem>手自一体</asp:ListItem>
                            <asp:ListItem>无级变速箱（CVT）</asp:ListItem>
                            <asp:ListItem>无级变速（VDT）</asp:ListItem>
                            <asp:ListItem>双离合变速箱（DCT）</asp:ListItem>
                            <asp:ListItem>序列变速箱（AMT）</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <span class="Validform_checktip"></span>
                </dd>
            </dl>
            <dl>
                <dt>年款：</dt>
                <dd>
                    <div class="rule-single-select">
                        <asp:DropDownList ID="ddlNiankuan" runat="server">
                            <asp:ListItem>2025</asp:ListItem>
                            <asp:ListItem>2024</asp:ListItem>
                            <asp:ListItem>2023</asp:ListItem>
                            <asp:ListItem>2022</asp:ListItem>
                            <asp:ListItem>2021</asp:ListItem>
                            <asp:ListItem>2020</asp:ListItem>
                            <asp:ListItem>2019</asp:ListItem>
                            <asp:ListItem>2018</asp:ListItem>
                            <asp:ListItem>2017</asp:ListItem>
                            <asp:ListItem>2016</asp:ListItem>
                            <asp:ListItem>2015</asp:ListItem>
                            <asp:ListItem>2014</asp:ListItem>
                            <asp:ListItem>2013</asp:ListItem>
                            <asp:ListItem>2012</asp:ListItem>
                            <asp:ListItem>2011</asp:ListItem>
                            <asp:ListItem>2010</asp:ListItem>
                            <asp:ListItem>2009</asp:ListItem>
                            <asp:ListItem>2008</asp:ListItem>
                            <asp:ListItem>2007</asp:ListItem>
                            <asp:ListItem>2006</asp:ListItem>
                            <asp:ListItem>2005</asp:ListItem>
                            <asp:ListItem>2004</asp:ListItem>
                            <asp:ListItem>2003</asp:ListItem>
                            <asp:ListItem>2002</asp:ListItem>
                            <asp:ListItem>2001</asp:ListItem>
                            <asp:ListItem>2000</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <span class="Validform_checktip"></span>
                </dd>
            </dl>
            <dl>
                <dt>全景相册名称：</dt>
                <dd>
                    <div class="rule-single-select">
                        <asp:DropDownList ID="ddlQjxc" runat="server"></asp:DropDownList>
                    </div>
                    <span class="Validform_checktip">如果没有留空或者到 360°全景添加</span>
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
