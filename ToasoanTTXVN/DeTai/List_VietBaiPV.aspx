<%@ Page Language="C#" MasterPageFile="~/Masters/ITC.Master" AutoEventWireup="true"
    CodeBehind="List_VietBaiPV.aspx.cs" Inherits="ToasoanTTXVN.DeTai.List_VietBaiPV" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<%@ Register TagPrefix="cc1" Namespace="HPCComponents.UI.WebControls" Assembly="HPCComponents" %>
<%@ Import Namespace="HPCComponents" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <script type="text/javascript" language="javascript">
        function check_num(obj, length, e) {
            var key = window.event ? e.keyCode : e.which;
            var len = obj.value.length + 1;
            if (length <= 3) begin = 48; else begin = 45;
            if (key >= begin && key <= 57 && len <= length || (key == 8 || key == 0)) {
            }
            else return false;
        }
        function SetInnerBaiTraLai(TotalRecords) {
            document.getElementById("__tab_ctl00_MainContent_TabContainer1_TabPanel1").innerHTML = "Bài bị trả lại (" + TotalRecords + ")";
        }
        function SetInnerBaiDangxyLy(TotalRecords) {
            document.getElementById("__tab_ctl00_MainContent_TabContainer1_tabpnltinXuly").innerHTML = "Bài đang xử lý (" + TotalRecords + ")";
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
        function CheckConfirmGuiThuKyToaSoanDangXyLy() {
            var bol;
            bol = get_check_value("ctl00_MainContent_TabContainer1_tabpnltinXuly_dgr_tintuc1_ctl01_chkAll");
            if (bol == true) {
                if (confirm("Bạn có chắc chắn muốn gửi TP phóng viên?"))
                    return true;
                else return false;
            }
            else {
                alert("Bạn chưa chọn bản ghi nào!"); return false;
            }
        }
        function CheckConfirmGuiThuKyToaSoanReturn() {
            var bol;
            bol = get_check_value("ctl00_MainContent_TabContainer1_TabPanel1_dgr_tintuc2_ctl01_chkAll");
            if (bol == true) {
                if (confirm("Bạn có chắc chắn muốn gửi TP phóng viên?"))
                    return true;
                else return false;
            }
            else {
                alert("Bạn chưa chọn bản ghi nào!"); return false;
            }
        }
        function CheckConfirmDeleteReturn() {
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
    </script>

    <div style="width: 100%; padding-bottom: 20px">
        <table border="0" cellpadding="1" cellspacing="1" style="width: 100%">
            <tr>
                <td style="text-align: right">
                    <span class="Titlelbl">Ngôn ngữ:</span>
                </td>
                <td style="width: 20%; text-align: right">
                    <asp:DropDownList ID="cboNgonNgu" runat="server" Width="100%" CssClass="inputtext"
                        TabIndex="1">
                    </asp:DropDownList>
                </td>
                <td style="text-align: right">
                    <span class="Titlelbl">Chuyên mục:</span>
                </td>
                <td style="width: 20%; text-align: right">
                    <asp:DropDownList ID="cbo_chuyenmuc" runat="server" Width="100%" CssClass="inputtext"
                        DataTextField="tenchuyenmuc" DataValueField="id" TabIndex="5">
                    </asp:DropDownList>
                </td>
                <td style="text-align: right">
                    <span class="Titlelbl">Tiêu đề:</span>
                </td>
                <td style="width: 20%; text-align: right">
                    <asp:TextBox ID="txt_tieude" TabIndex="1" Width="95%" runat="server" CssClass="inputtext"
                        Height="15"></asp:TextBox>
                </td>
                <td style="width: 20%; text-align: left;">
                    <asp:LinkButton runat="server" ID="cmdSeek" CssClass="iconFind" OnClick="cmdSeek_Click"
                        Text="Tìm kiếm">                                               
                    </asp:LinkButton>
                    <asp:LinkButton runat="server" ID="LinkButtonVietBai" OnClick="cmdAdd_Click" CssClass="iconAdd"
                        Text="Viết bài">                                               
                    </asp:LinkButton>
                </td>
            </tr>
        </table>
    </div>
    <asp:Panel runat="server" ID="pnListVietBai" BackColor="white">
        <table cellpadding="0" cellspacing="0" border="0" width="100%">
            <tr>
                <td style="width: 45%; vertical-align: top; padding-right: 4px;">
                    <asp:Panel ID="plSearch" runat="server" Width="100%" GroupingText="Nội dung và yêu cầu đề tài"
                        BackColor="white" BorderStyle="NotSet">
                        <table id="Table1" cellspacing="0" cellpadding="2" width="100%" border="0">
                            <tr>
                                <td style="text-align: right; width: 18%">
                                    <span class="Titlelbl">Chuyên mục:</span>
                                </td>
                                <td style="width: 82%; text-align: left">
                                    <asp:Label ID="lblNameCM" runat="server" Width="70%"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: right; width: 18%">
                                    <span class="Titlelbl">Tên đề tài:</span>
                                </td>
                                <td style="width: 82%; text-align: left">
                                    <asp:Label ID="lbtieude" runat="server" Width="70%"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: right; width: 18%">
                                    <span class="Titlelbl">Loại:</span>
                                </td>
                                <td style="width: 82%; text-align: left">
                                    <asp:Label runat="server" ID="ltr_loaibai1" Width="80%"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: right; width: 18%">
                                    <span class="Titlelbl">Ngày hoàn thành:</span>
                                </td>
                                <td style="width: 82%; text-align: left">
                                    <asp:Label ID="T_AllotmentNgayHT" runat="server" Width="80%"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: right; width: 18%; vertical-align: top">
                                    <span class="Titlelbl">Nội dung đề tài:</span>
                                </td>
                                <td style="width: 82%; text-align: left">
                                    <asp:Literal runat="server" ID="ltrYeuCau"></asp:Literal>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </td>
                <td valign="top">
                    <asp:Panel ID="Panel1" runat="server" Width="100%" GroupingText="Danh sách bài đang xử lý"
                        BackColor="white" BorderStyle="NotSet">
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <table width="100%" cellspacing="0" cellpadding="0" border="0">
                                    <tr>
                                        <td align="left">
                                            <cc2:TabContainer ID="TabContainer1" runat="server" CssClass="ajax__tab_yuitabview-theme"
                                                AutoPostBack="true" ActiveTabIndex="0" OnActiveTabChanged="TabContainer1_ActiveTabChanged">
                                                <cc2:TabPanel HeaderText="Bài đang xử lý" ID="tabpnltinXuly" runat="server">
                                                    <ContentTemplate>
                                                        <asp:DataGrid ID="dgr_tintuc1" runat="server" CssClass="Grid" Width="100%" BorderStyle="None"
                                                            AutoGenerateColumns="False" CellPadding="4" DataKeyField="Ma_Tinbai" BackColor="White"
                                                            BorderWidth="1px" OnEditCommand="dgData_EditCommand" CellSpacing="0" OnItemDataBound="dgData_ItemDataBound">
                                                            <ItemStyle CssClass="GridItem"></ItemStyle>
                                                            <AlternatingItemStyle CssClass="GridAltItem" />
                                                            <HeaderStyle CssClass="GridHeader"></HeaderStyle>
                                                            <Columns>
                                                                <%--<asp:TemplateColumn>
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
                                                                </asp:TemplateColumn>--%>
                                                                <asp:BoundColumn DataField="Ma_Tinbai" HeaderText="Ma_Tinbai" Visible="False"></asp:BoundColumn>
                                                                <asp:TemplateColumn>
                                                                    <HeaderStyle HorizontalAlign="Center" Width="45%"></HeaderStyle>
                                                                    <HeaderTemplate>
                                                                        <asp:Literal runat="server" ID="Litera11l0" Text="Tiêu đề"></asp:Literal>
                                                                    </HeaderTemplate>
                                                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                    <ItemTemplate>
                                                                        <a href="Javascript:open_window_Scroll('<%=Global.ApplicationPath%>/Quytrinh/ViewPhienBanTinBai.aspx?Menu_ID=<%=Page.Request.QueryString["Menu_ID"]%>&ID=<%# DataBinder.Eval(Container.DataItem, "Ma_Tinbai") %>',50,500,100,800);">
                                                                            <%# DataBinder.Eval(Container.DataItem, "Tieude" )%></a>
                                                                    </ItemTemplate>
                                                                </asp:TemplateColumn>
                                                                <asp:TemplateColumn HeaderText="Chuyên mục">
                                                                    <HeaderStyle Width="16%" HorizontalAlign="Center"></HeaderStyle>
                                                                    <ItemStyle Width="16%" HorizontalAlign="Left"></ItemStyle>
                                                                    <ItemTemplate>
                                                                        <span class="linkGridForm">
                                                                            <%#HPCBusinessLogic.UltilFunc.GetTenChuyenMuc(Eval("Ma_Chuyenmuc"))%></span>
                                                                    </ItemTemplate>
                                                                </asp:TemplateColumn>
                                                                <asp:TemplateColumn>
                                                                    <HeaderStyle HorizontalAlign="Center" Width="12%" />
                                                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                                    <HeaderTemplate>
                                                                        <asp:Literal runat="server" ID="Litera11l1" Text="Ngày tạo"></asp:Literal>
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="ngaytao" CssClass="linkGridForm" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Ngaytao")!=System.DBNull.Value?Convert.ToDateTime(DataBinder.Eval(Container, "DataItem.Ngaytao")).ToString("dd/MM/yyyy HH:mm:ss"):"" %>'>
                                                                        </asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateColumn>
                                                                <asp:TemplateColumn>
                                                                    <HeaderStyle HorizontalAlign="Center" Width="20%" />
                                                                    <HeaderTemplate>
                                                                        <asp:Literal runat="server" ID="Litera11l6" Text="Người tạo"></asp:Literal>
                                                                    </HeaderTemplate>
                                                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                    <ItemTemplate>
                                                                        <span class="linkGridForm">
                                                                            <%# HPCBusinessLogic.UltilFunc.GetUserFullName(Eval("Ma_Nguoitao"))%></span>
                                                                    </ItemTemplate>
                                                                </asp:TemplateColumn>
                                                                <asp:TemplateColumn>
                                                                    <HeaderStyle HorizontalAlign="center" Width="5%"></HeaderStyle>
                                                                    <ItemStyle HorizontalAlign="center" Width="5%"></ItemStyle>
                                                                    <HeaderTemplate>
                                                                        Xem
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <a href="Javascript:open_window_Scroll('<%=Global.ApplicationPath%>/Quytrinh/ViewPhienBanTinBai.aspx?Menu_ID=<%=Page.Request.QueryString["Menu_ID"]%>&ID=<%# DataBinder.Eval(Container.DataItem, "Ma_Tinbai") %>',50,500,100,800);" />
                                                                        <img src='<%=Global.ApplicationPath%>/Dungchung/Images/view.gif' border="0" onmouseover="(window.status=''); return true"
                                                                            style="cursor: hand; border: 0">
                                                                    </ItemTemplate>
                                                                </asp:TemplateColumn>
                                                            </Columns>
                                                        </asp:DataGrid>
                                                    </ContentTemplate>
                                                </cc2:TabPanel>
                                                <cc2:TabPanel HeaderText="Bài bị trả lại" ID="TabPanel1" runat="server">
                                                    <ContentTemplate>
                                                        <asp:DataGrid ID="dgr_tintuc2" runat="server" CssClass="Grid" Width="100%" BorderStyle="None"
                                                            AutoGenerateColumns="False" CellPadding="4" DataKeyField="Diea_ID" BackColor="White"
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
                                                                <asp:BoundColumn DataField="Diea_ID" HeaderText="Diea_ID" Visible="False"></asp:BoundColumn>
                                                                <asp:TemplateColumn>
                                                                    <HeaderStyle HorizontalAlign="Center" Width="45%"></HeaderStyle>
                                                                    <HeaderTemplate>
                                                                        <asp:Literal runat="server" ID="Litera11l0" Text="Tiêu đề"></asp:Literal>
                                                                    </HeaderTemplate>
                                                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton CssClass="linkGridForm" Text='<%# DataBinder.Eval(Container.DataItem, "Title" )%>'
                                                                            runat="server" ID="linkTittle" CommandName="Edit" CommandArgument="Edit" ToolTip="Phân công công việc"></asp:LinkButton>
                                                                    </ItemTemplate>
                                                                </asp:TemplateColumn>
                                                                <asp:TemplateColumn HeaderText="Chuyên mục">
                                                                    <HeaderStyle Width="16%" HorizontalAlign="Center"></HeaderStyle>
                                                                    <ItemStyle Width="16%" HorizontalAlign="Left"></ItemStyle>
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
                                                                    <HeaderStyle HorizontalAlign="Center" Width="20%" />
                                                                    <HeaderTemplate>
                                                                        <asp:Literal runat="server" ID="Litera11l5" Text="Người trả lại"></asp:Literal>
                                                                    </HeaderTemplate>
                                                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                    <ItemTemplate>
                                                                        <span class="linkGridForm">
                                                                            <%# HPCBusinessLogic.UltilFunc.GetUserFullName(Eval("User_Duyet")) %></span>
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
                                                                        <img src='../images/view.gif' border="0" alt="<%# DataBinder.Eval(Container.DataItem, "Title") %>"
                                                                            border="0" onmouseover="(window.status=''); return true" style="cursor: hand"
                                                                            title="xem">
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
                                        <td style="text-align: left; width: 100%">
                                            <div style="float: left; text-align: left; width: 30%; padding: 10px 8px;">
                                                <asp:LinkButton runat="server" ID="LinkButton3" CausesValidation="false" OnClick="Send_TKTS"
                                                    Visible="false" Text="Gửi TP Phóng viên" CssClass="iconSend">                                                
                                                </asp:LinkButton>
                                                <asp:LinkButton runat="server" ID="LinkButton4" CausesValidation="false" OnClick="Delete_Click"
                                                    Visible="false" Text="Xóa" CssClass="iconDel">                                              
                                                </asp:LinkButton>
                                                <asp:LinkButton runat="server" ID="LinkButton7" CausesValidation="false" OnClick="Back_Click"
                                                    Text="Quay lại" CssClass="iconExit">                                              
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
    </asp:Panel>
</asp:Content>
