﻿window.FontAwesomeKitConfig = { asyncLoading: { enabled: !0 }, autoA11y: { enabled: !0 }, baseUrl: "https://kit-pro.fontawesome.com", license: "pro", method: "css", minify: { enabled: !0 }, v4shim: { enabled: !0 }, version: "latest" }, function () { function c(e) { var t, n, i, o; prefixesArray = e || ["fa"], prefixesSelectorString = "." + Array.prototype.join.call(e, ",."), t = document.querySelectorAll(prefixesSelectorString), Array.prototype.forEach.call(t, function (e) { n = e.getAttribute("title"), e.setAttribute("aria-hidden", "true"), i = !e.nextElementSibling || !e.nextElementSibling.classList.contains("sr-only"), n && i && ((o = document.createElement("span")).innerHTML = n, o.classList.add("sr-only"), e.parentNode.insertBefore(o, e.nextSibling)) }) } !function () { if (!(void 0 === window.Element || "classList" in document.documentElement)) { var e, t, n, i = Array.prototype, o = i.push, a = i.splice, s = i.join; r.prototype = { add: function (e) { this.contains(e) || (o.call(this, e), this.el.className = this.toString()) }, contains: function (e) { return -1 != this.el.className.indexOf(e) }, item: function (e) { return this[e] || null }, remove: function (e) { if (this.contains(e)) { for (var t = 0; t < this.length && this[t] != e; t++); a.call(this, t, 1), this.el.className = this.toString() } }, toString: function () { return s.call(this, " ") }, toggle: function (e) { return this.contains(e) ? this.remove(e) : this.add(e), this.contains(e) } }, window.DOMTokenList = r, e = Element.prototype, t = "classList", n = function () { return new r(this) }, Object.defineProperty ? Object.defineProperty(e, t, { get: n }) : e.__defineGetter__(t, n) } function r(e) { for (var t = (this.el = e).className.replace(/^\s+|\s+$/g, "").split(/\s+/), n = 0; n < t.length; n++)o.call(this, t[n]) } }(); function f(e) { var t = document.createElement("link"); t.href = e, t.media = "all", t.rel = "stylesheet", document.getElementsByTagName("head")[0].appendChild(t) } function u(e) { !function (e, t, n) { var i, o = window.document, a = o.createElement("link"), s = (o.body || o.getElementsByTagName("head")[0]).childNodes; i = s[s.length - 1]; var r = o.styleSheets; a.rel = "stylesheet", a.href = e, a.media = "only x", function e(t) { if (o.body) return t(); setTimeout(function () { e(t) }) }(function () { i.parentNode.insertBefore(a, i.nextSibling) }); var l = function (e) { for (var t = a.href, n = r.length; n--;)if (r[n].href === t) return e(); setTimeout(function () { l(e) }) }; function d() { a.addEventListener && a.removeEventListener("load", d), a.media = n || "all" } a.addEventListener && a.addEventListener("load", d), (a.onloadcssdefined = l)(d) }(e) } function e(e, t) { var n = t && void 0 !== t.autoFetchSvg ? t.autoFetchSvg : void 0, i = t && void 0 !== t.async ? t.async : void 0, o = t && void 0 !== t.autoA11y ? t.autoA11y : void 0, a = document.createElement("script"), s = document.scripts[0]; a.src = e, void 0 !== o && a.setAttribute("data-auto-a11y", o ? "true" : "false"), n && (a.setAttributeNode(document.createAttribute("data-auto-fetch-svg")), a.setAttribute("data-fetch-svg-from", t.fetchSvgFrom)), i && a.setAttributeNode(document.createAttribute("defer")), s.parentNode.appendChild(a) } var t, n; function m(e, t) { var n = t && t.shim ? e.license + "-v4-shims" : e.license, i = t && t.minify ? ".min" : ""; return e.baseUrl + "/releases/" + ("latest" === e.version ? "latest" : "v".concat(e.version)) + "/" + e.method + "/" + n + i + "." + e.method } try { if (window.FontAwesomeKitConfig) { var i = window.FontAwesomeKitConfig; "js" === i.method && (n = { async: (t = i).asyncLoading.enabled, autoA11y: t.autoA11y.enabled }, "pro" === t.license && (n.autoFetchSvg = !0, n.fetchSvgFrom = t.baseUrl + "/releases/" + ("latest" === t.version ? "latest" : "v".concat(t.version)) + "/svgs"), t.v4shim.enabled && e(m(t, { shim: !0, minify: t.minify.enabled })), e(m(t, { minify: t.minify.enabled }), n)), "css" === i.method && function (e) { var t, n, i, o, a, s, r, l = c.bind(c, ["fa", "fab", "fas", "far", "fal"]); e.autoA11y.enabled && (t = l, i = [], a = (o = document).documentElement.doScroll, s = "DOMContentLoaded", (r = (a ? /^loaded|^c/ : /^loaded|^i|^c/).test(o.readyState)) || o.addEventListener(s, n = function () { for (o.removeEventListener(s, n), r = 1; n = i.shift();)n() }), r ? setTimeout(t, 0) : i.push(t), "undefined" != typeof MutationObserver && new MutationObserver(l).observe(document, { childList: !0, subtree: !0 })), e.v4shim.enabled && (e.asyncLoading.enabled ? u(m(e, { shim: !0, minify: e.minify.enabled })) : f(m(e, { shim: !0, minify: e.minify.enabled }))); var d = m(e, { minify: e.minify.enabled }); e.asyncLoading.enabled ? u(d) : f(d) }(i) } } catch (t) { } }();