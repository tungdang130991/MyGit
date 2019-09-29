<%@ Page Language="C#" MasterPageFile="~/Masters/ITC.Master" AutoEventWireup="true"
    CodeBehind="List_DuyetDeTaiTPPV.aspx.cs" Inherits="ToasoanTTXVN.DeTai.List_DuyetDeTaiTPPV" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<%@ Register TagPrefix="cc1" Namespace="HPCComponents.UI.WebControls" Assembly="HPCComponents" %>
<%@ Import Namespace="HPCComponents" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <script language="Javascript" type="text/javascript">
        function SetInnerBaiChuaPhanCong(TotalRecords) {
            document.getElementById("__tab_ctl00_MainContent_TabContainer1_tabpnltinXuly").innerHTML = "Đề tài chưa phân công công việc (" + TotalRecords + ")";
        }
        function SetInnerBaiDaHoanThanh(TotalRecords) {
            document.getElementById("__tab_ctl00_MainContent_TabContainer1_TabPanel1").innerHTML = "Đề tài đã hoàn thành (" + TotalRecords + ")";
        }
        function SetInnerBaiDaPhanCong(TotalRecords) {
            document.getElementById("__tab_ctl00_MainContent_TabContainer1_TabPanel22").innerHTML = "Đề tài bị trả lại (" + TotalRecords + ")";
        }
        function SetInnerDanhSach(TotalRecords) {
            document.getElementById("__tab_ctl00_MainContent_TabContainer1_TabPanel2").innerHTML = "Danh sách công việc (" + TotalRecords + ")";
        }
        function open_window_Scroll(url, top, height, left, width) {
            var tmp_Window = window.open(url, 'popup', 'location=no,directories=no,resizable=yes,status=yes,toolbar=no,menubar=no, width=695,height=' + screen.height + ',scrollbars=yes,top=5,left=150');
        }

        function SetInnerDuyetDetaiTPPV(_RChuaPhanCong, _HoanThanh, _DaPhanCong, _DsCongViec) {
            document.getElementById("__tab_ctl00_MainContent_TabContainer1_tabpnltinXuly").innerHTML = "Đề tài chưa phân công công việc (" + _RChuaPhanCong + ")";
            document.getElementById("__tab_ctl00_MainContent_TabContainer1_TabPanel1").innerHTML = "Đề tài đã hoàn thành (" + _HoanThanh + ")";
            document.getElementById("__tab_ctl00_MainContent_TabContainer1_TabPanel22").innerHTML = "Đề tài bị trả lại (" + _DaPhanCong + ")";
            document.getElementById("__tab_ctl00_MainContent_TabContainer1_TabPanel2").innerHTML = "Danh sách công việc (" + _DsCongViec + ")";
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
        function CheckConfirmDelete1() {
            var bol;
            bol = get_check_value("ctl00_MainContent_TabContainer1_TabPanel22_dgr_tintuc3_ctl01_chkAll");
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
                if (confirm("Bạn có chắc chắn muốn gửi biên tập?"))
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
                if (confirm("Bạn có chắc chắn muốn gửi biên tập?"))
                    return true;
                else return false;
            }
            else {
                alert("Bạn chưa chọn bản ghi nào!"); return false;
            }
        }
        function CheckConfirmTraLai() {
            var bol;
            bol = get_check_value("ctl00_MainContent_TabContainer1_tabpnltinXuly_dgr_tintuc1_ctl01_chkAll");
            if (bol == true) {
                if (confirm("Bạn có chắc chắn muốn gửi trả lại ?"))
                    return true;
                else return false;
            }
            else {
                alert("Bạn chưa chọn bản ghi nào!"); return false;
            }
        }
        function CheckConfirmTEMP() {
            var bol;
            bol = get_check_value("ctl00_MainContent_TabContainer1_tabpnltinXuly_dgr_tintuc1_ctl01_chkAll");
            if (bol == true) {
                if (confirm("Bạn có chắc chắn muốn gửi trả lại ?"))
                    return true;
                else return false;
            }
            else {
                alert("Bạn chưa chọn bản ghi nào!"); return false;
            }
        }
        function CheckConfirmTraLaireturn() {
            var bol;
            bol = get_check_value("ctl00_MainContent_TabContainer1_TabPanel1_dgr_tintuc2_ctl01_chkAll");
            if (bol == true) {
                if (confirm("Bạn có chắc chắn muốn gửi trả lại ?"))
                    return true;
                else return false;
            }
            else {
                alert("Bạn chưa chọn bản ghi nào!"); return false;
            }
        }
        function CheckConfirmGuiDuyets() {
            var bol;
            bol = get_check_value("ctl00_MainContent_TabContainer1_TabPanel22_dgr_tintuc3_ctl01_chkAll");
            if (bol == true) {
                if (confirm("Bạn có chắc chắn muốn gửi duyệt ?"))
                    return true;
                else return false;
            }
            else {
                alert("Bạn chưa chọn bản ghi nào!"); return false;
            }
        }
        function CheckConfirmGuiTra() {
            var bol;
            bol = get_check_value("ctl00_MainContent_TabContainer1_TabPanel22_dgr_tintuc3_ctl01_chkAll");
            if (bol == true) {
                if (confirm("Bạn có chắc chắn muốn gửi trả lại ?"))
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
                                <asp:DropDownList ID="ddlLang" runat="server" Width="100%" CssClass="inputtext">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 20%; text-align: right">
                                <span class="Titlelbl">Chuyên mục:</span>
                            </td>
                            <td colspan="3" style="width: 80%; text-align: left">
                                <asp:DropDownList ID="cbo_chuyenmuc" runat="server" Width="100%" CssClass="inputtext"
                                    DataTextField="tenchuyenmuc" DataValueField="id" TabIndex="5">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 40%; text-align: right">
                                <span class="Titlelbl">Tên đề tài:</span>
                            </td>
                            <td style="width: 60%; text-align: left">
                                <asp:TextBox ID="txt_tieude" TabIndex="1" Width="200px" runat="server" CssClass="inputtext"
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
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </td>
            <td valign="top">
                <asp:Panel ID="Panel1" runat="server" Width="100%" CssClass="TitlePanel" GroupingText="Danh sách đề tài"
                    BackColor="white" BorderStyle="NotSet">
                    <table width="100%" cellspacing="0" cellpadding="0" border="0">
                        <tr>
                            <td align="left">
                                <cc2:TabContainer ID="TabContainer1" runat="server" CssClass="ajax__tab_yuitabview-theme"
                                    AutoPostBack="true" ActiveTabIndex="0" OnActiveTabChanged="TabContainer1_ActiveTabChanged">
                                    <cc2:TabPanel HeaderText="Đề tài chưa phân công công việc" ID="tabpnltinXuly" runat="server">
                                        <ContentTemplate>
                                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                <ContentTemplate>
                                                    <asp:DataGrid ID="dgr_tintuc1" runat="server" Width="100%" BorderStyle="None" AutoGenerateColumns="False"
                                                        CssClass="Grid" CellPadding="4" DataKeyField="Diea_ID" BackColor="White" BorderWidth="1px"
                                                        OnEditCommand="dgData_EditCommand" CellSpacing="0" OnItemDataBound="dgData_ItemDataBound">
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
                                                                        runat="server" ID="linkTittle" CommandName="Edit" CommandArgument="Edit" Enabled='<%# IsEnable(DataBinder.Eval(Container.DataItem, "Diea_Lock").ToString(), DataBinder.Eval(Container.DataItem, "User_Edit").ToString())%>'
                                                                        ToolTip="Sửa tin bài"></asp:LinkButton>
                                                                    <%# LockedUser(DataBinder.Eval(Container.DataItem, "Diea_Lock").ToString(), DataBinder.Eval(Container.DataItem, "User_Edit").ToString())%>
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
                                                                        <%# HPCBusinessLogic.UltilFunc.GetTenChuyenMuc(Eval("Cat_ID")) %></span>
                                                                </ItemTemplate>
                                                            </asp:TemplateColumn>
                                                            <asp:TemplateColumn>
                                                                <HeaderStyle HorizontalAlign="Center" Width="15%" />
                                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                                <HeaderTemplate>
                                                                    <asp:Literal runat="server" ID="Litera11l1" Text="Ngày gửi"></asp:Literal>
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="ngaytao" CssClass="linkGridForm" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Date_Duyet")!=System.DBNull.Value?Convert.ToDateTime(DataBinder.Eval(Container, "DataItem.Date_Duyet")).ToString("dd/MM/yyyy HH:mm:ss"):"" %>'>
                                                                    </asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateColumn>
                                                            <asp:TemplateColumn>
                                                                <HeaderStyle HorizontalAlign="Center" Width="15%" />
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
                                                                <HeaderStyle HorizontalAlign="Center" Width="15%" />
                                                                <HeaderTemplate>
                                                                    <asp:Literal runat="server" ID="Litera11l6" Text="Người gửi"></asp:Literal>
                                                                </HeaderTemplate>
                                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                                <ItemTemplate>
                                                                    <span class="linkGridForm">
                                                                        <%#HPCBusinessLogic.UltilFunc.GetUserFullName(Eval("User_Duyet"))%></span>
                                                                </ItemTemplate>
                                                            </asp:TemplateColumn>
                                                            <asp:TemplateColumn>
                                                                <HeaderTemplate>
                                                                    Lock
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Image Enabled='<%# IsEnable(DataBinder.Eval(Container.DataItem, "Diea_Lock").ToString(), DataBinder.Eval(Container.DataItem, "User_Edit").ToString())%>'
                                                                        ID="image12" runat="server" BorderStyle="none" ImageUrl='<%# IsImageLock_SendBack(DataBinder.Eval(Container.DataItem, "Diea_Lock").ToString())%>'
                                                                        ImageAlign="AbsMiddle" />
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" Width="7%"></HeaderStyle>
                                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
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
                                                    <div id="pagenav1" style="text-align: right" class="pageNav">
                                                        <cc1:CurrentPage runat="server" ID="CurrentPage1">
                                                        </cc1:CurrentPage>
                                                        <cc1:Pager runat="server" ID="pages" OnIndexChanged="pages_IndexChanged_baidangxuly"></cc1:Pager>
                                                    </div>
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="TabContainer1" EventName="ActiveTabChanged" />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                        </ContentTemplate>
                                    </cc2:TabPanel>
                                    <cc2:TabPanel HeaderText="Đề tài đã hoàn thành" ID="TabPanel1" runat="server">
                                        <ContentTemplate>
                                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                <ContentTemplate>
                                                    <asp:DataGrid ID="dgr_tintuc2" runat="server" Width="100%" BorderStyle="None" AutoGenerateColumns="False"
                                                        CssClass="Grid" CellPadding="4" DataKeyField="Diea_ID" BackColor="White" BorderWidth="1px"
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
                                                                        Enabled='<%# IsEnable(DataBinder.Eval(Container.DataItem, "Diea_Lock").ToString(), DataBinder.Eval(Container.DataItem, "User_Edit").ToString())%>'
                                                                        runat="server" ID="linkTittle" CommandName="Edit" CommandArgument="Edit" ToolTip="Sửa tin bài"></asp:LinkButton>
                                                                    <%# LockedUser(DataBinder.Eval(Container.DataItem, "Diea_Lock").ToString(), DataBinder.Eval(Container.DataItem, "User_Edit").ToString())%>
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
                                                                        <%# HPCBusinessLogic.UltilFunc.GetTenChuyenMuc(Eval("Cat_ID")) %></span>
                                                                </ItemTemplate>
                                                            </asp:TemplateColumn>
                                                            <asp:TemplateColumn>
                                                                <HeaderStyle HorizontalAlign="Center" Width="15%" />
                                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                                <HeaderTemplate>
                                                                    <asp:Literal runat="server" ID="Litera11l1" Text="Ngày gửi"></asp:Literal>
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="ngaytao" CssClass="linkGridForm" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Date_Duyet")!=System.DBNull.Value?Convert.ToDateTime(DataBinder.Eval(Container, "DataItem.Date_Duyet")).ToString("dd/MM/yyyy HH:mm:ss"):"" %>'>
                                                                    </asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateColumn>
                                                            <asp:TemplateColumn>
                                                                <HeaderStyle HorizontalAlign="Center" Width="15%" />
                                                                <HeaderTemplate>
                                                                    <asp:Literal runat="server" ID="Litera11l5" Text="Người gửi"></asp:Literal>
                                                                </HeaderTemplate>
                                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                                <ItemTemplate>
                                                                    <span class="linkGridForm">
                                                                        <%#HPCBusinessLogic.UltilFunc.GetUserFullName(Eval("User_Duyet"))%></span>
                                                                </ItemTemplate>
                                                            </asp:TemplateColumn>
                                                            <asp:TemplateColumn>
                                                                <HeaderTemplate>
                                                                    Lock
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Image Enabled='<%# IsEnable(DataBinder.Eval(Container.DataItem, "Diea_Lock").ToString(), DataBinder.Eval(Container.DataItem, "User_Edit").ToString())%>'
                                                                        ID="image12" runat="server" BorderStyle="none" ImageUrl='<%# IsImageLock_SendBack(DataBinder.Eval(Container.DataItem, "Diea_Lock").ToString())%>'
                                                                        ImageAlign="AbsMiddle" />
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" Width="7%"></HeaderStyle>
                                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
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
                                                    <div id="Div1" style="text-align: right" class="pageNav">
                                                        <cc1:CurrentPage runat="server" ID="CurrentPage2">
                                                        </cc1:CurrentPage>
                                                        <cc1:Pager runat="server" ID="pages2" OnIndexChanged="pages_IndexChanged_baitralai"></cc1:Pager>
                                                    </div>
                                                    <div style="clear: left; text-align: left;">
                                                        <asp:LinkButton runat="server" ID="LinkButton3" CausesValidation="false" OnClick="Send_DuyetBT"
                                                            Text="Gửi TP biên tập" CssClass="iconSend">                                                
                                                        </asp:LinkButton>
                                                        <asp:LinkButton runat="server" ID="LinkButton5" CausesValidation="false" OnClick="Send_TraLai"
                                                            Text="Trả lại" CssClass="iconBack">
                                                
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
                                    <cc2:TabPanel HeaderText="Đề tài bị trả lại" ID="TabPanel22" runat="server">
                                        <ContentTemplate>
                                            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                                <ContentTemplate>
                                                    <asp:DataGrid ID="dgr_tintuc3" runat="server" Width="100%" BorderStyle="None" AutoGenerateColumns="False"
                                                        CssClass="Grid" CellPadding="4" DataKeyField="Diea_ID" BackColor="White" BorderWidth="1px"
                                                        OnEditCommand="dgData_EditCommand3" CellSpacing="0" OnItemDataBound="dgData_ItemDataBound3">
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
                                                                        Enabled='<%# IsEnable(DataBinder.Eval(Container.DataItem, "Diea_Lock").ToString(), DataBinder.Eval(Container.DataItem, "User_Edit").ToString())%>'
                                                                        runat="server" ID="linkTittle" CommandName="Edit" CommandArgument="Edit" ToolTip="Sửa tin bài"></asp:LinkButton>
                                                                    <%# LockedUser(DataBinder.Eval(Container.DataItem, "Diea_Lock").ToString(), DataBinder.Eval(Container.DataItem, "User_Edit").ToString())%>
                                                                </ItemTemplate>
                                                            </asp:TemplateColumn>
                                                            <asp:TemplateColumn>
                                                                <HeaderStyle HorizontalAlign="Center" Width="15%" />
                                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                                <HeaderTemplate>
                                                                    <asp:Literal runat="server" ID="chuyenmuc" Text="Chuyên mục"></asp:Literal>
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <span class="linkGridForm">
                                                                        <%# HPCBusinessLogic.UltilFunc.GetTenChuyenMuc(Eval("Cat_ID")) %></span>
                                                                </ItemTemplate>
                                                            </asp:TemplateColumn>
                                                            <asp:TemplateColumn>
                                                                <HeaderStyle HorizontalAlign="Center" Width="15%" />
                                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                                <HeaderTemplate>
                                                                    <asp:Literal runat="server" ID="Litera11l1" Text="Ngày phân việc"></asp:Literal>
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="ngaytao" CssClass="linkGridForm" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Date_Duyet")!=System.DBNull.Value?Convert.ToDateTime(DataBinder.Eval(Container, "DataItem.Date_Duyet")).ToString("dd/MM/yyyy HH:mm:ss"):"" %>'>
                                                                    </asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateColumn>
                                                            <asp:TemplateColumn>
                                                                <HeaderStyle HorizontalAlign="Center" Width="15%" />
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
                                                                <HeaderStyle HorizontalAlign="Center" Width="15%" />
                                                                <HeaderTemplate>
                                                                    <asp:Literal runat="server" ID="Litera11l6" Text="Người phân việc"></asp:Literal>
                                                                </HeaderTemplate>
                                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                                <ItemTemplate>
                                                                    <span class="linkGridForm">
                                                                        <%#HPCBusinessLogic.UltilFunc.GetUserFullName(Eval("User_Duyet"))%></span>
                                                                </ItemTemplate>
                                                            </asp:TemplateColumn>
                                                            <asp:TemplateColumn>
                                                                <HeaderTemplate>
                                                                    Lock
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Image Enabled='<%# IsEnable(DataBinder.Eval(Container.DataItem, "Diea_Lock").ToString(), DataBinder.Eval(Container.DataItem, "User_Edit").ToString())%>'
                                                                        ID="image12" runat="server" BorderStyle="none" ImageUrl='<%# IsImageLock_SendBack(DataBinder.Eval(Container.DataItem, "Diea_Lock").ToString())%>'
                                                                        ImageAlign="AbsMiddle" />
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" Width="7%"></HeaderStyle>
                                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
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
                                                    <div id="Div2" style="text-align: right" class="pageNav">
                                                        <cc1:CurrentPage runat="server" ID="CurrentPage3">
                                                        </cc1:CurrentPage>
                                                        <cc1:Pager runat="server" ID="Pager3" OnIndexChanged="pages_IndexChanged_baidangdaPCCV"></cc1:Pager>
                                                    </div>
                                                    <div style="clear: left; text-align: left;">
                                                        <asp:LinkButton runat="server" ID="LinkButton7" CausesValidation="false" Text="Gửi TP Biên tập"
                                                            CssClass="iconSend" OnClick="Send_DuyetBT">
                                                
                                                        </asp:LinkButton>
                                                        <asp:LinkButton runat="server" ID="LinkButton8" CausesValidation="false" Text="Trả lại PV"
                                                            CssClass="iconBack" Visible="true" OnClick="Send_TraLai">
                                                
                                                        </asp:LinkButton>
                                                        <asp:LinkButton runat="server" ID="LinkButton9" CausesValidation="false" Text="<%$ Resources:Strings, T_NEWSCHUYENDE_TKTS_LIST %>"
                                                            CssClass="iconDel" OnClick="XoaTralai_Click">
                                              
                                                        </asp:LinkButton>
                                                    </div>
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="TabContainer1" EventName="ActiveTabChanged" />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                        </ContentTemplate>
                                    </cc2:TabPanel>
                                    <cc2:TabPanel HeaderText="Danh sách công việc" ID="TabPanel2" runat="server">
                                        <ContentTemplate>
                                            <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                                <ContentTemplate>
                                                    <asp:DataGrid ID="dgr_tintuc4" AlternatingItemStyle-BackColor="#F1F1F2" runat="server"
                                                        Width="100%" BorderStyle="None" AutoGenerateColumns="False" BorderColor="#D9D9D9"
                                                        CssClass="Grid" CellPadding="4" DataKeyField="ID" BackColor="White" BorderWidth="1px"
                                                        OnItemDataBound="dgData_ItemDataBound4" OnEditCommand="dgData_EditCommand4">
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
                                                                    <asp:Literal runat="server" ID="Litera11l0" Text="Tên công việc"></asp:Literal>
                                                                </HeaderTemplate>
                                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                <ItemTemplate>
                                                                    <span class="linkGridForm">
                                                                        <%# DataBinder.Eval(Container.DataItem, "Title" )%></span>
                                                                </ItemTemplate>
                                                            </asp:TemplateColumn>
                                                            <asp:TemplateColumn HeaderText="Chuyên mục">
                                                                <HeaderStyle Width="12%" HorizontalAlign="Center"></HeaderStyle>
                                                                <ItemStyle Width="12%" HorizontalAlign="Center"></ItemStyle>
                                                                <ItemTemplate>
                                                                    <span class="linkGridForm">
                                                                        <%#HPCBusinessLogic.UltilFunc.GetTenChuyenMuc(Eval("CAT_ID"))%></span>
                                                                </ItemTemplate>
                                                            </asp:TemplateColumn>
                                                            <asp:TemplateColumn HeaderText="Người thực hiện">
                                                                <HeaderStyle Width="15%" HorizontalAlign="Center"></HeaderStyle>
                                                                <ItemStyle Width="15%" HorizontalAlign="Center"></ItemStyle>
                                                                <ItemTemplate>
                                                                    <span class="linkGridForm">
                                                                        <%#HPCBusinessLogic.UltilFunc.GetUserFullName(Eval("User_NguoiNhan"))%></span>
                                                                </ItemTemplate>
                                                            </asp:TemplateColumn>
                                                            <asp:TemplateColumn>
                                                                <HeaderStyle HorizontalAlign="Center" Width="15%" />
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
                                                                <HeaderStyle HorizontalAlign="Center" Width="15%" />
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
                                                                <HeaderStyle HorizontalAlign="Center" Width="20%" />
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
                                                    <div id="Div3" style="text-align: right" class="pageNav">
                                                        <cc1:CurrentPage runat="server" ID="CurrentPage4">
                                                        </cc1:CurrentPage>
                                                        <cc1:Pager runat="server" ID="Pager4" OnIndexChanged="pages_IndexChanged_Congviec"></cc1:Pager>
                                                    </div>
                                                    <div style="clear: left; text-align: left;">
                                                        <asp:LinkButton runat="server" ID="bt_dalete2" CausesValidation="false" OnClick="Delete_Click1"
                                                            Text="Xóa" CssClass="iconDel">
                                                
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
