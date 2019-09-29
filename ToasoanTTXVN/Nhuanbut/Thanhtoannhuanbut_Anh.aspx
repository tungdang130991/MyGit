<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/ITC.Master" AutoEventWireup="true"
    CodeBehind="Thanhtoannhuanbut_Anh.aspx.cs" Inherits="ToasoanTTXVN.Nhuanbut.Thanhtoannhuanbut_Anh" %>

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

    <script type="text/javascript" language="javascript">
        function ClientItemSelectedTacGiaTin(sender, e) {
            $get("<%=HiddenFieldTacgiatin.ClientID %>").value = e.get_value();
        }
    </script>

    <div class="divPanelSearch">
        <div style="width: 99%; padding: 5px 0.5%">
            <asp:UpdatePanel ID="UpdatePanelTimKiem" runat="server">
                <ContentTemplate>
                    <table border="0" cellpadding="2" cellspacing="2" style="width: 100%; text-align: left">
                        <tr>
                            <td class="Titlelbl" style="text-align: left">
                                <%=CommonLib.ReadXML("lblAnpham") %>
                            </td>
                            <td class="Titlelbl" style="text-align: left">
                                <%=CommonLib.ReadXML("lblSobao") %>
                            </td>
                            <td class="Titlelbl" style="text-align: left">
                                <%=CommonLib.ReadXML("lblChuyenmuc") %>
                            </td>
                            <td class="Titlelbl" style="text-align: left">
                                <%=CommonLib.ReadXML("lblTenbaiviet")%>
                            </td>
                            <td class="Titlelbl" style="text-align: left">
                                <%=CommonLib.ReadXML("lblTacgia")%>
                            </td>
                            <td class="Titlelbl" style="text-align: left">
                                <%=CommonLib.ReadXML("lblTungay")%>
                            </td>
                            <td class="Titlelbl" style="text-align: left">
                                <%=CommonLib.ReadXML("lblDenngay")%>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 10%; text-align: left">
                                <asp:DropDownList AutoPostBack="true" ID="cboAnPham" runat="server" Width="100%"
                                    CssClass="inputtext" DataTextField="Ten_Anpham" DataValueField="Ma_Anpham" OnSelectedIndexChanged="cboAnPham_SelectedIndexChanged"
                                    TabIndex="1">
                                </asp:DropDownList>
                            </td>
                            <td style="width: 10%; text-align: left">
                                <asp:DropDownList ID="cboSoBao" runat="server" Width="100%" CssClass="inputtext"
                                    DataTextField="Ten_Sobao" DataValueField="Ma_Sobao" TabIndex="5">
                                </asp:DropDownList>
                            </td>
                            <td style="width: 10%; text-align: left">
                                <asp:DropDownList ID="cbo_chuyenmuc" runat="server" Width="100%" CssClass="inputtext"
                                    DataTextField="tenchuyenmuc" DataValueField="id" TabIndex="5">
                                </asp:DropDownList>
                            </td>
                            <td style="width: 10%; text-align: left">
                                <asp:TextBox ID="txt_tieude" TabIndex="1" Width="95%" runat="server" CssClass="inputtext"
                                    placeholder="<%$Resources:cms.language, lblNhaptieudecantim%>" onkeypress="return clickButton(event,'ctl00_MainContent_cmdSeek');"></asp:TextBox>
                            </td>
                            <td style="text-align: left; width: 10%">
                                <asp:TextBox ID="txt_PVCTV" runat="server" Width="95%" CssClass="inputtext" placeholder="<%$Resources:cms.language, lblNhaptacgiacantim%>"></asp:TextBox>
                                <asp:HiddenField ID="HiddenFieldTacgiatin" runat="server" />
                                <ajaxtoolkit:AutoCompleteExtender runat="server" ID="autoCompleteTacgiaTin" TargetControlID="txt_PVCTV"
                                    ServicePath="../UploadFileMulti/AutoComplete.asmx" ServiceMethod="GetCompletionList"
                                    ContextKey="2" CompletionListCssClass="CompletionListCssClass" MinimumPrefixLength="1"
                                    CompletionInterval="1000" EnableCaching="true" CompletionSetCount="20" OnClientItemSelected="ClientItemSelectedTacGiaTin">
                                </ajaxtoolkit:AutoCompleteExtender>
                            </td>
                            <td style="width: 10%; text-align: left">
                                <asp:TextBox ID="txt_tungay" runat="server" Width="120px" CssClass="inputtext" MaxLength="10"
                                    onKeyPress="AscciiDisable()" onfocus="javascript:vDateType='3'" onKeyUp="DateFormat(this,this.value,event,false,'3')"
                                    onBlur="DateFormat(this,this.value,event,true,'3')"></asp:TextBox>
                                <asp:RegularExpressionValidator ID="Regularexpressionvalidator3" runat="server" ControlToValidate="txt_tungay"
                                    ValidationExpression="^\d{1,2}/\d{1,2}/\d{4}$" Display="Dynamic">Kiểu ngày:dd/MM/yyyy</asp:RegularExpressionValidator>
                            </td>
                            <td style="width: 10%; text-align: left">
                                <asp:TextBox ID="txt_denngay" runat="server" Width="120px" CssClass="inputtext" MaxLength="10"
                                    onKeyPress="AscciiDisable()" onfocus="javascript:vDateType='3'" onKeyUp="DateFormat(this,this.value,event,false,'3')"
                                    onBlur="DateFormat(this,this.value,event,true,'3')"></asp:TextBox>
                                <asp:RegularExpressionValidator ID="Regularexpressionvalidator1" runat="server" ControlToValidate="txt_denngay"
                                    ValidationExpression="^\d{1,2}/\d{1,2}/\d{4}$" Display="Dynamic">Kiểu ngày:dd/MM/yyyy</asp:RegularExpressionValidator>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: center" colspan="7">
                                <asp:Button runat="server" ID="btnTimkiem" CssClass="iconFind" Font-Bold="true" OnClick="btnTimkiem_Click"
                                    Text="<%$Resources:cms.language, lblTimkiem%>"></asp:Button>
                                <asp:Button ID="btn_chamnhuanbut" OnClick="btn_chamnhuanbut_click" runat="server"
                                    Text="<%$Resources:cms.language,lblChamnhuanbut %>" CssClass="iconSend" />
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </asp:UpdatePanel>
            <asp:UpdatePanel ID="UpdatePanelListTinbai" runat="server">
                <ContentTemplate>
                    <div style="text-align: left; width: 100%">
                        <asp:DataGrid ID="DataGrid_tinbaiAnh" runat="server" Width="100%" BorderStyle="None"
                            CssClass="Grid" AutoGenerateColumns="False" BorderColor="#d4d4d4" CellPadding="0"
                            DataKeyField="Ma_Anh" BackColor="White" BorderWidth="1px" AlternatingItemStyle-BackColor="#F1F1F2"
                            OnEditCommand="DataGrid_tinbaiAnh_EditCommand" OnItemDataBound="DataGrid_tinbaiAnh_ItemDataBound">
                            <ItemStyle CssClass="GridItem"></ItemStyle>
                            <AlternatingItemStyle CssClass="GridAltItem" />
                            <HeaderStyle CssClass="GridHeader"></HeaderStyle>
                            <Columns>
                                <asp:TemplateColumn>
                                    <HeaderStyle HorizontalAlign="Center" Width="4%"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center" CssClass="GridBorderVerSolid"></ItemStyle>
                                    <HeaderTemplate>
                                        <asp:CheckBox ID="chkAll" onclick="javascript:SelectAllCheckboxes(this);" runat="server"
                                            ToolTip="Chọn tất cả"></asp:CheckBox>
                                    </HeaderTemplate>
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:CheckBox runat="server" Text='' ID='optSelect' AutoPostBack="False"></asp:CheckBox>
                                        <asp:Label ID="lblMatinbai" Visible="false" runat="server" Text='<%#DataBinder.Eval(Container, "DataItem.Ma_Tinbai")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:BoundColumn DataField="Ma_Anh" HeaderText="Ma_Anh" Visible="False"></asp:BoundColumn>
                                <asp:TemplateColumn>
                                    <HeaderStyle HorizontalAlign="Center" Width="20%"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Left" Width="20%" CssClass="GridBorderVerSolid"></ItemStyle>
                                    <HeaderTemplate>
                                        <%=CommonLib.ReadXML("lblTenbaiviet") %>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <a class="linkGridForm" href="Javascript:open_window_Scroll('<%=Global.ApplicationPath%>/Quytrinh/ViewPhienBanTinBai.aspx?Menu_ID=<%=Page.Request.QueryString["Menu_ID"]%>&ID=<%# DataBinder.Eval(Container.DataItem, "Ma_Tinbai") %>',50,500,100,800);">
                                            <%# DataBinder.Eval(Container.DataItem, "Tieude" )%></a>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn>
                                    <HeaderStyle HorizontalAlign="Center" Width="8%"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center" Width="8%" CssClass="GridBorderVerSolid"></ItemStyle>
                                    <HeaderTemplate>
                                        File image
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <a class="linkGridForm" href="javascript:OpenImage('<%# DataBinder.Eval(Container.DataItem, "Duongdan_Anh")%>');">
                                            <img src="<%=Global.TinPath%><%# DataBinder.Eval(Container.DataItem, "Duongdan_Anh")%>"
                                                style="border: 0; width: 80px; height: 40px" alt="" />
                                        </a>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn>
                                    <HeaderStyle HorizontalAlign="Center" Width="8%"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Left" Width="8%" CssClass="GridBorderVerSolid"></ItemStyle>
                                    <HeaderTemplate>
                                        <%=CommonLib.ReadXML("lblAnpham") %>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <span class="linkGridForm">
                                            <%#HPCBusinessLogic.UltilFunc.GetTenAnpham(Eval("Ma_Anpham"))%></span>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn>
                                    <HeaderStyle HorizontalAlign="Center" Width="5%"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center" Width="5%" CssClass="GridBorderVerSolid"></ItemStyle>
                                    <HeaderTemplate>
                                        <%=CommonLib.ReadXML("lblTrang") %>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <span class="linkGridForm">
                                            <%#HPCBusinessLogic.UltilFunc.GetTenSoBaoFromT_Vitri_Tinbai(Eval("Ma_Tinbai"),1)%></span>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn>
                                    <HeaderStyle HorizontalAlign="Center" Width="5%"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center" Width="5%" CssClass="GridBorderVerSolid"></ItemStyle>
                                    <HeaderTemplate>
                                        <%=CommonLib.ReadXML("lblSobao") %>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <span class="linkGridForm">
                                            <%#HPCBusinessLogic.UltilFunc.GetTenSoBaoFromT_Vitri_Tinbai(Eval("Ma_Tinbai"),0)%></span>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn>
                                    <HeaderStyle Width="12%" HorizontalAlign="Center"></HeaderStyle>
                                    <ItemStyle Width="12%" HorizontalAlign="Left" CssClass="GridBorderVerSolid"></ItemStyle>
                                    <HeaderTemplate>
                                        <%=CommonLib.ReadXML("lblChuyenmuc") %>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <span class="linkGridForm">
                                            <%#HPCBusinessLogic.UltilFunc.GetTenChuyenMuc(Eval("Ma_Chuyenmuc"))%></span>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn>
                                    <HeaderStyle HorizontalAlign="Center" Width="8%"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Left" Width="8%" CssClass="GridBorderVerSolid"></ItemStyle>
                                    <HeaderTemplate>
                                        <%=CommonLib.ReadXML("lblTacgia") %>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblTacgiaanh" Visible="false" runat="server" Text='<%#Eval("Tacgiaanh") %>'></asp:Label>
                                        <span class="linkGridForm">
                                            <%#HPCBusinessLogic.UltilFunc.GetTenTacGiaTinBai(Eval("Tacgiaanh"))%></span>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn>
                                    <HeaderStyle HorizontalAlign="Center" Width="8%"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Right" Width="8%" CssClass="GridBorderVerSolid"></ItemStyle>
                                    <HeaderTemplate>
                                        <%=CommonLib.ReadXML("lblNhuanbut") %>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <div style="text-align: right">
                                            <asp:Label ID="lblsotientinbai" runat="server" Font-Bold="true" Font-Size="12px"
                                                Font-Names="Arial" Text='<%#DataBinder.Eval(Container, "DataItem.Sotien")!=System.DBNull.Value? String.Format("{0:00,0}", Convert.ToDecimal( DataBinder.Eval(Container.DataItem, "Sotien"))):""%>'></asp:Label></div>
                                        <div style="text-align: right">
                                            <asp:TextBox ID="txtsotientinbai" runat="server" Visible="false" Width="100px" Text='<%#DataBinder.Eval(Container, "DataItem.Sotien")%>'
                                                onkeyup="javascript:return Comma(this.id)"></asp:TextBox></div>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn>
                                    <HeaderStyle Width="10%" HorizontalAlign="Center"></HeaderStyle>
                                    <ItemStyle Width="10%" HorizontalAlign="Left" CssClass="GridBorderVerSolid"></ItemStyle>
                                    <HeaderTemplate>
                                        <%=CommonLib.ReadXML("lblNguoicham") %>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <span class="linkGridForm">
                                            <%#HPCBusinessLogic.UltilFunc.GetUserFullName(Eval("Nguoicham"))%></span>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn>
                                    <HeaderStyle Width="10%" HorizontalAlign="Center"></HeaderStyle>
                                    <ItemStyle Width="10%" HorizontalAlign="Left" CssClass="GridBorderVerSolid"></ItemStyle>
                                    <HeaderTemplate>
                                        <%=CommonLib.ReadXML("lblGhichu") %>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="LabelGhichu" CssClass="linkGridForm" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Ghichu" )%>'></asp:Label>
                                        <asp:TextBox ID="txt_ghichu" TextMode="MultiLine" Width="200px" Height="30px" runat="server"
                                            Visible="false" Text='<%# DataBinder.Eval(Container.DataItem, "Ghichu" )%>'></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn>
                                    <HeaderStyle HorizontalAlign="Center" Width="3%"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center" Width="3%" CssClass="GridBorderVerSolid"></ItemStyle>
                                    <HeaderTemplate>
                                        <%=CommonLib.ReadXML("lblCham") %>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:ImageButton ID="btnThanhtoan" runat="server" ImageUrl='../Dungchung/Images/Thanhtoan.png'
                                            Width="30px" Height="30px" ImageAlign="AbsMiddle" CommandName="Edit" CommandArgument="EditThanhtoan"
                                            BorderStyle="None" />
                                        <asp:ImageButton ID="btnUpdate" Width="15px" runat="server" ImageUrl="~/Dungchung/images/save.gif"
                                            Visible="false" ImageAlign="AbsMiddle" ToolTip="Cập nhật" CommandName="Edit"
                                            CommandArgument="Capnhat" BorderStyle="None"></asp:ImageButton>
                                        <asp:ImageButton ID="btnCancel" Width="15px" runat="server" ImageUrl="~/Dungchung/images/undo.gif"
                                            Visible="false" ImageAlign="AbsMiddle" ToolTip="Hủy bỏ" CommandName="Edit" CommandArgument="Cancel"
                                            BorderStyle="None"></asp:ImageButton>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                            </Columns>
                        </asp:DataGrid>
                    </div>
                    <div style="text-align: right" class="pageNav">
                        <cc1:CurrentPage runat="server" ID="CurrentPage" CssClass="pageNavTotal">
                        </cc1:CurrentPage>
                        <cc1:Pager runat="server" ID="pages" OnIndexChanged="pages_IndexChanged"></cc1:Pager>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>
