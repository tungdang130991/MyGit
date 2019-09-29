<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/ITC.Master" AutoEventWireup="true"
    CodeBehind="AdsPostList.aspx.cs" Inherits="ToasoanTTXVN.Quangcao.AdsPostList" %>

<%@ Import Namespace="HPCComponents" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <table border="0" cellpadding="0" width="100%" cellspacing="0">
        <tr>
            <td class="datagrid_top_left">
            </td>
            <td class="datagrid_top_center" style="text-align: left">
                <table border="0" cellpadding="1" cellspacing="1" style="float: left;">
                    <tr>
                        <td>
                            <img src="../Dungchung/Images/Icons/to-do-list-cheked-all-icon.png" width="16px" height="16px" />
                        </td>
                        <td style="vertical-align: middle">
                            <span class="TitlePanel">DANH SÁCH VỊ TRÍ QUẢNG CÁO</span>
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
                    <table width="100%" cellspacing="2" cellpadding="2" border="0">
                        <tr>
                            <td colspan="2" style="height: 5px">
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: left;" colspan="2">
                                <asp:Button runat="server" ID="btnAddMenu" CssClass="iconAdd" CausesValidation="false"
                                    Font-Bold="true" OnClick="btnAddMenu_Click" Text="Nhập vị trí" />
                                <asp:Button runat="server" ID="LinkDelete" CausesValidation="false" CssClass="iconDel"
                                    Text="Xóa vị trí" OnClick="btnLinkDelete_Click" />
                            </td>
                        </tr>
                        <tr>
                            <td align="center" colspan="2">
                                <asp:DataGrid runat="server" ID="grdListAdsPos" AutoGenerateColumns="false" DataKeyField="ID"
                                    CssClass="Grid" Width="100%" OnEditCommand="grdListAdsPos_EditCommand" OnItemDataBound="grdListAdsPos_ItemDataBound">
                                    <ItemStyle CssClass="GridItem"></ItemStyle>
                                    <AlternatingItemStyle CssClass="GridAltItem" />
                                    <HeaderStyle CssClass="GridHeader"></HeaderStyle>
                                    <Columns>
                                        <asp:TemplateColumn>
                                            <HeaderStyle HorizontalAlign="Center" Width="3%"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Center" Width="3%"></ItemStyle>
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
                                                Vị trí quảng cáo
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="Linkvitri" CommandName="Edit" CommandArgument="Edit" CssClass="linkEdit"
                                                    runat="server"> <%#Eval("Ads_Name")%></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn>
                                            <HeaderStyle HorizontalAlign="Left" Width="10%"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Left" Width="10%"></ItemStyle>
                                            <HeaderTemplate>
                                                Hình thức hiển thị
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <%#DisplayText(DataBinder.Eval(Container.DataItem, "Ads_DisplayType").ToString())%>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn>
                                            <HeaderStyle HorizontalAlign="Center" Width="5%"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Center" Width="5%"></ItemStyle>
                                            <HeaderTemplate>
                                                Width
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <%#Eval("Ads_Width")%>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn>
                                            <HeaderStyle HorizontalAlign="Center" Width="5%"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Center" Width="5%"></ItemStyle>
                                            <HeaderTemplate>
                                                Height
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <%#Eval("Ads_Height")%>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                    </Columns>
                                </asp:DataGrid>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 30%; text-align: left" colspan="2">
                                <asp:Button runat="server" ID="btnAddMenu2" CssClass="iconAdd" CausesValidation="false"
                                    Font-Bold="true" OnClick="btnAddMenu_Click" Text="Nhập vị trí" />
                                <asp:Button runat="server" ID="LinkDelete1" CausesValidation="false" CssClass="iconDel"
                                    Text="Xóa vị trí" OnClick="btnLinkDelete_Click" />
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
