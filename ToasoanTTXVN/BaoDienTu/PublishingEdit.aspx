<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/ITC.Master" AutoEventWireup="true"
    CodeBehind="PublishingEdit.aspx.cs" Inherits="ToasoanTTXVN.BaoDienTu.PublishingEdit" %>

<%@ Register TagPrefix="anthem" Namespace="Anthem" Assembly="Anthem" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<%@ Register TagPrefix="cc1" Namespace="HPCComponents.UI.WebControls" Assembly="HPCComponents" %>
<%@ Import Namespace="HPCComponents" %>
<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>
<%@ Register TagPrefix="nbc" Namespace="HPCComponents.UI" Assembly="HPCComponents" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <script src="../Dungchung/Scripts/jquery-1.4.2.js" type="text/javascript"></script>

    <!--Date-->
    <link href="../Dungchung/Scripts/DatetimeNews/jquery-calendar.css" type="text/css"
        rel="stylesheet" />

    <script type="text/javascript" language="javascript" src="../Dungchung/Scripts/DatetimeNews/jquery-calendar.js"></script>

    <script type="text/javascript">
        $(document).ready(function() {
            $('#<%=txtTimeXB.ClientID %>').calendar();
        });
        function chitiettin() {
            var txt_bodyEdit = "<%= txt_noidung.ClientID %>";
            var txt_TieudeEdit = $('#<%=Txt_tieude.ClientID%>').val();
            var txt_sapoEdit = "<%= txt_tomtat.ClientID %>";
            var _bodyEdit = CKEDITOR.instances[txt_bodyEdit].getData();
            var _tomtatEdit = CKEDITOR.instances[txt_sapoEdit].getData();
            var tieudephu = $('#<%=txt_TieuDePhu.ClientID%>').val();
            var imageThum = $('#<%=txtThumbnail.ClientID%>').val();
            var tacgia = $('#<%=txt_Author_name.ClientID%>').val();
            var cbdisplay = false;
            var _titimage = "";
            chitiet_edithpc(_bodyEdit, _tomtatEdit, txt_TieudeEdit, tieudephu, imageThum, tacgia);
        }
    </script>

    <script type="text/javascript">
        function BrowserVideoFile(vKey) {
            SubmitImage('../UploadVideos/Video_News.aspx?vType=3&vKey=' + vKey + '', 840, 580);
        }
        function OpenFullSize(path2Url) {
            //alert(path2Url);
            if (path2Url != '') {
                SubmitImage('../Until/FileManager.aspx?vType=1&imgPath=<%=HPCComponents.Global.UploadPathBDT%>' + path2Url + '', 1020, 780);
            }
            else
                SubmitImage('../Until/FileManager.aspx?vType=1', 1020, 780);
        }

        function getPath(valuePath, numArg, numID) {
            if (parseInt(numArg) == 1) {
                document.getElementById("ctl00_MainContent_txtThumbnail").value = valuePath;
                document.getElementById("ctl00_MainContent_ImgTemp").src = '<%=HPCComponents.Global.UploadPathBDT%>' + valuePath;
                document.getElementById("ctl00_MainContent_ImgTemp").style.display = '';
            }
            if (parseInt(numArg) == 2) {
                document.getElementById("ctl00_MainContent_txtVideoPath").value = valuePath;
            }
            if (parseInt(numArg) == 3) {
                document.getElementById("ctl00_MainContent_txtNewsAttach").value = valuePath;
            }
        }
        function uploadOnchange() {
            if (document.getElementById("ctl00_MainContent_txtThumbnail").value != '') {
                document.getElementById("ctl00_MainContent_ImgTemp").src = '<%=HPCComponents.Global.TinPathBDT%>' + document.getElementById("ctl00_MainContent_txtThumbnail").value;
                document.getElementById("ctl00_MainContent_ImgTemp").style.display = '';
            }
            else {
                document.getElementById("ctl00_MainContent_ImgTemp").style.display = 'none';
            }
        }
        function InsertImage(id, file_ext, oCKEditor) {
            // Get the editor instance that we want to interact with.
            //alert(id);
            var oEditor = CKEDITOR.instances.ctl00_MainContent_txt_noidung;
            if (id != "ctl00_MainContent_txt_noidung")
                oEditor = CKEDITOR.instances.ctl00_MainContent_txt_tomtat;
            //alert(file_ext);
            // Check the active editing mode.
            if (oEditor.mode == 'wysiwyg') {
                // Insert the desired HTML.
                oEditor.insertHtml(file_ext);
            }
            // else
            // alert('You must be on WYSIWYG mode!');
        }
        function ClearFile() {
            document.getElementById("ctl00_MainContent_txtThumbnail").value = "";
            document.getElementById("ctl00_MainContent_ImgTemp").style.display = 'none';
        }
        function OpenInsertNewRelation() {
            SubmitImage('../Until/InsertNewsRelations.aspx', 1000, 580);
        }
        function InsertNewRelation(valuePath1) {
            var _listID = document.getElementById("ctl00_MainContent_txtListID").value;
            if (_listID != "0") {

                var _listappend = _listID.concat("," + valuePath1);
                document.getElementById("ctl00_MainContent_txtListID").value = _listappend;
            }
            else {
                document.getElementById("ctl00_MainContent_txtListID").value = valuePath1;
            }
            onSuccess();
        }
        function onSuccess() {
            $get('ctl00_MainContent_btnLoad').click();
        }
    </script>

    <asp:TextBox ID="txtID" Text="0" runat="server" Style="display: none"></asp:TextBox>
    <table border="0" cellpadding="0" width="100%" cellspacing="0">
        <tr>
            <td class="datagrid_top_left">
            </td>
            <td class="datagrid_top_center">
                <table border="0" cellpadding="1" cellspacing="1" width="100%">
                    <tr>
                        <td style="text-align: left; width: 2%">
                            <img src="../Dungchung/Images/Icons/cog-edit-icon.png" alt="" width="16px" height="16px" />
                        </td>
                        <td style="vertical-align: middle; text-align: left">
                            <span class="TitlePanel">
                                <asp:Label ID="lblTitleCaption" runat="server"></asp:Label></span>
                        </td>
                    </tr>
                </table>
            </td>
            <td class="datagrid_top_right">
            </td>
        </tr>
        <tr>
            <td class="datagrid_content_left">
            </td>
            <td style="text-align: center">
                <table border="0" cellspacing="2" cellpadding="4" width="100%">
                    <tr>
                        <td style="text-align: left; vertical-align: top; width: 58%">
                            <div class="classSearchHeader">
                                <CKEditor:CKEditorControl ID="txt_noidung" Skin="v2" Toolbar="NoidungBDT" runat="server"
                                    BasePath="~/ckeditor" ContentsCss="../ckeditor/contentsbdt.css" Height="900px"
                                    Width="100%" ToolbarStartupExpanded="true">
                                </CKEditor:CKEditorControl>
                            </div>
                        </td>
                        <td style="width: 42%; text-align: left; vertical-align: top">
                            <div class="classSearchHeader">
                                <table cellspacing="4" cellpadding="4" width="100%" border="0">
                                    <tr>
                                        <td style="width: 20%; text-align: left" class="Titlelbl">
                                            <%= CommonLib.ReadXML("lblAnpham")%>:<span class="req_Field">*</span>
                                        </td>
                                        <td style="text-align: left; width: 60%" class="Titlelbl">
                                            <%= CommonLib.ReadXML("lblChuyenmuc") %>:<span class="req_Field">*</span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 20%; text-align: left">
                                            <anthem:DropDownList AutoCallBack="true" ID="cbo_lanquage" runat="server" Width="150px"
                                                Enabled="true" CssClass="inputtext" OnSelectedIndexChanged="cbo_lanquage_SelectedIndexChanged"
                                                TabIndex="2">
                                            </anthem:DropDownList>
                                        </td>
                                        <td style="text-align: left">
                                            <anthem:DropDownList AutoCallBack="true" ID="cbo_chuyenmuc" runat="server" Width="88%"
                                                CssClass="inputtext" TabIndex="3">
                                            </anthem:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: left; width: 100%" class="Titlelbl" colspan="2">
                                            <%= CommonLib.ReadXML("lblTieudephu") %>:
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: left; width: 100%" colspan="2">
                                            <asp:TextBox ID="txt_TieuDePhu" TabIndex="4" Width="90%" runat="server" CssClass="inputtext"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: left;" class="Titlelbl" colspan="2">
                                            <%= CommonLib.ReadXML("lblTieudechinh") %>:<span class="req_Field">*</span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: left;" colspan="2">
                                            <asp:TextBox ID="Txt_tieude" TabIndex="5" runat="server" Width="90%" CssClass="inputtext"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: left;" class="Titlelbl" colspan="2">
                                            <%= CommonLib.ReadXML("lblAnhdaidien") %>:
                                            <!--<anthem:CheckBox runat="server" AutoCallBack="true" Width="60%" ID="cbHienthiAnh"
                                                Text="Không hiển thị trong tin chi tiết" />-->
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: left;" colspan="2">
                                            <asp:TextBox ID="txtThumbnail" TabIndex="6" Width="60%" runat="server" CssClass="inputtext"
                                                onblur="uploadOnchange();"></asp:TextBox>
                                            <input class="PhotoSel" accesskey="S" onclick="OpenFullSize('')" type="button" value="Browse"
                                                name="cmd_SavePath2" />
                                            <img runat="server" id="ImgTemp" onclick="OpenFullSize(document.getElementById('ctl00_MainContent_txtThumbnail').value);"
                                                alt="Crop ảnh" style="width: 40px; height: 25px; border: 0px; vertical-align: middle;
                                                cursor: pointer;" />
                                            <img style="cursor: pointer;" onclick="ClearFile();" height="20" alt="Xóa ảnh" src="<%= Global.ApplicationPath %>/DungChung/images/delete.gif"
                                                width="20" border="0" />
                                        </td>
                                    </tr>
                                    <!--<tr>
                                        <td style="text-align: left;" class="Titlelbl" colspan="2">
                                            Chú thích ảnh đại diện
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: left;" colspan="2">
                                            <asp:TextBox ID="txtChuthichanh" TabIndex="5" runat="server" Width="90%" CssClass="inputtext"
                                                TextMode="MultiLine" Rows="2"></asp:TextBox>
                                        </td>
                                    </tr>-->
                                    <!--<tr>
                                        <td style="text-align: left;" class="Titlelbl" colspan="2">
                                            Đường dẫn Video:
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: left;" class="Titlelbl" colspan="2">
                                            <asp:TextBox CssClass="inputtext" ID="txtVideoPath" Width="60%" runat="server"></asp:TextBox>
                                            <asp:TextBox ID="txtVideoID" Width="10" runat="server" Visible="false"></asp:TextBox>
                                            <input class="PhotoSel" accesskey="S" onclick="BrowserVideoFile(2)" type="button"
                                                value="Browse" name="cmd_SavePath2" />
                                            &nbsp;<a href="javascript:void(0)" onclick="f_ViewAds('<%=txtVideoPath.ClientID%>','<%=txtThumbnail.ClientID %>');">
                                                Xem video</a>
                                        </td>
                                    </tr>-->
                                    <tr>
                                        <td style="text-align: left" valign="middle" class="Titlelbl" colspan="2">
                                            Sapo:
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: left" colspan="2">
                                            <CKEditor:CKEditorControl ID="txt_tomtat" Skin="v2" Toolbar="SapoBDT" runat="server"
                                                BasePath="~/ckeditor" ContentsCss="../ckeditor/contentsbdt.css" Height="100px"
                                                Width="93%" ToolbarStartupExpanded="true">
                                            </CKEditor:CKEditorControl>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                        </td>
                                    </tr>
                                    <!--<tr>
                                        <td style="text-align: left;padding-left: 4px;" valign="middle" class="Titlelbl" colspan="2">
                                            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                                <ContentTemplate>
                                                    <input accesskey="S" onclick="OpenInsertNewRelation()" type="button" class="iconAdd"
                                                        value="Chọn bài liên quan" tabindex="13" />
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: left;padding-left: 4px;" valign="middle" class="Titlelbl" colspan="2">
                                            <asp:UpdatePanel ID="UpdatePanelGrid" runat="server">
                                                <ContentTemplate>
                                                    <asp:TextBox ID="txtListID" runat="server" CssClass="inputtext" Width="90%" Text="0"
                                                        Style="display: none"></asp:TextBox>
                                                    <asp:DataGrid runat="server" ID="dgListNewRelation" AutoGenerateColumns="false" DataKeyField="News_ID"
                                                        Width="92%" CssClass="Grid" OnEditCommand="dgListNewRelation_EditCommand" OnItemDataBound="dgListNewRelation_ItemDataBound">
                                                        <ItemStyle CssClass="GridItem"></ItemStyle>
                                                        <AlternatingItemStyle CssClass="GridAltItem" />
                                                        <HeaderStyle CssClass="GridHeader"></HeaderStyle>
                                                        <Columns>
                                                            <asp:TemplateColumn>
                                                                <HeaderStyle Width="80%" HorizontalAlign="Left"></HeaderStyle>
                                                                <ItemStyle HorizontalAlign="Left" Width="80%"></ItemStyle>
                                                                <HeaderTemplate>
                                                                    Tên bài viết
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="btnEdit" runat="server" CssClass="linkEdit" Text=' <%#Eval("News_Tittle")%>'></asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateColumn>
                                                            <asp:TemplateColumn HeaderText="Xóa">
                                                                <HeaderStyle HorizontalAlign="Center" Width="20%"></HeaderStyle>
                                                                <ItemStyle HorizontalAlign="Center" Width="20%"></ItemStyle>
                                                                <ItemTemplate>
                                                                    <asp:ImageButton ID="btnDelete" Width="15px" runat="server" ImageUrl="~/Dungchung/images/cancel.gif"
                                                                        ImageAlign="AbsMiddle" ToolTip="Xóa bài liên quan" CommandName="Edit" CommandArgument="DeleteNewRelation"
                                                                        BorderStyle="None"></asp:ImageButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateColumn>
                                                        </Columns>
                                                    </asp:DataGrid>
                                                    <asp:Button ID="btnLoad" runat="server" OnClick="btnLoad_Click" Style="display: none" />
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </td>
                                    </tr>-->
                                    <tr>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: left;" class="Titlelbl" colspan="2">
                                            <%= CommonLib.ReadXML("lblTukhoa") %>:
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: left;" colspan="2">
                                            <asp:TextBox ID="txtTukhoa" TabIndex="9" runat="server" CssClass="inputtext" Width="90%"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: left" class="Titlelbl">
                                            <%= CommonLib.ReadXML("lblTacgia") %>:
                                        </td>
                                        <td style="text-align: left" class="Titlelbl">
                                            <%= CommonLib.ReadXML("lblNguon") %>:
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: left;">
                                            <asp:TextBox ID="txt_Author_name" runat="server" CssClass="inputtext" Width="90%"
                                                TabIndex="10"></asp:TextBox>
                                            <input runat="server" id="hdnValue" type="hidden" />
                                        </td>
                                        <td style="text-align: left;">
                                            <asp:TextBox ID="txtNguon" runat="server" CssClass="inputtext" Width="85%" TabIndex="10"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: left" class="Titlelbl" colspan="2">
                                            <%= CommonLib.ReadXML("lblTrangthai") %>:
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: left; width: 100%" colspan="2">
                                            <div class="classPanelForm" style="width: 100%">
                                                <asp:CheckBox runat="server" Text="<%$Resources:cms.language, lblNoibattrangchu%>"
                                                    Width="25%" ID="chk_IsHomePages" TabIndex="11" />
                                                <asp:CheckBox runat="server" Text="<%$Resources:cms.language, lblTinnong%>" Width="20%"
                                                    ID="chkNewsIsHot" TabIndex="12" />
                                                <asp:CheckBox runat="server" Text="<%$Resources:cms.language, lbltintieudiem%>" Width="20%"
                                                    ID="chkNewsIsFocus" TabIndex="13" />
                                                <asp:CheckBox runat="server" Text="<%$Resources:cms.language, lblNoibatchuyenmuc%>"
                                                    Width="30%" ID="chk_IsCategorys" TabIndex="14" />
                                            </div>
                                        </td>
                                    </tr>
                                    <!--<tr>
                                        <td style="text-align: left; width: 100%" colspan="2">
                                            <div class="classPanelForm" style="width: 90%">
                                                <asp:CheckBox runat="server" Text="Nổi bật chuyên mục cha" Width="32%" ID="chk_IsCategoryParrent"
                                                    TabIndex="26" />-->
                                    <!--<asp:CheckBox runat="server" Text="Tin được quan tâm" Width="30%" ID="cbMoreViews"
                                                    TabIndex="28" />
                                            </div>
                                        </td>
                                    </tr>-->
                                    <!--<tr>
                                        <td style="text-align: left; height: 30px;" class="Titlelbl" colspan="2">
                                            Thể loại:
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: left; width: 100%" colspan="2">
                                            <div class="classPanelForm" style="width: 90%">
                                                <asp:CheckBox runat="server" Text="Tin ảnh" Width="30%" ID="chkImages" TabIndex="29" />
                                                <asp:CheckBox runat="server" Text="Tin video" Width="30%" ID="chkVideo" TabIndex="30" />
                                                <asp:CheckBox runat="server" Text="Tin ẩn" Width="30%" ID="chkHistorys" TabIndex="31" />
                                            </div>
                                        </td>
                                    </tr>-->
                                    <!--<tr>
                                        <td style="text-align: left; width: 100%" colspan="2">
                                            <div class="classPanelForm" style="width: 90%">
                                                <asp:CheckBox runat="server" Text="Không hiển thị Mobile" Width="40%" ID="cbDisplayMobile"
                                                    TabIndex="32" />
                                            </div>
                                        </td>
                                    </tr>-->
                                    <tr>
                                        <td style="text-align: left" class="Titlelbl">
                                            <%= CommonLib.ReadXML("lblNhuanbut") %>:
                                        </td>
                                        <td style="text-align: left" class="Titlelbl">
                                            <%= CommonLib.ReadXML("lblHengioXB")%>:
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: left;">
                                            <asp:TextBox ID="txtTienNhuanBut" onKeyPress="return check_num(this,10,event);" onkeyup="javascript:return CommaMonney(this.id);"
                                                runat="server" CssClass="inputtext" Width="66%" TabIndex="15"></asp:TextBox>&nbsp;&nbsp;&nbsp;VNĐ
                                        </td>
                                        <td style="text-align: left;">
                                            <asp:TextBox ID="txtTimeXB" runat="server" CssClass="inputtext" Width="74%" TabIndex="16"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: left" valign="top" class="Titlelbl" colspan="2">
                                            Ghi chú:
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: left" colspan="2">
                                            <asp:TextBox ID="Txt_Comments" TabIndex="17" runat="server" Width="90%" CssClass="inputtext"
                                                TextMode="MultiLine" Rows="4"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center" colspan="2">
                                            <div class="classSearchHeader" style="width: 90%">
                                                <asp:Button runat="server" ID="btn_Layout" OnClick="btnLayout_Click" CssClass="iconFind"
                                                    Text="Load Layout" />
                                                <input type="button" id="Button1" class="iconView" 
                                                    value="<%= CommonLib.ReadXML("lblXemtruoc") %>" onclick="chitiettin();" />
                                                <asp:Button runat="server" ID="linkSave" CssClass="iconSave" TabIndex="19" OnClick="linkSave_Click"
                                                    Text="<%$Resources:cms.language, lblLuu%>"></asp:Button>
                                                <asp:Button runat="server" ID="btnXuatban" CssClass="iconPub" TabIndex="20" OnClick="btnXuatban_Click"
                                                    Text="<%$Resources:cms.language, lblXuatban%>"></asp:Button>
                                                <asp:Button runat="server" ID="LinkBack" CssClass="iconReply" TabIndex="22" Font-Bold="true"
                                                    OnClick="LinkBack_Click" Text="<%$Resources:cms.language, lblTralai%>" />
                                                <asp:Button runat="server" ID="btnExitForm" TabIndex="23" CssClass="iconExit" OnClick="linkExit_Click"
                                                    Text="<%$Resources:cms.language, lblThoat%>" CausesValidation="false"></asp:Button>
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </td>
                    </tr>
                </table>
            </td>
            <td class="datagrid_content_right">
            </td>
        </tr>
        <tr>
            <td class="datagrid_bottom_left">
            </td>
            <td class="datagrid_bottom_center">
            </td>
            <td class="datagrid_bottom_right">
            </td>
        </tr>
    </table>
</asp:Content>
