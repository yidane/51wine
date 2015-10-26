<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AfterPay.aspx.cs" Inherits="WeiXinPF.Web.weixin.restaurant.AfterPay" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <script src="js/shopCart.js" type="text/javascript"></script>
    <script type="text/javascript">
        var cart = new OAK.Shop.Cart(<%=shopid %>);
        <%=clearCache%>

        setTimeout(function () {
            document.location.href = "<%=newUrl%>";
        }, 500);

    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
        </div>
    </form>
</body>
</html>
