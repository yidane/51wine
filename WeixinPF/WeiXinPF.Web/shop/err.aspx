<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="err.aspx.cs" Inherits="MxWeiXinPF.Web.shop.err" %>

<!doctype html>
<html>
<head id="Head1" runat="server">
    <title>异常信息提示</title>
    <meta content="text/html; charset=utf-8" http-equiv="Content-Type" />
    <meta content="width=device-width, initial-scale=1.0, minimum-scale=1.0, maximum-scale=1.0, user-scalable=no" name="viewport">
    <meta name="Keywords" content="" />
    <meta name="Description" content="" />
    <!-- Mobile Devices Support @begin -->
    <meta content="application/xhtml+xml;charset=UTF-8" http-equiv="Content-Type">
    <meta content="no-cache,must-revalidate" http-equiv="Cache-Control">
    <meta content="no-cache" http-equiv="pragma">
    <meta content="0" http-equiv="expires">
    <meta content="telephone=no, address=no" name="format-detection">
    <meta name="apple-mobile-web-app-capable" content="yes" />
    <!-- apple devices fullscreen -->
    <meta name="apple-mobile-web-app-status-bar-style" content="black-translucent" />
    <!-- Mobile Devices Support @end -->

    <script src="../../scripts/jquery/jquery-1.10.2.min.js"></script>
  <style>
      .mysnbody {
      
      
      }

  </style>
</head>
<body onselectstart="return true;" ondragstart="return false;" class="mysnbody">
    <form id="form1" runat="server">
        <br />
        <div style="text-align:center; color:white; font-size:20px;line-height:30px; font-weight:bolder;">
            信息提醒
        </div>
          <br />
        <div class="nodata">
            <asp:Literal ID="literr" runat="server"></asp:Literal>
        </div>
    </form>
</body>
</html>
