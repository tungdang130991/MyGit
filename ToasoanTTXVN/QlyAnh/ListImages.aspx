<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/ITC.Master" AutoEventWireup="true"
    CodeBehind="ListImages.aspx.cs" Inherits="ToasoanTTXVN.QlyAnh.ListImages" %>

<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral,PublicKeyToken=31BF3856AD364E35" %>
<%@ Register TagPrefix="cc1" Namespace="HPCComponents.UI.WebControls" Assembly="HPCComponents" %>
<%@ Import Namespace="HPCComponents" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<%@ Register TagPrefix="nbc" Namespace="HPCComponents.UI" Assembly="HPCComponents" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <script type="text/javascript" language="javascript">

        function openWebLink(url) {
            if (url.length > 0)
                window.open(url, 'popup', 'location=no,directories=no,resizable=yes,status=yes,toolbar=no,menubar=no, width=' + screen.width + ',height=' + screen.height + ',scrollbars=yes,top='.concat((screen.height - 500) / 2).concat(',left=').concat(20));
        }
    </script>

    <script language="javascript" type="text/javascript">
        function cancel() {
            $get('ctl00_MainContent_btnCancel').click();
        }  
        
    </script>

    <script language="Javascript" type="text/javascript">

        function openNewImage(file, imgText) {
            if (file.lang == 'no-popup') return;
            picfile = new Image();
            picfile.src = (file.src);
            width = picfile.width;
            height = picfile.height;

            if (imgText != '' && height > 0) {
                height += 40;
            }
            else if (height == 0) {
                height = screen.height;
            }

            winDef = 'status=no,resizable=yes,scrollbars=no,toolbar=no,location=no,fullscreen=no,titlebar=yes,height='.concat(height).concat(',').concat('width=').concat(width).concat(',');
            winDef = winDef.concat('top=').concat((screen.height - height) / 2).concat(',');
            winDef = winDef.concat('left=').concat((screen.width - width) / 2);
            newwin = open('', '_blank', winDef);

            newwin.document.writeln('<style>a:visited{color:blue;text-decoration:none}</style>');
            newwin.document.writeln('<body topmargin="0" leftmargin="0" marginheight="0" marginwidth="0">');
            newwin.document.writeln('<div style="width:100%;height:100%;overflow:auto;"><a style="cursor:pointer" href="javascript:window.close()"><img src="', file.src, '" border=0></a>');
            if (imgText != '') {
                newwin.document.writeln('<div align="center" style="padding-top:5px;font-weight:bold;font-family:arial,Verdana,Tahoma;color:blue">', imgText, '</div></div>');
            }
            newwin.document.writeln('</body>');
            newwin.document.close();
        }
        
    </script>

    <style>
        #displayContainer2
        {
            padding-right: 1px;
            padding-left: 1px;
            scrollbar-face-color: #cacaca;
            font-size: 10pt;
            padding-bottom: 1px;
            margin: 0px;
            scrollbar-highlight-color: #cacaca;
            overflow: auto;
            width: 100%;
            scrollbar-shadow-color: #cacaca;
            color: #505050;
            scrollbar-3dlight-color: #cacaca;
            scrollbar-arrow-color: #505050;
            padding-top: 1px;
            scrollbar-track-color: #cacaca;
            font-family: Verdana, Arial, Helvetica, sans-serif;
            scrollbar-darkshadow-color: #cacaca;
            height: 380px;
            width: 99.6%;
        }
        TD.currentFolderContent
        {
            border-right: #cccccc 0px solid;
            border-top: #cccccc 0px solid;
            font-weight: bold;
            font-size: 12px;
            border-left: #cccccc 0px solid;
            color: #000099;
            border-bottom: #cccccc 1px solid;
            font-family: Arial, Helvetica, sans-serif;
            background-color: #ffffff;
        }
    </style>
    <link rel="Stylesheet" type="text/css" href="../Dungchung/Scripts/jsUpload/CSS/uploadify.css" />

    <script type="text/javascript" src="../Dungchung/Scripts/jsUpload/scripts/jquery-1.3.2.min.js"></script>

    <script type="text/javascript" src="../Dungchung/Scripts/jsUpload/scripts/jquery.uploadify.js"></script>

    <table border="0" cellpadding="0" width="100%" cellspacing="0">
        <tr>
            <td class="datagrid_top_left">
            </td>
            <td class="datagrid_top_center">
                <table border="0" cellpadding="1" cellspacing="1">
                    <tr>
                        <td>
                            <img src="../Images/Icons/cog-edit-icon.png" width="16px" alt="" height="16px" />
                        </td>
                        <td style="vertical-align: middle">
                            <span class="TitlePanel">+ ẢNH MỚI CẬP NHẬT </span>
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
            <td style="text-align: left">
                <table border="0" cellpadding="1" cellspacing="1" width="100%">
                    <tr>
                        <td style="width: 100%" colspan="2">
                            <table border="0" cellspacing="1px" cellspacing="1px" style="text-align: right; width: 100%">
                                <tr>
                                    <td class="Titlelbl" style="vertical-align: middle; text-align: right; width: 30%">
                                    </td>
                                    <td style="width: 10%; text-align: right" class="Titlelbl">
                                        Từ Ngày:
                                    </td>
                                    <td style="width: 15%; text-align: left">
                                        <nbc:NetDatePicker ImageUrl="../Dungchung/Images/events.gif" ImageFolder="../Dungchung/scripts/DatePicker/Images"
                                            Width="150px" CssClass="inputtext" ScriptSource="../Dungchung/Scripts/datepicker.js"
                                            ID="txt_FromDate" runat="server" onKeyPress="AscciiDisable()" onfocus="javascript:vDateType='3'"
                                            onKeyUp="DateFormat(this,this.value,event,false,'3')" onBlur="DateFormat(this,this.value,event,true,'3')"></nbc:NetDatePicker>
                                        <asp:RegularExpressionValidator ID="Regularexpressionvalidator3" runat="server" ControlToValidate="txt_FromDate"
                                            ValidationExpression="^\d{1,2}/\d{1,2}/\d{4}$" Display="Dynamic">Kiểu ngày:dd/MM/yyyy</asp:RegularExpressionValidator>
                                    </td>
                                    <td style="width: 10%; text-align: right" class="Titlelbl">
                                        Đến ngày:
                                    </td>
                                    <td style="width: 15%; text-align: left" class="Titlelbl">
                                        <nbc:NetDatePicker ImageUrl="../Dungchung/Images/events.gif" ImageFolder="../Dungchung/Scripts/DatePicker/Images"
                                            Width="150px" CssClass="inputtext" ScriptSource="../Dungchung/Scripts/datepicker.js"
                                            ID="txt_ToDate" runat="server" onKeyPress="AscciiDisable()" onfocus="javascript:vDateType='3'"
                                            onKeyUp="DateFormat(this,this.value,event,false,'3')" onBlur="DateFormat(this,this.value,event,true,'3')"></nbc:NetDatePicker>
                                        <asp:RegularExpressionValidator ID="Regularexpressionvalidator1" runat="server" ControlToValidate="txt_ToDate"
                                            ValidationExpression="^\d{1,2}/\d{1,2}/\d{4}$" Display="Dynamic">Kiểu ngày:dd/MM/yyyy</asp:RegularExpressionValidator><br />
                                    </td>
                                    <td style="width: 20%; text-align: left;">
                                        <asp:Button CausesValidation="false" runat="server" ID="btnSearch" CssClass="myButton green"
                                            Font-Bold="true" Text="Tìm kiếm" OnClick="btnSearch_Click"></asp:Button>
                                        <asp:Button CausesValidation="false" runat="server" ID="btnUpload" CssClass="myButton orange"
                                            Font-Bold="true" Text="Upload ảnh" OnClick="btnUpload_Click"></asp:Button>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <a id="hnkAddMenu" runat="server" style="visibility: hidden"></a>
                            <cc2:ModalPopupExtender ID="ModalPopupExtender1" BackgroundCssClass="ModalPopupBG"
                                runat="server" TargetControlID="hnkAddMenu" CancelControlID="btnCancel" PopupControlID="Panelone"
                                Drag="true" PopupDragHandleControlID="PopupHeader">
                            </cc2:ModalPopupExtender>
                            <!--Edit giay moi-->
                            <div id="Panelone" style="display: none; width: 750px">
                                <div class="popup_Container">
                                    <div class="popup_Titlebar" id="PopupHeader">
                                        <asp:UpdatePanel ID="UpnTit" runat="server">
                                            <ContentTemplate>
                                                <div class="TitlebarLeft">
                                                    <asp:Literal runat="server" ID="litTittleForm" Text="UPLOAD PHOTO"></asp:Literal>
                                                </div>
                                                <div class="TitlebarRight" onclick="cancel();">
                                                </div>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                    <div class="popup_Body">
                                        <asp:UpdatePanel ID="Upbody" runat="server">
                                            <ContentTemplate>
                                                <div id="displayContainer2">
                                                    <table width="100%" cellspacing="2" cellpadding="4" border="0" style="background-color: white;">
                                                        <tr>
                                                            <td colspan="2" style="height: 370px; vertical-align: top;">
                                                                <asp:FileUpload ID="FileUpload1" runat="server" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                            </td>
                                                            <td align="center">
                                                                <input id="btnCancel" value="Thoát" type="button" class="buttonhide" runat="server"
                                                                    onserverclick="btnCancel_Click" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 100%" colspan="2">
                            <asp:DataGrid runat="server" ID="dgrListAppro" AutoGenerateColumns="false" DataKeyField="Ma_Anh"
                                Width="100%" CssClass="Grid" CellPadding="1" OnItemDataBound="dgrListAppro_ItemDataBound"
                                OnUpdateCommand="dgrListAppro_UpdateCommand" OnCancelCommand="dgrListAppro_CancelCommand"
                                OnEditCommand="dgrListAppro_EditCommand">
                                <ItemStyle CssClass="GridItem" Height="25px"></ItemStyle>
                                <AlternatingItemStyle CssClass="GridAltItem" />
                                <HeaderStyle CssClass="GridHeader"></HeaderStyle>
                                <Columns>
                                    <asp:BoundColumn Visible="False" DataField="Ma_Anh">
                                        <HeaderStyle Width="1%"></HeaderStyle>
                                    </asp:BoundColumn>
                                    <asp:TemplateColumn>
                                        <HeaderStyle HorizontalAlign="Center" Width="5%"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Center" Width="5%" CssClass="GridBorderVerSolid"></ItemStyle>
                                        <HeaderTemplate>
                                            <asp:CheckBox ID="chkAll" onclick="javascript:SelectAllCheckboxes(this);" runat="server"
                                                ToolTip="Chọn tất cả"></asp:CheckBox>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:CheckBox runat="server" Text='' ID='optSelect' AutoPostBack="False"></asp:CheckBox>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn>
                                        <HeaderTemplate>
                                            Ảnh
                                        </HeaderTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle Width="12%" HorizontalAlign="Left" CssClass="GridBorderVerSolid"></ItemStyle>
                                        <ItemTemplate>
                                            <div class="gallery">
                                                <div class="pictgalery" style="width: 142px;">
                                                    <img src="<%=Global.ApplicationPath%><%#Eval("Duongdan_Anh")%>" align="middle" onclick="return openNewImage(this, '')">
                                                </div>
                                            </div>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn>
                                        <HeaderTemplate>
                                            Tiêu đề / Chú thích
                                        </HeaderTemplate>
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle Width="40%" HorizontalAlign="Left" CssClass="GridBorderVerSolid"></ItemStyle>
                                        <ItemTemplate>
                                            <div class="stringtieudeandsc">
                                                <div class="fontTitle" style="width: 90%;">
                                                    <asp:Label ID="lbtitle" runat="server" Text='<%#Eval("TieuDe")%>'></asp:Label>
                                                </div>
                                                <div class="chuthichcss">
                                                    <asp:Label ID="lbdesc" runat="server" Text='<%#Eval("Chuthich")%>'></asp:Label>
                                                </div>
                                                <div class="fontTitle" style="width: 100%; text-align: right;">
                                                    <asp:Label ID="lbtacgia" runat="server" Text='<%#Eval("NguoiChup")%>'></asp:Label>
                                                </div>
                                            </div>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <div class="stringtieudeandsc">
                                                <div class="fontTitle" style="width: 90%;">
                                                    <asp:TextBox ID="txtTieude" runat="server" Width="90%" Text='<%# DataBinder.Eval(Container.DataItem, "TieuDe")%>'></asp:TextBox>
                                                </div>
                                                <div class="chuthichcss">
                                                    <asp:TextBox ID="txtChuthich" TextMode="MultiLine" Rows="4" Width="90%" runat="server"
                                                        Text='<%# DataBinder.Eval(Container.DataItem, "Chuthich")%>'></asp:TextBox>
                                                </div>
                                                <div class="fontTitle" style="width: 90.5%; text-align: right;">
                                                    <asp:TextBox ID="txtTacgia" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "NguoiChup")%>'></asp:TextBox>
                                                    <cc2:AutoCompleteExtender runat="server" ID="autoComplete1" TargetControlID="txtTacgia"
                                                        ServicePath="AutoComplete.asmx" ServiceMethod="GetCompletionList" MinimumPrefixLength="1"
                                                        CompletionInterval="1000" EnableCaching="true" CompletionSetCount="20">
                                                    </cc2:AutoCompleteExtender>
                                                </div>
                                            </div>
                                        </EditItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn>
                                        <HeaderStyle HorizontalAlign="Center" Width="10%"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Center" Width="10%" CssClass="GridBorderVerSolid"></ItemStyle>
                                        <HeaderTemplate>
                                            Ngày tạo
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "NgayTao")).ToString("dd/MM/yyyy hh:mm")%>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn>
                                        <HeaderTemplate>
                                            Sửa
                                        </HeaderTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle Width="7%" HorizontalAlign="Center" CssClass="GridBorderVerSolid"></ItemStyle>
                                        <ItemTemplate>
                                            <asp:ImageButton ID="btnAdd" Width="15px" runat="server" ImageUrl="~/Dungchung/images/action.gif"
                                                ImageAlign="AbsMiddle" ToolTip="Sửa thông tin" CommandName="Edit" CommandArgument="Edit"
                                                BorderStyle="None" Enabled='<%#IsRoleWrite()%>'></asp:ImageButton>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:ImageButton ID="btnUpdate" Width="15px" runat="server" ImageUrl="~/Dungchung/images/save.gif"
                                                ImageAlign="AbsMiddle" ToolTip="Cập nhật" CommandName="Update" BorderStyle="None">
                                            </asp:ImageButton>
                                            <asp:ImageButton ID="btnCancel" Width="15px" runat="server" ImageUrl="~/Dungchung/images/undo.gif"
                                                ImageAlign="AbsMiddle" ToolTip="Hủy bỏ" CommandName="Cancel" BorderStyle="None">
                                            </asp:ImageButton>
                                        </EditItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn>
                                        <HeaderStyle HorizontalAlign="Center" Width="5%"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Center" Width="5%" CssClass="GridBorderVerSolid"></ItemStyle>
                                        <HeaderTemplate>
                                            Xóa
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:ImageButton ID="btnDelete" Width="15px" runat="server" ImageUrl="~/Dungchung/images/cancel.gif"
                                                ImageAlign="AbsMiddle" ToolTip="Xóa ảnh" CommandName="Edit" CommandArgument="Delete"
                                                BorderStyle="None" Enabled='<%#IsRoleDelete()%>'></asp:ImageButton>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                </Columns>
                            </asp:DataGrid>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" style="height: 10px;">
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left">
                            <asp:Button CausesValidation="false" runat="server" ID="btnDelete" CssClass="myButton red"
                                Font-Bold="true" Text="Xóa" OnClick="btnDelete_Click" Width="90px"></asp:Button>
                        </td>
                        <td style="text-align: right" class="pageNav">
                            <cc1:CurrentPage runat="server" ID="CurrentPage1"></cc1:CurrentPage>
                            &nbsp;
                            <cc1:Pager runat="server" ID="pageappro" OnIndexChanged="pageappro_IndexChanged"></cc1:Pager>
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

    <script type="text/javascript">
        $(window).load(
    function() {
        $("#<%=FileUpload1.ClientID %>").fileUpload({
            'uploader': '../Dungchung/Scripts/jsUpload/scripts/uploader.swf',
            'cancelImg': '../Dungchung/Scripts/jsUpload/images/cancel.png',
            'buttonText': 'Browse Files',
            'script': 'HandlerUpload.ashx?user=<%=GetUserName()%>',
            'folder': 'UploadAnh',
            'fileDesc': 'Image Files',
            'fileExt': '*.jpg;*.jpeg;*.gif;*.png',
            'multi': true,
            'auto': true,
            'onComplete': function() {
                var url = '<%=Global.ApplicationPath %>/QlyAnh/ListImages.aspx?Menu_ID=<%=Page.Request["Menu_ID"].ToString()%>';
                window.location = url;
            }

        });
    }
);
    </script>

</asp:Content>
