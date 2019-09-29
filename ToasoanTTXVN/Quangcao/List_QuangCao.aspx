﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/ITC.Master" AutoEventWireup="true"
    CodeBehind="List_QuangCao.aspx.cs" Inherits="ToasoanTTXVN.Quangcao.List_QuangCao" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<%@ Register TagPrefix="cc1" Namespace="HPCComponents.UI.WebControls" Assembly="HPCComponents" %>
<%@ Import Namespace="HPCComponents" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <script language="Javascript" type="text/javascript">
        function get_check_value(idkhac) {
            var elm = document.aspnetForm.elements;
            for (i = 0; i < elm.length; i++) {
                if (elm[i].type == "checkbox" && elm[i].checked && elm[i].id != idkhac)
                    return true;
            }
            return false;
        }
        function CheckConfirmDelete() {
            var bol;
            bol = get_check_value("ctl00_MainContent_TabContainer1_tabpnltinXuly_dgr_quangcao1_ctl01_chkAll");
            if (bol == true) {
                if (confirm("Bạn có chắc chắn muốn xóa?"))
                    return true;
                else return false;
            }
            else {
                alert("Bạn chưa chọn bản ghi nào!"); return false;
            }
        }
        function CheckConfirmGuiduyet() {
            var bol;
            bol = get_check_value("ctl00_MainContent_TabContainer1_tabpnltinXuly_dgr_quangcao1_ctl01_chkAll");
            if (bol == true) {
                if (confirm("Bạn có chắc chắn muốn duyệt quảng cáo này?"))
                    return true;
                else return false;
            }
            else {
                alert("Bạn chưa chọn bản ghi nào!"); return false;
            }
        }
        function CheckConfirmSave() {
            var bol;
            bol = get_check_value("ctl00_MainContent_TabContainer1_tabpnltinXuly_grdListSoBao_ctl01_chkAll");
            if (bol == true) {
                if (confirm("Bạn chắc chắn chọn?"))
                    return true;
                else return false;
            }
            else {
                alert("Bạn chưa chọn số báo nào!"); return false;
            }
        }
    </script>

    <script type="text/javascript" language="javascript">
        function CheckItem() {
            var allitem = $('.checkitem').find("input");
            var check = false;
            $.each(allitem, function(index, key) {
                var item = $(key);
                var itemchek = item.is(":checked");
                if (itemchek)
                    check = true;
            });
            var divcontrol = $('#divbutton');
            if (check) {
                divcontrol.css("display", "");
            }
            else
                divcontrol.css("display", "none");
        }
    </script>

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
                        <td style="text-align: right">
                            <asp:UpdatePanel ID="UpdatePanelSeach" runat="server">
                                <ContentTemplate>
                                    <table id="Tbl_Search" border="0" cellpadding="1" cellspacing="1" style="width: 100%;
                                        text-align: right">
                                        <tr>
                                            <td style="text-align: right; width: 8%" class="Titlelbl">
                                                <%=CommonLib.ReadXML("lblKhachhang")%>:
                                            </td>
                                            <td style="width: 20%; text-align: left">
                                                <asp:DropDownList ID="cbokhachang" runat="server" Width="100%" CssClass="inputtext"
                                                    DataTextField="Ten_KhachHang" DataValueField="Ma_KhachHang" TabIndex="5">
                                                </asp:DropDownList>
                                            </td>
                                            <td style="text-align: right; width: 8%" class="Titlelbl">
                                                <%=CommonLib.ReadXML("lblTieude")%>:
                                            </td>
                                            <td style="width: 20%; text-align: left">
                                                <asp:TextBox ID="txt_tenquangcao" TabIndex="1" Width="95%" runat="server" CssClass="inputtext"
                                                    onkeypress="return clickButton(event,'ctl00_MainContent_cmdSeek');"></asp:TextBox>
                                            </td>
                                            <td style="text-align: right; width: 8%" class="Titlelbl">
                                                <%=CommonLib.ReadXML("lblAnpham")%>:
                                            </td>
                                            <td style="width: 20%; text-align: left">
                                                <asp:DropDownList ID="cbo_loaibao" runat="server" Width="100%" CssClass="inputtext"
                                                    AutoPostBack="true" OnSelectedIndexChanged="cbo_loaibao_SelectedIndexChanged"
                                                    TabIndex="5">
                                                </asp:DropDownList>
                                            </td>
                                            <td style="text-align: right; width: 5%" class="Titlelbl">
                                                <%=CommonLib.ReadXML("lblTrang")%>:
                                            </td>
                                            <td style="width: 12%; text-align: left">
                                                <asp:DropDownList ID="cbo_trang" runat="server" Width="100%" CssClass="inputtext"
                                                    TabIndex="5">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: center; width: 100%" colspan="8">
                                                <asp:LinkButton runat="server" ID="cmdSeek" CssClass="iconFind" OnClick="cmdSeek_Click"
                                                    Text="<%$Resources:cms.language, lblTimkiem%>"></asp:LinkButton>
                                                <asp:LinkButton runat="server" ID="LinkAdd" CssClass="iconAdd" OnClick="ThemMoi_Click"
                                                    Text="<%$Resources:cms.language, lblThemmoi%>">
                                                </asp:LinkButton>
                                            </td>
                                        </tr>
                                    </table>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 4px">
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table width="100%" cellspacing="0" cellpadding="0" border="0">
                                <tr>
                                    <td align="left">
                                        <asp:UpdatePanel ID="upnllistquangcao" runat="server">
                                            <ContentTemplate>
                                                <cc2:TabContainer ID="TabContainer1" runat="server" CssClass="ajax__tab_yuitabview-theme"
                                                    AutoPostBack="true" ActiveTabIndex="0" OnActiveTabChanged="TabContainer1_ActiveTabChanged">
                                                    <cc2:TabPanel HeaderText="QC đang xử lý" ID="TabPanelQC_Dangxuly" runat="server">
                                                        <ContentTemplate>
                                                            <div style="float: left; width: 100%">
                                                                <asp:DataGrid ID="dgr_quangcao" runat="server" Width="100%" BorderStyle="None" AutoGenerateColumns="False"
                                                                    CssClass="Grid" BorderColor="#d4d4d4" CellPadding="0" DataKeyField="Ma_Quangcao"
                                                                    BackColor="White" BorderWidth="1px" AlternatingItemStyle-BackColor="#F1F1F2"
                                                                    OnEditCommand="dgr_quangcao_EditCommand" OnItemDataBound="dgr_quangcao_ItemDataBound">
                                                                    <ItemStyle CssClass="GridItem"></ItemStyle>
                                                                    <AlternatingItemStyle CssClass="GridAltItem" />
                                                                    <HeaderStyle CssClass="GridHeader" Height="30px"></HeaderStyle>
                                                                    <Columns>
                                                                        <asp:TemplateColumn>
                                                                            <HeaderStyle HorizontalAlign="Center" Width="3%"></HeaderStyle>
                                                                            <ItemStyle HorizontalAlign="Center" CssClass="GridBorderVerSolid"></ItemStyle>
                                                                            <HeaderTemplate>
                                                                                <asp:CheckBox ID="chkAll" onclick="javascript:SelectAllCheckboxes(this);" runat="server"
                                                                                    ToolTip="All"></asp:CheckBox>
                                                                            </HeaderTemplate>
                                                                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                                            <ItemTemplate>
                                                                                <asp:CheckBox runat="server" Text='' ID='optSelect' AutoPostBack="False" CssClass="checkitem"
                                                                                    onclick="javascript:CheckItem()" Enabled='<%# HPCBusinessLogic.Utils.IsEnable(_Role.R_Write,"0",_user.UserID)%>'>
                                                                                </asp:CheckBox>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateColumn>
                                                                        <asp:BoundColumn DataField="Ma_Quangcao" HeaderText="Mã" Visible="False"></asp:BoundColumn>
                                                                        <asp:TemplateColumn HeaderText="<%$Resources:cms.language, lblTieude%>">
                                                                            <HeaderStyle HorizontalAlign="Left" Width="30%"></HeaderStyle>
                                                                            <ItemStyle HorizontalAlign="Left"  CssClass="GridBorderVerSolid"></ItemStyle>
                                                                            <ItemTemplate>
                                                                                <asp:LinkButton CssClass="linkGridForm" Text='<%# DataBinder.Eval(Container.DataItem, "Ten_QuangCao" )%>'
                                                                                    runat="server" ID="linkTittle" CommandName="Edit" CommandArgument="Edit" ToolTip="Sửa bài"
                                                                                    Enabled='<%# HPCBusinessLogic.Utils.IsEnable(_Role.R_Write,"0",_user.UserID)%>'></asp:LinkButton>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateColumn>
                                                                        <asp:TemplateColumn HeaderText="<%$Resources:cms.language, lblKhachhang%>">
                                                                            <HeaderStyle HorizontalAlign="Left" Width="20%"></HeaderStyle>
                                                                            <ItemStyle HorizontalAlign="Left"  CssClass="GridBorderVerSolid"></ItemStyle>
                                                                            <ItemTemplate>
                                                                                <span class="linkGridForm">
                                                                                    <%#HPCBusinessLogic.UltilFunc.GetTenKhachHang(Eval("Ma_KhachHang"))%></span>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateColumn>
                                                                        <asp:TemplateColumn HeaderText="<%$Resources:cms.language, lblKichthuocquangcao%>">
                                                                            <HeaderStyle HorizontalAlign="Left" Width="10%"></HeaderStyle>
                                                                            <ItemStyle HorizontalAlign="Left" Width="10%" CssClass="GridBorderVerSolid"></ItemStyle>
                                                                            <ItemTemplate>
                                                                                <span class="linkGridForm">
                                                                                    <%# HPCBusinessLogic.UltilFunc.GetKichCoQuangCao(DataBinder.Eval(Container.DataItem, "kichthuoc"))%></span>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateColumn>
                                                                        <asp:TemplateColumn HeaderText="<%$Resources:cms.language, lblAnpham%>">
                                                                            <HeaderStyle HorizontalAlign="Left" Width="8%"></HeaderStyle>
                                                                            <ItemStyle HorizontalAlign="Left" Width="8%" CssClass="GridBorderVerSolid"></ItemStyle>
                                                                            <ItemTemplate>
                                                                                <span class="linkGridForm">
                                                                                    <%#HPCBusinessLogic.UltilFunc.GetTenAnpham(Eval("Ma_Loaibao"))%></span>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateColumn>
                                                                        <asp:TemplateColumn HeaderText="<%$Resources:cms.language, lblTrang%>">
                                                                            <HeaderStyle HorizontalAlign="Center" Width="5%"></HeaderStyle>
                                                                            <ItemStyle HorizontalAlign="Center" Width="5%" CssClass="GridBorderVerSolid"></ItemStyle>
                                                                            <ItemTemplate>
                                                                                <span class="linkGridForm">
                                                                                    <%#DataBinder.Eval(Container.DataItem, "Trang")%></span>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateColumn>
                                                                        <asp:TemplateColumn HeaderText="<%$Resources:cms.language, lblNgaydangquangcao%>">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="ngaybatdau" CssClass="linkGridForm" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Ngaydang")!=System.DBNull.Value?Convert.ToDateTime(DataBinder.Eval(Container, "DataItem.Ngaydang")).ToString("dd/MM/yyyy HH:mm"):"" %>'>
                                                                                </asp:Label>
                                                                            </ItemTemplate>
                                                                            <HeaderStyle HorizontalAlign="Center" Width="8%"></HeaderStyle>
                                                                            <ItemStyle HorizontalAlign="Center" CssClass="GridBorderVerSolid"></ItemStyle>
                                                                        </asp:TemplateColumn>
                                                                        <asp:TemplateColumn HeaderText="<%$Resources:cms.language, lblNgayxuly%>">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="ngaytao" CssClass="linkGridForm" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Ngaytao")!=System.DBNull.Value?Convert.ToDateTime(DataBinder.Eval(Container, "DataItem.Ngaytao")).ToString("dd/MM/yyyy HH:mm"):"" %>'>
                                                                                </asp:Label>
                                                                            </ItemTemplate>
                                                                            <HeaderStyle HorizontalAlign="Center" Width="8%"></HeaderStyle>
                                                                            <ItemStyle HorizontalAlign="Center" CssClass="GridBorderVerSolid"></ItemStyle>
                                                                        </asp:TemplateColumn>
                                                                        <asp:TemplateColumn HeaderText="<%$Resources:cms.language, lblNguoixuly%>">
                                                                            <ItemTemplate>
                                                                                <span class="linkGridForm">
                                                                                    <%#HPCBusinessLogic.UltilFunc.GetUserFullName(DataBinder.Eval(Container.DataItem, "Nguoitao"))%></span>
                                                                            </ItemTemplate>                                                                            
                                                                        </asp:TemplateColumn>
                                                                    </Columns>
                                                                </asp:DataGrid>
                                                            </div>
                                                        </ContentTemplate>
                                                    </cc2:TabPanel>
                                                    <cc2:TabPanel HeaderText="QC trả lại" ID="TabPanelQC_Tralai" runat="server">
                                                        <ContentTemplate>
                                                            <div style="float: left; width: 100%">
                                                                <asp:DataGrid ID="DataGridQC_Tralai" runat="server" Width="100%" BorderStyle="None"
                                                                    AutoGenerateColumns="False" CssClass="Grid" BorderColor="#d4d4d4" CellPadding="0"
                                                                    DataKeyField="Ma_Quangcao" BackColor="White" BorderWidth="1px" AlternatingItemStyle-BackColor="#F1F1F2"
                                                                    OnEditCommand="dgr_quangcao_EditCommand" OnItemDataBound="dgr_quangcao_ItemDataBound">
                                                                    <ItemStyle CssClass="GridItem"></ItemStyle>
                                                                    <AlternatingItemStyle CssClass="GridAltItem" />
                                                                    <HeaderStyle CssClass="GridHeader"></HeaderStyle>
                                                                    <Columns>
                                                                        <asp:TemplateColumn>
                                                                            <HeaderStyle HorizontalAlign="Center" Width="3%"></HeaderStyle>
                                                                            <ItemStyle HorizontalAlign="Center" CssClass="GridBorderVerSolid"></ItemStyle>
                                                                            <HeaderTemplate>
                                                                                <asp:CheckBox ID="chkAll" onclick="javascript:SelectAllCheckboxes(this);" runat="server"
                                                                                    ToolTip="Chọn tất cả"></asp:CheckBox>
                                                                            </HeaderTemplate>
                                                                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                                            <ItemTemplate>
                                                                                <asp:CheckBox runat="server" Text='' ID='optSelect' AutoPostBack="False" CssClass="checkitem"
                                                                                    onclick="javascript:CheckItem()" Enabled='<%# HPCBusinessLogic.Utils.IsEnable(_Role.R_Write,"0",_user.UserID)%>'>
                                                                                </asp:CheckBox>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateColumn>
                                                                        <asp:BoundColumn DataField="Ma_Quangcao" HeaderText="Mã" Visible="False"></asp:BoundColumn>
                                                                        <asp:TemplateColumn HeaderText="<%$Resources:cms.language, lblTieude%>">
                                                                            <HeaderStyle HorizontalAlign="Center" Width="20%"></HeaderStyle>
                                                                            <ItemStyle HorizontalAlign="Left" Width="20%" CssClass="GridBorderVerSolid"></ItemStyle>
                                                                            <ItemTemplate>
                                                                                <asp:LinkButton CssClass="linkGridForm" Text='<%# DataBinder.Eval(Container.DataItem, "Ten_QuangCao" )%>'
                                                                                    runat="server" ID="linkTittle" CommandName="Edit" CommandArgument="Edit" ToolTip="Sửa bài"
                                                                                    Enabled='<%# HPCBusinessLogic.Utils.IsEnable(_Role.R_Write,"0",_user.UserID)%>'></asp:LinkButton>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateColumn>
                                                                        <asp:TemplateColumn HeaderText="<%$Resources:cms.language, lblKhachhang%>">
                                                                            <HeaderStyle HorizontalAlign="Center" Width="12%"></HeaderStyle>
                                                                            <ItemStyle HorizontalAlign="Left" Width="12%" CssClass="GridBorderVerSolid"></ItemStyle>
                                                                            <ItemTemplate>
                                                                                <span class="linkGridForm">
                                                                                    <%#HPCBusinessLogic.UltilFunc.GetTenKhachHang(Eval("Ma_KhachHang"))%></span>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateColumn>
                                                                        <asp:TemplateColumn HeaderText="<%$Resources:cms.language, lblKichthuocquangcao%>">
                                                                            <HeaderStyle HorizontalAlign="Center" Width="10%"></HeaderStyle>
                                                                            <ItemStyle HorizontalAlign="Left" Width="10%" CssClass="GridBorderVerSolid"></ItemStyle>
                                                                            <ItemTemplate>
                                                                                <span class="linkGridForm">
                                                                                    <%#HPCBusinessLogic.UltilFunc.GetKichCoQuangCao( DataBinder.Eval(Container.DataItem, "kichthuoc" ))%></span>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateColumn>
                                                                        <asp:TemplateColumn HeaderText="<%$Resources:cms.language, lblAnpham%>">
                                                                            <HeaderStyle HorizontalAlign="Center" Width="8%"></HeaderStyle>
                                                                            <ItemStyle HorizontalAlign="Center" Width="8%" CssClass="GridBorderVerSolid"></ItemStyle>
                                                                            <ItemTemplate>
                                                                                <span class="linkGridForm">
                                                                                    <%#HPCBusinessLogic.UltilFunc.GetTenAnpham(Eval("Ma_Loaibao"))%></span>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateColumn>
                                                                        <asp:TemplateColumn HeaderText="<%$Resources:cms.language, lblTrang%>">
                                                                            <HeaderStyle HorizontalAlign="Center" Width="5%"></HeaderStyle>
                                                                            <ItemStyle HorizontalAlign="Center" Width="5%" CssClass="GridBorderVerSolid"></ItemStyle>
                                                                            <ItemTemplate>
                                                                                <span class="linkGridForm">
                                                                                    <%#DataBinder.Eval(Container.DataItem, "Trang")%></span>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateColumn>
                                                                        <asp:TemplateColumn HeaderText="<%$Resources:cms.language, lblNgaydangquangcao%>">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="ngaybatdau" CssClass="linkGridForm" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Ngaydang")!=System.DBNull.Value?Convert.ToDateTime(DataBinder.Eval(Container, "DataItem.Ngaydang")).ToString("dd/MM/yyyy HH:mm"):"" %>'>
                                                                                </asp:Label>
                                                                            </ItemTemplate>
                                                                            <HeaderStyle HorizontalAlign="Center" Width="8%"></HeaderStyle>
                                                                            <ItemStyle HorizontalAlign="Center" CssClass="GridBorderVerSolid"></ItemStyle>
                                                                        </asp:TemplateColumn>
                                                                        <asp:TemplateColumn HeaderText="<%$Resources:cms.language, lblNgaytra%>">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="ngaytra" CssClass="linkGridForm" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Ngaytao")!=System.DBNull.Value?Convert.ToDateTime(DataBinder.Eval(Container, "DataItem.Ngaytao")).ToString("dd/MM/yyyy HH:mm"):"" %>'>
                                                                                </asp:Label>
                                                                            </ItemTemplate>
                                                                            <HeaderStyle HorizontalAlign="Center" Width="8%"></HeaderStyle>
                                                                            <ItemStyle HorizontalAlign="Center" CssClass="GridBorderVerSolid"></ItemStyle>
                                                                        </asp:TemplateColumn>
                                                                        <asp:TemplateColumn HeaderText="<%$Resources:cms.language, lblNguoitra%>">
                                                                            <ItemTemplate>
                                                                                <span class="linkGridForm">
                                                                                    <%#HPCBusinessLogic.UltilFunc.GetUserFullName(DataBinder.Eval(Container.DataItem, "Nguoitao"))%></span>
                                                                            </ItemTemplate>
                                                                            <HeaderStyle HorizontalAlign="Center" Width="10%"></HeaderStyle>
                                                                            <ItemStyle HorizontalAlign="Left" CssClass="GridBorderVerSolid"></ItemStyle>
                                                                        </asp:TemplateColumn>
                                                                    </Columns>
                                                                </asp:DataGrid>
                                                            </div>
                                                        </ContentTemplate>
                                                    </cc2:TabPanel>
                                                    <cc2:TabPanel HeaderText="QC đã gửi" ID="TabPanelQC_Daxuly" runat="server">
                                                        <ContentTemplate>
                                                            <div style="float: left; width: 100%">
                                                                <asp:DataGrid ID="DataGridQC_Dagui" runat="server" Width="100%" BorderStyle="None"
                                                                    AutoGenerateColumns="False" CssClass="Grid" BorderColor="#d4d4d4" CellPadding="0"
                                                                    DataKeyField="ID" BackColor="White" BorderWidth="1px" AlternatingItemStyle-BackColor="#F1F1F2"
                                                                    OnEditCommand="dgr_quangcao_EditCommand" OnItemDataBound="dgr_quangcao_ItemDataBound">
                                                                    <ItemStyle CssClass="GridItem"></ItemStyle>
                                                                    <AlternatingItemStyle CssClass="GridAltItem" />
                                                                    <HeaderStyle CssClass="GridHeader"></HeaderStyle>
                                                                    <Columns>
                                                                        <asp:TemplateColumn>
                                                                            <HeaderStyle HorizontalAlign="Center" Width="3%"></HeaderStyle>
                                                                            <ItemStyle HorizontalAlign="Center" CssClass="GridBorderVerSolid"></ItemStyle>
                                                                            <HeaderTemplate>
                                                                                <asp:CheckBox ID="chkAll" onclick="javascript:SelectAllCheckboxes(this);" runat="server"
                                                                                    ToolTip="Chọn tất cả"></asp:CheckBox>
                                                                            </HeaderTemplate>
                                                                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                                            <ItemTemplate>
                                                                                <asp:CheckBox runat="server" Text='' ID='optSelect' AutoPostBack="False" CssClass="checkitem"
                                                                                    onclick="javascript:CheckItem()"></asp:CheckBox>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateColumn>
                                                                        <asp:BoundColumn DataField="ID" HeaderText="Mã" Visible="False"></asp:BoundColumn>
                                                                        <asp:TemplateColumn HeaderText="<%$Resources:cms.language, lblTieude%>">
                                                                            <HeaderStyle HorizontalAlign="Center" Width="20%"></HeaderStyle>
                                                                            <ItemStyle HorizontalAlign="Left" Width="20%" CssClass="GridBorderVerSolid"></ItemStyle>
                                                                            <ItemTemplate>
                                                                                <a class="linkGridForm" href="Javascript:open_window_Scroll('<%=Global.ApplicationPath%>/Quangcao/ViewPhienBanQC.aspx?Menu_ID=<%=Page.Request.QueryString["Menu_ID"]%>&ID=<%# DataBinder.Eval(Container.DataItem, "ma_quangcao") %>',50,500,100,800);">
                                                                                    <%# DataBinder.Eval(Container.DataItem, "Ten_QuangCao" )%></a>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateColumn>
                                                                        <asp:TemplateColumn HeaderText="<%$Resources:cms.language, lblKhachhang%>">
                                                                            <HeaderStyle HorizontalAlign="Center" Width="12%"></HeaderStyle>
                                                                            <ItemStyle HorizontalAlign="Left" Width="12%" CssClass="GridBorderVerSolid"></ItemStyle>
                                                                            <ItemTemplate>
                                                                                <span class="linkGridForm">
                                                                                    <%#HPCBusinessLogic.UltilFunc.GetTenKhachHang(Eval("Ma_KhachHang"))%></span>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateColumn>
                                                                        <asp:TemplateColumn HeaderText="<%$Resources:cms.language, lblKichthuocquangcao%>">
                                                                            <HeaderStyle HorizontalAlign="Center" Width="10%"></HeaderStyle>
                                                                            <ItemStyle HorizontalAlign="Left" Width="10%" CssClass="GridBorderVerSolid"></ItemStyle>
                                                                            <ItemTemplate>
                                                                                <span class="linkGridForm">
                                                                                    <%# HPCBusinessLogic.UltilFunc.GetKichCoQuangCao(DataBinder.Eval(Container.DataItem, "kichthuoc" ))%></span>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateColumn>
                                                                        <asp:TemplateColumn HeaderText="<%$Resources:cms.language, lblAnpham%>">
                                                                            <HeaderStyle HorizontalAlign="Center" Width="8%"></HeaderStyle>
                                                                            <ItemStyle HorizontalAlign="Center" Width="8%" CssClass="GridBorderVerSolid"></ItemStyle>
                                                                            <ItemTemplate>
                                                                                <span class="linkGridForm">
                                                                                    <%#HPCBusinessLogic.UltilFunc.GetTenAnpham(Eval("Ma_Loaibao"))%></span>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateColumn>
                                                                        <asp:TemplateColumn HeaderText="<%$Resources:cms.language, lblTrang%>">
                                                                            <HeaderStyle HorizontalAlign="Center" Width="5%"></HeaderStyle>
                                                                            <ItemStyle HorizontalAlign="Center" Width="5%" CssClass="GridBorderVerSolid"></ItemStyle>
                                                                            <ItemTemplate>
                                                                                <span class="linkGridForm">
                                                                                    <%#DataBinder.Eval(Container.DataItem, "Trang")%></span>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateColumn>
                                                                        <asp:TemplateColumn HeaderText="<%$Resources:cms.language, lblNgaydangquangcao%>">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="ngaybatdau" CssClass="linkGridForm" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Ngaydang")!=System.DBNull.Value?Convert.ToDateTime(DataBinder.Eval(Container, "DataItem.Ngaydang")).ToString("dd/MM/yyyy HH:mm"):"" %>'>
                                                                                </asp:Label>
                                                                            </ItemTemplate>
                                                                            <HeaderStyle HorizontalAlign="Center" Width="8%"></HeaderStyle>
                                                                            <ItemStyle HorizontalAlign="Center" CssClass="GridBorderVerSolid"></ItemStyle>
                                                                        </asp:TemplateColumn>
                                                                        <asp:TemplateColumn HeaderText="<%$Resources:cms.language, lblNgayxuly%>">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="ngaygui" CssClass="linkGridForm" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Ngaytao")!=System.DBNull.Value?Convert.ToDateTime(DataBinder.Eval(Container, "DataItem.Ngaytao")).ToString("dd/MM/yyyy HH:mm"):"" %>'>
                                                                                </asp:Label>
                                                                            </ItemTemplate>
                                                                            <HeaderStyle HorizontalAlign="Center" Width="8%"></HeaderStyle>
                                                                            <ItemStyle HorizontalAlign="Center" CssClass="GridBorderVerSolid"></ItemStyle>
                                                                        </asp:TemplateColumn>
                                                                        <asp:TemplateColumn HeaderText="<%$Resources:cms.language, lblNguoigui%>">
                                                                            <ItemTemplate>
                                                                                <span class="linkGridForm">
                                                                                    <%#HPCBusinessLogic.UltilFunc.GetUserFullName(DataBinder.Eval(Container.DataItem, "Nguoitao"))%></span>
                                                                            </ItemTemplate>
                                                                            <HeaderStyle HorizontalAlign="Center" Width="10%"></HeaderStyle>
                                                                            <ItemStyle HorizontalAlign="Left" CssClass="GridBorderVerSolid"></ItemStyle>
                                                                        </asp:TemplateColumn>
                                                                        <asp:TemplateColumn HeaderText="<%$Resources:cms.language, lblThaotac%>">
                                                                            <HeaderStyle HorizontalAlign="Center" Width="12%"></HeaderStyle>
                                                                            <ItemStyle HorizontalAlign="Left" Width="12%" CssClass="GridBorderVerSolid"></ItemStyle>
                                                                            <ItemTemplate>
                                                                                <span class="linkGridForm">
                                                                                    <%#HPCComponents.Global.GetTrangThaiFrom_T_PhienbanQuangcao(DataBinder.Eval(Container.DataItem, "Trangthai"))%></span>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateColumn>
                                                                    </Columns>
                                                                </asp:DataGrid>
                                                            </div>
                                                        </ContentTemplate>
                                                    </cc2:TabPanel>
                                                </cc2:TabContainer>
                                                <div style="width: 60%; text-align: left; float: left; margin-top: 10px; padding: 10px 8px;
                                                    display: none" id="divbutton">
                                                    <asp:Panel ID="pnlbutton" runat="server">
                                                        <asp:LinkButton runat="server" ID="LinkSend" CausesValidation="false" OnClick="Send_Click"
                                                            OnClientClick=" return confirm('Bạn chắc chắn gửi?');" Text="<%$Resources:cms.language, lblGui%>"
                                                            CssClass="iconSend"> </asp:LinkButton>
                                                        <asp:LinkButton runat="server" ID="LinkDelete" CausesValidation="false" OnClick="Delete_Click"
                                                            OnClientClick=" return confirm('Bạn chắc chắn xóa?');" Text="<%$Resources:cms.language, lblXoa%>"
                                                            CssClass="iconDel"></asp:LinkButton>
                                                    </asp:Panel>
                                                </div>
                                                <div style="text-align: right; float: right" class="pageNav">
                                                    <cc1:CurrentPage runat="server" ID="CurrentPage" CssClass="pageNavTotal">
                                                    </cc1:CurrentPage>
                                                    <cc1:Pager runat="server" ID="pages" OnIndexChanged="pages_IndexChanged_Quangcao"></cc1:Pager>
                                                </div>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
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
</asp:Content>
