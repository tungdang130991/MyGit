<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/ITCv2.Master" AutoEventWireup="true"
    CodeBehind="LayoutAdv.aspx.cs" Inherits="ToasoanTTXVN.QL_SanXuat.LayoutAdv" %>

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
                                URL_Get = "LayoutHandler.ashx?masobao=" + maso_bao + "&trangbao=" + trang_bao + "&title=" + arr[j][0] + "&width=" + arr[j][1] + "&height=" + arr[j][2] + "&left=" + arr[j][3] + "&top=" + arr[j][4] + "&matinbai=" + arr[j][5] + "&macv=" + arr[j][6] + "&maqc=" + arr[j][7] + "&type=3";
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
            else return false;
        };
        dragresize.isHandle = function(elm) {
            if (elm.className && elm.className.indexOf('drsMoveHandle') > -1) return true;
            else return false;
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
            var MaQC = 0;
            var arr = new Array();
            var obj = $(".drsElement");
            for (i = 0; i < obj.length; i++) {
                if (obj[i].className == 'drsElement') {
                    if (document.getElementById('txtAdv' + obj[i].id) != null && document.getElementById('txtAdv' + obj[i].id).value != "")
                        MaQC = document.getElementById('txtAdv' + obj[i].id).value;
                    else
                        MaQC = 0;
                    arr[counraArr] = new Array(obj[i].title, obj[i].style.width, obj[i].style.height, obj[i].style.left, obj[i].style.top, 0, 0, MaQC);
                    counraArr++;
                }
            }
            return arr;
        }
        document.onmousemove = function(event) {
            if (dragresize.handle == null) {
                getMouseAdv(document, event);
            }
        }
        function SelectItem(trangbao) {
            var canv = document.getElementById('ctl00_MainContent_Canvas');
            canv.innerHTML = "";
//            canv.removeAttribute('class');
//            canv.className = 'Canvas colorbg';
            $('#ctl00_MainContent_Trang_Bao').val(trangbao);
            var sobao = $('#ctl00_MainContent_MasoBao').val();
            var sLink = 'BindLayoutAdv.aspx?mabao=' + sobao + '&trangbao=' + trangbao
            var $loader = $('#ctl00_MainContent_Canvas');
            $loader.load(sLink);
            var Anpham = $('#ctl00_MainContent_cboAnPham').val();
            var sLink2 = 'BindListAdv.aspx?maAnpham=' + Anpham + '&mabao=' + sobao + '&trangbao=' + trangbao;
            var $loader2 = $('#DIVadvertisHolderID');
            $loader2.load(sLink2);


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

        function ChonAdv(obj, _id) {
            dialog.setTitle('Quảng cáo');
            dialog.setURI('DIVadvertisHolderID');
            dialog.setFrame(600, 430);
            dialog.doModal();
            $('#ctl00_MainContent_LayoutID').val(_id);
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
                <%--  <div class="Chugiai">
                    <span class="Chugiaitext">Chú giải:</span>
                    <img src="../Dungchung/Style/Layout/VRed.jpg" alt="" /><span class="ChugiaiTitle">Chưa
                        có bài</span>
                    <img src="../Dungchung/Style/Layout/Vvang.jpg" alt="" /><span class="ChugiaiTitle">Đã
                        có bài</span>
                    <img src="../Dungchung/Style/Layout/VXanh.jpg" alt="" /><span class="ChugiaiTitle">Bài
                        đã duyệt</span>
                </div>--%>
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
            <div id="DIVadvertisHolderID" style="height: 330px; width: 600px;">
            </div>
        </div>
    </center>
</asp:Content>
