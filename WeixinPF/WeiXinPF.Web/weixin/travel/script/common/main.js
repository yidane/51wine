$(function () {
    numSpinner.init();
    tabs.init("tab");
    $("input[data='submit']").click(function () {
        $(this).attr("disabled", "disabled").val("正在提交...");
        if ($(this).hasClass('ok-btn')) {
            $(this).addClass("ok-btn-disabled");
        }
        if ($(this).hasClass('form-btn')) {
            $(this).addClass("form-btn-disabled");
        }
    });
    // 轮播
    if ($('#home_slider').size() > 0) {
        $('#home_slider').flexslider({
            animation: 'slide',
            controlNav: true,
            directionNav: true,
            animationLoop: true,
            slideshow: false,
            useCSS: false,
            slideshow: true,
            slideshowSpeed: 3000
        });
    }
});

var numSpinner = {
    value: function (sums) {
        var numBox = $("#numSpinner").find("input"), unitPrice = $("#price").find("strong").text(), val = 1;
        switch (sums) {
            case "add": val = parseInt(numBox.val()) + 1; break;
            case "sub": val = parseInt(numBox.val()) - 1; break;
            default: val = numBox.val();
        }
        return [val, unitPrice]
    },
    count: function (sums) {
        var value = this.value(sums);
        var totalPrice = this.floatMul(parseFloat(value[1]), parseFloat(value[0]));
        if (value[0] > 0) {
            $("#numSpinner").find("input").val(value[0]);
            $("#totalPrice").html("<i>￥</i>" + this.toDecimal2(totalPrice));
        }
    },
    add: function () {
        $("#numSpinner").find(".add").click(function () {

            numSpinner.count("add");
        });
    },
    sub: function () {
        $("#numSpinner").find(".sub").click(function () {
            numSpinner.count("sub");
        });
    },
    input: function () {
        $("#numSpinner").find("input").blur(function () {
            numSpinner.count();
        }).bind('keyup', function () {
            var val = $(this).val().replace(/[^\d]/g, '');
            $(this).val(val);
        });
    },
    floatMul: function (arg1, arg2) {
        var m = 0, s1 = arg1.toString(), s2 = arg2.toString();
        try { m += s1.split(".")[1].length } catch (e) { };
        try { m += s2.split(".")[1].length } catch (e) { };
        return Number(s1.replace(".", "")) * Number(s2.replace(".", "")) / Math.pow(10, m);
    },
    toDecimal2: function (x) {
        var f = parseFloat(x);
        if (isNaN(f)) {
            return false;
        }
        var f = Math.round(x * 100) / 100;
        var s = f.toString();
        var rs = s.indexOf('.');
        if (rs < 0) {
            rs = s.length;
            s += '.';
        }
        while (s.length <= rs + 2) {
            s += '0';
        }
        return s;
    },
    init: function () {
        this.add();
        this.sub();
        this.input();
        this.count();
    }
};

var tabs = {
    init: function (divid) {
        $("#" + divid).find("a").click(function (e) {
            var itmeId = $(this).attr("href");
            if (itmeId.substr(0, 1) == "#") {
                e.preventDefault();
            }
            $(itmeId).addClass('active').siblings().removeClass("active");
            $(this).parent().addClass('active').siblings().removeClass("active");
        });
    }
};