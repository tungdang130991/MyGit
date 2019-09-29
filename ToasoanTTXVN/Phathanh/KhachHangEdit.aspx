<%@ Page Language="C#" MasterPageFile="~/Masters/ITC.Master" AutoEventWireup="true"
    CodeBehind="KhachHangEdit.aspx.cs" Inherits="ToasoanTTXVN.Phathanh.KhachHangEdit"
    Title="" %>

<%@ Import Namespace="HPCComponents" %>
<%@ Register Assembly="BusyBoxDotNet" Namespace="BusyBoxDotNet" TagPrefix="busyboxdotnet" %>
<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <script src="../Dungchung/Scripts/jquery-1.4.2.min.js" type="text/javascript"></script>

    <script type="text/javascript" src="../Dungchung/Scripts/jquery.validate.min.js"></script>

    <script type="text/javascript" language="javascript">
         function check_num(obj, length, e) {
             var key = window.event ? e.keyCode : e.which;
             var len = obj.value.length + 1;
             if (length <= 3) begin = 48; else begin = 45;
             if (key >= begin && key <= 57 && len <= length || (key == 8 || key == 0)) {
             }
             else return false;
         }          
           $(document).ready(function() {
            $("#aspnetForm").validate({
                rules: {<%=txt_Email.UniqueID %>: {                       
                        required: true,
                        email:true
                    }
                }, messages: {                   
                }
            });
        });
    </script>

    <table border="0" cellpadding="0" width="100%" cellspacing="0">
        <tr>
            <td class="datagrid_top_left">
            </td>
            <td class="datagrid_top_center">
                <table border="0" cellpadding="1" cellspacing="1">
                    <tr>
                        <td>
                            <img src="../../Dungchung/Images/Icons/cog-edit-icon.png" width="16px" height="16px" />
                        </td>
                        <td style="vertical-align: middle">
                            <span class="TitlePanel">CẬP NHẬT KHÁCH HÀNG</span>
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
            <td>
                <table border="0" cellpadding="2" cellspacing="1" align="center" width="80%">
                    <tr>
                        <td style="text-align: right; width: 30%" class="Titlelbl">
                            Tên khách hàng(<span class="req_Field">*</span>)
                        </td>
                        <td style="text-align: left; width: 70%" class="Titlelbl">
                            <asp:TextBox ID="txt_TenKH" runat="server" Width="60%" CssClass="inputtext"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Chưa nhập tên khách hàng"
                                ControlToValidate="txt_TenKH" Display="Dynamic" SetFocusOnError="True" Font-Size="Small"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right; width: 30%" class="Titlelbl">
                            Địa chỉ
                        </td>
                        <td style="text-align: left; width: 70%" class="Titlelbl">
                            <asp:TextBox ID="txt_Diachi" runat="server" Width="60%" CssClass="inputtext"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right; width: 30%" class="Titlelbl">
                            Điện thoại
                        </td>
                        <td style="text-align: left; width: 70%" class="Titlelbl">
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
                        <td style="text-align: left; width: 70%" class="Titlelbl">
                            <asp:TextBox ID="txt_Email" runat="server" Width="40%" CssClass="inputtext"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right; width: 30%" class="Titlelbl">
                            Người đại diện
                        </td>
                        <td style="text-align: left; width: 70%" class="Titlelbl">
                            <asp:TextBox ID="txt_Nguoidaidien" runat="server" Width="60%" CssClass="inputtext"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right; width: 30%" class="Titlelbl">
                            Tên đầy đủ
                        </td>
                        <td style="text-align: left; width: 70%" class="Titlelbl">
                            <asp:TextBox ID="txt_Tendaydu" runat="server" Width="60%" CssClass="inputtext"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right; width: 30%" class="Titlelbl">
                            Fax
                        </td>
                        <td style="text-align: left; width: 70%" class="Titlelbl">
                            <asp:TextBox ID="txt_Fax" runat="server" Width="40%" CssClass="inputtext" onKeyPress='return check_num(this,20,event)'></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right; width: 30%" class="Titlelbl">
                            Mã số thuế
                        </td>
                        <td style="text-align: left; width: 70%" class="Titlelbl">
                            <asp:TextBox ID="txt_Masothue" runat="server" Width="40%" CssClass="inputtext" onKeyPress='return check_num(this,20,event)'></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right; width: 30%" class="Titlelbl">
                            Số tài khoản
                        </td>
                        <td style="text-align: left; width: 70%" class="Titlelbl">
                            <asp:TextBox ID="txt_Taikhoan" runat="server" Width="40%" CssClass="inputtext" onKeyPress='return check_num(this,30,event)'></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right; width: 30%" class="Titlelbl">
                            Ghi chú
                        </td>
                        <td style="text-align: left; width: 70%" class="Titlelbl">
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
                            <asp:LinkButton runat="server" ID="linkSave" Font-Bold="true" CssClass="myButton blue"
                                OnClick="Save_Click" Text="<%$ Resources:Strings, BUTTON_SAVES %>" Width="90px">
                            </asp:LinkButton>
                            <asp:LinkButton runat="server" Font-Bold="true" ID="LinkCancel" CssClass="myButton blue"
                                OnClick="Cancel_Click" Text="<%$ Resources:Strings, BUTTON_SIGOUT %>" CausesValidation="false"
                                Width="90px">

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
