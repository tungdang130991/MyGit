<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/ITC.Master" AutoEventWireup="true"
    CodeBehind="Thongkethanhtoannhuanbut.aspx.cs" Inherits="ToasoanTTXVN.Baocaothongke.Thongkethanhtoannhuanbut" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
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
                        <td style="text-align: right">
                            <table border="0" cellpadding="4" cellspacing="4" style="width: 100%; text-align: left">
                                <tr>
                                    <td style="width: 5%; text-align: right" class="Titlelbl">
                                        <%=CommonLib.ReadXML("lblAnpham") %>:
                                    </td>
                                    <td style="width: 10%; text-align: left">
                                        <asp:DropDownList ID="cbo_Anpham" runat="server" Width="100%" CssClass="inputtext"
                                            TabIndex="1">
                                        </asp:DropDownList>
                                    </td>
                                    <td style="width: 5%; text-align: right" class="Titlelbl">
                                        <%=CommonLib.ReadXML("lblThanhtoan") %>:
                                    </td>
                                    <td style="width: 10%; text-align: left">
                                        <asp:DropDownList AutoPostBack="true" ID="cboThanhtoan" runat="server" Width="100%"
                                            CssClass="inputtext" TabIndex="5">
                                            <asp:ListItem Value="0">Chưa thanh toán</asp:ListItem>
                                            <asp:ListItem Value="1">Thanh toán</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td style="width: 5%; text-align: right" class="Titlelbl">
                                        <%=CommonLib.ReadXML("lblCongtacvien") %>:
                                    </td>
                                    <td style="width: 10%; text-align: left">
                                        <asp:DropDownList AutoPostBack="true" ID="cbo_PVCTV" runat="server" Width="100%"
                                            CssClass="inputtext" OnSelectedIndexChanged="cbo_PVCTV_OnSelectedIndexChanged"
                                            TabIndex="5">
                                            <asp:ListItem Value="-1">-----Chọn-----</asp:ListItem>
                                            <asp:ListItem Value="0">Phóng viên</asp:ListItem>
                                            <asp:ListItem Value="1">CTV</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td style="text-align: right; width: 5%;" class="Titlelbl">
                                       <%=CommonLib.ReadXML("lblVungmien")%>
                                    </td>
                                    <td style="width: 10%; text-align: left">
                                        <asp:DropDownList AutoPostBack="true" ID="cboVungmien" runat="server" Width="100%"
                                            CssClass="inputtext" TabIndex="5">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 5%; text-align: right" class="Titlelbl">
                                        <%=CommonLib.ReadXML("lblTungay") %>:
                                    </td>
                                    <td style="width: 8%; text-align: left">
                                        <asp:TextBox ID="txt_tungay" runat="server" Width="120px" CssClass="inputtext" MaxLength="10"
                                            onKeyPress="AscciiDisable()" onfocus="javascript:vDateType='3'" onKeyUp="DateFormat(this,this.value,event,false,'3')"
                                            onBlur="DateFormat(this,this.value,event,true,'3')"></asp:TextBox>
                                        <asp:RegularExpressionValidator ID="Regularexpressionvalidator3" runat="server" ControlToValidate="txt_tungay"
                                            ValidationExpression="^\d{1,2}/\d{1,2}/\d{4}$" Display="Dynamic">Kiểu ngày:dd/MM/yyyy</asp:RegularExpressionValidator>
                                    </td>
                                    <td style="width: 5%; text-align: right" class="Titlelbl">
                                        <%=CommonLib.ReadXML("lblDenngay") %>:
                                    </td>
                                    <td style="width: 8%; text-align: left">
                                        <asp:TextBox ID="txt_denngay" runat="server" Width="120px" CssClass="inputtext" MaxLength="10"
                                            onKeyPress="AscciiDisable()" onfocus="javascript:vDateType='3'" onKeyUp="DateFormat(this,this.value,event,false,'3')"
                                            onBlur="DateFormat(this,this.value,event,true,'3')"></asp:TextBox>
                                        <asp:RegularExpressionValidator ID="Regularexpressionvalidator1" runat="server" ControlToValidate="txt_denngay"
                                            ValidationExpression="^\d{1,2}/\d{1,2}/\d{4}$" Display="Dynamic">Kiểu ngày:dd/MM/yyyy</asp:RegularExpressionValidator>
                                    </td>
                                    <td style="width: 10%; text-align: left" colspan="3">
                                        <asp:UpdatePanel ID="UpdatePanelTimKiem" runat="server">
                                            <Triggers>
                                                <asp:PostBackTrigger ControlID="btn_xuatbaocao" />
                                            </Triggers>
                                            <ContentTemplate>
                                                <asp:Button runat="server" ID="btnTimkiem" CssClass="iconFind" Font-Bold="true" OnClick="btnTimkiem_Click"
                                                    Text="<%$Resources:cms.language,lblTimkiem %>"></asp:Button>
                                                <asp:Button ID="btn_xuatbaocao" OnClick="btn_xuatbaocao_click" runat="server" Text="<%$Resources:cms.language,lblXuatbaocao %>"
                                                    CssClass="iconSend" />
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 4px">
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:UpdatePanel ID="UpdatePanelListTinbai" runat="server">
                                <ContentTemplate>
                                    <table width="100%" cellspacing="0" cellpadding="0" border="0">
                                        <tr>
                                            <td valign="top" style="text-align: left; width: 100%">
                                                <table border="0" style="width: 100%">
                                                    <tr>
                                                        <td valign="top" style="text-align: left; width: 100%">
                                                            <asp:DataGrid ID="DataGrid_tinbai" runat="server" Width="100%" BorderStyle="None"
                                                                CssClass="Grid" AutoGenerateColumns="False" BorderColor="#d4d4d4" CellPadding="0"
                                                                DataKeyField="MaTinbai" BackColor="White" BorderWidth="1px" AlternatingItemStyle-BackColor="#F1F1F2"
                                                                OnItemDataBound="dgData_ItemDataBound">
                                                                <ItemStyle CssClass="GridItem"></ItemStyle>
                                                                <AlternatingItemStyle CssClass="GridAltItem" />
                                                                <HeaderStyle CssClass="GridHeader" Height="30px"></HeaderStyle>
                                                                <Columns>
                                                                    <asp:TemplateColumn>
                                                                        <HeaderStyle HorizontalAlign="Center" Width="4%" BackColor="#cccccc"></HeaderStyle>
                                                                        <ItemStyle HorizontalAlign="Center" BackColor="#cccccc"></ItemStyle>
                                                                        <HeaderTemplate>
                                                                            STT
                                                                        </HeaderTemplate>
                                                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                                        <ItemTemplate>
                                                                            <b><asp:Label ID="lblSTT" runat="server"></asp:Label></b>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateColumn>
                                                                    <asp:BoundColumn DataField="MaTinbai" HeaderText="Mã tin bài" Visible="False"></asp:BoundColumn>
                                                                    <asp:TemplateColumn>
                                                                        <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                                                        <ItemStyle HorizontalAlign="Left" CssClass="GridBorderVerSolid"></ItemStyle>
                                                                        <HeaderTemplate>
                                                                            <%=CommonLib.ReadXML("lblTenbaiviet") %>
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lbltieude" class="linkGridForm" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Tieude" )%>'>
                                                                            </asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateColumn>
                                                                    <asp:TemplateColumn>
                                                                        <HeaderStyle HorizontalAlign="Left" Width="15%"></HeaderStyle>
                                                                        <ItemStyle HorizontalAlign="Left" Width="15%" CssClass="GridBorderVerSolid"></ItemStyle>
                                                                        <HeaderTemplate>
                                                                            <%=CommonLib.ReadXML("lblTacgia") %>
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblMaTacGia" runat="server" Visible="false" Text='<%#Eval("TacgiaTin") %>'></asp:Label>
                                                                            <span class="linkGridForm">
                                                                                <%#HPCBusinessLogic.UltilFunc.GetTenTacGiaTinBai(Eval("TacgiaTin"))%></span>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateColumn>
                                                                    <asp:TemplateColumn>
                                                                        <HeaderStyle HorizontalAlign="Left" Width="30%"></HeaderStyle>
                                                                        <ItemStyle HorizontalAlign="Left" Width="30%" CssClass="GridBorderVerSolid"></ItemStyle>
                                                                        <HeaderTemplate>
                                                                            <%=CommonLib.ReadXML("lblDiachi") %>
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>
                                                                            <span class="linkGridForm">
                                                                                <%#HPCBusinessLogic.UltilFunc.GetDiaChiTacGia(Eval("TacgiaTin"),1)%></span>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateColumn>
                                                                    <asp:TemplateColumn>
                                                                        <HeaderStyle HorizontalAlign="Right" Width="10%"></HeaderStyle>
                                                                        <ItemStyle HorizontalAlign="Right" Width="10%" CssClass="GridBorderVerSolid"></ItemStyle>
                                                                        <HeaderTemplate>
                                                                            <%=CommonLib.ReadXML("lblNhuanbut") %>
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>
                                                                            <div style="text-align: right">
                                                                                <asp:Label ID="lblsotientinbai" CssClass="linkGridForm" runat="server" Font-Bold="true"
                                                                                    Font-Size="12px" Font-Names="Arial" Text='<%#DataBinder.Eval(Container, "DataItem.DiemTin")!=System.DBNull.Value? String.Format("{0:00,0}", Convert.ToDecimal( DataBinder.Eval(Container.DataItem, "DiemTin"))):""%>'></asp:Label></div>
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
