<%@ Page Language="C#" MasterPageFile="~/Masters/ITC.Master" AutoEventWireup="true"
    CodeBehind="ListLayout.aspx.cs" Inherits="ToasoanTTXVN.Danhmuc.ListLayout" Title="" %>

<%@ Register TagPrefix="cc1" Namespace="HPCComponents.UI.WebControls" Assembly="HPCComponents" %>
<%@ Import Namespace="HPCComponents" %>
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
                <span class="TitlePanel" style="float: left;">DANH SÁCH LAYOUT</span>
            </td>
            <td class="datagrid_top_right">
            </td>
        </tr>
        <tr>
            <td class="datagrid_content_left">
            </td>
            <td style="text-align: center">
                <table cellpadding="0" cellspacing="0" border="0" width="100%">
                    <tr>
                        <td style="text-align: left; width: 100%">
                            <table border="0" cellpadding="1" cellspacing="1" style="width: 100%; text-align: left;">
                                <tr>
                                    <td style="width: 10%; text-align: right" class="Titlelbl">
                                    </td>
                                    <td style="width: 30%; text-align: left;">
                                    </td>
                                    <td style="text-align: right; width: 10%;" class="Titlelbl">
                                    </td>
                                    <td style="text-align: left; width: 40%;">
                                    </td>
                                    <td style="text-align: left; width: 10%;">
                                    </td>
                                    <td style="text-align: left; width: 10%;">
                                        <asp:Button runat="server" ID="btnAdd" CssClass="myButton blue" Font-Bold="true"
                                            OnClick="btnAdd_Click" Text="<%$Resources:cms.language, lblThemmoi%>"></asp:Button>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 4px">
                        </td>
                    </tr>
                    <asp:Panel ID="panelContent" runat="server" Visible="true">
                        <tr>
                            <td style="width: 100%">
                                <asp:GridView ID="GVLayout" runat="Server" AutoGenerateColumns="False" BackColor="White"
                                    CssClass="Grid" GridLines="Vertical" Width="100%" OnRowDataBound="GVLayout_OnRowDataBound"
                                    OnRowCommand="GVLayout_OnRowCommand1" OnRowDeleting="GVLayout_RowDeleting" OnRowEditing="GVLayout_RowEditing"
                                    OnRowUpdating="GVLayout_RowUpdating" OnRowCancelingEdit="GVLayout_RowCancelingEdit"
                                    ShowFooter="False" AutoGenerateEditButton="false" DataKeyNames="Ma_Layout" EnableViewState="True">
                                    <RowStyle CssClass="GridItem" Height="25px" />
                                    <AlternatingRowStyle CssClass="GridAltItem" />
                                    <HeaderStyle CssClass="GridHeader"></HeaderStyle>
                                    <Columns>
                                        <asp:BoundField DataField="Ma_Layout" HeaderText="" ReadOnly="True" Visible="false" />
                                        <asp:TemplateField HeaderText="STT">
                                            <HeaderStyle HorizontalAlign="Center" Width="5%" />
                                            <ItemStyle HorizontalAlign="Center" Width="5%" CssClass="GridBorderVerSolid" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblSTT" runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Mô tả">
                                            <HeaderStyle HorizontalAlign="Center" Width="40%" />
                                            <ItemStyle HorizontalAlign="Center" Width="40%" CssClass="GridBorderVerSolid" />
                                            <ItemTemplate>
                                                <%# Eval("Mota") %>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txt_Mota" runat="Server" Text='<%# Eval("Mota") %>' Width="100%"></asp:TextBox>
                                            </EditItemTemplate>
                                            <FooterTemplate>
                                                <asp:TextBox ID="txt_Mota" runat="Server" Width="100%"></asp:TextBox>
                                            </FooterTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Chiều dài">
                                            <HeaderStyle HorizontalAlign="Center" Width="10%" />
                                            <ItemStyle HorizontalAlign="Center" Width="10%" CssClass="GridBorderVerSolid" />
                                            <ItemTemplate>
                                                <%# Eval("Chieudai") %>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txt_dai" runat="Server" Text='<%# Eval("Chieudai") %>' onKeyPress='return check_num(this,10,event)'></asp:TextBox>
                                            </EditItemTemplate>
                                            <FooterTemplate>
                                                <asp:TextBox ID="txt_dai" runat="Server"></asp:TextBox>
                                            </FooterTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Chiều rộng">
                                            <HeaderStyle HorizontalAlign="Center" Width="10%" />
                                            <ItemStyle HorizontalAlign="Center" Width="10%" CssClass="GridBorderVerSolid" />
                                            <ItemTemplate>
                                                <%# Eval("ChieuRong")%>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txt_rong" runat="Server" Text='<%# Eval("ChieuRong") %>' onKeyPress='return check_num(this,10,event)'></asp:TextBox>
                                            </EditItemTemplate>
                                            <FooterTemplate>
                                                <asp:TextBox ID="txt_rong" runat="Server"></asp:TextBox>
                                            </FooterTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Sửa">
                                            <HeaderStyle HorizontalAlign="Center" Width="5%" />
                                            <ItemStyle HorizontalAlign="Center" Width="5%" CssClass="GridBorderVerSolid" />
                                            <ItemTemplate>
                                                <asp:ImageButton ID="btnAdd" Width="15px" runat="server" ImageUrl="~/Dungchung/images/action.gif"
                                                    ImageAlign="AbsMiddle" ToolTip="Sửa thông tin" CommandName="Edit" CommandArgument="Edit"
                                                    BorderStyle="None"></asp:ImageButton>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:ImageButton ID="btnUpdate" Width="15px" runat="server" ImageUrl="~/Dungchung/images/save.gif"
                                                    ImageAlign="AbsMiddle" ToolTip="Cập nhật" CommandName="Update" BorderStyle="None">
                                                </asp:ImageButton>
                                                <asp:ImageButton ID="btnCancel" Width="15px" runat="server" ImageUrl="~/Dungchung/images/undo.gif"
                                                    ImageAlign="AbsMiddle" ToolTip="Hủy bỏ" CommandName="Cancel" BorderStyle="None">
                                                </asp:ImageButton>
                                            </EditItemTemplate>
                                            <FooterTemplate>
                                                <asp:ImageButton ID="btnAddNew" Width="15px" runat="server" ImageUrl="~/Dungchung/Images/Icons/Add.gif"
                                                    ImageAlign="AbsMiddle" ToolTip="Lưu giữ" CommandName="AddNew" BorderStyle="None">
                                                </asp:ImageButton>
                                            </FooterTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Xóa">
                                            <HeaderStyle HorizontalAlign="Center" Width="5%" />
                                            <ItemStyle HorizontalAlign="Center" Width="5%" CssClass="GridBorderVerSolid" />
                                            <ItemTemplate>
                                                <asp:ImageButton ID="btnDelete" Width="15px" runat="server" ImageUrl="~/Dungchung/images/cancel.gif"
                                                    ImageAlign="AbsMiddle" ToolTip="Xóa" CommandName="Delete" BorderStyle="None">
                                                </asp:ImageButton>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:ImageButton ID="btnCancel" Width="15px" runat="server" ImageUrl="~/Dungchung/Images/Icons/Cancel.gif"
                                                    ImageAlign="AbsMiddle" ToolTip="Hủy" CommandName="Cancel" BorderStyle="None">
                                                </asp:ImageButton>
                                            </FooterTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <FooterStyle BackColor="#CCCC99" />
                                   
                                </asp:GridView>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right" class="pageNav">
                                <cc1:CurrentPage runat="server" ID="curentPages"></cc1:CurrentPage>&nbsp;
                                <cc1:Pager runat="server" ID="pages" OnIndexChanged="pages_IndexChanged" />
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: center">
                            </td>
                        </tr>
                    </asp:Panel>
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
