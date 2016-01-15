var VI = {
    width: window.innerWidth < 320 ? 320 : window.innerWidth,
    height: window.innerHeight,
    regPhone: /^1[3|4|5|8|9][0-9]\d{8}$/,
};

VI.crs = function () {
    VI.src();
};

VI.src = function () {
    $('.crs').each(function (i, o) {
        var zThis = $(o);

        if (zThis.attr('resize') && zThis.attr('resize') != "") {
            var wh = zThis.attr('resize').split("x"),
                wOrig = parseInt(wh[0]),
                hOrig = parseInt(wh[1]);

            var zParent = zThis.parents('.resize-wrap'),
                hWrap = zParent.height(),
                wWrap = zParent.width();

            if (wOrig / wWrap > hOrig / hWrap) {
                zThis.height(hWrap + 'px');
            } else {
                zThis.width(wWrap + 'px');
            }
        }

        zThis.attr('src', zThis.attr('data-src'));
        zThis.removeClass('crs');
    });

};

VI.template = function (tmpl, data) {
    var render = template.compile(tmpl);

    return render(data);
};

VI.ajax = function (options) {
    $.ajax(options);
};

VI.console = function (msg) {
    // $('#console').html(msg);
};

VI.goTop = function (id) {

    var zTop = $(id);
    if (zTop.attr('clk') != 1) {
        zTop.attr('clk', 1);

        zTop.click(function () {
            $(window).scrollToTop(0);
        });
    }

    if ($(window).height() < $(document).height()) {
        zTop.show();
    } else {
        zTop.hide();
    }
};

VI.alert = function (msg) {
    var tmpl = '<div class="alert-all-wrap"><img src=\'../images/caution.png\'></img>\
					{{content}}\
				</div>';
    var html = VI.template(tmpl, { content: msg });

    $('.alert-all-wrap').remove();

    var zHtml = $(html);
    $('body').append(zHtml);

    setTimeout(function () {
        zHtml.remove();
    }, 300000);
};

VI.trim = function (str) {
    return str.replace(/^\s+|\s+$/g, "");
};

VI.idCard = function (str) {
    var wi = [7, 9, 10, 5, 8, 4, 2, 1, 6, 3, 7, 9, 10, 5, 8, 4, 2],
        xi = [1, 0, "X", 9, 8, 7, 6, 5, 4, 3, 2],
        pi = [11, 12, 13, 14, 15, 21, 22, 23, 31, 32, 33, 34, 35, 36, 37, 41, 42, 43, 44, 45, 46, 50, 51, 52, 53, 54, 61, 62, 63, 64, 65, 71, 81, 82, 91];

    if (str.match(/^\d{14,17}(\d|X)$/gi) == null) return false;

    var brithday18 = function (tmpl) {
        var year = parseFloat(tmpl.substr(6, 4));
        var month = parseFloat(tmpl.substr(10, 2));
        var day = parseFloat(tmpl.substr(12, 2));
        var checkDay = new Date(year, month - 1, day);
        var nowDay = new Date();
        if (1900 <= year && year <= nowDay.getFullYear() && month == (checkDay.getMonth() + 1) && day == checkDay.getDate()) {
            return true;
        }
        ;

        return false;
    };

    var brithday15 = function (tmpl) {
        var year = parseFloat(tmpl.substr(6, 2));
        var month = parseFloat(tmpl.substr(8, 2));
        var day = parseFloat(tmpl.substr(10, 2));
        var checkDay = new Date(year, month - 1, day);
        if (month == (checkDay.getMonth() + 1) && day == checkDay.getDate()) {
            return true;
        }
        ;

        return false;
    };

    var validate = function (tmpl) {

        var aIdCard = tmpl.split("");
        var sum = 0;
        for (var i = 0; i < wi.length; i++) {
            sum += wi[i] * aIdCard[i]; //线性加权求和  
        }
        ;
        var index = sum % 11; //求模，可能为0~10,可求对应的校验码是否于身份证的校验码匹配  
        if (xi[index] == aIdCard[17].toUpperCase()) {
            return true;
        }
        ;

        return false;
    };

    var province = function (tmpl) {
        var p2 = tmpl.substr(0, 2);
        for (var i = 0; i < pi.length; i++) {
            if (pi[i] == p2) {
                return true;
            };
        };

        return false;
    };

    if (str.length == 18 && province(str) && brithday18(str) && validate(str)) return true;

    if (str.length == 15 && province(str) && brithday15(str)) return true;

    return false;
};

