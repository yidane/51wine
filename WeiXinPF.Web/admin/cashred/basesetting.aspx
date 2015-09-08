<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="basesetting.aspx.cs" Inherits="WeiXinPF.Web.admin.cashred.basesetting" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>红包基本设置</title>
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

            <i class="arrow"></i>
            <span>红包基本设置</span>
        </div>
        <div class="line10"></div>
        <!--/导航栏-->

        <div class="mytips" id="mytips" runat="server" style="color: red; display:none;">
            <asp:Literal ID="litWarning" runat="server"></asp:Literal>
        </div>

        <!--内容-->
        <div class="content-tab-wrap">
            <div id="floatHead" class="content-tab">
                <div class="content-tab-ul-wrap">
                    <ul>
                        <li><a href="javascript:;" onclick="tabs(this);" class="selected">红包基本设置</a></li>
                        <li><a href="javascript:;" onclick="tabs(this);">使用说明</a></li>
                          <li><a href="javascript:;" onclick="tabs(this);">网页红包使用说明</a></li>
                    </ul>
                </div>
            </div>
        </div>

        <div class="tab-content">

            <dl>
                <dt>账号余额</dt>
                <dd>
                    <asp:HiddenField ID="hidId" runat="server" Value="0" />
                    <asp:TextBox ID="txtaccountBalance" runat="server" CssClass="input normal " datatype="/^(([1-9]{1}\d*)|([0]{1}))(\.(\d){1,2})?$/" sucmsg=" " nullmsg="请输入账号余额" Text="0"></asp:TextBox>（元）
                    <span class="Validform_checktip">*商户平台里的商户余额（小数点后保留1-2位）</span></dd>
            </dl>
            <dl>
                <dt>累计发放金额</dt>
                <dd>
                    <asp:Label ID="lbltotalLQMoney" runat="server" Text="0"></asp:Label>(元)
                    <span class="Validform_checktip">*累计发放的红包总金额</span></dd>
            </dl>

            <dl>
                <dt>备注</dt>
                <dd>

                    <textarea name="txtremark" rows="2" cols="20" id="txtremark" class="input" runat="server" datatype="*0-500" sucmsg=" " nullmsg=" "></textarea>
                    <span class="Validform_checktip"></span>
                </dd>
            </dl>
        </div>
        <div class="tab-content" style="display:none;">
            <div style="padding-left: 20px; padding-bottom: 20px; line-height: 24px; font-size: 16px;">
                <h3>使用说明及规则，请仔细阅读</h3>
                <ul style="list-style-type: circle; padding-left: 20px;">

                    <li style="list-style-type: circle;">（1）必须是认证过的服务号，开通了微信支付功能；在商家后台充足够多的钱来发红包。</li>
                    <li style="list-style-type: circle;">（2）发送频率规则<br />
◆ 每分钟发送红包数量不得超过1800个；<br />
◆ 北京时间0：00-8：00不触发红包赠送；（如果以上规则不满足您的需求，请发邮件至wxhongbao@tencent.com获取升级指引）</li>
                    <li style="list-style-type: circle;">（3）红包规则<br />
◆ 单个红包金额介于[1.00元，200.00元]之间；<br /> 
◆ 同一个红包只能发送给一个用户；（如果以上规则不满足您的需求，请发邮件至wxhongbao@tencent.com获取升级指引）</li>
                    <li style="list-style-type: circle;">（4）填写正确的AppId,Appsecret,以及微信支付的配置参数</li>
                    <li style="list-style-type: circle;">（5）分享接口请注意不要有诱导分享等违规行为，对于诱导分享行为将永久回收公众号接口权限，详细规则请查看：<a href="http://kf.qq.com/faq/131117ne2MV7141117JzI32q.html" target="_blank">朋友圈管理常见问题 。</a></li>
                </ul>
            </div>
        </div>

        <div class="tab-content" style="display:none;">
            <div style="padding-left: 20px; padding-bottom: 20px; line-height: 24px; font-size: 16px;">
                <h3>网页红包使用说明及规则，请仔细阅读</h3>
                <ul style="list-style-type: circle; padding-left: 20px;">

                    <li style="list-style-type: circle;">（1）网页授权配置：微信官方后台：开启网页授权(开发者中心---网页授权获取用户基本信息 --- 修改)<br />
                        <img src="img/oauth2peizhi.png" style="max-width:400px;" />
                    </li>
                  
                    <li style="list-style-type: circle;">（2）js分享接口配置：若分享没有效果，请登录官方后台，“公众号设置”---“功能设置”---“JS接口安全域名”里，将本站的域名填写好
                       <br />  <img src="img/jspeizhi.png" style="max-width:400px;" />
                    </li>
                </ul>
            </div>
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
