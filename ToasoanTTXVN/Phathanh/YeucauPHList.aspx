<%@ Page Language="C#" MasterPageFile="~/Masters/ITC.Master" AutoEventWireup="true"
    CodeBehind="YeucauPHList.aspx.cs" Inherits="ToasoanTTXVN.Phathanh.YeucauPHList"
    Title="" %>

<%@ Register TagPrefix="anthem" Namespace="Anthem" Assembly="Anthem" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral,PublicKeyToken=31BF3856AD364E35" %>
<%@ Register TagPrefix="cc1" Namespace="HPCComponents.UI.WebControls" Assembly="HPCComponents" %>
<%@ Import Namespace="HPCComponents" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <script src="../Dungchung/Scripts/jquery-1.4.2.min.js" type="text/javascript"></script>

    <script src="../Dungchung/Scripts/jquery.autocomplete.js" type="text/javascript"></script>

    <link href="../Dungchung/Style/jquery.autocomplete.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript">
        $(document).ready(function() { 
            $("#<%= txtTen_Khachhang.ClientID %>").autocomplete("AutoCompleteSearch.ashx").result(function (event, data, formatted) {
                    if (data) {                       
                        $("#<%= hdnValue.ClientID %>").val(data[1]);                     
                    }
                    else {
                        $("#<%= hdnValue.ClientID %>").val('0');
                    }
                });
                });
    </script>

    <table border="0" cellpadding="0" width="100%" cellspacing="0">
        <tr>
            <td class="datagrid_top_left">
            </td>
            <td class="datagrid_top_center">
                <span class="TitlePanel" style="float: left;">DANH SÁCH YÊU CẦU</span>
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
                                    <td style="text-align: right; width: 10%;" class="Titlelbl">
                                        Tên khách hàng &nbsp;
                                    </td>
                                    <td style="text-align: left; width: 35%;">
                                        <asp:TextBox ID="txtTen_Khachhang" Width="80%" runat="server" CssClass="inputtext"
                                            onkeypress="return clickButton(event,'ctl00_MainContent_btnSearch');"></asp:TextBox>
                                        <input runat="server" id="hdnValue" type="hidden" />
                                    </td>
                                    <td style="width: 10%; text-align: right" class="Titlelbl">
                                        Tên yêu cầu&nbsp;
                                    </td>
                                    <td style="width: 35%; text-align: left;">
                                        <asp:TextBox ID="txt_Noidung" Width="80%" runat="server" CssClass="inputtext" onkeypress="return clickButton(event,'ctl00_MainContent_btnSearch');"></asp:TextBox>
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
                                        <HeaderStyle HorizontalAlign="Center" Width="25%"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Left" Width="25%" CssClass="GridBorderVerSolid"></ItemStyle>
                                        <HeaderTemplate>
                                            Khách hàng
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:LinkButton ID="btnEdit" runat="server" CssClass="linkGridForm" Text='<%# TenKhachHang(DataBinder.Eval(Container.DataItem, "Ma_Khachhang").ToString())%>'
                                                ToolTip="Chỉnh sửa thông tin " CommandName="Edit" CommandArgument="Edit" Enabled='<%#IsRoleWrite()%>'></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn>
                                        <HeaderTemplate>
                                            Tên yêu cầu
                                        </HeaderTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle Width="30%" HorizontalAlign="Left" CssClass="GridBorderVerSolid"></ItemStyle>
                                        <ItemTemplate>
                                            <%# DataBinder.Eval(Container.DataItem, "TenQuangCao")%>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn>
                                        <HeaderStyle HorizontalAlign="Center" Width="15%"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Center" Width="15%" CssClass="GridBorderVerSolid"></ItemStyle>
                                        <HeaderTemplate>
                                            Ngày yêu cầu
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <%# DataBinder.Eval(Container, "DataItem.Ngaytao")!=System.DBNull.Value?Convert.ToDateTime(DataBinder.Eval(Container, "DataItem.Ngaytao")).ToString("dd/MM/yyyy HH:mm"):"" %>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn>
                                        <HeaderStyle HorizontalAlign="Center" Width="10%"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Center" Width="10%" CssClass="GridBorderVerSolid"></ItemStyle>
                                        <HeaderTemplate>
                                            Trạng thái
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <anthem:ImageButton ID="btnStatus" runat="server" ImageUrl='<%#IsStatusGet(DataBinder.Eval(Container.DataItem, "Trangthai").ToString())%>'
                                                ImageAlign="AbsMiddle" CommandName="Edit" CommandArgument="IsStatus" BorderStyle="None"
                                                AutoUpdateAfterCallBack="true" Enabled='<%#IsRoleWrite()%>' ToolTip='<%#GetTooltip(DataBinder.Eval(Container.DataItem, "Trangthai").ToString())%>' />
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
