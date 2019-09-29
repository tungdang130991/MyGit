<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/ITCv2.Master" AutoEventWireup="true"
    CodeBehind="LayoutReview.aspx.cs" Inherits="ToasoanTTXVN.QL_SanXuat.LayoutReview" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <script type="text/javascript" language="javascript">

        function SelectItem(trangbao) {
            var canv = document.getElementById('ctl00_MainContent_Canvas');
            canv.innerHTML = "";
            // this.className = "active";
//            canv.removeAttribute('class');
//            canv.className = 'Canvas colorbg';
            $('#ctl00_MainContent_Trang_Bao').val(trangbao);
            var sobao = $('#ctl00_MainContent_MasoBao').val();
            var sLink = 'BindReview.aspx?mabao=' + sobao + '&trangbao=' + trangbao
            var $loader = $('#ctl00_MainContent_Canvas');
            $loader.load(sLink);
        }
        function setValueSobao(sobao) {
            $('#ctl00_MainContent_MasoBao').val(sobao);
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
        </div>
        <asp:HiddenField ID="MasoBao" runat="server" />
    </center>
</asp:Content>
