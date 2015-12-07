<%@ Page Language="C#" AutoEventWireup="true" ValidateRequest="false" CodeBehind="hotel_info.aspx.cs" Inherits="WeiXinPF.Web.admin.hotel.hotel_info" %>

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
            $(".upload-album").each(function () {
                $(this).InitSWFUpload({ btntext: "批量上传", btnwidth: 66, single: false, water: true, thumbnail: true, filesize: "2048", sendurl: "../../tools/upload_ajax.ashx", flashurl: "../../scripts/swfupload/swfupload.swf", filetypes: "*.jpg;*.jpge;*.png;*.gif;" });
            });
            $(".attach-btn").click(function () {
                showAttachDialog();
            });

            //初始化编辑器
            var editor = KindEditor.create('.editor', {
                width: '98%',
                height: '350px',
                resizeType: 1,
                uploadJson: '../../tools/upload_ajax.ashx?action=EditorFile&IsWater=1',
                fileManagerJson: '../../tools/upload_ajax.ashx?action=ManagerFile',
                allowFileManager: true
            });
            var editorMini = KindEditor.create('.editor-mini', {
                width: '98%',
                height: '250px',
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
        <div class="location" runat="server" id="divLocation">
            
        </div>
        <div class="line10"></div>


        <div class="content-tab-wrap">
            <div id="floatHead" class="content-tab">
                <div class="content-tab-ul-wrap">
                    <ul>
                        <li><a href="javascript:;" onclick="tabs(this);" class="selected">商户或门店信息</a></li>
                        <li><a href="javascript:;" onclick="tabs(this);">商户或门店图片</a></li>
                        <asp:HiddenField ID="hidId" runat="server" Value="0" />
                    </ul>
                </div>
            </div>
        </div>


        <div class="tab-content">
            <dl>
                <dt>编号：</dt>
                <dd>
                    <asp:Label runat="server" ID="lblHotelCode" Font-Bold="true"></asp:Label>
                </dd>
            </dl>
            <dl>
                <dt>商家名称：</dt>
                <dd>
                    <asp:Label runat="server" ID="lblHotelName"></asp:Label>
                </dd>
            </dl>
            <dl>
                <dt>运营人：</dt>
                <dd>
                    <asp:Label runat="server" ID="lblOperator"></asp:Label>
                </dd>
            </dl>
            <dl>
                <dt>电话：</dt>
                <dd>
                    <asp:Label runat="server" ID="lblHotelPhone"></asp:Label>
                </dd>
            </dl>
            <dl>
                <dt>地址：</dt>
                <dd>
                    <asp:TextBox runat="server" ID="hotelAddress" CssClass="input normal" sucmsg=" " nullmsg=" " datatype="*1-100"></asp:TextBox>
                    <span class="Validform_checktip">*请在下方地图中输入具体地点，即可获取该地点的具体坐标</span>
                </dd>
                <dd>纬度（x）: 
                 <asp:TextBox ID="txtLatXPoint" runat="server" Width="200px" Text="" CssClass="input small " datatype="*1-20" sucmsg=" " nullmsg=" "></asp:TextBox>
                    <span class="Validform_checktip">*</span> &nbsp;&nbsp;&nbsp;
                </dd>
                <dd>经度（y）:
                      <asp:TextBox ID="txtLngYPoint" runat="server" Width="200px" Text="" CssClass="input small " datatype="*1-20" sucmsg=" " nullmsg=" "></asp:TextBox>
                    <span class="Validform_checktip">*</span>
                </dd>
                <dd>
                    <iframe id="baiduframe" src="../../weixin/map/qqmap/qqmap_getLocation.html" height="380" width="600" style="border: 1px solid #e1e1e1;"></iframe>
                </dd>
            </dl>
            <dl>
                <dt>手机号：</dt>
                <dd>
                    <asp:TextBox runat="server" ID="mobilPhone" CssClass="input normal" sucmsg=" " nullmsg=" " datatype="*1-100"></asp:TextBox>
                    <span class="Validform_checktip">*</span>
                </dd>
            </dl>
            <dl  style="display: none">
                <dt>Logo：</dt>
                <dd>
                    <asp:TextBox ID="coverPic" runat="server"   />
<%--                    <asp:TextBox ID="coverPic" runat="server" CssClass="input normal upload-path" datatype="*1-200"  sucmsg=" " nullmsg=" " />--%>
<%--                    <div class="upload-box upload-img"></div>--%>
<%--                    <span class="Validform_checktip">*</span>--%>
                </dd>
            </dl>
            <dl>
                <dt>首页封面：</dt>
                <dd>
                    <asp:TextBox ID="topPic" runat="server" CssClass="input normal upload-path" datatype="*1-200"  sucmsg=" " nullmsg=" " />
                    <div class="upload-box upload-img"></div>
                    <span class="Validform_checktip">*建议：为保证图片清晰的显示，建议您上传的图片尺寸为400*200</span>
                </dd>
            </dl>
            <dl>
                <dt>商家介绍：</dt>
                <dd>
                    <textarea id="hotelIntroduct" class="editor" style="visibility: hidden;" runat="server"></textarea>
                    <span class="Validform_checktip">*</span>
                </dd>
            </dl>
        </div>
        <div class="tab-content" style="display: none">
            <div class="alert alert-warning " role="alert" style=" text-align: left;">

                <strong>建议：</strong>为保证图片清晰的显示，建议您上传的图片尺寸为400*200</div>
           
            <dl>
                <dt>文字描述：</dt>
                <dd>
                    <asp:TextBox runat="server" ID="title1" CssClass="input normal" datatype="*1-100" sucmsg=" " nullmsg=" " Style="width: 100px;"></asp:TextBox>
                    <span class="Validform_checktip">*</span>
                    排序：         
                   <asp:TextBox runat="server" ID="sortid1" CssClass="input normal" sucmsg=" " nullmsg=" " datatype="n" Style="width: 50px;"></asp:TextBox>
                    <span class="Validform_checktip">*</span>
                    图片地址：                  
                   <asp:TextBox ID="picUrl1" runat="server" CssClass="input normal upload-path" datatype="*1-100" Style="width: 100px;" sucmsg=" " nullmsg=" " />
                    <div class="upload-box upload-img"></div>
                    图片跳转地址：                  
                   <asp:TextBox ID="picTiaozhuan1" runat="server" CssClass="input" Style="width: 100px;" sucmsg=" " nullmsg=" " />
                    <span class="Validform_checktip">*</span>
                </dd>
            </dl>
            <dl>
                <dt>文字描述：</dt>
                <dd>
                    <asp:TextBox runat="server" ID="title2" CssClass="input normal" datatype="*1-100" sucmsg=" " nullmsg=" " Style="width: 100px;"></asp:TextBox>
                    <span class="Validform_checktip">*</span>
                    排序：         
                   <asp:TextBox runat="server" ID="sortid2" CssClass="input normal" sucmsg=" " nullmsg=" " datatype="n" Style="width: 50px;"></asp:TextBox>
                    <span class="Validform_checktip">*</span>
                    图片地址：                  
                   <asp:TextBox ID="picUrl2" runat="server" CssClass="input normal upload-path" datatype="*1-100" Style="width: 100px;" sucmsg=" " nullmsg=" " />
                    <div class="upload-box upload-img"></div>
                    图片跳转地址：                  
                   <asp:TextBox ID="picTiaozhuan2" runat="server" CssClass="input" Style="width: 100px;" sucmsg=" " nullmsg=" " />
                    <span class="Validform_checktip">*</span>
                </dd>
            </dl>

            <dl>
                <dt>文字描述：</dt>
                <dd>
                    <asp:TextBox runat="server" ID="title3" CssClass="input normal" sucmsg=" " nullmsg=" " Style="width: 100px;"></asp:TextBox>
                    <span class="Validform_checktip">*</span>
                    排序：         
                   <asp:TextBox runat="server" ID="sortid3" CssClass="input normal" sucmsg=" " nullmsg=" " Style="width: 50px;"></asp:TextBox>
                    <span class="Validform_checktip">*</span>
                    图片地址：                  
                   <asp:TextBox ID="picUrl3" runat="server" CssClass="input normal upload-path" Style="width: 100px;" sucmsg=" " nullmsg=" " />
                    <div class="upload-box upload-img"></div>
                    图片跳转地址：                  
                   <asp:TextBox ID="picTiaozhuan3" runat="server" CssClass="input" Style="width: 100px;" sucmsg=" " nullmsg=" " />
                    <span class="Validform_checktip">*</span>
                </dd>
            </dl>

            <dl>
                <dt>文字描述：</dt>
                <dd>
                    <asp:TextBox runat="server" ID="title4" CssClass="input normal" sucmsg=" " nullmsg=" " Style="width: 100px;"></asp:TextBox>
                    <span class="Validform_checktip">*</span>
                    排序：         
                   <asp:TextBox runat="server" ID="sortid4" CssClass="input normal" sucmsg=" " nullmsg=" " Style="width: 50px;"></asp:TextBox>
                    <span class="Validform_checktip">*</span>
                    图片地址：                  
                   <asp:TextBox ID="picUrl4" runat="server" CssClass="input normal upload-path" Style="width: 100px;" sucmsg=" " nullmsg=" " />
                    <div class="upload-box upload-img"></div>
                    图片跳转地址：                  
                   <asp:TextBox ID="picTiaozhuan4" runat="server" CssClass="input" Style="width: 100px;" sucmsg=" " nullmsg=" " />
                    <span class="Validform_checktip">*</span>
                </dd>
            </dl>
            <dl>
                <dt>文字描述：</dt>
                <dd>
                    <asp:TextBox runat="server" ID="title5" CssClass="input normal" sucmsg=" " nullmsg=" " Style="width: 100px;"></asp:TextBox>
                    <span class="Validform_checktip">*</span>
                    排序：         
                   <asp:TextBox runat="server" ID="sortid5" CssClass="input normal" sucmsg=" " nullmsg=" " Style="width: 50px;"></asp:TextBox>
                    <span class="Validform_checktip">*</span>
                    图片地址：                  
                   <asp:TextBox ID="picUrl5" runat="server" CssClass="input normal upload-path" Style="width: 100px;" sucmsg=" " nullmsg=" " />
                    <div class="upload-box upload-img"></div>
                    图片跳转地址：                  
                   <asp:TextBox ID="picTiaozhuan5" runat="server" CssClass="input" Style="width: 100px;" sucmsg=" " nullmsg=" " />
                    <span class="Validform_checktip">*</span>
                </dd>
            </dl>

            <dl>
                <dt>文字描述：</dt>
                <dd>
                    <asp:TextBox runat="server" ID="title6" CssClass="input normal" sucmsg=" " nullmsg=" " Style="width: 100px;"></asp:TextBox>
                    <span class="Validform_checktip">*</span>
                    排序：         
                   <asp:TextBox runat="server" ID="sortid6" CssClass="input normal" sucmsg=" " nullmsg=" " Style="width: 50px;"></asp:TextBox>
                    <span class="Validform_checktip">*</span>
                    图片地址：                  
                   <asp:TextBox ID="picUrl6" runat="server" CssClass="input normal upload-path" Style="width: 100px;" sucmsg=" " nullmsg=" " />
                    <div class="upload-box upload-img"></div>
                    图片跳转地址：                  
                   <asp:TextBox ID="picTiaozhuan6" runat="server" CssClass="input" Style="width: 100px;" sucmsg=" " nullmsg=" " />
                    <span class="Validform_checktip">*</span>
                </dd>
            </dl>

        </div>

        <div class="page-footer">
            <div class="btn-list">
                <asp:Button ID="save_hotel" runat="server" CssClass="btn" Text="保存" OnClick="save_hotel_Click" />
            </div>
            <div class="clear"></div>
        </div>

    </form>
</body>
</html>
