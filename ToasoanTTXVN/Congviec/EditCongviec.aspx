<%@ Page Language="C#" MasterPageFile="~/Masters/ITC.Master" AutoEventWireup="true"
    CodeBehind="EditCongviec.aspx.cs" Inherits="ToasoanTTXVN.Congviec.EditCongviec" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxtoolkit" %>
<%@ Import Namespace="HPCComponents" %>
<%@ Register TagPrefix="nbc" Namespace="HPCComponents.UI" Assembly="HPCComponents" %>
<%@ Register TagPrefix="CKEditor" Namespace="CKEditor.NET" Assembly="CKEditor.NET" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <script type="text/javascript" language="javascript">

        function check_num(obj, length, e) {
            var key = window.event ? e.keyCode : e.which;
            var len = obj.value.length + 1;
            if (length <= 3) begin = 48; else begin = 45;
            if (key >= begin && key <= 57 && len <= length || (key == 8 || key == 0)) {
            }
            else return false;
        }
        function ValidateText(i) {
            if (i.value.length > 0) {
                i.value = i.value.replace(/[^\d]+/g, '');
            }
        }
        
    </script>

    <script type="text/javascript">
        $(document).ready(function() {
            $("#<%=txt_NgayHT.ClientID%>").datepicker({
                showOn: 'button',
                buttonText: 'Show Date',
                buttonImageOnly: true,
                buttonImage: '../Dungchung/Images/DatePicker/calendar.png'
            });

        });
    </script>

    <table border="0" cellpadding="0" width="100%" cellspacing="0">
        <tr>
            <td>
            </td>
            <td style="text-align: center; width: 90%">
                <table width="100%" align="center" cellpadding="4" cellspacing="4">
                    <tr>
                        <td style="width: 15%; text-align: right;" class="Titlelbl">
                            <%=CommonLib.ReadXML("lblCongviec")%>(<span class="req_Field">*</span>):
                        </td>
                        <td style="text-align: left;">
                            <asp:TextBox ID="txt_tencongviec" CssClass="inputtext" runat="server" Width="70%"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 15%; text-align: right;" class="Titlelbl">
                            <%=CommonLib.ReadXML("lblNoidung")%>(<span class="req_Field">*</span>):
                        </td>
                        <td style="text-align: left;">
                            <asp:TextBox ID="txt_NoidungCV" TabIndex="16" Width="70%" runat="server" CssClass="inputtext"
                                TextMode="MultiLine" Rows="6"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 15%; text-align: right;" class="Titlelbl">
                            <%=CommonLib.ReadXML("lblPhanhoi")%>:
                        </td>
                        <td style="text-align: left;">
                            <asp:TextBox ID="txt_phanhoi" TabIndex="16" Width="70%" runat="server" CssClass="inputtext"
                                TextMode="MultiLine" Rows="4"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 15%; text-align: right;" class="Titlelbl">
                            <%=CommonLib.ReadXML("lblPhongban")%>:
                        </td>
                        <td style="text-align: left;">
                            <asp:UpdatePanel ID="upnlphongban" runat="server">
                                <ContentTemplate>
                                    <asp:DropDownList ID="cbo_room" runat="server" AutoPostBack="true" CssClass="inputtext"
                                        Width="70%" OnSelectedIndexChanged="cbo_room_OnSelectedIndexChanged">
                                    </asp:DropDownList>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 15%; text-align: right;" class="Titlelbl">
                            <%=CommonLib.ReadXML("lblNguoiNhan")%>:
                        </td>
                        <td style="text-align: left;">
                            <asp:UpdatePanel ID="UpdatePanelnguoinhan" runat="server">
                                <ContentTemplate>
                                    <asp:DropDownList ID="cbo_nguoinhan" runat="server" CssClass="inputtext" Width="50%">
                                    </asp:DropDownList>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right; width: 15%" class="Titlelbl">
                            <%=CommonLib.ReadXML("lblNgayHoanThanh")%>(<span class="req_Field">*</span>):
                        </td>
                        <td style="text-align: left;">
                            <asp:TextBox ID="txt_NgayHT" runat="server" Width="150px" CssClass="inputtext" MaxLength="10"
                                onKeyPress="AscciiDisable()" onfocus="javascript:vDateType='3'" onKeyUp="DateFormat(this,this.value,event,false,'3')"
                                onBlur="DateFormat(this,this.value,event,true,'3')"></asp:TextBox>
                            <asp:RegularExpressionValidator ID="Regularexpressionvalidator2" runat="server" ControlToValidate="txt_NgayHT"
                                ValidationExpression="^\d{1,2}/\d{1,2}/\d{4}$" Display="Dynamic">Kiểu ngày:dd/MM/yyyy</asp:RegularExpressionValidator>
                            <asp:CustomValidator ID="CustomValidator1" runat="server" OnServerValidate="ValidateNgayHT"
                                Display="Dynamic" ControlToValidate="txt_NgayHT" ErrorMessage="Ngày hoàn thành lớn hơn ngày hiện tại"
                                SetFocusOnError="True"></asp:CustomValidator>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right; width: 15%" class="Titlelbl">
                            <%=CommonLib.ReadXML("lblDinhkemfile")%>:
                        </td>
                        <td style="text-align: left;">
                            <asp:FileUpload runat="server" ID="Filedinhkem" CssClass="inputtext" Width="30%" />
                            <a id="txt_attachfile" href="" runat="server" target="_blank" class="Titlelbl" visible="false">
                                <img src="../Dungchung/Images/attachment.png" alt='' />
                            </a>
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
                        <td colspan="2" style="text-align: center">
                            <div style="float: left; width: 100%">
                                <asp:LinkButton runat="server" ID="linkSave" CssClass="iconSave" OnClick="Save_Click"
                                    Text="<%$ Resources:cms.language, lblLuu %>">
                                </asp:LinkButton>
                                <asp:LinkButton runat="server" ID="LinkCancel" CssClass="iconExit" OnClick="Cancel_Click"
                                    Text="<%$ Resources:cms.language, lblThoat %>" CausesValidation="false">
                                </asp:LinkButton>
                            </div>
                        </td>
                    </tr>
                </table>
            </td>
            <td>
            </td>
        </tr>
    </table>
</asp:Content>