VI.showTicket = function (options) {
    var html = '<div class="full-mask tips-dialog">\
					<div class="alert-wrap">\
						<div class="gp-icons gpd-alert-icon"></div>\
						<div class="gpd-alert-body alert-p-distance">\
							<p>【半价票仅限以下人群使用】</p>\
							<p>1、参加全国统一普通高校招生考试，被录取的本科生、专科生，在校中、小学生，或18周岁以下未成年人（凭本人学生证、有效身份证或户口本）。</p>\
							<p>2、在外国的中国留学生（学生证、身份证需同时使用）。</p>\
							<p>3、60周岁-69周岁中国老人（含港、澳、台居民）凭有效身份证购买半价票。</p>\
							<p class="gpd-alert-tips">因不符合半价票规定导致无法入园引发的纠纷和损失，由订票人自己承担。</p>\
							<p class="gpd-agree-wrap"><em class="gp-icons gpd-agree-icon"></em>我已了解半价票使用规范</p>\
						</div>\
						<div class="gpd-btn-wrap">\
							<div class="gpd-btn gpd-btn-right gpd-btn-disabled">下一步</div>\
							<div class="gpd-btn gpd-btn-left">取消</div>\
						</div>\
					</div>\
				</div>';

    $('.full-mask').remove();

    var zHtml = $(html);

    $('body').append(zHtml);

    var zAgree = zHtml.find('.gpd-agree-icon'),
        zAgreeWrap = zHtml.find('.gpd-agree-wrap'),
        zBtnLeft = zHtml.find('.gpd-btn-left'),
        zBtnRight = zHtml.find('.gpd-btn-right');
    zAgreeWrap.click(function () {

        if (zAgree.hasClass('gpd-agree-ok')) {
            zAgree.removeClass('gpd-agree-ok');
            zBtnRight.addClass('gpd-btn-disabled');
        } else {
            zAgree.addClass('gpd-agree-ok');
            zBtnRight.removeClass('gpd-btn-disabled');
        }

    });

    zBtnLeft.click(function () {
        window.location.href = options.href;
    });

    zBtnRight.click(function () {
        if (!zBtnRight.hasClass('gpd-btn-disabled')) {
            zHtml.remove();
        }
    });
};

VI.showOrderTips = function (options) {

    options = $.extend({ text: '半价票入园前，请出示您的相关证件。' }, options);

    var html = '<div class="full-mask">\
					<div class="alert-wrap alert-single">\
						<div class="gp-icons gpd-alert-icon"></div>\
						<div class="gpd-alert-body">\
							<p class="gpd-alert-tips">' + options.text + '</p>\
						</div>\
						<div class="gpd-btn-wrap">\
							<div class="gpd-btn">我知道了</div>\
						</div>\
					</div>\
				</div>';

    $('.full-mask').remove();

    var zHtml = $(html);

    $('body').append(zHtml);

    //居中
    var zWrap = zHtml.find('.alert-wrap');
    zWrap.css('margin-top', (VI.height - zWrap.height() - 10) / 2 + 'px');

    zHtml.find('.gpd-btn-wrap .gpd-btn').click(function () {

        zHtml.remove();
    });
};

VI.showDialog = function (options) {

    options = $.extend({ text: '' }, options);

    var html = '	<div class="full-mask full-dialog">\
					<div class="alert-wrap alert-single">\
						<div class="gpd-alert-body">\
							<p class="gpd-alert-tips">'+ options.text + '</p>\
						</div>\
						<div class="gpd-btn-wrap">\
							<div class="gpd-btn">我知道了</div>\
						</div>\
					</div>\
				</div>';


    $('.full-mask').remove();

    var zHtml = $(html);

    $('body').append(zHtml);

    //居中
    var zWrap = zHtml.find('.alert-wrap');
    zWrap.css('margin-top', (VI.height - zWrap.height() - 10) / 2 + 'px');

    zHtml.find('.gpd-btn-wrap .gpd-btn').click(function () {

        options.callback && options.callback();

        zHtml.remove();
    });
}
VI.showShareDialog = function (options) {

    options = $.extend({ text: '赠送您一张喀纳斯门票，请尽快领取', tips: 0 }, options);

    var html = '<div class="full-mask full-dialog share-tips">\
	<div class="alert-wrap alert-single">\
		<div class="gpd-alert-body">\
			<p class="gpd-alert-tips">'+ options.text + '</p>\
		</div>\
		<div class="gpd-btn-wrap">\
			<div class="gpd-btn gpd-share-close">取消</div>\
			<div class="gpd-btn gpd-share-get">领取</div>\
		</div>\
	</div>\
</div>';

    $('.full-mask').remove();

    var zHtml = $(html);

    $('body').append(zHtml);

    var zWrap = zHtml.find('.alert-wrap');
    zWrap.css('margin-top', (VI.height - zWrap.height() - 10) / 2 + 'px');

    if (options.tips == 1) {
        zHtml.find('.gpd-btn-wrap').hide();
        zHtml.find('.gpd-alert-tips').css('text-align', 'center');
    }

    var getTicketHandler = function (type) {

        VI.ajax({
            type: "post",
            url: '/plugins/goods/front/getticket',
            dataType: 'json',
            data: { id: options.fname, type: type },
            success: function (data) {

                if (data.status == 0) {
                    window.location.reload();
                } else {
                    VI.alert(data.msg);
                }

            },
            error: function (request, text, error) {

            },

        });

    }

    zHtml.find('.gpd-btn-wrap .gpd-share-get').click(function () {

        //领取
        getTicketHandler(1);

    });
    zHtml.find('.gpd-btn-wrap .gpd-share-close').click(function () {

        //领取
        getTicketHandler(2);

    });

}

