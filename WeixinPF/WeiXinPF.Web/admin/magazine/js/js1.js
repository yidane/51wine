﻿!function (e, t)
{
    function n(e)
    {
        var t = e.length, n = ot.type(e);
        return ot.isWindow(e) ?!1 : 1 === e.nodeType && t ?!0 : "array" === n || "function" !== n && (0 === t || "number" == typeof t && t > 0 && t - 1 in e)
    }
    function r(e)
    {
        var t = ht[e] = {};
        return ot.each(e.match(at) || [], function (e, n)
        {
            t[n] = !0
        }), t
    }
    function i()
    {
        Object.defineProperty(this.cache = {}, 0, {
            get : function ()
            {
                return{}
            }
        }), this.expando = ot.expando + Math.random()
    }
    function o(e, n, r)
    {
        var i;
        if (r === t && 1 === e.nodeType)
        {
            if (i = "data-" + n.replace(vt, "-$1").toLowerCase(), r = e.getAttribute(i), "string" == typeof r) 
            {
                try 
                {
                    r = "true" === r ?!0 : "false" === r ?!1 : "null" === r ? null :+ r + "" === r ?+ r : yt.test(r) ? JSON.parse(r) : r 
                }
                catch (o) {}
                gt.set(e, n, r) 
            }
            else {
                r = t;
            }
            return r;
        }
    }
    function s()
    {
        return!0
    }
    function a()
    {
        return!1
    }
    function u()
    {
        try {
            return X.activeElement
        }
        catch (e) {}
    }
    function l(e, t)
    {
        for (; (e = e[t]) && 1 !== e.nodeType; ); return e
    }
    function c(e, t, n)
    {
        if (ot.isFunction(t)) {
            return ot.grep(e, function (e, r) 
            {
                return!!t.call(e, r, e) !== n 
            });
        }
        if (t.nodeType) {
            return ot.grep(e, function (e) 
            {
                return e === t !== n;
            });
        }
        if ("string" == typeof t) {
            if (St.test(t)) {
                return ot.filter(t, e, n);
            }
            t = ot.filter(t, e)
        }
        return ot.grep(e, function (e)
        {
            return tt.call(t, e) >= 0 !== n;
        })
    }
    function f(e, t)
    {
        return ot.nodeName(e, "table") && ot.nodeName(1 === t.nodeType ? t : t.firstChild, "tr") ? e.getElementsByTagName("tbody")[0] || e.appendChild(e.ownerDocument.createElement("tbody")) : e
    }
    function p(e)
    {
        return e.type = (null !== e.getAttribute("type")) + "/" + e.type, e
    }
    function d(e)
    {
        var t = Wt.exec(e.type);
        return t ? e.type = t[1] : e.removeAttribute("type"), e
    }
    function h(e, t)
    {
        for (var n = e.length, r = 0; n > r; r++) {
            mt.set(e[r], "globalEval", !t || mt.get(t[r], "globalEval"));
        }
    }
    function g(e, t)
    {
        var n, r, i, o, s, a, u, l;
        if (1 === t.nodeType)
        {
            if (mt.hasData(e) && (o = mt.access(e), s = mt.set(t, o), l = o.events))
            {
                delete s.handle, s.events = {};
                for (i in l) {
                    for (n = 0, r = l[i].length; r > n; n++) {
                        ot.event.add(t, i, l[i][n]);
                    }
                }
            }
            gt.hasData(e) && (a = gt.access(e), u = ot.extend({}, a), gt.set(t, u))
        }
    }
    function m(e, n)
    {
        var r = e.getElementsByTagName ? e.getElementsByTagName(n || "*") : e.querySelectorAll ? e.querySelectorAll(n || "*") : [];
        return n === t || n && ot.nodeName(e, n) ? ot.merge([e], r) : r
    }
    function y(e, t)
    {
        var n = t.nodeName.toLowerCase();
        "input" === n && Pt.test(e.type) ? t.checked = e.checked : ("input" === n || "textarea" === n) && (t.defaultValue = e.defaultValue)
    }
    function v(e, t)
    {
        if (t in e) {
            return t;
        }
        for (var n = t.charAt(0).toUpperCase() + t.slice(1), r = t, i = Zt.length; i--; ) {
            if (t = Zt[i] + n, t in e) {
                return t;
            }
            return r;
        }
    }
    function x(e, t)
    {
        return e = t || e, "none" === ot.css(e, "display") || !ot.contains(e.ownerDocument, e)
    }
    function b(t)
    {
        return e.getComputedStyle(t, null)
    }
    function w(e, t)
    {
        for (var n, r, i, o = [], s = 0, a = e.length; a > s; s++)
        {
            r = e[s], r.style && (o[s] = mt.get(r, "olddisplay"), n = r.style.display, t ? (o[s] || "none" !== n || (r.style.display = ""), 
            "" === r.style.display && x(r) && (o[s] = mt.access(r, "olddisplay", N(r.nodeName)))) : o[s] || (i = x(r), 
            (n && "none" !== n || !i) && mt.set(r, "olddisplay", i ? n : ot.css(r, "display"))));
        }
        for (s = 0; a > s; s++)
        {
            r = e[s], r.style && (t && "none" !== r.style.display && "" !== r.style.display || (r.style.display = t ? o[s] || "" : "none"));
        }
        return e
    }
    function T(e, t, n)
    {
        var r = Ut.exec(t);
        return r ? Math.max(0, r[1] - (n || 0)) + (r[2] || "px") : t
    }
    function C(e, t, n, r, i)
    {
        for (var o = n === (r ? "border" : "content") ? 4 : "width" === t ? 1 : 0, s = 0; 4 > o; o += 2)
        {
            "margin" === n && (s += ot.css(e, n + Kt[o], !0, i)), r ? ("content" === n && (s -= ot.css(e, 
            "padding" + Kt[o], !0, i)), "margin" !== n && (s -= ot.css(e, "border" + Kt[o] + "Width", 
            !0, i))) : (s += ot.css(e, "padding" + Kt[o], !0, i), "padding" !== n && (s += ot.css(e, "border" + Kt[o] + "Width", 
            !0, i)));
        }
        return s
    }
    function k(e, t, n)
    {
        var r = !0, i = "width" === t ? e.offsetWidth : e.offsetHeight, o = b(e), s = ot.support.boxSizing && "border-box" === ot.css(e, 
        "boxSizing", !1, o);
        if (0 >= i || null == i)
        {
            if (i = It(e, t, o), (0 > i || null == i) && (i = e.style[t]), Yt.test(i)) {
                return i;
            }
            r = s && (ot.support.boxSizingReliable || i === e.style[t]), i = parseFloat(i) || 0
        }
        return i + C(e, t, n || (s ? "border" : "content"), r, o) + "px"
    }
    function N(e)
    {
        var t = X, n = Gt[e];
        return n || (n = j(e, t), "none" !== n && n || (zt = (zt || ot("<iframe frameborder='0' width='0' height='0'/>").css("cssText", 
        "display:block !important")).appendTo(t.documentElement), t = (zt[0].contentWindow || zt[0].contentDocument).document, 
        t.write("<!doctype html><html><body>"), t.close(), n = j(e, t), zt.detach()), Gt[e] = n), n
    }
    function j(e, t)
    {
        var n = ot(t.createElement(e)).appendTo(t.body), r = ot.css(n[0], "display");
        return n.remove(), r
    }
    function E(e, t, n, r)
    {
        var i;
        if (ot.isArray(t))
        {
            ot.each(t, function (t, i) 
            {
                n || tn.test(e) ? r(e, i) : E(e + "[" + ("object" == typeof i ? t : "") + "]", i, n, r) 
            });
        }
        else if (n || "object" !== ot.type(t)) {
            r(e, t);
        }
        else {
            for (i in t) {
                E(e + "[" + i + "]", t[i], n, r);
            }
        }
    }
    function S(e)
    {
        return function (t, n)
        {
            "string" != typeof t && (n = t, t = "*");
            var r, i = 0, o = t.toLowerCase().match(at) || [];
            if (ot.isFunction(n))
            {
                for (; r = o[i++]; )
                {
                    "+" === r[0] ? (r = r.slice(1) || "*", (e[r] = e[r] || []).unshift(n)) : (e[r] = e[r] || []).push(n);
                }
            }
        }
    }
    function D(e, t, n, r)
    {
        function i(a)
        {
            var u;
            return o[a] = !0, ot.each(e[a] || [], function (e, a)
            {
                var l = a(t, n, r);
                return "string" != typeof l || s || o[l] ? s ?!(u = l) : void 0 : (t.dataTypes.unshift(l), 
                i(l), !1);
            }), u
        }
        var o = {}, s = e === xn;
        return i(t.dataTypes[0]) || !o["*"] && i("*")
    }
    function A(e, n)
    {
        var r, i, o = ot.ajaxSettings.flatOptions || {};
        for (r in n) {
            n[r] !== t && ((o[r] ? e : i || (i = {}))[r] = n[r]);
        }
        return i && ot.extend(!0, e, i), e
    }
    function q(e, n, r)
    {
        for (var i, o, s, a, u = e.contents, l = e.dataTypes; "*" === l[0]; ) {
            l.shift(), i === t && (i = e.mimeType || n.getResponseHeader("Content-Type"));
        }
        if (i) {
            for (o in u) {
                if (u[o] && u[o].test(i)) {
                    l.unshift(o);
                    break 
                }
                if (l[0]in r) {
                    s = l[0];
                };
            }
        }
        else {
            for (o in r) {
                if (!l[0] || e.converters[o + " " + l[0]]) {
                    s = o;
                    break
                }
                a || (a = o)
            }
            s = s || a
        }
        return s ? (s !== l[0] && l.unshift(s), r[s]) : void 0
    }
    function L(e, t, n, r)
    {
        var i, o, s, a, u, l = {}, c = e.dataTypes.slice();
        if (c[1]) {
            for (s in e.converters) {
                l[s.toLowerCase()] = e.converters[s];
            }
        }
        for (o = c.shift(); o; ) if (e.responseFields[o] && (n[e.responseFields[o]] = t), !u && r && e.dataFilter && (t = e.dataFilter(t, 
        e.dataType)), u = o, o = c.shift()) if ("*" === o) o = u;
        else if ("*" !== u && u !== o)
        {
            if (s = l[u + " " + o] || l["* " + o], !s)
            {
                for (i in l)
                {
                    if (a = i.split(" "), a[1] === o && (s = l[u + " " + a[0]] || l["* " + a[0]])) {
                        s === !0 ? s = l[i] : l[i] !== !0 && (o = a[0], c.unshift(a[1]));
                        break 
                    }
                    if (s !== !0) {
                        if (s && e["throws"]) {
                            t = s(t);
                        };;
                    }
                }
            }
            else
            {
                try {
                    t = s(t) 
                }
                catch (f) {
                    return {
                        state : "parsererror", error : s ? f : "No conversion from " + u + " to " + o 
                    }
                }
            }
        }
        return {
            state : "success", data : t
        }
    }
    function H()
    {
        return setTimeout(function ()
        {
            Sn = t;
        }), Sn = ot.now()
    }
    function O(e, t, n)
    {
        for (var r, i = (On[t] || []).concat(On["*"]), o = 0, s = i.length; s > o; o++) {
            if (r = i[o].call(n, t, e)) {
                return r;
            }
        }
    }
    function F(e, t, n)
    {
        var r, i, o = 0, s = Hn.length, a = ot.Deferred().always(function ()
        {
            delete u.elem
        }), u = function ()
        {
            if (i) {
                return!1;
            }
            for (var t = Sn || H(), n = Math.max(0, l.startTime + l.duration - t), r = n / l.duration || 0, 
            o = 1 - r, s = 0, u = l.tweens.length;
            u > s;
            s++) l.tweens[s].run(o);
            return a.notifyWith(e, [l, o, n]), 1 > o && u ? n : (a.resolveWith(e, [l]), !1);
        },
        l = a.promise(
        {
            elem : e, props : ot.extend({}, t), opts : ot.extend(!0, {
                specialEasing : {}
            }, n), originalProperties : t, originalOptions : n, startTime : Sn || H(), duration : n.duration, 
            tweens : [],
            createTween : function (t, n)
            {
                var r = ot.Tween(e, l.opts, t, n, l.opts.specialEasing[t] || l.opts.easing);
                return l.tweens.push(r), r;
            },
            stop : function (t)
            {
                var n = 0, r = t ? l.tweens.length : 0;
                if (i) {
                    return this;
                }
                for (i = !0; r > n; n++) {
                    l.tweens[n].run(1);
                }
                return t ? a.resolveWith(e, [l, t]) : a.rejectWith(e, [l, t]), this;
            }
        }), c = l.props;
        for (P(c, l.opts.specialEasing); s > o; o++)
        {
            if (r = Hn[o].call(l, e, c, l.opts)) {
                return r;
            }
            return ot.map(c, O, l), ot.isFunction(l.opts.start) && l.opts.start.call(e, l), ot.fx.timer(ot.extend(u, 
            {
                elem : e, anim : l, queue : l.opts.queue 
            })), l.progress(l.opts.progress).done(l.opts.done, l.opts.complete).fail(l.opts.fail).always(l.opts.always);
        }
    }
    function P(e, t)
    {
        var n, r, i, o, s;
        for (n in e)
        {
            if (r = ot.camelCase(n), i = t[r], o = e[n], ot.isArray(o) && (i = o[1], o = e[n] = o[0]), 
            n !== r && (e[r] = o, delete e[n]), s = ot.cssHooks[r], s && "expand"in s) {
                o = s.expand(o), delete e[r];
                for (n in o) {
                    n in e || (e[n] = o[n], t[n] = i) ;
                }
            }
            else {
                t[r] = i;
            }
        }
    }
    function R(e, n, r)
    {
        var i, o, s, a, u, l, c = this, f = {}, p = e.style, d = e.nodeType && x(e), h = mt.get(e, "fxshow");
        r.queue || (u = ot._queueHooks(e, "fx"), null == u.unqueued && (u.unqueued = 0, l = u.empty.fire, 
        u.empty.fire = function ()
        {
            u.unqueued || l()
        }), u.unqueued++, c.always(function ()
        {
            c.always(function ()
            {
                u.unqueued--, ot.queue(e, "fx").length || u.empty.fire()
            })
        })), 1 === e.nodeType && ("height"in n || "width"in n) && (r.overflow = [p.overflow, p.overflowX, 
        p.overflowY], "inline" === ot.css(e, "display") && "none" === ot.css(e, "float") && (p.display = "inline-block")), 
        r.overflow && (p.overflow = "hidden", c.always(function ()
        {
            p.overflow = r.overflow[0], p.overflowX = r.overflow[1], p.overflowY = r.overflow[2];
        }));
        for (i in n) if (o = n[i], An.exec(o))
        {
            if (delete n[i], s = s || "toggle" === o, o === (d ? "hide" : "show")) {
                if ("show" !== o || !h || h[i] === t) {
                    continue;
                }
                d = !0
            }
            f[i] = h && h[i] || ot.style(e, i)
        }
        if (!ot.isEmptyObject(f))
        {
            h ? "hidden"in h && (d = h.hidden) : h = mt.access(e, "fxshow", {}), s && (h.hidden = !d), 
            d ? ot(e).show() : c.done(function ()
            {
                ot(e).hide()
            }), c.done(function ()
            {
                var t;
                mt.remove(e, "fxshow");
                for (t in f) {
                    ot.style(e, t, f[t]);
                }
            });
            for (i in f)
            {
                a = O(d ? h[i] : 0, i, c), i in h || (h[i] = a.start, d && (a.end = a.start, a.start = "width" === i || "height" === i ? 1 : 0));
            }
        }
    }
    function M(e, t, n, r, i)
    {
        return new M.prototype.init(e, t, n, r, i)
    }
    function W(e, t)
    {
        var n, r = {
            height : e
        },
        i = 0;
        for (t = t ? 1 : 0; 4 > i; i += 2 - t) {
            n = Kt[i], r["margin" + n] = r["padding" + n] = e;
        }
        return t && (r.opacity = r.width = e), r
    }
    function $(e)
    {
        return ot.isWindow(e) ? e : 9 === e.nodeType && e.defaultView
    }
    var B, I, z = typeof t, _ = e.location, X = e.document, U = X.documentElement, Y = e.jQuery, V = e.$, 
    G = {}, Q = [], J = "2.0.3", K = Q.concat, Z = Q.push, et = Q.slice, tt = Q.indexOf, nt = G.toString, 
    rt = G.hasOwnProperty, it = J.trim, ot = function (e, t)
    {
        return new ot.fn.init(e, t, B);
    },
    st = /[+-]?(?:\d*\.|)\d+(?:[eE][+-]?\d+|)/.source, at = /\S+/g, ut = /^(?:\s*(<[\w\W]+>)[^>]*|#([\w-]*))$/, 
    lt = /^<(\w+)\s*\/?>(?:<\/\1>|)$/, ct = /^-ms-/, ft = /-([\da-z])/gi, pt = function (e, t)
    {
        return t.toUpperCase();
    },
    dt = function ()
    {
        X.removeEventListener("DOMContentLoaded", dt, !1), e.removeEventListener("load", dt, !1), ot.ready()
    };
    ot.fn = ot.prototype = 
    {
        jquery : J, constructor : ot,
        init : function (e, n, r)
        {
            var i, o;
            if (!e) {
                return this;
            }
            if ("string" == typeof e)
            {
                if (i = "<" === e.charAt(0) && ">" === e.charAt(e.length - 1) && e.length >= 3 ? [null, 
                e, null] : ut.exec(e), !i || !i[1] && n) return!n || n.jquery ? (n || r).find(e) : this.constructor(n).find(e);
                if (i[1])
                {
                    if (n = n instanceof ot ? n[0] : n, ot.merge(this, ot.parseHTML(i[1], n && n.nodeType ? n.ownerDocument || n : X, 
                    !0)), lt.test(i[1]) && ot.isPlainObject(n)) for (i in n) ot.isFunction(this [i]) ? this [i](n[i]) : this.attr(i, 
                    n[i]);
                    return this
                }
                return o = X.getElementById(i[2]), o && o.parentNode && (this.length = 1, this [0] = o), 
                this.context = X, this.selector = e, this
            }
            return e.nodeType ? (this.context = this [0] = e, this.length = 1, this) : ot.isFunction(e) ? r.ready(e) : (e.selector !== t && (this.selector = e.selector, 
            this.context = e.context), ot.makeArray(e, this));
        },
        selector : "", length : 0,
        toArray : function ()
        {
            return et.call(this);
        },
        get : function (e)
        {
            return null == e ? this.toArray() : 0 > e ? this [this.length + e] : this [e];
        },
        pushStack : function (e)
        {
            var t = ot.merge(this.constructor(), e);
            return t.prevObject = this, t.context = this.context, t;
        },
        each : function (e, t)
        {
            return ot.each(this, e, t);
        },
        ready : function (e)
        {
            return ot.ready.promise().done(e), this;
        },
        slice : function ()
        {
            return this.pushStack(et.apply(this, arguments));
        },
        first : function ()
        {
            return this.eq(0);
        },
        last : function ()
        {
            return this.eq(-1);
        },
        eq : function (e)
        {
            var t = this.length, n =+ e + (0 > e ? t : 0);
            return this.pushStack(n >= 0 && t > n ? [this [n]] : []);
        },
        map : function (e)
        {
            return this.pushStack(ot.map(this, function (t, n)
            {
                return e.call(t, n, t);
            }))
        },
        end : function ()
        {
            return this.prevObject || this.constructor(null);
        },
        push : Z, sort : [].sort, splice : [].splice
    },
    ot.fn.init.prototype = ot.fn, ot.extend = ot.fn.extend = function ()
    {
        var e, n, r, i, o, s, a = arguments[0] || {}, u = 1, l = arguments.length, c = !1;
        for ("boolean" == typeof a && (c = a, a = arguments[1] || {}, u = 2), "object" == typeof a || ot.isFunction(a) || (a = {}), 
        l === u && (a = this, --u);
        l > u;
        u++) if (null != (e = arguments[u])) for (n in e) r = a[n], i = e[n], a !== i && (c && i && (ot.isPlainObject(i) || (o = ot.isArray(i))) ? (o ? (o = !1, 
        s = r && ot.isArray(r) ? r : []) : s = r && ot.isPlainObject(r) ? r : {},
        a[n] = ot.extend(c, s, i)) : i !== t && (a[n] = i));
        return a;
    },
    ot.extend(
    {
        expando : "jQuery" + (J + Math.random()).replace(/\D/g, ""),
        noConflict : function (t)
        {
            return e.$ === ot && (e.$ = V), t && e.jQuery === ot && (e.jQuery = Y), ot;
        },
        isReady :!1, readyWait : 1,
        holdReady : function (e)
        {
            e ? ot.readyWait++: ot.ready(!0)
        },
        ready : function (e)
        {
            (e === !0 ?--ot.readyWait : ot.isReady) || (ot.isReady = !0, e !== !0 &&--ot.readyWait > 0 || (I.resolveWith(X, 
            [ot]), ot.fn.trigger && ot(X).trigger("ready").off("ready")))
        },
        isFunction : function (e)
        {
            return "function" === ot.type(e);
        },
        isArray : Array.isArray,
        isWindow : function (e)
        {
            return null != e && e === e.window;
        },
        isNumeric : function (e)
        {
            return!isNaN(parseFloat(e)) && isFinite(e)
        },
        type : function (e)
        {
            return null == e ? String(e) : "object" == typeof e || "function" == typeof e ? G[nt.call(e)] || "object" : typeof e;
        },
        isPlainObject : function (e)
        {
            if ("object" !== ot.type(e) || e.nodeType || ot.isWindow(e)) {
                return!1;
            }
            try {
                if (e.constructor && !rt.call(e.constructor.prototype, "isPrototypeOf")) {
                    return!1;
                }
            }
            catch (t) {
                return!1
            }
            return!0
        },
        isEmptyObject : function (e)
        {
            var t;
            for (t in e) {
                return!1;
            }
            return!0
        },
        error : function (e)
        {
            throw new Error(e)
        },
        parseHTML : function (e, t, n)
        {
            if (!e || "string" != typeof e) {
                return null;
            }
            "boolean" == typeof t && (n = t, t = !1), t = t || X;
            var r = lt.exec(e), i = !n && [];
            return r ? [t.createElement(r[1])] : (r = ot.buildFragment([e], t, i), i && ot(i).remove(), 
            ot.merge([], r.childNodes));
        },
        parseJSON : JSON.parse,
        parseXML : function (e)
        {
            var n, r;
            if (!e || "string" != typeof e) {
                return null;
            }
            try {
                r = new DOMParser, n = r.parseFromString(e, "text/xml")
            }
            catch (i) {
                n = t
            }
            return (!n || n.getElementsByTagName("parsererror").length) && ot.error("Invalid XML: " + e), 
            n;
        },
        noop : function () {},
        globalEval : function (e)
        {
            var t, n = eval;
            e = ot.trim(e), e && (1 === e.indexOf("use strict") ? (t = X.createElement("script"), t.text = e, 
            X.head.appendChild(t).parentNode.removeChild(t)) : n(e));
        },
        camelCase : function (e)
        {
            return e.replace(ct, "ms-").replace(ft, pt);
        },
        nodeName : function (e, t)
        {
            return e.nodeName && e.nodeName.toLowerCase() === t.toLowerCase();
        },
        each : function (e, t, r)
        {
            var i, o = 0, s = e.length, a = n(e);
            if (r)
            {
                if (a) {
                    for (; s > o && (i = t.apply(e[o], r), i !== !1); o++); 
                }
                else {
                    for (o in e) {
                        if (i = t.apply(e[o], r), i === !1) {
                            break;
                        };
                    }
                }
            }
            else if (a) {
                for (; s > o && (i = t.call(e[o], o, e[o]), i !== !1); o++); 
            }
            else {
                for (o in e) {
                    if (i = t.call(e[o], o, e[o]), i === !1) {
                        break;
                    };
                }
            }
            return e;
        },
        trim : function (e)
        {
            return null == e ? "" : it.call(e);
        },
        makeArray : function (e, t)
        {
            var r = t || [];
            return null != e && (n(Object(e)) ? ot.merge(r, "string" == typeof e ? [e] : e) : Z.call(r, 
            e)), r
        },
        inArray : function (e, t, n)
        {
            return null == t ?- 1 : tt.call(t, e, n);
        },
        merge : function (e, n)
        {
            var r = n.length, i = e.length, o = 0;
            if ("number" == typeof r) {
                for (; r > o; o++) {
                    e[i++] = n[o];
                }
            }
            else {
                for (; n[o] !== t; ) {
                    e[i++] = n[o++];
                }
            }
            return e.length = i, e;
        },
        grep : function (e, t, n)
        {
            var r, i = [], o = 0, s = e.length;
            for (n = !!n; s > o; o++) {
                r = !!t(e[o], o), n !== r && i.push(e[o]);
            }
            return i;
        },
        map : function (e, t, r)
        {
            var i, o = 0, s = e.length, a = n(e), u = [];
            if (a) {
                for (; s > o; o++) {
                    i = t(e[o], o, r), null != i && (u[u.length] = i);
                }
            }
            else {
                for (o in e) {
                    i = t(e[o], o, r), null != i && (u[u.length] = i);
                }
            }
            return K.apply([], u);
        },
        guid : 1,
        proxy : function (e, n)
        {
            var r, i, o;
            return "string" == typeof n && (r = e[n], n = e, e = r), ot.isFunction(e) ? (i = et.call(arguments, 
            2), o = function ()
            {
                return e.apply(n || this, i.concat(et.call(arguments)));
            },
            o.guid = e.guid = e.guid || ot.guid++, o) : t;
        },
        access : function (e, n, r, i, o, s, a)
        {
            var u = 0, l = e.length, c = null == r;
            if ("object" === ot.type(r)) {
                o = !0;
                for (u in r) {
                    ot.access(e, n, u, r[u], !0, s, a);
                }
            }
            else if (i !== t && (o = !0, ot.isFunction(i) || (a = !0), c && (a ? (n.call(e, i), n = null) : (c = n, {
            n = function (e, t, n)for (;
            {l > u;
                return c.call(ot(e), n);u++) n(e[u], r, a ? i : i.call(e[u], u, n(e[u], r)));
            })), n))
            }
            return o ? e : c ? n.call(e) : l ? n(e[0], r) : s;
},
now : Date.now,
swap : function (e, t, n, r)
{
    var i, o, s = {};
    for (o in t) {
        s[o] = e.style[o], e.style[o] = t[o];
    }
    i = n.apply(e, r || []);
    for (o in t) {
        e.style[o] = s[o];
    }
    return i;
}
}), ot.ready.promise = function (t)
{
    return I || (I = ot.Deferred(), "complete" === X.readyState ? setTimeout(ot.ready) : (X.addEventListener("DOMContentLoaded", 
    dt, !1), e.addEventListener("load", dt, !1))), I.promise(t);
},
ot.each("Boolean Number String Function Array Date RegExp Object Error".split(" "), function (e, t)
{
    G["[object " + t + "]"] = t.toLowerCase();
}), B = ot(X), function (e, t)
{
    function n(e, t, n, r)
    {
        var i, o, s, a, u, l, c, f, h, g;
        if ((t ? t.ownerDocument || t : $) !== L && q(t), t = t || L, n = n || [], !e || "string" != typeof e) {
            return n;
        }
        if (1 !== (a = t.nodeType) && 9 !== a) {
            return [];
        }
        if (O && !r)
        {
            if (i = xt.exec(e)) if (s = i[1])
            {
                if (9 === a) {
                    if (o = t.getElementById(s), !o || !o.parentNode) {
                        return n;
                    }
                    if (o.id === s) {
                        return n.push(o), n;
                    }
                }
                else if (t.ownerDocument && (o = t.ownerDocument.getElementById(s)) && M(t, o) && o.id === s) {
                    return n.push(o), n;
                }
            }
            else
            {
                if (i[2]) {
                    return et.apply(n, t.getElementsByTagName(e)), n;
                }
                if ((s = i[3]) && C.getElementsByClassName && t.getElementsByClassName) {
                    return et.apply(n, t.getElementsByClassName(s)), n;
                }
            }
            if (C.qsa && (!F || !F.test(e)))
            {
                if (f = c = W, h = t, g = 9 === a && e, 1 === a && "object" !== t.nodeName.toLowerCase())
                {
                    for (l = p(e), (c = t.getAttribute("id")) ? f = c.replace(Tt, "\\$&") : t.setAttribute("id", 
                    f), f = "[id='" + f + "'] ", u = l.length;
                    u--;
                    ) l[u] = f + d(l[u]);
                    h = dt.test(e) && t.parentNode || t, g = l.join(",")
                }
                if (g) {
                    try {
                        return et.apply(n, h.querySelectorAll(g)), n 
                    }
                    catch (m) {}
                }
                finally {
                    c || t.removeAttribute("id")
            }
        }
    }
    return w(e.replace(ct, "$1"), t, n, r)
}
function r()
{
    function e(n, r)
    {
        return t.push(n += " ") > N.cacheLength && delete e[t.shift()], e[n] = r
    }
    var t = [];
    return e
}
function i(e)
{
    return e[W] = !0, e
}
function o(e)
{
    var t = L.createElement("div");
    try {
        return!!e(t)
    }
    catch (n) {
        return!1
    }
    finally {
        t.parentNode && t.parentNode.removeChild(t), t = null;
    }
}
function s(e, t)
{
    for (var n = e.split("|"), r = e.length; r--; ) {
        N.attrHandle[n[r]] = t;
    }
}
function a(e, t)
{
    var n = t && e, r = n && 1 === e.nodeType && 1 === t.nodeType && (~t.sourceIndex || G) - (~e.sourceIndex || G);
    if (r) {
        return r;
    }
    if (n) {
        for (; n = n.nextSibling; ) {
            if (n === t) {
                return - 1;
            };
        }
    }
    return e ? 1 :- 1
}
function u(e)
{
    return function (t)
    {
        var n = t.nodeName.toLowerCase();
        return "input" === n && t.type === e;
    }
}
function l(e)
{
    return function (t)
    {
        var n = t.nodeName.toLowerCase();
        return ("input" === n || "button" === n) && t.type === e;
    }
}
function c(e)
{
    return i(function (t)
    {
        return t =+ t, i(function (n, r)
        {
            for (var i, o = e([], n.length, t), s = o.length; s--; ) {
                n[i = o[s]] && (n[i] = !(r[i] = n[i]));
            }
        })
    })
}
function f() {}
function p(e, t)
{
    var r, i, o, s, a, u, l, c = _[e + " "];
    if (c) {
        return t ? 0 : c.slice(0);
    }
    for (a = e, u = [], l = N.preFilter; a; )
    {
        (!r || (i = ft.exec(a))) && (i && (a = a.slice(i[0].length) || a), u.push(o = [])), r = !1, 
        (i = pt.exec(a)) && (r = i.shift(), o.push({
            value : r, type : i[0].replace(ct, " ")
        }), a = a.slice(r.length));
        for (s in N.filter)
        {
            !(i = yt[s].exec(a)) || l[s] && !(i = l[s](i)) || (r = i.shift(), o.push({
                value : r, type : s, matches : i 
            }), a = a.slice(r.length));
        }
        if (!r) {
            break;
        }
    }
    return t ? a.length : a ? n.error(e) : _(e, u).slice(0)
}
function d(e)
{
    for (var t = 0, n = e.length, r = ""; n > t; t++) {
        r += e[t].value;
    }
    return r
}
function h(e, t, n)
{
    var r = t.dir, i = n && "parentNode" === r, o = I++;
    return t.first ? function (t, n, o)
    {
        for (; t = t[r]; ) {
            if (1 === t.nodeType || i) {
                return e(t, n, o);
            }
        }
    }
     : function (t, n, s)
     {
         var a, u, l, c = B + " " + o;
         if (s) {
             for (; t = t[r]; ) {
                 if ((1 === t.nodeType || i) && e(t, n, s)) {
                     return!0;
                 }
             }
         }
         else
         {
             for (; t = t[r]; )
             {
                 if (1 === t.nodeType || i)
                 {
                     if (l = t[W] || (t[W] = {}), (u = l[r]) && u[0] === c) {
                         if ((a = u[1]) === !0 || a === k) {
                             return a === !0 ;
                         }
                     }
                     else if (u = l[r] = [c], u[1] = e(t, n, s) || k, u[1] === !0) {
                         return!0;
                     };;
                 }
             }
         }
     }
}
function g(e)
{
    return e.length > 1 ? function (t, n, r)
    {
        for (var i = e.length; i--; ) {
            if (!e[i](t, n, r)) {
                return!1;
            }
            return!0;
        }
    }
     : e[0]
}
function m(e, t, n, r, i)
{
    for (var o, s = [], a = 0, u = e.length, l = null != t; u > a; a++) {
        (o = e[a]) && (!n || n(o, r, i)) && (s.push(o), l && t.push(a));
    }
    return s
}
function y(e, t, n, r, o, s)
{
    return r && !r[W] && (r = y(r)), o && !o[W] && (o = y(o, s)), i(function (i, s, a, u)
    {
        var l, c, f, p = [], d = [], h = s.length, g = i || b(t || "*", a.nodeType ? [a] : a, 
        []), y = !e || !i && t ? g : m(g, p, e, a, u), v = n ? o || (i ? e : h || r) ? [] : s : y;
        if (n && n(y, v, a, u), r) {
            for (l = m(v, d), r(l, [], a, u), c = l.length; c--; ) {
                (f = l[c]) && (v[d[c]] = !(y[d[c]] = f));
            }
        }
        if (i)
        {
            if (o || e)
            {
                if (o) {
                    for (l = [], c = v.length; c--; ) {
                        (f = v[c]) && l.push(y[c] = f);
                    }
                    o(null, v = [], l, u)
                }
                for (c = v.length; c--; ) {
                    (f = v[c]) && (l = o ? nt.call(i, f) : p[c]) > - 1 && (i[l] = !(s[l] = f));
                }
            }
        }
        else {
            v = m(v === s ? v.splice(h, v.length) : v), o ? o(null, s, v, u) : et.apply(s, v);
        }
    })
}
function v(e)
{
    for (var t, n, r, i = e.length, o = N.relative[e[0].type], s = o || N.relative[" "], a = o ? 1 : 0, 
    u = h(function (e)
    {
        return e === t;
    },
    s, !0), l = h(function (e)
    {
        return nt.call(t, e) > -1;
    },
    s, !0), c = [function (e, n, r)
    {
        return!o && (r || n !== D) || ((t = n).nodeType ? u(e, n, r) : l(e, n, r))
    }];
    i > a;
    a++) if (n = N.relative[e[a].type]) c = [h(g(c), n)];
    else
    {
        if (n = N.filter[e[a].type].apply(null, e[a].matches), n[W])
        {
            for (r =++a; i > r && !N.relative[e[r].type]; r++); return y(a > 1 && g(c), a > 1 && d(e.slice(0, 
            a - 1).concat({
                value : " " === e[a - 2].type ? "*" : ""
            })).replace(ct, "$1"), n, r > a && v(e.slice(a, r)), i > r && v(e = e.slice(r)), i > r && d(e))
        }
        c.push(n)
    }
    return g(c)
}
function x(e, t)
{
    var r = 0, o = t.length > 0, s = e.length > 0, a = function (i, a, u, l, c)
    {
        var f, p, d, h = [], g = 0, y = "0", v = i && [], x = null != c, b = D, w = i || s && N.find.TAG("*", 
        c && a.parentNode || a), T = B += null == b ? 1 : Math.random() || .1;
        for (x && (D = a !== L && a, k = r); null != (f = w[y]); y++)
        {
            if (s && f) {
                for (p = 0; d = e[p++]; ) {
                    if (d(f, a, u)) {
                        l.push(f);
                        break 
                    }
                    x && (B = T, k =++r);
                }
            }
            o && ((f = !d && f) && g--, i && v.push(f))
        }
        if (g += y, o && y !== g)
        {
            for (p = 0; d = t[p++]; ) {
                d(v, h, a, u);
            }
            if (i) {
                if (g > 0) {
                    for (; y--; ) {
                        v[y] || h[y] || (h[y] = K.call(l));
                    }
                }
                h = m(h)
            }
            et.apply(l, h), x && !i && h.length > 0 && g + t.length > 1 && n.uniqueSort(l)
        }
        return x && (B = T, D = b), v;
    };
    return o ? i(a) : a
}
function b(e, t, r)
{
    for (var i = 0, o = t.length; o > i; i++) {
        n(e, t[i], r);
    }
    return r
}
function w(e, t, n, r)
{
    var i, o, s, a, u, l = p(e);
    if (!r && 1 === l.length)
    {
        if (o = l[0] = l[0].slice(0), o.length > 2 && "ID" === (s = o[0]).type && C.getById && 9 === t.nodeType && O && N.relative[o[1].type])
        {
            if (t = (N.find.ID(s.matches[0].replace(Ct, kt), t) || [])[0], !t) {
                return n;
            }
            e = e.slice(o.shift().value.length)
        }
        for (i = yt.needsContext.test(e) ? 0 : o.length; i--&& (s = o[i], !N.relative[a = s.type]); ) if ((u = N.find[a]) && (r = u(s.matches[0].replace(Ct, 
        kt), dt.test(o[0].type) && t.parentNode || t))) {
            if (o.splice(i, 1), e = r.length && d(o), !e) {
                return et.apply(n, r), n;
            }
            break
        }
    }
    return S(e, l)(r, t, !O, n, dt.test(e)), n
}
var T, C, k, N, j, E, S, D, A, q, L, H, O, F, P, R, M, W = "sizzle" +- new Date, $ = e.document, 
B = 0, I = 0, z = r(), _ = r(), X = r(), U = !1, Y = function (e, t)
{
    return e === t ? (U = !0, 0) : 0;
},
V = typeof t, G = 1 << 31, Q = {}.hasOwnProperty, J = [], K = J.pop, Z = J.push, et = J.push, 
tt = J.slice, nt = J.indexOf || function (e)
{
    for (var t = 0, n = this.length; n > t; t++) {
        if (this [t] === e) {
            return t;
        }
        return - 1;
    }
},
rt = "checked|selected|async|autofocus|autoplay|controls|defer|disabled|hidden|ismap|loop|multiple|open|readonly|required|scoped", 
it = "[\\x20\\t\\r\\n\\f]", st = "(?:\\\\.|[\\w-]|[^\\x00-\\xa0])+", at = st.replace("w", "w#"), 
ut = "\\[" + it + "*(" + st + ")" + it + "*(?:([*^$|!~]?=)" + it + "*(?:(['\"])((?:\\\\.|[^\\\\])*?)\\3|(" + at + ")|)|)" + it + "*\\]", 
lt = ":(" + st + ")(?:\\(((['\"])((?:\\\\.|[^\\\\])*?)\\3|((?:\\\\.|[^\\\\()[\\]]|" + ut.replace(3, 
8) + ")*)|.*)\\)|)", ct = new RegExp("^" + it + "+|((?:^|[^\\\\])(?:\\\\.)*)" + it + "+$", "g"), 
ft = new RegExp("^" + it + "*," + it + "*"), pt = new RegExp("^" + it + "*([>+~]|" + it + ")" + it + "*"), 
dt = new RegExp(it + "*[+~]"), ht = new RegExp("=" + it + "*([^\\]'\"]*)" + it + "*\\]", "g"), 
gt = new RegExp(lt), mt = new RegExp("^" + at + "$"), yt = 
{
    ID : new RegExp("^#(" + st + ")"), CLASS : new RegExp("^\\.(" + st + ")"), TAG : new RegExp("^(" + st.replace("w", 
    "w*") + ")"), ATTR : new RegExp("^" + ut), PSEUDO : new RegExp("^" + lt), CHILD : new RegExp("^:(only|first|last|nth|nth-last)-(child|of-type)(?:\\(" + it + "*(even|odd|(([+-]|)(\\d*)n|)" + it + "*(?:([+-]|)" + it + "*(\\d+)|))" + it + "*\\)|)", 
    "i"), bool : new RegExp("^(?:" + rt + ")$", "i"), needsContext : new RegExp("^" + it + "*[>+~]|:(even|odd|eq|gt|lt|nth|first|last)(?:\\(" + it + "*((?:-\\d)?\\d*)" + it + "*\\)|)(?=[^-]|$)", 
    "i")
},
vt = /^[^{]+\{\s*\[native \w/, xt = /^(?:#([\w-]+)|(\w+)|\.([\w-]+))$/, bt = /^(?:input|select|textarea|button)$/i, 
wt = /^h\d$/i, Tt = /'|\\/g, Ct = new RegExp("\\\\([\\da-f]{1,6}" + it + "?|(" + it + ")|.)", 
"ig"), kt = function (e, t, n)
{
    var r = "0x" + t - 65536;
    return r !== r || n ? t : 0 > r ? String.fromCharCode(r + 65536) : String.fromCharCode(r >> 10 | 55296, 
    1023 & r | 56320)
};
try {
    et.apply(J = tt.call($.childNodes), $.childNodes), J[$.childNodes.length].nodeType
}
catch (Nt)
{
    et = 
    {
        apply : J.length ? function (e, t)
        {
            Z.apply(e, tt.call(t))
        }
         : function (e, t)
         {
             for (var n = e.length, r = 0; e[n++] = t[r++]; ); e.length = n - 1; }
    }
}
E = n.isXML = function (e)
{
    var t = e && (e.ownerDocument || e).documentElement;
    return t ? "HTML" !== t.nodeName :!1;
},
C = n.support = {}, q = n.setDocument = function (e)
{
    var t = e ? e.ownerDocument || e : $, n = t.defaultView;
    return t !== L && 9 === t.nodeType && t.documentElement ? (L = t, H = t.documentElement, O = !E(t), 
    n && n.attachEvent && n !== n.top && n.attachEvent("onbeforeunload", function ()
    {
        q()
    }), C.attributes = o(function (e)
    {
        return e.className = "i", !e.getAttribute("className");
    }), C.getElementsByTagName = o(function (e)
    {
        return e.appendChild(t.createComment("")), !e.getElementsByTagName("*").length;
    }), C.getElementsByClassName = o(function (e)
    {
        return e.innerHTML = "<div class='a'></div><div class='a i'></div>", e.firstChild.className = "i", 
        2 === e.getElementsByClassName("i").length;
    }), C.getById = o(function (e)
    {
        return H.appendChild(e).id = W, !t.getElementsByName || !t.getElementsByName(W).length;
    }), C.getById ? (N.find.ID = function (e, t)
    {
        if (typeof t.getElementById !== V && O) {
            var n = t.getElementById(e);
            return n && n.parentNode ? [n] : [];
        }
    },
    N.filter.ID = function (e)
    {
        var t = e.replace(Ct, kt);
        return function (e)
        {
            return e.getAttribute("id") === t;
        }
    }) : (delete N.find.ID, N.filter.ID = function (e)
    {
        var t = e.replace(Ct, kt);
        return function (e)
        {
            var n = typeof e.getAttributeNode !== V && e.getAttributeNode("id");
            return n && n.value === t;
        }
    }), N.find.TAG = C.getElementsByTagName ? function (e, t)
    {
        return typeof t.getElementsByTagName !== V ? t.getElementsByTagName(e) : void 0
    }
     : function (e, t)
     {
         var n, r = [], i = 0, o = t.getElementsByTagName(e);
         if ("*" === e) {
             for (; n = o[i++]; ) {
                 1 === n.nodeType && r.push(n);
             }
             return r
         }
         return o;
     },
    N.find.CLASS = C.getElementsByClassName && function (e, t)
    {
        return typeof t.getElementsByClassName !== V && O ? t.getElementsByClassName(e) : void 0;
    },
    P = [], F = [], (C.qsa = vt.test(t.querySelectorAll)) && (o(function (e)
    {
        e.innerHTML = "<select><option selected=''></option></select>", e.querySelectorAll("[selected]").length || F.push("\\[" + it + "*(?:value|" + rt + ")"), 
        e.querySelectorAll(":checked").length || F.push(":checked");
    }), o(function (e)
    {
        var n = t.createElement("input");
        n.setAttribute("type", "hidden"), e.appendChild(n).setAttribute("t", ""), e.querySelectorAll("[t^='']").length && F.push("[*^$]=" + it + "*(?:''|\"\")"), 
        e.querySelectorAll(":enabled").length || F.push(":enabled", ":disabled"), e.querySelectorAll("*,:x"), 
        F.push(",.*:")
    })), (C.matchesSelector = vt.test(R = H.webkitMatchesSelector || H.mozMatchesSelector || H.oMatchesSelector || H.msMatchesSelector)) && o(function (e)
    {
        C.disconnectedMatch = R.call(e, "div"), R.call(e, "[s!='']:x"), P.push("!=", lt);
    }), F = F.length && new RegExp(F.join("|")), P = P.length && new RegExp(P.join("|")), M = vt.test(H.contains) || H.compareDocumentPosition ? function (e, 
    t)
    {
        var n = 9 === e.nodeType ? e.documentElement : e, r = t && t.parentNode;
        return e === r || !(!r || 1 !== r.nodeType || !(n.contains ? n.contains(r) : e.compareDocumentPosition && 16 & e.compareDocumentPosition(r)))
    }
     : function (e, t)
     {
         if (t) {
             for (; t = t.parentNode; ) {
                 if (t === e) {
                     return!0;
                 };
             }
         }
         return!1
     },
    Y = H.compareDocumentPosition ? function (e, n)
    {
        if (e === n) {
            return U = !0, 0;
        }
        var r = n.compareDocumentPosition && e.compareDocumentPosition && e.compareDocumentPosition(n);
        return r ? 1 & r || !C.sortDetached && n.compareDocumentPosition(e) === r ? e === t || M($, 
        e) ?- 1 : n === t || M($, n) ? 1 : A ? nt.call(A, e) - nt.call(A, n) : 0 : 4 & r ?- 1 : 1 : e.compareDocumentPosition ?- 1 : 1
    }
     : function (e, n)
     {
         var r, i = 0, o = e.parentNode, s = n.parentNode, u = [e], l = [n];
         if (e === n) {
             return U = !0, 0;
         }
         if (!o || !s) {
             return e === t ?- 1 : n === t ? 1 : o ?- 1 : s ? 1 : A ? nt.call(A, e) - nt.call(A, 
             n) : 0;
         }
         if (o === s) {
             return a(e, n);
         }
         for (r = e; r = r.parentNode; ) {
             u.unshift(r);
         }
         for (r = n; r = r.parentNode; ) {
             l.unshift(r);
         }
         for (; u[i] === l[i]; ) {
             i++;
         }
         return i ? a(u[i], l[i]) : u[i] === $ ?- 1 : l[i] === $ ? 1 : 0;
     }, t) : L
},
n.matches = function (e, t)
{
    return n(e, null, null, t);
},
n.matchesSelector = function (e, t)
{
    if ((e.ownerDocument || e) !== L && q(e), t = t.replace(ht, "='$1']"), !(!C.matchesSelector || !O || P && P.test(t) || F && F.test(t)))
    {
        try 
        {
            var r = R.call(e, t);
            if (r || C.disconnectedMatch || e.document && 11 !== e.document.nodeType) {
                return r ;
            }
        }
        catch (i) {}
    }
    return n(t, L, null, [e]).length > 0;
},
n.contains = function (e, t)
{
    return (e.ownerDocument || e) !== L && q(e), M(e, t);
},
n.attr = function (e, n)
{
    (e.ownerDocument || e) !== L && q(e);
    var r = N.attrHandle[n.toLowerCase()], i = r && Q.call(N.attrHandle, n.toLowerCase()) ? r(e, 
    n, !O) : t;
    return i === t ? C.attributes || !O ? e.getAttribute(n) : (i = e.getAttributeNode(n)) && i.specified ? i.value : null : i;
},
n.error = function (e)
{
    throw new Error("Syntax error, unrecognized expression: " + e)
},
n.uniqueSort = function (e)
{
    var t, n = [], r = 0, i = 0;
    if (U = !C.detectDuplicates, A = !C.sortStable && e.slice(0), e.sort(Y), U) {
        for (; t = e[i++]; ) {
            t === e[i] && (r = n.push(i));
        }
        for (; r--; ) {
            e.splice(n[r], 1);
        }
    }
    return e;
},
j = n.getText = function (e)
{
    var t, n = "", r = 0, i = e.nodeType;
    if (i)
    {
        if (1 === i || 9 === i || 11 === i)
        {
            if ("string" == typeof e.textContent) {
                return e.textContent;
            }
            for (e = e.firstChild; e; e = e.nextSibling) {
                n += j(e);
            }
        }
        else if (3 === i || 4 === i) {
            return e.nodeValue;
        }
    }
    else {
        for (; t = e[r]; r++) {
            n += j(t);
        }
    }
    return n;
},
N = n.selectors = 
{
    cacheLength : 50, createPseudo : i, match : yt, attrHandle : {}, find : {},
    relative : 
    {
        ">" : {
            dir : "parentNode", first :!0
        },
        " " : {
            dir : "parentNode"
        },
        "+" : {
            dir : "previousSibling", first :!0
        },
        "~" : {
            dir : "previousSibling"
        }
    },
    preFilter : 
    {
        ATTR : function (e)
        {
            return e[1] = e[1].replace(Ct, kt), e[3] = (e[4] || e[5] || "").replace(Ct, kt), "~=" === e[2] && (e[3] = " " + e[3] + " "), 
            e.slice(0, 4);
        },
        CHILD : function (e)
        {
            return e[1] = e[1].toLowerCase(), "nth" === e[1].slice(0, 3) ? (e[3] || n.error(e[0]), 
            e[4] =+ (e[4] ? e[5] + (e[6] || 1) : 2 * ("even" === e[3] || "odd" === e[3])), e[5] =+ (e[7] + e[8] || "odd" === e[3])) : e[3] && n.error(e[0]), 
            e;
        },
        PSEUDO : function (e)
        {
            var n, r = !e[5] && e[2];
            return yt.CHILD.test(e[0]) ? null : (e[3] && e[4] !== t ? e[2] = e[4] : r && gt.test(r) && (n = p(r, 
            !0)) && (n = r.indexOf(")", r.length - n) - r.length) && (e[0] = e[0].slice(0, n), 
            e[2] = r.slice(0, n)), e.slice(0, 3));
        }
    },
    filter : 
    {
        TAG : function (e)
        {
            var t = e.replace(Ct, kt).toLowerCase();
            return "*" === e ? function () {
                return!0
            }
             : function (e)
             {
                 return e.nodeName && e.nodeName.toLowerCase() === t;
             }
        },
        CLASS : function (e)
        {
            var t = z[e + " "];
            return t || (t = new RegExp("(^|" + it + ")" + e + "(" + it + "|$)")) && z(e, function (e)
            {
                return t.test("string" == typeof e.className && e.className || typeof e.getAttribute !== V && e.getAttribute("class") || "");
            })
        },
        ATTR : function (e, t, r)
        {
            return function (i)
            {
                var o = n.attr(i, e);
                return null == o ? "!=" === t : t ? (o += "", "=" === t ? o === r : "!=" === t ? o !== r : "^=" === t ? r && 0 === o.indexOf(r) : "*=" === t ? r && o.indexOf(r) > -1 : "$=" === t ? r && o.slice(-r.length) === r : "~=" === t ? (" " + o + " ").indexOf(r) > -1 : "|=" === t ? o === r || o.slice(0, 
                r.length + 1) === r + "-" :!1) :!0
            }
        },
        CHILD : function (e, t, n, r, i)
        {
            var o = "nth" !== e.slice(0, 3), s = "last" !== e.slice(-4), a = "of-type" === t;
            return 1 === r && 0 === i ? function (e)
            {
                return!!e.parentNode
            }
             : function (t, n, u)
             {
                 var l, c, f, p, d, h, g = o !== s ? "nextSibling" : "previousSibling", m = t.parentNode, 
                 y = a && t.nodeName.toLowerCase(), v = !u && !a;
                 if (m)
                 {
                     if (o)
                     {
                         for (; g; )
                         {
                             for (f = t; f = f[g]; )
                             {
                                 if (a ? f.nodeName.toLowerCase() === y : 1 === f.nodeType) {
                                     return!1;
                                 }
                                 h = g = "only" === e && !h && "nextSibling";
                             }
                         }
                         return!0
                     }
                     if (h = [s ? m.firstChild : m.lastChild], s && v)
                     {
                         for (c = m[W] || (m[W] = {}), l = c[e] || [], d = l[0] === B && l[1], 
                         p = l[0] === B && l[2], f = d && m.childNodes[d];
                         f =++d && f && f[g] || (p = d = 0) || h.pop();
                         ) if (1 === f.nodeType &&++p && f === t) {
                             c[e] = [B, d, p];
                             break
                         }
                     }
                     else if (v && (l = (t[W] || (t[W] = {}))[e]) && l[0] === B) {
                         p = l[1];
                     }
                     else
                     {
                         for (; (f =++d && f && f[g] || (p = d = 0) || h.pop()) && ((a ? f.nodeName.toLowerCase() !== y : 1 !== f.nodeType) || !++p || (v && ((f[W] || (f[W] = {}))[e] = [B, 
                         p]), f !== t));
                         );
                     }
                     return p -= i, p === r || p % r === 0 && p / r >= 0;
                 }
             }
        },
        PSEUDO : function (e, t)
        {
            var r, o = N.pseudos[e] || N.setFilters[e.toLowerCase()] || n.error("unsupported pseudo: " + e);
            return o[W] ? o(t) : o.length > 1 ? (r = [e, e, "", t], N.setFilters.hasOwnProperty(e.toLowerCase()) ? i(function (e, 
            n) {
                for (var r, i = o(e, t), s = i.length; s--; ) {
                    r = nt.call(e, i[s]), e[r] = !(n[r] = i[s]);
                }
            }) : function (e)
            {
                return o(e, 0, r);
            }) : o
        }
    },
    pseudos : 
    {
        not : i(function (e)
        {
            var t = [], n = [], r = S(e.replace(ct, "$1"));
            return r[W] ? i(function (e, t, n, i)
            {
                for (var o, s = r(e, null, i, []), a = e.length; a--; ) {
                    (o = s[a]) && (e[a] = !(t[a] = o));
                }
            }) : function (e, i, o)
            {
                return t[0] = e, r(t, null, o, n), !n.pop();
            }
        }), has : i(function (e)
        {
            return function (t)
            {
                return n(e, t).length > 0;
            }
        }), contains : i(function (e)
        {
            return function (t)
            {
                return (t.textContent || t.innerText || j(t)).indexOf(e) > -1;
            }
        }), lang : i(function (e)
        {
            return mt.test(e || "") || n.error("unsupported lang: " + e), e = e.replace(Ct, kt).toLowerCase(), 
            function (t)
            {
                var n;
                do if (n = O ? t.lang : t.getAttribute("xml:lang") || t.getAttribute("lang")) return n = n.toLowerCase(), 
                n === e || 0 === n.indexOf(e + "-");
                while ((t = t.parentNode) && 1 === t.nodeType);
                return!1
            }
        }),
        target : function (t)
        {
            var n = e.location && e.location.hash;
            return n && n.slice(1) === t.id;
        },
        root : function (e)
        {
            return e === H;
        },
        focus : function (e)
        {
            return e === L.activeElement && (!L.hasFocus || L.hasFocus()) && !!(e.type || e.href || ~e.tabIndex);
        },
        enabled : function (e)
        {
            return e.disabled === !1;
        },
        disabled : function (e)
        {
            return e.disabled === !0;
        },
        checked : function (e)
        {
            var t = e.nodeName.toLowerCase();
            return "input" === t && !!e.checked || "option" === t && !!e.selected;
        },
        selected : function (e)
        {
            return e.parentNode && e.parentNode.selectedIndex, e.selected === !0;
        },
        empty : function (e)
        {
            for (e = e.firstChild; e; e = e.nextSibling) {
                if (e.nodeName > "@" || 3 === e.nodeType || 4 === e.nodeType) {
                    return!1;
                }
                return!0;
            }
        },
        parent : function (e)
        {
            return!N.pseudos.empty(e)
        },
        header : function (e)
        {
            return wt.test(e.nodeName);
        },
        input : function (e)
        {
            return bt.test(e.nodeName);
        },
        button : function (e)
        {
            var t = e.nodeName.toLowerCase();
            return "input" === t && "button" === e.type || "button" === t;
        },
        text : function (e)
        {
            var t;
            return "input" === e.nodeName.toLowerCase() && "text" === e.type && (null == (t = e.getAttribute("type")) || t.toLowerCase() === e.type);
        },
        first : c(function ()
        {
            return [0];
        }), last : c(function (e, t)
        {
            return [t - 1];
        }), eq : c(function (e, t, n)
        {
            return [0 > n ? n + t : n];
        }), even : c(function (e, t)
        {
            for (var n = 0; t > n; n += 2) {
                e.push(n);
            }
            return e;
        }), odd : c(function (e, t)
        {
            for (var n = 1; t > n; n += 2) {
                e.push(n);
            }
            return e;
        }), lt : c(function (e, t, n)
        {
            for (var r = 0 > n ? n + t : n; --r >= 0; ) {
                e.push(r);
            }
            return e;
        }), gt : c(function (e, t, n)
        {
            for (var r = 0 > n ? n + t : n; ++r < t; ) {
                e.push(r);
            }
            return e;
        })
    }
},
N.pseudos.nth = N.pseudos.eq;
for (T in {
    radio :!0, checkbox :!0, file :!0, password :!0, image :!0
}) N.pseudos[T] = u(T);
for (T in {
    submit :!0, reset :!0
}) N.pseudos[T] = l(T);
f.prototype = N.filters = N.pseudos, N.setFilters = new f, S = n.compile = function (e, t)
{
    var n, r = [], i = [], o = X[e + " "];
    if (!o) {
        for (t || (t = p(e)), n = t.length; n--; ) {
            o = v(t[n]), o[W] ? r.push(o) : i.push(o);
        }
        o = X(e, x(i, r))
    }
    return o;
},
C.sortStable = W.split("").sort(Y).join("") === W, C.detectDuplicates = U, q(), C.sortDetached = o(function (e)
{
    return 1 & e.compareDocumentPosition(L.createElement("div"));
}), o(function (e)
{
    return e.innerHTML = "<a href='#'></a>", "#" === e.firstChild.getAttribute("href");
}) || s("type|href|height|width", function (e, t, n)
{
    return n ? void 0 : e.getAttribute(t, "type" === t.toLowerCase() ? 1 : 2);
}), C.attributes && o(function (e)
{
    return e.innerHTML = "<input/>", e.firstChild.setAttribute("value", ""), "" === e.firstChild.getAttribute("value");
}) || s("value", function (e, t, n)
{
    return n || "input" !== e.nodeName.toLowerCase() ? void 0 : e.defaultValue;
}), o(function (e)
{
    return null == e.getAttribute("disabled");
}) || s(rt, function (e, t, n)
{
    var r;
    return n ? void 0 : (r = e.getAttributeNode(t)) && r.specified ? r.value : e[t] === !0 ? t.toLowerCase() : null;
}), ot.find = n, ot.expr = n.selectors, ot.expr[":"] = ot.expr.pseudos, ot.unique = n.uniqueSort, 
ot.text = n.getText, ot.isXMLDoc = n.isXML, ot.contains = n.contains
}
(e);
var ht = {};
ot.Callbacks = function (e)
{
    e = "string" == typeof e ? ht[e] || r(e) : ot.extend({}, e);
    var n, i, o, s, a, u, l = [], c = !e.once && [], f = function (t)
    {
        for (n = e.memory && t, i = !0, u = s || 0, s = 0, a = l.length, o = !0; l && a > u; u++)
        {
            if (l[u].apply(t[0], t[1]) === !1 && e.stopOnFalse) {
                n = !1;
                break 
            }
            o = !1, l && (c ? c.length && f(c.shift()) : n ? l = [] : p.disable());
        }
    },
    p = 
    {
        add : function ()
        {
            if (l)
            {
                var t = l.length;
                !function r(t)
                {
                    ot.each(t, function (t, n)
                    {
                        var i = ot.type(n);
                        "function" === i ? e.unique && p.has(n) || l.push(n) : n && n.length && "string" !== i && r(n)
                    })
                }
                (arguments), o ? a = l.length : n && (s = t, f(n))
            }
            return this;
        },
        remove : function ()
        {
            return l && ot.each(arguments, function (e, t)
            {
                for (var n; (n = ot.inArray(t, l, n)) > -1; ) {
                    l.splice(n, 1), o && (a >= n && a--, u >= n && u--);
                }
            }), this
        },
        has : function (e)
        {
            return e ? ot.inArray(e, l) > -1 :!(!l || !l.length);
        },
        empty : function ()
        {
            return l = [], a = 0, this;
        },
        disable : function ()
        {
            return l = c = n = t, this;
        },
        disabled : function ()
        {
            return!l
        },
        lock : function ()
        {
            return c = t, n || p.disable(), this;
        },
        locked : function ()
        {
            return!c
        },
        fireWith : function (e, t)
        {
            return!l || i && !c || (t = t || [], t = [e, t.slice ? t.slice() : t], o ? c.push(t) : f(t)), 
            this;
        },
        fire : function ()
        {
            return p.fireWith(this, arguments), this;
        },
        fired : function ()
        {
            return!!i
        }
    };
    return p;
},
ot.extend(
{
    Deferred : function (e)
    {
        var t = [["resolve", "done", ot.Callbacks("once memory"), "resolved"], ["reject", "fail", 
        ot.Callbacks("once memory"), "rejected"], ["notify", "progress", ot.Callbacks("memory")]], 
        n = "pending", r = 
        {
            state : function ()
            {
                return n;
            },
            always : function ()
            {
                return i.done(arguments).fail(arguments), this;
            },
            then : function ()
            {
                var e = arguments;
                return ot.Deferred(function (n)
                {
                    ot.each(t, function (t, o)
                    {
                        var s = o[0], a = ot.isFunction(e[t]) && e[t];
                        i[o[1]](function ()
                        {
                            var e = a && a.apply(this, arguments);
                            e && ot.isFunction(e.promise) ? e.promise().done(n.resolve).fail(n.reject).progress(n.notify) : n[s + "With"](this === r ? n.promise() : this, 
                            a ? [e] : arguments)
                        })
                    }), e = null;
                }).promise()
            },
            promise : function (e)
            {
                return null != e ? ot.extend(e, r) : r;
            }
        },
        i = {};
        return r.pipe = r.then, ot.each(t, function (e, o)
        {
            var s = o[2], a = o[3];
            r[o[1]] = s.add, a && s.add(function ()
            {
                n = a;
            },
            t[1^e][2].disable, t[2][2].lock), i[o[0]] = function ()
            {
                return i[o[0] + "With"](this === i ? r : this, arguments), this;
            },
            i[o[0] + "With"] = s.fireWith;
        }), r.promise(i), e && e.call(i, i), i
    },
    when : function (e)
    {
        var t, n, r, i = 0, o = et.call(arguments), s = o.length, a = 1 !== s || e && ot.isFunction(e.promise) ? s : 0, 
        u = 1 === a ? e : ot.Deferred(), l = function (e, n, r)
        {
            return function (i)
            {
                n[e] = this, r[e] = arguments.length > 1 ? et.call(arguments) : i, r === t ? u.notifyWith(n, 
                r) :--a || u.resolveWith(n, r);
            }
        };
        if (s > 1)
        {
            for (t = new Array(s), n = new Array(s), r = new Array(s); s > i; i++)
            {
                o[i] && ot.isFunction(o[i].promise) ? o[i].promise().done(l(i, r, o)).fail(u.reject).progress(l(i, 
                n, t)) :--a;
            }
        }
        return a || u.resolveWith(r, o), u.promise();
    }
}), ot.support = function (t)
{
    var n = X.createElement("input"), r = X.createDocumentFragment(), i = X.createElement("div"), 
    o = X.createElement("select"), s = o.appendChild(X.createElement("option"));
    return n.type ? (n.type = "checkbox", t.checkOn = "" !== n.value, t.optSelected = s.selected, 
    t.reliableMarginRight = !0, t.boxSizingReliable = !0, t.pixelPosition = !1, n.checked = !0, t.noCloneChecked = n.cloneNode(!0).checked, 
    o.disabled = !0, t.optDisabled = !s.disabled, n = X.createElement("input"), n.value = "t", n.type = "radio", 
    t.radioValue = "t" === n.value, n.setAttribute("checked", "t"), n.setAttribute("name", "t"), r.appendChild(n), 
    t.checkClone = r.cloneNode(!0).cloneNode(!0).lastChild.checked, t.focusinBubbles = "onfocusin"in e, 
    i.style.backgroundClip = "content-box", i.cloneNode(!0).style.backgroundClip = "", t.clearCloneStyle = "content-box" === i.style.backgroundClip, 
    ot(function ()
    {
        var n, r, o = "padding:0;margin:0;border:0;display:block;-webkit-box-sizing:content-box;-moz-box-sizing:content-box;box-sizing:content-box", 
        s = X.getElementsByTagName("body")[0];
        s && (n = X.createElement("div"), n.style.cssText = "border:0;width:0;height:0;position:absolute;top:0;left:-9999px;margin-top:1px", 
        s.appendChild(n).appendChild(i), i.innerHTML = "", i.style.cssText = "-webkit-box-sizing:border-box;-moz-box-sizing:border-box;box-sizing:border-box;padding:1px;border:1px;display:block;width:4px;margin-top:1%;position:absolute;top:1%", 
        ot.swap(s, null != s.style.zoom ? {
            zoom : 1
        }
         : {}, function ()
         {
             t.boxSizing = 4 === i.offsetWidth;
         }), e.getComputedStyle && (t.pixelPosition = "1%" !== (e.getComputedStyle(i, null) || {}).top, 
        t.boxSizingReliable = "4px" === (e.getComputedStyle(i, null) || {
            width : "4px"
        }).width, r = i.appendChild(X.createElement("div")), r.style.cssText = i.style.cssText = o, 
        r.style.marginRight = r.style.width = "0", i.style.width = "1px", t.reliableMarginRight = !parseFloat((e.getComputedStyle(r, 
        null) || {}).marginRight)), s.removeChild(n))
    }), t) : t
}
({});
var gt, mt, yt = /(?:\{[\s\S]*\}|\[[\s\S]*\])$/, vt = /([A-Z])/g;
i.uid = 1, i.accepts = function (e)
{
    return e.nodeType ? 1 === e.nodeType || 9 === e.nodeType :!0;
},
i.prototype = 
{
    key : function (e)
    {
        if (!i.accepts(e)) {
            return 0;
        }
        var t = {}, n = e[this.expando];
        if (!n)
        {
            n = i.uid++;
            try {
                t[this.expando] = {
                    value : n
                },
                Object.defineProperties(e, t)
            }
            catch (r) {
                t[this.expando] = n, ot.extend(e, t);
            }
        }
        return this.cache[n] || (this.cache[n] = {}), n;
    },
    set : function (e, t, n)
    {
        var r, i = this.key(e), o = this.cache[i];
        if ("string" == typeof t) {
            o[t] = n;
        }
        else if (ot.isEmptyObject(o)) {
            ot.extend(this.cache[i], t);
        }
        else {
            for (r in t) {
                o[r] = t[r];
            }
        }
        return o;
    },
    get : function (e, n)
    {
        var r = this.cache[this.key(e)];
        return n === t ? r : r[n];
    },
    access : function (e, n, r)
    {
        var i;
        return n === t || n && "string" == typeof n && r === t ? (i = this.get(e, n), i !== t ? i : this.get(e, 
        ot.camelCase(n))) : (this.set(e, n, r), r !== t ? r : n);
    },
    remove : function (e, n)
    {
        var r, i, o, s = this.key(e), a = this.cache[s];
        if (n === t) {
            this.cache[s] = {};
        }
        else
        {
            ot.isArray(n) ? i = n.concat(n.map(ot.camelCase)) : (o = ot.camelCase(n), n in a ? i = [n, 
            o] : (i = o, i = i in a ? [i] : i.match(at) || [])), r = i.length;
            for (; r--; ) {
                delete a[i[r]];
            }
        }
    },
    hasData : function (e)
    {
        return!ot.isEmptyObject(this.cache[e[this.expando]] || {})
    },
    discard : function (e)
    {
        e[this.expando] && delete this.cache[e[this.expando]]
    }
},
gt = new i, mt = new i, ot.extend(
{
    acceptData : i.accepts,
    hasData : function (e)
    {
        return gt.hasData(e) || mt.hasData(e);
    },
    data : function (e, t, n)
    {
        return gt.access(e, t, n);
    },
    removeData : function (e, t)
    {
        gt.remove(e, t)
    },
    _data : function (e, t, n)
    {
        return mt.access(e, t, n);
    },
    _removeData : function (e, t)
    {
        mt.remove(e, t)
    }
}), ot.fn.extend(
{
    data : function (e, n)
    {
        var r, i, s = this [0], a = 0, u = null;
        if (e === t)
        {
            if (this.length && (u = gt.get(s), 1 === s.nodeType && !mt.get(s, "hasDataAttrs")))
            {
                for (r = s.attributes; a < r.length; a++) {
                    i = r[a].name, 0 === i.indexOf("data-") && (i = ot.camelCase(i.slice(5)), o(s, 
                    i, u[i]));
                }
                mt.set(s, "hasDataAttrs", !0)
            }
            return u
        }
        return "object" == typeof e ? this.each(function () {
            gt.set(this, e)
        }) : ot.access(this, function (n)
        {
            var r, i = ot.camelCase(e);
            if (s && n === t)
            {
                if (r = gt.get(s, e), r !== t) {
                    return r;
                }
                if (r = gt.get(s, i), r !== t) {
                    return r;
                }
                if (r = o(s, i, t), r !== t) {
                    return r;
                }
            }
            else
            {
                this.each(function () 
                {
                    var r = gt.get(this, i);
                    gt.set(this, i, n), - 1 !== e.indexOf("-") && r !== t && gt.set(this, e, n) 
                });
            }
        },
        null, n, arguments.length > 1, null, !0)
    },
    removeData : function (e)
    {
        return this.each(function ()
        {
            gt.remove(this, e)
        })
    }
}), ot.extend(
{
    queue : function (e, t, n)
    {
        var r;
        return e ? (t = (t || "fx") + "queue", r = mt.get(e, t), n && (!r || ot.isArray(n) ? r = mt.access(e, 
        t, ot.makeArray(n)) : r.push(n)), r || []) : void 0;
    },
    dequeue : function (e, t)
    {
        t = t || "fx";
        var n = ot.queue(e, t), r = n.length, i = n.shift(), o = ot._queueHooks(e, t), s = function ()
        {
            ot.dequeue(e, t)
        };
        "inprogress" === i && (i = n.shift(), r--), i && ("fx" === t && n.unshift("inprogress"), delete o.stop, 
        i.call(e, s, o)), !r && o && o.empty.fire();
    },
    _queueHooks : function (e, t)
    {
        var n = t + "queueHooks";
        return mt.get(e, n) || mt.access(e, n, 
        {
            empty : ot.Callbacks("once memory").add(function ()
            {
                mt.remove(e, [t + "queue", n])
            })
        })
    }
}), ot.fn.extend(
{
    queue : function (e, n)
    {
        var r = 2;
        return "string" != typeof e && (n = e, e = "fx", r--), arguments.length < r ? ot.queue(this [0], 
        e) : n === t ? this : this.each(function ()
        {
            var t = ot.queue(this, e, n);
            ot._queueHooks(this, e), "fx" === e && "inprogress" !== t[0] && ot.dequeue(this, e)
        })
    },
    dequeue : function (e)
    {
        return this.each(function ()
        {
            ot.dequeue(this, e)
        })
    },
    delay : function (e, t)
    {
        return e = ot.fx ? ot.fx.speeds[e] || e : e, t = t || "fx", this.queue(t, function (t, n)
        {
            var r = setTimeout(t, e);
            n.stop = function ()
            {
                clearTimeout(r)
            }
        })
    },
    clearQueue : function (e)
    {
        return this.queue(e || "fx", []);
    },
    promise : function (e, n)
    {
        var r, i = 1, o = ot.Deferred(), s = this, a = this.length, u = function ()
        {
            --i || o.resolveWith(s, [s])
        };
        for ("string" != typeof e && (n = e, e = t), e = e || "fx"; a--; ) {
            r = mt.get(s[a], e + "queueHooks"), r && r.empty && (i++, r.empty.add(u));
        }
        return u(), o.promise(n);
    }
});
var xt, bt, wt = /[\t\r\n\f]/g, Tt = /\r/g, Ct = /^(?:input|select|textarea|button)$/i;
ot.fn.extend(
{
    attr : function (e, t)
    {
        return ot.access(this, ot.attr, e, t, arguments.length > 1);
    },
    removeAttr : function (e)
    {
        return this.each(function ()
        {
            ot.removeAttr(this, e)
        })
    },
    prop : function (e, t)
    {
        return ot.access(this, ot.prop, e, t, arguments.length > 1);
    },
    removeProp : function (e)
    {
        return this.each(function ()
        {
            delete this [ot.propFix[e] || e]
        })
    },
    addClass : function (e)
    {
        var t, n, r, i, o, s = 0, a = this.length, u = "string" == typeof e && e;
        if (ot.isFunction(e)) {
            return this.each(function (t) 
            {
                ot(this).addClass(e.call(this, t, this.className)) 
            });
        }
        if (u)
        {
            for (t = (e || "").match(at) || []; a > s; s++)
            {
                if (n = this [s], r = 1 === n.nodeType && (n.className ? (" " + n.className + " ").replace(wt, 
                " ") : " ")) {
                    for (o = 0; i = t[o++]; ) {
                        r.indexOf(" " + i + " ") < 0 && (r += i + " ");
                    }
                    n.className = ot.trim(r) 
                }
                return this;;
            }
        }
    },
    removeClass : function (e)
    {
        var t, n, r, i, o, s = 0, a = this.length, u = 0 === arguments.length || "string" == typeof e && e;
        if (ot.isFunction(e))
        {
            return this.each(function (t) 
            {
                ot(this).removeClass(e.call(this, t, this.className)) 
            });
        }
        if (u)
        {
            for (t = (e || "").match(at) || []; a > s; s++)
            {
                if (n = this [s], r = 1 === n.nodeType && (n.className ? (" " + n.className + " ").replace(wt, 
                " ") : "")) 
                {
                    for (o = 0; i = t[o++]; ) {
                        for (; r.indexOf(" " + i + " ") >= 0; ) {
                            r = r.replace(" " + i + " ", " ");
                        }
                    }
                    n.className = e ? ot.trim(r) : "" 
                }
                return this;;
            }
        }
    },
    toggleClass : function (e, t)
    {
        var n = typeof e;
        return "boolean" == typeof t && "string" === n ? t ? this.addClass(e) : this.removeClass(e) : this.each(ot.isFunction(e) ? function (n) {
            ot(this).toggleClass(e.call(this, n, this.className, t), t)
        }
         : function ()
         {
             if ("string" === n)
             {
                 for (var t, r = 0, i = ot(this), o = e.match(at) || []; t = o[r++]; ) {
                     i.hasClass(t) ? i.removeClass(t) : i.addClass(t);
                 }
             }
             else (n === z || "boolean" === n) && (this.className && mt.set(this, "__className__", 
             this.className), this.className = this.className || e === !1 ? "" : mt.get(this, "__className__") || "");
         })
    },
    hasClass : function (e)
    {
        for (var t = " " + e + " ", n = 0, r = this.length; r > n; n++)
        {
            if (1 === this [n].nodeType && (" " + this [n].className + " ").replace(wt, " ").indexOf(t) >= 0) {
                return!0;
            }
            return!1;
        }
    },
    val : function (e)
    {
        var n, r, i, o = this [0];

        {
            if (arguments.length)
            {
                return i = ot.isFunction(e), this.each(function (r) 
                {
                    var o;
                    1 === this.nodeType && (o = i ? e.call(this, r, ot(this).val()) : e, null == o ? o = "" : "number" == typeof o ? o += "" : ot.isArray(o) && (o = ot.map(o, 
                    function (e) 
                    {
                        return null == e ? "" : e + "";
                    })), n = ot.valHooks[this.type] || ot.valHooks[this.nodeName.toLowerCase()], n && "set"in n && n.set(this, 
                    o, "value") !== t || (this.value = o));
                });
            }
            if (o)
            {
                return n = ot.valHooks[o.type] || ot.valHooks[o.nodeName.toLowerCase()], n && "get"in n && (r = n.get(o, 
                "value")) !== t ? r : (r = o.value, "string" == typeof r ? r.replace(Tt, "") : null == r ? "" : r);
            }
        }
    }
}), ot.extend(
{
    valHooks : 
    {
        option : {
            get : function (e)
            {
                var t = e.attributes.value;
                return!t || t.specified ? e.value : e.text
            }
        },
        select : 
        {
            get : function (e)
            {
                for (var t, n, r = e.options, i = e.selectedIndex, o = "select-one" === e.type || 0 > i, 
                s = o ? null : [], a = o ? i + 1 : r.length, u = 0 > i ? a : o ? i : 0;
                a > u;
                u++) if (n = r[u], !(!n.selected && u !== i || (ot.support.optDisabled ? n.disabled : null !== n.getAttribute("disabled")) || n.parentNode.disabled && ot.nodeName(n.parentNode, 
                "optgroup"))) {
                    if (t = ot(n).val(), o) {
                        return t;
                    }
                    s.push(t)
                }
                return s;
            },
            set : function (e, t)
            {
                for (var n, r, i = e.options, o = ot.makeArray(t), s = i.length; s--; ) {
                    r = i[s], (r.selected = ot.inArray(ot(r).val(), o) >= 0) && (n = !0);
                }
                return n || (e.selectedIndex =- 1), o;
            }
        }
    },
    attr : function (e, n, r)
    {
        var i, o, s = e.nodeType;
        if (e && 3 !== s && 8 !== s && 2 !== s)
        {
            return typeof e.getAttribute === z ? ot.prop(e, n, r) : (1 === s && ot.isXMLDoc(e) || (n = n.toLowerCase(), 
            i = ot.attrHooks[n] || (ot.expr.match.bool.test(n) ? bt : xt)), r === t ? i && "get"in i && null !== (o = i.get(e, 
            n)) ? o : (o = ot.find.attr(e, n), null == o ? t : o) : null !== r ? i && "set"in i && (o = i.set(e, 
            r, n)) !== t ? o : (e.setAttribute(n, r + ""), r) : void ot.removeAttr(e, n));
        }
    },
    removeAttr : function (e, t)
    {
        var n, r, i = 0, o = t && t.match(at);
        if (o && 1 === e.nodeType)
        {
            for (; n = o[i++]; ) {
                r = ot.propFix[n] || n, ot.expr.match.bool.test(n) && (e[r] = !1), e.removeAttribute(n);
            }
        }
    },
    attrHooks : 
    {
        type : 
        {
            set : function (e, t)
            {
                if (!ot.support.radioValue && "radio" === t && ot.nodeName(e, "input")) {
                    var n = e.value;
                    return e.setAttribute("type", t), n && (e.value = n), t;
                }
            }
        }
    },
    propFix : {
        "for" : "htmlFor", "class" : "className"
    },
    prop : function (e, n, r)
    {
        var i, o, s, a = e.nodeType;
        if (e && 3 !== a && 8 !== a && 2 !== a)
        {
            return s = 1 !== a || !ot.isXMLDoc(e), s && (n = ot.propFix[n] || n, o = ot.propHooks[n]), 
            r !== t ? o && "set"in o && (i = o.set(e, r, n)) !== t ? i : e[n] = r : o && "get"in o && null !== (i = o.get(e, 
            n)) ? i : e[n];
        }
    },
    propHooks : 
    {
        tabIndex : 
        {
            get : function (e)
            {
                return e.hasAttribute("tabindex") || Ct.test(e.nodeName) || e.href ? e.tabIndex :- 1;
            }
        }
    }
}), bt = {
    set : function (e, t, n)
    {
        return t === !1 ? ot.removeAttr(e, n) : e.setAttribute(n, n), n;
    }
},
ot.each(ot.expr.match.bool.source.match(/\w+/g), function (e, n)
{
    var r = ot.expr.attrHandle[n] || ot.find.attr;
    ot.expr.attrHandle[n] = function (e, n, i)
    {
        var o = ot.expr.attrHandle[n], s = i ? t : (ot.expr.attrHandle[n] = t) != r(e, n, i) ? n.toLowerCase() : null;
        return ot.expr.attrHandle[n] = o, s;
    }
}), ot.support.optSelected || (ot.propHooks.selected = 
{
    get : function (e)
    {
        var t = e.parentNode;
        return t && t.parentNode && t.parentNode.selectedIndex, null;
    }
}), ot.each(["tabIndex", "readOnly", "maxLength", "cellSpacing", "cellPadding", "rowSpan", "colSpan", 
"useMap", "frameBorder", "contentEditable"], function ()
{
    ot.propFix[this.toLowerCase()] = this;
}), ot.each(["radio", "checkbox"], function ()
{
    ot.valHooks[this] = 
    {
        set : function (e, t)
        {
            return ot.isArray(t) ? e.checked = ot.inArray(ot(e).val(), t) >= 0 : void 0;
        }
    },
    ot.support.checkOn || (ot.valHooks[this].get = function (e)
    {
        return null === e.getAttribute("value") ? "on" : e.value;
    })
});
var kt = /^key/, Nt = /^(?:mouse|contextmenu)|click/, jt = /^(?:focusinfocus|focusoutblur)$/, Et = /^([^.]*)(?:\.(.+)|)$/;
ot.event = 
{
    global : {},
    add : function (e, n, r, i, o)
    {
        var s, a, u, l, c, f, p, d, h, g, m, y = mt.get(e);
        if (y)
        {
            for (r.handler && (s = r, r = s.handler, o = s.selector), r.guid || (r.guid = ot.guid++), 
            (l = y.events) || (l = y.events = {}), (a = y.handle) || (a = y.handle = function (e)
            {
                return typeof ot === z || e && ot.event.triggered === e.type ? t : ot.event.dispatch.apply(a.elem, 
                arguments)
            },
            a.elem = e), n = (n || "").match(at) || [""], c = n.length;
            c--;
            ) u = Et.exec(n[c]) || [], h = m = u[1], g = (u[2] || "").split(".").sort(), h && (p = ot.event.special[h] || {},
            h = (o ? p.delegateType : p.bindType) || h, p = ot.event.special[h] || {},
            f = ot.extend(
            {
                type : h, origType : m, data : i, handler : r, guid : r.guid, selector : o, needsContext : o && ot.expr.match.needsContext.test(o), 
                namespace : g.join(".")
            }, s), (d = l[h]) || (d = l[h] = [], d.delegateCount = 0, p.setup && p.setup.call(e, i, 
            g, a) !== !1 || e.addEventListener && e.addEventListener(h, a, !1)), p.add && (p.add.call(e, 
            f), f.handler.guid || (f.handler.guid = r.guid)), o ? d.splice(d.delegateCount++, 0, f) : d.push(f), 
            ot.event.global[h] = !0);
            e = null;
        }
    },
    remove : function (e, t, n, r, i)
    {
        var o, s, a, u, l, c, f, p, d, h, g, m = mt.hasData(e) && mt.get(e);
        if (m && (u = m.events))
        {
            for (t = (t || "").match(at) || [""], l = t.length; l--; )
            {
                if (a = Et.exec(t[l]) || [], d = g = a[1], h = (a[2] || "").split(".").sort(), d) 
                {
                    for (f = ot.event.special[d] || {}, d = (r ? f.delegateType : f.bindType) || d, 
                    p = u[d] || [], a = a[2] && new RegExp("(^|\\.)" + h.join("\\.(?:.*\\.|)") + "(\\.|$)"), 
                    s = o = p.length;
                    o--;
                    ) c = p[o], !i && g !== c.origType || n && n.guid !== c.guid || a && !a.test(c.namespace) || r && r !== c.selector && ("**" !== r || !c.selector) || (p.splice(o, 
                    1), c.selector && p.delegateCount--, f.remove && f.remove.call(e, c));
                    s && !p.length && (f.teardown && f.teardown.call(e, h, m.handle) !== !1 || ot.removeEvent(e, 
                    d, m.handle), delete u[d]) 
                }
                else {
                    for (d in u) {
                        ot.event.remove(e, d + t[l], n, r, !0);
                    }
                }
                ot.isEmptyObject(u) && (delete m.handle, mt.remove(e, "events"));
            }
        }
    },
    trigger : function (n, r, i, o)
    {
        var s, a, u, l, c, f, p, d = [i || X], h = rt.call(n, "type") ? n.type : n, g = rt.call(n, 
        "namespace") ? n.namespace.split(".") : [];
        if (a = u = i = i || X, 3 !== i.nodeType && 8 !== i.nodeType && !jt.test(h + ot.event.triggered) && (h.indexOf(".") >= 0 && (g = h.split("."), 
        h = g.shift(), g.sort()), c = h.indexOf(":") < 0 && "on" + h, n = n[ot.expando] ? n : new ot.Event(h, 
        "object" == typeof n && n), n.isTrigger = o ? 2 : 3, n.namespace = g.join("."), n.namespace_re = n.namespace ? new RegExp("(^|\\.)" + g.join("\\.(?:.*\\.|)") + "(\\.|$)") : null, 
        n.result = t, n.target || (n.target = i), r = null == r ? [n] : ot.makeArray(r, [n]), p = ot.event.special[h] || {},
        o || !p.trigger || p.trigger.apply(i, r) !== !1))
        {
            if (!o && !p.noBubble && !ot.isWindow(i))
            {
                for (l = p.delegateType || h, jt.test(l + h) || (a = a.parentNode); a; a = a.parentNode) {
                    d.push(a), u = a;
                }
                u === (i.ownerDocument || X) && d.push(u.defaultView || u.parentWindow || e)
            }
            for (s = 0; (a = d[s++]) && !n.isPropagationStopped(); )
            {
                n.type = s > 1 ? l : p.bindType || h, f = (mt.get(a, "events") || {})[n.type] && mt.get(a, 
                "handle"), f && f.apply(a, r), f = c && a[c], f && ot.acceptData(a) && f.apply && f.apply(a, 
                r) === !1 && n.preventDefault();
            }
            return n.type = h, o || n.isDefaultPrevented() || p._default && p._default.apply(d.pop(), 
            r) !== !1 || !ot.acceptData(i) || c && ot.isFunction(i[h]) && !ot.isWindow(i) && (u = i[c], 
            u && (i[c] = null), ot.event.triggered = h, i[h](), ot.event.triggered = t, u && (i[c] = u)), 
            n.result;
        }
    },
    dispatch : function (e)
    {
        e = ot.event.fix(e);
        var n, r, i, o, s, a = [], u = et.call(arguments), l = (mt.get(this, "events") || {})[e.type] || [], 
        c = ot.event.special[e.type] || {};
        if (u[0] = e, e.delegateTarget = this, !c.preDispatch || c.preDispatch.call(this, e) !== !1)
        {
            for (a = ot.event.handlers.call(this, e, l), n = 0; (o = a[n++]) && !e.isPropagationStopped(); )
            {
                for (e.currentTarget = o.elem, r = 0; (s = o.handlers[r++]) && !e.isImmediatePropagationStopped(); )
                {
                    (!e.namespace_re || e.namespace_re.test(s.namespace)) && (e.handleObj = s, e.data = s.data, 
                    i = ((ot.event.special[s.origType] || {}).handle || s.handler).apply(o.elem, u), 
                    i !== t && (e.result = i) === !1 && (e.preventDefault(), e.stopPropagation()));
                }
            }
            return c.postDispatch && c.postDispatch.call(this, e), e.result;
        }
    },
    handlers : function (e, n)
    {
        var r, i, o, s, a = [], u = n.delegateCount, l = e.target;
        if (u && l.nodeType && (!e.button || "click" !== e.type))
        {
            for (; l !== this; l = l.parentNode || this)
            {
                if (l.disabled !== !0 || "click" !== e.type) 
                {
                    for (i = [], r = 0; u > r; r++)
                    {
                        s = n[r], o = s.selector + " ", i[o] === t && (i[o] = s.needsContext ? ot(o, 
                        this).index(l) >= 0 : ot.find(o, this, null, [l]).length), i[o] && i.push(s);
                    }
                    i.length && a.push({
                        elem : l, handlers : i 
                    }) 
                }
                return u < n.length && a.push( 
                {
                    elem : this, handlers : n.slice(u) 
                }), a;;
            }
        }
    },
    props : "altKey bubbles cancelable ctrlKey currentTarget eventPhase metaKey relatedTarget shiftKey target timeStamp view which".split(" "), 
    fixHooks : {}, keyHooks : 
    {
        props : "char charCode key keyCode".split(" "),
        filter : function (e, t)
        {
            return null == e.which && (e.which = null != t.charCode ? t.charCode : t.keyCode), e;
        }
    },
    mouseHooks : 
    {
        props : "button buttons clientX clientY offsetX offsetY pageX pageY screenX screenY toElement".split(" "), 
        filter : function (e, n)
        {
            var r, i, o, s = n.button;
            return null == e.pageX && null != n.clientX && (r = e.target.ownerDocument || X, i = r.documentElement, 
            o = r.body, e.pageX = n.clientX + (i && i.scrollLeft || o && o.scrollLeft || 0) - (i && i.clientLeft || o && o.clientLeft || 0), 
            e.pageY = n.clientY + (i && i.scrollTop || o && o.scrollTop || 0) - (i && i.clientTop || o && o.clientTop || 0)), 
            e.which || s === t || (e.which = 1 & s ? 1 : 2 & s ? 3 : 4 & s ? 2 : 0), e;
        }
    },
    fix : function (e)
    {
        if (e[ot.expando]) {
            return e;
        }
        var t, n, r, i = e.type, o = e, s = this.fixHooks[i];
        for (s || (this.fixHooks[i] = s = Nt.test(i) ? this.mouseHooks : kt.test(i) ? this.keyHooks : {}), 
        r = s.props ? this.props.concat(s.props) : this.props, e = new ot.Event(o), t = r.length;
        t--;
        ) n = r[t], e[n] = o[n];
        return e.target || (e.target = X), 3 === e.target.nodeType && (e.target = e.target.parentNode), 
        s.filter ? s.filter(e, o) : e;
    },
    special : 
    {
        load : {
            noBubble :!0
        },
        focus : 
        {
            trigger : function ()
            {
                return this !== u() && this.focus ? (this.focus(), !1) : void 0;
            },
            delegateType : "focusin"
        },
        blur : 
        {
            trigger : function ()
            {
                return this === u() && this.blur ? (this.blur(), !1) : void 0;
            },
            delegateType : "focusout"
        },
        click : 
        {
            trigger : function ()
            {
                return "checkbox" === this.type && this.click && ot.nodeName(this, "input") ? (this.click(), 
                !1) : void 0
            },
            _default : function (e)
            {
                return ot.nodeName(e.target, "a");
            }
        },
        beforeunload : {
            postDispatch : function (e)
            {
                e.result !== t && (e.originalEvent.returnValue = e.result);
            }
        }
    },
    simulate : function (e, t, n, r)
    {
        var i = ot.extend(new ot.Event, n, {
            type : e, isSimulated :!0, originalEvent : {}
        });
        r ? ot.event.trigger(i, null, t) : ot.event.dispatch.call(t, i), i.isDefaultPrevented() && n.preventDefault()
    }
},
ot.removeEvent = function (e, t, n)
{
    e.removeEventListener && e.removeEventListener(t, n, !1)
},
ot.Event = function (e, t)
{
    return this instanceof ot.Event ? (e && e.type ? (this.originalEvent = e, this.type = e.type, 
    this.isDefaultPrevented = e.defaultPrevented || e.getPreventDefault && e.getPreventDefault() ? s : a) : this.type = e, 
    t && ot.extend(this, t), this.timeStamp = e && e.timeStamp || ot.now(), void (this [ot.expando] = !0)) : new ot.Event(e, 
    t);
},
ot.Event.prototype = 
{
    isDefaultPrevented : a, isPropagationStopped : a, isImmediatePropagationStopped : a,
    preventDefault : function ()
    {
        var e = this.originalEvent;
        this.isDefaultPrevented = s, e && e.preventDefault && e.preventDefault();
    },
    stopPropagation : function ()
    {
        var e = this.originalEvent;
        this.isPropagationStopped = s, e && e.stopPropagation && e.stopPropagation();
    },
    stopImmediatePropagation : function ()
    {
        this.isImmediatePropagationStopped = s, this.stopPropagation();
    }
},
ot.each({
    mouseenter : "mouseover", mouseleave : "mouseout"
},
function (e, t)
{
    ot.event.special[e] = 
    {
        delegateType : t, bindType : t,
        handle : function (e)
        {
            var n, r = this, i = e.relatedTarget, o = e.handleObj;
            return (!i || i !== r && !ot.contains(r, i)) && (e.type = o.origType, n = o.handler.apply(this, 
            arguments), e.type = t), n;
        }
    }
}), ot.support.focusinBubbles || ot.each({
    focus : "focusin", blur : "focusout"
},
function (e, t)
{
    var n = 0, r = function (e)
    {
        ot.event.simulate(t, e.target, ot.event.fix(e), !0)
    };
    ot.event.special[t] = 
    {
        setup : function ()
        {
            0 === n++&& X.addEventListener(e, r, !0)
        },
        teardown : function ()
        {
            0 ===--n && X.removeEventListener(e, r, !0)
        }
    }
}), ot.fn.extend(
{
    on : function (e, n, r, i, o)
    {
        var s, u;
        if ("object" == typeof e) {
            "string" != typeof n && (r = r || n, n = t);
            for (u in e) {
                this.on(u, n, r, e[u], o);
            }
            return this
        }
        if (null == r && null == i ? (i = n, r = n = t) : null == i && ("string" == typeof n ? (i = r, 
        r = t) : (i = r, r = n, n = t)), i === !1) i = a;
        else if (!i) return this;
        return 1 === o && (s = i, i = function (e)
        {
            return ot().off(e), s.apply(this, arguments);
        },
        i.guid = s.guid || (s.guid = ot.guid++)), this.each(function ()
        {
            ot.event.add(this, e, i, r, n)
        })
    },
    one : function (e, t, n, r)
    {
        return this.on(e, t, n, r, 1);
    },
    off : function (e, n, r)
    {
        var i, o;
        if (e && e.preventDefault && e.handleObj)
        {
            return i = e.handleObj, ot(e.delegateTarget).off(i.namespace ? i.origType + "." + i.namespace : i.origType, 
            i.selector, i.handler), this;
        }
        if ("object" == typeof e) {
            for (o in e) {
                this.off(o, n, e[o]);
            }
            return this
        }
        return (n === !1 || "function" == typeof n) && (r = n, n = t), r === !1 && (r = a), this.each(function () {
            ot.event.remove(this, e, r, n)
        })
    },
    trigger : function (e, t)
    {
        return this.each(function ()
        {
            ot.event.trigger(e, t, this)
        })
    },
    triggerHandler : function (e, t)
    {
        var n = this [0];
        return n ? ot.event.trigger(e, t, n, !0) : void 0;
    }
});
var St = /^.[^:#\[\.,]*$/, Dt = /^(?:parents|prev(?:Until|All))/, At = ot.expr.match.needsContext, 
qt = {
    children :!0, contents :!0, next :!0, prev :!0
};
ot.fn.extend(
{
    find : function (e)
    {
        var t, n = [], r = this, i = r.length;
        if ("string" != typeof e)
        {
            return this.pushStack(ot(e).filter(function () 
            {
                for (t = 0; i > t; t++) {
                    if (ot.contains(r[t], this)) {
                        return!0 ;
                    }
                }
            }));
        }
        for (t = 0; i > t; t++) {
            ot.find(e, r[t], n);
        }
        return n = this.pushStack(i > 1 ? ot.unique(n) : n), n.selector = this.selector ? this.selector + " " + e : e, 
        n;
    },
    has : function (e)
    {
        var t = ot(e, this), n = t.length;
        return this.filter(function ()
        {
            for (var e = 0; n > e; e++) {
                if (ot.contains(this, t[e])) {
                    return!0;
                }
            }
        })
    },
    not : function (e)
    {
        return this.pushStack(c(this, e || [], !0));
    },
    filter : function (e)
    {
        return this.pushStack(c(this, e || [], !1));
    },
    is : function (e)
    {
        return!!c(this, "string" == typeof e && At.test(e) ? ot(e) : e || [], !1).length
    },
    closest : function (e, t)
    {
        for (var n, r = 0, i = this.length, o = [], s = At.test(e) || "string" != typeof e ? ot(e, 
        t || this.context) : 0;
        i > r;
        r++) for (n = this [r];
        n && n !== t;
        n = n.parentNode) if (n.nodeType < 11 && (s ? s.index(n) > -1 : 1 === n.nodeType && ot.find.matchesSelector(n, 
        e))) {
            n = o.push(n);
            break
        }
        return this.pushStack(o.length > 1 ? ot.unique(o) : o);
    },
    index : function (e)
    {
        return e ? "string" == typeof e ? tt.call(ot(e), this [0]) : tt.call(this, e.jquery ? e[0] : e) : this [0] && this [0].parentNode ? this.first().prevAll().length :- 1;
    },
    add : function (e, t)
    {
        var n = "string" == typeof e ? ot(e, t) : ot.makeArray(e && e.nodeType ? [e] : e), r = ot.merge(this.get(), 
        n);
        return this.pushStack(ot.unique(r));
    },
    addBack : function (e)
    {
        return this.add(null == e ? this.prevObject : this.prevObject.filter(e));
    }
}), ot.each(
{
    parent : function (e)
    {
        var t = e.parentNode;
        return t && 11 !== t.nodeType ? t : null;
    },
    parents : function (e)
    {
        return ot.dir(e, "parentNode");
    },
    parentsUntil : function (e, t, n)
    {
        return ot.dir(e, "parentNode", n);
    },
    next : function (e)
    {
        return l(e, "nextSibling");
    },
    prev : function (e)
    {
        return l(e, "previousSibling");
    },
    nextAll : function (e)
    {
        return ot.dir(e, "nextSibling");
    },
    prevAll : function (e)
    {
        return ot.dir(e, "previousSibling");
    },
    nextUntil : function (e, t, n)
    {
        return ot.dir(e, "nextSibling", n);
    },
    prevUntil : function (e, t, n)
    {
        return ot.dir(e, "previousSibling", n);
    },
    siblings : function (e)
    {
        return ot.sibling((e.parentNode || {}).firstChild, e);
    },
    children : function (e)
    {
        return ot.sibling(e.firstChild);
    },
    contents : function (e)
    {
        return e.contentDocument || ot.merge([], e.childNodes);
    }
},
function (e, t)
{
    ot.fn[e] = function (n, r)
    {
        var i = ot.map(this, t, n);
        return "Until" !== e.slice(-5) && (r = n), r && "string" == typeof r && (i = ot.filter(r, 
        i)), this.length > 1 && (qt[e] || ot.unique(i), Dt.test(e) && i.reverse()), this.pushStack(i);
    }
}), ot.extend(
{
    filter : function (e, t, n)
    {
        var r = t[0];
        return n && (e = ":not(" + e + ")"), 1 === t.length && 1 === r.nodeType ? ot.find.matchesSelector(r, 
        e) ? [r] : [] : ot.find.matches(e, ot.grep(t, function (e)
        {
            return 1 === e.nodeType;
        }))
    },
    dir : function (e, n, r)
    {
        for (var i = [], o = r !== t; (e = e[n]) && 9 !== e.nodeType; ) {
            if (1 === e.nodeType) {
                if (o && ot(e).is(r)) {
                    break;
                }
                i.push(e) 
            }
            return i;
        }
    },
    sibling : function (e, t)
    {
        for (var n = []; e; e = e.nextSibling) {
            1 === e.nodeType && e !== t && n.push(e);
        }
        return n;
    }
});
var Lt = /<(?!area|br|col|embed|hr|img|input|link|meta|param)(([\w:]+)[^>]*)\/>/gi, Ht = /<([\w:]+)/, 
Ot = /<|&#?\w+;/, Ft = /<(?:script|style|link)/i, Pt = /^(?:checkbox|radio)$/i, Rt = /checked\s*(?:[^=]|=\s*.checked.)/i, 
Mt = /^$|\/(?:java|ecma)script/i, Wt = /^true\/(.*)/, $t = /^\s*<!(?:\[CDATA\[|--)|(?:\]\]|--)>\s*$/g, 
Bt = 
{
    option : [1, "<select multiple='multiple'>", "</select>"], thead : [1, "<table>", "</table>"], 
    col : [2, "<table><colgroup>", "</colgroup></table>"], tr : [2, "<table><tbody>", "</tbody></table>"], 
    td : [3, "<table><tbody><tr>", "</tr></tbody></table>"], _default : [0, "", ""]
};
Bt.optgroup = Bt.option, Bt.tbody = Bt.tfoot = Bt.colgroup = Bt.caption = Bt.thead, Bt.th = Bt.td, 
ot.fn.extend(
{
    text : function (e)
    {
        return ot.access(this, function (e)
        {
            return e === t ? ot.text(this) : this.empty().append((this [0] && this [0].ownerDocument || X).createTextNode(e));
        },
        null, e, arguments.length)
    },
    append : function ()
    {
        return this.domManip(arguments, function (e)
        {
            if (1 === this.nodeType || 11 === this.nodeType || 9 === this.nodeType) {
                var t = f(this, e);
                t.appendChild(e)
            }
        })
    },
    prepend : function ()
    {
        return this.domManip(arguments, function (e)
        {
            if (1 === this.nodeType || 11 === this.nodeType || 9 === this.nodeType) {
                var t = f(this, e);
                t.insertBefore(e, t.firstChild)
            }
        })
    },
    before : function ()
    {
        return this.domManip(arguments, function (e)
        {
            this.parentNode && this.parentNode.insertBefore(e, this)
        })
    },
    after : function ()
    {
        return this.domManip(arguments, function (e)
        {
            this.parentNode && this.parentNode.insertBefore(e, this.nextSibling)
        })
    },
    remove : function (e, t)
    {
        for (var n, r = e ? ot.filter(e, this) : this, i = 0; null != (n = r[i]); i++)
        {
            t || 1 !== n.nodeType || ot.cleanData(m(n)), n.parentNode && (t && ot.contains(n.ownerDocument, 
            n) && h(m(n, "script")), n.parentNode.removeChild(n));
        }
        return this;
    },
    empty : function ()
    {
        for (var e, t = 0; null != (e = this [t]); t++) {
            1 === e.nodeType && (ot.cleanData(m(e, !1)), e.textContent = "");
        }
        return this;
    },
    clone : function (e, t)
    {
        return e = null == e ?!1 : e, t = null == t ? e : t, this.map(function ()
        {
            return ot.clone(this, e, t);
        })
    },
    html : function (e)
    {
        return ot.access(this, function (e)
        {
            var n = this [0] || {}, r = 0, i = this.length;
            if (e === t && 1 === n.nodeType) {
                return n.innerHTML;
            }
            if ("string" == typeof e && !Ft.test(e) && !Bt[(Ht.exec(e) || ["", ""])[1].toLowerCase()])
            {
                e = e.replace(Lt, "<$1></$2>");
                try
                {
                    for (; i > r; r++) {
                        n = this [r] || {}, 1 === n.nodeType && (ot.cleanData(m(n, !1)), n.innerHTML = e);
                    }
                    n = 0
                }
                catch (o) {}
            }
            n && this.empty().append(e)
        },
        null, e, arguments.length)
    },
    replaceWith : function ()
    {
        var e = ot.map(this, function (e)
        {
            return [e.nextSibling, e.parentNode];
        }), t = 0;
        return this.domManip(arguments, function (n)
        {
            var r = e[t++], i = e[t++];
            i && (r && r.parentNode !== i && (r = this.nextSibling), ot(this).remove(), i.insertBefore(n, 
            r));
        },
        !0), t ? this : this.remove()
    },
    detach : function (e)
    {
        return this.remove(e, !0);
    },
    domManip : function (e, t, n)
    {
        e = K.apply([], e);
        var r, i, o, s, a, u, l = 0, c = this.length, f = this, h = c - 1, g = e[0], y = ot.isFunction(g);
        if (y || !(1 >= c || "string" != typeof g || ot.support.checkClone) && Rt.test(g))
        {
            return this.each(function (r) 
            {
                var i = f.eq(r);
                y && (e[0] = g.call(this, r, i.html())), i.domManip(e, t, n);
            });
        }
        if (c && (r = ot.buildFragment(e, this [0].ownerDocument, !1, !n && this), i = r.firstChild, 
        1 === r.childNodes.length && (r = i), i))
        {
            for (o = ot.map(m(r, "script"), p), s = o.length; c > l; l++)
            {
                a = r, l !== h && (a = ot.clone(a, !0, !0), s && ot.merge(o, m(a, "script"))), t.call(this [l], 
                a, l);
            }
            if (s)
            {
                for (u = o[o.length - 1].ownerDocument, ot.map(o, d), l = 0; s > l; l++)
                {
                    a = o[l], Mt.test(a.type || "") && !mt.access(a, "globalEval") && ot.contains(u, 
                    a) && (a.src ? ot._evalUrl(a.src) : ot.globalEval(a.textContent.replace($t, "")));
                }
            }
        }
        return this;
    }
}), ot.each(
{
    appendTo : "append", prependTo : "prepend", insertBefore : "before", insertAfter : "after", replaceAll : "replaceWith"
},
function (e, t)
{
    ot.fn[e] = function (e)
    {
        for (var n, r = [], i = ot(e), o = i.length - 1, s = 0; o >= s; s++) {
            n = s === o ? this : this.clone(!0), ot(i[s])[t](n), Z.apply(r, n.get());
        }
        return this.pushStack(r);
    }
}), ot.extend(
{
    clone : function (e, t, n)
    {
        var r, i, o, s, a = e.cloneNode(!0), u = ot.contains(e.ownerDocument, e);
        if (!(ot.support.noCloneChecked || 1 !== e.nodeType && 11 !== e.nodeType || ot.isXMLDoc(e))) {
            for (s = m(a), o = m(e), r = 0, i = o.length; i > r; r++) {
                y(o[r], s[r]);
            }
        }
        if (t)
        {
            if (n) {
                for (o = o || m(e), s = s || m(a), r = 0, i = o.length; i > r; r++) {
                    g(o[r], s[r]);
                }
            }
            else {
                g(e, a);
            }
            return s = m(a, "script"), s.length > 0 && h(s, !u && m(e, "script")), a;
        }
    },
    buildFragment : function (e, t, n, r)
    {
        for (var i, o, s, a, u, l, c = 0, f = e.length, p = t.createDocumentFragment(), d = []; f > c; c++)
        {
            if (i = e[c], i || 0 === i)
            {
                if ("object" === ot.type(i)) {
                    ot.merge(d, i.nodeType ? [i] : i);
                }
                else if (Ot.test(i)) 
                {
                    for (o = o || p.appendChild(t.createElement("div")), s = (Ht.exec(i) || ["", ""])[1].toLowerCase(), 
                    a = Bt[s] || Bt._default, o.innerHTML = a[1] + i.replace(Lt, "<$1></$2>") + a[2], 
                    l = a[0];
                    l--;
                    ) o = o.lastChild;
                    ot.merge(d, o.childNodes), o = p.firstChild, o.textContent = "" 
                }
                else {
                    d.push(t.createTextNode(i));
                }
                for (p.textContent = "", c = 0; i = d[c++]; )
                {
                    if ((!r ||- 1 === ot.inArray(i, r)) && (u = ot.contains(i.ownerDocument, i), o = m(p.appendChild(i), 
                    "script"), u && h(o), n)) for (l = 0;
                    i = o[l++];
                    ) Mt.test(i.type || "") && n.push(i);
                    return p;;;
                }
            }
        }
    },
    cleanData : function (e)
    {
        for (var n, r, o, s, a, u, l = ot.event.special, c = 0; (r = e[c]) !== t; c++)
        {
            if (i.accepts(r) && (a = r[mt.expando], a && (n = mt.cache[a])))
            {
                if (o = Object.keys(n.events || {}), o.length) for (u = 0;
                (s = o[u]) !== t;
                u++) l[s] ? ot.event.remove(r, s) : ot.removeEvent(r, s, n.handle);
                mt.cache[a] && delete mt.cache[a]
            }
            delete gt.cache[r[gt.expando]]
        }
    },
    _evalUrl : function (e)
    {
        return ot.ajax(
        {
            url : e, type : "GET", dataType : "script", async :!1, global :!1, "throws" :!0
        })
    }
}), ot.fn.extend(
{
    wrapAll : function (e)
    {
        var t;
        return ot.isFunction(e) ? this.each(function (t)
        {
            ot(this).wrapAll(e.call(this, t))
        }) : (this [0] && (t = ot(e, this [0].ownerDocument).eq(0).clone(!0), this [0].parentNode && t.insertBefore(this [0]), 
        t.map(function ()
        {
            for (var e = this; e.firstElementChild; ) {
                e = e.firstElementChild;
            }
            return e;
        }).append(this)), this)
    },
    wrapInner : function (e)
    {
        return this.each(ot.isFunction(e) ? function (t)
        {
            ot(this).wrapInner(e.call(this, t))
        }
         : function ()
         {
             var t = ot(this), n = t.contents();
             n.length ? n.wrapAll(e) : t.append(e)
         })
    },
    wrap : function (e)
    {
        var t = ot.isFunction(e);
        return this.each(function (n)
        {
            ot(this).wrapAll(t ? e.call(this, n) : e)
        })
    },
    unwrap : function ()
    {
        return this.parent().each(function ()
        {
            ot.nodeName(this, "body") || ot(this).replaceWith(this.childNodes)
        }).end()
    }
});
var It, zt, _t = /^(none|table(?!-c[ea]).+)/, Xt = /^margin/, Ut = new RegExp("^(" + st + ")(.*)$", 
"i"), Yt = new RegExp("^(" + st + ")(?!px)[a-z%]+$", "i"), Vt = new RegExp("^([+-])=(" + st + ")", 
"i"), Gt = {
    BODY : "block"
},
Qt = {
    position : "absolute", visibility : "hidden", display : "block"
},
Jt = {
    letterSpacing : 0, fontWeight : 400
},
Kt = ["Top", "Right", "Bottom", "Left"], Zt = ["Webkit", "O", "Moz", "ms"];
ot.fn.extend(
{
    css : function (e, n)
    {
        return ot.access(this, function (e, n, r)
        {
            var i, o, s = {}, a = 0;
            if (ot.isArray(n)) {
                for (i = b(e), o = n.length; o > a; a++) {
                    s[n[a]] = ot.css(e, n[a], !1, i);
                }
                return s
            }
            return r !== t ? ot.style(e, n, r) : ot.css(e, n);
        },
        e, n, arguments.length > 1)
    },
    show : function ()
    {
        return w(this, !0);
    },
    hide : function ()
    {
        return w(this);
    },
    toggle : function (e)
    {
        return "boolean" == typeof e ? e ? this.show() : this.hide() : this.each(function () {
            x(this) ? ot(this).show() : ot(this).hide()
        })
    }
}), ot.extend(
{
    cssHooks : {
        opacity : {
            get : function (e, t)
            {
                if (t) {
                    var n = It(e, "opacity");
                    return "" === n ? "1" : n;
                }
            }
        }
    },
    cssNumber : 
    {
        columnCount :!0, fillOpacity :!0, fontWeight :!0, lineHeight :!0, opacity :!0, order :!0, 
        orphans :!0, widows :!0, zIndex :!0, zoom :!0
    },
    cssProps : {
        "float" : "cssFloat"
    },
    style : function (e, n, r, i)
    {
        if (e && 3 !== e.nodeType && 8 !== e.nodeType && e.style)
        {
            var o, s, a, u = ot.camelCase(n), l = e.style;
            return n = ot.cssProps[u] || (ot.cssProps[u] = v(l, u)), a = ot.cssHooks[n] || ot.cssHooks[u], 
            r === t ? a && "get"in a && (o = a.get(e, !1, i)) !== t ? o : l[n] : (s = typeof r, "string" === s && (o = Vt.exec(r)) && (r = (o[1] + 1) * o[2] + parseFloat(ot.css(e, 
            n)), s = "number"), null == r || "number" === s && isNaN(r) || ("number" !== s || ot.cssNumber[u] || (r += "px"), 
            ot.support.clearCloneStyle || "" !== r || 0 !== n.indexOf("background") || (l[n] = "inherit"), 
            a && "set"in a && (r = a.set(e, r, i)) === t || (l[n] = r)), void 0);
        }
    },
    css : function (e, n, r, i)
    {
        var o, s, a, u = ot.camelCase(n);
        return n = ot.cssProps[u] || (ot.cssProps[u] = v(e.style, u)), a = ot.cssHooks[n] || ot.cssHooks[u], 
        a && "get"in a && (o = a.get(e, !0, r)), o === t && (o = It(e, n, i)), "normal" === o && n in Jt && (o = Jt[n]), 
        "" === r || r ? (s = parseFloat(o), r === !0 || ot.isNumeric(s) ? s || 0 : o) : o;
    }
}), It = function (e, n, r)
{
    var i, o, s, a = r || b(e), u = a ? a.getPropertyValue(n) || a[n] : t, l = e.style;
    return a && ("" !== u || ot.contains(e.ownerDocument, e) || (u = ot.style(e, n)), Yt.test(u) && Xt.test(n) && (i = l.width, 
    o = l.minWidth, s = l.maxWidth, l.minWidth = l.maxWidth = l.width = u, u = a.width, l.width = i, 
    l.minWidth = o, l.maxWidth = s)), u;
},
ot.each(["height", "width"], function (e, t)
{
    ot.cssHooks[t] = 
    {
        get : function (e, n, r)
        {
            return n ? 0 === e.offsetWidth && _t.test(ot.css(e, "display")) ? ot.swap(e, Qt, function ()
            {
                return k(e, t, r);
            }) : k(e, t, r) : void 0
        },
        set : function (e, n, r)
        {
            var i = r && b(e);
            return T(e, n, r ? C(e, t, r, ot.support.boxSizing && "border-box" === ot.css(e, "boxSizing", 
            !1, i), i) : 0)
        }
    }
}), ot(function ()
{
    ot.support.reliableMarginRight || (ot.cssHooks.marginRight = 
    {
        get : function (e, t)
        {
            return t ? ot.swap(e, 
            {
                display : "inline-block"
            },
            It, [e, "marginRight"]) : void 0
        }
    }), !ot.support.pixelPosition && ot.fn.position && ot.each(["top", "left"], function (e, t)
    {
        ot.cssHooks[t] = 
        {
            get : function (e, n)
            {
                return n ? (n = It(e, t), Yt.test(n) ? ot(e).position()[t] + "px" : n) : void 0;
            }
        }
    })
}), ot.expr && ot.expr.filters && (ot.expr.filters.hidden = function (e)
{
    return e.offsetWidth <= 0 && e.offsetHeight <= 0;
},
ot.expr.filters.visible = function (e)
{
    return!ot.expr.filters.hidden(e)
}), ot.each({
    margin : "", padding : "", border : "Width"
},
function (e, t)
{
    ot.cssHooks[e + t] = 
    {
        expand : function (n)
        {
            for (var r = 0, i = {}, o = "string" == typeof n ? n.split(" ") : [n]; 4 > r; r++) i[e + Kt[r] + t] = o[r] || o[r - 2] || o[0]; return i
        }
    },
    Xt.test(e) || (ot.cssHooks[e + t].set = T);
});
var en = /%20/g, tn = /\[\]$/, nn = /\r?\n/g, rn = /^(?:submit|button|image|reset|file)$/i, on = /^(?:input|select|textarea|keygen)/i;
ot.fn.extend(
{
    serialize : function ()
    {
        return ot.param(this.serializeArray());
    },
    serializeArray : function ()
    {
        return this.map(function ()
        {
            var e = ot.prop(this, "elements");
            return e ? ot.makeArray(e) : this;
        }).filter(function ()
        {
            var e = this.type;
            return this.name && !ot(this).is(":disabled") && on.test(this.nodeName) && !rn.test(e) && (this.checked || !Pt.test(e));
        }).map(function (e, t)
        {
            var n = ot(this).val();
            return null == n ? null : ot.isArray(n) ? ot.map(n, function (e)
            {
                return {
                    name : t.name, value : e.replace(nn, "\r\n")
                }
            }) : {
                name : t.name, value : n.replace(nn, "\r\n")
            }
        }).get()
    }
}), ot.param = function (e, n)
{
    var r, i = [], o = function (e, t)
    {
        t = ot.isFunction(t) ? t() : null == t ? "" : t, i[i.length] = encodeURIComponent(e) + "=" + encodeURIComponent(t);
    };
    if (n === t && (n = ot.ajaxSettings && ot.ajaxSettings.traditional), ot.isArray(e) || e.jquery && !ot.isPlainObject(e)) {
        ot.each(e, function () 
        {
            o(this.name, this.value) 
        });
    }
    else {
        for (r in e) {
            E(r, e[r], n, o);
        }
    }
    return i.join("&").replace(en, "+");
},
ot.each("blur focus focusin focusout load resize scroll unload click dblclick mousedown mouseup mousemove mouseover mouseout mouseenter mouseleave change select submit keydown keypress keyup error contextmenu".split(" "), 
function (e, t)
{
    ot.fn[t] = function (e, n)
    {
        return arguments.length > 0 ? this.on(t, null, e, n) : this.trigger(t);
    }
}), ot.fn.extend(
{
    hover : function (e, t)
    {
        return this.mouseenter(e).mouseleave(t || e);
    },
    bind : function (e, t, n)
    {
        return this.on(e, null, t, n);
    },
    unbind : function (e, t)
    {
        return this.off(e, null, t);
    },
    delegate : function (e, t, n, r)
    {
        return this.on(t, e, n, r);
    },
    undelegate : function (e, t, n)
    {
        return 1 === arguments.length ? this.off(e, "**") : this.off(t, e || "**", n);
    }
});
var sn, an, un = ot.now(), ln = /\?/, cn = /#.*$/, fn = /([?&])_=[^&]*/, pn = /^(.*?):[ \t]*([^\r\n]*)$/gm, 
dn = /^(?:about|app|app-storage|.+-extension|file|res|widget):$/, hn = /^(?:GET|HEAD)$/, gn = /^\/\//, 
mn = /^([\w.+-]+:)(?:\/\/([^\/?#:]*)(?::(\d+)|)|)/, yn = ot.fn.load, vn = {},
xn = {}, bn = "*/".concat("*");
try {
    an = _.href
}
catch (wn) {
    an = X.createElement("a"), an.href = "", an = an.href
}
sn = mn.exec(an.toLowerCase()) || [], ot.fn.load = function (e, n, r)
{
    if ("string" != typeof e && yn) {
        return yn.apply(this, arguments);
    }
    var i, o, s, a = this, u = e.indexOf(" ");
    return u >= 0 && (i = e.slice(u), e = e.slice(0, u)), ot.isFunction(n) ? (r = n, n = t) : n && "object" == typeof n && (o = "POST"), 
    a.length > 0 && ot.ajax({
        url : e, type : o, dataType : "html", data : n
    }).done(function (e)
    {
        s = arguments, a.html(i ? ot("<div>").append(ot.parseHTML(e)).find(i) : e);
    }).complete(r && function (e, t)
    {
        a.each(r, s || [e.responseText, t, e])
    }), this
},
ot.each(["ajaxStart", "ajaxStop", "ajaxComplete", "ajaxError", "ajaxSuccess", "ajaxSend"], function (e, 
t) {
    ot.fn[t] = function (e)
    {
        return this.on(t, e);
    }
}), ot.extend(
{
    active : 0, lastModified : {}, etag : {}, ajaxSettings : 
    {
        url : an, type : "GET", isLocal : dn.test(sn[1]), global :!0, processData :!0, async :!0, 
        contentType : "application/x-www-form-urlencoded; charset=UTF-8", accepts : 
        {
            "*" : bn, text : "text/plain", html : "text/html", xml : "application/xml, text/xml", 
            json : "application/json, text/javascript"
        },
        contents : {
            xml : /xml/, html : /html/, json : /json/
        },
        responseFields : {
            xml : "responseXML", text : "responseText", json : "responseJSON"
        },
        converters : {
            "* text" : String, "text html" :!0, "text json" : ot.parseJSON, "text xml" : ot.parseXML
        },
        flatOptions : {
            url :!0, context :!0
        }
    },
    ajaxSetup : function (e, t)
    {
        return t ? A(A(e, ot.ajaxSettings), t) : A(ot.ajaxSettings, e);
    },
    ajaxPrefilter : S(vn), ajaxTransport : S(xn),
    ajax : function (e, n)
    {
        function r(e, n, r, a)
        {
            var l, f, v, x, w, C = n;
            2 !== b && (b = 2, u && clearTimeout(u), i = t, s = a || "", T.readyState = e > 0 ? 4 : 0, 
            l = e >= 200 && 300 > e || 304 === e, r && (x = q(p, T, r)), x = L(p, x, T, l), l ? (p.ifModified && (w = T.getResponseHeader("Last-Modified"), 
            w && (ot.lastModified[o] = w), w = T.getResponseHeader("etag"), w && (ot.etag[o] = w)), 
            204 === e || "HEAD" === p.type ? C = "nocontent" : 304 === e ? C = "notmodified" : (C = x.state, 
            f = x.data, v = x.error, l = !v)) : (v = C, (e || !C) && (C = "error", 0 > e && (e = 0))), 
            T.status = e, T.statusText = (n || C) + "", l ? g.resolveWith(d, [f, C, T]) : g.rejectWith(d, 
            [T, C, v]), T.statusCode(y), y = t, c && h.trigger(l ? "ajaxSuccess" : "ajaxError", [T, 
            p, l ? f : v]), m.fireWith(d, [T, C]), c && (h.trigger("ajaxComplete", [T, p]), --ot.active || ot.event.trigger("ajaxStop")))
        }
        "object" == typeof e && (n = e, e = t), n = n || {};
        var i, o, s, a, u, l, c, f, p = ot.ajaxSetup({}, n), d = p.context || p, h = p.context && (d.nodeType || d.jquery) ? ot(d) : ot.event, 
        g = ot.Deferred(), m = ot.Callbacks("once memory"), y = p.statusCode || {},
        v = {}, x = {}, b = 0, w = "canceled", T = 
        {
            readyState : 0,
            getResponseHeader : function (e)
            {
                var t;
                if (2 === b) {
                    if (!a) {
                        for (a = {}; t = pn.exec(s); ) a[t[1].toLowerCase()] = t[2]; 
                    }
                    t = a[e.toLowerCase()]
                }
                return null == t ? null : t;
            },
            getAllResponseHeaders : function ()
            {
                return 2 === b ? s : null;
            },
            setRequestHeader : function (e, t)
            {
                var n = e.toLowerCase();
                return b || (e = x[n] = x[n] || e, v[e] = t), this;
            },
            overrideMimeType : function (e)
            {
                return b || (p.mimeType = e), this;
            },
            statusCode : function (e)
            {
                var t;
                if (e) {
                    if (2 > b) {
                        for (t in e) {
                            y[t] = [y[t], e[t]];
                        }
                    }
                    else {
                        T.always(e[T.status]);
                    }
                    return this;
                }
            },
            abort : function (e)
            {
                var t = e || w;
                return i && i.abort(t), r(0, t), this;
            }
        };
        if (g.promise(T).complete = m.add, T.success = T.done, T.error = T.fail, p.url = ((e || p.url || an) + "").replace(cn, 
        "").replace(gn, sn[1] + "//"), p.type = n.method || n.type || p.method || p.type, p.dataTypes = ot.trim(p.dataType || "*").toLowerCase().match(at) || [""], 
        null == p.crossDomain && (l = mn.exec(p.url.toLowerCase()), p.crossDomain = !(!l || l[1] === sn[1] && l[2] === sn[2] && (l[3] || ("http:" === l[1] ? "80" : "443")) === (sn[3] || ("http:" === sn[1] ? "80" : "443")))), 
        p.data && p.processData && "string" != typeof p.data && (p.data = ot.param(p.data, p.traditional)), 
        D(vn, p, n, T), 2 === b) return T;
        c = p.global, c && 0 === ot.active++&& ot.event.trigger("ajaxStart"), p.type = p.type.toUpperCase(), 
        p.hasContent = !hn.test(p.type), o = p.url, p.hasContent || (p.data && (o = p.url += (ln.test(o) ? "&" : "?") + p.data, 
        delete p.data), p.cache === !1 && (p.url = fn.test(o) ? o.replace(fn, "$1_=" + un++) : o + (ln.test(o) ? "&" : "?") + "_=" + un++)), 
        p.ifModified && (ot.lastModified[o] && T.setRequestHeader("If-Modified-Since", ot.lastModified[o]), 
        ot.etag[o] && T.setRequestHeader("If-None-Match", ot.etag[o])), (p.data && p.hasContent && p.contentType !== !1 || n.contentType) && T.setRequestHeader("Content-Type", 
        p.contentType), T.setRequestHeader("Accept", p.dataTypes[0] && p.accepts[p.dataTypes[0]] ? p.accepts[p.dataTypes[0]] + ("*" !== p.dataTypes[0] ? ", " + bn + "; q=0.01" : "") : p.accepts["*"]);
        for (f in p.headers) {
            T.setRequestHeader(f, p.headers[f]);
        }
        if (p.beforeSend && (p.beforeSend.call(d, T, p) === !1 || 2 === b)) {
            return T.abort();
        }
        w = "abort";
        for (f in {
            success : 1, error : 1, complete : 1
        }) T[f](p[f]);
        if (i = D(xn, p, n, T))
        {
            T.readyState = 1, c && h.trigger("ajaxSend", [T, p]), p.async && p.timeout > 0 && (u = setTimeout(function ()
            {
                T.abort("timeout")
            },
            p.timeout));
            try {
                b = 1, i.send(v, r)
            }
            catch (C) {
                if (!(2 > b)) {
                    throw C;
                }
                r(-1, C)
            }
        }
        else {
            r(-1, "No Transport");
        }
        return T;
    },
    getJSON : function (e, t, n)
    {
        return ot.get(e, t, n, "json");
    },
    getScript : function (e, n)
    {
        return ot.get(e, t, n, "script");
    }
}), ot.each(["get", "post"], function (e, n)
{
    ot[n] = function (e, r, i, o)
    {
        return ot.isFunction(r) && (o = o || i, i = r, r = t), ot.ajax(
        {
            url : e, type : n, dataType : o, data : r, success : i
        })
    }
}), ot.ajaxSetup(
{
    accepts : 
    {
        script : "text/javascript, application/javascript, application/ecmascript, application/x-ecmascript"
    },
    contents : {
        script : /(?:java|ecma)script/
    },
    converters : {
        "text script" : function (e)
        {
            return ot.globalEval(e), e;
        }
    }
}), ot.ajaxPrefilter("script", function (e)
{
    e.cache === t && (e.cache = !1), e.crossDomain && (e.type = "GET");
}), ot.ajaxTransport("script", function (e)
{
    if (e.crossDomain)
    {
        var t, n;
        return {
            send : function (r, i)
            {
                t = ot("<script>").prop({
                    async :!0, charset : e.scriptCharset, src : e.url
                }).on("load error", n = function (e)
                {
                    t.remove(), n = null, e && i("error" === e.type ? 404 : 200, e.type);
                }), X.head.appendChild(t[0])
            },
            abort : function ()
            {
                n && n()
            }
        }
    }
});
var Tn = [], Cn = /(=)\?(?=&|$)|\?\?/;
ot.ajaxSetup(
{
    jsonp : "callback",
    jsonpCallback : function ()
    {
        var e = Tn.pop() || ot.expando + "_" + un++;
        return this [e] = !0, e;
    }
}), ot.ajaxPrefilter("json jsonp", function (n, r, i)
{
    var o, s, a, u = n.jsonp !== !1 && (Cn.test(n.url) ? "url" : "string" == typeof n.data && !(n.contentType || "").indexOf("application/x-www-form-urlencoded") && Cn.test(n.data) && "data");
    return u || "jsonp" === n.dataTypes[0] ? (o = n.jsonpCallback = ot.isFunction(n.jsonpCallback) ? n.jsonpCallback() : n.jsonpCallback, 
    u ? n[u] = n[u].replace(Cn, "$1" + o) : n.jsonp !== !1 && (n.url += (ln.test(n.url) ? "&" : "?") + n.jsonp + "=" + o), 
    n.converters["script json"] = function ()
    {
        return a || ot.error(o + " was not called"), a[0];
    },
    n.dataTypes[0] = "json", s = e[o], e[o] = function ()
    {
        a = arguments;
    },
    i.always(function ()
    {
        e[o] = s, n[o] && (n.jsonpCallback = r.jsonpCallback, Tn.push(o)), a && ot.isFunction(s) && s(a[0]), 
        a = s = t;
    }), "script") : void 0
}), ot.ajaxSettings.xhr = function ()
{
    try {
        return new XMLHttpRequest
    }
    catch (e) {}
};
var kn = ot.ajaxSettings.xhr(), Nn = {
    0 : 200, 1223 : 204
},
jn = 0, En = {};
e.ActiveXObject && ot(e).on("unload", function ()
{
    for (var e in En) {
        En[e]();
    }
    En = t;
}), ot.support.cors = !!kn && "withCredentials"in kn, ot.support.ajax = kn = !!kn, ot.ajaxTransport(function (e)
{
    var n;
    return ot.support.cors || kn && !e.crossDomain ? 
        {
            send : function (r, i)
            {
                var o, s, a = e.xhr();
                if (a.open(e.type, e.url, e.async, e.username, e.password), e.xhrFields) {
                    for (o in e.xhrFields) {
                        a[o] = e.xhrFields[o];
                    }
                }
                e.mimeType && a.overrideMimeType && a.overrideMimeType(e.mimeType), e.crossDomain || r["X-Requested-With"] || (r["X-Requested-With"] = "XMLHttpRequest");
                for (o in r) {
                    a.setRequestHeader(o, r[o]);
                }
                n = function (e)
                {
                    return function ()
                    {
                        n && (delete En[s], n = a.onload = a.onerror = null, "abort" === e ? a.abort() : "error" === e ? i(a.status || 404, 
                        a.statusText) : i(Nn[a.status] || a.status, a.statusText, "string" == typeof a.responseText ? {
                            text : a.responseText
                        }
                         : t, a.getAllResponseHeaders()))
                    }
                },
                a.onload = n(), a.onerror = n("error"), n = En[s = jn++] = n("abort"), a.send(e.hasContent && e.data || null);
            },
            abort : function ()
            {
                n && n()
            }
        }
     : void 0
});
var Sn, Dn, An = /^(?:toggle|show|hide)$/, qn = new RegExp("^(?:([+-])=|)(" + st + ")([a-z%]*)$", 
"i"), Ln = /queueHooks$/, Hn = [R], On = 
{
    "*" : [function (e, t)
    {
        var n = this.createTween(e, t), r = n.cur(), i = qn.exec(t), o = i && i[3] || (ot.cssNumber[e] ? "" : "px"), 
        s = (ot.cssNumber[e] || "px" !== o &&+ r) && qn.exec(ot.css(n.elem, e)), a = 1, u = 20;
        if (s && s[3] !== o)
        {
            o = o || s[3], i = i || [], s =+ r || 1;
            do a = a || ".5", s /= a, ot.style(n.elem, e, s + o);
            while (a !== (a = n.cur() / r) && 1 !== a &&--u) {;
            }
        }
        return i && (s = n.start =+ s ||+ r || 0, n.unit = o, n.end = i[1] ? s + (i[1] + 1) * i[2] :+ i[2]), 
        n
    }];
};
ot.Animation = ot.extend(F, 
{
    tweener : function (e, t)
    {
        ot.isFunction(e) ? (t = e, e = ["*"]) : e = e.split(" ");
        for (var n, r = 0, i = e.length; i > r; r++) {
            n = e[r], On[n] = On[n] || [], On[n].unshift(t);
        }
    },
    prefilter : function (e, t)
    {
        t ? Hn.unshift(e) : Hn.push(e)
    }
}), ot.Tween = M, M.prototype = 
{
    constructor : M,
    init : function (e, t, n, r, i, o)
    {
        this.elem = e, this.prop = n, this.easing = i || "swing", this.options = t, this.start = this.now = this.cur(), 
        this.end = r, this.unit = o || (ot.cssNumber[n] ? "" : "px");
    },
    cur : function ()
    {
        var e = M.propHooks[this.prop];
        return e && e.get ? e.get(this) : M.propHooks._default.get(this);
    },
    run : function (e)
    {
        var t, n = M.propHooks[this.prop];
        return this.pos = t = this.options.duration ? ot.easing[this.easing](e, this.options.duration * e, 
        0, 1, this.options.duration) : e, this.now = (this.end - this.start) * t + this.start, this.options.step && this.options.step.call(this.elem, 
        this.now, this), n && n.set ? n.set(this) : M.propHooks._default.set(this), this;
    }
},
M.prototype.init.prototype = M.prototype, M.propHooks = 
{
    _default : 
    {
        get : function (e)
        {
            var t;
            return null == e.elem[e.prop] || e.elem.style && null != e.elem.style[e.prop] ? (t = ot.css(e.elem, 
            e.prop, ""), t && "auto" !== t ? t : 0) : e.elem[e.prop];
        },
        set : function (e)
        {
            ot.fx.step[e.prop] ? ot.fx.step[e.prop](e) : e.elem.style && (null != e.elem.style[ot.cssProps[e.prop]] || ot.cssHooks[e.prop]) ? ot.style(e.elem, 
            e.prop, e.now + e.unit) : e.elem[e.prop] = e.now;
        }
    }
},
M.propHooks.scrollTop = M.propHooks.scrollLeft = {
    set : function (e)
    {
        e.elem.nodeType && e.elem.parentNode && (e.elem[e.prop] = e.now);
    }
},
ot.each(["toggle", "show", "hide"], function (e, t)
{
    var n = ot.fn[t];
    ot.fn[t] = function (e, r, i)
    {
        return null == e || "boolean" == typeof e ? n.apply(this, arguments) : this.animate(W(t, !0), 
        e, r, i)
    }
}), ot.fn.extend(
{
    fadeTo : function (e, t, n, r)
    {
        return this.filter(x).css("opacity", 0).show().end().animate(
        {
            opacity : t
        }, e, n, r)
    },
    animate : function (e, t, n, r)
    {
        var i = ot.isEmptyObject(e), o = ot.speed(t, n, r), s = function ()
        {
            var t = F(this, ot.extend({}, e), o);
            (i || mt.get(this, "finish")) && t.stop(!0)
        };
        return s.finish = s, i || o.queue === !1 ? this.each(s) : this.queue(o.queue, s);
    },
    stop : function (e, n, r)
    {
        var i = function (e)
        {
            var t = e.stop;
            delete e.stop, t(r)
        };
        return "string" != typeof e && (r = n, n = e, e = t), n && e !== !1 && this.queue(e || "fx", 
        []), this.each(function ()
        {
            var t = !0, n = null != e && e + "queueHooks", o = ot.timers, s = mt.get(this);
            if (n) {
                s[n] && s[n].stop && i(s[n]);
            }
            else {
                for (n in s) {
                    s[n] && s[n].stop && Ln.test(n) && i(s[n]);
                }
            }
            for (n = o.length; n--; )
            {
                o[n].elem !== this || null != e && o[n].queue !== e || (o[n].anim.stop(r), t = !1, 
                o.splice(n, 1));
            }
            (t || !r) && ot.dequeue(this, e)
        })
    },
    finish : function (e)
    {
        return e !== !1 && (e = e || "fx"), this.each(function ()
        {
            var t, n = mt.get(this), r = n[e + "queue"], i = n[e + "queueHooks"], o = ot.timers, s = r ? r.length : 0;
            for (n.finish = !0, ot.queue(this, e, []), i && i.stop && i.stop.call(this, !0), t = o.length; t--; ) {
                o[t].elem === this && o[t].queue === e && (o[t].anim.stop(!0), o.splice(t, 1));
            }
            for (t = 0; s > t; t++) {
                r[t] && r[t].finish && r[t].finish.call(this);
            }
            delete n.finish
        })
    }
}), ot.each(
{
    slideDown : W("show"), slideUp : W("hide"), slideToggle : W("toggle"), fadeIn : {
        opacity : "show"
    },
    fadeOut : {
        opacity : "hide"
    },
    fadeToggle : {
        opacity : "toggle"
    }
},
function (e, t)
{
    ot.fn[e] = function (e, n, r)
    {
        return this.animate(t, e, n, r);
    }
}), ot.speed = function (e, t, n)
{
    var r = e && "object" == typeof e ? ot.extend({}, e) : 
        {
            complete : n || !n && t || ot.isFunction(e) && e, duration : e, easing : n && t || t && !ot.isFunction(t) && t
        };
    return r.duration = ot.fx.off ? 0 : "number" == typeof r.duration ? r.duration : r.duration in ot.fx.speeds ? ot.fx.speeds[r.duration] : ot.fx.speeds._default, 
    (null == r.queue || r.queue === !0) && (r.queue = "fx"), r.old = r.complete, r.complete = function ()
    {
        ot.isFunction(r.old) && r.old.call(this), r.queue && ot.dequeue(this, r.queue)
    },
    r
},
ot.easing = {
    linear : function (e)
    {
        return e;
    },
    swing : function (e)
    {
        return.5 - Math.cos(e * Math.PI) / 2
    }
},
ot.timers = [], ot.fx = M.prototype.init, ot.fx.tick = function ()
{
    var e, n = ot.timers, r = 0;
    for (Sn = ot.now(); r < n.length; r++) {
        e = n[r], e() || n[r] !== e || n.splice(r--, 1);
    }
    n.length || ot.fx.stop(), Sn = t;
},
ot.fx.timer = function (e)
{
    e() && ot.timers.push(e) && ot.fx.start()
},
ot.fx.interval = 13, ot.fx.start = function ()
{
    Dn || (Dn = setInterval(ot.fx.tick, ot.fx.interval));
},
ot.fx.stop = function ()
{
    clearInterval(Dn), Dn = null;
},
ot.fx.speeds = {
    slow : 600, fast : 200, _default : 400
},
ot.fx.step = {}, ot.expr && ot.expr.filters && (ot.expr.filters.animated = function (e)
{
    return ot.grep(ot.timers, function (t)
    {
        return e === t.elem;
    }).length
}), ot.fn.offset = function (e)
{
    if (arguments.length) {
        return e === t ? this : this.each(function (t) 
        {
            ot.offset.setOffset(this, e, t) 
        });
    }
    var n, r, i = this [0], o = {
        top : 0, left : 0
    },
    s = i && i.ownerDocument;
    if (s)
    {
        return n = s.documentElement, ot.contains(n, i) ? (typeof i.getBoundingClientRect !== z && (o = i.getBoundingClientRect()), 
        r = $(s), {
            top : o.top + r.pageYOffset - n.clientTop, left : o.left + r.pageXOffset - n.clientLeft 
        }) : o;
    }
},
ot.offset = 
{
    setOffset : function (e, t, n)
    {
        var r, i, o, s, a, u, l, c = ot.css(e, "position"), f = ot(e), p = {};
        "static" === c && (e.style.position = "relative"), a = f.offset(), o = ot.css(e, "top"), u = ot.css(e, 
        "left"), l = ("absolute" === c || "fixed" === c) && (o + u).indexOf("auto") > -1, l ? (r = f.position(), 
        s = r.top, i = r.left) : (s = parseFloat(o) || 0, i = parseFloat(u) || 0), ot.isFunction(t) && (t = t.call(e, 
        n, a)), null != t.top && (p.top = t.top - a.top + s), null != t.left && (p.left = t.left - a.left + i), 
        "using"in t ? t.using.call(e, p) : f.css(p);
    }
},
ot.fn.extend(
{
    position : function ()
    {
        if (this [0])
        {
            var e, t, n = this [0], r = {
                top : 0, left : 0
            };
            return "fixed" === ot.css(n, "position") ? t = n.getBoundingClientRect() : (e = this.offsetParent(), 
            t = this.offset(), ot.nodeName(e[0], "html") || (r = e.offset()), r.top += ot.css(e[0], 
            "borderTopWidth", !0), r.left += ot.css(e[0], "borderLeftWidth", !0)), 
            {
                top : t.top - r.top - ot.css(n, "marginTop", !0), left : t.left - r.left - ot.css(n, 
                "marginLeft", !0)
            }
        }
    },
    offsetParent : function ()
    {
        return this.map(function ()
        {
            for (var e = this.offsetParent || U; e && !ot.nodeName(e, "html") && "static" === ot.css(e, 
            "position");
            ) e = e.offsetParent;
            return e || U;
        })
    }
}), ot.each({
    scrollLeft : "pageXOffset", scrollTop : "pageYOffset"
},
function (n, r)
{
    var i = "pageYOffset" === r;
    ot.fn[n] = function (o)
    {
        return ot.access(this, function (n, o, s)
        {
            var a = $(n);
            return s === t ? a ? a[r] : n[o] : void (a ? a.scrollTo(i ? e.pageXOffset : s, i ? s : e.pageYOffset) : n[o] = s);
        },
        n, o, arguments.length, null)
    }
}), ot.each({
    Height : "height", Width : "width"
},
function (e, n)
{
    ot.each({
        padding : "inner" + e, content : n, "" : "outer" + e
    },
    function (r, i)
    {
        ot.fn[i] = function (i, o)
        {
            var s = arguments.length && (r || "boolean" != typeof i), a = r || (i === !0 || o === !0 ? "margin" : "border");
            return ot.access(this, function (n, r, i)
            {
                var o;
                return ot.isWindow(n) ? n.document.documentElement["client" + e] : 9 === n.nodeType ? (o = n.documentElement, 
                Math.max(n.body["scroll" + e], o["scroll" + e], n.body["offset" + e], o["offset" + e], 
                o["client" + e])) : i === t ? ot.css(n, r, a) : ot.style(n, r, i, a);
            },
            n, s ? i : t, s, null)
        }
    })
}), ot.fn.size = function ()
{
    return this.length;
},
ot.fn.andSelf = ot.fn.addBack, "object" == typeof module && module && "object" == typeof module.exports ? module.exports = ot : "function" == typeof define && define.amd && define("jquery", 
[], function ()
{
    return ot;
}), "object" == typeof e && "object" == typeof e.document && (e.jQuery = e.$ = ot)
}
(window);
var requirejs, require, define;
!function (e)
{
    function t(e, t)
    {
        return y.call(e, t)
    }
    function n(e, t)
    {
        var n, r, i, o, s, a, u, l, c, f, p, d = t && t.split("/"), h = g.map, m = h && h["*"] || {};
        if (e && "." === e.charAt(0))
        {
            if (t) 
            {
                for (d = d.slice(0, d.length - 1), e = e.split("/"), s = e.length - 1, g.nodeIdCompat && x.test(e[s]) && (e[s] = e[s].replace(x, 
                "")), e = d.concat(e), c = 0;
                c < e.length;
                c += 1) if (p = e[c], "." === p) e.splice(c, 1), c -= 1;
                else if (".." === p) {
                    if (1 === c && (".." === e[2] || ".." === e[0])) {
                        break;
                    }
                    c > 0 && (e.splice(c - 1, 2), c -= 2) 
                }
                e = e.join("/") 
            }
            else {
                0 === e.indexOf("./") && (e = e.substring(2));
            }
            if ((d || m) && h) 
            {
                for (n = e.split("/"), c = n.length; c > 0; c -= 1) 
                {
                    if (r = n.slice(0, c).join("/"), d)
                    {
                        for (f = d.length; f > 0; f -= 1) {
                            if (i = h[d.slice(0, f).join("/")], i && (i = i[r])) {
                                o = i, a = c;
                                break 
                            }
                            if (o) {
                                break;
                            };
                        }
                    }
                    !u && m && m[r] && (u = m[r], l = c) 
                }
                !o && u && (o = u, a = l), o && (n.splice(0, a, o), e = n.join("/")) 
            }
            return e;
        }
    }
    function r(t, n)
    {
        return function ()
        {
            var r = v.call(arguments, 0);
            return "string" != typeof r[0] && 1 === r.length && r.push(null), c.apply(e, r.concat([t, 
            n]))
        }
    }
    function i(e)
    {
        return function (t)
        {
            return n(t, e);
        }
    }
    function o(e)
    {
        return function (t)
        {
            d[e] = t;
        }
    }
    function s(n)
    {
        if (t(h, n)) {
            var r = h[n];
            delete h[n], m[n] = !0, l.apply(e, r)
        }
        if (!t(d, n) && !t(m, n)) {
            throw new Error("No " + n);
        }
        return d[n]
    }
    function a(e)
    {
        var t, n = e ? e.indexOf("!") :- 1;
        return n > -1 && (t = e.substring(0, n), e = e.substring(n + 1, e.length)), [t, e]
    }
    function u(e)
    {
        return function ()
        {
            return g && g.config && g.config[e] || {}
        }
    }
    var l, c, f, p, d = {}, h = {}, g = {}, m = {}, y = Object.prototype.hasOwnProperty, v = [].slice, 
    x = /\.js$/;
    f = function (e, t)
    {
        var r, o = a(e), u = o[0];
        return e = o[1], u && (u = n(u, t), r = s(u)), u ? e = r && r.normalize ? r.normalize(e, i(t)) : n(e, 
        t) : (e = n(e, t), o = a(e), u = o[0], e = o[1], u && (r = s(u))), {
            f : u ? u + "!" + e : e, n : e, pr : u, p : r
        }
    },
    p = 
    {
        require : function (e)
        {
            return r(e);
        },
        exports : function (e)
        {
            var t = d[e];
            return "undefined" != typeof t ? t : d[e] = {}
        },
        module : function (e)
        {
            return {
                id : e, uri : "", exports : d[e], config : u(e)
            }
        }
    },
    l = function (n, i, a, u)
    {
        var l, c, g, y, v, x, b = [], w = typeof a;
        if (u = u || n, "undefined" === w || "function" === w)
        {
            for (i = !i.length && a.length ? ["require", "exports", "module"] : i, v = 0; v < i.length; v += 1)
            {
                if (y = f(i[v], u), c = y.f, "require" === c) {
                    b[v] = p.require(n);
                }
                else if ("exports" === c) {
                    b[v] = p.exports(n), x = !0;
                }
                else if ("module" === c) {
                    l = b[v] = p.module(n);
                }
                else if (t(d, c) || t(h, c) || t(m, c)) {
                    b[v] = s(c);
                }
                else {
                    if (!y.p) {
                        throw new Error(n + " missing " + c);
                    }
                    y.p.load(y.n, r(u, !0), o(c), {}), b[v] = d[c] 
                }
                g = a ? a.apply(d[n], b) : void 0, n && (l && l.exports !== e && l.exports !== d[n] ? d[n] = l.exports : g === e && x || (d[n] = g));
            }
        }
        else {
            n && (d[n] = a);
        }
    },
    requirejs = require = c = function (t, n, r, i, o)
    {
        if ("string" == typeof t) {
            return p[t] ? p[t](n) : s(f(t, n).f);
        }
        if (!t.splice) {
            if (g = t, g.deps && c(g.deps, g.callback), !n) {
                return;
            }
            n.splice ? (t = n, n = r, r = null) : t = e
        }
        return n = n || function () {}, "function" == typeof r && (r = i, i = o), i ? l(e, t, n, r) : setTimeout(function ()
        {
            l(e, t, n, r)
        }, 4), c
    },
    c.config = function (e)
    {
        return c(e);
    },
    requirejs._defined = d, define = function (e, n, r)
    {
        n.splice || (r = n, n = []), t(d, e) || t(h, e) || (h[e] = [e, n, r]);
    },
    define.amd = {
        jQuery :!0
    }
}(),
define("text", [], function () {}), define("backbone", [], function ()
{
    return Backbone;
}), define("underscore", [], function ()
{
    return _;
}), define("zepto", [], function ()
{
    return Zepto;
}), define("jQuery", [], function ()
{
    return Zepto;
}), define("jquery", [], function ()
{
    return Zepto;
});




