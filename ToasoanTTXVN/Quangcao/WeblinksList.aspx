<%@ Page Language="C#" MasterPageFile="~/Masters/ITC.Master" AutoEventWireup="true"
    CodeBehind="WeblinksList.aspx.cs" Inherits="ToasoanTTXVN.Quangcao.WeblinksList"%>

<%@ Register TagPrefix="cc1" Namespace="HPCComponents.UI.WebControls" Assembly="HPCComponents" %>
<%@ Import Namespace="HPCComponents" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Panel ID="pnlListCategory" runat="server">
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
                                <span class="TitlePanel"><%= CommonLib.ReadXML("titDslienket")%></span>
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
                        <table border="0" cellpadding="2" cellspacing="2" style="width: 100%">
                            <tr>
                                <td style="width: 10%; text-align: right" class="Titlelbl">
                                    <%= CommonLib.ReadXML("lblAnpham")%>:
                                </td>
                                <td style="width: 15%; text-align: left">
                                    <asp:DropDownList ID="ddlLang" runat="server" Width="150" CssClass="inputtext">
                                    </asp:DropDownList>
                                </td>
                                <td style="width: 10%; text-align: right" class="Titlelbl">
                                    <%= CommonLib.ReadXML("lblType")%>:
                                </td>
                                <td style="width: 15%; text-align: left">
                                    <asp:DropDownList ID="ddlType" runat="server" Width="150" CssClass="inputtext"
                                        TabIndex="1">
                                        <asp:ListItem Text="Web Links" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="Sponsored Links" Value="2"></asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td style="width: 10%; text-align: right" class="Titlelbl">
                                    <%= CommonLib.ReadXML("lblDiachiurl")%>:
                                </td>
                                <td style="width: 30%; text-align: left">
                                    <asp:TextBox ID="txtSearch_Cate" Width="90%" CssClass="inputtext" runat="server"
                                        onkeypress="return clickButton(event,'<%=btnSearch.ClientID%>');"></asp:TextBox>
                                        
                                </td>
                                <td style="width: 10%; text-align: center">
                                    <asp:Button runat="server" ID="btnSearch" CssClass="iconFind" Font-Bold="true" OnClick="linkSearch_Click"
                                        Text="<%$Resources:cms.language, lblTimkiem%>"></asp:Button>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div class="classSearchHeader">
                        <table width="100%" cellspacing="2" cellpadding="2" border="0">
                            <tr>
                                <td style="width: 100%;" align="left" colspan="2">
                                    <asp:Button runat="server" ID="btnAddMenu" CausesValidation="false" CssClass="iconAddNew"
                                        OnClick="btnAddMenu_Click" Text="<%$Resources:cms.language, lblThemmoi%>" />
                                    <asp:Button runat="server" ID="LinkDelete" CausesValidation="false" CssClass="iconDel"
                                        Text="<%$Resources:cms.language, lblXoa%>" OnClick="btnLinkDelete_Click" />
                                </td>
                            </tr>
                            <tr>
                                <td align="left" colspan="2">
                                    <asp:DataGrid runat="server" ID="grdListCate" AutoGenerateColumns="false" DataKeyField="ID"
                                        Width="100%" OnEditCommand="grdListCategory_EditCommand" OnItemDataBound="grdListCategory_ItemDataBound"
                                        CssClass="Grid">
                                        <ItemStyle CssClass="GridItem"></ItemStyle>
                                        <AlternatingItemStyle CssClass="GridAltItem" />
                                        <HeaderStyle CssClass="GridHeader"></HeaderStyle>
                                        <Columns>
                                            <asp:TemplateColumn>
                                                <HeaderStyle HorizontalAlign="Center" Width="5%"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center" Width="5%"></ItemStyle>
                                                <HeaderTemplate>
                                                    <asp:CheckBox ID="chkAll" onclick="javascript:SelectAllCheckboxes(this);" runat="server" ToolTip="Chọn tất cả"></asp:CheckBox>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:CheckBox runat="server" Text='' ID='optSelect' AutoPostBack="False"></asp:CheckBox>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:BoundColumn Visible="False" DataField="ID">
                                                <HeaderStyle Width="1%"></HeaderStyle>
                                            </asp:BoundColumn>
                                            <asp:TemplateColumn HeaderText="<%$Resources:cms.language, lblDiachiurl%>">
                                                <HeaderStyle HorizontalAlign="Left" Width="35%"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Left" Width="35%"></ItemStyle>
                                                <ItemTemplate>
                                                    <%# DataBinder.Eval(Container.DataItem, "URL")%>
                                                </ItemTemplate>
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="btnEdit" runat="server" CssClass="linkEdit" Text='<%# DataBinder.Eval(Container.DataItem, "URL")%>'
                                                        ToolTip="Chỉnh sửa thông tin khách hàng" CommandName="Edit" 
                                                        CommandArgument="Edit"></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="<%$Resources:cms.language, lblTieude%>">
                                                <HeaderStyle HorizontalAlign="Center" Width="25%"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Left" Width="25%"></ItemStyle>
                                                <ItemTemplate>
                                                    <%# DataBinder.Eval(Container.DataItem, "Tittle")%>
                                                </ItemTemplate>
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="btnEdit1" runat="server" CssClass="linkEdit" Text='<%# DataBinder.Eval(Container.DataItem, "Tittle")%>'
                                                        ToolTip="Chỉnh sửa thông tin khách hàng" CommandName="Edit" 
                                                        CommandArgument="Edit"></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="<%$Resources:cms.language, lblType%>">
                                                <HeaderStyle HorizontalAlign="Center" Width="15%"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Left" Width="15%"></ItemStyle>
                                                <ItemTemplate>
                                                    <%#  GetTypeLink(Eval("IsType").ToString())%>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn>
                                                <HeaderStyle HorizontalAlign="Center" Width="15%"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center" Width="15%"></ItemStyle>
                                                <HeaderTemplate>
                                                    Logo
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <div style="padding-right: 10px; padding-left: 10px">
                                                        <img alt="" style="<%#CommonLib.CheckImgView(DataBinder.Eval(Container.DataItem,"Logo"))%>;
                                                            padding-top: 3px; border: 0; cursor: pointer;" src="<%# Global.TinPathBDT+(DataBinder.Eval(Container.DataItem, "Logo")) %>"
                                                            width="80px" height="60px" align="middle" onclick="return openNewImage(this, 'Close')">
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="<%$Resources:cms.language, lblThutu%>">
                                                <HeaderStyle HorizontalAlign="Center" Width="5%"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center" Width="5%"></ItemStyle>
                                                <ItemTemplate>
                                                    <%# DataBinder.Eval(Container.DataItem, "OrderLinks")%>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                        </Columns>
                                    </asp:DataGrid>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 30%;" align="left">
                                    
                                </td>
                                <td style="text-align: right;" class="pageNav">
                                    <cc1:CurrentPage runat="server" ID="curentPages">
                                    </cc1:CurrentPage>
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
    </asp:Panel>
</asp:Content>