VI.imagePre = function (options) {

    options = $.extend({ up: 0 }, options);

    var urls = [];
    $(options.cls).each(function (i, o) {

        if (options.up == 1) {
            var upsrc = $(o).attr('up-src');
            if (upsrc) urls.push(VI.base_url + upsrc);
        } else {
            urls.push(o.src);
        }
    });

    wx.previewImage({
        current: options.curr, // 当前显示图片的http链接
        urls: urls // 需要预览的图片http链接列表
    });
};

VI.goBottom = function () {

    if ($(window).scrollTop() < 200) $(window).scrollToTop(1000);

};

VI.orderChange = function (options) {

    var html = '<div class="order-mask">\
					<div class="lsc-imgwrap">\
						<img src="">\
					</div>\
				</div>';

    var jHtml = $(html);
    $('body').append(jHtml);

    options.callback = function (orientation) {

        if (orientation == 'landscape') {
            //横屏

            //获取当前的图片地址
            var imgSrc = $('.swiper-slide-active .gpo-curr-img').attr('src');

            jHtml.find('img').attr('src', imgSrc);

            jHtml.show();
        } else {
            jHtml.hide();
        }

    };

    VI.orientation(options);

}
/*横竖屏切换*/
VI.orientation = function (options) {

    options = $.extend({ callback: function (orientation) { } }, options);

    var supportsOrientation = (typeof window.orientation == 'number' && typeof window.onorientationchange == 'object');

    var updateOrientation = function () {

        if (supportsOrientation) {
            updateOrientation = function () {

                var orientation = window.orientation;

                switch (orientation) {
                    case 90:
                    case -90:
                        orientation = 'landscape';
                        break;
                    default:
                        orientation = 'portrait';
                }

                options.callback(orientation);
            }
        } else {
            updateOrientation = function () {
                var orientation = (window.innerWidth > window.innerHeight) ? 'landscape' : 'portrait';

                options.callback(orientation);
            }
        }

        updateOrientation();
    };

    updateOrientation();

    if (supportsOrientation) {
        window.addEventListener('orientationchange', updateOrientation, false);
    } else {
        setInterval(updateOrientation, 1000);
    }

}

function VPlayer(options) {

    this.opt = options;
    this.init();

}
VPlayer.prototype.init = function () {

    var This = this,
		options = this.opt;

    This.players = {};
    This.currId = -1;

    $(options.id).each(function (i, o) {

        var zThis = $(o);
        if (zThis.attr('is_init') != 1) {
            zThis.attr('is_init', 1);

            zThis.click(function () {

                //if(navigator.userAgent.indexOf("MQQBrowser") != -1){
                //	This.playQQ(zThis);
                //} else {
                This.play(zThis);
                //}


            });
        }


    });
}
VPlayer.prototype.addPlayer = function () {
    var This = this,
		options = this.opt;

    $(options.id).each(function (i, o) {

        var zThis = $(o);
        if (zThis.attr('is_init') != 1) {
            zThis.attr('is_init', 1);

            zThis.click(function () {

                This.play(zThis);

            });
        }

    });
}
VPlayer.prototype.play = function (zItem) {

    var This = this,
        audioUrl = zItem.attr('audio-url'),
        auidoId = zItem.attr('id'),
        duration = 0,
        lastPos = 0,
        zParent = zItem.parent();

    // audioUrl = 'http://longmen.qqdyw.cn' + audioUrl;

    //暂停所有音频，注意可能有的音频正在加载
    This.pause();
    if (zParent.hasClass('yyl-item-playing')) {
        //正在播放则停止
        zParent.removeClass('yyl-item-playing');
        This.currId = -1;
        return;
    }

    //如果正在加载，直接退出
    if (zParent.hasClass('yyl-item-playload')) return;

    $('.yyl-item-playing').removeClass('yyl-item-playing'); //删除所有正在播放的效果
    $('.yyl-item-playload').removeClass('yyl-item-playload');

    //判断是否已经存在
    if (!This.players[auidoId]) {

        var tId = 'player_' + auidoId;
        $('body').append('<audio id="' + tId + '" src="' + audioUrl + '"></audio>');

        zItem.attr('audio-id', auidoId);
        var tPlayer = document.getElementById(tId);

        This.players[auidoId] = tPlayer;

        //启动定时关闭脚本
        if (This.players.length > 1) {
            // This.pauseTimer();
        }
    }

    This.player = This.players[auidoId];
    This.player.play();
    zParent.addClass('yyl-item-playload'); //添加正在加载动画
    This.currId = auidoId;

    //获取状态栏
    This.progressBar = zParent.find('.progress-bar .expand');

    //设置定时任务，检测动态，有些浏览器不支持事件
    var progressBarFun = function () {
        // duration = Math.ceil(This.player._duration); //总时间 QQ和Howler不一样
        // var pos = Math.ceil(This.player.pos());//当前时间 QQ和Howler不一样

        duration = Math.ceil(This.player.duration); //加载的时候为0
        var pos = Math.ceil(This.player.currentTime); //

        var width = 0,
            maxWidth = 100;
        if (!isNaN(duration)) { //正在加载
            // if(duration == 0) //结束

            width = ((pos / duration) * 100);

            if (width >= maxWidth) width = maxWidth;

            if (pos > 0) {
                zParent.removeClass('yyl-item-playload');
                zParent.addClass('yyl-item-playing');
            }
            // VI.console(pos + '==' + duration + '==' + width);

            This.progressBar.css('width', width + '%');

            if (width == maxWidth) { //结束播放
                zParent.removeClass('yyl-item-playing');
            }
        }

        This.playerTimer = setTimeout(progressBarFun, 1000);
    };
    progressBarFun();

};

