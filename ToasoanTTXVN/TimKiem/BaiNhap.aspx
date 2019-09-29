<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/ITC.Master" AutoEventWireup="true"
    CodeBehind="BaiNhap.aspx.cs" Inherits="ToasoanTTXVN.TimKiem.BaiNhap" %>

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
            <td style="text-align: center">
                <div class="divPanelSearch">
                    <div style="width: 99%; padding: 5px 0.5%">
                        <table border="0" cellpadding="2" cellspacing="2" style="width: 100%; text-align: left">
                            <tr>
                                <td style="width: 40%; text-align: left">
                                    <asp:TextBox ID="txt_tieude" TabIndex="1" Width="95%" runat="server" CssClass="inputtext"
                                        Text="Nhập tiêu đề cần tìm" onfocus="if(this.value=='Nhập tiêu đề cần tìm') { this.value=''; }"
                                        onblur="if (this.value=='') { this.value='Nhập tiêu đề cần tìm'; }" onkeypress="return clickButton(event,'ctl00_MainContent_btnTimkiem');"></asp:TextBox>
                                </td>
                                <td style="width: 10%; text-align: left">
                                    <asp:TextBox ID="txt_tungay" runat="server" Width="120px" CssClass="inputtext" MaxLength="10"
                                        onkeypress="AscciiDisable()" onfocus="javascript:vDateType='3'" onkeyup="DateFormat(this,this.value,event,false,'3')"
                                        onblur="DateFormat(this,this.value,event,true,'3')"></asp:TextBox>
                                    <asp:RegularExpressionValidator ID="Regularexpressionvalidator3" runat="server" ControlToValidate="txt_tungay"
                                        ValidationExpression="^\d{1,2}/\d{1,2}/\d{4}$" Display="Dynamic">Kiểu ngày:dd/MM/yyyy</asp:RegularExpressionValidator>
                                </td>
                                <td style="width: 10%; text-align: left">
                                    <asp:TextBox ID="txt_denngay" runat="server" Width="120px" CssClass="inputtext" MaxLength="10"
                                        onkeypress="AscciiDisable()" onfocus="javascript:vDateType='3'" onkeyup="DateFormat(this,this.value,event,false,'3')"
                                        onblur="DateFormat(this,this.value,event,true,'3')"></asp:TextBox>
                                    <asp:RegularExpressionValidator ID="Regularexpressionvalidator1" runat="server" ControlToValidate="txt_denngay"
                                        ValidationExpression="^\d{1,2}/\d{1,2}/\d{4}$" Display="Dynamic">Kiểu ngày:dd/MM/yyyy</asp:RegularExpressionValidator>
                                </td>
                                <td style="width: 5%; text-align: left">
                                    <asp:UpdatePanel ID="UpdatePanelTimKiem" runat="server">
                                        <ContentTemplate>
                                            <asp:Button runat="server" ID="btnTimkiem" CssClass="iconFind" Font-Bold="true" OnClick="btnTimkiem_Click"
                                                Text="Tìm kiếm"></asp:Button>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
                <div class="divPanelResult">
                    <asp:UpdatePanel ID="UpdatePanelListTinbai" runat="server">
                        <ContentTemplate>
                            <div style="width: 99%; padding: 5px 0.5%">
                                <asp:DataGrid ID="DataGrid_tinbai" runat="server" Width="100%" BorderStyle="None"
                                    AutoGenerateColumns="False" CssClass="Grid" CellPadding="1" DataKeyField="ID"
                                    BorderWidth="1px" OnEditCommand="DataGrid_EditCommand" OnItemDataBound="dgData_ItemDataBound">
                                    <ItemStyle CssClass="GridItem"></ItemStyle>
                                    <AlternatingItemStyle CssClass="GridAltItem" />
                                    <HeaderStyle CssClass="GridHeader"></HeaderStyle>
                                    <Columns>
                                        <asp:TemplateColumn>
                                            <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Left" Width="30%" CssClass="GridBorderVerSolid"></ItemStyle>
                                            <HeaderTemplate>
                                                Tên bài viết
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <a class="linkGridForm" href="Javascript:open_window_Scroll('<%=Global.ApplicationPath%>/Timkiem/ViewBaiNhap.aspx?Menu_ID=<%=Page.Request.QueryString["Menu_ID"]%>&ID=<%# DataBinder.Eval(Container.DataItem, "ID") %>',50,500,100,800);">
                                                    <%# DataBinder.Eval(Container.DataItem, "Tieude" )%></a>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="Ngày tạo">
                                            <HeaderStyle HorizontalAlign="Center" Width="10%"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Center" CssClass="GridBorderVerSolid"></ItemStyle>
                                            <ItemTemplate>
                                                <span class="linkGridForm">
                                                    <%#Convert.ToDateTime(Eval("NgayTao")).ToString("dd/MM/yyyy")%></span>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn>
                                            <HeaderStyle HorizontalAlign="Center" Width="5%"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Center" Width="5%" CssClass="GridBorderVerSolid"></ItemStyle>
                                            <HeaderTemplate>
                                                Xóa
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:ImageButton ID="btnDelete" Width="15px" runat="server" ImageUrl="~/Dungchung/images/cancel.gif"
                                                    ImageAlign="AbsMiddle" ToolTip="Xóa" CommandName="Edit" CommandArgument="Delete"
                                                    BorderStyle="None"></asp:ImageButton>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                    </Columns>
                                </asp:DataGrid>
                            </div>
                            <div style="width: 99%; padding: 5px 0.5%; text-align: right" class="pageNav">
                                <cc1:CurrentPage runat="server" ID="CurrentPage" CssClass="pageNavTotal">
                                </cc1:CurrentPage>
                                <cc1:Pager runat="server" ID="pages" OnIndexChanged="pages_IndexChanged"></cc1:Pager>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </td>
        </tr>
    </table>
</asp:Content>
