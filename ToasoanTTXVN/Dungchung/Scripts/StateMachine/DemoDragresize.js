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
var divSave;
var phanviec;
var selectAdv;
var inputTenDT;
var inputMaDT;
var inputSTT;
var lblMaDT;
var lblTenDT;
var lblSTT;
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
        //        else {
        //            document.onmousemove = function(Event) {
        //                if (dragresize.handle == null) {
        //                    getMouse(document, Event);
        //                }
        //                //                if (Event && Event.stopPropagation)
        //                //                    Event.stopPropagation()
        //                //  delete document.onmousemove;

        //                // Or, nulliing
        //                // Event.onmousemove = null;
        //                //eventObject.stopPropagation()
        //                //this.removeEventListener('onmousemove')
        //                // document.getElementById('Canvas').removeEventListener('onmousemove', Event,false);
        //                document.getElementById('Canvas').onmousemove = null;
        //            }
        //        }
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

//Hiển thị div khi chọn Thêm đối tượng
function CreateElementDOM(ID, MaDT, TenDT, STT) {
    
    if (!CreateElementDOM.lastAssignedId)
        CreateElementDOM.lastAssignedId = 10;
    id = CreateElementDOM.lastAssignedId;
    
    d = document.createElement('div');
    d.title = '0';
    d.id = id;
    d.className = 'drsElement';
    if (ID != null)
        d.style.left = '220px';
    else
        d.style.left = '20px';
    d.style.top = '40px';
    d.style.width = '160px';
    d.style.height = '180px';
    d.style.cursor = 'move';
    divmove = document.createElement('div');
    divmove.className = 'drsMoveHandle';

    divRemove = document.createElement('input');
    divRemove.type = "button";
    divRemove.id = "btnremoveItem";
    divRemove.className = 'btnremoveChild';
    divRemove.value = 'Thoát';
    divRemove.setAttribute("onclick", "return removeDivChild('" + id + "');");

    divSave = document.createElement('input');
    divSave.type = "button";
    divSave.id = "btnSaveItem";
    divSave.className = 'btnSaveChild';
    divSave.value = 'Lưu';
    if (ID != null)
        divSave.setAttribute("onclick", "return insertDoiTuong('" + ID + "','#txtMaDT" + id + "','#txtTenDT" + id + "','#txtSTT" + id + "','#" + id + "');");
    else
        divSave.setAttribute("onclick", "return insertDoiTuong('0', '#txtMaDT" + id + "','#txtTenDT" + id + "','#txtSTT" + id + "','#" + id + "');");   
     
    ele = document.createElement('div');
    ele.className = 'divtextclass';
    ele.id = 'divtext' + id;

    lblTenDT = document.createElement('div');
    lblTenDT.id = "lblTenDoiTuong";
    lblTenDT.className = "lblTitle";
    lblTenDT.innerHTML = "Tên đối tượng :";

    inputTenDT = document.createElement('input');
    inputTenDT.type = "text";
    inputTenDT.id = "txtTenDT" + id;
    inputTenDT.className = 'inputtextDT';
    if (ID != null)
        inputTenDT.value = '' + TenDT + '';
    else
        inputTenDT.value = '';
    var _id = "txtTenDT" + id;
    inputTenDT.setAttribute("onkeypress", "return autoLoad('" + _id + "');");
    
    lblMaDT = document.createElement('div');
    lblMaDT.id = "lblMaDoiTuong";
    lblMaDT.className = "lblTitle";
    lblMaDT.innerHTML = "Mã đối tượng :";

    inputMaDT = document.createElement('input');
    inputMaDT.type = "text";
    inputMaDT.id = "txtMaDT" + id;
    inputMaDT.className = 'inputtextDT';
    if (ID != null)
        inputMaDT.value = '' + MaDT + '';
    else
        inputMaDT.value = '';
   
    lblSTT = document.createElement('div');
    lblSTT.id = "lblStt";
    lblSTT.className = "lblTitle";
    lblSTT.innerHTML = "Thứ tự :";

    inputSTT = document.createElement('input');
    inputSTT.type = "text";
    inputSTT.id = "txtSTT" + id;
    inputSTT.className = 'inputtextDT';
    if (ID != null)
        inputSTT.value = '' + STT + '';
    else
        inputSTT.value = '';
    inputSTT.setAttribute("onkeypress", "return check_Number(this,event);")
    
    document.getElementById('ctl00_MainContent_demo').appendChild(d);
//    d.style.width = Math.abs(posx - initx) + 'px';
//    d.style.height = Math.abs(posy - inity) + 'px';
//    d.style.left = posx - initx < 0 ? posx + 'px' : initx + 'px';
//    d.style.top = posy - inity < 0 ? posy + 'px' : inity + 'px';

    d.appendChild(divmove);
    d.appendChild(divSave);
    d.appendChild(divRemove);
    d.appendChild(lblTenDT);
    d.appendChild(inputTenDT);
    d.appendChild(lblMaDT);
    d.appendChild(inputMaDT);
    d.appendChild(lblSTT);
    d.appendChild(inputSTT);
    d.appendChild(ele);

    CreateElementDOM.lastAssignedId++;
}

