<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/ITC.Master" AutoEventWireup="true"
    CodeBehind="Send_Email.aspx.cs" Inherits="ToasoanTTXVN.Quytrinh.Send_Email" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxtoolkit" %>
<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>
<%@ Import Namespace="HPCComponents" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <table border="0" cellpadding="0" width="100%" cellspacing="0">
        <tr>
            <td style="text-align: center">
                <div class="divPanelResult">
                    <div style="width: 99%; float: left; padding: 5px 0.5%">
                        <table cellspacing="4" cellpadding="4" width="100%" border="0">
                            <tr>
                                <td style="text-align: left; width: 100%">
                                    <asp:TextBox ID="txt_PVCTV" runat="server" Width="50%" Text="Nhập người nhận" onfocus="if(this.value=='Nhập người nhận') { this.value=''; }"
                                        onblur="if (this.value=='') { this.value='Nhập người nhận'; }" CssClass="inputtext"></asp:TextBox>
                                    <asp:HiddenField ID="HiddenFieldTacgiatin" runat="server" />
                                    <ajaxtoolkit:AutoCompleteExtender runat="server" ID="autoCompleteTacgiaTin" TargetControlID="txt_PVCTV"
                                        ServicePath="../UploadFileMulti/AutoComplete.asmx" ServiceMethod="GetCompletionList"
                                        ContextKey="3" CompletionListCssClass="CompletionListCssClass" MinimumPrefixLength="1"
                                        CompletionInterval="1000" EnableCaching="true" CompletionSetCount="20" OnClientItemSelected="ClientItemSelectedTacGiaTin">
                                    </ajaxtoolkit:AutoCompleteExtender>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: left; width: 100%">
                                    <asp:TextBox ID="Txt_tieude" TabIndex="1" Width="95%" runat="server" Text="Nhập tiêu đề"
                                        onfocus="if(this.value=='Nhập tiêu đề') { this.value=''; }" onblur="if (this.value=='') { this.value='Nhập tiêu đề'; }"
                                        CssClass="inputtext"></asp:TextBox><br />
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: left; width: 100%">
                                    <asp:TextBox ID="Txt_Comments" TabIndex="2" Width="95%" runat="server" CssClass="inputtext"
                                        Text="Nhập góp ý" onfocus="if(this.value=='Nhập góp ý') { this.value=''; }" onblur="if (this.value=='') { this.value='Nhập góp ý'; }"
                                        TextMode="MultiLine" Rows="2"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="vertical-align: top; width: 100%; border: 0px solid #abcdef;">
                                    <CKEditor:CKEditorControl ID="CKE_Noidung" Height="300px" BorderStyle="None" BasePath="../ckeditor/"
                                        Toolbar="Noidung" runat="server" ContentsCss="../ckeditor/contents.css">
                                    </CKEditor:CKEditorControl>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: left; width: 100%">
                                    <div style="width: 10%; float: left; text-align: left">
                                        <asp:FileUpload ID="FileUploadImageAttach" runat="server" />
                                    </div>
                                    <div style="width: 90%; float: left; text-align: left">
                                        <asp:UpdatePanel ID="Upnl_DeleteIMG" runat="server">
                                            <ContentTemplate>
                                                <asp:Button runat="server" ID="btnXoaAnh" TabIndex="21" CssClass="iconDel" Text="Xóa ảnh"
                                                    CausesValidation="false" OnClick="btnXoaAnh_Click"></asp:Button>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: left; width: 100%">
                                    <asp:UpdatePanel ID="UpdatePanel_images" runat="server">
                                        <ContentTemplate>
                                            <div style="text-align: left; width: 100%">
                                                <asp:DataList ID="dgrListImages" runat="server" RepeatColumns="5" RepeatDirection="Horizontal"
                                                    DataKeyField="Ma_Anh" Width="100%" CellPadding="4" OnEditCommand="dgrListImages_EditCommand"
                                                    OnItemDataBound="dgrListImages_ItemDataBound">
                                                    <ItemStyle Width="20%" BorderWidth="0" VerticalAlign="top" HorizontalAlign="Left">
                                                    </ItemStyle>
                                                    <ItemTemplate>
                                                        <div style="width: 90%; height: auto; float: left; background-color: #DCDCDC; position: relative;">
                                                            <asp:CheckBox runat="server" Text='' ID="optSelect" AutoPostBack="False"></asp:CheckBox>
                                                            <asp:ImageButton ID="btnAdd" Style="width: 20px; height: 18px; position: absolute;
                                                                right: 35px; top: 0px; cursor: poiter" CausesValidation="false" runat="server"
                                                                ImageUrl="../Dungchung/Images/action.gif" ImageAlign="AbsMiddle" ToolTip="Sửa thông tin"
                                                                CommandName="Edit" CommandArgument="EditInfo" BorderStyle="None"></asp:ImageButton>
                                                            <asp:ImageButton ID="Imagebuttondelete" Style="width: 20px; height: 20px; position: absolute;
                                                                right: 5px; top: 0px; cursor: poiter" CausesValidation="false" runat="server"
                                                                ImageUrl="../Dungchung/Images/icon-delete.gif" ImageAlign="AbsMiddle" ToolTip="Xóa thông tin"
                                                                CommandName="Edit" CommandArgument="Delete" BorderStyle="None"></asp:ImageButton>
                                                        </div>
                                                        <div style="width: 90%; float: left;">
                                                            <ul class="hoverbox">
                                                                <li><a href="<%=Global.ApplicationPath%><%#Eval("Duongdan_Anh")%>">
                                                                    <img src="<%=Global.ApplicationPath%><%#Eval("Duongdan_Anh")%>" alt="<%# DataBinder.Eval(Container.DataItem, "Chuthich")%>" />
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
                                                        <asp:TextBox ID="txtChuthich" TextMode="MultiLine" Rows="2" Width="90%" runat="server"
                                                            onfocus="if(this.value=='Nhập chú thích ảnh') { this.value=''; }" onblur="if (this.value=='') { this.value='Nhập chú thích ảnh'; }"
                                                            Visible="false" Text='<%# DataBinder.Eval(Container.DataItem, "Chuthich")%>'>
                                                        </asp:TextBox>
                                                        <div style="width: 90%; float: left; text-align: right; margin-top: 4px;">
                                                            <asp:TextBox ID="txtTacgia" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "NguoiChup")%>'
                                                                onfocus="if(this.value=='Nhập tác giả ảnh') { this.value=''; }" onblur="if (this.value=='') { this.value='Nhập tác giả ảnh'; }"
                                                                Visible="false" Width="100%" data-in='<%# DataBinder.Eval(Container.DataItem, "Ma_Anh")%>'></asp:TextBox>
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
                                                        <div style="width: 90%; float: left; margin-top: 3px; text-align: center">
                                                            <asp:ImageButton ID="btnUpdate" Width="20px" CausesValidation="false" runat="server"
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
                                <td align="right">
                                    <asp:Label ID="lblMsg" runat="server" ForeColor="Red"></asp:Label>
                                    <asp:TextBox ID="txtID" Text="0" runat="server" Style="display: none"></asp:TextBox>
                                    <asp:HiddenField ID="word_count" runat="server" Value="0" />
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: center; width: 100%;">
                                    <asp:LinkButton runat="server" ID="linkSave" CssClass="iconSend" TabIndex="17" OnClick="linkSave_Click"
                                        OnClientClick="Demtu();" Text="Gửi tin bài"></asp:LinkButton>
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </td>
        </tr>
    </table>

    <script type="text/javascript" language="javascript">

        function Demtu() {
            var sotu = document.getElementById('cke_wordcount_ctl00_MainContent_CKE_Noidung').innerHTML;
            sotu = sotu.split(':');
            document.getElementById('<% = word_count.ClientID%>').value = sotu[1];
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
            'folder': 'UploadAnh',
            'fileDesc': 'Image Files',
            'fileExt': '*.jpg;*.jpeg;*.gif;*.png',
            'buttonText': 'Ảnh đính kèm',
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

</asp:Content>
