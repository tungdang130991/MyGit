<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/ITC.Master" AutoEventWireup="true"
    CodeBehind="AdsPosEdit.aspx.cs" Inherits="ToasoanTTXVN.Quangcao.AdsPosEdit" %>

<%@ Import Namespace="HPCComponents" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <table border="0" cellpadding="0" width="100%" cellspacing="0">
        <tr>
            <td class="datagrid_top_left">
            </td>
            <td class="datagrid_top_center">
                <table border="0" cellpadding="1" cellspacing="1" width="100%">
                    <tr>
                        <td style="text-align: left; width: 2%">
                            <img alt="" src="../Images/Icons/cog-edit-icon.png" width="16px" height="16px" />
                        </td>
                        <td style="vertical-align: middle; text-align: left">
                            <span class="TitlePanel">CẬP NHẬT THÔNG TIN VỊ TRÍ QUẢNG CÁO</span>
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
                            Vị trí quảng cáo: <span class="req_Field">*</span>
                        </td>
                        <td style="text-align: left;">
                            <asp:TextBox ID="txt_Ads_Name" runat="server" Width="500px" CssClass="inputtext"></asp:TextBox>
                            &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txt_Ads_Name"
                                Display="Dynamic" ErrorMessage="*" CssClass="req_Field" SetFocusOnError="True"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;" class="Titlelbl">
                            Cách thức hiện thị:
                        </td>
                        <td style="text-align: left;">
                            <asp:DropDownList ID="cbo_Ads_DisplayType" runat="server" CssClass="inputtext" Width="512">
                                <asp:ListItem Selected="True" Value="1">Danh sách</asp:ListItem>
                                <asp:ListItem Value="2">Ngẫu nhiên</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;" class="Titlelbl">
                            Width: <span class="req_Field">*</span>
                        </td>
                        <td style="height: 20px; text-align: left;">
                            <asp:TextBox ID="txt_Ads_Width" runat="server" onKeyPress='return check_num(this,5,event)'
                                Width="100" CssClass="inputtext"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;" class="Titlelbl">
                            Height: <span class="req_Field">*</span>
                        </td>
                        <td style="text-align: left;">
                            <asp:TextBox ID="txt_Ads_Height" runat="server" Width="100" onKeyPress='return check_num(this,5,event)'
                                CssClass="inputtext"></asp:TextBox>
                        </td>
                    </tr>
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
                            <asp:Button runat="server" ID="linkSave" CssClass="iconAdd" Font-Bold="true" OnClick="linkSave_Click"
                                Text="<%$ Resources:Strings, BUTTON_SAVES %>"></asp:Button>
                            <asp:Button runat="server" ID="LinkCancel" CssClass="iconExit" CausesValidation="false"
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
