<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/ITC.Master" AutoEventWireup="true"
    CodeBehind="List_TuLieuAnh.aspx.cs" Inherits="ToasoanTTXVN.Tulieu.List_TuLieuAnh" %>

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
            <td class="datagrid_content_left">
            </td>
            <td style="text-align: center">
                <div style="text-align: left; width: 100%; padding: 5px">
                    <div style="text-align: left; width: 100%">
                        <table border="0" cellpadding="2" cellspacing="2" style="width: 100%; text-align: left">
                            <tr>
                                <td style="width: 5%; text-align: right" class="Titlelbl">
                                    Chú thích:
                                </td>
                                <td style="width: 12%; text-align: left">
                                    <asp:TextBox ID="txt_chuthich" runat="server" CssClass="inputtext" Width="95%"></asp:TextBox>
                                </td>
                                <td style="text-align: right; width: 8%;" class="Titlelbl">
                                    Tên file gốc
                                </td>
                                <td style="width: 12%; text-align: left">
                                    <asp:TextBox ID="txt_file" runat="server" CssClass="inputtext" Width="95%"></asp:TextBox>
                                </td>
                                <td style="width: 5%; text-align: right" class="Titlelbl">
                                    Từ ngày:
                                </td>
                                <td style="width: 10%; text-align: left">
                                    <asp:TextBox ID="txt_tungay" runat="server" Width="120px" CssClass="inputtext" MaxLength="10"
                                        onkeypress="AscciiDisable()" onfocus="javascript:vDateType='3'" onkeyup="DateFormat(this,this.value,event,false,'3')"
                                        onblur="DateFormat(this,this.value,event,true,'3')"></asp:TextBox>
                                    <asp:RegularExpressionValidator ID="Regularexpressionvalidator3" runat="server" ControlToValidate="txt_tungay"
                                        ValidationExpression="^\d{1,2}/\d{1,2}/\d{4}$" Display="Dynamic">Kiểu ngày:dd/MM/yyyy</asp:RegularExpressionValidator>
                                </td>
                                <td style="width: 5%; text-align: right" class="Titlelbl">
                                    Đến ngày:
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
                                    Tác giả:
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
                                <td style="width: 5%; text-align: right" class="Titlelbl">
                                    Tên bài viết:
                                </td>
                                <td style="width: 12%; text-align: left">
                                    <asp:TextBox ID="txt_tieude" TabIndex="1" Width="95%" runat="server" CssClass="inputtext"
                                        onkeypress="return clickButton(event,'ctl00_MainContent_btnTimkiem');"></asp:TextBox>
                                </td>
                                <td colspan="3" style="width: 30%; text-align: left">
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
                    <div style="height: 20px">
                    </div>
                    <div style="text-align: center; width: 100%">
                        <asp:UpdatePanel ID="UpdatePanel_Tulieuanh" runat="server">
                            <Triggers>
                                <asp:PostBackTrigger ControlID="dgrListImages" />
                            </Triggers>
                            <ContentTemplate>
                                <asp:DataList ID="dgrListImages" runat="server" RepeatColumns="5" RepeatDirection="Horizontal"
                                    DataKeyField="Ma_Anh" Width="100%" CellPadding="4" OnEditCommand="dgrListImages_EditCommand">
                                    <ItemStyle Width="20%" BorderWidth="0" VerticalAlign="top" HorizontalAlign="Center">
                                    </ItemStyle>
                                    <ItemTemplate>
                                        <div style="width: 80%; float: left;">
                                            <ul class="hoverbox">
                                                <li><a href="javascript:OpenImage('<%# DataBinder.Eval(Container.DataItem, "Duongdan_Anh")%>');">
                                                    <img src="<%=Global.TinPath%><%#Eval("Duongdan_Anh")%>" alt="<%# DataBinder.Eval(Container.DataItem, "Chuthich")%>" />
                                                </a></li>
                                            </ul>
                                        </div>
                                        <div style="width: 80%; float: left; text-align: left; padding: 3px 0">
                                            <asp:Label ID="lbFileAttach" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Duongdan_Anh") %>'
                                                Visible="false">
                                            </asp:Label>
                                            <asp:Label ID="lbdesc" CssClass="linkGridForm" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Chuthich")%>'>
                                            </asp:Label>
                                        </div>
                                        <div style="width: 80%; float: left; font-weight: bold; text-align: right; padding: 3px 0">
                                            <asp:Label ID="lbtacgia" CssClass="styleforimages" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "NguoiChup")%>'>
                                            </asp:Label>
                                        </div>
                                        <div style="width: 80%; float: left; margin-top: 3px; text-align: left">
                                            <asp:ImageButton ID="btnUpdate" Width="20px" CausesValidation="false" runat="server"
                                                ImageUrl="../Dungchung/Images/save.gif" ImageAlign="AbsMiddle" ToolTip="lấy ảnh"
                                                CommandName="Edit" CommandArgument="Download" BorderStyle="None"></asp:ImageButton>
                                        </div>
                                    </ItemTemplate>
                                </asp:DataList>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
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

    <script type="text/javascript" language="javascript">
        function ClientItemSelectedTacGiaTin(sender, e) {
            $get("<%=HiddenFieldTacgiatin.ClientID %>").value = e.get_value();
        }
    </script>

</asp:Content>
