<%@ Page Language="C#" MasterPageFile="~/Masters/ITC.Master" AutoEventWireup="true"
    CodeBehind="ListCongviec.aspx.cs" Inherits="ToasoanTTXVN.Congviec.ListCongviec"
    Title="" %>

<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral,PublicKeyToken=31BF3856AD364E35" %>
<%@ Register TagPrefix="cc1" Namespace="HPCComponents.UI.WebControls" Assembly="HPCComponents" %>
<%@ Import Namespace="HPCComponents" %>
<%@ Register TagPrefix="nbc" Namespace="HPCComponents.UI" Assembly="HPCComponents" %>
<%@ Register TagPrefix="anthem" Namespace="Anthem" Assembly="Anthem" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<%@ Register TagPrefix="cc1" Namespace="HPCComponents.UI.WebControls" Assembly="HPCComponents" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <table border="0" cellpadding="0" width="100%" cellspacing="0">
        <tr>
            <td class="datagrid_top_left">
            </td>
            <td class="datagrid_top_center">
                <span class="TitlePanel" style="float: left;">DANH SÁCH CÔNG VIỆC</span>
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
                                    <td style="width: 90%; text-align: right" class="Titlelbl">
                                        <asp:Label ID="lblUser" runat="server" Text=''></asp:Label>&nbsp;
                                        <asp:DropDownList ID="ddl_User" runat="server" Width="250px" CssClass="inputtext"
                                            DataTextField="UserFullName" DataValueField="UserID">
                                        </asp:DropDownList>
                                        &nbsp; Ngày tạo&nbsp;
                                        <nbc:NetDatePicker CssClass="inputtext" ImageUrl="../Dungchung/Images/events.gif"
                                            ImageFolder="../Dungchung/scripts/DatePicker/Images" Height="16px" Width="150px"
                                            ScriptSource="../Dungchung/scripts/datepicker.js" ID="txt_NgayTao" runat="server"
                                            onkeypress="AscciiDisable()" onfocus="javascript:vDateType='3'" onkeyup="DateFormat(this,this.value,event,false,'3')"
                                            onblur="DateFormat(this,this.value,event,true,'3')" MaxLength="10">
                                        </nbc:NetDatePicker>
                                        <asp:RegularExpressionValidator ID="Regularexpressionvalidator3" runat="server" ControlToValidate="txt_NgayTao"
                                            ValidationExpression="^\d{1,2}/\d{1,2}/\d{4}$" Display="Dynamic">Kiểu ngày:dd/MM/yyyy</asp:RegularExpressionValidator>
                                        &nbsp; Ngày hoàn thành&nbsp;
                                        <nbc:NetDatePicker CssClass="inputtext" ImageUrl="../Dungchung/Images/events.gif"
                                            ImageFolder="../Dungchung/scripts/DatePicker/Images" Height="16px" Width="150px"
                                            ScriptSource="../Dungchung/scripts/datepicker.js" ID="txt_Ngayhoanthanh" runat="server"
                                            onkeypress="AscciiDisable()" onfocus="javascript:vDateType='3'" onkeyup="DateFormat(this,this.value,event,false,'3')"
                                            onblur="DateFormat(this,this.value,event,true,'3')" MaxLength="10">
                                        </nbc:NetDatePicker>
                                        <asp:RegularExpressionValidator ID="Regularexpressionvalidator1" runat="server" ControlToValidate="txt_Ngayhoanthanh"
                                            ValidationExpression="^\d{1,2}/\d{1,2}/\d{4}$" Display="Dynamic">Kiểu ngày:dd/MM/yyyy</asp:RegularExpressionValidator>&nbsp;
                                    </td>
                                    <td style="text-align: left; width: 10%;">
                                        <asp:Button runat="server" ID="btnSearch" CssClass="myButton blue" Font-Bold="true"
                                            OnClick="Search_Click" Text="Tìm kiếm"></asp:Button>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="height: 10px" colspan="2">
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 100%;" align="left" colspan="2">
                                        <cc2:TabContainer ID="TabContainerListCV" runat="server" CssClass="ajax__tab_yuitabview-theme"
                                            AutoPostBack="true" ActiveTabIndex="0" OnActiveTabChanged="TabContainer1_ActiveTabChanged">
                                            <cc2:TabPanel HeaderText="Công việc cần làm" ID="tabpnlCVCanlam" runat="server">
                                                <ContentTemplate>
                                                    <asp:UpdatePanel ID="UpdatePanelListCV" runat="server">
                                                        <Triggers>
                                                            <asp:PostBackTrigger ControlID="grdListCVCanLam" />
                                                        </Triggers>
                                                        <ContentTemplate>
                                                            <table border="0" style="width: 100%">
                                                                <tr>
                                                                    <td>
                                                                        <asp:DataGrid runat="server" ID="grdListCVCanLam" AutoGenerateColumns="false" DataKeyField="Ma_Congviec"
                                                                            Width="100%" CssClass="Grid" CellPadding="1" OnEditCommand="grdListCVCanLam_EditCommand"
                                                                            OnItemDataBound="grdListCVCanLam_ItemDataBound">
                                                                            <ItemStyle CssClass="GridItem" Height="25px"></ItemStyle>
                                                                            <AlternatingItemStyle CssClass="GridAltItem" />
                                                                            <HeaderStyle CssClass="GridHeader"></HeaderStyle>
                                                                            <Columns>
                                                                                <asp:BoundColumn Visible="False" DataField="Ma_Congviec">
                                                                                    <HeaderStyle Width="1%"></HeaderStyle>
                                                                                </asp:BoundColumn>
                                                                                <asp:TemplateColumn>
                                                                                    <HeaderStyle HorizontalAlign="Center" Width="5%"></HeaderStyle>
                                                                                    <ItemStyle HorizontalAlign="Center" Width="5%" CssClass="GridBorderVerSolid"></ItemStyle>
                                                                                    <HeaderTemplate>
                                                                                        STT
                                                                                    </HeaderTemplate>
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblSTT" runat="server"></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateColumn>
                                                                                <asp:TemplateColumn>
                                                                                    <HeaderStyle HorizontalAlign="Center" Width="5%"></HeaderStyle>
                                                                                    <ItemStyle HorizontalAlign="Center" Width="5%" CssClass="GridBorderVerSolid"></ItemStyle>
                                                                                    <HeaderTemplate>
                                                                                    </HeaderTemplate>
                                                                                    <ItemTemplate>
                                                                                        <anthem:ImageButton ID="imgStatus" runat="server" BorderStyle="None" ImageAlign="AbsMiddle"
                                                                                            CommandName="Edit" CommandArgument="IsEdit" AutoUpdateAfterCallBack="true" />
                                                                                        <asp:Label ID="lblStatus" runat="server" Visible="false" Text='<%# Eval("Status")%>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateColumn>
                                                                                <asp:TemplateColumn>
                                                                                    <HeaderTemplate>
                                                                                        Công việc
                                                                                    </HeaderTemplate>
                                                                                    <HeaderStyle HorizontalAlign="Center" />
                                                                                    <ItemStyle Width="50%" HorizontalAlign="Left" CssClass="GridBorderVerSolid"></ItemStyle>
                                                                                    <ItemTemplate>
                                                                                        <%# DataBinder.Eval(Container.DataItem, "TenCongviec")%>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateColumn>
                                                                                <asp:TemplateColumn>
                                                                                    <HeaderStyle HorizontalAlign="Center" Width="5%"></HeaderStyle>
                                                                                    <ItemStyle HorizontalAlign="Center" Width="5%" CssClass="GridBorderVerSolid"></ItemStyle>
                                                                                    <HeaderTemplate>
                                                                                        Số từ
                                                                                    </HeaderTemplate>
                                                                                    <ItemTemplate>
                                                                                        <%# DataBinder.Eval(Container.DataItem, "Sotu")%>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateColumn>
                                                                                <asp:TemplateColumn>
                                                                                    <HeaderStyle HorizontalAlign="Center" Width="15%"></HeaderStyle>
                                                                                    <ItemStyle HorizontalAlign="Center" Width="15%" CssClass="GridBorderVerSolid"></ItemStyle>
                                                                                    <HeaderTemplate>
                                                                                        Người giao việc
                                                                                    </HeaderTemplate>
                                                                                    <ItemTemplate>
                                                                                        <%#BindUserName(DataBinder.Eval(Container.DataItem, "NguoiGiaoViec").ToString())%>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateColumn>
                                                                                <asp:TemplateColumn>
                                                                                    <HeaderStyle HorizontalAlign="Center" Width="10%"></HeaderStyle>
                                                                                    <ItemStyle HorizontalAlign="Center" Width="10%" CssClass="GridBorderVerSolid"></ItemStyle>
                                                                                    <HeaderTemplate>
                                                                                        Ngày tạo
                                                                                    </HeaderTemplate>
                                                                                    <ItemTemplate>
                                                                                        <%#Eval("NgayTao") != System.DBNull.Value ? Convert.ToDateTime(Eval("NgayTao")).ToString("dd/MM/yyyy") : ""%>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateColumn>
                                                                                <asp:TemplateColumn>
                                                                                    <HeaderStyle HorizontalAlign="Center" Width="10%"></HeaderStyle>
                                                                                    <ItemStyle HorizontalAlign="Center" Width="10%" CssClass="GridBorderVerSolid"></ItemStyle>
                                                                                    <HeaderTemplate>
                                                                                        Ngày hoàn thành
                                                                                    </HeaderTemplate>
                                                                                    <ItemTemplate>
                                                                                        <%#Eval("NgayHoanthanh") != System.DBNull.Value ? Convert.ToDateTime(Eval("NgayHoanthanh")).ToString("dd/MM/yyyy") : ""%>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateColumn>
                                                                                <asp:TemplateColumn Visible="false">
                                                                                    <HeaderStyle HorizontalAlign="Center" Width="5%"></HeaderStyle>
                                                                                    <ItemStyle HorizontalAlign="Center" Width="5%" CssClass="GridBorderVerSolid"></ItemStyle>
                                                                                    <HeaderTemplate>
                                                                                        Xóa
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
                                                                        <cc1:Pager runat="server" ID="pages" OnIndexChanged="pages_IndexChangedCVCanlam" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </ContentTemplate>
                                            </cc2:TabPanel>
                                            <cc2:TabPanel HeaderText="Công việc cần theo dõi" ID="tabCVTheodoi" runat="server">
                                                <ContentTemplate>
                                                    <asp:UpdatePanel ID="UpdatePanelCVTheodoi" runat="server">
                                                        <Triggers>
                                                            <asp:PostBackTrigger ControlID="grdListTheodoi" />
                                                        </Triggers>
                                                        <ContentTemplate>
                                                            <table border="0" style="width: 100%">
                                                                <tr>
                                                                    <td>
                                                                        <asp:DataGrid runat="server" ID="grdListTheodoi" AutoGenerateColumns="false" DataKeyField="Ma_Congviec"
                                                                            Width="100%" CssClass="Grid" CellPadding="1" OnItemDataBound="grdListTheodoi_ItemDataBound">
                                                                            <ItemStyle CssClass="GridItem" Height="25px"></ItemStyle>
                                                                            <AlternatingItemStyle CssClass="GridAltItem" />
                                                                            <HeaderStyle CssClass="GridHeader"></HeaderStyle>
                                                                            <Columns>
                                                                                <asp:BoundColumn Visible="False" DataField="Ma_Congviec">
                                                                                    <HeaderStyle Width="1%"></HeaderStyle>
                                                                                </asp:BoundColumn>
                                                                                <asp:TemplateColumn>
                                                                                    <HeaderStyle HorizontalAlign="Center" Width="5%"></HeaderStyle>
                                                                                    <ItemStyle HorizontalAlign="Center" Width="5%" CssClass="GridBorderVerSolid"></ItemStyle>
                                                                                    <HeaderTemplate>
                                                                                        STT
                                                                                    </HeaderTemplate>
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblSTT" runat="server"></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateColumn>
                                                                                <asp:TemplateColumn>
                                                                                    <HeaderStyle HorizontalAlign="Center" Width="5%"></HeaderStyle>
                                                                                    <ItemStyle HorizontalAlign="Center" Width="5%" CssClass="GridBorderVerSolid"></ItemStyle>
                                                                                    <HeaderTemplate>
                                                                                    </HeaderTemplate>
                                                                                    <ItemTemplate>
                                                                                        <anthem:ImageButton ID="imgStatus" runat="server" BorderStyle="None" ImageAlign="AbsMiddle" />
                                                                                        <asp:Label ID="lblStatus" runat="server" Visible="false" Text='<%# Eval("Status")%>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateColumn>
                                                                                <asp:TemplateColumn>
                                                                                    <HeaderTemplate>
                                                                                        Công việc
                                                                                    </HeaderTemplate>
                                                                                    <HeaderStyle HorizontalAlign="Center" />
                                                                                    <ItemStyle Width="50%" HorizontalAlign="Left" CssClass="GridBorderVerSolid"></ItemStyle>
                                                                                    <ItemTemplate>
                                                                                        <%# DataBinder.Eval(Container.DataItem, "Tencongviec")%>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateColumn>
                                                                                <asp:TemplateColumn>
                                                                                    <HeaderStyle HorizontalAlign="Center" Width="5%"></HeaderStyle>
                                                                                    <ItemStyle HorizontalAlign="Center" Width="5%" CssClass="GridBorderVerSolid"></ItemStyle>
                                                                                    <HeaderTemplate>
                                                                                        Số từ
                                                                                    </HeaderTemplate>
                                                                                    <ItemTemplate>
                                                                                        <%# DataBinder.Eval(Container.DataItem, "Sotu")%>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateColumn>
                                                                                <asp:TemplateColumn>
                                                                                    <HeaderStyle HorizontalAlign="Center" Width="15%"></HeaderStyle>
                                                                                    <ItemStyle HorizontalAlign="Center" Width="15%" CssClass="GridBorderVerSolid"></ItemStyle>
                                                                                    <HeaderTemplate>
                                                                                        Người nhận
                                                                                    </HeaderTemplate>
                                                                                    <ItemTemplate>
                                                                                        <%#BindUserName(DataBinder.Eval(Container.DataItem, "NguoiNhan").ToString())%>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateColumn>
                                                                                <asp:TemplateColumn>
                                                                                    <HeaderStyle HorizontalAlign="Center" Width="10%"></HeaderStyle>
                                                                                    <ItemStyle HorizontalAlign="Center" Width="10%" CssClass="GridBorderVerSolid"></ItemStyle>
                                                                                    <HeaderTemplate>
                                                                                        Ngày tạo
                                                                                    </HeaderTemplate>
                                                                                    <ItemTemplate>
                                                                                        <%#Eval("NgayTao") != System.DBNull.Value ? Convert.ToDateTime(Eval("NgayTao")).ToString("dd/MM/yyyy") : ""%>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateColumn>
                                                                                <asp:TemplateColumn>
                                                                                    <HeaderStyle HorizontalAlign="Center" Width="10%"></HeaderStyle>
                                                                                    <ItemStyle HorizontalAlign="Center" Width="10%" CssClass="GridBorderVerSolid"></ItemStyle>
                                                                                    <HeaderTemplate>
                                                                                        Ngày hoàn thành
                                                                                    </HeaderTemplate>
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblNgayHoanthanh" runat="server" Text='  <%#Eval("NgayHoanthanh") != System.DBNull.Value ? Convert.ToDateTime(Eval("NgayHoanthanh")).ToString("dd/MM/yyyy") : ""%>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateColumn>
                                                                                <asp:TemplateColumn Visible="false">
                                                                                    <HeaderStyle HorizontalAlign="Center" Width="5%"></HeaderStyle>
                                                                                    <ItemStyle HorizontalAlign="Center" Width="5%" CssClass="GridBorderVerSolid"></ItemStyle>
                                                                                    <HeaderTemplate>
                                                                                        Xóa
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
                                                                    <td class="pageNav" style="text-align: right">
                                                                        <cc1:CurrentPage runat="server" ID="CurrentPage1" CssClass="pageNavTotal"></cc1:CurrentPage>
                                                                        <cc1:Pager runat="server" ID="pages1" OnIndexChanged="pages_IndexChanged_Theodoi"></cc1:Pager>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </ContentTemplate>
                                            </cc2:TabPanel>
                                        </cc2:TabContainer>
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
