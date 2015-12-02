<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="hotel_room_info.aspx.cs" Inherits="WeiXinPF.Web.admin.hotel.hotel_room_info" ValidateRequest="false" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <script type="text/javascript" src="../../scripts/jquery/jquery-2.1.0.min.js"></script>
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
    <style>
          .alert-success {
            color: #3c763d;
            background-color: #dff0d8;
            border-color: #d6e9c6;
        }

        .alert-danger {
            color: #a94442;
            background-color: #f2dede;
            border-color: #ebccd1;
        }

        .alert-warning {
            color: #8a6d3b;
            background-color: #fcf8e3;
            border-color: #faebcc;
        }

        .alert-info {
            color: #31708f;
            background-color: #d9edf7;
            border-color: #bce8f1;
        }

        .alert {
            padding: 15px;
            padding-left: 85px;
            border: 1px solid transparent;
            border-radius: 10px;
        }
    </style>
    <script type="text/javascript">
        $(function () {
            //初始化表单验证
            $("#form1").initValidform();

            //初始化上传控件
            $(".upload-img").each(function () {
                $(this).InitSWFUpload({ sendurl: "../../tools/upload_ajax.ashx", flashurl: "../../scripts/swfupload/swfupload.swf" });
            });

            var editor = KindEditor.create('.editor', {
                width: '98%',
                height: '350px',
                resizeType: 1,
                uploadJson: '../../tools/upload_ajax.ashx?action=EditorFile&IsWater=1',
                fileManagerJson: '../../tools/upload_ajax.ashx?action=ManagerFile',
                allowFileManager: true
            });
        });
    </script>
