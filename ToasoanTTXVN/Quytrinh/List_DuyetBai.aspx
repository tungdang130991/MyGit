<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/ITC.Master" AutoEventWireup="true"
    CodeBehind="List_DuyetBai.aspx.cs" Inherits="ToasoanTTXVN.Quytrinh.List_DuyetBai" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register TagPrefix="anthem" Namespace="Anthem" Assembly="Anthem" %>
<%@ Register TagPrefix="cc1" Namespace="HPCComponents.UI.WebControls" Assembly="HPCComponents" %>
<%@ Import Namespace="HPCComponents" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <script type="text/javascript">
        $(document).ready(function() {
            $("#<%=txt_tungay.ClientID%>").datepicker({
                showOn: 'button',
                buttonText: 'Show Date',
                buttonImageOnly: true,
                buttonImage: '../Dungchung/Images/DatePicker/calendar.png'
            });
            $("#<%=txt_denngay.ClientID%>").datepicker({
                showOn: 'button',
                buttonText: 'Show Date',
                buttonImageOnly: true,
                buttonImage: '../Dungchung/Images/DatePicker/calendar.png'
            });
        });
 
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

            if (check)
                divcontrol.css("display", "");
            else
                divcontrol.css("display", "none");


        }
        $(function() {
        $('.GridColumnFix a.viewmore').live("click",function() {
                $(this).parent().find('span').removeClass();
                $(this).parent().find('span').addClass('ChuthichExtend');
                $(this).hide();
            });
        });
    </script>

    <table border="0" cellpadding="0" width="100%" cellspacing="0">
        <tr>
            <td class="datagrid_content_left">
            </td>
            <td style="text-align: left;">
                <div class="divPanelSearch">
                    <div style="width: 99%; padding: 5px 0.5%">
                        <table border="0" cellpadding="2" cellspacing="2" style="width: 100%; text-align: right;">
                            <tr>
                                <td class="Titlelbl" style="text-align: left">
                                    <%=CommonLib.ReadXML("lblAnpham") %>
                                </td>
                                <td class="Titlelbl" style="text-align: left">
                                    <%=CommonLib.ReadXML("lblSobao") %>
                                </td>
                                <td class="Titlelbl" style="text-align: left">
                                    <%=CommonLib.ReadXML("lblTrang")%>
                                </td>
                                <td class="Titlelbl" style="text-align: left">
                                    <%=CommonLib.ReadXML("lblChuyenmuc") %>
                                </td>
                                <td class="Titlelbl" style="text-align: left">
                                    <%=CommonLib.ReadXML("lblTenbaiviet")%>
                                </td>
                                <td class="Titlelbl" style="text-align: left">
                                    <%=CommonLib.ReadXML("lblTungay")%>
                                </td>
                                <td class="Titlelbl" style="text-align: left">
                                    <%=CommonLib.ReadXML("lblDenngay")%>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 10%; text-align: left">
                                    <asp:UpdatePanel ID="UpdatePanel_LB" runat="server">
                                        <ContentTemplate>
                                            <asp:DropDownList ID="cbo_Anpham" runat="server" Width="100%" CssClass="inputtext"
                                                AutoPostBack="true" OnSelectedIndexChanged="cbo_Anpham_SelectedIndexChanged"
                                                TabIndex="1">
                                            </asp:DropDownList>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td style="width: 10%; text-align: left">
                                    <asp:UpdatePanel ID="UpdatePanel_SB" runat="server">
                                        <ContentTemplate>
                                            <asp:DropDownList ID="cboSoBao" runat="server" AutoPostBack="true" Width="100%" CssClass="inputtext"
                                                OnSelectedIndexChanged="cboSoBao_OnSelectedIndexChanged" TabIndex="5">
                                            </asp:DropDownList>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td style="width: 10%; text-align: left">
                                    <asp:UpdatePanel ID="UpdatePanel_Trang" runat="server">
                                        <ContentTemplate>
                                            <asp:DropDownList ID="cboPage" runat="server" AutoPostBack="true" Width="100%" OnSelectedIndexChanged="cboPage_OnSelectedIndexChanged"
                                                CssClass="inputtext">
                                            </asp:DropDownList>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td style="width: 12%; text-align: left">
                                    <asp:UpdatePanel ID="UpdatePanel_CM" runat="server">
                                        <ContentTemplate>
                                            <asp:DropDownList ID="cbo_chuyenmuc" runat="server" Width="100%" CssClass="inputtext"
                                                TabIndex="5">
                                            </asp:DropDownList>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td style="width: 12%; text-align: left">
                                    <asp:TextBox ID="txt_tieude" TabIndex="1" Width="93%" runat="server" CssClass="inputtext"
                                        placeholder="<%$Resources:cms.language, lblNhaptieudecantim%>" onkeypress="return clickButton(event,'ctl00_MainContent_cmdSeek');"></asp:TextBox>
                                </td>
                                <td style="width: 10%; text-align: left">
                                    <asp:TextBox ID="txt_tungay" runat="server" Width="120px" CssClass="inputtext" MaxLength="10"
                                        ToolTip="Từ ngày" onkeypress="AscciiDisable()" onfocus="javascript:vDateType='3'"
                                        onkeyup="DateFormat(this,this.value,event,false,'3')" onblur="DateFormat(this,this.value,event,true,'3')"></asp:TextBox>
                                    <asp:RegularExpressionValidator ID="Regularexpressionvalidator3" runat="server" ControlToValidate="txt_tungay"
                                        ValidationExpression="^\d{1,2}/\d{1,2}/\d{4}$" Display="Dynamic">Kiểu ngày:dd/MM/yyyy</asp:RegularExpressionValidator>
                                </td>
                                <td style="width: 10%; text-align: left">
                                    <asp:TextBox ID="txt_denngay" runat="server" Width="120px" CssClass="inputtext" MaxLength="10"
                                        ToolTip="Đến ngày" onkeypress="AscciiDisable()" onfocus="javascript:vDateType='3'"
                                        onkeyup="DateFormat(this,this.value,event,false,'3')" onblur="DateFormat(this,this.value,event,true,'3')"></asp:TextBox>
                                    <asp:RegularExpressionValidator ID="Regularexpressionvalidator1" runat="server" ControlToValidate="txt_denngay"
                                        ValidationExpression="^\d{1,2}/\d{1,2}/\d{4}$" Display="Dynamic">Kiểu ngày:dd/MM/yyyy</asp:RegularExpressionValidator>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: center; width: 100%;" colspan="7">
                                    <asp:UpdatePanel ID="UpdatePanelTimKiem" runat="server">
                                        <ContentTemplate>
                                            <asp:LinkButton runat="server" ID="btnTimkiem" CssClass="iconFind" OnClick="btnTimkiem_Click"
                                                Text="<%$Resources:cms.language, lblTimkiem%>"></asp:LinkButton>
                                            <asp:LinkButton runat="server" ID="LinkAdd" CssClass="iconAdd" OnClick="ThemMoi_Click"
                                                Text="<%$Resources:cms.language, lblNhapbai%>">
                                            </asp:LinkButton>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
                <div class="divPanelResult">
                    <table cellpadding="2" cellspacing="2" border="0" width="100%">
                        <tr>
                            <td style="vertical-align: top; width: 100%; padding: 5px 5px 5px 5px;">
                                <asp:UpdatePanel ID="UpdatePanelTabContainer" runat="server">
                                    <ContentTemplate>
                                        <div style="text-align: left; width: 100%">
                                            <ajaxToolkit:TabContainer ID="TabContainer1" runat="server" CssClass="ajax__tab_yuitabview-theme"
                                                AutoPostBack="true" ActiveTabIndex="0" OnActiveTabChanged="TabContainer1_ActiveTabChanged">
                                                <ajaxToolkit:TabPanel HeaderText="<%$Resources:cms.language, lblTinmoi%>" ID="tabpnltinChoXuly"
                                                    runat="server">
                                                    <ContentTemplate>
                                                        <table border="0" style="width: 100%">
                                                            <tr>
                                                                <td>
                                                                    <anthem:DataGrid ID="DataGrid_TinMoi" runat="server" Width="100%" BorderStyle="None"
                                                                        AutoGenerateColumns="False" BorderColor="#d4d4d4" CellPadding="0" DataKeyField="Ma_Tinbai"
                                                                        BackColor="White" CssClass="Grid" BorderWidth="1px" AlternatingItemStyle-BackColor="#F1F1F2"
                                                                        OnEditCommand="DataGrid_EditCommand" OnItemDataBound="DataGrid_OnItemDataBound">
                                                                        <ItemStyle CssClass="GridItem"></ItemStyle>
                                                                        <AlternatingItemStyle CssClass="GridAltItem" />
                                                                        <HeaderStyle CssClass="GridHeader"></HeaderStyle>
                                                                        <Columns>
                                                                            <asp:TemplateColumn>
                                                                                <HeaderStyle HorizontalAlign="Center" Width="4%"></HeaderStyle>
                                                                                <ItemStyle HorizontalAlign="Center" CssClass="GridBorderVerSolid"></ItemStyle>
                                                                                <HeaderTemplate>
                                                                                    <asp:CheckBox ID="chkAll" onclick="javascript:SelectAllCheckboxes(this);" runat="server"
                                                                                        ToolTip="Chọn tất cả"></asp:CheckBox>
                                                                                </HeaderTemplate>
                                                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                                                <ItemTemplate>
                                                                                    <asp:CheckBox runat="server" Text='' ID='optSelect' CssClass="checkitem" onclick="javascript:CheckItem()"
                                                                                        Enabled='<%# HPCBusinessLogic.Utils.IsEnable(_Role.R_Write,DataBinder.Eval(Container.DataItem, "Nguoi_Khoa").ToString(),_user.UserID)%>'
                                                                                        AutoPostBack="False"></asp:CheckBox>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateColumn>
                                                                            <asp:BoundColumn DataField="Ma_Tinbai" HeaderText="Mã tin bài" Visible="False"></asp:BoundColumn>
                                                                            <asp:TemplateColumn HeaderText="Image">
                                                                                <HeaderStyle HorizontalAlign="Center" Width="5%"></HeaderStyle>
                                                                                <ItemStyle HorizontalAlign="Center" CssClass="GridBorderVerSolid"></ItemStyle>
                                                                                <ItemTemplate>
                                                                                    <div style="float: left; width: 100%; text-align: center">
                                                                                        <%#HPCBusinessLogic.UltilFunc.GetImageAttach(Eval("Ma_Tinbai"))%>
                                                                                    </div>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateColumn>
                                                                            <asp:TemplateColumn HeaderText="<%$Resources:cms.language, lblTenbaiviet%>">
                                                                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                                                <ItemStyle HorizontalAlign="Left" CssClass="GridBorderVerSolid"></ItemStyle>
                                                                                <ItemTemplate>
                                                                                    <div class="GridColumnFix">
                                                                                        <asp:LinkButton CssClass="linkGridForm" Text='<%#HPCComponents.CommonLib.GetTieuDeBaiGac(Eval("Ma_Tinbai"))%>'
                                                                                            Enabled='<%# HPCBusinessLogic.Utils.IsEnable(_Role.R_Write,DataBinder.Eval(Container.DataItem, "Nguoi_Khoa").ToString(),_user.UserID)%>'
                                                                                            runat="server" ID="linkTittle" CommandName="Edit" CommandArgument="Edit" ToolTip="Sửa bài"></asp:LinkButton>
                                                                                        <span class="linkGridForm">
                                                                                            <%# HPCBusinessLogic.Utils.LockedUser(DataBinder.Eval(Container.DataItem, "Nguoi_Khoa").ToString(), _user.UserID)%></span>
                                                                                        <br />
                                                                                        <%#HPCBusinessLogic.UltilFunc.SeeMore(Eval("Ghichu"))%>
                                                                                    </div>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateColumn>
                                                                            <asp:TemplateColumn HeaderText="<%$Resources:cms.language, lblAnpham%>">
                                                                                <HeaderStyle HorizontalAlign="Center" Width="10%"></HeaderStyle>
                                                                                <ItemStyle HorizontalAlign="Center" Width="10%" CssClass="GridBorderVerSolid"></ItemStyle>
                                                                                <ItemTemplate>
                                                                                    <span class="linkGridForm">
                                                                                        <%#HPCBusinessLogic.UltilFunc.GetTenAnpham(Eval("Ma_Anpham"))%></span>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateColumn>
                                                                            <asp:TemplateColumn HeaderText="<%$Resources:cms.language, lblTrang%>">
                                                                                <HeaderStyle HorizontalAlign="Center" Width="3%"></HeaderStyle>
                                                                                <ItemStyle HorizontalAlign="Center" Width="3%" CssClass="GridBorderVerSolid"></ItemStyle>
                                                                                <ItemTemplate>
                                                                                    <span class="linkGridForm">
                                                                                        <%#HPCBusinessLogic.UltilFunc.GetTenSoBaoFromT_Vitri_Tinbai(Eval("Ma_Tinbai"),1)%></span>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateColumn>
                                                                            <asp:TemplateColumn HeaderText="<%$Resources:cms.language, lblSotu%>">
                                                                                <HeaderStyle HorizontalAlign="Center" Width="5%"></HeaderStyle>
                                                                                <ItemStyle HorizontalAlign="Center" Width="5%" CssClass="GridBorderVerSolid"></ItemStyle>
                                                                                <ItemTemplate>
                                                                                    <span class="linkGridForm">
                                                                                        <%#Eval("Sotu")%></span>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateColumn>
                                                                            <asp:TemplateColumn HeaderText="<%$Resources:cms.language, lblSobao%>">
                                                                                <HeaderStyle HorizontalAlign="Center" Width="8%"></HeaderStyle>
                                                                                <ItemStyle HorizontalAlign="Center" Width="8%" CssClass="GridBorderVerSolid"></ItemStyle>
                                                                                <ItemTemplate>
                                                                                    <span class="linkGridForm">
                                                                                        <%#HPCBusinessLogic.UltilFunc.GetTenSoBaoFromT_Vitri_Tinbai(Eval("Ma_Tinbai"),0)%></span>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateColumn>
                                                                            <asp:TemplateColumn HeaderText="<%$Resources:cms.language, lblChuyenmuc%>">
                                                                                <HeaderStyle Width="12%" HorizontalAlign="Center"></HeaderStyle>
                                                                                <ItemStyle Width="12%" HorizontalAlign="Left" CssClass="GridBorderVerSolid"></ItemStyle>
                                                                                <ItemTemplate>
                                                                                    <span class="linkGridForm">
                                                                                        <%#HPCBusinessLogic.UltilFunc.GetTenChuyenMuc(Eval("Ma_Chuyenmuc"))%></span>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateColumn>
                                                                            <asp:TemplateColumn HeaderText="<%$Resources:cms.language, lblNguoigui%>">
                                                                                <HeaderStyle HorizontalAlign="Center" Width="10%"></HeaderStyle>
                                                                                <ItemStyle HorizontalAlign="Left" Width="10%" CssClass="GridBorderVerSolid"></ItemStyle>
                                                                                <ItemTemplate>
                                                                                    <span class="linkGridForm">
                                                                                        <%#HPCBusinessLogic.UltilFunc.GetUserFullName(Eval("Ma_Nguoitao"))%></span>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateColumn>
                                                                            <asp:TemplateColumn HeaderText="<%$Resources:cms.language, lblNgaygui%>">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="ngaytao" CssClass="linkGridForm" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Ngaytao")!=System.DBNull.Value?Convert.ToDateTime(DataBinder.Eval(Container, "DataItem.Ngaytao")).ToString("dd/MM/yyyy HH:mm"):"" %>'>
                                                                                    </asp:Label>
                                                                                </ItemTemplate>
                                                                                <HeaderStyle HorizontalAlign="Center" Width="10%"></HeaderStyle>
                                                                                <ItemStyle HorizontalAlign="Center" CssClass="GridBorderVerSolid"></ItemStyle>
                                                                            </asp:TemplateColumn>
                                                                            <asp:TemplateColumn HeaderText="<%$Resources:cms.language, lblXoa%>">
                                                                                <HeaderStyle HorizontalAlign="Center" Width="3%"></HeaderStyle>
                                                                                <ItemStyle HorizontalAlign="Center" Width="3%" CssClass="GridBorderVerSolid"></ItemStyle>
                                                                                <ItemTemplate>
                                                                                    <asp:ImageButton ID="btnDelete" Width="15px" runat="server" ImageUrl="~/Dungchung/images/cancel.gif"
                                                                                        Enabled='<%# HPCBusinessLogic.Utils.IsEnable(_Role.R_Write,DataBinder.Eval(Container.DataItem, "Nguoi_Khoa").ToString(),_user.UserID)%>'
                                                                                        ImageAlign="AbsMiddle" ToolTip="Xóa" CommandName="Edit" CommandArgument="Delete"
                                                                                        CausesValidation="false" BorderStyle="None"></asp:ImageButton>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateColumn>
                                                                            <asp:TemplateColumn HeaderText="<%$Resources:cms.language, lblXem%>">
                                                                                <HeaderStyle HorizontalAlign="Center" Width="3%"></HeaderStyle>
                                                                                <ItemStyle HorizontalAlign="Center" Width="3%" CssClass="GridBorderVerSolid"></ItemStyle>
                                                                                <ItemTemplate>
                                                                                    <a href="Javascript:open_window_Scroll('<%=Global.ApplicationPath%>/Quytrinh/ViewPhienBanTinBai.aspx?Menu_ID=<%=Page.Request.QueryString["Menu_ID"]%>&ID=<%# DataBinder.Eval(Container.DataItem, "Ma_Tinbai") %>',50,500,100,800);" />
                                                                                    <img src='<%=Global.ApplicationPath%>/Dungchung/Images/view.gif' border="0" onmouseover="(window.status=''); return true"
                                                                                        style="cursor: hand; border: 0" />
                                                                                </ItemTemplate>
                                                                            </asp:TemplateColumn>
                                                                            <asp:TemplateColumn>
                                                                                <HeaderStyle HorizontalAlign="Center" Width="4%"></HeaderStyle>
                                                                                <ItemStyle HorizontalAlign="Center" Width="4%" CssClass="GridBorderVerSolid"></ItemStyle>
                                                                                <HeaderTemplate>
                                                                                    Preview
                                                                                </HeaderTemplate>
                                                                                <ItemTemplate>
                                                                                    <a href="Javascript:open_window_Scroll('<%=Global.ApplicationPath%>/Quytrinh/Preview.aspx?Menu_ID=<%=Page.Request.QueryString["Menu_ID"]%>&ID=<%# DataBinder.Eval(Container.DataItem, "Ma_Tinbai") %>',50,500,100,800);">
                                                                                        <img src='<%=Global.ApplicationPath%>/Dungchung/Images/Icons/preview.png' border="0"
                                                                                            style="cursor: hand; border: 0" /></a>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateColumn>
                                                                        </Columns>
                                                                    </anthem:DataGrid>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </ContentTemplate>
                                                </ajaxToolkit:TabPanel>
                                                <ajaxToolkit:TabPanel HeaderText="<%$Resources:cms.language, lblTinbientap%>" ID="TabPanelDangxuly"
                                                    runat="server">
                                                    <ContentTemplate>
                                                        <table border="0" style="width: 100%">
                                                            <tr>
                                                                <td>
                                                                    <anthem:DataGrid ID="DataGrid_TinDangXuLy" runat="server" Width="100%" AutoGenerateColumns="False"
                                                                        CssClass="Grid" BorderColor="#7F8080" CellPadding="0" DataKeyField="Ma_Tinbai"
                                                                        BorderStyle="None" BackColor="White" BorderWidth="1px" AlternatingItemStyle-BackColor="#F1F1F2"
                                                                        OnItemDataBound="DataGrid_OnItemDataBound" OnEditCommand="DataGrid_EditCommand">
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
                                                                                <ItemStyle HorizontalAlign="Center" CssClass="GridBorderVerSolid"></ItemStyle>
                                                                                <ItemTemplate>
                                                                                    <asp:CheckBox runat="server" Text='' ID='optSelect' AutoPostBack="False" CssClass="checkitem"
                                                                                        onclick="javascript:CheckItem()" Enabled='<%# HPCBusinessLogic.Utils.IsEnable(_Role.R_Write,DataBinder.Eval(Container.DataItem, "Nguoi_Khoa").ToString(),_user.UserID)%>'>
                                                                                    </asp:CheckBox>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateColumn>
                                                                            <asp:BoundColumn DataField="Ma_Tinbai" HeaderText="Ma_Tinbai" Visible="False"></asp:BoundColumn>
                                                                            <asp:TemplateColumn HeaderText="Image">
                                                                                <HeaderStyle HorizontalAlign="Center" Width="5%"></HeaderStyle>
                                                                                <ItemStyle HorizontalAlign="Center" CssClass="GridBorderVerSolid"></ItemStyle>
                                                                                <ItemTemplate>
                                                                                    <div style="float: left; width: 100%; text-align: center">
                                                                                        <%#HPCBusinessLogic.UltilFunc.GetImageAttach(Eval("Ma_Tinbai"))%>
                                                                                    </div>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateColumn>
                                                                            <asp:TemplateColumn HeaderText="<%$Resources:cms.language, lblTenbaiviet%>">
                                                                                <HeaderStyle HorizontalAlign="Center" Width="25%"></HeaderStyle>
                                                                                <ItemStyle HorizontalAlign="Left" CssClass="GridBorderVerSolid"></ItemStyle>
                                                                                <ItemTemplate>
                                                                                    <asp:LinkButton CssClass="linkGridForm" Text='<%#HPCComponents.CommonLib.GetTieuDeBaiGac(Eval("Ma_Tinbai"))%>'
                                                                                        Enabled='<%# HPCBusinessLogic.Utils.IsEnable(_Role.R_Write,DataBinder.Eval(Container.DataItem, "Nguoi_Khoa").ToString(),_user.UserID)%>'
                                                                                        runat="server" ID="linkTittle" CommandName="Edit" CommandArgument="Edit" ToolTip="Sửa bài"></asp:LinkButton>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateColumn>
                                                                            <asp:TemplateColumn HeaderText="<%$Resources:cms.language, lblAnpham%>">
                                                                                <HeaderStyle HorizontalAlign="Center" Width="10%"></HeaderStyle>
                                                                                <ItemStyle HorizontalAlign="Center" Width="10%" CssClass="GridBorderVerSolid"></ItemStyle>
                                                                                <ItemTemplate>
                                                                                    <span class="linkGridForm">
                                                                                        <%#HPCBusinessLogic.UltilFunc.GetTenAnpham(Eval("Ma_Anpham"))%></span>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateColumn>
                                                                            <asp:TemplateColumn HeaderText="<%$Resources:cms.language, lblTrang%>">
                                                                                <HeaderStyle HorizontalAlign="Center" Width="3%"></HeaderStyle>
                                                                                <ItemStyle HorizontalAlign="Center" Width="3%" CssClass="GridBorderVerSolid"></ItemStyle>
                                                                                <ItemTemplate>
                                                                                    <span class="linkGridForm">
                                                                                        <%#HPCBusinessLogic.UltilFunc.GetTenSoBaoFromT_Vitri_Tinbai(Eval("Ma_Tinbai"),1)%></span>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateColumn>
                                                                            <asp:TemplateColumn HeaderText="<%$Resources:cms.language, lblSotu%>">
                                                                                <HeaderStyle HorizontalAlign="Center" Width="5%"></HeaderStyle>
                                                                                <ItemStyle HorizontalAlign="Center" Width="5%" CssClass="GridBorderVerSolid"></ItemStyle>
                                                                                <ItemTemplate>
                                                                                    <span class="linkGridForm">
                                                                                        <%#Eval("Sotu")%></span>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateColumn>
                                                                            <asp:TemplateColumn HeaderText="<%$Resources:cms.language, lblSobao%>">
                                                                                <HeaderStyle HorizontalAlign="Center" Width="8%"></HeaderStyle>
                                                                                <ItemStyle HorizontalAlign="Center" Width="8%" CssClass="GridBorderVerSolid"></ItemStyle>
                                                                                <ItemTemplate>
                                                                                    <span class="linkGridForm">
                                                                                        <%#HPCBusinessLogic.UltilFunc.GetTenSoBaoFromT_Vitri_Tinbai(Eval("Ma_Tinbai"),0)%></span>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateColumn>
                                                                            <asp:TemplateColumn HeaderText="<%$Resources:cms.language, lblChuyenmuc%>">
                                                                                <HeaderStyle Width="12%" HorizontalAlign="Center"></HeaderStyle>
                                                                                <ItemStyle Width="12%" HorizontalAlign="Left" CssClass="GridBorderVerSolid"></ItemStyle>
                                                                                <ItemTemplate>
                                                                                    <span class="linkGridForm">
                                                                                        <%#HPCBusinessLogic.UltilFunc.GetTenChuyenMuc(Eval("Ma_Chuyenmuc"))%></span>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateColumn>
                                                                            <asp:TemplateColumn HeaderText="<%$Resources:cms.language, lblNguoixuly%>">
                                                                                <HeaderStyle HorizontalAlign="Center" Width="10%"></HeaderStyle>
                                                                                <ItemStyle HorizontalAlign="Left" Width="10%" CssClass="GridBorderVerSolid"></ItemStyle>
                                                                                <ItemTemplate>
                                                                                    <span class="linkGridForm">
                                                                                        <%#HPCBusinessLogic.UltilFunc.GetUserFullName(Eval("Ma_Nguoitao"))%></span>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateColumn>
                                                                            <asp:TemplateColumn HeaderText="<%$Resources:cms.language, lblNgayxuly%>">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="ngaytao" CssClass="linkGridForm" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Ngaytao")!=System.DBNull.Value?Convert.ToDateTime(DataBinder.Eval(Container, "DataItem.Ngaytao")).ToString("dd/MM/yyyy HH:mm:ss"):"" %>'>
                                                                                    </asp:Label>
                                                                                </ItemTemplate>
                                                                                <HeaderStyle HorizontalAlign="Center" Width="10%"></HeaderStyle>
                                                                                <ItemStyle HorizontalAlign="Center" CssClass="GridBorderVerSolid"></ItemStyle>
                                                                            </asp:TemplateColumn>
                                                                            <asp:TemplateColumn HeaderText="<%$Resources:cms.language, lblXoa%>">
                                                                                <HeaderStyle HorizontalAlign="Center" Width="3%"></HeaderStyle>
                                                                                <ItemStyle HorizontalAlign="Center" Width="3%" CssClass="GridBorderVerSolid"></ItemStyle>
                                                                                <ItemTemplate>
                                                                                    <asp:ImageButton ID="btnDelete" Width="15px" runat="server" ImageUrl="~/Dungchung/images/cancel.gif"
                                                                                        Enabled='<%# HPCBusinessLogic.Utils.IsEnable(_Role.R_Write,DataBinder.Eval(Container.DataItem, "Nguoi_Khoa").ToString(),_user.UserID)%>'
                                                                                        ImageAlign="AbsMiddle" ToolTip="Xóa" CommandName="Edit" CommandArgument="Delete"
                                                                                        CausesValidation="false" BorderStyle="None"></asp:ImageButton>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateColumn>
                                                                            <asp:TemplateColumn HeaderText="<%$Resources:cms.language, lblXem%>">
                                                                                <HeaderStyle HorizontalAlign="Center" Width="3%"></HeaderStyle>
                                                                                <ItemStyle HorizontalAlign="Center" Width="3%" CssClass="GridBorderVerSolid"></ItemStyle>
                                                                                <ItemTemplate>
                                                                                    <a href="Javascript:open_window_Scroll('<%=Global.ApplicationPath%>/Quytrinh/ViewPhienBanTinBai.aspx?Menu_ID=<%=Page.Request.QueryString["Menu_ID"]%>&ID=<%# DataBinder.Eval(Container.DataItem, "Ma_Tinbai") %>',50,500,100,800);" />
                                                                                    <img src='<%=Global.ApplicationPath%>/Dungchung/Images/view.gif' border="0" onmouseover="(window.status=''); return true"
                                                                                        style="cursor: hand; border: 0" />
                                                                                </ItemTemplate>
                                                                            </asp:TemplateColumn>
                                                                            <asp:TemplateColumn>
                                                                                <HeaderStyle HorizontalAlign="Center" Width="4%"></HeaderStyle>
                                                                                <ItemStyle HorizontalAlign="Center" Width="4%" CssClass="GridBorderVerSolid"></ItemStyle>
                                                                                <HeaderTemplate>
                                                                                    Preview
                                                                                </HeaderTemplate>
                                                                                <ItemTemplate>
                                                                                    <a href="Javascript:open_window_Scroll('<%=Global.ApplicationPath%>/Quytrinh/Preview.aspx?Menu_ID=<%=Page.Request.QueryString["Menu_ID"]%>&ID=<%# DataBinder.Eval(Container.DataItem, "Ma_Tinbai") %>',50,500,100,800);">
                                                                                        <img src='<%=Global.ApplicationPath%>/Dungchung/Images/Icons/preview.png' border="0"
                                                                                            style="cursor: hand; border: 0" /></a>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateColumn>
                                                                        </Columns>
                                                                    </anthem:DataGrid>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </ContentTemplate>
                                                </ajaxToolkit:TabPanel>
                                                <ajaxToolkit:TabPanel HeaderText="<%$Resources:cms.language, lblTintralai%>" ID="TabPanelTinTraLai"
                                                    runat="server">
                                                    <ContentTemplate>
                                                        <table border="0" style="width: 100%">
                                                            <tr>
                                                                <td>
                                                                    <anthem:DataGrid ID="DataGrid_TinTralai" runat="server" Width="100%" AutoGenerateColumns="False"
                                                                        CssClass="Grid" BorderColor="#7F8080" CellPadding="0" DataKeyField="Ma_Tinbai"
                                                                        BorderStyle="None" BackColor="White" BorderWidth="1px" AlternatingItemStyle-BackColor="#F1F1F2"
                                                                        OnItemDataBound="DataGrid_OnItemDataBound" OnEditCommand="DataGrid_EditCommand">
                                                                        <ItemStyle CssClass="GridItem"></ItemStyle>
                                                                        <AlternatingItemStyle CssClass="GridAltItem" />
                                                                        <HeaderStyle CssClass="GridHeader"></HeaderStyle>
                                                                        <Columns>
                                                                            <asp:TemplateColumn>
                                                                                <HeaderStyle HorizontalAlign="Center" Width="5%"></HeaderStyle>
                                                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                                                <HeaderTemplate>
                                                                                    <asp:CheckBox ID="chkAll" onclick="javascript:SelectAllCheckboxes(this);" runat="server"
                                                                                        ToolTip="Select all"></asp:CheckBox>
                                                                                </HeaderTemplate>
                                                                                <ItemStyle HorizontalAlign="Center" CssClass="GridBorderVerSolid"></ItemStyle>
                                                                                <ItemTemplate>
                                                                                    <asp:CheckBox runat="server" Text='' ID='optSelect' CssClass="checkitem" onclick="javascript:CheckItem()"
                                                                                        Enabled='<%# HPCBusinessLogic.Utils.IsEnable(_Role.R_Write,DataBinder.Eval(Container.DataItem, "Nguoi_Khoa").ToString(),_user.UserID)%>'
                                                                                        AutoPostBack="False"></asp:CheckBox>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateColumn>
                                                                            <asp:BoundColumn DataField="Ma_Tinbai" HeaderText="Ma_Tinbai" Visible="False"></asp:BoundColumn>
                                                                            <asp:TemplateColumn HeaderText="Image">
                                                                                <HeaderStyle HorizontalAlign="Center" Width="5%"></HeaderStyle>
                                                                                <ItemStyle HorizontalAlign="Center" CssClass="GridBorderVerSolid"></ItemStyle>
                                                                                <ItemTemplate>
                                                                                    <div style="float: left; width: 100%; text-align: center">
                                                                                        <%#HPCBusinessLogic.UltilFunc.GetImageAttach(Eval("Ma_Tinbai"))%>
                                                                                    </div>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateColumn>
                                                                            <asp:TemplateColumn HeaderText="<%$Resources:cms.language, lblTenbaiviet%>">
                                                                                <HeaderStyle HorizontalAlign="Center" Width="25%"></HeaderStyle>
                                                                                <ItemStyle HorizontalAlign="Left" CssClass="GridBorderVerSolid"></ItemStyle>
                                                                                <ItemTemplate>
                                                                                    <asp:LinkButton CssClass="linkGridForm" Text='<%#HPCComponents.CommonLib.GetTieuDeBaiGac(Eval("Ma_Tinbai"))%>'
                                                                                        Enabled='<%# HPCBusinessLogic.Utils.IsEnable(_Role.R_Write,DataBinder.Eval(Container.DataItem, "Nguoi_Khoa").ToString(),_user.UserID)%>'
                                                                                        runat="server" ID="linkTittle" CommandName="Edit" CommandArgument="Edit" ToolTip="Sửa bài"></asp:LinkButton>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateColumn>
                                                                            <asp:TemplateColumn HeaderText="<%$Resources:cms.language, lblAnpham%>">
                                                                                <HeaderStyle HorizontalAlign="Center" Width="8%"></HeaderStyle>
                                                                                <ItemStyle HorizontalAlign="Center" Width="8%" CssClass="GridBorderVerSolid"></ItemStyle>
                                                                                <ItemTemplate>
                                                                                    <span class="linkGridForm">
                                                                                        <%#HPCBusinessLogic.UltilFunc.GetTenAnpham(Eval("Ma_Anpham"))%></span>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateColumn>
                                                                            <asp:TemplateColumn HeaderText="<%$Resources:cms.language, lblTrang%>">
                                                                                <HeaderStyle HorizontalAlign="Center" Width="3%"></HeaderStyle>
                                                                                <ItemStyle HorizontalAlign="Center" Width="3%" CssClass="GridBorderVerSolid"></ItemStyle>
                                                                                <ItemTemplate>
                                                                                    <span class="linkGridForm">
                                                                                        <%#HPCBusinessLogic.UltilFunc.GetTenSoBaoFromT_Vitri_Tinbai(Eval("Ma_Tinbai"),1)%></span>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateColumn>
                                                                            <asp:TemplateColumn HeaderText="<%$Resources:cms.language, lblSotu%>">
                                                                                <HeaderStyle HorizontalAlign="Center" Width="5%"></HeaderStyle>
                                                                                <ItemStyle HorizontalAlign="Center" Width="5%" CssClass="GridBorderVerSolid"></ItemStyle>
                                                                                <ItemTemplate>
                                                                                    <span class="linkGridForm">
                                                                                        <%#Eval("Sotu")%></span>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateColumn>
                                                                            <asp:TemplateColumn HeaderText="<%$Resources:cms.language, lblSobao%>">
                                                                                <HeaderStyle HorizontalAlign="Center" Width="8%"></HeaderStyle>
                                                                                <ItemStyle HorizontalAlign="Center" Width="8%" CssClass="GridBorderVerSolid"></ItemStyle>
                                                                                <ItemTemplate>
                                                                                    <span class="linkGridForm">
                                                                                        <%#HPCBusinessLogic.UltilFunc.GetTenSoBaoFromT_Vitri_Tinbai(Eval("Ma_Tinbai"),0)%></span>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateColumn>
                                                                            <asp:TemplateColumn HeaderText="<%$Resources:cms.language, lblChuyenmuc%>">
                                                                                <HeaderStyle Width="12%" HorizontalAlign="Center"></HeaderStyle>
                                                                                <ItemStyle Width="12%" HorizontalAlign="Left" CssClass="GridBorderVerSolid"></ItemStyle>
                                                                                <ItemTemplate>
                                                                                    <span class="linkGridForm">
                                                                                        <%#HPCBusinessLogic.UltilFunc.GetTenChuyenMuc(Eval("Ma_Chuyenmuc"))%></span>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateColumn>
                                                                            <asp:TemplateColumn HeaderText="<%$Resources:cms.language, lblNguoitra%>">
                                                                                <HeaderStyle HorizontalAlign="Center" Width="10%"></HeaderStyle>
                                                                                <ItemStyle HorizontalAlign="Left" Width="10%" CssClass="GridBorderVerSolid"></ItemStyle>
                                                                                <ItemTemplate>
                                                                                    <span class="linkGridForm">
                                                                                        <%#HPCBusinessLogic.UltilFunc.GetUserFullName(HPCBusinessLogic.UltilFunc.GetNguoiTralaiNgayTralai(Eval("Ma_Tinbai"), 0))%></span>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateColumn>
                                                                            <asp:TemplateColumn HeaderText="<%$Resources:cms.language, lblNgaytra%>">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="ngaytao" CssClass="linkGridForm" runat="server" Text='<%#HPCBusinessLogic.UltilFunc.GetNguoiTralaiNgayTralai(Eval("Ma_Tinbai"),1) %>'>
                                                                                    </asp:Label>
                                                                                </ItemTemplate>
                                                                                <HeaderStyle HorizontalAlign="Center" Width="10%"></HeaderStyle>
                                                                                <ItemStyle HorizontalAlign="Center" CssClass="GridBorderVerSolid"></ItemStyle>
                                                                            </asp:TemplateColumn>
                                                                            <asp:TemplateColumn HeaderText="<%$Resources:cms.language, lblXoa%>">
                                                                                <HeaderStyle HorizontalAlign="Center" Width="3%"></HeaderStyle>
                                                                                <ItemStyle HorizontalAlign="Center" Width="3%" CssClass="GridBorderVerSolid"></ItemStyle>
                                                                                <ItemTemplate>
                                                                                    <asp:ImageButton ID="btnDelete" Width="15px" runat="server" ImageUrl="~/Dungchung/images/cancel.gif"
                                                                                        Enabled='<%# HPCBusinessLogic.Utils.IsEnable(_Role.R_Write,DataBinder.Eval(Container.DataItem, "Nguoi_Khoa").ToString(),_user.UserID)%>'
                                                                                        ImageAlign="AbsMiddle" ToolTip="Xóa" CommandName="Edit" CommandArgument="Delete"
                                                                                        CausesValidation="false" BorderStyle="None"></asp:ImageButton>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateColumn>
                                                                            <asp:TemplateColumn HeaderText="<%$Resources:cms.language, lblXem%>">
                                                                                <HeaderStyle HorizontalAlign="Center" Width="3%"></HeaderStyle>
                                                                                <ItemStyle HorizontalAlign="Center" Width="3%" CssClass="GridBorderVerSolid"></ItemStyle>
                                                                                <ItemTemplate>
                                                                                    <a href="Javascript:open_window_Scroll('<%=Global.ApplicationPath%>/Quytrinh/ViewPhienBanTinBai.aspx?Menu_ID=<%=Page.Request.QueryString["Menu_ID"]%>&ID=<%# DataBinder.Eval(Container.DataItem, "Ma_Tinbai") %>',50,500,100,800);" />
                                                                                    <img src='<%=Global.ApplicationPath%>/Dungchung/Images/view.gif' border="0" onmouseover="(window.status=''); return true"
                                                                                        style="cursor: hand; border: 0" />
                                                                                </ItemTemplate>
                                                                            </asp:TemplateColumn>
                                                                        </Columns>
                                                                    </anthem:DataGrid>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </ContentTemplate>
                                                </ajaxToolkit:TabPanel>
                                                <ajaxToolkit:TabPanel HeaderText="<%$Resources:cms.language, lblTindagui%>" ID="TabPanelTinDaXuLy"
                                                    runat="server">
                                                    <ContentTemplate>
                                                        <table border="0" style="width: 100%">
                                                            <tr>
                                                                <td>
                                                                    <anthem:DataGrid ID="DataGrid_TinDaXuLy" runat="server" Width="100%" AutoGenerateColumns="False"
                                                                        CssClass="Grid" BorderColor="#7F8080" CellPadding="0" AlternatingItemStyle-BackColor="#F1F1F2"
                                                                        OnItemDataBound="DataGrid_OnItemDataBound" DataKeyField="Ma_Phienban" BackColor="White"
                                                                        BorderStyle="None" BorderWidth="1px">
                                                                        <ItemStyle CssClass="GridItem"></ItemStyle>
                                                                        <AlternatingItemStyle CssClass="GridAltItem" />
                                                                        <HeaderStyle CssClass="GridHeader"></HeaderStyle>
                                                                        <Columns>
                                                                            <asp:BoundColumn DataField="Ma_Phienban" HeaderText="Ma_Phienban" Visible="False">
                                                                            </asp:BoundColumn>
                                                                            <asp:TemplateColumn HeaderText="<%$Resources:cms.language, lblTenbaiviet%>">
                                                                                <HeaderStyle Width="30%" HorizontalAlign="Center"></HeaderStyle>
                                                                                <ItemStyle Width="30%" HorizontalAlign="Left" CssClass="GridBorderVerSolid"></ItemStyle>
                                                                                <ItemTemplate>
                                                                                    <a class="linkGridForm" href="Javascript:open_window_Scroll('<%=Global.ApplicationPath%>/Quytrinh/ViewPhienBanTinBai.aspx?Menu_ID=<%=Page.Request.QueryString["Menu_ID"]%>&ID=<%# DataBinder.Eval(Container.DataItem, "Ma_Tinbai") %>',50,500,100,800);">
                                                                                        <%#Eval("Tieude")%></a>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateColumn>
                                                                            <asp:TemplateColumn HeaderText="<%$Resources:cms.language, lblAnpham%>">
                                                                                <HeaderStyle HorizontalAlign="Center" Width="8%"></HeaderStyle>
                                                                                <ItemStyle HorizontalAlign="Center" Width="8%" CssClass="GridBorderVerSolid"></ItemStyle>
                                                                                <ItemTemplate>
                                                                                    <span class="linkGridForm">
                                                                                        <%#HPCBusinessLogic.UltilFunc.GetTenAnpham(Eval("Ma_Anpham"))%></span>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateColumn>
                                                                            <asp:TemplateColumn HeaderText="<%$Resources:cms.language, lblTrang%>">
                                                                                <HeaderStyle HorizontalAlign="Center" Width="3%"></HeaderStyle>
                                                                                <ItemStyle HorizontalAlign="Center" Width="3%" CssClass="GridBorderVerSolid"></ItemStyle>
                                                                                <ItemTemplate>
                                                                                    <span class="linkGridForm">
                                                                                        <%#HPCBusinessLogic.UltilFunc.GetTenSoBaoFromT_Vitri_Tinbai(Eval("Ma_Tinbai"),1)%></span>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateColumn>
                                                                            <asp:TemplateColumn HeaderText="<%$Resources:cms.language, lblChuyenmuc%>">
                                                                                <HeaderStyle Width="12%" HorizontalAlign="Center"></HeaderStyle>
                                                                                <ItemStyle Width="12%" HorizontalAlign="Left" CssClass="GridBorderVerSolid"></ItemStyle>
                                                                                <ItemTemplate>
                                                                                    <span class="linkGridForm">
                                                                                        <%#HPCBusinessLogic.UltilFunc.GetTenChuyenMuc(Eval("Ma_Chuyenmuc"))%></span>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateColumn>
                                                                            <asp:TemplateColumn HeaderText="<%$Resources:cms.language, lblSobao%>">
                                                                                <HeaderStyle HorizontalAlign="Center" Width="8%"></HeaderStyle>
                                                                                <ItemStyle HorizontalAlign="Center" Width="8%" CssClass="GridBorderVerSolid"></ItemStyle>
                                                                                <ItemTemplate>
                                                                                    <span class="linkGridForm">
                                                                                        <%#HPCBusinessLogic.UltilFunc.GetTenSoBaoFromT_Vitri_Tinbai(Eval("Ma_Tinbai"),0)%></span>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateColumn>
                                                                            <asp:TemplateColumn HeaderText="<%$Resources:cms.language, lblNgaygui%>">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="ngaytao" CssClass="linkGridForm" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Ngaytao")!=System.DBNull.Value?Convert.ToDateTime(DataBinder.Eval(Container, "DataItem.Ngaytao")).ToString("dd/MM/yyyy HH:mm:ss"):"" %>'>
                                                                                    </asp:Label>
                                                                                </ItemTemplate>
                                                                                <HeaderStyle HorizontalAlign="Center" Width="10%"></HeaderStyle>
                                                                                <ItemStyle HorizontalAlign="Center" CssClass="GridBorderVerSolid"></ItemStyle>
                                                                            </asp:TemplateColumn>
                                                                            <asp:TemplateColumn HeaderText="<%$Resources:cms.language, lblThaotac%>">
                                                                                <HeaderStyle Width="15%" HorizontalAlign="Center"></HeaderStyle>
                                                                                <ItemStyle Width="15%" HorizontalAlign="Left" CssClass="GridBorderVerSolid"></ItemStyle>
                                                                                <ItemTemplate>
                                                                                    <span class="linkGridForm">
                                                                                        <%#HPCComponents.Global.GetActionName(Eval("Ma_Phienban"))%></span>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateColumn>
                                                                            <asp:TemplateColumn HeaderText="<%$Resources:cms.language, lblTrangthai%>">
                                                                                <HeaderStyle Width="15%" HorizontalAlign="Center"></HeaderStyle>
                                                                                <ItemStyle Width="15%" HorizontalAlign="Left" CssClass="GridBorderVerSolid"></ItemStyle>
                                                                                <ItemTemplate>
                                                                                    <span class="linkGridForm">
                                                                                        <%#HPCComponents.Global.GetTrangThaiFrom_T_version(Eval("Ma_Tinbai"))%></span>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateColumn>
                                                                        </Columns>
                                                                    </anthem:DataGrid>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </ContentTemplate>
                                                </ajaxToolkit:TabPanel>
                                                <ajaxToolkit:TabPanel HeaderText="<%$Resources:cms.language, lblThungrac%>" ID="TabPanelThungrac"
                                                    runat="server">
                                                    <ContentTemplate>
                                                        <table border="0" style="width: 100%">
                                                            <tr>
                                                                <td>
                                                                    <anthem:DataGrid ID="DataGrid_Thungrac" runat="server" Width="100%" BorderStyle="None"
                                                                        AutoGenerateColumns="False" BorderColor="#d4d4d4" CellPadding="0" DataKeyField="Ma_Tinbai"
                                                                        BackColor="White" CssClass="Grid" BorderWidth="1px" AlternatingItemStyle-BackColor="#F1F1F2"
                                                                        OnEditCommand="DataGrid_EditCommand" OnItemDataBound="DataGrid_OnItemDataBound">
                                                                        <ItemStyle CssClass="GridItem"></ItemStyle>
                                                                        <AlternatingItemStyle CssClass="GridAltItem" />
                                                                        <HeaderStyle CssClass="GridHeader"></HeaderStyle>
                                                                        <Columns>
                                                                            <asp:TemplateColumn>
                                                                                <HeaderStyle HorizontalAlign="Center" Width="4%"></HeaderStyle>
                                                                                <ItemStyle HorizontalAlign="Center" CssClass="GridBorderVerSolid"></ItemStyle>
                                                                                <HeaderTemplate>
                                                                                    <asp:CheckBox ID="chkAll" onclick="javascript:SelectAllCheckboxes(this);" runat="server"
                                                                                        ToolTip="Select all"></asp:CheckBox>
                                                                                </HeaderTemplate>
                                                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                                                <ItemTemplate>
                                                                                    <asp:CheckBox runat="server" Text='' ID='optSelect' CssClass="checkitem" onclick="javascript:CheckItem()"
                                                                                        Enabled='<%# HPCBusinessLogic.Utils.IsEnable(_Role.R_Write,DataBinder.Eval(Container.DataItem, "Nguoi_Khoa").ToString(),_user.UserID)%>'
                                                                                        AutoPostBack="False"></asp:CheckBox>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateColumn>
                                                                            <asp:BoundColumn DataField="Ma_Tinbai" HeaderText="Mã tin bài" Visible="False"></asp:BoundColumn>
                                                                            <asp:TemplateColumn HeaderText="<%$Resources:cms.language, lblTenbaiviet%>">
                                                                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                                                <ItemStyle HorizontalAlign="Left" CssClass="GridBorderVerSolid"></ItemStyle>
                                                                                <ItemTemplate>
                                                                                    <asp:LinkButton CssClass="linkGridForm" Text='<%# DataBinder.Eval(Container.DataItem, "Tieude" )%>'
                                                                                        Enabled='<%# HPCBusinessLogic.Utils.IsEnable(_Role.R_Write,DataBinder.Eval(Container.DataItem, "Nguoi_Khoa").ToString(),_user.UserID)%>'
                                                                                        runat="server" ID="linkTittle" CommandName="Edit" CommandArgument="Edit" ToolTip="Sửa bài"></asp:LinkButton>
                                                                                    <%# HPCBusinessLogic.Utils.LockedUser(DataBinder.Eval(Container.DataItem, "Nguoi_Khoa").ToString(), _user.UserID)%>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateColumn>
                                                                            <asp:TemplateColumn HeaderText="<%$Resources:cms.language, lblAnpham%>">
                                                                                <HeaderStyle HorizontalAlign="Center" Width="8%"></HeaderStyle>
                                                                                <ItemStyle HorizontalAlign="Center" Width="8%" CssClass="GridBorderVerSolid"></ItemStyle>
                                                                                <ItemTemplate>
                                                                                    <span class="linkGridForm">
                                                                                        <%#HPCBusinessLogic.UltilFunc.GetTenAnpham(Eval("Ma_Anpham"))%></span>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateColumn>
                                                                            <asp:TemplateColumn HeaderText="<%$Resources:cms.language, lblTrang%>">
                                                                                <HeaderStyle HorizontalAlign="Center" Width="3%"></HeaderStyle>
                                                                                <ItemStyle HorizontalAlign="Center" Width="3%" CssClass="GridBorderVerSolid"></ItemStyle>
                                                                                <ItemTemplate>
                                                                                    <span class="linkGridForm">
                                                                                        <%#HPCBusinessLogic.UltilFunc.GetTenSoBaoFromT_Vitri_Tinbai(Eval("Ma_Tinbai"),1)%></span>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateColumn>
                                                                            <asp:TemplateColumn HeaderText="<%$Resources:cms.language, lblSotu%>">
                                                                                <HeaderStyle HorizontalAlign="Center" Width="5%"></HeaderStyle>
                                                                                <ItemStyle HorizontalAlign="Center" Width="5%" CssClass="GridBorderVerSolid"></ItemStyle>
                                                                                <ItemTemplate>
                                                                                    <span class="linkGridForm">
                                                                                        <%#Eval("Sotu")%></span>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateColumn>
                                                                            <asp:TemplateColumn HeaderText="<%$Resources:cms.language, lblSotu%>">
                                                                                <HeaderStyle HorizontalAlign="Center" Width="8%"></HeaderStyle>
                                                                                <ItemStyle HorizontalAlign="Center" Width="8%" CssClass="GridBorderVerSolid"></ItemStyle>
                                                                                <ItemTemplate>
                                                                                    <span class="linkGridForm">
                                                                                        <%#HPCBusinessLogic.UltilFunc.GetTenSoBaoFromT_Vitri_Tinbai(Eval("Ma_Tinbai"),0)%></span>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateColumn>
                                                                            <asp:TemplateColumn HeaderText="<%$Resources:cms.language, lblChuyenmuc%>">
                                                                                <HeaderStyle Width="12%" HorizontalAlign="Center"></HeaderStyle>
                                                                                <ItemStyle Width="12%" HorizontalAlign="Center" CssClass="GridBorderVerSolid"></ItemStyle>
                                                                                <ItemTemplate>
                                                                                    <span class="linkGridForm">
                                                                                        <%#HPCBusinessLogic.UltilFunc.GetTenChuyenMuc(Eval("Ma_Chuyenmuc"))%></span>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateColumn>
                                                                            <asp:TemplateColumn HeaderText="<%$Resources:cms.language, lblNguoigui%>">
                                                                                <HeaderStyle HorizontalAlign="Center" Width="10%"></HeaderStyle>
                                                                                <ItemStyle HorizontalAlign="Left" Width="10%" CssClass="GridBorderVerSolid"></ItemStyle>
                                                                                <ItemTemplate>
                                                                                    <span class="linkGridForm">
                                                                                        <%#HPCBusinessLogic.UltilFunc.GetUserFullName(Eval("Ma_Nguoitao"))%></span>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateColumn>
                                                                            <asp:TemplateColumn HeaderText="<%$Resources:cms.language, lblNgaygui%>">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="ngaytao" CssClass="linkGridForm" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Ngaytao")!=System.DBNull.Value?Convert.ToDateTime(DataBinder.Eval(Container, "DataItem.Ngaytao")).ToString("dd/MM/yyyy HH:mm"):"" %>'>
                                                                                    </asp:Label>
                                                                                </ItemTemplate>
                                                                                <HeaderStyle HorizontalAlign="Center" Width="10%"></HeaderStyle>
                                                                                <ItemStyle HorizontalAlign="Center" CssClass="GridBorderVerSolid"></ItemStyle>
                                                                            </asp:TemplateColumn>
                                                                            <asp:TemplateColumn HeaderText="<%$Resources:cms.language, lblXoa%>">
                                                                                <HeaderStyle HorizontalAlign="Center" Width="3%"></HeaderStyle>
                                                                                <ItemStyle HorizontalAlign="Center" Width="3%" CssClass="GridBorderVerSolid"></ItemStyle>
                                                                                <ItemTemplate>
                                                                                    <asp:ImageButton ID="btnDelete" Width="15px" runat="server" ImageUrl="~/Dungchung/images/cancel.gif"
                                                                                        Enabled='<%# HPCBusinessLogic.Utils.IsEnable(_Role.R_Write,DataBinder.Eval(Container.DataItem, "Nguoi_Khoa").ToString(),_user.UserID)%>'
                                                                                        ImageAlign="AbsMiddle" ToolTip="Xóa" CommandName="Edit" CommandArgument="Delete"
                                                                                        BorderStyle="None"></asp:ImageButton>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateColumn>
                                                                            <asp:TemplateColumn HeaderText="<%$Resources:cms.language, lblXem%>">
                                                                                <HeaderStyle HorizontalAlign="Center" Width="3%"></HeaderStyle>
                                                                                <ItemStyle HorizontalAlign="Center" Width="3%" CssClass="GridBorderVerSolid"></ItemStyle>
                                                                                <ItemTemplate>
                                                                                    <a href="Javascript:open_window_Scroll('<%=Global.ApplicationPath%>/Quytrinh/ViewPhienBanTinBai.aspx?Menu_ID=<%=Page.Request.QueryString["Menu_ID"]%>&ID=<%# DataBinder.Eval(Container.DataItem, "Ma_Tinbai") %>',50,500,100,800);" />
                                                                                    <img src='<%=Global.ApplicationPath%>/Dungchung/Images/view.gif' onmouseover="(window.status=''); return true"
                                                                                        style="cursor: hand; border: 0" />
                                                                                </ItemTemplate>
                                                                            </asp:TemplateColumn>
                                                                            <asp:TemplateColumn>
                                                                                <HeaderStyle HorizontalAlign="Center" Width="3%"></HeaderStyle>
                                                                                <ItemStyle HorizontalAlign="Center" Width="3%" CssClass="GridBorderVerSolid"></ItemStyle>
                                                                                <HeaderTemplate>
                                                                                    Preview
                                                                                </HeaderTemplate>
                                                                                <ItemTemplate>
                                                                                    <a href="Javascript:open_window_Scroll('<%=Global.ApplicationPath%>/Quytrinh/ViewPhienBanTinBai.aspx?Menu_ID=<%=Page.Request.QueryString["Menu_ID"]%>&ID=<%# DataBinder.Eval(Container.DataItem, "Ma_Tinbai") %>',50,500,100,800);">
                                                                                        <img src='<%=Global.ApplicationPath%>/Dungchung/Images/view.gif' style="cursor: pointer;
                                                                                            border: 0" alt="Preview" /></a>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateColumn>
                                                                        </Columns>
                                                                    </anthem:DataGrid>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </ContentTemplate>
                                                </ajaxToolkit:TabPanel>
                                                <ajaxToolkit:TabPanel HeaderText="<%$Resources:cms.language, lblMaketpdf%>" ID="TabPanelMaketPDF"
                                                    runat="server">
                                                    <ContentTemplate>
                                                        <asp:DataList ID="DataGrid_FilePDF" runat="server" RepeatColumns="10" RepeatDirection="Horizontal"
                                                            DataKeyField="ID" Width="100%" CellPadding="4">
                                                            <ItemStyle Width="10%" BorderWidth="0" VerticalAlign="top" HorizontalAlign="Left">
                                                            </ItemStyle>
                                                            <ItemTemplate>
                                                                <div style="width: 90%; float: left; text-align: center">
                                                                    <a href="<%=Global.TinPath%><%# DataBinder.Eval(Container.DataItem, "Publish_Pdf")%>"
                                                                        target="_blank">
                                                                        <%#HPCBusinessLogic.UltilFunc.GetPath_PDF(Eval("Page_Number"))%>
                                                                        <img src="../Dungchung/Images/pdf.png" style="border: 0; width: 80px; height: 80px"
                                                                            alt="" />
                                                                    </a>
                                                                </div>
                                                            </ItemTemplate>
                                                        </asp:DataList>
                                                    </ContentTemplate>
                                                </ajaxToolkit:TabPanel>
                                            </ajaxToolkit:TabContainer>
                                        </div>
                                        <div style="text-align: left; float: left; width: 70%; margin-top: 10px; padding: 10px 8px;
                                            display: none" id="divbutton">
                                            <asp:Panel ID="pnlbutton" runat="server">
                                                <table border="0" style="width: 100%">
                                                    <tr>
                                                        <td style="text-align: left; float: left; width: 100%">
                                                            <asp:LinkButton runat="server" ID="LinkDelete" CausesValidation="false" OnClick="Delete_Click"
                                                                OnClientClick=" return confirm('Bạn chắc chắn xóa?');" Text="<%$Resources:cms.language, lblXoa%>"
                                                                CssClass="iconDel"></asp:LinkButton>
                                                            <asp:LinkButton runat="server" ID="LinkBack" CausesValidation="false" OnClick="SendBack_Click"
                                                                OnClientClick=" return confirm('Bạn chắc chắn trả lại?');" Text="<%$Resources:cms.language, lblTralai%>"
                                                                CssClass="iconBack" Visible="False"> </asp:LinkButton>
                                                            <asp:DataList ID="DataListDoiTuong" runat="server" OnItemCommand="DataListDoiTuong_ItemCommand"
                                                                RepeatDirection="horizontal" RepeatColumns="5" RepeatLayout="Flow">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="lnkdoituong" runat="server" CssClass="iconSend" CommandName="cmd"
                                                                        OnClientClick="Demtu();return" CommandArgument='<%# DataBinder.Eval(Container.DataItem,"Ma_DoiTuong")%>'
                                                                        Text='<%# Bind("ThaoTac")%>'>
                                                                    </asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:DataList>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </asp:Panel>
                                        </div>
                                        <div style="text-align: right; float: right" class="pageNav">
                                            <cc1:CurrentPage runat="server" ID="CurrentPage7" CssClass="pageNavTotal">
                                            </cc1:CurrentPage>
                                            <cc1:Pager runat="server" ID="Pager7" OnIndexChanged="Pager_IndexChanged"></cc1:Pager>
                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                        </tr>
                    </table>
                </div>
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
