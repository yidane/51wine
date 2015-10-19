<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="shop_register.aspx.cs" Inherits="WeiXinPF.Web.admin.diancai.shop_register" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
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
        <div class="location">
            <a href="shop_list.aspx" class="home"><i></i><span>商户或门店列表</span></a>
            <i class="arrow"></i>
            <span>商户或门店入驻登记</span>
        </div>
        <div class="line10"></div>
        <!--/导航栏-->

        <div class="content-tab-wrap">
            <div id="floatHead" class="content-tab">
                <div class="content-tab-ul-wrap">
                    <ul>
                        <li><a href="javascript:;" onclick="tabs(this);" class="selected">入驻登记信息</a></li>
                    </ul>
                </div>
            </div>
        </div>

        <div class="tab-content">
            <dl>
                <dt>商户号：</dt>
                <dd>
                    <asp:Label ID="lblShopId" runat="server" Text="" Font-Bold="true">系统自动生成</asp:Label>
                </dd>
            </dl>
            <dl>
                <dt>商户或门店名称：</dt>
                <dd>
                    <asp:TextBox runat="server" ID="txtHotelName" CssClass="input normal" sucmsg=" " nullmsg="" datatype="*1-100"></asp:TextBox>
                    <span class="Validform_checktip">*</span>
                </dd>
            </dl>
            <dl>
                <dt>商户类型：</dt>
                <dd>
                    <select name="type" id="ddlKcType" runat="server">
                        <option value="小吃快餐">小吃快餐</option>
                        <option value="自助餐">自助餐</option>
                        <option value="日韩料理">日韩料理</option>
                        <option value="面包甜点">面包甜点</option>
                        <option value="火锅">火锅</option>
                        <option value="西餐">西餐</option>
                        <option value="海鲜">海鲜</option>
                        <option value="烧烤">烧烤</option>
                        <option value="川菜">川菜</option>
                        <option value="咖啡厅">咖啡厅</option>
                        <option value="东南亚菜">东南亚菜</option>
                        <option value="闽菜">闽菜</option>
                        <option value="粤菜">粤菜</option>
                        <option value="湘菜">湘菜</option>
                        <option value="茶餐厅">茶餐厅</option>
                        <option value="创意菜">创意菜</option>
                        <option value="客家菜">客家菜</option>
                        <option value="东北菜">东北菜</option>
                        <option value="本帮江浙菜">本帮江浙菜</option>
                        <option value="湖北菜">湖北菜</option>
                        <option value="台湾菜">台湾菜</option>
                        <option value="西北菜">西北菜</option>
                        <option value="素菜">素菜</option>
                        <option value="江西菜">江西菜</option>
                        <option value="赣菜">赣菜</option>
                        <option value="水果店">水果店</option>
                        <option value="便利店">便利店</option>
                        <option value="小吃">小吃</option>
                        <option value="甜品">甜品</option>
                        <option value="外卖">外卖</option>
                        <option value="其他">其他</option>
                    </select>
                </dd>
            </dl>
            <dl>
                <dt>运营人：</dt>
                <dd>
                    <asp:TextBox runat="server" ID="txtOperator" CssClass="input normal" sucmsg=" " nullmsg=" " datatype="*1-100"></asp:TextBox>
                    <span class="Validform_checktip">*</span>
                </dd>
            </dl>
            <dl>
                <dt>公司电话：</dt>
                <dd>
                    <asp:TextBox runat="server" ID="txtTel" CssClass="input normal" sucmsg=" " nullmsg=" " datatype="*1-100"></asp:TextBox>
                    <span class="Validform_checktip">*</span>
                </dd>
            </dl>

            <dl>
                <dt>公司邮箱：</dt>
                <dd>
                    <asp:TextBox runat="server" ID="txtEmail" CssClass="input normal" sucmsg=" " nullmsg="请输入邮箱" datatype="*1-100"></asp:TextBox>
                    <span class="Validform_checktip">*</span>
                </dd>
            </dl>
            <dl>
                <dt>是否推荐：</dt>
                <dd>
                    <div class="rule-single-checkbox">
                        <asp:CheckBox ID="cbRecommend" runat="server" Checked="false" />
                    </div>
                    <span class="Validform_checktip">*设置为推荐将会在美食美宿首页显示。</span></dd>
            </dl>
        </div>
        <div class="page-footer">
            <div class="btn-list">
                <asp:Button ID="btnSubmit" runat="server" CssClass="btn" Text="保存" OnClick="btnSubmit_Click" />
            </div>
            <div class="clear"></div>
        </div>
    </form>
</body>
</html>

