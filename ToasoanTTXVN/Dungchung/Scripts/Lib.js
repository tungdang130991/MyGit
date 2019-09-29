var TimerID = null;
var TimerOn = false;
var CurrentMenu = null;
var Flag = 0;
var tmp_Window;

function OpenImage(_value) {

    tmp_Window = window.open("../UploadFileMulti/ViewImages.aspx?url=" + _value, "", "directories=no,menubar=no, resizable=no,toolbar=no");

}
function ShowMenu(SubMenu) {
    Flag = 1;

    // ẩn menu đang hiện nếu có
    HideItem(CurrentMenu);
    CurrentMenu = SubMenu;

    // hiện Menu mới
    document.getElementById(SubMenu).className = 'Show';
    //alert(document.getElementById(SubMenu).className);
    StopTimer();
}


function HideMenu(SubMenu, second) {
    TimerID = setTimeout("HideItem('" + SubMenu + "')", second);
    TimerOn = true;
}

function HideItem(elem) {
    if (document.getElementById(elem))
        document.getElementById(elem).className = 'Hide';
}

function StopTimer() {
    if (TimerOn) {
        clearTimeout(TimerID);
        TimerID = null;
        TimerOn = false;
    }
}

function HideAllItem(elem) {
    alert(elem);
    if (document.getElementById(CurrentMenu))
        document.getElementById(CurrentMenu).className = 'Hide';
}

// Khi click vào Trang thì Tab sẽ nhận dc sự kiện trước rồi mới đến Event này
function BodyClick() {
    if (Flag == 0) {
        HideMenu(CurrentMenu, 10);
    }

    Flag = 0;
}
// JScript File



var arVersion = navigator.appVersion.split("MSIE")
var version = parseFloat(arVersion[1])



function FixPNG(myImage) {

    if ((version >= 5.5) && (version < 7) && (document.body.filters)) {

        var imgID = (myImage.id) ? "id='" + myImage.id + "' " : ""

        var imgClass = (myImage.className) ? "class='" + myImage.className + "' " : ""
        //alert(imgClass);
        var imgTitle = (myImage.title) ?

		             "title='" + myImage.title + "' " : "title='" + myImage.alt + "' "

        var imgStyle = "display:inline-block;" + myImage.style.cssText

        var strNewHTML = "<span " + imgID + imgClass + imgTitle

                  + " style=\"" + "width:" + myImage.width

                  + "px; height:" + myImage.height

                  + "px;" + imgStyle + ";"

                  + "filter:progid:DXImageTransform.Microsoft.AlphaImageLoader"

                  + "(src=\'" + myImage.src + "\', sizingMethod='scale');\"></span>"



        myImage.outerHTML = strNewHTML

    }

}


var lastColor;
function DG_changeBackColor(row, highlight) {
    if (highlight) {
        row.style.cursor = "pointer";
        lastColor = row.style.backgroundColor;
        row.style.backgroundColor = '#0968b3';
    }
    else
        row.style.backgroundColor = lastColor;
}
function checkAll_Current(objRef, ID) {

    var GridView = document.getElementById(ID);
    var inputList = GridView.getElementsByTagName("input");

    for (var i = 0; i < inputList.length; i++) {

        if (inputList[i].type == "checkbox" && objRef != inputList[i]) {
            if (objRef.checked && inputList[i].disabled == false) {
                inputList[i].checked = true;
            }
            else {
                inputList[i].checked = false;
            }
        }
    }
}
function SelectAllCheckboxes(spanChk) {

    // Added as ASPX uses SPAN for checkbox 
    var xState = spanChk.checked;
    var theBox = spanChk;

    elm = theBox.form.elements;
    for (i = 0; i < elm.length; i++)
        if (elm[i].type == "checkbox" && elm[i].id != theBox.id) {
        //elm[i].click();
        if (elm[i].checked != xState)
            elm[i].click();
        //elm[i].checked=xState;
    }
}

function getAllCombo() {
    var elm = document.forms.elements;
    for (i = 0; i < elm.length; i++) {
        if (elm[i] == "select-one")
            alert('ddd');
    }
}
function makeWindowed(p_div) {
    var is_ie6 =
           document.all &&
           (navigator.userAgent.toLowerCase().indexOf("msie 6.") != -1);
    if (is_ie6) {
        var html =
              "<iframe style=\"position: absolute; display: block; " +
              "z-index: -1; width: 100%; height: 100%; top: 0; left: 0;" +
              "filter: mask(); \"></iframe>";
        ///alert(p_div);
        if (p_div) p_div.innerHTML += html;
        //alert(p_div.innerHTML);
        // force refresh of div
        var olddisplay = p_div.style.display;

        p_div.style.display = 'none';
        p_div.style.display = olddisplay;

    };
}
//Add News Cao Tien Bo
function clickButton(e, buttonid) {
    var evt = e ? e : window.event;
    var bt = document.getElementById(buttonid);
    if (bt) {
        if (evt.keyCode == 13) {
            bt.click();
            return false;
        }
    }
}

