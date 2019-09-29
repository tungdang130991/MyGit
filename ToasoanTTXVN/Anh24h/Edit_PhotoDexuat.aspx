<%@ Page Language="C#" MasterPageFile="~/Masters/ITC.Master" AutoEventWireup="true"
    CodeBehind="Edit_PhotoDexuat.aspx.cs" Inherits="ToasoanTTXVN.Anh24h.Edit_PhotoDexuat" %>

<%@ Register TagPrefix="cc1" Namespace="HPCComponents.UI.WebControls" Assembly="HPCComponents" %>
<%@ Import Namespace="HPCComponents" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <script language="Javascript" type="text/javascript">
        var tmp_Window;
        function BrowserVideoFile(vKey) {
            SubmitImage('../Until/FileManager.aspx?vType=4&vKey=' + vKey + '', 840, 580);
        }
        function getPath(valuePath, numArg, Size, FileExtension) {
            if (parseInt(numArg) == 4) {
                document.getElementById("ctl00_MainContent_txtThumbnail").value = valuePath;
                document.getElementById("ctl00_MainContent_ImgTemp").src = '<%=HPCComponents.Global.UploadPathBDT%>' + valuePath;
                document.getElementById("ctl00_MainContent_ImgTemp").style.display = '';
            }
        }
        function ClearImage() {
            document.getElementById("ctl00_MainContent_txtThumbnail").value = "";
            document.getElementById("ctl00_MainContent_ImgTemp").style.display = 'none';
        }
    </script>

    <table border="0" cellpadding="0" width="100%" cellspacing="0">
        <tr>
            <td class="datagrid_top_left">
            </td>
            <td class="datagrid_top_center">
                <table border="0" cellpadding="1" cellspacing="1" width="100%">
                    <tr>
                        <td style="text-align: left; width: 2%">
                            <img src="../Dungchung/images/Icons/cog-edit-icon.png" alt="" width="16px" height="16px" />
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
                <table width="100%" align="center">
                    <tr>
                        <td style="width: 20%; height: 20px; text-align: right;">
                            <asp:Label ID="Label4" runat="server" CssClass="Titlelbl" Text="<%$Resources:cms.language, lblAnpham%>"></asp:Label>(<span class="req_Field">*</span>):&nbsp;
                        </td>
                        <td style="height: 20px; text-align: left;">
                            <asp:DropDownList CssClass="inputtext" ID="cboNgonNgu" runat="server"
                                Width="150px"></asp:DropDownList>
                            <br />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="cboNgonNgu"
                                Display="Dynamic" ErrorMessage="<%$Resources:cms.language, lblbanchuachonanpham%>" Font-Size="Small" InitialValue="0"
                                SetFocusOnError="True"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 20%; text-align: right;vertical-align:top;">
                            <asp:Label ID="Label2" runat="server" CssClass="Titlelbl" Text="<%$Resources:cms.language, lblTieude%>"></asp:Label>(<span
                                class="req_Field">*</span>):&nbsp;
                        </td>
                        <td style="text-align: left;">
                            <asp:TextBox ID="txt_Abl_Photo_Name" runat="server" Width="665px" TextMode="MultiLine"
                                Rows="5"></asp:TextBox>
                            <br />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txt_Abl_Photo_Name"
                                Display="Dynamic" ErrorMessage="<%$Resources:cms.language, lblNhaptieudeanh%>" Font-Size="Small" SetFocusOnError="True"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 20%; text-align: right;">
                            <asp:Label ID="Label10" runat="server" CssClass="Titlelbl" Text="<%$Resources:cms.language, lblHinhanh%>"></asp:Label>(<span
                                class="req_Field">*</span>):&nbsp;
                        </td>
                        <td class="Time" align="left">
                            <asp:TextBox ID="txtThumbnail" runat="server" CssClass="inputtext" Width="660px"></asp:TextBox>
                            <input class="PhotoSel" accesskey="S" onclick="BrowserVideoFile(1)" type="button"
                                value="Browse" name="cmd_SavePath2" />
                            <img runat="server" id="ImgTemp" onclick="openNewImage(this,'Close');" alt="Click Xem ảnh"
                                title="Xem ảnh" style="width: 40px; height: 28px; border: 0px; vertical-align: middle;
                                cursor: pointer;" />
                            <img style="cursor: pointer;" onclick="ClearImage();" height="20" alt="Xóa ảnh" src="<%= Global.ApplicationPath %>/Dungchung/Images/delete.gif"
                                width="20" border="0" />
                            <br />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtThumbnail"
                                Display="Dynamic" ErrorMessage="%$Resources:cms.language, lblNhapanh%>" Font-Size="Small" SetFocusOnError="True"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <!-- <tr>
                <td style="width: 20%; height: 20px; text-align: right;">
                    <asp:Label ID="Label6" runat="server" CssClass="Titlelbl" Text="Dung lượng:"></asp:Label>&nbsp;
                </td>
                <td style="height: 20px; text-align: left;">
                    <asp:TextBox ID="txt_Dungluong" runat="server" Width="200px"></asp:TextBox></td>
            </tr>
            <tr>
                <td style="width: 20%; height: 20px; text-align: right;">
                    <asp:Label ID="Label5" runat="server" CssClass="Titlelbl" Text="Loại file:"></asp:Label>&nbsp;
                </td>
                <td style="height: 20px; text-align: left;">
                    <asp:TextBox ID="txt_loaifile" runat="server" Width="200px"></asp:TextBox></td>
            </tr>
            
            <tr>
                <td style="width: 20%; height: 20px; text-align: right;">
                    <asp:Label ID="Label3" runat="server" CssClass="Titlelbl" Text="Người cập nhật:"></asp:Label>&nbsp;
                </td>
                <td style="height: 20px; text-align: left;">
                    <asp:TextBox ID="txt_PeopleCreator" runat="server" Width="200px"></asp:TextBox></td>
            </tr>-->
                    <tr>
                        <td style="width: 20%; height: 20px; text-align: right;">
                            <asp:Label ID="Label7" runat="server" CssClass="Titlelbl" Text="<%$Resources:cms.language, lblTacgia%>"></asp:Label>:&nbsp;
                        </td>
                        <td style="height: 20px; text-align: left;">
                            <asp:TextBox ID="txt_Authod_Name" runat="server" Width="660px" CssClass="inputtext"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 20%; text-align: right;vertical-align:top;">
                            <asp:Label ID="Label1" runat="server" CssClass="Titlelbl" Text="<%$Resources:cms.language, lblGhichu%>"></asp:Label>:
                        </td>
                        <td style="text-align: left;">
                            <asp:TextBox ID="txtGhichu" runat="server" Width="665px" TextMode="MultiLine"
                                Rows="3"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" style="text-align: center;" class="Titlelbl">
                            <u>- <%= CommonLib.ReadXML("lblGhichu")%>:</u> &nbsp;<%= CommonLib.ReadXML("lblGhichuluu")%>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td align="center">
                            <div class="processbuttonone">
                                <asp:Button runat="server" ID="linkSave" OnClick="linkSave_Click" CssClass="iconSave"
                                    Text="<%$Resources:cms.language, lblLuu%>" />
                                <asp:Button runat="server" ID="LinkDanganh" CausesValidation="true" OnClick="LinkDanganh_Click"
                                    CssClass="iconPub" Text="<%$Resources:cms.language, lblGui%>" />
                                <asp:Button runat="server" ID="LinkCancel" CausesValidation="false" OnClick="LinkCancel_Click"
                                    CssClass="iconExit" Text="<%$Resources:cms.language, lblThoat%>" />
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
    </asp:Panel>
</asp:Content>
