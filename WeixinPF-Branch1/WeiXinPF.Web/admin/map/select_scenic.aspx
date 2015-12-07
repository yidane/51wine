<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="select_scenic.aspx.cs" Inherits="WeiXinPF.Web.admin.map.select_scenic" %>

<%@ Import Namespace="WeiXinPF.Common" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <style type="text/css">
        * {
            margin: 0;
            padding: 0;
        }

        ul, li {
            list-style: none;
        }

        .scenic-list {
            padding: 20px;
            overflow: hidden;
        }

            .scenic-list li {
                float: left;
                width: 100px;
                height: 140px;
                padding: 10px;
                margin-left: 10px;
                margin-bottom: 10px;
                text-align: center;
                cursor: pointer;
            }

                .scenic-list li.selected, .scenic-list li:hover {
                    outline: solid 2px #0094ff;
                }

                .scenic-list li img {
                    border-radius: 50%;
                    width: 100px;
                    height: 100px;
                }

                .scenic-list li span {
                    display: block;
                    width: 100px;
                    height: 30px;
                    text-align: center;
                    line-height: 30px;
                }
    </style>
    <script type="text/javascript" src="../../scripts/jquery/jquery-1.10.2.min.js"></script>
    <script type="text/javascript" src="../../scripts/lhgdialog/lhgdialog.js?skin=idialog"></script>
    <script type="text/javascript" src="../../scripts/jquery/jquery.query.js"></script>
    <script type="text/javascript">
        //窗口API
        var api = frameElement.api, W = api.opener;
        api.button({
            name: '确定',
            focus: true,
            callback: function () {
                selectScenic();
                return false;
            }
        }, {
            name: '取消'
        });
        $(function () {
            $(".scenic-list").on("click", "li", function () {
                $(".scenic-list").find("li").removeClass("selected");
                $(this).addClass("selected");
            });
        });

        function selectScenic() {
            var selectedScenic = $(".scenic-list").find("li.selected");
            if (selectedScenic.length === 0) {
                return false;
            }

            var txt = $.query.get("txt");
            $("#" + txt, W.document).val(selectedScenic.data("url"));

            api.close();
            return true;
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <asp:Repeater ID="rpScenic" runat="server">
            <HeaderTemplate>
                <ul class="scenic-list">
            </HeaderTemplate>
            <ItemTemplate>
                <li data-url="<%# string.Format("{0}/weixin/scenic/detail.aspx?id={1}",MyCommFun.getWebSite(), Eval("Id")) %>">
                    <img src="<%#Eval("Cover") %>" />
                    <span><%#Eval("Name") %></span>
                </li>
            </ItemTemplate>
            <FooterTemplate>
                </ul>
            </FooterTemplate>
        </asp:Repeater>
    </form>
</body>
</html>
