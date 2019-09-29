<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/ITC.Master" AutoEventWireup="true"
    CodeBehind="Edit_TPQC.aspx.cs" Inherits="ToasoanTTXVN.Quangcao.Edit_TPQC" %>

<%@ Import Namespace="HPCComponents" %>
<%@ Register TagPrefix="nbc" Namespace="HPCComponents.UI" Assembly="HPCComponents" %>
<%@ Register TagPrefix="CKeditor" Namespace="CKEditor.NET" Assembly="CKEditor.NET" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <script type="text/javascript">
        $(document).ready(function() {
            $("#<%=txtNgaybatdau.ClientID%>").datepicker({
                showOn: 'button',
                buttonText: 'Show Date',
                buttonImageOnly: true,
                buttonImage: '../Dungchung/Images/DatePicker/calendar.png'
            });

        });
 
    </script>

    <table cellspacing="0" cellpadding="0" width="100%" border="0">
        <tr>
            <td width="100%" align="center" colspan="2">
                <table cellspacing="4" cellpadding="4" width="100%" align="left" border="0">
                    <tr>
                        <td style="text-align: right; width: 10%" class="Titlelbl">
                            <span>
                                <%=CommonLib.ReadXML("lblTieude")%></span>(<span style="color: Red">*</span>):
                        </td>
                        <td align="left" width="90%" colspan="3">
                            <asp:TextBox ID="txt_tenquangcao" runat="server" Width="80%" CssClass="inputtext">
                            </asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" style="text-align: center">
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txt_tenquangcao"
                                Display="Dynamic" ErrorMessage="<%$Resources:cms.language, lblNhaptieude%>" Font-Size="Small"
                                SetFocusOnError="True"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td valign="top" style="text-align: right; width: 10%" class="Titlelbl">
                            <span>
                                <%=CommonLib.ReadXML("lblNoidung")%></span>(<span style="color: Red">*</span>):
                        </td>
                        <td align="left" width="90%" colspan="3">
                            <CKeditor:CKEditorControl ID="ckeNoidung" Toolbar="Noidung" runat="server" BasePath="~/ckeditor"
                                ContentsCss="~/ckeditor/contents.css" Height="200px" Width="90%">
                            </CKeditor:CKEditorControl>
                            <br />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="ckeNoidung"
                                Display="Dynamic" ErrorMessage="<%$Resources:cms.language, lblNhapnoidung%>"
                                Font-Size="Small" SetFocusOnError="True"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right; width: 10%" class="Titlelbl">
                            <span>
                                <%=CommonLib.ReadXML("lblKhachhang")%></span>(<span style="color: Red">*</span>):
                        </td>
                        <td align="left" width="20%">
                            <asp:DropDownList ID="cboKhachhang" runat="server" Width="80%" CssClass="inputtext">
                            </asp:DropDownList>
                        </td>
                        <td style="text-align: right; width: 10%" class="Titlelbl">
                            <asp:Label ID="Label1" runat="server"><%=CommonLib.ReadXML("lblAnpham")%></asp:Label>(<span
                                style="color: Red">*</span>):
                        </td>
                        <td align="left" width="20%">
                            <asp:UpdatePanel ID="UpdatePanellb" runat="server">
                                <ContentTemplate>
                                    <asp:DropDownList AutoPostBack="true" ID="cboAnPham" runat="server" Width="80%" CssClass="inputtext"
                                        DataTextField="Ten_Anpham" DataValueField="Ma_Anpham" OnSelectedIndexChanged="cboAnPham_SelectedIndexChanged"
                                        TabIndex="1">
                                    </asp:DropDownList>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" style="text-align: center">
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="cboKhachhang"
                                Display="Dynamic" ErrorMessage="<%$Resources:cms.language, lblChonkhachhangQC%>"
                                InitialValue="0"></asp:RequiredFieldValidator>
                        </td>
                        <td colspan="2" style="text-align: center">
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="cboAnPham"
                                Display="Dynamic" ErrorMessage="<%$Resources:cms.language, lblChonanpham%>" InitialValue="0"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right; width: 10%" class="Titlelbl">
                            <span>
                                <%=CommonLib.ReadXML("lblKichthuocquangcao")%></span>(<span style="color: Red">*</span>):
                        </td>
                        <td align="left" width="20%">
                            <asp:DropDownList ID="cbokichthuoc" runat="server" Width="80%" CssClass="inputtext">
                            </asp:DropDownList>
                        </td>
                        <td style="text-align: right; width: 10%" class="Titlelbl">
                            <span>
                                <%=CommonLib.ReadXML("lblTrang")%></span>(<span style="color: Red">*</span>):
                        </td>
                        <td style="text-align: left; width: 20%">
                            <asp:UpdatePanel ID="UpdatePanelTrang" runat="server">
                                <ContentTemplate>
                                    <asp:DropDownList ID="cboTrang" runat="server" Width="80%" CssClass="inputtext" TabIndex="1">
                                    </asp:DropDownList>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" style="text-align: center">
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="cbokichthuoc"
                                Display="Dynamic" ErrorMessage="<%$Resources:cms.language, lblChonkichthuocQC%>"
                                InitialValue="0"></asp:RequiredFieldValidator>
                        </td>
                        <td colspan="2" style="text-align: center">
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="cboTrang"
                                Display="Dynamic" ErrorMessage="<%$Resources:cms.language, lblChontrang%>" InitialValue="0">
                            </asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right; width: 10%;" class="Titlelbl">
                            <span>
                                <%=CommonLib.ReadXML("lblNgaydangquangcao")%></span>(<span style="color: Red">*</span>):
                        </td>
                        <td style="width: 90%; text-align: right;" colspan="3">
                            <div style="width: 50%; float: left; text-align: left;">
                                <div style="width: 40%; float: left; text-align: left">
                                    <asp:TextBox ID="txtNgaybatdau" runat="server" Width="120px" CssClass="inputtext"
                                        MaxLength="10" ToolTip="Từ ngày" onkeypress="AscciiDisable()" onfocus="javascript:vDateType='3'"
                                        onkeyup="DateFormat(this,this.value,event,false,'3')" onblur="DateFormat(this,this.value,event,true,'3')"></asp:TextBox>
                                    <asp:RegularExpressionValidator ID="Regularexpressionvalidator3" runat="server" ControlToValidate="txtNgaybatdau"
                                        ValidationExpression="^\d{1,2}/\d{1,2}/\d{4}$" Display="Dynamic">Kiểu ngày:dd/MM/yyyy</asp:RegularExpressionValidator>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtNgaybatdau"
                                        Display="Dynamic" ErrorMessage="<%$Resources:cms.language, lblNhapngaydangQC%>"
                                        Font-Size="Small" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                </div>
                                <div style="width: 40%; float: left; text-align: left; padding-left: 5px">
                                    <asp:LinkButton runat="server" ID="LinkAddNgayDang" CssClass="iconAdd" CausesValidation="false"
                                        OnClick="LinkAddNgayDang_Click" Text="<%$Resources:cms.language, lblThemngaydangquangcao%>">
                                    </asp:LinkButton>
                                </div>
                            </div>
                            <div style="width: 50%; float: left; text-align: left">
                                <div style="width: 20%; float: left; text-align: left">
                                    <div id="FileUploadImageAttach">
                                    </div>
                                </div>
                                <div style="width: 80%; float: left; text-align: left">
                                    <asp:UpdatePanel ID="Upnl_DeleteIMG" runat="server">
                                        <ContentTemplate>
                                            <asp:ImageButton runat="server" ID="btnXoaAnh" TabIndex="21" ImageUrl="../Dungchung/Images/Icons/mail_delete.png"
                                                OnClientClick=" return confirm('Do you want to delete?');" Width="30px" CausesValidation="false"
                                                OnClick="btnXoaAnh_Click"></asp:ImageButton>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td style="width: 90%; text-align: right;" colspan="3">
                            <div style="width: 50%; float: left; text-align: left;">
                                <asp:DataGrid ID="DataGridDatePublish" runat="server" Width="80%" BorderStyle="None"
                                    AutoGenerateColumns="False" CssClass="Grid" BorderColor="#d4d4d4" CellPadding="0"
                                    DataKeyField="ID" BackColor="White" BorderWidth="1px" AlternatingItemStyle-BackColor="#F1F1F2"
                                    OnEditCommand="DataGridDatePublish_EditCommand" OnItemDataBound="DataGridDatePublish_ItemDataBound">
                                    <ItemStyle CssClass="GridItem"></ItemStyle>
                                    <AlternatingItemStyle CssClass="GridAltItem" />
                                    <HeaderStyle CssClass="GridHeader"></HeaderStyle>
                                    <Columns>
                                        <asp:TemplateColumn HeaderText="<%$Resources:cms.language, lblAnpham%>">
                                            <HeaderStyle HorizontalAlign="Center" Width="8%"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Center" Width="8%" CssClass="GridBorderVerSolid"></ItemStyle>
                                            <ItemTemplate>
                                                <span class="linkGridForm">
                                                    <%#HPCBusinessLogic.UltilFunc.GetTenAnpham(Eval("Ma_Loaibao"))%></span>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="<%$Resources:cms.language, lblSobao%>">
                                            <HeaderStyle HorizontalAlign="Center" Width="5%"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Center" Width="5%" CssClass="GridBorderVerSolid"></ItemStyle>
                                            <ItemTemplate>
                                                <span class="linkGridForm">
                                                    <%#HPCBusinessLogic.UltilFunc.GetSobao(Eval("Ma_Sobao"))%></span>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="<%$Resources:cms.language, lblNgaydangquangcao%>">
                                            <HeaderStyle HorizontalAlign="Center" Width="8%"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Center" CssClass="GridBorderVerSolid"></ItemStyle>
                                            <ItemTemplate>
                                                <asp:Label ID="ngaydang" CssClass="linkGridForm" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Ngaydang")!=System.DBNull.Value?Convert.ToDateTime(DataBinder.Eval(Container, "DataItem.Ngaydang")).ToString("dd/MM/yyyy HH:mm"):"" %>'>
                                                </asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="<%$Resources:cms.language, lblXoa%>">
                                            <HeaderStyle HorizontalAlign="Center" Width="3%"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Center" CssClass="GridBorderVerSolid"></ItemStyle>
                                            <ItemTemplate>
                                                <asp:ImageButton ID="Imagebuttondelete" CausesValidation="false" runat="server" ImageUrl="../Dungchung/Images/icon-delete.gif"
                                                    ImageAlign="AbsMiddle" ToolTip="Xóa thông tin" CommandName="Edit" CommandArgument="Delete"
                                                    BorderStyle="None" OnClientClick=" return confirm('Do you want to delete?');">
                                                </asp:ImageButton>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                    </Columns>
                                </asp:DataGrid>
                            </div>
                            <div style="width: 50%; float: left; text-align: left">
                                <asp:UpdatePanel ID="UpdatePanel_images" runat="server">
                                    <ContentTemplate>
                                        <asp:DataList ID="dgrListImages" runat="server" RepeatColumns="4" RepeatDirection="Horizontal"
                                            DataKeyField="ID" Width="100%" CellPadding="4" OnEditCommand="dgrListImages_EditCommand">
                                            <ItemStyle Width="20%" BorderWidth="0" VerticalAlign="top" HorizontalAlign="Left">
                                            </ItemStyle>
                                            <ItemTemplate>
                                                <div style="width: 90%; height: auto; float: left; background-color: #DCDCDC; position: relative;">
                                                    <asp:CheckBox runat="server" Text='' ID="optSelect" AutoPostBack="False"></asp:CheckBox>
                                                    <asp:ImageButton ID="Imagebuttondelete" Style="width: 20px; height: 20px; position: absolute;
                                                        right: 5px; top: 0px; cursor: poiter" CausesValidation="false" runat="server"
                                                        ImageUrl="../Dungchung/Images/icon-delete.gif" ImageAlign="AbsMiddle" ToolTip="Xóa thông tin"
                                                        CommandName="Edit" CommandArgument="Delete" BorderStyle="None" OnClientClick=" return confirm('Bạn chắc chắn xóa file?');">
                                                    </asp:ImageButton>
                                                </div>
                                                <div style="width: 90%; float: left;">
                                                    <ul class="hoverbox">
                                                        <li><a href="<%=Global.PathImageFTP%><%#DataBinder.Eval(Container.DataItem, "PathFile")%>"
                                                            target='_blank'>
                                                            <img src="<%#HPCComponents.CommonLib.GetPathImgWordPDF(Eval("PathFile"))%>" alt='' />
                                                        </a></li>
                                                    </ul>
                                                </div>
                                                <div style="width: 90%; float: left; padding-top: 4px;">
                                                    <asp:Label ID="lbFileAttach" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "PathFile") %>'
                                                        Visible="false">
                                                    </asp:Label>
                                                </div>
                                            </ItemTemplate>
                                        </asp:DataList>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" style="width: 100%; text-align: center">
                            <asp:LinkButton runat="server" ID="linkSave" CssClass="iconSave" CausesValidation="true"
                                OnClick="linkSave_Click" Text="<%$ Resources:cms.language, lblLuu %>">
                            </asp:LinkButton>
                            <asp:LinkButton runat="server" ID="LinkButtonBack" CausesValidation="true" CssClass="iconSend"
                                OnClick="LinkButtonBack_Click" Text="<%$ Resources:cms.language, lblTralai %>">
                            </asp:LinkButton>
                            <asp:LinkButton runat="server" ID="LinkGuiDuyet" CausesValidation="true" CssClass="iconSend"
                                OnClick="linkSend_Click" Text="<%$ Resources:cms.language, lblGui %>">
                            </asp:LinkButton>
                            <asp:LinkButton runat="server" ID="LinkGuiDT" CausesValidation="true" CssClass="iconSend"
                                OnClick="LinkGuiDT_Click" Text="<%$ Resources:cms.language, lblGuihoasy %>">
                            </asp:LinkButton>
                            <asp:LinkButton runat="server" ID="LinkCancel" CausesValidation="false" CssClass="iconExit"
                                OnClick="LinkCancel_Click" Text="<%$ Resources:cms.language, lblThoat %>">
                            </asp:LinkButton>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" style="width: 100%; height: 10px; text-align: center">
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>

    <script type="text/javascript">
            
        $(window).load(
    function() {

         $("#FileUploadImageAttach").uploadify({
            'swf': '../Dungchung/Scripts/UploadMulti/uploadify.swf',
            'uploader': '../UploadFileMulti/UploadFileQuangcao.ashx?userid=<%=GetUserName()+','+Request["ID"]%>',
            'auto': true,
            'multi': true,
            'folder': 'Upload',
            'fileDesc': 'Image Files',
            'fileExt': '*.jpg;*.jpeg;*.gif;*.png;*.doc;*.docx;*.pdf',
            'buttonText': '<%=(string)HttpContext.GetGlobalResourceObject("cms.language", "lblDinhkemfile")%>',
            'onUploadSuccess': function() {
                doPostBackAsync('UploadImageSuccess', ''); 
            }
        });
    }
);
        
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
