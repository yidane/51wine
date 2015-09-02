<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="edit_action.aspx.cs" Inherits="MxWeiXinPF.Web.admin.cashred.edit_action" %>


<%@ Import Namespace="MxWeiXinPF.Common" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>编辑现金红包活动</title>
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

            $("#ddlhbType").change(function () {

                var v = $(this).val();

                controlShow(v);

            });

            $("input[name='radmoneyType']").click(function () {
                jinefanwei($("input[name='radmoneyType']:checked").val());
            });

            controlShow($("#ddlhbType").val());
            jinefanwei($("input[name='radmoneyType']:checked").val());

        });

        //红包类型的控制
        function controlShow(v) {
            $("#dl_keywords").hide();
            $("#dl_link").hide();
            $("#dl_share_url").hide();
             $("#dl_page_pic").hide();

            if (v == "0")
            { $("#dl_share_url").show(); }
            else if (v == "1") {
                $("#dl_keywords").show();
               
                $("#dl_share_url").show();
            }
            else if (v == "2") {
                $("#dl_link").show();
                $("#dl_page_pic").show();
            }
        }

        //定额红包和随机红包控制金额范文
        function jinefanwei(t) {
            if (t == "0") {
                $("#fanwei_tag").hide();
                $("#txtmax_value").hide();
            }
            else {
                $("#fanwei_tag").show();
                $("#txtmax_value").show();

            }

        }

    </script>
</head>

