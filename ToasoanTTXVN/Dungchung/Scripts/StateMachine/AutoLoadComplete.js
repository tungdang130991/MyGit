function AutoCompleteTen() {
    $('#ctl00_MainContent_GVDoituong_ctl05_txt_Tendoituong').autocomplete({
        //autoFocus: true,
        source: function(request, response) {
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                url: "SaveQuytrinh.asmx/AutoCompleteTenDT",
                //data: "{'TenDT':'" + document.getElementById('txtTenDT').value + "'}",
                data: "{'TenDT':'" + request.term + "'}",
                dataType: "json",
                success: function(data) {
                    if (data == 'Tên đối tượng này chưa tồn tại !!') {
                        response(data);
//                        $('#lblTenDoiTuong').css('color', '#1E90FF');
//                        $('#txtTenDT').css('color', '#000000');
//                        $('#txtTenDT').css('border', '1px solid #DCDCDC');
                    }
                    else {
                        response(data);
                        $('.ui-corner-all').css('color', '#ff0000');
//                        $('#lblTenDoiTuong').css('color', '#ff0000');
//                        $('#txtTenDT').css('color', '#ff0000');
//                        $('#txtTenDT').css('border', '1px solid #ff0000');
                    }
                },
                error: function(result) {
                    alert("Error Occurred");
                }
            });
        }
    });
}
function AutoCompleteMa() {
    $('#ctl00_MainContent_GVDoituong_ctl05_txt_MaDoituong').autocomplete({
        //autoFocus: true,
        source: function(request, response) {
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                url: "SaveQuytrinh.asmx/AutoCompleteMaDT",
                //data: "{'MaDT':'" + document.getElementById('txtMaDT').value + "'}",
                data: "{'MaDT':'" + request.term + "'}",
                dataType: "json",
                success: function(data) {
                    if (data == 'Mã đối tượng này chưa tồn tại !!') {
                        response(data);
//                        $('#lblMaDoiTuong').css('color', '#1E90FF');
//                        $('#txtMaDT').css('color', '#000000');
//                        $('#txtMaDT').css('border', '1px solid #DCDCDC');
                    }
                    else {
                        response(data);
                        $('.ui-corner-all').css('color', '#ff0000');
//                        $('#lblMaDoiTuong').css('color', '#ff0000');
//                        $('#txtMaDT').css('color', '#ff0000');
//                        $('#txtMaDT').css('border', '1px solid #ff0000');
                    }
                },
                error: function(result) {
                    alert("Error Occurred");
                }
            });
        }
    });
} 
function AutoCompleteStt() {
    $('#ctl00_MainContent_GVDoituong_ctl05_txt_STT').autocomplete({
        //autoFocus: true,
        source: function(request, response) {
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                url: "SaveQuytrinh.asmx/AutoCompleteStt",
                //data: "{'Stt':'" + document.getElementById('txtSTT').value + "'}",
                data: "{'Stt':'" + request.term + "'}",
                dataType: "json",
                success: function(data) {
                    if (data == 'Số thứ tự này chưa tồn tại !!') {
                        response(data);
//                        $('#lblSoTT').css('color', '#1E90FF');
//                        $('#txtSTT').css('color', '#000000');
//                        $('#txtSTT').css('border', '1px solid #DCDCDC');
                    }
                    else {
                        response(data);
                        $('.ui-corner-all').css('color', '#ff0000');
//                        $('#lblSoTT').css('color', '#ff0000');
//                        $('#txtSTT').css('color', '#ff0000');
//                        $('#txtSTT').css('border', '1px solid #ff0000');
                    }
                },
                error: function(result) {
                    alert("Error Occurred");
                }
            });
        }
    });
}