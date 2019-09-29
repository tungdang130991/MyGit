<%@ Page Language="C#" MasterPageFile="~/Masters/ITC.Master" AutoEventWireup="true"
    CodeBehind="ChiTiet_PhatHanh_Edit.aspx.cs" Inherits="ToasoanTTXVN.Phathanh.ChiTiet_PhatHanh_Edit"
    Title="" %>

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
                            <span class="TitlePanel">CẬP NHẬT CHI TIẾT PHÁT HÀNH</span>
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
                            Ấn phẩm
                        </td>
                        <td style="text-align: left; width: 70%" class="Titlelbl">
                            <anthem:DropDownList AutoCallBack="true" ID="ddl_AnPham" runat="server" Width="50%"
                                CssClass="inputtext" DataTextField="Ten_AnPham" DataValueField="Ma_AnPham" OnSelectedIndexChanged="ddl_AnPham_SelectedIndexChanged">
                            </anthem:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Chưa chọn ấn phẩm"
                                ControlToValidate="ddl_AnPham" Display="Dynamic" SetFocusOnError="True" Font-Size="Small"
                                InitialValue="0"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right; width: 30%" class="Titlelbl">
                            Số báo
                        </td>
                        <td style="text-align: left; width: 70%" class="Titlelbl">
                            <anthem:DropDownList AutoPostBack="true" ID="ddl_Sobao" runat="server" Width="50%"
                                CssClass="inputtext" DataTextField="Ten_Sobao" DataValueField="Ma_Sobao" OnSelectedIndexChanged="ddl_Sobao_SelectedIndexChanged">
                            </anthem:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Chưa chọn số báo"
                                ControlToValidate="ddl_Sobao" Display="Dynamic" SetFocusOnError="True" Font-Size="Small"
                                InitialValue="0"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right; width: 30%" class="Titlelbl">
                            Ngày phát hành
                        </td>
                        <td style="text-align: left; width: 70%" class="Titlelbl">
                            <asp:TextBox ID="txt_NgayPhatHanh" runat="server" Width="150px" CssClass="inputtext"
                                ReadOnly="true"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right; width: 30%" class="Titlelbl">
                            Số lượng phát hành
                        </td>
                        <td style="text-align: left; width: 70%" class="Titlelbl">
                            <asp:TextBox ID="txt_SoluongPH" runat="server" Width="150px" CssClass="inputtext"
                                onkeyup="ValidateText(this);"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right; width: 30%" class="Titlelbl">
                            Số lượng tồn
                        </td>
                        <td style="text-align: left; width: 70%" class="Titlelbl">
                            <asp:TextBox ID="txt_SoluongTon" runat="server" Width="150px" CssClass="inputtext"
                                onkeyup="ValidateText(this);"></asp:TextBox>&nbsp;
                            <asp:CompareValidator runat="server" ID="cmpNumbers" ControlToValidate="txt_SoluongTon"
                                ControlToCompare="txt_SoluongPH" Operator="LessThan" Type="Integer" ErrorMessage="Số lượng tồn < Số lượng phát hành!" />
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
