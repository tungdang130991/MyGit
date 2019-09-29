//Phan trang hungviet
function PagingHungViet(pageindex, pagesize, _totalRecords, dv_pageNav, loadData) {
    if (pageindex == 0) {
        pageindex = 1;
    }
    var htmlPaging = "";
    var _sotrang;
    _sotrang = eval(parseFloat(_totalRecords) / parseFloat(pagesize));
    //alert(_sotrang);
    if (_sotrang <= 0.5)
        _sotrang.split = 1;
    else if (_sotrang > 1) {
        var _st = _sotrang.toString().split(".");
        if (_st.length == 2) {
            _sotrang = parseFloat(_st[0]) + parseFloat(1);
        }
    }
    else _sotrang = eval(Math.round(_sotrang));
    var _trangsau = eval(parseFloat(pageindex) + parseFloat(1));
    var _trangtruoc = eval(parseFloat(pageindex) - parseFloat(1));
    var _tranghtai = _trangsau - 1;
    var _tongsotrang = _sotrang;
    var _denbanghi = _tranghtai * pagesize;
    var _tubanghi = _denbanghi - (pagesize - 1);
    if (_trangtruoc == 0) {
        if (_sotrang > 1) {
            htmlPaging += "<div class='DivPageNumber' style='display:inline'></div> <a style='text-decoration:none;' href='javascript:void(0);' onclick=\"" + loadData + "(" + _trangsau + ",'');\">></a> <a style='text-decoration:none;' href='javascript:void(0);' onclick=\"" + loadData + "(" + _tongsotrang + ",'');\">>></a> | Trang <b><input id='pageIndex' title='Nhập số trang' style='width:30px;height:13px;font-size:10px;text-align:center' type='text' value='" + _tranghtai + "' onchange=\"" + loadData + "(this.value," + _tongsotrang + ");\" onkeypress=\"return check_Number(this,event);\" />" + " / " + _tongsotrang + "</b> | Hiển thị <b>" + _tubanghi + " - " + _denbanghi + " / " + _totalRecords + "</b> bản ghi ";
        } else {
            htmlPaging += "Tổng số bản ghi: <b>" + _totalRecords + "</b>";
        }
    }
    else {
        if (_sotrang > 1) {
            if (_trangtruoc < _trangsau && _trangsau <= _sotrang) {
                htmlPaging += "<a style='text-decoration:none;' href='javascript:void(0);' onclick=\"" + loadData + "(1,'');\"><<</a> <a style='text-decoration:none;' href='javascript:void(0);' onclick=\"" + loadData + "(" + _trangtruoc + ",'');\"><</a> <div class='DivPageNumber' style='display:inline'></div> <a style='text-decoration:none;' href='javascript:void(0);' onclick=\"" + loadData + "(" + _trangsau + ",'');\">></a> <a style='text-decoration:none;' href='javascript:void(0);' onclick=\"" + loadData + "(" + _tongsotrang + ",'');\">>></a> | Trang <b><input id='pageIndex' title='Nhập số trang' style='width:30px;height:13px;font-size:10px;text-align:center' type='text' value='" + _tranghtai + "' onchange=\"" + loadData + "(this.value," + _tongsotrang + ");\" onkeypress=\"return check_Number(this,event);\" />" + " / " + _tongsotrang + "</b> | Hiển thị <b>" + _tubanghi + " - " + _denbanghi + " / " + _totalRecords + "</b> bản ghi ";
            }
            else if (_trangsau > _sotrang) {
                if (parseFloat(_totalRecords) < parseFloat(_denbanghi)) {
                    htmlPaging += "<a style='text-decoration:none;' href='javascript:void(0);' onclick=\"" + loadData + "(1,'');\"><<</a> <a style='text-decoration:none;' href='javascript:void(0);' onclick=\"" + loadData + "(" + _trangtruoc + ",'');\"><</a> <div class='DivPageNumber' style='display:inline'></div> | Trang <b><input id='pageIndex' title='Nhập số trang' style='width:30px;height:13px;font-size:10px;text-align:center' type='text' value='" + _tranghtai + "' onchange=\"" + loadData + "(this.value," + _tongsotrang + ");\" onkeypress=\"return check_Number(this,event);\" />" + " / " + _tongsotrang + "</b> | Hiển thị <b>" + _tubanghi + " - " + _totalRecords + " / " + _totalRecords + "</b> bản ghi ";
                }
                else {
                    htmlPaging += "<a style='text-decoration:none;' href='javascript:void(0);' onclick=\"" + loadData + "(1,'');\"><<</a> <a style='text-decoration:none;' href='javascript:void(0);' onclick=\"" + loadData + "(" + _trangtruoc + ",'');\"><</a> <div class='DivPageNumber' style='display:inline'></div> | Trang <b><input id='pageIndex' title='Nhập số trang' style='width:30px;height:13px;font-size:10px;text-align:center' type='text' value='" + _tranghtai + "' onchange=\"" + loadData + "(this.value," + _tongsotrang + ");\" onkeypress=\"return check_Number(this,event);\" />" + " / " + _tongsotrang + "</b> | Hiển thị <b>" + _tubanghi + " - " + _denbanghi + " / " + _totalRecords + "</b> bản ghi ";
                }
            }
        } else {
            htmlPaging += "Tổng số bản ghi: <b>" + _totalRecords + "</b>";
        }

    }
    //page number
    var htmlPagesNumber = "";
    var currentPage = parseInt(pageindex);
    var numberOfPagesToDisplay = 8;
    var start = 1;
    var end = _tongsotrang;
    if (_tongsotrang > numberOfPagesToDisplay) {
        var middle = parseInt(Math.ceil(numberOfPagesToDisplay / 2)) - 1;
        var below = (currentPage - middle);
        var above = (currentPage + middle);

        if (below < 4) {
            above = numberOfPagesToDisplay;
            below = 1;
        }
        else if (above > (_tongsotrang - 4)) {
            above = _tongsotrang;
            below = (_tongsotrang - numberOfPagesToDisplay + 1);
        }
        start = below;
        end = above;
    }
    if (start > 1) {
        htmlPagesNumber += "<a style='width:20px!important;height:13px;display: inline;' href='javascript:void(0);' onclick=\"" + loadData + "(1,'');\">1</a> ";
        if (start > 3) {
            htmlPagesNumber += "<a style='width:20px!important;height:13px;display: inline;' href='javascript:void(0);' onclick=\"" + loadData + "(1,'');\">2</a> ";
        }
        if (start > 2) {
            htmlPagesNumber += "...... ";
        }
    }

    for (var i = start; i <= end; i++) {
        if (i == currentPage || (currentPage <= 0 && i == 0)) {
            htmlPagesNumber += "<span class=\"current\">" + i + "</span> ";
        }
        else {
            htmlPagesNumber += "<a style='width:20px!important;height:13px;display: inline;' href='javascript:void(0);' onclick=\"" + loadData + "('" + i + "','');\">" + i + "</a> ";
        }
    }
    if (end < _tongsotrang) {
        if (end < _tongsotrang - 1) {
            htmlPagesNumber += "...... ";
        }
        if (_tongsotrang - 2 > end) {
            var _pagePrevEnd = parseInt(_tongsotrang) - 1;
            htmlPagesNumber += "<a style='width:20px!important;height:13px;display: inline;' href='javascript:void(0);' onclick=\"" + loadData + "('" + _pagePrevEnd + "','');\">" + _pagePrevEnd + "</a> ";
        }
        htmlPagesNumber += "<a style='width:20px!important;height:13px;display: inline;' href='javascript:void(0);' onclick=\"" + loadData + "('" + _tongsotrang + "','');\">" + _tongsotrang + "</a> ";
    }
    //
    $(dv_pageNav).html(htmlPaging);
    $(".DivPageNumber").html(htmlPagesNumber);
    $("#pageIndex").val(pageindex);
}
//