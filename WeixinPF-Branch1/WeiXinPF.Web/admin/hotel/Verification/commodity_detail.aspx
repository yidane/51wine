<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="commodity_detail.aspx.cs" Inherits="WeiXinPF.Web.admin.hotel.Verification.commodity_detail" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>订单处理</title>
    <script type="text/javascript" src="../../../scripts/jquery/jquery-1.10.2.min.js"></script>
    <script type="text/javascript" src="../../../scripts/lhgdialog/lhgdialog.js?skin=idialog"></script>
    <script type="text/javascript" src="../../js/layout.js"></script>
       <link href="../../skin/default/style.css" rel="stylesheet" type="text/css" />
     <link href="../../skin/mystyle.css" rel="stylesheet" type="text/css" />
        <link href="../../../weixin/restaurant/css/diancai.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        function parentToIndex(id) {
            parent.location.href = "/admin/Index.aspx?id=" + id;

        }

        $(function () {


        });

    </script>
    <style>
       a.shenghe {
        color:red;
        
        }
    </style>
</head>
<body class="mainbody">
    <form id="form1" runat="server">
          <div class="location">
          <% if (IsWeiXinCode())
                {%>
            <a    href="../hotel_list.aspx" class="home"><i></i><span>商户或门店列表</span></a>
              <i class="arrow"></i>
                <%}%>
                <%
                else
                {%>
               
                <%} %> <span><a href="dingdan_confirm.aspx?shopid=<%=hotelid %>">服务码验证</a></span>
            <i class="arrow"></i>            
            <span>商品处理</span>
        </div>
      
        <!--/导航栏-->


        <div  class="tab-content"> 
       <ul class="round">
       <li class="title"><span class="none smallspan">商品详情</span></li>
       <table width="100%" border="0" cellpadding="0" cellspacing="0" class="cpbiaoge">
      
      <%=Dingdanlist %>
      </table>

      </ul>

       <ul class="round">
       <li class="title"><span class="none smallspan">订单信息</span></li>
        
     <table width="100%" border="0" cellpadding="0" cellspacing="0" class="cpbiaoge">
    <%=dingdanren %>
 </table>

</ul>  

</div>
           <div class="page-footer">
            <div class="btn-list">
                <asp:Button ID="save_groupbase" runat="server" CssClass="btn" Text="确认核销" OnClick="save_groupbase_Click"  />
            </div>
            <div class="clear"></div>
        </div>
    </form>
</body>
</html>