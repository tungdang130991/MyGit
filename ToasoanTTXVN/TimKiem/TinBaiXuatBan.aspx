<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/ITC.Master" AutoEventWireup="true"
    CodeBehind="TinBaiXuatBan.aspx.cs" Inherits="ToasoanTTXVN.TimKiem.TinBaiXuatBan" %>

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

    <div class="divPanelSearch">
        <div style="width: 99%; padding: 5px 0.5%">
            <table border="0" cellpadding="4" cellspacing="4" style="width: 100%; text-align: left">
                <tr>
                    <td class="Titlelbl" style="text-align: left">
                        <%=CommonLib.ReadXML("lblAnpham") %>
                    </td>
                    <td class="Titlelbl" style="text-align: left">
                        <%=CommonLib.ReadXML("lblSobao") %>
                    </td>
                    <td class="Titlelbl" style="text-align: left">
                        <%=CommonLib.ReadXML("lblTrang") %>
                    </td>
                    <td class="Titlelbl" style="text-align: left">
                        <%=CommonLib.ReadXML("lblChuyenmuc") %>
                    </td>
                    <td class="Titlelbl" style="text-align: left">
                        <%=CommonLib.ReadXML("lblTacgia") %>
                    </td>
                    <td class="Titlelbl" style="text-align: left">
                        <%=CommonLib.ReadXML("lblTukhoa") %>
                    </td>
                    <td class="Titlelbl" style="text-align: left">
                        <%=CommonLib.ReadXML("lblTungay") %>
                    </td>
                    <td class="Titlelbl" style="text-align: left">
                        <%=CommonLib.ReadXML("lblDenngay") %>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: left">
                        <asp:UpdatePanel ID="UpdatePanelLB" runat="server">
                            <ContentTemplate>
                                <asp:DropDownList AutoPostBack="true" ID="cboAnPham" runat="server" Width="100%"
                                    CssClass="inputtext" DataTextField="Ten_Anpham" DataValueField="Ma_Anpham" OnSelectedIndexChanged="cboAnPham_SelectedIndexChanged"
                                    TabIndex="1">
                                </asp:DropDownList>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                    <td style="text-align: left">
                        <asp:UpdatePanel ID="UpdatePanelsb" runat="server">
                            <ContentTemplate>
                                <asp:DropDownList ID="cboSoBao" runat="server" Width="100%" CssClass="inputtext"
                                    DataTextField="Ten_Sobao" DataValueField="Ma_Sobao" TabIndex="5">
                                </asp:DropDownList>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                    <td style="text-align: left">
                        <asp:UpdatePanel ID="pnlTrang" runat="server">
                            <ContentTemplate>
                                <asp:DropDownList ID="cboPage" runat="server" AutoPostBack="true" Width="100%" OnSelectedIndexChanged="cboPage_OnSelectedIndexChanged"
                                    CssClass="inputtext">
                                </asp:DropDownList>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                    <td style="text-align: left">
                        <asp:UpdatePanel ID="UpdatePanelcm" runat="server">
                            <ContentTemplate>
                                <asp:DropDownList ID="cbo_chuyenmuc" runat="server" Width="100%" CssClass="inputtext"
                                    DataTextField="tenchuyenmuc" DataValueField="id" TabIndex="5">
                                </asp:DropDownList>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                    <td style="text-align: left">
                        <asp:TextBox ID="txt_PVCTV" runat="server" Width="95%" CssClass="inputtext"></asp:TextBox>
                        <asp:HiddenField ID="HiddenFieldTacgiatin" runat="server" />
                        <ajaxtoolkit:AutoCompleteExtender runat="server" ID="autoCompleteTacgiaTin" TargetControlID="txt_PVCTV"
                            ServicePath="../UploadFileMulti/AutoComplete.asmx" ServiceMethod="GetCompletionList"
                            ContextKey="2" CompletionListCssClass="CompletionListCssClass" MinimumPrefixLength="1"
                            CompletionInterval="1000" EnableCaching="true" CompletionSetCount="20" OnClientItemSelected="ClientItemSelectedTacGiaTin">
                        </ajaxtoolkit:AutoCompleteExtender>
                    </td>
                    <td style="text-align: left;">
                        <asp:TextBox ID="txt_tieude" TabIndex="1" Width="95%" runat="server" CssClass="inputtext"
                            onkeypress="return clickButton(event,'ctl00_MainContent_btnTimkiem');"></asp:TextBox>
                    </td>
                    <td style="text-align: left">
                        <asp:TextBox ID="txt_tungay" runat="server" Width="120px" CssClass="inputtext" MaxLength="10"
                            ToolTip="Từ ngày" onkeypress="AscciiDisable()" onfocus="javascript:vDateType='3'"
                            onkeyup="DateFormat(this,this.value,event,false,'3')" onblur="DateFormat(this,this.value,event,true,'3')"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="Regularexpressionvalidator3" runat="server" ControlToValidate="txt_tungay"
                            ValidationExpression="^\d{1,2}/\d{1,2}/\d{4}$" Display="Dynamic">Kiểu ngày:dd/MM/yyyy</asp:RegularExpressionValidator>
                    </td>
                    <td style="text-align: left">
                        <asp:TextBox ID="txt_denngay" runat="server" Width="120px" CssClass="inputtext" MaxLength="10"
                            ToolTip="Đến ngày" onkeypress="AscciiDisable()" onfocus="javascript:vDateType='3'"
                            onkeyup="DateFormat(this,this.value,event,false,'3')" onblur="DateFormat(this,this.value,event,true,'3')"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="Regularexpressionvalidator1" runat="server" ControlToValidate="txt_denngay"
                            ValidationExpression="^\d{1,2}/\d{1,2}/\d{4}$" Display="Dynamic">Kiểu ngày:dd/MM/yyyy</asp:RegularExpressionValidator>
                    </td>
                </tr>
                <tr>
                    <td style="width: 100%; text-align: center" colspan="8">
                        <asp:UpdatePanel ID="UpdatePanelTimKiem" runat="server">
                            <ContentTemplate>
                                <asp:Button runat="server" ID="btnTimkiem" CssClass="iconFind" OnClick="btnTimkiem_Click"
                                    Text="<%$Resources:cms.language, lblTimkiem%>"></asp:Button>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                </tr>
            </table>
        </div>
        <div id="divbutton" style="width: 100%; float: left; text-align: left; margin-top: 10px;
            display: none">
            <asp:LinkButton runat="server" ID="Linkdownloadfile" CausesValidation="false" OnClick="Linkdownloadfile_Click"
                Text="Download files" CssClass="iconBack"> </asp:LinkButton>
        </div>
        <div class="divPanelResult">
            <asp:UpdatePanel ID="UpdatePanelListTinbai" runat="server">
                <Triggers>
                    <asp:PostBackTrigger ControlID="DataGrid_tinbai" />
                </Triggers>
                <ContentTemplate>
                    <div style="width: 99%; padding: 5px 0.5%">
                        <asp:DataGrid ID="DataGrid_tinbai" runat="server" Width="100%" BorderStyle="None"
                            AutoGenerateColumns="False" CssClass="Grid" CellPadding="1" DataKeyField="Ma_Tinbai"
                            BorderWidth="1px" OnItemDataBound="dgData_ItemDataBound" OnEditCommand="DataGrid_EditCommand">
                            <ItemStyle CssClass="GridItem"></ItemStyle>
                            <AlternatingItemStyle CssClass="GridAltItem" />
                            <HeaderStyle CssClass="GridHeader" Height="30px"></HeaderStyle>
                            <Columns>
                                <asp:BoundColumn DataField="Ma_Tinbai" HeaderText="Mã tin bài" Visible="False"></asp:BoundColumn>
                                <asp:TemplateColumn>
                                    <HeaderStyle HorizontalAlign="Center" Width="4%"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center" CssClass="GridBorderVerSolid"></ItemStyle>
                                    <HeaderTemplate>
                                        <asp:CheckBox ID="chkAll" onclick="javascript:SelectAllCheckboxes(this);CheckItem();"
                                            runat="server" ToolTip="Chọn tất cả"></asp:CheckBox>
                                    </HeaderTemplate>
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:CheckBox runat="server" Text='' ID='optSelect' AutoPostBack="False" CssClass="checkitem"
                                            onclick="javascript:CheckItem();"></asp:CheckBox>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn>
                                    <HeaderStyle HorizontalAlign="Center" Width="5%"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center" Width="5%" CssClass="GridBorderVerSolid"></ItemStyle>
                                    <HeaderTemplate>
                                        Download file
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:ImageButton ID="btnUpdate" Width="20px" CausesValidation="false" runat="server"
                                            ImageUrl="../Dungchung/Images/dn3.gif" ImageAlign="AbsMiddle" ToolTip="lấy tin bài"
                                            CommandName="Edit" CommandArgument="Download" BorderStyle="None"></asp:ImageButton>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="<%$Resources:cms.language, lblTenbaiviet%>">
                                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Left" CssClass="GridBorderVerSolid"></ItemStyle>
                                    <ItemTemplate>
                                        <a class="linkGridForm" href="Javascript:open_window_Scroll('<%=Global.ApplicationPath%>/Quytrinh/ViewPhienBanTinBai.aspx?Menu_ID=<%=Page.Request.QueryString["Menu_ID"]%>&ID=<%# DataBinder.Eval(Container.DataItem, "Ma_Tinbai") %>',50,500,100,800);">
                                            <b>
                                                <%# DataBinder.Eval(Container.DataItem, "Tieude" )%></b></a>
                                        <asp:Label ID="lbFiledoc" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "PathFileDocuments") %>'
                                            Visible="false">
                                        </asp:Label>
                                        <br />
                                        <%=CommonLib.ReadXML("lblChuyenmuc") %>:<b><%#HPCBusinessLogic.UltilFunc.GetTenChuyenMuc(Eval("Ma_Chuyenmuc"))%></b>
                                        <br />
                                        <%=CommonLib.ReadXML("lblAnpham") %>:<b><%#HPCBusinessLogic.UltilFunc.GetTenAnpham(Eval("Ma_Anpham"))%></b>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="<%$Resources:cms.language, lblTrang%>">
                                    <HeaderStyle HorizontalAlign="Center" Width="3%"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center" Width="3%" CssClass="GridBorderVerSolid"></ItemStyle>
                                    <ItemTemplate>
                                        <span class="linkGridForm">
                                            <%#HPCBusinessLogic.UltilFunc.GetTenSoBaoFromT_Vitri_Tinbai(Eval("Ma_Tinbai"),1)%></span>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="<%$Resources:cms.language, lblSobao%>">
                                    <HeaderStyle HorizontalAlign="Center" Width="8%"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center" Width="8%" CssClass="GridBorderVerSolid"></ItemStyle>
                                    <ItemTemplate>
                                        <span class="linkGridForm">
                                            <%#HPCBusinessLogic.UltilFunc.GetTenSoBaoFromT_Vitri_Tinbai(Eval("Ma_Tinbai"),0)%></span>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="<%$Resources:cms.language, lblTacgia%>">
                                    <HeaderStyle HorizontalAlign="Left" Width="10%"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Left" Width="10%" CssClass="GridBorderVerSolid"></ItemStyle>
                                    <ItemTemplate>
                                        <span class="linkGridForm">
                                            <%#HPCBusinessLogic.UltilFunc.GetTenTacGiaTinBai(Eval("Ma_TacGia"))%></span>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="<%$Resources:cms.language, lblNguoixuly%>">
                                    <HeaderStyle HorizontalAlign="Left" Width="10%"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Left" Width="10%" CssClass="GridBorderVerSolid"></ItemStyle>
                                    <ItemTemplate>
                                        <span class="linkGridForm">
                                            <%#HPCBusinessLogic.UltilFunc.GetUserFullName(HPCBusinessLogic.UltilFunc.GetNguoiTralaiNgayTralai(Eval("Ma_Tinbai"), 0))%></span>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="<%$Resources:cms.language, lblNgayxuly%>">
                                    <HeaderStyle HorizontalAlign="Center" Width="10%"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center" CssClass="GridBorderVerSolid"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="ngayxuly" CssClass="linkGridForm" runat="server" Text='<%#HPCBusinessLogic.UltilFunc.GetNguoiTralaiNgayTralai(Eval("Ma_Tinbai"),1) %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="<%$Resources:cms.language, lblTrangthai%>">
                                    <HeaderStyle Width="15%" HorizontalAlign="Left"></HeaderStyle>
                                    <ItemStyle Width="15%" HorizontalAlign="Left" CssClass="GridBorderVerSolid"></ItemStyle>
                                    <ItemTemplate>
                                        <span class="linkGridForm">
                                            <%#HPCComponents.Global.GetTrangThaiFrom_T_version(Eval("Ma_Tinbai"))%></span>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                            </Columns>
                        </asp:DataGrid>
                    </div>
                    <div style="text-align: right; float: right" class="pageNav">
                        <cc1:CurrentPage runat="server" ID="CurrentPage" CssClass="pageNavTotal">
                        </cc1:CurrentPage>
                        <cc1:Pager runat="server" ID="pages" OnIndexChanged="pages_IndexChanged"></cc1:Pager>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>

    <script type="text/javascript" language="javascript">
        function ClientItemSelectedTacGiaTin(sender, e) {
            $get("<%=HiddenFieldTacgiatin.ClientID %>").value = e.get_value();
        }
    </script>

</asp:Content>
