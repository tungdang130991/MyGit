<%@ Page Language="C#" MasterPageFile="~/Masters/ITC.Master" AutoEventWireup="true"
    CodeBehind="Edit_DuyetDeTaiTBT.aspx.cs" Inherits="ToasoanTTXVN.DeTai.Edit_DuyetDeTaiTBT" %>

<%@ Import Namespace="HPCComponents" %>
<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Panel runat="server" ID="pnlEdit_Editor" CssClass="TitlePanel" BackColor="white">
        <table id="Table7" cellspacing="0" cellpadding="4" width="100%" border="0">
            <tr>
                <td style="width: 10%; text-align: right" class="Titlelbl">
                    <span class="Titlelbl">Ngôn ngữ</span> <font color="#ff0033">(*)</font>:
                </td>
                <td style="width: 90%; text-align: left">
                    <asp:DropDownList ID="ddlLang" runat="server" Width="50%" CssClass="inputtext">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td style="width: 10%; text-align: right" class="Titlelbl">
                    <span class="Titlelbl">Chuyên mục</span> <font color="#ff0033">(*)</font>:
                </td>
                <td style="width: 90%; text-align: left">
                    <asp:DropDownList ID="cbo_chuyenmuc" runat="server" Width="50%" CssClass="inputtext"
                        DataTextField="tenchuyenmuc" DataValueField="id" TabIndex="5">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td style="width: 10%; text-align: right" class="Titlelbl">
                    <span class="Titlelbl">Loại đề tài: </span>
                </td>
                <td style="width: 90%; text-align: left">
                    <asp:DropDownList ID="cbb_Loai" runat="server" Width="50%" OnSelectedIndexChanged="cbb_Loai_SelectedIndexChanged"
                        CssClass="inputtext" AutoPostBack="true">
                        <asp:ListItem Text="Đề tài đã thực hiện" Selected="True" Value="1"></asp:ListItem>
                        <asp:ListItem Text="Đề tài chưa thực hiện" Value="2"></asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td style="width: 10%; text-align: right; height: 30;">
                    <span class="Titlelbl">Tên đề tài<font color="#ff0033">(*)</font>:</span>
                </td>
                <td style="width: 90%; text-align: left; height: 30;">
                    <asp:TextBox ID="Txt_tieude" TabIndex="7" runat="server" CssClass="inputtext" Width="80%"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="width: 10%; text-align: right" valign="top">
                    <span class="Titlelbl">Nội dung đề tài:</span>
                </td>
                <td style="width: 90%; text-align: left">
                    <CKEditor:CKEditorControl ID="txt_noidung" runat="server" BasePath="~/ckeditor/"
                        Toolbar="Noidung" ContentsCss="~/ckeditor/contents.css" Height="400px" Width="98%"
                        ToolbarStartExpanded="true">
                    </CKEditor:CKEditorControl>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txt_noidung"
                        Display="Dynamic" ErrorMessage="Bạn phải nhập nội dung tin bài" SetFocusOnError="True"
                        InitialValue='6'></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td style="width: 10%; text-align: right" valign="top">
                    <asp:Label ID="lb_noidungbaiviet" runat="server" CssClass="Titlelbl">Nội dung bài viết:( <font color="red">*</font>)</asp:Label>
                </td>
                <td style="width: 90%; text-align: left">
                    <CKEditor:CKEditorControl ID="txt_noidungbaiviet" runat="server" BasePath="~/ckeditor/"
                        Toolbar="Noidung" ContentsCss="~/ckeditor/contents.css" Height="400px" Width="98%"
                        toolbarstartexpanded="true" Visible="true">
                    </CKEditor:CKEditorControl>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txt_noidungbaiviet"
                        Display="Dynamic" ErrorMessage="Bạn phải nhập nội dung tin bài" SetFocusOnError="True"
                        InitialValue='6'></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td align="center" colspan="2">
                    <asp:LinkButton runat="server" ID="linkSave" CssClass="iconSave" OnClick="linkSave_Click"
                        Text="<%$ Resources:Strings, BUTTON_LUU %>">
                                    
                    </asp:LinkButton>
                    <asp:LinkButton runat="server" ID="LinkButtonDuyet" CssClass="iconSend" OnClick="linkDuyet_Click"
                        Text="Duyệt đề tài">
                                    
                    </asp:LinkButton>
                    <asp:LinkButton runat="server" ValidationGroup="Login" ID="linkExit" CssClass="iconExit"
                        OnClick="linkExit_Click" Text="<%$ Resources:Strings, BUTTON_SIGOUT %>">
                    </asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td style="height: 20px">
                </td>
            </tr>
        </table>
    </asp:Panel>
</asp:Content>
