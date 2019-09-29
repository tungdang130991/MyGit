<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/ITC.Master" AutoEventWireup="true"
    CodeBehind="List_PublistPDF.aspx.cs" Inherits="ToasoanTTXVN.Quytrinh.List_PublistPDF" %>

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
            <td style="text-align: center">
                <div class="divPanelSearch">
                    <div style="width: 99%; padding: 5px 0.5%">
                        <table border="0" cellpadding="2" cellspacing="2" style="width: 100%;">
                            <tr>
                                <td class="Titlelbl" style="text-align: left">
                                    <%=CommonLib.ReadXML("lblAnpham") %>
                                </td>
                                <td class="Titlelbl" style="text-align: left">
                                    <%=CommonLib.ReadXML("lblSobao") %>
                                </td>
                                <td class="Titlelbl" style="text-align: left">
                                    <%=CommonLib.ReadXML("lblTrang")%>
                                </td>
                                <td class="Titlelbl" style="text-align: left">
                                    <%=CommonLib.ReadXML("lblTungay")%>
                                </td>
                                <td class="Titlelbl" style="text-align: left">
                                    <%=CommonLib.ReadXML("lblDenngay")%>
                                </td>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 10%; text-align: left">
                                    <asp:UpdatePanel ID="UpdatePanel_LB" runat="server">
                                        <ContentTemplate>
                                            <asp:DropDownList ID="cbo_Anpham" runat="server" Width="100%" CssClass="inputtext"
                                                AutoPostBack="true" OnSelectedIndexChanged="cbo_Anpham_SelectedIndexChanged"
                                                TabIndex="1">
                                            </asp:DropDownList>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td style="width: 12%; text-align: left">
                                    <asp:UpdatePanel ID="UpdatePanel_SB" runat="server">
                                        <ContentTemplate>
                                            <asp:DropDownList ID="cbo_Sobao" runat="server" Width="100%" CssClass="inputtext"
                                                AutoPostBack="true" OnSelectedIndexChanged="cboSoBao_OnSelectedIndexChanged"
                                                TabIndex="5">
                                            </asp:DropDownList>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td style="width: 10%; text-align: left">
                                    <asp:UpdatePanel ID="UpdatePanel_Trang" runat="server">
                                        <ContentTemplate>
                                            <asp:DropDownList ID="cboPage" runat="server" Width="100%" AutoPostBack="true" CssClass="inputtext"
                                                OnSelectedIndexChanged="cboPage_OnSelectedIndexChanged">
                                            </asp:DropDownList>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td style="width: 8%; text-align: left">
                                    <asp:TextBox ID="txt_tungay" runat="server" Width="120px" CssClass="inputtext" MaxLength="10"
                                        ToolTip="Từ ngày" onkeypress="AscciiDisable()" onfocus="javascript:vDateType='3'"
                                        onkeyup="DateFormat(this,this.value,event,false,'3')" onblur="DateFormat(this,this.value,event,true,'3')"></asp:TextBox>
                                    <asp:RegularExpressionValidator ID="Regularexpressionvalidator3" runat="server" ControlToValidate="txt_tungay"
                                        ValidationExpression="^\d{1,2}/\d{1,2}/\d{4}$" Display="Dynamic">Kiểu ngày:dd/MM/yyyy</asp:RegularExpressionValidator>&nbsp;&nbsp;
                                </td>
                                <td style="width: 8%; text-align: left">
                                    <asp:TextBox ID="txt_denngay" runat="server" Width="120px" CssClass="inputtext" MaxLength="10"
                                        ToolTip="Đến ngày" onkeypress="AscciiDisable()" onfocus="javascript:vDateType='3'"
                                        onkeyup="DateFormat(this,this.value,event,false,'3')" onblur="DateFormat(this,this.value,event,true,'3')"></asp:TextBox>
                                    <asp:RegularExpressionValidator ID="Regularexpressionvalidator1" runat="server" ControlToValidate="txt_denngay"
                                        ValidationExpression="^\d{1,2}/\d{1,2}/\d{4}$" Display="Dynamic">Kiểu ngày:dd/MM/yyyy</asp:RegularExpressionValidator>
                                </td>
                                <td style="text-align: left; width: 10%">
                                    <asp:UpdatePanel ID="Upnl_TimKiem" runat="server">
                                        <ContentTemplate>
                                            <asp:Button runat="server" ID="btnTimkiem" CssClass="iconFind" OnClick="btnTimkiem_Click"
                                                Text="<%$Resources:cms.language, lblTimkiem%>"></asp:Button>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
                <div class="divPanelResult">
                    <asp:UpdatePanel ID="Upnl_FilePDF" runat="server">
                        <Triggers>
                            <asp:PostBackTrigger ControlID="DataGrid_FilePDF" />
                        </Triggers>
                        <ContentTemplate>
                            <div style="text-align: left; width: 100%">
                                <asp:DataList ID="DataGrid_FilePDF" runat="server" RepeatColumns="10" RepeatDirection="Horizontal"
                                    DataKeyField="ID" Width="100%" CellPadding="4" OnEditCommand="dgrListPDF_EditCommand">
                                    <ItemStyle Width="10%" BorderWidth="0" VerticalAlign="top" HorizontalAlign="Left">
                                    </ItemStyle>
                                    <ItemTemplate>
                                        <div style="width: 90%; height: auto; float: left; text-align: center; position: relative;">
                                            <asp:CheckBox runat="server" Text='' ID="optSelect" AutoPostBack="False"></asp:CheckBox>
                                        </div>
                                        <div style="width: 90%; float: left; text-align: center">
                                            <a href="<%=Global.TinPath%><%# DataBinder.Eval(Container.DataItem, "Publish_Pdf")%>"
                                                target="_blank">
                                                <%#HPCBusinessLogic.UltilFunc.GetPath_PDF(Eval("Page_Number"))%>
                                                <img src="../Dungchung/Images/pdf.png" style="border: 0; width: 80px; height: 80px"
                                                    alt="" />
                                            </a>
                                            <asp:Label ID="lbFileAttach" runat="server" Text='<%#Eval("Publish_Pdf")%>' Visible="false"></asp:Label>
                                        </div>
                                        <div style="width: 90%; float: left; text-align: center">
                                            <asp:TextBox ID="txt_chuthich" runat="server" TextMode="MultiLine" Height="30px"
                                                Width="100%" Text='<%#Eval("Comments")%>'></asp:TextBox></div>
                                    </ItemTemplate>
                                </asp:DataList>
                            </div>
                            <div style="text-align: left; width: 100%">
                                <asp:Button ID="btnbackpage" runat="server" CssClass="iconSend" Text="Chuyển cho nhà in"
                                    OnClick="btnbackpage_Click" />
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </td>
        </tr>
    </table>
</asp:Content>
