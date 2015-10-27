<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="hotel_register.aspx.cs" Inherits="WeiXinPF.Web.admin.hotel.hotel_register" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <script type="text/javascript" src="../../scripts/jquery/jquery-2.1.0.min.js"></script>
    <script type="text/javascript" src="../../scripts/jquery/Validform_v5.3.2_min.js"></script>
    <script type="text/javascript" src="../../scripts/lhgdialog/lhgdialog.js?skin=idialog"></script>
    <script type="text/javascript" src="../js/layout.js"></script>
    <link href="../skin/default/style.css" rel="stylesheet" type="text/css" />
    <link href="../skin/mystyle.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        $(function () {
            //初始化表单验证
            $("#form1").initValidform();
        });
    </script>
</head>
<body class="mainbody">
    <form id="form1" runat="server">
        <div class="location">
            <a href="hotel_list.aspx" class="home"><i></i><span>商户或门店列表</span></a>
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
                    <asp:Label ID="lblHotelCode" runat="server" Text="" Font-Bold="true">系统自动生成</asp:Label>
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
                <dt>星级：</dt>
                <dd>
                    <div class="rule-single-select">
                        <asp:DropDownList runat="server" ID="ddlHotelLevel">
                            <asp:ListItem Value="无星级">无星级</asp:ListItem>
                            <asp:ListItem Value="一星级">一星级</asp:ListItem>
                            <asp:ListItem Value="二星级">二星级</asp:ListItem>
                            <asp:ListItem Value="三星级">三星级</asp:ListItem>
                            <asp:ListItem Value="四星级">四星级</asp:ListItem>
                            <asp:ListItem Value="五星级">五星级</asp:ListItem>
                        </asp:DropDownList>
                    </div>
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
                    <div class="rule-multi-radio">
                        <asp:RadioButtonList ID="rblRecommend" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
                            <asp:ListItem Value="1">推荐</asp:ListItem>
                            <asp:ListItem Value="0" Selected="True">不推荐</asp:ListItem>
                        </asp:RadioButtonList>
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

