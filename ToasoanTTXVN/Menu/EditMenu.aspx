<%@ Page Language="C#" MasterPageFile="~/Masters/ITC.Master" AutoEventWireup="true"
    CodeBehind="EditMenu.aspx.cs" Inherits="ToasoanTTXVN.Menu.EditMenu" Title="" %>

<%@ Register TagPrefix="anthem" Namespace="Anthem" Assembly="Anthem" %>
<%@ Import Namespace="HPCComponents" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <table border="0" cellpadding="0" width="100%" cellspacing="0">
        <tr>
            <td class="datagrid_top_left">
            </td>
            <td class="datagrid_top_center">
                <span class="TitlePanel" style="text-align: left; float: left"></span>
            </td>
            <td class="datagrid_top_right">
            </td>
        </tr>
        <tr>
            <td class="datagrid_content_left">
            </td>
            <td style="text-align: center">
                <table width="60%" cellpadding="0" cellspacing="0" align="center">
                    <tr align="center">
                        <td align="center">
                            <table cellspacing="2" cellpadding="2" width="100%" border="0" align="center">
                                <tr>
                                    <td align="right" class="Titlelbl" style="width:20%">
                                        <%=CommonLib.ReadXML("lblTentienganh") %>:
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txt_lang" runat="server" CssClass="inputtext" Width="50%">
                                        </asp:TextBox>
                                        <asp:RequiredFieldValidator runat="server" ID="reqName" ControlToValidate="txt_lang"
                                            InitialValue="" SetFocusOnError="True" Display="Dynamic" ErrorMessage="Bạn chưa nhập menu english!" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="Titlelbl" align="right">
                                        <%=CommonLib.ReadXML("lblTentiengviet") %>(<span class="req_Field">*</span>):
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txt_Tenchucnang" runat="server" CssClass="inputtext" Width="60%"></asp:TextBox><br />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Chưa nhập tên chức năng"
                                            ControlToValidate="txt_Tenchucnang" Display="Dynamic" SetFocusOnError="True"
                                            Font-Size="Small"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" class="Titlelbl">
                                        <%=CommonLib.ReadXML("lblChucnangcha") %>:
                                    </td>
                                    <td align="left">
                                        <asp:DropDownList ID="ddl_Chucnangcha" runat="server" CssClass="inputtext" Width="50%">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" class="Titlelbl">
                                        <%=CommonLib.ReadXML("lblLienket") %>:
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txt_URL" runat="server" CssClass="inputtext" Width="60%"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" class="Titlelbl">
                                        <%=CommonLib.ReadXML("lblMota") %>:
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txt_Mota" runat="server" CssClass="inputtext" Width="60%" TextMode="MultiLine"
                                            Rows="4"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" class="Titlelbl">
                                        <%=CommonLib.ReadXML("lblQuytrinh") %>:
                                    </td>
                                    <td align="left">
                                        <anthem:CheckBox ID="chk_Quytrinh" runat="server" AutoCallBack="true" OnCheckedChanged="chk_Quytrinh_OnCheckedChanged" />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" class="Titlelbl">
                                        <%=CommonLib.ReadXML("lblMadoituong") %>:
                                    </td>
                                    <td align="left">
                                        <anthem:DropDownList ID="ddl_Madoituong" AutoCallBack="true" runat="server" CssClass="inputtext"
                                            Width="50%">
                                        </anthem:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" class="Titlelbl">
                                        <%=CommonLib.ReadXML("lblSothutu") %>:
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txt_STT" runat="server" CssClass="inputtext" Width="10%" onKeyPress='return check_num(this,5,event)'></asp:TextBox>
                                        <br />
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txt_STT"
                                            Display="Dynamic" ErrorMessage="Thứ tự phải là số" Font-Size="Small" SetFocusOnError="True"
                                            ValidationExpression="^\d+$"></asp:RegularExpressionValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" class="Titlelbl">
                                        <%=CommonLib.ReadXML("lblHienthi") %>:
                                    </td>
                                    <td align="left">
                                        <asp:CheckBox ID="chk_Hoatdong" runat="server" />
                                    </td>
                                </tr>
                            </table>
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
                    <tr>
                        <td>
                            <asp:LinkButton runat="server" ID="LinkButton_Save" OnClick="Save_Click" Text="<%$ Resources:cms.language, lblLuu %>"
                                CausesValidation="true" CssClass="iconSave" Width="90px">
                            </asp:LinkButton>
                            <asp:LinkButton runat="server" CausesValidation="false" ID="Lkb_Cancel" OnClick="Cancel_Click"
                                Text="<%$ Resources:cms.language, lblThoat %>" CssClass="iconExit" Width="90px">
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