function check_num(obj, length, e) {
    var key = window.event ? e.keyCode : e.which;
    var len = obj.value.length + 1;
    if (length <= 3) begin = 48; else begin = 45;
    if (key >= begin && key <= 57 && len <= length || (key == 8 || key == 0)) {
    }
    else return false;
}

function SubmitImage(_value, vWidth, vHeight) {
    //alert(vWidth); alert(_value); alert(vHeight);
    winDef = 'scrollbars=no,scrolling=no,location=no,toolbar=no,height='.concat(vHeight).concat(',').concat('width=').concat(vWidth).concat(',');
    winDef = winDef.concat('top=').concat((screen.height - vHeight) / 2).concat(',');
    winDef = winDef.concat('left=').concat((screen.width - (vWidth)) / 2);
    window.open(_value, "", winDef);
}
function ViewImages(vLink) {

    var sLink = (typeof (vLink.href) == 'undefined') ?
		vLink : vLink.href;
    if (sLink == '') { return false; }
    var myImage = new Image();
    myImage.src = vLink;
    //alert(myImage.src.height);
    var vHeight = myImage.height;
    var vWidth = myImage.width;
    winDef = 'status=no,resizable=no,scrollbars=no,toolbar=no,location=no,fullscreen=no,titlebar=no,height='.concat(vHeight).concat(',').concat('width=').concat(vWidth).concat(',');
    winDef = winDef.concat('top=').concat((screen.height - vHeight) / 2).concat(',');
    winDef = winDef.concat('left=').concat((screen.width - (vWidth)) / 2);
    var url = '../Until/ViewImages.aspx?url=' + sLink + '';
    newwin = open(url, '_blank', winDef); if (typeof (vLink.href) != 'undefined') {
        return false;
    }
}
function open_window_Scroll(url, top, height, left, width) {
    var tmp_Window = window.open(url, 'popup', 'height=1600,width=1800,location=no,directories=no,resizable=yes,status=yes,toolbar=no,menubar=no,scrollbars=yes,top=5,left=150');

}

//placeholder input
function FauxPlaceholder() {
    if (!ElementSupportAttribute('input', 'placeholder')) {
        $("input[placeholder]").each(function() {
            var $input = $(this);
            $input.after('<input id="' + $input.attr('id') + '-faux" style="display:none;" type="text" value="' + $input.attr('placeholder') + '" />');
            var $faux = $('#' + $input.attr('id') + '-faux');

            $faux.show().attr('class', $input.attr('class')).attr('style', $input.attr('style'));
            $input.hide();

            $faux.focus(function() {
                $faux.hide();
                $input.show().focus();
            });

            $input.blur(function() {
                if ($input.val() == '') {
                    $input.hide();
                    $faux.show();
                }
            });
        });
    }
}

