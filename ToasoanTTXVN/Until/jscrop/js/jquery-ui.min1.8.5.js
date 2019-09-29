﻿/*!
* jQuery UI 1.8.5
*
* Copyright 2010, AUTHORS.txt (http://jqueryui.com/about)
* Dual licensed under the MIT or GPL Version 2 licenses.
* http://jquery.org/license
*
* http://docs.jquery.com/UI
*/
(function(c, j) {
    function k(a) { return !c(a).parents().andSelf().filter(function() { return c.curCSS(this, "visibility") === "hidden" || c.expr.filters.hidden(this) }).length } c.ui = c.ui || {}; if (!c.ui.version) {
        c.extend(c.ui, { version: "1.8.5", keyCode: { ALT: 18, BACKSPACE: 8, CAPS_LOCK: 20, COMMA: 188, COMMAND: 91, COMMAND_LEFT: 91, COMMAND_RIGHT: 93, CONTROL: 17, DELETE: 46, DOWN: 40, END: 35, ENTER: 13, ESCAPE: 27, HOME: 36, INSERT: 45, LEFT: 37, MENU: 93, NUMPAD_ADD: 107, NUMPAD_DECIMAL: 110, NUMPAD_DIVIDE: 111, NUMPAD_ENTER: 108, NUMPAD_MULTIPLY: 106,
            NUMPAD_SUBTRACT: 109, PAGE_DOWN: 34, PAGE_UP: 33, PERIOD: 190, RIGHT: 39, SHIFT: 16, SPACE: 32, TAB: 9, UP: 38, WINDOWS: 91}
        }); c.fn.extend({ _focus: c.fn.focus, focus: function(a, b) { return typeof a === "number" ? this.each(function() { var d = this; setTimeout(function() { c(d).focus(); b && b.call(d) }, a) }) : this._focus.apply(this, arguments) }, scrollParent: function() {
            var a; a = c.browser.msie && /(static|relative)/.test(this.css("position")) || /absolute/.test(this.css("position")) ? this.parents().filter(function() {
                return /(relative|absolute|fixed)/.test(c.curCSS(this,
"position", 1)) && /(auto|scroll)/.test(c.curCSS(this, "overflow", 1) + c.curCSS(this, "overflow-y", 1) + c.curCSS(this, "overflow-x", 1))
            }).eq(0) : this.parents().filter(function() { return /(auto|scroll)/.test(c.curCSS(this, "overflow", 1) + c.curCSS(this, "overflow-y", 1) + c.curCSS(this, "overflow-x", 1)) }).eq(0); return /fixed/.test(this.css("position")) || !a.length ? c(document) : a
        }, zIndex: function(a) {
            if (a !== j) return this.css("zIndex", a); if (this.length) {
                a = c(this[0]); for (var b; a.length && a[0] !== document; ) {
                    b = a.css("position");
                    if (b === "absolute" || b === "relative" || b === "fixed") { b = parseInt(a.css("zIndex")); if (!isNaN(b) && b != 0) return b } a = a.parent()
                } 
            } return 0
        }, disableSelection: function() { return this.bind("mousedown.ui-disableSelection selectstart.ui-disableSelection", function(a) { a.preventDefault() }) }, enableSelection: function() { return this.unbind(".ui-disableSelection") } 
        }); c.each(["Width", "Height"], function(a, b) {
            function d(f, g, l, m) {
                c.each(e, function() {
                    g -= parseFloat(c.curCSS(f, "padding" + this, true)) || 0; if (l) g -= parseFloat(c.curCSS(f,
"border" + this + "Width", true)) || 0; if (m) g -= parseFloat(c.curCSS(f, "margin" + this, true)) || 0
                }); return g
            } var e = b === "Width" ? ["Left", "Right"] : ["Top", "Bottom"], h = b.toLowerCase(), i = { innerWidth: c.fn.innerWidth, innerHeight: c.fn.innerHeight, outerWidth: c.fn.outerWidth, outerHeight: c.fn.outerHeight }; c.fn["inner" + b] = function(f) { if (f === j) return i["inner" + b].call(this); return this.each(function() { c.style(this, h, d(this, f) + "px") }) }; c.fn["outer" + b] = function(f, g) {
                if (typeof f !== "number") return i["outer" + b].call(this, f); return this.each(function() {
                    c.style(this,
h, d(this, f, true, g) + "px")
                })
            } 
        }); c.extend(c.expr[":"], { data: function(a, b, d) { return !!c.data(a, d[3]) }, focusable: function(a) { var b = a.nodeName.toLowerCase(), d = c.attr(a, "tabindex"); if ("area" === b) { b = a.parentNode; d = b.name; if (!a.href || !d || b.nodeName.toLowerCase() !== "map") return false; a = c("img[usemap=#" + d + "]")[0]; return !!a && k(a) } return (/input|select|textarea|button|object/.test(b) ? !a.disabled : "a" == b ? a.href || !isNaN(d) : !isNaN(d)) && k(a) }, tabbable: function(a) { var b = c.attr(a, "tabindex"); return (isNaN(b) || b >= 0) && c(a).is(":focusable") } });
        c(function() { var a = document.createElement("div"), b = document.body; c.extend(a.style, { minHeight: "100px", height: "auto", padding: 0, borderWidth: 0 }); c.support.minHeight = b.appendChild(a).offsetHeight === 100; b.removeChild(a).style.display = "none" }); c.extend(c.ui, { plugin: { add: function(a, b, d) { a = c.ui[a].prototype; for (var e in d) { a.plugins[e] = a.plugins[e] || []; a.plugins[e].push([b, d[e]]) } }, call: function(a, b, d) {
            if ((b = a.plugins[b]) && a.element[0].parentNode) for (var e = 0; e < b.length; e++) a.options[b[e][0]] && b[e][1].apply(a.element,
d)
        } 
        }, contains: function(a, b) { return document.compareDocumentPosition ? a.compareDocumentPosition(b) & 16 : a !== b && a.contains(b) }, hasScroll: function(a, b) { if (c(a).css("overflow") === "hidden") return false; b = b && b === "left" ? "scrollLeft" : "scrollTop"; var d = false; if (a[b] > 0) return true; a[b] = 1; d = a[b] > 0; a[b] = 0; return d }, isOverAxis: function(a, b, d) { return a > b && a < b + d }, isOver: function(a, b, d, e, h, i) { return c.ui.isOverAxis(a, d, h) && c.ui.isOverAxis(b, e, i) } 
        })
    } 
})(jQuery);
(function(b, j) {
    if (b.cleanData) { var k = b.cleanData; b.cleanData = function(a) { for (var c = 0, d; (d = a[c]) != null; c++) b(d).triggerHandler("remove"); k(a) } } else { var l = b.fn.remove; b.fn.remove = function(a, c) { return this.each(function() { if (!c) if (!a || b.filter(a, [this]).length) b("*", this).add([this]).each(function() { b(this).triggerHandler("remove") }); return l.call(b(this), a, c) }) } } b.widget = function(a, c, d) {
        var e = a.split(".")[0], f; a = a.split(".")[1]; f = e + "-" + a; if (!d) { d = c; c = b.Widget } b.expr[":"][f] = function(h) {
            return !!b.data(h,
a)
        }; b[e] = b[e] || {}; b[e][a] = function(h, g) { arguments.length && this._createWidget(h, g) }; c = new c; c.options = b.extend(true, {}, c.options); b[e][a].prototype = b.extend(true, c, { namespace: e, widgetName: a, widgetEventPrefix: b[e][a].prototype.widgetEventPrefix || a, widgetBaseClass: f }, d); b.widget.bridge(a, b[e][a])
    }; b.widget.bridge = function(a, c) {
        b.fn[a] = function(d) {
            var e = typeof d === "string", f = Array.prototype.slice.call(arguments, 1), h = this; d = !e && f.length ? b.extend.apply(null, [true, d].concat(f)) : d; if (e && d.substring(0, 1) ===
"_") return h; e ? this.each(function() { var g = b.data(this, a); if (!g) throw "cannot call methods on " + a + " prior to initialization; attempted to call method '" + d + "'"; if (!b.isFunction(g[d])) throw "no such method '" + d + "' for " + a + " widget instance"; var i = g[d].apply(g, f); if (i !== g && i !== j) { h = i; return false } }) : this.each(function() { var g = b.data(this, a); g ? g.option(d || {})._init() : b.data(this, a, new c(d, this)) }); return h
        } 
    }; b.Widget = function(a, c) { arguments.length && this._createWidget(a, c) }; b.Widget.prototype = { widgetName: "widget",
        widgetEventPrefix: "", options: { disabled: false }, _createWidget: function(a, c) { b.data(c, this.widgetName, this); this.element = b(c); this.options = b.extend(true, {}, this.options, b.metadata && b.metadata.get(c)[this.widgetName], a); var d = this; this.element.bind("remove." + this.widgetName, function() { d.destroy() }); this._create(); this._init() }, _create: function() { }, _init: function() { }, destroy: function() {
            this.element.unbind("." + this.widgetName).removeData(this.widgetName); this.widget().unbind("." + this.widgetName).removeAttr("aria-disabled").removeClass(this.widgetBaseClass +
"-disabled ui-state-disabled")
        }, widget: function() { return this.element }, option: function(a, c) { var d = a, e = this; if (arguments.length === 0) return b.extend({}, e.options); if (typeof a === "string") { if (c === j) return this.options[a]; d = {}; d[a] = c } b.each(d, function(f, h) { e._setOption(f, h) }); return e }, _setOption: function(a, c) { this.options[a] = c; if (a === "disabled") this.widget()[c ? "addClass" : "removeClass"](this.widgetBaseClass + "-disabled ui-state-disabled").attr("aria-disabled", c); return this }, enable: function() {
            return this._setOption("disabled",
false)
        }, disable: function() { return this._setOption("disabled", true) }, _trigger: function(a, c, d) { var e = this.options[a]; c = b.Event(c); c.type = (a === this.widgetEventPrefix ? a : this.widgetEventPrefix + a).toLowerCase(); d = d || {}; if (c.originalEvent) { a = b.event.props.length; for (var f; a; ) { f = b.event.props[--a]; c[f] = c.originalEvent[f] } } this.element.trigger(c, d); return !(b.isFunction(e) && e.call(this.element[0], c, d) === false || c.isDefaultPrevented()) } }
    })(jQuery);
    (function(c) {
        c.widget("ui.mouse", { options: { cancel: ":input,option", distance: 1, delay: 0 }, _mouseInit: function() { var a = this; this.element.bind("mousedown." + this.widgetName, function(b) { return a._mouseDown(b) }).bind("click." + this.widgetName, function(b) { if (a._preventClickEvent) { a._preventClickEvent = false; b.stopImmediatePropagation(); return false } }); this.started = false }, _mouseDestroy: function() { this.element.unbind("." + this.widgetName) }, _mouseDown: function(a) {
            a.originalEvent = a.originalEvent || {}; if (!a.originalEvent.mouseHandled) {
                this._mouseStarted &&
this._mouseUp(a); this._mouseDownEvent = a; var b = this, e = a.which == 1, f = typeof this.options.cancel == "string" ? c(a.target).parents().add(a.target).filter(this.options.cancel).length : false; if (!e || f || !this._mouseCapture(a)) return true; this.mouseDelayMet = !this.options.delay; if (!this.mouseDelayMet) this._mouseDelayTimer = setTimeout(function() { b.mouseDelayMet = true }, this.options.delay); if (this._mouseDistanceMet(a) && this._mouseDelayMet(a)) {
                    this._mouseStarted = this._mouseStart(a) !== false; if (!this._mouseStarted) {
                        a.preventDefault();
                        return true
                    } 
                } this._mouseMoveDelegate = function(d) { return b._mouseMove(d) }; this._mouseUpDelegate = function(d) { return b._mouseUp(d) }; c(document).bind("mousemove." + this.widgetName, this._mouseMoveDelegate).bind("mouseup." + this.widgetName, this._mouseUpDelegate); c.browser.safari || a.preventDefault(); return a.originalEvent.mouseHandled = true
            } 
        }, _mouseMove: function(a) {
            if (c.browser.msie && !a.button) return this._mouseUp(a); if (this._mouseStarted) { this._mouseDrag(a); return a.preventDefault() } if (this._mouseDistanceMet(a) &&
this._mouseDelayMet(a)) (this._mouseStarted = this._mouseStart(this._mouseDownEvent, a) !== false) ? this._mouseDrag(a) : this._mouseUp(a); return !this._mouseStarted
        }, _mouseUp: function(a) { c(document).unbind("mousemove." + this.widgetName, this._mouseMoveDelegate).unbind("mouseup." + this.widgetName, this._mouseUpDelegate); if (this._mouseStarted) { this._mouseStarted = false; this._preventClickEvent = a.target == this._mouseDownEvent.target; this._mouseStop(a) } return false }, _mouseDistanceMet: function(a) {
            return Math.max(Math.abs(this._mouseDownEvent.pageX -
a.pageX), Math.abs(this._mouseDownEvent.pageY - a.pageY)) >= this.options.distance
        }, _mouseDelayMet: function() { return this.mouseDelayMet }, _mouseStart: function() { }, _mouseDrag: function() { }, _mouseStop: function() { }, _mouseCapture: function() { return true } 
        })
    })(jQuery);
    (function(d) {
        d.widget("ui.draggable", d.ui.mouse, { widgetEventPrefix: "drag", options: { addClasses: true, appendTo: "parent", axis: false, connectToSortable: false, containment: false, cursor: "auto", cursorAt: false, grid: false, handle: false, helper: "original", iframeFix: false, opacity: false, refreshPositions: false, revert: false, revertDuration: 500, scope: "default", scroll: true, scrollSensitivity: 20, scrollSpeed: 20, snap: false, snapMode: "both", snapTolerance: 20, stack: false, zIndex: false }, _create: function() {
            if (this.options.helper ==
"original" && !/^(?:r|a|f)/.test(this.element.css("position"))) this.element[0].style.position = "relative"; this.options.addClasses && this.element.addClass("ui-draggable"); this.options.disabled && this.element.addClass("ui-draggable-disabled"); this._mouseInit()
        }, destroy: function() { if (this.element.data("draggable")) { this.element.removeData("draggable").unbind(".draggable").removeClass("ui-draggable ui-draggable-dragging ui-draggable-disabled"); this._mouseDestroy(); return this } }, _mouseCapture: function(a) {
            var b =
this.options; if (this.helper || b.disabled || d(a.target).is(".ui-resizable-handle")) return false; this.handle = this._getHandle(a); if (!this.handle) return false; return true
        }, _mouseStart: function(a) {
            var b = this.options; this.helper = this._createHelper(a); this._cacheHelperProportions(); if (d.ui.ddmanager) d.ui.ddmanager.current = this; this._cacheMargins(); this.cssPosition = this.helper.css("position"); this.scrollParent = this.helper.scrollParent(); this.offset = this.positionAbs = this.element.offset(); this.offset = { top: this.offset.top -
this.margins.top, left: this.offset.left - this.margins.left
            }; d.extend(this.offset, { click: { left: a.pageX - this.offset.left, top: a.pageY - this.offset.top }, parent: this._getParentOffset(), relative: this._getRelativeOffset() }); this.originalPosition = this.position = this._generatePosition(a); this.originalPageX = a.pageX; this.originalPageY = a.pageY; b.cursorAt && this._adjustOffsetFromHelper(b.cursorAt); b.containment && this._setContainment(); if (this._trigger("start", a) === false) { this._clear(); return false } this._cacheHelperProportions();
            d.ui.ddmanager && !b.dropBehaviour && d.ui.ddmanager.prepareOffsets(this, a); this.helper.addClass("ui-draggable-dragging"); this._mouseDrag(a, true); return true
        }, _mouseDrag: function(a, b) {
            this.position = this._generatePosition(a); this.positionAbs = this._convertPositionTo("absolute"); if (!b) { b = this._uiHash(); if (this._trigger("drag", a, b) === false) { this._mouseUp({}); return false } this.position = b.position } if (!this.options.axis || this.options.axis != "y") this.helper[0].style.left = this.position.left + "px"; if (!this.options.axis ||
this.options.axis != "x") this.helper[0].style.top = this.position.top + "px"; d.ui.ddmanager && d.ui.ddmanager.drag(this, a); return false
        }, _mouseStop: function(a) {
            var b = false; if (d.ui.ddmanager && !this.options.dropBehaviour) b = d.ui.ddmanager.drop(this, a); if (this.dropped) { b = this.dropped; this.dropped = false } if (!this.element[0] || !this.element[0].parentNode) return false; if (this.options.revert == "invalid" && !b || this.options.revert == "valid" && b || this.options.revert === true || d.isFunction(this.options.revert) && this.options.revert.call(this.element,
b)) { var c = this; d(this.helper).animate(this.originalPosition, parseInt(this.options.revertDuration, 10), function() { c._trigger("stop", a) !== false && c._clear() }) } else this._trigger("stop", a) !== false && this._clear(); return false
        }, cancel: function() { this.helper.is(".ui-draggable-dragging") ? this._mouseUp({}) : this._clear(); return this }, _getHandle: function(a) {
            var b = !this.options.handle || !d(this.options.handle, this.element).length ? true : false; d(this.options.handle, this.element).find("*").andSelf().each(function() {
                if (this ==
a.target) b = true
            }); return b
        }, _createHelper: function(a) { var b = this.options; a = d.isFunction(b.helper) ? d(b.helper.apply(this.element[0], [a])) : b.helper == "clone" ? this.element.clone() : this.element; a.parents("body").length || a.appendTo(b.appendTo == "parent" ? this.element[0].parentNode : b.appendTo); a[0] != this.element[0] && !/(fixed|absolute)/.test(a.css("position")) && a.css("position", "absolute"); return a }, _adjustOffsetFromHelper: function(a) {
            if (typeof a == "string") a = a.split(" "); if (d.isArray(a)) a = { left: +a[0], top: +a[1] ||
0
            }; if ("left" in a) this.offset.click.left = a.left + this.margins.left; if ("right" in a) this.offset.click.left = this.helperProportions.width - a.right + this.margins.left; if ("top" in a) this.offset.click.top = a.top + this.margins.top; if ("bottom" in a) this.offset.click.top = this.helperProportions.height - a.bottom + this.margins.top
        }, _getParentOffset: function() {
            this.offsetParent = this.helper.offsetParent(); var a = this.offsetParent.offset(); if (this.cssPosition == "absolute" && this.scrollParent[0] != document && d.ui.contains(this.scrollParent[0],
this.offsetParent[0])) { a.left += this.scrollParent.scrollLeft(); a.top += this.scrollParent.scrollTop() } if (this.offsetParent[0] == document.body || this.offsetParent[0].tagName && this.offsetParent[0].tagName.toLowerCase() == "html" && d.browser.msie) a = { top: 0, left: 0 }; return { top: a.top + (parseInt(this.offsetParent.css("borderTopWidth"), 10) || 0), left: a.left + (parseInt(this.offsetParent.css("borderLeftWidth"), 10) || 0)}
        }, _getRelativeOffset: function() {
            if (this.cssPosition == "relative") {
                var a = this.element.position(); return { top: a.top -
(parseInt(this.helper.css("top"), 10) || 0) + this.scrollParent.scrollTop(), left: a.left - (parseInt(this.helper.css("left"), 10) || 0) + this.scrollParent.scrollLeft()}
                } else return { top: 0, left: 0}
            }, _cacheMargins: function() { this.margins = { left: parseInt(this.element.css("marginLeft"), 10) || 0, top: parseInt(this.element.css("marginTop"), 10) || 0} }, _cacheHelperProportions: function() { this.helperProportions = { width: this.helper.outerWidth(), height: this.helper.outerHeight()} }, _setContainment: function() {
                var a = this.options; if (a.containment ==
"parent") a.containment = this.helper[0].parentNode; if (a.containment == "document" || a.containment == "window") this.containment = [0 - this.offset.relative.left - this.offset.parent.left, 0 - this.offset.relative.top - this.offset.parent.top, d(a.containment == "document" ? document : window).width() - this.helperProportions.width - this.margins.left, (d(a.containment == "document" ? document : window).height() || document.body.parentNode.scrollHeight) - this.helperProportions.height - this.margins.top]; if (!/^(document|window|parent)$/.test(a.containment) &&
a.containment.constructor != Array) {
                    var b = d(a.containment)[0]; if (b) {
                        a = d(a.containment).offset(); var c = d(b).css("overflow") != "hidden"; this.containment = [a.left + (parseInt(d(b).css("borderLeftWidth"), 10) || 0) + (parseInt(d(b).css("paddingLeft"), 10) || 0) - this.margins.left, a.top + (parseInt(d(b).css("borderTopWidth"), 10) || 0) + (parseInt(d(b).css("paddingTop"), 10) || 0) - this.margins.top, a.left + (c ? Math.max(b.scrollWidth, b.offsetWidth) : b.offsetWidth) - (parseInt(d(b).css("borderLeftWidth"), 10) || 0) - (parseInt(d(b).css("paddingRight"),
10) || 0) - this.helperProportions.width - this.margins.left, a.top + (c ? Math.max(b.scrollHeight, b.offsetHeight) : b.offsetHeight) - (parseInt(d(b).css("borderTopWidth"), 10) || 0) - (parseInt(d(b).css("paddingBottom"), 10) || 0) - this.helperProportions.height - this.margins.top]
                    } 
                } else if (a.containment.constructor == Array) this.containment = a.containment
            }, _convertPositionTo: function(a, b) {
                if (!b) b = this.position; a = a == "absolute" ? 1 : -1; var c = this.cssPosition == "absolute" && !(this.scrollParent[0] != document && d.ui.contains(this.scrollParent[0],
this.offsetParent[0])) ? this.offsetParent : this.scrollParent, f = /(html|body)/i.test(c[0].tagName); return { top: b.top + this.offset.relative.top * a + this.offset.parent.top * a - (d.browser.safari && d.browser.version < 526 && this.cssPosition == "fixed" ? 0 : (this.cssPosition == "fixed" ? -this.scrollParent.scrollTop() : f ? 0 : c.scrollTop()) * a), left: b.left + this.offset.relative.left * a + this.offset.parent.left * a - (d.browser.safari && d.browser.version < 526 && this.cssPosition == "fixed" ? 0 : (this.cssPosition == "fixed" ? -this.scrollParent.scrollLeft() :
f ? 0 : c.scrollLeft()) * a)}
            }, _generatePosition: function(a) {
                var b = this.options, c = this.cssPosition == "absolute" && !(this.scrollParent[0] != document && d.ui.contains(this.scrollParent[0], this.offsetParent[0])) ? this.offsetParent : this.scrollParent, f = /(html|body)/i.test(c[0].tagName), e = a.pageX, g = a.pageY; if (this.originalPosition) {
                    if (this.containment) {
                        if (a.pageX - this.offset.click.left < this.containment[0]) e = this.containment[0] + this.offset.click.left; if (a.pageY - this.offset.click.top < this.containment[1]) g = this.containment[1] +
this.offset.click.top; if (a.pageX - this.offset.click.left > this.containment[2]) e = this.containment[2] + this.offset.click.left; if (a.pageY - this.offset.click.top > this.containment[3]) g = this.containment[3] + this.offset.click.top
                    } if (b.grid) {
                        g = this.originalPageY + Math.round((g - this.originalPageY) / b.grid[1]) * b.grid[1]; g = this.containment ? !(g - this.offset.click.top < this.containment[1] || g - this.offset.click.top > this.containment[3]) ? g : !(g - this.offset.click.top < this.containment[1]) ? g - b.grid[1] : g + b.grid[1] : g; e = this.originalPageX +
Math.round((e - this.originalPageX) / b.grid[0]) * b.grid[0]; e = this.containment ? !(e - this.offset.click.left < this.containment[0] || e - this.offset.click.left > this.containment[2]) ? e : !(e - this.offset.click.left < this.containment[0]) ? e - b.grid[0] : e + b.grid[0] : e
                    } 
                } return { top: g - this.offset.click.top - this.offset.relative.top - this.offset.parent.top + (d.browser.safari && d.browser.version < 526 && this.cssPosition == "fixed" ? 0 : this.cssPosition == "fixed" ? -this.scrollParent.scrollTop() : f ? 0 : c.scrollTop()), left: e - this.offset.click.left -
this.offset.relative.left - this.offset.parent.left + (d.browser.safari && d.browser.version < 526 && this.cssPosition == "fixed" ? 0 : this.cssPosition == "fixed" ? -this.scrollParent.scrollLeft() : f ? 0 : c.scrollLeft())}
                }, _clear: function() { this.helper.removeClass("ui-draggable-dragging"); this.helper[0] != this.element[0] && !this.cancelHelperRemoval && this.helper.remove(); this.helper = null; this.cancelHelperRemoval = false }, _trigger: function(a, b, c) {
                    c = c || this._uiHash(); d.ui.plugin.call(this, a, [b, c]); if (a == "drag") this.positionAbs =
this._convertPositionTo("absolute"); return d.Widget.prototype._trigger.call(this, a, b, c)
                }, plugins: {}, _uiHash: function() { return { helper: this.helper, position: this.position, originalPosition: this.originalPosition, offset: this.positionAbs} } 
            }); d.extend(d.ui.draggable, { version: "1.8.5" }); d.ui.plugin.add("draggable", "connectToSortable", { start: function(a, b) {
                var c = d(this).data("draggable"), f = c.options, e = d.extend({}, b, { item: c.element }); c.sortables = []; d(f.connectToSortable).each(function() {
                    var g = d.data(this, "sortable");
                    if (g && !g.options.disabled) { c.sortables.push({ instance: g, shouldRevert: g.options.revert }); g._refreshItems(); g._trigger("activate", a, e) } 
                })
            }, stop: function(a, b) {
                var c = d(this).data("draggable"), f = d.extend({}, b, { item: c.element }); d.each(c.sortables, function() {
                    if (this.instance.isOver) {
                        this.instance.isOver = 0; c.cancelHelperRemoval = true; this.instance.cancelHelperRemoval = false; if (this.shouldRevert) this.instance.options.revert = true; this.instance._mouseStop(a); this.instance.options.helper = this.instance.options._helper;
                        c.options.helper == "original" && this.instance.currentItem.css({ top: "auto", left: "auto" })
                    } else { this.instance.cancelHelperRemoval = false; this.instance._trigger("deactivate", a, f) } 
                })
            }, drag: function(a, b) {
                var c = d(this).data("draggable"), f = this; d.each(c.sortables, function() {
                    this.instance.positionAbs = c.positionAbs; this.instance.helperProportions = c.helperProportions; this.instance.offset.click = c.offset.click; if (this.instance._intersectsWith(this.instance.containerCache)) {
                        if (!this.instance.isOver) {
                            this.instance.isOver =
1; this.instance.currentItem = d(f).clone().appendTo(this.instance.element).data("sortable-item", true); this.instance.options._helper = this.instance.options.helper; this.instance.options.helper = function() { return b.helper[0] }; a.target = this.instance.currentItem[0]; this.instance._mouseCapture(a, true); this.instance._mouseStart(a, true, true); this.instance.offset.click.top = c.offset.click.top; this.instance.offset.click.left = c.offset.click.left; this.instance.offset.parent.left -= c.offset.parent.left - this.instance.offset.parent.left;
                            this.instance.offset.parent.top -= c.offset.parent.top - this.instance.offset.parent.top; c._trigger("toSortable", a); c.dropped = this.instance.element; c.currentItem = c.element; this.instance.fromOutside = c
                        } this.instance.currentItem && this.instance._mouseDrag(a)
                    } else if (this.instance.isOver) {
                        this.instance.isOver = 0; this.instance.cancelHelperRemoval = true; this.instance.options.revert = false; this.instance._trigger("out", a, this.instance._uiHash(this.instance)); this.instance._mouseStop(a, true); this.instance.options.helper =
this.instance.options._helper; this.instance.currentItem.remove(); this.instance.placeholder && this.instance.placeholder.remove(); c._trigger("fromSortable", a); c.dropped = false
                    } 
                })
            } 
            }); d.ui.plugin.add("draggable", "cursor", { start: function() { var a = d("body"), b = d(this).data("draggable").options; if (a.css("cursor")) b._cursor = a.css("cursor"); a.css("cursor", b.cursor) }, stop: function() { var a = d(this).data("draggable").options; a._cursor && d("body").css("cursor", a._cursor) } }); d.ui.plugin.add("draggable", "iframeFix", { start: function() {
                var a =
d(this).data("draggable").options; d(a.iframeFix === true ? "iframe" : a.iframeFix).each(function() { d('<div class="ui-draggable-iframeFix" style="background: #fff;"></div>').css({ width: this.offsetWidth + "px", height: this.offsetHeight + "px", position: "absolute", opacity: "0.001", zIndex: 1E3 }).css(d(this).offset()).appendTo("body") })
            }, stop: function() { d("div.ui-draggable-iframeFix").each(function() { this.parentNode.removeChild(this) }) } 
            }); d.ui.plugin.add("draggable", "opacity", { start: function(a, b) {
                a = d(b.helper); b = d(this).data("draggable").options;
                if (a.css("opacity")) b._opacity = a.css("opacity"); a.css("opacity", b.opacity)
            }, stop: function(a, b) { a = d(this).data("draggable").options; a._opacity && d(b.helper).css("opacity", a._opacity) } 
            }); d.ui.plugin.add("draggable", "scroll", { start: function() { var a = d(this).data("draggable"); if (a.scrollParent[0] != document && a.scrollParent[0].tagName != "HTML") a.overflowOffset = a.scrollParent.offset() }, drag: function(a) {
                var b = d(this).data("draggable"), c = b.options, f = false; if (b.scrollParent[0] != document && b.scrollParent[0].tagName !=
"HTML") {
                    if (!c.axis || c.axis != "x") if (b.overflowOffset.top + b.scrollParent[0].offsetHeight - a.pageY < c.scrollSensitivity) b.scrollParent[0].scrollTop = f = b.scrollParent[0].scrollTop + c.scrollSpeed; else if (a.pageY - b.overflowOffset.top < c.scrollSensitivity) b.scrollParent[0].scrollTop = f = b.scrollParent[0].scrollTop - c.scrollSpeed; if (!c.axis || c.axis != "y") if (b.overflowOffset.left + b.scrollParent[0].offsetWidth - a.pageX < c.scrollSensitivity) b.scrollParent[0].scrollLeft = f = b.scrollParent[0].scrollLeft + c.scrollSpeed; else if (a.pageX -
b.overflowOffset.left < c.scrollSensitivity) b.scrollParent[0].scrollLeft = f = b.scrollParent[0].scrollLeft - c.scrollSpeed
                } else {
                    if (!c.axis || c.axis != "x") if (a.pageY - d(document).scrollTop() < c.scrollSensitivity) f = d(document).scrollTop(d(document).scrollTop() - c.scrollSpeed); else if (d(window).height() - (a.pageY - d(document).scrollTop()) < c.scrollSensitivity) f = d(document).scrollTop(d(document).scrollTop() + c.scrollSpeed); if (!c.axis || c.axis != "y") if (a.pageX - d(document).scrollLeft() < c.scrollSensitivity) f = d(document).scrollLeft(d(document).scrollLeft() -
c.scrollSpeed); else if (d(window).width() - (a.pageX - d(document).scrollLeft()) < c.scrollSensitivity) f = d(document).scrollLeft(d(document).scrollLeft() + c.scrollSpeed)
                } f !== false && d.ui.ddmanager && !c.dropBehaviour && d.ui.ddmanager.prepareOffsets(b, a)
            } 
            }); d.ui.plugin.add("draggable", "snap", { start: function() {
                var a = d(this).data("draggable"), b = a.options; a.snapElements = []; d(b.snap.constructor != String ? b.snap.items || ":data(draggable)" : b.snap).each(function() {
                    var c = d(this), f = c.offset(); this != a.element[0] && a.snapElements.push({ item: this,
                        width: c.outerWidth(), height: c.outerHeight(), top: f.top, left: f.left
                    })
                })
            }, drag: function(a, b) {
                for (var c = d(this).data("draggable"), f = c.options, e = f.snapTolerance, g = b.offset.left, n = g + c.helperProportions.width, m = b.offset.top, o = m + c.helperProportions.height, h = c.snapElements.length - 1; h >= 0; h--) {
                    var i = c.snapElements[h].left, k = i + c.snapElements[h].width, j = c.snapElements[h].top, l = j + c.snapElements[h].height; if (i - e < g && g < k + e && j - e < m && m < l + e || i - e < g && g < k + e && j - e < o && o < l + e || i - e < n && n < k + e && j - e < m && m < l + e || i - e < n && n < k + e && j - e < o &&
o < l + e) {
                        if (f.snapMode != "inner") { var p = Math.abs(j - o) <= e, q = Math.abs(l - m) <= e, r = Math.abs(i - n) <= e, s = Math.abs(k - g) <= e; if (p) b.position.top = c._convertPositionTo("relative", { top: j - c.helperProportions.height, left: 0 }).top - c.margins.top; if (q) b.position.top = c._convertPositionTo("relative", { top: l, left: 0 }).top - c.margins.top; if (r) b.position.left = c._convertPositionTo("relative", { top: 0, left: i - c.helperProportions.width }).left - c.margins.left; if (s) b.position.left = c._convertPositionTo("relative", { top: 0, left: k }).left - c.margins.left } var t =
p || q || r || s; if (f.snapMode != "outer") { p = Math.abs(j - m) <= e; q = Math.abs(l - o) <= e; r = Math.abs(i - g) <= e; s = Math.abs(k - n) <= e; if (p) b.position.top = c._convertPositionTo("relative", { top: j, left: 0 }).top - c.margins.top; if (q) b.position.top = c._convertPositionTo("relative", { top: l - c.helperProportions.height, left: 0 }).top - c.margins.top; if (r) b.position.left = c._convertPositionTo("relative", { top: 0, left: i }).left - c.margins.left; if (s) b.position.left = c._convertPositionTo("relative", { top: 0, left: k - c.helperProportions.width }).left - c.margins.left } if (!c.snapElements[h].snapping &&
(p || q || r || s || t)) c.options.snap.snap && c.options.snap.snap.call(c.element, a, d.extend(c._uiHash(), { snapItem: c.snapElements[h].item })); c.snapElements[h].snapping = p || q || r || s || t
                    } else { c.snapElements[h].snapping && c.options.snap.release && c.options.snap.release.call(c.element, a, d.extend(c._uiHash(), { snapItem: c.snapElements[h].item })); c.snapElements[h].snapping = false } 
                } 
            } 
            }); d.ui.plugin.add("draggable", "stack", { start: function() {
                var a = d(this).data("draggable").options; a = d.makeArray(d(a.stack)).sort(function(c, f) {
                    return (parseInt(d(c).css("zIndex"),
10) || 0) - (parseInt(d(f).css("zIndex"), 10) || 0)
                }); if (a.length) { var b = parseInt(a[0].style.zIndex) || 0; d(a).each(function(c) { this.style.zIndex = b + c }); this[0].style.zIndex = b + a.length } 
            } 
            }); d.ui.plugin.add("draggable", "zIndex", { start: function(a, b) { a = d(b.helper); b = d(this).data("draggable").options; if (a.css("zIndex")) b._zIndex = a.css("zIndex"); a.css("zIndex", b.zIndex) }, stop: function(a, b) { a = d(this).data("draggable").options; a._zIndex && d(b.helper).css("zIndex", a._zIndex) } })
        })(jQuery);
        (function(d) {
            d.widget("ui.droppable", { widgetEventPrefix: "drop", options: { accept: "*", activeClass: false, addClasses: true, greedy: false, hoverClass: false, scope: "default", tolerance: "intersect" }, _create: function() {
                var a = this.options, b = a.accept; this.isover = 0; this.isout = 1; this.accept = d.isFunction(b) ? b : function(c) { return c.is(b) }; this.proportions = { width: this.element[0].offsetWidth, height: this.element[0].offsetHeight }; d.ui.ddmanager.droppables[a.scope] = d.ui.ddmanager.droppables[a.scope] || []; d.ui.ddmanager.droppables[a.scope].push(this);
                a.addClasses && this.element.addClass("ui-droppable")
            }, destroy: function() { for (var a = d.ui.ddmanager.droppables[this.options.scope], b = 0; b < a.length; b++) a[b] == this && a.splice(b, 1); this.element.removeClass("ui-droppable ui-droppable-disabled").removeData("droppable").unbind(".droppable"); return this }, _setOption: function(a, b) { if (a == "accept") this.accept = d.isFunction(b) ? b : function(c) { return c.is(b) }; d.Widget.prototype._setOption.apply(this, arguments) }, _activate: function(a) {
                var b = d.ui.ddmanager.current; this.options.activeClass &&
this.element.addClass(this.options.activeClass); b && this._trigger("activate", a, this.ui(b))
            }, _deactivate: function(a) { var b = d.ui.ddmanager.current; this.options.activeClass && this.element.removeClass(this.options.activeClass); b && this._trigger("deactivate", a, this.ui(b)) }, _over: function(a) {
                var b = d.ui.ddmanager.current; if (!(!b || (b.currentItem || b.element)[0] == this.element[0])) if (this.accept.call(this.element[0], b.currentItem || b.element)) {
                    this.options.hoverClass && this.element.addClass(this.options.hoverClass);
                    this._trigger("over", a, this.ui(b))
                } 
            }, _out: function(a) { var b = d.ui.ddmanager.current; if (!(!b || (b.currentItem || b.element)[0] == this.element[0])) if (this.accept.call(this.element[0], b.currentItem || b.element)) { this.options.hoverClass && this.element.removeClass(this.options.hoverClass); this._trigger("out", a, this.ui(b)) } }, _drop: function(a, b) {
                var c = b || d.ui.ddmanager.current; if (!c || (c.currentItem || c.element)[0] == this.element[0]) return false; var e = false; this.element.find(":data(droppable)").not(".ui-draggable-dragging").each(function() {
                    var g =
d.data(this, "droppable"); if (g.options.greedy && !g.options.disabled && g.options.scope == c.options.scope && g.accept.call(g.element[0], c.currentItem || c.element) && d.ui.intersect(c, d.extend(g, { offset: g.element.offset() }), g.options.tolerance)) { e = true; return false } 
                }); if (e) return false; if (this.accept.call(this.element[0], c.currentItem || c.element)) {
                    this.options.activeClass && this.element.removeClass(this.options.activeClass); this.options.hoverClass && this.element.removeClass(this.options.hoverClass); this._trigger("drop",
a, this.ui(c)); return this.element
                } return false
            }, ui: function(a) { return { draggable: a.currentItem || a.element, helper: a.helper, position: a.position, offset: a.positionAbs} } 
            }); d.extend(d.ui.droppable, { version: "1.8.5" }); d.ui.intersect = function(a, b, c) {
                if (!b.offset) return false; var e = (a.positionAbs || a.position.absolute).left, g = e + a.helperProportions.width, f = (a.positionAbs || a.position.absolute).top, h = f + a.helperProportions.height, i = b.offset.left, k = i + b.proportions.width, j = b.offset.top, l = j + b.proportions.height;
                switch (c) {
                    case "fit": return i <= e && g <= k && j <= f && h <= l; case "intersect": return i < e + a.helperProportions.width / 2 && g - a.helperProportions.width / 2 < k && j < f + a.helperProportions.height / 2 && h - a.helperProportions.height / 2 < l; case "pointer": return d.ui.isOver((a.positionAbs || a.position.absolute).top + (a.clickOffset || a.offset.click).top, (a.positionAbs || a.position.absolute).left + (a.clickOffset || a.offset.click).left, j, i, b.proportions.height, b.proportions.width); case "touch": return (f >= j && f <= l || h >= j && h <= l || f < j && h > l) && (e >=
i && e <= k || g >= i && g <= k || e < i && g > k); default: return false
                } 
            }; d.ui.ddmanager = { current: null, droppables: { "default": [] }, prepareOffsets: function(a, b) {
                var c = d.ui.ddmanager.droppables[a.options.scope] || [], e = b ? b.type : null, g = (a.currentItem || a.element).find(":data(droppable)").andSelf(), f = 0; a: for (; f < c.length; f++) if (!(c[f].options.disabled || a && !c[f].accept.call(c[f].element[0], a.currentItem || a.element))) {
                    for (var h = 0; h < g.length; h++) if (g[h] == c[f].element[0]) { c[f].proportions.height = 0; continue a } c[f].visible = c[f].element.css("display") !=
"none"; if (c[f].visible) { c[f].offset = c[f].element.offset(); c[f].proportions = { width: c[f].element[0].offsetWidth, height: c[f].element[0].offsetHeight }; e == "mousedown" && c[f]._activate.call(c[f], b) } 
                } 
            }, drop: function(a, b) {
                var c = false; d.each(d.ui.ddmanager.droppables[a.options.scope] || [], function() {
                    if (this.options) {
                        if (!this.options.disabled && this.visible && d.ui.intersect(a, this, this.options.tolerance)) c = c || this._drop.call(this, b); if (!this.options.disabled && this.visible && this.accept.call(this.element[0], a.currentItem ||
a.element)) { this.isout = 1; this.isover = 0; this._deactivate.call(this, b) } 
                    } 
                }); return c
            }, drag: function(a, b) {
                a.options.refreshPositions && d.ui.ddmanager.prepareOffsets(a, b); d.each(d.ui.ddmanager.droppables[a.options.scope] || [], function() {
                    if (!(this.options.disabled || this.greedyChild || !this.visible)) {
                        var c = d.ui.intersect(a, this, this.options.tolerance); if (c = !c && this.isover == 1 ? "isout" : c && this.isover == 0 ? "isover" : null) {
                            var e; if (this.options.greedy) {
                                var g = this.element.parents(":data(droppable):eq(0)"); if (g.length) {
                                    e =
d.data(g[0], "droppable"); e.greedyChild = c == "isover" ? 1 : 0
                                } 
                            } if (e && c == "isover") { e.isover = 0; e.isout = 1; e._out.call(e, b) } this[c] = 1; this[c == "isout" ? "isover" : "isout"] = 0; this[c == "isover" ? "_over" : "_out"].call(this, b); if (e && c == "isout") { e.isout = 0; e.isover = 1; e._over.call(e, b) } 
                        } 
                    } 
                })
            } }
            })(jQuery);
            (function(e) {
                e.widget("ui.resizable", e.ui.mouse, { widgetEventPrefix: "resize", options: { alsoResize: false, animate: false, animateDuration: "slow", animateEasing: "swing", aspectRatio: false, autoHide: false, containment: false, ghost: false, grid: false, handles: "e,s,se", helper: false, maxHeight: null, maxWidth: null, minHeight: 10, minWidth: 10, zIndex: 1E3 }, _create: function() {
                    var b = this, a = this.options; this.element.addClass("ui-resizable"); e.extend(this, { _aspectRatio: !!a.aspectRatio, aspectRatio: a.aspectRatio, originalElement: this.element,
                        _proportionallyResizeElements: [], _helper: a.helper || a.ghost || a.animate ? a.helper || "ui-resizable-helper" : null
                    }); if (this.element[0].nodeName.match(/canvas|textarea|input|select|button|img/i)) {
                        /relative/.test(this.element.css("position")) && e.browser.opera && this.element.css({ position: "relative", top: "auto", left: "auto" }); this.element.wrap(e('<div class="ui-wrapper" style="overflow: hidden;"></div>').css({ position: this.element.css("position"), width: this.element.outerWidth(), height: this.element.outerHeight(),
                            top: this.element.css("top"), left: this.element.css("left")
                        })); this.element = this.element.parent().data("resizable", this.element.data("resizable")); this.elementIsWrapper = true; this.element.css({ marginLeft: this.originalElement.css("marginLeft"), marginTop: this.originalElement.css("marginTop"), marginRight: this.originalElement.css("marginRight"), marginBottom: this.originalElement.css("marginBottom") }); this.originalElement.css({ marginLeft: 0, marginTop: 0, marginRight: 0, marginBottom: 0 }); this.originalResizeStyle =
this.originalElement.css("resize"); this.originalElement.css("resize", "none"); this._proportionallyResizeElements.push(this.originalElement.css({ position: "static", zoom: 1, display: "block" })); this.originalElement.css({ margin: this.originalElement.css("margin") }); this._proportionallyResize()
                    } this.handles = a.handles || (!e(".ui-resizable-handle", this.element).length ? "e,s,se" : { n: ".ui-resizable-n", e: ".ui-resizable-e", s: ".ui-resizable-s", w: ".ui-resizable-w", se: ".ui-resizable-se", sw: ".ui-resizable-sw", ne: ".ui-resizable-ne",
                        nw: ".ui-resizable-nw"
                    }); if (this.handles.constructor == String) { if (this.handles == "all") this.handles = "n,e,s,w,se,sw,ne,nw"; var c = this.handles.split(","); this.handles = {}; for (var d = 0; d < c.length; d++) { var f = e.trim(c[d]), g = e('<div class="ui-resizable-handle ' + ("ui-resizable-" + f) + '"></div>'); /sw|se|ne|nw/.test(f) && g.css({ zIndex: ++a.zIndex }); "se" == f && g.addClass("ui-icon ui-icon-gripsmall-diagonal-se"); this.handles[f] = ".ui-resizable-" + f; this.element.append(g) } } this._renderAxis = function(h) {
                        h = h || this.element; for (var i in this.handles) {
                            if (this.handles[i].constructor ==
String) this.handles[i] = e(this.handles[i], this.element).show(); if (this.elementIsWrapper && this.originalElement[0].nodeName.match(/textarea|input|select|button/i)) { var j = e(this.handles[i], this.element), k = 0; k = /sw|ne|nw|se|n|s/.test(i) ? j.outerHeight() : j.outerWidth(); j = ["padding", /ne|nw|n/.test(i) ? "Top" : /se|sw|s/.test(i) ? "Bottom" : /^e$/.test(i) ? "Right" : "Left"].join(""); h.css(j, k); this._proportionallyResize() } e(this.handles[i])
                        } 
                    }; this._renderAxis(this.element); this._handles = e(".ui-resizable-handle", this.element).disableSelection();
                    this._handles.mouseover(function() { if (!b.resizing) { if (this.className) var h = this.className.match(/ui-resizable-(se|sw|ne|nw|n|e|s|w)/i); b.axis = h && h[1] ? h[1] : "se" } }); if (a.autoHide) { this._handles.hide(); e(this.element).addClass("ui-resizable-autohide").hover(function() { e(this).removeClass("ui-resizable-autohide"); b._handles.show() }, function() { if (!b.resizing) { e(this).addClass("ui-resizable-autohide"); b._handles.hide() } }) } this._mouseInit()
                }, destroy: function() {
                    this._mouseDestroy(); var b = function(c) { e(c).removeClass("ui-resizable ui-resizable-disabled ui-resizable-resizing").removeData("resizable").unbind(".resizable").find(".ui-resizable-handle").remove() };
                    if (this.elementIsWrapper) { b(this.element); var a = this.element; a.after(this.originalElement.css({ position: a.css("position"), width: a.outerWidth(), height: a.outerHeight(), top: a.css("top"), left: a.css("left") })).remove() } this.originalElement.css("resize", this.originalResizeStyle); b(this.originalElement); return this
                }, _mouseCapture: function(b) { var a = false; for (var c in this.handles) if (e(this.handles[c])[0] == b.target) a = true; return !this.options.disabled && a }, _mouseStart: function(b) {
                    var a = this.options, c = this.element.position(),
d = this.element; this.resizing = true; this.documentScroll = { top: e(document).scrollTop(), left: e(document).scrollLeft() }; if (d.is(".ui-draggable") || /absolute/.test(d.css("position"))) d.css({ position: "absolute", top: c.top, left: c.left }); e.browser.opera && /relative/.test(d.css("position")) && d.css({ position: "relative", top: "auto", left: "auto" }); this._renderProxy(); c = m(this.helper.css("left")); var f = m(this.helper.css("top")); if (a.containment) { c += e(a.containment).scrollLeft() || 0; f += e(a.containment).scrollTop() || 0 } this.offset =
this.helper.offset(); this.position = { left: c, top: f }; this.size = this._helper ? { width: d.outerWidth(), height: d.outerHeight()} : { width: d.width(), height: d.height() }; this.originalSize = this._helper ? { width: d.outerWidth(), height: d.outerHeight()} : { width: d.width(), height: d.height() }; this.originalPosition = { left: c, top: f }; this.sizeDiff = { width: d.outerWidth() - d.width(), height: d.outerHeight() - d.height() }; this.originalMousePosition = { left: b.pageX, top: b.pageY }; this.aspectRatio = typeof a.aspectRatio == "number" ? a.aspectRatio :
this.originalSize.width / this.originalSize.height || 1; a = e(".ui-resizable-" + this.axis).css("cursor"); e("body").css("cursor", a == "auto" ? this.axis + "-resize" : a); d.addClass("ui-resizable-resizing"); this._propagate("start", b); return true
                }, _mouseDrag: function(b) {
                    var a = this.helper, c = this.originalMousePosition, d = this._change[this.axis]; if (!d) return false; c = d.apply(this, [b, b.pageX - c.left || 0, b.pageY - c.top || 0]); if (this._aspectRatio || b.shiftKey) c = this._updateRatio(c, b); c = this._respectSize(c, b); this._propagate("resize",
b); a.css({ top: this.position.top + "px", left: this.position.left + "px", width: this.size.width + "px", height: this.size.height + "px" }); !this._helper && this._proportionallyResizeElements.length && this._proportionallyResize(); this._updateCache(c); this._trigger("resize", b, this.ui()); return false
                }, _mouseStop: function(b) {
                    this.resizing = false; var a = this.options, c = this; if (this._helper) {
                        var d = this._proportionallyResizeElements, f = d.length && /textarea/i.test(d[0].nodeName); d = f && e.ui.hasScroll(d[0], "left") ? 0 : c.sizeDiff.height;
                        f = { width: c.size.width - (f ? 0 : c.sizeDiff.width), height: c.size.height - d }; d = parseInt(c.element.css("left"), 10) + (c.position.left - c.originalPosition.left) || null; var g = parseInt(c.element.css("top"), 10) + (c.position.top - c.originalPosition.top) || null; a.animate || this.element.css(e.extend(f, { top: g, left: d })); c.helper.height(c.size.height); c.helper.width(c.size.width); this._helper && !a.animate && this._proportionallyResize()
                    } e("body").css("cursor", "auto"); this.element.removeClass("ui-resizable-resizing"); this._propagate("stop",
b); this._helper && this.helper.remove(); return false
                }, _updateCache: function(b) { this.offset = this.helper.offset(); if (l(b.left)) this.position.left = b.left; if (l(b.top)) this.position.top = b.top; if (l(b.height)) this.size.height = b.height; if (l(b.width)) this.size.width = b.width }, _updateRatio: function(b) {
                    var a = this.position, c = this.size, d = this.axis; if (b.height) b.width = c.height * this.aspectRatio; else if (b.width) b.height = c.width / this.aspectRatio; if (d == "sw") { b.left = a.left + (c.width - b.width); b.top = null } if (d == "nw") {
                        b.top =
a.top + (c.height - b.height); b.left = a.left + (c.width - b.width)
                    } return b
                }, _respectSize: function(b) {
                    var a = this.options, c = this.axis, d = l(b.width) && a.maxWidth && a.maxWidth < b.width, f = l(b.height) && a.maxHeight && a.maxHeight < b.height, g = l(b.width) && a.minWidth && a.minWidth > b.width, h = l(b.height) && a.minHeight && a.minHeight > b.height; if (g) b.width = a.minWidth; if (h) b.height = a.minHeight; if (d) b.width = a.maxWidth; if (f) b.height = a.maxHeight; var i = this.originalPosition.left + this.originalSize.width, j = this.position.top + this.size.height,
k = /sw|nw|w/.test(c); c = /nw|ne|n/.test(c); if (g && k) b.left = i - a.minWidth; if (d && k) b.left = i - a.maxWidth; if (h && c) b.top = j - a.minHeight; if (f && c) b.top = j - a.maxHeight; if ((a = !b.width && !b.height) && !b.left && b.top) b.top = null; else if (a && !b.top && b.left) b.left = null; return b
                }, _proportionallyResize: function() {
                    if (this._proportionallyResizeElements.length) for (var b = this.helper || this.element, a = 0; a < this._proportionallyResizeElements.length; a++) {
                        var c = this._proportionallyResizeElements[a]; if (!this.borderDif) {
                            var d = [c.css("borderTopWidth"),
c.css("borderRightWidth"), c.css("borderBottomWidth"), c.css("borderLeftWidth")], f = [c.css("paddingTop"), c.css("paddingRight"), c.css("paddingBottom"), c.css("paddingLeft")]; this.borderDif = e.map(d, function(g, h) { g = parseInt(g, 10) || 0; h = parseInt(f[h], 10) || 0; return g + h })
                        } e.browser.msie && (e(b).is(":hidden") || e(b).parents(":hidden").length) || c.css({ height: b.height() - this.borderDif[0] - this.borderDif[2] || 0, width: b.width() - this.borderDif[1] - this.borderDif[3] || 0 })
                    } 
                }, _renderProxy: function() {
                    var b = this.options; this.elementOffset =
this.element.offset(); if (this._helper) { this.helper = this.helper || e('<div style="overflow:hidden;"></div>'); var a = e.browser.msie && e.browser.version < 7, c = a ? 1 : 0; a = a ? 2 : -1; this.helper.addClass(this._helper).css({ width: this.element.outerWidth() + a, height: this.element.outerHeight() + a, position: "absolute", left: this.elementOffset.left - c + "px", top: this.elementOffset.top - c + "px", zIndex: ++b.zIndex }); this.helper.appendTo("body").disableSelection() } else this.helper = this.element
                }, _change: { e: function(b, a) {
                    return { width: this.originalSize.width +
a}
                    }, w: function(b, a) { return { left: this.originalPosition.left + a, width: this.originalSize.width - a} }, n: function(b, a, c) { return { top: this.originalPosition.top + c, height: this.originalSize.height - c} }, s: function(b, a, c) { return { height: this.originalSize.height + c} }, se: function(b, a, c) { return e.extend(this._change.s.apply(this, arguments), this._change.e.apply(this, [b, a, c])) }, sw: function(b, a, c) { return e.extend(this._change.s.apply(this, arguments), this._change.w.apply(this, [b, a, c])) }, ne: function(b, a, c) {
                        return e.extend(this._change.n.apply(this,
arguments), this._change.e.apply(this, [b, a, c]))
                    }, nw: function(b, a, c) { return e.extend(this._change.n.apply(this, arguments), this._change.w.apply(this, [b, a, c])) } 
                }, _propagate: function(b, a) { e.ui.plugin.call(this, b, [a, this.ui()]); b != "resize" && this._trigger(b, a, this.ui()) }, plugins: {}, ui: function() { return { originalElement: this.originalElement, element: this.element, helper: this.helper, position: this.position, size: this.size, originalSize: this.originalSize, originalPosition: this.originalPosition} } 
                }); e.extend(e.ui.resizable,
{ version: "1.8.5" }); e.ui.plugin.add("resizable", "alsoResize", { start: function() {
    var b = e(this).data("resizable").options, a = function(c) { e(c).each(function() { var d = e(this); d.data("resizable-alsoresize", { width: parseInt(d.width(), 10), height: parseInt(d.height(), 10), left: parseInt(d.css("left"), 10), top: parseInt(d.css("top"), 10), position: d.css("position") }) }) }; if (typeof b.alsoResize == "object" && !b.alsoResize.parentNode) if (b.alsoResize.length) { b.alsoResize = b.alsoResize[0]; a(b.alsoResize) } else e.each(b.alsoResize,
function(c) { a(c) }); else a(b.alsoResize)
}, resize: function(b, a) {
    var c = e(this).data("resizable"); b = c.options; var d = c.originalSize, f = c.originalPosition, g = { height: c.size.height - d.height || 0, width: c.size.width - d.width || 0, top: c.position.top - f.top || 0, left: c.position.left - f.left || 0 }, h = function(i, j) {
        e(i).each(function() {
            var k = e(this), q = e(this).data("resizable-alsoresize"), p = {}, r = j && j.length ? j : k.parents(a.originalElement[0]).length ? ["width", "height"] : ["width", "height", "top", "left"]; e.each(r, function(n, o) {
                if ((n =
(q[o] || 0) + (g[o] || 0)) && n >= 0) p[o] = n || null
            }); if (e.browser.opera && /relative/.test(k.css("position"))) { c._revertToRelativePosition = true; k.css({ position: "absolute", top: "auto", left: "auto" }) } k.css(p)
        })
    }; typeof b.alsoResize == "object" && !b.alsoResize.nodeType ? e.each(b.alsoResize, function(i, j) { h(i, j) }) : h(b.alsoResize)
}, stop: function() {
    var b = e(this).data("resizable"), a = b.options, c = function(d) { e(d).each(function() { var f = e(this); f.css({ position: f.data("resizable-alsoresize").position }) }) }; if (b._revertToRelativePosition) {
        b._revertToRelativePosition =
false; typeof a.alsoResize == "object" && !a.alsoResize.nodeType ? e.each(a.alsoResize, function(d) { c(d) }) : c(a.alsoResize)
    } e(this).removeData("resizable-alsoresize")
} 
}); e.ui.plugin.add("resizable", "animate", { stop: function(b) {
    var a = e(this).data("resizable"), c = a.options, d = a._proportionallyResizeElements, f = d.length && /textarea/i.test(d[0].nodeName), g = f && e.ui.hasScroll(d[0], "left") ? 0 : a.sizeDiff.height; f = { width: a.size.width - (f ? 0 : a.sizeDiff.width), height: a.size.height - g }; g = parseInt(a.element.css("left"), 10) + (a.position.left -
a.originalPosition.left) || null; var h = parseInt(a.element.css("top"), 10) + (a.position.top - a.originalPosition.top) || null; a.element.animate(e.extend(f, h && g ? { top: h, left: g} : {}), { duration: c.animateDuration, easing: c.animateEasing, step: function() {
    var i = { width: parseInt(a.element.css("width"), 10), height: parseInt(a.element.css("height"), 10), top: parseInt(a.element.css("top"), 10), left: parseInt(a.element.css("left"), 10) }; d && d.length && e(d[0]).css({ width: i.width, height: i.height }); a._updateCache(i); a._propagate("resize",
b)
} 
})
} 
}); e.ui.plugin.add("resizable", "containment", { start: function() {
    var b = e(this).data("resizable"), a = b.element, c = b.options.containment; if (a = c instanceof e ? c.get(0) : /parent/.test(c) ? a.parent().get(0) : c) {
        b.containerElement = e(a); if (/document/.test(c) || c == document) { b.containerOffset = { left: 0, top: 0 }; b.containerPosition = { left: 0, top: 0 }; b.parentData = { element: e(document), left: 0, top: 0, width: e(document).width(), height: e(document).height() || document.body.parentNode.scrollHeight} } else {
            var d = e(a), f = []; e(["Top",
"Right", "Left", "Bottom"]).each(function(i, j) { f[i] = m(d.css("padding" + j)) }); b.containerOffset = d.offset(); b.containerPosition = d.position(); b.containerSize = { height: d.innerHeight() - f[3], width: d.innerWidth() - f[1] }; c = b.containerOffset; var g = b.containerSize.height, h = b.containerSize.width; h = e.ui.hasScroll(a, "left") ? a.scrollWidth : h; g = e.ui.hasScroll(a) ? a.scrollHeight : g; b.parentData = { element: a, left: c.left, top: c.top, width: h, height: g}
        } 
    } 
}, resize: function(b) {
    var a = e(this).data("resizable"), c = a.options, d = a.containerOffset,
f = a.position; b = a._aspectRatio || b.shiftKey; var g = { top: 0, left: 0 }, h = a.containerElement; if (h[0] != document && /static/.test(h.css("position"))) g = d; if (f.left < (a._helper ? d.left : 0)) { a.size.width += a._helper ? a.position.left - d.left : a.position.left - g.left; if (b) a.size.height = a.size.width / c.aspectRatio; a.position.left = c.helper ? d.left : 0 } if (f.top < (a._helper ? d.top : 0)) { a.size.height += a._helper ? a.position.top - d.top : a.position.top; if (b) a.size.width = a.size.height * c.aspectRatio; a.position.top = a._helper ? d.top : 0 } a.offset.left =
a.parentData.left + a.position.left; a.offset.top = a.parentData.top + a.position.top; c = Math.abs((a._helper ? a.offset.left - g.left : a.offset.left - g.left) + a.sizeDiff.width); d = Math.abs((a._helper ? a.offset.top - g.top : a.offset.top - d.top) + a.sizeDiff.height); f = a.containerElement.get(0) == a.element.parent().get(0); g = /relative|absolute/.test(a.containerElement.css("position")); if (f && g) c -= a.parentData.left; if (c + a.size.width >= a.parentData.width) { a.size.width = a.parentData.width - c; if (b) a.size.height = a.size.width / a.aspectRatio } if (d +
a.size.height >= a.parentData.height) { a.size.height = a.parentData.height - d; if (b) a.size.width = a.size.height * a.aspectRatio } 
}, stop: function() {
    var b = e(this).data("resizable"), a = b.options, c = b.containerOffset, d = b.containerPosition, f = b.containerElement, g = e(b.helper), h = g.offset(), i = g.outerWidth() - b.sizeDiff.width; g = g.outerHeight() - b.sizeDiff.height; b._helper && !a.animate && /relative/.test(f.css("position")) && e(this).css({ left: h.left - d.left - c.left, width: i, height: g }); b._helper && !a.animate && /static/.test(f.css("position")) &&
e(this).css({ left: h.left - d.left - c.left, width: i, height: g })
} 
}); e.ui.plugin.add("resizable", "ghost", { start: function() { var b = e(this).data("resizable"), a = b.options, c = b.size; b.ghost = b.originalElement.clone(); b.ghost.css({ opacity: 0.25, display: "block", position: "relative", height: c.height, width: c.width, margin: 0, left: 0, top: 0 }).addClass("ui-resizable-ghost").addClass(typeof a.ghost == "string" ? a.ghost : ""); b.ghost.appendTo(b.helper) }, resize: function() {
    var b = e(this).data("resizable"); b.ghost && b.ghost.css({ position: "relative",
        height: b.size.height, width: b.size.width
    })
}, stop: function() { var b = e(this).data("resizable"); b.ghost && b.helper && b.helper.get(0).removeChild(b.ghost.get(0)) } 
}); e.ui.plugin.add("resizable", "grid", { resize: function() {
    var b = e(this).data("resizable"), a = b.options, c = b.size, d = b.originalSize, f = b.originalPosition, g = b.axis; a.grid = typeof a.grid == "number" ? [a.grid, a.grid] : a.grid; var h = Math.round((c.width - d.width) / (a.grid[0] || 1)) * (a.grid[0] || 1); a = Math.round((c.height - d.height) / (a.grid[1] || 1)) * (a.grid[1] || 1); if (/^(se|s|e)$/.test(g)) {
        b.size.width =
d.width + h; b.size.height = d.height + a
    } else if (/^(ne)$/.test(g)) { b.size.width = d.width + h; b.size.height = d.height + a; b.position.top = f.top - a } else { if (/^(sw)$/.test(g)) { b.size.width = d.width + h; b.size.height = d.height + a } else { b.size.width = d.width + h; b.size.height = d.height + a; b.position.top = f.top - a } b.position.left = f.left - h } 
} 
}); var m = function(b) { return parseInt(b, 10) || 0 }, l = function(b) { return !isNaN(parseInt(b, 10)) } 
            })(jQuery);
            (function(e) {
                e.widget("ui.selectable", e.ui.mouse, { options: { appendTo: "body", autoRefresh: true, distance: 0, filter: "*", tolerance: "touch" }, _create: function() {
                    var c = this; this.element.addClass("ui-selectable"); this.dragged = false; var f; this.refresh = function() {
                        f = e(c.options.filter, c.element[0]); f.each(function() {
                            var d = e(this), b = d.offset(); e.data(this, "selectable-item", { element: this, $element: d, left: b.left, top: b.top, right: b.left + d.outerWidth(), bottom: b.top + d.outerHeight(), startselected: false, selected: d.hasClass("ui-selected"),
                                selecting: d.hasClass("ui-selecting"), unselecting: d.hasClass("ui-unselecting")
                            })
                        })
                    }; this.refresh(); this.selectees = f.addClass("ui-selectee"); this._mouseInit(); this.helper = e("<div class='ui-selectable-helper'></div>")
                }, destroy: function() { this.selectees.removeClass("ui-selectee").removeData("selectable-item"); this.element.removeClass("ui-selectable ui-selectable-disabled").removeData("selectable").unbind(".selectable"); this._mouseDestroy(); return this }, _mouseStart: function(c) {
                    var f = this; this.opos = [c.pageX,
c.pageY]; if (!this.options.disabled) {
                        var d = this.options; this.selectees = e(d.filter, this.element[0]); this._trigger("start", c); e(d.appendTo).append(this.helper); this.helper.css({ left: c.clientX, top: c.clientY, width: 0, height: 0 }); d.autoRefresh && this.refresh(); this.selectees.filter(".ui-selected").each(function() {
                            var b = e.data(this, "selectable-item"); b.startselected = true; if (!c.metaKey) {
                                b.$element.removeClass("ui-selected"); b.selected = false; b.$element.addClass("ui-unselecting"); b.unselecting = true; f._trigger("unselecting",
c, { unselecting: b.element })
                            } 
                        }); e(c.target).parents().andSelf().each(function() { var b = e.data(this, "selectable-item"); if (b) { var g = !c.metaKey || !b.$element.hasClass("ui-selected"); b.$element.removeClass(g ? "ui-unselecting" : "ui-selected").addClass(g ? "ui-selecting" : "ui-unselecting"); b.unselecting = !g; b.selecting = g; (b.selected = g) ? f._trigger("selecting", c, { selecting: b.element }) : f._trigger("unselecting", c, { unselecting: b.element }); return false } })
                    } 
                }, _mouseDrag: function(c) {
                    var f = this; this.dragged = true; if (!this.options.disabled) {
                        var d =
this.options, b = this.opos[0], g = this.opos[1], h = c.pageX, i = c.pageY; if (b > h) { var j = h; h = b; b = j } if (g > i) { j = i; i = g; g = j } this.helper.css({ left: b, top: g, width: h - b, height: i - g }); this.selectees.each(function() {
    var a = e.data(this, "selectable-item"); if (!(!a || a.element == f.element[0])) {
        var k = false; if (d.tolerance == "touch") k = !(a.left > h || a.right < b || a.top > i || a.bottom < g); else if (d.tolerance == "fit") k = a.left > b && a.right < h && a.top > g && a.bottom < i; if (k) {
            if (a.selected) { a.$element.removeClass("ui-selected"); a.selected = false } if (a.unselecting) {
                a.$element.removeClass("ui-unselecting");
                a.unselecting = false
            } if (!a.selecting) { a.$element.addClass("ui-selecting"); a.selecting = true; f._trigger("selecting", c, { selecting: a.element }) } 
        } else {
            if (a.selecting) if (c.metaKey && a.startselected) { a.$element.removeClass("ui-selecting"); a.selecting = false; a.$element.addClass("ui-selected"); a.selected = true } else { a.$element.removeClass("ui-selecting"); a.selecting = false; if (a.startselected) { a.$element.addClass("ui-unselecting"); a.unselecting = true } f._trigger("unselecting", c, { unselecting: a.element }) } if (a.selected) if (!c.metaKey &&
!a.startselected) { a.$element.removeClass("ui-selected"); a.selected = false; a.$element.addClass("ui-unselecting"); a.unselecting = true; f._trigger("unselecting", c, { unselecting: a.element }) } 
        } 
    } 
}); return false
                    } 
                }, _mouseStop: function(c) {
                    var f = this; this.dragged = false; e(".ui-unselecting", this.element[0]).each(function() { var d = e.data(this, "selectable-item"); d.$element.removeClass("ui-unselecting"); d.unselecting = false; d.startselected = false; f._trigger("unselected", c, { unselected: d.element }) }); e(".ui-selecting", this.element[0]).each(function() {
                        var d =
e.data(this, "selectable-item"); d.$element.removeClass("ui-selecting").addClass("ui-selected"); d.selecting = false; d.selected = true; d.startselected = true; f._trigger("selected", c, { selected: d.element })
                    }); this._trigger("stop", c); this.helper.remove(); return false
                } 
                }); e.extend(e.ui.selectable, { version: "1.8.5" })
            })(jQuery);
            (function(d) {
                d.widget("ui.sortable", d.ui.mouse, { widgetEventPrefix: "sort", options: { appendTo: "parent", axis: false, connectWith: false, containment: false, cursor: "auto", cursorAt: false, dropOnEmpty: true, forcePlaceholderSize: false, forceHelperSize: false, grid: false, handle: false, helper: "original", items: "> *", opacity: false, placeholder: false, revert: false, scroll: true, scrollSensitivity: 20, scrollSpeed: 20, scope: "default", tolerance: "intersect", zIndex: 1E3 }, _create: function() {
                    this.containerCache = {}; this.element.addClass("ui-sortable");
                    this.refresh(); this.floating = this.items.length ? /left|right/.test(this.items[0].item.css("float")) : false; this.offset = this.element.offset(); this._mouseInit()
                }, destroy: function() { this.element.removeClass("ui-sortable ui-sortable-disabled").removeData("sortable").unbind(".sortable"); this._mouseDestroy(); for (var a = this.items.length - 1; a >= 0; a--) this.items[a].item.removeData("sortable-item"); return this }, _setOption: function(a, b) {
                    if (a === "disabled") { this.options[a] = b; this.widget()[b ? "addClass" : "removeClass"]("ui-sortable-disabled") } else d.Widget.prototype._setOption.apply(this,
arguments)
                }, _mouseCapture: function(a, b) {
                    if (this.reverting) return false; if (this.options.disabled || this.options.type == "static") return false; this._refreshItems(a); var c = null, e = this; d(a.target).parents().each(function() { if (d.data(this, "sortable-item") == e) { c = d(this); return false } }); if (d.data(a.target, "sortable-item") == e) c = d(a.target); if (!c) return false; if (this.options.handle && !b) { var f = false; d(this.options.handle, c).find("*").andSelf().each(function() { if (this == a.target) f = true }); if (!f) return false } this.currentItem =
c; this._removeCurrentsFromItems(); return true
                }, _mouseStart: function(a, b, c) {
                    b = this.options; var e = this; this.currentContainer = this; this.refreshPositions(); this.helper = this._createHelper(a); this._cacheHelperProportions(); this._cacheMargins(); this.scrollParent = this.helper.scrollParent(); this.offset = this.currentItem.offset(); this.offset = { top: this.offset.top - this.margins.top, left: this.offset.left - this.margins.left }; this.helper.css("position", "absolute"); this.cssPosition = this.helper.css("position"); d.extend(this.offset,
{ click: { left: a.pageX - this.offset.left, top: a.pageY - this.offset.top }, parent: this._getParentOffset(), relative: this._getRelativeOffset() }); this.originalPosition = this._generatePosition(a); this.originalPageX = a.pageX; this.originalPageY = a.pageY; b.cursorAt && this._adjustOffsetFromHelper(b.cursorAt); this.domPosition = { prev: this.currentItem.prev()[0], parent: this.currentItem.parent()[0] }; this.helper[0] != this.currentItem[0] && this.currentItem.hide(); this._createPlaceholder(); b.containment && this._setContainment();
                    if (b.cursor) { if (d("body").css("cursor")) this._storedCursor = d("body").css("cursor"); d("body").css("cursor", b.cursor) } if (b.opacity) { if (this.helper.css("opacity")) this._storedOpacity = this.helper.css("opacity"); this.helper.css("opacity", b.opacity) } if (b.zIndex) { if (this.helper.css("zIndex")) this._storedZIndex = this.helper.css("zIndex"); this.helper.css("zIndex", b.zIndex) } if (this.scrollParent[0] != document && this.scrollParent[0].tagName != "HTML") this.overflowOffset = this.scrollParent.offset(); this._trigger("start",
a, this._uiHash()); this._preserveHelperProportions || this._cacheHelperProportions(); if (!c) for (c = this.containers.length - 1; c >= 0; c--) this.containers[c]._trigger("activate", a, e._uiHash(this)); if (d.ui.ddmanager) d.ui.ddmanager.current = this; d.ui.ddmanager && !b.dropBehaviour && d.ui.ddmanager.prepareOffsets(this, a); this.dragging = true; this.helper.addClass("ui-sortable-helper"); this._mouseDrag(a); return true
                }, _mouseDrag: function(a) {
                    this.position = this._generatePosition(a); this.positionAbs = this._convertPositionTo("absolute");
                    if (!this.lastPositionAbs) this.lastPositionAbs = this.positionAbs; if (this.options.scroll) {
                        var b = this.options, c = false; if (this.scrollParent[0] != document && this.scrollParent[0].tagName != "HTML") {
                            if (this.overflowOffset.top + this.scrollParent[0].offsetHeight - a.pageY < b.scrollSensitivity) this.scrollParent[0].scrollTop = c = this.scrollParent[0].scrollTop + b.scrollSpeed; else if (a.pageY - this.overflowOffset.top < b.scrollSensitivity) this.scrollParent[0].scrollTop = c = this.scrollParent[0].scrollTop - b.scrollSpeed; if (this.overflowOffset.left +
this.scrollParent[0].offsetWidth - a.pageX < b.scrollSensitivity) this.scrollParent[0].scrollLeft = c = this.scrollParent[0].scrollLeft + b.scrollSpeed; else if (a.pageX - this.overflowOffset.left < b.scrollSensitivity) this.scrollParent[0].scrollLeft = c = this.scrollParent[0].scrollLeft - b.scrollSpeed
                        } else {
                            if (a.pageY - d(document).scrollTop() < b.scrollSensitivity) c = d(document).scrollTop(d(document).scrollTop() - b.scrollSpeed); else if (d(window).height() - (a.pageY - d(document).scrollTop()) < b.scrollSensitivity) c = d(document).scrollTop(d(document).scrollTop() +
b.scrollSpeed); if (a.pageX - d(document).scrollLeft() < b.scrollSensitivity) c = d(document).scrollLeft(d(document).scrollLeft() - b.scrollSpeed); else if (d(window).width() - (a.pageX - d(document).scrollLeft()) < b.scrollSensitivity) c = d(document).scrollLeft(d(document).scrollLeft() + b.scrollSpeed)
                        } c !== false && d.ui.ddmanager && !b.dropBehaviour && d.ui.ddmanager.prepareOffsets(this, a)
                    } this.positionAbs = this._convertPositionTo("absolute"); if (!this.options.axis || this.options.axis != "y") this.helper[0].style.left = this.position.left +
"px"; if (!this.options.axis || this.options.axis != "x") this.helper[0].style.top = this.position.top + "px"; for (b = this.items.length - 1; b >= 0; b--) {
                        c = this.items[b]; var e = c.item[0], f = this._intersectsWithPointer(c); if (f) if (e != this.currentItem[0] && this.placeholder[f == 1 ? "next" : "prev"]()[0] != e && !d.ui.contains(this.placeholder[0], e) && (this.options.type == "semi-dynamic" ? !d.ui.contains(this.element[0], e) : true)) {
                            this.direction = f == 1 ? "down" : "up"; if (this.options.tolerance == "pointer" || this._intersectsWithSides(c)) this._rearrange(a,
c); else break; this._trigger("change", a, this._uiHash()); break
                        } 
                    } this._contactContainers(a); d.ui.ddmanager && d.ui.ddmanager.drag(this, a); this._trigger("sort", a, this._uiHash()); this.lastPositionAbs = this.positionAbs; return false
                }, _mouseStop: function(a, b) {
                    if (a) {
                        d.ui.ddmanager && !this.options.dropBehaviour && d.ui.ddmanager.drop(this, a); if (this.options.revert) {
                            var c = this; b = c.placeholder.offset(); c.reverting = true; d(this.helper).animate({ left: b.left - this.offset.parent.left - c.margins.left + (this.offsetParent[0] ==
document.body ? 0 : this.offsetParent[0].scrollLeft), top: b.top - this.offset.parent.top - c.margins.top + (this.offsetParent[0] == document.body ? 0 : this.offsetParent[0].scrollTop)
                            }, parseInt(this.options.revert, 10) || 500, function() { c._clear(a) })
                        } else this._clear(a, b); return false
                    } 
                }, cancel: function() {
                    var a = this; if (this.dragging) {
                        this._mouseUp(); this.options.helper == "original" ? this.currentItem.css(this._storedCSS).removeClass("ui-sortable-helper") : this.currentItem.show(); for (var b = this.containers.length - 1; b >= 0; b--) {
                            this.containers[b]._trigger("deactivate",
null, a._uiHash(this)); if (this.containers[b].containerCache.over) { this.containers[b]._trigger("out", null, a._uiHash(this)); this.containers[b].containerCache.over = 0 } 
                        } 
                    } this.placeholder[0].parentNode && this.placeholder[0].parentNode.removeChild(this.placeholder[0]); this.options.helper != "original" && this.helper && this.helper[0].parentNode && this.helper.remove(); d.extend(this, { helper: null, dragging: false, reverting: false, _noFinalSort: null }); this.domPosition.prev ? d(this.domPosition.prev).after(this.currentItem) :
d(this.domPosition.parent).prepend(this.currentItem); return this
                }, serialize: function(a) { var b = this._getItemsAsjQuery(a && a.connected), c = []; a = a || {}; d(b).each(function() { var e = (d(a.item || this).attr(a.attribute || "id") || "").match(a.expression || /(.+)[-=_](.+)/); if (e) c.push((a.key || e[1] + "[]") + "=" + (a.key && a.expression ? e[1] : e[2])) }); !c.length && a.key && c.push(a.key + "="); return c.join("&") }, toArray: function(a) {
                    var b = this._getItemsAsjQuery(a && a.connected), c = []; a = a || {}; b.each(function() {
                        c.push(d(a.item || this).attr(a.attribute ||
"id") || "")
                    }); return c
                }, _intersectsWith: function(a) {
                    var b = this.positionAbs.left, c = b + this.helperProportions.width, e = this.positionAbs.top, f = e + this.helperProportions.height, g = a.left, h = g + a.width, i = a.top, k = i + a.height, j = this.offset.click.top, l = this.offset.click.left; j = e + j > i && e + j < k && b + l > g && b + l < h; return this.options.tolerance == "pointer" || this.options.forcePointerForContainers || this.options.tolerance != "pointer" && this.helperProportions[this.floating ? "width" : "height"] > a[this.floating ? "width" : "height"] ? j : g < b +
this.helperProportions.width / 2 && c - this.helperProportions.width / 2 < h && i < e + this.helperProportions.height / 2 && f - this.helperProportions.height / 2 < k
                }, _intersectsWithPointer: function(a) {
                    var b = d.ui.isOverAxis(this.positionAbs.top + this.offset.click.top, a.top, a.height); a = d.ui.isOverAxis(this.positionAbs.left + this.offset.click.left, a.left, a.width); b = b && a; a = this._getDragVerticalDirection(); var c = this._getDragHorizontalDirection(); if (!b) return false; return this.floating ? c && c == "right" || a == "down" ? 2 : 1 : a && (a == "down" ?
2 : 1)
                }, _intersectsWithSides: function(a) { var b = d.ui.isOverAxis(this.positionAbs.top + this.offset.click.top, a.top + a.height / 2, a.height); a = d.ui.isOverAxis(this.positionAbs.left + this.offset.click.left, a.left + a.width / 2, a.width); var c = this._getDragVerticalDirection(), e = this._getDragHorizontalDirection(); return this.floating && e ? e == "right" && a || e == "left" && !a : c && (c == "down" && b || c == "up" && !b) }, _getDragVerticalDirection: function() { var a = this.positionAbs.top - this.lastPositionAbs.top; return a != 0 && (a > 0 ? "down" : "up") },
                    _getDragHorizontalDirection: function() { var a = this.positionAbs.left - this.lastPositionAbs.left; return a != 0 && (a > 0 ? "right" : "left") }, refresh: function(a) { this._refreshItems(a); this.refreshPositions(); return this }, _connectWith: function() { var a = this.options; return a.connectWith.constructor == String ? [a.connectWith] : a.connectWith }, _getItemsAsjQuery: function(a) {
                        var b = [], c = [], e = this._connectWith(); if (e && a) for (a = e.length - 1; a >= 0; a--) for (var f = d(e[a]), g = f.length - 1; g >= 0; g--) {
                            var h = d.data(f[g], "sortable"); if (h && h !=
this && !h.options.disabled) c.push([d.isFunction(h.options.items) ? h.options.items.call(h.element) : d(h.options.items, h.element).not(".ui-sortable-helper").not(".ui-sortable-placeholder"), h])
                        } c.push([d.isFunction(this.options.items) ? this.options.items.call(this.element, null, { options: this.options, item: this.currentItem }) : d(this.options.items, this.element).not(".ui-sortable-helper").not(".ui-sortable-placeholder"), this]); for (a = c.length - 1; a >= 0; a--) c[a][0].each(function() { b.push(this) }); return d(b)
                    }, _removeCurrentsFromItems: function() {
                        for (var a =
this.currentItem.find(":data(sortable-item)"), b = 0; b < this.items.length; b++) for (var c = 0; c < a.length; c++) a[c] == this.items[b].item[0] && this.items.splice(b, 1)
                    }, _refreshItems: function(a) {
                        this.items = []; this.containers = [this]; var b = this.items, c = [[d.isFunction(this.options.items) ? this.options.items.call(this.element[0], a, { item: this.currentItem }) : d(this.options.items, this.element), this]], e = this._connectWith(); if (e) for (var f = e.length - 1; f >= 0; f--) for (var g = d(e[f]), h = g.length - 1; h >= 0; h--) {
                            var i = d.data(g[h], "sortable");
                            if (i && i != this && !i.options.disabled) { c.push([d.isFunction(i.options.items) ? i.options.items.call(i.element[0], a, { item: this.currentItem }) : d(i.options.items, i.element), i]); this.containers.push(i) } 
                        } for (f = c.length - 1; f >= 0; f--) { a = c[f][1]; e = c[f][0]; h = 0; for (g = e.length; h < g; h++) { i = d(e[h]); i.data("sortable-item", a); b.push({ item: i, instance: a, width: 0, height: 0, left: 0, top: 0 }) } } 
                    }, refreshPositions: function(a) {
                        if (this.offsetParent && this.helper) this.offset.parent = this._getParentOffset(); for (var b = this.items.length - 1; b >=
0; b--) { var c = this.items[b], e = this.options.toleranceElement ? d(this.options.toleranceElement, c.item) : c.item; if (!a) { c.width = e.outerWidth(); c.height = e.outerHeight() } e = e.offset(); c.left = e.left; c.top = e.top } if (this.options.custom && this.options.custom.refreshContainers) this.options.custom.refreshContainers.call(this); else for (b = this.containers.length - 1; b >= 0; b--) {
                            e = this.containers[b].element.offset(); this.containers[b].containerCache.left = e.left; this.containers[b].containerCache.top = e.top; this.containers[b].containerCache.width =
this.containers[b].element.outerWidth(); this.containers[b].containerCache.height = this.containers[b].element.outerHeight()
                        } return this
                    }, _createPlaceholder: function(a) {
                        var b = a || this, c = b.options; if (!c.placeholder || c.placeholder.constructor == String) {
                            var e = c.placeholder; c.placeholder = { element: function() { var f = d(document.createElement(b.currentItem[0].nodeName)).addClass(e || b.currentItem[0].className + " ui-sortable-placeholder").removeClass("ui-sortable-helper")[0]; if (!e) f.style.visibility = "hidden"; return f },
                                update: function(f, g) { if (!(e && !c.forcePlaceholderSize)) { g.height() || g.height(b.currentItem.innerHeight() - parseInt(b.currentItem.css("paddingTop") || 0, 10) - parseInt(b.currentItem.css("paddingBottom") || 0, 10)); g.width() || g.width(b.currentItem.innerWidth() - parseInt(b.currentItem.css("paddingLeft") || 0, 10) - parseInt(b.currentItem.css("paddingRight") || 0, 10)) } } }
                            } b.placeholder = d(c.placeholder.element.call(b.element, b.currentItem)); b.currentItem.after(b.placeholder); c.placeholder.update(b, b.placeholder)
                        }, _contactContainers: function(a) {
                            for (var b =
null, c = null, e = this.containers.length - 1; e >= 0; e--) if (!d.ui.contains(this.currentItem[0], this.containers[e].element[0])) if (this._intersectsWith(this.containers[e].containerCache)) { if (!(b && d.ui.contains(this.containers[e].element[0], b.element[0]))) { b = this.containers[e]; c = e } } else if (this.containers[e].containerCache.over) { this.containers[e]._trigger("out", a, this._uiHash(this)); this.containers[e].containerCache.over = 0 } if (b) if (this.containers.length === 1) {
                                this.containers[c]._trigger("over", a, this._uiHash(this));
                                this.containers[c].containerCache.over = 1
                            } else if (this.currentContainer != this.containers[c]) {
                                b = 1E4; e = null; for (var f = this.positionAbs[this.containers[c].floating ? "left" : "top"], g = this.items.length - 1; g >= 0; g--) if (d.ui.contains(this.containers[c].element[0], this.items[g].item[0])) { var h = this.items[g][this.containers[c].floating ? "left" : "top"]; if (Math.abs(h - f) < b) { b = Math.abs(h - f); e = this.items[g] } } if (e || this.options.dropOnEmpty) {
                                    this.currentContainer = this.containers[c]; e ? this._rearrange(a, e, null, true) : this._rearrange(a,
null, this.containers[c].element, true); this._trigger("change", a, this._uiHash()); this.containers[c]._trigger("change", a, this._uiHash(this)); this.options.placeholder.update(this.currentContainer, this.placeholder); this.containers[c]._trigger("over", a, this._uiHash(this)); this.containers[c].containerCache.over = 1
                                } 
                            } 
                        }, _createHelper: function(a) {
                            var b = this.options; a = d.isFunction(b.helper) ? d(b.helper.apply(this.element[0], [a, this.currentItem])) : b.helper == "clone" ? this.currentItem.clone() : this.currentItem; a.parents("body").length ||
d(b.appendTo != "parent" ? b.appendTo : this.currentItem[0].parentNode)[0].appendChild(a[0]); if (a[0] == this.currentItem[0]) this._storedCSS = { width: this.currentItem[0].style.width, height: this.currentItem[0].style.height, position: this.currentItem.css("position"), top: this.currentItem.css("top"), left: this.currentItem.css("left") }; if (a[0].style.width == "" || b.forceHelperSize) a.width(this.currentItem.width()); if (a[0].style.height == "" || b.forceHelperSize) a.height(this.currentItem.height()); return a
                        }, _adjustOffsetFromHelper: function(a) {
                            if (typeof a ==
"string") a = a.split(" "); if (d.isArray(a)) a = { left: +a[0], top: +a[1] || 0 }; if ("left" in a) this.offset.click.left = a.left + this.margins.left; if ("right" in a) this.offset.click.left = this.helperProportions.width - a.right + this.margins.left; if ("top" in a) this.offset.click.top = a.top + this.margins.top; if ("bottom" in a) this.offset.click.top = this.helperProportions.height - a.bottom + this.margins.top
                        }, _getParentOffset: function() {
                            this.offsetParent = this.helper.offsetParent(); var a = this.offsetParent.offset(); if (this.cssPosition ==
"absolute" && this.scrollParent[0] != document && d.ui.contains(this.scrollParent[0], this.offsetParent[0])) { a.left += this.scrollParent.scrollLeft(); a.top += this.scrollParent.scrollTop() } if (this.offsetParent[0] == document.body || this.offsetParent[0].tagName && this.offsetParent[0].tagName.toLowerCase() == "html" && d.browser.msie) a = { top: 0, left: 0 }; return { top: a.top + (parseInt(this.offsetParent.css("borderTopWidth"), 10) || 0), left: a.left + (parseInt(this.offsetParent.css("borderLeftWidth"), 10) || 0)}
                        }, _getRelativeOffset: function() {
                            if (this.cssPosition ==
"relative") { var a = this.currentItem.position(); return { top: a.top - (parseInt(this.helper.css("top"), 10) || 0) + this.scrollParent.scrollTop(), left: a.left - (parseInt(this.helper.css("left"), 10) || 0) + this.scrollParent.scrollLeft()} } else return { top: 0, left: 0}
                        }, _cacheMargins: function() { this.margins = { left: parseInt(this.currentItem.css("marginLeft"), 10) || 0, top: parseInt(this.currentItem.css("marginTop"), 10) || 0} }, _cacheHelperProportions: function() { this.helperProportions = { width: this.helper.outerWidth(), height: this.helper.outerHeight()} },
                        _setContainment: function() {
                            var a = this.options; if (a.containment == "parent") a.containment = this.helper[0].parentNode; if (a.containment == "document" || a.containment == "window") this.containment = [0 - this.offset.relative.left - this.offset.parent.left, 0 - this.offset.relative.top - this.offset.parent.top, d(a.containment == "document" ? document : window).width() - this.helperProportions.width - this.margins.left, (d(a.containment == "document" ? document : window).height() || document.body.parentNode.scrollHeight) - this.helperProportions.height -
this.margins.top]; if (!/^(document|window|parent)$/.test(a.containment)) {
                                var b = d(a.containment)[0]; a = d(a.containment).offset(); var c = d(b).css("overflow") != "hidden"; this.containment = [a.left + (parseInt(d(b).css("borderLeftWidth"), 10) || 0) + (parseInt(d(b).css("paddingLeft"), 10) || 0) - this.margins.left, a.top + (parseInt(d(b).css("borderTopWidth"), 10) || 0) + (parseInt(d(b).css("paddingTop"), 10) || 0) - this.margins.top, a.left + (c ? Math.max(b.scrollWidth, b.offsetWidth) : b.offsetWidth) - (parseInt(d(b).css("borderLeftWidth"),
10) || 0) - (parseInt(d(b).css("paddingRight"), 10) || 0) - this.helperProportions.width - this.margins.left, a.top + (c ? Math.max(b.scrollHeight, b.offsetHeight) : b.offsetHeight) - (parseInt(d(b).css("borderTopWidth"), 10) || 0) - (parseInt(d(b).css("paddingBottom"), 10) || 0) - this.helperProportions.height - this.margins.top]
                            } 
                        }, _convertPositionTo: function(a, b) {
                            if (!b) b = this.position; a = a == "absolute" ? 1 : -1; var c = this.cssPosition == "absolute" && !(this.scrollParent[0] != document && d.ui.contains(this.scrollParent[0], this.offsetParent[0])) ?
this.offsetParent : this.scrollParent, e = /(html|body)/i.test(c[0].tagName); return { top: b.top + this.offset.relative.top * a + this.offset.parent.top * a - (d.browser.safari && this.cssPosition == "fixed" ? 0 : (this.cssPosition == "fixed" ? -this.scrollParent.scrollTop() : e ? 0 : c.scrollTop()) * a), left: b.left + this.offset.relative.left * a + this.offset.parent.left * a - (d.browser.safari && this.cssPosition == "fixed" ? 0 : (this.cssPosition == "fixed" ? -this.scrollParent.scrollLeft() : e ? 0 : c.scrollLeft()) * a)}
                        }, _generatePosition: function(a) {
                            var b =
this.options, c = this.cssPosition == "absolute" && !(this.scrollParent[0] != document && d.ui.contains(this.scrollParent[0], this.offsetParent[0])) ? this.offsetParent : this.scrollParent, e = /(html|body)/i.test(c[0].tagName); if (this.cssPosition == "relative" && !(this.scrollParent[0] != document && this.scrollParent[0] != this.offsetParent[0])) this.offset.relative = this._getRelativeOffset(); var f = a.pageX, g = a.pageY; if (this.originalPosition) {
                                if (this.containment) {
                                    if (a.pageX - this.offset.click.left < this.containment[0]) f = this.containment[0] +
this.offset.click.left; if (a.pageY - this.offset.click.top < this.containment[1]) g = this.containment[1] + this.offset.click.top; if (a.pageX - this.offset.click.left > this.containment[2]) f = this.containment[2] + this.offset.click.left; if (a.pageY - this.offset.click.top > this.containment[3]) g = this.containment[3] + this.offset.click.top
                                } if (b.grid) {
                                    g = this.originalPageY + Math.round((g - this.originalPageY) / b.grid[1]) * b.grid[1]; g = this.containment ? !(g - this.offset.click.top < this.containment[1] || g - this.offset.click.top > this.containment[3]) ?
g : !(g - this.offset.click.top < this.containment[1]) ? g - b.grid[1] : g + b.grid[1] : g; f = this.originalPageX + Math.round((f - this.originalPageX) / b.grid[0]) * b.grid[0]; f = this.containment ? !(f - this.offset.click.left < this.containment[0] || f - this.offset.click.left > this.containment[2]) ? f : !(f - this.offset.click.left < this.containment[0]) ? f - b.grid[0] : f + b.grid[0] : f
                                } 
                            } return { top: g - this.offset.click.top - this.offset.relative.top - this.offset.parent.top + (d.browser.safari && this.cssPosition == "fixed" ? 0 : this.cssPosition == "fixed" ? -this.scrollParent.scrollTop() :
e ? 0 : c.scrollTop()), left: f - this.offset.click.left - this.offset.relative.left - this.offset.parent.left + (d.browser.safari && this.cssPosition == "fixed" ? 0 : this.cssPosition == "fixed" ? -this.scrollParent.scrollLeft() : e ? 0 : c.scrollLeft())}
                            }, _rearrange: function(a, b, c, e) {
                                c ? c[0].appendChild(this.placeholder[0]) : b.item[0].parentNode.insertBefore(this.placeholder[0], this.direction == "down" ? b.item[0] : b.item[0].nextSibling); this.counter = this.counter ? ++this.counter : 1; var f = this, g = this.counter; window.setTimeout(function() {
                                    g ==
f.counter && f.refreshPositions(!e)
                                }, 0)
                            }, _clear: function(a, b) {
                                this.reverting = false; var c = []; !this._noFinalSort && this.currentItem[0].parentNode && this.placeholder.before(this.currentItem); this._noFinalSort = null; if (this.helper[0] == this.currentItem[0]) { for (var e in this._storedCSS) if (this._storedCSS[e] == "auto" || this._storedCSS[e] == "static") this._storedCSS[e] = ""; this.currentItem.css(this._storedCSS).removeClass("ui-sortable-helper") } else this.currentItem.show(); this.fromOutside && !b && c.push(function(f) {
                                    this._trigger("receive",
f, this._uiHash(this.fromOutside))
                                }); if ((this.fromOutside || this.domPosition.prev != this.currentItem.prev().not(".ui-sortable-helper")[0] || this.domPosition.parent != this.currentItem.parent()[0]) && !b) c.push(function(f) { this._trigger("update", f, this._uiHash()) }); if (!d.ui.contains(this.element[0], this.currentItem[0])) {
                                    b || c.push(function(f) { this._trigger("remove", f, this._uiHash()) }); for (e = this.containers.length - 1; e >= 0; e--) if (d.ui.contains(this.containers[e].element[0], this.currentItem[0]) && !b) {
                                        c.push(function(f) {
                                            return function(g) {
                                                f._trigger("receive",
g, this._uiHash(this))
                                            } 
                                        } .call(this, this.containers[e])); c.push(function(f) { return function(g) { f._trigger("update", g, this._uiHash(this)) } } .call(this, this.containers[e]))
                                    } 
                                } for (e = this.containers.length - 1; e >= 0; e--) {
                                    b || c.push(function(f) { return function(g) { f._trigger("deactivate", g, this._uiHash(this)) } } .call(this, this.containers[e])); if (this.containers[e].containerCache.over) {
                                        c.push(function(f) { return function(g) { f._trigger("out", g, this._uiHash(this)) } } .call(this, this.containers[e])); this.containers[e].containerCache.over =
0
                                    } 
                                } this._storedCursor && d("body").css("cursor", this._storedCursor); this._storedOpacity && this.helper.css("opacity", this._storedOpacity); if (this._storedZIndex) this.helper.css("zIndex", this._storedZIndex == "auto" ? "" : this._storedZIndex); this.dragging = false; if (this.cancelHelperRemoval) { if (!b) { this._trigger("beforeStop", a, this._uiHash()); for (e = 0; e < c.length; e++) c[e].call(this, a); this._trigger("stop", a, this._uiHash()) } return false } b || this._trigger("beforeStop", a, this._uiHash()); this.placeholder[0].parentNode.removeChild(this.placeholder[0]);
                                this.helper[0] != this.currentItem[0] && this.helper.remove(); this.helper = null; if (!b) { for (e = 0; e < c.length; e++) c[e].call(this, a); this._trigger("stop", a, this._uiHash()) } this.fromOutside = false; return true
                            }, _trigger: function() { d.Widget.prototype._trigger.apply(this, arguments) === false && this.cancel() }, _uiHash: function(a) { var b = a || this; return { helper: b.helper, placeholder: b.placeholder || d([]), position: b.position, originalPosition: b.originalPosition, offset: b.positionAbs, item: b.currentItem, sender: a ? a.element : null} } 
                        });
                        d.extend(d.ui.sortable, { version: "1.8.5" })
                    })(jQuery);
                    jQuery.effects || function(f, j) {
                        function l(c) {
                            var a; if (c && c.constructor == Array && c.length == 3) return c; if (a = /rgb\(\s*([0-9]{1,3})\s*,\s*([0-9]{1,3})\s*,\s*([0-9]{1,3})\s*\)/.exec(c)) return [parseInt(a[1], 10), parseInt(a[2], 10), parseInt(a[3], 10)]; if (a = /rgb\(\s*([0-9]+(?:\.[0-9]+)?)\%\s*,\s*([0-9]+(?:\.[0-9]+)?)\%\s*,\s*([0-9]+(?:\.[0-9]+)?)\%\s*\)/.exec(c)) return [parseFloat(a[1]) * 2.55, parseFloat(a[2]) * 2.55, parseFloat(a[3]) * 2.55]; if (a = /#([a-fA-F0-9]{2})([a-fA-F0-9]{2})([a-fA-F0-9]{2})/.exec(c)) return [parseInt(a[1],
16), parseInt(a[2], 16), parseInt(a[3], 16)]; if (a = /#([a-fA-F0-9])([a-fA-F0-9])([a-fA-F0-9])/.exec(c)) return [parseInt(a[1] + a[1], 16), parseInt(a[2] + a[2], 16), parseInt(a[3] + a[3], 16)]; if (/rgba\(0, 0, 0, 0\)/.exec(c)) return m.transparent; return m[f.trim(c).toLowerCase()]
                        } function r(c, a) { var b; do { b = f.curCSS(c, a); if (b != "" && b != "transparent" || f.nodeName(c, "body")) break; a = "backgroundColor" } while (c = c.parentNode); return l(b) } function n() {
                            var c = document.defaultView ? document.defaultView.getComputedStyle(this, null) : this.currentStyle,
a = {}, b, d; if (c && c.length && c[0] && c[c[0]]) for (var e = c.length; e--; ) { b = c[e]; if (typeof c[b] == "string") { d = b.replace(/\-(\w)/g, function(g, h) { return h.toUpperCase() }); a[d] = c[b] } } else for (b in c) if (typeof c[b] === "string") a[b] = c[b]; return a
                        } function o(c) { var a, b; for (a in c) { b = c[a]; if (b == null || f.isFunction(b) || a in s || /scrollbar/.test(a) || !/color/i.test(a) && isNaN(parseFloat(b))) delete c[a] } return c } function t(c, a) { var b = { _: 0 }, d; for (d in a) if (c[d] != a[d]) b[d] = a[d]; return b } function k(c, a, b, d) {
                            if (typeof c == "object") {
                                d =
a; b = null; a = c; c = a.effect
                            } if (f.isFunction(a)) { d = a; b = null; a = {} } if (typeof a == "number" || f.fx.speeds[a]) { d = b; b = a; a = {} } if (f.isFunction(b)) { d = b; b = null } a = a || {}; b = b || a.duration; b = f.fx.off ? 0 : typeof b == "number" ? b : f.fx.speeds[b] || f.fx.speeds._default; d = d || a.complete; return [c, a, b, d]
                        } f.effects = {}; f.each(["backgroundColor", "borderBottomColor", "borderLeftColor", "borderRightColor", "borderTopColor", "color", "outlineColor"], function(c, a) {
                            f.fx.step[a] = function(b) {
                                if (!b.colorInit) {
                                    b.start = r(b.elem, a); b.end = l(b.end); b.colorInit =
true
                                } b.elem.style[a] = "rgb(" + Math.max(Math.min(parseInt(b.pos * (b.end[0] - b.start[0]) + b.start[0], 10), 255), 0) + "," + Math.max(Math.min(parseInt(b.pos * (b.end[1] - b.start[1]) + b.start[1], 10), 255), 0) + "," + Math.max(Math.min(parseInt(b.pos * (b.end[2] - b.start[2]) + b.start[2], 10), 255), 0) + ")"
                            } 
                        }); var m = { aqua: [0, 255, 255], azure: [240, 255, 255], beige: [245, 245, 220], black: [0, 0, 0], blue: [0, 0, 255], brown: [165, 42, 42], cyan: [0, 255, 255], darkblue: [0, 0, 139], darkcyan: [0, 139, 139], darkgrey: [169, 169, 169], darkgreen: [0, 100, 0], darkkhaki: [189,
183, 107], darkmagenta: [139, 0, 139], darkolivegreen: [85, 107, 47], darkorange: [255, 140, 0], darkorchid: [153, 50, 204], darkred: [139, 0, 0], darksalmon: [233, 150, 122], darkviolet: [148, 0, 211], fuchsia: [255, 0, 255], gold: [255, 215, 0], green: [0, 128, 0], indigo: [75, 0, 130], khaki: [240, 230, 140], lightblue: [173, 216, 230], lightcyan: [224, 255, 255], lightgreen: [144, 238, 144], lightgrey: [211, 211, 211], lightpink: [255, 182, 193], lightyellow: [255, 255, 224], lime: [0, 255, 0], magenta: [255, 0, 255], maroon: [128, 0, 0], navy: [0, 0, 128], olive: [128, 128, 0], orange: [255,
165, 0], pink: [255, 192, 203], purple: [128, 0, 128], violet: [128, 0, 128], red: [255, 0, 0], silver: [192, 192, 192], white: [255, 255, 255], yellow: [255, 255, 0], transparent: [255, 255, 255]
                        }, p = ["add", "remove", "toggle"], s = { border: 1, borderBottom: 1, borderColor: 1, borderLeft: 1, borderRight: 1, borderTop: 1, borderWidth: 1, margin: 1, padding: 1 }; f.effects.animateClass = function(c, a, b, d) {
                            if (f.isFunction(b)) { d = b; b = null } return this.each(function() {
                                var e = f(this), g = e.attr("style") || " ", h = o(n.call(this)), q, u = e.attr("className"); f.each(p, function(v,
i) { c[i] && e[i + "Class"](c[i]) }); q = o(n.call(this)); e.attr("className", u); e.animate(t(h, q), a, b, function() { f.each(p, function(v, i) { c[i] && e[i + "Class"](c[i]) }); if (typeof e.attr("style") == "object") { e.attr("style").cssText = ""; e.attr("style").cssText = g } else e.attr("style", g); d && d.apply(this, arguments) })
                            })
                        }; f.fn.extend({ _addClass: f.fn.addClass, addClass: function(c, a, b, d) { return a ? f.effects.animateClass.apply(this, [{ add: c }, a, b, d]) : this._addClass(c) }, _removeClass: f.fn.removeClass, removeClass: function(c, a, b, d) {
                            return a ?
f.effects.animateClass.apply(this, [{ remove: c }, a, b, d]) : this._removeClass(c)
                        }, _toggleClass: f.fn.toggleClass, toggleClass: function(c, a, b, d, e) { return typeof a == "boolean" || a === j ? b ? f.effects.animateClass.apply(this, [a ? { add: c} : { remove: c }, b, d, e]) : this._toggleClass(c, a) : f.effects.animateClass.apply(this, [{ toggle: c }, a, b, d]) }, switchClass: function(c, a, b, d, e) { return f.effects.animateClass.apply(this, [{ add: a, remove: c }, b, d, e]) } 
                        }); f.extend(f.effects, { version: "1.8.5", save: function(c, a) {
                            for (var b = 0; b < a.length; b++) a[b] !==
null && c.data("ec.storage." + a[b], c[0].style[a[b]])
                        }, restore: function(c, a) { for (var b = 0; b < a.length; b++) a[b] !== null && c.css(a[b], c.data("ec.storage." + a[b])) }, setMode: function(c, a) { if (a == "toggle") a = c.is(":hidden") ? "show" : "hide"; return a }, getBaseline: function(c, a) { var b; switch (c[0]) { case "top": b = 0; break; case "middle": b = 0.5; break; case "bottom": b = 1; break; default: b = c[0] / a.height } switch (c[1]) { case "left": c = 0; break; case "center": c = 0.5; break; case "right": c = 1; break; default: c = c[1] / a.width } return { x: c, y: b} }, createWrapper: function(c) {
                            if (c.parent().is(".ui-effects-wrapper")) return c.parent();
                            var a = { width: c.outerWidth(true), height: c.outerHeight(true), "float": c.css("float") }, b = f("<div></div>").addClass("ui-effects-wrapper").css({ fontSize: "100%", background: "transparent", border: "none", margin: 0, padding: 0 }); c.wrap(b); b = c.parent(); if (c.css("position") == "static") { b.css({ position: "relative" }); c.css({ position: "relative" }) } else {
                                f.extend(a, { position: c.css("position"), zIndex: c.css("z-index") }); f.each(["top", "left", "bottom", "right"], function(d, e) { a[e] = c.css(e); if (isNaN(parseInt(a[e], 10))) a[e] = "auto" });
                                c.css({ position: "relative", top: 0, left: 0 })
                            } return b.css(a).show()
                        }, removeWrapper: function(c) { if (c.parent().is(".ui-effects-wrapper")) return c.parent().replaceWith(c); return c }, setTransition: function(c, a, b, d) { d = d || {}; f.each(a, function(e, g) { unit = c.cssUnit(g); if (unit[0] > 0) d[g] = unit[0] * b + unit[1] }); return d } 
                        }); f.fn.extend({ effect: function(c) { var a = k.apply(this, arguments); a = { options: a[1], duration: a[2], callback: a[3] }; var b = f.effects[c]; return b && !f.fx.off ? b.call(this, a) : this }, _show: f.fn.show, show: function(c) {
                            if (!c ||
typeof c == "number" || f.fx.speeds[c] || !f.effects[c]) return this._show.apply(this, arguments); else { var a = k.apply(this, arguments); a[1].mode = "show"; return this.effect.apply(this, a) } 
                        }, _hide: f.fn.hide, hide: function(c) { if (!c || typeof c == "number" || f.fx.speeds[c] || !f.effects[c]) return this._hide.apply(this, arguments); else { var a = k.apply(this, arguments); a[1].mode = "hide"; return this.effect.apply(this, a) } }, __toggle: f.fn.toggle, toggle: function(c) {
                            if (!c || typeof c == "number" || f.fx.speeds[c] || !f.effects[c] || typeof c ==
"boolean" || f.isFunction(c)) return this.__toggle.apply(this, arguments); else { var a = k.apply(this, arguments); a[1].mode = "toggle"; return this.effect.apply(this, a) } 
                        }, cssUnit: function(c) { var a = this.css(c), b = []; f.each(["em", "px", "%", "pt"], function(d, e) { if (a.indexOf(e) > 0) b = [parseFloat(a), e] }); return b } 
                        }); f.easing.jswing = f.easing.swing; f.extend(f.easing, { def: "easeOutQuad", swing: function(c, a, b, d, e) { return f.easing[f.easing.def](c, a, b, d, e) }, easeInQuad: function(c, a, b, d, e) { return d * (a /= e) * a + b }, easeOutQuad: function(c,
a, b, d, e) { return -d * (a /= e) * (a - 2) + b }, easeInOutQuad: function(c, a, b, d, e) { if ((a /= e / 2) < 1) return d / 2 * a * a + b; return -d / 2 * (--a * (a - 2) - 1) + b }, easeInCubic: function(c, a, b, d, e) { return d * (a /= e) * a * a + b }, easeOutCubic: function(c, a, b, d, e) { return d * ((a = a / e - 1) * a * a + 1) + b }, easeInOutCubic: function(c, a, b, d, e) { if ((a /= e / 2) < 1) return d / 2 * a * a * a + b; return d / 2 * ((a -= 2) * a * a + 2) + b }, easeInQuart: function(c, a, b, d, e) { return d * (a /= e) * a * a * a + b }, easeOutQuart: function(c, a, b, d, e) { return -d * ((a = a / e - 1) * a * a * a - 1) + b }, easeInOutQuart: function(c, a, b, d, e) {
    if ((a /=
e / 2) < 1) return d / 2 * a * a * a * a + b; return -d / 2 * ((a -= 2) * a * a * a - 2) + b
}, easeInQuint: function(c, a, b, d, e) { return d * (a /= e) * a * a * a * a + b }, easeOutQuint: function(c, a, b, d, e) { return d * ((a = a / e - 1) * a * a * a * a + 1) + b }, easeInOutQuint: function(c, a, b, d, e) { if ((a /= e / 2) < 1) return d / 2 * a * a * a * a * a + b; return d / 2 * ((a -= 2) * a * a * a * a + 2) + b }, easeInSine: function(c, a, b, d, e) { return -d * Math.cos(a / e * (Math.PI / 2)) + d + b }, easeOutSine: function(c, a, b, d, e) { return d * Math.sin(a / e * (Math.PI / 2)) + b }, easeInOutSine: function(c, a, b, d, e) {
    return -d / 2 * (Math.cos(Math.PI * a / e) - 1) +
b
}, easeInExpo: function(c, a, b, d, e) { return a == 0 ? b : d * Math.pow(2, 10 * (a / e - 1)) + b }, easeOutExpo: function(c, a, b, d, e) { return a == e ? b + d : d * (-Math.pow(2, -10 * a / e) + 1) + b }, easeInOutExpo: function(c, a, b, d, e) { if (a == 0) return b; if (a == e) return b + d; if ((a /= e / 2) < 1) return d / 2 * Math.pow(2, 10 * (a - 1)) + b; return d / 2 * (-Math.pow(2, -10 * --a) + 2) + b }, easeInCirc: function(c, a, b, d, e) { return -d * (Math.sqrt(1 - (a /= e) * a) - 1) + b }, easeOutCirc: function(c, a, b, d, e) { return d * Math.sqrt(1 - (a = a / e - 1) * a) + b }, easeInOutCirc: function(c, a, b, d, e) {
    if ((a /= e / 2) < 1) return -d /
2 * (Math.sqrt(1 - a * a) - 1) + b; return d / 2 * (Math.sqrt(1 - (a -= 2) * a) + 1) + b
}, easeInElastic: function(c, a, b, d, e) { c = 1.70158; var g = 0, h = d; if (a == 0) return b; if ((a /= e) == 1) return b + d; g || (g = e * 0.3); if (h < Math.abs(d)) { h = d; c = g / 4 } else c = g / (2 * Math.PI) * Math.asin(d / h); return -(h * Math.pow(2, 10 * (a -= 1)) * Math.sin((a * e - c) * 2 * Math.PI / g)) + b }, easeOutElastic: function(c, a, b, d, e) {
    c = 1.70158; var g = 0, h = d; if (a == 0) return b; if ((a /= e) == 1) return b + d; g || (g = e * 0.3); if (h < Math.abs(d)) { h = d; c = g / 4 } else c = g / (2 * Math.PI) * Math.asin(d / h); return h * Math.pow(2, -10 *
a) * Math.sin((a * e - c) * 2 * Math.PI / g) + d + b
}, easeInOutElastic: function(c, a, b, d, e) { c = 1.70158; var g = 0, h = d; if (a == 0) return b; if ((a /= e / 2) == 2) return b + d; g || (g = e * 0.3 * 1.5); if (h < Math.abs(d)) { h = d; c = g / 4 } else c = g / (2 * Math.PI) * Math.asin(d / h); if (a < 1) return -0.5 * h * Math.pow(2, 10 * (a -= 1)) * Math.sin((a * e - c) * 2 * Math.PI / g) + b; return h * Math.pow(2, -10 * (a -= 1)) * Math.sin((a * e - c) * 2 * Math.PI / g) * 0.5 + d + b }, easeInBack: function(c, a, b, d, e, g) { if (g == j) g = 1.70158; return d * (a /= e) * a * ((g + 1) * a - g) + b }, easeOutBack: function(c, a, b, d, e, g) {
    if (g == j) g = 1.70158;
    return d * ((a = a / e - 1) * a * ((g + 1) * a + g) + 1) + b
}, easeInOutBack: function(c, a, b, d, e, g) { if (g == j) g = 1.70158; if ((a /= e / 2) < 1) return d / 2 * a * a * (((g *= 1.525) + 1) * a - g) + b; return d / 2 * ((a -= 2) * a * (((g *= 1.525) + 1) * a + g) + 2) + b }, easeInBounce: function(c, a, b, d, e) { return d - f.easing.easeOutBounce(c, e - a, 0, d, e) + b }, easeOutBounce: function(c, a, b, d, e) { return (a /= e) < 1 / 2.75 ? d * 7.5625 * a * a + b : a < 2 / 2.75 ? d * (7.5625 * (a -= 1.5 / 2.75) * a + 0.75) + b : a < 2.5 / 2.75 ? d * (7.5625 * (a -= 2.25 / 2.75) * a + 0.9375) + b : d * (7.5625 * (a -= 2.625 / 2.75) * a + 0.984375) + b }, easeInOutBounce: function(c,
a, b, d, e) { if (a < e / 2) return f.easing.easeInBounce(c, a * 2, 0, d, e) * 0.5 + b; return f.easing.easeOutBounce(c, a * 2 - e, 0, d, e) * 0.5 + d * 0.5 + b } 
                        })
                    } (jQuery);
                    (function(b) {
                        b.effects.blind = function(c) {
                            return this.queue(function() {
                                var a = b(this), g = ["position", "top", "left"], f = b.effects.setMode(a, c.options.mode || "hide"), d = c.options.direction || "vertical"; b.effects.save(a, g); a.show(); var e = b.effects.createWrapper(a).css({ overflow: "hidden" }), h = d == "vertical" ? "height" : "width"; d = d == "vertical" ? e.height() : e.width(); f == "show" && e.css(h, 0); var i = {}; i[h] = f == "show" ? d : 0; e.animate(i, c.duration, c.options.easing, function() {
                                    f == "hide" && a.hide(); b.effects.restore(a, g); b.effects.removeWrapper(a);
                                    c.callback && c.callback.apply(a[0], arguments); a.dequeue()
                                })
                            })
                        } 
                    })(jQuery);
                    (function(e) {
                        e.effects.bounce = function(b) {
                            return this.queue(function() {
                                var a = e(this), l = ["position", "top", "left"], h = e.effects.setMode(a, b.options.mode || "effect"), d = b.options.direction || "up", c = b.options.distance || 20, m = b.options.times || 5, i = b.duration || 250; /show|hide/.test(h) && l.push("opacity"); e.effects.save(a, l); a.show(); e.effects.createWrapper(a); var f = d == "up" || d == "down" ? "top" : "left"; d = d == "up" || d == "left" ? "pos" : "neg"; c = b.options.distance || (f == "top" ? a.outerHeight({ margin: true }) / 3 : a.outerWidth({ margin: true }) /
3); if (h == "show") a.css("opacity", 0).css(f, d == "pos" ? -c : c); if (h == "hide") c /= m * 2; h != "hide" && m--; if (h == "show") { var g = { opacity: 1 }; g[f] = (d == "pos" ? "+=" : "-=") + c; a.animate(g, i / 2, b.options.easing); c /= 2; m-- } for (g = 0; g < m; g++) { var j = {}, k = {}; j[f] = (d == "pos" ? "-=" : "+=") + c; k[f] = (d == "pos" ? "+=" : "-=") + c; a.animate(j, i / 2, b.options.easing).animate(k, i / 2, b.options.easing); c = h == "hide" ? c * 2 : c / 2 } if (h == "hide") {
                                    g = { opacity: 0 }; g[f] = (d == "pos" ? "-=" : "+=") + c; a.animate(g, i / 2, b.options.easing, function() {
                                        a.hide(); e.effects.restore(a, l); e.effects.removeWrapper(a);
                                        b.callback && b.callback.apply(this, arguments)
                                    })
                                } else { j = {}; k = {}; j[f] = (d == "pos" ? "-=" : "+=") + c; k[f] = (d == "pos" ? "+=" : "-=") + c; a.animate(j, i / 2, b.options.easing).animate(k, i / 2, b.options.easing, function() { e.effects.restore(a, l); e.effects.removeWrapper(a); b.callback && b.callback.apply(this, arguments) }) } a.queue("fx", function() { a.dequeue() }); a.dequeue()
                            })
                        } 
                    })(jQuery);
                    (function(b) {
                        b.effects.clip = function(e) {
                            return this.queue(function() {
                                var a = b(this), i = ["position", "top", "left", "height", "width"], f = b.effects.setMode(a, e.options.mode || "hide"), c = e.options.direction || "vertical"; b.effects.save(a, i); a.show(); var d = b.effects.createWrapper(a).css({ overflow: "hidden" }); d = a[0].tagName == "IMG" ? d : a; var g = { size: c == "vertical" ? "height" : "width", position: c == "vertical" ? "top" : "left" }; c = c == "vertical" ? d.height() : d.width(); if (f == "show") { d.css(g.size, 0); d.css(g.position, c / 2) } var h = {}; h[g.size] =
f == "show" ? c : 0; h[g.position] = f == "show" ? 0 : c / 2; d.animate(h, { queue: false, duration: e.duration, easing: e.options.easing, complete: function() { f == "hide" && a.hide(); b.effects.restore(a, i); b.effects.removeWrapper(a); e.callback && e.callback.apply(a[0], arguments); a.dequeue() } })
                            })
                        } 
                    })(jQuery);
                    (function(c) {
                        c.effects.drop = function(d) {
                            return this.queue(function() {
                                var a = c(this), h = ["position", "top", "left", "opacity"], e = c.effects.setMode(a, d.options.mode || "hide"), b = d.options.direction || "left"; c.effects.save(a, h); a.show(); c.effects.createWrapper(a); var f = b == "up" || b == "down" ? "top" : "left"; b = b == "up" || b == "left" ? "pos" : "neg"; var g = d.options.distance || (f == "top" ? a.outerHeight({ margin: true }) / 2 : a.outerWidth({ margin: true }) / 2); if (e == "show") a.css("opacity", 0).css(f, b == "pos" ? -g : g); var i = { opacity: e == "show" ? 1 :
0
                                }; i[f] = (e == "show" ? b == "pos" ? "+=" : "-=" : b == "pos" ? "-=" : "+=") + g; a.animate(i, { queue: false, duration: d.duration, easing: d.options.easing, complete: function() { e == "hide" && a.hide(); c.effects.restore(a, h); c.effects.removeWrapper(a); d.callback && d.callback.apply(this, arguments); a.dequeue() } })
                            })
                        } 
                    })(jQuery);
                    (function(j) {
                        j.effects.explode = function(a) {
                            return this.queue(function() {
                                var c = a.options.pieces ? Math.round(Math.sqrt(a.options.pieces)) : 3, d = a.options.pieces ? Math.round(Math.sqrt(a.options.pieces)) : 3; a.options.mode = a.options.mode == "toggle" ? j(this).is(":visible") ? "hide" : "show" : a.options.mode; var b = j(this).show().css("visibility", "hidden"), g = b.offset(); g.top -= parseInt(b.css("marginTop"), 10) || 0; g.left -= parseInt(b.css("marginLeft"), 10) || 0; for (var h = b.outerWidth(true), i = b.outerHeight(true), e = 0; e < c; e++) for (var f =
0; f < d; f++) b.clone().appendTo("body").wrap("<div></div>").css({ position: "absolute", visibility: "visible", left: -f * (h / d), top: -e * (i / c) }).parent().addClass("ui-effects-explode").css({ position: "absolute", overflow: "hidden", width: h / d, height: i / c, left: g.left + f * (h / d) + (a.options.mode == "show" ? (f - Math.floor(d / 2)) * (h / d) : 0), top: g.top + e * (i / c) + (a.options.mode == "show" ? (e - Math.floor(c / 2)) * (i / c) : 0), opacity: a.options.mode == "show" ? 0 : 1 }).animate({ left: g.left + f * (h / d) + (a.options.mode == "show" ? 0 : (f - Math.floor(d / 2)) * (h / d)), top: g.top +
e * (i / c) + (a.options.mode == "show" ? 0 : (e - Math.floor(c / 2)) * (i / c)), opacity: a.options.mode == "show" ? 1 : 0
}, a.duration || 500); setTimeout(function() { a.options.mode == "show" ? b.css({ visibility: "visible" }) : b.css({ visibility: "visible" }).hide(); a.callback && a.callback.apply(b[0]); b.dequeue(); j("div.ui-effects-explode").remove() }, a.duration || 500)
                            })
                        } 
                    })(jQuery);
                    (function(b) { b.effects.fade = function(a) { return this.queue(function() { var c = b(this), d = b.effects.setMode(c, a.options.mode || "hide"); c.animate({ opacity: d }, { queue: false, duration: a.duration, easing: a.options.easing, complete: function() { a.callback && a.callback.apply(this, arguments); c.dequeue() } }) }) } })(jQuery);
                    (function(c) {
                        c.effects.fold = function(a) {
                            return this.queue(function() {
                                var b = c(this), j = ["position", "top", "left"], d = c.effects.setMode(b, a.options.mode || "hide"), g = a.options.size || 15, h = !!a.options.horizFirst, k = a.duration ? a.duration / 2 : c.fx.speeds._default / 2; c.effects.save(b, j); b.show(); var e = c.effects.createWrapper(b).css({ overflow: "hidden" }), f = d == "show" != h, l = f ? ["width", "height"] : ["height", "width"]; f = f ? [e.width(), e.height()] : [e.height(), e.width()]; var i = /([0-9]+)%/.exec(g); if (i) g = parseInt(i[1], 10) / 100 *
f[d == "hide" ? 0 : 1]; if (d == "show") e.css(h ? { height: 0, width: g} : { height: g, width: 0 }); h = {}; i = {}; h[l[0]] = d == "show" ? f[0] : g; i[l[1]] = d == "show" ? f[1] : 0; e.animate(h, k, a.options.easing).animate(i, k, a.options.easing, function() { d == "hide" && b.hide(); c.effects.restore(b, j); c.effects.removeWrapper(b); a.callback && a.callback.apply(b[0], arguments); b.dequeue() })
                            })
                        } 
                    })(jQuery);
                    (function(b) {
                        b.effects.highlight = function(c) {
                            return this.queue(function() {
                                var a = b(this), e = ["backgroundImage", "backgroundColor", "opacity"], d = b.effects.setMode(a, c.options.mode || "show"), f = { backgroundColor: a.css("backgroundColor") }; if (d == "hide") f.opacity = 0; b.effects.save(a, e); a.show().css({ backgroundImage: "none", backgroundColor: c.options.color || "#ffff99" }).animate(f, { queue: false, duration: c.duration, easing: c.options.easing, complete: function() {
                                    d == "hide" && a.hide(); b.effects.restore(a, e); d == "show" && !b.support.opacity &&
this.style.removeAttribute("filter"); c.callback && c.callback.apply(this, arguments); a.dequeue()
                                } 
                                })
                            })
                        } 
                    })(jQuery);
                    (function(d) {
                        d.effects.pulsate = function(a) {
                            return this.queue(function() {
                                var b = d(this), c = d.effects.setMode(b, a.options.mode || "show"); times = (a.options.times || 5) * 2 - 1; duration = a.duration ? a.duration / 2 : d.fx.speeds._default / 2; isVisible = b.is(":visible"); animateTo = 0; if (!isVisible) { b.css("opacity", 0).show(); animateTo = 1 } if (c == "hide" && isVisible || c == "show" && !isVisible) times--; for (c = 0; c < times; c++) { b.animate({ opacity: animateTo }, duration, a.options.easing); animateTo = (animateTo + 1) % 2 } b.animate({ opacity: animateTo }, duration,
a.options.easing, function() { animateTo == 0 && b.hide(); a.callback && a.callback.apply(this, arguments) }); b.queue("fx", function() { b.dequeue() }).dequeue()
                            })
                        } 
                    })(jQuery);
                    (function(c) {
                        c.effects.puff = function(b) { return this.queue(function() { var a = c(this), e = c.effects.setMode(a, b.options.mode || "hide"), g = parseInt(b.options.percent, 10) || 150, h = g / 100, i = { height: a.height(), width: a.width() }; c.extend(b.options, { fade: true, mode: e, percent: e == "hide" ? g : 100, from: e == "hide" ? i : { height: i.height * h, width: i.width * h} }); a.effect("scale", b.options, b.duration, b.callback); a.dequeue() }) }; c.effects.scale = function(b) {
                            return this.queue(function() {
                                var a = c(this), e = c.extend(true, {}, b.options), g = c.effects.setMode(a,
b.options.mode || "effect"), h = parseInt(b.options.percent, 10) || (parseInt(b.options.percent, 10) == 0 ? 0 : g == "hide" ? 0 : 100), i = b.options.direction || "both", f = b.options.origin; if (g != "effect") { e.origin = f || ["middle", "center"]; e.restore = true } f = { height: a.height(), width: a.width() }; a.from = b.options.from || (g == "show" ? { height: 0, width: 0} : f); h = { y: i != "horizontal" ? h / 100 : 1, x: i != "vertical" ? h / 100 : 1 }; a.to = { height: f.height * h.y, width: f.width * h.x }; if (b.options.fade) {
                                    if (g == "show") { a.from.opacity = 0; a.to.opacity = 1 } if (g == "hide") {
                                        a.from.opacity =
1; a.to.opacity = 0
                                    } 
                                } e.from = a.from; e.to = a.to; e.mode = g; a.effect("size", e, b.duration, b.callback); a.dequeue()
                            })
                        }; c.effects.size = function(b) {
                            return this.queue(function() {
                                var a = c(this), e = ["position", "top", "left", "width", "height", "overflow", "opacity"], g = ["position", "top", "left", "overflow", "opacity"], h = ["width", "height", "overflow"], i = ["fontSize"], f = ["borderTopWidth", "borderBottomWidth", "paddingTop", "paddingBottom"], k = ["borderLeftWidth", "borderRightWidth", "paddingLeft", "paddingRight"], p = c.effects.setMode(a,
b.options.mode || "effect"), n = b.options.restore || false, m = b.options.scale || "both", l = b.options.origin, j = { height: a.height(), width: a.width() }; a.from = b.options.from || j; a.to = b.options.to || j; if (l) { l = c.effects.getBaseline(l, j); a.from.top = (j.height - a.from.height) * l.y; a.from.left = (j.width - a.from.width) * l.x; a.to.top = (j.height - a.to.height) * l.y; a.to.left = (j.width - a.to.width) * l.x } var d = { from: { y: a.from.height / j.height, x: a.from.width / j.width }, to: { y: a.to.height / j.height, x: a.to.width / j.width} }; if (m == "box" || m == "both") {
                                    if (d.from.y !=
d.to.y) { e = e.concat(f); a.from = c.effects.setTransition(a, f, d.from.y, a.from); a.to = c.effects.setTransition(a, f, d.to.y, a.to) } if (d.from.x != d.to.x) { e = e.concat(k); a.from = c.effects.setTransition(a, k, d.from.x, a.from); a.to = c.effects.setTransition(a, k, d.to.x, a.to) } 
                                } if (m == "content" || m == "both") if (d.from.y != d.to.y) { e = e.concat(i); a.from = c.effects.setTransition(a, i, d.from.y, a.from); a.to = c.effects.setTransition(a, i, d.to.y, a.to) } c.effects.save(a, n ? e : g); a.show(); c.effects.createWrapper(a); a.css("overflow", "hidden").css(a.from);
                                if (m == "content" || m == "both") {
                                    f = f.concat(["marginTop", "marginBottom"]).concat(i); k = k.concat(["marginLeft", "marginRight"]); h = e.concat(f).concat(k); a.find("*[width]").each(function() {
                                        child = c(this); n && c.effects.save(child, h); var o = { height: child.height(), width: child.width() }; child.from = { height: o.height * d.from.y, width: o.width * d.from.x }; child.to = { height: o.height * d.to.y, width: o.width * d.to.x }; if (d.from.y != d.to.y) {
                                            child.from = c.effects.setTransition(child, f, d.from.y, child.from); child.to = c.effects.setTransition(child,
f, d.to.y, child.to)
                                        } if (d.from.x != d.to.x) { child.from = c.effects.setTransition(child, k, d.from.x, child.from); child.to = c.effects.setTransition(child, k, d.to.x, child.to) } child.css(child.from); child.animate(child.to, b.duration, b.options.easing, function() { n && c.effects.restore(child, h) })
                                    })
                                } a.animate(a.to, { queue: false, duration: b.duration, easing: b.options.easing, complete: function() {
                                    a.to.opacity === 0 && a.css("opacity", a.from.opacity); p == "hide" && a.hide(); c.effects.restore(a, n ? e : g); c.effects.removeWrapper(a); b.callback &&
b.callback.apply(this, arguments); a.dequeue()
                                } 
                                })
                            })
                        } 
                    })(jQuery);
                    (function(d) {
                        d.effects.shake = function(a) {
                            return this.queue(function() {
                                var b = d(this), j = ["position", "top", "left"]; d.effects.setMode(b, a.options.mode || "effect"); var c = a.options.direction || "left", e = a.options.distance || 20, l = a.options.times || 3, f = a.duration || a.options.duration || 140; d.effects.save(b, j); b.show(); d.effects.createWrapper(b); var g = c == "up" || c == "down" ? "top" : "left", h = c == "up" || c == "left" ? "pos" : "neg"; c = {}; var i = {}, k = {}; c[g] = (h == "pos" ? "-=" : "+=") + e; i[g] = (h == "pos" ? "+=" : "-=") + e * 2; k[g] = (h == "pos" ? "-=" : "+=") +
e * 2; b.animate(c, f, a.options.easing); for (e = 1; e < l; e++) b.animate(i, f, a.options.easing).animate(k, f, a.options.easing); b.animate(i, f, a.options.easing).animate(c, f / 2, a.options.easing, function() { d.effects.restore(b, j); d.effects.removeWrapper(b); a.callback && a.callback.apply(this, arguments) }); b.queue("fx", function() { b.dequeue() }); b.dequeue()
                            })
                        } 
                    })(jQuery);
                    (function(c) {
                        c.effects.slide = function(d) {
                            return this.queue(function() {
                                var a = c(this), h = ["position", "top", "left"], e = c.effects.setMode(a, d.options.mode || "show"), b = d.options.direction || "left"; c.effects.save(a, h); a.show(); c.effects.createWrapper(a).css({ overflow: "hidden" }); var f = b == "up" || b == "down" ? "top" : "left"; b = b == "up" || b == "left" ? "pos" : "neg"; var g = d.options.distance || (f == "top" ? a.outerHeight({ margin: true }) : a.outerWidth({ margin: true })); if (e == "show") a.css(f, b == "pos" ? -g : g); var i = {}; i[f] = (e == "show" ? b == "pos" ?
"+=" : "-=" : b == "pos" ? "-=" : "+=") + g; a.animate(i, { queue: false, duration: d.duration, easing: d.options.easing, complete: function() { e == "hide" && a.hide(); c.effects.restore(a, h); c.effects.removeWrapper(a); d.callback && d.callback.apply(this, arguments); a.dequeue() } })
                            })
                        } 
                    })(jQuery);
                    (function(e) {
                        e.effects.transfer = function(a) {
                            return this.queue(function() {
                                var b = e(this), c = e(a.options.to), d = c.offset(); c = { top: d.top, left: d.left, height: c.innerHeight(), width: c.innerWidth() }; d = b.offset(); var f = e('<div class="ui-effects-transfer"></div>').appendTo(document.body).addClass(a.options.className).css({ top: d.top, left: d.left, height: b.innerHeight(), width: b.innerWidth(), position: "absolute" }).animate(c, a.duration, a.options.easing, function() {
                                    f.remove(); a.callback && a.callback.apply(b[0], arguments);
                                    b.dequeue()
                                })
                            })
                        } 
                    })(jQuery);
                    (function(c) {
                        c.widget("ui.accordion", { options: { active: 0, animated: "slide", autoHeight: true, clearStyle: false, collapsible: false, event: "click", fillSpace: false, header: "> li > :first-child,> :not(li):even", icons: { header: "ui-icon-triangle-1-e", headerSelected: "ui-icon-triangle-1-s" }, navigation: false, navigationFilter: function() { return this.href.toLowerCase() === location.href.toLowerCase() } }, _create: function() {
                            var a = this, b = a.options; a.running = 0; a.element.addClass("ui-accordion ui-widget ui-helper-reset").children("li").addClass("ui-accordion-li-fix");
                            a.headers = a.element.find(b.header).addClass("ui-accordion-header ui-helper-reset ui-state-default ui-corner-all").bind("mouseenter.accordion", function() { b.disabled || c(this).addClass("ui-state-hover") }).bind("mouseleave.accordion", function() { b.disabled || c(this).removeClass("ui-state-hover") }).bind("focus.accordion", function() { b.disabled || c(this).addClass("ui-state-focus") }).bind("blur.accordion", function() { b.disabled || c(this).removeClass("ui-state-focus") }); a.headers.next().addClass("ui-accordion-content ui-helper-reset ui-widget-content ui-corner-bottom");
                            if (b.navigation) { var d = a.element.find("a").filter(b.navigationFilter).eq(0); if (d.length) { var f = d.closest(".ui-accordion-header"); a.active = f.length ? f : d.closest(".ui-accordion-content").prev() } } a.active = a._findActive(a.active || b.active).addClass("ui-state-default ui-state-active").toggleClass("ui-corner-all ui-corner-top"); a.active.next().addClass("ui-accordion-content-active"); a._createIcons(); a.resize(); a.element.attr("role", "tablist"); a.headers.attr("role", "tab").bind("keydown.accordion", function(g) { return a._keydown(g) }).next().attr("role",
"tabpanel"); a.headers.not(a.active || "").attr({ "aria-expanded": "false", tabIndex: -1 }).next().hide(); a.active.length ? a.active.attr({ "aria-expanded": "true", tabIndex: 0 }) : a.headers.eq(0).attr("tabIndex", 0); c.browser.safari || a.headers.find("a").attr("tabIndex", -1); b.event && a.headers.bind(b.event.split(" ").join(".accordion ") + ".accordion", function(g) { a._clickHandler.call(a, g, this); g.preventDefault() })
                        }, _createIcons: function() {
                            var a = this.options; if (a.icons) {
                                c("<span></span>").addClass("ui-icon " + a.icons.header).prependTo(this.headers);
                                this.active.children(".ui-icon").toggleClass(a.icons.header).toggleClass(a.icons.headerSelected); this.element.addClass("ui-accordion-icons")
                            } 
                        }, _destroyIcons: function() { this.headers.children(".ui-icon").remove(); this.element.removeClass("ui-accordion-icons") }, destroy: function() {
                            var a = this.options; this.element.removeClass("ui-accordion ui-widget ui-helper-reset").removeAttr("role"); this.headers.unbind(".accordion").removeClass("ui-accordion-header ui-accordion-disabled ui-helper-reset ui-state-default ui-corner-all ui-state-active ui-state-disabled ui-corner-top").removeAttr("role").removeAttr("aria-expanded").removeAttr("tabIndex");
                            this.headers.find("a").removeAttr("tabIndex"); this._destroyIcons(); var b = this.headers.next().css("display", "").removeAttr("role").removeClass("ui-helper-reset ui-widget-content ui-corner-bottom ui-accordion-content ui-accordion-content-active ui-accordion-disabled ui-state-disabled"); if (a.autoHeight || a.fillHeight) b.css("height", ""); return c.Widget.prototype.destroy.call(this)
                        }, _setOption: function(a, b) {
                            c.Widget.prototype._setOption.apply(this, arguments); a == "active" && this.activate(b); if (a == "icons") {
                                this._destroyIcons();
                                b && this._createIcons()
                            } if (a == "disabled") this.headers.add(this.headers.next())[b ? "addClass" : "removeClass"]("ui-accordion-disabled ui-state-disabled")
                        }, _keydown: function(a) {
                            if (!(this.options.disabled || a.altKey || a.ctrlKey)) {
                                var b = c.ui.keyCode, d = this.headers.length, f = this.headers.index(a.target), g = false; switch (a.keyCode) {
                                    case b.RIGHT: case b.DOWN: g = this.headers[(f + 1) % d]; break; case b.LEFT: case b.UP: g = this.headers[(f - 1 + d) % d]; break; case b.SPACE: case b.ENTER: this._clickHandler({ target: a.target }, a.target);
                                        a.preventDefault()
                                } if (g) { c(a.target).attr("tabIndex", -1); c(g).attr("tabIndex", 0); g.focus(); return false } return true
                            } 
                        }, resize: function() {
                            var a = this.options, b; if (a.fillSpace) {
                                if (c.browser.msie) { var d = this.element.parent().css("overflow"); this.element.parent().css("overflow", "hidden") } b = this.element.parent().height(); c.browser.msie && this.element.parent().css("overflow", d); this.headers.each(function() { b -= c(this).outerHeight(true) }); this.headers.next().each(function() {
                                    c(this).height(Math.max(0, b - c(this).innerHeight() +
c(this).height()))
                                }).css("overflow", "auto")
                            } else if (a.autoHeight) { b = 0; this.headers.next().each(function() { b = Math.max(b, c(this).height("").height()) }).height(b) } return this
                        }, activate: function(a) { this.options.active = a; a = this._findActive(a)[0]; this._clickHandler({ target: a }, a); return this }, _findActive: function(a) { return a ? typeof a === "number" ? this.headers.filter(":eq(" + a + ")") : this.headers.not(this.headers.not(a)) : a === false ? c([]) : this.headers.filter(":eq(0)") }, _clickHandler: function(a, b) {
                            var d = this.options;
                            if (!d.disabled) if (a.target) {
                                a = c(a.currentTarget || b); b = a[0] === this.active[0]; d.active = d.collapsible && b ? false : this.headers.index(a); if (!(this.running || !d.collapsible && b)) {
                                    this.active.removeClass("ui-state-active ui-corner-top").addClass("ui-state-default ui-corner-all").children(".ui-icon").removeClass(d.icons.headerSelected).addClass(d.icons.header); if (!b) {
                                        a.removeClass("ui-state-default ui-corner-all").addClass("ui-state-active ui-corner-top").children(".ui-icon").removeClass(d.icons.header).addClass(d.icons.headerSelected);
                                        a.next().addClass("ui-accordion-content-active")
                                    } h = a.next(); f = this.active.next(); g = { options: d, newHeader: b && d.collapsible ? c([]) : a, oldHeader: this.active, newContent: b && d.collapsible ? c([]) : h, oldContent: f }; d = this.headers.index(this.active[0]) > this.headers.index(a[0]); this.active = b ? c([]) : a; this._toggle(h, f, g, b, d)
                                } 
                            } else if (d.collapsible) {
                                this.active.removeClass("ui-state-active ui-corner-top").addClass("ui-state-default ui-corner-all").children(".ui-icon").removeClass(d.icons.headerSelected).addClass(d.icons.header);
                                this.active.next().addClass("ui-accordion-content-active"); var f = this.active.next(), g = { options: d, newHeader: c([]), oldHeader: d.active, newContent: c([]), oldContent: f }, h = this.active = c([]); this._toggle(h, f, g)
                            } 
                        }, _toggle: function(a, b, d, f, g) {
                            var h = this, e = h.options; h.toShow = a; h.toHide = b; h.data = d; var j = function() { if (h) return h._completed.apply(h, arguments) }; h._trigger("changestart", null, h.data); h.running = b.size() === 0 ? a.size() : b.size(); if (e.animated) {
                                d = {}; d = e.collapsible && f ? { toShow: c([]), toHide: b, complete: j,
                                    down: g, autoHeight: e.autoHeight || e.fillSpace} : { toShow: a, toHide: b, complete: j, down: g, autoHeight: e.autoHeight || e.fillSpace }; if (!e.proxied) e.proxied = e.animated; if (!e.proxiedDuration) e.proxiedDuration = e.duration; e.animated = c.isFunction(e.proxied) ? e.proxied(d) : e.proxied; e.duration = c.isFunction(e.proxiedDuration) ? e.proxiedDuration(d) : e.proxiedDuration; f = c.ui.accordion.animations; var i = e.duration, k = e.animated; if (k && !f[k] && !c.easing[k]) k = "slide"; f[k] || (f[k] = function(l) { this.slide(l, { easing: k, duration: i || 700 }) });
                                    f[k](d)
                                } else { if (e.collapsible && f) a.toggle(); else { b.hide(); a.show() } j(true) } b.prev().attr({ "aria-expanded": "false", tabIndex: -1 }).blur(); a.prev().attr({ "aria-expanded": "true", tabIndex: 0 }).focus()
                            }, _completed: function(a) { this.running = a ? 0 : --this.running; if (!this.running) { this.options.clearStyle && this.toShow.add(this.toHide).css({ height: "", overflow: "" }); this.toHide.removeClass("ui-accordion-content-active"); this._trigger("change", null, this.data) } } 
                        }); c.extend(c.ui.accordion, { version: "1.8.5", animations: { slide: function(a,
b) {
                            a = c.extend({ easing: "swing", duration: 300 }, a, b); if (a.toHide.size()) if (a.toShow.size()) {
                                var d = a.toShow.css("overflow"), f = 0, g = {}, h = {}, e; b = a.toShow; e = b[0].style.width; b.width(parseInt(b.parent().width(), 10) - parseInt(b.css("paddingLeft"), 10) - parseInt(b.css("paddingRight"), 10) - (parseInt(b.css("borderLeftWidth"), 10) || 0) - (parseInt(b.css("borderRightWidth"), 10) || 0)); c.each(["height", "paddingTop", "paddingBottom"], function(j, i) {
                                    h[i] = "hide"; j = ("" + c.css(a.toShow[0], i)).match(/^([\d+-.]+)(.*)$/); g[i] = { value: j[1],
                                        unit: j[2] || "px"}
                                    }); a.toShow.css({ height: 0, overflow: "hidden" }).show(); a.toHide.filter(":hidden").each(a.complete).end().filter(":visible").animate(h, { step: function(j, i) { if (i.prop == "height") f = i.end - i.start === 0 ? 0 : (i.now - i.start) / (i.end - i.start); a.toShow[0].style[i.prop] = f * g[i.prop].value + g[i.prop].unit }, duration: a.duration, easing: a.easing, complete: function() { a.autoHeight || a.toShow.css("height", ""); a.toShow.css({ width: e, overflow: d }); a.complete() } })
                                } else a.toHide.animate({ height: "hide", paddingTop: "hide",
                                    paddingBottom: "hide"
                                }, a); else a.toShow.animate({ height: "show", paddingTop: "show", paddingBottom: "show" }, a)
                            }, bounceslide: function(a) { this.slide(a, { easing: a.down ? "easeOutBounce" : "swing", duration: a.down ? 1E3 : 200 }) } }
                        })
                    })(jQuery);
                    (function(e) {
                        e.widget("ui.autocomplete", { options: { appendTo: "body", delay: 300, minLength: 1, position: { my: "left top", at: "left bottom", collision: "none" }, source: null }, _create: function() {
                            var a = this, b = this.element[0].ownerDocument; this.element.addClass("ui-autocomplete-input").attr("autocomplete", "off").attr({ role: "textbox", "aria-autocomplete": "list", "aria-haspopup": "true" }).bind("keydown.autocomplete", function(c) {
                                if (!a.options.disabled) {
                                    var d = e.ui.keyCode; switch (c.keyCode) {
                                        case d.PAGE_UP: a._move("previousPage",
c); break; case d.PAGE_DOWN: a._move("nextPage", c); break; case d.UP: a._move("previous", c); c.preventDefault(); break; case d.DOWN: a._move("next", c); c.preventDefault(); break; case d.ENTER: case d.NUMPAD_ENTER: a.menu.element.is(":visible") && c.preventDefault(); case d.TAB: if (!a.menu.active) return; a.menu.select(c); break; case d.ESCAPE: a.element.val(a.term); a.close(c); break; default: clearTimeout(a.searching); a.searching = setTimeout(function() { if (a.term != a.element.val()) { a.selectedItem = null; a.search(null, c) } }, a.options.delay);
                                            break
                                    } 
                                } 
                            }).bind("focus.autocomplete", function() { if (!a.options.disabled) { a.selectedItem = null; a.previous = a.element.val() } }).bind("blur.autocomplete", function(c) { if (!a.options.disabled) { clearTimeout(a.searching); a.closing = setTimeout(function() { a.close(c); a._change(c) }, 150) } }); this._initSource(); this.response = function() { return a._response.apply(a, arguments) }; this.menu = e("<ul></ul>").addClass("ui-autocomplete").appendTo(e(this.options.appendTo || "body", b)[0]).mousedown(function(c) {
                                var d = a.menu.element[0];
                                c.target === d && setTimeout(function() { e(document).one("mousedown", function(f) { f.target !== a.element[0] && f.target !== d && !e.ui.contains(d, f.target) && a.close() }) }, 1); setTimeout(function() { clearTimeout(a.closing) }, 13)
                            }).menu({ focus: function(c, d) { d = d.item.data("item.autocomplete"); false !== a._trigger("focus", null, { item: d }) && /^key/.test(c.originalEvent.type) && a.element.val(d.value) }, selected: function(c, d) {
                                d = d.item.data("item.autocomplete"); var f = a.previous; if (a.element[0] !== b.activeElement) {
                                    a.element.focus();
                                    a.previous = f
                                } if (false !== a._trigger("select", c, { item: d })) { a.term = d.value; a.element.val(d.value) } a.close(c); a.selectedItem = d
                            }, blur: function() { a.menu.element.is(":visible") && a.element.val() !== a.term && a.element.val(a.term) } 
                            }).zIndex(this.element.zIndex() + 1).css({ top: 0, left: 0 }).hide().data("menu"); e.fn.bgiframe && this.menu.element.bgiframe()
                        }, destroy: function() {
                            this.element.removeClass("ui-autocomplete-input").removeAttr("autocomplete").removeAttr("role").removeAttr("aria-autocomplete").removeAttr("aria-haspopup");
                            this.menu.element.remove(); e.Widget.prototype.destroy.call(this)
                        }, _setOption: function(a, b) { e.Widget.prototype._setOption.apply(this, arguments); a === "source" && this._initSource(); if (a === "appendTo") this.menu.element.appendTo(e(b || "body", this.element[0].ownerDocument)[0]) }, _initSource: function() {
                            var a = this, b, c; if (e.isArray(this.options.source)) { b = this.options.source; this.source = function(d, f) { f(e.ui.autocomplete.filter(b, d.term)) } } else if (typeof this.options.source === "string") {
                                c = this.options.source; this.source =
function(d, f) { a.xhr && a.xhr.abort(); a.xhr = e.getJSON(c, d, function(g, i, h) { h === a.xhr && f(g); a.xhr = null }) } 
                            } else this.source = this.options.source
                        }, search: function(a, b) { a = a != null ? a : this.element.val(); this.term = this.element.val(); if (a.length < this.options.minLength) return this.close(b); clearTimeout(this.closing); if (this._trigger("search") !== false) return this._search(a) }, _search: function(a) { this.element.addClass("ui-autocomplete-loading"); this.source({ term: a }, this.response) }, _response: function(a) {
                            if (a.length) {
                                a =
this._normalize(a); this._suggest(a); this._trigger("open")
                            } else this.close(); this.element.removeClass("ui-autocomplete-loading")
                        }, close: function(a) { clearTimeout(this.closing); if (this.menu.element.is(":visible")) { this._trigger("close", a); this.menu.element.hide(); this.menu.deactivate() } }, _change: function(a) { this.previous !== this.element.val() && this._trigger("change", a, { item: this.selectedItem }) }, _normalize: function(a) {
                            if (a.length && a[0].label && a[0].value) return a; return e.map(a, function(b) {
                                if (typeof b ===
"string") return { label: b, value: b }; return e.extend({ label: b.label || b.value, value: b.value || b.label }, b)
                            })
                        }, _suggest: function(a) { var b = this.menu.element.empty().zIndex(this.element.zIndex() + 1), c; this._renderMenu(b, a); this.menu.deactivate(); this.menu.refresh(); this.menu.element.show().position(e.extend({ of: this.element }, this.options.position)); a = b.width("").outerWidth(); c = this.element.outerWidth(); b.outerWidth(Math.max(a, c)) }, _renderMenu: function(a, b) { var c = this; e.each(b, function(d, f) { c._renderItem(a, f) }) },
                            _renderItem: function(a, b) { return e("<li></li>").data("item.autocomplete", b).append(e("<a></a>").text(b.label)).appendTo(a) }, _move: function(a, b) { if (this.menu.element.is(":visible")) if (this.menu.first() && /^previous/.test(a) || this.menu.last() && /^next/.test(a)) { this.element.val(this.term); this.menu.deactivate() } else this.menu[a](b); else this.search(null, b) }, widget: function() { return this.menu.element } 
                        }); e.extend(e.ui.autocomplete, { escapeRegex: function(a) { return a.replace(/[-[\]{}()*+?.,\\^$|#\s]/g, "\\$&") },
                            filter: function(a, b) { var c = new RegExp(e.ui.autocomplete.escapeRegex(b), "i"); return e.grep(a, function(d) { return c.test(d.label || d.value || d) }) } 
                        })
                    })(jQuery);
                    (function(e) {
                        e.widget("ui.menu", { _create: function() { var a = this; this.element.addClass("ui-menu ui-widget ui-widget-content ui-corner-all").attr({ role: "listbox", "aria-activedescendant": "ui-active-menuitem" }).click(function(b) { if (e(b.target).closest(".ui-menu-item a").length) { b.preventDefault(); a.select(b) } }); this.refresh() }, refresh: function() {
                            var a = this; this.element.children("li:not(.ui-menu-item):has(a)").addClass("ui-menu-item").attr("role", "menuitem").children("a").addClass("ui-corner-all").attr("tabindex",
-1).mouseenter(function(b) { a.activate(b, e(this).parent()) }).mouseleave(function() { a.deactivate() })
                        }, activate: function(a, b) { this.deactivate(); if (this.hasScroll()) { var c = b.offset().top - this.element.offset().top, d = this.element.attr("scrollTop"), f = this.element.height(); if (c < 0) this.element.attr("scrollTop", d + c); else c >= f && this.element.attr("scrollTop", d + c - f + b.height()) } this.active = b.eq(0).children("a").addClass("ui-state-hover").attr("id", "ui-active-menuitem").end(); this._trigger("focus", a, { item: b }) },
                            deactivate: function() { if (this.active) { this.active.children("a").removeClass("ui-state-hover").removeAttr("id"); this._trigger("blur"); this.active = null } }, next: function(a) { this.move("next", ".ui-menu-item:first", a) }, previous: function(a) { this.move("prev", ".ui-menu-item:last", a) }, first: function() { return this.active && !this.active.prevAll(".ui-menu-item").length }, last: function() { return this.active && !this.active.nextAll(".ui-menu-item").length }, move: function(a, b, c) {
                                if (this.active) {
                                    a = this.active[a + "All"](".ui-menu-item").eq(0);
                                    a.length ? this.activate(c, a) : this.activate(c, this.element.children(b))
                                } else this.activate(c, this.element.children(b))
                            }, nextPage: function(a) {
                                if (this.hasScroll()) if (!this.active || this.last()) this.activate(a, this.element.children(":first")); else { var b = this.active.offset().top, c = this.element.height(), d = this.element.children("li").filter(function() { var f = e(this).offset().top - b - c + e(this).height(); return f < 10 && f > -10 }); d.length || (d = this.element.children(":last")); this.activate(a, d) } else this.activate(a, this.element.children(!this.active ||
this.last() ? ":first" : ":last"))
                            }, previousPage: function(a) { if (this.hasScroll()) if (!this.active || this.first()) this.activate(a, this.element.children(":last")); else { var b = this.active.offset().top, c = this.element.height(); result = this.element.children("li").filter(function() { var d = e(this).offset().top - b + c - e(this).height(); return d < 10 && d > -10 }); result.length || (result = this.element.children(":first")); this.activate(a, result) } else this.activate(a, this.element.children(!this.active || this.first() ? ":last" : ":first")) },
                            hasScroll: function() { return this.element.height() < this.element.attr("scrollHeight") }, select: function(a) { this._trigger("selected", a, { item: this.active }) } 
                        })
                    })(jQuery);
                    (function(a) {
                        var g, i = function(b) { a(":ui-button", b.target.form).each(function() { var c = a(this).data("button"); setTimeout(function() { c.refresh() }, 1) }) }, h = function(b) { var c = b.name, d = b.form, e = a([]); if (c) e = d ? a(d).find("[name='" + c + "']") : a("[name='" + c + "']", b.ownerDocument).filter(function() { return !this.form }); return e }; a.widget("ui.button", { options: { disabled: null, text: true, label: null, icons: { primary: null, secondary: null} }, _create: function() {
                            this.element.closest("form").unbind("reset.button").bind("reset.button",
i); if (typeof this.options.disabled !== "boolean") this.options.disabled = this.element.attr("disabled"); this._determineButtonType(); this.hasTitle = !!this.buttonElement.attr("title"); var b = this, c = this.options, d = this.type === "checkbox" || this.type === "radio", e = "ui-state-hover" + (!d ? " ui-state-active" : ""); if (c.label === null) c.label = this.buttonElement.html(); if (this.element.is(":disabled")) c.disabled = true; this.buttonElement.addClass("ui-button ui-widget ui-state-default ui-corner-all").attr("role", "button").bind("mouseenter.button",
function() { if (!c.disabled) { a(this).addClass("ui-state-hover"); this === g && a(this).addClass("ui-state-active") } }).bind("mouseleave.button", function() { c.disabled || a(this).removeClass(e) }).bind("focus.button", function() { a(this).addClass("ui-state-focus") }).bind("blur.button", function() { a(this).removeClass("ui-state-focus") }); d && this.element.bind("change.button", function() { b.refresh() }); if (this.type === "checkbox") this.buttonElement.bind("click.button", function() {
    if (c.disabled) return false; a(this).toggleClass("ui-state-active");
    b.buttonElement.attr("aria-pressed", b.element[0].checked)
}); else if (this.type === "radio") this.buttonElement.bind("click.button", function() { if (c.disabled) return false; a(this).addClass("ui-state-active"); b.buttonElement.attr("aria-pressed", true); var f = b.element[0]; h(f).not(f).map(function() { return a(this).button("widget")[0] }).removeClass("ui-state-active").attr("aria-pressed", false) }); else {
                                this.buttonElement.bind("mousedown.button", function() {
                                    if (c.disabled) return false; a(this).addClass("ui-state-active");
                                    g = this; a(document).one("mouseup", function() { g = null })
                                }).bind("mouseup.button", function() { if (c.disabled) return false; a(this).removeClass("ui-state-active") }).bind("keydown.button", function(f) { if (c.disabled) return false; if (f.keyCode == a.ui.keyCode.SPACE || f.keyCode == a.ui.keyCode.ENTER) a(this).addClass("ui-state-active") }).bind("keyup.button", function() { a(this).removeClass("ui-state-active") }); this.buttonElement.is("a") && this.buttonElement.keyup(function(f) { f.keyCode === a.ui.keyCode.SPACE && a(this).click() })
                            } this._setOption("disabled",
c.disabled)
                        }, _determineButtonType: function() {
                            this.type = this.element.is(":checkbox") ? "checkbox" : this.element.is(":radio") ? "radio" : this.element.is("input") ? "input" : "button"; if (this.type === "checkbox" || this.type === "radio") { this.buttonElement = this.element.parents().last().find("label[for=" + this.element.attr("id") + "]"); this.element.addClass("ui-helper-hidden-accessible"); var b = this.element.is(":checked"); b && this.buttonElement.addClass("ui-state-active"); this.buttonElement.attr("aria-pressed", b) } else this.buttonElement =
this.element
                        }, widget: function() { return this.buttonElement }, destroy: function() {
                            this.element.removeClass("ui-helper-hidden-accessible"); this.buttonElement.removeClass("ui-button ui-widget ui-state-default ui-corner-all ui-state-hover ui-state-active  ui-button-icons-only ui-button-icon-only ui-button-text-icons ui-button-text-icon-primary ui-button-text-icon-secondary ui-button-text-only").removeAttr("role").removeAttr("aria-pressed").html(this.buttonElement.find(".ui-button-text").html()); this.hasTitle ||
this.buttonElement.removeAttr("title"); a.Widget.prototype.destroy.call(this)
                        }, _setOption: function(b, c) { a.Widget.prototype._setOption.apply(this, arguments); if (b === "disabled") c ? this.element.attr("disabled", true) : this.element.removeAttr("disabled"); this._resetButton() }, refresh: function() {
                            var b = this.element.is(":disabled"); b !== this.options.disabled && this._setOption("disabled", b); if (this.type === "radio") h(this.element[0]).each(function() {
                                a(this).is(":checked") ? a(this).button("widget").addClass("ui-state-active").attr("aria-pressed",
true) : a(this).button("widget").removeClass("ui-state-active").attr("aria-pressed", false)
                            }); else if (this.type === "checkbox") this.element.is(":checked") ? this.buttonElement.addClass("ui-state-active").attr("aria-pressed", true) : this.buttonElement.removeClass("ui-state-active").attr("aria-pressed", false)
                        }, _resetButton: function() {
                            if (this.type === "input") this.options.label && this.element.val(this.options.label); else {
                                var b = this.buttonElement.removeClass("ui-button-icons-only ui-button-icon-only ui-button-text-icons ui-button-text-icon-primary ui-button-text-icon-secondary ui-button-text-only"),
c = a("<span></span>").addClass("ui-button-text").html(this.options.label).appendTo(b.empty()).text(), d = this.options.icons, e = d.primary && d.secondary; if (d.primary || d.secondary) {
                                    b.addClass("ui-button-text-icon" + (e ? "s" : d.primary ? "-primary" : "-secondary")); d.primary && b.prepend("<span class='ui-button-icon-primary ui-icon " + d.primary + "'></span>"); d.secondary && b.append("<span class='ui-button-icon-secondary ui-icon " + d.secondary + "'></span>"); if (!this.options.text) {
                                        b.addClass(e ? "ui-button-icons-only" : "ui-button-icon-only").removeClass("ui-button-text-icons ui-button-text-icon-primary ui-button-text-icon-secondary");
                                        this.hasTitle || b.attr("title", c)
                                    } 
                                } else b.addClass("ui-button-text-only")
                            } 
                        } 
                        }); a.widget("ui.buttonset", { _create: function() { this.element.addClass("ui-buttonset"); this._init() }, _init: function() { this.refresh() }, _setOption: function(b, c) { b === "disabled" && this.buttons.button("option", b, c); a.Widget.prototype._setOption.apply(this, arguments) }, refresh: function() { this.buttons = this.element.find(":button, :submit, :reset, :checkbox, :radio, a, :data(button)").filter(":ui-button").button("refresh").end().not(":ui-button").button().end().map(function() { return a(this).button("widget")[0] }).removeClass("ui-corner-all ui-corner-left ui-corner-right").filter(":visible").filter(":first").addClass("ui-corner-left").end().filter(":last").addClass("ui-corner-right").end().end().end() },
                            destroy: function() { this.element.removeClass("ui-buttonset"); this.buttons.map(function() { return a(this).button("widget")[0] }).removeClass("ui-corner-left ui-corner-right").end().button("destroy"); a.Widget.prototype.destroy.call(this) } 
                        })
                    })(jQuery);
                    (function(d, G) {
                        function L() {
                            this.debug = false; this._curInst = null; this._keyEvent = false; this._disabledInputs = []; this._inDialog = this._datepickerShowing = false; this._mainDivId = "ui-datepicker-div"; this._inlineClass = "ui-datepicker-inline"; this._appendClass = "ui-datepicker-append"; this._triggerClass = "ui-datepicker-trigger"; this._dialogClass = "ui-datepicker-dialog"; this._disableClass = "ui-datepicker-disabled"; this._unselectableClass = "ui-datepicker-unselectable"; this._currentClass = "ui-datepicker-current-day"; this._dayOverClass =
"ui-datepicker-days-cell-over"; this.regional = []; this.regional[""] = { closeText: "Done", prevText: "Prev", nextText: "Next", currentText: "Today", monthNames: ["January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December"], monthNamesShort: ["Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"], dayNames: ["Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday"], dayNamesShort: ["Sun", "Mon", "Tue", "Wed", "Thu", "Fri", "Sat"], dayNamesMin: ["Su",
"Mo", "Tu", "We", "Th", "Fr", "Sa"], weekHeader: "Wk", dateFormat: "mm/dd/yy", firstDay: 0, isRTL: false, showMonthAfterYear: false, yearSuffix: ""
}; this._defaults = { showOn: "focus", showAnim: "fadeIn", showOptions: {}, defaultDate: null, appendText: "", buttonText: "...", buttonImage: "", buttonImageOnly: false, hideIfNoPrevNext: false, navigationAsDateFormat: false, gotoCurrent: false, changeMonth: false, changeYear: false, yearRange: "c-10:c+10", showOtherMonths: false, selectOtherMonths: false, showWeek: false, calculateWeek: this.iso8601Week, shortYearCutoff: "+10",
    minDate: null, maxDate: null, duration: "fast", beforeShowDay: null, beforeShow: null, onSelect: null, onChangeMonthYear: null, onClose: null, numberOfMonths: 1, showCurrentAtPos: 0, stepMonths: 1, stepBigMonths: 12, altField: "", altFormat: "", constrainInput: true, showButtonPanel: false, autoSize: false
}; d.extend(this._defaults, this.regional[""]); this.dpDiv = d('<div id="' + this._mainDivId + '" class="ui-datepicker ui-widget ui-widget-content ui-helper-clearfix ui-corner-all ui-helper-hidden-accessible"></div>')
                        } function E(a, b) {
                            d.extend(a,
b); for (var c in b) if (b[c] == null || b[c] == G) a[c] = b[c]; return a
                        } d.extend(d.ui, { datepicker: { version: "1.8.5"} }); var y = (new Date).getTime(); d.extend(L.prototype, { markerClassName: "hasDatepicker", log: function() { this.debug && console.log.apply("", arguments) }, _widgetDatepicker: function() { return this.dpDiv }, setDefaults: function(a) { E(this._defaults, a || {}); return this }, _attachDatepicker: function(a, b) {
                            var c = null; for (var e in this._defaults) {
                                var f = a.getAttribute("date:" + e); if (f) {
                                    c = c || {}; try { c[e] = eval(f) } catch (h) {
                                        c[e] =
f
                                    } 
                                } 
                            } e = a.nodeName.toLowerCase(); f = e == "div" || e == "span"; if (!a.id) { this.uuid += 1; a.id = "dp" + this.uuid } var i = this._newInst(d(a), f); i.settings = d.extend({}, b || {}, c || {}); if (e == "input") this._connectDatepicker(a, i); else f && this._inlineDatepicker(a, i)
                        }, _newInst: function(a, b) { return { id: a[0].id.replace(/([^A-Za-z0-9_])/g, "\\\\$1"), input: a, selectedDay: 0, selectedMonth: 0, selectedYear: 0, drawMonth: 0, drawYear: 0, inline: b, dpDiv: !b ? this.dpDiv : d('<div class="' + this._inlineClass + ' ui-datepicker ui-widget ui-widget-content ui-helper-clearfix ui-corner-all"></div>')} },
                            _connectDatepicker: function(a, b) { var c = d(a); b.append = d([]); b.trigger = d([]); if (!c.hasClass(this.markerClassName)) { this._attachments(c, b); c.addClass(this.markerClassName).keydown(this._doKeyDown).keypress(this._doKeyPress).keyup(this._doKeyUp).bind("setData.datepicker", function(e, f, h) { b.settings[f] = h }).bind("getData.datepicker", function(e, f) { return this._get(b, f) }); this._autoSize(b); d.data(a, "datepicker", b) } }, _attachments: function(a, b) {
                                var c = this._get(b, "appendText"), e = this._get(b, "isRTL"); b.append &&
b.append.remove(); if (c) { b.append = d('<span class="' + this._appendClass + '">' + c + "</span>"); a[e ? "before" : "after"](b.append) } a.unbind("focus", this._showDatepicker); b.trigger && b.trigger.remove(); c = this._get(b, "showOn"); if (c == "focus" || c == "both") a.focus(this._showDatepicker); if (c == "button" || c == "both") {
                                    c = this._get(b, "buttonText"); var f = this._get(b, "buttonImage"); b.trigger = d(this._get(b, "buttonImageOnly") ? d("<img/>").addClass(this._triggerClass).attr({ src: f, alt: c, title: c }) : d('<button type="button"></button>').addClass(this._triggerClass).html(f ==
"" ? c : d("<img/>").attr({ src: f, alt: c, title: c }))); a[e ? "before" : "after"](b.trigger); b.trigger.click(function() { d.datepicker._datepickerShowing && d.datepicker._lastInput == a[0] ? d.datepicker._hideDatepicker() : d.datepicker._showDatepicker(a[0]); return false })
                                } 
                            }, _autoSize: function(a) {
                                if (this._get(a, "autoSize") && !a.inline) {
                                    var b = new Date(2009, 11, 20), c = this._get(a, "dateFormat"); if (c.match(/[DM]/)) {
                                        var e = function(f) { for (var h = 0, i = 0, g = 0; g < f.length; g++) if (f[g].length > h) { h = f[g].length; i = g } return i }; b.setMonth(e(this._get(a,
c.match(/MM/) ? "monthNames" : "monthNamesShort"))); b.setDate(e(this._get(a, c.match(/DD/) ? "dayNames" : "dayNamesShort")) + 20 - b.getDay())
                                    } a.input.attr("size", this._formatDate(a, b).length)
                                } 
                            }, _inlineDatepicker: function(a, b) {
                                var c = d(a); if (!c.hasClass(this.markerClassName)) {
                                    c.addClass(this.markerClassName).append(b.dpDiv).bind("setData.datepicker", function(e, f, h) { b.settings[f] = h }).bind("getData.datepicker", function(e, f) { return this._get(b, f) }); d.data(a, "datepicker", b); this._setDate(b, this._getDefaultDate(b),
true); this._updateDatepicker(b); this._updateAlternate(b)
                                } 
                            }, _dialogDatepicker: function(a, b, c, e, f) {
                                a = this._dialogInst; if (!a) { this.uuid += 1; this._dialogInput = d('<input type="text" id="' + ("dp" + this.uuid) + '" style="position: absolute; top: -100px; width: 0px; z-index: -10;"/>'); this._dialogInput.keydown(this._doKeyDown); d("body").append(this._dialogInput); a = this._dialogInst = this._newInst(this._dialogInput, false); a.settings = {}; d.data(this._dialogInput[0], "datepicker", a) } E(a.settings, e || {}); b = b && b.constructor ==
Date ? this._formatDate(a, b) : b; this._dialogInput.val(b); this._pos = f ? f.length ? f : [f.pageX, f.pageY] : null; if (!this._pos) this._pos = [document.documentElement.clientWidth / 2 - 100 + (document.documentElement.scrollLeft || document.body.scrollLeft), document.documentElement.clientHeight / 2 - 150 + (document.documentElement.scrollTop || document.body.scrollTop)]; this._dialogInput.css("left", this._pos[0] + 20 + "px").css("top", this._pos[1] + "px"); a.settings.onSelect = c; this._inDialog = true; this.dpDiv.addClass(this._dialogClass); this._showDatepicker(this._dialogInput[0]);
                                d.blockUI && d.blockUI(this.dpDiv); d.data(this._dialogInput[0], "datepicker", a); return this
                            }, _destroyDatepicker: function(a) { var b = d(a), c = d.data(a, "datepicker"); if (b.hasClass(this.markerClassName)) { var e = a.nodeName.toLowerCase(); d.removeData(a, "datepicker"); if (e == "input") { c.append.remove(); c.trigger.remove(); b.removeClass(this.markerClassName).unbind("focus", this._showDatepicker).unbind("keydown", this._doKeyDown).unbind("keypress", this._doKeyPress).unbind("keyup", this._doKeyUp) } else if (e == "div" || e == "span") b.removeClass(this.markerClassName).empty() } },
                            _enableDatepicker: function(a) { var b = d(a), c = d.data(a, "datepicker"); if (b.hasClass(this.markerClassName)) { var e = a.nodeName.toLowerCase(); if (e == "input") { a.disabled = false; c.trigger.filter("button").each(function() { this.disabled = false }).end().filter("img").css({ opacity: "1.0", cursor: "" }) } else if (e == "div" || e == "span") b.children("." + this._inlineClass).children().removeClass("ui-state-disabled"); this._disabledInputs = d.map(this._disabledInputs, function(f) { return f == a ? null : f }) } }, _disableDatepicker: function(a) {
                                var b =
d(a), c = d.data(a, "datepicker"); if (b.hasClass(this.markerClassName)) { var e = a.nodeName.toLowerCase(); if (e == "input") { a.disabled = true; c.trigger.filter("button").each(function() { this.disabled = true }).end().filter("img").css({ opacity: "0.5", cursor: "default" }) } else if (e == "div" || e == "span") b.children("." + this._inlineClass).children().addClass("ui-state-disabled"); this._disabledInputs = d.map(this._disabledInputs, function(f) { return f == a ? null : f }); this._disabledInputs[this._disabledInputs.length] = a } 
                            }, _isDisabledDatepicker: function(a) {
                                if (!a) return false;
                                for (var b = 0; b < this._disabledInputs.length; b++) if (this._disabledInputs[b] == a) return true; return false
                            }, _getInst: function(a) { try { return d.data(a, "datepicker") } catch (b) { throw "Missing instance data for this datepicker"; } }, _optionDatepicker: function(a, b, c) {
                                var e = this._getInst(a); if (arguments.length == 2 && typeof b == "string") return b == "defaults" ? d.extend({}, d.datepicker._defaults) : e ? b == "all" ? d.extend({}, e.settings) : this._get(e, b) : null; var f = b || {}; if (typeof b == "string") { f = {}; f[b] = c } if (e) {
                                    this._curInst == e &&
this._hideDatepicker(); var h = this._getDateDatepicker(a, true); E(e.settings, f); this._attachments(d(a), e); this._autoSize(e); this._setDateDatepicker(a, h); this._updateDatepicker(e)
                                } 
                            }, _changeDatepicker: function(a, b, c) { this._optionDatepicker(a, b, c) }, _refreshDatepicker: function(a) { (a = this._getInst(a)) && this._updateDatepicker(a) }, _setDateDatepicker: function(a, b) { if (a = this._getInst(a)) { this._setDate(a, b); this._updateDatepicker(a); this._updateAlternate(a) } }, _getDateDatepicker: function(a, b) {
                                (a = this._getInst(a)) &&
!a.inline && this._setDateFromField(a, b); return a ? this._getDate(a) : null
                            }, _doKeyDown: function(a) {
                                var b = d.datepicker._getInst(a.target), c = true, e = b.dpDiv.is(".ui-datepicker-rtl"); b._keyEvent = true; if (d.datepicker._datepickerShowing) switch (a.keyCode) {
                                    case 9: d.datepicker._hideDatepicker(); c = false; break; case 13: c = d("td." + d.datepicker._dayOverClass, b.dpDiv).add(d("td." + d.datepicker._currentClass, b.dpDiv)); c[0] ? d.datepicker._selectDay(a.target, b.selectedMonth, b.selectedYear, c[0]) : d.datepicker._hideDatepicker();
                                        return false; case 27: d.datepicker._hideDatepicker(); break; case 33: d.datepicker._adjustDate(a.target, a.ctrlKey ? -d.datepicker._get(b, "stepBigMonths") : -d.datepicker._get(b, "stepMonths"), "M"); break; case 34: d.datepicker._adjustDate(a.target, a.ctrlKey ? +d.datepicker._get(b, "stepBigMonths") : +d.datepicker._get(b, "stepMonths"), "M"); break; case 35: if (a.ctrlKey || a.metaKey) d.datepicker._clearDate(a.target); c = a.ctrlKey || a.metaKey; break; case 36: if (a.ctrlKey || a.metaKey) d.datepicker._gotoToday(a.target); c = a.ctrlKey ||
a.metaKey; break; case 37: if (a.ctrlKey || a.metaKey) d.datepicker._adjustDate(a.target, e ? +1 : -1, "D"); c = a.ctrlKey || a.metaKey; if (a.originalEvent.altKey) d.datepicker._adjustDate(a.target, a.ctrlKey ? -d.datepicker._get(b, "stepBigMonths") : -d.datepicker._get(b, "stepMonths"), "M"); break; case 38: if (a.ctrlKey || a.metaKey) d.datepicker._adjustDate(a.target, -7, "D"); c = a.ctrlKey || a.metaKey; break; case 39: if (a.ctrlKey || a.metaKey) d.datepicker._adjustDate(a.target, e ? -1 : +1, "D"); c = a.ctrlKey || a.metaKey; if (a.originalEvent.altKey) d.datepicker._adjustDate(a.target,
a.ctrlKey ? +d.datepicker._get(b, "stepBigMonths") : +d.datepicker._get(b, "stepMonths"), "M"); break; case 40: if (a.ctrlKey || a.metaKey) d.datepicker._adjustDate(a.target, +7, "D"); c = a.ctrlKey || a.metaKey; break; default: c = false
                                } else if (a.keyCode == 36 && a.ctrlKey) d.datepicker._showDatepicker(this); else c = false; if (c) { a.preventDefault(); a.stopPropagation() } 
                            }, _doKeyPress: function(a) {
                                var b = d.datepicker._getInst(a.target); if (d.datepicker._get(b, "constrainInput")) {
                                    b = d.datepicker._possibleChars(d.datepicker._get(b, "dateFormat"));
                                    var c = String.fromCharCode(a.charCode == G ? a.keyCode : a.charCode); return a.ctrlKey || c < " " || !b || b.indexOf(c) > -1
                                } 
                            }, _doKeyUp: function(a) { a = d.datepicker._getInst(a.target); if (a.input.val() != a.lastVal) try { if (d.datepicker.parseDate(d.datepicker._get(a, "dateFormat"), a.input ? a.input.val() : null, d.datepicker._getFormatConfig(a))) { d.datepicker._setDateFromField(a); d.datepicker._updateAlternate(a); d.datepicker._updateDatepicker(a) } } catch (b) { d.datepicker.log(b) } return true }, _showDatepicker: function(a) {
                                a = a.target ||
a; if (a.nodeName.toLowerCase() != "input") a = d("input", a.parentNode)[0]; if (!(d.datepicker._isDisabledDatepicker(a) || d.datepicker._lastInput == a)) {
                                    var b = d.datepicker._getInst(a); d.datepicker._curInst && d.datepicker._curInst != b && d.datepicker._curInst.dpDiv.stop(true, true); var c = d.datepicker._get(b, "beforeShow"); E(b.settings, c ? c.apply(a, [a, b]) : {}); b.lastVal = null; d.datepicker._lastInput = a; d.datepicker._setDateFromField(b); if (d.datepicker._inDialog) a.value = ""; if (!d.datepicker._pos) {
                                        d.datepicker._pos = d.datepicker._findPos(a);
                                        d.datepicker._pos[1] += a.offsetHeight
                                    } var e = false; d(a).parents().each(function() { e |= d(this).css("position") == "fixed"; return !e }); if (e && d.browser.opera) { d.datepicker._pos[0] -= document.documentElement.scrollLeft; d.datepicker._pos[1] -= document.documentElement.scrollTop } c = { left: d.datepicker._pos[0], top: d.datepicker._pos[1] }; d.datepicker._pos = null; b.dpDiv.css({ position: "absolute", display: "block", top: "-1000px" }); d.datepicker._updateDatepicker(b); c = d.datepicker._checkOffset(b, c, e); b.dpDiv.css({ position: d.datepicker._inDialog &&
d.blockUI ? "static" : e ? "fixed" : "absolute", display: "none", left: c.left + "px", top: c.top + "px"
                                    }); if (!b.inline) {
                                        c = d.datepicker._get(b, "showAnim"); var f = d.datepicker._get(b, "duration"), h = function() { d.datepicker._datepickerShowing = true; var i = d.datepicker._getBorders(b.dpDiv); b.dpDiv.find("iframe.ui-datepicker-cover").css({ left: -i[0], top: -i[1], width: b.dpDiv.outerWidth(), height: b.dpDiv.outerHeight() }) }; b.dpDiv.zIndex(d(a).zIndex() + 1); d.effects && d.effects[c] ? b.dpDiv.show(c, d.datepicker._get(b, "showOptions"), f,
h) : b.dpDiv[c || "show"](c ? f : null, h); if (!c || !f) h(); b.input.is(":visible") && !b.input.is(":disabled") && b.input.focus(); d.datepicker._curInst = b
                                    } 
                                } 
                            }, _updateDatepicker: function(a) {
                                var b = this, c = d.datepicker._getBorders(a.dpDiv); a.dpDiv.empty().append(this._generateHTML(a)).find("iframe.ui-datepicker-cover").css({ left: -c[0], top: -c[1], width: a.dpDiv.outerWidth(), height: a.dpDiv.outerHeight() }).end().find("button, .ui-datepicker-prev, .ui-datepicker-next, .ui-datepicker-calendar td a").bind("mouseout", function() {
                                    d(this).removeClass("ui-state-hover");
                                    this.className.indexOf("ui-datepicker-prev") != -1 && d(this).removeClass("ui-datepicker-prev-hover"); this.className.indexOf("ui-datepicker-next") != -1 && d(this).removeClass("ui-datepicker-next-hover")
                                }).bind("mouseover", function() {
                                    if (!b._isDisabledDatepicker(a.inline ? a.dpDiv.parent()[0] : a.input[0])) {
                                        d(this).parents(".ui-datepicker-calendar").find("a").removeClass("ui-state-hover"); d(this).addClass("ui-state-hover"); this.className.indexOf("ui-datepicker-prev") != -1 && d(this).addClass("ui-datepicker-prev-hover");
                                        this.className.indexOf("ui-datepicker-next") != -1 && d(this).addClass("ui-datepicker-next-hover")
                                    } 
                                }).end().find("." + this._dayOverClass + " a").trigger("mouseover").end(); c = this._getNumberOfMonths(a); var e = c[1]; e > 1 ? a.dpDiv.addClass("ui-datepicker-multi-" + e).css("width", 17 * e + "em") : a.dpDiv.removeClass("ui-datepicker-multi-2 ui-datepicker-multi-3 ui-datepicker-multi-4").width(""); a.dpDiv[(c[0] != 1 || c[1] != 1 ? "add" : "remove") + "Class"]("ui-datepicker-multi"); a.dpDiv[(this._get(a, "isRTL") ? "add" : "remove") + "Class"]("ui-datepicker-rtl");
                                a == d.datepicker._curInst && d.datepicker._datepickerShowing && a.input && a.input.is(":visible") && !a.input.is(":disabled") && a.input.focus()
                            }, _getBorders: function(a) { var b = function(c) { return { thin: 1, medium: 2, thick: 3}[c] || c }; return [parseFloat(b(a.css("border-left-width"))), parseFloat(b(a.css("border-top-width")))] }, _checkOffset: function(a, b, c) {
                                var e = a.dpDiv.outerWidth(), f = a.dpDiv.outerHeight(), h = a.input ? a.input.outerWidth() : 0, i = a.input ? a.input.outerHeight() : 0, g = document.documentElement.clientWidth + d(document).scrollLeft(),
k = document.documentElement.clientHeight + d(document).scrollTop(); b.left -= this._get(a, "isRTL") ? e - h : 0; b.left -= c && b.left == a.input.offset().left ? d(document).scrollLeft() : 0; b.top -= c && b.top == a.input.offset().top + i ? d(document).scrollTop() : 0; b.left -= Math.min(b.left, b.left + e > g && g > e ? Math.abs(b.left + e - g) : 0); b.top -= Math.min(b.top, b.top + f > k && k > f ? Math.abs(f + i) : 0); return b
                            }, _findPos: function(a) {
                                for (var b = this._get(this._getInst(a), "isRTL"); a && (a.type == "hidden" || a.nodeType != 1); ) a = a[b ? "previousSibling" : "nextSibling"];
                                a = d(a).offset(); return [a.left, a.top]
                            }, _hideDatepicker: function(a) {
                                var b = this._curInst; if (!(!b || a && b != d.data(a, "datepicker"))) if (this._datepickerShowing) {
                                    a = this._get(b, "showAnim"); var c = this._get(b, "duration"), e = function() { d.datepicker._tidyDialog(b); this._curInst = null }; d.effects && d.effects[a] ? b.dpDiv.hide(a, d.datepicker._get(b, "showOptions"), c, e) : b.dpDiv[a == "slideDown" ? "slideUp" : a == "fadeIn" ? "fadeOut" : "hide"](a ? c : null, e); a || e(); if (a = this._get(b, "onClose")) a.apply(b.input ? b.input[0] : null, [b.input ? b.input.val() :
"", b]); this._datepickerShowing = false; this._lastInput = null; if (this._inDialog) { this._dialogInput.css({ position: "absolute", left: "0", top: "-100px" }); if (d.blockUI) { d.unblockUI(); d("body").append(this.dpDiv) } } this._inDialog = false
                                } 
                            }, _tidyDialog: function(a) { a.dpDiv.removeClass(this._dialogClass).unbind(".ui-datepicker-calendar") }, _checkExternalClick: function(a) {
                                if (d.datepicker._curInst) {
                                    a = d(a.target); a[0].id != d.datepicker._mainDivId && a.parents("#" + d.datepicker._mainDivId).length == 0 && !a.hasClass(d.datepicker.markerClassName) &&
!a.hasClass(d.datepicker._triggerClass) && d.datepicker._datepickerShowing && !(d.datepicker._inDialog && d.blockUI) && d.datepicker._hideDatepicker()
                                } 
                            }, _adjustDate: function(a, b, c) { a = d(a); var e = this._getInst(a[0]); if (!this._isDisabledDatepicker(a[0])) { this._adjustInstDate(e, b + (c == "M" ? this._get(e, "showCurrentAtPos") : 0), c); this._updateDatepicker(e) } }, _gotoToday: function(a) {
                                a = d(a); var b = this._getInst(a[0]); if (this._get(b, "gotoCurrent") && b.currentDay) {
                                    b.selectedDay = b.currentDay; b.drawMonth = b.selectedMonth = b.currentMonth;
                                    b.drawYear = b.selectedYear = b.currentYear
                                } else { var c = new Date; b.selectedDay = c.getDate(); b.drawMonth = b.selectedMonth = c.getMonth(); b.drawYear = b.selectedYear = c.getFullYear() } this._notifyChange(b); this._adjustDate(a)
                            }, _selectMonthYear: function(a, b, c) { a = d(a); var e = this._getInst(a[0]); e._selectingMonthYear = false; e["selected" + (c == "M" ? "Month" : "Year")] = e["draw" + (c == "M" ? "Month" : "Year")] = parseInt(b.options[b.selectedIndex].value, 10); this._notifyChange(e); this._adjustDate(a) }, _clickMonthYear: function(a) {
                                var b =
this._getInst(d(a)[0]); b.input && b._selectingMonthYear && setTimeout(function() { b.input.focus() }, 0); b._selectingMonthYear = !b._selectingMonthYear
                            }, _selectDay: function(a, b, c, e) { var f = d(a); if (!(d(e).hasClass(this._unselectableClass) || this._isDisabledDatepicker(f[0]))) { f = this._getInst(f[0]); f.selectedDay = f.currentDay = d("a", e).html(); f.selectedMonth = f.currentMonth = b; f.selectedYear = f.currentYear = c; this._selectDate(a, this._formatDate(f, f.currentDay, f.currentMonth, f.currentYear)) } }, _clearDate: function(a) {
                                a =
d(a); this._getInst(a[0]); this._selectDate(a, "")
                            }, _selectDate: function(a, b) { a = this._getInst(d(a)[0]); b = b != null ? b : this._formatDate(a); a.input && a.input.val(b); this._updateAlternate(a); var c = this._get(a, "onSelect"); if (c) c.apply(a.input ? a.input[0] : null, [b, a]); else a.input && a.input.trigger("change"); if (a.inline) this._updateDatepicker(a); else { this._hideDatepicker(); this._lastInput = a.input[0]; typeof a.input[0] != "object" && a.input.focus(); this._lastInput = null } }, _updateAlternate: function(a) {
                                var b = this._get(a,
"altField"); if (b) { var c = this._get(a, "altFormat") || this._get(a, "dateFormat"), e = this._getDate(a), f = this.formatDate(c, e, this._getFormatConfig(a)); d(b).each(function() { d(this).val(f) }) } 
                            }, noWeekends: function(a) { a = a.getDay(); return [a > 0 && a < 6, ""] }, iso8601Week: function(a) { a = new Date(a.getTime()); a.setDate(a.getDate() + 4 - (a.getDay() || 7)); var b = a.getTime(); a.setMonth(0); a.setDate(1); return Math.floor(Math.round((b - a) / 864E5) / 7) + 1 }, parseDate: function(a, b, c) {
                                if (a == null || b == null) throw "Invalid arguments"; b = typeof b ==
"object" ? b.toString() : b + ""; if (b == "") return null; for (var e = (c ? c.shortYearCutoff : null) || this._defaults.shortYearCutoff, f = (c ? c.dayNamesShort : null) || this._defaults.dayNamesShort, h = (c ? c.dayNames : null) || this._defaults.dayNames, i = (c ? c.monthNamesShort : null) || this._defaults.monthNamesShort, g = (c ? c.monthNames : null) || this._defaults.monthNames, k = c = -1, l = -1, u = -1, j = false, o = function(p) { (p = z + 1 < a.length && a.charAt(z + 1) == p) && z++; return p }, m = function(p) {
    o(p); p = new RegExp("^\\d{1," + (p == "@" ? 14 : p == "!" ? 20 : p == "y" ? 4 : p == "o" ?
3 : 2) + "}"); p = b.substring(s).match(p); if (!p) throw "Missing number at position " + s; s += p[0].length; return parseInt(p[0], 10)
}, n = function(p, w, H) { p = o(p) ? H : w; for (w = 0; w < p.length; w++) if (b.substr(s, p[w].length).toLowerCase() == p[w].toLowerCase()) { s += p[w].length; return w + 1 } throw "Unknown name at position " + s; }, r = function() { if (b.charAt(s) != a.charAt(z)) throw "Unexpected literal at position " + s; s++ }, s = 0, z = 0; z < a.length; z++) if (j) if (a.charAt(z) == "'" && !o("'")) j = false; else r(); else switch (a.charAt(z)) {
                                    case "d": l = m("d");
                                        break; case "D": n("D", f, h); break; case "o": u = m("o"); break; case "m": k = m("m"); break; case "M": k = n("M", i, g); break; case "y": c = m("y"); break; case "@": var v = new Date(m("@")); c = v.getFullYear(); k = v.getMonth() + 1; l = v.getDate(); break; case "!": v = new Date((m("!") - this._ticksTo1970) / 1E4); c = v.getFullYear(); k = v.getMonth() + 1; l = v.getDate(); break; case "'": if (o("'")) r(); else j = true; break; default: r()
                                } if (c == -1) c = (new Date).getFullYear(); else if (c < 100) c += (new Date).getFullYear() - (new Date).getFullYear() % 100 + (c <= e ? 0 : -100); if (u >
-1) { k = 1; l = u; do { e = this._getDaysInMonth(c, k - 1); if (l <= e) break; k++; l -= e } while (1) } v = this._daylightSavingAdjust(new Date(c, k - 1, l)); if (v.getFullYear() != c || v.getMonth() + 1 != k || v.getDate() != l) throw "Invalid date"; return v
                            }, ATOM: "yy-mm-dd", COOKIE: "D, dd M yy", ISO_8601: "yy-mm-dd", RFC_822: "D, d M y", RFC_850: "DD, dd-M-y", RFC_1036: "D, d M y", RFC_1123: "D, d M yy", RFC_2822: "D, d M yy", RSS: "D, d M y", TICKS: "!", TIMESTAMP: "@", W3C: "yy-mm-dd", _ticksTo1970: (718685 + Math.floor(492.5) - Math.floor(19.7) + Math.floor(4.925)) * 24 *
60 * 60 * 1E7, formatDate: function(a, b, c) {
    if (!b) return ""; var e = (c ? c.dayNamesShort : null) || this._defaults.dayNamesShort, f = (c ? c.dayNames : null) || this._defaults.dayNames, h = (c ? c.monthNamesShort : null) || this._defaults.monthNamesShort; c = (c ? c.monthNames : null) || this._defaults.monthNames; var i = function(o) { (o = j + 1 < a.length && a.charAt(j + 1) == o) && j++; return o }, g = function(o, m, n) { m = "" + m; if (i(o)) for (; m.length < n; ) m = "0" + m; return m }, k = function(o, m, n, r) { return i(o) ? r[m] : n[m] }, l = "", u = false; if (b) for (var j = 0; j < a.length; j++) if (u) if (a.charAt(j) ==
"'" && !i("'")) u = false; else l += a.charAt(j); else switch (a.charAt(j)) {
        case "d": l += g("d", b.getDate(), 2); break; case "D": l += k("D", b.getDay(), e, f); break; case "o": l += g("o", (b.getTime() - (new Date(b.getFullYear(), 0, 0)).getTime()) / 864E5, 3); break; case "m": l += g("m", b.getMonth() + 1, 2); break; case "M": l += k("M", b.getMonth(), h, c); break; case "y": l += i("y") ? b.getFullYear() : (b.getYear() % 100 < 10 ? "0" : "") + b.getYear() % 100; break; case "@": l += b.getTime(); break; case "!": l += b.getTime() * 1E4 + this._ticksTo1970; break; case "'": if (i("'")) l +=
"'"; else u = true; break; default: l += a.charAt(j)
    } return l
}, _possibleChars: function(a) { for (var b = "", c = false, e = function(h) { (h = f + 1 < a.length && a.charAt(f + 1) == h) && f++; return h }, f = 0; f < a.length; f++) if (c) if (a.charAt(f) == "'" && !e("'")) c = false; else b += a.charAt(f); else switch (a.charAt(f)) { case "d": case "m": case "y": case "@": b += "0123456789"; break; case "D": case "M": return null; case "'": if (e("'")) b += "'"; else c = true; break; default: b += a.charAt(f) } return b }, _get: function(a, b) { return a.settings[b] !== G ? a.settings[b] : this._defaults[b] },
                            _setDateFromField: function(a, b) { if (a.input.val() != a.lastVal) { var c = this._get(a, "dateFormat"), e = a.lastVal = a.input ? a.input.val() : null, f, h; f = h = this._getDefaultDate(a); var i = this._getFormatConfig(a); try { f = this.parseDate(c, e, i) || h } catch (g) { this.log(g); e = b ? "" : e } a.selectedDay = f.getDate(); a.drawMonth = a.selectedMonth = f.getMonth(); a.drawYear = a.selectedYear = f.getFullYear(); a.currentDay = e ? f.getDate() : 0; a.currentMonth = e ? f.getMonth() : 0; a.currentYear = e ? f.getFullYear() : 0; this._adjustInstDate(a) } }, _getDefaultDate: function(a) {
                                return this._restrictMinMax(a,
this._determineDate(a, this._get(a, "defaultDate"), new Date))
                            }, _determineDate: function(a, b, c) {
                                var e = function(h) { var i = new Date; i.setDate(i.getDate() + h); return i }, f = function(h) {
                                    try { return d.datepicker.parseDate(d.datepicker._get(a, "dateFormat"), h, d.datepicker._getFormatConfig(a)) } catch (i) { } var g = (h.toLowerCase().match(/^c/) ? d.datepicker._getDate(a) : null) || new Date, k = g.getFullYear(), l = g.getMonth(); g = g.getDate(); for (var u = /([+-]?[0-9]+)\s*(d|D|w|W|m|M|y|Y)?/g, j = u.exec(h); j; ) {
                                        switch (j[2] || "d") {
                                            case "d": case "D": g +=
parseInt(j[1], 10); break; case "w": case "W": g += parseInt(j[1], 10) * 7; break; case "m": case "M": l += parseInt(j[1], 10); g = Math.min(g, d.datepicker._getDaysInMonth(k, l)); break; case "y": case "Y": k += parseInt(j[1], 10); g = Math.min(g, d.datepicker._getDaysInMonth(k, l)); break
                                        } j = u.exec(h)
                                    } return new Date(k, l, g)
                                }; if (b = (b = b == null ? c : typeof b == "string" ? f(b) : typeof b == "number" ? isNaN(b) ? c : e(b) : b) && b.toString() == "Invalid Date" ? c : b) { b.setHours(0); b.setMinutes(0); b.setSeconds(0); b.setMilliseconds(0) } return this._daylightSavingAdjust(b)
                            },
                            _daylightSavingAdjust: function(a) { if (!a) return null; a.setHours(a.getHours() > 12 ? a.getHours() + 2 : 0); return a }, _setDate: function(a, b, c) {
                                var e = !b, f = a.selectedMonth, h = a.selectedYear; b = this._restrictMinMax(a, this._determineDate(a, b, new Date)); a.selectedDay = a.currentDay = b.getDate(); a.drawMonth = a.selectedMonth = a.currentMonth = b.getMonth(); a.drawYear = a.selectedYear = a.currentYear = b.getFullYear(); if ((f != a.selectedMonth || h != a.selectedYear) && !c) this._notifyChange(a); this._adjustInstDate(a); if (a.input) a.input.val(e ?
"" : this._formatDate(a))
                            }, _getDate: function(a) { return !a.currentYear || a.input && a.input.val() == "" ? null : this._daylightSavingAdjust(new Date(a.currentYear, a.currentMonth, a.currentDay)) }, _generateHTML: function(a) {
                                var b = new Date; b = this._daylightSavingAdjust(new Date(b.getFullYear(), b.getMonth(), b.getDate())); var c = this._get(a, "isRTL"), e = this._get(a, "showButtonPanel"), f = this._get(a, "hideIfNoPrevNext"), h = this._get(a, "navigationAsDateFormat"), i = this._getNumberOfMonths(a), g = this._get(a, "showCurrentAtPos"), k =
this._get(a, "stepMonths"), l = i[0] != 1 || i[1] != 1, u = this._daylightSavingAdjust(!a.currentDay ? new Date(9999, 9, 9) : new Date(a.currentYear, a.currentMonth, a.currentDay)), j = this._getMinMaxDate(a, "min"), o = this._getMinMaxDate(a, "max"); g = a.drawMonth - g; var m = a.drawYear; if (g < 0) { g += 12; m-- } if (o) { var n = this._daylightSavingAdjust(new Date(o.getFullYear(), o.getMonth() - i[0] * i[1] + 1, o.getDate())); for (n = j && n < j ? j : n; this._daylightSavingAdjust(new Date(m, g, 1)) > n; ) { g--; if (g < 0) { g = 11; m-- } } } a.drawMonth = g; a.drawYear = m; n = this._get(a,
"prevText"); n = !h ? n : this.formatDate(n, this._daylightSavingAdjust(new Date(m, g - k, 1)), this._getFormatConfig(a)); n = this._canAdjustMonth(a, -1, m, g) ? '<a class="ui-datepicker-prev ui-corner-all" onclick="DP_jQuery_' + y + ".datepicker._adjustDate('#" + a.id + "', -" + k + ", 'M');\" title=\"" + n + '"><span class="ui-icon ui-icon-circle-triangle-' + (c ? "e" : "w") + '">' + n + "</span></a>" : f ? "" : '<a class="ui-datepicker-prev ui-corner-all ui-state-disabled" title="' + n + '"><span class="ui-icon ui-icon-circle-triangle-' + (c ? "e" : "w") + '">' +
n + "</span></a>"; var r = this._get(a, "nextText"); r = !h ? r : this.formatDate(r, this._daylightSavingAdjust(new Date(m, g + k, 1)), this._getFormatConfig(a)); f = this._canAdjustMonth(a, +1, m, g) ? '<a class="ui-datepicker-next ui-corner-all" onclick="DP_jQuery_' + y + ".datepicker._adjustDate('#" + a.id + "', +" + k + ", 'M');\" title=\"" + r + '"><span class="ui-icon ui-icon-circle-triangle-' + (c ? "w" : "e") + '">' + r + "</span></a>" : f ? "" : '<a class="ui-datepicker-next ui-corner-all ui-state-disabled" title="' + r + '"><span class="ui-icon ui-icon-circle-triangle-' +
(c ? "w" : "e") + '">' + r + "</span></a>"; k = this._get(a, "currentText"); r = this._get(a, "gotoCurrent") && a.currentDay ? u : b; k = !h ? k : this.formatDate(k, r, this._getFormatConfig(a)); h = !a.inline ? '<button type="button" class="ui-datepicker-close ui-state-default ui-priority-primary ui-corner-all" onclick="DP_jQuery_' + y + '.datepicker._hideDatepicker();">' + this._get(a, "closeText") + "</button>" : ""; e = e ? '<div class="ui-datepicker-buttonpane ui-widget-content">' + (c ? h : "") + (this._isInRange(a, r) ? '<button type="button" class="ui-datepicker-current ui-state-default ui-priority-secondary ui-corner-all" onclick="DP_jQuery_' +
y + ".datepicker._gotoToday('#" + a.id + "');\">" + k + "</button>" : "") + (c ? "" : h) + "</div>" : ""; h = parseInt(this._get(a, "firstDay"), 10); h = isNaN(h) ? 0 : h; k = this._get(a, "showWeek"); r = this._get(a, "dayNames"); this._get(a, "dayNamesShort"); var s = this._get(a, "dayNamesMin"), z = this._get(a, "monthNames"), v = this._get(a, "monthNamesShort"), p = this._get(a, "beforeShowDay"), w = this._get(a, "showOtherMonths"), H = this._get(a, "selectOtherMonths"); this._get(a, "calculateWeek"); for (var M = this._getDefaultDate(a), I = "", C = 0; C < i[0]; C++) {
                                    for (var N =
"", D = 0; D < i[1]; D++) {
                                        var J = this._daylightSavingAdjust(new Date(m, g, a.selectedDay)), t = " ui-corner-all", x = ""; if (l) { x += '<div class="ui-datepicker-group'; if (i[1] > 1) switch (D) { case 0: x += " ui-datepicker-group-first"; t = " ui-corner-" + (c ? "right" : "left"); break; case i[1] - 1: x += " ui-datepicker-group-last"; t = " ui-corner-" + (c ? "left" : "right"); break; default: x += " ui-datepicker-group-middle"; t = ""; break } x += '">' } x += '<div class="ui-datepicker-header ui-widget-header ui-helper-clearfix' + t + '">' + (/all|left/.test(t) && C == 0 ? c ?
f : n : "") + (/all|right/.test(t) && C == 0 ? c ? n : f : "") + this._generateMonthYearHeader(a, g, m, j, o, C > 0 || D > 0, z, v) + '</div><table class="ui-datepicker-calendar"><thead><tr>'; var A = k ? '<th class="ui-datepicker-week-col">' + this._get(a, "weekHeader") + "</th>" : ""; for (t = 0; t < 7; t++) { var q = (t + h) % 7; A += "<th" + ((t + h + 6) % 7 >= 5 ? ' class="ui-datepicker-week-end"' : "") + '><span title="' + r[q] + '">' + s[q] + "</span></th>" } x += A + "</tr></thead><tbody>"; A = this._getDaysInMonth(m, g); if (m == a.selectedYear && g == a.selectedMonth) a.selectedDay = Math.min(a.selectedDay,
A); t = (this._getFirstDayOfMonth(m, g) - h + 7) % 7; A = l ? 6 : Math.ceil((t + A) / 7); q = this._daylightSavingAdjust(new Date(m, g, 1 - t)); for (var O = 0; O < A; O++) {
                                            x += "<tr>"; var P = !k ? "" : '<td class="ui-datepicker-week-col">' + this._get(a, "calculateWeek")(q) + "</td>"; for (t = 0; t < 7; t++) {
                                                var F = p ? p.apply(a.input ? a.input[0] : null, [q]) : [true, ""], B = q.getMonth() != g, K = B && !H || !F[0] || j && q < j || o && q > o; P += '<td class="' + ((t + h + 6) % 7 >= 5 ? " ui-datepicker-week-end" : "") + (B ? " ui-datepicker-other-month" : "") + (q.getTime() == J.getTime() && g == a.selectedMonth &&
a._keyEvent || M.getTime() == q.getTime() && M.getTime() == J.getTime() ? " " + this._dayOverClass : "") + (K ? " " + this._unselectableClass + " ui-state-disabled" : "") + (B && !w ? "" : " " + F[1] + (q.getTime() == u.getTime() ? " " + this._currentClass : "") + (q.getTime() == b.getTime() ? " ui-datepicker-today" : "")) + '"' + ((!B || w) && F[2] ? ' title="' + F[2] + '"' : "") + (K ? "" : ' onclick="DP_jQuery_' + y + ".datepicker._selectDay('#" + a.id + "'," + q.getMonth() + "," + q.getFullYear() + ', this);return false;"') + ">" + (B && !w ? "&#xa0;" : K ? '<span class="ui-state-default">' + q.getDate() +
"</span>" : '<a class="ui-state-default' + (q.getTime() == b.getTime() ? " ui-state-highlight" : "") + (q.getTime() == J.getTime() ? " ui-state-active" : "") + (B ? " ui-priority-secondary" : "") + '" href="#">' + q.getDate() + "</a>") + "</td>"; q.setDate(q.getDate() + 1); q = this._daylightSavingAdjust(q)
                                            } x += P + "</tr>"
                                        } g++; if (g > 11) { g = 0; m++ } x += "</tbody></table>" + (l ? "</div>" + (i[0] > 0 && D == i[1] - 1 ? '<div class="ui-datepicker-row-break"></div>' : "") : ""); N += x
                                    } I += N
                                } I += e + (d.browser.msie && parseInt(d.browser.version, 10) < 7 && !a.inline ? '<iframe src="javascript:false;" class="ui-datepicker-cover" frameborder="0"></iframe>' :
""); a._keyEvent = false; return I
                            }, _generateMonthYearHeader: function(a, b, c, e, f, h, i, g) {
                                var k = this._get(a, "changeMonth"), l = this._get(a, "changeYear"), u = this._get(a, "showMonthAfterYear"), j = '<div class="ui-datepicker-title">', o = ""; if (h || !k) o += '<span class="ui-datepicker-month">' + i[b] + "</span>"; else {
                                    i = e && e.getFullYear() == c; var m = f && f.getFullYear() == c; o += '<select class="ui-datepicker-month" onchange="DP_jQuery_' + y + ".datepicker._selectMonthYear('#" + a.id + "', this, 'M');\" onclick=\"DP_jQuery_" + y + ".datepicker._clickMonthYear('#" +
a.id + "');\">"; for (var n = 0; n < 12; n++) if ((!i || n >= e.getMonth()) && (!m || n <= f.getMonth())) o += '<option value="' + n + '"' + (n == b ? ' selected="selected"' : "") + ">" + g[n] + "</option>"; o += "</select>"
                                } u || (j += o + (h || !(k && l) ? "&#xa0;" : "")); if (h || !l) j += '<span class="ui-datepicker-year">' + c + "</span>"; else {
                                    g = this._get(a, "yearRange").split(":"); var r = (new Date).getFullYear(); i = function(s) { s = s.match(/c[+-].*/) ? c + parseInt(s.substring(1), 10) : s.match(/[+-].*/) ? r + parseInt(s, 10) : parseInt(s, 10); return isNaN(s) ? r : s }; b = i(g[0]); g = Math.max(b,
i(g[1] || "")); b = e ? Math.max(b, e.getFullYear()) : b; g = f ? Math.min(g, f.getFullYear()) : g; for (j += '<select class="ui-datepicker-year" onchange="DP_jQuery_' + y + ".datepicker._selectMonthYear('#" + a.id + "', this, 'Y');\" onclick=\"DP_jQuery_" + y + ".datepicker._clickMonthYear('#" + a.id + "');\">"; b <= g; b++) j += '<option value="' + b + '"' + (b == c ? ' selected="selected"' : "") + ">" + b + "</option>"; j += "</select>"
                                } j += this._get(a, "yearSuffix"); if (u) j += (h || !(k && l) ? "&#xa0;" : "") + o; j += "</div>"; return j
                            }, _adjustInstDate: function(a, b, c) {
                                var e =
a.drawYear + (c == "Y" ? b : 0), f = a.drawMonth + (c == "M" ? b : 0); b = Math.min(a.selectedDay, this._getDaysInMonth(e, f)) + (c == "D" ? b : 0); e = this._restrictMinMax(a, this._daylightSavingAdjust(new Date(e, f, b))); a.selectedDay = e.getDate(); a.drawMonth = a.selectedMonth = e.getMonth(); a.drawYear = a.selectedYear = e.getFullYear(); if (c == "M" || c == "Y") this._notifyChange(a)
                            }, _restrictMinMax: function(a, b) { var c = this._getMinMaxDate(a, "min"); a = this._getMinMaxDate(a, "max"); b = c && b < c ? c : b; return b = a && b > a ? a : b }, _notifyChange: function(a) {
                                var b = this._get(a,
"onChangeMonthYear"); if (b) b.apply(a.input ? a.input[0] : null, [a.selectedYear, a.selectedMonth + 1, a])
                            }, _getNumberOfMonths: function(a) { a = this._get(a, "numberOfMonths"); return a == null ? [1, 1] : typeof a == "number" ? [1, a] : a }, _getMinMaxDate: function(a, b) { return this._determineDate(a, this._get(a, b + "Date"), null) }, _getDaysInMonth: function(a, b) { return 32 - (new Date(a, b, 32)).getDate() }, _getFirstDayOfMonth: function(a, b) { return (new Date(a, b, 1)).getDay() }, _canAdjustMonth: function(a, b, c, e) {
                                var f = this._getNumberOfMonths(a);
                                c = this._daylightSavingAdjust(new Date(c, e + (b < 0 ? b : f[0] * f[1]), 1)); b < 0 && c.setDate(this._getDaysInMonth(c.getFullYear(), c.getMonth())); return this._isInRange(a, c)
                            }, _isInRange: function(a, b) { var c = this._getMinMaxDate(a, "min"); a = this._getMinMaxDate(a, "max"); return (!c || b.getTime() >= c.getTime()) && (!a || b.getTime() <= a.getTime()) }, _getFormatConfig: function(a) {
                                var b = this._get(a, "shortYearCutoff"); b = typeof b != "string" ? b : (new Date).getFullYear() % 100 + parseInt(b, 10); return { shortYearCutoff: b, dayNamesShort: this._get(a,
"dayNamesShort"), dayNames: this._get(a, "dayNames"), monthNamesShort: this._get(a, "monthNamesShort"), monthNames: this._get(a, "monthNames")}
                                }, _formatDate: function(a, b, c, e) { if (!b) { a.currentDay = a.selectedDay; a.currentMonth = a.selectedMonth; a.currentYear = a.selectedYear } b = b ? typeof b == "object" ? b : this._daylightSavingAdjust(new Date(e, c, b)) : this._daylightSavingAdjust(new Date(a.currentYear, a.currentMonth, a.currentDay)); return this.formatDate(this._get(a, "dateFormat"), b, this._getFormatConfig(a)) } 
                            }); d.fn.datepicker =
function(a) {
    if (!d.datepicker.initialized) { d(document).mousedown(d.datepicker._checkExternalClick).find("body").append(d.datepicker.dpDiv); d.datepicker.initialized = true } var b = Array.prototype.slice.call(arguments, 1); if (typeof a == "string" && (a == "isDisabled" || a == "getDate" || a == "widget")) return d.datepicker["_" + a + "Datepicker"].apply(d.datepicker, [this[0]].concat(b)); if (a == "option" && arguments.length == 2 && typeof arguments[1] == "string") return d.datepicker["_" + a + "Datepicker"].apply(d.datepicker, [this[0]].concat(b));
    return this.each(function() { typeof a == "string" ? d.datepicker["_" + a + "Datepicker"].apply(d.datepicker, [this].concat(b)) : d.datepicker._attachDatepicker(this, a) })
}; d.datepicker = new L; d.datepicker.initialized = false; d.datepicker.uuid = (new Date).getTime(); d.datepicker.version = "1.8.5"; window["DP_jQuery_" + y] = d
                        })(jQuery);
                        (function(c, j) {
                            c.widget("ui.dialog", { options: { autoOpen: true, buttons: {}, closeOnEscape: true, closeText: "close", dialogClass: "", draggable: true, hide: null, height: "auto", maxHeight: false, maxWidth: false, minHeight: 150, minWidth: 150, modal: false, position: { my: "center", at: "center", of: window, collision: "fit", using: function(a) { var b = c(this).css(a).offset().top; b < 0 && c(this).css("top", a.top - b) } }, resizable: true, show: null, stack: true, title: "", width: 300, zIndex: 1E3 }, _create: function() {
                                this.originalTitle = this.element.attr("title");
                                if (typeof this.originalTitle !== "string") this.originalTitle = ""; this.options.title = this.options.title || this.originalTitle; var a = this, b = a.options, d = b.title || "&#160;", f = c.ui.dialog.getTitleId(a.element), g = (a.uiDialog = c("<div></div>")).appendTo(document.body).hide().addClass("ui-dialog ui-widget ui-widget-content ui-corner-all " + b.dialogClass).css({ zIndex: b.zIndex }).attr("tabIndex", -1).css("outline", 0).keydown(function(i) { if (b.closeOnEscape && i.keyCode && i.keyCode === c.ui.keyCode.ESCAPE) { a.close(i); i.preventDefault() } }).attr({ role: "dialog",
                                    "aria-labelledby": f
                                }).mousedown(function(i) { a.moveToTop(false, i) }); a.element.show().removeAttr("title").addClass("ui-dialog-content ui-widget-content").appendTo(g); var e = (a.uiDialogTitlebar = c("<div></div>")).addClass("ui-dialog-titlebar ui-widget-header ui-corner-all ui-helper-clearfix").prependTo(g), h = c('<a href="#"></a>').addClass("ui-dialog-titlebar-close ui-corner-all").attr("role", "button").hover(function() { h.addClass("ui-state-hover") }, function() { h.removeClass("ui-state-hover") }).focus(function() { h.addClass("ui-state-focus") }).blur(function() { h.removeClass("ui-state-focus") }).click(function(i) {
                                    a.close(i);
                                    return false
                                }).appendTo(e); (a.uiDialogTitlebarCloseText = c("<span></span>")).addClass("ui-icon ui-icon-closethick").text(b.closeText).appendTo(h); c("<span></span>").addClass("ui-dialog-title").attr("id", f).html(d).prependTo(e); if (c.isFunction(b.beforeclose) && !c.isFunction(b.beforeClose)) b.beforeClose = b.beforeclose; e.find("*").add(e).disableSelection(); b.draggable && c.fn.draggable && a._makeDraggable(); b.resizable && c.fn.resizable && a._makeResizable(); a._createButtons(b.buttons); a._isOpen = false; c.fn.bgiframe &&
g.bgiframe()
                            }, _init: function() { this.options.autoOpen && this.open() }, destroy: function() { var a = this; a.overlay && a.overlay.destroy(); a.uiDialog.hide(); a.element.unbind(".dialog").removeData("dialog").removeClass("ui-dialog-content ui-widget-content").hide().appendTo("body"); a.uiDialog.remove(); a.originalTitle && a.element.attr("title", a.originalTitle); return a }, widget: function() { return this.uiDialog }, close: function(a) {
                                var b = this, d; if (false !== b._trigger("beforeClose", a)) {
                                    b.overlay && b.overlay.destroy(); b.uiDialog.unbind("keypress.ui-dialog");
                                    b._isOpen = false; if (b.options.hide) b.uiDialog.hide(b.options.hide, function() { b._trigger("close", a) }); else { b.uiDialog.hide(); b._trigger("close", a) } c.ui.dialog.overlay.resize(); if (b.options.modal) { d = 0; c(".ui-dialog").each(function() { if (this !== b.uiDialog[0]) d = Math.max(d, c(this).css("z-index")) }); c.ui.dialog.maxZ = d } return b
                                } 
                            }, isOpen: function() { return this._isOpen }, moveToTop: function(a, b) {
                                var d = this, f = d.options; if (f.modal && !a || !f.stack && !f.modal) return d._trigger("focus", b); if (f.zIndex > c.ui.dialog.maxZ) c.ui.dialog.maxZ =
f.zIndex; if (d.overlay) { c.ui.dialog.maxZ += 1; d.overlay.$el.css("z-index", c.ui.dialog.overlay.maxZ = c.ui.dialog.maxZ) } a = { scrollTop: d.element.attr("scrollTop"), scrollLeft: d.element.attr("scrollLeft") }; c.ui.dialog.maxZ += 1; d.uiDialog.css("z-index", c.ui.dialog.maxZ); d.element.attr(a); d._trigger("focus", b); return d
                            }, open: function() {
                                if (!this._isOpen) {
                                    var a = this, b = a.options, d = a.uiDialog; a.overlay = b.modal ? new c.ui.dialog.overlay(a) : null; d.next().length && d.appendTo("body"); a._size(); a._position(b.position); d.show(b.show);
                                    a.moveToTop(true); b.modal && d.bind("keypress.ui-dialog", function(f) { if (f.keyCode === c.ui.keyCode.TAB) { var g = c(":tabbable", this), e = g.filter(":first"); g = g.filter(":last"); if (f.target === g[0] && !f.shiftKey) { e.focus(1); return false } else if (f.target === e[0] && f.shiftKey) { g.focus(1); return false } } }); c(a.element.find(":tabbable").get().concat(d.find(".ui-dialog-buttonpane :tabbable").get().concat(d.get()))).eq(0).focus(); a._isOpen = true; a._trigger("open"); return a
                                } 
                            }, _createButtons: function(a) {
                                var b = this, d = false,
f = c("<div></div>").addClass("ui-dialog-buttonpane ui-widget-content ui-helper-clearfix"), g = c("<div></div>").addClass("ui-dialog-buttonset").appendTo(f); b.uiDialog.find(".ui-dialog-buttonpane").remove(); typeof a === "object" && a !== null && c.each(a, function() { return !(d = true) }); if (d) { c.each(a, function(e, h) { h = c.isFunction(h) ? { click: h, text: e} : h; e = c("<button></button>", h).unbind("click").click(function() { h.click.apply(b.element[0], arguments) }).appendTo(g); c.fn.button && e.button() }); f.appendTo(b.uiDialog) } 
                            }, _makeDraggable: function() {
                                function a(e) {
                                    return { position: e.position,
                                        offset: e.offset}
                                    } var b = this, d = b.options, f = c(document), g; b.uiDialog.draggable({ cancel: ".ui-dialog-content, .ui-dialog-titlebar-close", handle: ".ui-dialog-titlebar", containment: "document", start: function(e, h) { g = d.height === "auto" ? "auto" : c(this).height(); c(this).height(c(this).height()).addClass("ui-dialog-dragging"); b._trigger("dragStart", e, a(h)) }, drag: function(e, h) { b._trigger("drag", e, a(h)) }, stop: function(e, h) {
                                        d.position = [h.position.left - f.scrollLeft(), h.position.top - f.scrollTop()]; c(this).removeClass("ui-dialog-dragging").height(g);
                                        b._trigger("dragStop", e, a(h)); c.ui.dialog.overlay.resize()
                                    } 
                                    })
                                }, _makeResizable: function(a) {
                                    function b(e) { return { originalPosition: e.originalPosition, originalSize: e.originalSize, position: e.position, size: e.size} } a = a === j ? this.options.resizable : a; var d = this, f = d.options, g = d.uiDialog.css("position"); a = typeof a === "string" ? a : "n,e,s,w,se,sw,ne,nw"; d.uiDialog.resizable({ cancel: ".ui-dialog-content", containment: "document", alsoResize: d.element, maxWidth: f.maxWidth, maxHeight: f.maxHeight, minWidth: f.minWidth, minHeight: d._minHeight(),
                                        handles: a, start: function(e, h) { c(this).addClass("ui-dialog-resizing"); d._trigger("resizeStart", e, b(h)) }, resize: function(e, h) { d._trigger("resize", e, b(h)) }, stop: function(e, h) { c(this).removeClass("ui-dialog-resizing"); f.height = c(this).height(); f.width = c(this).width(); d._trigger("resizeStop", e, b(h)); c.ui.dialog.overlay.resize() } 
                                    }).css("position", g).find(".ui-resizable-se").addClass("ui-icon ui-icon-grip-diagonal-se")
                                }, _minHeight: function() {
                                    var a = this.options; return a.height === "auto" ? a.minHeight : Math.min(a.minHeight,
a.height)
                                }, _position: function(a) {
                                    var b = [], d = [0, 0], f; if (a) { if (typeof a === "string" || typeof a === "object" && "0" in a) { b = a.split ? a.split(" ") : [a[0], a[1]]; if (b.length === 1) b[1] = b[0]; c.each(["left", "top"], function(g, e) { if (+b[g] === b[g]) { d[g] = b[g]; b[g] = e } }); a = { my: b.join(" "), at: b.join(" "), offset: d.join(" ")} } a = c.extend({}, c.ui.dialog.prototype.options.position, a) } else a = c.ui.dialog.prototype.options.position; (f = this.uiDialog.is(":visible")) || this.uiDialog.show(); this.uiDialog.css({ top: 0, left: 0 }).position(a);
                                    f || this.uiDialog.hide()
                                }, _setOption: function(a, b) {
                                    var d = this, f = d.uiDialog, g = f.is(":data(resizable)"), e = false; switch (a) {
                                        case "beforeclose": a = "beforeClose"; break; case "buttons": d._createButtons(b); e = true; break; case "closeText": d.uiDialogTitlebarCloseText.text("" + b); break; case "dialogClass": f.removeClass(d.options.dialogClass).addClass("ui-dialog ui-widget ui-widget-content ui-corner-all " + b); break; case "disabled": b ? f.addClass("ui-dialog-disabled") : f.removeClass("ui-dialog-disabled"); break; case "draggable": b ?
d._makeDraggable() : f.draggable("destroy"); break; case "height": e = true; break; case "maxHeight": g && f.resizable("option", "maxHeight", b); e = true; break; case "maxWidth": g && f.resizable("option", "maxWidth", b); e = true; break; case "minHeight": g && f.resizable("option", "minHeight", b); e = true; break; case "minWidth": g && f.resizable("option", "minWidth", b); e = true; break; case "position": d._position(b); break; case "resizable": g && !b && f.resizable("destroy"); g && typeof b === "string" && f.resizable("option", "handles", b); !g && b !== false &&
d._makeResizable(b); break; case "title": c(".ui-dialog-title", d.uiDialogTitlebar).html("" + (b || "&#160;")); break; case "width": e = true; break
                                    } c.Widget.prototype._setOption.apply(d, arguments); e && d._size()
                                }, _size: function() {
                                    var a = this.options, b; this.element.css({ width: "auto", minHeight: 0, height: 0 }); if (a.minWidth > a.width) a.width = a.minWidth; b = this.uiDialog.css({ height: "auto", width: a.width }).height(); this.element.css(a.height === "auto" ? { minHeight: Math.max(a.minHeight - b, 0), height: c.support.minHeight ? "auto" : Math.max(a.minHeight -
b, 0)} : { minHeight: 0, height: Math.max(a.height - b, 0) }).show(); this.uiDialog.is(":data(resizable)") && this.uiDialog.resizable("option", "minHeight", this._minHeight())
                                    } 
                                }); c.extend(c.ui.dialog, { version: "1.8.5", uuid: 0, maxZ: 0, getTitleId: function(a) { a = a.attr("id"); if (!a) { this.uuid += 1; a = this.uuid } return "ui-dialog-title-" + a }, overlay: function(a) { this.$el = c.ui.dialog.overlay.create(a) } }); c.extend(c.ui.dialog.overlay, { instances: [], oldInstances: [], maxZ: 0, events: c.map("focus,mousedown,mouseup,keydown,keypress,click".split(","),
function(a) { return a + ".dialog-overlay" }).join(" "), create: function(a) {
    if (this.instances.length === 0) { setTimeout(function() { c.ui.dialog.overlay.instances.length && c(document).bind(c.ui.dialog.overlay.events, function(d) { if (c(d.target).zIndex() < c.ui.dialog.overlay.maxZ) return false }) }, 1); c(document).bind("keydown.dialog-overlay", function(d) { if (a.options.closeOnEscape && d.keyCode && d.keyCode === c.ui.keyCode.ESCAPE) { a.close(d); d.preventDefault() } }); c(window).bind("resize.dialog-overlay", c.ui.dialog.overlay.resize) } var b =
(this.oldInstances.pop() || c("<div></div>").addClass("ui-widget-overlay")).appendTo(document.body).css({ width: this.width(), height: this.height() }); c.fn.bgiframe && b.bgiframe(); this.instances.push(b); return b
}, destroy: function(a) { this.oldInstances.push(this.instances.splice(c.inArray(a, this.instances), 1)[0]); this.instances.length === 0 && c([document, window]).unbind(".dialog-overlay"); a.remove(); var b = 0; c.each(this.instances, function() { b = Math.max(b, this.css("z-index")) }); this.maxZ = b }, height: function() {
    var a,
b; if (c.browser.msie && c.browser.version < 7) { a = Math.max(document.documentElement.scrollHeight, document.body.scrollHeight); b = Math.max(document.documentElement.offsetHeight, document.body.offsetHeight); return a < b ? c(window).height() + "px" : a + "px" } else return c(document).height() + "px"
}, width: function() {
    var a, b; if (c.browser.msie && c.browser.version < 7) {
        a = Math.max(document.documentElement.scrollWidth, document.body.scrollWidth); b = Math.max(document.documentElement.offsetWidth, document.body.offsetWidth); return a <
b ? c(window).width() + "px" : a + "px"
    } else return c(document).width() + "px"
}, resize: function() { var a = c([]); c.each(c.ui.dialog.overlay.instances, function() { a = a.add(this) }); a.css({ width: 0, height: 0 }).css({ width: c.ui.dialog.overlay.width(), height: c.ui.dialog.overlay.height() }) } 
                                }); c.extend(c.ui.dialog.overlay.prototype, { destroy: function() { c.ui.dialog.overlay.destroy(this.$el) } })
                            })(jQuery);
                            (function(c) {
                                c.ui = c.ui || {}; var n = /left|center|right/, o = /top|center|bottom/, t = c.fn.position, u = c.fn.offset; c.fn.position = function(b) {
                                    if (!b || !b.of) return t.apply(this, arguments); b = c.extend({}, b); var a = c(b.of), d = a[0], g = (b.collision || "flip").split(" "), e = b.offset ? b.offset.split(" ") : [0, 0], h, k, j; if (d.nodeType === 9) { h = a.width(); k = a.height(); j = { top: 0, left: 0} } else if (d.scrollTo && d.document) { h = a.width(); k = a.height(); j = { top: a.scrollTop(), left: a.scrollLeft()} } else if (d.preventDefault) {
                                        b.at = "left top"; h = k = 0; j =
{ top: b.of.pageY, left: b.of.pageX}
                                    } else { h = a.outerWidth(); k = a.outerHeight(); j = a.offset() } c.each(["my", "at"], function() { var f = (b[this] || "").split(" "); if (f.length === 1) f = n.test(f[0]) ? f.concat(["center"]) : o.test(f[0]) ? ["center"].concat(f) : ["center", "center"]; f[0] = n.test(f[0]) ? f[0] : "center"; f[1] = o.test(f[1]) ? f[1] : "center"; b[this] = f }); if (g.length === 1) g[1] = g[0]; e[0] = parseInt(e[0], 10) || 0; if (e.length === 1) e[1] = e[0]; e[1] = parseInt(e[1], 10) || 0; if (b.at[0] === "right") j.left += h; else if (b.at[0] === "center") j.left += h /
2; if (b.at[1] === "bottom") j.top += k; else if (b.at[1] === "center") j.top += k / 2; j.left += e[0]; j.top += e[1]; return this.each(function() {
    var f = c(this), l = f.outerWidth(), m = f.outerHeight(), p = parseInt(c.curCSS(this, "marginLeft", true)) || 0, q = parseInt(c.curCSS(this, "marginTop", true)) || 0, v = l + p + parseInt(c.curCSS(this, "marginRight", true)) || 0, w = m + q + parseInt(c.curCSS(this, "marginBottom", true)) || 0, i = c.extend({}, j), r; if (b.my[0] === "right") i.left -= l; else if (b.my[0] === "center") i.left -= l / 2; if (b.my[1] === "bottom") i.top -= m; else if (b.my[1] ===
"center") i.top -= m / 2; i.left = parseInt(i.left); i.top = parseInt(i.top); r = { left: i.left - p, top: i.top - q }; c.each(["left", "top"], function(s, x) { c.ui.position[g[s]] && c.ui.position[g[s]][x](i, { targetWidth: h, targetHeight: k, elemWidth: l, elemHeight: m, collisionPosition: r, collisionWidth: v, collisionHeight: w, offset: e, my: b.my, at: b.at }) }); c.fn.bgiframe && f.bgiframe(); f.offset(c.extend(i, { using: b.using }))
})
                                }; c.ui.position = { fit: { left: function(b, a) {
                                    var d = c(window); d = a.collisionPosition.left + a.collisionWidth - d.width() - d.scrollLeft();
                                    b.left = d > 0 ? b.left - d : Math.max(b.left - a.collisionPosition.left, b.left)
                                }, top: function(b, a) { var d = c(window); d = a.collisionPosition.top + a.collisionHeight - d.height() - d.scrollTop(); b.top = d > 0 ? b.top - d : Math.max(b.top - a.collisionPosition.top, b.top) } 
                                }, flip: { left: function(b, a) {
                                    if (a.at[0] !== "center") {
                                        var d = c(window); d = a.collisionPosition.left + a.collisionWidth - d.width() - d.scrollLeft(); var g = a.my[0] === "left" ? -a.elemWidth : a.my[0] === "right" ? a.elemWidth : 0, e = a.at[0] === "left" ? a.targetWidth : -a.targetWidth, h = -2 * a.offset[0];
                                        b.left += a.collisionPosition.left < 0 ? g + e + h : d > 0 ? g + e + h : 0
                                    } 
                                }, top: function(b, a) { if (a.at[1] !== "center") { var d = c(window); d = a.collisionPosition.top + a.collisionHeight - d.height() - d.scrollTop(); var g = a.my[1] === "top" ? -a.elemHeight : a.my[1] === "bottom" ? a.elemHeight : 0, e = a.at[1] === "top" ? a.targetHeight : -a.targetHeight, h = -2 * a.offset[1]; b.top += a.collisionPosition.top < 0 ? g + e + h : d > 0 ? g + e + h : 0 } } }
                                }; if (!c.offset.setOffset) {
                                    c.offset.setOffset = function(b, a) {
                                        if (/static/.test(c.curCSS(b, "position"))) b.style.position = "relative"; var d =
c(b), g = d.offset(), e = parseInt(c.curCSS(b, "top", true), 10) || 0, h = parseInt(c.curCSS(b, "left", true), 10) || 0; g = { top: a.top - g.top + e, left: a.left - g.left + h }; "using" in a ? a.using.call(b, g) : d.css(g)
                                    }; c.fn.offset = function(b) { var a = this[0]; if (!a || !a.ownerDocument) return null; if (b) return this.each(function() { c.offset.setOffset(this, b) }); return u.call(this) } 
                                } 
                            })(jQuery);
                            (function(b, c) {
                                b.widget("ui.progressbar", { options: { value: 0 }, min: 0, max: 100, _create: function() { this.element.addClass("ui-progressbar ui-widget ui-widget-content ui-corner-all").attr({ role: "progressbar", "aria-valuemin": this.min, "aria-valuemax": this.max, "aria-valuenow": this._value() }); this.valueDiv = b("<div class='ui-progressbar-value ui-widget-header ui-corner-left'></div>").appendTo(this.element); this._refreshValue() }, destroy: function() {
                                    this.element.removeClass("ui-progressbar ui-widget ui-widget-content ui-corner-all").removeAttr("role").removeAttr("aria-valuemin").removeAttr("aria-valuemax").removeAttr("aria-valuenow");
                                    this.valueDiv.remove(); b.Widget.prototype.destroy.apply(this, arguments)
                                }, value: function(a) { if (a === c) return this._value(); this._setOption("value", a); return this }, _setOption: function(a, d) { if (a === "value") { this.options.value = d; this._refreshValue(); this._trigger("change") } b.Widget.prototype._setOption.apply(this, arguments) }, _value: function() { var a = this.options.value; if (typeof a !== "number") a = 0; return Math.min(this.max, Math.max(this.min, a)) }, _refreshValue: function() {
                                    var a = this.value(); this.valueDiv.toggleClass("ui-corner-right",
a === this.max).width(a + "%"); this.element.attr("aria-valuenow", a)
                                } 
                                }); b.extend(b.ui.progressbar, { version: "1.8.5" })
                            })(jQuery);
                            (function(d) {
                                d.widget("ui.slider", d.ui.mouse, { widgetEventPrefix: "slide", options: { animate: false, distance: 0, max: 100, min: 0, orientation: "horizontal", range: false, step: 1, value: 0, values: null }, _create: function() {
                                    var a = this, b = this.options; this._mouseSliding = this._keySliding = false; this._animateOff = true; this._handleIndex = null; this._detectOrientation(); this._mouseInit(); this.element.addClass("ui-slider ui-slider-" + this.orientation + " ui-widget ui-widget-content ui-corner-all"); b.disabled && this.element.addClass("ui-slider-disabled ui-disabled");
                                    this.range = d([]); if (b.range) { if (b.range === true) { this.range = d("<div></div>"); if (!b.values) b.values = [this._valueMin(), this._valueMin()]; if (b.values.length && b.values.length !== 2) b.values = [b.values[0], b.values[0]] } else this.range = d("<div></div>"); this.range.appendTo(this.element).addClass("ui-slider-range"); if (b.range === "min" || b.range === "max") this.range.addClass("ui-slider-range-" + b.range); this.range.addClass("ui-widget-header") } d(".ui-slider-handle", this.element).length === 0 && d("<a href='#'></a>").appendTo(this.element).addClass("ui-slider-handle");
                                    if (b.values && b.values.length) for (; d(".ui-slider-handle", this.element).length < b.values.length; ) d("<a href='#'></a>").appendTo(this.element).addClass("ui-slider-handle"); this.handles = d(".ui-slider-handle", this.element).addClass("ui-state-default ui-corner-all"); this.handle = this.handles.eq(0); this.handles.add(this.range).filter("a").click(function(c) { c.preventDefault() }).hover(function() { b.disabled || d(this).addClass("ui-state-hover") }, function() { d(this).removeClass("ui-state-hover") }).focus(function() {
                                        if (b.disabled) d(this).blur();
                                        else { d(".ui-slider .ui-state-focus").removeClass("ui-state-focus"); d(this).addClass("ui-state-focus") } 
                                    }).blur(function() { d(this).removeClass("ui-state-focus") }); this.handles.each(function(c) { d(this).data("index.ui-slider-handle", c) }); this.handles.keydown(function(c) {
                                        var e = true, f = d(this).data("index.ui-slider-handle"), h, g, i; if (!a.options.disabled) {
                                            switch (c.keyCode) {
                                                case d.ui.keyCode.HOME: case d.ui.keyCode.END: case d.ui.keyCode.PAGE_UP: case d.ui.keyCode.PAGE_DOWN: case d.ui.keyCode.UP: case d.ui.keyCode.RIGHT: case d.ui.keyCode.DOWN: case d.ui.keyCode.LEFT: e =
false; if (!a._keySliding) { a._keySliding = true; d(this).addClass("ui-state-active"); h = a._start(c, f); if (h === false) return } break
                                            } i = a.options.step; h = a.options.values && a.options.values.length ? (g = a.values(f)) : (g = a.value()); switch (c.keyCode) {
                                                case d.ui.keyCode.HOME: g = a._valueMin(); break; case d.ui.keyCode.END: g = a._valueMax(); break; case d.ui.keyCode.PAGE_UP: g = a._trimAlignValue(h + (a._valueMax() - a._valueMin()) / 5); break; case d.ui.keyCode.PAGE_DOWN: g = a._trimAlignValue(h - (a._valueMax() - a._valueMin()) / 5); break; case d.ui.keyCode.UP: case d.ui.keyCode.RIGHT: if (h ===
a._valueMax()) return; g = a._trimAlignValue(h + i); break; case d.ui.keyCode.DOWN: case d.ui.keyCode.LEFT: if (h === a._valueMin()) return; g = a._trimAlignValue(h - i); break
                                            } a._slide(c, f, g); return e
                                        } 
                                    }).keyup(function(c) { var e = d(this).data("index.ui-slider-handle"); if (a._keySliding) { a._keySliding = false; a._stop(c, e); a._change(c, e); d(this).removeClass("ui-state-active") } }); this._refreshValue(); this._animateOff = false
                                }, destroy: function() {
                                    this.handles.remove(); this.range.remove(); this.element.removeClass("ui-slider ui-slider-horizontal ui-slider-vertical ui-slider-disabled ui-widget ui-widget-content ui-corner-all").removeData("slider").unbind(".slider");
                                    this._mouseDestroy(); return this
                                }, _mouseCapture: function(a) {
                                    var b = this.options, c, e, f, h, g; if (b.disabled) return false; this.elementSize = { width: this.element.outerWidth(), height: this.element.outerHeight() }; this.elementOffset = this.element.offset(); c = this._normValueFromMouse({ x: a.pageX, y: a.pageY }); e = this._valueMax() - this._valueMin() + 1; h = this; this.handles.each(function(i) { var j = Math.abs(c - h.values(i)); if (e > j) { e = j; f = d(this); g = i } }); if (b.range === true && this.values(1) === b.min) { g += 1; f = d(this.handles[g]) } if (this._start(a,
g) === false) return false; this._mouseSliding = true; h._handleIndex = g; f.addClass("ui-state-active").focus(); b = f.offset(); this._clickOffset = !d(a.target).parents().andSelf().is(".ui-slider-handle") ? { left: 0, top: 0} : { left: a.pageX - b.left - f.width() / 2, top: a.pageY - b.top - f.height() / 2 - (parseInt(f.css("borderTopWidth"), 10) || 0) - (parseInt(f.css("borderBottomWidth"), 10) || 0) + (parseInt(f.css("marginTop"), 10) || 0) }; this._slide(a, g, c); return this._animateOff = true
                                }, _mouseStart: function() { return true }, _mouseDrag: function(a) {
                                    var b =
this._normValueFromMouse({ x: a.pageX, y: a.pageY }); this._slide(a, this._handleIndex, b); return false
                                }, _mouseStop: function(a) { this.handles.removeClass("ui-state-active"); this._mouseSliding = false; this._stop(a, this._handleIndex); this._change(a, this._handleIndex); this._clickOffset = this._handleIndex = null; return this._animateOff = false }, _detectOrientation: function() { this.orientation = this.options.orientation === "vertical" ? "vertical" : "horizontal" }, _normValueFromMouse: function(a) {
                                    var b; if (this.orientation === "horizontal") {
                                        b =
this.elementSize.width; a = a.x - this.elementOffset.left - (this._clickOffset ? this._clickOffset.left : 0)
                                    } else { b = this.elementSize.height; a = a.y - this.elementOffset.top - (this._clickOffset ? this._clickOffset.top : 0) } b = a / b; if (b > 1) b = 1; if (b < 0) b = 0; if (this.orientation === "vertical") b = 1 - b; a = this._valueMax() - this._valueMin(); return this._trimAlignValue(this._valueMin() + b * a)
                                }, _start: function(a, b) {
                                    var c = { handle: this.handles[b], value: this.value() }; if (this.options.values && this.options.values.length) {
                                        c.value = this.values(b);
                                        c.values = this.values()
                                    } return this._trigger("start", a, c)
                                }, _slide: function(a, b, c) {
                                    var e; if (this.options.values && this.options.values.length) { e = this.values(b ? 0 : 1); if (this.options.values.length === 2 && this.options.range === true && (b === 0 && c > e || b === 1 && c < e)) c = e; if (c !== this.values(b)) { e = this.values(); e[b] = c; a = this._trigger("slide", a, { handle: this.handles[b], value: c, values: e }); this.values(b ? 0 : 1); a !== false && this.values(b, c, true) } } else if (c !== this.value()) {
                                        a = this._trigger("slide", a, { handle: this.handles[b], value: c });
                                        a !== false && this.value(c)
                                    } 
                                }, _stop: function(a, b) { var c = { handle: this.handles[b], value: this.value() }; if (this.options.values && this.options.values.length) { c.value = this.values(b); c.values = this.values() } this._trigger("stop", a, c) }, _change: function(a, b) { if (!this._keySliding && !this._mouseSliding) { var c = { handle: this.handles[b], value: this.value() }; if (this.options.values && this.options.values.length) { c.value = this.values(b); c.values = this.values() } this._trigger("change", a, c) } }, value: function(a) {
                                    if (arguments.length) {
                                        this.options.value =
this._trimAlignValue(a); this._refreshValue(); this._change(null, 0)
                                    } return this._value()
                                }, values: function(a, b) {
                                    var c, e, f; if (arguments.length > 1) { this.options.values[a] = this._trimAlignValue(b); this._refreshValue(); this._change(null, a) } if (arguments.length) if (d.isArray(arguments[0])) { c = this.options.values; e = arguments[0]; for (f = 0; f < c.length; f += 1) { c[f] = this._trimAlignValue(e[f]); this._change(null, f) } this._refreshValue() } else return this.options.values && this.options.values.length ? this._values(a) : this.value();
                                    else return this._values()
                                }, _setOption: function(a, b) {
                                    var c, e = 0; if (d.isArray(this.options.values)) e = this.options.values.length; d.Widget.prototype._setOption.apply(this, arguments); switch (a) {
                                        case "disabled": if (b) { this.handles.filter(".ui-state-focus").blur(); this.handles.removeClass("ui-state-hover"); this.handles.attr("disabled", "disabled"); this.element.addClass("ui-disabled") } else { this.handles.removeAttr("disabled"); this.element.removeClass("ui-disabled") } break; case "orientation": this._detectOrientation();
                                            this.element.removeClass("ui-slider-horizontal ui-slider-vertical").addClass("ui-slider-" + this.orientation); this._refreshValue(); break; case "value": this._animateOff = true; this._refreshValue(); this._change(null, 0); this._animateOff = false; break; case "values": this._animateOff = true; this._refreshValue(); for (c = 0; c < e; c += 1) this._change(null, c); this._animateOff = false; break
                                    } 
                                }, _value: function() { var a = this.options.value; return a = this._trimAlignValue(a) }, _values: function(a) {
                                    var b, c; if (arguments.length) {
                                        b = this.options.values[a];
                                        return b = this._trimAlignValue(b)
                                    } else { b = this.options.values.slice(); for (c = 0; c < b.length; c += 1) b[c] = this._trimAlignValue(b[c]); return b } 
                                }, _trimAlignValue: function(a) { if (a < this._valueMin()) return this._valueMin(); if (a > this._valueMax()) return this._valueMax(); var b = this.options.step > 0 ? this.options.step : 1, c = a % b; a = a - c; if (Math.abs(c) * 2 >= b) a += c > 0 ? b : -b; return parseFloat(a.toFixed(5)) }, _valueMin: function() { return this.options.min }, _valueMax: function() { return this.options.max }, _refreshValue: function() {
                                    var a =
this.options.range, b = this.options, c = this, e = !this._animateOff ? b.animate : false, f, h = {}, g, i, j, l; if (this.options.values && this.options.values.length) this.handles.each(function(k) {
    f = (c.values(k) - c._valueMin()) / (c._valueMax() - c._valueMin()) * 100; h[c.orientation === "horizontal" ? "left" : "bottom"] = f + "%"; d(this).stop(1, 1)[e ? "animate" : "css"](h, b.animate); if (c.options.range === true) if (c.orientation === "horizontal") {
        if (k === 0) c.range.stop(1, 1)[e ? "animate" : "css"]({ left: f + "%" }, b.animate); if (k === 1) c.range[e ? "animate" : "css"]({ width: f -
g + "%"
        }, { queue: false, duration: b.animate })
    } else { if (k === 0) c.range.stop(1, 1)[e ? "animate" : "css"]({ bottom: f + "%" }, b.animate); if (k === 1) c.range[e ? "animate" : "css"]({ height: f - g + "%" }, { queue: false, duration: b.animate }) } g = f
}); else {
                                        i = this.value(); j = this._valueMin(); l = this._valueMax(); f = l !== j ? (i - j) / (l - j) * 100 : 0; h[c.orientation === "horizontal" ? "left" : "bottom"] = f + "%"; this.handle.stop(1, 1)[e ? "animate" : "css"](h, b.animate); if (a === "min" && this.orientation === "horizontal") this.range.stop(1, 1)[e ? "animate" : "css"]({ width: f + "%" },
b.animate); if (a === "max" && this.orientation === "horizontal") this.range[e ? "animate" : "css"]({ width: 100 - f + "%" }, { queue: false, duration: b.animate }); if (a === "min" && this.orientation === "vertical") this.range.stop(1, 1)[e ? "animate" : "css"]({ height: f + "%" }, b.animate); if (a === "max" && this.orientation === "vertical") this.range[e ? "animate" : "css"]({ height: 100 - f + "%" }, { queue: false, duration: b.animate })
                                    } 
                                } 
                                }); d.extend(d.ui.slider, { version: "1.8.5" })
                            })(jQuery);
                            (function(d, p) {
                                function u() { return ++v } function w() { return ++x } var v = 0, x = 0; d.widget("ui.tabs", { options: { add: null, ajaxOptions: null, cache: false, cookie: null, collapsible: false, disable: null, disabled: [], enable: null, event: "click", fx: null, idPrefix: "ui-tabs-", load: null, panelTemplate: "<div></div>", remove: null, select: null, show: null, spinner: "<em>Loading&#8230;</em>", tabTemplate: "<li><a href='#{href}'><span>#{label}</span></a></li>" }, _create: function() { this._tabify(true) }, _setOption: function(a, e) {
                                    if (a == "selected") this.options.collapsible &&
e == this.options.selected || this.select(e); else { this.options[a] = e; this._tabify() } 
                                }, _tabId: function(a) { return a.title && a.title.replace(/\s/g, "_").replace(/[^\w\u00c0-\uFFFF-]/g, "") || this.options.idPrefix + u() }, _sanitizeSelector: function(a) { return a.replace(/:/g, "\\:") }, _cookie: function() { var a = this.cookie || (this.cookie = this.options.cookie.name || "ui-tabs-" + w()); return d.cookie.apply(null, [a].concat(d.makeArray(arguments))) }, _ui: function(a, e) { return { tab: a, panel: e, index: this.anchors.index(a)} }, _cleanup: function() {
                                    this.lis.filter(".ui-state-processing").removeClass("ui-state-processing").find("span:data(label.tabs)").each(function() {
                                        var a =
d(this); a.html(a.data("label.tabs")).removeData("label.tabs")
                                    })
                                }, _tabify: function(a) {
                                    function e(g, f) { g.css("display", ""); !d.support.opacity && f.opacity && g[0].style.removeAttribute("filter") } var b = this, c = this.options, h = /^#.+/; this.list = this.element.find("ol,ul").eq(0); this.lis = d(" > li:has(a[href])", this.list); this.anchors = this.lis.map(function() { return d("a", this)[0] }); this.panels = d([]); this.anchors.each(function(g, f) {
                                        var i = d(f).attr("href"), l = i.split("#")[0], q; if (l && (l === location.toString().split("#")[0] ||
(q = d("base")[0]) && l === q.href)) { i = f.hash; f.href = i } if (h.test(i)) b.panels = b.panels.add(b._sanitizeSelector(i)); else if (i && i !== "#") { d.data(f, "href.tabs", i); d.data(f, "load.tabs", i.replace(/#.*$/, "")); i = b._tabId(f); f.href = "#" + i; f = d("#" + i); if (!f.length) { f = d(c.panelTemplate).attr("id", i).addClass("ui-tabs-panel ui-widget-content ui-corner-bottom").insertAfter(b.panels[g - 1] || b.list); f.data("destroy.tabs", true) } b.panels = b.panels.add(f) } else c.disabled.push(g)
                                    }); if (a) {
                                        this.element.addClass("ui-tabs ui-widget ui-widget-content ui-corner-all");
                                        this.list.addClass("ui-tabs-nav ui-helper-reset ui-helper-clearfix ui-widget-header ui-corner-all"); this.lis.addClass("ui-state-default ui-corner-top"); this.panels.addClass("ui-tabs-panel ui-widget-content ui-corner-bottom"); if (c.selected === p) {
                                            location.hash && this.anchors.each(function(g, f) { if (f.hash == location.hash) { c.selected = g; return false } }); if (typeof c.selected !== "number" && c.cookie) c.selected = parseInt(b._cookie(), 10); if (typeof c.selected !== "number" && this.lis.filter(".ui-tabs-selected").length) c.selected =
this.lis.index(this.lis.filter(".ui-tabs-selected")); c.selected = c.selected || (this.lis.length ? 0 : -1)
                                        } else if (c.selected === null) c.selected = -1; c.selected = c.selected >= 0 && this.anchors[c.selected] || c.selected < 0 ? c.selected : 0; c.disabled = d.unique(c.disabled.concat(d.map(this.lis.filter(".ui-state-disabled"), function(g) { return b.lis.index(g) }))).sort(); d.inArray(c.selected, c.disabled) != -1 && c.disabled.splice(d.inArray(c.selected, c.disabled), 1); this.panels.addClass("ui-tabs-hide"); this.lis.removeClass("ui-tabs-selected ui-state-active");
                                        if (c.selected >= 0 && this.anchors.length) { this.panels.eq(c.selected).removeClass("ui-tabs-hide"); this.lis.eq(c.selected).addClass("ui-tabs-selected ui-state-active"); b.element.queue("tabs", function() { b._trigger("show", null, b._ui(b.anchors[c.selected], b.panels[c.selected])) }); this.load(c.selected) } d(window).bind("unload", function() { b.lis.add(b.anchors).unbind(".tabs"); b.lis = b.anchors = b.panels = null })
                                    } else c.selected = this.lis.index(this.lis.filter(".ui-tabs-selected")); this.element[c.collapsible ? "addClass" :
"removeClass"]("ui-tabs-collapsible"); c.cookie && this._cookie(c.selected, c.cookie); a = 0; for (var j; j = this.lis[a]; a++) d(j)[d.inArray(a, c.disabled) != -1 && !d(j).hasClass("ui-tabs-selected") ? "addClass" : "removeClass"]("ui-state-disabled"); c.cache === false && this.anchors.removeData("cache.tabs"); this.lis.add(this.anchors).unbind(".tabs"); if (c.event !== "mouseover") {
                                        var k = function(g, f) { f.is(":not(.ui-state-disabled)") && f.addClass("ui-state-" + g) }, n = function(g, f) { f.removeClass("ui-state-" + g) }; this.lis.bind("mouseover.tabs",
function() { k("hover", d(this)) }); this.lis.bind("mouseout.tabs", function() { n("hover", d(this)) }); this.anchors.bind("focus.tabs", function() { k("focus", d(this).closest("li")) }); this.anchors.bind("blur.tabs", function() { n("focus", d(this).closest("li")) })
                                    } var m, o; if (c.fx) if (d.isArray(c.fx)) { m = c.fx[0]; o = c.fx[1] } else m = o = c.fx; var r = o ? function(g, f) {
                                        d(g).closest("li").addClass("ui-tabs-selected ui-state-active"); f.hide().removeClass("ui-tabs-hide").animate(o, o.duration || "normal", function() {
                                            e(f, o); b._trigger("show",
null, b._ui(g, f[0]))
                                        })
                                    } : function(g, f) { d(g).closest("li").addClass("ui-tabs-selected ui-state-active"); f.removeClass("ui-tabs-hide"); b._trigger("show", null, b._ui(g, f[0])) }, s = m ? function(g, f) { f.animate(m, m.duration || "normal", function() { b.lis.removeClass("ui-tabs-selected ui-state-active"); f.addClass("ui-tabs-hide"); e(f, m); b.element.dequeue("tabs") }) } : function(g, f) { b.lis.removeClass("ui-tabs-selected ui-state-active"); f.addClass("ui-tabs-hide"); b.element.dequeue("tabs") }; this.anchors.bind(c.event + ".tabs",
function() {
    var g = this, f = d(g).closest("li"), i = b.panels.filter(":not(.ui-tabs-hide)"), l = d(b._sanitizeSelector(g.hash)); if (f.hasClass("ui-tabs-selected") && !c.collapsible || f.hasClass("ui-state-disabled") || f.hasClass("ui-state-processing") || b.panels.filter(":animated").length || b._trigger("select", null, b._ui(this, l[0])) === false) { this.blur(); return false } c.selected = b.anchors.index(this); b.abort(); if (c.collapsible) if (f.hasClass("ui-tabs-selected")) {
        c.selected = -1; c.cookie && b._cookie(c.selected, c.cookie); b.element.queue("tabs",
function() { s(g, i) }).dequeue("tabs"); this.blur(); return false
    } else if (!i.length) { c.cookie && b._cookie(c.selected, c.cookie); b.element.queue("tabs", function() { r(g, l) }); b.load(b.anchors.index(this)); this.blur(); return false } c.cookie && b._cookie(c.selected, c.cookie); if (l.length) { i.length && b.element.queue("tabs", function() { s(g, i) }); b.element.queue("tabs", function() { r(g, l) }); b.load(b.anchors.index(this)) } else throw "jQuery UI Tabs: Mismatching fragment identifier."; d.browser.msie && this.blur()
}); this.anchors.bind("click.tabs",
function() { return false })
                                }, _getIndex: function(a) { if (typeof a == "string") a = this.anchors.index(this.anchors.filter("[href$=" + a + "]")); return a }, destroy: function() {
                                    var a = this.options; this.abort(); this.element.unbind(".tabs").removeClass("ui-tabs ui-widget ui-widget-content ui-corner-all ui-tabs-collapsible").removeData("tabs"); this.list.removeClass("ui-tabs-nav ui-helper-reset ui-helper-clearfix ui-widget-header ui-corner-all"); this.anchors.each(function() {
                                        var e = d.data(this, "href.tabs"); if (e) this.href =
e; var b = d(this).unbind(".tabs"); d.each(["href", "load", "cache"], function(c, h) { b.removeData(h + ".tabs") })
                                    }); this.lis.unbind(".tabs").add(this.panels).each(function() { d.data(this, "destroy.tabs") ? d(this).remove() : d(this).removeClass("ui-state-default ui-corner-top ui-tabs-selected ui-state-active ui-state-hover ui-state-focus ui-state-disabled ui-tabs-panel ui-widget-content ui-corner-bottom ui-tabs-hide") }); a.cookie && this._cookie(null, a.cookie); return this
                                }, add: function(a, e, b) {
                                    if (b === p) b = this.anchors.length;
                                    var c = this, h = this.options; e = d(h.tabTemplate.replace(/#\{href\}/g, a).replace(/#\{label\}/g, e)); a = !a.indexOf("#") ? a.replace("#", "") : this._tabId(d("a", e)[0]); e.addClass("ui-state-default ui-corner-top").data("destroy.tabs", true); var j = d("#" + a); j.length || (j = d(h.panelTemplate).attr("id", a).data("destroy.tabs", true)); j.addClass("ui-tabs-panel ui-widget-content ui-corner-bottom ui-tabs-hide"); if (b >= this.lis.length) { e.appendTo(this.list); j.appendTo(this.list[0].parentNode) } else {
                                        e.insertBefore(this.lis[b]);
                                        j.insertBefore(this.panels[b])
                                    } h.disabled = d.map(h.disabled, function(k) { return k >= b ? ++k : k }); this._tabify(); if (this.anchors.length == 1) { h.selected = 0; e.addClass("ui-tabs-selected ui-state-active"); j.removeClass("ui-tabs-hide"); this.element.queue("tabs", function() { c._trigger("show", null, c._ui(c.anchors[0], c.panels[0])) }); this.load(0) } this._trigger("add", null, this._ui(this.anchors[b], this.panels[b])); return this
                                }, remove: function(a) {
                                    a = this._getIndex(a); var e = this.options, b = this.lis.eq(a).remove(), c = this.panels.eq(a).remove();
                                    if (b.hasClass("ui-tabs-selected") && this.anchors.length > 1) this.select(a + (a + 1 < this.anchors.length ? 1 : -1)); e.disabled = d.map(d.grep(e.disabled, function(h) { return h != a }), function(h) { return h >= a ? --h : h }); this._tabify(); this._trigger("remove", null, this._ui(b.find("a")[0], c[0])); return this
                                }, enable: function(a) {
                                    a = this._getIndex(a); var e = this.options; if (d.inArray(a, e.disabled) != -1) {
                                        this.lis.eq(a).removeClass("ui-state-disabled"); e.disabled = d.grep(e.disabled, function(b) { return b != a }); this._trigger("enable", null,
this._ui(this.anchors[a], this.panels[a])); return this
                                    } 
                                }, disable: function(a) { a = this._getIndex(a); var e = this.options; if (a != e.selected) { this.lis.eq(a).addClass("ui-state-disabled"); e.disabled.push(a); e.disabled.sort(); this._trigger("disable", null, this._ui(this.anchors[a], this.panels[a])) } return this }, select: function(a) { a = this._getIndex(a); if (a == -1) if (this.options.collapsible && this.options.selected != -1) a = this.options.selected; else return this; this.anchors.eq(a).trigger(this.options.event + ".tabs"); return this },
                                    load: function(a) {
                                        a = this._getIndex(a); var e = this, b = this.options, c = this.anchors.eq(a)[0], h = d.data(c, "load.tabs"); this.abort(); if (!h || this.element.queue("tabs").length !== 0 && d.data(c, "cache.tabs")) this.element.dequeue("tabs"); else {
                                            this.lis.eq(a).addClass("ui-state-processing"); if (b.spinner) { var j = d("span", c); j.data("label.tabs", j.html()).html(b.spinner) } this.xhr = d.ajax(d.extend({}, b.ajaxOptions, { url: h, success: function(k, n) {
                                                d(e._sanitizeSelector(c.hash)).html(k); e._cleanup(); b.cache && d.data(c, "cache.tabs",
true); e._trigger("load", null, e._ui(e.anchors[a], e.panels[a])); try { b.ajaxOptions.success(k, n) } catch (m) { } 
                                            }, error: function(k, n) { e._cleanup(); e._trigger("load", null, e._ui(e.anchors[a], e.panels[a])); try { b.ajaxOptions.error(k, n, a, c) } catch (m) { } } 
                                            })); e.element.dequeue("tabs"); return this
                                        } 
                                    }, abort: function() { this.element.queue([]); this.panels.stop(false, true); this.element.queue("tabs", this.element.queue("tabs").splice(-2, 2)); if (this.xhr) { this.xhr.abort(); delete this.xhr } this._cleanup(); return this }, url: function(a,
e) { this.anchors.eq(a).removeData("cache.tabs").data("load.tabs", e); return this }, length: function() { return this.anchors.length } 
                                }); d.extend(d.ui.tabs, { version: "1.8.5" }); d.extend(d.ui.tabs.prototype, { rotation: null, rotate: function(a, e) {
                                    var b = this, c = this.options, h = b._rotate || (b._rotate = function(j) { clearTimeout(b.rotation); b.rotation = setTimeout(function() { var k = c.selected; b.select(++k < b.anchors.length ? k : 0) }, a); j && j.stopPropagation() }); e = b._unrotate || (b._unrotate = !e ? function(j) { j.clientX && b.rotate(null) } :
function() { t = c.selected; h() }); if (a) { this.element.bind("tabsshow", h); this.anchors.bind(c.event + ".tabs", e); h() } else { clearTimeout(b.rotation); this.element.unbind("tabsshow", h); this.anchors.unbind(c.event + ".tabs", e); delete this._rotate; delete this._unrotate } return this
                                } 
                                })
                            })(jQuery);
