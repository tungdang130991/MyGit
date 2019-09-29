<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/ITC.Master" AutoEventWireup="true"
    CodeBehind="ThongKeTruyCap.aspx.cs" Inherits="ToasoanTTXVN.BaoCao.ThongKeTruyCap" %>

<%@ Register TagPrefix="cc1" Namespace="HPCComponents.UI.WebControls" Assembly="HPCComponents" %>
<%@ Import Namespace="HPCComponents" %>
<%@ Register TagPrefix="nbc" Namespace="HPCComponents.UI" Assembly="HPCComponents" %>
<%@ Register TagPrefix="anthem" Namespace="Anthem" Assembly="Anthem" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
<script type="text/javascript">
        $(document).ready(function() {
            $("#<%=txt_FromDate.ClientID%>").datepicker({
                showOn: 'button',
                buttonText: 'Show Date',
                buttonImageOnly: true,
                buttonImage: '../Dungchung/Images/DatePicker/calendar.png'
            });
            $("#<%=txt_ToDate.ClientID%>").datepicker({
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
                <table cellpadding="0" cellspacing="0" border="0" width="100%">
                    <tr>
                        <td style="text-align: left">
                            <table border="0" cellpadding="1" cellspacing="1" style="width: 100%; text-align: left;">
                                <tr>
                                    <td style="width: 10%; text-align: right" class="Titlelbl">
                                        <%=CommonLib.ReadXML("lblChuyenmuc") %>:
                                    </td>
                                    <td style="width: 20%; text-align: left" class="Titlelbl">
                                        <asp:DropDownList ID="ddlCate" runat="server" Width="200px" CssClass="inputtext">
                                        </asp:DropDownList>
                                    </td>
                                    <td style="width: 10%; text-align: right" class="Titlelbl">
                                        <%=CommonLib.ReadXML("lblTungay") %>:
                                    </td>
                                    <td style="width: 20%; text-align: left" class="Titlelbl">
                                        <asp:TextBox ID="txt_FromDate" runat="server" Width="120px" CssClass="inputtext" MaxLength="10"
                                            onkeypress="AscciiDisable()" onfocus="javascript:vDateType='3'" onkeyup="DateFormat(this,this.value,event,false,'3')"
                                            onblur="DateFormat(this,this.value,event,true,'3')"></asp:TextBox>
                                        <asp:RegularExpressionValidator ID="Regularexpressionvalidator2" runat="server" ControlToValidate="txt_FromDate"
                                            ValidationExpression="^\d{1,2}/\d{1,2}/\d{4}$" Display="Dynamic">Kiểu ngày:dd/MM/yyyy</asp:RegularExpressionValidator>
                                    </td>
                                    <td style="width: 10%; text-align: right" class="Titlelbl">
                                        <%=CommonLib.ReadXML("lblDenngay") %>:
                                    </td>
                                    <td>
                                         <asp:TextBox ID="txt_ToDate" runat="server" Width="120px" CssClass="inputtext" MaxLength="10"
                                            onkeypress="AscciiDisable()" onfocus="javascript:vDateType='3'" onkeyup="DateFormat(this,this.value,event,false,'3')"
                                            onblur="DateFormat(this,this.value,event,true,'3')"></asp:TextBox>
                                        <asp:RegularExpressionValidator ID="Regularexpressionvalidator3" runat="server" ControlToValidate="txt_ToDate"
                                            ValidationExpression="^\d{1,2}/\d{1,2}/\d{4}$" Display="Dynamic">Kiểu ngày:dd/MM/yyyy</asp:RegularExpressionValidator>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 10px">
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Button ID="Button1" CssClass="iconFind" runat="server" Text="<%$Resources:cms.language,lblXembaocao %>" OnClick="ViewReport_OnClick">
                            </asp:Button>
                            <asp:Button ID="Button2" CssClass="iconFind" runat="server" Text="<%$Resources:cms.language,lblIn %>" OnClick="linkExport_OnClick">
                            </asp:Button>
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 10px">
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:DataGrid runat="server" ID="grdListHistory" AutoGenerateColumns="false" DataKeyField="sl"
                                BorderColor="#D9D9D9" CellPadding="4" AlternatingItemStyle-BackColor="#F1F1F2"
                                BackColor="White" Width="100%" BorderWidth="1px" CssClass="Grid" OnItemDataBound="grdListHistory_ItemDataBound">
                                <ItemStyle CssClass="GridItem"></ItemStyle>
                                <AlternatingItemStyle CssClass="GridAltItem" />
                                <HeaderStyle CssClass="GridHeader" Height="30px"></HeaderStyle>
                                <Columns>
                                    <asp:BoundColumn Visible="False" DataField="sl">
                                        <HeaderStyle Width="1%"></HeaderStyle>
                                    </asp:BoundColumn>
                                    <asp:TemplateColumn>
                                        <HeaderTemplate>
                                            STT
                                        </HeaderTemplate>
                                        <HeaderStyle HorizontalAlign="Center" BackColor="#cccccc" />
                                        <ItemStyle Width="5%" HorizontalAlign="Center" BackColor="#cccccc"></ItemStyle>
                                        <ItemTemplate>
                                            <b><%#Container.ItemIndex + 1%></b>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn>
                                        <HeaderStyle HorizontalAlign="Left" Width="80%"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Left" Font-Bold="true" Width="50%"></ItemStyle>
                                        <HeaderTemplate>
                                            <%=CommonLib.ReadXML("lblChuyenmuc") %>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <%# DataBinder.Eval(Container.DataItem, "Ten_ChuyenMuc")%>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn>
                                        <HeaderStyle HorizontalAlign="Center" Width="20%"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Center" Width="20%"></ItemStyle>
                                        <HeaderTemplate>
                                            <%=CommonLib.ReadXML("lblSoluong") %>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                           <b> <%# DataBinder.Eval(Container.DataItem, "sl")%></b>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                </Columns>
                            </asp:DataGrid>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td class="datagrid_content_right">
            </td>
            <tr>
                <td class="datagrid_bottom_left">
                </td>
                <td class="datagrid_bottom_center">
                </td>
                <td class="datagrid_bottom_right">
                </td>
            </tr>
        </tr>
    </table>
</asp:Content>
