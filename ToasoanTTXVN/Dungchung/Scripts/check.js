function RemoveIDOrther(idkhac) {
    var elm = document.aspnetForm.elements;
    for (i = 0; i < elm.length; i++) {
        if (elm[i].type == "checkbox" && elm[i].checked && elm[i].id != idkhac)
            return true;
    }
    return false;
}
function CheckItem() {
    var allitem = $('.checkitem').find("input");
    var check = false;
    $.each(allitem, function(index, key) {
        var item = $(key);
        var itemchek = item.is(":checked");
        if (itemchek)
            check = true;
    });
    var divcontrol = $('#divbutton');
    if (check) {
        divcontrol.css("display", "");
    }
    else
        divcontrol.css("display", "none");
}
function ConfirmQuestion(_question, _id) {
    var bol;
    bol = RemoveIDOrther(_id);
    if (bol == true) {
        if (confirm(_question))
            return true;
        else return false;
    }
    else {
        alert("Bạn chưa chọn bản ghi nào!"); return false;
    }
}
function get_check_value(idkhac) {
    var elm = document.aspnetForm.elements;
    for (i = 0; i < elm.length; i++) {
        if (elm[i].type == "checkbox" && elm[i].checked && elm[i].id != idkhac)
            return true;
    }
    return false;
}
function CheckConfirmDelete(id) {
    var bol;
    bol = get_check_value(id);
    if (bol == true) {
        if (confirm("Bạn có chắc chắn muốn xóa?"))
            return true;
        else return false;
    }
    else {
        alert("Bạn chưa chọn bản ghi nào!"); return false;
    }
}
function CheckConfirmGuiTinbai(AlertMessage, id) {
    var bol;
    bol = get_check_value(id);
    if (bol == true) {
        if (confirm(AlertMessage))
            return true;
        else return false;
    }
    else {
        alert("Bạn chưa chọn bản ghi nào!"); return false;
    }
}
function closePopUpImagePV() {
    $find("ctl00_MainContent_ShowImagesPopup").hide();
    return false;
}
function CancelListPopUpDoiTuong() {
    $find("ctl00_MainContent_ModalPopupExtender1").hide();
    return false;
}
function HidepopupPV() {
    $find("ctl00_MainContent_PopupDoituong").hide();
    return false;
}
function search() {

    $get('ctl00_MainContent_btnSearch').click();

}
function closePopUpImage() {

    $find("ctl00_MainContent_ModalPupup").hide();
    return false;
}
function Hidepopup() {

    $find("ctl00_MainContent_ModalPopupDoiTuong").hide();

    return false;
}
function HidePopupNhuanbut() {
    $find("ctl00_MainContent_ModalPopupExtenderNhuanbut").hide();
    return false;
}
function HidePopupLanguages() {
    $find("ctl00_MainContent_ModalPopupLanguages").hide();
    return false;
}
function AutoCompleteSearch_Author(arr_Name, arr_ID) {
    var i = 0;
    if (arr_Name != '' && arr_ID != '') {
        var arr_ID_Name = arr_Name.split(",");
        var arr_ID_ID = arr_ID.split(",");
        for (i = 0; i < arr_ID_Name.length; i++) {
            var a = arr_ID_Name[i];
            var b = arr_ID_ID[i]
            Run(a, b);
        }
    }
}
function Run(a, b) {
    $(document).ready(function() {
        $('#' + a).autocomplete("SearchTacGia.ashx").result(
                function(event, data, formatted) {
                    if (data) { $('#' + b).val(data[1]); }
                    else { $('#' + b).val('0'); }
                });
    });
}