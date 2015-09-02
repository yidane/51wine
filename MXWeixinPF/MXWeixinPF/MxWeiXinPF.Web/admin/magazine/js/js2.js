define("wap/showcase/modules/swp/utils", ["require"], function () {
    function t() {
        this.__events = {}
    }
    var e = $.extend;
    t.prototype = {
        on: function (t, e) {
            return this.__events[t] || (this.__events[t] = []),
            this.__events[t].push(e),
            this
        },
        emit: function (t) {
            var e = this.__events[t],
                i = Array.prototype.slice.call(arguments, 1),
                n = this;
            e && e.forEach(function (t) {
                t.apply(n, i)
            })
        },
        removeListener: function (t, e) {
            if (void 0 != this.__events[t]) {
                var i;
                e ? (i = this.__events[t].indexOf(e), i > 0 && this.__events[t].splice(i, 1)) : delete this.__events[t]
            }
        }
    };
    var i = function () {
        var t = window.getComputedStyle(document.documentElement, ""),
            e = (Array.prototype.slice.call(t).join("").match(/-(moz|webkit|ms)-/) || "" === t.OLink && ["", "o"])[1],
            i = "WebKit|Moz|MS|O".match(new RegExp("(" + e + ")", "i"))[1];
        return {
            dom: i,
            lowercase: e,
            css: "-" + e + "-",
            js: e[0].toUpperCase() + e.substr(1)
        }
    }();
    return {
        extend: e,
        EventEmitter: t,
        prefix: i
    }
}),
define("wap/showcase/modules/swp/input", ["require", "./utils"], function (t) {
    function e(t) {
        n.apply(this, arguments),
        this.isTouching = !1,
        this.startPt = null,
        this.endPt = null,
        this.bind(t)
    }
    var i = t("./utils"),
        n = i.EventEmitter,
        s = i.extend;
    return e.prototype = Object.create(new n),
    s(e.prototype, {
        bind: function (t) {
            var e = this;
            $(t).on("touchstart mousedown", function (t) {
                e.isTouching || (e.isTouching = !0, e.startPt = e.pointerEventToXY(t))
            }).on("touchend mouseup", function (t) {
                var i;
                e.isTouching = !1,
                e.endPt = e.pointerEventToXY(t),
                i = e.startPt.y - e.endPt.y,
                distX = e.startPt.x - e.endPt.x,
                0 != i && e.emit(i > 0 ? "up" : "down", i),
                0 != distX && e.emit(distX > 0 ? "left" : "right", distX)
            })
        },
        pointerEventToXY: function (t) {
            var e = {
                x: 0,
                y: 0
            },
                i = t.type;
            if ("touchstart" == i || "touchmove" == i || "touchend" == i || "touchcancel" == i) {
                var n = t.touches[0] || t.changedTouches[0];
                e.x = n.pageX,
                e.y = n.pageY
            } else ("mousedown" == i || "mouseup" == i || "mousemove" == i || "mouseover" == i || "mouseout" == i || "mouseenter" == i || "mouseleave" == i) && (e.x = t.pageX, e.y = t.pageY);
            return e
        }
    }),
    e
}),
define("wap/showcase/modules/swp/anm_elem", ["require", "./utils"], function (t) {
    function e(t, e) {
        if (this.elem = t, e && e.transition) {
            var i = s.css + "transition",
                n = {
                    transition: e.transition
                };
            n[i] = e.transition,
            this.elem.css(n)
        }
    }
    var i = t("./utils"),
        n = i.extend,
        s = i.prefix;
    return e.prototype = Object.create({}),
    n(e.prototype, {
        from: function (t) {
            return this.fromFrame = this.transformGenerate(t),
            this
        },
        to: function (t) {
            var e = this.elem;
            void 0 == this.fromFrame && (this.fromFrame = this.transformGenerate()),
            t = this.transformGenerate(t),
            e.css({
                display: "none"
            }),
            this.nap("display"),
            e.css(n({
                display: "block"
            }, this.fromFrame)),
            this.nap("-webkit-transform");
            var i = new $.Deferred;
            return e.css(t).on("transitionend webkitTransitionEnd", function s() {
                e.unbind("transitionend webkitTransitionEnd", s),
                i.resolve()
            }),
            delete this.fromFrame,
            delete this.toFrame,
            i.promise()
        },
        transformGenerate: function (t) {
            var e = s.css + "transform";
            return t = n({
                x: 0,
                y: 0,
                z: 0
            }, t),
            t[e] = "translate3d(" + t.x + "," + t.y + "," + t.z + ")",
            t.scale && (t[e] += " scale(" + t.scale + ")"),
            t
        },
        nap: function (t) {
            t && window.getComputedStyle(this.elem[0])[t]
        },
        equal: function (t) {
            return t.elem && t.elem[0] ? this.elem[0] == t.elem[0] : !1
        },
        getElem: function () {
            return this.elem
        }
    }),
    e
}),
define("wap/showcase/modules/swp/scroll", ["require", "./utils", "./anm_elem"], function (t) {
    function e(t, e) {
        n.apply(this, arguments),
        this.wrapElem = t,
        this.currentIndex = 0,
        this.options = s({
            loop: !0
        }, e),
        this.init.apply(this, arguments)
    }
    var i = t("./utils"),
        n = i.EventEmitter,
        s = i.extend,
        a = t("./anm_elem");
    return e.prototype = Object.create(new n),
    s(e.prototype, {
        init: function () {
            var t, e, i = (this.wrapElem, this);
            this.pages = this.wrapElem.find(".swp-page"),
            t = {
                position: "absolute",
                top: 0,
                left: 0,
                width: "100%",
                height: "100%"
            },
            this.pages.each(function (n) {
                e = $(this),
                n != i.currentIndex && e.css("display", "none"),
                e.css(t)
            }),
            this.pages = [].map.call(this.pages, function (t) {
                return new a($(t), {
                    transition: ".5s all ease-out"
                })
            })
        },
        changePage: function (t) {
            var e, i, n = this.getCurrentPage(),
                s = this;
            if (this.turnPage(t), e = this.getCurrentPage(), !n.equal(e)) {
                switch (this.emit("pageChangeStart", n, this.pages.indexOf(n)), this.options.flipway) {
                    case "zoomOut":
                        i = {
                            scale: .5
                        };
                        break;
                    default:
                        i = {}
                }
                i.y = (t > 0 ? "-" : "") + "100%",
                n.to(i).then(function () {
                    n.equal(s.getCurrentPage()) || n.getElem().css({
                        display: "none"
                    })
                }),
                e.from({
                    y: (t > 0 ? "" : "-") + "100%"
                }).to().then(function () {
                    s.emit("pageChangeEnd", e, s.pages.indexOf(e), s.pages.indexOf(n))
                })
            }
        },
        getCurrentPage: function () {
            return this.pages[this.currentIndex]
        },
        turnPage: function (t) {
            this.currentIndex += t,
            this.currentIndex >= this.pages.length && (this.currentIndex = this.options.loop ? 0 : this.pages.length - 1),
            this.currentIndex < 0 && (this.currentIndex = this.options.loop ? this.pages.length - 1 : 0)
        }
    }),
    e
}),
define("wap/showcase/modules/swp/anm_player", ["require", "./anm_elem"], function (t) {
    function e() {
        this.init.apply(this, arguments),
        this.hasPlayed = !1
    }
    var i = t("./anm_elem"),
        n = {
            fadeIn: [{},
            {}],
            drop: [{
                y: "-200px"
            },
            {
                y: 0
            }],
            "float": [{
                y: "200px"
            },
            {
                y: 0
            }],
            leftin: [{
                x: "-200px"
            },
            {
                x: 0
            }],
            rightin: [{
                x: "200px"
            },
            {
                x: 0
            }]
        };
    return Object.keys(n).forEach(function (t) {
        n[t][0].opacity = 0,
        n[t][1].opacity = 1
    }),
    e.prototype = {
        init: function (t) {
            var e;
            this.anms = [].map.call(t, function (t) {
                return e = $(t),
                new i(e, {
                    transition: e.data("duration") + "s ease all " + e.data("delay") + "s"
                })
            }),
            this.anmsStyles = [].map.call(t, function (t) {
                return e = $(t).data("anmstyle")
            })
        },
        play: function () {
            if (!this.hasPlayed) {
                this.hasPlayed = !0;
                var t, e = this;
                this.anms.forEach(function (i, s) {
                    t = n[e.anmsStyles[s]],
                    i.from(t[0]).to(t[1])
                })
            }
        },
        reset: function () {
            this.hasPlayed = !1,
            this.anms.forEach(function (t) {
                t.getElem().css({
                    display: "none"
                })
            })
        }
    },
    e
}),
define("wap/showcase/modules/swp/main", ["require", "./utils", "./input", "./scroll", "./anm_player"], function (t) {
    function e(t, e) {
        this.wrapElem = t,
        this.options = e
    }
    var i = (t("./utils"), t("./input")),
        n = t("./scroll"),
        s = t("./anm_player");
    return e.prototype = {
        init: function () {
            this.input = new i(this.wrapElem),
            this.scroll = new n(this.wrapElem, this.options);
            var t = this.scroll;
            this.input.on("up", function () {
                t.changePage(1)
            }).on("down", function () {
                t.changePage(-1)
            });
            var e = this.scroll.pages.map(function (t) {
                return new s(t.getElem().find(".swp-item-wrap .js-swp-item"))
            });
            t.on("pageChangeEnd", function (t, i) {
                e[i].play()
            }).on("pageChangeStart", function (t, i) {
                e[i].reset()
            }),
            t.emit("pageChangeEnd", t.getCurrentPage(), t.currentIndex)
        }
    },
    e
}),
define("wap/showcase/modules/swp/resource_loader", ["require"], function () {
    function t(t) {
        this.wrap = t
    }
    return t.prototype = {
        start: function () {
            var t = new $.Deferred,
                e = this;
            return "complete" == document.readyState && t.resolve(),
            $(window).on("load", function i() {
                t.resolve(),
                $(window).unbind("load", i)
            }),
            t.promise().then(function () {
                return e.loadRes()
            })
        },
        loadRes: function () {
            var t = this,
                e = [];
            return this.wrap.find(".js-res-load").each(function () {
                var i = $(this),
                    n = new $.Deferred;
                t.loadImage(i, i.data("src"), function () {
                    n.resolve()
                }),
                e.push(n.promise())
            }),
            $.when.apply(window, e)
        },
        loadImage: function (t, e, i) {
            $("<img>").attr("src", e).on("load", function () {
                $(this).remove(),
                "img" == t.prop("tagName").toLowerCase() ? t.attr("src", e) : t.css("background-image", "url(" + e + ")"),
                i && i()
            }).on("error", function () {
                i && i()
            })
        }
    },
    t
}),
define("wap/showcase/modules/scroll/lottery", ["require"], function () {
    function t(t, e, i, n, s, a) {
        this.conNode = t,
        this.background = null,
        this.backCtx = null,
        this.mask = null,
        this.maskCtx = null,
        this.lottery = {},
        this.lotteryType = "image",
        this.cover = e || "#000",
        this.coverType = i,
        this.pixlesData = null,
        this.width = n,
        this.height = s,
        this.lastPosition = null,
        this.drawPercentCallback = a,
        this.vail = !1
    }
    return t.prototype = {
        createElement: function (t, e) {
            var i = document.createElement(t);
            for (var n in e) i.setAttribute(n, e[n]);
            return i
        },
        getTransparentPercent: function (t, e, i) {
            try {
                for (var n = t.getImageData(0, 0, e, i), s = n.data, a = [], o = 0, r = s.length; r > o; o += 4) {
                    var h = s[o + 3];
                    128 > h && a.push(o)
                }
            } catch (l) {
                return 100
            }
            return (a.length / (s.length / 4) * 100).toFixed(2)
        },
        resizeCanvas: function (t, e, i) {
            t.width = e,
            t.height = i,
            t.getContext("2d").clearRect(0, 0, e, i)
        },
        resizeCanvas_w: function (t, e, i) {
            t.width = e,
            t.height = i,
            t.getContext("2d").clearRect(0, 0, e, i),
            this.vail ? this.drawLottery() : this.drawMask()
        },
        drawPoint: function (t, e) {
            this.maskCtx.beginPath(),
            this.maskCtx.arc(t, e, 30, 0, 2 * Math.PI),
            this.maskCtx.fill(),
            this.maskCtx.beginPath(),
            this.maskCtx.lineWidth = 60,
            this.maskCtx.lineCap = this.maskCtx.lineJoin = "round",
            this.lastPosition && this.maskCtx.moveTo(this.lastPosition[0], this.lastPosition[1]),
            this.maskCtx.lineTo(t, e),
            this.maskCtx.stroke(),
            this.lastPosition = [t, e],
            this.mask.style.zIndex = 20 == this.mask.style.zIndex ? 21 : 20
        },
        bindEvent: function () {
            var t = this,
                e = /android|webos|iphone|ipad|ipod|blackberry|iemobile|opera mini/i.test(navigator.userAgent.toLowerCase()),
                i = e ? "touchstart" : "mousedown",
                n = e ? "touchmove" : "mousemove";
            if (e) t.conNode.addEventListener("touchmove", function (t) {
                s && t.preventDefault(),
                t.cancelable ? t.preventDefault() : window.event.returnValue = !1
            }, !1),
            t.conNode.addEventListener("touchend", function () {
                s = !1;
                var e = t.getTransparentPercent(t.maskCtx, t.width, t.height);
                e >= 30 && "function" == typeof t.drawPercentCallback && t.drawPercentCallback()
            }, !1);
            else {
                var s = !1;
                t.conNode.addEventListener("mouseup", function (e) {
                    e.preventDefault(),
                    s = !1;
                    var i = t.getTransparentPercent(t.maskCtx, t.width, t.height);
                    i >= 30 && "function" == typeof t.drawPercentCallback && t.drawPercentCallback()
                }, !1)
            }
            this.mask.addEventListener(i, function (i) {
                i.preventDefault(),
                s = !0;
                var n = e ? i.touches[0].pageX : i.pageX || i.x,
                    a = e ? i.touches[0].pageY : i.pageY || i.y;
                t.drawPoint(n, a, s)
            }, !1),
            this.mask.addEventListener(n, function (i) {
                if (i.preventDefault(), !s) return !1;
                i.preventDefault();
                var n = e ? i.touches[0].pageX : i.pageX || i.x,
                    a = e ? i.touches[0].pageY : i.pageY || i.y;
                t.drawPoint(n, a, s)
            }, !1)
        },
        drawLottery: function () {
            if ("image" == this.lotteryType) {
                var t = new Image,
                    e = this;
                t.onload = function () {
                    this.width = e.width,
                    this.height = e.height,
                    e.resizeCanvas(e.background, e.width, e.height),
                    e.backCtx.drawImage(this, 0, 0, e.width, e.height)
                },
                t.src = this.lottery.info
            } else if ("text" == this.lotteryType) {
                this.width = this.width,
                this.height = this.height,
                this.resizeCanvas(this.background, this.width, this.height),
                this.backCtx.save(),
                this.backCtx.fillStyle = "#FFF",
                this.backCtx.fillRect(0, 0, this.width, this.height),
                this.backCtx.restore(),
                this.backCtx.save();
                var i = 30;
                this.backCtx.font = "Bold " + i + "px Arial",
                this.backCtx.textAlign = "center",
                this.backCtx.fillStyle = "#F60",
                this.backCtx.fillText(this.lottery.info, this.width / 2, this.height / 2 + i / 2),
                this.backCtx.restore(),
                this.drawMask()
            }
        },
        drawMask: function () {
            if ("color" == this.coverType) this.maskCtx.fillStyle = this.cover,
            this.maskCtx.fillRect(0, 0, this.width, this.height),
            this.maskCtx.globalCompositeOperation = "destination-out";
            else if ("image" == this.coverType) {
                var t = new Image,
                    e = this;
                t.onload = function () {
                    e.resizeCanvas(e.mask, e.width, e.height);
                    /android/i.test(navigator.userAgent.toLowerCase());
                    e.maskCtx.globalAlpha = .98,
                    e.maskCtx.drawImage(this, 0, 0, this.width, this.height, 0, 0, e.width, e.height);
                    var t = 50,
                        i = "",
                        n = e.maskCtx.createLinearGradient(0, 0, e.width, 0);
                    n.addColorStop("0", "#fff"),
                    n.addColorStop("1.0", "#000"),
                    e.maskCtx.font = "Bold " + t + "px Arial",
                    e.maskCtx.textAlign = "left",
                    e.maskCtx.fillStyle = n,
                    e.maskCtx.fillText(i, e.width / 2 - e.maskCtx.measureText(i).width / 2, e.height - 100),
                    e.maskCtx.globalAlpha = 1,
                    e.maskCtx.globalCompositeOperation = "destination-out",
                    e.drawLottery()
                },
                t.crossOrigin = "anonymous",
                t.src = this.cover
            }
        },
        init: function (t, e) {
            t && (this.lottery.info = t, this.lottery.width = this.width, this.lottery.height = this.height, this.lotteryType = e || "image", this.vail = !0),
            this.vail && (this.background = this.background || this.createElement("canvas", {
                style: "position:fixed;left:50%;top:0;width:640px;margin-left:-320px;height:100%;background-color:transparent;"
            })),
            this.mask = this.mask || this.createElement("canvas", {
                style: "position:fixed;left:50%;top:0;width:640px;margin-left:-320px;height:100%;background-color:transparent;"
            }),
            this.mask.style.zIndex = 20,
            this.conNode.innerHTML.replace(/[\w\W]| /g, "") || (this.vail && this.conNode.appendChild(this.background), this.conNode.appendChild(this.mask), this.bindEvent()),
            this.vail && (this.backCtx = this.backCtx || this.background.getContext("2d")),
            this.maskCtx = this.maskCtx || this.mask.getContext("2d"),
            this.drawMask();
            var i = this;
            window.addEventListener("resize", function () {
                i.width = document.documentElement.clientWidth,
                i.height = document.documentElement.clientHeight,
                i.resizeCanvas_w(i.mask, i.width, i.height)
            }, !1)
        }
    },
    t
}),

