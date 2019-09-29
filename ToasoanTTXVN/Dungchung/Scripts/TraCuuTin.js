

function Login() {
    $.ajax({
        type: "POST",
        url: "Tracuutin.asmx/Login",
        data: "{IpClient:'" + _IpClient + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        cache: false,
        success: function(response) {
            _Ticket = response.d;
            LoadDDLTypes();
        },
        error: function(response) {
            //alert("Cập nhật không thành công !!");
        }
    });
};
function LoadDDLTypes() {
    $("#headerNews").show();
    $("#headerPhotos").hide();
    $.ajax({
        type: "POST",
        url: "Tracuutin.asmx/GetListTypes",
        data: "{Ticket:'" + _Ticket + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        cache: false,
        success: function (response) {
            var _option = "<select id='ddlTypes' class='inputtext' onchange='LoadDDLProducts();' style='width:90%' >";
            jQuery.each(response.d, function (rec) {
                _option += '<option value="' + this.TypeID + '">' + this.TypeName + '</option>';
            });
            _option += "</select>";
            $("#spTypes").html(_option);
            LoadDDLProducts();
        },
        error: function (response) {
            //alert("Cập nhật không thành công !!");
        }
    });
};
function LoadDDLProducts() {
    $("#txtTieuDe").val('');
    _TypeID = $("#ddlTypes").val();
    $.ajax({
        type: "POST",
        url: "Tracuutin.asmx/GetListProducts",
        data: "{Ticket:'" + _Ticket + "', TypeID:'" + _TypeID + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        cache: false,
        success: function (response) {
            var _option = "<select id='ddlProducts' class='inputtext' onchange='LoadDDLLanguages();' style='width:90%;' >";
            jQuery.each(response.d, function (rec) {
                _option += '<option value="' + this.ProductID + '">' + this.ProductName + '</option>';
            });
            _option += "</select>";
            $("#spProducts").html(_option);
            LoadDDLLanguages();
        },
        error: function (response) {
            //alert("Cập nhật không thành công !!");
        }
    });
};
function LoadDDLLanguages() {
    $("#txtTieuDe").val('');
    var _ProductID = $("#ddlProducts").val();
    _TypeID = $("#ddlTypes").val();
    $.ajax({
        type: "POST",
        url: "Tracuutin.asmx/GetListLanguages",
        data: "{Ticket:'" + _Ticket + "', TypeID:'" + _TypeID + "', ProductID:'" + _ProductID + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        cache: false,
        success: function (response) {
            var _option = "<select id='ddlLanguages' class='inputtext' onchange='LoadDDLCategorys();' style='width:60%;' >";
            jQuery.each(response.d, function (rec) {
                _option += '<option value="' + this.LanguageID + '">' + this.LanguageName + '</option>';
            });
            _option += "</select>";
            $("#spLanguages").html(_option);
            LoadDDLCategorys()
        },
        error: function (response) {
            //alert("Cập nhật không thành công !!");
        }
    });
};
function LoadDDLCategorys() {
    $("#txtTieuDe").val('');
    var _ProductID = $("#ddlProducts").val();
    var _LanguageID = $("#ddlLanguages").val();
    _TypeID = $("#ddlTypes").val();
    $.ajax({
        type: "POST",
        url: "Tracuutin.asmx/GetListCategorys",
        data: "{Ticket:'" + _Ticket + "', TypeID:'" + _TypeID + "', ProductID:'" + _ProductID + "', LanguageID:'" + _LanguageID + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        cache: false,
        success: function (response) {
            var _option = "<select id='ddlCategorys' class='inputtext' style='width:95%;' >";
            jQuery.each(response.d, function (rec) {
                _option += '<option value="' + this.CategoryID + '">' + this.CategoryName + '</option>';
            });
            _option += "</select>";
            $("#spCategorys").html(_option);
        },
        error: function (response) {
            //alert("Cập nhật không thành công !!");
        }
    });
};
function LoadData(pageindex, totalpages) {
    if (totalpages != '') {
        var page = parseInt(pageindex);
        var total = parseInt(totalpages);
        if (page <= total) {
        }
        else {
            alert('Không có trang ' + page);
            pageindex = total;
        }
    }
    $("#divcontent").html('<tr class="GridBorderVerSolid" style="border:0px !important"><td><div style="text-align:center;width:100%;border:0px"><img style="border:0px;width:500px;height:266px" src="loading-gif-animation.gif" /></div></td></tr>');
    _TypeID = $("#ddlTypes").val();
    var _ProductID = $("#ddlProducts").val();
    var _LanguageID = $("#ddlLanguages").val();
    var _CategoryID = $("#ddlCategorys").val();
    var _FromDate = $("#ctl00_MainContent_txt_FromDate").val();
    var _ToDate = $("#ctl00_MainContent_txtToDate").val();
    var _TuKhoa = encodeURIComponent($("#txtTieuDe").val());
    if (_FromDate == '') {
        $("#ctl00_MainContent_txt_FromDate").focus();
        document.getElementById("txt_FromDate").style.borderColor = "red";
    }
    if (_ToDate == '') {
        $("#ctl00_MainContent_txtToDate").focus();
        document.getElementById("txtToDate").style.borderColor = "red";
    }
    var pagesize;
    if (_TypeID == 1)
        pagesize = 15;
    else
        pagesize = 10;
    if (_TypeID == 1) {
        $("#headerNews").show();
        $("#headerPhotos").hide();
    } else {
        $("#headerNews").hide();
        $("#headerPhotos").show();
    }
    $.ajax({
        type: "POST",
        url: "Tracuutin.asmx/BindGridData",
        data: "{Ticket:'" + _Ticket + "',TypeID:'" + _TypeID + "',ProductID:'" + _ProductID + "',LanguageID:'" + _LanguageID + "',CategoryID:'" + _CategoryID + "',FromDate:'" + _FromDate + "',ToDate:'" + _ToDate + "', TuKhoa:'" + _TuKhoa + "',PageIndex:'" + pageindex + "',PageSize:'" + pagesize + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function(response) {
            var data = response.d;
            var strHtml = "";
            jQuery.each(data, function(rec, value) {
                if (_TypeID == 1) {
                    if (rec == 'ListNews') {
                        for (var i = 0; i < value.length; i++) {
                            strHtml += "<tr class='GridBorderVerSolid' style='padding : 0px !important' onmouseout=\"this.style.backgroundColor=currColor\" onmouseover=\"currColor=this.style.backgroundColor;this.style.backgroundColor='#B9E0E9'\" >";
                            strHtml += "<td align='left' style='width:65%'><a style='text-decoration:none' class='linkGridForm' href='javascript:void(0);'  onclick='detail(" + value[i].ID + "," + value[i].ProductID + ");'>" + decodeURIComponent(value[i].Title) + "</a></td>";
                            strHtml += "<td align='left' style='width:15%'>" + value[i].Author + "</td>";
                            strHtml += "<td align='center' style='width:15%'>" + value[i].DateCreate + "</td>";
                            strHtml += "<td align='center' style='width:5%'><img onclick='detail(" + value[i].ID + "," + value[i].ProductID + ");' src='../Dungchung/Images/view.gif' border='0' alt='Xem' onmouseover='(window.status=''); return true' style='cursor: pointer;' title='Xem'></td></tr>";
                        }
                    }
                } else {
                    if (rec == 'ListPhotos') {
                        for (var i = 0; i < value.length; i++) {

                            //alert(value[i].ImageThumbString);
                            strHtml += "<tr class='GridBorderVerSolid' style='padding : 0px !important' onmouseout=\"this.style.backgroundColor=currColor\" onmouseover=\"currColor=this.style.backgroundColor;this.style.backgroundColor='#B9E0E9'\" >";
                            strHtml += "<td align='center' style='width:2%'><input id='optSelect' title='Chọn ảnh muốn lấy về hệ thống' class='idList' type='checkbox' runnat='server' value='" + value[i].ID + "'/> </td>";
                            strHtml += "<td align='center' style='width:12%;position: relative;'>";
                            strHtml += "<div style='display:inline;cursor:pointer' title='Lưu ảnh'>";
                            //strHtml += "<img style='width:120px !important;'";
                            //strHtml += " src='../TraCuu/Handler.ashx?id=" + value[i].ID + "&ticket=" + _Ticket + "&productID=" + value[i].ProductID + "'/>";
                            strHtml += "<img style='width:120px !important;'";
                            strHtml += " src='data:image/jpeg;base64," + value[i].ImageThumbString + "'/>";

                            //strHtml += "<img src='../Images/download.png' style='width:26px; height:26px; float: right;position: absolute;right: 0px;top: 0px;'";
                            //strHtml += " onclick=\"DownloadImageResult('" + value[i].ID + "');\" title='Lưu ảnh' /></div></td>";
                            strHtml += "<div class='downloadIMG' title='Lưu ảnh về máy tính' onclick=\"DownloadImageResult('" + value[i].ID + "','" + value[i].ProductID + "','" + _Ticket + "');\"></div>"
                            //strHtml += "<br><img src='../Images/Icons/TaiVe.gif' style='border:0px;' title='Lưu ảnh về máy tính' onclick=\"DownloadImageResult('" + value[i].ID + "','" + value[i].ProductID + "','" + _Ticket + "');\"";
                            strHtml += "<td align='left' style='width:18%'>" + decodeURIComponent(value[i].Title) + "</td>";
                            strHtml += "<td align='left' style='width:42%'>" + decodeURIComponent(value[i].Summary) + "</td>";
                            strHtml += "<td align='left' style=' text-align:center;width:9%'>" + value[i].CategoryName + "</td>";
                            strHtml += "<td align='left' style=' text-align:center;width:7%'>" + value[i].Author + "</td>";
                            //strHtml += "<td align='left' style=' text-align:center;width:5%'>" + value[i].Nguon_Anh + "</td>";
                            strHtml += "<td  align='center' style=' text-align:center;width:10%'>" + value[i].DateCreate + "</td>";
                            strHtml += "</tr>";
                        }
                    }
                }
            });
            $("#divcontent").html(strHtml);
            //phan trang
            var _totalRecords = data.TotalRecords;
            var dv_pageNav = ".pageNav";
            var loadData = "LoadData";
            Pager(pageindex, pagesize, _totalRecords, dv_pageNav, loadData);
        },
        error: function(msg) {
            $("#divcontent").html("");
            $(".pageNav").html("");
        }
    });
}
function detail(ID, ProductID) {
    var urldetail = "../View/ChiTietTin.aspx?Ticket=" + _Ticket + "&ProductID=" + ProductID + "&ID=" + ID;
    window.open(urldetail, '_blank');
}
//Download photos
function DownloadImageResult(imgID, productID, ticket) {
    try {
        if (imgID == '') {
            var count = 1;
            var id;
            var id_list = $(".idList");
            for (var i = 0; i < id_list.length; i++) {
                id = id_list[i];
                if (id.checked) {
                    if (imgID == '') {
                        imgID = id.value;
                    } else {
                        imgID += "," + id.value;
                    }

                } else {
                    //count++
                    if (count++ == 10) {
                        alert("Bạn hãy chọn ảnh muốn lấy về hệ thống");
                    }
                }
            }
            if (imgID != '') {
                //save file to server
                //alert(ticket);
                Download(imgID, $("#ddlProducts").val(), _Ticket);
            }
        } else {
            //save file to computer
            //alert(imgID);
            //CheckUrlImg(productID, imgID);
            if (confirm("Bạn có muốn lưu ảnh về máy không")) {
                window.location = 'SaveImgFromComputer.ashx?imgID=' + imgID + '&Ticket=' + ticket + '&ProductID=' + productID + "&userID=" + _userid + "&userName=" + _username;
            }
        }
    }
    catch (err) {

    }
}
function Download(imgID, productID, ticket) {
    //alert(ticket); alert(productID); alert(imgID);
    try {
        if (confirm("Bạn có muốn lấy ảnh về hệ thống không")) {
            $.ajax({
                type: "POST", //object Ticket, object ProductID, object ID
                url: "TraCuuTin.asmx/Downloadimages",
                data: "{Ticket:'" + ticket + "', ProductID:'" + productID + "',ID:'" + imgID + "',MenuID:'" + _menuid + "', UserID:'" + _userid + "',UserName:'" + _username + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function(msg) {
                    alert(msg.d);
                },
                error: function(msg) {
                    alert("Error:" + msg.d);
                }
            });
        }
    } catch (err) {
    }
}
//Kiem tra ảnh
function CheckUrlImg(_ProductID, _ID) {
    $.ajax({
        type: "POST", //object Ticket, object ProductID, object ID
        url: "TraCuuTin.asmx/Check_Url_Img",
        data: "{Ticket:'" + _Ticket + "',ProductID:'" + _ProductID + "',ID:'" + _ID + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function(msg) {
            //alert(msg);
            if (msg.d == 'success') {
                if (confirm("Bạn có muốn lưu ảnh về máy không")) {
                    window.location = 'SaveImgFromComputer.ashx?imgID=' + _id + '&Ticket=' + _Ticket + '&ProductID=' + _ProductID + '&menuid=' + _menuid; //
                }
            }
            //else {
            //    alert(msg.d);
            //}
        },
        error: function(msg) {
            //alert("Error:" + msg);
        }
    });
}
//strHtml += "<td align='center' style='width:10%'>";
//                        strHtml += "<a target='_blank' href='../../View/ChiTietTin.aspx?Ticket=" + _Ticket + "&ProductID=" + value[i].ProductID + "&ID=" + value[i].ID + ") %>' />";
//                        strHtml += "<img src='../Images/view.gif' border='0' alt='Xem' onmouseover='(window.status=''); return true' style='cursor: pointer;' title='Xem'></td>
function LoadContentNews(id, productID) {
    $.ajax({
        type: "POST",
        url: "Tracuutin.asmx/GetContentNewsByID",
        data: "{Ticket:'" + _Ticket + "',ProductID:'" + productID + "',ID:'" + id + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            var data = msg.d; //eval(msg.d);
            var content = decodeURIComponent(data.Content);
            if (_check == 'pc')
                window.opener.InsertImage(ck_id, content);
            else
                window.opener.InsertRadEditer(ck_id, content);
            //INSERT LOG
            InsertLog(data.Title, id);
            window.close();
        },
        error: function (msg) {
        }
    });
};

