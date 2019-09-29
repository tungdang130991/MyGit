<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/ITC.Master" AutoEventWireup="true"
    CodeBehind="List_CTV.aspx.cs" Inherits="ToasoanTTXVN.Hethong.List_CTV" %>

<%@ Register TagPrefix="cc1" Namespace="HPCComponents.UI.WebControls" Assembly="HPCComponents" %>
<%@ Register TagPrefix="anthem" Namespace="Anthem" Assembly="Anthem" %>
<%@ Import Namespace="HPCComponents" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <table border="0" cellpadding="0" width="100%" cellspacing="0">
        <tr>
            <td class="datagrid_top_left">
            </td>
            <td class="datagrid_top_center">
                <span class="TitlePanel"></span>
            </td>
            <td class="datagrid_top_right">
            </td>
        </tr>
        <tr>
            <td class="datagrid_content_left">
            </td>
            <td style="text-align: center">
                <table border="0" cellspacing="1px" cellspacing="1px" style="width: 100%">
                    <tr>
                        <td style="text-align: left; width: 30%">
                            <table border="0" cellspacing="1px" cellspacing="1px" style="text-align: left; width: 100%">
                                <tr>
                                    <td class="Titlelbl" style="text-align: left; width: 20%">
                                        <%=CommonLib.ReadXML("lblTendaydu") %>:
                                    </td>
                                    <td style="width: 80%; text-align: left">
                                        <asp:TextBox ID="txt_userfullname" Width="100%" CssClass="inputtext" runat="server"
                                            onkeypress="return clickButton(event,'ctl00_MainContent_linkSearch');"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td style="text-align: right; width: 70%">
                            <table border="0" cellspacing="1px" cellspacing="1px" style="text-align: right; width: 100%">
                                <tr>
                                    <td class="Titlelbl" style="width: 20%; text-align: right">
                                        <%=CommonLib.ReadXML("lblTendangnhap") %>:
                                    </td>
                                    <td style="width: 40%; text-align: left">
                                        <asp:TextBox ID="txtSearch_UserName" Width="100%" CssClass="inputtext" runat="server"
                                            onkeypress="return clickButton(event,'ctl00_MainContent_linkSearch');"></asp:TextBox>
                                    </td>
                                    <td style="width: 15%; text-align: center">
                                        <asp:Button CausesValidation="false" runat="server" ID="linkSearch" CssClass="iconFind"
                                            Font-Bold="true" OnClick="linkSearch_Click" Text="<%$Resources:cms.language, lblTimkiem%>"></asp:Button>
                                    </td>
                                    <td style="width: 25%; text-align: right">
                                        <asp:LinkButton runat="server" ID="btnAddMenu" CausesValidation="false" CssClass="iconAdd"
                                            OnClick="btnAddMenu_Click" Text="<%$Resources:cms.language, lblThemmoi%>">
                                        </asp:LinkButton>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 20px" colspan="4">
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <asp:DataGrid runat="server" ID="DataGridCTV" AutoGenerateColumns="false" DataKeyField="Ma_Nguoidung"
                                CssClass="Grid" BorderColor="#d4d4d4" CellPadding="0" OnEditCommand="grdListUser_EditCommand"
                                AlternatingItemStyle-BackColor="#F1F1F1" OnItemDataBound="grdListUser_ItemDataBound"
                                BackColor="#ffffff" Width="100%" BorderWidth="1px">
                                <ItemStyle CssClass="GridItem"></ItemStyle>
                                <AlternatingItemStyle CssClass="GridAltItem" />
                                <HeaderStyle CssClass="GridHeader"></HeaderStyle>
                                <HeaderStyle CssClass="GridHeader" Height="30"></HeaderStyle> 
                                <Columns>
                                    <asp:BoundColumn Visible="False" DataField="Ma_Nguoidung">
                                        <HeaderStyle Width="1%"></HeaderStyle>
                                    </asp:BoundColumn>
                                     <asp:BoundColumn HeaderText="#">
                                        <HeaderStyle Width="1%" BackColor="#d4d4d4" />
                                        <ItemStyle BackColor="#d4d4d4" />
                                    </asp:BoundColumn>
                                    <asp:TemplateColumn>
                                        <HeaderStyle HorizontalAlign="Left" Width="50%"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="left" Width="50%" CssClass="GridBorderVerSolid"></ItemStyle>
                                        <HeaderTemplate>
                                            <%=CommonLib.ReadXML("lblTendangnhap") %>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:LinkButton ID="btnUserName" runat="server" CssClass="linkGridForm" Text='<%# DataBinder.Eval(Container.DataItem, "Ten_Dangnhap") %>'
                                                ToolTip="Chỉnh sửa thông tin người dùng" CommandName="Edit" CommandArgument="EditUsers"></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn>
                                        <HeaderStyle HorizontalAlign="Left" Width="35%"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="left" Width="35%" CssClass="GridBorderVerSolid"></ItemStyle>
                                        <HeaderTemplate>
                                            <%=CommonLib.ReadXML("lblTendaydu") %>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:LinkButton ID="btnEdit" runat="server" CssClass="linkGridForm" Text='<%# DataBinder.Eval(Container.DataItem, "TenDaydu") %>'
                                                ToolTip="Chỉnh sửa thông tin người dùng" CommandName="Edit" CommandArgument="EditUsers"></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn>
                                        <HeaderStyle HorizontalAlign="Center" Width="3%"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Center" Width="3%" CssClass="GridBorderVerSolid"></ItemStyle>
                                        <HeaderTemplate>
                                            <%=CommonLib.ReadXML("lblXoa") %>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:ImageButton ID="btnDelete" Width="15px" runat="server" ImageUrl="../Dungchung/images/cancel.gif"
                                                ImageAlign="AbsMiddle" ToolTip="Xóa thông tin người dùng" CommandName="Edit"
                                                CommandArgument="Delete" BorderStyle="None"></asp:ImageButton>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                </Columns>
                            </asp:DataGrid>
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 25px" colspan="4">
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right" colspan="4" class="pageNav">
                            <cc1:CurrentPage runat="server" ID="currentPage">
                            </cc1:CurrentPage>
                            <cc1:Pager runat="server" ID="pages" OnIndexChanged="pages_IndexChanged" />
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
