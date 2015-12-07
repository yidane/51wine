<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="gzReg_ret.aspx.cs" Inherits="WeiXinPF.Web.weixin.zhanhui.gzReg_ret" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <meta name="viewport" content="width=device-width,height=device-height,inital-scale=1.0,maximum-scale=1.0,user-scalable=no;" />
    <meta name="apple-mobile-web-app-capable" content="yes" />
    <meta name="apple-mobile-web-app-status-bar-style" content="black" />
    <meta name="format-detection" content="telephone=no" />
    <meta name="Author" content="Power by:JerryYan QQ:855222 JerrySoft Framework 1.1" />
    <style type="text/css">
        p {
            font-size: 13px;
            line-height: 20px;
        }

        h4 {
            margin: 10px 0px;
        }

        * {
            margin: 0;
            outline: 0;
            padding: 0;
            font-size: 100%;
            -webkit-tap-highlight-color: rgba(0, 0, 0, 0);
        }

        .c_back {
            width: 90%;
            margin: 0px auto;
        }

        .c_main {
            width: 100%;
            float: left;
            margin: 10px 0px;
        }

            .c_main > div {
                float: left;
                width: 50%;
                text-align: center;
            }

                .c_main > div > p {
                    font-size: 15px;
                    font-weight: bold;
                    margin: 5px 0px;    
                }
    </style>
</head>
<body>
    <img src="images/banner.jpg" width="100%" />
    <div class="c_back">
        <h4>尊敬的：<%=name %></h4>
        <p>您的预登记已成功，请打印此确认函，务必携带至展会现场</p>
        <div class="c_main">
            <div>
                <img src="images/gz.jpg" width="80%" />
            </div>
            <div>
                <p><%=name %></p>
                <p><%=pampany_name %></p>
                <p><%=email %></p>
                <img src="getdata.ashx?myact=getqrcode" />
            </div>
        </div>
        <p>感谢您注册我们的展会,您已经成功的进行了网上预登记。</p>
        <p>请打印此确认函并且务必携带至展会现场，您将可以在预登记观众通道直接获取“观众胸卡”。</p>
        <h4>注：</h4>
        <p>⑴展会仅对专业观众开放，观众入场请佩戴胸卡。</p>
        <p>⑵此信息必须真实有效，否则主办机构将视为无效登记。</p>
        <h4>展览时间:</h4>
        <p>2015年6月4日  ~6月6日</p>
        <p></p>

        <h4>展览地点 :</h4>
        <p>上海国际展览中心</p>
        <p>如果您需要更多的信息，请您及时与我们联系，我们希望能够在2015箱包手袋展上与您见面</p>
        <p>电话：<a href="tel:86-21-64827889">86-21-64827889</a></p>
        <p>电子邮件：shyhzl@163.com</p>
        <p>网址: <a href="http://www.cshbox.com">http://www.cshbox.com</a></p>
    </div>
</body>
</html>
