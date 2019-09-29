<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/ITC.Master" AutoEventWireup="true"
    CodeBehind="Baocaolichsuthaotachethong.aspx.cs" Inherits="ToasoanTTXVN.Baocaothongke.Baocaolichsuthaotachethong" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxtoolkit" %>
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
                <table id="tableall" cellpadding="2" cellspacing="2" border="0" width="100%">
                    <tr>
                        <td style="width: 8%; text-align: right" class="Titlelbl">
                            Từ ngày:
                        </td>
                        <td style="width: 10%; text-align: left">
                            <asp:TextBox ID="txt_tungay" runat="server" Width="120px" CssClass="inputtext" MaxLength="10"
                                onkeypress="AscciiDisable()" onfocus="javascript:vDateType='3'" onkeyup="DateFormat(this,this.value,event,false,'3')"
                                onblur="DateFormat(this,this.value,event,true,'3')"></asp:TextBox>
                            <asp:RegularExpressionValidator ID="Regularexpressionvalidator3" runat="server" ControlToValidate="txt_tungay"
                                ValidationExpression="^\d{1,2}/\d{1,2}/\d{4}$" Display="Dynamic">Kiểu ngày:dd/MM/yyyy</asp:RegularExpressionValidator>
                        </td>
                        <td style="width: 8%; text-align: right" class="Titlelbl">
                            Đến ngày:
                        </td>
                        <td style="width: 10%; text-align: left">
                            <asp:TextBox ID="txt_denngay" runat="server" Width="120px" CssClass="inputtext" MaxLength="10"
                                onkeypress="AscciiDisable()" onfocus="javascript:vDateType='3'" onkeyup="DateFormat(this,this.value,event,false,'3')"
                                onblur="DateFormat(this,this.value,event,true,'3')"></asp:TextBox>
                            <asp:RegularExpressionValidator ID="Regularexpressionvalidator1" runat="server" ControlToValidate="txt_denngay"
                                ValidationExpression="^\d{1,2}/\d{1,2}/\d{4}$" Display="Dynamic">Kiểu ngày:dd/MM/yyyy</asp:RegularExpressionValidator>
                        </td>
                        <td style="width: 8%; text-align: right" class="Titlelbl">
                            Người dùng:
                        </td>
                        <td style="width: 20%; text-align: left">
                            <asp:TextBox ID="txt_tennguoidung" runat="server" Width="95%" CssClass="inputtext"
                                onkeypress="return clickButton(event,'ctl00_MainContent_btnTimkiem');"></asp:TextBox>
                        </td>
                        <td style="width: 10%; text-align: left; padding-left: 10px">
                            <asp:UpdatePanel ID="UpdatePanelTimKiem" runat="server">
                                <ContentTemplate>
                                    <asp:Button runat="server" ID="btnTimkiem" CssClass="iconFind" Font-Bold="true" OnClick="btnTimkiem_Click"
                                        Text="Tìm kiếm"></asp:Button>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="7">
                            <asp:UpdatePanel ID="UpdatePanelListTinbai" runat="server">
                                <ContentTemplate>
                                    <table width="100%" cellspacing="0" cellpadding="0" border="0">
                                        <tr>
                                            <td valign="top" style="text-align: left; width: 100%">
                                                <table border="0" style="width: 100%">
                                                    <tr>
                                                        <td valign="top" style="text-align: left; width: 100%">
                                                            <asp:DataGrid ID="DataGrid_ThaoTacHeThong" runat="server" Width="100%" BorderStyle="None"
                                                                AutoGenerateColumns="False" CssClass="Grid" CellPadding="1" DataKeyField="Log_ID"
                                                                BorderWidth="1px" OnItemDataBound="dgData_ItemDataBound">
                                                                <ItemStyle CssClass="GridItem"></ItemStyle>
                                                                <AlternatingItemStyle CssClass="GridAltItem" />
                                                                <HeaderStyle CssClass="GridHeader"></HeaderStyle>
                                                                <Columns>
                                                                    <asp:TemplateColumn>
                                                                        <HeaderStyle HorizontalAlign="Center" Width="12%"></HeaderStyle>
                                                                        <ItemStyle HorizontalAlign="Left" CssClass="GridBorderVerSolid" Width="12%"></ItemStyle>
                                                                        <HeaderTemplate>
                                                                            Tên người dùng
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>
                                                                            <span class="linkGridForm">
                                                                                <%# DataBinder.Eval(Container.DataItem, "Tendaydu" )%></span>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateColumn>
                                                                    <asp:TemplateColumn>
                                                                        <HeaderStyle HorizontalAlign="Center" Width="8%"></HeaderStyle>
                                                                        <ItemStyle HorizontalAlign="Left" Width="8%" CssClass="GridBorderVerSolid"></ItemStyle>
                                                                        <HeaderTemplate>
                                                                            Địa chỉ IP
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>
                                                                            <span class="linkGridForm">
                                                                                <%#Eval("HostIP")%></span>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateColumn>
                                                                    <asp:TemplateColumn>
                                                                        <HeaderStyle HorizontalAlign="Center" Width="10%"></HeaderStyle>
                                                                        <ItemStyle HorizontalAlign="Center" Width="10%" CssClass="GridBorderVerSolid"></ItemStyle>
                                                                        <HeaderTemplate>
                                                                            Ngày thao tác
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>
                                                                            <span class="linkGridForm">
                                                                                <%#Convert.ToDateTime( Eval("NgayThaoTac")).ToString("dd/MM/yyyy hh:mm:ss")%></span>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateColumn>
                                                                    <asp:TemplateColumn>
                                                                        <HeaderStyle HorizontalAlign="Left" Width="30%"></HeaderStyle>
                                                                        <ItemStyle HorizontalAlign="Left" Width="30%" CssClass="GridBorderVerSolid"></ItemStyle>
                                                                        <HeaderTemplate>
                                                                            Thao tác
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>
                                                                            <span class="linkGridForm">
                                                                                <%#Eval("ThaoTac")%></span>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateColumn>
                                                                </Columns>
                                                            </asp:DataGrid>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2" style="text-align: right">
                                                            <table border="0" style="width: 100%">
                                                                <tr>
                                                                    <td style="text-align: right" class="pageNav">
                                                                        <cc1:CurrentPage runat="server" ID="CurrentPage" CssClass="pageNavTotal">
                                                                        </cc1:CurrentPage>
                                                                        <cc1:Pager runat="server" ID="pages" OnIndexChanged="pages_IndexChanged"></cc1:Pager>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
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
