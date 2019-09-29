<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/ITC.Master" AutoEventWireup="true"
    CodeBehind="ButDanh.aspx.cs" Inherits="ToasoanTTXVN.Hethong.ButDanh" %>

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
                <table border="0" cellspacing="2" cellpadding="2" style="width: 100%">
                    <tr>
                        <td class="Titlelbl" style="width: 10%; text-align: right">
                            <%=CommonLib.ReadXML("lblButdanh") %>:
                        </td>
                        <td style="width: 40%; text-align: left">
                            <asp:TextBox ID="txtSearch_UserName" Width="60%" CssClass="inputtext" runat="server"
                                onkeypress="return clickButton(event,'ctl00_MainContent_linkSearch');"></asp:TextBox>&nbsp;&nbsp;&nbsp;
                            <asp:Button CausesValidation="false" runat="server" ID="linkSearch" CssClass="iconFind"
                                OnClick="linkSearch_Click" Text="<%$Resources:cms.language, lblTimkiem%>"></asp:Button>
                        </td>
                        <td class="Titlelbl" style="text-align: right; width: 10%">
                            <%=CommonLib.ReadXML("lblButdanh") %>:
                        </td>
                        <td style="width: 40%; text-align: left">
                            <asp:TextBox ID="txt_butdanh" runat="server" CssClass="inputtext" Width="60%"></asp:TextBox>&nbsp;&nbsp;&nbsp;
                            <asp:LinkButton runat="server" ID="btnAddMenu" CssClass="iconAdd" OnClick="btnAddMenu_Click"
                                Text="<%$Resources:cms.language, lblThemmoi%>">
                            </asp:LinkButton>
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 20px" colspan="4">
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <asp:DataGrid runat="server" ID="DataGridButDanh" AutoGenerateColumns="false" DataKeyField="Ma_Nguoidung"
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
                                        <HeaderStyle HorizontalAlign="Left" Width="90%"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="left" Width="90%" CssClass="GridBorderVerSolid"></ItemStyle>
                                        <HeaderTemplate>
                                            <%=CommonLib.ReadXML("lblButdanh") %>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:LinkButton ID="btnUserName" runat="server" CssClass="linkGridForm" Text='<%# DataBinder.Eval(Container.DataItem, "Ten_Dangnhap") %>'
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
                                                ImageAlign="AbsMiddle" ToolTip="<%$Resources:cms.language,lblXoa %>" CommandName="Edit"
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
                            <cc1:currentpage runat="server" id="currentPage" >
                            </cc1:currentpage>
                            <cc1:pager runat="server" id="pages" onindexchanged="pages_IndexChanged" />
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
