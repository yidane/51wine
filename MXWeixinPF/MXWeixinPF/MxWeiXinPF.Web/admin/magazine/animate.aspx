<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="animate.aspx.cs" Inherits="MxWeiXinPF.Web.admin.magazine.animate" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>HTML5截图功能 可拖拽图片DEMO演示</title>
    <link href="../skin/default/style.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" href="css/demo.css" />
    <!--必要样式-->
    <link rel="stylesheet" type="text/css" href="css/component.css" />
    <script type="text/javascript">
        //窗口API
        var api = frameElement.api, W = api.opener;
        api.button({
            name: '确定',
            focus: true,
            callback: function () {
            }
        }, {
            name: '取消'
        });
    </script>
</head>
<body>
    <form>
        <input type="file" id="newUpl" style="opacity: 0; position: absolute; top: -100px" accept="image/jpeg, image/jpg, image/png" />
        <input type="file" id="upload" style="opacity: 0; position: absolute; top: -100px" accept="image/jpeg, image/jpg, image/png" />
        <input type="hidden" value="<%=iid %>" id="g_iid" />
        <div class="content">
            <div class="component">
                <img src="" width="100%" id="mzimg" />
                <%--                <img class="resize-image" src="img/image.jpg" alt="动画图片" id="i_11" />
                <img class="resize-image" src="img/Hydrangeas.jpg" alt="动画图片" id="i_12" />--%>
            </div>
            <div class="animate_dl">
                <%--<dl id="11">
                    <dt>
                        <img src="img/image.jpg" width="90%" />
                        <span class="tm_bz" style="width: 90%;">重新上传</span>
                    </dt>
                    <dd>
                        <span class="Validform_checktip">宽</span>
                        <input type="text" class="input small am_width" />
                        <span class="Validform_checktip">高</span>
                        <input type="text" class="input small am_height" />
                    </dd>
                    <dd>
                        <span class="Validform_checktip">横向位置：</span>
                        <input type="text" class="input small am_x" />
                    </dd>
                    <dd>
                        <span class="Validform_checktip">纵向位置：</span>
                        <input type="text" class="input small am_y" />
                    </dd>
                    <dd>
                        <span class="Validform_checktip">动画效果：</span>
                        <select>
                            <option value="none">无</option>
                            <option value="dr">淡入</option>
                            <option value="xz">下坠</option>
                            <option value="sf">上浮</option>
                            <option value="zr">左入</option>
                            <option value="yr">右入</option>
                        </select>
                    </dd>
                    <dd>
                        <span class="Validform_checktip">开始时间：</span>
                        <input type="text" class="input small" />
                        <span class="Validform_checktip">秒</span>
                    </dd>
                    <dd>
                        <span class="Validform_checktip">动画持续：</span>
                        <input type="text" class="input small" />
                        <span class="Validform_checktip">秒</span>
                    </dd>
                    <dd class="rem_dl">×</dd>
                </dl>--%>
                <div class="addEle">
                    <img src="img/icon-add.png" style="vertical-align: middle;" />添加元素
                </div>
            </div>
        </div>
    </form>
    <!-- /content -->
    <script type="text/javascript" src="js/jquery-2.1.1.min.js"></script>
    <script src="js/helper_min.js"></script>
    <script type="text/javascript" charset="gb2312" src="js/component.js?v=333"></script>
</body>
</html>
