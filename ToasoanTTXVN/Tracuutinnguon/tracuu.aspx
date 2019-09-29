<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="tracuu.aspx.cs" Inherits="ToasoanTTXVN.Tracuutinnguon.tracuu" %>

<%@ Register TagPrefix="nbc" Namespace="HPCComponents.UI" Assembly="HPCComponents" %>
<%@ Import Namespace="HPCComponents" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>TRA CỨU TIN NGUỒN, TIN TƯ LIỆU </title>
    <link rel="stylesheet" type="text/css" href="../Dungchung/Style/style.css" />

    <script type="text/javascript" src='../Dungchung/Scripts/AutoFormatDatTime.js'></script>

    <script type="text/javascript" src="../Dungchung/Scripts/Lib.js"></script>

    <script type="text/javascript" src="../Dungchung/Scripts/jquery-1.4.2.js"></script>

    <script type="text/javascript">
        function insert_content(_value) {
            var editor = Window_GetDialogArguments(window);
            editor.FocusDocument();
            editor.PasteHTML(_value);

        }
        //Window_GetDialogArguments.js
        function Window_GetDialogArguments(win) {
            var top = win.top;
            try {
                var opener = top.opener;
                if (opener && opener.document._dialog_arguments)
                    return opener.document._dialog_arguments;
            }
            catch (x) {
            }
            if (top.document._dialog_arguments)
                return top.document._dialog_arguments;
            if (top.dialogArguments)
                return top.dialogArguments;
            return top.document._dialog_arguments;
        }
    </script>

