﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="gift_edit.aspx.cs" Inherits="WeiXinPF.Web.admin.ucard.gift_edit" %>


<%@ Import Namespace="WeiXinPF.Common" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>发布优惠券</title>
    <script type="text/javascript" src="../../scripts/jquery/jquery-1.10.2.min.js"></script>
    <script type="text/javascript" src="../../scripts/jquery/Validform_v5.3.2_min.js"></script>
    <script type="text/javascript" src="../../scripts/lhgdialog/lhgdialog.js?skin=idialog"></script>
    <script type="text/javascript" src="../../scripts/datepicker/WdatePicker.js"></script>
    <script type="text/javascript" src="../../scripts/swfupload/swfupload.js"></script>
    <script type="text/javascript" src="../../scripts/swfupload/swfupload.queue.js"></script>
    <script type="text/javascript" src="../../scripts/swfupload/swfupload.handlers.js"></script>
    <script type="text/javascript" charset="utf-8" src="../../editor/kindeditor-min.js"></script>
    <script type="text/javascript" charset="utf-8" src="../../editor/lang/zh_CN.js"></script>
    <script type="text/javascript" src="../../admin/js/layout.js"></script>
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

        });
    </script>
</head>

<body class="mainbody">
    <form id="form1" runat="server">
        <!--导航栏-->
        <div class="location"> 
            <a href="gift_list.aspx?id=<%=sid %>" class="home"><span>礼品券列表</span></a>
            <i class="arrow"></i>
            <span>发布优惠券</span>
        </div>
        <div class="line10"></div>
        <!--/导航栏-->
        <asp:HiddenField ID="hidid" runat="server" Value="0" />
        <!--内容-->
        <div class="content-tab-wrap">
            <div id="floatHead" class="content-tab">
                <div class="content-tab-ul-wrap">
                    <ul>
                        <li><a href="javascript:;" onclick="tabs(this);" class="selected">发布礼品券</a></li>

                    </ul>
                </div>
            </div>
        </div>

        <div class="tab-content">
            <dl>
                <dt>礼品名称</dt>
                <dd>
                    <asp:TextBox ID="txtgName" runat="server" CssClass="input normal" datatype="*1-50" sucmsg=" " Text="" />
                    <span class="Validform_checktip">*请不要多于50字!</span>
                </dd>
            </dl>
                
             <dl>
                <dt>有效期</dt>
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
                <dt>使用说明</dt>
                <dd>
                    <textarea name="txtuseContent" rows="3" cols="20" id="txtuseContent" class="input" runat="server" datatype="*0-500" sucmsg=" " nullmsg=" "></textarea>
                    <span class="Validform_checktip">*</span>
                    <br />在此说明券的使用方式，如最低消费金额，优惠券打折信息，不与其他优惠同时使用、节假日不可使用等。
                </dd>
            </dl>
 
            <dl>
                <dt>兑换礼品所需积分</dt>
                <dd>
                  <asp:TextBox ID="txtscore" runat="server" CssClass="input small" datatype="n" sucmsg=" "></asp:TextBox>
                </dd>
            </dl>
        </div>

        <!--/内容-->

        <!--工具栏-->
        <div class="page-footer">
            <div class="btn-list">
                <asp:Button ID="btnSubmit" runat="server" Text="提交保存" CssClass="btn" OnClick="btnSubmit_Click" />
                <a href="gift_list.aspx?id=<%=sid %>"><span class="btn yellow">返回上一页</span></a>

            </div>
            <div class="clear"></div>
        </div>
        <!--/工具栏-->
    </form>
</body>
</html>
