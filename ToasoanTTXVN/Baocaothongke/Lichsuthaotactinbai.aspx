<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/ITC.Master" AutoEventWireup="true"
    CodeBehind="Lichsuthaotactinbai.aspx.cs" Inherits="ToasoanTTXVN.Baocaothongke.Lichsuthaotactinbai" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxtoolkit" %>
<%@ Register TagPrefix="cc1" Namespace="HPCComponents.UI.WebControls" Assembly="HPCComponents" %>
<%@ Import Namespace="HPCComponents" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <script type="text/javascript" language="javascript">
        function Cancellichsuthaotactinbai() {
            $find("ctl00_MainContent_ModalPopupThaotactinbai").hide();
            return false;
        }</script>

    <script type="text/javascript" language="javascript">
        function ClientItemSelectedTacGiaTin(sender, e) {
            $get("<%=HiddenFieldTacgiatin.ClientID %>").value = e.get_value();
        }
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
                <table border="0" cellpadding="0" width="100%" cellspacing="0">
                    <tr>
                        <td style="text-align: center">
                            <div class="classSearchHeader">
                                <div style="width: 99%">
                                    <table border="0" cellpadding="2" cellspacing="2" style="width: 100%; text-align: left">
                                        <tr>
                                            <td class="Titlelbl" style="text-align:left">
                                                <%=CommonLib.ReadXML("lblAnPham") %>
                                            </td>
                                            <td class="Titlelbl" style="text-align:left">
                                                <%=CommonLib.ReadXML("lblSobao") %>
                                            </td>
                                            <td class="Titlelbl" style="text-align:left">
                                                <%=CommonLib.ReadXML("lblChuyenmuc")%>
                                            </td>
                                            <td class="Titlelbl" style="text-align:left">
                                                <%=CommonLib.ReadXML("lblNguoisudung")%>
                                            </td>
                                            <td class="Titlelbl" style="text-align:left">
                                                <%=CommonLib.ReadXML("lblTacgia")%>
                                            </td>
                                            <td class="Titlelbl" style="text-align:left">
                                                <%=CommonLib.ReadXML("lblTenbaiviet")%>
                                            </td>
                                            <td></td>
                                        </tr>
                                        <tr>
                                            <td style="width: 10%; text-align: left">
                                                <asp:UpdatePanel ID="upnllb" runat="server">
                                                    <ContentTemplate>
                                                        <asp:DropDownList AutoPostBack="true" ID="cboAnPham" runat="server" Width="100%"
                                                            CssClass="inputtext" DataTextField="Ten_Anpham" DataValueField="Ma_Anpham" OnSelectedIndexChanged="cboAnPham_SelectedIndexChanged"
                                                            TabIndex="1">
                                                        </asp:DropDownList>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </td>
                                            <td style="width: 10%; text-align: left">
                                                <asp:UpdatePanel ID="UpdatePanelsb" runat="server">
                                                    <ContentTemplate>
                                                        <asp:DropDownList ID="cboSoBao" runat="server" Width="100%" CssClass="inputtext"
                                                            DataTextField="Ten_Sobao" DataValueField="Ma_Sobao" TabIndex="5">
                                                        </asp:DropDownList>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </td>
                                            <td style="width: 10%; text-align: left">
                                                <asp:UpdatePanel ID="UpdatePanelcm" runat="server">
                                                    <ContentTemplate>
                                                        <asp:DropDownList AutoPostBack="true" ID="cbo_chuyenmuc" runat="server" Width="100%"
                                                            CssClass="inputtext" DataTextField="tenchuyenmuc" DataValueField="id" TabIndex="5">
                                                        </asp:DropDownList>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </td>
                                            <td style="text-align: left; width: 10%">
                                                <asp:DropDownList ID="cbodoituong" runat="server" Width="100%" CssClass="inputtext"
                                                    DataTextField="Ten_Doituong" DataValueField="Ma_Doituong" TabIndex="5">
                                                </asp:DropDownList>
                                            </td>
                                            <td style="width: 10%; text-align: left">
                                                <asp:TextBox ID="txt_PVCTV" runat="server" Width="93%" CssClass="inputtext" ></asp:TextBox>
                                                <asp:HiddenField ID="HiddenFieldTacgiatin" runat="server" />
                                                <ajaxtoolkit:AutoCompleteExtender runat="server" ID="autoCompleteTacgiaTin" TargetControlID="txt_PVCTV"
                                                    ServicePath="../UploadFileMulti/AutoComplete.asmx" ServiceMethod="GetCompletionList"
                                                    ContextKey="2" CompletionListCssClass="CompletionListCssClass" MinimumPrefixLength="1"
                                                    CompletionInterval="1000" EnableCaching="true" CompletionSetCount="20" OnClientItemSelected="ClientItemSelectedTacGiaTin">
                                                </ajaxtoolkit:AutoCompleteExtender>
                                            </td>
                                            <td style="width: 12%; text-align: left" colspan="3">
                                                <asp:TextBox ID="txt_tieude" TabIndex="1" Width="95%" runat="server" CssClass="inputtext"
                                                     onkeypress="return clickButton(event,'ctl00_MainContent_cmdSeek');"></asp:TextBox>
                                            </td>
                                            <td style="width: 8%; text-align: left">
                                                <asp:UpdatePanel ID="UpdatePanelsearch" runat="server">
                                                    <ContentTemplate>
                                                        <asp:Button runat="server" ID="btnTimkiem" CssClass="iconFind" Font-Bold="true" OnClick="btnTimkiem_Click"
                                                            Text="<%$Resources:cms.language,lblTimkiem %>"></asp:Button>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </div>
                            <div style="height:5px"></div>
                            <div class="classSearchHeader">
                                <asp:UpdatePanel ID="upnllist" runat="server">
                                    <ContentTemplate>
                                        <div style="width: 99%;">
                                            <asp:DataGrid ID="DataGrid_tinbai" runat="server" Width="100%" BorderStyle="None"
                                                AutoGenerateColumns="False" CssClass="Grid" CellPadding="1" DataKeyField="Ma_Tinbai"
                                                BorderWidth="1px" OnItemDataBound="dgData_ItemDataBound" OnEditCommand="DataGrid_tinbai_EditCommand">
                                                <ItemStyle CssClass="GridItem" Height="25px"></ItemStyle>
                                                <AlternatingItemStyle CssClass="GridAltItem" />
                                                <HeaderStyle CssClass="GridHeader" Height="30px"></HeaderStyle>
                                                <Columns>
                                                    <asp:BoundColumn DataField="Ma_Tinbai" HeaderText="#ID" Visible="true" >
                                                        <HeaderStyle HorizontalAlign="Center" Font-Bold=true Width="5%" BackColor="#cccccc" />
                                                        <ItemStyle HorizontalAlign="Center" Font-Bold=true BackColor="#cccccc" />
                                                    </asp:BoundColumn>
                                                    <asp:TemplateColumn>
                                                        <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Left" CssClass="GridBorderVerSolid"></ItemStyle>
                                                        <HeaderTemplate>
                                                            <%=CommonLib.ReadXML("lblTenbaiviet") %>                                                            
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:LinkButton CssClass="linkGridForm" Font-Bold=true Text='<%# DataBinder.Eval(Container.DataItem, "Tieude" )%>'
                                                                runat="server" ID="linkTittle" CommandName="Edit" CommandArgument="Edit" ToolTip="View"></asp:LinkButton>
                                                            <br />
                                                            <%=CommonLib.ReadXML("lblChuyenmuc") %>:<b> <%#HPCBusinessLogic.UltilFunc.GetTenChuyenMuc(Eval("Ma_Chuyenmuc"))%></b>
                                                            <br />
                                                            <%=CommonLib.ReadXML("lblAnpham") %>:<b><%#HPCBusinessLogic.UltilFunc.GetTenAnpham(Eval("Ma_Anpham"))%></b>
                                                           
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                    
                                                    <asp:TemplateColumn>
                                                        <HeaderStyle HorizontalAlign="Center" Width="5%"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Center" Width="5%" CssClass="GridBorderVerSolid"></ItemStyle>
                                                        <HeaderTemplate>
                                                            <%=CommonLib.ReadXML("lblTrang") %>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>                                                            
                                                                <b><%#HPCBusinessLogic.UltilFunc.GetTenSoBaoFromT_Vitri_Tinbai(Eval("Ma_Tinbai"),1)%></b>
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                    <asp:TemplateColumn>
                                                        <HeaderStyle HorizontalAlign="Center" Width="8%"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Center" Width="8%" CssClass="GridBorderVerSolid"></ItemStyle>
                                                        <HeaderTemplate>
                                                            <%=CommonLib.ReadXML("lblSobao") %>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <b>
                                                                <%#HPCBusinessLogic.UltilFunc.GetTenSoBaoFromT_Vitri_Tinbai(Eval("Ma_Tinbai"),0)%></b>
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                    
                                                    <asp:TemplateColumn>
                                                        <HeaderStyle HorizontalAlign="Left" Width="10%"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Left" Width="10%" CssClass="GridBorderVerSolid"></ItemStyle>
                                                        <HeaderTemplate>
                                                            <%=CommonLib.ReadXML("lblTacgia") %>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <span class="linkGridForm">
                                                                <%#HPCBusinessLogic.UltilFunc.GetTenTacGiaTinBai(Eval("Ma_TacGia"))%></span>
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                    <asp:TemplateColumn>
                                                        <HeaderStyle HorizontalAlign="Left" Width="10%"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Left" Width="10%" CssClass="GridBorderVerSolid"></ItemStyle>
                                                        <HeaderTemplate>
                                                           <%=CommonLib.ReadXML("lblNguoixuly")%>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <span class="linkGridForm">
                                                                <%#HPCBusinessLogic.UltilFunc.GetUserFullName(HPCBusinessLogic.UltilFunc.GetNguoiTralaiNgayTralai(Eval("Ma_Tinbai"), 0))%></span>
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                    <asp:TemplateColumn HeaderText="<%$Resources:cms.language,lblNgayxuly %>">
                                                        <HeaderStyle HorizontalAlign="Center" Width="10%"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Center" CssClass="GridBorderVerSolid"></ItemStyle>
                                                        <ItemTemplate>
                                                            <asp:Label ID="ngayxuly" CssClass="linkGridForm" runat="server" Text='<%#HPCBusinessLogic.UltilFunc.GetNguoiTralaiNgayTralai(Eval("Ma_Tinbai"),1) %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                    <asp:TemplateColumn HeaderText="<%$Resources:cms.language,lblTrangthai %>">
                                                        <HeaderStyle Width="12%" HorizontalAlign="Left"></HeaderStyle>
                                                        <ItemStyle Width="12%" HorizontalAlign="Left" CssClass="GridBorderVerSolid"></ItemStyle>
                                                        <ItemTemplate>
                                                            <span class="linkGridForm">
                                                                <%#HPCComponents.Global.GetTrangThaiFrom_T_version(Eval("Ma_Tinbai"))%></span>
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                </Columns>
                                            </asp:DataGrid>
                                        </div>
                                        <div style="text-align: right; width: 99%;" class="pageNav">
                                            <cc1:CurrentPage runat="server" ID="CurrentPage" CssClass="pageNavTotal">
                                            </cc1:CurrentPage>
                                            <cc1:Pager runat="server" ID="pages" OnIndexChanged="pages_IndexChanged"></cc1:Pager>
                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                            <div>
                                <asp:UpdatePanel ID="UpnlThaotactinbai" runat="server">
                                    <ContentTemplate>
                                        <div style="width: 100%">
                                            <a id="hnkAddMenu" runat="server" style="visibility: hidden"></a>
                                            <ajaxtoolkit:ModalPopupExtender ID="ModalPopupThaotactinbai" BackgroundCssClass="ModalPopupBG"
                                                runat="server" TargetControlID="hnkAddMenu" CancelControlID="btnCancel" PopupControlID="Panelone"
                                                Drag="true" PopupDragHandleControlID="PopupHeader">
                                            </ajaxtoolkit:ModalPopupExtender>
                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                                <div id="Panelone" style="display: none; width: 800px; height: auto">
                                    <div class="popup_Container">
                                        <div class="popup_Titlebar" id="PopupHeader">
                                            <div class="TitlebarLeft">
                                                <asp:Literal runat="server" ID="litTittleForm" Text="View history"></asp:Literal>
                                            </div>
                                            <div class="TitlebarRight" onclick="Cancellichsuthaotactinbai();">
                                            </div>
                                        </div>
                                        <div class="popup_Body">
                                            <div id="displayContainer2">
                                                <asp:UpdatePanel ID="upnlpoup" runat="server">
                                                    <ContentTemplate>
                                                        <table width="100%" cellspacing="2" cellpadding="2" border="0" style="background-color: white;">
                                                            <tr>
                                                                <td style="width: 100%">
                                                                    <div style="height: 400px; width: 100%; vertical-align: top; overflow:auto; padding-right:5px; overflow-y : scroll;">
                                                                        <asp:DataGrid runat="server" ID="DataGridLichsuthaotactinbai" AutoGenerateColumns="False"
                                                                            CssClass="Grid" DataKeyField="Log_ID" BorderColor="#D9D9D9"  AlternatingItemStyle-BackColor="#F1F1F2"
                                                                            BackColor="White" Width="100%" BorderWidth="1px">
                                                                            <ItemStyle CssClass="GridItem" Height="25px"></ItemStyle>
                                                                            <AlternatingItemStyle CssClass="GridAltItem" />
                                                                            <HeaderStyle CssClass="GridHeader" Height="30px"></HeaderStyle>
                                                                            <Columns>
                                                                                <asp:BoundColumn Visible="true" HeaderText="#ID"  DataField="Log_ID">
                                                                                    <HeaderStyle Font-Bold=true Width="2%" BackColor="#cccccc"></HeaderStyle>
                                                                                    <ItemStyle HorizontalAlign="Center" BackColor="#cccccc" Font-Bold=true />
                                                                                </asp:BoundColumn>
                                                                                <asp:TemplateColumn>
                                                                                    <HeaderStyle HorizontalAlign="Left" Width="12%"></HeaderStyle>
                                                                                    <ItemStyle HorizontalAlign="Left" Width="12%" ></ItemStyle>
                                                                                    <HeaderTemplate>
                                                                                        <%=CommonLib.ReadXML("lblNguoisudung")%>
                                                                                    </HeaderTemplate>
                                                                                    <ItemTemplate>
                                                                                        <span class="linkGridForm">
                                                                                            <%# DataBinder.Eval(Container.DataItem, "TenDaydu")%></span>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateColumn>
                                                                                <asp:TemplateColumn>
                                                                                    <HeaderStyle HorizontalAlign="Center" Width="8%"></HeaderStyle>
                                                                                    <ItemStyle HorizontalAlign="Center" Width="8%" CssClass="GridBorderVerSolid"></ItemStyle>
                                                                                    <HeaderTemplate>
                                                                                        <%=CommonLib.ReadXML("lblDiachi")%>
                                                                                    </HeaderTemplate>
                                                                                    <ItemTemplate>
                                                                                        <span class="linkGridForm">
                                                                                            <%# DataBinder.Eval(Container.DataItem, "HostIP")%></span>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateColumn>
                                                                                <asp:TemplateColumn>
                                                                                    <HeaderStyle HorizontalAlign="Center" Width="10%"></HeaderStyle>
                                                                                    <ItemStyle HorizontalAlign="Center" Width="10%" CssClass="GridBorderVerSolid"></ItemStyle>
                                                                                    <HeaderTemplate>
                                                                                        <%=CommonLib.ReadXML("lblNgayxuly") %>
                                                                                    </HeaderTemplate>
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="ngaythaotac" CssClass="linkGridForm" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.NgayThaotac")!=System.DBNull.Value?Convert.ToDateTime(DataBinder.Eval(Container, "DataItem.NgayThaotac")).ToString("dd/MM/yyyy HH:mm:ss"):"" %>'>
                                                                                        </asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateColumn>
                                                                                <asp:TemplateColumn>
                                                                                    <HeaderStyle HorizontalAlign="Left" Width="30%"></HeaderStyle>
                                                                                    <ItemStyle HorizontalAlign="Left" Width="30%" CssClass="GridBorderVerSolid"></ItemStyle>
                                                                                    <HeaderTemplate>
                                                                                        <%=CommonLib.ReadXML("lblGhichu") %>
                                                                                    </HeaderTemplate>
                                                                                    <ItemTemplate>
                                                                                        <span class="linkGridForm">
                                                                                            <%# DataBinder.Eval(Container.DataItem, "Thaotac")%></span>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateColumn>
                                                                            </Columns>
                                                                            <AlternatingItemStyle BackColor="#F1F1F2" />
                                                                        </asp:DataGrid>
                                                                    </div>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="4" align="center">
                                                                    <asp:LinkButton runat="server" ID="btnCancel" CssClass="iconExit" CausesValidation="false"
                                                                        Text="Cancel" OnClick="btnCancel_Click">
                                        
                                                                    </asp:LinkButton>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>
                                        </div>
                                    </div>
                                </div>
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
