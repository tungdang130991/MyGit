<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/ITC.Master" AutoEventWireup="true"
    CodeBehind="ThongketheoChuyenMuc.aspx.cs" Inherits="ToasoanTTXVN.BaoCao.ThongketheoChuyenMuc" %>

<%@ Register TagPrefix="cc1" Namespace="HPCComponents.UI.WebControls" Assembly="HPCComponents" %>
<%@ Import Namespace="HPCComponents" %>
<%@ Register TagPrefix="nbc" Namespace="HPCComponents.UI" Assembly="HPCComponents" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
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
                            <div class="classSearchHeader">
                                <table border="0" cellpadding="2" cellspacing="2" width="100%">
                                    <tr>
                                        <td style="text-align: right" class="Titlelbl">
                                            <%=CommonLib.ReadXML("lblChuyenmuc") %>:
                                        </td>
                                        <td style="text-align: left;">
                                            <anthem:DropDownList AutoCallBack="true" ID="cbo_chuyenmuc" runat="server" Width="206px"
                                                CssClass="inputtext" DataTextField="tenchuyenmuc" DataValueField="id" TabIndex="5"
                                                AutoPostBack="True" OnSelectedIndexChanged="cbo_chuyenmuc_SelectedIndexChanged">
                                            </anthem:DropDownList>
                                        </td>
                                        <td style="text-align: right" class="Titlelbl">
                                            <%=CommonLib.ReadXML("lblTungay") %>:
                                        </td>
                                        <td style="text-align: left">
                                             <asp:TextBox ID="txt_FromDate" runat="server" Width="120px" CssClass="inputtext" MaxLength="10"
                                            onkeypress="AscciiDisable()" onfocus="javascript:vDateType='3'" onkeyup="DateFormat(this,this.value,event,false,'3')"
                                            onblur="DateFormat(this,this.value,event,true,'3')"></asp:TextBox>
                                            <asp:RegularExpressionValidator ID="Regularexpressionvalidator3" runat="server" ControlToValidate="txt_FromDate"
                                            ValidationExpression="^\d{1,2}/\d{1,2}/\d{4}$" Display="Dynamic">Kiểu ngày:dd/MM/yyyy</asp:RegularExpressionValidator>                                            
                                        </td>
                                        <td style="text-align: right" class="Titlelbl">
                                            <%=CommonLib.ReadXML("lblDenngay") %>:
                                        </td>
                                        <td style="text-align: left">
                                            <asp:TextBox ID="txt_ToDate" runat="server" Width="120px" CssClass="inputtext" MaxLength="10"
                                            onkeypress="AscciiDisable()" onfocus="javascript:vDateType='3'" onkeyup="DateFormat(this,this.value,event,false,'3')"
                                            onblur="DateFormat(this,this.value,event,true,'3')"></asp:TextBox>
                                        <asp:RegularExpressionValidator ID="Regularexpressionvalidator2" runat="server" ControlToValidate="txt_ToDate"
                                            ValidationExpression="^\d{1,2}/\d{1,2}/\d{4}$" Display="Dynamic">Kiểu ngày:dd/MM/yyyy</asp:RegularExpressionValidator>                                            
                                        </td>
                                        <td style="text-align: right" class="Titlelbl">
                                            <%=CommonLib.ReadXML("lblTacgia") %>:
                                        </td>
                                        <td style="text-align: left">
                                            <asp:DropDownList ID="drop_User" runat="server" Width="90%" CssClass="inputtext">
                                            </asp:DropDownList>
                                        </td>
                                        <td>
                                            <asp:Button ID="btnExport" CssClass="iconFind" runat="server" Text="<%$Resources:cms.language,lblXembaocao %>"
                                                OnClick="ViewReport_OnClick"></asp:Button>
                                            <asp:Button ID="Button1" CssClass="iconFind" runat="server" Text="<%$Resources:cms.language,lblIn %>" OnClick="Print_OnClick">
                                            </asp:Button>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <br />
                            <div class="classSearchHeader" style="text-align:center">
                                <table border="0" cellpadding="1" cellspacing="1" width="100%">
                                    <tr>
                                        <td style="text-align:center">
                                            <asp:GridView ID="gvList" runat="server" AutoGenerateColumns="false" DataKeyField="Categorys_ID"
                                                BorderColor="#D9D9D9" CellPadding="4" AlternatingItemStyle-BackColor="#F1F1F2"
                                                BackColor="White" Width="100%" BorderWidth="1px" CssClass="Grid" OnRowDataBound="gvList_RowDataBound"
                                                ShowFooter="true">
                                                <SelectedRowStyle Font-Bold="True" ForeColor="#CCFF99" BackColor="#009999" />
                                                <RowStyle CssClass="GridItem" />
                                                <AlternatingRowStyle CssClass="GridAltItem" />
                                                <HeaderStyle CssClass="GridHeader" Height="30px" />
                                                <Columns>
                                                    <asp:TemplateField>
                                                        <HeaderStyle Width="2%" HorizontalAlign="Center" />
                                                        <HeaderTemplate>
                                                            #ID
                                                        </HeaderTemplate>
                                                        <ItemStyle Width="2%" HorizontalAlign="Center"></ItemStyle>
                                                        <ItemTemplate>
                                                            <%#  Container.DataItemIndex + 1%>
                                                        </ItemTemplate>
                                                        <FooterStyle CssClass="GridHeader" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField>
                                                        <HeaderStyle HorizontalAlign="Left" />
                                                        <HeaderTemplate>
                                                            <%=CommonLib.ReadXML("lblChuyenmuc") %>
                                                        </HeaderTemplate>
                                                        <ItemStyle Width="90%" HorizontalAlign="Left"></ItemStyle>
                                                        <ItemTemplate>
                                                            <b>
                                                                <%# Eval("Ten_ChuyenMuc")%></b>
                                                        </ItemTemplate>
                                                        <FooterStyle CssClass="GridHeader" HorizontalAlign="Right" />
                                                        <FooterTemplate>
                                                            <asp:Label ID="lbltxttotal" runat="server" Text="<%$Resources:cms.language,lblTongso %>" Font-Bold="true" />
                                                        </FooterTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField>
                                                        <HeaderStyle HorizontalAlign=Right />
                                                        <HeaderTemplate>
                                                            <%=CommonLib.ReadXML("lblSoluong") %>
                                                        </HeaderTemplate>
                                                        <ItemStyle Width="10%" HorizontalAlign="Right"></ItemStyle>
                                                        <ItemTemplate>
                                                           <b> <%# Eval("sl")%></b>
                                                        </ItemTemplate>
                                                        <FooterStyle CssClass="GridHeader" HorizontalAlign=Right />
                                                        <FooterTemplate>
                                                            <asp:Label ID="lblTotal" runat="server" Font-Bold="true" />
                                                        </FooterTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </td>
                                    </tr>
                                </table>
                            </div>
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
