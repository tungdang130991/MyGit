<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/ITC.Master" AutoEventWireup="true"
    CodeBehind="EditGroup.aspx.cs" Inherits="ToasoanTTXVN.Hethong.EditGroup" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <table border="0" cellpadding="0" width="100%" cellspacing="0">
        <tr>
            <td class="datagrid_top_left">
            </td>
            <td class="datagrid_top_center">
                <span class="TitlePanel" style="float: left;"></span>
            </td>
            <td class="datagrid_top_right">
            </td>
        </tr>
        <tr>
            <td class="datagrid_content_left">
            </td>
            <td style="text-align: left">
                <table width="100%" cellpadding="0" cellspacing="0">
                    <tr>
                        <td align="center">
                            <table cellspacing="2" cellpadding="2" width="50%" border="0">
                                <tr>
                                    <td align="right" class="Titlelbl" style="width: 20%">
                                        <asp:Label ID="lblUserName" runat="server" Text="<%$ Resources:cms.language, lblNhom %>"></asp:Label>(<span
                                            class="req_Field">*</span>):&nbsp;
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txtName" runat="server" CssClass="inputtext" Width="80%"></asp:TextBox><br />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Bạn phải nhập tên nhóm."
                                            ControlToValidate="txtName" Display="Dynamic" SetFocusOnError="True" Font-Size="Small"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" class="Titlelbl">
                                        <asp:Label ID="lblPass" runat="server" Text="<%$ Resources:cms.language, lblMota %>"></asp:Label>:
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txtDesc" runat="server" CssClass="inputtext" Width="80%"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
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
                    <tr>
                        <td colspan="2" style="text-align: center">
                            <asp:LinkButton runat="server" ID="linkSave" CssClass="iconSave" OnClick="linkSave_Click"
                                Text="<%$ Resources:cms.language, lblLuu %>" Font-Bold="true">
                            </asp:LinkButton>
                            <asp:LinkButton runat="server" ID="LinkCancel" CssClass="iconExit" OnClick="LinkCancel_Click"
                                CausesValidation="false" Text="<%$ Resources:cms.language, lblThoat %>" Font-Bold="true">
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
