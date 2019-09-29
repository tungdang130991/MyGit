<%@ Page Language="C#" MasterPageFile="~/Masters/ITC.Master" AutoEventWireup="true"
    CodeBehind="List_XuLyCongViec.aspx.cs" Inherits="ToasoanTTXVN.DeTai.List_XuLyCongViec" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<%@ Register TagPrefix="cc1" Namespace="HPCComponents.UI.WebControls" Assembly="HPCComponents" %>
<%@ Import Namespace="HPCComponents" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <script language="Javascript" type="text/javascript">

        function SetTotal(_dxl, _hoanthanh) {
            document.getElementById("__tab_ctl00_MainContent_TabContainer1_tabpnltinXuly").innerHTML = "Đề tài đang xử lý (" + _dxl + ")";
            document.getElementById("__tab_ctl00_MainContent_TabContainer1_TabPanel1").innerHTML = "Đề tài đã hoàn thành (" + _hoanthanh + ")";
            //alert(_dxl);
        }
        function open_window_Scroll(url, top, height, left, width) {
            var tmp_Window = window.open(url, 'popup', 'location=no,directories=no,resizable=yes,status=yes,toolbar=no,menubar=no, width=695,height=' + screen.height + ',scrollbars=yes,top=5,left=150');
        }
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
            bol = get_check_value("ctl00_MainContent_TabContainer1_tabpnltinXuly_dgr_tintuc1_ctl01_chkAll");
            if (bol == true) {
                if (confirm("Bạn có chắc chắn muốn xóa?"))
                    return true;
                else return false;
            }
            else {
                alert("Bạn chưa chọn bản ghi nào!"); return false;
            }
        }
        function CheckConfirmDeletereturn() {
            var bol;
            bol = get_check_value("ctl00_MainContent_TabContainer1_TabPanel1_dgr_tintuc2_ctl01_chkAll");
            if (bol == true) {
                if (confirm("Bạn có chắc chắn muốn xóa?"))
                    return true;
                else return false;
            }
            else {
                alert("Bạn chưa chọn bản ghi nào!"); return false;
            }
        }
        function CheckConfirmGuiDuyet() {
            var bol;
            bol = get_check_value("ctl00_MainContent_TabContainer1_tabpnltinXuly_dgr_tintuc1_ctl01_chkAll");
            if (bol == true) {
                if (confirm("Bạn có chắc chắn muốn hoàn thành đề tài này?"))
                    return true;
                else return false;
            }
            else {
                alert("Bạn chưa chọn bản ghi nào!"); return false;
            }
        }
        function CheckConfirmGuiDuyetreturn() {
            var bol;
            bol = get_check_value("ctl00_MainContent_TabContainer1_TabPanel1_dgr_tintuc2_ctl01_chkAll");
            if (bol == true) {
                if (confirm("Bạn có chắc chắn muốn gửi TPPV?"))
                    return true;
                else return false;
            }
            else {
                alert("Bạn chưa chọn bản ghi nào!"); return false;
            }
        }
    </script>

    <table border="0" cellpadding="1" cellspacing="1" style="width: 100%">
        <tr>
            <td style="width: 25%; vertical-align: top;">
                <asp:Panel ID="plSearch" runat="server" Width="100%" CssClass="TitlePanel" GroupingText="Tìm kiếm"
                    BackColor="white" BorderStyle="NotSet">
                    <table border="0" cellpadding="1" cellspacing="1" style="width: 100%">
                        <tr>
                            <td style="width: 20%; text-align: right">
                                <span class="Titlelbl">Ngôn ngữ:</span>
                            </td>
                            <td style="width: 80%; text-align: left">
                                <asp:DropDownList ID="ddlLang" runat="server" Width="100%" CssClass="inputtext">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 20%; text-align: right">
                                <span class="Titlelbl">C.Mục:</span>
                            </td>
                            <td style="width: 80%; text-align: left">
                                <asp:DropDownList ID="cbo_chuyenmuc" runat="server" Width="100%" CssClass="inputtext"
                                    DataTextField="tenchuyenmuc" DataValueField="id" TabIndex="5">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 20%; text-align: right">
                                <span class="Titlelbl">Tiêu đề:</span>
                            </td>
                            <td style="width: 80%; text-align: left">
                                <asp:TextBox ID="txt_tieude" TabIndex="1" Width="95%" runat="server" CssClass="inputtext"
                                    Height="15"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                            <td>
                                <div style="clear: left; text-align: center;">
                                    <asp:LinkButton runat="server" ID="cmdSeek" CssClass="iconFind" OnClick="cmdSeek_Click"
                                        Text="Tìm kiếm">                                                
                                    </asp:LinkButton>
                                </div>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </td>
            <td valign="top">
                <asp:Panel ID="Panel1" runat="server" Width="100%" CssClass="TitlePanel" GroupingText="Danh sách đề tài"
                    BackColor="white" BorderStyle="NotSet">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <table width="100%" cellspacing="0" cellpadding="0" border="0">
                                <tr>
                                    <td align="left">
                                        <cc2:TabContainer ID="TabContainer1" runat="server" CssClass="ajax__tab_yuitabview-theme"
                                            AutoPostBack="true" ActiveTabIndex="0" OnActiveTabChanged="TabContainer1_ActiveTabChanged">
                                            <cc2:TabPanel HeaderText="Đề tài đang xử lý" ID="tabpnltinXuly" runat="server">
                                                <ContentTemplate>
                                                    <asp:DataGrid ID="dgr_tintuc1" runat="server" CssClass="Grid" Width="100%" BorderStyle="None"
                                                        AutoGenerateColumns="False" CellPadding="4" DataKeyField="ID" BackColor="White"
                                                        BorderWidth="1px" OnEditCommand="dgData_EditCommand" CellSpacing="0" OnItemDataBound="dgData_ItemDataBound">
                                                        <ItemStyle CssClass="GridItem"></ItemStyle>
                                                        <AlternatingItemStyle CssClass="GridAltItem" />
                                                        <HeaderStyle CssClass="GridHeader"></HeaderStyle>
                                                        <Columns>
                                                            <asp:TemplateColumn>
                                                                <HeaderStyle HorizontalAlign="Center" Width="5%"></HeaderStyle>
                                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                                <HeaderTemplate>
                                                                    <asp:CheckBox ID="chkAll" onclick="javascript:SelectAllCheckboxes(this);" runat="server"
                                                                        ToolTip="Chọn tất cả"></asp:CheckBox>
                                                                </HeaderTemplate>
                                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                                <ItemTemplate>
                                                                    <asp:CheckBox runat="server" Text='' ID='optSelect' AutoPostBack="False"></asp:CheckBox>
                                                                </ItemTemplate>
                                                            </asp:TemplateColumn>
                                                            <asp:BoundColumn DataField="ID" HeaderText="ID" Visible="False"></asp:BoundColumn>
                                                            <asp:TemplateColumn>
                                                                <HeaderStyle HorizontalAlign="Center" Width="35%"></HeaderStyle>
                                                                <HeaderTemplate>
                                                                    <asp:Literal runat="server" ID="Litera11l0" Text="Tên đề tài"></asp:Literal>
                                                                </HeaderTemplate>
                                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                <ItemTemplate>
                                                                    <asp:LinkButton CssClass="linkGridForm" Text='<%# DataBinder.Eval(Container.DataItem, "Title" )%>'
                                                                        runat="server" ID="linkTittle" CommandName="Edit" CommandArgument="Edit" ToolTip="Sửa tin bài"></asp:LinkButton>
                                                                    <%# HaveBaitralaiOfDetai(DataBinder.Eval(Container.DataItem, "ID").ToString())%>
                                                                </ItemTemplate>
                                                            </asp:TemplateColumn>
                                                            <asp:TemplateColumn HeaderText="Chuyên mục">
                                                                <HeaderStyle Width="13%" HorizontalAlign="Center"></HeaderStyle>
                                                                <ItemStyle Width="13%" HorizontalAlign="Left"></ItemStyle>
                                                                <ItemTemplate>
                                                                    <span class="linkGridForm">
                                                                        <%#HPCBusinessLogic.UltilFunc.GetTenChuyenMuc(Eval("CAT_ID"))%></span>
                                                                </ItemTemplate>
                                                            </asp:TemplateColumn>
                                                            <asp:TemplateColumn HeaderText="Người phân việc">
                                                                <HeaderStyle Width="15%" HorizontalAlign="Center"></HeaderStyle>
                                                                <ItemStyle Width="15%" HorizontalAlign="Left"></ItemStyle>
                                                                <ItemTemplate>
                                                                    <span class="linkGridForm">
                                                                        <%#HPCBusinessLogic.UltilFunc.GetUserFullName(Eval("User_Duyet"))%></span>
                                                                </ItemTemplate>
                                                            </asp:TemplateColumn>
                                                            <asp:TemplateColumn>
                                                                <HeaderStyle HorizontalAlign="Center" Width="10%" />
                                                                <ItemStyle HorizontalAlign="Center" />
                                                                <HeaderTemplate>
                                                                    <asp:Literal runat="server" ID="tieudestart" Text="Ngày bắt đầu"></asp:Literal>
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="ngaytart" CssClass="linkGridForm" runat="server" Text='<%#DataBinder.Eval(Container, "DataItem.Date_start") %>'>
                                                                    </asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateColumn>
                                                            <asp:TemplateColumn>
                                                                <HeaderStyle HorizontalAlign="Center" Width="10%" />
                                                                <ItemStyle HorizontalAlign="Center" />
                                                                <HeaderTemplate>
                                                                    <asp:Literal runat="server" ID="tieudeEnd" Text="Ngày kết thúc"></asp:Literal>
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="ngayEnd" CssClass="linkGridForm" runat="server" Text='<%#DataBinder.Eval(Container, "DataItem.Date_End") %>'>
                                                                    </asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateColumn>
                                                        </Columns>
                                                    </asp:DataGrid>
                                                </ContentTemplate>
                                            </cc2:TabPanel>
                                            <cc2:TabPanel HeaderText="Đề tài đã hoàn thành" ID="TabPanel1" runat="server">
                                                <ContentTemplate>
                                                    <asp:DataGrid ID="dgr_tintuc2" runat="server" CssClass="Grid" Width="100%" BorderStyle="None"
                                                        AutoGenerateColumns="False" CellPadding="4" DataKeyField="ID" BackColor="White"
                                                        BorderWidth="1px" OnItemDataBound="dgData_ItemDataBound1" OnEditCommand="dgData_EditCommand1">
                                                        <ItemStyle CssClass="GridItem"></ItemStyle>
                                                        <AlternatingItemStyle CssClass="GridAltItem" />
                                                        <HeaderStyle CssClass="GridHeader"></HeaderStyle>
                                                        <Columns>
                                                            <asp:TemplateColumn>
                                                                <HeaderStyle HorizontalAlign="Center" Width="5%"></HeaderStyle>
                                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                                <HeaderTemplate>
                                                                    <asp:CheckBox ID="chkAll" onclick="javascript:SelectAllCheckboxes(this);" runat="server"
                                                                        ToolTip="Chọn tất cả"></asp:CheckBox>
                                                                </HeaderTemplate>
                                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                                <ItemTemplate>
                                                                    <asp:CheckBox runat="server" Text='' ID='optSelect' AutoPostBack="False"></asp:CheckBox>
                                                                </ItemTemplate>
                                                            </asp:TemplateColumn>
                                                            <asp:BoundColumn DataField="ID" HeaderText="ID" Visible="False"></asp:BoundColumn>
                                                            <asp:TemplateColumn>
                                                                <HeaderStyle HorizontalAlign="Center" Width="25%"></HeaderStyle>
                                                                <HeaderTemplate>
                                                                    <asp:Literal runat="server" ID="Litera11l0" Text="Tên đề tài"></asp:Literal>
                                                                </HeaderTemplate>
                                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                <ItemTemplate>
                                                                    <asp:LinkButton CssClass="linkGridForm" Text='<%# DataBinder.Eval(Container.DataItem, "Title" )%>'
                                                                        runat="server" ID="linkTittle" CommandName="Edit" CommandArgument="Edit" ToolTip="Sửa tin bài"></asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateColumn>
                                                            <asp:TemplateColumn HeaderText="Chuyên mục">
                                                                <HeaderStyle Width="13%" HorizontalAlign="Center"></HeaderStyle>
                                                                <ItemStyle Width="13%" HorizontalAlign="Left"></ItemStyle>
                                                                <ItemTemplate>
                                                                    <%#HPCBusinessLogic.UltilFunc.GetTenChuyenMuc(Eval("CAT_ID"))%>
                                                                </ItemTemplate>
                                                            </asp:TemplateColumn>
                                                            <asp:TemplateColumn HeaderText="Người phân việc">
                                                                <HeaderStyle Width="20%" HorizontalAlign="Center"></HeaderStyle>
                                                                <ItemStyle Width="20%" HorizontalAlign="Left"></ItemStyle>
                                                                <ItemTemplate>
                                                                    <%#HPCBusinessLogic.UltilFunc.GetUserFullName(Eval("User_Duyet"))%>
                                                                </ItemTemplate>
                                                            </asp:TemplateColumn>
                                                            <asp:TemplateColumn>
                                                                <HeaderStyle HorizontalAlign="Center" Width="10%" />
                                                                <ItemStyle HorizontalAlign="Center" />
                                                                <HeaderTemplate>
                                                                    <asp:Literal runat="server" ID="tieudestart" Text="Ngày bắt đầu"></asp:Literal>
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="ngaytart" CssClass="linkGridForm" runat="server" Text='<%#DataBinder.Eval(Container, "DataItem.Date_start") %>'>
                                                                    </asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateColumn>
                                                            <asp:TemplateColumn>
                                                                <HeaderStyle HorizontalAlign="Center" Width="10%" />
                                                                <ItemStyle HorizontalAlign="Center" />
                                                                <HeaderTemplate>
                                                                    <asp:Literal runat="server" ID="tieudeEnd" Text="Ngày kết thúc"></asp:Literal>
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="ngayEnd" CssClass="linkGridForm" runat="server" Text='<%#DataBinder.Eval(Container, "DataItem.Date_End") %>'>
                                                                    </asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateColumn>
                                                            <asp:TemplateColumn>
                                                                <HeaderStyle HorizontalAlign="Center" Width="15%" />
                                                                <ItemStyle HorizontalAlign="Center" />
                                                                <HeaderTemplate>
                                                                    <asp:Literal runat="server" ID="End" Text="Ngày hoàn thành"></asp:Literal>
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="ngaytao" CssClass="linkGridForm" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Date_Duyet")!=System.DBNull.Value?Convert.ToDateTime(DataBinder.Eval(Container, "DataItem.Date_Duyet")).ToString("dd/MM/yyyy HH:mm:ss"):"" %>'>
                                                                    </asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateColumn>
                                                        </Columns>
                                                    </asp:DataGrid>
                                                </ContentTemplate>
                                            </cc2:TabPanel>
                                        </cc2:TabContainer>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <div style="float: left; text-align: left; padding: 10px 8px;">
                                            <asp:LinkButton runat="server" ID="Lbt_Send_Duyet" CausesValidation="false" OnClick="Send_Duyet"
                                                Text="Hoàn thành" CssClass="iconSend">
                                                
                                            </asp:LinkButton>
                                        </div>
                                        <div id="pagenav1" style="float: right; text-align: right" class="pageNav">
                                            <cc1:CurrentPage runat="server" ID="CurrentPage">
                                            </cc1:CurrentPage>
                                            <cc1:Pager runat="server" ID="pages" OnIndexChanged="pages_IndexChanged_baidangxuly"></cc1:Pager>
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="TabContainer1" EventName="ActiveTabChanged" />
                        </Triggers>
                    </asp:UpdatePanel>
                </asp:Panel>
            </td>
        </tr>
    </table>
</asp:Content>
