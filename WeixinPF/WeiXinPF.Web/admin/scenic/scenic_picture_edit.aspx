<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="scenic_picture_edit.aspx.cs" Inherits="WeiXinPF.Web.admin.scenic.scenic_picture_edit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>图片信息</title>
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
        });
    </script>
</head>

<body class="mainbody">
    <form id="form1" runat="server">
        <!--导航栏-->
        <div class="location">
            <a href="scenic_detail_list.aspx" class="home"><i></i><span>景点列表</span></a>
            <i class="arrow"></i>
            <a href="scenic_picture_list.aspx?detailId=<%=DetailId %>"><span>图片列表</span></a>
            <i class="arrow"></i>
            <span>图片编辑</span>
        </div>
        <div class="line10"></div>
        <!--/导航栏-->

        <!--内容-->
        <div class="content-tab-wrap">
            <div id="floatHead" class="content-tab">
                <div class="content-tab-ul-wrap">
                    <ul>
                        <li><a href="javascript:;" onclick="tabs(this);" class="selected">图片编辑</a></li>
                    </ul>
                </div>
            </div>
        </div>

        <div class="tab-content">
            <dl>
                <dt>图片名称</dt>
                <dd>
                    <asp:HiddenField ID="hidid" runat="server" Value="0" />
                    <asp:TextBox ID="txtName" runat="server" CssClass="input normal" datatype="*1-20" sucmsg=" " Text="" />
                    <span class="Validform_checktip">*尽量简单，不要超过20字.</span>
                </dd>
            </dl>
            <dl>
                <dt>图片地址</dt>
                <dd>
                    <asp:Image ID="imgfacePicPic" runat="server" ImageUrl="/images/noneimg.jpg" Style="max-height: 80px;" />
                    <asp:TextBox ID="txtImgUrl" runat="server" CssClass="input normal upload-path" />
                    <div class="upload-box upload-img"></div>
                    <span class="Validform_checktip">（尺寸：宽720像素，高400像素） 小于200k;</span>
                </dd>
            </dl>

            <dl>
                <dt>图片说明</dt>
                <dd>
                    <textarea name="txtpContent" rows="2" cols="20" id="txtContent" class="input" runat="server" datatype="*1-300" sucmsg=" " nullmsg=" "></textarea>

                    <span class="Validform_checktip">*请简单描述相册内容，尽量控制在150文字以内。</span>
                </dd>
            </dl>
            <dl>
                <dt>排序</dt>
                <dd>
                    <asp:TextBox ID="txtOrderNo" runat="server" CssClass="input small" datatype="n" sucmsg=" ">99</asp:TextBox>
                    <span class="Validform_checktip">*数字，越小越向前</span>
                </dd>
            </dl>
        </div>



        <!--/内容-->

        <!--工具栏-->
        <div class="page-footer">
            <div class="btn-list">
                <asp:Button ID="btnSubmit" runat="server" Text="提交保存" CssClass="btn" OnClick="btnSubmit_Click" />
                <a href="scenic_piture_list.aspx?detailId=<%=DetailId %>"><span class="btn yellow">返回上一页</span></a>

            </div>
            <div class="clear"></div>
        </div>
        <!--/工具栏-->
    </form>
</body>
</html>

