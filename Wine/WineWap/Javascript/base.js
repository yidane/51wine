(function ($) {
    $.fn.ad_data = function (params) {
        var url = params.url;
        this.each(function (index, e) {
            var id = $(e).attr("data");
            $(e).html("加载中...");
            $.get(url, { id: id }, function (data) {
                var img_str = "<img title='" + data.result.ad_title + "' src='" + data.result.ad_img + "' />";
                $(e).attr("href", data.result.ad_link).html(img_str);
            }, "JSON");
        });
    };
})(jQuery);

function getQueryString(name) {
    var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)", "i");
    var r = window.location.search.substr(1).match(reg);
    if (r != null) return unescape(r[2]); return null;
}