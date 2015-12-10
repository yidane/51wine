(function ($) {
    $.fn.extend({
        tzSelect: function (options) {
            return this.each(function (i) {
                var selet = $(this);
                var seletWidth = selet.width() > 120 ? selet.width() : 120;
                selet.closest("div").each(function () {
                    if ($(this).hasClass("none") || $(this).attr("display") == "none") {
                        $(this).show();
                        seletWidth = selet.width();
                        $(this).hide();
                    }
                });
                var inputtext = $("<input>", {
                    width: seletWidth - 45,
                    type: "text",
                    readonly: "readonly",
                    relval: ""
                }).addClass("text");
                var inputbut = $("<span>", {
                    "class": "arrow arrow-bottom select-btn"
                });
                var selectBoxContainer = $("<div>", {
                    width: seletWidth
                }).addClass("selectBoxContainer");
                var selectTable = $("<table>", {
                    width: seletWidth,
                    cellspacing: "0",
                    cellpadding: "0",
                    html: "<tr><td valign='middle'></td><td valign='middle' class='left-line'></td></tr>"
                }).addClass("select-table");
                var dropDown = $("<ul>", {
                    //"width": seletWidth+30
                }).addClass("dropDown ofx");
                //构造ul下拉列表
                function makeDropDown() {
                    var li = "";
                    selet.find("option").each(function (i) {
                        var option = $(this);
                        if (option.val() == "") {
                            li += "<li value=''";
                        }
                        else {
                            li += "<li value=" + option.val();
                        }
                        if (i == selet.get(0).selectedIndex) {
                            li += " class='selected'";
                            inputtext.val(option.text()).attr("relval", $(this).val());
                            selet.attr({
                                "relText": option.text(),
                                "relValue": $(this).val()
                            });
                        }
                        li += ">" + option.text() + "</li>";
                    });
                    return li;
                }
                selectTable.find("td:first").append(inputtext).end().find("td:eq(1)").append(inputbut).end().appendTo(selectBoxContainer);
                selet.css({
                    "position": "absolute",
                    "zIndex": "-1",
                    "top": "-99999px"
                }).before(selectBoxContainer);
                dropDown.append(makeDropDown());

                var selectBoxContainerIndex = selet.siblings(".selectBoxContainer").length;
                if (selectBoxContainerIndex > 1) {
                    selet.siblings(".selectBoxContainer:first").remove();
                }

                dropDown.find("li").mousedown(function (e) {
                    stopPropagation(e);
                });
                dropDown.mousedown(function (e) {
                    stopPropagation(e);
                });
                selectBoxContainer.mousedown(function (e) {
                    stopPropagation(e);
                });
                dropDown.find("li").click(function () {
                    inputtext.val($(this).text()).attr("relval", $(this).attr("value"));
                    selet.attr({
                        "relText": $(this).text(),
                        "relValue": $(this).attr("value")
                    });
                    $(this).addClass('selected').siblings().removeClass('selected');
                    selet.get(0).options[$(this).index()].selected = true;
                    selet.change();
                    //alert($(this).index());
                    dropDown.hide();
                    //selet.val($(this).val());
                    return false;
                });

                dropDown.bind('show', function () {
                    if (dropDown.is(':animated')) {
                        return false;
                    }
                    selectBox.addClass('expanded');
                    dropDown.slideDown("fast");
                }).bind('hide', function () {
                    if (dropDown.is(':animated')) {
                        return false;
                    }
                    selectBox.removeClass('expanded');
                    dropDown.slideUp("fast");
                }).bind('toggle', function () {
                    if (selectBox.hasClass('expanded')) {
                        dropDown.trigger('hide');
                    } else dropDown.trigger('show');
                });

                selectTable.click(function () {
                    var parentTop = 0,
                        parentLeft = 0;
                    if (window.top == window.self) {
                        $(".dropDown").hide();
                        dropDown.appendTo(selectBoxContainer);
                    } else {
                        var parentPage = window.parent;
                        var win = window;
                        while (parentPage != win) {
                            $("iframe", parentPage.document).each(function () {
                                if (win.document.body.ownerDocument === this.contentWindow.document) {
                                    parentTop += $(this).offset().top;
                                    parentLeft += $(this).offset().left;
                                }
                            });
                            parentPage = parentPage.parent;
                            win = win.parent;
                        }
                        var eventTop = $(this).offset().top,
                            eventLeft = $(this).offset().left;
                        dropDown.css({
                            top: eventTop + parentTop + selectTable.outerHeight(true) + "px",
                            left: eventLeft + parentLeft + 'px',
                            position: "absolute"
                        });
                        $(window.top.document).find(".dropDown").hide();
                        $(window.top.document).find("body").append(dropDown);
                    }
                    var dropDownDisplay = dropDown.css("display");
                    if (dropDownDisplay == "none") {
                        dropDown.show();
                        dropDown.css("zIndex", 10001);
                        selectBoxContainer.css("zIndex", 10001);
                    } else {
                        dropDown.hide();
                        dropDown.css("zIndex", 10);
                        selectBoxContainer.css("zIndex", 10);
                    }
                    var dropDownWidth = seletWidth > dropDown.find("li").outerWidth(true) ? seletWidth : dropDown.find("li").outerWidth(true);
                    dropDown.width(dropDownWidth);
                    return false;
                });

                $(document).mousedown(function () {
                    dropDownClose();
                });
                $(window.top.document).mousedown(function () {
                    dropDownClose();
                });
                $(window).unload(function () {
                    dropDown.remove();
                });

                function dropDownClose() {
                    dropDown.hide();
                    dropDown.css("zIndex", 10);
                    selectBoxContainer.css("zIndex", 10);
                }

                function stopPropagation(e) {
                    e = e || window.event;
                    if (e.stopPropagation) { //W3C阻止冒泡方法  
                        e.stopPropagation();
                    } else {
                        e.cancelBubble = true; //IE阻止冒泡方法  
                    }
                }
            });
        },
        calendarInput: function (options) {
            return this.each(function () {
                var calendar = $(this);
                var calendarBox = $("<div>", {
                    "class": "calendarBox"
                });
                var calendarTable = $("<table>", {
                    width: "100%",
                    cellspacing: "0",
                    cellpadding: "0",
                    html: "<tr><td valign='middle'></td><td valign='middle' style='width:50px;'></td><td valign='middle' class='left-line'></td></tr>"
                });
                var calendarBtn = $("<span>", {
                    "class": "calendarBtn"
                });
                calendarBox.bind("click", function () {
                    var calendarId = calendar.attr("id");
                    var option1 = calendar.attr("data-options");
                    option1 = eval("(" + option1 + ")");
                    var option = {
                        el: calendarId,
                        onpicked: function (dp) {
                            dp.hide();
                            calendarTable.find("td:eq(1)").html(dp.cal.getNewP('D'));
                        },
                        onclearing: function () {
                            calendarTable.find("td:eq(1)").html('');
                        }
                    };
                    option = $.extend(option1, option);
                    WdatePicker(option);
                    return false;
                });
                calendar.after(calendarBox);
                calendarBox.append(calendarTable);
                calendarTable.find("td:eq(0)").append(calendar).end().find("td:eq(2)").append(calendarBtn);
            });
        },
        numSpinner: function (options) {
            var defaults = {
                increment: 1,
                min: null,
                max: null,
                onChange: function (value) { }
            }
            var opts = $.extend(defaults, options);
            return this.each(function () {
                var $this = $(this);
                var contentBox = $("<div>", {
                    "class": "numSpinnerBox"
                });
                var arrowBox = $("<span>", {
                    "class": "arrowBox"
                });
                var arrowup = $("<a>", {
                    "class": "arrow arrow-top",
                    "href": "javascript:void(0);"
                });
                var arrowdown = $("<a>", {
                    "class": "arrow arrow-bottom",
                    "href": "javascript:void(0);"
                });
                $this.after(contentBox);
                arrowBox.append(arrowup, arrowdown);
                contentBox.append($this, arrowBox);
                $this.val($this.val() || 0);
                arrowup.click(function () {
                    $this.val($this.val() - opts.increment);
                    opts.onChange($this.val());
                });
                arrowdown.click(function () {
                    $this.val(parseInt($this.val()) + opts.increment);
                    opts.onChange($this.val());
                });
                $this.blur(function () {
                    var re = /^(-?\d+)(\.\d+)?$/;
                    if (!re.test($this.val())) {
                        $this.val(0);
                    }
                });
            });
        },
        switchBox: function (options) {
            var defaults = {
                onText: "ON",
                offText: "OFF",
                onChange: function (value) { }
            }
            var opts = $.extend(defaults, options);
            return this.each(function () {
                var $this = $(this);
                var switchItem = function () {
                    var switchText = $("<span>", {
                        "class": "switch-text"
                    });
                    var switchBtn = $("<span>", {
                        "class": "switch-btn"
                    });
                    return [switchText, switchBtn];
                }
                var switchOn = $("<div>", {
                    "class": "switch-on"
                }).append(switchItem()[0], switchItem()[1]);
                var switchOff = $("<div>", {
                    "class": "switch-off"
                }).append(switchItem()[0], switchItem()[1]);
                var switchBox = $("<div>", {
                    "class": "switch-panel"
                }).append(switchOn, switchOff);
                var $thisChecked = $this.attr("checked"),
                    ischecked = "";
                if ($this.prev().hasClass('switch-panel')) {
                    $this.prev().remove();
                }
                $this.hide().before(switchBox);
                if (typeof ($thisChecked) == "undefined" || !$thisChecked) {
                    switchOn.addClass('switch-off').find("span:first").text(opts.offText).end().find("span:last").addClass('switch-btn-off');
                    ischecked = false;
                } else {
                    switchOn.removeClass('switch-off').find("span:first").text(opts.onText).end().find("span:last").removeClass('switch-btn-off');
                    ischecked = true;
                }
                //switchOn.find("span:first").text(opts.onText);

                switchOn.find("span:last").click(function () {
                    if (!ischecked) {
                        $this.attr("checked");
                        switchOn.removeClass('switch-off').find("span:first").text(opts.onText).end().find("span:last").removeClass('switch-btn-off');
                        ischecked = true;
                    } else {
                        $this.removeAttr("checked");
                        switchOn.addClass('switch-off').find("span:first").text(opts.offText).end().find("span:last").addClass('switch-btn-off');
                        ischecked = false;
                    }
                    opts.onChange(ischecked);
                });
            });
        },
        radioBox: function (options) {
            return this.each(function () {
                var $this = $(this),
                    checked = $this.attr("checked"),
                    name = $(this).attr("name");
                var radioBox = $("<span>", {
                    "class": "radio-box"
                });
                if (typeof (checked) != "undefined") {
                    radioBox.addClass('radio-box-checked');
                }
                $this.hide().after(radioBox);
                radioBox.click(function () {
                    var radioNum = $("input[name='" + name + "']").length;
                    if (radioNum == 0 || radioNum == 1) {
                        if (typeof (checked) == "undefined" || !checked) {
                            $(this).addClass('radio-box-checked');
                            checked = true;
                        } else {
                            $(this).removeClass('radio-box-checked');
                            checked = false;
                        }
                    } else {
                        $("input[name='" + name + "']").next("span").each(function () {
                            $(this).removeClass('radio-box-checked');
                        });
                        $(this).addClass('radio-box-checked');
                    }
                    $this.click();
                });
            });
        },
        checkBox: function (options) {
            return this.each(function () {
                var $this = $(this),
                    checked = $this.attr("checked"),
                    ischeck = typeof (checked) == "undefined" ? false : true;
                var isCheckSpan = $this.parent().hasClass('check-box');
                var checkBox = $("<span>", {
                    "class": "check-box"
                }).on("click", function () {
                    //$this.click();
                    if ($this.is(':checked')) {
                        $(this).addClass('check-box-checked');
                    } else {
                        $(this).removeClass('check-box-checked');
                    }
                });

                if (isCheckSpan) {
                    $this.parent().after($this);
                    $this.prev().remove();
                }
                if (ischeck) {
                    checkBox.addClass('check-box-checked');
                }
                $this.after(checkBox);
                checkBox.append($this);
            });
        },
        upload: function (options) {
            var defaults = {
                url: "",
                data: "",
                btnTitle: "+图片上传"
            }
            var opts = $.extend({}, defaults, options);

            return this.each(function () {
                var $this = $(this),
                    uploadBox = $("<span>").addClass('upload-box'),
                    uploadBtn = $("<span>").addClass('upload-btn').text(opts.btnTitle).appendTo(uploadBox),
                    enterBtn = $("<span>").addClass('upload-enter-btn').text("上传").click(function () {
                        if ($this.val() == "") {
                            alert("请选择上传文件！");
                            return false;
                        }
                        jQuery.ajaxFileUpload({
                            url: opts.url,
                            secureuri: false,
                            fileElementId: $this.attr("id"),
                            dataType: 'json',
                            data: opts.data,
                            success: function (data, status) {
                                if (typeof (data.error) != 'undefined') {
                                    //出现异常
                                    if (data.error != '') {
                                        alert(data.error);
                                    }
                                        //上传成功
                                    else if (data.msg != '') {
                                        alert("上传成功！");
                                    }
                                }
                            },
                            error: function (data, status, e) {
                                alert(e);
                            }
                        });
                    });
                $this.after(uploadBox);
                uploadBox.append($this).after(enterBtn);
            });
        }
    });
})(jQuery)