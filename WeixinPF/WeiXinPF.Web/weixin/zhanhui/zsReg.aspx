<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="zsReg.aspx.cs" Inherits="WeiXinPF.Web.weixin.zhanhui.zsReg" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1">
    <link href="style/selfform.css" rel="stylesheet" type="text/css" />
    <title>展商登记</title>
    <meta name="viewport" content="width=device-width,height=device-height,inital-scale=1.0,maximum-scale=1.0,user-scalable=no;" />
    <meta name="apple-mobile-web-app-capable" content="yes" />
    <meta name="apple-mobile-web-app-status-bar-style" content="black" />
    <meta name="format-detection" content="telephone=no" />
    <style type="text/css">
        .deploy_ctype_tip {
            z-index: 1001;
            width: 100%;
            text-align: center;
            position: fixed;
            top: 50%;
            margin-top: -23px;
            left: 0;
        }

            .deploy_ctype_tip p {
                display: inline-block;
                padding: 13px 24px;
                border: solid #d6d482 1px;
                background: #f5f4c5;
                font-size: 16px;
                color: #8f772f;
                line-height: 18px;
                border-radius: 3px;
            }

        .cj {
            float: right;
            display: block;
            width: 90px;
            margin-right: 10px;
            background: #ffac12;
            text-align: center;
            -webkit-border-radius: 5px;
            -moz-border-radius: 5px;
            border-radius: 5px;
            color: #8c0500;
        }

            .cj a {
                color: #8c0500;
                font-size: 16px;
                font-family: "黑体";
                text-decoration: none;
            }

        #zs_reg {
            -webkit-appearance: none;
            outline: medium;
            border: 0px;
            width: 30%;
            height: 30px;
            font-weight: bold;
            font-family: 微软雅黑;
            font-size: 16px;
            text-align: center;
            background-color: red;
            color: white;
        }
    </style>
    <meta name="Author" content="Power by:JerryYan QQ:855222 JerrySoft Framework 1.1" />
    <script src="../../scripts/jquery/jquery-2.1.0.min.js"></script>
    <script type="text/javascript">
        $(function () {

            //注册观众信息
            $("#zs_reg").click(function () {
                var $this = $(this);
                var allinput = $("input[isnull=false]");
                var i_len = 0;
                allinput.each(function (i, ob) {
                    if ($(ob).val() != '')
                        i_len++;
                });
                if (i_len < allinput.size()) {
                    alert("请填写完整信息！");
                    return;
                }
                var req = {
                    tel: $("#tel").val(),
                    fax: $("#fax").val(),
                    mobile: $("#mobile").val(),
                    linkman: $("#linkman").val(),
                    company: $("#company").val(),
                    email: $("#email").val(),
                    qq: $("#qq").val(),
                    area: $("#area").val(),
                    type: $("#type").val(),
                };
                $this.prop("disabled", false);
                $.post("getdata.ashx?myact=addZs&v=" + Math.random(), req, function (data) {
                    if (data.res != 1) {
                        alert("添加错误！");
                        return;
                    }
                    location.href = "zsReg_ret.aspx?zsid=" + data.content;
                }, "json");

            });

        });
    </script>
