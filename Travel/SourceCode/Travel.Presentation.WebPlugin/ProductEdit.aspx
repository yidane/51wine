<%@ Page Title="" Language="C#" MasterPageFile="~/ListContent.Master" AutoEventWireup="true" CodeBehind="ProductEdit.aspx.cs" Inherits="Travel.Presentation.WebPlugin.ProductEdit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!--导航栏-->
    <div class="location">
        <a href="ProductList.aspx" class="home"><i></i><span>产品列表</span></a>
        <i class="arrow"></i>
        <span>编辑信息</span>
    </div>
    <div class="line10"></div>
    <!--/导航栏-->

    <!--内容-->
    <div class="content-tab-wrap">
        <div id="floatHead" class="content-tab">
            <div class="content-tab-ul-wrap">
                <ul>
                    <li><a href="javascript:;" onclick="tabs(this);" class="selected">基本信息</a></li>
                </ul>
            </div>
        </div>
    </div>

    <div class="tab-content">
        <dl>
            <dt>产品ID</dt>
            <dd>
                <asp:TextBox ID="txtProductID" runat="server" CssClass="input normal" datatype="*2-100" sucmsg=" " />
            </dd>
        </dl>

        <dl>
            <dt>产品包ID</dt>
            <dd>
                <asp:TextBox ID="txtProductPackageID" runat="server" CssClass="input normal" datatype="*2-100" sucmsg=" " />
            </dd>
        </dl>

        <dl>
            <dt>产品来源</dt>
            <dd>
                <asp:TextBox ID="txtProductSource" runat="server" CssClass="input normal" datatype="*2-100" sucmsg=" " />
            </dd>
        </dl>

        <dl>
            <dt>产品类型ID</dt>
            <dd>
                <asp:TextBox ID="txtProductCategoryId" Enabled="False" runat="server" CssClass="input normal" datatype="*2-100" sucmsg=" " />
            </dd>
        </dl>

        <dl>
            <dt>产品名称</dt>
            <dd>
                <asp:TextBox ID="txtProductName" runat="server" MaxLength="255" CssClass="input normal" />
            </dd>
        </dl>

        <dl>
            <dt>原产品名称</dt>
            <dd>
                <asp:TextBox ID="txtOldProductName" runat="server" MaxLength="255" CssClass="input normal" />
            </dd>
        </dl>

        <dl>
            <dt>产品类型</dt>
            <dd>
                <asp:TextBox ID="txtProductType" runat="server" MaxLength="255" CssClass="input normal" />
            </dd>
        </dl>

        <dl>
            <dt>产品编码</dt>
            <dd>
                <asp:TextBox ID="txtProductCode" runat="server" MaxLength="255" CssClass="input normal" />
            </dd>
        </dl>

        <dl>
            <dt>产品价格</dt>
            <dd>
                <asp:TextBox ID="txtProductPrice" runat="server" MaxLength="255" CssClass="input normal" />
            </dd>
        </dl>

        <dl>
            <dt>产品描叙</dt>
            <dd>
                <asp:TextBox ID="txtProductDescription" runat="server" TextMode="MultiLine" Height="100px" MaxLength="255" CssClass="input normal" />
            </dd>
        </dl>

        <dl>
            <dt>是否为组合类型产品</dt>
            <dd>
                <asp:CheckBox runat="server" ID="cboIsCombinedProduct" />
            </dd>
        </dl>

        <dl>
            <dt>是否为自定义产品</dt>
            <dd>
                <asp:CheckBox runat="server" ID="cboIsSelfDefinedProduct" />
            </dd>
        </dl>

        <dl>
            <dt>当前状态</dt>
            <dd>
                <asp:TextBox ID="txtCurrentStatus" runat="server" MaxLength="255" CssClass="input normal" />
            </dd>
        </dl>

        <dl>
            <dt>一级排序</dt>
            <dd>
                <asp:TextBox ID="txtFirstSort" runat="server" MaxLength="255" CssClass="input normal" />
            </dd>
        </dl>

        <dl>
            <dt>二级排序</dt>
            <dd>
                <asp:TextBox ID="txtSecondSort" runat="server" MaxLength="255" CssClass="input normal" />
            </dd>
        </dl>

    </div>

    <div id="field_tab_content" runat="server" visible="false" class="tab-content" style="display: none"></div>
    <!--/内容-->

    <!--工具栏-->
    <div class="page-footer">
        <div class="btn-list">
            <asp:Button ID="btnSubmit" runat="server" Text="提交保存" CssClass="btn" OnClick="btnSubmit_Click" />
            <input name="btnReturn" type="button" value="返回上一页" class="btn yellow" onclick="javascript: history.back(-1);" />
        </div>
        <div class="clear"></div>
    </div>
    <!--/工具栏-->
</asp:Content>
