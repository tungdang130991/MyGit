<%@ Page Language="C#" MasterPageFile="~/Masters/ITC.Master" AutoEventWireup="true"
    CodeBehind="ListChuyenMuc.aspx.cs" Inherits="ToasoanTTXVN.Danhmuc.ListChuyenMuc"
    Title="" %>

<%@ Register TagPrefix="cc1" Namespace="HPCComponents.UI.WebControls" Assembly="HPCComponents" %>
<%@ Import Namespace="HPCComponents" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <table border="0" cellpadding="0" width="100%" cellspacing="0">
        <tr>
            <td class="datagrid_top_left">
            </td>
            <td class="datagrid_top_center">
                <span class="TitlePanel" style="float: left;"></span>
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
                                        <%=CommonLib.ReadXML("lblAnpham") %>:
                                    </td>
                                    <td style="width: 20%; text-align: left;">
                                        <asp:DropDownList ID="cbo_Anpham" Width="100%" runat="server" CssClass="inputtext">
                                        </asp:DropDownList>
                                    </td>
                                    <td style="text-align: right; width: 10%;" class="Titlelbl">
                                        <%=CommonLib.ReadXML("lblChuyenmuc") %>:
                                    </td>
                                    <td style="text-align: left; width: 20%;">
                                        <asp:TextBox ID="txtSearch_ChuyenMuc" Width="80%" runat="server" CssClass="inputtext"
                                            onkeypress="return clickButton(event,'ctl00_MainContent_linkSearch');"></asp:TextBox>
                                    </td>
                                    <td style="text-align: left; width: 12%;">
                                        <asp:CheckBox ID="chk_Hoatdong" Width="80%" CssClass="inputtext" runat="server" Checked="true" Text="<%$Resources:cms.language,lblDangsudung %>" />
                                    </td>
                                    <td style="text-align: left; width: 12%;">
                                        <asp:CheckBox ID="CheckBoxBaoDT" Width="80%" CssClass="inputtext" runat="server" Checked="False" Text="<%$Resources:cms.language,lblBaodientu %>" />
                                    </td>
                                    <td style="text-align: left; width: 12%;">
                                        <asp:CheckBox ID="CheckBoxBaoIn" Width="80%" CssClass="inputtext" runat="server" Checked="False" Text="<%$Resources:cms.language,lblBaoin %>" />
                                    </td>
                                    <td style="text-align: left; width: 10%;">
                                        <asp:UpdatePanel ID="upnlbuttonsearch" runat="server">
                                            <ContentTemplate>
                                                <asp:Button runat="server" ID="btnSearch" CssClass="iconFind" Font-Bold="true" OnClick="Search_Click"
                                                    Text="<%$Resources:cms.language,lblTimkiem %>"></asp:Button>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </td>
                                    <td style="text-align: left; width: 10%;">
                                        <asp:Button runat="server" ID="btnAddMenu" CssClass="iconAdd" Font-Bold="true" OnClick="btnAddMenu_Click"
                                            Text="<%$Resources:cms.language, lblThemmoi%>"></asp:Button>
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
                            <asp:UpdatePanel ID="UpdatePanelList" runat="server">
                                <ContentTemplate>
                                    <div>
                                        <asp:DataGrid runat="server" ID="grdListCate" AutoGenerateColumns="false" DataKeyField="Ma_ChuyenMuc"
                                            Width="100%" CssClass="Grid" CellPadding="1" OnEditCommand="grdList_EditCommand"
                                            OnItemDataBound="grdList_ItemDataBound">
                                            <ItemStyle CssClass="GridItem" Height="25px"></ItemStyle>
                                            <AlternatingItemStyle CssClass="GridAltItem" />
                                            <HeaderStyle CssClass="GridHeader" Height="30px"></HeaderStyle>
                                            <Columns>
                                                <asp:BoundColumn Visible="False" DataField="Ma_ChuyenMuc">
                                                    <HeaderStyle Width="1%"></HeaderStyle>
                                                </asp:BoundColumn>
                                                <asp:TemplateColumn>
                                                    <HeaderStyle HorizontalAlign="Center" Width="3%"></HeaderStyle>
                                                    <ItemStyle HorizontalAlign="Center" Width="3%" CssClass="GridBorderVerSolid"></ItemStyle>
                                                    <HeaderTemplate>
                                                        #
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSTT" runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn>
                                                    <HeaderTemplate>
                                                        <%=CommonLib.ReadXML("lblChuyenmuc") %>
                                                    </HeaderTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                    <ItemStyle Width="50%" HorizontalAlign="Left" CssClass="GridBorderVerSolid"></ItemStyle>
                                                    <ItemTemplate>
                                                        <%# DataBinder.Eval(Container.DataItem, "Ten_ChuyenMuc")%>
                                                    </ItemTemplate>
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="btnEdit" runat="server" CssClass="linkGridForm" Text='<%# DataBinder.Eval(Container.DataItem, "Ten_ChuyenMuc")%>'
                                                            ToolTip="Chỉnh sửa thông tin chuyên mục" CommandName="Edit" CommandArgument="Edit"></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn>
                                                    <HeaderStyle HorizontalAlign="Left" Width="15%"></HeaderStyle>
                                                    <ItemStyle HorizontalAlign="Left" Width="15%" CssClass="GridBorderVerSolid"></ItemStyle>
                                                    <HeaderTemplate>
                                                       <%=CommonLib.ReadXML("lblAnpham") %>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <span class="linkGridForm">
                                                            <%#HPCBusinessLogic.UltilFunc.GetTenAnpham(DataBinder.Eval(Container.DataItem, "Ma_AnPham"))%></span>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn>
                                                    <HeaderTemplate>
                                                        <%=CommonLib.ReadXML("lblSothutu") %>
                                                    </HeaderTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle Width="5%" HorizontalAlign="Center" CssClass="GridBorderVerSolid"></ItemStyle>
                                                    <ItemTemplate>
                                                        <%# DataBinder.Eval(Container.DataItem, "ThuTuHienThi")%>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn>
                                                    <HeaderStyle HorizontalAlign="Center" Width="5%"></HeaderStyle>
                                                    <ItemStyle HorizontalAlign="Center" Width="5%" CssClass="GridBorderVerSolid"></ItemStyle>
                                                    <HeaderTemplate>
                                                        <%=CommonLib.ReadXML("lblBaodientu") %>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="btnActiveBDT" runat="server" ImageUrl='<%#Hoatdong(DataBinder.Eval(Container.DataItem, "HienThi_BDT").ToString())%>'
                                                            CausesValidation="false" ImageAlign="AbsMiddle" ToolTip="Hiển thị BDT" CommandName="Edit"
                                                            CommandArgument="Active_BDT" BorderStyle="None" />
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn>
                                                    <HeaderStyle HorizontalAlign="Center" Width="5%"></HeaderStyle>
                                                    <ItemStyle HorizontalAlign="Center" Width="5%" CssClass="GridBorderVerSolid"></ItemStyle>
                                                    <HeaderTemplate>
                                                        <%=CommonLib.ReadXML("lblDangsudung") %>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="btnHoatdong" runat="server" ImageUrl='<%#Hoatdong(DataBinder.Eval(Container.DataItem, "Hoatdong").ToString())%>'
                                                            CausesValidation="false" ImageAlign="AbsMiddle" ToolTip="Hiển thị" CommandName="Edit"
                                                            CommandArgument="EditDisplay" BorderStyle="None" />
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn>
                                                    <HeaderStyle HorizontalAlign="Center" Width="5%"></HeaderStyle>
                                                    <ItemStyle HorizontalAlign="Center" Width="5%" CssClass="GridBorderVerSolid"></ItemStyle>
                                                    <HeaderTemplate>
                                                        <%=CommonLib.ReadXML("lblXoa") %>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="btnDelete" Width="15px" runat="server" ImageUrl="~/Dungchung/Images/cancel.gif"
                                                            CausesValidation="false" ImageAlign="AbsMiddle" ToolTip="Xóa thông tin chuyên mục"
                                                            CommandName="Edit" CommandArgument="Delete" BorderStyle="None"></asp:ImageButton>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                            </Columns>
                                        </asp:DataGrid>
                                    </div>
                                    <div style="text-align: right" class="pageNav">
                                        <cc1:CurrentPage runat="server" ID="curentPages">
                                        </cc1:CurrentPage>&nbsp;
                                        <cc1:Pager runat="server" ID="pages" OnIndexChanged="pages_IndexChanged" />
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
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
