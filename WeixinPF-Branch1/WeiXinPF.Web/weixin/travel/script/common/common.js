/**
 * 公用方法
 */
var common = new function () {
    /**
	 * 提交url，完成后刷新页面
	 * message为提交的确认信息。
	 */
    this.doPost = function (url, message) {
        if (confirm(message)) {
            var url = url;
            var options = {};
            options.url = url;
            options.dataType = "text";
            options.success = function (text) {
                var obj = eval(text);
                alert(obj.message[0]);
                if (obj.flag == "success") {
                    location.reload();
                }
            };
            $.ajax(options);
        }
    }
    /**
	 * 提交url，完成后刷新页面,返回backUrl
	 * message为提交的确认信息。
	 */
    this.doPostBack = function (url, message, backUrl) {
        if (confirm(message)) {
            var url = url;
            var options = {};
            options.url = url;
            options.dataType = "text";
            options.success = function (text) {
                var obj = eval(text);
                alert(obj.message[0]);
                if (obj.flag == "success") {
                    window.location.href = backUrl;
                }
            };
            $.ajax(options);
        }
    }

}