<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/ITC.Master" AutoEventWireup="true"
    CodeBehind="QuytrinhDynamic.aspx.cs" Inherits="ToasoanTTXVN.Hethong.QuytrinhDynamic" %>

<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral,PublicKeyToken=31BF3856AD364E35" %>
<%@ Register TagPrefix="anthem" Namespace="Anthem" Assembly="Anthem" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<%@ Register TagPrefix="cc1" Namespace="HPCComponents.UI.WebControls" Assembly="HPCComponents" %>
<%@ Import Namespace="HPCComponents" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <link href="../Dungchung/Style/StateMachine/jquery-ui.css" rel="stylesheet" type="text/css" />
    <link href="../Dungchung/Style/StateMachine/stateMachineDemo.css" rel="stylesheet"
        type="text/css" />
    <link href="../Dungchung/Style/Layout/DemoDragresize.css" rel="stylesheet" type="text/css" />

    <script src="../Dungchung/Scripts/StateMachine/jquery.min.js" type="text/javascript"></script>

    <script src="../Dungchung/Scripts/StateMachine/jquery-ui.min.js" type="text/javascript"></script>

    <script type="text/javascript">

        $.fx.speeds._default = 1000;

        jQuery(function() {

            var dlg = jQuery("#dialog").dialog({
                title: 'Danh sách đối tượng',
                bgiframe: true,
                draggable: true,
                resizable: true,
                show: 'explode',
                hide: 'scale',
                width: 500,
                minwidth: 500,
                maxwidth: 800,
                autoOpen: false,
                modal: true
            });

            dlg.parent().appendTo(jQuery("form:first"));

            $("#opener").click(function() {

                $("#dialog").dialog("open");

                return false;
            });
        });

        
    </script>

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
                                        <td class="Titlelbl" style="text-align: right; width: 30%">
                                            <%=CommonLib.ReadXML("lblQuytrinh") %>:
                                        </td>
                                        <td style="width: 60%; text-align: left">
                                            <asp:TextBox ID="txtTenAnpham" CssClass="inputtext" Width="80%" runat="server" onkeypress="return clickButton(event,'ctl00_MainContent_linkSearch');"></asp:TextBox>
                                            <asp:Button runat="server" ID="linkSearch" CssClass="iconFind" Font-Bold="true" Style="margin-left: 5px"
                                                OnClick="linkSearch_OnClick" Text="<%$Resources:cms.language,lblTimkiem %>">
                                            </asp:Button>
                                        </td>
                                        <td style="width: 20%; text-align: right">
                                            <asp:Button ID="btn_add" runat="server" Text="<%$Resources:cms.language,lblThemmoi %>"
                                                CssClass="iconAdd" OnClick="OnClick_btn_add" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:DataGrid runat="server" ID="grdListAnpham" AlternatingItemStyle-BackColor="#F1F1F2"
                                    CssClass="Grid" AutoGenerateColumns="false" DataKeyField="ID" BorderColor="#d4d4d4"
                                    CellPadding="0" BackColor="White" Width="100%" BorderWidth="1px" OnEditCommand="grdListWorkFlow_EditCommand"
                                    OnItemDataBound="grdListWorkFlow_ItemDataBound">
                                    <ItemStyle CssClass="GridItem"></ItemStyle>
                                    <AlternatingItemStyle CssClass="GridAltItem" />
                                    <HeaderStyle CssClass="GridHeader" Height="30"></HeaderStyle>
                                    <HeaderStyle CssClass="GridHeader"></HeaderStyle>
                                    <Columns>
                                        <asp:BoundColumn Visible="False" DataField="ID">
                                            <HeaderStyle Width="1%"></HeaderStyle>
                                        </asp:BoundColumn>
                                        <asp:BoundColumn HeaderText="#">
                                            <HeaderStyle Width="1%" BackColor="#d4d4d4" />
                                            <ItemStyle BackColor="#d4d4d4" />
                                        </asp:BoundColumn>
                                        <asp:TemplateColumn>
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <HeaderTemplate>
                                                <%=CommonLib.ReadXML("lblAnpham") %>
                                            </HeaderTemplate>
                                            <ItemStyle Width="60%" HorizontalAlign="Left" CssClass="GridBorderVerSolid"></ItemStyle>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="btnEdit" CssClass="linkGridForm" Text='<%# DataBinder.Eval(Container.DataItem, "Ten_QuyTrinh")%>'
                                                    runat="server" CommandName="Edit" CommandArgument="Role" Enabled='<%# HPCBusinessLogic.Utils.IsEnable(_Role.R_Write,"0",_user.UserID)%>'>
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn>
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <HeaderTemplate>
                                                <%=CommonLib.ReadXML("lblMota") %>
                                            </HeaderTemplate>
                                            <ItemStyle Width="20%" HorizontalAlign="Left" CssClass="GridBorderVerSolid"></ItemStyle>
                                            <ItemTemplate>
                                                <span class="linkGridForm">
                                                    <%# DataBinder.Eval(Container.DataItem, "Mota")%></span>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn>
                                            <HeaderStyle HorizontalAlign="Center" Width="5%"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Center" Width="5%" CssClass="GridBorderVerSolid"></ItemStyle>
                                            <HeaderTemplate>
                                                <%=CommonLib.ReadXML("lblSua") %>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:ImageButton ID="btneditor" Width="20px" runat="server" ImageUrl="~/Dungchung/images/action.gif"
                                                    ImageAlign="AbsMiddle" CommandName="Edit" CommandArgument="Edit" BorderStyle="None">
                                                </asp:ImageButton>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn>
                                            <HeaderStyle HorizontalAlign="Center" Width="5%"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Center" Width="5%" CssClass="GridBorderVerSolid"></ItemStyle>
                                            <HeaderTemplate>
                                                Copy
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:ImageButton ID="btnCopyQT" Width="20px" runat="server" ImageUrl="~/Dungchung/images/Icons/copy_icon.jpg"
                                                    ImageAlign="AbsMiddle" ToolTip="copy" CommandName="Edit" CommandArgument="Copy"
                                                    BorderStyle="None"></asp:ImageButton>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn>
                                            <HeaderStyle HorizontalAlign="Center" Width="5%"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Center" Width="5%" CssClass="GridBorderVerSolid"></ItemStyle>
                                            <HeaderTemplate>
                                                <%=CommonLib.ReadXML("lblHienthi") %>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <anthem:ImageButton ID="btnHoatdong" runat="server" ImageUrl='<%#Global.GetHoatdong(DataBinder.Eval(Container.DataItem, "Active"))%>'
                                                    ImageAlign="AbsMiddle" ToolTip="Hiển thị" CommandName="Edit" CommandArgument="EditDisplay"
                                                    BorderStyle="None" AutoUpdateAfterCallBack="true" />
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
    <input type="text" id="lblMenuID" runat="server" style="display: none" />
    <input type="text" id="lblIPAddress" runat="server" style="display: none" />
    <input type="text" id="lblMaDoiTuongGui" runat="server" style="display: none" />
    <input type="text" id="lblMaAnPham" runat="server" style="display: none" />
    <input type="text" id="lblSTT" runat="server" style="display: none" />
    <input type="text" id="lblMaDT" style="display: none" runat="server" />
    <asp:Panel ID="plRole" runat="server" Visible="false" CssClass="TitlePanel" BackColor="white"
        BorderStyle="NotSet">

        <script src="../Dungchung/Scripts/StateMachine/jquery.jsPlumb-1.3.11-all-min.js"
            type="text/javascript"></script>

        <script src="../Dungchung/Scripts/StateMachine/demo-list.js" type="text/javascript"></script>

        <script src="../Dungchung/Scripts/StateMachine/demo-helper-jquery.js" type="text/javascript"></script>

        <table border="0" cellpadding="0" width="100%" cellspacing="0">
            <tr>
                <td class="datagrid_top_left">
                </td>
                <td class="datagrid_top_center">
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
                                <table style="width: 100%">
                                    <tr>
                                        <td style="text-align: left">
                                            <asp:Label ID="roleChucNang" runat="server"></asp:Label>
                                        </td>
                                        <td style="text-align: right;">
                                            <input id="opener" class="addNew" type="button" value="<%=CommonLib.ReadXML("lblChondoituong")%>" />
                                            <input id="btnSave" class="savePosition" type="button" onclick="getPosition();" value="<%=CommonLib.ReadXML("lblLuuvitri")%>" />
                                            <asp:Button ID="btnReset" class="resetQT" runat="server" Text="<%$Resources:cms.language,lblHuythietlap %>"
                                                OnClick="btnReset_Click" />
                                            <asp:Button ID="btnExit" class="exitQT" runat="server" Text="<%$Resources:cms.language,lblThoat %>"
                                                OnClick="btnExit_Click" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td valign="top">
                                <table style="width: 100%">
                                    <tr>
                                        <td style="width: 100%; height: 400px;" colspan="2">
                                            <div style="position: relative; border: solid 4px #f2f2f2; width: 100%; height: 100%">
                                                <div id="demo" style="height: 100%" runat="server">
                                                    <input type="text" id="lblDTGui" style="display: none" runat="server" />
                                                    <input type="text" id="lblDTNhan" style="display: none" runat="server" />
                                                    <asp:Repeater ID="rptDoituong" runat="server">
                                                        <ItemTemplate>
                                                            <div class="w" id='<%# Eval("Ma_Doituong")%>' style="left: <%# Eval("CssLeft")%>;
                                                                top: <%# Eval("CssTop")%>">
                                                                <div title="Di chuyển đối tượng" style="display: inline">
                                                                    <%# Eval("Ten_Doituong")%>
                                                                </div>
                                                                <div class="ep" title="Kéo mũi tên đến các đối tượng">
                                                                </div>
                                                                <div class="delItem" title="Xóa đối tượng" onclick="btnDelItem('<%# Eval("ID")%>','<%# Eval("Ten_Doituong")%>','<%# Eval("Ma_AnPham")%>');">
                                                                </div>
                                                            </div>
                                                            </br><br />
                                                            <br />
                                                            <br />
                                                        </ItemTemplate>
                                                    </asp:Repeater>
                                                </div>
                                            </div>
                                        </td>
                                    </tr>
                                </table>
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
    <div id="dialog" style="display: none">
        <table width="100%" cellspacing="2" cellpadding="4" border="0">
            <tr>
                <td colspan="2" style="text-align: center">
                    <table border="0" cellspacing="1" cellpadding="1" style="width: 100%">
                        <tr>
                            <td style="text-align: center; width: 80%;">
                                <asp:Label ID="lblMessError" runat="server" ForeColor="Red"></asp:Label>
                            </td>
                            <td style="text-align: left; width: 20%;">
                                <asp:Button ID="btnThem" class="addNewDT" ToolTip="<%$Resources:cms.language,lblTendoituong %>"
                                    runat="server" OnClick="btnThem_Click" />
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 100%" colspan="2">
                                <asp:GridView ID="GVDoituong" runat="Server" AutoGenerateColumns="False" BackColor="White"
                                    CssClass="Grid" GridLines="Vertical" AllowSorting="true" Width="100%" OnRowCommand="GVDoituong_OnRowCommand"
                                    OnRowDeleting="GVDoituong_RowDeleting" OnRowEditing="GVDoituong_RowEditing" OnRowUpdating="GVDoituong_RowUpdating"
                                    OnRowCancelingEdit="GVDoituong_RowCancelingEdit" ShowFooter="False" AutoGenerateEditButton="false"
                                    DataKeyNames="ID" EnableViewState="True">
                                    <RowStyle CssClass="GridItem" Height="25px" />
                                    <AlternatingRowStyle CssClass="GridAltItem" />
                                    <HeaderStyle CssClass="GridHeader" Height="30px"></HeaderStyle>
                                    <Columns>
                                        <asp:BoundField DataField="ID" HeaderText="" ReadOnly="True" Visible="false" />
                                        <asp:TemplateField>
                                            <HeaderStyle HorizontalAlign="Center" Width="4%" CssClass="sectionbanner"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Center" CssClass="GridBorderVerSolid"></ItemStyle>
                                            <HeaderTemplate>
                                                <asp:CheckBox ID="chkAll" onclick="javascript:CheckAllandUnCheckAllBTQ(this);" runat="server"
                                                    AutoPostBack="false" ToolTip="Select/Deselect All"></asp:CheckBox>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox type="checkbox" ID="optSelect" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="<%$Resources:cms.language,lblMadoituong %>">
                                            <HeaderStyle HorizontalAlign="Center" Width="30%" />
                                            <ItemStyle HorizontalAlign="Center" Width="30%" CssClass="GridBorderVerSolid" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblMaDT" runat="server" Text='<%# Eval("Ma_Doituong")%>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txt_MaDoituong" runat="Server" Text='<%# Eval("Ma_Doituong") %>'
                                                    Width="95%"></asp:TextBox><br />
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="..."
                                                    ControlToValidate="txt_MaDoituong">(Nhập mã đối tượng)
                                                </asp:RequiredFieldValidator>
                                            </EditItemTemplate>
                                            <FooterTemplate>
                                                <asp:TextBox ID="txt_MaDoituong" runat="Server" Width="95%"></asp:TextBox><br />
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="..."
                                                    ControlToValidate="txt_MaDoituong">(Nhập mã đối tượng)
                                                </asp:RequiredFieldValidator>
                                            </FooterTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="<%$Resources:cms.language,lblTendoituong %>">
                                            <HeaderStyle HorizontalAlign="Center" Width="30%" />
                                            <ItemStyle HorizontalAlign="Center" Width="30%" CssClass="GridBorderVerSolid" VerticalAlign="Top" />
                                            <ItemTemplate>
                                                <%# Eval("Ten_Doituong")%>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txt_Tendoituong" runat="Server" Text='<%# Eval("Ten_Doituong") %>'
                                                    Width="95%"></asp:TextBox><br />
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="..."
                                                    ControlToValidate="txt_Tendoituong">(Nhập tên đối tượng)
                                                </asp:RequiredFieldValidator>
                                            </EditItemTemplate>
                                            <FooterTemplate>
                                                <asp:TextBox ID="txt_Tendoituong" runat="Server" Width="95%"></asp:TextBox><br />
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="..."
                                                    ControlToValidate="txt_Tendoituong">(Nhập tên đối tượng)
                                                </asp:RequiredFieldValidator>
                                            </FooterTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="<%$Resources:cms.language,lblThutu %>">
                                            <HeaderStyle HorizontalAlign="Center" Width="20%" />
                                            <ItemStyle HorizontalAlign="Center" Width="20%" CssClass="GridBorderVerSolid" VerticalAlign="Top" />
                                            <ItemTemplate>
                                                <%# Eval("STT")%>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txt_STT" runat="Server" Text='<%# Eval("STT") %>' Width="95%" 
                                                    onkeypress="return check_Number(this,event);"></asp:TextBox><br />
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="..."
                                                    ControlToValidate="txt_STT">(Nhập số thứ tự)
                                                </asp:RequiredFieldValidator>
                                            </EditItemTemplate>
                                            <FooterTemplate>
                                                <asp:TextBox ID="txt_STT" runat="Server" Width="95%" onkeypress="return check_Number(this,event);"></asp:TextBox>
                                                <br />
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="..."
                                                    ControlToValidate="txt_STT">(Nhập số thứ tự)
                                                </asp:RequiredFieldValidator>
                                            </FooterTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="<%$Resources:cms.language,lblSua %>">
                                            <HeaderStyle HorizontalAlign="Center" Width="5%" />
                                            <ItemStyle HorizontalAlign="Center" Width="5%" CssClass="GridBorderVerSolid" />
                                            <ItemTemplate>
                                                <asp:ImageButton ID="btnAdd" Width="15px" runat="server" ImageUrl="~/Dungchung/images/modify.png"
                                                    ImageAlign="AbsMiddle" ToolTip="Sửa thông tin" CommandName="Edit" CommandArgument="Edit"
                                                    BorderStyle="None"></asp:ImageButton>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:ImageButton ID="btnUpdate" Width="15px" runat="server" ImageUrl="~/Dungchung/images/save.gif"
                                                    ImageAlign="AbsMiddle" ToolTip="Lưu giữ" CommandName="Update" BorderStyle="None">
                                                </asp:ImageButton>
                                                <asp:ImageButton ID="btnCancel" Width="15px" runat="server" ImageUrl="~/Dungchung/images/undo.gif"
                                                    ImageAlign="AbsMiddle" ToolTip="Hủy bỏ" CausesValidation="false" CommandName="Cancel"
                                                    BorderStyle="None"></asp:ImageButton>
                                            </EditItemTemplate>
                                            <FooterTemplate>
                                                <asp:ImageButton ID="btnAddNew" Width="15px" runat="server" ImageUrl="~/Dungchung/Images/Icons/Add.gif"
                                                    ImageAlign="AbsMiddle" ToolTip="Thêm mới" CommandName="AddNew" BorderStyle="None">
                                                </asp:ImageButton>
                                            </FooterTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="<%$Resources:cms.language,lblXoa %>">
                                            <HeaderStyle HorizontalAlign="Center" Width="5%" />
                                            <ItemStyle HorizontalAlign="Center" Width="5%" CssClass="GridBorderVerSolid" />
                                            <ItemTemplate>
                                                <asp:ImageButton ID="btnDelete" Width="15px" runat="server" ImageUrl="~/Dungchung/Style/Layout/Delitem.png"
                                                    ImageAlign="AbsMiddle" ToolTip="Xóa" OnClientClick="return confirm('Bạn có muốn xóa đối tượng này không ?');"
                                                    CommandName="Delete" BorderStyle="None"></asp:ImageButton>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:ImageButton ID="btnCancel" Width="15px" runat="server" ImageUrl="~/Dungchung/Images/Icons/Cancel.gif"
                                                    ImageAlign="AbsMiddle" ToolTip="Hủy" CausesValidation="false" CommandName="Cancel"
                                                    BorderStyle="None"></asp:ImageButton>
                                            </FooterTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <FooterStyle BackColor="#CCCC99" />
                                </asp:GridView>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right" class="pageNav" colspan="2">
                                <cc1:CurrentPage runat="server" ID="curentPages"></cc1:CurrentPage>
                                <cc1:Pager runat="server" ID="Pager1" OnIndexChanged="pages_IndexChanged" />
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: left; width: 10%;">
                                <asp:Button runat="server" ID="ThemDT" CssClass="myButton blue" Font-Bold="true"
                                    OnClick="ThemDT_Click" Text="<%$Resources:cms.language,lblChondoituong %>"></asp:Button>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td align="center">
                    <input id="btnCancel" value="<%$Resources:cms.language,lblThoat %>" type="button"
                        class="buttonhide" runat="server" onserverclick="btnCancel_Click" />
                </td>
            </tr>
        </table>
    </div>
    <!-- demo code -->

    <script src="../Dungchung/Scripts/StateMachine/stateMachineDemo.js" type="text/javascript"></script>

    <script src="../Dungchung/Scripts/StateMachine/stateMachineDemo-jquery.js" type="text/javascript"></script>

    <!-- end demo code -->
    <!-- demo help code -->

    <script src="../Dungchung/Scripts/StateMachine/ControlManager.js" type="text/javascript"></script>

    <script type="text/javascript">

        function check_Number(obj, e) {
            var key;
            var isCtrl;
            if (window.event) {
                key = window.event.keyCode;     //IE
                if (window.event.ctrlKey)
                    isCtrl = true;
                else
                    isCtrl = false;
            }
            else {
                key = e.which;     //firefox
                if (e.ctrlKey)
                    isCtrl = true;
                else
                    isCtrl = false;
            }
            if ((key >= 48 && key <= 57) || key == 8 || key == 44 || ((key == 118 || key == 99 || key == 97) && isCtrl)) {
            }
            else return false;
        }
    </script>

</asp:Content>
