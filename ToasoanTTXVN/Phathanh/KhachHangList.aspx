<%@ Page Language="C#" MasterPageFile="~/Masters/ITC.Master" AutoEventWireup="true"
    CodeBehind="KhachHangList.aspx.cs" Inherits="ToasoanTTXVN.Phathanh.KhachHangList"
    Title="" %>

<%@ Register TagPrefix="cc1" Namespace="HPCComponents.UI.WebControls" Assembly="HPCComponents" %>
<%@ Import Namespace="HPCComponents" %>
<%@ Register TagPrefix="anthem" Namespace="Anthem" Assembly="Anthem" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <script src="../Dungchung/Scripts/jquery-1.4.2.min.js" type="text/javascript"></script>

    <script src="../Dungchung/Scripts/jquery.autocomplete.js" type="text/javascript"></script>

    <link href="../Dungchung/Style/jquery.autocomplete.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript">
        $(document).ready(function() { 
            $("#<%= txtTen_Khachhang.ClientID %>").autocomplete("AutoCompleteSearch.ashx")            
                });
         var oldgridcolor;
            function SetMouseOver(element) {
            oldgridcolor = element.style.backgroundColor;
            element.style.backgroundColor = '#FFFACD';
            element.style.cursor = 'pointer';
//            element.style.textDecoration = 'underline';
            }
            function SetMouseOut(element) {
            element.style.backgroundColor = oldgridcolor;
            element.style.textDecoration = 'none';

}
    </script>

    <table border="0" cellpadding="0" width="100%" cellspacing="0">
        <tr>
            <td class="datagrid_top_left">
            </td>
            <td class="datagrid_top_center">
                <span class="TitlePanel" style="float: left;">DANH SÁCH KHÁCH HÀNG</span>
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
                                        Tên khách hàng:
                                    </td>
                                    <td style="text-align: left; width: 40%;">
                                        <asp:TextBox ID="txtTen_Khachhang" Width="80%" runat="server" CssClass="inputtext"
                                            onkeypress="return clickButton(event,'ctl00_MainContent_btnSearch');"></asp:TextBox>
                                    </td>
                                    <td style="text-align: left; width: 10%;">
                                        <asp:Button runat="server" ID="btnSearch" CssClass="myButton blue" Font-Bold="true"
                                            OnClick="Search_Click" Text="Tìm kiếm"></asp:Button>
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
                    <tr>
                        <td style="width: 100%">
                            <asp:DataGrid runat="server" ID="grdList" AutoGenerateColumns="false" DataKeyField="Ma_KhachHang"
                                Width="100%" CssClass="Grid" CellPadding="1" OnEditCommand="grdList_EditCommand"
                                OnItemDataBound="grdList_ItemDataBound">
                                <ItemStyle CssClass="GridItem" Height="25px"></ItemStyle>
                                <AlternatingItemStyle CssClass="GridAltItem" />
                                <HeaderStyle CssClass="GridHeader"></HeaderStyle>
                                <Columns>
                                    <asp:BoundColumn Visible="False" DataField="Ma_KhachHang">
                                        <HeaderStyle Width="1%"></HeaderStyle>
                                    </asp:BoundColumn>
                                    <asp:TemplateColumn>
                                        <HeaderStyle HorizontalAlign="Center" Width="5%"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Center" Width="5%" CssClass="GridBorderVerSolid"></ItemStyle>
                                        <HeaderTemplate>
                                            STT
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblSTT" runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn>
                                        <HeaderTemplate>
                                            Tên khách hàng
                                        </HeaderTemplate>
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle Width="35%" HorizontalAlign="Left" CssClass="GridBorderVerSolid"></ItemStyle>
                                        <ItemTemplate>
                                            <%# DataBinder.Eval(Container.DataItem, "Ten_KhachHang")%>
                                        </ItemTemplate>
                                        <ItemTemplate>
                                            <asp:LinkButton ID="btnEdit" runat="server" CssClass="linkGridForm" Text='<%# DataBinder.Eval(Container.DataItem, "Ten_KhachHang")%>'
                                                ToolTip="Chỉnh sửa thông tin khách hàng" CommandName="Edit" CommandArgument="Edit"
                                                Enabled='<%#IsRoleWrite()%>'></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn>
                                        <HeaderStyle HorizontalAlign="Center" Width="15%"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Center" Width="15%" CssClass="GridBorderVerSolid"></ItemStyle>
                                        <HeaderTemplate>
                                            Điện thoại
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <%# DataBinder.Eval(Container.DataItem, "SoDienThoai")%>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn>
                                        <HeaderStyle HorizontalAlign="Center" Width="30%"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Center" Width="30%" CssClass="GridBorderVerSolid"></ItemStyle>
                                        <HeaderTemplate>
                                            Địa chỉ
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <%# DataBinder.Eval(Container.DataItem, "DiaChi")%>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn>
                                        <HeaderStyle HorizontalAlign="Center" Width="20%"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Center" Width="20%" CssClass="GridBorderVerSolid"></ItemStyle>
                                        <HeaderTemplate>
                                            Email
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <%# DataBinder.Eval(Container.DataItem, "Email")%>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn>
                                        <HeaderStyle HorizontalAlign="Center" Width="5%"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Center" Width="5%" CssClass="GridBorderVerSolid"></ItemStyle>
                                        <HeaderTemplate>
                                            Xóa
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:ImageButton ID="btnDelete" Width="15px" runat="server" ImageUrl="~/Dungchung/Images/cancel.gif"
                                                ImageAlign="AbsMiddle" ToolTip="Xóa thông tin chuyên mục" CommandName="Edit"
                                                CommandArgument="Delete" BorderStyle="None" Enabled='<%#IsRoleDelete()%>'></asp:ImageButton>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                </Columns>
                            </asp:DataGrid>
                        </td>
                    </tr>
                    <%--<tr>
                        <td style="width: 100%">
                            <asp:GridView ID="GVCustomers" runat="server" AutoGenerateColumns="False" Width="100%"
                                DataKeyNames="Ma_KhachHang" AllowSorting="True" OnRowEditing="GVCustomers_OnRowEditing"
                                CellPadding="4" OnSorting="GVCustomers_Sorting" OnRowDataBound="GVCustomers_RowDataBound"
                                OnRowCreated="GVCustomers_RowCreated" AlternatingRowStyle-BackColor="#F1F1F2"
                                BackColor="White" BorderWidth="1px" EnableViewState="True">
                                <RowStyle CssClass="GridItem" Height="25px" />
                                <AlternatingRowStyle CssClass="GridAltItem" />
                                <HeaderStyle ForeColor="#FFFFFF" CssClass="tbDataFlowList"></HeaderStyle>
                                <Columns>
                                    <asp:BoundField DataField="Ma_KhachHang" HeaderText="" ReadOnly="True" Visible="false" />
                                    <asp:TemplateField HeaderText="STT">
                                        <HeaderStyle HorizontalAlign="Center" Width="5%" />
                                        <ItemStyle HorizontalAlign="Center" Width="5%" CssClass="GridBorderVerSolid" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblSTT" runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Tên khách hàng" SortExpression="Ten_KhachHang" ItemStyle-CssClass="GridBorderVerSolid">
                                        <HeaderStyle HorizontalAlign="Left" Width="35%" />
                                        <ItemStyle HorizontalAlign="Left" Width="35%" />
                                        <ItemTemplate>
                                            <asp:LinkButton ID="btnEdit" runat="server" CssClass="linkGridForm" Text='<%# DataBinder.Eval(Container.DataItem, "Ten_KhachHang")%>'
                                                ToolTip="Chỉnh sửa thông tin khách hàng" CommandName="Edit" CommandArgument="Edit"
                                                Enabled='<%#IsRoleWrite()%>'></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Điện thoại" ItemStyle-CssClass="GridBorderVerSolid">
                                        <HeaderStyle HorizontalAlign="Left" Width="15%" />
                                        <ItemStyle HorizontalAlign="Left" Width="15%" />
                                        <ItemTemplate>
                                            <%# DataBinder.Eval(Container.DataItem, "SoDienThoai")%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Địa chỉ" ItemStyle-CssClass="GridBorderVerSolid">
                                        <HeaderStyle HorizontalAlign="Left" Width="30%" />
                                        <ItemStyle HorizontalAlign="Left" Width="30%" />
                                        <ItemTemplate>
                                            <%# DataBinder.Eval(Container.DataItem, "DiaChi")%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Email" ItemStyle-CssClass="GridBorderVerSolid">
                                        <HeaderStyle HorizontalAlign="Left" Width="30%" />
                                        <ItemStyle HorizontalAlign="Left" Width="30%" />
                                        <ItemTemplate>
                                            <%# DataBinder.Eval(Container.DataItem, "Email")%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Xóa">
                                        <HeaderStyle HorizontalAlign="Center" Width="5%" />
                                        <ItemStyle HorizontalAlign="Center" Width="5%" CssClass="GridBorderVerSolid" />
                                        <ItemTemplate>
                                            <asp:ImageButton ID="btnDelete" Width="15px" runat="server" ImageUrl="~/Dungchung/images/cancel.gif"
                                                ImageAlign="AbsMiddle" ToolTip="Xóa" CommandName="Delete" BorderStyle="None">
                                            </asp:ImageButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </td>
                    </tr>--%>
                    <tr>
                        <td style="text-align: right" class="pageNav">
                            <cc1:CurrentPage runat="server" ID="curentPages"></cc1:CurrentPage>&nbsp;
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