<body class="mainbody">
    <form id="form1" runat="server">
        <!--导航栏-->
        <div class="location">
            <a href="actionmgr.aspx" class="back"><i></i><span>返回现金红包活动列表</span></a>
            <i class="arrow"></i>
            <span>编辑现金红包活动</span>
        </div>
        <div class="line10"></div>
        <!--/导航栏-->

        <!--内容-->
        <div class="content-tab-wrap">
            <div id="floatHead" class="content-tab">
                <div class="content-tab-ul-wrap">
                    <ul>
                        <li><a href="javascript:;" onclick="tabs(this);" class="selected">活动基本信息</a></li>
                        <li><a href="javascript:;" onclick="tabs(this);">红包参数设置</a></li>
                    </ul>
                </div>
            </div>
        </div>
         <asp:HiddenField ID="hidid" runat="server" Value="0" />
        <div class="tab-content">
            <dl>
                <dt>活动名称</dt>
                <dd>
                    <asp:TextBox ID="txtact_name" runat="server" CssClass="input normal" datatype="*1-32" sucmsg=" " Text="红包活动开始了" />
                    <span class="Validform_checktip">*请不要多于32字!</span>
                </dd>
            </dl>

            <dl>
                <dt>活动类型</dt>
                <dd>
                   
                    <div class="rule-single-select">
                        <asp:DropDownList ID="ddlhbType" runat="server" datatype="*"
                            errormsg="请选择活动类型！" sucmsg=" ">
                            <asp:ListItem Value="">请选择类型...</asp:ListItem>
                            <asp:ListItem Value="0">关注的红包</asp:ListItem>
                            <asp:ListItem Value="1">关键词红包</asp:ListItem>
                            <asp:ListItem Value="2">网页抽奖红包</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </dd>
            </dl>

            <dl id="dl_keywords" style="display: none;">
                <dt>关键词</dt>
                <dd>
                    <asp:TextBox ID="txtKW" runat="server" CssClass="input normal" datatype="*0-20" sucmsg=" " Text="现金红包" />
                    <span class="Validform_checktip">*<span style="color:red;">该关键词需要在自定义回复里设置好</span>，用户输入此关键词将会触发此活动。</span>
                </dd>
            </dl>

            <dl id="dl_page_pic" style="display: none;">
                <dt>活动的图片</dt>
                <dd>
                    <asp:Image ID="imgactPic" runat="server" ImageUrl="/weixin/cashred/images/hongbao.jpg" Style="max-height: 80px;" sucmsg=" " />
                    <asp:TextBox ID="txtactPic" runat="server" CssClass="input normal upload-path" datatype="*0-500" />
                    <div class="upload-box upload-img"></div>
                    <span class="Validform_checktip"> 图文的图片（尺寸：700px*300px） 小于200k;</span>
                </dd>
            </dl> 

            <dl id="dl_link" style="display: none;">
                <dt>访问链接</dt>
                <dd>
                    <asp:Label ID="lblLink" runat="server" Text="系统自动生成"></asp:Label>
                </dd>
            </dl>

            <dl>
                <dt>活动时间</dt>
                <dd>
                    <div class="input-date">
                        <asp:TextBox ID="txtbeginDate" runat="server" CssClass="input date" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd HH:mm:ss'})" datatype="*1-50" errormsg="请选择正确的日期" sucmsg=" " nullmsg=" " />
                        <i>开始时间</i>
                    </div>
                    到
                  
                    <div class="input-date">
                        <asp:TextBox ID="txtendDate" runat="server" CssClass="input date" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd HH:mm:ss'})" datatype="*1-50" errormsg="请选择正确的日期" sucmsg=" " nullmsg=" " />
                        <i>结束时间</i>
                    </div>
                    <span class="Validform_checktip">*</span>

                </dd>
            </dl>

            <dl>
                <dt>领取方式</dt>
                <dd>
                    <div class="rule-multi-radio">
                        <asp:RadioButtonList ID="radlqType" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
                            <asp:ListItem Value="0" Selected="True">只能领取一次</asp:ListItem>
                            <asp:ListItem Value="1">每天可领取一次</asp:ListItem>

                        </asp:RadioButtonList>
                    </div>
                    <span class="Validform_checktip">*</span>
                </dd>
            </dl>

            <dl>
                <dt>红包总金额</dt>
                <dd>
                    <asp:TextBox ID="txttotalMoney" runat="server" CssClass="input" datatype="/^(([1-9]{1}\d*)|([0]{1}))(\.(\d){1,2})?$/" sucmsg=" " Text="0"  nullmsg=" "/>(元)
                    <span class="Validform_checktip">*</span>
                </dd>
            </dl>
             <dl>
                <dt>已领取总金额</dt>
                <dd>
                    <asp:Label ID="lblLqMoney" runat="server" Text="0"></asp:Label>(元)
                </dd>
            </dl>

            <dl>
                <dt>红包金额类型</dt>
                <dd>
                    <div class="rule-multi-radio">
                        <asp:RadioButtonList ID="radmoneyType" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
                            <asp:ListItem Value="0" Selected="True">定额红包</asp:ListItem>
                            <asp:ListItem Value="1">随机红包</asp:ListItem>
                        </asp:RadioButtonList>
                    </div>
                    <span class="Validform_checktip">*定额红包，红包金额为固定的一个值；随机红包：红包金额在一个范围内</span>
                </dd>
            </dl>
            <dl>
                <dt>红包金额</dt>
                <dd>
                    <asp:TextBox ID="txtmin_value" runat="server" CssClass="input" datatype="/^(([1-9]{1}\d*)|([0]{1}))(\.(\d){1,2})?$/" sucmsg=" " Text="1" />
                    <span id="fanwei_tag">~</span>
                    <asp:TextBox ID="txtmax_value" runat="server" CssClass="input" datatype="/^(([1-9]{1}\d*)|([0]{1}))(\.(\d){1,2})?$/" sucmsg=" " Text="0" />(元)
                    <span class="Validform_checktip">*</span>
                </dd>
            </dl>

            <dl>
                <dt>备注</dt>
                <dd>
                    <textarea name="txtremark" rows="2" cols="20" id="txtremark" class="input" runat="server">亲，请点击进入现金红包活动页面，祝您好运哦！</textarea>
                    <span class="Validform_checktip"></span>
                </dd>
            </dl>



        </div>
        <div class="tab-content" style="display: none">
            <dl>
                <dt>祝福语</dt>
                <dd>
                    <asp:TextBox ID="txtwishing" runat="server" CssClass="input normal" datatype="*1-128" sucmsg=" " Text="" />
                    <span class="Validform_checktip">*请不要多于128字!</span>
                </dd>
            </dl>
            <dl>
                <dt>提供方名称</dt>
                <dd>
                    <asp:TextBox ID="txtnick_name" runat="server" CssClass="input normal" datatype="*1-32" sucmsg=" " Text="" />
                    <span class="Validform_checktip">*请不要多于32字!</span>
                </dd>
            </dl>
            <dl>
                <dt>商户名称</dt>
                <dd>
                    <asp:TextBox ID="txtsend_name" runat="server" CssClass="input normal" datatype="*1-32" sucmsg=" " Text="" />
                    <span class="Validform_checktip">*请不要多于32字!</span>
                </dd>
            </dl>

            <dl>
                <dt>商户logo的</dt>
                <dd>
                    <asp:Image ID="imglogo_imgurl" runat="server" ImageUrl="" Style="max-height: 80px;" />
                    <asp:TextBox ID="txtlogo_imgurl" runat="server" CssClass="input normal upload-path" datatype="*0-800" />
                    <div class="upload-box upload-img"></div>
                    <span class="Validform_checktip">（尺寸：宽303像素，高251像素） 小于200k;</span>
                </dd>
            </dl>
            <dl>
                <dt>调用接口的机器 Ip 地址</dt>
                <dd>
                    <asp:TextBox ID="txtclient_ip" runat="server" CssClass="input normal" datatype="*4-15" sucmsg=" " Text="" />
                    <span class="Validform_checktip">*请不要多于15字!</span>
                </dd>
            </dl>

            <dl>
                <dt>分享文案</dt>
                <dd>
                    <textarea name="txtshare_content" rows="2" cols="20" id="txtshare_content" class="input" runat="server" datatype="*1-300" sucmsg=" " nullmsg=" "></textarea>
                    <span class="Validform_checktip">*</span>
                </dd>
            </dl>
            <dl id="dl_share_url" style="display: none;">
                <dt>分享的链接</dt>
                <dd>
                    <textarea name="txtshare_url" rows="2" cols="20" id="txtshare_url" class="input" runat="server" datatype="*0-300" sucmsg=" " nullmsg=" "></textarea>
                    <span class="Validform_checktip">*</span>
                </dd>
            </dl>

            <dl>
                <dt>分享的图片</dt>
                <dd>
                    <asp:Image ID="imgshare_imgurl" runat="server" ImageUrl="" Style="max-height: 80px; max-width: 80px;" />
                    <asp:TextBox ID="txtshare_imgurl" runat="server" CssClass="input normal upload-path" datatype="*0-800" />
                    <div class="upload-box upload-img"></div>
                    <span class="Validform_checktip">（尺寸：200px*200px）图片格式Png, 小于30k;</span>
                </dd>
            </dl>



        </div>


        <!--/内容-->

        <!--工具栏-->
        <div class="page-footer">
            <div class="btn-list">
                <asp:Button ID="btnSubmit" runat="server" Text="提交保存" CssClass="btn" OnClick="btnSubmit_Click" />
                <a href="dzplist.aspx"><span class="btn yellow">返回上一页</span></a>

            </div>
            <div class="clear"></div>
        </div>
        <!--/工具栏-->
    </form>
</body>
</html>
