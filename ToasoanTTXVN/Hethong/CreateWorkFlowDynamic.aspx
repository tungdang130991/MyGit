<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/ITC.Master" AutoEventWireup="true"
    CodeBehind="CreateWorkFlowDynamic.aspx.cs" Inherits="ToasoanTTXVN.Hethong.CreateWorkFlowDynamic" %>

<%@ Register TagPrefix="anthem" Namespace="Anthem" Assembly="Anthem" %>
<%@ Register TagPrefix="cc1" Namespace="HPCComponents.UI.WebControls" Assembly="HPCComponents" %>
<%@ Import Namespace="HPCComponents" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Panel ID="pnlList" runat="server">
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
                    <table border="0" cellspacing="1" cellpadding="1" style="width: 100%">
                        <tr>
                            <td style="text-align: right">
                                <table border="0" cellspacing="1px" cellspacing="1px" style="text-align: right; width: 100%">
                                    <tr>
                                        <td class="Titlelbl" style="vertical-align: middle; text-align: right">
                                            Tên ấn phẩm:
                                        </td>
                                        <td style="width: 20%">
                                            <asp:TextBox ID="txtTenAnpham" CssClass="inputtext" Width="80%" runat="server" onkeypress="return clickButton(event,'ctl00_MainContent_linkSearch');"></asp:TextBox>
                                        </td>
                                        <td style="width: 10%">
                                            <asp:Button runat="server" ID="linkSearch" CssClass="myButton" Font-Bold="true" OnClick="linkSearch_OnClick"
                                                Text="Tìm kiếm"></asp:Button>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:DataGrid runat="server" ID="grdListAnpham" AlternatingItemStyle-BackColor="#F1F1F2"
                                    AutoGenerateColumns="false" DataKeyField="Ma_AnPham" BorderColor="#d4d4d4" CellPadding="0"
                                    BackColor="White" Width="100%" BorderWidth="1px" OnEditCommand="grdListWorkFlow_EditCommand">
                                    <ItemStyle CssClass="GridBorderVerSolid" Height="28px"></ItemStyle>
                                    <HeaderStyle CssClass="tbDataFlowList"></HeaderStyle>
                                    <Columns>
                                        <asp:BoundColumn Visible="False" DataField="Ma_AnPham">
                                            <HeaderStyle Width="1%"></HeaderStyle>
                                        </asp:BoundColumn>
                                        <asp:TemplateColumn>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <HeaderTemplate>
                                                <asp:Literal ID="Literal12" runat="server" Text="Tên ấn phẩm"></asp:Literal>
                                            </HeaderTemplate>
                                            <ItemStyle Width="10%" HorizontalAlign="Left" CssClass="GridBorderVerSolid"></ItemStyle>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="btnEdit" CssClass="linkGridForm" Text='<%# DataBinder.Eval(Container.DataItem, "Ten_Anpham")%>'
                                                    runat="server" ToolTip="<%$ Resources:Strings, SYSTEM_GridTiitleEdit %>" CommandName="Edit"
                                                    CommandArgument="Role">
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn>
                                            <HeaderStyle HorizontalAlign="Center" Width="5%"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Center" Width="5%" CssClass="GridBorderVerSolid"></ItemStyle>
                                            <HeaderTemplate>
                                                Roll WorkFlow
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:ImageButton ID="btnRole" Width="15px" runat="server" ImageUrl="~/Dungchung/images/roles.gif"
                                                    ImageAlign="AbsMiddle" ToolTip="<%$ Resources:Strings, GRIDTITTLERolePhanQuyen %>"
                                                    CommandName="Edit" CommandArgument="Role" BorderStyle="None"></asp:ImageButton>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                    </Columns>
                                </asp:DataGrid>
                            </td>
                        </tr>
                        <tr>
                            <td class="pageNav" style="text-align: right">
                                <cc1:CurrentPage runat="server" ID="currentPage"></cc1:CurrentPage>
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
    </asp:Panel>
    <asp:Panel ID="plRole" runat="server" Visible="false" CssClass="TitlePanel" BackColor="white"
        BorderStyle="NotSet">
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
                    <table cellpadding="0" cellspacing="0" border="0" width="100%">
                        <tr>
                            <td class="TitlePanel" style="height: 25px; text-align: right">
                                <asp:Label ID="roleChucNang" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td valign="top" style="width: 20%; text-align: left">
                                <anthem:ListBox ID="lst_Quytrinh" Height="296px" Width="250px" runat="server" AutoCallBack="True"
                                    OnSelectedIndexChanged="lst_Quytrinh_SelectedIndexChanged1">
                                </anthem:ListBox>
                            </td>
                            <td valign="top" style="width: 40%; text-align: left">
                                <anthem:DataGrid runat="server" ID="gdListQuytrinh" AutoGenerateColumns="false" DataKeyField="Ma_Doituong"
                                    BorderColor="#d4d4d4" CellPadding="0" AlternatingItemStyle-BackColor="#F1F1F2"
                                    BackColor="White" Width="100%" BorderWidth="1px">
                                    <ItemStyle CssClass="GridBorderVerSolid" Height="30px"></ItemStyle>
                                    <HeaderStyle CssClass="tbDataFlowList"></HeaderStyle>
                                    <Columns>
                                        <asp:TemplateColumn>
                                            <HeaderStyle HorizontalAlign="Center" Width="4%" CssClass="sectionbanner"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Center" CssClass="GridBorderVerSolid"></ItemStyle>
                                            <HeaderTemplate>
                                                <asp:CheckBox ID="chkAll" onclick="javascript:CheckAllandUnCheckAllBTQ(this);" runat="server"
                                                    AutoPostBack="false" ToolTip="Select/Deselect All"></asp:CheckBox>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox type="checkbox" ID="optSelect" runat="server" Checked='<%# DataBinder.Eval(Container.DataItem, "Role") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn>
                                            <HeaderStyle HorizontalAlign="Center" Width="30%"></HeaderStyle>
                                            <ItemStyle CssClass="GridBorderVerSolid" HorizontalAlign="Left"></ItemStyle>
                                            <HeaderTemplate>
                                                <asp:Literal ID="Literal" runat="server" Text="Tên đối tượng"></asp:Literal>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <%# DataBinder.Eval(Container.DataItem, "Ten_Doituong")%>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn>
                                            <ItemStyle CssClass="GridBorderVerSolid"></ItemStyle>
                                            <HeaderStyle Width="30%"></HeaderStyle>
                                            <HeaderTemplate>
                                                <asp:Literal ID="Literal1" runat="server" Text="STT"></asp:Literal>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <%# DataBinder.Eval(Container.DataItem, "STT")%>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                    </Columns>
                                </anthem:DataGrid>
                            </td>
                            <td valign="top" style="width: 40%; text-align: left">
                                <anthem:DataGrid runat="server" ID="DataGridWorkFlow" AutoGenerateColumns="false"
                                    DataKeyField="ID" BorderColor="#d4d4d4" CellPadding="0" AlternatingItemStyle-BackColor="#F1F1F2"
                                    BackColor="White" Width="100%" BorderWidth="1px">
                                    <ItemStyle CssClass="GridBorderVerSolid" Height="30px"></ItemStyle>
                                    <HeaderStyle CssClass="tbDataFlowList"></HeaderStyle>
                                    <Columns>
                                        <asp:TemplateColumn>
                                            <HeaderStyle HorizontalAlign="Center" Width="30%"></HeaderStyle>
                                            <ItemStyle CssClass="GridBorderVerSolid" HorizontalAlign="Left"></ItemStyle>
                                            <HeaderTemplate>
                                                <asp:Literal ID="Literal" runat="server" Text="Từ"></asp:Literal>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <%# DataBinder.Eval(Container.DataItem, "Ma_Doituong_Gui")%>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn>
                                            <HeaderStyle HorizontalAlign="Center" Width="30%"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Left" CssClass="GridBorderVerSolid"></ItemStyle>
                                            <HeaderTemplate>
                                                <asp:Literal ID="Literal1" runat="server" Text="Đến"></asp:Literal>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <%# DataBinder.Eval(Container.DataItem, "Ma_Doituong_Nhan")%>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                    </Columns>
                                </anthem:DataGrid>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4">
                                <br />
                                <div style="clear: both; text-align: center;">
                                    <ul class="processbutton">
                                        <li>
                                            <asp:LinkButton runat="server" ID="linkSave" OnClick="linkSave_Click" Text="<%$ Resources:Strings, BUTTON_SAVES %>"
                                                Font-Bold="true">
                                    
                                            </asp:LinkButton>
                                            <asp:LinkButton runat="server" ID="LinkExit" OnClick="LinkExit_Click" Font-Bold="true"
                                                Text="<%$ Resources:Strings, BUTTON_ACCEPTEXIT %>">
                                    
                                    
                                            </asp:LinkButton>
                                        </li>
                                    </ul>
                                </div>
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
    </asp:Panel>
</asp:Content>