function CheckAllandUnCheckAllBTQ(spanChk) {

    var xState = spanChk.checked;
    var theBox = spanChk;

    elm = theBox.form.elements;
    for (i = 0; i < elm.length; i++)
        if (elm[i].type == "checkbox" && elm[i].id != theBox.id) {

        if (elm[i].checked != xState)
            elm[i].click();

    }
}
function ChkParrent(spanChk, iTemp) {

    var xState = spanChk.checked;
    var index;
    if (iTemp < 10)
        index = '0' + iTemp;
    else index = iTemp;

    var childId = "ctl00_MainContent_gdListMenu_ctl" + index + "_chkR_Add"
    childCheckbox = document.getElementById(childId)
    if (childCheckbox != null)
        if (xState) document.getElementById(childId).checked = 'checked'
    else document.getElementById(childId).checked = ''

    childId = "ctl00_MainContent_gdListMenu_ctl" + index + "_chkR_Edit"
    childCheckbox = document.getElementById(childId)
    if (childCheckbox != null)
        if (xState) document.getElementById(childId).checked = 'checked'
    else document.getElementById(childId).checked = ''

    childId = "ctl00_MainContent_gdListMenu_ctl" + index + "_chkR_Del"
    childCheckbox = document.getElementById(childId)
    if (childCheckbox != null)
        if (xState) document.getElementById(childId).checked = 'checked'
    else document.getElementById(childId).checked = ''

    childId = "ctl00_MainContent_gdListMenu_ctl" + index + "_chkR_Pub"
    childCheckbox = document.getElementById(childId)
    if (childCheckbox != null)
        if (xState) document.getElementById(childId).checked = 'checked'
    else document.getElementById(childId).checked = ''

}
function CheckConfirmAll(_id) {

    if (confirm(_id))
        return true;
    else return false;
}
function Comma(id) {

    var number = document.getElementById(id).value;
    var no = number;
    no.indexOf(',');
    if (no != -1) {
        no = no.replace(',', ''); no = no.replace(',', ''); no = no.replace(',', '');
        no = no.replace(',', ''); no = no.replace(',', ''); no = no.replace(',', '');
        number = no;
        number = '' + number;
        if (number.length > 3) {
            var mod = number.length % 3;
            var output = (mod > 0 ? (number.substring(0, mod)) : '');
            for (i = 0; i < Math.floor(number.length / 3); i++) {
                if ((mod == 0) && (i == 0))
                    output += number.substring(mod + 3 * i, mod + 3 * i + 3);
                else
                    output += ',' + number.substring(mod + 3 * i, mod + 3 * i + 3);
                document.getElementById(id).value = output;
            }

            return (output);

        }

        else return number;

    }
    else {
        number = '' + number;
        if (number.length > 3) {
            var mod = number.length % 3;
            var output = (mod > 0 ? (number.substring(0, mod)) : '');
            for (i = 0; i < Math.floor(number.length / 3); i++) {
                if ((mod == 0) && (i == 0))
                    output += number.substring(mod + 3 * i, mod + 3 * i + 3);
                else
                    output += ',' + number.substring(mod + 3 * i, mod + 3 * i + 3);
                document.getElementById(id).value = output;
            }

            return (output);

        }

        else return number;

    }

}
function CommaMonney(id) {
    var number = document.getElementById(id).value;
    //alert(number);
    var no = number;
    no.indexOf(',');
    if (no != -1) {
        no = no.replace(',', ''); no = no.replace(',', ''); no = no.replace(',', '');
        no = no.replace(',', ''); no = no.replace(',', ''); no = no.replace(',', '');
        number = no;
        number = '' + number;
        if (number.length > 3) {
            var mod = number.length % 3;
            var output = (mod > 0 ? (number.substring(0, mod)) : '');
            for (i = 0; i < Math.floor(number.length / 3); i++) {
                if ((mod == 0) && (i == 0))
                    output += number.substring(mod + 3 * i, mod + 3 * i + 3);
                else
                    output += ',' + number.substring(mod + 3 * i, mod + 3 * i + 3);
                document.getElementById(id).value = output;
            }
            return (output);
        }
        else return number;
    }
    else {
        number = '' + number;
        if (number.length > 3) {
            var mod = number.length % 3;
            var output = (mod > 0 ? (number.substring(0, mod)) : '');
            for (i = 0; i < Math.floor(number.length / 3); i++) {
                if ((mod == 0) && (i == 0))
                    output += number.substring(mod + 3 * i, mod + 3 * i + 3);
                else
                    output += ',' + number.substring(mod + 3 * i, mod + 3 * i + 3);
                document.getElementById(id).value = output;
            }
            return (output);
        }
        else return number;
    }

}

function ParrentClick(ParrentID) {

    var ml = document.forms[0];
    var len = ml.elements.length;
    var strCheckName = 'Parrent' + ParrentID;

    if (document.getElementById('Parrent' + ParrentID).checked == true) {

        for (var i = 0; i < len; i++) {
            var e = ml.elements[i];

            if (e.type == 'checkbox') {

                if (e.id.substr(0, strCheckName.length) == strCheckName) {

                    document.getElementById(e.id).checked = true;
                }

            }
        }

    }

    else {
        for (var i = 0; i < len; i++) {
            var e = ml.elements[i];

            if (e.type == 'checkbox') {

                if (e.id.substr(0, strCheckName.length) == strCheckName) {

                    document.getElementById(e.id).checked = false;
                }

            }
        }
    }

}

