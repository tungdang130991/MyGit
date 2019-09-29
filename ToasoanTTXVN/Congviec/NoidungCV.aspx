<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/ITC.Master" AutoEventWireup="true"
    CodeBehind="NoidungCV.aspx.cs" Inherits="ToasoanTTXVN.Congviec.NoidungCV" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxtoolkit" %>
<%@ Import Namespace="HPCComponents" %>
<%@ Register TagPrefix="anthem" Namespace="Anthem" Assembly="Anthem" %>
<%@ Register TagPrefix="nbc" Namespace="HPCComponents.UI" Assembly="HPCComponents" %>
<%@ Register TagPrefix="CKEditor" Namespace="CKEditor.NET" Assembly="CKEditor.NET" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <table border="0" cellpadding="0" width="100%" cellspacing="0">
        <tr>
            <td class="datagrid_content_left">
            </td>
            <td>
                <table width="100%" align="center" cellpadding="2" cellspacing="2">
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
                            <asp:DropDownList ID="cbo_room" runat="server" AutoPostBack="true" CssClass="inputtext"
                                Width="70%">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 15%; text-align: right;" class="Titlelbl">
                            <%=CommonLib.ReadXML("lblNguoiNhan")%>:
                        </td>
                        <td style="text-align: left;">
                            <asp:DropDownList ID="cbo_nguoinhan" runat="server" CssClass="inputtext" Width="50%">
                            </asp:DropDownList>
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
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right; width: 15%" class="Titlelbl">
                            <%=CommonLib.ReadXML("lblDinhkemfile")%>:
                        </td>
                        <td style="text-align: left;">
                            <a id="txt_attachfile" href="" runat="server" target="_blank" class="Titlelbl" visible="false">
                                <img src="../Dungchung/Images/attachment.png" alt='' />
                            </a>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" style="height: 10px">
                        </td>
                    </tr>
                    <tr>
                        <td align="center" colspan="2">
                            <div style="padding-left: 200px; float: left">
                                <asp:LinkButton runat="server" ID="linkSave" CssClass="iconSave" OnClick="Save_Click"
                                    Text="<%$ Resources:cms.language, lblHoanthanh %>">
                                </asp:LinkButton>
                                <asp:LinkButton runat="server" ID="Linkhuycv" CssClass="iconSave" OnClick="Linkhuycv_Click"
                                    Text="<%$ Resources:cms.language, lblHuyviec %>">
                                </asp:LinkButton>
                                <asp:LinkButton runat="server" ID="LinkCancel" CssClass="iconExit" OnClick="Cancel_Click"
                                    Text="<%$ Resources:cms.language, lblThoat %>" CausesValidation="false">
                                </asp:LinkButton>
                            </div>
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
