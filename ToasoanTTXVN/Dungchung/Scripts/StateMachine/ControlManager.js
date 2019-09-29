
var menuID = $("#ctl00_MainContent_lblMenuID");
var ipAddress = $("#ctl00_MainContent_lblIPAddress");
var maanpham = $("#ctl00_MainContent_lblMaAnPham");
//Cập nhật vị trí các đối tượng
function getPosition() {
    if (confirm("Bạn có muốn cập nhập vị trí các đối tượng ko ?")) {
        var _left;
        var _top;

        var maDT = $(".w");
        for (var i = 0; i < maDT.length; i++) {
                var _id;
                _id = '#' + maDT[i].id;
                _left = $(_id).css('left');
                _top = $(_id).css('top');
                $.ajax({ type: "POST",
                url: "SaveQuytrinh.asmx/UpdatePosition",
                    data: "{MaDoituong:'" + maDT[i].id + "', MaAnpham:'" + maanpham.val() + "', Left:'" + _left + "', Top:'" + _top + "',MenuID:'" + menuID.val() + "',IpAddress:'" + ipAddress.val() + "'}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    cache: false,
                    success: function(response) {
                    $('#msg').html(response.d);
                      //  alert("Thành công !!");
                    },
                    error: function(response) {
                        alert("Cập nhật vị trị không thành công !!");
                    }
                });
        }
        return true;
    }
    else
        return false;
}

//Tạo kết nối giữa các đối tượng
function Insert(DTGui, DTNhan) {
    if (confirm("Bạn có muốn tạo kết nối từ '" + DTGui + "' đến '" + DTNhan + "' không ?")) {
        $.ajax({ type: "POST",
            url: "SaveQuytrinh.asmx/Save",
            data: "{Ma_Doituong_Gui:'" + DTGui + "', Ma_Doituong_Nhan:'" + DTNhan + "',MaAnpham: '" + maanpham.val() + "',MenuID:'" + menuID.val() + "',IpAddress:'" + ipAddress.val() + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            cache: false,
            success: function(response) {
                if (response.d == 'ConnectSuccess') {
                    $("#ctl00_MainContent_demo").load('LoadDoiTuong.aspx?maanpham=' + maanpham.val() + '&random=' + Math.random());
                }
                else if (response.d == 'SaveError') {
                    alert("Kết nối đã tồn tại !");
                    $("#ctl00_MainContent_demo").load('LoadDoiTuong.aspx?maanpham=' + maanpham.val() + '&random=' + Math.random());
                    //document.getElementById('ctl00_MainContent_btnReset').click();
                }
                else if (response.d == 'ConnectError') {
                    alert("Không thể tạo kết nối !");
                    $("#ctl00_MainContent_demo").load('LoadDoiTuong.aspx?maanpham=' + maanpham.val() + '&random=' + Math.random());
                    //document.getElementById('ctl00_MainContent_btnReset').click();
                }
            },
            error: function(response) {
                alert("Kết nối đối tượng không thành công !!");
            }
        });
        return true;
    }
    else
        return false;
}
//Xóa kết nối giữa các đối tượng
function Remove(DTGui, DTNhan) {
    $.ajax({ type: "POST",
        url: "SaveQuytrinh.asmx/DeleteQuytrinh",
        data: "{Ma_Doituong_Gui:'" + DTGui + "', Ma_Doituong_Nhan:'" + DTNhan + "',MaAnpham: '" + maanpham.val() + "',MenuID:'" + menuID.val() + "',IpAddress:'" + ipAddress.val() + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        cache: false,
        success: function(response) {
            //$('#msg').html(response.d);
        },
        error: function(response) {
            alert("Xóa không thành công !!");
        }
    });
}

