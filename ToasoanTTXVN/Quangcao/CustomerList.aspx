<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/ITC.Master" AutoEventWireup="true"
    CodeBehind="CustomerList.aspx.cs" Inherits="ToasoanTTXVN.Quangcao.CustomerList" %>

<%@ Register TagPrefix="cc1" Namespace="HPCComponents.UI.WebControls" Assembly="HPCComponents" %>
<%@ Import Namespace="HPCComponents" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <table border="0" cellpadding="0" width="100%" cellspacing="0">
        <tr>
            <td class="datagrid_top_left">
            </td>
            <td class="datagrid_top_center">
                <table border="0" cellpadding="1" cellspacing="1" width="100%">
                    <tr>
                        <td style="text-align: left; width: 2%">
                            <img src="../Dungchung/Images/Icons/cog-edit-icon.png" width="16px" height="16px" alt="Customers" />
                        </td>
                        <td style="vertical-align: middle; text-align: left">
                            <span class="TitlePanel" style="float: left;">DANH SÁCH KHÁCH HÀNG QUẢNG CÁO</span>
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
            <td style="text-align: center">
                <div class="classSearchHeader" style="margin-bottom: 5px;">
                    <table border="0" cellpadding="2" cellspacing="2" style="width: 100%; text-align: right">
                        <tr>
                            <td style="text-align: right; width: 10%" class="Titlelbl">
                                Tên khách hàng:
                            </td>
                            <td style="text-align: left; width: 60%">
                                <asp:TextBox ID="txtSearch_Cate" Width="95%" CssClass="inputtext" runat="server"
                                    onkeypress="return clickButton(event,'ctl00_MainContent_btnSearch');"></asp:TextBox>
                            </td>
                            <td style="text-align: left">
                                <asp:Button runat="server" ID="btnSearch" CssClass="iconFind" Font-Bold="true" OnClick="linkSearch_Click"
                                    Text="Tìm kiếm"></asp:Button>
                            </td>
                        </tr>
                    </table>
                </div>
                <div class="classSearchHeader">
                    <table cellspacing="2" cellpadding="2" style="width: 100%; border: 0px;">
                        <tr>
                            <td style="text-align: left" colspan="2">
                                <asp:Button runat="server" ID="btnAddMenu" CausesValidation="false" Font-Bold="true"
                                    CssClass="iconAddNew" OnClick="btnAddMenu_Click" Text="Nhập khách hàng" />
                                <asp:Button runat="server" ID="LinkDelete" CausesValidation="false" CssClass="iconDel"
                                    Text="Xóa khách hàng" OnClick="btnLinkDelete_Click"></asp:Button>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" colspan="2">
                                <asp:DataGrid runat="server" ID="grdListCate" Width="100%" AutoGenerateColumns="false"
                                    DataKeyField="ID" OnEditCommand="grdListCategory_EditCommand" OnItemDataBound="grdListCategory_ItemDataBound"
                                    CssClass="Grid">
                                    <ItemStyle CssClass="GridItem"></ItemStyle>
                                    <AlternatingItemStyle CssClass="GridAltItem" />
                                    <HeaderStyle CssClass="GridHeader"></HeaderStyle>
                                    <Columns>
                                        <asp:TemplateColumn>
                                            <HeaderStyle HorizontalAlign="Center" Width="4%"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                            <HeaderTemplate>
                                                <asp:CheckBox ID="chkAll" onclick="javascript:SelectAllCheckboxes(this);" runat="server"
                                                    ToolTip="Chọn tất cả"></asp:CheckBox>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox runat="server" Text='' ID='optSelect' AutoPostBack="False"></asp:CheckBox>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:BoundColumn Visible="False" DataField="ID">
                                            <HeaderStyle Width="1%"></HeaderStyle>
                                        </asp:BoundColumn>
                                        <asp:TemplateColumn>
                                            <HeaderStyle HorizontalAlign="Left" Width="30%"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Left" Width="30%"></ItemStyle>
                                            <HeaderTemplate>
                                                Tên khách hàng
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="btnEdit" runat="server" CssClass="linkEdit" Text='<%# DataBinder.Eval(Container.DataItem, "Name")%>'
                                                    ToolTip="Sửa đổi" CommandName="Edit" CommandArgument="Edit"></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn>
                                            <HeaderStyle HorizontalAlign="Center" Width="15%"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Center" Width="15%"></ItemStyle>
                                            <HeaderTemplate>
                                                Địa chỉ
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <%# DataBinder.Eval(Container.DataItem, "Address")%>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn>
                                            <HeaderStyle HorizontalAlign="Center" Width="10%"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Center" Width="10%"></ItemStyle>
                                            <HeaderTemplate>
                                                Điện thoại
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <%# DataBinder.Eval(Container.DataItem, "Phone")%>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn>
                                            <HeaderStyle HorizontalAlign="Left" Width="10%"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="left" Width="10%"></ItemStyle>
                                            <HeaderTemplate>
                                                Email
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <%# DataBinder.Eval(Container.DataItem, "Email")%>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn>
                                            <HeaderStyle HorizontalAlign="Center" Width="10%"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Center" Width="10%"></ItemStyle>
                                            <HeaderTemplate>
                                                Người nhập
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <%# HPCBusinessLogic.UltilFunc.GetUserName(Eval("User_Created")) %>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn>
                                            <HeaderStyle HorizontalAlign="Center" Width="10%"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Center" Width="10%"></ItemStyle>
                                            <HeaderTemplate>
                                                Ngày nhập
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <%#Eval("Date_Created") != System.DBNull.Value ? Convert.ToDateTime(Eval("Date_Created")).ToString("dd/MM hh:mm:ss") : ""%>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                    </Columns>
                                </asp:DataGrid>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: left; width: 50%">
                                <asp:Button runat="server" ID="btnAddMenu2" CausesValidation="false" Font-Bold="true"
                                    CssClass="iconAddNew" OnClick="btnAddMenu_Click" Text="Nhập khách hàng"></asp:Button>
                                <asp:Button runat="server" ID="LinkDelete1" CausesValidation="false" CssClass="iconDel"
                                    Text="Xóa khách hàng" OnClick="btnLinkDelete_Click"></asp:Button>
                            </td>
                            <td style="text-align: right;" class="pageNav">
                                <cc1:CurrentPage runat="server" ID="curentPages" CssClass="pageNavTotal">
                                </cc1:CurrentPage>&nbsp;
                                <cc1:Pager runat="server" ID="pages" OnIndexChanged="pages_IndexChanged">
                                </cc1:Pager>
                            </td>
                        </tr>
                    </table>
                </div>
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