VPlayer.prototype.pauseTimer = function () {
    var This = this,
        pTimer = null;

    var pauseHandler = function () {

        $.each(This.players, function (i, o) {
            if (i != This.currId) o.pause();
        });

        setTimeout(pauseHandler, 2000);
    }
    pauseHandler();
};

VPlayer.prototype.pause = function (zItem) {

    var This = this;

    $.each(This.players, function (i, o) {
        o.pause();
    });

    This.playerTimer && clearTimeout(This.playerTimer);
};

function VAlbum(options) {

    this.opt = options;
    this.init();
};

VAlbum.prototype.init = function () {

    var This = this,
        options = this.opt;

    $(options.id).click(function () {

        This.showPhoto();

    });
};

VAlbum.prototype.showPhoto = function () {

    var This = this,
		options = this.opt;
    $('.f-foot-wrap').hide();
    var tmpl = '<div class="photo-mask">\
					<div class="icons alb-close-icon"></div>\
					<div class="icons alb-top-icon"></div>\
					<div class="row photo-list">\
						{{each list}}\
						<div class="photo-item resize-wrap" style="height:{{height}}px"><img class="crs" resize="{{$value.w}}x{{$value.h}}" data-src="{{$value.src}}"></div>\
						{{/each}}\
					</div>\
				</div>';

    var tmplData = {
        height: Math.floor(VI.width / 3),
        list: options.items
    }

    var html = VI.template(tmpl, tmplData);

    $('.photo-mask').remove();
    $('body').append(html);
    $('body').addClass('open-mask');

    VI.src();

    $(".photo-list .photo-item").each(function (i, o) {
        var zThis = $(o);

        zThis.click(function () {
            This.open(i);
        })
    });

    $('.photo-mask .alb-close-icon').click(function () {

        $('.photo-mask').remove();

        $('body').removeClass('open-mask');
        $('.f-foot-wrap').show();
    });
}
VAlbum.prototype.open = function (index) {

    var This = this,
		options = this.opt;

    if (!This.pswpElement) {
        var tmpl = '	<div id="pswp" class="pswp album-wrap" tabindex="-1" role="dialog" aria-hidden="true">\
					    <div class="pswp__bg">\
					    	 <div class="icons alb-top-icon"></div>\
					    </div>\
					    <div class="icons alb-close-icon"></div>\
					    <div class="pswp__scroll-wrap">\
					        <div class="pswp__container">\
					            <div class="pswp__item"></div>\
					            <div class="pswp__item"></div>\
					            <div class="pswp__item"></div>\
					        </div>\
					        <div class="pswp__ui">\
					            <div class="pswp__top-bar">\
					                <div class="pswp__counter"></div>\
					                <div class="pswp__preloader">\
					                    <div class="pswp__preloader__icn">\
					                      <div class="pswp__preloader__cut">\
					                        <div class="pswp__preloader__donut"></div>\
					                      </div>\
					                    </div>\
					                </div>\
					            </div>\
					            <div class="pswp__share-modal pswp__share-modal--hidden pswp__single-tap">\
					                <div class="pswp__share-tooltip"></div>\
					            </div>\
					            <div class="pswp__caption">\
					                <div class="pswp__caption__center"></div>\
					            </div>\
					        </div>\
					        <div class="pswp-bottom">\
					        	<div id="p_counter" class="p_counter"></div>\
					        	<div id="pswp-content" class="pswp-content">\
					        	</div>\
					        </div>\
					    </div>\
					</div>';
        $('body').append(tmpl);

        This.pswpElement = $('#pswp')[0];

        $('.pswp .alb-close-icon').click(function () {
            This.pswp.close();
        });
    }


    var items = options.items;
    var options = {
        index: index, // start at first slide
        closeOnScroll: false,
        closeOnVerticalDrag: false,
        history: false
    };

    var pswp = new PhotoSwipe(This.pswpElement, PhotoSwipeUI_Default, items, options);

    pswp.init();

    This.pswp = pswp;
}

