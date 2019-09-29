<%@ Page Language="C#" MasterPageFile="~/Masters/ITC.Master" AutoEventWireup="true"
    CodeBehind="ListAnPham.aspx.cs" Inherits="ToasoanTTXVN.Danhmuc.ListAnPham" Title="" %>

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
                                    <td style="text-align: right; width: 10%;" class="Titlelbl">
                                        <%=CommonLib.ReadXML("lblAnpham") %>:
                                    </td>
                                    <td style="text-align: left; width: 60%;">
                                        <asp:TextBox ID="txt_TenAnPham" Width="95%" runat="server" CssClass="inputtext" onkeypress="return clickButton(event,'ctl00_MainContent_linkSearch');"></asp:TextBox>
                                    </td>
                                    <td style="text-align: left; width: 20%;">
                                        <asp:Button runat="server" ID="btnSearch" CssClass="iconFind" OnClick="Search_Click"
                                            Text="<%$Resources:cms.language,lblTimkiem %>"></asp:Button>
                                        <asp:Button ID="btn_updateqt" runat="server" Text="<%$Resources:cms.language,lblChonquytrinh %>" CssClass="iconSave"
                                            OnClick="btn_updateqt_Click" />                                       
                                    </td>
                                    <td style="text-align:right">
                                         <asp:Button runat="server" ID="btnAddMenu" CssClass="iconAdd" OnClick="Add_Click"
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
                            <asp:DataGrid runat="server" ID="grdList" AutoGenerateColumns="false" DataKeyField="Ma_AnPham"
                                Width="100%" CssClass="Grid" CellPadding="1" OnEditCommand="grdList_EditCommand"
                                OnItemDataBound="grdList_ItemDataBound">
                                <ItemStyle CssClass="GridItem" Height="25px"></ItemStyle>
                                <AlternatingItemStyle CssClass="GridAltItem" />
                                <HeaderStyle CssClass="GridHeader" Height="30px"></HeaderStyle>
                                <Columns>
                                    <asp:BoundColumn Visible="False" DataField="Ma_AnPham">
                                        <HeaderStyle Width="1%"></HeaderStyle>
                                    </asp:BoundColumn>
                                    <asp:TemplateColumn>
                                        <HeaderStyle HorizontalAlign="Center" Width="3%"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                        <HeaderTemplate>
                                            <asp:CheckBox ID="chkAll" onclick="javascript:SelectAllCheckboxes(this);" runat="server"
                                                ToolTip="Select all"></asp:CheckBox>
                                        </HeaderTemplate>
                                        <ItemStyle HorizontalAlign="Center" CssClass="GridBorderVerSolid"></ItemStyle>
                                        <ItemTemplate>
                                            <asp:CheckBox runat="server" Text='' ID='optSelect' AutoPostBack="false"></asp:CheckBox>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn>
                                        <HeaderStyle HorizontalAlign="Center" Width="3%"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Center" Width="3%" CssClass="GridBorderVerSolid"></ItemStyle>
                                        <HeaderTemplate>
                                            STT
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblSTT" runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn>
                                        <HeaderTemplate>
                                            <%=CommonLib.ReadXML("lblAnpham") %>
                                        </HeaderTemplate>
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle Width="40%" HorizontalAlign="Left" CssClass="GridBorderVerSolid"></ItemStyle>
                                        <ItemTemplate>
                                            <div class="stringtieudeandsc">
                                                <div class="fontTitle" style="width: 90%;">
                                                    <asp:Label ID="lbtitle" runat="server" Text='<%#Eval("Ten_AnPham")%>'></asp:Label>
                                                </div>
                                                <div class="chuthichcss">
                                                    <asp:Label ID="lbdesc" runat="server" Text='<%#Eval("Mota")%>'></asp:Label>
                                                </div>
                                            </div>
                                        </ItemTemplate>
                                        <ItemTemplate>
                                            <asp:LinkButton ID="btnEdit" runat="server" CssClass="linkGridForm" Text='<%# DataBinder.Eval(Container.DataItem, "Ten_AnPham")%>'
                                                Enabled='<%# HPCBusinessLogic.Utils.IsEnable(_Role.R_Write,"0",_user.UserID)%>'
                                                ToolTip="Edit" CommandName="Edit" CommandArgument="Edit"></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn>
                                        <HeaderTemplate>
                                            <%=CommonLib.ReadXML("lblQuytrinh") %>
                                        </HeaderTemplate>
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle Width="30%" HorizontalAlign="Left" CssClass="GridBorderVerSolid"></ItemStyle>
                                        <ItemTemplate>
                                            <asp:HiddenField ID="HiddenField_MaQT" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "Ma_QT")%>' />
                                            <asp:DropDownList ID="cbo_quytrinh" runat="server" Width="100%" CssClass="inputtext">
                                            </asp:DropDownList>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn>
                                        <HeaderTemplate>
                                           <%=CommonLib.ReadXML("lblSotrang") %>
                                        </HeaderTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle Width="5%" HorizontalAlign="Center" CssClass="GridBorderVerSolid"></ItemStyle>
                                        <ItemTemplate>
                                            <%# DataBinder.Eval(Container.DataItem, "Sotrang")%>
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
                                                ImageAlign="AbsMiddle" ToolTip="Xóa thông tin chuyên mục" CommandName="Edit"
                                                CommandArgument="Delete" BorderStyle="None"></asp:ImageButton>
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
