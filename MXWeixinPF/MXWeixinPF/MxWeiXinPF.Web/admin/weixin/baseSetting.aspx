<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="baseSetting.aspx.cs" Inherits="MxWeiXinPF.Web.admin.weixin.baseSetting" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>编辑微信公众号</title>
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
            $(".upload-attach").InitSWFUpload({ filesize: "51200", sendurl: "../../tools/upload_ajax.ashx", flashurl: "../../scripts/swfupload/swfupload.swf", filetypes: "*.gif;*.jpg;*.png;*.bmp;*.rar;*.zip;*.doc;*.xls;*.txt;*.p12" });


            $(".attach-btn").click(function () {
                showAttachDialog();
            });


        });

        //创建附件窗口
        function showAttachDialog(obj) {
            var objNum = arguments.length;
            var attachDialog = $.dialog({
                id: 'attachDialogId',
                lock: true,
                max: false,
                min: false,
                title: "上传附件",
                content: 'url:dialog/dialog_attach.aspx',
                width: 500,
                height: 180
            });
            //如果是修改状态，将对象传进去
            if (objNum == 1) {
                attachDialog.data = obj;
            }
        }


    </script>
</head>

<body class="mainbody">
    <form id="form1" runat="server">
        <!--导航栏-->
        <div class="location">
            <a href="myweixinlist.aspx" class="back" navid="list_weixin" target="mainframe"><i></i><span>返回列表页</span></a>
            <i class="arrow"></i>
            <span>微信公众号基本信息</span>
        </div>
        <div class="line10"></div>
        <!--/导航栏-->

        <!--内容-->
        <div class="content-tab-wrap">
            <div id="floatHead" class="content-tab">
                <div class="content-tab-ul-wrap">
                    <ul>
                        <li><a href="javascript:;" onclick="tabs(this);" class="selected">微支付相关参数</a></li>
                        <li><a href="javascript:;" onclick="tabs(this);" >基础参数设置</a></li>
                    </ul>
                </div>
            </div>
        </div>

        <div class="tab-content">
             <dl>
                <dt>微信号</dt>
                <dd>
                    <asp:Label ID="lblWeixinName" runat="server" Text=""></asp:Label>
                </dd>
            </dl>
             <dl>
                <dt>appid</dt>
                <dd>
                    <asp:Label ID="lblAppid" runat="server" Text=""></asp:Label>
                </dd>
            </dl>

            <dl>
                <dt>商户号mch_id</dt>
                <dd>
                    <asp:HiddenField ID="hidId" runat="server" Value="0" />
                    <asp:TextBox ID="txtmch_id" runat="server" CssClass="input normal " datatype="*1-32" sucmsg=" " nullmsg="请填写商户号mch_id"></asp:TextBox>
                    <span class="Validform_checktip">*微信支付商户号（接口文档中的商户号MCHID）</span></dd>
            </dl>
            <dl>
                <dt>商户支付密钥Key</dt>
                <dd>
                    <asp:TextBox ID="txtpaykey" runat="server" CssClass="input normal " datatype="*1-100" sucmsg=" " nullmsg="请填写商户支付密钥Key"></asp:TextBox>
                    <span class="Validform_checktip">*登录微信商户后台，进入栏目【账户设置】【密码安全】【API 安全】【API 密钥】</span></dd>
            </dl>
            <dl id="div_attach_container" runat="server">
                <dt>上传附件</dt>
                <dd>
                    <asp:TextBox ID="txtcertInfoPath" runat="server" CssClass="input normal upload-path" />
                    <div class="upload-box upload-attach"></div>
                </dd>
            </dl>
            <dl>
                <dt>证书密码</dt>
                <dd>

                    <asp:TextBox ID="txtcerInfoPwd" runat="server" CssClass="input normal " datatype="*1-50" sucmsg=" " nullmsg="请填写证书密码"></asp:TextBox>
                    <span class="Validform_checktip">*默认值为商户号</span></dd>
            </dl>
        </div>

         <div class="tab-content">
             <dl>
                <dt>登录接口配置</dt>
                
                   <dd>
                         <asp:HiddenField ID="hidOpenOauthId" runat="server" Value="0" />
                    <div class="rule-multi-radio">
                        <asp:RadioButtonList ID="radOpenOAuth" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
                            <asp:ListItem Value="0" Selected="True">关闭</asp:ListItem>
                            <asp:ListItem Value="1">开启</asp:ListItem>
                           
                        </asp:RadioButtonList>
                    </div><br />
                    <span class="Validform_checktip">*微信官方登录接口(OAuth网页授权)确认使用（<span style="color:red;">仅认证服务号可用</span>）；
                        <br />并且需要在公众号官方后台将本站点的域名填写到网页授权里：开发者中心---网页服务---网页账号，授权回调页面域名</span>
                </dd>

                 
            </dl>
             </div>
        <!--/内容-->

        <!--工具栏-->
        <div class="page-footer">
            <div class="btn-list">
                <asp:Button ID="btnSubmit" runat="server" Text="提交保存" CssClass="btn" OnClick="btnSubmit_Click" />
                <a href="myweixinlist.aspx"><span class="btn yellow">返回上一页</span></a>

            </div>
            <div class="clear"></div>
        </div>
        <!--/工具栏-->
    </form>
</body>
</html>