//function getMouse(obj, e) {
//    if (!getMouse.lastAssignedId)
//        getMouse.lastAssignedId = 10;
//    posx = 0;
//    posy = 0;
//    id = getMouse.lastAssignedId;
//    var ev = (!e) ? window.event : e; //Moz:IE
//    if (ev.pageX) {//Moz
//        posx = ev.pageX + window.pageXOffset - 20;
//        // posy = ev.pageY + window.pageYOffset-247;
//        posy = ev.pageY - 280;
//    }
//    else if (ev.clientX) {//IE
//        posx = ev.clientX + document.body.scrollLeft - 20;
//        //  posy = ev.clientY + document.body.scrollTop - 250;
//        posy = ev.clientY + document.documentElement.scrollTop - 280;
//    }
//    else { return false } //old browsers

//    var target = (e && e.target) || (event && event.srcElement);

//    if (target.id == 'ctl00_MainContent_demo') {
//        obj.onmousedown = function() {
//            initx = posx;
//            inity = posy;
//            d = document.createElement('div');
//            d.title = '0';
//            d.id = id;
//            d.className = 'drsElement';
//            d.style.left = initx + 'px';
//            d.style.top = inity + 'px';
//            d.style.cursor = 'move';
//            divmove = document.createElement('div');
//            divmove.className = 'drsMoveHandle';

//            divRemove = document.createElement('input');
//            divRemove.type = "button";
//            divRemove.id = "btnremoveItem";
//            divRemove.className = 'btnremoveChild';
//            divRemove.value = 'Xóa';
//            divRemove.setAttribute("onclick", "return removeDivChild('" + id + "');");

//            divSave = document.createElement('input');
//            divSave.type = "button";
//            divSave.id = "btnSaveItem";
//            divSave.className = 'btnSaveChild';
//            divSave.value = 'Lưu';
//            divSave.setAttribute("onclick", "return insertDoiTuong(event,'#txtMaDT" + id + "','#txtTenDT" + id + "','#txtSTT" + id + "','#" + id + "');");

//            ele = document.createElement('div');
//            ele.className = 'divtextclass';
//            ele.id = 'divtext' + id;

//            lblMaDT = document.createElement('div');
//            lblMaDT.id = "lblMaDoiTuong";
//            lblMaDT.className = "lblTitle";
//            lblMaDT.innerHTML = "Mã đối tượng :";

//            inputMaDT = document.createElement('input');
//            inputMaDT.type = "text";
//            inputMaDT.id = "txtMaDT" + id;
//            inputMaDT.className = 'inputtextDT';
//            inputMaDT.value = '';

//            lblTenDT = document.createElement('div');
//            lblTenDT.id = "lblTenDoiTuong";
//            lblTenDT.className = "lblTitle";
//            lblTenDT.innerHTML = "Tên đối tượng :";

//            inputTenDT = document.createElement('input');
//            inputTenDT.type = "text";
//            inputTenDT.id = "txtTenDT" + id;
//            inputTenDT.className = 'inputtextDT';
//            inputTenDT.value = '';

//            lblSTT = document.createElement('div');
//            lblSTT.id = "lblStt";
//            lblSTT.className = "lblTitle";
//            lblSTT.innerHTML = "Thứ tự :";

//            inputSTT = document.createElement('input');
//            inputSTT.type = "text";
//            inputSTT.id = "txtSTT" + id;
//            inputSTT.className = 'inputtextDT';
//            inputSTT.value = '';
//            inputSTT.setAttribute("onkeypress", "return check_Number(this,event);")
//        }
//        obj.onmouseup = function() { initx = false; inity = false; }
//        if (initx) {
//            if (Math.abs(posx - initx) > 100 && Math.abs(posy - inity) > 80) {
//                // document.getElementsByTagName('body')[0].appendChild(d)
//                document.getElementById('ctl00_MainContent_demo').appendChild(d);
//                //            var _width = Math.abs(posx - initx);
//                //            var _height = Math.abs(posy - inity);
//                //            if (_width < 20)
//                //                d.style.width = 250 + 'px';
//                //            else
//                //                d.style.width = Math.abs(posx - initx) + 'px';
//                //            if (_height < 20)
//                //                d.style.height = 80 + 'px';
//                //            else
//                //                d.style.height = Math.abs(posy - inity) + 'px';

