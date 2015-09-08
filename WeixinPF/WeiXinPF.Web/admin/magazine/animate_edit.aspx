<html>
<head>
    <title>使用鼠标拖动图片</title>
    <script language="javascript">
<!--
    down = false;
    var x, y, imgID;
    function dragImage(obj) {
        imgID = obj;
        x = event.x - parseInt(imgID.style.left);
        y = event.y - parseInt(imgID.style.top);
        down = true;
    }
    function cancelDrag() { down = false; }
    function moveImage() {
        if (down) {
            imgID.style.left = event.x - x;
            imgID.style.top = event.y - y;
            event.returnValue = false;
        }
    }
    document.onmousemove = moveImage;
    document.onmouseup = cancelDrag;
    //-->
    </script>
</head>
<body>
    <div>
        <img src="http://www.veryhuo.com/images/logo.gif" style="position: absolute; left: 0px; top: 0px"
            onmousedown="dragImage(this)">
    </div>
    <br />
    <center>如不能显示效果，请按Ctrl+F5刷新本页，更多网页代码：<a href='http://www.veryhuo.com/' target='_blank'>http://www.veryhuo.com/</a></center>

</body>
</html>
