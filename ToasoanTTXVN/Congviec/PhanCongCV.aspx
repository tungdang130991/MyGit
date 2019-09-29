<%@ Page Language="C#" MasterPageFile="~/Masters/ITC.Master" AutoEventWireup="true"
    CodeBehind="PhanCongCV.aspx.cs" Inherits="ToasoanTTXVN.Congviec.PhanCongCV" Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxtoolkit" %>
<%@ Register TagPrefix="cc1" Namespace="HPCComponents.UI.WebControls" Assembly="HPCComponents" %>
<%@ Import Namespace="HPCComponents" %>
<%@ Register TagPrefix="cc1" Namespace="HPCComponents.UI.WebControls" Assembly="HPCComponents" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <script type="text/javascript">
        $(document).ready(function() {

            $("#<%=txt_denngay.ClientID%>").datepicker({
                showOn: 'button',
                buttonText: 'Show Date',
                buttonImageOnly: true,
                buttonImage: '../Dungchung/Images/DatePicker/calendar.png'
            });
        });
    </script>

    <table border="0" cellpadding="0" width="100%" cellspacing="0">
        <tr>
            <td class="datagrid_top_left">
            </td>
            <td class="datagrid_top_center">
                <span class="TitlePanel" style="float: left;">
                    <%=CommonLib.ReadXML("lblDanhsachcongviec")%></span>
            </td>
            <td class="datagrid_top_right">
            </td>
        </tr>
        <tr>
            <td class="datagrid_content_left">
            </td>
            <td style="text-align: center">
                <table border="0" cellpadding="1" cellspacing="1" style="width: 100%; text-align: left;">
                    <tr>
                        <td style="width: 8%; text-align: right" class="Titlelbl">
                            <span>
                                <%=CommonLib.ReadXML("lblPhongban")%></span>
                        </td>
                        <td style="width: 20%; text-align: left">
                            <asp:UpdatePanel ID="upnlroom" runat="server">
                                <ContentTemplate>
                                    <asp:DropDownList ID="cbo_room" runat="server" AutoPostBack="true" CssClass="inputtext"
                                        Width="95%" OnSelectedIndexChanged="cbo_room_OnSelectedIndexChanged">
                                    </asp:DropDownList>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                        <td style="width: 8%; text-align: right" class="Titlelbl">
                            <asp:UpdatePanel ID="lblnguoidung" runat="server">
                                <ContentTemplate>
                                    <asp:Label ID="lblUser" runat="server" Text=''></asp:Label></ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                        <td style="width: 20%; text-align: right">
                            <asp:UpdatePanel ID="UpnlNguoinhan" runat="server">
                                <ContentTemplate>
                                    <asp:DropDownList ID="cbo_nguoinhan" runat="server" CssClass="inputtext" Width="95%">
                                    </asp:DropDownList>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                        <td style="width: 8%; text-align: right" class="Titlelbl">
                            <span>
                                <%=CommonLib.ReadXML("lblNgayhoanthanh")%>:</span>
                        </td>
                        <td style="width: 10%; text-align: left">
                            <asp:TextBox ID="txt_denngay" runat="server" Width="110px" CssClass="inputtext" MaxLength="10"
                                ToolTip="Đến ngày" onkeypress="AscciiDisable()" onfocus="javascript:vDateType='3'"
                                onkeyup="DateFormat(this,this.value,event,false,'3')" onblur="DateFormat(this,this.value,event,true,'3')"></asp:TextBox>
                            <asp:RegularExpressionValidator ID="Regularexpressionvalidator1" runat="server" ControlToValidate="txt_denngay"
                                ValidationExpression="^\d{1,2}/\d{1,2}/\d{4}$" Display="Dynamic">Kiểu ngày:dd/MM/yyyy</asp:RegularExpressionValidator>
                        </td>
                        <td style="text-align: left; width: 15%">
                            <asp:Button runat="server" ID="btnSearch" CssClass="iconFind" OnClick="Search_Click"
                                Text="<%$Resources:cms.language, lblTimkiem%>"></asp:Button>
                            <asp:Button runat="server" ID="btnAdd" CssClass="iconAdd" OnClick="btnAdd_Click"
                                Text="<%$Resources:cms.language, lblGiaoviec%>"></asp:Button>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 100%" align="left" colspan="7">
                            <asp:UpdatePanel ID="UpdatePanelTabContainer" runat="server">
                                <ContentTemplate>
                                    <div style="text-align: left; width: 100%; vertical-align: top">
                                        <ajaxtoolkit:TabContainer ID="TabContainerListCV" runat="server" CssClass="ajax__tab_yuitabview-theme"
                                            AutoPostBack="true" ActiveTabIndex="0" OnActiveTabChanged="TabContainer1_ActiveTabChanged">
                                            <ajaxtoolkit:TabPanel HeaderText="<%$Resources:cms.language, lblCongviecduocgiao%>"
                                                ID="tabpnlCVCanlam" runat="server">
                                                <ContentTemplate>
                                                    <div>
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
                                                                <asp:TemplateColumn HeaderText="<%$Resources:cms.language, lblSTT%>">
                                                                    <HeaderStyle HorizontalAlign="Center" Width="5%"></HeaderStyle>
                                                                    <ItemStyle HorizontalAlign="Center" Width="5%" CssClass="GridBorderVerSolid"></ItemStyle>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblSTT" runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateColumn>
                                                                <asp:TemplateColumn HeaderText="<%$Resources:cms.language, lblTrangthai%>">
                                                                    <HeaderStyle HorizontalAlign="Center" Width="5%"></HeaderStyle>
                                                                    <ItemStyle HorizontalAlign="Center" Width="5%" CssClass="GridBorderVerSolid"></ItemStyle>
                                                                    <ItemTemplate>
                                                                        <asp:ImageButton ID="imgStatus" runat="server" BorderStyle="None" ImageAlign="AbsMiddle"
                                                                            CommandName="Edit" CommandArgument="IsEdit" AutoUpdateAfterCallBack="true" />
                                                                        <asp:Label ID="lblStatus" runat="server" Visible="false" Text='<%# Eval("Status")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateColumn>
                                                                <asp:TemplateColumn HeaderText="<%$Resources:cms.language, lblCongviec%>">
                                                                    <HeaderStyle HorizontalAlign="Center" />
                                                                    <ItemStyle Width="30%" HorizontalAlign="Left" CssClass="GridBorderVerSolid"></ItemStyle>
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton ID="btnEdit" runat="server" CssClass="linkGridForm" Text='<%# DataBinder.Eval(Container.DataItem, "Tencongviec")%>'
                                                                            ToolTip="Xem nội dung CV" CommandName="Edit" CommandArgument="Edit"></asp:LinkButton>
                                                                    </ItemTemplate>
                                                                </asp:TemplateColumn>
                                                                <asp:TemplateColumn HeaderText="<%$Resources:cms.language, lblPhanhoi%>">
                                                                    <HeaderStyle HorizontalAlign="Center" Width="20%"></HeaderStyle>
                                                                    <ItemStyle HorizontalAlign="Left" Width="20%" CssClass="GridBorderVerSolid"></ItemStyle>
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="txt_phanhoi" runat="server" Width="95%" CssClass="inputtext" Text='<%#Eval("Vet")%>'
                                                                            TextMode="MultiLine" Rows="2"></asp:TextBox>
                                                                    </ItemTemplate>
                                                                </asp:TemplateColumn>
                                                                <asp:TemplateColumn HeaderText="<%$Resources:cms.language, lblPhongban_nguoinhan%>">
                                                                    <HeaderStyle HorizontalAlign="Center" Width="12%"></HeaderStyle>
                                                                    <ItemStyle HorizontalAlign="Left" Width="12%" CssClass="GridBorderVerSolid"></ItemStyle>
                                                                    <ItemTemplate>
                                                                        <span class="linkGridForm" style="font-weight: bold">
                                                                            <%#HPCBusinessLogic.UltilFunc.GetTenPhongBan(DataBinder.Eval(Container.DataItem, "Phong_ID").ToString())%></span><br />
                                                                        <span class="linkGridForm">
                                                                            <%#HPCBusinessLogic.UltilFunc.GetUserFullName(DataBinder.Eval(Container.DataItem, "NguoiNhan").ToString())%></span>
                                                                    </ItemTemplate>
                                                                </asp:TemplateColumn>
                                                                <asp:TemplateColumn HeaderText="<%$Resources:cms.language, lblNguoigiao%>">
                                                                    <HeaderStyle HorizontalAlign="Center" Width="12%"></HeaderStyle>
                                                                    <ItemStyle HorizontalAlign="Left" Width="12%" CssClass="GridBorderVerSolid"></ItemStyle>
                                                                    <ItemTemplate>
                                                                        <span class="linkGridForm">
                                                                            <%#HPCBusinessLogic.UltilFunc.GetUserFullName(DataBinder.Eval(Container.DataItem, "NguoiGiaoViec").ToString())%></span>
                                                                    </ItemTemplate>
                                                                </asp:TemplateColumn>
                                                                <asp:TemplateColumn HeaderText="<%$Resources:cms.language, lblNgayhoanthanh%>">
                                                                    <HeaderStyle HorizontalAlign="Center" Width="10%"></HeaderStyle>
                                                                    <ItemStyle HorizontalAlign="Center" Width="10%" CssClass="GridBorderVerSolid"></ItemStyle>
                                                                    <ItemTemplate>
                                                                        <asp:Label class="linkGridForm" ID="lblNgayHoanthanh" runat="server" Text='<%#Eval("NgayHoanthanh") != System.DBNull.Value ? Convert.ToDateTime(Eval("NgayHoanthanh")).ToString("dd/MM/yyyy") : ""%>'>
                                                                        </asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateColumn>
                                                                <asp:TemplateColumn HeaderText="<%$Resources:cms.language, lblHoanthanh%>">
                                                                    <HeaderStyle HorizontalAlign="Center" Width="5%"></HeaderStyle>
                                                                    <ItemStyle HorizontalAlign="Center" Width="5%" CssClass="GridBorderVerSolid"></ItemStyle>
                                                                    <ItemTemplate>
                                                                        <asp:ImageButton ID="btnfinish" Width="25px" runat="server" ImageUrl='<%#StatusComplete(DataBinder.Eval(Container.DataItem, "Status"))%>'
                                                                            ImageAlign="AbsMiddle" ToolTip="Hoàn thành CV" CommandName="Edit" CommandArgument="FinishCV"
                                                                            BorderStyle="None"></asp:ImageButton>
                                                                    </ItemTemplate>
                                                                </asp:TemplateColumn>
                                                                <asp:TemplateColumn HeaderText="<%$Resources:cms.language, lblNhanviec%>">
                                                                    <HeaderStyle HorizontalAlign="Center" Width="5%"></HeaderStyle>
                                                                    <ItemStyle HorizontalAlign="Center" Width="5%" CssClass="GridBorderVerSolid"></ItemStyle>
                                                                    <ItemTemplate>
                                                                        <asp:ImageButton ID="btnAcept" Width="15px" runat="server" ImageUrl="~/Dungchung/Images/plainbutton.gif"
                                                                            ImageAlign="AbsMiddle" ToolTip="Xóa thông tin chuyên mục" CommandName="Edit"
                                                                            CommandArgument="Nhanviec" BorderStyle="None"></asp:ImageButton>
                                                                    </ItemTemplate>
                                                                </asp:TemplateColumn>
                                                                <asp:TemplateColumn HeaderText="<%$Resources:cms.language, lblhuyviec%>">
                                                                    <HeaderStyle HorizontalAlign="Center" Width="5%"></HeaderStyle>
                                                                    <ItemStyle HorizontalAlign="Center" Width="5%" CssClass="GridBorderVerSolid"></ItemStyle>
                                                                    <ItemTemplate>
                                                                        <asp:ImageButton ID="btnDelete" Width="15px" runat="server" ImageUrl="~/Dungchung/Images/cancel.gif"
                                                                            ImageAlign="AbsMiddle" ToolTip="Hủy công việc" CommandName="Edit" CommandArgument="CancelCV"
                                                                            BorderStyle="None"></asp:ImageButton>
                                                                    </ItemTemplate>
                                                                </asp:TemplateColumn>
                                                            </Columns>
                                                        </asp:DataGrid>
                                                    </div>
                                                </ContentTemplate>
                                            </ajaxtoolkit:TabPanel>
                                            <ajaxtoolkit:TabPanel HeaderText="<%$Resources:cms.language, lblCongviecdagiao%>"
                                                ID="tabCVTheodoi" runat="server">
                                                <ContentTemplate>
                                                    <div>
                                                        <asp:DataGrid runat="server" ID="grdListTheodoi" AutoGenerateColumns="false" DataKeyField="Ma_Congviec"
                                                            Width="100%" CssClass="Grid" CellPadding="1" OnEditCommand="grdListCVCanLam_EditCommand"
                                                            OnItemDataBound="grdListTheodoi_ItemDataBound">
                                                            <ItemStyle CssClass="GridItem" Height="25px"></ItemStyle>
                                                            <AlternatingItemStyle CssClass="GridAltItem" />
                                                            <HeaderStyle CssClass="GridHeader"></HeaderStyle>
                                                            <Columns>
                                                                <asp:BoundColumn Visible="False" DataField="Ma_Congviec">
                                                                    <HeaderStyle Width="1%"></HeaderStyle>
                                                                </asp:BoundColumn>
                                                                <asp:TemplateColumn HeaderText="<%$Resources:cms.language, lblSTT%>">
                                                                    <HeaderStyle HorizontalAlign="Center" Width="5%"></HeaderStyle>
                                                                    <ItemStyle HorizontalAlign="Center" Width="5%" CssClass="GridBorderVerSolid"></ItemStyle>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblSTT" runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateColumn>
                                                                <asp:TemplateColumn HeaderText="<%$Resources:cms.language, lblTrangthai%>">
                                                                    <HeaderStyle HorizontalAlign="Center" Width="5%"></HeaderStyle>
                                                                    <ItemStyle HorizontalAlign="Center" Width="5%" CssClass="GridBorderVerSolid"></ItemStyle>
                                                                    <ItemTemplate>
                                                                        <asp:ImageButton ID="imgStatus" runat="server" BorderStyle="None" ImageAlign="AbsMiddle" />
                                                                        <asp:Label ID="lblStatus" runat="server" Visible="false" Text='<%# Eval("Status")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateColumn>
                                                                <asp:TemplateColumn HeaderText="<%$Resources:cms.language, lblCongviec%>">
                                                                    <HeaderStyle HorizontalAlign="Center" />
                                                                    <ItemStyle Width="30%" HorizontalAlign="Left" CssClass="GridBorderVerSolid"></ItemStyle>
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton ID="btnEdit" runat="server" CssClass="linkGridForm" Text='<%# DataBinder.Eval(Container.DataItem, "Tencongviec")%>'
                                                                            CommandName="Edit" CommandArgument="Edit" Enabled='<%# HPCBusinessLogic.Utils.IsEnable(_Role.R_Write,"0",_user.UserID)%>'></asp:LinkButton>
                                                                    </ItemTemplate>
                                                                </asp:TemplateColumn>
                                                                <asp:TemplateColumn HeaderText="<%$Resources:cms.language, lblPhanhoi%>">
                                                                    <HeaderStyle HorizontalAlign="Center" Width="30%"></HeaderStyle>
                                                                    <ItemStyle HorizontalAlign="Left" Width="30%" CssClass="GridBorderVerSolid"></ItemStyle>
                                                                    <ItemTemplate>
                                                                        <span class="linkGridForm" style="font-weight: bold">
                                                                            <%#DataBinder.Eval(Container.DataItem, "TenNguoiNhan")%></span>
                                                                        <br />
                                                                        <div style="float: left; width: 85%">
                                                                            <asp:TextBox ID="txt_phanhoi" runat="server" Width="98%" CssClass="inputtext" Text='<%#Eval("Vet")%>'
                                                                                TextMode="MultiLine" Rows="2"></asp:TextBox></div>
                                                                        <div style="float: left; width: 15%; padding-top: 18px; text-align: center">
                                                                            <asp:ImageButton ID="btnPhanhoi" Width="25px" runat="server" ImageUrl="~/Dungchung/Images/Button-Back.png"
                                                                                ImageAlign="AbsMiddle" ToolTip="Phản hồi lại" CommandName="Edit" CommandArgument="Phanhoi"
                                                                                BorderStyle="None"></asp:ImageButton></div>
                                                                    </ItemTemplate>
                                                                </asp:TemplateColumn>
                                                                <asp:TemplateColumn HeaderText="<%$Resources:cms.language, lblPhongban_nguoinhan%>">
                                                                    <HeaderStyle HorizontalAlign="Center" Width="12%"></HeaderStyle>
                                                                    <ItemStyle HorizontalAlign="Left" Width="12%" CssClass="GridBorderVerSolid"></ItemStyle>
                                                                    <ItemTemplate>
                                                                        <span class="linkGridForm" style="font-weight: bold">
                                                                            <%#HPCBusinessLogic.UltilFunc.GetTenPhongBan(DataBinder.Eval(Container.DataItem, "Phong_ID").ToString())%></span><br />
                                                                        <span class="linkGridForm">
                                                                            <%#HPCBusinessLogic.UltilFunc.GetUserFullName(DataBinder.Eval(Container.DataItem, "NguoiNhan").ToString())%></span>
                                                                    </ItemTemplate>
                                                                </asp:TemplateColumn>
                                                                <asp:TemplateColumn HeaderText="<%$Resources:cms.language, lblNgayhoanthanh%>">
                                                                    <HeaderStyle HorizontalAlign="Center" Width="10%"></HeaderStyle>
                                                                    <ItemStyle HorizontalAlign="Center" Width="10%" CssClass="GridBorderVerSolid"></ItemStyle>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblNgayHoanthanh" CssClass="linkGridForm" runat="server" Text='<%#Eval("NgayHoanthanh") != System.DBNull.Value ? Convert.ToDateTime(Eval("NgayHoanthanh")).ToString("dd/MM/yyyy") : ""%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateColumn>
                                                                <asp:TemplateColumn HeaderText="<%$Resources:cms.language, lblXoa%>">
                                                                    <HeaderStyle HorizontalAlign="Center" Width="5%"></HeaderStyle>
                                                                    <ItemStyle HorizontalAlign="Center" Width="5%" CssClass="GridBorderVerSolid"></ItemStyle>
                                                                    <ItemTemplate>
                                                                        <asp:ImageButton ID="btnDelete" Width="15px" runat="server" ImageUrl="~/Dungchung/Images/cancel.gif"
                                                                            ImageAlign="AbsMiddle" ToolTip="Xóa thông tin chuyên mục" CommandName="Edit"
                                                                            CommandArgument="Delete" BorderStyle="None"></asp:ImageButton>
                                                                    </ItemTemplate>
                                                                </asp:TemplateColumn>
                                                            </Columns>
                                                        </asp:DataGrid>
                                                    </div>
                                                </ContentTemplate>
                                            </ajaxtoolkit:TabPanel>
                                        </ajaxtoolkit:TabContainer>
                                    </div>
                                    <div style="text-align: right; float: right" class="pageNav">
                                        <cc1:CurrentPage runat="server" ID="curentPages"></cc1:CurrentPage>&nbsp;
                                        <cc1:Pager runat="server" ID="pages" OnIndexChanged="pages_IndexChanged" />
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
</asp:Content>