var imgReady = (function () {

    var list = [], intervalId = null,



    // 用来执行队列
    tick = function () {
        var i = 0;
        for (; i < list.length; i++) {
            list[i].end ? list.splice(i--, 1) : list[i]();
        };
        !list.length && stop();
    },
    // 停止所有定时器队列
    stop = function () {
        clearInterval(intervalId);
        intervalId = null;
    };
    return function (url, ready, load, error) {
        var onready, width, height, newWidth, newHeight,
            img = new Image();
        img.src = url;

        // 如果图片被缓存，则直接返回缓存数据
        if (img.complete) {
            ready.call(img);
            load && load.call(img);
            return;
        };
        width = img.width;
        height = img.height;

        // 加载错误后的事件
        img.onerror = function () {
            error && error.call(img);
            onready.end = true;
            img = img.onload = img.onerror = null;
        };

        // 图片尺寸就绪
        onready = function () {
            newWidth = img.width;
            newHeight = img.height;
            if (newWidth !== width || newHeight !== height ||
                // 如果图片已经在其他地方加载可使用面积检测
                newWidth * newHeight > 1024
            ) {
                ready.call(img);
                onready.end = true;
            };
        };
        onready();
        // 完全加载完毕的事件
        img.onload = function () {
            // onload在定时器时间差范围内可能比onready快
            // 这里进行检查并保证onready优先执行
            !onready.end && onready();
            load && load.call(img);

            // IE gif动画会循环执行onload，置空onload即可
            img = img.onload = img.onerror = null;
        };
        // 加入队列中定期执行
        if (!onready.end) {
            list.push(onready);

            // 无论何时只允许出现一个定时器，减少浏览器性能损耗

            if (intervalId === null) intervalId = setInterval(tick, 40);

        };

    };

})();

function VPay(options) {
    this.opt = $.extend({ refund: 0 }, options);

    this.init();
}

VPay.prototype.init = function () {

    var This = this,
        options = this.opt;

    $("#price").html(options.price + "元");
    $('#gpdMoney').html(options.price);
    var zPay = $(options.id);
    zPay.click(function () {

        This.order(); //提交订单


        //if (options.refund == 1) {
        //    This.refund(); //退款
        //} else {
        //    This.order();//提交订单
        //}

        return false;
    });


    if (options.refund == 1) {
        This.imgAdd();
    }

    var zNum = $('#ticketNum'),
        zMoney = $('#gpdMoney'),
        zNumSub = $('#ticketNumSub'),
        zNumAdd = $('#ticketNumAdd'),
        zMessage = $('#message');

    if (options.max <= 0) {
        zNum.attr('disabled', true);
        zMessage.attr('disabled', true);


        VI.alert('没票可退');
        return;
    }

    var showMoney = function (val) {

        if (val <= 1) {
            zNumSub.addClass('num-gray');
        } else {
            zNumSub.removeClass('num-gray');
        }

        if (val >= options.max) {
            zNumAdd.addClass('num-gray');
        } else {
            zNumAdd.removeClass('num-gray');
        }

        zMoney.html(val * (options.price * 100) / 100);
    };

    var keyHandler = function (e) {
        var val = zNum.val();
        if (!/^\d+$/.test(val)) {
            val = val.replace(/[^\d]/g, '');
        }

        if (val == "") val = "1";
        val = val.substr(-1);
        if (val > options.max) val = options.max;

        zNum.val(val);

        showMoney(val);
    };

    zNum.keyup(function (e) {
        keyHandler(e);
    }).change(function (e) {
        keyHandler(e);
    });

    //加减
    var changeValue = function (i) {
        var val = zNum.val();
        if (val == "") val = 1;

        val = parseInt(val) + i;
        if (val < 1) val = 1;
        if (val > options.max) val = options.max;

        zNum.val(val);
        showMoney(val);
    };

    zNumSub.click(function () {
        changeValue(-1);
    });
    zNumAdd.click(function () {
        changeValue(1);
    });
};

