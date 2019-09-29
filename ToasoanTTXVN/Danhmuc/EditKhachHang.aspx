<%@ Page Language="C#" MasterPageFile="~/Masters/ITC.Master" AutoEventWireup="true"
    CodeBehind="EditKhachHang.aspx.cs" Inherits="ToasoanTTXVN.Danhmuc.EditKhachHang"
    Title="" %>

<%@ Import Namespace="HPCComponents" %>
<%@ Register Assembly="BusyBoxDotNet" Namespace="BusyBoxDotNet" TagPrefix="busyboxdotnet" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<%@ Register TagPrefix="anthem" Namespace="Anthem" Assembly="Anthem" %>
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
    </script>

    <table border="0" cellpadding="0" width="100%" cellspacing="0">
        <tr>
            <td class="datagrid_top_left">
            </td>
            <td class="datagrid_top_center">
                
            </td>
            <td class="datagrid_top_right">
            </td>
        </tr>
        <tr>
            <td class="datagrid_content_left">
            </td>
            <td>
                <table border="0" cellpadding="2" cellspacing="2" align="center" width="80%">
                    <tr>
                        <td style="text-align: right; width: 30%" class="Titlelbl">
                            <%=CommonLib.ReadXML("lblTenkhachhang")%>(<span class="req_Field">*</span>)
                        </td>
                        <td style="text-align: left; width: 70%">
                            <asp:TextBox ID="txt_TenKH" runat="server" Width="60%" CssClass="inputtext"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Chưa nhập tên khách hàng"
                                ControlToValidate="txt_TenKH" Display="Dynamic" SetFocusOnError="True" Font-Size="Small"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right; width: 30%" class="Titlelbl">
                            <%=CommonLib.ReadXML("lblDiachi")%>
                        </td>
                        <td style="text-align: left; width: 70%">
                            <asp:TextBox ID="txt_Diachi" runat="server" Width="60%" CssClass="inputtext"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right; width: 30%" class="Titlelbl">
                           <%=CommonLib.ReadXML("lblDienthoai")%>
                        </td>
                        <td style="text-align: left; width: 70%">
                            <asp:TextBox ID="txt_Dienthoai" runat="server" Width="40%" CssClass="inputtext" onKeyPress='return check_num(this,20,event)'></asp:TextBox>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txt_Dienthoai"
                                Display="Dynamic" ErrorMessage="Nhập chữ số" Font-Size="Small" SetFocusOnError="True"
                                ValidationExpression="^\d+$"></asp:RegularExpressionValidator>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right; width: 30%" class="Titlelbl">
                            Email
                        </td>
                        <td style="text-align: left; width: 70%">
                            <asp:TextBox ID="txt_Email" runat="server" Width="40%" CssClass="inputtext"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right; width: 30%" class="Titlelbl">
                            <%=CommonLib.ReadXML("lblNguoidaidien")%>
                        </td>
                        <td style="text-align: left; width: 70%">
                            <asp:TextBox ID="txt_Nguoidaidien" runat="server" Width="60%" CssClass="inputtext"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right; width: 30%" class="Titlelbl">
                             <%=CommonLib.ReadXML("lblTendaydu")%>
                        </td>
                        <td style="text-align: left; width: 70%">
                            <asp:TextBox ID="txt_Tendaydu" runat="server" Width="60%" CssClass="inputtext"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right; width: 30%" class="Titlelbl">
                            Fax
                        </td>
                        <td style="text-align: left; width: 70%">
                            <asp:TextBox ID="txt_Fax" runat="server" Width="40%" CssClass="inputtext" onKeyPress='return check_num(this,20,event)'></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right; width: 30%" class="Titlelbl">
                            <%=CommonLib.ReadXML("lblMasothue")%>
                        </td>
                        <td style="text-align: left; width: 70%">
                            <asp:TextBox ID="txt_Masothue" runat="server" Width="40%" CssClass="inputtext" onKeyPress='return check_num(this,20,event)'></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right; width: 30%" class="Titlelbl">
                            <%=CommonLib.ReadXML("lblSotaikhoan")%>
                        </td>
                        <td style="text-align: left; width: 70%">
                            <asp:TextBox ID="txt_Taikhoan" runat="server" Width="40%" CssClass="inputtext" onKeyPress='return check_num(this,30,event)'></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right; width: 30%" class="Titlelbl">
                            <%=CommonLib.ReadXML("lblGhichu")%>
                        </td>
                        <td style="text-align: left; width: 70%">
                            <asp:TextBox ID="txt_Mota" runat="server" CssClass="inputtext" Width="60%" TextMode="MultiLine"
                                Rows="4"></asp:TextBox>
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
                            <asp:LinkButton runat="server" ID="linkSave" Font-Bold="true" CssClass="iconSave"
                                OnClick="Save_Click" Text="<%$ Resources:cms.language, lblLuu %>" Width="90px">
                            </asp:LinkButton>
                            <asp:LinkButton runat="server" Font-Bold="true" ID="LinkCancel" CssClass="iconExit"
                                OnClick="Cancel_Click" Text="<%$ Resources:cms.language, lblThoat %>" CausesValidation="false"
                                Width="90px">

                            </asp:LinkButton>
                        </td>
                    </tr>
                </table>
            </td>
            <td class="datagrid_content_right">
            </td>
        </tr>
    </table>
</asp:Content>
