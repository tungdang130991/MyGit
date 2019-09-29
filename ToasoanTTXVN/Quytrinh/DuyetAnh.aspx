<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/ITC.Master" AutoEventWireup="true"
    CodeBehind="DuyetAnh.aspx.cs" Inherits="ToasoanTTXVN.Quytrinh.DuyetAnh" %>

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

    <div style="width: 100%; float: left; padding-bottom: 20px">
        <table border="0" cellpadding="0" width="100%" cellspacing="0">
            <tr>
                <td style="text-align: center">
                    <div style="text-align: left; width: 100%">
                        <div style="text-align: left; width: 100%; padding-top: 5px">
                            <table border="0" cellpadding="4" cellspacing="4" style="width: 100%; text-align: left">
                                <tr>
                                    <td style="width: 12%; text-align: left">
                                        <asp:DropDownList ID="cbo_anpham" runat="server" Width="100%" CssClass="inputtext"
                                            TabIndex="5">
                                        </asp:DropDownList>
                                    </td>
                                    <td style="width: 12%; text-align: left">
                                        <asp:DropDownList ID="cboSoBao" runat="server" Width="100%" CssClass="inputtext"
                                            TabIndex="5">
                                        </asp:DropDownList>
                                    </td>
                                    <td style="width: 8%; text-align: left">
                                        <asp:UpdatePanel ID="upnltrang" runat="server">
                                            <ContentTemplate>
                                                <asp:DropDownList ID="cboPage" runat="server" AutoPostBack="true" Width="100%" OnSelectedIndexChanged="cboPage_OnSelectedIndexChanged"
                                                    CssClass="inputtext">
                                                </asp:DropDownList>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </td>
                                    <td style="width: 10%; text-align: left">
                                        <asp:TextBox ID="txt_tieude" TabIndex="1" Width="95%" runat="server" CssClass="inputtext"
                                            Text="Nhập tiêu đề cần tìm" onfocus="if(this.value=='Nhập tiêu đề cần tìm') { this.value=''; }"
                                            onblur="if (this.value=='') { this.value='Nhập tiêu đề cần tìm'; }" onkeypress="return clickButton(event,'ctl00_MainContent_btnTimkiem');"></asp:TextBox>
                                    </td>
                                    <td style="width: 10%; text-align: left">
                                        <asp:TextBox ID="txt_chuthich" runat="server" CssClass="inputtext" Width="95%" Text="Nhập chú thích cần tìm"
                                            onfocus="if(this.value=='Nhập chú thích cần tìm') { this.value=''; }" onblur="if (this.value=='') { this.value='Nhập chú thích cần tìm'; }"></asp:TextBox>
                                    </td>
                                    <td style="width: 10%; text-align: left">
                                        <asp:TextBox ID="txt_PVCTV" runat="server" Width="95%" CssClass="inputtext" Text="Nhập tác giả cần tìm"
                                            onfocus="if(this.value=='Nhập tác giả cần tìm') { this.value=''; }" onblur="if (this.value=='') { this.value='Nhập tác giả cần tìm'; }"></asp:TextBox>
                                        <asp:HiddenField ID="HiddenFieldTacgiatin" runat="server" />
                                        <ajaxtoolkit:AutoCompleteExtender runat="server" ID="autoCompleteTacgiaTin" TargetControlID="txt_PVCTV"
                                            ServicePath="../UploadFileMulti/AutoComplete.asmx" ServiceMethod="GetCompletionList"
                                            ContextKey="1" CompletionListCssClass="CompletionListCssClass" MinimumPrefixLength="1"
                                            CompletionInterval="1000" EnableCaching="true" CompletionSetCount="20" OnClientItemSelected="ClientItemSelectedTacGiaTin">
                                        </ajaxtoolkit:AutoCompleteExtender>
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
                                </tr>
                                <tr>
                                    <td style="width: 100%; text-align: center" colspan="8">
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
                        <div style="text-align: left; width: 100%">
                            <asp:UpdatePanel ID="UpdatePanel_Tulieuanh" runat="server">
                                <ContentTemplate>
                                    <ajaxtoolkit:TabContainer ID="TabContainer1" runat="server" CssClass="ajax__tab_yuitabview-theme"
                                        AutoPostBack="true" ActiveTabIndex="0" OnActiveTabChanged="TabContainer1_ActiveTabChanged">
                                        <ajaxtoolkit:TabPanel HeaderText="Duyệt ảnh" ID="TabPanelDuyetAnh" runat="server">
                                            <ContentTemplate>
                                                <asp:DataList ID="dgrListImages" runat="server" RepeatColumns="5" RepeatDirection="Horizontal"
                                                    DataKeyField="Ma_Anh" Width="100%" CellPadding="4" CellSpacing="4" OnEditCommand="dgrListImages_EditCommand">
                                                    <ItemStyle Width="20%" BorderWidth="0" VerticalAlign="top" HorizontalAlign="Center">
                                                    </ItemStyle>
                                                    <ItemTemplate>
                                                        <div style="width: 80%; float: left; text-align: left; padding: 2px 0;">
                                                            <ul class="hoverbox">
                                                                <li><a href="javascript:OpenImage('<%# DataBinder.Eval(Container.DataItem, "Duongdan_Anh")%>');">
                                                                    <img src="<%=Global.TinPath%><%#Eval("Duongdan_Anh")%>" alt="<%# DataBinder.Eval(Container.DataItem, "Chuthich")%>" />
                                                                </a></li>
                                                            </ul>
                                                        </div>
                                                        <div style="width: 80%; float: left; text-align: left; padding: 2px 0;">
                                                            <a class="linkGridForm" href="Javascript:open_window_Scroll('<%=Global.ApplicationPath%>/Quytrinh/ViewPhienBanTinBai.aspx?Menu_ID=<%=Page.Request.QueryString["Menu_ID"]%>&ID=<%#Eval("Ma_Tinbai")%>',50,500,100,800);">
                                                                <b><span>Tin bài:</span></b>
                                                                <%#Eval("Tieude")%></a>
                                                        </div>
                                                        <div style="width: 80%; float: left; padding: 2px 0; text-align: left">
                                                            <b><span>Chú thích:</span></b>
                                                            <asp:Label ID="lbdesc" CssClass="linkGridForm" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Chuthich")%>'>
                                                            </asp:Label>
                                                        </div>
                                                        <div style="width: 80%; float: left; font-weight: bold; text-align: left; padding: 2px 0;">
                                                            <b><span>Tác giả:</span></b>
                                                            <asp:Label ID="lbtacgia" CssClass="linkGridForm" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "NguoiChup")%>'>
                                                            </asp:Label>
                                                        </div>
                                                        <div style="width: 80%; float: left; font-weight: bold; text-align: left; padding: 2px 0;">
                                                            <asp:TextBox ID="txt_nhanxet" runat="server" TextMode="MultiLine" Width="95%" CssClass="inputtext"
                                                                Text="Nhận xét" onfocus="if(this.value=='Nhận xét') { this.value=''; }" onblur="if (this.value=='') { this.value='Nhận xét'; }"
                                                                Height="30px"></asp:TextBox>
                                                        </div>
                                                        <div style="width: 80%; float: left; margin-top: 3px; text-align: right; padding-right: 5px">
                                                            <asp:LinkButton ID="btnUpdate" CausesValidation="false" runat="server" CssClass="iconCancel"
                                                                Text="Hủy ảnh" ToolTip="Hủy ảnh" CommandName="Edit" CommandArgument="HuyAnh"
                                                                BorderStyle="None"></asp:LinkButton>
                                                        </div>
                                                    </ItemTemplate>
                                                </asp:DataList>
                                            </ContentTemplate>
                                        </ajaxtoolkit:TabPanel>
                                        <ajaxtoolkit:TabPanel HeaderText="Ảnh bị loại" ID="TabPanelHuyDuyetAnh" runat="server">
                                            <ContentTemplate>
                                                <asp:DataList ID="DataListImgHuy" runat="server" RepeatColumns="5" RepeatDirection="Horizontal"
                                                    DataKeyField="Ma_Anh" Width="100%" CellPadding="4" CellSpacing="4" OnEditCommand="dgrListImages_EditCommand">
                                                    <ItemStyle Width="20%" BorderWidth="0" VerticalAlign="top" HorizontalAlign="Center">
                                                    </ItemStyle>
                                                    <ItemTemplate>
                                                        <div style="width: 80%; float: left; padding: 2px 0;">
                                                            <ul class="hoverbox">
                                                                <li><a href="javascript:OpenImage('<%# DataBinder.Eval(Container.DataItem, "Duongdan_Anh")%>');">
                                                                    <img src="<%=Global.TinPath%><%#Eval("Duongdan_Anh")%>" alt="<%# DataBinder.Eval(Container.DataItem, "Chuthich")%>" />
                                                                </a></li>
                                                            </ul>
                                                        </div>
                                                        <div style="width: 80%; float: left; text-align: left; padding: 2px 0;">
                                                            <a class="linkGridForm" href="Javascript:open_window_Scroll('<%=Global.ApplicationPath%>/Quytrinh/ViewPhienBanTinBai.aspx?Menu_ID=<%=Page.Request.QueryString["Menu_ID"]%>&ID=<%#Eval("Ma_Tinbai")%>',50,500,100,800);">
                                                                <b><span>Tin bài:</span></b>
                                                                <%#Eval("Tieude")%></a>
                                                        </div>
                                                        <div style="width: 80%; float: left; text-align: left; padding: 2px 0;">
                                                            <b><span>Chú thích:</span></b>
                                                            <asp:Label ID="lbdesc" CssClass="linkGridForm" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Chuthich")%>'>
                                                            </asp:Label>
                                                        </div>
                                                        <div style="width: 80%; float: left; font-weight: bold; text-align: left; padding: 2px 0;">
                                                            <b><span>Tác giả:</span></b>
                                                            <asp:Label ID="lbtacgia" CssClass="linkGridForm" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "NguoiChup")%>'>
                                                            </asp:Label>
                                                        </div>
                                                        <div style="width: 80%; float: left; font-weight: bold; text-align: left; padding: 2px 0;">
                                                            <b><span>Nhận xét:</span></b>
                                                            <asp:Label ID="Labelnhanxet" CssClass="linkGridForm" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Nhanxet")%>'>
                                                            </asp:Label>
                                                        </div>
                                                        <div style="width: 80%; float: left; margin-top: 3px; text-align: right; padding-right: 5px">
                                                            <asp:LinkButton ID="btnUpdate" CssClass="linkGridForm" CausesValidation="false" runat="server"
                                                                Text="Lấy ảnh" ToolTip="Lấy ảnh" CommandName="Edit" CommandArgument="Undo" BorderStyle="None"></asp:LinkButton>
                                                        </div>
                                                    </ItemTemplate>
                                                </asp:DataList>
                                            </ContentTemplate>
                                        </ajaxtoolkit:TabPanel>
                                    </ajaxtoolkit:TabContainer>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                </td>
            </tr>
        </table>
    </div>

    <script type="text/javascript" language="javascript">
        function ClientItemSelectedTacGiaTin(sender, e) {
            $get("<%=HiddenFieldTacgiatin.ClientID %>").value = e.get_value();
        }
    </script>

</asp:Content>