function ChildeClick(CategoryID) {
    var strCheckName = 'Parrent' + CategoryID;
    document.getElementById(strCheckName).checked = document.getElementById(strCheckName).checked;
}
function getIDSelect() {

    var strCateID = "";
    var ml = document.forms[0];
    var len = ml.elements.length;

    for (var i = 0; i < len; i++) {
        var e = ml.elements[i];

        if (e.type == 'checkbox' && e.checked == true) {
            if (e.checked == true && e.id != "chkAll" && e.value != "");
            {
                if (strCateID != "")
                    strCateID = strCateID + ";" + e.value;
                else
                    strCateID = e.value;
            }
        }
    }
    document.getElementById('ctl00_MainContent_txtCateAccess').value = strCateID;


}
function getGroupIDSelect() {

    var strCateID = "";
    var ml = document.forms[0];
    var len = ml.elements.length;

    for (var i = 0; i < len; i++) {
        var e = ml.elements[i];

        if (e.type == 'checkbox' && e.checked == true) {
            if (e.checked == true && e.id != "chkAll" && e.value != "");
            {
                if (strCateID != "")
                    strCateID = strCateID + ";" + e.value;
                else
                    strCateID = e.value;
            }
        }
    }
    document.getElementById('ctl00_MainContent_txtCateAccess').value = strCateID;


}
//dan trang Convert Unicode to BKHCM2

function ConvertBKHCM2_IMGDESC(txtOriginal, TDID, FontID) {

    var tdID = "";
    var tbl = document.getElementById(TDID);
    if (tbl == null) return;
    for (var i = 0; i < tbl.rows.length; i++) {

        tdID = tbl.rows[i].cells[0].id;

        if (tdID != "") {
            if (CheckBrowser())
                HPC_Convert_Text(txtOriginal, tdID, FontID);
            else
                HPC_DetectEnc();
        }
    }

}
function HPC_Convert_Text(src_Element, des_Element, FontBKID) {

    HPC_DetectEnc(); //convert font for firefox
    //convert font for IE
    var content_AfterConvert, content_Before; convertArea;

    document.getElementById(src_Element).value = document.getElementById(des_Element).innerHTML;

    content_Before = document.getElementById(src_Element).value;

    if (!convertArea(document.getElementById(src_Element), "UNICODE")) {

        return;
    }
    // Convert from Unicode Font to BKTPHCM2 Font_ID:12
    content_AfterConvert = convertTo(document.getElementById(src_Element), parseInt(FontBKID));
    document.getElementById(des_Element).innerHTML = document.getElementById(src_Element).value;

}
function HPC_DetectEnc() {

    var id = detectMap(document.getElementById('convert_content'));
    if (id)
        document.all['curmap'].options[id - 1].selected = true;
    return true;
}
function CheckBrowser() {
    var bName = navigator.appName;
    if (bName == "Microsoft Internet Explorer")
        return true;
    else
        return false;
}
function getEditorValue_export(instanceName) {

    var oEditor = document.getElementById(instanceName);
    return oEditor.innerHTML;
}
//end
function PopupWindow(url) {
    var chasm = screen.availWidth;
    var mount = screen.availHeight;
    var w = 870;
    var h = 500;
    pupop = window.open(url, '', 'menubar=yes,toolbar=no, status=yes, scrollbars=yes, width=' + w + ',height=' + h + ',left=' + ((chasm - w - 10) * .5) + ',top=' + ((mount - h - 30) * .5));
    pupop.focus();
}
function PopupWindowVideo(url) {
    var chasm = screen.availWidth;
    var mount = screen.availHeight;
    var w = 580;
    var h = 420;
    pupop = window.open(url, '', 'status=no,resizable=no,scrollbars=no,toolbar=no,location=no,fullscreen=no,titlebar=no,width=' + w + ',height=' + h + ',left=' + ((chasm - w - 10) * .5) + ',top=' + ((mount - h - 30) * .5));
    pupop.focus();
}
// JS BAO DIEN TU
function f_ViewVideo(_value, txtimge) {
    var _url = document.getElementById('' + _value + '').value;
    var _img = document.getElementById('' + txtimge + '').value;
    if (_url != "") {
        var vHeight = 500;
        var vWidth = 600;
        winDef = 'scrollbars=yes,scrolling=yes,location=no,toolbar=no,height='.concat(vHeight).concat(',').concat('width=').concat(vWidth).concat(',');
        winDef = winDef.concat('top=').concat((screen.height - vHeight) / 2).concat(',');
        winDef = winDef.concat('left=').concat((screen.width - (vWidth)) / 2);
        //alert(pagesite);
        window.open(pagesite + "/Multimedia/ViewVideoPath.aspx?imgvod=" + _img + "&url=" + _url + "", "", winDef);
    }
}
function openNewImage(file, imgText) {
    if (file.lang == 'no-popup') return;
    picfile = new Image();
    picfile.src = (file.src);
    width = picfile.width;
    height = picfile.height;

    if (imgText != '' && height > 0) {
        height += 40;
    }
    else if (height == 0) {
        height = screen.height;
    }

    winDef = 'status=no,resizable=yes,scrollbars=no,toolbar=no,location=no,fullscreen=no,titlebar=yes,height='.concat(height).concat(',').concat('width=').concat(width).concat(',');
    winDef = winDef.concat('top=').concat((screen.height - height) / 2).concat(',');
    winDef = winDef.concat('left=').concat((screen.width - width) / 2);
    newwin = open('', '_blank', winDef);

    newwin.document.writeln('<style>a:visited{color:blue;text-decoration:none}</style>');
    newwin.document.writeln('<body topmargin="0" leftmargin="0" marginheight="0" marginwidth="0">');
    newwin.document.writeln('<div style="width:100%;height:100%;overflow:auto;"><a style="cursor:pointer" href="javascript:window.close()"><img src="', file.src, '" border=0></a>');
    if (imgText != '') {
        newwin.document.writeln('<div align="center" style="padding-top:5px;font-weight:bold;font-family:arial,Verdana,Tahoma;color:blue"><a style="cursor:pointer" href="javascript:window.close()">', imgText, '</a></div></div>');
    }
    newwin.document.writeln('</body>');
    newwin.document.close();
}

