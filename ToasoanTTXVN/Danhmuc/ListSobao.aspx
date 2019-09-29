<%@ Page Language="C#" MasterPageFile="~/Masters/ITC.Master" AutoEventWireup="true"
    CodeBehind="ListSobao.aspx.cs" Inherits="ToasoanTTXVN.Danhmuc.ListSobao" Title="" %>

<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral,PublicKeyToken=31BF3856AD364E35" %>
<%@ Register TagPrefix="anthem" Namespace="Anthem" Assembly="Anthem" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<%@ Register TagPrefix="cc1" Namespace="HPCComponents.UI.WebControls" Assembly="HPCComponents" %>
<%@ Import Namespace="HPCComponents" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <script type="text/javascript">
        function cancel() {
            $get('ctl00_MainContent_btnCancel').click();
        }
        function ValidateText(i) {
            if (i.value.length > 0) {
                i.value = i.value.replace(/[^\d]+/g, '');
            }
        }    
    </script>

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
                                    <td style="text-align: right; width: 10%;" class="Titlelbl">
                                        <%=CommonLib.ReadXML("lblSobao") %>:
                                    </td>
                                    <td style="text-align: right; width: 30%;">
                                        <asp:TextBox ID="txtTen_Sobao" Width="90%" runat="server" CssClass="inputtext" onkeypress="return clickButton(event,'ctl00_MainContent_linkSearch');"></asp:TextBox>
                                    </td>
                                    <td style="text-align: right; width: 10%;" class="Titlelbl">
                                        <%=CommonLib.ReadXML("lblAnpham") %>:
                                    </td>
                                    <td style="text-align: right; width: 30%;">
                                        <asp:DropDownList ID="cbo_loaibao" Width="100%" runat="server" CssClass="inputtext">
                                        </asp:DropDownList>
                                    </td>
                                    <td style="text-align: left; width: 10%;">
                                        <asp:Button runat="server" ID="btnSearch" CssClass="iconFind" Font-Bold="true" OnClick="Search_Click"
                                            Text="<%$Resources:cms.language,lblTimkiem %>"></asp:Button>
                                    </td>
                                    <td style="text-align: right; width: 10%;">
                                        <asp:Button runat="server" ID="btnAdd" CssClass="iconAdd" Font-Bold="true" OnClick="btnAdd_Click"
                                            Text="<%$Resources:cms.language, lblThemmoi%>"></asp:Button>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 4px" colspan="4">
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 100%" colspan="4">
                            <asp:DataGrid runat="server" ID="grdList" AutoGenerateColumns="false" DataKeyField="Ma_Sobao"
                                Width="100%" CssClass="Grid" CellPadding="1" OnEditCommand="grdList_EditCommand"
                                OnItemDataBound="grdList_ItemDataBound">
                                <ItemStyle CssClass="GridItem" Height="25px"></ItemStyle>
                                <AlternatingItemStyle CssClass="GridAltItem" />
                                <HeaderStyle CssClass="GridHeader" Height="30px"></HeaderStyle>
                                <Columns>
                                    <asp:BoundColumn Visible="False" DataField="Ma_Sobao">
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
                                            <%=CommonLib.ReadXML("lblSobao") %>
                                        </HeaderTemplate>
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle Width="30%" HorizontalAlign="Left" CssClass="GridBorderVerSolid"></ItemStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="lblTenSobao" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Ten_Sobao")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemTemplate>
                                            <asp:LinkButton ID="btnEdit" runat="server" CssClass="linkGridForm" Text='<%# DataBinder.Eval(Container.DataItem, "Ten_Sobao")%>'
                                                Enabled='<%# HPCBusinessLogic.Utils.IsEnable(_Role.R_Write,"0",_user.UserID)%>'
                                                ToolTip="Edit" CommandName="Edit" CommandArgument="Edit"></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn>
                                        <HeaderStyle HorizontalAlign="Center" Width="15%"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Center" Width="15%" CssClass="GridBorderVerSolid"></ItemStyle>
                                        <HeaderTemplate>
                                            <%=CommonLib.ReadXML("lblNgayXB")%>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <span class="linkGridForm">
                                                <%#Eval("Ngay_Xuatban") != System.DBNull.Value ? Convert.ToDateTime(Eval("Ngay_Xuatban")).ToString("dd/MM/yyyy") : ""%></span>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn>
                                        <HeaderStyle HorizontalAlign="Left" Width="30%"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Left" Width="30%" CssClass="GridBorderVerSolid"></ItemStyle>
                                        <HeaderTemplate>
                                            <%=CommonLib.ReadXML("lblAnpham")%>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblMaAnPham" Visible="false" CssClass="linkGridForm" runat="server"
                                                Text='<%#DataBinder.Eval(Container.DataItem, "Ma_AnPham")%>'></asp:Label>
                                            <asp:Label ID="lblTenanpham" CssClass="linkGridForm" runat="server" Text='<%#TenAnpham(DataBinder.Eval(Container.DataItem, "Ma_AnPham").ToString())%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn>
                                        <HeaderStyle HorizontalAlign="Center" Width="5%"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Center" Width="5%" CssClass="GridBorderVerSolid"></ItemStyle>
                                        <HeaderTemplate>
                                            <%=CommonLib.ReadXML("lblXoa")%>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:ImageButton ID="btnDelete" Width="15px" runat="server" ImageUrl="~/Dungchung/Images/cancel.gif"
                                                ImageAlign="AbsMiddle" ToolTip="Xóa thông tin chuyên mục" CommandName="Edit"
                                                CommandArgument="Delete" BorderStyle="None"></asp:ImageButton>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                </Columns>
                            </asp:DataGrid>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right" class="pageNav" colspan="4">
                            <cc1:CurrentPage runat="server" ID="curentPages">
                            </cc1:CurrentPage>&nbsp;
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