</head>
<body>
    <form id="form2" runat="server">
    <div style="font-size: 12px; font-family: Tahoma;">
        <table cellpadding="0" cellspacing="0" border="0" width="100%">
            <tr>
                <td style="width: 100%; vertical-align: top; padding-right: 4px;">
                    <asp:Panel ID="plSearch" runat="server" Width="100%" CssClass="TitlePanel" GroupingText="Tìm kiếm"
                        BackColor="white" BorderStyle="NotSet">
                        <table border="0" cellpadding="1" cellspacing="1" style="width: 100%">
                            <tr>
                                <td style="width: 30%; text-align: center" class="Titlelbl">
                                    <select id="selTin" style="width: 250px; height: 22px;" onchange="LoadataDrop();">
                                        <option value="1" selected="selected">Tin nguồn </option>
                                        <option value="2">Tin tư liệu </option>
                                    </select>
                                </td>
                                <td style="width: 30%; text-align: center">
                                    <!--Chuyen Muc-->
                                    <span id="dropdownlistid"></span>
                                    <!--End-->
                                </td>
                                <td style="width: 40%; text-align: center" class="Titlelbl">
                                    <asp:Literal runat="server" ID="Literal3" Text="Từ khóa:"></asp:Literal>
                                    <input type="text" id="txtSearch" style="width: 250px;" />
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: center" class="Titlelbl">
                                    <div style="margin-top: 5px">
                                        <nbc:NetDatePicker ImageUrl="../Dungchung/Images/events.gif" ImageFolder="../Dungchung/scripts/DatePicker/Images"
                                            Height="16px" Width="150px" ScriptSource="../Dungchung/Scripts/datepicker.js"
                                            ID="txt_FromDate" runat="server" onKeyPress="AscciiDisable()" onfocus="javascript:vDateType='3'"
                                            onKeyUp="DateFormat(this,this.value,event,false,'3')" onBlur="DateFormat(this,this.value,event,true,'3')"></nbc:NetDatePicker>
                                        <asp:CustomValidator ID="CustomValidator1" runat="server" ErrorMessage="Ngày tháng ko tồn tại"
                                            ClientValidationFunction="valComments_ClientValidate" ControlToValidate="txt_FromDate"
                                            SetFocusOnError="true"></asp:CustomValidator>
                                    </div>
                                </td>
                                <td style="text-align: center" class="Titlelbl">
                                    <div style="margin-top: 5px">
                                        <asp:Literal runat="server" ID="Literal4" Text="Đến ngày"></asp:Literal>:
                                        <nbc:NetDatePicker ImageUrl="../Dungchung/Images/events.gif" ImageFolder="../Dungchung/scripts/DatePicker/Images"
                                            Height="16px" Width="150px" ScriptSource="<%=Global.ApplicationPath%>/Dungchung/Scripts/datepicker.js"
                                            ID="txtToDate" runat="server" onKeyPress="AscciiDisable()" onfocus="javascript:vDateType='3'"
                                            onKeyUp="DateFormat(this,this.value,event,false,'3')" onBlur="DateFormat(this,this.value,event,true,'3')"></nbc:NetDatePicker>
                                        <asp:CustomValidator ID="CustomValidator2" runat="server" ErrorMessage="Ngày tháng ko tồn tại"
                                            ClientValidationFunction="valComments_ClientValidate" ControlToValidate="txtToDate"
                                            SetFocusOnError="true"></asp:CustomValidator>
                                    </div>
                                </td>
                                <td style="text-align: right">
                                    <span id="ddlYear" style="float: left; margin-left: 5px;"></span><span class="buttononespan">
                                        <input type="button" id="btnSearchAdv" style="margin-right: 23px; margin-bottom: 1px;"
                                            class="buttonone" value="Tìm kiếm" onclick="LoadataContentSearch(1,'');" />
                                    </span>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="3" style="text-align: center; color: Red;">
                                    <input id="txtType" style="display: none" type="text" />
                                    <input id="txtYear" style="display: none" type="text" />
                                    <span id="msgTinNguon" style="display: none">Chú ý : Chỉ tìm kiếm trong năm.</span>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Panel ID="Panel1" runat="server" Width="100%" CssClass="TitlePanel" GroupingText="Danh sách tin bài"
                        BackColor="white" BorderStyle="NotSet">
                        <table width="100%" cellspacing="0" cellpadding="0" border="0">
                            <tr>
                                <td align="left">
                                    <%--<span id="divcontent"></span>--%>
                                    <table width="100%">
                                        <tr>
                                            <td class="GridBorderVerSolid" style="padding: 0px !important; border: 0px !important">
                                                <table cellspacing="0" cellpadding="4" rules="all" border="1" id="dgr_tintuc1" style="background-color: White;
                                                    border-color: #D9D9D9; border-width: 1px; border-style: None; width: 100%; border-collapse: collapse;">
                                                    <tr class="tbDataFlowList" style="color: White;">
                                                        <td align="center">
                                                            Tiêu đề
                                                        </td>
                                                        <td align="center" style="width: 15%;">
                                                            Ngày tạo
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="GridBorderVerSolid" style="padding: 0px !important; border: 0px !important">
                                                <table cellspacing="0" cellpadding="4" rules="all" border="1" id="divcontent" style="background-color: White;
                                                    border-color: #D9D9D9; border-width: 1px; border-style: None; width: 100%; border-collapse: collapse;">
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                    <div id="DivPaging" class="pageNav">
                                    </div>
                                </td>
                            </tr>
                            <tr style="text-align: center;">
                                <td>
                                    <input class="buttonClose" type="button" onclick="window.top.close()" />
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </td>
            </tr>
        </table>
        <asp:TextBox ID="txt_BlackList" runat="server" Style="display: none"></asp:TextBox>
    </div>
    </form>

    <script type="text/javascript">
        $(document).ready(function() {
            getDate();
            LoadataDrop();
        });
        function getDate() {
            var currentDate = new Date();
            var month = currentDate.getMonth() + 1;
            var day = currentDate.getDate();
            var year = currentDate.getFullYear();
            var date = day + "/" + month + "/" + year;
            
            var _txt_FromDate = $("#txt_FromDate").val(date);
            var _txtToDate = $("#txtToDate").val(date);
        }

        function LoadataDrop() {
            var _iType = $("#selTin").val();
            $("#txtType").val(_iType);
            if (_iType == 1) {
                $("#msgTinNguon").css("display", "inline");
                //set date
                var currentDate = new Date();
                var _year = currentDate.getFullYear();
                $("#txtYear").val(_year.toString());
                //
            }
            else {
                $("#msgTinNguon").css("display", "none");
            }
            var _blackList = $("#<%= txt_BlackList.ClientID %>").val();
            $.ajax({
                type: "POST",
                url: "<%=Global.ApplicationPath %>/ckeditor/plugins/insert_tinnguon/WebService.asmx/GetCategoris",
                data: "{iType:" + _iType + "}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function(msg) {
                    var data = msg;
                    var _option = "<select id='categorysid' style='width:250px;height:22px;' >";
                    jQuery.each(data, function(rec) {
                        if (_iType == 2 && this.CategoryID == _blackList) {

                        } else {
                            var _space = '<%= HttpUtility.HtmlDecode("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;") %>';
                            if (this.CategoryID == 0) {
                                _option += '<option style="font-weight:bold" value="' + this.CategoryID + '">' + this.CategoryName + '</option>';
                            } else {
                                _option += '<option value="' + this.CategoryID + '">' + _space + this.CategoryName + '</option>';
                            }
                        }
                    });
                    _option += "</select>";
                    $("#dropdownlistid").html(_option);
                },
                error: function(msg) {

                }
            });
        };
        function LoadataContentSearch(pageindex, totalpages) {
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
            $("#divcontent").html('<tr><td class="GridBorderVerSolid"><div style="text-align:center;width:100%;border:0px"><img style="border:0px;width:500px;height:266px" src="loading-gif-animation.gif" /></div></td></tr>');

            var _type = $("#selTin").val();
            var _category = $("#categorysid").val();
            var txtsearch;
            txtsearch = $("#txtSearch").val();
            if (_type == 2 && txtsearch != '') {
                txtsearch = '"' + splitString(txtsearch) + '"';

            }
            var _txt_FromDate = $("#txt_FromDate").val();
            if (_type == 1) {
                $("#msgTinNguon").css("display", "inline");
                var _date = _txt_FromDate.split("/");
                var _year = _date[2];
                $("#txtYear").val(_year.toString());
            } else {
                $("#msgTinNguon").css("display", "none");
            }
            var _txtToDate = $("#txtToDate").val();
            var pagesize = 15;
            $.ajax({
                type: "POST",
                url: "<%=Global.ApplicationPath %>/ckeditor/plugins/insert_tinnguon/WebService.asmx/GetListOfTypeAdvence",
                data: "{skip:" + pageindex + ",take:" + pagesize + ",category:" + _category + ",type:" + _type + ",fromDate:'" + _txt_FromDate + "',toDate:'" + _txtToDate + "',sort:1,search:'" + txtsearch + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",

                success: function(msg) {
                    var data = msg;

                    var strTemp = "";

                    jQuery.each(data, function(rec, value) {
                        if (rec == 'listTN') {
                            for (var i = 0; i < data.listTN.length; i++) {
                                //strTemp += "<a style='text-decoration:none; color:#4F89A1;font:12px Tahoma;' href='javascript:void(0);' onclick='LoadataNews(" + data.listTN[i].MA_TIN_NGUON + "," + 1 + "," + 1 + ");'>" + data.listTN[i].TIEU_DE + "</a>&nbsp;<i>" + data.listTN[i].NGAY_TAO + "</i>" + "<br>";
                                strTemp += "<tr class='GridBorderVerSolid' style='padding : 0px !important' onmouseout=\"this.style.backgroundColor=currColor\" onmouseover=\"currColor=this.style.backgroundColor;this.style.backgroundColor='#B9E0E9'\" >";
                                strTemp += "<td class='GridBorderVerSolid' align='left'><a style='text-decoration:none;font:12px Tahoma;' href='javascript:void(0);' onclick='LoadataNews(" + data.listTN[i].MA_TIN_NGUON + "," + _type + "," + 1 + ");'>" + data.listTN[i].TIEU_DE + "</a></td>";
                                strTemp += "<td class='GridBorderVerSolid' align='center' style='width:15%'>" + data.listTN[i].NGAY_TAO + "</td></tr>";
                            }
                        }
                    });

                    $("#divcontent").html(strTemp);
                    //Phan trang
                    var _totalRecords = data.TotalRecords;
                    Pager(pageindex, pagesize, _totalRecords);
                    //

                },
                error: function(msg) {
                }
            });
        };
        //Phan trang
        function Pager(pageindex, pagesize, _totalRecords) {
            var htmlPaging = "";
            var _sotrang;
            _sotrang = eval(parseFloat(_totalRecords) / parseFloat(pagesize));

            if (_sotrang <= 0.5)
                _sotrang.split = 1;
            else if (_sotrang > 1) {
                var _st = _sotrang.toString().split(".");
                if (_st.length == 2) {
                    _sotrang = parseFloat(_st[0]) + parseFloat(1)
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
                    htmlPaging += "<div class='DivPageNumber' style='display:inline'></div> <a style='text-decoration:none;' href='javascript:void(0);' onclick=\"LoadataContentSearch(" + _trangsau + ",'');\">></a> <a style='text-decoration:none;' href='javascript:void(0);' onclick=\"LoadataContentSearch(" + _tongsotrang + ",'');\">>></a> | Trang <b><input id='pageIndex' title='Nhập số trang' style='width:30px;height:13px;font-size:10px;text-align:center' type='text' value='" + _tranghtai + "' onchange=\"LoadataContentSearch(this.value," + _tongsotrang + ");\" onkeypress=\"return check_Number(this,event);\" />" + " / " + _tongsotrang + "</b> | Hiển thị <b>" + _tubanghi + " - " + _denbanghi + " / " + _totalRecords + "</b> bản ghi ";
                } else {
                    htmlPaging += "Tổng số bản ghi: <b>" + _totalRecords + "</b>";
                }
            }
            else {
                if (_sotrang > 1) {
                    if (_trangtruoc < _trangsau && _trangsau <= _sotrang) {
                        htmlPaging += "<a style='text-decoration:none;' href='javascript:void(0);' onclick=\"LoadataContentSearch(1,'');\"><<</a> <a style='text-decoration:none;' href='javascript:void(0);' onclick=\"LoadataContentSearch(" + _trangtruoc + ",'');\"><</a> <div class='DivPageNumber' style='display:inline'></div> <a style='text-decoration:none;' href='javascript:void(0);' onclick=\"LoadataContentSearch(" + _trangsau + ",'');\">></a> <a style='text-decoration:none;' href='javascript:void(0);' onclick=\"LoadataContentSearch(" + _tongsotrang + ",'');\">>></a> | Trang <b><input id='pageIndex' title='Nhập số trang' style='width:30px;height:13px;font-size:10px;text-align:center' type='text' value='" + _tranghtai + "' onchange=\"LoadataContentSearch(this.value," + _tongsotrang + ");\" onkeypress=\"return check_Number(this,event);\" />" + " / " + _tongsotrang + "</b> | Hiển thị <b>" + _tubanghi + " - " + _denbanghi + " / " + _totalRecords + "</b> bản ghi ";
                    }
                    else if (_trangsau > _sotrang) {
                        if (parseFloat(_totalRecords) < parseFloat(_denbanghi)) {
                            htmlPaging += "<a style='text-decoration:none;' href='javascript:void(0);' onclick=\"LoadataContentSearch(1,'');\"><<</a> <a style='text-decoration:none;' href='javascript:void(0);' onclick=\"LoadataContentSearch(" + _trangtruoc + ",'');\"><</a> <div class='DivPageNumber' style='display:inline'></div> | Trang <b><input id='pageIndex' title='Nhập số trang' style='width:30px;height:13px;font-size:10px;text-align:center' type='text' value='" + _tranghtai + "' onchange=\"LoadataContentSearch(this.value," + _tongsotrang + ");\" onkeypress=\"return check_Number(this,event);\" />" + " / " + _tongsotrang + "</b> | Hiển thị <b>" + _tubanghi + " - " + _totalRecords + " / " + _totalRecords + "</b> bản ghi ";
                        }
                        else {
                            htmlPaging += "<a style='text-decoration:none;' href='javascript:void(0);' onclick=\"LoadataContentSearch(1,'');\"><<</a> <a style='text-decoration:none;' href='javascript:void(0);' onclick=\"LoadataContentSearch(" + _trangtruoc + ",'');\"><</a> <div class='DivPageNumber' style='display:inline'></div> | Trang <b><input id='pageIndex' title='Nhập số trang' style='width:30px;height:13px;font-size:10px;text-align:center' type='text' value='" + _tranghtai + "' onchange=\"LoadataContentSearch(this.value," + _tongsotrang + ");\" onkeypress=\"return check_Number(this,event);\" />" + " / " + _tongsotrang + "</b> | Hiển thị <b>" + _tubanghi + " - " + _denbanghi + " / " + _totalRecords + "</b> bản ghi ";
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
                htmlPagesNumber += "<a style='width:20px!important;height:13px;display: inline;' href='javascript:void(0);' onclick=\"LoadataContentSearch(1,'');\">1</a> ";
                if (start > 3) {
                    htmlPagesNumber += "<a style='width:20px!important;height:13px;display: inline;' href='javascript:void(0);' onclick=\"LoadataContentSearch(1,'');\">2</a> ";
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
                    htmlPagesNumber += "<a style='width:20px!important;height:13px;display: inline;' href='javascript:void(0);' onclick=\"LoadataContentSearch('" + i + "','');\">" + i + "</a> ";
                }
            }
            if (end < _tongsotrang) {
                if (end < _tongsotrang - 1) {
                    htmlPagesNumber += "...... ";
                }
                if (_tongsotrang - 2 > end) {
                    var _pagePrevEnd = parseInt(_tongsotrang) - 1;
                    htmlPagesNumber += "<a style='width:20px!important;height:13px;display: inline;' href='javascript:void(0);' onclick=\"LoadataContentSearch('" + _pagePrevEnd + "','');\">" + _pagePrevEnd + "</a> ";
                }
                htmlPagesNumber += "<a style='width:20px!important;height:13px;display: inline;' href='javascript:void(0);' onclick=\"LoadataContentSearch('" + _tongsotrang + "','');\">" + _tongsotrang + "</a> ";
            }
            //
            $(".pageNav").html(htmlPaging);
            $(".DivPageNumber").html(htmlPagesNumber);
            $("#pageIndex").val(pageindex);
        }
        //

        function LoadataNews(id, type, isFull) {

            var _category = $("#categorysid").val();
            var txtsearch = $("#txtSearch").val();
            var _year = $("#txtYear").val();

            $.ajax({
                type: "POST",
                url: "<%=Global.ApplicationPath %>/ckeditor/plugins/insert_tinnguon/WebService.asmx/GetByID",
                data: "{id:" + id + ",type:" + type + ",isFull:" + isFull + ",year:" + _year + "}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function(msg) {
                    var data = msg;
                    insert_content(data.NOI_DUNG)
                    //INSERT LOG
                    InsertLog(data.TIEU_DE, id);
                    //END
                    window.top.close();
                },
                error: function(msg) {
                    alert('Error');
                }
            });
        };

        //INSERT LOG
        function InsertLog(_tieude, _id) {
            //alert("start");
            var _userid = '<%=_user.UserID.ToString()%>';
            var _username = '<%=_user.UserFullName.ToString()%>';
            var _tin = $("#txtType").val();
            var _year = $("#txtYear").val();
            $.ajax({
                type: "POST",
                url: "<%=Global.ApplicationPath %>/ckeditor/plugins/insert_tinnguon/WebServiceInsertLog.asmx/InsertLog",
                data: "{id:'" + _id + "',user_id:'" + _userid + "',user_name:'" + _username + "',tieude:'" + _tieude + "',tracuutin:" + _tin + ",year:'" + _year + "'}",
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
        function valComments_ClientValidate(source, args) {
            //            var dd = tdate.slice(0, 2);
            //            var dd = new Number(dd);
            //            var mm = tdate.slice(3, 5);
            //            var mm = new Number(mm);
            //            var yyyy = tdate.slice(6, 10);
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
    </script>

</body>
</html>
