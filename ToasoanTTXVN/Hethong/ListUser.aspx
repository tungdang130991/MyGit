<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/ITC.Master" AutoEventWireup="true"
    CodeBehind="ListUser.aspx.cs" Inherits="ToasoanTTXVN.Hethong.ListUser" %>

<%@ Register TagPrefix="cc1" Namespace="HPCComponents.UI.WebControls" Assembly="HPCComponents" %>
<%@ Import Namespace="HPCComponents" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Panel ID="pnlList" runat="server">
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
                    <table border="0" cellspacing="2px" cellspacing="2px" style="width: 100%">
                        <tr>
                            <td style="text-align: right; width: 100%" colspan="4">
                                <asp:UpdatePanel ID="upnl_searchusers" runat="server" UpdateMode="Always">
                                    <ContentTemplate>
                                        <table border="0" cellspacing="1px" cellspacing="1px" style="text-align: right; width: 100%">
                                            <tr>
                                                <td style="text-align: right; width: 10%">
                                                    <span class="Titlelbl">
                                                        <%=CommonLib.ReadXML("lblTendaydu") %>:</span>
                                                </td>
                                                <td style="width: 15%; text-align: left">
                                                    <asp:TextBox ID="txt_userfullname" Width="90%" CssClass="inputtext" runat="server"
                                                        onkeypress="return clickButton(event,'ctl00_MainContent_linkSearch');"></asp:TextBox>
                                                </td>
                                                <td class="Titlelbl" style="vertical-align: middle; width: 10%; text-align: right">
                                                    <%=CommonLib.ReadXML("lblTendangnhap")%>:
                                                </td>
                                                <td style="width: 15%; text-align: left">
                                                    <asp:TextBox ID="txtSearch_UserName" Width="100%" CssClass="inputtext" runat="server"
                                                        onkeypress="return clickButton(event,'ctl00_MainContent_linkSearch');"></asp:TextBox>
                                                </td>
                                                <td class="Titlelbl" style="text-align: right">
                                                    <%=CommonLib.ReadXML("lblTrangthaihoatdong") %>:
                                                </td>
                                                <td style="text-align: left">
                                                    <asp:CheckBox ID="checkactive" Checked="true" runat="server" />
                                                </td>
                                                <td class="Titlelbl" style="vertical-align: middle; width: 10%; text-align: right">
                                                    <%=CommonLib.ReadXML("lblPhongban") %>:
                                                </td>
                                                <td style="width: 20%; text-align: left">
                                                    <asp:DropDownList ID="cbo_phongban" runat="server" Width="100%" CssClass="inputtext">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 10%; text-align: center" colspan="8">
                                                    <asp:Button CausesValidation="false" runat="server" ID="linkSearch" CssClass="iconFind"
                                                        Font-Bold="true" OnClick="linkSearch_Click" Text="<%$Resources:cms.language, lblTimkiem%>">
                                                    </asp:Button>
                                                    <asp:Button runat="server" ID="btnAddMenu" CausesValidation="false" CssClass="iconAdd"
                                                        OnClick="btnAddMenu_Click" Text="<%$Resources:cms.language, lblThemmoi%>"></asp:Button>
                                                    <asp:Button runat="server" ID="btn_ConvertPassword" CausesValidation="false" CssClass="iconAdd"
                                                        Visible="false" OnClick="btn_ConvertPassword_Click" Text="Convert Password">
                                                    </asp:Button>
                                                </td>
                                            </tr>
                                        </table>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4" style="height: 5px">
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4">
                                <asp:UpdatePanel ID="upnl_listusers" runat="server">
                                    <Triggers>
                                        <asp:PostBackTrigger ControlID="grdListUser" />
                                    </Triggers>
                                    <ContentTemplate>
                                        <div>
                                            <asp:DataGrid runat="server" ID="grdListUser" AutoGenerateColumns="false" DataKeyField="UserID"
                                                CssClass="Grid" BorderColor="#d4d4d4" CellPadding="0" OnEditCommand="grdListUser_EditCommand"
                                                AlternatingItemStyle-BackColor="#F1F1F1" OnItemDataBound="grdListUser_ItemDataBound"
                                                BackColor="#ffffff" Width="100%" BorderWidth="1px">
                                                <ItemStyle CssClass="GridItem"></ItemStyle>
                                                <AlternatingItemStyle CssClass="GridAltItem" />
                                                <HeaderStyle CssClass="GridHeader" Height="30"></HeaderStyle>
                                                <Columns>
                                                    <asp:BoundColumn Visible="False" DataField="UserID">
                                                        <HeaderStyle Width="1%"></HeaderStyle>
                                                    </asp:BoundColumn>
                                                    <asp:TemplateColumn>
                                                        <HeaderStyle HorizontalAlign="Center" Width="3%"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Center" Width="3%" CssClass="GridBorderVerSolid"></ItemStyle>
                                                        <HeaderTemplate>
                                                            <%=CommonLib.ReadXML("lblCapnhatPhongban") %>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblmaphongban" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container.DataItem, "Maphongban") %>'></asp:Label>
                                                            <asp:Label ID="lblUserId" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container.DataItem, "UserID") %>'></asp:Label>
                                                            <asp:ImageButton ID="btnUpdateRoom" Width="20px" runat="server" ImageUrl='<%#StatusRoom(DataBinder.Eval(Container.DataItem, "Maphongban"))%>'
                                                                ImageAlign="AbsMiddle" ToolTip="Cập nhật phòng ban" CommandName="Edit" CommandArgument="UpdateRoom"
                                                                BorderStyle="None"></asp:ImageButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                    <asp:TemplateColumn>
                                                        <HeaderStyle HorizontalAlign="Center" Width="15%"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="left" Width="15%" CssClass="GridBorderVerSolid"></ItemStyle>
                                                        <HeaderTemplate>
                                                            <%=CommonLib.ReadXML("lblTendaydu") %>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <span class="linkGridForm" style="font-weight: bold">
                                                                <%#DataBinder.Eval(Container.DataItem, "Ten_Phongban")%></span>
                                                            <asp:LinkButton ID="btnEdit" runat="server" CssClass="linkGridForm" Text='<%# DataBinder.Eval(Container.DataItem, "UserFullName") %>'
                                                                Enabled='<%# HPCBusinessLogic.Utils.IsEnable(_Role.R_Write,"0",_user.UserID)%>'
                                                                ToolTip="Chỉnh sửa thông tin người dùng" CommandName="Edit" CommandArgument="EditUsers"></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                    <asp:TemplateColumn>
                                                        <HeaderStyle HorizontalAlign="Center" Width="10%"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="left" Width="10%" CssClass="GridBorderVerSolid"></ItemStyle>
                                                        <HeaderTemplate>
                                                            <%=CommonLib.ReadXML("lblTendangnhap") %>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="btnUserName" runat="server" CssClass="linkGridForm" Text='<%# DataBinder.Eval(Container.DataItem, "UserName") %>'
                                                                ToolTip="Chỉnh sửa thông tin người dùng" CommandName="Edit" CommandArgument="EditUsers"></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                    <asp:TemplateColumn>
                                                        <HeaderStyle HorizontalAlign="Center" Width="4%"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Center" Width="4%" CssClass="GridBorderVerSolid"></ItemStyle>
                                                        <HeaderTemplate>
                                                            <%=CommonLib.ReadXML("lblQuytrinh") %>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="btnqtbt" Width="17px" runat="server" ImageUrl="../Dungchung/images/rollqtbt.png"
                                                                ImageAlign="AbsMiddle" ToolTip="Quy trình biên tập" CommandName="Edit" CommandArgument="QTBT"
                                                                BorderStyle="None"></asp:ImageButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                    <asp:TemplateColumn>
                                                        <HeaderStyle HorizontalAlign="Center" Width="4%"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Center" Width="4%" CssClass="GridBorderVerSolid"></ItemStyle>
                                                        <HeaderTemplate>
                                                            <%=CommonLib.ReadXML("lblNhom") %>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="btnGroup" Width="17px" runat="server" ImageUrl="../Dungchung/images/icon_users_32px.gif"
                                                                ImageAlign="AbsMiddle" ToolTip="Nhóm người dùng" CommandName="Edit" CommandArgument="Group"
                                                                BorderStyle="None"></asp:ImageButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                    <asp:TemplateColumn Visible="false">
                                                        <HeaderStyle HorizontalAlign="Center" Width="4%"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Center" Width="4%" CssClass="GridBorderVerSolid"></ItemStyle>
                                                        <HeaderTemplate>
                                                            <%=CommonLib.ReadXML("lblNgonngu") %>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="btnRoleNgonngu" Width="17px" runat="server" ImageUrl="../Dungchung/images/Ngonngu.png"
                                                                ImageAlign="AbsMiddle" ToolTip="Phân quyền ngôn ngữ" CommandName="Edit" CommandArgument="RoleNgonNgu"
                                                                BorderStyle="None"></asp:ImageButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                    <asp:TemplateColumn>
                                                        <HeaderStyle HorizontalAlign="Center" Width="4%"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Center" Width="4%" CssClass="GridBorderVerSolid"></ItemStyle>
                                                        <HeaderTemplate>
                                                            <%=CommonLib.ReadXML("lblChucnang") %>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="btnRole" Width="17px" runat="server" ImageUrl="../Dungchung/images/Hethong.png"
                                                                ImageAlign="AbsMiddle" ToolTip="Phân quyền sử dụng chức năng" CommandName="Edit"
                                                                CommandArgument="Role" BorderStyle="None"></asp:ImageButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                    <asp:TemplateColumn>
                                                        <HeaderStyle HorizontalAlign="Center" Width="4%"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Center" Width="4%" CssClass="GridBorderVerSolid"></ItemStyle>
                                                        <HeaderTemplate>
                                                            <%=CommonLib.ReadXML("lblChuyenmuc") %>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="btnRoleCategory" Width="17px" runat="server" ImageUrl="../Dungchung/images/QLCM.gif"
                                                                ImageAlign="AbsMiddle" ToolTip="Phân quyền sử dụng chuyên mục" CommandName="Edit"
                                                                CommandArgument="RoleCategory" BorderStyle="None"></asp:ImageButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                    <asp:TemplateColumn>
                                                        <HeaderStyle HorizontalAlign="Center" Width="3%"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Center" Width="3%" CssClass="GridBorderVerSolid"></ItemStyle>
                                                        <HeaderTemplate>
                                                            <%=CommonLib.ReadXML("lblTrangthaihoatdong") %>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="btnIsReporter" runat="server" ImageUrl='<%#IsStatusGet(DataBinder.Eval(Container.DataItem, "UserActive").ToString())%>'
                                                                ImageAlign="AbsMiddle" ToolTip="Trạng thái kích hoạt" CommandName="Edit" CommandArgument="IsReporter"
                                                                BorderStyle="None" />
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                    <asp:TemplateColumn>
                                                        <HeaderStyle HorizontalAlign="Center" Width="3%"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Center" Width="3%" CssClass="GridBorderVerSolid"></ItemStyle>
                                                        <HeaderTemplate>
                                                            Reset password
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="btnResetPass" Width="17px" runat="server" ImageUrl="../Dungchung/images/undo.png"
                                                                ImageAlign="AbsMiddle" ToolTip="Khôi phục về mặc khẩu mặc định" CommandName="Edit"
                                                                CommandArgument="ResetPass" BorderStyle="None"></asp:ImageButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                    <asp:TemplateColumn>
                                                        <HeaderStyle HorizontalAlign="Center" Width="3%"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Center" Width="3%" CssClass="GridBorderVerSolid"></ItemStyle>
                                                        <HeaderTemplate>
                                                            <%=CommonLib.ReadXML("lblXoa") %>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="btnDelete" Width="17px" runat="server" ImageUrl="../Dungchung/images/cancel.gif"
                                                                ImageAlign="AbsMiddle" ToolTip="Xóa thông tin người dùng" CommandName="Edit"
                                                                CommandArgument="Delete" BorderStyle="None"></asp:ImageButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                </Columns>
                                            </asp:DataGrid>
                                        </div>
                                        <div class="pageNav" style="text-align: right; width: 98%; font-size: 14px; font-weight: bold">
                                            <asp:Label ID="lbltotalrecord" runat="server"></asp:Label>
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
    </asp:Panel>
    <!--Phan Quyen QTBT-->
    <asp:Panel runat="server" ID="PanelQTBT" Visible="false" CssClass="TitlePanel" BackColor="white"
        BorderStyle="NotSet">
        <table border="0" cellpadding="0" width="100%" cellspacing="0" style="text-align: center">
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
                    <table cellpadding="0" cellspacing="0" border="0" style="text-align: center; width: 100%">
                        <tr>
                            <td class="TitlePanel" style="text-align: left; width: 100%; padding-bottom: 10px">
                                <asp:Label ID="LabelQTBT" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: center; width: 100%; padding-bottom: 10px">
                                <asp:DataGrid runat="server" ID="DataGridQTBT" OnItemDataBound="gdListMenu_ItemDataBound"
                                    AutoGenerateColumns="false" AlternatingItemStyle-BackColor="#F1F1F2" DataKeyField="ID"
                                    CssClass="Grid" BorderColor="#d4d4d4" CellPadding="0" BackColor="White" Width="100%"
                                    BorderWidth="1px">
                                    <ItemStyle CssClass="GridItem"></ItemStyle>
                                    <AlternatingItemStyle CssClass="GridAltItem" />
                                    <HeaderStyle CssClass="GridHeader" Height="30"></HeaderStyle>
                                    <Columns>
                                        <asp:TemplateColumn>
                                            <HeaderStyle HorizontalAlign="Center" Width="1%"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                            <HeaderTemplate>
                                                <asp:CheckBox ID="chkAll" onclick="javascript:CheckAllandUnCheckAllBTQ(this);" runat="server"
                                                    ToolTip="Select/Deselect All"></asp:CheckBox>
                                            </HeaderTemplate>
                                            <ItemStyle HorizontalAlign="Center" CssClass="GridBorderVerSolid"></ItemStyle>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="optSelect" Enabled='<%#Eval("RoleGroup")%>' runat="server" Checked='<%#Eval("Role")%>' />
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn>
                                            <HeaderStyle Width="90%" HorizontalAlign="Left"></HeaderStyle>
                                            <ItemStyle CssClass="GridBorderVerSolid" HorizontalAlign="Left"></ItemStyle>
                                            <HeaderTemplate>
                                                <%=CommonLib.ReadXML("lblQuytrinh") %>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="LabelNgonNgu" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Ten_Quytrinh")%>'
                                                    CssClass="linkGridForm"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                    </Columns>
                                </asp:DataGrid>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div style="clear: both; text-align: left;">
                                    <asp:LinkButton runat="server" ID="LinkButtonApplyQTBT" Text="<%$Resources:cms.language,lblLuu %>"
                                        CssClass="iconSave" OnClick="LinkButtonApplyQTBT_Click">
                                               
                                    </asp:LinkButton>
                                    <asp:LinkButton ID="LinkButtonCancelQTBT" Text="<%$Resources:cms.language,lblThoat %>"
                                        OnClick="btnCancelRole_Click" CssClass="iconExit" runat="server">
                                               
                                    </asp:LinkButton>
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
    <!--End-->
    <!--Phan Quyen Language-->
    <asp:Panel runat="server" ID="PnlNgonNgu" Visible="false" CssClass="TitlePanel" BackColor="white"
        BorderStyle="NotSet">
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
                            <td class="TitlePanel" style="text-align: left; width: 100%; padding-bottom: 10px">
                                <asp:Label ID="LabelRoleNgonngu" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: center; width: 100%; padding-bottom: 10px">
                                <asp:DataGrid runat="server" ID="DataGridNgonNgu" OnItemDataBound="gdListMenu_ItemDataBound"
                                    AutoGenerateColumns="false" AlternatingItemStyle-BackColor="#F1F1F2" DataKeyField="ID"
                                    CssClass="Grid" BorderColor="#d4d4d4" CellPadding="0" BackColor="White" Width="50%"
                                    BorderWidth="1px">
                                    <ItemStyle CssClass="GridItem"></ItemStyle>
                                    <AlternatingItemStyle CssClass="GridAltItem" />
                                    <HeaderStyle CssClass="GridHeader" Height="30"></HeaderStyle>
                                    <Columns>
                                        <asp:TemplateColumn>
                                            <HeaderStyle HorizontalAlign="Center" Width="3%"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                            <HeaderTemplate>
                                                <asp:CheckBox ID="chkAll" onclick="javascript:CheckAllandUnCheckAllBTQ(this);" runat="server"
                                                    ToolTip="Select/Deselect All"></asp:CheckBox>
                                            </HeaderTemplate>
                                            <ItemStyle HorizontalAlign="Center" CssClass="GridBorderVerSolid"></ItemStyle>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="optSelect" Enabled='<%#Eval("RoleGroup")%>' runat="server" Checked='<%#Eval("Role")%>' />
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn>
                                            <HeaderStyle Width="15%" HorizontalAlign="Center"></HeaderStyle>
                                            <ItemStyle CssClass="GridBorderVerSolid" HorizontalAlign="Left"></ItemStyle>
                                            <HeaderTemplate>
                                                <%=CommonLib.ReadXML("lblNgonngu") %>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="LabelNgonNgu" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "TenNgonNgu")%>'
                                                    CssClass="linkGridForm"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                    </Columns>
                                </asp:DataGrid>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div style="clear: both; text-align: left;">
                                    <asp:LinkButton runat="server" ID="lbtnSaveLang" Text="<%$Resources:cms.language,lblLuu %>"
                                        CssClass="iconSave" OnClick="btnApplyRoleLanguage_Click">
                                               
                                    </asp:LinkButton>
                                    <asp:LinkButton ID="lbtnExit" Text="<%$Resources:cms.language,lblThoat %>" OnClick="btnCancelRole_Click"
                                        CssClass="iconExit" runat="server">
                                               
                                    </asp:LinkButton>
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
    <!--End-->
    <!--Phan Quyen chuc nang-->
    <asp:Panel runat="server" ID="plRole" Visible="false" CssClass="TitlePanel" BackColor="white"
        BorderStyle="NotSet">
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
                    <table cellpadding="2" cellspacing="2" border="0" width="100%">
                        <tr>
                            <td class="TitlePanel" style="height: 25px; text-align: left; width: 40%">
                                <asp:Label ID="roleChucNang" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" colspan="3">
                                <asp:DataGrid runat="server" ID="gdListMenu" OnItemDataBound="gdListMenu_ItemDataBound"
                                    AutoGenerateColumns="false" AlternatingItemStyle-BackColor="#F1F1F2" DataKeyField="Ma_Chucnang"
                                    CssClass="Grid" BorderColor="#d4d4d4" CellPadding="0" BackColor="White" Width="100%"
                                    BorderWidth="1px">
                                    <ItemStyle CssClass="GridItem"></ItemStyle>
                                    <AlternatingItemStyle CssClass="GridAltItem" />
                                    <HeaderStyle CssClass="GridHeader" Height="30"></HeaderStyle>
                                    <Columns>
                                        <asp:TemplateColumn>
                                            <HeaderStyle HorizontalAlign="Center" Width="3%"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                            <HeaderTemplate>
                                                <asp:CheckBox ID="chkAll" onclick="javascript:CheckAllandUnCheckAllBTQ(this);" runat="server"
                                                    ToolTip="Select/Deselect All"></asp:CheckBox>
                                            </HeaderTemplate>
                                            <ItemStyle HorizontalAlign="Center" CssClass="GridBorderVerSolid"></ItemStyle>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="optSelect" Enabled='<%# Eval("Role_Group") %>' runat="server" Checked='<%# Eval("Role_Menu") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn>
                                            <HeaderStyle Width="15%"></HeaderStyle>
                                            <ItemStyle CssClass="GridBorderVerSolid"></ItemStyle>
                                            <HeaderTemplate>
                                                <%=CommonLib.ReadXML("lblTenchuyenmuc") %>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="LabelMenuName" runat="server" Text='<%# SetNameMenu(DataBinder.Eval(Container.DataItem, "Ten_chucnang"), DataBinder.Eval(Container.DataItem, "Ma_Chucnang"))%>'
                                                    CssClass="linkGridForm"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn>
                                            <HeaderStyle Width="15%"></HeaderStyle>
                                            <ItemStyle CssClass="GridBorderVerSolid"></ItemStyle>
                                            <HeaderTemplate>
                                                <%=CommonLib.ReadXML("lblMotadanhmuc") %>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <%# DataBinder.Eval(Container.DataItem, "Mota")%>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn ItemStyle-CssClass="GridBorderVerSolid" HeaderText="Thêm" HeaderStyle-HorizontalAlign="Center">
                                            <HeaderStyle Width="6%" CssClass="GridBorderHeader"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkR_Add" Enabled='<%# Eval("Role_Group") %>' runat="server" Checked='<%# Eval("Doc") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn ItemStyle-CssClass="GridBorderVerSolid" HeaderText="Sửa" HeaderStyle-HorizontalAlign="Center">
                                            <HeaderStyle Width="6%" CssClass="GridBorderHeader"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkR_Edit" Enabled='<%# Eval("Role_Group") %>' runat="server" Checked='<%# Eval("Ghi") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn ItemStyle-CssClass="GridBorderVerSolid" HeaderText="Xóa" HeaderStyle-HorizontalAlign="Center">
                                            <HeaderStyle Width="6%" CssClass="GridBorderHeader"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkR_Del" Enabled='<%# Eval("Role_Group") %>' runat="server" Checked='<%# Eval("Xoa") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                    </Columns>
                                </asp:DataGrid>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3">
                                <div style="clear: both; text-align: center;">
                                    <asp:LinkButton runat="server" ID="LinkButton4" Text="<%$Resources:cms.language,lblLuu %>"
                                        CssClass="iconSave" OnClick="btnApplyRole_Click">
                                               
                                    </asp:LinkButton>
                                    <asp:LinkButton ID="LinkButton5" Text="<%$Resources:cms.language,lblThoat %>" OnClick="btnCancelRole_Click"
                                        CssClass="iconExit" runat="server">
                                               
                                    </asp:LinkButton>
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
    <!--End-->
    <!--Phan Quyen Nhom Nguoi Dung-->
    <asp:Panel runat="server" ID="plGroup" Visible="false" CssClass="TitlePanel">
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
                    <table border="0" cellspacing="5" cellpadding="2" width="100%" align="center">
                        <tr>
                            <td class="TitlePanel" colspan="3" style="height: 25px; text-align: left">
                                <asp:Label ID="lblThuocNhom" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 45%; text-align: right">
                                <table style="width: 100%" cellspacing="2" cellpadding="2">
                                    <tr>
                                        <td style="height: 25px; background-color: #CAC9C0; font-weight: bold; font-size: 12px;
                                            font-family: Arial; text-align: left">
                                            <img src="../Dungchung/Images/15200913357373.png" align="left" />
                                            &nbsp;<%=CommonLib.ReadXML("lblDanhsachnhom") %>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center" style="height: 25px; background-color: #CAC9C0">
                                            <asp:UpdatePanel ID="upnlleftPane" runat="server">
                                                <ContentTemplate>
                                                    <asp:ListBox ID="leftPane" runat="server" CssClass="inputtext" Width="100%" Rows="20"
                                                        DataValueField="Ma_nhom" DataTextField="Ten_nhom" TabIndex="1"></asp:ListBox>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td style="width: 5%; text-align: center; vertical-align: middle">
                                <table style="width: 100%" cellspacing="2" cellpadding="2">
                                    <tr>
                                        <td align="center" valign="middle">
                                            <asp:UpdatePanel ID="UpdatePanelbuttonremove" runat="server">
                                                <ContentTemplate>
                                                    <asp:Button runat="server" ID="Button1" Style="cursor: hand; margin-bottom: 5px"
                                                        CssClass="button" Width="50" TabIndex="3" Text=">>" OnClick="btAddAll_Click">
                                                    </asp:Button>
                                                    <asp:Button runat="server" ID="btAddOne" Style="cursor: hand; margin-bottom: 5px"
                                                        CssClass="button" Width="50" TabIndex="3" Text=">" OnClick="btAddOne_Click">
                                                    </asp:Button>
                                                    <asp:Button runat="server" ID="btRemoveOne" Style="cursor: hand; margin-bottom: 5px"
                                                        CssClass="button" Width="50" TabIndex="4" Text="<" OnClick="btRemoveOne_Click">
                                                    </asp:Button>
                                                    <asp:Button runat="server" ID="Button2" Style="cursor: hand" CssClass="button" Width="50"
                                                        TabIndex="4" Text="<<" OnClick="btRemoveAll_Click"></asp:Button>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td valign="top" align="center">
                                <table style="width: 100%" cellspacing="2" cellpadding="2">
                                    <tr>
                                        <td style="height: 25px; background-color: #CAC9C0; font-weight: bold; font-size: 12px;
                                            font-family: Arial; text-align: left">
                                            <img src="../Dungchung/Images/Group.gif" align="left" />
                                            &nbsp;<%=CommonLib.ReadXML("lblThanhviethuocnhom") %>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center" style="height: 25px; background-color: #CAC9C0">
                                            <asp:UpdatePanel ID="upnlrightPane" runat="server">
                                                <ContentTemplate>
                                                    <asp:ListBox ID="rightPane" runat="server" Width="100%" Rows="20" CssClass="inputtext"
                                                        DataValueField="Ma_nhom" DataTextField="Ten_nhom" TabIndex="6"></asp:ListBox>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                    <br />
                    <table border="0" cellspacing="5" cellpadding="2" width="100%" align="center">
                        <tr>
                            <td align="center">
                                <asp:LinkButton runat="server" ID="linkGroupApplly" OnClick="linkGroupApplly_Click"
                                    CssClass="iconSave" Text="<%$Resources:cms.language,lblLuu %>">                                            
                                                    
                                </asp:LinkButton>
                                <asp:LinkButton runat="server" ID="linkGroupExit" Text="<%$Resources:cms.language,lblThoat %>"
                                    CssClass="iconExit" OnClick="linkGroupExit_Click">                                              
                                                    
                                </asp:LinkButton>
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
    <!--End-->
    <!--Phan Quyen Categorys-->
    <asp:Panel runat="server" ID="pnlRoleCategorys" Visible="false" CssClass="TitlePanel"
        BackColor="white" BorderStyle="NotSet">
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
                    <table cellpadding="2" cellspacing="2" border="0" width="100%">
                        <tr>
                            <td align="right" style="width: 100%">
                                <table cellpadding="2" cellspacing="2" border="0" width="100%">
                                    <tr>
                                        <td class="TitlePanel" style="height: 25px; width: 30%; text-align: left">
                                            <asp:Label ID="roleChuyenMuc" runat="server"></asp:Label>
                                        </td>
                                        <td class="Titlelbl" style="text-align: right">
                                            <%=CommonLib.ReadXML("lblAnpham") %>:
                                        </td>
                                        <td style="text-align: right; width: 20%;">
                                            <asp:DropDownList ID="cbo_anpham" Width="100%" CssClass="inputtext" runat="server"
                                                AutoPostBack="true" OnSelectedIndexChanged="cbo_anpham_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="height: 4px" colspan="3">
                                            <input type="hidden" id="txtCateAccess" runat="server" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td align="left">
                                <asp:DataGrid runat="server" ID="dgListCategorys" OnItemDataBound="gdListMenu_ItemDataBound"
                                    AutoGenerateColumns="false" DataKeyField="Ma_Chuyenmuc" CssClass="Grid" BorderColor="#d4d4d4"
                                    CellPadding="0" AlternatingItemStyle-BackColor="#F1F1F1" BackColor="White" Width="100%"
                                    BorderWidth="1px">
                                    <ItemStyle CssClass="GridItem"></ItemStyle>
                                    <AlternatingItemStyle CssClass="GridAltItem" />
                                    <HeaderStyle CssClass="GridHeader" Height="30"></HeaderStyle>
                                    <Columns>
                                        <asp:TemplateColumn>
                                            <HeaderStyle HorizontalAlign="Center" Width="3%"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                            <HeaderTemplate>
                                                <asp:CheckBox ID="chkAll" onclick="javascript:SelectAllCheckboxes(this);" runat="server"
                                                    ToolTip="Select/Deselect All"></asp:CheckBox>
                                            </HeaderTemplate>
                                            <ItemStyle HorizontalAlign="Center" CssClass="GridBorderVerSolid"></ItemStyle>
                                            <ItemTemplate>
                                                <input type="checkbox" runat="server" id="inputcheckbox" cate-ld='<%# DataBinder.Eval(Container.DataItem, "Ma_Chuyenmuc") %>'
                                                    cate-parrent='<%# DataBinder.Eval(Container.DataItem, "Chuyenmuccha") %>' checked='<%# DataBinder.Eval(Container.DataItem, "RoleCheck") %>'
                                                    onclick='CheckParrent(this);' />
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn>
                                            <HeaderStyle Width="40%"></HeaderStyle>
                                            <ItemStyle CssClass="GridBorderVerSolid"></ItemStyle>
                                            <HeaderTemplate>
                                                <%=CommonLib.ReadXML("lblTenchuyenmuc") %>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="Labelrollcm" CssClass="linkGridForm" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Ten_ChuyenMuc")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                    </Columns>
                                </asp:DataGrid>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div style="clear: both; text-align: center;">
                                    <asp:LinkButton runat="server" ID="linkRoleCateSaves" CssClass="iconSave" Text="<%$Resources:cms.language,lblLuu %>"
                                        OnClick="linkRoleCateSaves_Click">
                                              
                                    </asp:LinkButton>
                                    <asp:LinkButton runat="server" ID="linkRoleCateExit" CssClass="iconExit" Text="<%$Resources:cms.language,   lblThoat %>"
                                        OnClick="linkRoleCateExit_Click">
                                               
                                    </asp:LinkButton>
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
    <!--End-->

    <script language="javascript" type="text/javascript">

        function CheckParrent(_Check) {
            var allitemParrent = $.find('input[cate-parrent="' + $(_Check).attr("cate-ld") + '"]');
            $.each(allitemParrent, function(index, key) {
                var item = $(key);
                if (_Check.checked)
                    item.attr("checked", true);
                else
                    item.attr("checked", false);
            });

        }

    </script>

</asp:Content>
