<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/ITC.Master" AutoEventWireup="true" CodeBehind="AddMutiPhotos.aspx.cs" Inherits="ToasoanTTXVN.Anh24h.AddMutiPhotos" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<%@ Register TagPrefix="cc1" Namespace="HPCComponents.UI.WebControls" Assembly="HPCComponents" %>
<%@ Import Namespace="HPCComponents" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <link rel="Stylesheet" type="text/css" href="../Dungchung/scripts/UploadMulti/uploadify.css" />

    <script src="../Dungchung/scripts/UploadMulti/jquery-1.7.2.min.js" type="text/javascript" />

    <script type="text/javascript" src="../Dungchung/scripts/UploadMulti/jquery.uploadify-3.1.min.js"></script>

    <script type="text/javascript" src="../Dungchung/scripts/UploadMulti/jquery.uploadify-3.1.js"></script>

    <link href="../Dungchung/Style/jquery.autocomplete.css" rel="stylesheet" type="text/css" />

    <script src="../Dungchung/Scripts/JsAutoload/jquery.autocomplete.min.js" type="text/javascript"></script>

    <script language="Javascript" type="text/javascript">
        function CheckAll_One(objRef) {
            var GridView = document.getElementById('<%=grdListCate.ClientID%>');
            var inputList = GridView.getElementsByTagName("input");
            for (var i = 0; i < inputList.length; i++) {
                var row = inputList[i].parentNode.parentNode;
                if (inputList[i].type == "checkbox" && objRef != inputList[i]) {
                    if (objRef.checked && inputList[i].disabled == false) {
                        inputList[i].checked = true;
                    }
                    else {
                        inputList[i].checked = false;
                    }
                }
            }
        }
        
        function OnCheckBox(_values) {
            document.getElementById(_values).checked = true;
            var GridView = document.getElementById('<%=grdListCate.ClientID%>');
            var elm = GridView.getElementsByTagName("input");
            for (i = 0; i < elm.length; i++) {
                if (elm[i].type == "checkbox" && elm[i].id != _values) {
                    if (elm[i].checked) {
                        elm[i].checked = false;
                    }
                }
            }
        }
        function cancel() {
            $find('ctl00_MainContent_ModalPopupExtender1').hide();
        }

        var tmp_Window;
        function BrowserVideoFile(vKey) {
            SubmitImage('../Until/FileManager.aspx?vType=4&vKey=' + vKey + '', 840, 580);
        }
    </script>

    <script type="text/javascript">

        function AutoCompleteSearch_Author(arr_Name, arr_ID) {
            try {
                var i = 0;
                var arr_ID_Name = arr_Name.split(",");
                var arr_ID_ID = arr_ID.split(",");
                for (i = 0; i < arr_ID_Name.length; i++) {
                    var a = arr_ID_Name[i];
                    var b = arr_ID_ID[i]
                    if (a.length > 0 && b.length > 0)
                        Run(a, b);
                }
            } catch (err) {
                //alert(err); 
            }
        }
        function Run(a, b) {
            try {
            $(document).ready(function() {
                $('#' + a).autocomplete("AutoCompleteSearch.ashx").result(
                    function(event, data, formatted) {
                        try {
                            if (data) { $('#' + b).val(data[1]); }
                            else { $('#' + b).val('0'); }
                        } catch (err) {
                            //alert(err);
                        }
                    });
            });
            }
            catch (err) {
                //alert(err);
            }
        }
        window.onload = function() {
            FauxPlaceholder();
        }
        
    </script>

    <table border="0" cellpadding="0" width="100%" cellspacing="0">
        <tr>
            <td class="datagrid_top_left">
            </td>
            <td class="datagrid_top_center">
                <table border="0" cellpadding="1" cellspacing="1" width="100%">
                    <tr>
                        <td style="width: 2%">
                            <img src="../Dungchung/Images/Icons/cog-edit-icon.png" width="16px" height="16px" />
                        </td>
                        <td style="vertical-align: middle; text-align: left">
                            <span class="TitlePanel">CẬP NHẬT ẢNH THỜI SỰ
                                
                        </td>
                    </tr>
                </table>
            </td>
            <td class="datagrid_top_right">
            </td>
        </tr>
        <tr>
            <td class="datagrid_content_left">
            </td>
            <td style="text-align: center">
                <table width="100%" align="center" cellspacing="2" cellpadding="2">
                    <tr>
                        <td colspan="2" style="text-align: left">
                            <div>
                                <asp:FileUpload ID="FileUpload1" runat="server" />
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 100%" colspan="2">
                            <asp:UpdatePanel ID="UpdatePL" runat="server">
                                <ContentTemplate>
                                    <div class="classSearchHeader">
                                        <table border="0" cellspacing="0" cellpadding="0" width="100%">
                                            <tr>
                                                <td colspan="2">
                                                    <asp:DataGrid runat="server" ID="grdListCate" AutoGenerateColumns="false" DataKeyField="Photo_ID"
                                                        Width="100%" CssClass="Grid" OnEditCommand="grdListCategory_EditCommand" OnItemDataBound="grdListCategory_ItemDataBound" >
                                                        <ItemStyle CssClass="GridItem"></ItemStyle>
                                                        <AlternatingItemStyle CssClass="GridAltItem" />
                                                        <HeaderStyle CssClass="GridHeader"></HeaderStyle>
                                                        <Columns>
                                                            <asp:TemplateColumn>
                                                                <HeaderStyle HorizontalAlign="Center" Width="3%"></HeaderStyle>
                                                                <ItemStyle HorizontalAlign="Center" Width="3%"></ItemStyle>
                                                                <HeaderTemplate>
                                                                    <asp:CheckBox ID="chkAll" onclick="javascript:CheckAll_One(this);" runat="server"
                                                                        ToolTip="<%$Resources:cms.language, lblChontatca%>"></asp:CheckBox>
                                                                </HeaderTemplate>
                                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                                <ItemTemplate>
                                                                    <asp:CheckBox runat="server" Text='' ID='optSelect' AutoPostBack="False"></asp:CheckBox>
                                                                </ItemTemplate>
                                                            </asp:TemplateColumn>
                                                            <asp:BoundColumn Visible="False" DataField="Photo_ID">
                                                                <HeaderStyle Width="1%"></HeaderStyle>
                                                            </asp:BoundColumn>
                                                            <asp:TemplateColumn HeaderText="<%$Resources:cms.language, lblHinhanh%>">
                                                                <HeaderStyle HorizontalAlign="center" Width="15%"></HeaderStyle>
                                                                <ItemStyle HorizontalAlign="center" Width="15%"></ItemStyle>
                                                                <ItemTemplate>
                                                                    <asp:Label runat="server" ID="lblID" Text='<%# DataBinder.Eval(Container.DataItem, "Photo_ID")%>'
                                                                        Visible="false"></asp:Label>
                                                                    <asp:Label runat="server" ID="lblUrlPath" Text='<%# DataBinder.Eval(Container.DataItem, "Photo_Medium")%>'
                                                                        Visible="false"></asp:Label>
                                                                    <div>
                                                                        <img style="<%#CommonLib.CheckImgView(DataBinder.Eval(Container.DataItem,"Photo_Medium"))%>;
                                                                            padding-top: 3px; border: 0; cursor: pointer;" src="<%# CommonLib.tinpathBDT(DataBinder.Eval(Container.DataItem, "Photo_Medium")) %>"
                                                                            width="120px" height="80px" align="middle" alt="" onclick="return openNewImage(this, 'close')" />
                                                                       <%-- <asp:Image ID="imgView" style="<%#CommonLib.CheckImgView(DataBinder.Eval(Container.DataItem,"Photo_Medium"))%>;
                                                                            padding-top: 3px; border: 0; cursor: pointer;" ImageUrl="<%# CommonLib.tinpathBDT(DataBinder.Eval(Container.DataItem, "Photo_Medium")) %>"
                                                                            Width="120px" Height="80px" runat="server" OnClick="return openNewImage(this, 'close')"/>
                                                                        <asp:Image ID="imgBrowse" style="<%#CommonLib.CheckImgView(DataBinder.Eval(Container.DataItem,"Photo_Medium"))%>;
                                                                            padding-top: 3px; border: 0; cursor: pointer;" ImageUrl="<%# CommonLib.tinpathBDT(DataBinder.Eval(Container.DataItem, "Photo_Medium")) %>"
                                                                            Width="120px" Height="80px" runat="server" OnClick="BrowserVideoFile(1);" />--%>
                                                                    </div>
                                                                </ItemTemplate>
                                                            </asp:TemplateColumn>                                         
                                                            <asp:TemplateColumn HeaderText="<%$Resources:cms.language, lblTieude%>">
                                                                <HeaderStyle HorizontalAlign="Center" Width="37%"></HeaderStyle>
                                                                <ItemStyle HorizontalAlign="Left" Width="37%"></ItemStyle>
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="btnEdit" runat="server" CssClass="linkGridForm" Text='<%# DataBinder.Eval(Container.DataItem, "Photo_Name")%>'
                                                                        ToolTip="<%$Resources:cms.language, lblEdit%>" CommandName="Edit" CommandArgument="Edit"></asp:LinkButton>
                                                                    <asp:TextBox runat="server" ID="txtTitle" placeholder="<%$Resources:cms.language, lblNhaptieudeanh%>" Width="95%" TextMode="MultiLine" Rows="3"
                                                                        CssClass="inputtext" Text='<%# DataBinder.Eval(Container.DataItem, "Photo_Name")%>'></asp:TextBox><br />
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtTitle"
                                                                        Display="Dynamic" ErrorMessage="<%$Resources:cms.language, msgNhaptieudeanh%>" Font-Size="Small" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                                </ItemTemplate>
                                                            </asp:TemplateColumn>
                                                            <asp:TemplateColumn HeaderText="<%$Resources:cms.language, lblAnpham%>">
                                                                <HeaderStyle HorizontalAlign="Center" Width="15%"></HeaderStyle>
                                                                <ItemStyle HorizontalAlign="Center" Width="15%"></ItemStyle>
                                                                <ItemTemplate>
                                                                    <asp:Label runat="server" ID="lblLangId" Text='<%# DataBinder.Eval(Container.DataItem, "Lang_ID")%>'
                                                                        Visible="false"></asp:Label>
                                                                    <asp:Label runat="server" ID="lblAnpham" Text='<%#HPCBusinessLogic.UltilFunc.GetTenAnpham(Eval("Lang_ID"))%>'
                                                                        Visible="false"></asp:Label>
                                                                    <asp:DropDownList CssClass="inputtext" ID="cboNgonNgu" runat="server"
                                                                        DataTextField="TenNgonNgu" DataValueField="ID" Width="95%"></asp:DropDownList><br />
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="cboNgonNgu"
                                                                        Display="Dynamic" ErrorMessage="<%$Resources:cms.language, msgChonanpham%>" Font-Size="Small" InitialValue="0"
                                                                        SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                                </ItemTemplate>
                                                            </asp:TemplateColumn>
                                                            <asp:TemplateColumn HeaderText="<%$Resources:cms.language, lblTacgia%>">
                                                                <HeaderStyle HorizontalAlign="Center" Width="15%"></HeaderStyle>
                                                                <ItemStyle HorizontalAlign="Center" Width="15%"></ItemStyle>
                                                                <ItemTemplate>
                                                                    <asp:Label runat="server" ID="lblTacGia" Text='<%# DataBinder.Eval(Container.DataItem, "Author_Name")%>'
                                                                        Visible="false"></asp:Label>
                                                                    <asp:TextBox ID="txt_tacgia" CssClass="inputtext" placeholder="<%$Resources:cms.language, lblNhaptacgia%>" Width="90%" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Author_Name")%>'>
                                                                    </asp:TextBox>
                                                                </ItemTemplate>
                                                            </asp:TemplateColumn> 
                                                            <asp:TemplateColumn HeaderText="<%$Resources:cms.language, lblNgaynhap%>">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="ngaytao" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Date_Create")!=System.DBNull.Value?Convert.ToDateTime(DataBinder.Eval(Container, "DataItem.Date_Create")).ToString("dd/MM/yyyy HH:mm"):"" %>'>
                                                                    </asp:Label>
                                                                    <asp:Label ID="ID1" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Photo_ID")%>'>
                                                                    </asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" Width="10%"></HeaderStyle>
                                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                            </asp:TemplateColumn>   
                                                            <asp:TemplateColumn>
                                                                <HeaderStyle HorizontalAlign="Center" Width="5%"></HeaderStyle>
                                                                <ItemStyle HorizontalAlign="Center" Width="5%"></ItemStyle>
                                                                <ItemTemplate>
                                                                    <asp:ImageButton ID="btnSave" runat="server" ImageUrl="~/Dungchung/Images/button/save-icon.png"
                                                                            ToolTip="<%$Resources:cms.language, lblLuu%>" CommandName="Edit" CausesValidation="false" CommandArgument="SavePhoto" />
                                                                    <asp:ImageButton ID="btnModify" runat="server" ImageUrl="~/Dungchung/Images/EditImg.png"
                                                                            ToolTip="<%$Resources:cms.language, lblEdit%>" CommandName="Edit" CausesValidation="false" CommandArgument="EditPhoto" />
                                                                    <asp:ImageButton ID="btnBack" runat="server" ImageUrl="~/Dungchung/Images/Button-Back.png"
                                                                            ToolTip="<%$Resources:cms.language, lblThoat%>" style="height:23px; width:23px;" CausesValidation="false" CommandName="Edit" CommandArgument="Back" />
                                                                </ItemTemplate>
                                                            </asp:TemplateColumn>                                            
                                                        </Columns>
                                                    </asp:DataGrid>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2" style="height: 5px">
                                                    <span style="font-weight: bold; font-size: medium; color: #EB0C27; float: left;">
                                                        <asp:Literal runat="server" ID="litMessages"></asp:Literal>
                                                    </span>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 40%; text-align: left">
                                                    <asp:Button runat="server" ID="linkSave" CssClass="iconSave" Font-Bold="true" OnClick="linkSave_Click"
                                                        Text="<%$Resources:cms.language, lblLuu%>" />
                                                    <asp:Button runat="server" ID="LinkEdit" CssClass="iconAddNew" Font-Bold="true" OnClick="LinkEdit_Click"
                                                        Text="<%$Resources:cms.language, lblSuatatca%>" />
                                                    <asp:Button runat="server" ID="LinkDangAnh1" OnClick="btnLinkDuyetAnh_Click" CssClass="iconPub"
                                                        Text="<%$Resources:cms.language, lblGui%>" CausesValidation="false" />
                                                    <asp:Button runat="server" ID="LinkTrans" CssClass="iconCopy" OnClick="link_copy_Click"
                                                            Text="<%$Resources:cms.language, lblDich%>" CausesValidation="false" />
                                                    <asp:Button runat="server" ID="LinkTra" OnClick="btnLinkTra_Click" CssClass="iconReply"
                                                        Text="<%$Resources:cms.language, lblTralai%>" CausesValidation="false" />
                                                    <asp:Button runat="server" ID="LinkCancel" CssClass="iconDel" CausesValidation="false"
                                                        Font-Bold="true" OnClick="LinkCancel_Click" Text="Xóa" />
                                                    <asp:Button runat="server" ID="Link_Back" CssClass="iconExit" Font-Bold="true" CausesValidation="false"
                                                        OnClick="Link_Back_Click" Text="<%$Resources:cms.language, lblThoat%>" />
                                                </td>
                                                <td class="Titlelbl" style="text-align: right">
                                                    <cc1:CurrentPage runat="server" ID="curentPages" CssClass="pageNavTotal">
                                                    </cc1:CurrentPage>
                                                    <cc1:Pager runat="server" ID="pages" OnIndexChanged="pages_IndexChanged">
                                                    </cc1:Pager>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
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
    <!--Phần popup-->
    <div style="clear: both;" />
    <a id="hnkAddMenu" runat="server" style="visibility: hidden"></a>
    <asp:Label ID="lbl_CATID" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lbl_News_CopyFrom_ID" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lbl_News_ID" runat="server" Text="" Visible="true"></asp:Label>
    <cc2:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="hnkAddMenu"
        BackgroundCssClass="ModalPopupBG" PopupControlID="Panelone" Drag="true" PopupDragHandleControlID="PopupHeader">
    </cc2:ModalPopupExtender>
    <div id="Panelone" style="display: none;">
        <div class="popup_ContainerEvent">
            <div class="popup_Titlebar" id="PopupHeader">
                <div class="TitlebarLeft">
                    <asp:Literal runat="server" ID="litTittleForm"></asp:Literal>
                    Chọn ngôn ngữ cần dịch
                </div>
                <div class="TitlebarRight" onclick="cancel();">
                </div>
            </div>
            <div class="popup_BodyCopy">
                <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                    <ContentTemplate>
                        <div id="displayContainer">
                            <table width="100%" cellspacing="2" cellpadding="2" border="0" style="background-color: white;">
                                <tr>
                                    <td style="width: 100%; text-align: right">
                                        <div class="popup_Body_Fix_width_heightCopy" style="width: 98%">
                                            <table border="0" cellpadding="1" cellspacing="1" style="width: 100%; text-align: left;">
                                                <tr>
                                                    <td>
                                                        <asp:DataGrid runat="server" ID="dgCopyNgonNgu" AutoGenerateColumns="false" DataKeyField="ID"
                                                            Width="100%" CssClass="Grid">
                                                            <ItemStyle CssClass="GridItem"></ItemStyle>
                                                            <AlternatingItemStyle CssClass="GridAltItem" />
                                                            <HeaderStyle CssClass="GridHeader"></HeaderStyle>
                                                            <Columns>
                                                                <asp:TemplateColumn>
                                                                    <HeaderStyle HorizontalAlign="Center" Width="6%"></HeaderStyle>
                                                                    <ItemStyle HorizontalAlign="Center" Width="6%"></ItemStyle>
                                                                    <HeaderTemplate>
                                                                        <asp:CheckBox ID="chkAll" onclick="javascript:checkboxlang(this);" runat="server"
                                                                            ToolTip="Chọn tất cả"></asp:CheckBox>
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:CheckBox runat="server" Text='' ID='optSelect' AutoPostBack="False" Enabled="true">
                                                                        </asp:CheckBox>
                                                                    </ItemTemplate>
                                                                </asp:TemplateColumn>
                                                                <asp:TemplateColumn>
                                                                    <HeaderStyle Width="50%" HorizontalAlign="Left"></HeaderStyle>
                                                                    <ItemStyle HorizontalAlign="Left" Width="50%"></ItemStyle>
                                                                    <HeaderTemplate>
                                                                        Ngôn ngữ
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <%#Eval("TenNgonNgu")%>
                                                                    </ItemTemplate>
                                                                </asp:TemplateColumn>
                                                            </Columns>
                                                        </asp:DataGrid>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: left;" align="left">
                                        <div class="classSearchHeader" style="width: 98%">
                                            <table style="width: 100%">
                                                <tr>
                                                    <td style="text-align: left">
                                                        <asp:Button runat="server" ID="but_XB" CssClass="iconCopy" Font-Bold="true" OnClick="but_Trans_Click"
                                                            Text="Dịch ngữ"></asp:Button>
                                        </div>
                                    </td>
                                    <td style="text-align: right;">
                                        <input class="iconExit" type="button" value="Đóng" onclick="cancel();" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                        </td> </tr> </table> </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
    <script type="text/javascript">
        $(window).load(
            function() {
                $("#<%=FileUpload1.ClientID %>").uploadify({
                'swf': '../Dungchung/Scripts/UploadMulti/uploadify.swf',
                    'buttonText': 'Chọn ảnh',
                    'uploader': 'UploadAnhTS.ashx?user=<%=GetUserName()%>',
                    'folder': 'Upload',
                    'fileDesc': 'Images Files',
                    'fileExt': '*.jpg;*.png;*.gif;*.jpeg;',
                    'multi': true,
                    'auto': true,
                    'onQueueComplete': function(queueData) {
                    var url = '<%=Global.ApplicationPath %>/Anh24h/AddMutiPhotos.aspx?<%=getUrlParameter()%>';
                        window.location = url;
                    }
                });
            }
        );
    </script>

</asp:Content>

