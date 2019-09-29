<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/ITC.Master" AutoEventWireup="true"
    CodeBehind="ThongketheoUser.aspx.cs" Inherits="ToasoanTTXVN.BaoCao.ThongketheoUser" %>

<%@ Register TagPrefix="cc1" Namespace="HPCComponents.UI.WebControls" Assembly="HPCComponents" %>
<%@ Import Namespace="HPCComponents" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
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
                <table cellpadding="4" cellspacing="0" border="0" width="100%">
                    <tr>
                        <td style="text-align: center">
                            <div class="classSearchHeader">
                                <table border="0" cellpadding="2" cellspacing="2" style=" width:100%; text-align: center;">
                                    <tr>
                                        <td style="width: 10%; text-align: right" class="Titlelbl">
                                            <%=CommonLib.ReadXML("lblTacgia")%>:
                                        </td>
                                        <td style="width: 30%; text-align: left">
                                            <asp:DropDownList ID="drop_User" runat="server" Width="250px" CssClass="inputtext"
                                                TabIndex="1">
                                            </asp:DropDownList>
                                        </td>
                                        <td style="width: 10%; text-align: right" class="Titlelbl">
                                            <%=CommonLib.ReadXML("lblChuyenmuc") %>:
                                        </td>
                                        <td style="width: 30%; text-align: left;">
                                            <anthem:DropDownList AutoCallBack="true" ID="cbo_chuyenmuc" runat="server" Width="200px"
                                                CssClass="inputtext" DataTextField="tenchuyenmuc" DataValueField="id" TabIndex="2">
                                            </anthem:DropDownList>
                                        </td>                                       
                                    </tr>
                                    <tr>
                                        <td style="width: 10%; text-align: right" class="Titlelbl">
                                            <%=CommonLib.ReadXML("lblTungay") %>:
                                        </td>
                                        <td style="width: 30%; text-align: left">
                                            <asp:TextBox ID="txt_FromDate" runat="server" Width="120px" CssClass="inputtext" MaxLength="10"
                                            onkeypress="AscciiDisable()" onfocus="javascript:vDateType='3'" onkeyup="DateFormat(this,this.value,event,false,'3')"
                                            onblur="DateFormat(this,this.value,event,true,'3')"></asp:TextBox>
                                        <asp:RegularExpressionValidator ID="Regularexpressionvalidator2" runat="server" ControlToValidate="txt_FromDate"
                                            ValidationExpression="^\d{1,2}/\d{1,2}/\d{4}$" Display="Dynamic">Kiểu ngày:dd/MM/yyyy</asp:RegularExpressionValidator>
                                           
                                        </td>
                                        <td style="width: 10%; text-align: right" class="Titlelbl">
                                            <%=CommonLib.ReadXML("lblDenngay") %>:
                                        </td>
                                        <td style="width: 30%; text-align: left">
                                            <asp:TextBox ID="txt_ToDate" runat="server" Width="120px" CssClass="inputtext" MaxLength="10"
                                            onkeypress="AscciiDisable()" onfocus="javascript:vDateType='3'" onkeyup="DateFormat(this,this.value,event,false,'3')"
                                            onblur="DateFormat(this,this.value,event,true,'3')"></asp:TextBox>
                                        <asp:RegularExpressionValidator ID="Regularexpressionvalidator3" runat="server" ControlToValidate="txt_ToDate"
                                            ValidationExpression="^\d{1,2}/\d{1,2}/\d{4}$" Display="Dynamic">Kiểu ngày:dd/MM/yyyy</asp:RegularExpressionValidator>
                                            
                                        </td>                                        
                                    </tr>
                                    <tr>
                                        <td style="text-align: center" colspan="4">
                                            <asp:Button ID="Button1" CssClass="iconFind" runat="server" Text="<%$Resources:cms.language,lblXembaocao %>" OnClick="linkExport_OnClick">
                                            </asp:Button>
                                            <asp:Button ID="Button2" CssClass="iconFind" runat="server" Text="<%$Resources:cms.language,lblIn %>" OnClick="Print_OnClick">
                                            </asp:Button>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left">
                            <asp:GridView ID="gvList" runat="server" AutoGenerateColumns="false" DataKeyField="News_ID"
                                BorderColor="#D9D9D9" CellPadding="4" AlternatingItemStyle-BackColor="#F1F1F2"
                                BackColor="White" Width="100%" BorderWidth="1px" CssClass="Grid">
                                <SelectedRowStyle Font-Bold="True" ForeColor="#CCFF99" BackColor="#009999" />
                                <RowStyle CssClass="GridItem" />
                                <AlternatingRowStyle CssClass="GridAltItem" />
                                <HeaderStyle CssClass="GridHeader" Height="30px" />
                                <Columns>
                                    <asp:TemplateField>
                                        <HeaderStyle Width="5%" HorizontalAlign="Center" BackColor="#cccccc" />
                                        <HeaderTemplate>
                                            #ID
                                        </HeaderTemplate>
                                        <ItemStyle Width="5%" HorizontalAlign="Center" BackColor="#cccccc"></ItemStyle>
                                        <ItemTemplate>
                                            <b><%#  Container.DataItemIndex + 1%></b>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            <%=CommonLib.ReadXML("lblTacgia") %>
                                        </HeaderTemplate>
                                        <ItemStyle Width="20%" HorizontalAlign="Left"></ItemStyle>
                                        <ItemTemplate>
                                            <b>
                                                <%# Eval("TenDaydu")%></b>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            Tiêu đề
                                        </HeaderTemplate>
                                        <ItemStyle Width="45%" HorizontalAlign="Left"></ItemStyle>
                                        <ItemTemplate>
                                            <%# Eval("News_Tittle")%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            Chuyên mục
                                        </HeaderTemplate>
                                        <ItemStyle Width="20%" HorizontalAlign="Left"></ItemStyle>
                                        <ItemTemplate>
                                            <%# Eval("Ten_ChuyenMuc")%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            Ngày xuất bản
                                        </HeaderTemplate>
                                        <ItemStyle Width="10%" HorizontalAlign="Left"></ItemStyle>
                                        <ItemTemplate>
                                            <%# DataBinder.Eval(Container, "DataItem.News_DatePublished") != System.DBNull.Value ? Convert.ToDateTime(DataBinder.Eval(Container, "DataItem.News_DatePublished")).ToString("dd/MM/yyyy HH:mm") : ""%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 4px" colspan="8">
                        </td>
                    </tr>
                </table>
            </td>
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
