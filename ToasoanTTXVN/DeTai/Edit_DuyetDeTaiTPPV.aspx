<%@ Page Language="C#" MasterPageFile="~/Masters/ITC.Master" AutoEventWireup="true"
    CodeBehind="Edit_DuyetDeTaiTPPV.aspx.cs" Inherits="ToasoanTTXVN.DeTai.Edit_DuyetDeTaiTPPV" %>

<%@ Register TagPrefix="nbc" Namespace="HPCComponents.UI" Assembly="HPCComponents" %>
<%@ Import Namespace="HPCComponents" %>
<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <script type="text/javascript">
        $(document).ready(function() {
            $("#<%=txt_FromDate.ClientID%>").datepicker({
                showOn: 'button',
                buttonText: 'Show Date',
                buttonImageOnly: true,
                buttonImage: '../Dungchung/Images/DatePicker/calendar.png'
            });
            $("#<%=txt_ToDate.ClientID%>").datepicker({
                showOn: 'button',
                buttonText: 'Show Date',
                buttonImageOnly: true,
                buttonImage: '../Dungchung/Images/DatePicker/calendar.png'
            });
        });
    </script>

    <script type="text/javascript" language="javascript">
        function check_num(obj, length, e) {
            var key = window.event ? e.keyCode : e.which;
            var len = obj.value.length + 1;
            if (length <= 3) begin = 48; else begin = 45;
            if (key >= begin && key <= 57 && len <= length || (key == 8 || key == 0)) {
            }
            else return false;
        }
        function valComments_ClientValidate(source, args) {
            var tdate = args.Value
            var dd = tdate.slice(0, 2);
            var dd = new Number(dd);
            var mm = tdate.slice(3, 5);
            var mm = new Number(mm);
            var yyyy = tdate.slice(6, 10);
            var yyyy = new Number(yyyy);
            var today = new Date();
            var testyyyy = yyyy % 4;
            if (mm < 13 && mm > 0) {
                if (mm == 1 || mm == 3 || mm == 5 || mm == 7 || mm == 8 || mm == 10 || mm == 12) {
                    if (dd >= 1 && dd <= 31) {
                        args.IsValid = true;
                    }
                    else {
                        args.IsValid = false;

                    }
                }
                else if (mm == 2) {
                    if (testyyyy == 0 && dd <= 29 && dd >= 1)
                    { args.IsValid = true; }

                    else if (dd <= 28 && dd >= 1) {
                        args.IsValid = true;

                    }
                    else { args.IsValid = false; }


                }
                else {
                    if (dd >= 1 && dd <= 30)
                    { args.IsValid = true }
                    else { args.IsValid = false }
                }
            }
            else
                args.IsValid = false;
        }
    </script>

    <table id="Table7" cellspacing="0" cellpadding="2" width="100%" border="0">
        <tr>
            <td style="width: 10%; text-align: right">
                <span class="Titlelbl">Ngôn ngữ:</span> <font color="#ff0033">*</font>
            </td>
            <td style="width: 90%; text-align: left">
                <asp:DropDownList ID="ddlLang" runat="server" Width="30%" CssClass="inputtext">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td style="width: 10%; text-align: right" class="Titlelbl">
                <span class="Titlelbl">Chuyên mục:</span> <font color="#ff0033">*</font>
            </td>
            <td style="width: 90%; text-align: left">
                <asp:DropDownList ID="cbo_chuyenmuc" runat="server" Width="30%" CssClass="inputtext"
                    DataTextField="tenchuyenmuc" DataValueField="id" TabIndex="5">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td style="width: 10%; text-align: right; height: 30;">
                <span class="Titlelbl">tiêu đề:</span> <font color="#ff0033">*</font>
            </td>
            <td style="width: 90%; text-align: left; height: 30;">
                <asp:TextBox ID="Txt_tieude" TabIndex="7" runat="server" CssClass="inputtext" Width="90%"></asp:TextBox><br />
            </td>
        </tr>
        <tr>
            <td style="width: 10%; text-align: right" class="Titlelbl">
                <asp:Literal runat="server" ID="Literal4" Text="Loại đề tài:" Visible="false"></asp:Literal>
            </td>
            <td style="width: 90%; text-align: left">
                <asp:DropDownList ID="cbb_Loai" runat="server" Width="30%" OnSelectedIndexChanged="cbb_Loai_SelectedIndexChanged"
                    CssClass="inputtext" AutoPostBack="true" Visible="false">
                    <asp:ListItem Text="Đề tài đã thực hiện" Selected="True" Value="1"></asp:ListItem>
                    <asp:ListItem Text="Đề tài chưa thực hiện" Value="2"></asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td style="width: 10%; text-align: right" valign="top">
                <span class="Titlelbl">Nội dung đề tài:</span> <font color="#ff0033">*</font>
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
                <span id="lb_noidungbaiviet" runat="server" class="Titlelbl">Nội dung bài viết:( <font
                    color="red">*</font>)</span>
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
            <td colspan="2" style="width: 100%">
                <asp:Panel runat="server" ID="pnlEdit_Editor1" CssClass="TitlePanel" BackColor="white">
                    <table border="0" cellpadding="1" cellspacing="1" style="width: 100%">
                        <tr>
                            <td style="width: 10%; text-align: right" class="Titlelbl">
                                <span class="Titlelbl">Loại yêu cầu:</span>
                            </td>
                            <td style="width: 90%; text-align: left">
                                <asp:DropDownList ID="cbb_LoaiBaiviet" runat="server" CssClass="inputtext" Width="30%">
                                    <asp:ListItem Text="Bài viết" Selected="True" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="Ảnh" Value="2"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 10%; text-align: right;">
                                <span class="Titlelbl">Từ ngày:</span> <font color="#ff0033">*</font>
                            </td>
                            <td style="width: 90%; text-align: left;">
                                <asp:TextBox ID="txt_FromDate" runat="server" Width="120px" CssClass="inputtext"
                                    MaxLength="10" onkeypress="AscciiDisable()" onfocus="javascript:vDateType='3'"
                                    onkeyup="DateFormat(this,this.value,event,false,'3')" onblur="DateFormat(this,this.value,event,true,'3')"></asp:TextBox>
                                <asp:RegularExpressionValidator ID="Regularexpressionvalidator3" runat="server" ControlToValidate="txt_FromDate"
                                    ValidationExpression="^\d{1,2}/\d{1,2}/\d{4}$" Display="Dynamic">Kiểu ngày:dd/MM/yyyy</asp:RegularExpressionValidator>
                                <asp:CustomValidator ID="CustomValidator1" runat="server" ErrorMessage="Ngày tháng ko tồn tại"
                                    ClientValidationFunction="valComments_ClientValidate" ControlToValidate="txt_FromDate"
                                    SetFocusOnError="true"></asp:CustomValidator>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 10%; text-align: right;">
                                <span class="Titlelbl">Đến ngày:</span> <font color="#ff0033">*</font>
                            </td>
                            <td style="width: 90%; text-align: left;">
                                <asp:TextBox ID="txt_ToDate" runat="server" Width="120px" CssClass="inputtext" MaxLength="10"
                                    onkeypress="AscciiDisable()" onfocus="javascript:vDateType='3'" onkeyup="DateFormat(this,this.value,event,false,'3')"
                                    onblur="DateFormat(this,this.value,event,true,'3')"></asp:TextBox>
                                <asp:RegularExpressionValidator ID="Regularexpressionvalidator1" runat="server" ControlToValidate="txt_ToDate"
                                    ValidationExpression="^\d{1,2}/\d{1,2}/\d{4}$" Display="Dynamic">Kiểu ngày:dd/MM/yyyy</asp:RegularExpressionValidator>
                                <asp:CustomValidator ID="CustomValidator2" runat="server" ErrorMessage="Ngày tháng ko tồn tại"
                                    ClientValidationFunction="valComments_ClientValidate" ControlToValidate="txt_ToDate"
                                    SetFocusOnError="true"></asp:CustomValidator>
                            </td>
                        </tr>
                        <tr>
                            <td style="height: 20px; text-align: right;">
                                <span class="Titlelbl">Chọn nhóm:</span> <font color="#ff0033">*</font>
                            </td>
                            <td style="width: 90%; text-align: left">
                                <asp:UpdatePanel ID="upnlgroup" runat="server">
                                    <ContentTemplate>
                                        <asp:DropDownList ID="ddlGroup" runat="server" AutoPostBack="true" Width="30%" CssClass="inputtext"
                                            OnSelectedIndexChanged="ddlGroup_SelectedIndexChanged" TabIndex="5">
                                        </asp:DropDownList>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 10%; text-align: right;">
                                <span class="Titlelbl">Người nhận việc:</span> <font color="#ff0033">*</font>
                            </td>
                            <td style="width: 90%; text-align: left">
                                <asp:UpdatePanel ID="UpdatePanelnguoinhan" runat="server">
                                    <ContentTemplate>
                                        <asp:DropDownList ID="cbo_NguoiNhan" runat="server" Width="30%" CssClass="inputtext"
                                            TabIndex="5">
                                        </asp:DropDownList>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td align="center" colspan="2">
                <asp:LinkButton runat="server" ID="linkSave" CssClass="iconSave" OnClick="linkSave_Click"
                    Text="<%$ Resources:Strings, BUTTON_LUU %>">
                                    
                </asp:LinkButton>
                <asp:LinkButton runat="server" ID="linkbtn_SendTPBT" CssClass="iconSend" OnClick="linkbtn_SendTPBT_Click"
                    Text="Gửi TPBT">
                                    
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
</asp:Content>
