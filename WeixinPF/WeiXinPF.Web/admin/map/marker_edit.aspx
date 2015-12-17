<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="marker_edit.aspx.cs" Inherits="WeiXinPF.Web.admin.map.marker_edit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <script type="text/javascript" src="../../scripts/jquery/jquery-1.10.2.min.js"></script>
    <script type="text/javascript" src="../../scripts/jquery/Validform_v5.3.2_min.js"></script>
    <script type="text/javascript" src="../../scripts/lhgdialog/lhgdialog.js?skin=idialog"></script>
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

            $("#txtSelectUrl").on("click", function () {
                $.dialog({
                    id: 'selectmodule',
                    fixed: true,
                    lock: true,
                    max: false,
                    min: false,

                    title: "选择模型",
                    content: "url:/admin/map/select_scenic.aspx?txt=txtUrl",
                    height: 400,
                    width: 450
                });
            });
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <!--导航栏-->
        <div class="location">
            <a href="map_scenic.aspx" class="home"><i></i><span>景区导航设置</span></a>
            <i class="arrow"></i>
            <span>景点编辑</span>
        </div>
        <div class="line10"></div>
        <!--/导航栏-->
        <!--内容-->
        <div class="content-tab-wrap">
            <div id="floatHead" class="content-tab">
                <div class="content-tab-ul-wrap">
                    <ul>
                        <li><a href="javascript:;" onclick="tabs(this);" class="selected">景点详情</a></li>
                    </ul>
                </div>
            </div>
        </div>
        <div class="tab-content">
            <dl>
                <dt>景点</dt>
                <dd>
                    <asp:HiddenField ID="hfScenicId" runat="server" />
                    <asp:TextBox ID="txtUrl" runat="server" datatype="*2-100" CssClass="input normal" />
                    <input id="txtSelectUrl" type="button" value="选择" class="btn" />
                    <span class="Validform_checktip">*</span>
                </dd>
            </dl>
            <dl>
                <dt>名称</dt>
                <dd>
                    <asp:TextBox ID="txtName" runat="server" CssClass="input normal" datatype="*2-20" sucmsg=" " />
                    <span class="Validform_checktip">*名称最多20个字符</span>
                </dd>
            </dl>
            <dl>
                <dt>景点描述</dt>
                <dd>
                    <asp:TextBox ID="txtRemark" runat="server" datatype="*2-100" CssClass="input normal upload-path" />
                    <span class="Validform_checktip">*</span>
                </dd>
            </dl>
            <dl style="display: none">
                <dt>引路说明</dt>
                <dd>
                    <asp:TextBox ID="txtDescription" TextMode="MultiLine" runat="server" CssClass="input normal" />
                </dd>
            </dl>
            <dl>
                <dt>地图坐标：</dt>
                <dd>
                    <%--<asp:TextBox runat="server" ID="txtAddress" CssClass="input normal" sucmsg=" " nullmsg=" " datatype="*1-100"></asp:TextBox>--%>
                    <span class="Validform_checktip">*请在下方地图中输入具体地点，即可获取该地点的具体坐标</span>
                </dd>
                <dd>纬度（x）: 
                    <asp:TextBox ID="txtLatXPoint" runat="server" Width="200px" Text="" CssClass="input small " datatype="*1-20" sucmsg=" " nullmsg=" "></asp:TextBox>
                    <span class="Validform_checktip">*</span>
                </dd>
                <dd>经度（y）:
                    <asp:TextBox ID="txtLngYPoint" runat="server" Width="200px" Text="" CssClass="input small " datatype="*1-20" sucmsg=" " nullmsg=" "></asp:TextBox>
                    <span class="Validform_checktip">*</span>
                </dd>
                <dd>
                    <iframe id="qqframe" src="../../weixin/map/qqmap/qqmap_getLocation.html" height="380" width="600" style="border: 1px solid #e1e1e1;"></iframe>
                </dd>
            </dl>
            <dl>
                <dt>是否推荐：</dt>
                <dd>
                    <div class="rule-multi-radio">
                        <asp:RadioButtonList ID="rblRecommend" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
                            <asp:ListItem Value="1">推荐</asp:ListItem>
                            <asp:ListItem Value="0" Selected="True">不推荐</asp:ListItem>
                        </asp:RadioButtonList>
                    </div>
                    <span class="Validform_checktip">*设置为推荐将会在美食美宿首页热门推荐栏目中显示。</span></dd>
            </dl>
        </div>
        <!--/内容-->
        <!--工具栏-->
        <div class="page-footer">
            <div class="btn-list">
                <asp:Button ID="btnSubmit" runat="server" Text="提交保存" CssClass="btn" OnClick="btnSubmit_Click" />
                <a href="map_scenic.aspx"><span class="btn yellow">返回上一页</span></a>
            </div>
            <div class="clear"></div>
        </div>
        <!--/工具栏-->
    </form>
</body>
</html>
