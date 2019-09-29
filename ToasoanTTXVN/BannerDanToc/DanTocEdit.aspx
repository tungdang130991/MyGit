<%@ Page Language="C#" MasterPageFile="~/Masters/ITC.Master" AutoEventWireup="true" CodeBehind="DanTocEdit.aspx.cs" Inherits="ToasoanTTXVN.BannerDanToc.DanTocEdit" %>


<%@ Register TagPrefix="nbc" Namespace="HPCComponents.UI" Assembly="HPCComponents" %>
<%@ Import Namespace="HPCComponents" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <script type="text/javascript">
        function f_SubmitImageQC(check) {
            SubmitImage('../UploadFileMulti/Video_News.aspx?vType=2&vKey=' + check + '', 840, 580);
        }

        function getPath(valuePath, numArg, Size, FileExtension) {
            if (parseInt(numArg) == 1) {
                document.getElementById("ctl00_MainContent_txtThumbnail").value = valuePath;
                document.getElementById("ctl00_MainContent_ImgTemp").src = '<%=HPCComponents.Global.UploadPath%>' + valuePath;
                document.getElementById("ctl00_MainContent_ImgTemp").style.display = '';
            }
            if (parseInt(numArg) == 2) {
                document.getElementById("ctl00_MainContent_Txt_DiachiQC").value = valuePath;
            }
            if (parseInt(numArg) == 3) {
                document.getElementById("ctl00_MainContent_txtImageVideo").value = valuePath;
                document.getElementById("ctl00_MainContent_ImagesVd").src = '<%=HPCComponents.Global.UploadPath%>' + valuePath + '';
                document.getElementById("ctl00_MainContent_ImagesVd").style.display = '';
            }

        }
        function f_SubmitImage_FileDoc(check) {
            SubmitImage("../UploadVideos/Videos_Managerment.aspx?vType=3&vKey=" + check + "", 840, 580);
        }
    </script>

    <table border="0" cellpadding="0" width="100%" cellspacing="0">
        <tr>
            <td class="datagrid_top_left">
            </td>
            <td class="datagrid_top_center">
                <table border="0" cellpadding="1" cellspacing="1" width="100%">
                    <tr>
                        <td style="width: 2%; text-align: left">
                            <img alt="CMS" src="../Dungchung/Images/Icons/cog-edit-icon.png" width="16px" height="16px" />
                        </td>
                        <td style="vertical-align: middle; text-align: left">
                            <span class="TitlePanel">Nhập thông tin</span>
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
                <table cellspacing="2" cellpadding="2" width="100%" border="0">
                    <tr>
                        <td colspan="4" align="right">
                            <asp:Label ID="lblMsg" runat="server" ForeColor="Red"></asp:Label><br>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;" class="Titlelbl">
                            Tên Dân tộc: <span class="req_Field">*</span>
                        </td>
                        <td style="text-align: left;">
                            <asp:TextBox ID="txtMota" TabIndex="7" runat="server" CssClass="inputtext" Width="400"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtMota"
                                Display="Dynamic" ErrorMessage="*" CssClass="req_Field" SetFocusOnError="True"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;" class="Titlelbl">
                            Đường dẫn:
                        </td>
                        <td style="text-align: left;">
                            <asp:TextBox ID="Txt_DiachiQC" TabIndex="7" runat="server" CssClass="inputtext" Width="400"></asp:TextBox>
                            <!--<input accesskey="S" onclick="f_SubmitImageQC(2)" class="PhotoSel" type="button"
                                value="Browse" name="cmd_SavePath2" />-->
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;" class="Titlelbl">
                            Ảnh đại diện Dân tộc: <span class="req_Field">*</span>
                        </td>
                        <td style="text-align: left;">
                            <asp:TextBox ID="txtImageVideo" TabIndex="7" runat="server" CssClass="inputtext"
                                Width="400"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtImageVideo"
                                Display="Dynamic" ErrorMessage="*" CssClass="req_Field" SetFocusOnError="True"></asp:RequiredFieldValidator>
                            <input class="PhotoSel" accesskey="S" onclick="f_SubmitImageQC(3)" type="button"
                                value="Browse" name="cmd_SavePath3" />
                            <img id="Img1" src="<%= Global.ApplicationPath %>/Dungchung/images/find.gif" onclick="f_ViewAds('<%=txtImageVideo.ClientID%>','<%=txtImageVideo.ClientID %>');"
                                alt="Click Xem" title="Click Xem" style="border: 0px; vertical-align: middle;
                                cursor: pointer;" />
                            <img style="cursor: pointer; vertical-align: middle;" onclick="document.getElementById('<%=txtImageVideo.ClientID%>').value = ''"
                                height="20" alt="Xóa ảnh" src="<%= Global.ApplicationPath %>/Dungchung/images/delete.gif"
                                width="20" border="0" />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;" class="Titlelbl">
                            Thứ tự:
                        </td>
                        <td style="text-align: left;">
                            <asp:TextBox ID="txtOrder" runat="server" Width="10%" CssClass="inputtext" onKeyPress='return check_num(this,4,event)'></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;" class="Titlelbl">
                            Hiển thị:
                        </td>
                        <td style="text-align: left;">
                            <asp:CheckBox ID="chkDisplay" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">
                        </td>
                        <td style="text-align: left;" class="Titlelbl">
                            -<u>Ghi chú: </u><span class="Titlelbl_ghichu">Các mục đánh dấu (<span class="req_Field">*</span>)
                                bắt buộc phải nhập.</span> </i>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td align="left">
                            <asp:Button runat="server" ID="linkSave" CssClass="iconSave" Font-Bold="true" OnClick="linkSave_Click"
                                Text="Lưu giữ"></asp:Button>
                            <asp:Button runat="server" CssClass="iconExit" ValidationGroup="Login" ID="linkExit"
                                Font-Bold="true" OnClick="LinkCancel_Click" Text="<%$ Resources:Strings, BUTTON_SIGOUT %>">
                            </asp:Button>
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
