<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/ITCv2.Master" AutoEventWireup="true"
    CodeBehind="LayouPhanviec.aspx.cs" Inherits="ToasoanTTXVN.QL_SanXuat.LayouPhanviec" %>

<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral,PublicKeyToken=31BF3856AD364E35" %>
<%@ Register TagPrefix="anthem" Namespace="Anthem" Assembly="Anthem" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<%@ Register TagPrefix="cc1" Namespace="HPCComponents.UI.WebControls" Assembly="HPCComponents" %>
<%@ Register TagPrefix="nbc" Namespace="HPCComponents.UI" Assembly="HPCComponents" %>
<%@ Import Namespace="HPCComponents" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <script type="text/javascript" language="javascript">
        function HandlerInfoDiv() {
            var URL_Get;
            var trang_bao = $('#ctl00_MainContent_Trang_Bao').val();
            var maso_bao = $('#ctl00_MainContent_MasoBao').val();
            if (maso_bao != "") {
                if (trang_bao != "") {
                    var arr = GetValuesFromHtml();
                    if (arr.length > 0) {
                        for (var j = 0; j < arr.length; j++) {
                            for (var k in arr[j]) {
                                URL_Get = "LayoutHandler.ashx?masobao=" + maso_bao + "&trangbao=" + trang_bao + "&title=" + arr[j][0] + "&width=" + arr[j][1] + "&height=" + arr[j][2] + "&left=" + arr[j][3] + "&top=" + arr[j][4] + "&matinbai=" + arr[j][5] + "&macv=" + arr[j][6] + "&maqc=" + arr[j][7] + "&type=1";
                            }
                            if (URL_Get != undefined) {
                                var xmlHttp = CreateAjax();
                                xmlHttp.open("GET", URL_Get, true);
                                xmlHttp.send(null);
                            }
                        }
                        xmlHttp.onreadystatechange = function() {
                            if (xmlHttp.readyState == 4) {
                                if (xmlHttp.responseText == "1") {
                                    alert("Lưu thông tin thành công.");
                                    //ResetDivGraper();
                                }
                                else {
                                    alert("Lưu thông tin THẤT BẠI.");
                                }
                            }
                        }
                        return false;
                    }
                }
                else { alert("Chưa chọn trang báo cần thao tác"); }
            }
            else {
                alert("Chưa chọn số báo cần thao tác");
            }
        }
    </script>

    <script src="../Dungchung/Scripts/Layout/dragresize.js" type="text/javascript"></script>

    <script type="text/javascript">
        var dragresize = new DragResize('dragresize',
           { minWidth: 200, minHeight: 100, minLeft: 5, minTop: 5, maxLeft: 1209, maxTop: 1100 });
        dragresize.isElement = function(elm) {
            if (elm.className && elm.className.indexOf('drsElement') > -1) return true;
        };
        dragresize.isHandle = function(elm) {
            if (elm.className && elm.className.indexOf('drsMoveHandle') > -1) return true;
        };
        dragresize.ondragfocus = function() { };
        dragresize.ondragstart = function(isResize) { };
        dragresize.ondragmove = function(isResize) { };
        dragresize.ondragend = function(isResize) { };
        dragresize.ondragblur = function() { };
        dragresize.apply(document);

    </script>

    <script type="text/javascript" language="javascript">
        function GetValuesFromHtml() {
            var counraArr = 0;
            var MaTinbai = 0;
            var MaCV = 0;
            var MaQC = 0;
            var arr = new Array();
            var obj = $(".drsElement");
            for (i = 0; i < obj.length; i++) {
                if (obj[i].className == 'drsElement') {
                    if (document.getElementById('txtCV' + obj[i].id) != null && document.getElementById('txtCV' + obj[i].id).value != "")
                        MaCV = document.getElementById('txtCV' + obj[i].id).value;
                    else
                        MaCV = 0;
                    arr[counraArr] = new Array(obj[i].title, obj[i].style.width, obj[i].style.height, obj[i].style.left, obj[i].style.top, 0, MaCV, 0);
                    counraArr++;
                }
            }
            return arr;
        }
        document.onmousemove = function(event) {
            if (dragresize.handle == null) {
                getMouse(document, event);
            }
        }
        function SelectItem(trangbao) {
            var canv = document.getElementById('ctl00_MainContent_Canvas');
            canv.innerHTML = "";
            //            canv.removeAttribute('class');
            //            canv.className = 'Canvas colorbg';
            $('#ctl00_MainContent_Trang_Bao').val(trangbao);
            var sobao = $('#ctl00_MainContent_MasoBao').val();
            var sLink = 'BindLayoutCV.aspx?mabao=' + sobao + '&trangbao=' + trangbao
            var $loader = $('#ctl00_MainContent_Canvas');
            $loader.load(sLink);
        }

        function setValueSobao(sobao) {
            $('#ctl00_MainContent_MasoBao').val(sobao);
        }

        function ResetValue() {
            $('#ctl00_MainContent_Trang_Bao').val('');
            $('#ctl00_MainContent_MasoBao').val('');
        }

        function check_num(obj, length, e) {
            var key = window.event ? e.keyCode : e.which;
            var len = obj.value.length + 1;
            if (length <= 3) begin = 48; else begin = 45;
            if (key >= begin && key <= 57 && len <= length || (key == 8 || key == 0)) {
            }
            else return false;
        }


    </script>

    <script language="javascript" type="text/javascript">

        var dialog = new JDialog({
            uri: 'DIVHolderID',
            title: 'Thiết lập hạn định',
            showDefaultButton: false,
            showCancelButton: true,
            enableScrollHandler: true,
            reload: true,
            loadingImageUrl: '../Dungchung/Style/Layout/Images/activity.gif',
            customSettings:
            {
                frameWidth: 550,
                frameHeight: 300
            }
        });

        function PhanViec(obj, _id) {
            dialog.setTitle('Phân việc');
            dialog.setURI('DivPhanViecHolderID');
            dialog.setFrame(600, 430);
            dialog.doModal();
            $('#ctl00_MainContent_LayoutID').val(_id);
            BindListCongviec();
        }

        function BindListCongviec() {
            var $loadercv = $('#tab1Content');
            var Linkcv = 'BindListCongviec.aspx';
            $loadercv.load(Linkcv);
        }

        function btnSaveCongviec() {
            var URL_Get;
            var menu_id = '<%=Request["Menu_ID"]%>';
            var nguoinhan = $("#ctl00_MainContent_ddl_NguoiNhan option:selected").val();
            var tieudecv = $('#txt_tieudecv').val();
            var noidung = $('#txt_noidung').val();
            var sotu = $('#txt_Sotu').val();
            var ngayHT = $('#ctl00_MainContent_txt_NgayHT').val();
            var ip = '<%=IpAddress()%>';
            URL_Get = 'LuuCongviec.ashx?mn_id=' + menu_id + '&tieudecv=' + tieudecv + '&nguoinhan=' + nguoinhan + '&noidung=' + noidung + '&sotu=' + sotu + '&ngayht=' + ngayHT + '&ip=' + ip;
            if (URL_Get != undefined) {
                var xmlHttp = CreateAjax();
                xmlHttp.open("GET", URL_Get, true);
                xmlHttp.send(null);
            }
            xmlHttp.onreadystatechange = function() {
                if (xmlHttp.readyState == 4) {
                    if (xmlHttp.responseText == "1") {
                        alert("Lưu thông tin thành công.");
                        ResetControlConviec();
                    }
                    else {
                        alert("Lưu thông tin THẤT BẠI.");
                    }
                }
            }
            return false;
        }
        function ResetControlConviec() {

            $('#txt_tieudecv').val('');
            $('#txt_noidung').val('');
            $('#txt_Sotu').val('');
            $('#ctl00_MainContent_txt_NgayHT').val('');
            $("#<%=ddl_NguoiNhan.ClientID %>").val('0')
        }
        function PagingAjax(PageID) {
            var $loadercv = $('#tab1Content');
            var Linkcv = 'BindListCongviec.aspx?pageid=' + PageID;
            $loadercv.load(Linkcv);
        }
    </script>

    <script type="text/javascript">
        var arrTabs = new Array("tab1", "tab2");
        var currentTab = "tab1";
        function showTabMRight(tab) {
            BindListCongviec();
            // set current tab
            currentTab = tab;
            // disable all tab
            for (var i = 0; i < arrTabs.length; i++) {
                document.getElementById(arrTabs[i]).className = "tbaoL";
                document.getElementById(arrTabs[i] + "Content").style.display = "none";
            }
            // show current tab
            document.getElementById(currentTab).className = "tbaoL-active";
            document.getElementById(currentTab + "Content").style.display = "block";
        }
    </script>

    <center>
        <div class="wraperLayout">
            <div class="GroupControlTopLayout">
                <span class="Titlelbl">Loại ấn phẩm:</span>
                <asp:DropDownList AutoPostBack="true" ID="cboAnPham" runat="server" Width="300px"
                    CssClass="inputtext" DataTextField="Ten_Anpham" DataValueField="Ma_Anpham" OnSelectedIndexChanged="cboAnPham_SelectedIndexChanged"
                    TabIndex="1">
                </asp:DropDownList>
                <span style="padding-left: 20px;" class="Titlelbl">Số báo:</span>
                <asp:DropDownList AutoPostBack="true" ID="cboSoBao" runat="server" Width="300px"
                    CssClass="inputtext" DataTextField="Ten_Sobao" DataValueField="Ma_Sobao" TabIndex="5"
                    OnSelectedIndexChanged="cboSobao_SelectedIndexChanged">
                </asp:DropDownList>
            </div>
            <div id="mangxec" runat="server" class="MenuTopLayoutMangXec">
            </div>
            <div class="MenuTopLayout">
                <div class="Chugiai">
                    <span class="Chugiaitext">Chú giải:</span>
                    <img src="../Dungchung/Style/Layout/VRed.jpg" alt="" /><span class="ChugiaiTitle">Chưa
                        có bài</span>
                    <img src="../Dungchung/Style/Layout/Vvang.jpg" alt="" /><span class="ChugiaiTitle">Đã
                        có bài</span>
                    <img src="../Dungchung/Style/Layout/VXanh.jpg" alt="" /><span class="ChugiaiTitle">Bài
                        đã duyệt</span>
                </div>
                <input id="btnsave" type="button" onclick="HandlerInfoDiv();" class="iconSave" value="Lưu" />
                <span class="spaceButton"></span>
                <input id="btnReset" type="button" class="iconReset" value="Làm mới" onclick="ResetDivGraper();" />
            </div>
            <!--LIST DATA LEFT-->
            <div class="MainWorking">
                <div class="Main_Search">
                    <div class="Main_Search_Advanded" id="Main_Search_Advanded" style="display: inline;">
                        <asp:Literal runat="server" ID="ltrListLayout"></asp:Literal>
                    </div>
                    <div class="Main_Search_Bottom" id="Main_Search_Bottom">
                        <div class="Main_Search_Bottom_Left">
                            <div class="Main_Search_Bottom_Toggle" onclick="return Show_Hide_Toggle2('Main_Search_Advanded','img_Toggle_Arrow','Main_Search_Bottom','ctl00_MainContent_Canvas');">
                                <img id="img_Toggle_Arrow" src="../Dungchung/Style/Layout/Search_Toggle_Down.png"
                                    alt="" />
                            </div>
                        </div>
                        <div class="Main_Search_Bottom_Center">
                        </div>
                        <div class="Main_Search_Bottom_Right">
                        </div>
                    </div>
                </div>
                <!--END-->
                <div style="width: 100%; height: 1110px; position: relative">
                    <div style="width: 100%; height: 1110px; float: left; overflow: scroll" id="creategrid"
                        runat="server">
                    </div>
                    <div id="Canvas" class="Canvas" runat="server">
                    </div>
                </div>
            </div>
            <asp:HiddenField ID="MasoBao" runat="server" />
            <asp:HiddenField ID="Trang_Bao" runat="server" />
            <asp:HiddenField ID="LayoutID" runat="server" />
        </div>
        <div id="DeleteItem" style="display: none">
        </div>
        <div style="display: none">
            <div id="DivPhanViecHolderID" style="height: 330px; width: 600px;">
                <div class="boundTab">
                    <div class="c-right-iL">
                        <div class="c-right-iL-tab">
                            <div class="tbaoL-active" id="tab1" onclick="showTabMRight('tab1');">
                                <a><span>Danh sách công việc</span></a></div>
                            <div class="tbaoL" id="tab2" onclick="showTabMRight('tab2');">
                                <a><span>Thêm mới</span></a></div>
                        </div>
                        <div class="c-right-iL-nd">
                            <!--TAB DANH SACH CONG VIEC-->
                            <div class="tb-iL" id="tab1Content">
                            </div>
                            <!--TAB THEM MOI CONG VIEC-->
                            <div class="tb-iL" id="tab2Content" style="display: none">
                                <div class="AddnewItem">
                                    <div class="AddnewItemLalbel">
                                        Người nhận:</div>
                                    <div class="AddnewItemControl">
                                        <anthem:DropDownList AutoCallBack="true" ID="ddl_NguoiNhan" CssClass="inputtextLayout"
                                            runat="server" Width="50%" DataTextField="UserFullName" DataValueField="UserID">
                                        </anthem:DropDownList>
                                    </div>
                                </div>
                                <div class="AddnewItem">
                                    <div class="AddnewItemLalbel">
                                        Tiêu đề:(<span class="req_Field">*</span>)</div>
                                    <div class="AddnewItemControl">
                                        <input id="txt_tieudecv" cssclass="inputtextLayout" style="width: 250px" rows="1"></input>
                                    </div>
                                </div>
                                <div class="AddnewItem">
                                    <div class="AddnewItemLalbel">
                                        Nội dung:(<span class="req_Field">*</span>)</div>
                                    <div class="AddnewItemControl">
                                        <textarea id="txt_noidung" cols="20" class="inputtextLayout" rows="8"></textarea>
                                    </div>
                                </div>
                                <div class="AddnewItem">
                                    <div class="AddnewItemLalbel">
                                        Số từ:</div>
                                    <div class="AddnewItemControl">
                                        <input id="txt_Sotu" class="inputtextLayout" style="width: 90px" onkeypress='return check_num(this,5,event)' />
                                    </div>
                                </div>
                                <div class="AddnewItem">
                                    <div class="AddnewItemLalbel">
                                        Ngày hoàn thành:</div>
                                    <div class="AddnewItemControl">
                                        <nbc:NetDatePicker CssClass="inputtext" ImageUrl="../Dungchung/Images/events.gif"
                                            ImageFolder="../Dungchung/scripts/DatePicker/Images" Height="16px" Width="150px"
                                            ScriptSource="../Dungchung/scripts/datepicker.js" ID="txt_NgayHT" runat="server">
                                        </nbc:NetDatePicker>
                                    </div>
                                </div>
                                <div class="AddnewItem">
                                    <div style="padding-left: 105px;">
                                        - <u>Ghi chú:</u> &nbsp;Các ô có đánh dấu <span class="req_Field">*</span> là trường
                                        bắt buộc phải nhập.
                                    </div>
                                </div>
                                <div class="AddnewItem">
                                    <input id="btnLuugiu" style="width: 90px; margin-left: 105px; font-weight: bold"
                                        type="button" onclick="btnSaveCongviec();" class="myButton blue" value="Lưu giữ" />
                                </div>
                                <div style="float: left; width: 100%; height: 22px">
                                </div>
                            </div>
                        </div>
                        <div class="c-right-iL-buttom">
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </center>
</asp:Content>
