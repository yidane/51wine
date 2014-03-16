<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Test.aspx.cs" Inherits="WineWeb.Test" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <fieldset>
            <legend>菜单接口</legend>
            <asp:Button ID="btnCreateMenu" runat="server" Text="测试创建菜单" OnClick="btnCreateMenu_Click" />
            <asp:Button ID="btnDeleteMenu" runat="server" Text="测试删除菜单" OnClick="btnDeleteMenu_Click" />
            <asp:Button ID="btnSearchMenu" runat="server" Text="测试查询菜单" OnClick="btnSearchMenu_Click" />
        </fieldset>
        <fieldset>
            <legend>用户组接口</legend>

        </fieldset>
    </form>
</body>
</html>
