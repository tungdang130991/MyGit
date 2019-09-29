/*

DragResize v1.0
(c) 2005-2006 Angus Turnbull, TwinHelix Designs http://www.twinhelix.com

Licensed under the CC-GNU LGPL, version 2.1 or later:
http://creativecommons.org/licenses/LGPL/2.1/
This is distributed WITHOUT ANY WARRANTY; without even the implied
warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.

*/

//Khai bao
var d;
var id;
var posx;
var posy;
var initx = false;
var inity = false;
var status = false;
var text;
var LayoutID = 1;
var ele;
var eleAdv;
var divmove;
var divRemove;
var selectItem;
var StatusgetMouse = 'hidden';

//Xu ly Event
if (typeof addEvent != 'function') {
    var addEvent = function(o, t, f, l) {
        var d = 'addEventListener', n = 'on' + t, rO = o, rT = t, rF = f, rL = l;
        if (o[d] && !l)
            return o[d](t, f, false);
        if (!o._evts)
            o._evts = {};
        if (!o._evts[t]) {
            o._evts[t] = o[n] ? { b: o[n]} : {};
            o[n] = new Function('e', 'var r=true,o=this,a=o._evts["' + t + '"],i;for(i in a){o._f=a[i];r=o._f(e||window.event)!=false&&r;o._f=null}return r');
            if (t != 'unload')
                addEvent(window, 'unload', function() {
                    removeEvent(rO, rT, rF, rL)
                })
        }
        if (!f._i)
            f._i = addEvent._i++;
        o._evts[t][f._i] = f
    };
    addEvent._i = 1;
    var removeEvent = function(o, t, f, l) {
        var d = 'removeEventListener';
        if (o[d] && !l)
            return o[d](t, f, false);
        if (o._evts && o._evts[t] && f._i)
            delete o._evts[t][f._i]
    }
}
function cancelEvent(e, c) {
    e.returnValue = false;
    if (e.preventDefault) e.preventDefault();
    if (c) {
        e.cancelBubble = true;
        if (e.stopPropagation)
            e.stopPropagation()
    }
};
function DragResize(myName, config) {
    var props = { myName: myName, enabled: true, handles: ['tl', 'tm', 'tr', 'ml', 'mr', 'bl', 'bm', 'br'], isElement: null, isHandle: null, element: null, handle: null, minWidth: 10, minHeight: 10, minLeft: 0, maxLeft: 9999, minTop: 0, maxTop: 9999, zIndex: 1, mouseX: 0, mouseY: 0, lastMouseX: 0, lastMouseY: 0, mOffX: 0, mOffY: 0, elmX: 0, elmY: 0, elmW: 0, elmH: 0, allowBlur: true, ondragfocus: null, ondragstart: null, ondragmove: null, ondragend: null, idselect: 0, ondragblur: null }; for (var p in props) this[p] = (typeof config[p] == 'undefined') ? props[p] : config[p]
};
DragResize.prototype.apply = function(node) {
    var obj = this;
    addEvent(node, 'mousedown', function(e) { obj.mouseDown(e) });
    addEvent(node, 'mousemove', function(e) { obj.mouseMove(e) });
    addEvent(node, 'mouseup', function(e) { obj.mouseUp(e) })
};
DragResize.prototype.select = function(newElement) {
    with (this) {
        if (!document.getElementById || !enabled)
            return;
        if (newElement && (newElement != element) && enabled) {
            element = newElement; element.style.zIndex = ++zIndex;
            if (this.resizeHandleSet)
                this.resizeHandleSet(element, true);
            elmX = parseInt(element.style.left);
            elmY = parseInt(element.style.top);
            elmW = element.offsetWidth;
            elmH = element.offsetHeight;
            if (ondragfocus)
                this.ondragfocus()
        }
    }
};
DragResize.prototype.deselect = function(delHandles) {
    with (this) {
        if (!document.getElementById || !enabled)
            return;
        if (delHandles) {
            if (ondragblur)
                this.ondragblur();
            if (this.resizeHandleSet)
                this.resizeHandleSet(element, false);
            element = null
        }
        handle = null;
        mOffX = 0;
        mOffY = 0
    }
};
DragResize.prototype.mouseDown = function(e) {
    with (this) {
        if (!document.getElementById || !enabled)
            return true;
        var elm = e.target || e.srcElement, newElement = null, newHandle = null, hRE = new RegExp(myName + '-([trmbl]{2})', '');
        while (elm) {
            if (elm.className) {
                //Phần them vao
                if (elm.className == 'drsElementStatic')
                    return false;
                //End
                if (!newHandle && (hRE.test(elm.className) || isHandle(elm)))
                    newHandle = elm;
                if (isElement(elm)) {
                    newElement = elm;
                    break
                }
            } elm = elm.parentNode
        }
        if (element && (element != newElement) && allowBlur)
            deselect(true);
        if (newElement && (!element || (newElement == element))) {
            if (newHandle)
                cancelEvent(e);
            select(newElement, newHandle);
            handle = newHandle;
            if (handle && ondragstart)
                this.ondragstart(hRE.test(handle.className))

        }
        //co the bo
        //        else {
        //            document.onmousemove = function(Event) {
        //                if (dragresize.handle == null) {
        //                    getMouse(document, Event);
        //                }
        //                if (Event && Event.stopPropagation)
        //                    Event.stopPropagation()
        //                delete document.onmousemove;

        //                // Or, nulliing
        //                Event.onmousemove = null;
        //                eventObject.stopPropagation()
        //                this.removeEventListener('onmousemove')
        //                document.getElementById('ctl00_MainContent_Canvas').removeEventListener('onmousemove', Event, false);
        //                document.getElementById('ctl00_MainContent_Canvas').onmousemove = null;
        //            }
        //        }
        //end
    }
};
DragResize.prototype.mouseMove = function(e) {
    with (this) {
        if (!document.getElementById || !enabled)
            return true;
        mouseX = e.pageX || e.clientX + document.documentElement.scrollLeft;
        mouseY = e.pageY || e.clientY + document.documentElement.scrollTop;
        var diffX = mouseX - lastMouseX + mOffX;
        var diffY = mouseY - lastMouseY + mOffY;
        mOffX = mOffY = 0;
        lastMouseX = mouseX;
        lastMouseY = mouseY;
        if (!handle)
            return true;
        var isResize = false;
        if (this.resizeHandleDrag && this.resizeHandleDrag(diffX, diffY)) {
            isResize = true
        }
        else {
            var dX = diffX, dY = diffY;
            if (elmX + dX < minLeft)
                mOffX = (dX - (diffX = minLeft - elmX));
            else if (elmX + elmW + dX > maxLeft)
                mOffX = (dX - (diffX = maxLeft - elmX - elmW));
            if (elmY + dY < minTop)
                mOffY = (dY - (diffY = minTop - elmY));
            else if (elmY + elmH + dY > maxTop)
                mOffY = (dY - (diffY = maxTop - elmY - elmH));
            elmX += diffX; elmY += diffY
        } with (element.style) {
            left = elmX + 'px';
            width = elmW + 'px';
            top = elmY + 'px';
            height = elmH + 'px'
        }
        if (window.opera && document.documentElement) {
            var oDF = document.getElementById('op-drag-fix');
            if (!oDF) {
                var oDF = document.createElement('input');
                oDF.id = 'op-drag-fix';
                oDF.style.display = 'none';
                document.body.appendChild(oDF)
            } oDF.focus()
        }
        if (ondragmove)
            this.ondragmove(isResize);
        cancelEvent(e)
    }
};
DragResize.prototype.mouseUp = function(e) {
    with (this) {
        if (!document.getElementById || !enabled)
            return;
        var hRE = new RegExp(myName + '-([trmbl]{2})', '');
        if (handle && ondragend)
            this.ondragend(hRE.test(handle.className));
        deselect(false)
    }
};
DragResize.prototype.resizeHandleSet = function(elm, show) {
    with (this) {
        if (!elm._handle_tr) {
            for (var h = 0; h < handles.length; h++) {
                var hDiv = document.createElement('div');
                hDiv.className = myName + ' ' + myName + '-' + handles[h];
                elm['_handle_' + handles[h]] = elm.appendChild(hDiv)
                idselect = element.attributes.id;
                var d = elm;
                var dsd = this.id;
            }
        }
        for (var h = 0; h < handles.length; h++) {
            elm['_handle_' + handles[h]].style.visibility = show ? 'inherit' : 'hidden'
        }
    }
};
DragResize.prototype.resizeHandleDrag = function(diffX, diffY) {
    with (this) {
        var hClass = handle && handle.className && handle.className.match(new RegExp(myName + '-([tmblr]{2})')) ? RegExp.$1 : '';
        var dY = diffY, dX = diffX, processed = false;
        if (hClass.indexOf('t') >= 0) {
            rs = 1;
            if (elmH - dY < minHeight)
                mOffY = (dY - (diffY = elmH - minHeight));
            else if (elmY + dY < minTop)
                mOffY = (dY - (diffY = minTop - elmY));
            elmY += diffY;
            elmH -= diffY;
            processed = true
        }
        if (hClass.indexOf('b') >= 0) {
            rs = 1;
            if (elmH + dY < minHeight)
                mOffY = (dY - (diffY = minHeight - elmH));
            else if (elmY + elmH + dY > maxTop)
                mOffY = (dY - (diffY = maxTop - elmY - elmH));
            elmH += diffY;
            processed = true
        }
        if (hClass.indexOf('l') >= 0) {
            rs = 1;
            if (elmW - dX < minWidth)
                mOffX = (dX - (diffX = elmW - minWidth));
            else if (elmX + dX < minLeft)
                mOffX = (dX - (diffX = minLeft - elmX));
            elmX += diffX;
            elmW -= diffX;
            processed = true
        }
        if (hClass.indexOf('r') >= 0) {
            rs = 1;
            if (elmW + dX < minWidth)
                mOffX = (dX - (diffX = minWidth - elmW));
            else if (elmX + elmW + dX > maxLeft)
                mOffX = (dX - (diffX = maxLeft - elmX - elmW));
            elmW += diffX;
            processed = true
        }
        return processed
    }
};

