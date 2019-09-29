<%@ Page Language="C#" MasterPageFile="~/Masters/ITC.Master" AutoEventWireup="true"
    CodeBehind="List_Idiea.aspx.cs" Inherits="ToasoanTTXVN.DeTai.List_Idiea" %>

<%@ Register TagPrefix="anthem" Namespace="Anthem" Assembly="Anthem" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<%@ Register TagPrefix="cc1" Namespace="HPCComponents.UI.WebControls" Assembly="HPCComponents" %>
<%@ Import Namespace="HPCComponents" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <script language="Javascript" type="text/javascript">
        function SetInnerBaiTraLai(TotalRecords) {
            document.getElementById("__tab_ctl00_MainContent_TabContainer1_TabPanel1").innerHTML = "Đề tài trả lại (" + TotalRecords + ")";
        }
        function SetInnerBaiDangxyLy(TotalRecords, _return, _listCongViec) {
            //alert(TotalRecords);
            document.getElementById("__tab_ctl00_MainContent_TabContainer1_tabpnltinXuly").innerHTML = "Đề tài đang xử lý (" + TotalRecords + ")";
            document.getElementById("__tab_ctl00_MainContent_TabContainer1_TabPanel1").innerHTML = "Đề tài trả lại (" + _return + ")";
            document.getElementById("__tab_ctl00_MainContent_TabContainer1_TabPanel2").innerHTML = "Đề tài đã xử lý (" + _listCongViec + ")";

        }
        function fnShowMessage() {
            alert(" Invoke Javascript function from Server Side Code Behind ");
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
                if (confirm("Bạn có chắc chắn muốn gửi duyệt?"))
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
                if (confirm("Bạn có chắc chắn muốn gửi duyệt?"))
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
            <td style="width: 24%; vertical-align: top; padding-right: 4px;">
                <asp:Panel ID="plSearch" runat="server" Width="100%" CssClass="TitlePanel" GroupingText="Tìm kiếm"
                    BackColor="white" BorderStyle="NotSet">
                    <table border="0" cellpadding="1" cellspacing="1" style="width: 100%">
                        <tr>
                            <td style="width: 20%; text-align: right">
                                <span class="Titlelbl">Ngôn ngữ:</span>
                            </td>
                            <td style="width: 60%; text-align: left">
                                <anthem:DropDownList AutoCallBack="true" ID="ddlLang" runat="server" Width="206px"
                                    CssClass="inputtext" OnSelectedIndexChanged="ddlLang_SelectedIndexChanged">
                                </anthem:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 40%; text-align: right">
                                <span class="Titlelbl">Chuyên mục:</span>
                            </td>
                            <td style="width: 60%; text-align: left">
                                <anthem:DropDownList AutoCallBack="true" ID="cbo_chuyenmuc" runat="server" Width="206px"
                                    CssClass="inputtext" DataTextField="tenchuyenmuc" DataValueField="id" TabIndex="5">
                                </anthem:DropDownList>
                                <br />
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 40%; text-align: right">
                                <span class="Titlelbl">Tên đề tài:</span>
                            </td>
                            <td style="width: 60%; text-align: left">
                                <asp:TextBox ID="txt_tieude" TabIndex="1" Width="95%" runat="server" CssClass="inputtext"
                                    Height="15"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                            <td>
                                <asp:LinkButton runat="server" ID="cmdSeek" CssClass="iconFind" OnClick="cmdSeek_Click"
                                    Text="Tìm kiếm">                                                
                                </asp:LinkButton>
                                <asp:LinkButton runat="server" ID="LinkButton1" OnClick="cmdAdd_Click" CssClass="iconAdd"
                                    Text="Thêm mới">
                                               
                                </asp:LinkButton>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </td>
            <td style="width: 78%; vertical-align: top;">
                <asp:Panel ID="Panel1" runat="server" Width="100%" CssClass="TitlePanel" GroupingText="Danh sách đề tài"
                    BackColor="white" BorderStyle="NotSet">
                    <table width="100%" cellspacing="0" cellpadding="0" border="0">
                        <tr>
                            <td align="left">
                                <cc2:TabContainer ID="TabContainer1" runat="server" CssClass="ajax__tab_yuitabview-theme"
                                    AutoPostBack="true" ActiveTabIndex="0" OnActiveTabChanged="TabContainer1_ActiveTabChanged">
                                    <cc2:TabPanel HeaderText="Đề tài đang xử lý" ID="tabpnltinXuly" runat="server">
                                        <ContentTemplate>
                                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                <ContentTemplate>
                                                    <asp:DataGrid ID="dgr_tintuc1" runat="server" CssClass="Grid" Width="100%" BorderStyle="None"
                                                        AutoGenerateColumns="False" CellPadding="1" DataKeyField="Diea_ID" BorderWidth="1px"
                                                        OnEditCommand="dgData_EditCommand" CellSpacing="0" OnItemDataBound="dgData_ItemDataBound">
                                                        <ItemStyle CssClass="GridItem"></ItemStyle>
                                                        <AlternatingItemStyle CssClass="GridAltItem" />
                                                        <HeaderStyle CssClass="GridHeader"></HeaderStyle>
                                                        <Columns>
                                                            <asp:TemplateColumn>
                                                                <HeaderStyle HorizontalAlign="Center" Width="3%"></HeaderStyle>
                                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                                <HeaderTemplate>
                                                                    <asp:CheckBox ID="chkAll" onclick="javascript:SelectAllCheckboxes(this);" runat="server"
                                                                        ToolTip="Chọn tất cả"></asp:CheckBox>
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:CheckBox runat="server" Text='' ID='optSelect' AutoPostBack="False"></asp:CheckBox>
                                                                </ItemTemplate>
                                                            </asp:TemplateColumn>
                                                            <asp:BoundColumn DataField="Diea_ID" HeaderText="Diea_ID" Visible="False"></asp:BoundColumn>
                                                            <asp:TemplateColumn>
                                                                <HeaderStyle HorizontalAlign="Center" Width="40%"></HeaderStyle>
                                                                <ItemStyle HorizontalAlign="Left" Width="20%"></ItemStyle>
                                                                <HeaderTemplate>
                                                                    <asp:Literal runat="server" ID="Litera11l0" Text="Tên đề tài"></asp:Literal>
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:LinkButton CssClass="linkGridForm" Text='<%# DataBinder.Eval(Container.DataItem, "Title" )%>'
                                                                        runat="server" ID="linkTittle" CommandName="Edit" CommandArgument="Edit" ToolTip="Sửa tin bài"></asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateColumn>
                                                            <asp:TemplateColumn>
                                                                <HeaderStyle HorizontalAlign="Center" Width="12%" />
                                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                                <HeaderTemplate>
                                                                    <asp:Literal runat="server" ID="chuyenmuc" Text="Chuyên mục"></asp:Literal>
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <span class="linkGridForm">
                                                                        <%#HPCBusinessLogic.UltilFunc.GetTenChuyenMuc(Eval("CAT_ID"))%></span>
                                                                </ItemTemplate>
                                                            </asp:TemplateColumn>
                                                            <asp:TemplateColumn>
                                                                <HeaderStyle HorizontalAlign="Center" Width="12%" />
                                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                                <HeaderTemplate>
                                                                    <asp:Literal runat="server" ID="Litera11l1" Text="Ngày đề xuất"></asp:Literal>
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="ngaytao" CssClass="linkGridForm" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Date_Created")!=System.DBNull.Value?Convert.ToDateTime(DataBinder.Eval(Container, "DataItem.Date_Created")).ToString("dd/MM/yyyy HH:mm:ss"):"" %>'>
                                                                    </asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateColumn>
                                                            <asp:TemplateColumn>
                                                                <HeaderStyle HorizontalAlign="Center" Width="12%" />
                                                                <HeaderTemplate>
                                                                    <asp:Literal runat="server" ID="Litera11l2" Text="Người đề xuất"></asp:Literal>
                                                                </HeaderTemplate>
                                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                                <ItemTemplate>
                                                                    <span class="linkGridForm">
                                                                        <%#HPCBusinessLogic.UltilFunc.GetUserFullName(Eval("User_Created"))%></span>
                                                                </ItemTemplate>
                                                            </asp:TemplateColumn>
                                                            <asp:TemplateColumn>
                                                                <HeaderStyle HorizontalAlign="center" Width="5%"></HeaderStyle>
                                                                <ItemStyle HorizontalAlign="center" Width="5%"></ItemStyle>
                                                                <HeaderTemplate>
                                                                    Xem
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <a href="Javascript:open_window_Scroll('<%=Global.ApplicationPath%>/DeTai/ViewNews1.aspx?Menu_ID=<%=Page.Request.QueryString["Menu_ID"]%>&ID=<%# DataBinder.Eval(Container.DataItem, "Diea_ID") %>',50,500,100,800);" />
                                                                    <img src='<%=Global.ApplicationPath%>/Dungchung/Images/view.gif' border="0" border="0"
                                                                        onmouseover="(window.status=''); return true" style="cursor: hand" title="xem">
                                                                </ItemTemplate>
                                                            </asp:TemplateColumn>
                                                        </Columns>
                                                    </asp:DataGrid>
                                                    <div style="text-align: right" id="pagenav1" class="pageNav">
                                                        <cc1:CurrentPage runat="server" ID="CurrentPage1">
                                                        </cc1:CurrentPage>
                                                        <cc1:Pager runat="server" ID="pages" OnIndexChanged="pages_IndexChanged_baidangxuly"></cc1:Pager>
                                                    </div>
                                                    <div style="clear: left; text-align: left;">
                                                        <asp:LinkButton runat="server" ID="Lbt_Send_Duyet" CausesValidation="false" OnClick="Send_Duyet"
                                                            Text="Gửi duyệt" CssClass="iconSend">
                                                
                                                        </asp:LinkButton>
                                                        <asp:LinkButton runat="server" ID="Lbt_Delete" CausesValidation="false" OnClick="Delete_Click"
                                                            Text="<%$ Resources:Strings, T_NEWSCHUYENDE_TKTS_LIST %>" CssClass="iconDel">
                                              
                                                        </asp:LinkButton>
                                                    </div>
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="TabContainer1" EventName="ActiveTabChanged" />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                        </ContentTemplate>
                                    </cc2:TabPanel>
                                    <cc2:TabPanel ID="TabPanel1" runat="server">
                                        <ContentTemplate>
                                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                <ContentTemplate>
                                                    <asp:DataGrid ID="dgr_tintuc2" runat="server" CssClass="Grid" Width="100%" BorderStyle="None"
                                                        AutoGenerateColumns="False" CellPadding="1" DataKeyField="Diea_ID" B BorderWidth="1px"
                                                        OnItemDataBound="dgData_ItemDataBound1" OnEditCommand="dgData_EditCommand1">
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
                                                            <asp:BoundColumn DataField="Diea_ID" HeaderText="Diea_ID" Visible="False"></asp:BoundColumn>
                                                            <asp:TemplateColumn>
                                                                <HeaderStyle HorizontalAlign="Center" Width="35%"></HeaderStyle>
                                                                <HeaderTemplate>
                                                                    <asp:Literal runat="server" ID="Litera11l0" Text="Tên đề tài"></asp:Literal>
                                                                </HeaderTemplate>
                                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                <ItemTemplate>
                                                                    <asp:LinkButton CssClass="linkGridForm" Text='<%# DataBinder.Eval(Container.DataItem, "Title" )%>'
                                                                        runat="server" ID="linkTittle" CommandName="Edit" CommandArgument="Edit" ToolTip="Sửa tin bài"></asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateColumn>
                                                            <asp:TemplateColumn>
                                                                <HeaderStyle HorizontalAlign="Center" Width="12%" />
                                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                                <HeaderTemplate>
                                                                    <asp:Literal runat="server" ID="chuyenmuc" Text="Chuyên mục"></asp:Literal>
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <span class="linkGridForm">
                                                                        <%#HPCBusinessLogic.UltilFunc.GetTenChuyenMuc(Eval("CAT_ID"))%></span>
                                                                </ItemTemplate>
                                                            </asp:TemplateColumn>
                                                            <asp:TemplateColumn>
                                                                <HeaderStyle HorizontalAlign="Center" Width="20%" />
                                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                                <HeaderTemplate>
                                                                    <asp:Literal runat="server" ID="Litera11l1" Text="Ngày trả lại"></asp:Literal>
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="ngaytao" CssClass="linkGridForm" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Date_Duyet")!=System.DBNull.Value?Convert.ToDateTime(DataBinder.Eval(Container, "DataItem.Date_Duyet")).ToString("dd/MM/yyyy HH:mm:ss"):"" %>'>
                                                                    </asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateColumn>
                                                            <asp:TemplateColumn>
                                                                <HeaderStyle HorizontalAlign="Center" Width="15%" />
                                                                <HeaderTemplate>
                                                                    <asp:Literal runat="server" ID="Litera11l4" Text="Người đề xuất"></asp:Literal>
                                                                </HeaderTemplate>
                                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                                <ItemTemplate>
                                                                    <span class="linkGridForm">
                                                                        <%#HPCBusinessLogic.UltilFunc.GetUserFullName(Eval("User_Created"))%></span>
                                                                </ItemTemplate>
                                                            </asp:TemplateColumn>
                                                            <asp:TemplateColumn>
                                                                <HeaderStyle HorizontalAlign="Center" Width="15%" />
                                                                <HeaderTemplate>
                                                                    <asp:Literal runat="server" ID="Litera11l5" Text="Người trả"></asp:Literal>
                                                                </HeaderTemplate>
                                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                                <ItemTemplate>
                                                                    <span class="linkGridForm">
                                                                        <%#HPCBusinessLogic.UltilFunc.GetUserFullName(Eval("User_Duyet"))%></span>
                                                                </ItemTemplate>
                                                            </asp:TemplateColumn>
                                                            <asp:TemplateColumn>
                                                                <HeaderStyle HorizontalAlign="center" Width="5%"></HeaderStyle>
                                                                <ItemStyle HorizontalAlign="center" Width="5%"></ItemStyle>
                                                                <HeaderTemplate>
                                                                    Xem
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <a href="Javascript:open_window_Scroll('<%=Global.ApplicationPath%>/DeTai/ViewNews1.aspx?Menu_ID=<%=Page.Request.QueryString["Menu_ID"]%>&ID=<%# DataBinder.Eval(Container.DataItem, "Diea_ID") %>',50,500,100,800);" />
                                                                    <img src='<%=Global.ApplicationPath%>/Dungchung/Images/view.gif' border="0" alt="<%# DataBinder.Eval(Container.DataItem, "Title") %>"
                                                                        border="0" onmouseover="(window.status=''); return true" style="cursor: hand"
                                                                        title="xem">
                                                                </ItemTemplate>
                                                            </asp:TemplateColumn>
                                                        </Columns>
                                                    </asp:DataGrid>
                                                    <div id="Div1" style="text-align: right" class="pageNav">
                                                        <cc1:CurrentPage runat="server" ID="CurrentPage2">
                                                        </cc1:CurrentPage>
                                                        <cc1:Pager runat="server" ID="pages2" OnIndexChanged="pages_IndexChanged_baitralai"></cc1:Pager>
                                                    </div>
                                                    <div style="clear: left; text-align: left;">
                                                        <asp:LinkButton runat="server" ID="LinkButton3" CausesValidation="false" OnClick="Send_Duyet"
                                                            Text="Gửi duyệt" CssClass="iconSend">                                                
                                                        </asp:LinkButton>
                                                        <asp:LinkButton runat="server" ID="LinkButton4" CausesValidation="false" OnClick="Delete_Click"
                                                            Text="<%$ Resources:Strings, T_NEWSCHUYENDE_TKTS_LIST %>" CssClass="iconDel">
                                              
                                                        </asp:LinkButton>
                                                    </div>
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="TabContainer1" EventName="ActiveTabChanged" />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                        </ContentTemplate>
                                    </cc2:TabPanel>
                                    <cc2:TabPanel HeaderText="Đề tài đã xử lý" ID="TabPanel2" runat="server">
                                        <ContentTemplate>
                                            <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                                <ContentTemplate>
                                                    <asp:DataGrid ID="dgr_ListCongViec" runat="server" CssClass="Grid" Width="100%" BorderStyle="None"
                                                        AutoGenerateColumns="False" CellPadding="1" DataKeyField="ID" BorderWidth="1px"
                                                        OnItemDataBound="dgr_ListCongViec_ItemDataBound" OnEditCommand="dgr_ListCongViec_EditCommand">
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
                                                            <asp:BoundColumn DataField="Diea_ID" HeaderText="Diea_ID" Visible="False"></asp:BoundColumn>
                                                            <asp:TemplateColumn>
                                                                <HeaderStyle HorizontalAlign="Center" Width="35%"></HeaderStyle>
                                                                <HeaderTemplate>
                                                                    <asp:Literal runat="server" ID="Litera11l0" Text="Tên đề tài"></asp:Literal>
                                                                </HeaderTemplate>
                                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                <ItemTemplate>
                                                                    <span class="linkGridForm">
                                                                        <%# DataBinder.Eval(Container.DataItem, "Title" )%></span>
                                                                </ItemTemplate>
                                                            </asp:TemplateColumn>
                                                            <asp:TemplateColumn>
                                                                <HeaderStyle HorizontalAlign="Center" Width="20%" />
                                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                                <HeaderTemplate>
                                                                    <asp:Literal runat="server" ID="chuyenmuc" Text="Chuyên mục"></asp:Literal>
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <span class="linkGridForm">
                                                                        <%#HPCBusinessLogic.UltilFunc.GetTenChuyenMuc(Eval("CAT_ID"))%></span>
                                                                </ItemTemplate>
                                                            </asp:TemplateColumn>
                                                            <asp:TemplateColumn>
                                                                <HeaderStyle HorizontalAlign="Center" Width="20%" />
                                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                                <HeaderTemplate>
                                                                    <asp:Literal runat="server" ID="Litera11l1" Text="Ngày đề xuất"></asp:Literal>
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="ngaytao" CssClass="linkGridForm" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Date_Created")!=System.DBNull.Value?Convert.ToDateTime(DataBinder.Eval(Container, "DataItem.Date_Created")).ToString("dd/MM/yyyy HH:mm:ss"):"" %>'>
                                                                    </asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateColumn>
                                                            <asp:TemplateColumn>
                                                                <HeaderStyle HorizontalAlign="Center" Width="20%" />
                                                                <HeaderTemplate>
                                                                    <asp:Literal runat="server" ID="Litera11l2" Text="Người đề xuất"></asp:Literal>
                                                                </HeaderTemplate>
                                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                                <ItemTemplate>
                                                                    <span class="linkGridForm">
                                                                        <%#HPCBusinessLogic.UltilFunc.GetUserFullName(Eval("User_Created"))%></span>
                                                                </ItemTemplate>
                                                            </asp:TemplateColumn>
                                                            <asp:TemplateColumn>
                                                                <HeaderStyle HorizontalAlign="center" Width="5%"></HeaderStyle>
                                                                <ItemStyle HorizontalAlign="center" Width="5%"></ItemStyle>
                                                                <HeaderTemplate>
                                                                    Xem
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <a href="Javascript:open_window_Scroll('<%=Global.ApplicationPath%>/DeTai/ViewNews1.aspx?Menu_ID=<%=Page.Request.QueryString["Menu_ID"]%>&ID=<%# DataBinder.Eval(Container.DataItem, "Diea_ID") %>',50,500,100,800);" />
                                                                    <img src='<%=Global.ApplicationPath%>/Dungchung/Images/view.gif' border="0" alt="<%# DataBinder.Eval(Container.DataItem, "Title") %>"
                                                                        border="0" onmouseover="(window.status=''); return true" style="cursor: hand"
                                                                        title="xem">
                                                                </ItemTemplate>
                                                            </asp:TemplateColumn>
                                                        </Columns>
                                                    </asp:DataGrid>
                                                    <div id="Div3" style="text-align: right" class="pageNav">
                                                        <cc1:CurrentPage runat="server" ID="CurrentPage4">
                                                        </cc1:CurrentPage>
                                                        <cc1:Pager runat="server" ID="Pager4" OnIndexChanged="pages_IndexChanged_Congviec"></cc1:Pager>
                                                    </div>
                                                    <div style="clear: left; text-align: left;">
                                                        <asp:LinkButton runat="server" ID="bt_dalete2" CausesValidation="false" Text="Xóa"
                                                            CssClass="iconDel" OnClick="DeleteCongViec_Click">
                                                
                                                        </asp:LinkButton>
                                                    </div>
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="TabContainer1" EventName="ActiveTabChanged" />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                        </ContentTemplate>
                                    </cc2:TabPanel>
                                </cc2:TabContainer>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </td>
        </tr>
    </table>
</asp:Content>
