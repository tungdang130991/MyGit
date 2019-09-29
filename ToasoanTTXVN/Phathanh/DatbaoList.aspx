<%@ Page Language="C#" MasterPageFile="~/Masters/ITC.Master" AutoEventWireup="true"
    CodeBehind="DatbaoList.aspx.cs" Inherits="ToasoanTTXVN.Phathanh.DatbaoList" Title="" %>

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
    </script>

    <table border="0" cellpadding="0" width="100%" cellspacing="0">
        <tr>
            <td class="datagrid_top_left">
            </td>
            <td class="datagrid_top_center">
                <span class="TitlePanel" style="float: left;">DANH SÁCH ĐẶT BÁO</span>
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
                                    <td style="width: 80%; text-align: right" class="Titlelbl">
                                        Tên khách hàng&nbsp;
                                        <asp:TextBox ID="txtTen_Khachhang" Width="350px" runat="server" CssClass="inputtext"
                                            onkeypress="return clickButton(event,'ctl00_MainContent_btnSearch');"></asp:TextBox>&nbsp;
                                             Ấn phẩm&nbsp;
                                              <anthem:DropDownList AutoCallBack="true" ID="ddl_Anpham" runat="server" Width="250px"
                                            CssClass="inputtext" DataTextField="Ten_AnPham" DataValueField="Ma_AnPham" >
                                        </anthem:DropDownList>
                                    </td>
                                    <td style="text-align: right; width: 10%;">
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
                            <asp:DataGrid runat="server" ID="grdList" AutoGenerateColumns="false" DataKeyField="ID"
                                Width="100%" CssClass="Grid" CellPadding="1" OnEditCommand="grdList_EditCommand"
                                OnItemDataBound="grdList_ItemDataBound">
                                <ItemStyle CssClass="GridItem" Height="25px"></ItemStyle>
                                <AlternatingItemStyle CssClass="GridAltItem" />
                                <HeaderStyle CssClass="GridHeader"></HeaderStyle>
                                <Columns>
                                    <asp:BoundColumn Visible="False" DataField="ID">
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
                                                ToolTip="Chỉnh sửa thông tin đặt hàng" CommandName="Edit" CommandArgument="Edit"
                                                Enabled='<%#IsRoleWrite()%>'></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn>
                                        <HeaderStyle HorizontalAlign="Center" Width="15%"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Center" Width="15%" CssClass="GridBorderVerSolid"></ItemStyle>
                                        <HeaderTemplate>
                                            Ấn phẩm
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblTenanpham" runat="server" Text='<%#TenAnpham(DataBinder.Eval(Container.DataItem, "Ma_anpham").ToString())%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn>
                                        <HeaderStyle HorizontalAlign="Center" Width="10%"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Center" Width="10%" CssClass="GridBorderVerSolid"></ItemStyle>
                                        <HeaderTemplate>
                                            Số lượng
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <%# DataBinder.Eval(Container.DataItem, "Soluong")%>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn>
                                        <HeaderStyle HorizontalAlign="Center" Width="10%"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Center" Width="10%" CssClass="GridBorderVerSolid"></ItemStyle>
                                        <HeaderTemplate>
                                            Số tiền
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <%#DataBinder.Eval(Container, "DataItem.Sotien") != System.DBNull.Value ? String.Format("{0:00,0}", Convert.ToDecimal(DataBinder.Eval(Container.DataItem, "Sotien"))) : ""%>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn>
                                        <HeaderStyle HorizontalAlign="Center" Width="12%"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Center" Width="12%" CssClass="GridBorderVerSolid"></ItemStyle>
                                        <HeaderTemplate>
                                            Ngày bắt đầu
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <%# DataBinder.Eval(Container, "DataItem.NGAY_BATDAU") != System.DBNull.Value ? Convert.ToDateTime(DataBinder.Eval(Container, "DataItem.NGAY_BATDAU")).ToString("dd/MM/yyyy") : ""%>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn>
                                        <HeaderStyle HorizontalAlign="Center" Width="12%"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Center" Width="12%" CssClass="GridBorderVerSolid"></ItemStyle>
                                        <HeaderTemplate>
                                            Ngày kết thúc
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <%# DataBinder.Eval(Container, "DataItem.NGAY_KETTHUC") != System.DBNull.Value ? Convert.ToDateTime(DataBinder.Eval(Container, "DataItem.NGAY_KETTHUC")).ToString("dd/MM/yyyy") : ""%>
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
