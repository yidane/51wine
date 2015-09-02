<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="xiaoshouMgr.aspx.cs" Inherits="MxWeiXinPF.Web.weixin.wqiche.xiaoshouMgr" %>


<!DOCTYPE html>
<html>
<head>
    <meta charset="utf-8" />
    <meta content="no-cache,must-revalidate" http-equiv="Cache-Control">
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8">
    <meta name="viewport" content="width=device-width,height=device-height,inital-scale=1.0,maximum-scale=1.0,user-scalable=no;">
    <meta name="apple-mobile-web-app-capable" content="yes">
    <meta name="apple-mobile-web-app-status-bar-style" content="black">
    <meta name="format-detection" content="telephone=no">
    <link rel="stylesheet" type="text/css" href="css/reset.css" media="all" />
    <link rel="stylesheet" type="text/css" href="css/common.css" media="all" />
    <script type="text/javascript" src="js/jquery152_min.js"></script>
    <title>销售管理</title>
    <style>
        img {
            width: 100%!important;
        }
    </style>
</head>
<body onselectstart="return true;" ondragstart="return false;">
    <body onselectstart="return true;" ondragstart="return false;">
        <style type="text/css">
            #book_new, #book_list {
                display: none;
            }

                #book_new.on, #book_list.on {
                    display: inherit;
                }

            .list_book {
                border-radius: 0;
                border: 0;
                background: #f2f4f3;
            }

                .list_book dt {
                    background: #fff;
                    line-height: 35px;
                    text-align: center;
                }

                    .list_book dt div:first-of-type {
                        margin-right: 10px;
                    }

                    .list_book dt div a {
                        display: block;
                        height: 100%;
                        background: #777777;
                        color: #fff;
                    }

            .book_list {
                border: 0!important;
            }

            #nav_contact {
                border: 5px 5px 0 0;
                overflow: hidden;
            }

                #nav_contact a {
                    color: #000;
                    display: block;
                    text-align: center;
                }

                #nav_contact div:first-of-type {
                    margin-right: 10px;
                }

                #nav_contact div {
                    line-height: 35px;
                    border-radius: 5px 5px 0 0;
                    background: #f2f4f3;
                    overflow: hidden;
                }

                    #nav_contact div a.on {
                        color: #fff;
                        background-color: #777777;
                    }
        </style>
        <div class="body">
            <section class="p_10">
                <nav id="nav_contact">
                    <dt class="box">
                        <div><a id="book_list_a" href="#" class="on">售前/售后客服</a></div>
                    </dt>
                </nav>
                <div id="book_new" class="on">
                    <dl class="list_book">
                        <ul class="list_contact">
                            <asp:Repeater ID="rptList" runat="server">
                                <ItemTemplate>
                                    <li class="tbox">
                                        <div>
                                            <img src="<%#Eval("headpic") %>" style="width: 60px!important;" />
                                        </div>
                                        <div>
                                            <p><%#Eval("Name") %></p>
                                            <p><a style="padding: 1px;" href="tel:<%#Eval("telephone") %>"><%#Eval("xsType") %></a></p>
                                            <div><%#Eval("remark") %></div>
                                            <p><a href="tel:<%#Eval("telephone") %>"><%#Eval("telephone") %></a></p>
                                        </div>
                                    </li>
                                </ItemTemplate>
                            </asp:Repeater>
                        </ul>
                    </dl>
                </div>
            </section>
            <p>
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
            </p>
        </div>
<%--        <span class="copyright">版权归我所有        </span>--%>
    </body>
    <div mark="stat_code" style="width: 0px; height: 0px; display: none;">
    </div>
</body>
</html>
