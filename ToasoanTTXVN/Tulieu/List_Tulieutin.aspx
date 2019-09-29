<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/ITC.Master" AutoEventWireup="true"
    CodeBehind="List_Tulieutin.aspx.cs" Inherits="ToasoanTTXVN.Tulieu.List_Tulieutin" %>

<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral,PublicKeyToken=31BF3856AD364E35" %>
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
                        <td style="text-align: right">
                            <table border="0" cellpadding="2" cellspacing="2" style="width: 100%; text-align: left">
                                <tr>
                                    <td style="width: 5%; text-align: right" class="Titlelbl">
                                        <%=CommonLib.ReadXML("lblAnpham") %>:
                                    </td>
                                    <td style="width: 12%; text-align: left">
                                        <asp:DropDownList AutoPostBack="true" ID="cbo_anpham" runat="server" Width="100%"
                                            CssClass="inputtext" OnSelectedIndexChanged="cbo_anpham_SelectedIndexChanged"
                                            TabIndex="1">
                                        </asp:DropDownList>
                                    </td>
                                    <td style="text-align: right; width: 5%;" class="Titlelbl"><%=CommonLib.ReadXML("lblChuyenmuc")%>:
                                    </td>
                                    <td style="width: 12%; text-align: left">
                                        <asp:DropDownList ID="cbo_chuyenmuc" runat="server" Width="100%" CssClass="inputtext"
                                            DataTextField="tenchuyenmuc" DataValueField="id" TabIndex="5">
                                        </asp:DropDownList>
                                    </td>
                                    <td style="width: 5%; text-align: right" class="Titlelbl">
                                        <%=CommonLib.ReadXML("lblTungay")%>:
                                    </td>
                                    <td style="width: 10%; text-align: left">
                                        <asp:TextBox ID="txt_tungay" runat="server" Width="120px" CssClass="inputtext" MaxLength="10"
                                            onkeypress="AscciiDisable()" onfocus="javascript:vDateType='3'" onkeyup="DateFormat(this,this.value,event,false,'3')"
                                            onblur="DateFormat(this,this.value,event,true,'3')"></asp:TextBox>
                                        <asp:RegularExpressionValidator ID="Regularexpressionvalidator3" runat="server" ControlToValidate="txt_tungay"
                                            ValidationExpression="^\d{1,2}/\d{1,2}/\d{4}$" Display="Dynamic">Kiểu ngày:dd/MM/yyyy</asp:RegularExpressionValidator>
                                    </td>
                                    <td style="width: 5%; text-align: right" class="Titlelbl">
                                        <%=CommonLib.ReadXML("lblDenngay")%>:
                                    </td>
                                    <td style="width: 10%; text-align: left">
                                        <asp:TextBox ID="txt_denngay" runat="server" Width="120px" CssClass="inputtext" MaxLength="10"
                                            onkeypress="AscciiDisable()" onfocus="javascript:vDateType='3'" onkeyup="DateFormat(this,this.value,event,false,'3')"
                                            onblur="DateFormat(this,this.value,event,true,'3')"></asp:TextBox>
                                        <asp:RegularExpressionValidator ID="Regularexpressionvalidator1" runat="server" ControlToValidate="txt_denngay"
                                            ValidationExpression="^\d{1,2}/\d{1,2}/\d{4}$" Display="Dynamic">Kiểu ngày:dd/MM/yyyy</asp:RegularExpressionValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 5%; text-align: right" class="Titlelbl">
                                        <%=CommonLib.ReadXML("lblTacgia")%>:
                                    </td>
                                    <td style="width: 12%; text-align: left">
                                        <asp:TextBox ID="txt_PVCTV" runat="server" Width="95%" CssClass="inputtext"></asp:TextBox>
                                        <asp:HiddenField ID="HiddenFieldTacgiatin" runat="server" />
                                        <ajaxtoolkit:AutoCompleteExtender runat="server" ID="autoCompleteTacgiaTin" TargetControlID="txt_PVCTV"
                                            ServicePath="../UploadFileMulti/AutoComplete.asmx" ServiceMethod="GetCompletionList"
                                            ContextKey="1" CompletionListCssClass="CompletionListCssClass" MinimumPrefixLength="1"
                                            CompletionInterval="1000" EnableCaching="true" CompletionSetCount="20" OnClientItemSelected="ClientItemSelectedTacGiaTin">
                                        </ajaxtoolkit:AutoCompleteExtender>
                                    </td>
                                    <td style="width: 8%; text-align: right" class="Titlelbl">
                                        <%=CommonLib.ReadXML("lblTenbaiviet")%>:
                                    </td>
                                    <td style="width: 12%; text-align: left">
                                        <asp:TextBox ID="txt_tieude" TabIndex="1" Width="95%" runat="server" CssClass="inputtext"
                                            onkeypress="return clickButton(event,'ctl00_MainContent_btnTimkiem');"></asp:TextBox>
                                    </td>
                                    <td colspan="3" style="width: 30%; text-align: left">
                                        <asp:UpdatePanel ID="UpdatePanelTimKiem" runat="server">
                                            <ContentTemplate>
                                                <asp:Button runat="server" ID="btnTimkiem" CssClass="iconFind" Font-Bold="true" OnClick="btnTimkiem_Click"
                                                    Text="<%$Resources:cms.language,lblTimkiem %>"></asp:Button>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="height: 15px">
                                    </td>
                                </tr>
                            </table>
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
                                                                AutoGenerateColumns="False" CssClass="Grid" CellPadding="1" DataKeyField="Ma_Tulieu"
                                                                BorderWidth="1px" OnItemDataBound="dgData_ItemDataBound">
                                                                <ItemStyle CssClass="GridItem"></ItemStyle>
                                                                <AlternatingItemStyle CssClass="GridAltItem" />
                                                                <HeaderStyle CssClass="GridHeader" Height="30px"></HeaderStyle>
                                                                <Columns>
                                                                    <asp:TemplateColumn HeaderText="#ID">
                                                                        <HeaderStyle Width="5%" HorizontalAlign="Center" BackColor="#cccccc" />
                                                                        <ItemStyle HorizontalAlign="Center" BackColor="#cccccc" />
                                                                        <ItemTemplate>
                                                                            <b><%# DataBinder.Eval(Container.DataItem, "Ma_Tulieu")%></b>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateColumn>
                                                                    <asp:TemplateColumn>
                                                                        <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                                                        <ItemStyle HorizontalAlign="Left" CssClass="GridBorderVerSolid"></ItemStyle>
                                                                        <HeaderTemplate>
                                                                            <%=CommonLib.ReadXML("lblTenbaiviet")%>
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>
                                                                            <a class="linkGridForm" href="Javascript:open_window_Scroll('<%=Global.ApplicationPath%>/Tulieu/ViewsTulieu.aspx?Menu_ID=<%=Page.Request.QueryString["Menu_ID"]%>&ID=<%# DataBinder.Eval(Container.DataItem, "Ma_Tulieu") %>',50,500,100,800);">
                                                                                <%# DataBinder.Eval(Container.DataItem, "Tieude" )%></a>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateColumn>
                                                                    <asp:TemplateColumn>
                                                                        <HeaderStyle HorizontalAlign="Left" Width="10%"></HeaderStyle>
                                                                        <ItemStyle HorizontalAlign="Left" CssClass="GridBorderVerSolid"></ItemStyle>
                                                                        <HeaderTemplate>
                                                                            <%=CommonLib.ReadXML("lblAnpham")%>
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>                                                                            
                                                                                <%#HPCBusinessLogic.UltilFunc.GetTenAnpham(Eval("Ma_Anpham"))%>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateColumn>
                                                                    <asp:TemplateColumn HeaderText="<%$Resources:cms.language,lblChuyenmuc %>">
                                                                        <HeaderStyle Width="12%" HorizontalAlign="Left"></HeaderStyle>
                                                                        <ItemStyle Width="12%" HorizontalAlign="Left" CssClass="GridBorderVerSolid"></ItemStyle>
                                                                        <ItemTemplate>                                                                            
                                                                                <%#HPCBusinessLogic.UltilFunc.GetTenChuyenMuc(Eval("Ma_Chuyenmuc"))%>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateColumn>
                                                                    <asp:TemplateColumn>
                                                                        <HeaderStyle HorizontalAlign="Left" Width="10%"></HeaderStyle>
                                                                        <ItemStyle HorizontalAlign="Left" Width="10%" CssClass="GridBorderVerSolid"></ItemStyle>
                                                                        <HeaderTemplate>
                                                                            <%=CommonLib.ReadXML("lblTacgia")%>
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>
                                                                           
                                                                                <%#HPCBusinessLogic.UltilFunc.GetTenTacGiaTinBai(Eval("Ma_TacGia"))%>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateColumn>
                                                                    <asp:TemplateColumn HeaderText="<%$Resources:cms.language,lblNgaynhap %>">
                                                                        <HeaderStyle HorizontalAlign="Center" Width="10%"></HeaderStyle>
                                                                        <ItemStyle HorizontalAlign="Center" CssClass="GridBorderVerSolid"></ItemStyle>
                                                                        <ItemTemplate>
                                                                            
                                                                                <%#Convert.ToDateTime(Eval("NgayTao")).ToString("dd/MM/yyyy")%>
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

    <script type="text/javascript" language="javascript">
        function ClientItemSelectedTacGiaTin(sender, e) {
            $get("<%=HiddenFieldTacgiatin.ClientID %>").value = e.get_value();
        }
    </script>

</asp:Content>