function () {
    var t = {
        _audioNode: $(".js-audio"),
        _audio: null,
        _audio_val: !0,
        _videoNode: $(".j-video"),
        audio_init: function () {
            if (!(t._audioNode.length <= 0)) {
                var e = {
                    loop: !0,
                    preload: "auto",
                    src: this._audioNode.attr("data-src")
                };
                this._audio = new Audio;
                for (var i in e) e.hasOwnProperty(i) && i in this._audio && (this._audio[i] = e[i])
            }
        },
        audio_addEvent: function () {
            if (!(this._audioNode.length <= 0)) {
                var e = (this._audioNode.find(".txt_audio"), this._audioNode.find(".js-music-btn")),
                    i = this,
                    n = /android|webos|iphone|ipad|ipod|blackberry|iemobile|opera mini/i.test(navigator.userAgent.toLowerCase()),
                    s = n ? "touchstart" : "click";
                $(this._audioNode).on(s, function () {
                    i.audio_contorl()
                }),
                $(this._audio).on("play", function () {
                    t._audio_val = !1,
                    e.removeClass("ui-music-off"),
                    $.fn.coffee.start()
                }),
                $(this._audio).on("pause", function () {
                    t._audio_val = !0,
                    e.addClass("ui-music-off"),
                    $.fn.coffee.stop()
                })
            }
        },
        audio_contorl: function () {
            t._audio_val ? t.audio_play() : t.audio_stop()
        },
        audio_play: function () {
            t._audio_val = !1,
            t._audio && t._audio.play()
        },
        audio_stop: function () {
            t._audio_val = !0,
            t._audio && t._audio.pause()
        },
        video_init: function () {
            this._videoNode.each(function () {
                var t = {
                    controls: "controls",
                    preload: "none",
                    width: $(this).attr("data-width"),
                    height: $(this).attr("data-height"),
                    src: $(this).attr("data-src")
                },
                    e = $('<video class="f-hide"></video>')[0],
                    i = $(this).find(".img");
                for (var n in t) t.hasOwnProperty(n) && n in e && (e[n] = t[n]),
                this.appendChild(e);
                $(e).on("play", function () {
                    i.hide(),
                    $(e).removeClass("f-hide")
                }),
                $(e).on("pause", function () {
                    i.show(),
                    $(e).addClass("f-hide")
                })
            })
        },
        media_control: function () {
            this._audio && ($("video").length <= 0 || ($(this._audio).on("play", function () {
                $("video").each(function () {
                    this.paused || this.pause()
                })
            }), $("video").on("play", function () {
                t._audio_val || t.audio_contorl()
            }), $("video").on("pause", function () {
                t._audio_val && t.audio_contorl()
            })))
        },
        media_init: function () {
            this.audio_init(),
            this.video_init(),
            this.audio_addEvent(),
            this.media_control()
        }
    };
    window.Media = t
}(),
define("wap/showcase/modules/scroll/media", function () { }),

