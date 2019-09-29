<%@ Page Language="C#" MasterPageFile="~/Masters/ITC.Master" AutoEventWireup="true"
    CodeBehind="Edit_PV.aspx.cs" Inherits="ToasoanTTXVN.Quytrinh.Edit_PV" Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxtoolkit" %>
<%@ Register Assembly="Aceoffix, Version=3.0.0.1, Culture=neutral, PublicKeyToken=e6a26169e940f541"
    Namespace="Aceoffix" TagPrefix="ace" %>
<%@ Import Namespace="HPCComponents" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <script type="text/javascript" language="javascript">
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);
        function EndRequestHandler(sender, args) {
            if (args.get_error() != undefined) {
                args.set_errorHandled(true);
            }
        }
    </script>

    <script language="javascript" type="text/javascript">
        function OnCustomMenuClick(iIndex, sCaption) {
            var AceoffixCtrl1 = document.getElementById("AceoffixCtrl_Noidung");
            if (iIndex == 0)
                alert("The caption of titlebar is: " + AceoffixCtrl1.Caption);
            if (iIndex == 2)
                SaveAsHTML();
        }
        function SaveDocument() {
            SaveAsHTML();
            document.getElementById("AceoffixCtrl_Noidung").SaveDocument();
        }
        function SaveAsHTML() {
            document.getElementById("AceoffixCtrl_Noidung").SaveDocumentAsHTML();

        }
        function ShowPageSetupDlg() {
            document.getElementById("AceoffixCtrl_Noidung").ShowDialog(5);
        }
        function ShowPrintDlg() {
            document.getElementById("AceoffixCtrl_Noidung").ShowDialog(4);
        }
        function SwitchFullScreen() {
            document.getElementById("AceoffixCtrl_Noidung").FullScreen = !document.getElementById("AceoffixCtrl_Noidung").FullScreen;
        }
        function open_window_reviewScroll() {
            var MaTinBai = '<%=Request["ID"]%>';
            if (MaTinBai == "") {
                MaTinBai = $("#<%=txtID.ClientID %>").val();
            }
            if (MaTinBai != "") {
                window.open('../Quytrinh/Preview.aspx?ID=' + MaTinBai, 'popup', 'height=1600,width=1800,location=no,directories=no,resizable=yes,status=yes,toolbar=no,menubar=no,scrollbars=yes,top=5,left=150');

            }
        }
    </script>

    <table border="0" cellpadding="0" width="100%" cellspacing="0">
        <tr>
            <td class="datagrid_content_left">
            </td>
            <td style="vertical-align: top; width: 100%">
                <div class="divPanelResult">
                    <div style="width: 99%; float: left; padding: 5px 0.5%">
                        <table cellspacing="2" cellpadding="2" width="100%" border="0">
                            <tr>
                                <td style="vertical-align: top; width: 70%;">
                                    <div id="div_content" style="text-align: left; float: left; height: 800px; width: 100%;">
                                        <ace:AceoffixCtrl ID="AceoffixCtrl_Noidung" runat="server" BorderStyle="BorderThin"
                                            Menubar="True" Theme="Office2007" OnLoad="AceoffixCtrl1_Load" TitlebarColor="ActiveCaption">
                                        </ace:AceoffixCtrl>
                                    </div>
                                </td>
                                <td style="vertical-align: top; width: 30%;">
                                    <table cellspacing="2" cellpadding="2" width="100%" border="0">
                                        <tr>
                                            <td style="text-align: left" class="Titlelbl" colspan="2">
                                                <span>
                                                    <%=CommonLib.ReadXML("lblTieude")%></span>(<span style="color: Red">*</span>)
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: left; width: 100%" colspan="2">
                                                <asp:TextBox ID="Txt_tieude" TabIndex="4" TextMode="MultiLine" Width="95%" runat="server"
                                                    placeholder="<%$Resources:cms.language, lblNhaptieude%>" CssClass="inputtext"></asp:TextBox>
                                                <asp:RequiredFieldValidator runat="server" ID="reqName" ControlToValidate="Txt_tieude"
                                                    InitialValue="" SetFocusOnError="True" Display="Dynamic" ErrorMessage="<%$Resources:cms.language, lblBanchuanhaptieude%>" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: left" class="Titlelbl" colspan="2">
                                                <span>
                                                    <%=CommonLib.ReadXML("lblGhichu")%></span>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: left; width: 100%" colspan="2">
                                                <asp:TextBox ID="Txt_Comments" TabIndex="16" Width="95%" runat="server" CssClass="inputtext"
                                                    TextMode="MultiLine" Rows="2"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: left" class="Titlelbl">
                                                <span>
                                                    <%=CommonLib.ReadXML("lblAnpham")%></span>(<span style="color: Red">*</span>)
                                            </td>
                                            <td style="text-align: left" class="Titlelbl">
                                                <span>
                                                    <%=CommonLib.ReadXML("lblChuyenmuc")%></span>(<span style="color: Red">*</span>)
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: left; width: 50%">
                                                <asp:UpdatePanel ID="UpdatePanelLB" runat="server">
                                                    <ContentTemplate>
                                                        <asp:DropDownList ID="cbo_AnPham" Width="95%" CssClass="inputtext" Enabled="false"
                                                            AutoPostBack="true" OnSelectedIndexChanged="cbo_AnPham_SelectedIndexChanged"
                                                            runat="server">
                                                        </asp:DropDownList>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                                <asp:RequiredFieldValidator ControlToValidate="cbo_AnPham" ID="RequiredFieldValidatorAP"
                                                    ErrorMessage="<%$Resources:cms.language, lblBanchuachonanpham%>" InitialValue="0"
                                                    runat="server" Display="Dynamic" SetFocusOnError="true">
                                                </asp:RequiredFieldValidator>
                                            </td>
                                            <td style="text-align: left; width: 50%">
                                                <asp:UpdatePanel ID="UpdatePanelCM" runat="server">
                                                    <ContentTemplate>
                                                        <asp:DropDownList ID="cbo_chuyenmuc" Width="95%" CssClass="inputtext" runat="server">
                                                        </asp:DropDownList>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                                <asp:RequiredFieldValidator ControlToValidate="cbo_chuyenmuc" ID="RequiredFieldValidatorCM"
                                                    ErrorMessage="<%$Resources:cms.language, lblBanchuachonchuyenmuc%>" InitialValue="0"
                                                    runat="server" Display="Dynamic" SetFocusOnError="true">
                                                </asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: left" class="Titlelbl" colspan="2">
                                                <span>
                                                    <%=CommonLib.ReadXML("lblTacgia")%></span>(<span style="color: Red">*</span>)
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: left; width: 100%" colspan="2">
                                                <asp:TextBox ID="txt_PVCTV" runat="server" Width="95%" CssClass="inputtext" placeholder="<%$Resources:cms.language, lblNhaptacgia%>"></asp:TextBox>
                                                <div style="display: none">
                                                    <asp:TextBox ID="HiddenFieldTacgiatin" runat="server" Text="0"></asp:TextBox></div>
                                                <ajaxtoolkit:AutoCompleteExtender runat="server" ID="autoCompleteTacgiaTin" TargetControlID="txt_PVCTV"
                                                    ServicePath="../UploadFileMulti/AutoComplete.asmx" ServiceMethod="GetCompletionList"
                                                    ContextKey="1" CompletionListCssClass="CompletionListCssClass" MinimumPrefixLength="1"
                                                    CompletionInterval="1000" EnableCaching="true" CompletionSetCount="20" OnClientItemSelected="ClientItemSelectedTacGiaTin">
                                                </ajaxtoolkit:AutoCompleteExtender>
                                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidatorAuthor" ControlToValidate="txt_PVCTV"
                                                    InitialValue="" Display="Dynamic" SetFocusOnError="True" ErrorMessage="<%$Resources:cms.language, lblBanchuanhaptacgia%>" />
                                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidatormatacgia" ControlToValidate="HiddenFieldTacgiatin"
                                                    InitialValue="0" SetFocusOnError="True" Display="Dynamic" ErrorMessage="<%$Resources:cms.language, lblBanchuachontacgiatronghethong%>"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: left; width: 100%" colspan="2">
                                                <div style="float: left; width: 50%; font-family: Arial; font-size: 14px; color: #880000">
                                                    <asp:CheckBox ID="checkbaoonline" runat="server" Text="VietnamNews online" /></div>
                                                <div style="float: left; width: 50%; font-family: Arial; font-size: 14px; color: #880000">
                                                    <asp:CheckBox ID="checkbizbub" runat="server" Text="Bizbub online" /></div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: left; width: 100%" colspan="2">
                                                <div style="width: 30%; float: left; text-align: left">
                                                    <asp:FileUpload ID="FileUploadImageAttach" runat="server" />
                                                </div>
                                                <div style="width: 70%; float: left; text-align: left">
                                                    <asp:UpdatePanel ID="Upnl_DeleteIMG" runat="server">
                                                        <ContentTemplate>
                                                            <asp:ImageButton runat="server" ID="btnXoaAnh" TabIndex="21" ImageUrl="../Dungchung/Images/Icons/mail_delete.png"
                                                                OnClientClick="confirm('Do you want to delete');" Width="30px" CausesValidation="false"
                                                                OnClick="btnXoaAnh_Click"></asp:ImageButton>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: left; width: 100%" colspan="2">
                                                <asp:UpdatePanel ID="UpdatePanel_images" runat="server">
                                                    <ContentTemplate>
                                                        <div style="text-align: left; width: 100%">
                                                            <asp:DataList ID="dgrListImages" runat="server" RepeatColumns="2" RepeatDirection="Horizontal"
                                                                DataKeyField="Ma_Anh" Width="100%" CellPadding="4" OnEditCommand="dgrListImages_EditCommand">
                                                                <ItemStyle Width="50%" BorderWidth="0" VerticalAlign="top" HorizontalAlign="Left">
                                                                </ItemStyle>
                                                                <ItemTemplate>
                                                                    <div style="width: 90%; height: auto; float: left; background-color: #DCDCDC; position: relative;">
                                                                        <asp:CheckBox runat="server" Text='' ID="optSelect" AutoPostBack="False"></asp:CheckBox>
                                                                        <asp:ImageButton ID="btnAdd" Style="width: 20px; height: 18px; position: absolute;
                                                                            right: 35px; top: 0px; cursor: poiter" CausesValidation="false" runat="server"
                                                                            ImageUrl="../Dungchung/Images/EditImg.png" ImageAlign="AbsMiddle" ToolTip="Sửa thông tin"
                                                                            CommandName="Edit" CommandArgument="EditInfo" BorderStyle="None"></asp:ImageButton>
                                                                        <asp:ImageButton ID="Imagebuttondelete" OnClientClick="confirm('Bạn chắc chắn hủy ảnh?');"
                                                                            Style="width: 20px; height: 20px; position: absolute; right: 5px; top: 0px; cursor: poiter"
                                                                            CausesValidation="false" runat="server" ImageUrl="../Dungchung/Images/icon-delete.gif"
                                                                            ImageAlign="AbsMiddle" ToolTip="Hủy ảnh" CommandName="Edit" CommandArgument="Delete"
                                                                            BorderStyle="None"></asp:ImageButton>
                                                                    </div>
                                                                    <div style="width: 90%; float: left;">
                                                                        <ul class="hoverbox">
                                                                            <li><a href="javascript:OpenImage('<%# DataBinder.Eval(Container.DataItem, "Duongdan_Anh")%>');">
                                                                                <img src="<%=Global.TinPath%><%#Eval("Duongdan_Anh")%>" alt="<%# DataBinder.Eval(Container.DataItem, "Chuthich")%>" />
                                                                            </a></li>
                                                                        </ul>
                                                                    </div>
                                                                    <div style="width: 90%; float: left; padding-top: 4px;">
                                                                        <asp:Label ID="lbFileAttach" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Duongdan_Anh") %>'
                                                                            Visible="false">
                                                                        </asp:Label>
                                                                        <asp:Label ID="lbdesc" CssClass="linkGridForm" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Chuthich")%>'>
                                                                        </asp:Label>
                                                                    </div>
                                                                    <div style="width: 90%; float: left; font-weight: bold; text-align: left; padding: 3px 0">
                                                                        <asp:Label ID="lbtacgia" CssClass="styleforimages" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "NguoiChup")%>'>
                                                                        </asp:Label>
                                                                    </div>
                                                                    <asp:TextBox ID="txtChuthich" TextMode="MultiLine" Rows="4" Width="90%" runat="server"
                                                                        placeholder="<%$Resources:cms.language, lblNhapchuthich%>" Visible="false" Text='<%# DataBinder.Eval(Container.DataItem, "Chuthich")%>'>
                                                                    </asp:TextBox>
                                                                    <%--<asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" ControlToValidate="txtChuthich"
                                                                        InitialValue="" Display="Dynamic" SetFocusOnError="True" ErrorMessage="<%$Resources:cms.language, lblBanchuanhapchuthich%>" />--%>
                                                                    <div style="width: 90%; float: left; text-align: right; margin-top: 4px;">
                                                                        <asp:TextBox ID="txtTacgia" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "NguoiChup")%>'
                                                                            placeholder="<%$Resources:cms.language, lblNhaptacgia%>" Visible="false" Width="100%"
                                                                            data-in='<%# DataBinder.Eval(Container.DataItem, "Ma_Anh")%>'></asp:TextBox>
                                                                        <div style="display: none">
                                                                            <asp:TextBox runat="server" ID="hdnValueTacGiaAnh" Width="100%" Text='<%# DataBinder.Eval(Container.DataItem, "Ma_Nguoichup")%>'
                                                                                data-in='<%# DataBinder.Eval(Container.DataItem, "Ma_Anh")%>'></asp:TextBox>
                                                                        </div>
                                                                        <ajaxtoolkit:AutoCompleteExtender runat="server" ID="autoCompleteTacgiaAnh" TargetControlID="txtTacgia"
                                                                            ServicePath="../UploadFileMulti/AutoComplete.asmx" ServiceMethod="GetCompletionList"
                                                                            ContextKey="2" CompletionListCssClass="CompletionListCssClass" MinimumPrefixLength="1"
                                                                            CompletionInterval="1000" EnableCaching="true" CompletionSetCount="20" OnClientItemSelected="ClientItemSelected">
                                                                        </ajaxtoolkit:AutoCompleteExtender>
                                                                    </div>
                                                                    <%--<div style="width: 90%; float: left; margin-top: 3px; text-align: left">
                                                                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidatorAuthor" ControlToValidate="txtTacgia"
                                                                            InitialValue="" Display="Dynamic" SetFocusOnError="True" ErrorMessage="<%$Resources:cms.language, lblBanchuanhaptacgia%>" /></div>--%>
                                                                    <div style="width: 90%; float: left; margin-top: 3px; text-align: center">
                                                                        <asp:ImageButton ID="btnUpdate" Width="20px" CausesValidation="true" runat="server"
                                                                            Visible="false" ImageUrl="../Dungchung/Images/save.gif" ImageAlign="AbsMiddle"
                                                                            ToolTip="Cập nhật" CommandName="Edit" CommandArgument="Update" BorderStyle="None">
                                                                        </asp:ImageButton>
                                                                        <asp:ImageButton ID="btnCancel" Width="20px" runat="server" CausesValidation="false"
                                                                            ImageUrl="../Dungchung/Images/undo.gif" ImageAlign="AbsMiddle" ToolTip="Hủy bỏ"
                                                                            CommandName="Edit" CommandArgument="Cancel" Visible="false" BorderStyle="None">
                                                                        </asp:ImageButton>
                                                                    </div>
                                                                </ItemTemplate>
                                                            </asp:DataList>
                                                        </div>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2" align="right">
                                                <asp:Label ID="lblMsg" runat="server" ForeColor="Red"></asp:Label>
                                                <asp:TextBox ID="txtID" Text="0" runat="server" Style="display: none"></asp:TextBox>
                                                <asp:TextBox ID="txt_autosaveid" Text="0" runat="server" Style="display: none"></asp:TextBox>
                                                <asp:HiddenField ID="word_count" runat="server" Value="0" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: center; width: 100%;" colspan="2">
                                                <span id="ErrorMesages" style="font-family: Arial; font-size: medium; color: Red"
                                                    runat="server"></span>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: center; width: 100%;" colspan="2">
                                                <asp:LinkButton runat="server" ID="linkSave" CssClass="iconSave" TabIndex="17" OnClick="linkSave_Click"
                                                    CausesValidation="true" OnClientClick="validateText();" Text="<%$ Resources:cms.language, lblLuu %>"></asp:LinkButton>
                                                <asp:DataList ID="DataListDoiTuong" runat="server" OnItemCommand="DataListDoiTuong_ItemCommand"
                                                    RepeatDirection="horizontal" RepeatColumns="4" RepeatLayout="Flow">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkdoituong" runat="server" CssClass="iconSend" CommandName="cmd"
                                                            CausesValidation="true" OnClientClick="validateText();" CommandArgument='<%# DataBinder.Eval(Container.DataItem,"Ma_DoiTuong")%>'
                                                            Text='<%# Bind("ThaoTac")%>'>
                                                        </asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:DataList>
                                                <asp:LinkButton runat="server" ID="ButtonExit" TabIndex="20" CssClass="iconExit"
                                                    OnClick="linkExit_Click" Text="<%$ Resources:cms.language, lblThoat %>" CausesValidation="false"></asp:LinkButton>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
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

        function validateText() {
            var txt_title = document.getElementById('<%= Txt_tieude.ClientID %>');
            var matacgiatin = document.getElementById('<%= HiddenFieldTacgiatin.ClientID %>');
            var txt_tacgia = document.getElementById('<%= txt_PVCTV.ClientID %>');
            var ErrorMesages = document.getElementById('<%= ErrorMesages.ClientID %>');
            var cboanpham = document.getElementById('<%= cbo_AnPham.ClientID %>');
            var cbochuyenmuc = document.getElementById('<%= cbo_chuyenmuc.ClientID %>');
            if (txt_title.value == "") {
                ErrorMesages.innerHTML = '';
                return;
            }
            if (cboanpham.value == 0) {
                ErrorMesages.innerHTML = '';
                return;
            }
            if (cbochuyenmuc.value == 0) {
                ErrorMesages.innerHTML = '';
                return;
            }
            if (txt_tacgia.value == "" || matacgiatin.value == "0") {
                ErrorMesages.innerHTML = '';
                return;
            }
            SaveDocument();
        } 
        
    </script>

    <script type="text/javascript">
            
        $(window).load(
    function() {

         $("#<%=FileUploadImageAttach.ClientID %>").uploadify({
            'swf': '../Dungchung/Scripts/UploadMulti/uploadify.swf',
            'uploader': '../QlyAnh/HandlerUpload.ashx?user=<%=GetUserName()+','+Request["ID"]%>',
            'auto': true,
            'multi': true,
            'folder': 'Upload',
            'fileDesc': 'Image Files',
            'fileExt': '*.jpg;*.jpeg;*.gif;*.png',
            'buttonText': '<%=(string)HttpContext.GetGlobalResourceObject("cms.language", "lblChonanhdinhkem")%>',
            'onUploadSuccess': function() {
                doPostBackAsync('UploadImageSuccess', ''); 
            }
        });
    }
);
        function ClientItemSelected(sender, e) {
            var tentacgia=$(sender.get_element());
            var allitem = $.find('input[data-in="'+tentacgia.attr("data-in")+'"]');
            $(allitem[1]).val(e.get_value());

        }
        function ClientItemSelectedTacGiaTin(sender, e) {
            $get("<%=HiddenFieldTacgiatin.ClientID %>").value = e.get_value();
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

    <script language="JavaScript" type="text/javascript">
        var _width = $(window).width() * 0.70;
        document.getElementById('div_content').style.width = _width + 'px';
        $(window).resize(function() {
            _width = $(window).width() * 0.70;
            document.getElementById('div_content').style.width = _width + 'px';
        });



        function HideActiveX() {
            var checkB = checkBrowser();
            if (checkB == 1) {
                document.getElementById('div_content').style.display = 'none';
            }
            else if (checkB == 2) {
                document.getElementById('div_content').style.width = '1px';
                document.getElementById('div_content').style.height = '1px';
            }
        }
        function ShowActiveX() {
            var checkB = checkBrowser();
            if (checkB == 1) {
                document.getElementById('div_content').style.display = 'inline';
            }
            else if (checkB == 2) {
                var width = $(window).width() * 0.70;
                document.getElementById('div_content').style.width = width + 'px';
                document.getElementById('div_content').style.height = '600px';
            }
        }


        function checkBrowser() {
            var ua = window.navigator.userAgent;
            var is_chrome = navigator.userAgent.toLowerCase().indexOf('chrome') > -1;
            var check = 0;
            // parseInt(ua.substring(msie + 5, ua.indexOf(".", msie)));
            var msie = ua.indexOf("MSIE ");
            if (msie > 0)
                check = 1;
            else {
                if (is_chrome)
                    check = 2;
                else
                    check = 0;
            }
            return check;
        }
    </script>

</asp:Content>