</head>
<body>
    <div class="banner">
        <div id="wrapper">
            <div id="scroller" style="float: none">
                <ul id="thelist">
                    <li style="float: none; list-style: none;">
                        <img src="images/banner.jpg" alt="参观预登记" style="width: 100%">
                    </li>
                </ul>
            </div>
        </div>
        <div class="clr">
        </div>
    </div>
    <h3 style="margin-left: 10px; text-align: left; margin-top: 20px; font-size: 18px;">2015第十二届上海国际箱包皮具手袋展览会</h3>
    <%--<div class="tsy" style="text-align: left; color: #298bd4">
        登记成功！请凭以下卡号或手机号到展会现场办理参观胸卡。
    </div>--%>
    <div class="cardexplain">
        <!--intro-->
        <form method="post" id="form" onsubmit="return tgSubmit()">
            <ul class="round" style="list-style: none;">
                <li class="nob" style="padding-right: 5px;">
                    <table width="100%" border="0" cellspacing="0" cellpadding="0" class="kuang">
                        <tbody>
                            <tr>
                                <td width="18%" height="40" id="padding_left">联系人
                                </td>
                                <td width="84%" class="align_left">
                                    <input type="text" value="" id="linkman" isnull="false" />
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </li>
                <li class="nob" style="padding-right: 5px;">
                    <table width="100%" border="0" cellspacing="0" cellpadding="0" class="kuang">
                        <tbody>
                            <tr class="line">
                                <td height="30" width="17%">公司
                                </td>
                                <td width="83%" class="align_left">
                                    <input type="text" value="" isnull="false" id="company" />
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </li>
                <li class="nob" style="padding-right: 5px;">
                    <table width="100%" border="0" cellspacing="0" cellpadding="0" class="kuang">
                        <tbody>
                            <tr class="line">
                                <td width="18%" height="30">展品类别
                                </td>
                                <td width="84%" class="align_left">
                                    <input type="text" value="" id="type" isnull="false" />
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </li>
                <li class="nob" style="padding-right: 5px;">
                    <table width="100%" border="0" cellspacing="0" cellpadding="0" class="kuang">
                        <tbody>
                            <tr class="line">
                                <td width="18%" height="30">申请面积
                                </td>
                                <td width="84%" class="align_left">
                                    <select id="area">
                                        <option value="室内光地（36m2起租）平方">1室内光地（36m2起租）平方</option>
                                        <option value="标改展位（3m×3m）个">标改展位（3m×3m）个</option>
                                    </select>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </li>
                <li class="nob" style="padding-right: 5px;">
                    <table width="100%" border="0" cellspacing="0" cellpadding="0" class="kuang">
                        <tbody>
                            <tr class="line">
                                <td width="18%" height="30">手机
                                </td>
                                <td width="84%" class="align_left">
                                    <input type="text" value="" id="mobile" isnull="false" />
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </li>
                <li class="nob" style="padding-right: 5px;">
                    <table width="100%" border="0" cellspacing="0" cellpadding="0" class="kuang">
                        <tbody>
                            <tr class="line">
                                <td width="18%" height="30">电话
                                </td>
                                <td width="84%" class="align_left">
                                    <input type="text" value="" id="tel" isnull="false" />
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </li>
                <li class="nob" style="padding-right: 5px;">
                    <table width="100%" border="0" cellspacing="0" cellpadding="0" class="kuang">
                        <tbody>
                            <tr class="line">
                                <td width="18%" height="30">邮箱
                                </td>
                                <td width="84%" class="align_left">
                                    <input type="text" value="" id="email" isnull="false" />
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </li>

                <li class="nob" style="padding-right: 5px;">
                    <table width="100%" border="0" cellspacing="0" cellpadding="0" class="kuang">
                        <tbody>
                            <tr class="line">
                                <td width="18%" height="30">传真
                                </td>
                                <td width="84%" class="align_left">
                                    <input type="text" value="" id="fax" isnull="false" />
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </li>

                <li class="nob" style="padding-right: 5px;">
                    <table width="100%" border="0" cellspacing="0" cellpadding="0" class="kuang">
                        <tbody>
                            <tr class="line">
                                <td width="18%" height="30">QQ
                                </td>
                                <td width="84%" class="align_left">
                                    <input type="text" value="" id="qq" isnull="false" />
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </li>

                <li class="nob" style="padding-right: 5px;">
                    <table width="100%" border="0" cellspacing="0" cellpadding="0" class="kuang">
                        <tbody>
                            <tr class="line">
                                <td height="30" width="32%" style="border-bottom: none; text-align: center">
                                    <input id="zs_reg" type="button" value="展商登记" />
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </li>
            </ul>
        </form>
    </div>
</body>
</html>