VPay.prototype.imgAdd = function () {

    var This = this,
		options = this.opt;
    var maxSum = 3;
    var zImage = $('#imgadd'),
		zImageWrap = $('#imgwrap');

    if (options.max <= 0) return VI.alert('没票可退');

    zImage.click(function () {
        zImages = $('.rfd-form-img img.rfd-img-item');
        //微信图片上传
        var count = zImages.length;
        var remder = maxSum - count;
        if (remder < 0) remder = 0;

        wx.chooseImage({
            success: function (res) {
                var localIds = res.localIds;
                var upImgs = [];
                // var size = localIds.length > remder?remder:localIds;
                var size = Math.min.apply(Math, [localIds.length, remder, maxSum]);
                for (var i = 0; i < size; i++) {

                    var zTmpImg = $('<img src="' + localIds[i] + '" class="rfd-img-item rfd-img-pre">');
                    zImageWrap.before(zTmpImg);
                    upImgs.push({ id: localIds[i], img: zTmpImg });
                }
                count = count + size;
                if (count >= maxSum) {
                    // zImage.css("display", "none");
                    zImage.addClass("hide");
                }
                downloadImg(upImgs);
            }
        });
    });

    function downloadImg(imgAry) {
        var imgObj = imgAry.pop();
        wx.uploadImage({
            localId: imgObj.id, // 需要上传的图片的本地ID，由chooseImage接口获得
            isShowProgressTips: 1, // 默认为1，显示进度提示
            success: function (res) {
                var serverId = res.serverId; // 返回图片的服务器端ID
                VI.ajax({
                    type: 'POST',
                    url: VI.base_url + "/admin/download",
                    data: { mediaId: serverId },
                    dataType: 'json',
                    success: function (data, status, xhr) {
                        if (data.status == 0) {
                            imgObj.img.attr("up-src", data.info.path);
                            imgObj.img.click(function () {
                                VI.imagePre({ cls: '.rfd-img-pre', curr: this.src, up: 1 });
                            });
                            touch.on(imgObj.img[0], 'hold', function (ev) {
                                $(".full-mask.rfd-mask").removeClass("hide");
                                $(".rfd-alert-wrap .alert-btn-right").click(function () {
                                    $(".full-mask.rfd-mask").addClass("hide");
                                });
                                $(".rfd-alert-wrap .alert-btn-left").click(function () {
                                    $(".full-mask.rfd-mask").addClass("hide");
                                    imgObj.img[0].remove();
                                    zImage.removeClass("hide");
                                });
                            });
                        } else {
                            // alert(data.msg);
                        }
                    },
                    error: function (xhr, type) {

                    }
                });

                if (imgAry.length > 0) {
                    downloadImg(imgAry);
                }

            },
            fail: function (res) {

            }
        });
    }

}
VPay.prototype.refund = function () {

    var This = this,
		options = this.opt;

    if (options.max <= 0) return VI.alert("无票可退");

    var zNum = $('#ticketNum');
    var zNumVal = parseInt(zNum.val());
    if (zNumVal <= 0) return VI.alert('退票数量不能为空！');
    if (zNumVal > options.max) return VI.alert('退票数量不能大于' + options.max + '张！');

    var zMessage = $('#message');
    var zMessageVal = zMessage.val();
    if (zMessageVal == "") return VI.alert('退票原因不能为空！');

    //获取图片信息
    var zImageWrap = $('#imgwrap');
    var images = [];
    $('.rfd-img-item').each(function (i, o) {

        var zThis = $(o);

        var upsrc = zThis.attr('up-src');
        if (upsrc) images.push({ url: upsrc });

    });


    if (This.isLoading == 1) return;

    This.isLoading = 1;
    VI.ajax({
        type: "post",
        url: '/plugins/person/front/applyreticke',
        dataType: 'json',
        data: { orid: options.orid, num: zNumVal, message: zMessageVal, imgs: JSON.stringify(images) },
        success: function (data) {


            if (data.status == 0) {

                VI.showDialog({
                    text: '退票申请成功，工作人员会在5-10个工作日内为您办理。',
                    callback: function () {
                        // window.location.href = '/plugins/person/front/refundmain?id=' + options.orid;
                        window.location.href = '/plugins/person/front/refundhistory?orcid=' + options.orid;
                        This.isLoading = 0;
                    }
                });

            } else {

                VI.alert(data.msg);
                This.isLoading = 0;

            }


        },
        error: function (request, text, error) {
            This.isLoading = 0;
        },
    });

}
VPay.prototype.pay = function () {
    //wx2015062111574916f529c8d00447954312

    wx.chooseWXPay({
        timestamp: 0, // 支付签名时间戳，注意微信jssdk中的所有使用timestamp字段均为小写。但最新版的支付后台生成签名使用的timeStamp字段名需大写其中的S字符
        nonceStr: '', // 支付签名随机串，不长于 32 位
        package: '', // 统一支付接口返回的prepay_id参数值，提交格式如：prepay_id=***）
        signType: '', // 签名方式，默认为'SHA1'，使用新版支付需传入'MD5'
        paySign: '', // 支付签名
        success: function (res) {
            // 支付成功后的回调函数
        }
    });
}
//创建订单
VPay.prototype.order = function () {

    var This = this,
        options = this.opt;

    var ticketId = options.ticketId;
    var ticketName = options.ticketName;

    //参数校验
    var zName = $('#name');
    var zNameVal = zName.val();
    if (zNameVal === "") {
        VI.goBottom();
        return VI.alert('姓名不能为空！');
    }
    var zPhone = $('#phone');
    var zPhoneVal = zPhone.val();
    if (zPhoneVal === "") {
        VI.goBottom();
        return VI.alert('电话不能为空！');
    }

    var preVisitTime = $('#preVisitTime');
    var preVisitTimeVal = preVisitTime.val();
    if (preVisitTimeVal === "") {
        VI.goBottom();
        return VI.alert('入园日期不能为空！');
    }

    if (!(VI.regPhone.test(zPhoneVal))) {
        VI.goBottom();
        return VI.alert('电话号码错误！');
    }

    var zIDCard = $('#idcard');
    var zIDCardVal = zIDCard.val();
    if (zIDCardVal === "") {
        VI.goBottom();
        return VI.alert('身份证不能为空！');
    }
    if (!VI.idCard(zIDCardVal)) {
        VI.goBottom();
        return VI.alert('身份证号错误！');
    }

    var zNum = $('#ticketNum');
    var zNumVal = parseInt(zNum.val());
    if (zNumVal <= 0) {
        VI.goBottom();
        return VI.alert('购票数量不能为空！');
    }

    if (This.isLoading == 1) return;

    This.isLoading = 1;
    $("#loading").show();
    $(".container").hide();
    var zPayBtn = $('#payBtn');
    zPayBtn.addClass('z-btn-top');

    //保存联系人信息
    VI.ajax({
        type: "post",
        url: '../WebService/UserWebService.asmx/SaveContract',
        dataType: 'json',
        data: { openId: GetQueryString('openId'), userName: zNameVal, mobile: zPhoneVal, idCard: zIDCardVal },
        success: function () {
        }
    });

    VI.ajax({
        type: "post",
        url: '../WebService/TicketWebService.asmx/CreateOrder',
        dataType: 'json',
        data: { openId: GetQueryString('openId'), ticketCategoryId: ticketId, ticketName: ticketName, ticketCount: zNumVal, couponId: 0, couponCount: 0, orderNo: options.orid, contractName: zNameVal, contractPhone: zPhoneVal, contractIdCard: zIDCardVal, preUseTime: preVisitTimeVal },
        success: function (result) {
            function OrderRelease(orderId) {
                VI.ajax({
                    type: "post",
                    url: '../WebService/TicketWebService.asmx/OrderRelease',
                    data: { orderId: orderId },
                    dataType: 'json',
                    success: function () {
                    }
                });
            }

            if (result.IsSuccess) {
                wx.chooseWXPay({
                    timestamp: result.Data.timeStamp, // 支付签名时间戳，注意微信jssdk中的所有使用timestamp字段均为小写。但最新版的支付后台生成签名使用的timeStamp字段名需大写其中的S字符
                    nonceStr: result.Data.nonceStr, // 支付签名随机串，不长于 32 位
                    package: result.Data.package, // 统一支付接口返回的prepay_id参数值，提交格式如：prepay_id=***）
                    signType: 'MD5', // 签名方式，默认为'SHA1'，使用新版支付需传入'MD5'
                    paySign: result.Data.paySign, // 支付签名
                    success: function (res) {
                        This.isLoading = 0;
                        $("#loading").hide();
                        $(".container").show();
                        document.location.href = "../order/MyOrderList.html";
                    },
                    fail: function (res) {
                        OrderRelease(result.Data.orderId);
                        This.isLoading = 0;
                        $("#loading").hide();
                        $(".container").show();
                        alert(JSON.stringify(res));
                    },
                    cancel: function (res) {
                        OrderRelease(result.Data.orderId);
                        This.isLoading = 0;
                        $("#loading").hide();
                        $(".container").show();
                    },
                    complete: function (res) {
                        This.isLoading = 0;
                        $("#loading").hide();
                        $(".container").show();
                        zPayBtn.removeClass('z-btn-top');
                    }
                });
            } else {
                alert(result.Message);
                //alert("已达今日门票配额上限");
                This.isLoading = 0;
                $("#loading").hide();
                $(".container").show();
                zPayBtn.removeClass('z-btn-top');
            }
        },
        error: function (request, text, error) {
            alert(error);
            //alert("已达今日门票配额上限");
            This.isLoading = 0;
            $("#loading").hide();
            $(".container").show();
            zPayBtn.removeClass('z-btn-top');
        },
    });
};


