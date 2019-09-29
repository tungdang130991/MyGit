<%@ Page Language="C#" MasterPageFile="~/Masters/ITC.Master" AutoEventWireup="true"
    CodeBehind="EditSobao.aspx.cs" Inherits="ToasoanTTXVN.Danhmuc.EditSobao" Title="" %>

<%@ Import Namespace="HPCComponents" %>
<%@ Register TagPrefix="nbc" Namespace="HPCComponents.UI" Assembly="HPCComponents" %>
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

    <script type="text/javascript">

        $(window).ready(
 function() {

     $('#<%=txt_NgayXB.ClientID%>').datepicker();


 });  
 
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
                            <%=CommonLib.ReadXML("lblSobao") %>(<span class="req_Field">*</span>)
                        </td>
                        <td style="text-align: left; width: 60%">
                            <asp:TextBox runat="server" Width="60%" CssClass="inputtext" ID="txt_Tensobao"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Chưa nhập tên số báo"
                                ControlToValidate="txt_TenSobao" Display="Dynamic" SetFocusOnError="True" Font-Size="Small"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right; width: 30%" class="Titlelbl">
                            <%=CommonLib.ReadXML("lblNgayXB") %>
                        </td>
                        <td style="text-align: left; width: 70%">
                            <asp:TextBox ID="txt_NgayXB" runat="server" Width="150px" CssClass="inputtext" MaxLength="10"
                                onKeyPress="AscciiDisable()" onfocus="javascript:vDateType='3'" onKeyUp="DateFormat(this,this.value,event,false,'3')"
                                onBlur="DateFormat(this,this.value,event,true,'3')"></asp:TextBox>
                            <asp:RegularExpressionValidator ID="Regularexpressionvalidator3" runat="server" ControlToValidate="txt_NgayXB"
                                ValidationExpression="^\d{1,2}/\d{1,2}/\d{4}$" Display="Dynamic">Kiểu ngày:dd/MM/yyyy</asp:RegularExpressionValidator>
                            <asp:CustomValidator ID="CustomValidator1" runat="server" OnServerValidate="ValidateNgayXB"
                                ControlToValidate="txt_NgayXB" ErrorMessage="Ngày xuất bản nhỏ hơn ngày hiện tại"
                                SetFocusOnError="True"></asp:CustomValidator>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right; width: 30%" class="Titlelbl">
                            <%=CommonLib.ReadXML("lblAnpham") %>
                        </td>
                        <td style="text-align: left; width: 70%">
                            <anthem:DropDownList AutoCallBack="true" ID="ddl_AnPham" CssClass="inputtext" runat="server"
                                Width="40%" DataTextField="Ten_AnPham" DataValueField="Ma_AnPham" OnSelectedIndexChanged="ddl_Anpham_SelectedIndexChanged">
                            </anthem:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Chưa chọn ấn phẩm"
                                ControlToValidate="ddl_AnPham" Display="Dynamic" SetFocusOnError="True" Font-Size="Small"
                                InitialValue="0"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right; width: 30%" class="Titlelbl">
                        </td>
                        <td style="text-align: left; width: 70%">
                            <anthem:DropDownList AutoCallBack="true" ID="ddl_AnphamMau" CssClass="inputtext"
                                runat="server" Width="40%" DataTextField="Mota" DataValueField="MA_Mau" Visible="false">
                            </anthem:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right; width: 30%" class="Titlelbl">
                            <%=CommonLib.ReadXML("lblGhichu") %>
                        </td>
                        <td style="text-align: left; width: 60%">
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
                            <asp:LinkButton runat="server" ID="linkSave" CssClass="iconSave" OnClick="Save_Click"
                                Text="<%$ Resources:cms.language, lblLuu %>">
                            </asp:LinkButton>
                            <asp:LinkButton runat="server" ID="LinkCancel" CssClass="iconExit" OnClick="Cancel_Click"
                                Text="<%$ Resources:cms.language, lblThoat %>" CausesValidation="false">
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
