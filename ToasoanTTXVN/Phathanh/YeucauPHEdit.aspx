<%@ Page Language="C#" MasterPageFile="~/Masters/ITC.Master" AutoEventWireup="true"
    CodeBehind="YeucauPHEdit.aspx.cs" Inherits="ToasoanTTXVN.Phathanh.YeucauPHEdit"
    Title="" %>

<%@ Register TagPrefix="CKeditor" Namespace="CKEditor.NET" Assembly="CKEditor.NET" %>
<%@ Register TagPrefix="anthem" Namespace="Anthem" Assembly="Anthem" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <table border="0" cellpadding="0" width="100%" cellspacing="0">
        <tr>
            <td class="datagrid_top_left">
            </td>
            <td class="datagrid_top_center">
                <table border="0" cellpadding="1" cellspacing="1">
                    <tr>
                        <td>
                            <img src="../../Dungchung/Images/Icons/cog-edit-icon.png" width="16px" height="16px" />
                        </td>
                        <td style="vertical-align: middle">
                            <span class="TitlePanel">CẬP NHẬT YÊU CẦU PHÁT HÀNH</span>
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
            <td>
                <table border="0" cellpadding="2" cellspacing="1" align="center" width="80%">
                    <tr>
                        <td style="text-align: right; width: 20%" class="Titlelbl">
                            Tên khách hàng
                        </td>
                        <td style="text-align: left; width: 80%" class="Titlelbl">
                            <anthem:DropDownList AutoCallBack="true" ID="ddl_TenKH" CssClass="inputtext" runat="server"
                                Width="50%" DataTextField="Ten_KhachHang" DataValueField="Ma_KhachHang" AppendDataBoundItems="true">
                            </anthem:DropDownList>
                            (<span class="req_Field">*</span>)
                            <anthem:RequiredFieldValidator ID="reqValidator1" runat="server" ErrorMessage="Chưa chọn khách hàng"
                                ControlToValidate="ddl_TenKH" InitialValue="0" Display="Dynamic" SetFocusOnError="True" Font-Size="Small"></anthem:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right; width: 20%" class="Titlelbl">
                            Tiêu đề
                        </td>
                        <td style="text-align: left; width: 80%" class="Titlelbl">
                            <asp:TextBox ID="txt_Tieude" runat="server" Width="80%" CssClass="inputtext"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right; width: 20%" class="Titlelbl">
                            Nội dung
                        </td>
                        <td style="text-align: left; width: 80%" class="Titlelbl">
                            <CKeditor:CKEditorControl ID="ckeNoidung" Toolbar="Noidung" runat="server" BasePath="~/ckeditor"
                                ContentsCss="~/ckeditor/contents.css" Height="300px">
                            </CKeditor:CKEditorControl>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:ValidationSummary ID="vs_Themmoi" runat="server" ValidationGroup="ValidCapNhap"
                                ShowSummary="false" />
                            <asp:Label ID="txtMessage" CssClass="Error" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" style="height: 10px">
                        </td>
                    </tr>
                    <tr align="center">
                        <td>
                        </td>
                        <td align="left">
                            <asp:LinkButton runat="server" ID="linkSave" Font-Bold="true" CssClass="myButton blue"
                                OnClick="Save_Click" Text="<%$ Resources:Strings, BUTTON_SAVES %>" Width="90px">
                            </asp:LinkButton>
                            <asp:LinkButton runat="server" Font-Bold="true" ID="LinkCancel" CssClass="myButton blue"
                                OnClick="Cancel_Click" Text="<%$ Resources:Strings, BUTTON_SIGOUT %>" CausesValidation="false"
                                Width="90px">

                            </asp:LinkButton>
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
