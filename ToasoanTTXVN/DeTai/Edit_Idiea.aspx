<%@ Page Language="C#" MasterPageFile="~/Masters/ITC.Master" AutoEventWireup="true"
    CodeBehind="Edit_Idiea.aspx.cs" Inherits="ToasoanTTXVN.DeTai.Edit_Idiea" %>

<%@ Import Namespace="HPCComponents" %>
<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Panel runat="server" ID="pnlEdit_Editor" CssClass="TitlePanel" BackColor="white">
        <table id="Table7" cellspacing="0" cellpadding="2" width="100%" border="0">
            <tr>
                <td style="width: 10%; text-align: right">
                    <span class="Titlelbl">Ngôn ngữ<font color="#ff0033">*</font>:</span>
                </td>
                <td style="width: 90%; text-align: left">
                    <asp:DropDownList ID="ddlLang" runat="server" Width="30%" CssClass="inputtext">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td style="width: 10%; text-align: right" class="Titlelbl">
                    <span class="Titlelbl">Chuyên mục<font color="#ff0033">*</font>:</span>
                </td>
                <td colspan="3" style="width: 90%; text-align: left">
                    <asp:DropDownList ID="cbo_chuyenmuc" runat="server" Width="30%" CssClass="inputtext"
                        DataTextField="tenchuyenmuc" DataValueField="id" TabIndex="5">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td style="width: 10%; text-align: right; height: 30;">
                    <span class="Titlelbl">tiêu đề<font color="#ff0033">*</font>:</span>
                </td>
                <td colspan="3" style="width: 90%; text-align: left; height: 30;">
                    <asp:TextBox ID="Txt_tieude" TabIndex="7" runat="server" CssClass="inputtext" Width="80%"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="width: 10%; text-align: right" valign="top">
                    <span class="Titlelbl">Nội dung<font color="#ff0033">*</font>:</span>
                </td>
                <td style="width: 90%; text-align: left">
                    <CKEditor:CKEditorControl ID="txt_noidung" runat="server" BasePath="~/ckeditor/"
                        Toolbar="Noidung" ContentsCss="~/ckeditor/contents.css" Height="400px" Width="99%"
                        ToolbarStartExpanded="true">
                    </CKEditor:CKEditorControl>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txt_noidung"
                        Display="Dynamic" ErrorMessage="Bạn phải nhập nội dung tin bài" SetFocusOnError="True"
                        InitialValue='6'></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td align="center" colspan="2">
                    <div class="processbuttonone" style="clear: left; text-align: center;">
                        <asp:LinkButton runat="server" ID="linkSave" CssClass="iconSave" OnClick="linkSave_Click"
                            Text="<%$ Resources:Strings, BUTTON_LUU %>">
                                    
                        </asp:LinkButton>
                        <asp:LinkButton runat="server" ID="LinkButtonSend" CssClass="iconSend" OnClick="linkSend_Click"
                            Text="Gửi đề tài">
                                    
                        </asp:LinkButton>
                        <asp:LinkButton runat="server" ValidationGroup="Login" ID="linkExit" CssClass="iconExit"
                            OnClick="linkExit_Click" Text="<%$ Resources:Strings, BUTTON_SIGOUT %>">
                        </asp:LinkButton>
                    </div>
                </td>
            </tr>
            <tr>
                <td style="height: 20px">
                </td>
            </tr>
        </table>
    </asp:Panel>
</asp:Content>
