<%@ Page Language="C#" AutoEventWireup="true" Async="true" CodeBehind="LaunchCommands.aspx.cs" Inherits="LaunchCommands" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Button runat="server" ID="btnCheckRestMoney" Text="查询余额" OnClick="btnCheckRestMoney_Click"/>
                
    
    </div>
        <div>        
            <asp:Button runat="server" ID="btnRestaurantCategory" Text="商品类型" OnClick="btnRestaurantCategory_Click"/>
            <asp:TextBox runat="server" ID="txtCategoryName" ></asp:TextBox>
    </div>
    </form>
</body>
</html>
