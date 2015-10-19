<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="caipin_manage_add.aspx.cs" Inherits="WeiXinPF.Web.admin.diancai.caipin_manage_add" ValidateRequest="false" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
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
      <div class="location">
           <a href="shop_list.aspx" class="home"><i></i><span>商户或门店列表</span></a>
            <i  class="arrow"></i><span><a href="caipin_manage.aspx?shopid=<%=shopid %>">商品信息查看</a></span>
            <i class="arrow"></i>
            <span>编辑商品</span>
        </div>
        <div class="line10"></div>
        <!--/导航栏-->

          <div class="content-tab-wrap">
            <div id="floatHead" class="content-tab">
                <div class="content-tab-ul-wrap">
                    <ul>
                        <li><a href="javascript:;" onclick="tabs(this);" class="selected">编辑商品</a></li>                    
                        <asp:HiddenField ID="hidId" runat="server" Value="0" />
                    </ul>
                </div>
            </div>
        </div>
         <div  class="tab-content" >
              <dl>
                  <dt>编号：</dt>
                  <dd>
                  <asp:TextBox runat="server" ID="number" CssClass="input normal" disabled='disabled'  placeholder="系统自动生成"  ></asp:TextBox>
                  <span class="Validform_checktip" >系统自动生成*</span>
                  </dd>
              </dl>
              <dl>
                  <dt>商品名称：</dt>
                  <dd>
                  <asp:TextBox runat="server" ID="cpName" CssClass="input normal" sucmsg=" " nullmsg="" datatype="*1-100" ></asp:TextBox>
                  <span class="Validform_checktip">*</span>
                  </dd>
              </dl>
             <dl>
                  <dt>商品类别：</dt>
                  <dd>
                        <div class="rule-single-select">
                        <asp:DropDownList ID="dllCategoryName" runat="server" datatype="*" sucmsg=" ">
                        </asp:DropDownList>
                    </div>
                  </dd>
              </dl>
                <dl>
                  <dt>原价：</dt>
                  <dd>
                  <asp:TextBox runat="server" ID="cpPrice" CssClass="input normal" sucmsg=" " nullmsg="" datatype="n" ></asp:TextBox>
                  <span class="Validform_checktip">*</span>
                  </dd>
              </dl>
                <dl>
                  <dt>折扣价：</dt>
                  <dd>
                  <asp:TextBox runat="server" ID="zkPrice" CssClass="input normal" sucmsg=" " nullmsg="" datatype="n" ></asp:TextBox>
                  <span class="Validform_checktip">*</span>
                  </dd>
              </dl>
                 <dl>
                  <dt>计价单位：</dt>
                  <dd>
                  <asp:TextBox runat="server" ID="priceUnite" CssClass="input normal" sucmsg=" " nullmsg="" datatype="*1-100" ></asp:TextBox>
                  <span class="Validform_checktip">*</span>
                  </dd>
              </dl>
               <dl style="display:none;">
                  <dt>图片：</dt>
                  <dd>
                  <asp:TextBox runat="server" ID="cpPic" CssClass="input normal" sucmsg=" " nullmsg="" ></asp:TextBox>
                         <span class="Validform_checktip">*</span>
                  </dd>
              </dl>
                <dl>
                  <dt>商品图片：</dt>
                  <dd>
                   <asp:TextBox ID="picUrl" runat="server" CssClass="input normal upload-path"  style="width:200px;"  sucmsg=" " nullmsg=""  />
                    <div class="upload-box upload-img"></div>
                  </dd>
              </dl>
             
              <dl>
                <dt>使用有效期</dt>
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
                  <dt>商品说明：</dt>
                  <dd>
                    <textarea name="detailContent" rows="2" cols="20" id="detailContent" datatype="*1-1000" sucmsg=" " nullmsg=""  class="input" runat="server"></textarea>
                        <span class="Validform_checktip">*</span>
                  </dd>
              </dl>
              <dl>
                  <dt>使用说明：</dt>
                  <dd>
                    <textarea name="instructions" rows="2" cols="20" id="instructions" datatype="*1-1000" sucmsg=" " nullmsg=""  class="input" runat="server"></textarea>
                        <span class="Validform_checktip">*</span>
                  </dd>
              </dl>
             
                <dl>
                  <dt>退单规则：</dt>
                  <dd>
                    <textarea name="chargeback" rows="2" cols="20" id="chargeback" datatype="*1-1000" sucmsg=" " nullmsg=""  class="input" runat="server"></textarea>
                        <span class="Validform_checktip">*</span>
                  </dd>
              </dl>

                <dl>
                <dt>商品介绍：</dt>
                <dd>
                    <textarea name="shopIntroduction"  id="shopIntroduction" class="editor" style="visibility: hidden;" runat="server"></textarea>
                    <span class="Validform_checktip">*</span>
                </dd>
            </dl>
              <dl>
                <dt>排序：</dt>
                <dd>
                    <asp:TextBox runat="server" ID="sortid" CssClass="input normal" sucmsg=" " nullmsg="" datatype="n" ></asp:TextBox>
                    <span class="Validform_checktip">*</span>
                </dd>
            </dl>
         </div>

         <div class="page-footer" >
            <div class="btn-list">      
             <asp:Button ID="save_caidanmanage" runat="server"  CssClass="btn" Text="保存" OnClick="save_caidanmanage_Click"    />
            </div>
            <div class="clear"></div>
        </div>
    </form>
</body>
</html>