/*Pop Up Xem chi tiet*/
function chitiet_edithpc(body, _tomtatEdit, txt_TieudeEdit, tieudephu, imageThum, tacgia) {
    //alert(body);
    var vWidth = screen.width;
    var vHeight = screen.height;
    winDef = 'scrollbars=yes,scrolling=yes,location=no,toolbar=no,height='.concat(vHeight).concat(',').concat('width=').concat(vWidth).concat(',');
    winDef = winDef.concat('top=').concat((screen.height - vHeight) / 2).concat(',');
    winDef = winDef.concat('left=').concat((screen.width - (vWidth)) / 2);
    newwin = open('', '_blank', winDef);

    /*Header*/
    newwin.document.writeln('<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">');
    newwin.document.writeln('<html xmlns="http://www.w3.org/1999/xhtml" ><head>');
    newwin.document.writeln('<title>XEM BÀI VIẾT</title>');
    newwin.document.writeln('<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />');
    newwin.document.writeln('<link href="' + pagesite + '/View/Layout/Styles.css" rel="stylesheet" type="text/css" />');
    newwin.document.writeln('<link href="' + pagesite + '/View/Layout/menu.css" rel="stylesheet" type="text/css" />');
    newwin.document.writeln('<link href="' + pagesite + '/View/Layout/jcarousel.css" rel="stylesheet" type="text/css" />');
    newwin.document.writeln('<link href="' + pagesite + '/View/Layout/simple-scroll.css" rel="stylesheet" type="text/css" />');
    newwin.document.writeln('<link href="' + pagesite + '/View/Layout/fonts.css" rel="stylesheet" type="text/css" />');


    newwin.document.writeln(' </head>');
    /*End Header*/
    /*BODY*/
    newwin.document.writeln('<body>');
    newwin.document.writeln('<div id="wrapper">');
    newwin.document.writeln(' <div id="main"><div class="pagel" style="margin-left: 120px;">');
    newwin.document.writeln('<div class="p-news pdt15"><div class="detail">');
    newwin.document.writeln('<div class="title">');
    //newwin.document.writeln('<!--Tieu De Phu--> <span>' + txt_TieudeEdit + '</span></div>');
    //newwin.document.writeln('<div class="time"><span><!--Ngay Thang Nam--></span></div>');
    newwin.document.writeln(txt_TieudeEdit);
    newwin.document.writeln('</div>');
    //
    newwin.document.writeln('<div class="func"><span class="time"><!--Ngay Thang Nam--></span>');
    newwin.document.writeln('<div class="share"><a href="javascript:window.print();" class="print" style="font-weight:bold;">IN BÀI VIẾT</a></div></div>');
    //SAPO
    newwin.document.writeln('<div class="sapo">' + _tomtatEdit + '</div>');
    // END
    // BODY
    newwin.document.writeln('<div class="content">');
    newwin.document.writeln(body);
    newwin.document.writeln('</div>');
    // END
    newwin.document.writeln('<div class="author">' + tacgia + '<!--TAC GIA--></div>');

    newwin.document.writeln('</div> </div></div></div></div> </body></html>');

    newwin.document.close();
}
/*end*/
