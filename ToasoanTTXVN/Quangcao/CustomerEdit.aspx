<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/ITC.Master" AutoEventWireup="true"
    CodeBehind="CustomerEdit.aspx.cs" Inherits="ToasoanTTXVN.Quangcao.CustomerEdit" %>

<%@ Import Namespace="HPCComponents" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <table border="0" cellpadding="0" width="100%" cellspacing="0">
        <tr>
            <td class="datagrid_top_left">
            </td>
            <td class="datagrid_top_center">
                <table border="0" cellpadding="1" cellspacing="1" width="100%">
                    <tr>
                        <td style="width: 2%; text-align: left">
                            <img src="../Dungchung/Images/Icons/cog-edit-icon.png" width="16px" height="16px" />
                        </td>
                        <td style="vertical-align: middle; text-align: left">
                            <span class="TitlePanel">CẬP NHẬT THÔNG TIN KHÁCH HÀNG</span>
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
                <table width="100%" cellpadding="2" cellspacing="2" style="text-align: center">
                    <tr>
                        <td style="width: 30%; text-align: right;" class="Titlelbl">
                            Tên khách hàng: <span class="req_Field">*</span>
                        </td>
                        <td style="text-align: left;">
                            <asp:TextBox ID="txt_Name" runat="server" Width="60%" CssClass="inputtext" MaxLength="125"></asp:TextBox>
                            <br />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txt_Name"
                                Display="Dynamic" ErrorMessage="Chưa nhập tên khách hàng" Font-Size="Small" SetFocusOnError="True"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;" class="Titlelbl">
                            Địa chỉ:
                        </td>
                        <td style="text-align: left;">
                            <asp:TextBox ID="Txt_Address" runat="server" Width="60%" CssClass="inputtext" TextMode="MultiLine"
                                Rows="3"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;" class="Titlelbl">
                            Điện thoại:
                        </td>
                        <td style="height: 20px; text-align: left;">
                            <asp:TextBox ID="txt_Phone" runat="server" CssClass="inputtext" Width="60%"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;" class="Titlelbl">
                            Số Fax:
                        </td>
                        <td style="text-align: left;" class="Titlelbl">
                            <asp:TextBox ID="txt_Fax" runat="server" Width="60%" CssClass="inputtext"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;" class="Titlelbl">
                            Email:
                        </td>
                        <td style="text-align: left;">
                            <asp:TextBox ID="txt_Email" runat="server" Width="60%" CssClass="inputtext"></asp:TextBox>
                        </td>
                    </tr>
                    <%--  <tr>
                <td style="width: 30%; height: 20px; text-align: right;">
                    <asp:Label ID="Label3" runat="server" CssClass="Titlelbl" Text="Ngày nhập:"></asp:Label>&nbsp;
                </td>
                <td style="height: 20px; text-align: left;">
                    <asp:TextBox ID="txt_Date_Created" runat="server" Width="200px" Enabled="false"></asp:TextBox></td>
            </tr>--%>
                    <tr>
                        <td>
                        </td>
                        <td style="text-align: left" class="Titlelbl_ghichu">
                            - <u>Ghi chú:</u> &nbsp;Các ô có đánh dấu <span class="req_Field">*</span> là trường
                            bắt buộc phải nhập.
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td align="left">
                            <asp:Button runat="server" ID="linkSave" Font-Bold="true" CssClass="iconSave" OnClick="linkSave_Click"
                                Text="<%$ Resources:Strings, BUTTON_SAVES %>"></asp:Button>
                            <asp:Button runat="server" ID="LinkCancel" CausesValidation="false" Font-Bold="true"
                                CssClass="iconExit" OnClick="LinkCancel_Click" Text="<%$ Resources:Strings, BUTTON_SIGOUT %>">
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