function VOrder(options) {
    this.opt = $.extend({ maxtime: 5000, sync: 0, isgive: 0 }, options);

    this.init();
}
VOrder.prototype.init = function () {
    var This = this,
		options = this.opt;

    //处理赠送功能
    window.share.link = VI.curr_site_url + "/shareapp?id=" + shareAppId;
    window.share.appcallback = function () {
        //回调函数。

        VI.ajax({
            type: "post",
            url: '/plugins/goods/front/give',
            dataType: 'json',
            data: { id: options.orid, fname: shareAppId },
            success: function (data) {

                if (data.status == 0) {
                    This.syncStatus(data.info.fname);
                } else {
                    VI.alert(data.msg);
                }

            },
            error: function (request, text, error) {
                This.isLoading = 0;
            },

        });

    }


    //设置定时查看状态
    if (options.sync == 0) {
        This.timeout = setTimeout(function () {
            This.currStatus();
        }, options.maxtime);
    }


}
VOrder.prototype.currStatus = function () {

    var This = this,
		options = this.opt;

    var jWrap = $('.swiper-slide-active .gpo-qr-wrap');
    var fname = jWrap.attr('fname');

    //状态为已使用且不显示二维码，或者退票不再请求
    if (jWrap.attr('stat') == 3 && jWrap.attr('show') == 0
		|| jWrap.attr('stat') == 5 && jWrap.attr('show') == 0) {

        This.timeout = setTimeout(function () {
            This.currStatus();
        }, options.maxtime);
    } else {
        This.syncStatus(fname);
    }

}
VOrder.prototype.syncStatus = function (fname) {

    var This = this,
		options = this.opt;

    if (This.timeout) clearTimeout(This.timeout);
    if (This.xhr) This.xhr.abort();

    VI.ajax({
        type: "post",
        url: '/plugins/goods/front/syncstatus',
        dataType: 'json',
        data: { id: options.orid, fname: fname, isgive: options.isgive },
        success: function (data) {

            if (data.status == 0) {

                This.syncHandler(data.info);
            }

            This.timeout = setTimeout(function () {
                This.currStatus();
            }, options.maxtime);
        },
        error: function (request, text, error) {
            This.timeout = setTimeout(function () {
                This.currStatus();
            }, options.maxtime);
        },

    });


}
VOrder.prototype.syncHandler = function (info) {
    var This = this,
		options = this.opt;


    //设置信息
    $('.gpo-qr-wrap').each(function (i, o) {

        var jThis = $(o);
        if (jThis.attr('fname') == info.fname) {

            if (jThis.attr('stat') != info.status) {
                var jParent = jThis.parent();
                var jLabel = jParent.find('.gpo-status-label');
                jLabel.html('<span class=" gpo-status-' + info.status + '">' + info.text + '</span>');

                jThis.attr('stat', info.status);
            }


            if (jThis.attr('show') != info.show) {
                var html = '<img src="/assets/img/gpo_bad_qr_bg1.png">';
                jThis.html(html);

                jThis.attr('show', info.show);
            }

        }

    });

}