//Tao function Ajax
function CreateAjax() {
    //#region
    var XmlHttp;
    //Creating object of XMLHTTP in IE
    try {
        XmlHttp = new ActiveXObject("Msxml2.XMLHTTP");
    }
    catch (e) {
        try {
            XmlHttp = new ActiveXObject("Microsoft.XMLHTTP");
        }
        catch (oc) {
            XmlHttp = null;
        }
    }
    //Creating object of XMLHTTP in Mozilla and Safari
    if (!XmlHttp && typeof XMLHttpRequest != "undefined") {
        XmlHttp = new XMLHttpRequest();
    }
    return XmlHttp;

    //#endregion
}

//Vẽ div khi di chuyen chuot
function getMouse(obj, e) {
    if (!getMouse.lastAssignedId)
        getMouse.lastAssignedId = 10;
    posx = 0;
    posy = 0;
    id = getMouse.lastAssignedId;
    var ev = (!e) ? window.event : e; //Moz:IE
    if (ev.pageX) {//Moz
        posx = ev.pageX + window.pageXOffset - 190;
        // posy = ev.pageY + window.pageYOffset-247;
        posy = ev.pageY - 247;
    }
    else if (ev.clientX) {//IE
        posx = ev.clientX + document.body.scrollLeft - 190;
        //  posy = ev.clientY + document.body.scrollTop - 250;
        posy = ev.clientY + document.documentElement.scrollTop - 250;
    }
    else { return false } //old browsers

    var target = (e && e.target) || (event && event.srcElement);

    if (target.id == 'ctl00_MainContent_Canvas') {
        obj.onmousedown = function() {
            initx = posx;
            inity = posy;
            d = document.createElement('div');
            d.title = '0';
            d.id = id;
            d.className = 'drsElement';
            d.style.left = initx + 'px';
            d.style.top = inity + 'px';
            d.style.cursor = 'move';
            divmove = document.createElement('div');
            divmove.className = 'drsMoveHandle';

            divRemove = document.createElement('input');
            divRemove.type = "button";
            divRemove.id = "btnremoveItem";
            divRemove.className = 'btnremoveChild';
            divRemove.value = 'Xóa';
            divRemove.setAttribute("onclick", "return removeDivChild('" + id + "');");

            selectItem = document.createElement('input');
            selectItem.type = "button";
            selectItem.id = "btnPhanviec";
            selectItem.className = 'btnPhanviec';
            selectItem.value = 'Phân việc';
            selectItem.setAttribute("onclick", "PhanViec(this,'" + id + "');");

            ele = document.createElement('div');
            ele.className = 'divtextclass';
            ele.id = 'divtext' + id;
        }
        obj.onmouseup = function() { initx = false; inity = false; }
        if (initx) {
            if (Math.abs(posx - initx) > 100 && Math.abs(posy - inity) > 80) {
                // document.getElementsByTagName('body')[0].appendChild(d)
                document.getElementById('ctl00_MainContent_Canvas').appendChild(d);
                //            var _width = Math.abs(posx - initx);
                //            var _height = Math.abs(posy - inity);
                //            if (_width < 20)
                //                d.style.width = 250 + 'px';
                //            else
                //                d.style.width = Math.abs(posx - initx) + 'px';
                //            if (_height < 20)
                //                d.style.height = 80 + 'px';
                //            else
                //                d.style.height = Math.abs(posy - inity) + 'px';

                d.style.width = Math.abs(posx - initx) + 'px';
                d.style.height = Math.abs(posy - inity) + 'px';
                d.style.left = posx - initx < 0 ? posx + 'px' : initx + 'px';
                d.style.top = posy - inity < 0 ? posy + 'px' : inity + 'px';

                d.appendChild(divmove);
                d.appendChild(divRemove);
                d.appendChild(selectItem);
                d.appendChild(ele);
            }
        }
    }
    else { return false }
    getMouse.lastAssignedId++;
}
//Vẽ div khi di chuyen chuot
function getMouseTinbai(obj, e) {
    if (!getMouseTinbai.lastAssignedId)
        getMouseTinbai.lastAssignedId = 10;
    posx = 0;
    posy = 0;
    id = getMouseTinbai.lastAssignedId;
    var ev = (!e) ? window.event : e; //Moz:IE
    if (ev.pageX) {//Moz
        posx = ev.pageX + window.pageXOffset - 190;
        // posy = ev.pageY + window.pageYOffset-247;
        posy = ev.pageY - 247;
    }
    else if (ev.clientX) {//IE
        posx = ev.clientX + document.body.scrollLeft - 190;
        //  posy = ev.clientY + document.body.scrollTop - 250;
        posy = ev.clientY + document.documentElement.scrollTop - 250;
    }
    else { return false } //old browsers

    var target = (e && e.target) || (event && event.srcElement);

    if (target.id == 'ctl00_MainContent_Canvas') {
        obj.onmousedown = function() {
            initx = posx;
            inity = posy;
            d = document.createElement('div');
            d.title = '0';
            d.id = id;
            d.className = 'drsElement';
            d.style.left = initx + 'px';
            d.style.top = inity + 'px';
            d.style.cursor = 'move';
            divmove = document.createElement('div');
            divmove.className = 'drsMoveHandle';

            divRemove = document.createElement('input');
            divRemove.type = "button";
            divRemove.id = "btnremoveItem";
            divRemove.className = 'btnremoveChild';
            divRemove.value = 'Xóa';
            divRemove.setAttribute("onclick", "return removeDivChild('" + id + "');");

            selectItem = document.createElement('input');
            selectItem.type = "button";
            selectItem.id = "ChonTinBai";
            selectItem.className = 'btnChontinbai';
            selectItem.value = 'Chọn tin bài';
            selectItem.setAttribute("onclick", "Chonbaiviet(this,'" + id + "');");

            ele = document.createElement('div');
            ele.className = 'divtextclass';
            ele.id = 'divtext' + id;

        }
        obj.onmouseup = function() { initx = false; inity = false; }
        if (initx) {
            if (Math.abs(posx - initx) > 100 && Math.abs(posy - inity) > 80) {
                document.getElementById('ctl00_MainContent_Canvas').appendChild(d);
                d.style.width = Math.abs(posx - initx) + 'px';
                d.style.height = Math.abs(posy - inity) + 'px';
                d.style.left = posx - initx < 0 ? posx + 'px' : initx + 'px';
                d.style.top = posy - inity < 0 ? posy + 'px' : inity + 'px';

                d.appendChild(divmove);
                d.appendChild(divRemove);
                d.appendChild(selectItem);
                d.appendChild(ele);
            }
        }
    }
    else { return false }
    getMouseTinbai.lastAssignedId++;
}
//Vẽ div khi di chuyen chuot
function getMouseAdv(obj, e) {
    if (!getMouseAdv.lastAssignedId)
        getMouseAdv.lastAssignedId = 10;
    posx = 0;
    posy = 0;
    id = getMouseAdv.lastAssignedId;
    var ev = (!e) ? window.event : e; //Moz:IE
    if (ev.pageX) {//Moz
        posx = ev.pageX + window.pageXOffset - 190;
        // posy = ev.pageY + window.pageYOffset-247;
        posy = ev.pageY - 247;
    }
    else if (ev.clientX) {//IE
        posx = ev.clientX + document.body.scrollLeft - 190;
        //  posy = ev.clientY + document.body.scrollTop - 250;
        posy = ev.clientY + document.documentElement.scrollTop - 250;
    }
    else { return false } //old browsers

    var target = (e && e.target) || (event && event.srcElement);

    if (target.id == 'ctl00_MainContent_Canvas') {
        obj.onmousedown = function() {
            initx = posx;
            inity = posy;
            d = document.createElement('div');
            d.title = '0';
            d.id = id;
            d.className = 'drsElement';
            d.style.left = initx + 'px';
            d.style.top = inity + 'px';
            d.style.cursor = 'move';
            divmove = document.createElement('div');
            divmove.className = 'drsMoveHandle';

            divRemove = document.createElement('input');
            divRemove.type = "button";
            divRemove.id = "btnremoveItem";
            divRemove.className = 'btnremoveChild';
            divRemove.value = 'Xóa';
            divRemove.setAttribute("onclick", "return removeDivChild('" + id + "');");

            selectItem = document.createElement('input');
            selectItem.type = "button";
            selectItem.id = "btnAdvertis";
            selectItem.className = 'btnAdvertis';
            selectItem.value = 'Chọn quảng cáo';
            selectItem.setAttribute("onclick", "ChonAdv(this,'" + id + "');");

            ele = document.createElement('div');
            ele.className = 'divtextclass';
            ele.id = 'divtext' + id;

        }
        obj.onmouseup = function() { initx = false; inity = false; }
        if (initx) {
            if (Math.abs(posx - initx) > 100 && Math.abs(posy - inity) > 80) {
                document.getElementById('ctl00_MainContent_Canvas').appendChild(d);
                d.style.width = Math.abs(posx - initx) + 'px';
                d.style.height = Math.abs(posy - inity) + 'px';
                d.style.left = posx - initx < 0 ? posx + 'px' : initx + 'px';
                d.style.top = posy - inity < 0 ? posy + 'px' : inity + 'px';

                d.appendChild(divmove);
                d.appendChild(divRemove);
                d.appendChild(selectItem);
                d.appendChild(ele);
            }
        }
    }
    else { return false }
    getMouseAdv.lastAssignedId++;
}
function addEvent(el, evt, fxn) {
    if (el.addEventListener) el.addEventListener(evt, fxn, false); // for standards
    else if (el.attachEvent) el.attachEvent("on" + evt, fxn); // for IE
    else el['on' + evt] = fxn; // old style, but defeats purpose of using this function
}
//reload lai trang
function ResetDivGraper() {
    window.location.reload();
}
//function removeChildSafe(el) {
//    //before deleting el, recursively delete all of its children.
//    while (el.childNodes.length > 0) {
//        removeChildSafe(el.childNodes[el.childNodes.length - 1]);
//    }
//    el.parentNode.removeChild(el);
//} 
//Xoa bo div
function removeDivChild(divID) {
    var sLinkt = 'updateItemLayout.aspx?ItemID=' + divID;
    var $loader = $('#DeleteItem');
    var ele = document.getElementById('ctl00_MainContent_Canvas');
    var divRemove = document.getElementById(divID);
    if (divRemove != null) {
        if (confirm("Bạn có chắc chắn muốn xóa?")) {
            if (divRemove.hasChildNodes()) {
                ele.removeChild(divRemove);
                $loader.load(sLinkt);
            }
            return true;
        }
        else return false;
    }
}
function removeDivChildAdv(divID) {
    var sLinkt = 'DelItemLayout.aspx?ItemID=' + divID;
    var $loader = $('#DeleteItem');
    var ele = document.getElementById('ctl00_MainContent_Canvas');
    var divRemove = document.getElementById(divID);
    if (divRemove != null) {
        if (confirm("Bạn có chắc chắn muốn xóa?")) {
            if (divRemove.hasChildNodes()) {
                ele.removeChild(divRemove);
                $loader.load(sLinkt);
            }
            return true;
        }
        else return false;
    }
}
function removeDivChild2() {
    var elemt = document.getElementById('ctl00_MainContent_Canvas');
    if (dragresize.isHandle) {
        var _id = dragresize.idselect;
        if (_id.hasChildNodes())
            elemt.removeChild(_id);
    }
}
//Them vach ngan
function add_separated() {
    if (!add_separated.lastAssignedId)
        add_separated.lastAssignedId = 100;

    d = document.createElement('div');
    d.id = add_separated.lastAssignedId;
    d.className = 'drsElement separated';
    d.style.left = '400px';
    d.style.top = '60px';
    divmove = document.createElement('div');
    divmove.className = 'drsMoveHandle MoveCustom';

    divRemove = document.createElement('div');
    divRemove.className = 'drsDiv_removeChild';
    divRemove.setAttribute("onclick", "removeDivChild(" + add_separated.lastAssignedId + ");");

    // document.getElementsByTagName('body')[0].appendChild(d)
    document.getElementById('ctl00_MainContent_Canvas').appendChild(d);
    d.appendChild(divmove);
    d.appendChild(divRemove);
    add_separated.lastAssignedId++;
}