//INSERT LOG
function InsertLog(_tieude, _id) {
    var _ProductID = $("#ddlProducts").val();
    $.ajax({
        type: "POST",
        url: "Tracuutin.asmx/InsertLogTin", //object Ticket, object ProductID, object ID, object MenuID, object UserID, object UserName
        data: "{Ticket:'" + _Ticket + "',ProductID:'" + _ProductID + "',ID:'" + _id + "',MenuID:'" + _menuid + "',UserID:'" + _userid + "',UserName:'" + _username + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function(msg) {
            //alert(msg);
        },
        error: function(msg) {
            //alert("error");
        }
    });
}
//END
function splitString(_content) {
    var _key = "";
    var _leng = _content.length;
    var checkand = 0;
    var _continue = 0;
    for (var i = 0; i < _leng; i++) {
        if (_content[i] == ' ' && _continue == 0) {
            checkand = 0;
            for (var j = i + 1; j < _leng; j++) {
                if (_content[j] == ' ') {
                }
                else if (_content[j] == '+') {
                    checkand = 1;
                }
                else if (_content[j] != ' ' && _content[j] != '+') {
                    if (checkand == 0)
                        _key = _key + "\" OR \"";
                    else
                        _key = _key + "\" and \"";
                    i = j - 1;
                    checkand = 0;
                    break;
                }
            }
        }
        else if (_content[i] == '+') {
            for (var j = i + 1; j < _leng; j++) {
                if (_content[j] == ' ' || _content[j] == '+') {
                }
                else {
                    _key = _key + "\" and \"";
                    i = j - 1;
                    break;
                }
            }
        }
        else if (_content[i] == '"' && _continue == 0) {
            _continue = 1;
        }

        else if (_content[i] == '"' && _continue == 1) {
            _continue = 0;
            if (i < _leng - 1 && _content[i + 1] != ' ' && _content[i + 1] != '+') {
                _key = _key + "\" OR \"";
            }
        }
        else {
            checkand = 0;
            _key = _key + _content[i];
        }
    }
    _key = _key + "";
    return _key;
}
function setDate() {
    var currentDate = new Date();
    var month = currentDate.getMonth() + 1;
    var day = currentDate.getDate();
    var year = currentDate.getFullYear();
    var date = day + "/" + month + "/" + year;
    var _txt_FromDate = $("#txt_FromDate").val(date);
    var _txtToDate = $("#txtToDate").val(date);
}

function valComments_ClientValidate(source, args) {
    var tdate = args.Value
    var _date = tdate.split("/");
    var dd = _date[0];
    var dd = new Number(dd);
    var mm = _date[1];
    var mm = new Number(mm);
    var yyyy = _date[2];
    var yyyy = new Number(yyyy);
    var today = new Date();
    var testyyyy = yyyy % 4;
    if (mm < 13 && mm > 0) {
        if (mm == 1 || mm == 3 || mm == 5 || mm == 7 || mm == 8 || mm == 10 || mm == 12) {
            if (dd >= 1 && dd <= 31) {
                args.IsValid = true;
            }
            else {
                args.IsValid = false;

            }
        }
        else if (mm == 2) {
            if (testyyyy == 0 && dd <= 29 && dd >= 1)
            { args.IsValid = true; }

            else if (dd <= 28 && dd >= 1) {
                args.IsValid = true;

            }
            else { args.IsValid = false; }


        }
        else {
            if (dd >= 1 && dd <= 30)
            { args.IsValid = true }
            else { args.IsValid = false }
        }
    }
    else
        args.IsValid = false;
}