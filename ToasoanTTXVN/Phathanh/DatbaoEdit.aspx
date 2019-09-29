<%@ Page Language="C#" MasterPageFile="~/Masters/ITC.Master" AutoEventWireup="true"
    CodeBehind="DatbaoEdit.aspx.cs" Inherits="ToasoanTTXVN.Phathanh.DatbaoEdit" Title="" %>

<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral,PublicKeyToken=31BF3856AD364E35" %>
<%@ Import Namespace="HPCComponents" %>
<%@ Register Assembly="BusyBoxDotNet" Namespace="BusyBoxDotNet" TagPrefix="busyboxdotnet" %>
<%@ Register TagPrefix="anthem" Namespace="Anthem" Assembly="Anthem" %>
<%@ Register TagPrefix="nbc" Namespace="HPCComponents.UI" Assembly="HPCComponents" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <script type="text/javascript">
function ValidateText(i)
        {
            if(i.value.length>0)
            {
            i.value = i.value.replace(/[^\d]+/g, '');
            }
        }    
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
                            <span class="TitlePanel">CẬP NHẬT CHI TIẾT ĐẶT BÁO</span>
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
                            <anthem:DropDownList AutoCallBack="true" ID="ddl_TenKH" runat="server" Width="50%"
                                CssClass="inputtext" DataTextField="Ten_KhachHang" DataValueField="Ma_KhachHang"
                                OnSelectedIndexChanged="ddl_TenKH_SelectedIndexChanged">
                            </anthem:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Chưa chọn khách hàng"
                                ControlToValidate="ddl_TenKH" Display="Dynamic" SetFocusOnError="True" Font-Size="Small"
                                InitialValue="0"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right; width: 30%" class="Titlelbl">
                            Số hợp đồng
                        </td>
                        <td style="text-align: left; width: 70%" class="Titlelbl">
                            <anthem:DropDownList ID="ddl_SoHD" AutoCallBack="true" runat="server" Width="50%"
                                CssClass="inputtext" DataTextField="hopdongso" DataValueField="ID">
                            </anthem:DropDownList>
                            <%-- <asp:TextBox ID="txt_SoHD" runat="server" Width="40%" CssClass="inputtext"></asp:TextBox>--%>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Chưa nhập số hợp đồng "
                                ControlToValidate="ddl_SoHD" InitialValue="0" Display="Dynamic" SetFocusOnError="True"
                                Font-Size="Small"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <%-- <tr>
                        <td style="text-align: right; width: 30%" class="Titlelbl">
                            Nội dung yêu cầu
                        </td>
                        <td style="text-align: left; width: 70%" class="Titlelbl">
                            <anthem:TextBox runat="server" ID="txtYeucau" ReadOnly="true" Width="60%" TextMode="MultiLine"
                                Rows="4" AutoCallBack="true"></anthem:TextBox>
                        </td>
                    </tr>--%>
                    <tr>
                        <td style="text-align: right; width: 30%" class="Titlelbl">
                            Ấn phẩm
                        </td>
                        <td style="text-align: left; width: 70%" class="Titlelbl">
                            <anthem:DropDownList AutoCallBack="true" ID="ddl_Anpham" runat="server" Width="50%"
                                CssClass="inputtext" DataTextField="Ten_AnPham" DataValueField="Ma_AnPham">
                            </anthem:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Chưa chọn ấn phẩm"
                                ControlToValidate="ddl_Anpham" Display="Dynamic" SetFocusOnError="True" Font-Size="Small"
                                InitialValue="0"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right; width: 30%" class="Titlelbl">
                            Số lượng
                        </td>
                        <td style="text-align: left; width: 70%" class="Titlelbl">
                            <asp:TextBox ID="txt_Soluong" runat="server" Width="40%" CssClass="inputtext" onkeyup="ValidateText(this);"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right; width: 30%" class="Titlelbl">
                            Số tiền
                        </td>
                        <td style="text-align: left; width: 70%" class="Titlelbl">
                            <asp:TextBox ID="txt_Sotien" runat="server" Width="40%" CssClass="inputtext" onkeyup="ValidateText(this);"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right; width: 30%" class="Titlelbl">
                            Ngày bắt đầu
                        </td>
                        <td style="text-align: left; width: 70%" class="Titlelbl">
                            <nbc:NetDatePicker CssClass="inputtext" ImageUrl="../Dungchung/Images/events.gif"
                                ImageFolder="../Dungchung/scripts/DatePicker/Images" Height="16px" Width="150px"
                                ScriptSource="../Dungchung/scripts/datepicker.js" ID="txt_NgayBatDau" runat="server"
                                onKeyPress="AscciiDisable()" onfocus="javascript:vDateType='3'" onKeyUp="DateFormat(this,this.value,event,false,'3')"
                                onBlur="DateFormat(this,this.value,event,true,'3')" MaxLength="10">
                            </nbc:NetDatePicker>
                            <asp:RegularExpressionValidator ID="Regularexpressionvalidator3" runat="server" ControlToValidate="txt_NgayBatDau"
                                ValidationExpression="^\d{1,2}/\d{1,2}/\d{4}$" Display="Dynamic">Kiểu ngày:dd/MM/yyyy</asp:RegularExpressionValidator>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right; width: 30%" class="Titlelbl">
                            Ngày kết thúc
                        </td>
                        <td style="text-align: left; width: 70%" class="Titlelbl">
                            <nbc:NetDatePicker CssClass="inputtext" ImageUrl="../Dungchung/Images/events.gif"
                                ImageFolder="../Dungchung/scripts/DatePicker/Images" Height="16px" Width="150px"
                                ScriptSource="../Dungchung/scripts/datepicker.js" ID="txt_NgayKetThuc" runat="server"
                                onfocus="javascript:vDateType='3'" onKeyUp="DateFormat(this,this.value,event,false,'3')"
                                onBlur="DateFormat(this,this.value,event,true,'3')" MaxLength="10">
                            </nbc:NetDatePicker>
                            <asp:RegularExpressionValidator ID="Regularexpressionvalidator1" runat="server" ControlToValidate="txt_NgayKetThuc"
                                ValidationExpression="^\d{1,2}/\d{1,2}/\d{4}$" Display="Dynamic">Kiểu ngày:dd/MM/yyyy</asp:RegularExpressionValidator>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right; width: 30%" class="Titlelbl">
                            Ghi chú
                        </td>
                        <td style="text-align: left; width: 70%" class="Titlelbl">
                            <asp:TextBox ID="txt_Ghichu" runat="server" CssClass="inputtext" Width="60%" TextMode="MultiLine"
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