</head>
<body class="mainbody">
    <form id="form1" runat="server">
        <div class="location" runat="server" id="divLocation">
        </div>
        <div class="line10"></div>
        <div class="content-tab-wrap">
            <div id="floatHead" class="content-tab">
                <div class="content-tab-ul-wrap">
                    <ul>
                        <li><a href="javascript:;" onclick="tabs(this);" class="selected">编辑商品</a></li>
                        <li><a href="javascript:;" onclick="tabs(this);">商品图片</a></li>
                        <asp:HiddenField ID="hidId" runat="server" Value="0" />
                    </ul>
                </div>
            </div>
        </div>

        <div class="tab-content">
            <dl>
                <dt>商品编号：</dt>
                <dd>
                    <asp:Label ID="lblRoomCode" runat="server">编号由系统自动生成</asp:Label>
                </dd>
            </dl>
            <dl>
                <dt>商品名称：</dt>
                <dd>
                    <asp:TextBox runat="server" ID="roomType" CssClass="input normal" sucmsg=" " nullmsg=" " datatype="*1-100"></asp:TextBox>
                    <span class="Validform_checktip">*</span>
                </dd>
            </dl>

            <dl>
                <dt>原价：</dt>
                <dd>
                    <asp:TextBox runat="server" ID="roomPrice" CssClass="input normal" sucmsg=" " nullmsg=" " datatype="num"></asp:TextBox>
                    <span class="Validform_checktip">*</span>
                </dd>
            </dl>
            <dl>
                <dt>优惠价：</dt>
                <dd>
                    <asp:TextBox runat="server" ID="salePrice" CssClass="input normal" sucmsg=" " nullmsg=" " datatype="num"></asp:TextBox>
                    <span class="Validform_checktip">*</span>
                </dd>
            </dl>
            <dl>
                <dt>商品说明：</dt>
                <dd>
                    <textarea name="indroduce" rows="2" cols="20" id="indroduce" sucmsg=" " nullmsg=" " datatype="*1-50" class="input" runat="server"></textarea>
                    <span class="Validform_checktip">*最多输入50个字</span>
                </dd>
            </dl>
            <dl>
                <dt>使用说明：</dt>
                <dd>
                    <textarea name="useIntroduction" rows="2" cols="20" id="txtUsueIntroduction" sucmsg=" " nullmsg=" " class="input" runat="server"></textarea>
                    <span class="Validform_checktip">*</span>
                </dd>
            </dl>
            <dl>
                <dt>退单规则：</dt>
                <dd>
                    <textarea name="refundRule" rows="2" cols="20" id="txtRefundRule" sucmsg=" " nullmsg=" " class="input" runat="server"></textarea>
                    <span class="Validform_checktip">*</span>
                </dd>
            </dl>
            <dl>
                <dt>产品介绍：</dt>
                <dd>
                    <textarea id="facilities" class="editor" runat="server"></textarea>
                    <span class="Validform_checktip">*</span>
                </dd>
            </dl>
        </div>
        <div class="tab-content" style="display: none">
            <div class="alert alert-warning " role="alert" style="text-align: left;">

                <strong>建议：</strong>为保证图片清晰的显示，建议您上传的图片尺寸为400*200</div>
            <dl>
                <dt>文字描述：</dt>
                <dd>
                    <asp:TextBox runat="server" ID="title1" CssClass="input normal" datatype="*1-100" sucmsg=" " nullmsg=" " Style="width: 100px;"></asp:TextBox>
                    <span class="Validform_checktip">*</span>
                    排序：         
                   <asp:TextBox runat="server" ID="sortpicid1" CssClass="input normal" sucmsg=" " nullmsg=" " datatype="n" Style="width: 50px;"></asp:TextBox>
                    <span class="Validform_checktip">*</span>
                    图片地址：                  
                   <asp:TextBox ID="roomPic1" runat="server" CssClass="input normal upload-path" datatype="*1-100" Style="width: 100px;" sucmsg=" " nullmsg=" " />
                    <div class="upload-box upload-img"></div>
                    图片跳转地址：                  
                   <asp:TextBox ID="roomPictz1" runat="server" CssClass="input" Style="width: 100px;" sucmsg=" " nullmsg=" " />
                    <span class="Validform_checktip">*</span>
                </dd>
            </dl>

            <dl>
                <dt>文字描述：</dt>
                <dd>
                    <asp:TextBox runat="server" ID="title2" CssClass="input normal" sucmsg=" " nullmsg=" " Style="width: 100px;"></asp:TextBox>
                    <span class="Validform_checktip">*</span>
                    排序：         
                   <asp:TextBox runat="server" ID="sortpicid2" CssClass="input normal" sucmsg=" " nullmsg=" " datatype="n" Style="width: 50px;"></asp:TextBox>
                    <span class="Validform_checktip">*</span>
                    图片地址：                  
                   <asp:TextBox ID="roomPic2" runat="server" CssClass="input normal upload-path" Style="width: 100px;" sucmsg=" " nullmsg=" " />
                    <div class="upload-box upload-img"></div>
                    图片跳转地址：                  
                   <asp:TextBox ID="roomPictz2" runat="server" CssClass="input" Style="width: 100px;" sucmsg=" " nullmsg=" " />
                    <span class="Validform_checktip">*</span>
                </dd>
            </dl>

            <dl>
                <dt>文字描述：</dt>
                <dd>
                    <asp:TextBox runat="server" ID="title3" CssClass="input normal" sucmsg=" " nullmsg=" " Style="width: 100px;"></asp:TextBox>
                    <span class="Validform_checktip">*</span>
                    排序：         
                   <asp:TextBox runat="server" ID="sortpicid3" CssClass="input normal" sucmsg=" " nullmsg=" " Style="width: 50px;"></asp:TextBox>
                    <span class="Validform_checktip">*</span>
                    图片地址：                  
                   <asp:TextBox ID="roomPic3" runat="server" CssClass="input normal upload-path" Style="width: 100px;" sucmsg=" " nullmsg=" " />
                    <div class="upload-box upload-img"></div>
                    图片跳转地址：                  
                   <asp:TextBox ID="roomPictz3" runat="server" CssClass="input" Style="width: 100px;" sucmsg=" " nullmsg=" " />
                    <span class="Validform_checktip">*</span>
                </dd>
            </dl>

            <dl>
                <dt>文字描述：</dt>
                <dd>
                    <asp:TextBox runat="server" ID="title4" CssClass="input normal" sucmsg=" " nullmsg=" " Style="width: 100px;"></asp:TextBox>
                    <span class="Validform_checktip">*</span>
                    排序：         
                   <asp:TextBox runat="server" ID="sortpicid4" CssClass="input normal" sucmsg=" " nullmsg=" " Style="width: 50px;"></asp:TextBox>
                    <span class="Validform_checktip">*</span>
                    图片地址：                  
                   <asp:TextBox ID="roomPic4" runat="server" CssClass="input normal upload-path" Style="width: 100px;" sucmsg=" " nullmsg=" " />
                    <div class="upload-box upload-img"></div>
                    图片跳转地址：                  
                   <asp:TextBox ID="roomPictz4" runat="server" CssClass="input" Style="width: 100px;" sucmsg=" " nullmsg=" " />
                    <span class="Validform_checktip">*</span>
                </dd>
            </dl>

            <dl>
                <dt>文字描述：</dt>
                <dd>
                    <asp:TextBox runat="server" ID="title5" CssClass="input normal" sucmsg=" " nullmsg=" " Style="width: 100px;"></asp:TextBox>
                    <span class="Validform_checktip">*</span>
                    排序：         
                   <asp:TextBox runat="server" ID="sortpicid5" CssClass="input normal" sucmsg=" " nullmsg=" " Style="width: 50px;"></asp:TextBox>
                    <span class="Validform_checktip">*</span>
                    图片地址：                  
                   <asp:TextBox ID="roomPic5" runat="server" CssClass="input normal upload-path" Style="width: 100px;" sucmsg=" " nullmsg=" " />
                    <div class="upload-box upload-img"></div>
                    图片跳转地址：                  
                   <asp:TextBox ID="roomPictz5" runat="server" CssClass="input" Style="width: 100px;" sucmsg=" " nullmsg=" " />
                    <span class="Validform_checktip">*</span>
                </dd>
            </dl>

            <dl>
                <dt>文字描述：</dt>
                <dd>
                    <asp:TextBox runat="server" ID="title6" CssClass="input normal" sucmsg=" " nullmsg=" " Style="width: 100px;"></asp:TextBox>
                    <span class="Validform_checktip">*</span>
                    排序：         
                   <asp:TextBox runat="server" ID="sortpicid6" CssClass="input normal" sucmsg=" " nullmsg=" " Style="width: 50px;"></asp:TextBox>
                    <span class="Validform_checktip">*</span>
                    图片地址：                  
                   <asp:TextBox ID="roomPic6" runat="server" CssClass="input normal upload-path" Style="width: 100px;" sucmsg=" " nullmsg=" " />
                    <div class="upload-box upload-img"></div>
                    图片跳转地址：                  
                   <asp:TextBox ID="roomPictz6" runat="server" CssClass="input" Style="width: 100px;" sucmsg=" " nullmsg=" " />
                    <span class="Validform_checktip">*</span>
                </dd>
            </dl>

        </div>

        <div class="page-footer">
            <div class="btn-list">
                <asp:Label runat="server" ID="lblComment" Font-Size="12px">审核意见：</asp:Label>
                <asp:TextBox runat="server" ID="txtComment" CssClass="input txt"></asp:TextBox>
                <asp:Button ID="save_room" runat="server" CssClass="btn" Text="保存并提交" OnClick="save_room_Click" />
                <asp:Button ID="btnAgree" runat="server" CssClass="btn" Text="审核通过" OnClick="btnAgree_Click" />
                <asp:Button ID="btnRefuse" runat="server" CssClass="btn" Text="审核不通过" OnClick="btnRefuse_Click" />
                <asp:Button ID="btnPublish" runat="server" CssClass="btn" Text="发布" OnClick="btnPublish_Click" />
            </div>
            <div class="clear"></div>
        </div>
    </form>
</body>
</html>
