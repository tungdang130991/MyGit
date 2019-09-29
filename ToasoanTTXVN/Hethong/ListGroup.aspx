<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/ITC.Master" AutoEventWireup="true"
    CodeBehind="ListGroup.aspx.cs" Inherits="ToasoanTTXVN.Hethong.ListGroup" %>

<% @ Import Namespace = "System.Data"%>
<%@ Register TagPrefix="cc1" Namespace="HPCComponents.UI.WebControls" Assembly="HPCComponents" %>
<%@ Import Namespace="WDF.Component" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <script language="javascript" type="text/javascript">
        $(document).ready(function() {
            var allitem = $.find('div[div-child="child"]');
            $.each(allitem, function(index, key) {
                var item = $(key).parent().parent();
                if ($(key).attr("userid") != "0") {
                    item.css('display', 'none');
                }
            });
            var img_expandcollapse = $.find('img[img-parrent="imgflus"]');
            $.each(img_expandcollapse, function(index, key) {
                if ($(key).attr("count-child") == "0") {
                    $(key).attr("src", "../Dungchung/Images/bullet_toggle_minus.png");
                }
            });

        });
        function divexpandcollapse(divname, imgdiv) {
            var childdiv = $.find('div[div-parrent="collapsedivparrent"]');
            $.each(childdiv, function(index, key) {
                if ($(key).attr("userid-parrent") != "0") {
                    $(key).css('display', 'none');
                }
            });

            var allitem = $.find('div[data-in="div' + divname + '"]');
            $.each(allitem, function(index, key) {
                var item = $(key).parent().parent();

                if ($(key).attr("userid") != "0") {
                    if (item.css('display') == "none") {
                        item.css('display', '');
                        $(imgdiv).attr("src", "../Dungchung/Images/bullet_toggle_minus.png");

                    } else {
                        $(imgdiv).attr("src", "../Dungchung/Images/bullet_toggle_plus.png");
                        item.css('display', 'none');
                    }

                }

            });

        }

    </script>

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
                    <table border="0" cellspacing="1" cellpadding="1" style="width: 100%">
                        <tr>
                            <td style="text-align: right">
                                <table border="0" cellspacing="1px" cellspacing="1px" style="text-align: right; width: 100%">
                                    <tr>
                                        <td class="Titlelbl" style="width: 10%; vertical-align: middle; text-align: right">
                                            <%=HPCComponents.CommonLib.ReadXML("lblNhom") %>
                                        </td>
                                        <td style="width: 30%; text-align: left">
                                            <asp:TextBox ID="txtTenNhom" CssClass="inputtext" Width="95%" runat="server" onkeypress="return clickButton(event,'ctl00_MainContent_linkSearch');"></asp:TextBox>
                                        </td>
                                        <td style="width: 10%; text-align: left">
                                            <asp:Button runat="server" ID="linkSearch" CssClass="iconFind" OnClick="linkSearch_OnClick"
                                                Text="<%$Resources:cms.language, lblTimkiem%>"></asp:Button>
                                        </td>
                                        <td style="width: 60%; text-align: left">
                                            <asp:LinkButton runat="server" ID="linkAddNews" CssClass="iconAdd" CausesValidation="false"
                                                OnClick="linkAddNews_Click" Text="<%$Resources:cms.language, lblThemmoi%>" Font-Bold="true">
                                            </asp:LinkButton>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td style="height: 5px">
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:DataGrid runat="server" ID="grdListNhom" AlternatingItemStyle-BackColor="#F1F1F2"
                                    CssClass="Grid" AutoGenerateColumns="false" DataKeyField="Ma_Nhom" BorderColor="#d4d4d4"
                                    CellPadding="0" BackColor="White" Width="100%" BorderWidth="1px" OnEditCommand="grdListNhom_EditCommand"
                                    OnItemDataBound="grdListNhom_ItemDataBound">
                                    <ItemStyle CssClass="GridItem"></ItemStyle>
                                    <AlternatingItemStyle CssClass="GridAltItem" />
                                    <HeaderStyle CssClass="GridHeader" Height="30"></HeaderStyle>
                                    <Columns>
                                        <asp:BoundColumn Visible="False" DataField="Ma_Nhom">
                                            <HeaderStyle Width="1%"></HeaderStyle>
                                        </asp:BoundColumn>
                                        <asp:TemplateColumn HeaderText="#">
                                            <HeaderStyle HorizontalAlign="Center" BackColor="#d4d4d4" Width="1%" />
                                            <ItemStyle BackColor="#d4d4d4" />
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn>
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <HeaderTemplate>
                                                <asp:Literal ID="Literal12" runat="server" Text="<%$ Resources:cms.language, lblNhom %>"></asp:Literal>
                                            </HeaderTemplate>
                                            <ItemStyle Width="20%" HorizontalAlign="Left" CssClass="GridBorderVerSolid"></ItemStyle>
                                            <ItemTemplate>
                                                <% if (StatusDisplay(0))
                                                   { %>
                                                <div id="collapsediv" style="width: 25px; float: left; text-align: center; cursor: pointer;
                                                    padding-right: 5px;" div-parrent="collapsedivparrent" userid-parrent="<%# DataBinder.Eval(Container.DataItem, "UserID")%>">
                                                    <img id="imgdivcollapse" style="border: 0; width: 30px; cursor: pointer;" src="../Dungchung/Images/bullet_toggle_plus.png"
                                                        img-parrent="imgflus" count-child='<%# DataBinder.Eval(Container.DataItem, "Dem")%>'
                                                        alt='' onclick="divexpandcollapse('<%# DataBinder.Eval(Container.DataItem, "Ma_nhom")%>',this);" />
                                                </div>
                                                <%}%>
                                                <asp:LinkButton ID="btnEdit" CssClass="linkGridForm extencss" Text='<%# DataBinder.Eval(Container.DataItem, "Ten_nhom")%>'
                                                    runat="server" CommandName="Edit" CommandArgument="Edit" Enabled='<%# HPCBusinessLogic.Utils.IsEnable(_Role.R_Write,"0",_user.UserID)%>'>
                                                </asp:LinkButton>
                                                <div id="div_child" style="float: left; padding-left: 5px" data-in='div<%# DataBinder.Eval(Container.DataItem, "Ma_nhom")%>'
                                                    userid='<%# DataBinder.Eval(Container.DataItem, "UserID")%>' div-child="child">
                                                    <span class="linkGridForm" style="font-weight: bold">
                                                        <%#DataBinder.Eval(Container.DataItem, "Userfullname")%></span>
                                                </div>
                                                <asp:Label ID="lbluserid" Visible="False" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "UserID")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn>
                                            <HeaderStyle HorizontalAlign="Center" Width="4%"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Center" Width="4%" CssClass="GridBorderVerSolid"></ItemStyle>
                                            <HeaderTemplate>
                                                <%=HPCComponents.CommonLib.ReadXML("lblQuytrinh") %>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:ImageButton ID="btnqtbt" Width="17px" runat="server" ImageUrl="../Dungchung/images/rollqtbt.png"
                                                    ImageAlign="AbsMiddle" ToolTip="Quy trình biên tập" CommandName="Edit" CommandArgument="RoleQTBT"
                                                    BorderStyle="None"></asp:ImageButton>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn Visible="false">
                                            <HeaderStyle HorizontalAlign="Center" Width="5%"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Center" Width="5%" CssClass="GridBorderVerSolid"></ItemStyle>
                                            <HeaderTemplate>
                                                Role Ngôn ngữ
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:ImageButton ID="btnRoleLanguage" Width="17px" runat="server" ImageUrl="~/Dungchung/images/Ngonngu.png"
                                                    ImageAlign="AbsMiddle" CommandName="Edit" CommandArgument="RoleLanguage" BorderStyle="None">
                                                </asp:ImageButton>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn>
                                            <HeaderStyle HorizontalAlign="Center" Width="5%"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Center" Width="5%" CssClass="GridBorderVerSolid"></ItemStyle>
                                            <HeaderTemplate>
                                                <%=HPCComponents.CommonLib.ReadXML("lblChucnang") %>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:ImageButton ID="btnRole" Width="17px" runat="server" ImageUrl="~/Dungchung/images/roles.gif"
                                                    ImageAlign="AbsMiddle" CommandName="Edit" CommandArgument="Role" BorderStyle="None">
                                                </asp:ImageButton>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn>
                                            <HeaderStyle HorizontalAlign="Center" Width="5%"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Center" Width="5%" CssClass="GridBorderVerSolid"></ItemStyle>
                                            <HeaderTemplate>
                                                <%=HPCComponents.CommonLib.ReadXML("lblChuyenmuc") %>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:ImageButton ID="btnRoleCate" Width="17px" runat="server" ImageUrl="~/Dungchung/images/QLCM.gif"
                                                    ImageAlign="AbsMiddle" ToolTip="Phân quyền chuyên mục" CommandName="Edit" CommandArgument="btnRoleCate"
                                                    BorderStyle="None"></asp:ImageButton>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn>
                                            <HeaderStyle HorizontalAlign="Center" Width="5%"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Center" Width="5%" CssClass="GridBorderVerSolid"></ItemStyle>
                                            <HeaderTemplate>
                                                <asp:Literal ID="Literal22" runat="server" Text="<%$ Resources:cms.language, lblXoa %>"></asp:Literal>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:ImageButton ID="btnDelete" Width="17px" runat="server" ImageUrl="~/Dungchung/images/cancel.gif"
                                                    ImageAlign="AbsMiddle" ToolTip="<%$ Resources:cms.language, lblXoa %>" CommandName="Edit"
                                                    CommandArgument="Delete" BorderStyle="None"></asp:ImageButton>
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
    <asp:Panel runat="server" ID="PanelGroupQTBT" Visible="false" CssClass="TitlePanel"
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
                    <table cellpadding="0" cellspacing="0" border="0" width="100%">
                        <tr>
                            <td class="TitlePanel" style="text-align: left; width: 100%; padding-bottom: 10px">
                                <asp:Label ID="LabelRoleQTBT" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: center; width: 100%; padding-bottom: 10px">
                                <asp:DataGrid runat="server" ID="DataGridGroupQTBT" OnItemDataBound="gdListMenu_ItemDataBound"
                                    AutoGenerateColumns="false" AlternatingItemStyle-BackColor="#F1F1F2" DataKeyField="ID"
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
                                                <asp:CheckBox ID="optSelect" runat="server" Checked='<%#Eval("Role")%>' />
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn>
                                            <HeaderStyle HorizontalAlign="Left" Width="95%"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Left" CssClass="GridBorderVerSolid"></ItemStyle>
                                            <HeaderTemplate>
                                                <%=HPCComponents.CommonLib.ReadXML("lblQuytrinh") %>
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
                                    <asp:LinkButton runat="server" ID="btnApplyRoleQTBT" Text="<%$Resources:cms.language,lblLuu %>"
                                        CssClass="iconSave" OnClick="btnApplyRoleQTBT_Click">
                                               
                                    </asp:LinkButton>
                                    <asp:LinkButton ID="LinkButtonExitQTBT" Text="<%$Resources:cms.language,lblThoat %>"
                                        OnClick="linkRoleCateExit_Click" CssClass="iconExit" runat="server">
                                               
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
    <asp:Panel runat="server" ID="PnlNgonNgu" Visible="false" CssClass="TitlePanel" BackColor="white"
        BorderStyle="NotSet">
        <table border="0" cellpadding="0" width="100%" cellspacing="0">
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
                                                <asp:CheckBox ID="optSelect" runat="server" Checked='<%#Eval("Role")%>' />
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn>
                                            <HeaderStyle Width="15%"></HeaderStyle>
                                            <ItemStyle CssClass="GridBorderVerSolid"></ItemStyle>
                                            <HeaderTemplate>
                                                Ngôn ngữ
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
                                    <asp:LinkButton runat="server" ID="lbtnSaveLang" Text="Lưu" CssClass="iconSave" OnClick="btnApplyRoleLanguage_Click">
                                               
                                    </asp:LinkButton>
                                    <asp:LinkButton ID="lbtnExit" Text="Thoát" OnClick="linkRoleCateExit_Click" CssClass="iconExit"
                                        runat="server">
                                               
                                    </asp:LinkButton>
                                </div>
                            </td>
                        </tr>
                    </table>
                </td>
                <td class="datagrid_content_right">
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
                            <td class="TitlePanel" style="height: 25px; text-align: right; width: 30%;">
                                <asp:Label ID="roleChucNang" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" colspan="3">
                                <asp:DataGrid runat="server" ID="gdListMenu" OnItemDataBound="gdListMenu_ItemDataBound"
                                    AutoGenerateColumns="false" DataKeyField="Ma_chucnang" CssClass="Grid" BorderColor="#d4d4d4"
                                    CellPadding="0" AlternatingItemStyle-BackColor="#F1F1F2" BackColor="White" Width="100%"
                                    BorderWidth="1px">
                                    <ItemStyle CssClass="GridItem"></ItemStyle>
                                    <AlternatingItemStyle CssClass="GridAltItem" />
                                    <HeaderStyle CssClass="GridHeader" Height="30"></HeaderStyle>
                                    <Columns>
                                        <asp:TemplateColumn>
                                            <HeaderStyle HorizontalAlign="Center" Width="4%" CssClass="sectionbanner"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Center" CssClass="GridBorderVerSolid"></ItemStyle>
                                            <HeaderTemplate>
                                                <asp:CheckBox ID="chkAll" onclick="javascript:CheckAllandUnCheckAllBTQ(this);" runat="server"
                                                    AutoPostBack="false" ToolTip="Select/Deselect All"></asp:CheckBox>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <input type="checkbox" id="optSelect" runat="server" onclick='ChkParrent(this,this.value);'
                                                    checked='<%# DataBinder.Eval(Container.DataItem, "Role_Menu") %>' value='<%#Container.ItemIndex+2%>' />
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn>
                                            <ItemStyle CssClass="GridBorderVerSolid"></ItemStyle>
                                            <HeaderStyle Width="30%"></HeaderStyle>
                                            <HeaderTemplate>
                                                <asp:Literal ID="Literal" runat="server" Text="<%$ Resources:cms.language, lblChucnang %>"></asp:Literal>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <%# SetNameMenu(DataBinder.Eval(Container.DataItem, "Ten_chucnang"), DataBinder.Eval(Container.DataItem, "Ma_chucnang"))%>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn>
                                            <ItemStyle CssClass="GridBorderVerSolid"></ItemStyle>
                                            <HeaderStyle Width="30%"></HeaderStyle>
                                            <HeaderTemplate>
                                                <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:cms.language, lblMota %>"></asp:Literal>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <%# DataBinder.Eval(Container.DataItem, "Mota")%>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText=" <span>Thêm </span>" HeaderStyle-HorizontalAlign="Center">
                                            <HeaderStyle Width="6%"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Center" CssClass="GridBorderVerSolid"></ItemStyle>
                                            <ItemTemplate>
                                                <input type="checkbox" id="chkR_Add" runat="server" name="chkR_Add" checked='<%# Eval("Doc") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="<span>Sửa</span>" HeaderStyle-HorizontalAlign="Center">
                                            <HeaderStyle Width="6%"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Center" CssClass="GridBorderVerSolid"></ItemStyle>
                                            <ItemTemplate>
                                                <input type="checkbox" id="chkR_Edit" runat="server" name="chkR_Edit" checked='<%# Eval("Ghi") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="<span>Xóa</span>" HeaderStyle-HorizontalAlign="Center">
                                            <HeaderStyle Width="6%"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Center" CssClass="GridBorderVerSolid"></ItemStyle>
                                            <ItemTemplate>
                                                <input type="checkbox" id="chkR_Del" runat="server" name="chkR_Del" checked='<%# Eval("Xoa") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                    </Columns>
                                </asp:DataGrid>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3" style="text-align: center">
                                <asp:LinkButton runat="server" ID="linkGroupSave" CssClass="iconSave" OnClick="linkGroupSave_Click"
                                    Text="<%$ Resources:cms.language, lblLuu %>">
                                    
                                </asp:LinkButton>
                                <asp:LinkButton runat="server" ID="LinkGroupExit" CssClass="iconExit" OnClick="LinkGroupExit_Click"
                                    Text="<%$ Resources:cms.language, lblThoat %>">
                                    
                                    
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
                            <td>
                                <table cellpadding="0" cellspacing="0" border="0" width="100%">
                                    <tr>
                                        <td style="width: 70%; text-align: left; vertical-align: middle">
                                            <asp:Label ID="lblRoleChuyenMuc" runat="server"></asp:Label>
                                        </td>
                                        <td style="text-align: right; width: 10%;">
                                            <span class="Titlelbl">
                                                <%=HPCComponents.CommonLib.ReadXML("lblAnpham") %></span>
                                        </td>
                                        <td style="text-align: left; width: 30%">
                                            <asp:DropDownList ID="cbo_anpham" Width="100%" CssClass="inputtext" runat="server"
                                                AutoPostBack="true" OnSelectedIndexChanged="cbo_anpham_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3" style="height: 4px">
                                            <input type="hidden" id="txtCateAccess" runat="server">
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" colspan="2">
                                <asp:DataGrid runat="server" ID="dgCategory" OnItemDataBound="gdListMenu_ItemDataBound"
                                    AutoGenerateColumns="false" DataKeyField="Ma_ChuyenMuc" CssClass="Grid" BorderColor="#d4d4d4"
                                    CellPadding="0" AlternatingItemStyle-BackColor="#F1F1F2" BackColor="White" Width="100%"
                                    BorderWidth="1px">
                                    <ItemStyle CssClass="GridItem"></ItemStyle>
                                    <AlternatingItemStyle CssClass="GridAltItem" />
                                    <HeaderStyle CssClass="GridHeader" Height="30"></HeaderStyle>
                                    <Columns>
                                        <asp:TemplateColumn>
                                            <HeaderStyle HorizontalAlign="Center" Width="2%"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Center" CssClass="GridBorderVerSolid"></ItemStyle>
                                            <HeaderTemplate>
                                                <asp:CheckBox ID="chkAll" onclick="javascript:SelectAllCheckboxes(this);" runat="server"
                                                    ToolTip="Select/Deselect All"></asp:CheckBox>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <input type="checkbox" id="<%# DataBinder.Eval(Container.DataItem, "CategoryParrent") %>"
                                                    name="optSelect" <%# DataBinder.Eval(Container.DataItem, "Role") %> onclick="<%# DataBinder.Eval(Container.DataItem, "OnClick_Event") %>"
                                                    value="<%# DataBinder.Eval(Container.DataItem, "Ma_ChuyenMuc") %>" />
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn>
                                            <HeaderStyle Width="30%"></HeaderStyle>
                                            <ItemStyle CssClass="GridBorderVerSolid"></ItemStyle>
                                            <HeaderTemplate>
                                                <%=HPCComponents.CommonLib.ReadXML("lblTenchuyenmuc") %>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <span class="linkGridForm">
                                                    <%# DataBinder.Eval(Container.DataItem, "Ten_chuyenmuc")%></span>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                    </Columns>
                                </asp:DataGrid>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: center">
                                <asp:LinkButton runat="server" ID="linkRoleCateSaves" OnClick="linkRoleCateSaves_Click"
                                    CssClass="iconSave" Text="<%$ Resources:cms.language, lblLuu %>" Font-Bold="true">
                                </asp:LinkButton>
                                <asp:LinkButton runat="server" ID="linkRoleCateExit" OnClick="linkRoleCateExit_Click"
                                    CssClass="iconExit" Text="<%$ Resources:cms.language, lblThoat %>" Font-Bold="true">
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
</asp:Content>