function (t) {
    t.fn.coffee = function (e) {
        function i() {
            var e = a(6, d.steamMaxSize),
                i = s(1, d.steamsFontFamily),
                n = "#" + s(6, "0123456789ABCDEF"),
                r = a(20, 40),
                h = a(-90, 89),
                l = o(.4, 1),
                c = ":rotate(" + h + "deg) scale(" + l + ");",
                f = "-webkit-transform" + c + "transform" + c,
                m = t('<span class="coffee-steam">' + s(1, d.steams) + "</span>"),
                p = a(0, 40),
                w = a(d.steamFlyTime / 2, 1.2 * d.steamFlyTime);
            m.css({
                position: "absolute",
                left: r,
                top: d.steamHeight,
                "font-size:": e + "px",
                color: n,
                "font-family": i,
                display: "block",
                opacity: 1
            }).attr("style", m.attr("style") + f).appendTo(u),
            m.size() && m.get(0).clientLeft,
            m.css({
                top: a(d.steamHeight / 2, 0),
                left: p,
                opacity: 0,
                transition: "all ease " + w / 1e3 + "s"
            }),
            setTimeout(function () {
                m.remove(),
                m = null
            }, w)
        }

        function n() {
            var t = a(0, 40);
            u.size() && u.get(0).clientLeft,
            u.css({
                left: t,
                transition: "all ease " + a(1e3, 3e3) / 1e3 + "s"
            })
        }

        function s(t, e) {
            t = t || 1;
            for (var i = "", n = e.length - 1, s = 0, o = 0; t > o; o++) s = a(0, n - 1),
            i += e.slice(s, s + 1);
            return i
        }

        function a(t, e) {
            var i = e - t,
                n = t + Math.round(Math.random() * i);
            return parseInt(n)
        }

        function o(t, e) {
            var i = e - t,
                n = t + Math.random() * i;
            return parseFloat(n)
        }
        var r = null,
            h = null,
            l = t(this),
            d = t.extend({}, t.fn.coffee.defaults, e),
            u = (d.steamWidth, t('<div class="coffee-steam-box"></div>').css({
                height: d.steamHeight,
                width: d.steamWidth,
                left: 20,
                top: -40,
                position: "absolute",
                overflow: "hidden",
                "z-index": 0
            }).appendTo(l));
        return t.fn.coffee.stop = function () {
            clearInterval(r),
            clearInterval(h)
        },
        t.fn.coffee.start = function () {
            r = setInterval(function () {
                i()
            }, a(d.steamInterval / 2, 2 * d.steamInterval)),
            h = setInterval(function () {
                n()
            }, a(100, 1e3) + a(1e3, 3e3))
        },
        l
    },
    t.fn.coffee.defaults = {
        steams: ["jQuery", "HTML5"],
        steamsFontFamily: ["Verdana", "Geneva", "Comic Sans MS", "MS Serif", "Lucida Sans Unicode", "Times New Roman", "Trebuchet MS", "Arial", "Courier New", "Georgia"],
        steamFlyTime: 5e3,
        steamInterval: 1e3,
        steamMaxSize: 10,
        steamHeight: 200,
        steamWidth: 300
    },
    t.fn.coffee.version = "2.0.0"
}($),
define("wap/showcase/modules/scroll/coffee", function () { }),
require(["wap/showcase/modules/swp/main", "wap/showcase/modules/swp/resource_loader", "wap/showcase/modules/scroll/lottery", "wap/showcase/modules/scroll/media", "wap/showcase/modules/scroll/coffee"], function (t, e, i) {
    function n(i) {
        this.wrap = i,
        this.posterContent = i.find(".js-poster-content"),
        this.loadingElem = i.find(".js-loading"),
        this.arrowElem = i.find(".js-up-arrow"),
        this.maskNodeElem = i.find(".js-mask"),
        this.coffeeElem = i.find(".js-beta-coffee"),
        this.coffeeAnimation = '<img src="http://kdt-static.qiniudn.com/v2/image/scroll/audio_widget_01@2x.png" />',
        this.resLoader = new e(this.posterContent);
        var n = i.find(".swp-wrap");
        this.swp = new t(n, {
            loop: n.data("loop") !== !1,
            flipway: n.data("flipway")
        })
    }
    n.prototype = {
        init: function () {
            var t = this;
            try {
                window.Media.media_init()
            } catch (e) { }
            $(window).on("beforeunload", function () {
                window.Media.audio_stop()
            }).on("touchmove", function (t) {
                t.preventDefault()
            }),
            this.resLoader.start().then(function () {
                return t.loadingElem.remove(),
                $.Deferred().resolveWith()
            }).then(this.drawMask.bind(this)).then(this.onStart.bind(this))
        },
        onStart: function () {
            var t = this;
            this.showArrow(),
            this.coffee(),
            window.Media.audio_play(),
            this.posterContent.removeClass("hide"),
            this.swp.init(),
            this.swp.scroll.on("pageChangeStart", function e() {
                t.showTimeout && window.clearTimeout(t.showTimeout),
                t.arrowElem.hide(),
                t.swp.scroll.removeListener("pageChangeStart", e)
            }),
            this.maskNodeElem.length || $("body").one("touchstart", function () {
                window.Media.audio_play()
            })
        },
        showArrow: function () {
            var t = this;
            this.showTimeout = window.setTimeout(function () {
                t.arrowElem.show()
            }, 2e3)
        },
        drawMask: function () {
            function t() {
                h.wrap.find(".js-mask canvas").remove(),
                e.resolve()
            }
            var e = new $.Deferred;
            if (!this.maskNodeElem.length) return e.resolveWith();
            var n = this.maskNodeElem.data("url"),
                s = "image",
                a = 640,
                o = $(window).height(),
                r = this.maskNodeElem[0],
                h = this;
            return this.lottery = new i(r, n, s, a, o, t).init(this.maskNodeElem.data("bg")),
            e.promise()
        },
        coffee: function () {
            this.coffeeElem.coffee({
                steams: [this.coffeeAnimation, this.coffeeAnimation],
                steamHeight: 100,
                steamWidth: 80
            })
        }
    };
    var s = new n($(".js-poster"));
    s.init()
}),
define("main", function () { });