//                d.style.width = Math.abs(posx - initx) + 'px';
//                d.style.height = Math.abs(posy - inity) + 'px';
//                d.style.left = posx - initx < 0 ? posx + 'px' : initx + 'px';
//                d.style.top = posy - inity < 0 ? posy + 'px' : inity + 'px';

//                d.appendChild(divmove);
//                d.appendChild(divSave);
//                d.appendChild(divRemove);
//                d.appendChild(lblMaDT);
//                d.appendChild(inputMaDT);
//                d.appendChild(lblTenDT);
//                d.appendChild(inputTenDT);
//                d.appendChild(lblSTT);
//                d.appendChild(inputSTT);
//                d.appendChild(ele);
//            }
//        }
//    }
//    else { return false }
//    getMouse.lastAssignedId++;
//}
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
//    var sLinkt = 'DelItemLayout.aspx?ItemID=' + divID;
//    var $loader = $('#DeleteItem');
    var ele = document.getElementById('ctl00_MainContent_demo');
    var divRemove = document.getElementById(divID);
    if (divRemove != null) {
        if (confirm("Bạn có muốn thoát không ?")) {
            if (divRemove.hasChildNodes()) {
                ele.removeChild(divRemove);
                //$loader.load(sLinkt);
            }
            return true;
        }
        else return false;
    }
}
function removeDivChild2() {
    var elemt = document.getElementById('ctl00_MainContent_demo');
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
    d.className = 'w separated';
    d.style.left = '400px';
    d.style.top = '60px';
    divmove = document.createElement('div');
    divmove.className = 'drsMoveHandle MoveCustom';

    divRemove = document.createElement('div');
    divRemove.className = 'drsDiv_removeChild';
    divRemove.setAttribute("onclick", "removeDivChild(" + add_separated.lastAssignedId + ");");

    // document.getElementsByTagName('body')[0].appendChild(d)
    document.getElementById('ctl00_MainContent_demo').appendChild(d);
    d.appendChild(divmove);
    d.appendChild(divRemove);
    add_separated.lastAssignedId++;
}
//function insertDoiTuong(ID, MaDT, TenDT, Stt, divID) {
//    var _maDT = $(MaDT).val();
//    var _tenDT = $(TenDT).val();
//    var _stt = $(Stt).val();
//    if (_maDT != "" && _tenDT != "" && _stt != "") {
//        if (confirm("Bạn có muốn cập nhật đối tượng ko ?")) {
//            var _maDT = $(MaDT).val();
//            var _tenDT = $(TenDT).val();
//            var _stt = $(Stt).val();
//            var _left = $(divID).css('left');
//            var _top = $(divID).css('top');
//            $.ajax({ type: "POST",
//                url: "SaveQuytrinh.asmx/InsertDoiTuong",
//                data: "{ID:'" + ID + "', MaDoituong:'" + _maDT + "', TenDoiTuong:'" + _tenDT + "', Stt:'" + _stt + "', CssLeft:'" + _left + "', CssTop:'" + _top + "',MenuID:'" + menuID.val() + "',IpAddress:'" + ipAddress.val() + "'}",
//                contentType: "application/json; charset=utf-8",
//                dataType: "json",cache: false,
//                success: function(response) {
//                    if (response == 'success') {
//                        
//                    }
//                    else
//                        alert(response);
//                    document.getElementById('ctl00_MainContent_btnReset').click();
//                },
//                error: function(response) {
//                    alert("Cập nhật đối tượng mới không thành công !!");
//                }
//            });
//            return true;
//        }
//        else
//            return false;
//    }
//    else
//        alert("Bạn hãy nhập đầy đủ thông tin !");
//}
//function check_Number(obj, e) {
//    var key;
//    var isCtrl;
//    if (window.event) {
//        key = window.event.keyCode;     //IE
//        if (window.event.ctrlKey)
//            isCtrl = true;
//        else
//            isCtrl = false;
//    }
//    else {
//        key = e.which;     //firefox
//        if (e.ctrlKey)
//            isCtrl = true;
//        else
//            isCtrl = false;
//    }
//    if ((key >= 48 && key <= 57) || key == 8 || key == 44 || ((key == 118 || key == 99 || key == 97) && isCtrl)) {
//    }
//    else return false;
//}
