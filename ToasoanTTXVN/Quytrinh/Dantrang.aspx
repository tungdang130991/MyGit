<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/ITC.Master" AutoEventWireup="True"
    CodeBehind="Dantrang.aspx.cs" Inherits="ToasoanTTXVN.Quytrinh.Dantrang" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<%@ Register TagPrefix="cc1" Namespace="HPCComponents.UI.WebControls" Assembly="HPCComponents" %>
<%@ Import Namespace="HPCComponents" %>
<asp:Content ID="ContentDT" ContentPlaceHolderID="MainContent" runat="server">

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

        function showuploadpdf() {
            var divuploadpdf = $('#divuploadpdf')
            var trang = $('#<%= cboPage.ClientID%>').val();
            if (divuploadpdf != null && trang != null) {

                if (trang != null && trang > 0) {
                    divuploadpdf.css("display", "");
                }
                else
                    divuploadpdf.css("display", "none");
                LoadFileUpload();
            }
        }
    </script>

    <table border="0" cellpadding="0" width="100%" cellspacing="0">
        <tr>
            <td style="text-align: center;">
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
                                    <%=CommonLib.ReadXML("lblChuyenmuc") %>
                                </td>
                                <td class="Titlelbl" style="text-align: left">
                                    <%=CommonLib.ReadXML("lblTenbaiviet")%>
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
                                    <asp:UpdatePanel ID="UpdatePanellang" runat="server">
                                        <ContentTemplate>
                                            <asp:DropDownList ID="cbo_Anpham" runat="server" Width="100%" CssClass="inputtext"
                                                AutoPostBack="true" OnSelectedIndexChanged="cbo_Anpham_SelectedIndexChanged"
                                                TabIndex="1">
                                            </asp:DropDownList>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td style="width: 10%; text-align: left">
                                    <asp:UpdatePanel ID="UpdatePanelsb" runat="server">
                                        <ContentTemplate>
                                            <asp:DropDownList ID="cboSoBao" runat="server" Width="100%" AutoPostBack="true" CssClass="inputtext"
                                                OnSelectedIndexChanged="cboSoBao_OnSelectedIndexChanged" onclick="showuploadpdf();"
                                                TabIndex="5">
                                            </asp:DropDownList>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td style="width: 8%; text-align: left">
                                    <asp:UpdatePanel ID="pnlTrang" runat="server">
                                        <ContentTemplate>
                                            <asp:DropDownList ID="cboPage" runat="server" AutoPostBack="true" Width="100%" OnSelectedIndexChanged="cboPage_OnSelectedIndexChanged"
                                                onclick="showuploadpdf();" CssClass="inputtext">
                                            </asp:DropDownList>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td style="width: 10%; text-align: left">
                                    <asp:UpdatePanel ID="UpdatePanelcm" runat="server">
                                        <ContentTemplate>
                                            <asp:DropDownList ID="cbo_chuyenmuc" runat="server" Width="98%" CssClass="inputtext"
                                                TabIndex="5">
                                            </asp:DropDownList>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td style="text-align: left; width: 10%">
                                    <asp:TextBox ID="txt_tieude" TabIndex="1" Width="95%" runat="server" CssClass="inputtext"
                                        placeholder="<%$Resources:cms.language, lblNhaptieudecantim%>" onkeypress="return clickButton(event,'ctl00_MainContent_cmdSeek');"></asp:TextBox>
                                </td>
                                <td style="width: 10%; text-align: left">
                                    <asp:TextBox ID="txt_tungay" runat="server" Width="120px" CssClass="inputtext" MaxLength="10"
                                        ToolTip="Từ ngày" onkeypress="AscciiDisable()" onfocus="javascript:vDateType='3'"
                                        onkeyup="DateFormat(this,this.value,event,false,'3')" onblur="DateFormat(this,this.value,event,true,'3')"></asp:TextBox>
                                    <asp:RegularExpressionValidator ID="Regularexpressionvalidator3" runat="server" ControlToValidate="txt_tungay"
                                        ValidationExpression="^\d{1,2}/\d{1,2}/\d{4}$" Display="Dynamic">Kiểu ngày:dd/MM/yyyy</asp:RegularExpressionValidator>&nbsp;&nbsp;
                                </td>
                                <td style="width: 10%; text-align: left">
                                    <asp:TextBox ID="txt_denngay" runat="server" Width="120px" CssClass="inputtext" MaxLength="10"
                                        ToolTip="Đến ngày" onkeypress="AscciiDisable()" onfocus="javascript:vDateType='3'"
                                        onkeyup="DateFormat(this,this.value,event,false,'3')" onblur="DateFormat(this,this.value,event,true,'3')"></asp:TextBox>
                                    <asp:RegularExpressionValidator ID="Regularexpressionvalidator1" runat="server" ControlToValidate="txt_denngay"
                                        ValidationExpression="^\d{1,2}/\d{1,2}/\d{4}$" Display="Dynamic">Kiểu ngày:dd/MM/yyyy</asp:RegularExpressionValidator>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: center; width: 100%" colspan="7">
                                    <asp:UpdatePanel ID="UpdatePanelSearch" runat="server">
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
                    <div id="divuploadpdf" style="width: 99%; text-align: left; padding: 5px 0.5%; display: none">
                        <div style="text-align: left; width: auto; float: left">
                            <div style="width: auto; float: left; text-align: left; padding-right: 10px">
                                <asp:FileUpload ID="FileUploadPDF" runat="server" />
                            </div>
                            <div style="width: auto; float: left; text-align: left; padding-right: 10px">
                                <asp:UpdatePanel ID="UpdatePaneldeletePDF" runat="server">
                                    <ContentTemplate>
                                        <asp:Button runat="server" ID="btnXoaFile" CssClass="iconDel" Text="<%$Resources:cms.language, lblXoafilepdf%>"
                                            CausesValidation="false" OnClientClick=" return confirm('Bạn chắc chắn xóa file?');"
                                            OnClick="btnXoaFile_Click" Visible="False"></asp:Button>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                            <div style="width: auto; float: left; text-align: left; padding-right: 10px">
                                <asp:UpdatePanel ID="UpdatePanelSendPdf" runat="server">
                                    <ContentTemplate>
                                        <asp:Button runat="server" ID="btnSendPublish" CssClass="iconSend" Text="<%$Resources:cms.language, lblGuifilepdf%>"
                                            OnClientClick=" return confirm('Bạn chắc chắn gửi?');" CausesValidation="false"
                                            Visible="False" OnClick="btnSendPublish_Click"></asp:Button>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                            <div style="width: auto; float: left; text-align: left; padding-right: 10px; display: none">
                                <asp:UpdatePanel ID="upnlblMessageMaket" runat="server">
                                    <ContentTemplate>
                                        <asp:Label ID="lblMessage" runat="server" CssClass="TitleHeader" ForeColor="Red"
                                            Text="<%$Resources:cms.language, lblDaguifilepdf%>" Visible="false"></asp:Label>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                    </div>
                    <div id="divlistlayoutPDF" style="text-align: left; width: 100%; float: left">
                        <asp:UpdatePanel ID="UpnlListPDF" runat="server">
                            <ContentTemplate>
                                <asp:DataList ID="DataGrid_FilePDF" runat="server" RepeatColumns="10" RepeatDirection="Horizontal"
                                    DataKeyField="ID" Width="100%" CellPadding="4" OnEditCommand="DataGrid_FilePDF_EditCommand">
                                    <ItemStyle Width="10%" BorderWidth="0" VerticalAlign="top" HorizontalAlign="Left">
                                    </ItemStyle>
                                    <ItemTemplate>
                                        <div style="width: 90%; float: left; text-align: center">
                                            <%#Get_Status(Eval("Status"))%>
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
                                        <div style="width: 90%; float: left; text-align: center">
                                            <asp:ImageButton ID="Imagebuttondelete" OnClientClick="confirm('Bạn chắc chắn xóa file?');"
                                                Style="width: 20px; height: 20px; cursor: poiter" CausesValidation="false" runat="server"
                                                ImageUrl="../Dungchung/Images/icon-delete.gif" ImageAlign="AbsMiddle" ToolTip="Hủy file pdf"
                                                CommandName="Edit" CommandArgument="Delete" BorderStyle="None"></asp:ImageButton>&nbsp;&nbsp;
                                            <asp:ImageButton ID="ImagebuttonReply" Style="width: 20px; height: 20px; cursor: poiter"
                                                CausesValidation="false" runat="server" ImageUrl="../Dungchung/Images/reply-icon.png"
                                                ImageAlign="AbsMiddle" ToolTip="Phản hồi" CommandName="Edit" CommandArgument="Reply"
                                                BorderStyle="None"></asp:ImageButton>
                                        </div>
                                    </ItemTemplate>
                                </asp:DataList>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                    <div style="text-align: left; width: 100%; float: left; display: none" id="divbutton">
                        <asp:LinkButton runat="server" ID="Linkdownloadfile" CausesValidation="false" OnClick="Linkdownloadfile_Click"
                            Text="Download files" CssClass="iconBack"> </asp:LinkButton>
                        <asp:LinkButton runat="server" ID="LinkBackFromDT" CausesValidation="false" OnClick="SendBack_Click"
                            OnClientClick=" return confirm('Bạn chắc chắn trả lại?');" Text="<%$Resources:cms.language, lblTralai%>"
                            CssClass="iconBack" Visible="false"> </asp:LinkButton>
                    </div>
                    <div style="text-align: left; float: left; width: 99%; padding: 5px 0.5%">
                        <asp:UpdatePanel ID="UpdatePanelTabContainer" runat="server">
                            <ContentTemplate>
                                <div style="text-align: left; width: 100%; vertical-align: top">
                                    <cc2:TabContainer ID="TabContainer1" runat="server" CssClass="ajax__tab_yuitabview-theme"
                                        AutoPostBack="true" ActiveTabIndex="0" OnActiveTabChanged="TabContainer1_ActiveTabChanged">
                                        <cc2:TabPanel HeaderText="<%$Resources:cms.language, lblTinmoi%>" ID="tabpnltinChoXuly"
                                            runat="server">
                                            <ContentTemplate>
                                                <table style="width: 100%; border: 0">
                                                    <tr>
                                                        <td>
                                                            <asp:DataGrid ID="DataGrid_DanTrang" runat="server" Width="100%" BorderStyle="None"
                                                                AutoGenerateColumns="False" CssClass="Grid" BorderColor="#d4d4d4" CellPadding="0"
                                                                DataKeyField="Ma_Tinbai" BackColor="White" BorderWidth="1px" AlternatingItemStyle-BackColor="#F1F1F2"
                                                                OnEditCommand="DataGrid_EditCommand" OnItemDataBound="DataGrid_ItemDataBound">
                                                                <ItemStyle CssClass="GridItem"></ItemStyle>
                                                                <AlternatingItemStyle CssClass="GridAltItem" />
                                                                <HeaderStyle CssClass="GridHeader"></HeaderStyle>
                                                                <Columns>
                                                                    <asp:TemplateColumn>
                                                                        <HeaderStyle HorizontalAlign="Center" Width="4%"></HeaderStyle>
                                                                        <ItemStyle HorizontalAlign="Center" CssClass="GridBorderVerSolid"></ItemStyle>
                                                                        <HeaderTemplate>
                                                                            <asp:CheckBox ID="chkAll" onclick="checkAll_Current(this,'ctl00_MainContent_TabContainer1_tabpnltinChoXuly_DataGrid_DanTrang');CheckItem();"
                                                                                runat="server" ToolTip="Chọn tất cả"></asp:CheckBox>
                                                                        </HeaderTemplate>
                                                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                                        <ItemTemplate>
                                                                            <asp:CheckBox runat="server" ID='optSelect' Enabled='<%# HPCBusinessLogic.Utils.IsEnable(_Role.R_Write,DataBinder.Eval(Container.DataItem, "Nguoi_Khoa").ToString(),_user.UserID)%>'
                                                                                CssClass="checkitem" onclick="javascript:CheckItem();" AutoPostBack="False">
                                                                            </asp:CheckBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateColumn>
                                                                    <asp:BoundColumn DataField="Ma_Tinbai" HeaderText="Mã tin bài" Visible="False"></asp:BoundColumn>
                                                                    <asp:TemplateColumn HeaderText="Image">
                                                                        <HeaderStyle HorizontalAlign="Center" Width="5%"></HeaderStyle>
                                                                        <ItemStyle HorizontalAlign="Center" CssClass="GridBorderVerSolid"></ItemStyle>
                                                                        <ItemTemplate>
                                                                            <div style="float: left; width: 100%; text-align: center">
                                                                                <%#HPCBusinessLogic.UltilFunc.GetImageAttach(Eval("Ma_Tinbai"))%>
                                                                            </div>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateColumn>
                                                                    <asp:TemplateColumn HeaderText="<%$Resources:cms.language, lblTralai%>" Visible="false">
                                                                        <HeaderStyle HorizontalAlign="Center" Width="5%"></HeaderStyle>
                                                                        <ItemStyle HorizontalAlign="Center" Width="5%" CssClass="GridBorderVerSolid"></ItemStyle>
                                                                        <ItemTemplate>
                                                                            <asp:ImageButton ID="btnBack" Width="15px" runat="server" ImageUrl="~/Dungchung/images/prev.gif"
                                                                                ImageAlign="AbsMiddle" ToolTip="Trả lại" CommandName="Edit" CommandArgument="Back"
                                                                                CausesValidation="false" BorderStyle="None"></asp:ImageButton>
                                                                            <asp:Label ID="lblnoidung" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Noidung") %>'
                                                                                Visible="false"></asp:Label>
                                                                            <asp:Label ID="lbltieude" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Tieude") %>'
                                                                                Visible="false">
                                                                            </asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateColumn>
                                                                    <asp:TemplateColumn HeaderText="<%$Resources:cms.language, lblTieudetin%>">
                                                                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                                        <ItemStyle HorizontalAlign="Left" CssClass="GridBorderVerSolid"></ItemStyle>
                                                                        <ItemTemplate>
                                                                            <a class="linkGridForm" href="Javascript:open_window_Scroll('<%=Global.ApplicationPath%>/Quytrinh/ViewTinbaiDantrang.aspx?Menu_ID=<%=Page.Request.QueryString["Menu_ID"]%>&ID=<%# DataBinder.Eval(Container.DataItem, "Ma_Tinbai") %>',50,500,100,800);">
                                                                                <%#HPCComponents.CommonLib.GetStatusUpdateContents(Eval("Ma_Tinbai"))%></a>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateColumn>
                                                                    <asp:TemplateColumn HeaderText="<%$Resources:cms.language, lblTrang%>">
                                                                        <HeaderStyle HorizontalAlign="Center" Width="5%"></HeaderStyle>
                                                                        <ItemStyle HorizontalAlign="Center" Width="5%" CssClass="GridBorderVerSolid"></ItemStyle>
                                                                        <ItemTemplate>
                                                                            <span class="linkGridForm">
                                                                                <%#HPCBusinessLogic.UltilFunc.GetTenSoBaoFromT_Vitri_Tinbai(Eval("Ma_Tinbai"),1)%></span>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateColumn>
                                                                    <asp:TemplateColumn HeaderText="<%$Resources:cms.language, lblAnpham%>">
                                                                        <HeaderStyle HorizontalAlign="Center" Width="8%"></HeaderStyle>
                                                                        <ItemStyle HorizontalAlign="Center" Width="8%" CssClass="GridBorderVerSolid"></ItemStyle>
                                                                        <ItemTemplate>
                                                                            <span class="linkGridForm">
                                                                                <%#HPCBusinessLogic.UltilFunc.GetTenAnpham(Eval("Ma_Anpham"))%></span>
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
                                                                    <asp:TemplateColumn HeaderText="<%$Resources:cms.language, lblChuyenmuc%>">
                                                                        <HeaderStyle Width="12%" HorizontalAlign="Center"></HeaderStyle>
                                                                        <ItemStyle Width="12%" HorizontalAlign="Left" CssClass="GridBorderVerSolid"></ItemStyle>
                                                                        <ItemTemplate>
                                                                            <span class="linkGridForm">
                                                                                <%#HPCBusinessLogic.UltilFunc.GetTenChuyenMuc(Eval("Ma_Chuyenmuc"))%></span>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateColumn>
                                                                    <asp:TemplateColumn HeaderText="<%$Resources:cms.language, lblNguoigui%>">
                                                                        <HeaderStyle HorizontalAlign="Center" Width="10%"></HeaderStyle>
                                                                        <ItemStyle HorizontalAlign="Left" Width="10%" CssClass="GridBorderVerSolid"></ItemStyle>
                                                                        <ItemTemplate>
                                                                            <span class="linkGridForm">
                                                                                <%#HPCBusinessLogic.UltilFunc.GetUserFullName(Eval("Ma_Nguoitao"))%></span>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateColumn>
                                                                    <asp:TemplateColumn HeaderText="<%$Resources:cms.language, lblNgaygui%>">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="ngaytao" CssClass="linkGridForm" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Ngaytao")!=System.DBNull.Value?Convert.ToDateTime(DataBinder.Eval(Container, "DataItem.Ngaytao")).ToString("dd/MM/yyyy HH:mm"):"" %>'>
                                                                            </asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Center" Width="10%"></HeaderStyle>
                                                                        <ItemStyle HorizontalAlign="Center" CssClass="GridBorderVerSolid"></ItemStyle>
                                                                    </asp:TemplateColumn>
                                                                    <asp:TemplateColumn HeaderText="<%$Resources:cms.language, lblXem%>">
                                                                        <HeaderStyle HorizontalAlign="Center" Width="5%"></HeaderStyle>
                                                                        <ItemStyle HorizontalAlign="Center" Width="5%" CssClass="GridBorderVerSolid"></ItemStyle>
                                                                        <ItemTemplate>
                                                                            <a href="Javascript:open_window_Scroll('<%=Global.ApplicationPath%>/Quytrinh/ViewPhienBanTinBai.aspx?Menu_ID=<%=Page.Request.QueryString["Menu_ID"]%>&ID=<%# DataBinder.Eval(Container.DataItem, "Ma_Tinbai") %>',50,800,100,800);" />
                                                                            <img src='<%=Global.ApplicationPath%>/Dungchung/Images/view.gif' onmouseover="(window.status=''); return true"
                                                                                alt='' style="cursor: hand; border: 0" />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateColumn>
                                                                    <asp:TemplateColumn>
                                                                        <HeaderStyle HorizontalAlign="Center" Width="4%"></HeaderStyle>
                                                                        <ItemStyle HorizontalAlign="Center" Width="4%" CssClass="GridBorderVerSolid"></ItemStyle>
                                                                        <HeaderTemplate>
                                                                            Preview
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>
                                                                            <a href="Javascript:open_window_Scroll('<%=Global.ApplicationPath%>/Quytrinh/Preview.aspx?Menu_ID=<%=Page.Request.QueryString["Menu_ID"]%>&ID=<%# DataBinder.Eval(Container.DataItem, "Ma_Tinbai") %>',50,500,100,800);">
                                                                                <img src='<%=Global.ApplicationPath%>/Dungchung/Images/Icons/preview.png' border="0"
                                                                                    style="cursor: hand; border: 0" /></a>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateColumn>
                                                                </Columns>
                                                            </asp:DataGrid>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </ContentTemplate>
                                        </cc2:TabPanel>
                                        <cc2:TabPanel HeaderText="<%$Resources:cms.language, lblTindagui%>" ID="TabPanelTinDaXuLy"
                                            runat="server" Visible="false">
                                            <ContentTemplate>
                                                <table border="0" style="width: 100%">
                                                    <tr>
                                                        <td>
                                                            <asp:DataGrid ID="DataGrid_Daxuly" runat="server" Width="100%" AutoGenerateColumns="False"
                                                                CssClass="Grid" BorderColor="#7F8080" CellPadding="0" AlternatingItemStyle-BackColor="#F1F1F2"
                                                                OnItemDataBound="DataGrid_ItemDataBound" DataKeyField="Ma_Phienban" BackColor="White"
                                                                BorderStyle="None" BorderWidth="1px">
                                                                <ItemStyle CssClass="GridItem"></ItemStyle>
                                                                <AlternatingItemStyle CssClass="GridAltItem" />
                                                                <HeaderStyle CssClass="GridHeader"></HeaderStyle>
                                                                <Columns>
                                                                    <asp:BoundColumn DataField="Ma_Phienban" HeaderText="Ma_Phienban" Visible="False">
                                                                    </asp:BoundColumn>
                                                                    <asp:TemplateColumn HeaderText="<%$Resources:cms.language, lblTieudetin%>">
                                                                        <HeaderStyle Width="30%" HorizontalAlign="Center"></HeaderStyle>
                                                                        <ItemStyle Width="30%" HorizontalAlign="Left" CssClass="GridBorderVerSolid"></ItemStyle>
                                                                        <ItemTemplate>
                                                                            <a class="linkGridForm" href="Javascript:open_window_Scroll('<%=Global.ApplicationPath%>/Quytrinh/ViewPhienBanTinBai.aspx?Menu_ID=<%=Page.Request.QueryString["Menu_ID"]%>&ID=<%# DataBinder.Eval(Container.DataItem, "Ma_Tinbai") %>',50,500,100,800);">
                                                                                <%#Eval("Tieude")%></a>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateColumn>
                                                                    <asp:TemplateColumn HeaderText="<%$Resources:cms.language, lblChuyenmuc%>">
                                                                        <HeaderStyle Width="12%" HorizontalAlign="Center"></HeaderStyle>
                                                                        <ItemStyle Width="12%" HorizontalAlign="Left" CssClass="GridBorderVerSolid"></ItemStyle>
                                                                        <ItemTemplate>
                                                                            <span class="linkGridForm">
                                                                                <%#HPCBusinessLogic.UltilFunc.GetTenChuyenMuc(Eval("Ma_Chuyenmuc"))%></span>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateColumn>
                                                                    <asp:TemplateColumn HeaderText="<%$Resources:cms.language, lblAnpham%>">
                                                                        <HeaderStyle HorizontalAlign="Center" Width="8%"></HeaderStyle>
                                                                        <ItemStyle HorizontalAlign="Center" Width="8%" CssClass="GridBorderVerSolid"></ItemStyle>
                                                                        <ItemTemplate>
                                                                            <span class="linkGridForm">
                                                                                <%#HPCBusinessLogic.UltilFunc.GetTenAnpham(Eval("Ma_Anpham"))%></span>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateColumn>
                                                                    <asp:TemplateColumn HeaderText="<%$Resources:cms.language, lblNgaygui%>">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="ngaytao" CssClass="linkGridForm" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Ngaytao")!=System.DBNull.Value?Convert.ToDateTime(DataBinder.Eval(Container, "DataItem.Ngaytao")).ToString("dd/MM/yyyy HH:mm:ss"):"" %>'>
                                                                            </asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Center" Width="10%"></HeaderStyle>
                                                                        <ItemStyle HorizontalAlign="Center" CssClass="GridBorderVerSolid"></ItemStyle>
                                                                    </asp:TemplateColumn>
                                                                    <asp:TemplateColumn HeaderText="<%$Resources:cms.language, lblThaotac%>">
                                                                        <HeaderStyle Width="15%" HorizontalAlign="Center"></HeaderStyle>
                                                                        <ItemStyle Width="15%" HorizontalAlign="Left" CssClass="GridBorderVerSolid"></ItemStyle>
                                                                        <ItemTemplate>
                                                                            <span class="linkGridForm">
                                                                                <%#HPCComponents.Global.GetActionName(Eval("Ma_Phienban"))%></span>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateColumn>
                                                                    <asp:TemplateColumn HeaderText="<%$Resources:cms.language, lblTrangthai%>">
                                                                        <HeaderStyle Width="12%" HorizontalAlign="Center"></HeaderStyle>
                                                                        <ItemStyle Width="12%" HorizontalAlign="Left" CssClass="GridBorderVerSolid"></ItemStyle>
                                                                        <ItemTemplate>
                                                                            <span class="linkGridForm">
                                                                                <%#HPCComponents.Global.GetTrangThaiFrom_T_version(Eval("Ma_Tinbai"))%></span>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateColumn>
                                                                    <asp:TemplateColumn HeaderText="<%$Resources:cms.language, lblXem%>">
                                                                        <HeaderStyle HorizontalAlign="Center" Width="5%"></HeaderStyle>
                                                                        <ItemStyle HorizontalAlign="Center" Width="5%" CssClass="GridBorderVerSolid"></ItemStyle>
                                                                        <ItemTemplate>
                                                                            <a href="Javascript:open_window_Scroll('<%=Global.ApplicationPath%>/Quytrinh/ViewNews_Version.aspx?Menu_ID=<%=Page.Request.QueryString["Menu_ID"]%>&ID=<%# DataBinder.Eval(Container.DataItem, "Ma_Tinbai") %>',50,500,100,800);" />
                                                                            <img src='<%=Global.ApplicationPath%>/Dungchung/Images/view.gif' border="0" onmouseover="(window.status=''); return true"
                                                                                style="cursor: hand; border: 0" />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateColumn>
                                                                </Columns>
                                                            </asp:DataGrid>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </ContentTemplate>
                                        </cc2:TabPanel>
                                    </cc2:TabContainer>
                                </div>
                                <div style="float: right;" class="pageNav">
                                    <cc1:CurrentPage runat="server" ID="CurrentPage" CssClass="pageNavTotal">
                                    </cc1:CurrentPage>
                                    <cc1:Pager runat="server" ID="pages" OnIndexChanged="pages_IndexChanged_baichoxuly">
                                    </cc1:Pager>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </td>
        </tr>
    </table>

    <script type="text/javascript" language="javascript">
        $(window).load(function() {
            LoadFileUpload();
        }
);
        function LoadFileUpload() {
            $("#<%=FileUploadPDF.ClientID %>").uploadify({
                'swf': '../Dungchung/Scripts/UploadMulti/uploadify.swf',
                'uploader': '../UploadFileMulti/UploadFilePDF.ashx?user=<%=GetUserName()%> ' + ',' + $('#<%=cboPage.ClientID%>').val() + ',' + $('#<%=cboSoBao.ClientID%>').val() + ',' + 2,
                'auto': true,
                'multi': true,
                'folder': 'Upload',
                'fileDesc': 'Image Files',
                'fileExt': '*.pdf;*.jpeg;',
                'buttonText': '<%=(string)HttpContext.GetGlobalResourceObject("cms.language", "lblChonfilepdf")%>',
                'onUploadSuccess': function() {
                    doPostBackAsync('UploadImageSuccess', '');
                }
            });
        }

        function doPostBackAsync(eventTarget, eventArgs) {
            var pageReqMgr = Sys.WebForms.PageRequestManager.getInstance();
            if (!Array.contains(pageReqMgr._asyncPostBackControlIDs, eventTarget)) {
                pageReqMgr._asyncPostBackControlIDs.push(eventTarget);
            }
            if (!Array.contains(pageReqMgr._asyncPostBackControlClientIDs, eventTarget)) {
                pageReqMgr._asyncPostBackControlClientIDs.push(eventTarget);
            }
            __doPostBack(eventTarget, eventArgs);
        }
        
    </script>

</asp:Content>