var pnlAdd = document.getElementById('ctl00_MainContent_pnlAddNew');
//Hiển thị form cập nhật
function add_Form(ID, MaDT, TenDT, STT) {
    var _id = 0;

    if (ID == null) {
        pnlAdd.style.display = 'inline';
        $('#txtID').val(_id);
        $('#txtTenDT').val('');
        $('#txtMaDT').val('');
        $('#txtSTT').val('');
        $('#txtTenDT').focus();
    }
    else {
        pnlAdd.style.display = 'inline';
        $('#txtID').val(ID);
        $('#txtTenDT').val(TenDT);
        $('#txtMaDT').val(MaDT);
        $('#txtSTT').val(STT);
        $('#txtTenDT').focus();
//        $('#lblTenDoiTuong').css('color', '#1E90FF');
//        $('#txtTenDT').css('color', '#000000');
//        $('#txtTenDT').css('border', '1px solid #DCDCDC');
//        $('#lblMaDoiTuong').css('color', '#1E90FF');
//        $('#txtMaDT').css('color', '#000000');
//        $('#txtMaDT').css('border', '1px solid #DCDCDC');
//        $('#lblSoTT').css('color', '#1E90FF');
//        $('#txtSTT').css('color', '#000000');
//        $('#txtSTT').css('border', '1px solid #DCDCDC');
    }
}
//Thoát form cập nhật
function exit_Form() {
    pnlAdd.style.display = 'none';
//    $('#txtTenDT').val('');
//    $('#txtMaDT').val('');
//    $('#txtSTT').val('');
//    $('#lblTenDoiTuong').css('color', '#1E90FF');
//    $('#txtTenDT').css('color', '#000000');
//    $('#txtTenDT').css('border', '1px solid #DCDCDC');
//    $('#lblMaDoiTuong').css('color', '#1E90FF');
//    $('#txtMaDT').css('color', '#000000');
//    $('#txtMaDT').css('border', '1px solid #DCDCDC');
//    $('#lblSoTT').css('color', '#1E90FF');
//    $('#txtSTT').css('color', '#000000');
//    $('#txtSTT').css('border', '1px solid #DCDCDC');
    
}
//Cập nhập đối tượng
function insertDoiTuong(ID, MaDT, TenDT, Stt) {
    var _stt = $(Stt).val();
    var _id = $(ID).val();
    var _maDT = $(MaDT).val();
    var _tenDT = $(TenDT).val();
    if (_maDT != "" && _tenDT != "" && _stt != "") {
        if (confirm("Bạn có muốn cập nhật đối tượng ko ?")) {
            var _maDT = $(MaDT).val();
            var _tenDT = $(TenDT).val();
            var _stt = $(Stt).val();
            $.ajax({ type: "POST",
                url: "SaveQuytrinh.asmx/InsertDoiTuong",
                data: "{ID:'" + _id + "', MaDoituong:'" + _maDT + "', TenDoiTuong:'" + _tenDT + "', Stt:'" + _stt + "', MenuID:'" + menuID.val() + "',IpAddress:'" + ipAddress.val() + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                cache: false,
                success: function(response) {
                    if (response == 'AddSuccess') {
                        //document.getElementById('ctl00_MainContent_btnReset').click();
                        $("#ctl00_MainContent_demo").load('LoadDoiTuong.aspx?maanpham=' + maanpham.val() + '&random=' + Math.random());
                    }
                    else if (response == 'EditSuccess') {
                        document.getElementById('ctl00_MainContent_btnReset').click();
                        //$("#ctl00_MainContent_demo").load('LoadDoiTuong.aspx?maanpham=' + maanpham.val() + '&random=' + Math.random());
                    }
                    else if (response == 'TenDTError') {
                        alert("Tên đối tượng đã tôn tại. Bạn hãy nhập lại Tên đối tượng !");
                        $(TenDT).val('');
                        $(TenDT).focus(); 
                        $('#lblTenDoiTuong').css('color', '#ff0000');
                        $('#txtTenDT').css('border', '1px solid #ff0000');
                    }
                    else if (response == 'MaDTError') {
                        alert("Mã đối tượng đã tôn tại. Bạn hãy nhập lại Mã đối tượng !");
                        $(MaDT).val('');
                        $(MaDT).focus();
                        $('#lblMaDoiTuong').css('color', '#ff0000');
                        $('#txtMaDT').css('border', '1px solid #ff0000');
                    }
                    else if (response == 'STTError') {
                        alert("Số thứ tự này đã tồn tại. Bạn hãy nhập lại Số thứ tự !");
                        $(Stt).val('');
                        $(Stt).focus();
                        $('#lblSoTT').css('color', '#ff0000');
                        $('#txtSTT').css('border', '1px solid #ff0000');
                    }
                },
                error: function(response) {
                    alert("Cập nhật đối tượng mới không thành công !!");
                }
            });
            return true;
        }
        else
            return false;
    }
//    else if (_maDT == "" && _tenDT == "" && _stt == "") {
//        alert("Bạn hãy nhập đầy đủ thông tin !");
//        $(TenDT).focus();
//    }
    else if (_tenDT == "") {
        alert("Bạn hãy nhập 'Tên đối tượng' !");
        $(TenDT).focus();
//        $('#lblTenDoiTuong').css('color', '#ff0000');
//        $('#txtTenDT').css('border', '1px solid #ff0000');
    }
    else if (_maDT == "") {
        alert("Bạn hãy nhập 'Mã đối tượng' !");
        $(MaDT).focus();
//        $('#lblMaDoiTuong').css('color', '#ff0000');
//        $('#txtMaDT').css('border', '1px solid #ff0000');
    }
    else if (_stt == "") {
        alert("Bạn hãy nhập 'Thứ tự' !");
        $(Stt).focus();
//        $('#lblSoTT').css('color', '#ff0000');
//        $('#txtSTT').css('border', '1px solid #ff0000');
    }
}
//Xóa đối tượng
function btnDelItem(id, name, maanpham) {
    if (confirm("Bạn có muốn xóa đối tượng '" + name + "' ra khỏi quy trình ko ?")) {
        $.ajax({ type: "POST",
            url: "SaveQuytrinh.asmx/Delete_DoiTuong_AnPham",
            data: "{ID:'" + id + "', Name:'" + name + "', Ma_Anpham:'" + maanpham + "', MenuID:'" + menuID.val() + "',IpAddress:'" + ipAddress.val() + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            cache: false,
            success: function(response) {
                //$("#ctl00_MainContent_demo").load('LoadDoiTuong.aspx?maanpham=' + maanpham.val() + '&random=' + Math.random());
                document.getElementById('ctl00_MainContent_btnReset').click();
            },
            error: function(response) {
                alert("Delete Error !!");
            }
        });
    }
}

