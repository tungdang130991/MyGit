﻿<%@ Page Language="C#" MasterPageFile="~/Masters/ITC.Master" AutoEventWireup="true"
    CodeBehind="Edit_XuLyCongViec.aspx.cs" Inherits="ToasoanTTXVN.DeTai.Edit_XuLyCongViec" %>

<%@ Import Namespace="HPCComponents" %>
<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div style="width: 100%; float: left; margin-bottom: 10px">
        <table id="Table7" cellspacing="2" cellpadding="2" width="100%" border="0">
            <tr>
                <td style="width: 12%; text-align: right">
                    <span class="Titlelbl">Ngôn ngữ<font color="#ff0033">*</font>:</span>
                </td>
                <td style="width: 88%; text-align: left">
                    <asp:DropDownList ID="ddlLang" runat="server" Width="30%" CssClass="inputtext">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td style="width: 12%; text-align: right" class="Titlelbl">
                    <span class="Titlelbl">Chuyên mục<font color="#ff0033">*</font>:</span>
                </td>
                <td style="width: 88%; text-align: left">
                    <asp:DropDownList ID="cbo_chuyenmuc" runat="server" Width="30%" CssClass="inputtext"
                        TabIndex="5">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td style="width: 88%; text-align: left" colspan="2">
                    <asp:DropDownList ID="cbb_Loai" runat="server" Width="30%" OnSelectedIndexChanged="cbb_Loai_SelectedIndexChanged"
                        AutoPostBack="true" Visible="false">
                        <asp:ListItem Text="Đề tài đã thực hiện" Selected="True" Value="1"></asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td style="width: 12%; text-align: right" valign="top">
                    <span class="Titlelbl">Nội dung đề tài<font color="#ff0033">*</font>:</span>
                </td>
                <td style="width: 88%; text-align: left">
                    <CKEditor:CKEditorControl ID="txt_noidung" runat="server" BasePath="~/ckeditor/"
                        Toolbar="Noidung" ContentsCss="~/ckeditor/contents.css" Height="300px" Width="99%"
                        ToolbarStartExpanded="true">
                    </CKEditor:CKEditorControl>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txt_noidung"
                        Display="Dynamic" ErrorMessage="Bạn phải nhập nội dung tin bài" SetFocusOnError="True"
                        InitialValue='6'></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td style="width: 12%; text-align: right; height: 30;">
                    <span class="Titlelbl">Tên bài viết<font color="#ff0033">*</font>:</span>
                </td>
                <td style="width: 88%; text-align: left; height: 30;">
                    <asp:TextBox ID="Txt_tieude" TabIndex="7" runat="server" CssClass="inputtext" Width="88%"></asp:TextBox><br />
                </td>
            </tr>
            <tr>
                <td style="width: 12%; text-align: right" valign="top">
                    <span id="lb_noidungbaiviet" runat="server" class="Titlelbl">Nội dung tin bài<font
                        color="#ff0033">*</font>:</span>
                </td>
                <td style="width: 88%; text-align: left">
                    <CKEditor:CKEditorControl ID="txt_noidungbaiviet" runat="server" BasePath="~/ckeditor/"
                        Toolbar="Noidung" ContentsCss="~/ckeditor/contents.css" Height="400px" Width="99%"
                        ToolbarStartExpanded="true">
                    </CKEditor:CKEditorControl>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txt_noidungbaiviet"
                        Display="Dynamic" ErrorMessage="Bạn phải nhập nội dung tin bài" SetFocusOnError="True"
                        InitialValue='6'></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td align="center" colspan="2">
                    <asp:LinkButton runat="server" ID="linkSave" CssClass="iconSave" OnClick="linkSave_Click"
                        Text="Lưu giữ">
                                    
                    </asp:LinkButton>
                    <asp:LinkButton runat="server" ValidationGroup="Login" ID="linkExit" CssClass="iconExit"
                        OnClick="linkExit_Click" Text="Quay lại">
                    </asp:LinkButton>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