VI.statisticData = function (options) {
    var type = options.type;
    var dataId = options.dataId;
    var date = new Date();
    var img = new Image();
    img.src = VI.base_url + "/admin/statisticData?type=" + type + "&dataId=" + dataId + "&t=" + date.getTime();
}


function VPage(options) {
    var This = this;
    This.body = $(options.id);
    This.url = options.url;

    This.hasNextTips = $('.yyd-moretips');
    This.currPage = 1;
    This.hasNext = false;
    This.isLoading = 0;
    this.init();

}

VPage.prototype.init = function () {
    var This = this;


    //加载更多
    $(window).scroll(function () {

        if ($(window).scrollTop() == $(document).height() - $(window).height()) {
            This.loadNext();
        }

    })


}

VPage.prototype.setData = function (data) {
    var This = this;
    if (!This.tmpl) return;
    This.itemRender = template.compile(This.tmpl);
    var jItems = $(This.itemRender(data));
    This.body.append(jItems);

    This.hasNext = data.hasNext;

    This.callback && This.callback(jItems);

    if (This.hasNext) $(window).scrollTop($(window).scrollTop() - 1);

    if (This.hasNext == 1) {
        This.hasNextTips.show();
    } else {
        This.hasNextTips.hide();
    }
}

VPage.prototype.loadNext = function () {
    var This = this;
    if (This.hasNext == 0 || This.isLoading == 1) return;

    This.isLoading = 1;

    VI.ajax({
        type: "get",
        url: This.url,
        dataType: 'json',
        data: { id: This.category, p: This.currPage + 1 },
        success: function (data) {

            This.setData(data);
            This.isLoading = 0;
            This.currPage++;
        },
        error: function (request, text, error) {
            This.isLoading = 0;
        },
    });
}

// Usage: $(element).scrollToTop([position])
; (function ($) {
    var scrollToTopInProgress = false

    $.fn.scrollToTop = function (position) {
        var $this = this,
          targetY = position || 0,
          initialY = $this.scrollTop(),
          lastY = initialY,
          delta = targetY - initialY,
          speed = Math.min(750, Math.min(1500, Math.abs(initialY - targetY))),
          start, t, y, frame = window.requestAnimationFrame ||
            window.webkitRequestAnimationFrame ||
            window.mozRequestAnimationFrame ||
            function (callback) { setTimeout(callback, 15) },
          cancelScroll = function () { abort() }

        if (scrollToTopInProgress) return
        if (delta == 0) return

        function smooth(pos) {
            if ((pos /= 0.5) < 1) return 0.5 * Math.pow(pos, 5)
            return 0.5 * (Math.pow((pos - 2), 5) + 2)
        }

        function abort() {
            $this.off('touchstart', cancelScroll)
            scrollToTopInProgress = false
        }

        $this.on('touchstart', cancelScroll)
        scrollToTopInProgress = true

        frame(function render(now) {
            if (!scrollToTopInProgress) return
            if (!start) start = now
            t = Math.min(1, Math.max((now - start) / speed, 0))
            y = Math.round(initialY + delta * smooth(t))
            if (delta > 0 && y > targetY) y = targetY
            if (delta < 0 && y < targetY) y = targetY
            if (lastY != y) $this.scrollTop(y)
            lastY = y
            if (y !== targetY) frame(render)
            else abort()
        })
    }
})(Zepto)
