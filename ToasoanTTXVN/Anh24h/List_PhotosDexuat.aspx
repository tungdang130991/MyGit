<%@ Page Language="C#" MasterPageFile="~/Masters/ITC.Master" AutoEventWireup="true"
    CodeBehind="List_PhotosDexuat.aspx.cs" Inherits="HPCApplication.Anh24h.List_PhotosDexuat" %>

<%@ Register TagPrefix="cc1" Namespace="HPCComponents.UI.WebControls" Assembly="HPCComponents" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<%@ Import Namespace="HPCComponents" %>
<%@ Import Namespace="HPCBusinessLogic" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <%--<link rel="Stylesheet" type="text/css" href="../Dungchung/scripts/UploadMulti/uploadify.css" />    

    <script type="text/javascript" src="../Dungchung/scripts/UploadMulti/jquery.uploadify-3.1.min.js"></script>

    <script type="text/javascript" src="../Dungchung/scripts/UploadMulti/jquery.uploadify-3.1.js"></script>

    <link href="../Dungchung/Style/jquery.autocomplete.css" rel="stylesheet" type="text/css" />

    <script src="../Dungchung/Scripts/JsAutoload/jquery.autocomplete.min.js" type="text/javascript"></script>--%>

    <script language="Javascript" type="text/javascript">
        function SetTotal(_dangxl, _tralai) {
            document.getElementById("__tab_ctl00_MainContent_TabContainer1_tabpnltinXuly").innerHTML = "Ảnh đang xử lý (" + _dangxl + ")";
            document.getElementById("__tab_ctl00_MainContent_TabContainer1_TabPanel1").innerHTML = "Ảnh bị trả lại (" + _tralai + ")";
        }
        function checkAll_DM_Clips(objRef, objectid) {
            var GridView = document.getElementById('<%=grdListCate.ClientID%>');
            var inputList = GridView.getElementsByTagName("input");
            for (var i = 0; i < inputList.length; i++) {
                var row = inputList[i].parentNode.parentNode;
                if (inputList[i].type == "checkbox" && objRef != inputList[i]) {
                    if (objRef.checked) {
                        inputList[i].checked = true;
                    }
                    else {
                        inputList[i].checked = false;
                    }
                }
            }
        }
        function checkAll_DM_Clips_tralai(objRef, objectid) {
            var GridView = document.getElementById('<%=dgr_anhbientaplai.ClientID%>');
            var inputList = GridView.getElementsByTagName("input");
            for (var i = 0; i < inputList.length; i++) {
                var row = inputList[i].parentNode.parentNode;
                if (inputList[i].type == "checkbox" && objRef != inputList[i]) {
                    if (objRef.checked) {
                        inputList[i].checked = true;
                    }
                    else {
                        inputList[i].checked = false;
                    }
                }
            }
        }
        function setdisplay(status) {
            if (status == 0)
                $('#displayUpload').hide();
            else
                $('#displayUpload').show();
        }
    </script>

    <table cellpadding="0" cellspacing="0" border="0" width="100%">
        <tr>
            <td class="datagrid_top_left">
            </td>
            <td class="datagrid_top_center">
                <table border="0" cellpadding="1" cellspacing="1" style="float: left;">
                    <tr>
                        <td>
                            <img src="../Dungchung/Images/Icons/to-do-list-cheked-all-icon.png" width="16px"
                                height="16px" />
                        </td>
                        <td style="vertical-align: middle;">
                            <span class="TitlePanel"><%= CommonLib.ReadXML("titNhapanhts")%></span>
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
            <td class="datagrid_content_center">
                <div class="classSearchHeader" style="margin-bottom: 5px;">
                    <table style="width: 100%" cellpadding="1" cellspacing="1">
                        <tr>
                            <td style="width: 20%; text-align: right" class="Titlelbl">
                                <asp:Label ID="Label3" class="Titlelbl" runat="server" 
                                                Text="<%$Resources:cms.language, lblAnpham%>"></asp:Label>
                            </td>
                            <td style="width: 20%; text-align: left">
                                <asp:DropDownList ID="cboLang" runat="server" Width="150" CssClass="inputtext" TabIndex="2">
                                </asp:DropDownList>
                            </td>
                            <td style="width: 10%; text-align: right" class="Titlelbl">
                                <asp:Label ID="Label1" class="Titlelbl" runat="server" 
                                                Text="<%$Resources:cms.language, lblTieude%>"></asp:Label>
                            </td>
                            <td style="width: 30%; text-align: left">
                                <asp:TextBox ID="txtSearch" Width="80%" CssClass="inputtext" onkeypress="return clickButton(event,'ctl00_MainContent_linkSearch');"
                                    runat="server"></asp:TextBox>
                            </td>
                            <td style="width: 20%; text-align: left">
                                <asp:Button runat="server" ID="linkSearch" OnClick="linkSearch_Click" CssClass="iconFind"
                                    Text="<%$Resources:cms.language, lblTimkiem%>" />
                            </td>
                        </tr>
                    </table>
                </div>
            </td>
            <td class="datagrid_content_right">
            </td>
        </tr>
        <tr>
            <td class="datagrid_content_left">
            </td>
            <td class="" style="height: 15px">
            </td>
            <td class="datagrid_content_right">
            </td>
        </tr>
        <tr>
            <td class="datagrid_content_left">
            </td>
            <td style="text-align: left">
                <div class="classSearchHeader">
                    <table width="100%" cellspacing="2" cellpadding="2" border="0">
                        <tr>
                            <td>
                                <div id="displayUpload">
                                    <input type="file" id="FileUploadMutilhpc" />
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td align="left">
                                <cc2:TabContainer ID="TabContainer1" runat="server" CssClass="ajax__tab_yuitabview-theme"
                                    AutoPostBack="true" ActiveTabIndex="0" OnActiveTabChanged="TabContainer1_ActiveTabChanged">
                                    <cc2:TabPanel HeaderText="<%$Resources:cms.language, lblAnhchoxuly%>" ID="tabpnltinXuly" runat="server">
                                        <ContentTemplate>
                                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                <ContentTemplate>
                                                    <div class="classSearchHeader">
                                                        <table border="0" cellspacing="0" cellpadding="0" width="100%">
                                                            <tr>
                                                                <td colspan="2">
                                                                    <asp:DataGrid runat="server" ID="grdListCate" AutoGenerateColumns="false" DataKeyField="Photo_ID"
                                                                        Width="100%" CssClass="Grid" OnEditCommand="grdListCategory_EditCommand" OnItemDataBound="grdListCategory_ItemDataBound">
                                                                        <ItemStyle CssClass="GridItem"></ItemStyle>
                                                                        <AlternatingItemStyle CssClass="GridAltItem" />
                                                                        <HeaderStyle CssClass="GridHeader"></HeaderStyle>
                                                                        <Columns>
                                                                            <asp:TemplateColumn>
                                                                                <HeaderStyle HorizontalAlign="Center" Width="3%"></HeaderStyle>
                                                                                <ItemStyle HorizontalAlign="Center" Width="3%"></ItemStyle>
                                                                                <HeaderTemplate>
                                                                                    <asp:CheckBox ID="chkAll" onclick="javascript:checkAll_DM_Clips(this);" runat="server"
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
                                                                                <HeaderStyle HorizontalAlign="center" Width="10%"></HeaderStyle>
                                                                                <ItemStyle HorizontalAlign="center" Width="10%"></ItemStyle>
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
                                                                                <HeaderStyle HorizontalAlign="Center" Width="36%"></HeaderStyle>
                                                                                <ItemStyle HorizontalAlign="Left" Width="36%"></ItemStyle>
                                                                                <ItemTemplate>
                                                                                    <asp:LinkButton ID="btnEdit" runat="server" CssClass="linkGridForm" Text='<%# DataBinder.Eval(Container.DataItem, "Photo_Name")%>'
                                                                                        ToolTip="<%$Resources:cms.language, lblEdit%>" CommandName="Edit" CommandArgument="Edit"></asp:LinkButton>
                                                                                    <asp:TextBox runat="server" ID="txtTitle" placeholder="<%$Resources:cms.language, lblNhaptieudeanh%>" Width="97%"
                                                                                        TextMode="MultiLine" Rows="3" CssClass="inputtext" Text='<%# DataBinder.Eval(Container.DataItem, "Photo_Name")%>'></asp:TextBox><br />
                                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtTitle"
                                                                                        Display="Dynamic" ErrorMessage="<%$Resources:cms.language, msgNhaptieudeanh%>" Font-Size="Small" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                                                    <div class="clear"></div>
                                                                                    <div class="eol-level-small">
                                                                                        <%= CommonLib.ReadXML("lblNgaynhap") %>:
                                                                                        <asp:Label ID="ngaytao" Font-Bold="true" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Date_Create")!=System.DBNull.Value?Convert.ToDateTime(DataBinder.Eval(Container, "DataItem.Date_Create")).ToString("dd/MM/yyyy HH:mm"):"" %>'>
                                                                                        </asp:Label>
                                                                                        &nbsp;&nbsp;|&nbsp;&nbsp;<%= CommonLib.ReadXML("lblNguoinhap") %>: <b>
                                                                                            <%#HPCBusinessLogic.UltilFunc.GetUserName(Eval("Creator"))%></b>&nbsp;&nbsp;
                                                                                    </div>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateColumn>
                                                                            <asp:TemplateColumn HeaderText="<%$Resources:cms.language, lblAnpham%>">
                                                                                <HeaderStyle HorizontalAlign="Center" Width="14%"></HeaderStyle>
                                                                                <ItemStyle HorizontalAlign="Center" Width="14%"></ItemStyle>
                                                                                <ItemTemplate>
                                                                                    <asp:Label runat="server" ID="lblLangId" Text='<%# DataBinder.Eval(Container.DataItem, "Lang_ID")%>'
                                                                                        Visible="false"></asp:Label>
                                                                                    <asp:Label runat="server" ID="lblAnpham" Text='<%#HPCBusinessLogic.UltilFunc.GetTenAnpham(Eval("Lang_ID"))%>'
                                                                                        Visible="false"></asp:Label>
                                                                                    <asp:DropDownList CssClass="inputtext" ID="cboNgonNgu" runat="server" DataTextField="TenNgonNgu"
                                                                                        DataValueField="ID" Width="100%">
                                                                                    </asp:DropDownList>
                                                                                    <br />
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
                                                                                    <asp:TextBox ID="txt_tacgia" CssClass="inputtext" placeholder="<%$Resources:cms.language, lblNhaptacgia%>" Width="93%"
                                                                                        runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Author_Name")%>'>
                                                                                    </asp:TextBox>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateColumn>
                                                                            <asp:TemplateColumn  HeaderText="<%$Resources:cms.language, lblGhichu%>">
                                                                                <ItemTemplate>
                                                                                    <asp:Label runat="server" ID="lblGhichu" Text='<%# DataBinder.Eval(Container.DataItem, "Photo_Desc")%>'
                                                                                        Visible="false"></asp:Label>
                                                                                    <asp:TextBox ID="txtGhichu" CssClass="inputtext" placeholder="<%$Resources:cms.language, msgNhapghichu%>" Width="94%"
                                                                                        runat="server" TextMode="MultiLine" Rows="3" Text='<%# DataBinder.Eval(Container.DataItem, "Photo_Desc")%>'></asp:TextBox>
                                                                                </ItemTemplate>
                                                                                <HeaderStyle HorizontalAlign="Center" Width="17%"></HeaderStyle>
                                                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
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
                                                                                        ToolTip="<%$Resources:cms.language, lblThoat%>" Style="height: 23px; width: 23px;" CausesValidation="false" CommandName="Edit"
                                                                                        CommandArgument="Back" />
                                                                                </ItemTemplate>
                                                                            </asp:TemplateColumn>
                                                                        </Columns>
                                                                    </asp:DataGrid>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 40%; text-align: left">
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
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="TabContainer1" EventName="ActiveTabChanged" />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                        </ContentTemplate>
                                    </cc2:TabPanel>
                                    <cc2:TabPanel HeaderText="<%$Resources:cms.language, lblAnhtralai%>" ID="TabPanel1" runat="server">
                                        <ContentTemplate>
                                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                <ContentTemplate>
                                                    <table border="0" style="width: 100%">
                                                        <tr>
                                                            <td>
                                                                <asp:DataGrid runat="server" ID="dgr_anhbientaplai" AutoGenerateColumns="false" DataKeyField="Photo_ID"
                                                                    Width="100%" CssClass="Grid" OnEditCommand="dgData_EditCommand1" OnItemDataBound="dgData_ItemDataBound1">
                                                                    <ItemStyle CssClass="GridItem"></ItemStyle>
                                                                    <AlternatingItemStyle CssClass="GridAltItem" />
                                                                    <HeaderStyle CssClass="GridHeader"></HeaderStyle>
                                                                    <Columns>
                                                                        <asp:TemplateColumn>
                                                                            <HeaderStyle HorizontalAlign="Center" Width="3%"></HeaderStyle>
                                                                            <ItemStyle HorizontalAlign="Center" Width="3%"></ItemStyle>
                                                                            <HeaderTemplate>
                                                                                <asp:CheckBox ID="chkAll" onclick="javascript:checkAll_DM_Clips_tralai(this);" runat="server"
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
                                                                            <HeaderStyle HorizontalAlign="center" Width="8%"></HeaderStyle>
                                                                            <ItemStyle HorizontalAlign="center" Width="8%"></ItemStyle>
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblID" Text='<%# DataBinder.Eval(Container.DataItem, "Photo_ID")%>'
                                                                                    Visible="false"></asp:Label>
                                                                                <asp:Label runat="server" ID="lblUrlPath" Text='<%# DataBinder.Eval(Container.DataItem, "Photo_Medium")%>'
                                                                                    Visible="false"></asp:Label>
                                                                                <div>
                                                                                    <img style="<%#CommonLib.CheckImgView(DataBinder.Eval(Container.DataItem,"Photo_Medium"))%>;
                                                                                        padding-top: 3px; border: 0; cursor: pointer;" src="<%# CommonLib.tinpathBDT(DataBinder.Eval(Container.DataItem, "Photo_Medium")) %>"
                                                                                        width="120px" height="80px" align="middle" alt="" onclick="return openNewImage(this, 'close')" />
                                                                                </div>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateColumn>
                                                                        <asp:TemplateColumn HeaderText="<%$Resources:cms.language, lblTieude%>">
                                                                            <HeaderStyle HorizontalAlign="Center" Width="24%"></HeaderStyle>
                                                                            <ItemStyle HorizontalAlign="Left" Width="24%"></ItemStyle>
                                                                            <ItemTemplate>
                                                                                <asp:LinkButton ID="btnEdit" runat="server" CssClass="linkGridForm" Text='<%# DataBinder.Eval(Container.DataItem, "Photo_Name")%>'
                                                                                    ToolTip="<%$Resources:cms.language, lblEdit%>" CommandName="Edit" CommandArgument="Edit"></asp:LinkButton>
                                                                                <asp:TextBox runat="server" ID="txtTitle" placeholder="<%$Resources:cms.language, lblNhaptieudeanh%>" Width="95%"
                                                                                    TextMode="MultiLine" Rows="3" CssClass="inputtext" Text='<%# DataBinder.Eval(Container.DataItem, "Photo_Name")%>'></asp:TextBox><br />
                                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtTitle"
                                                                                    Display="Dynamic" ErrorMessage="<%$Resources:cms.language, msgNhaptieudeanh%>" Font-Size="Small" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                                                <div class="clear"></div>
                                                                                <div class="eol-level-small">
                                                                                    <%= CommonLib.ReadXML("lblNgaynhap") %>:
                                                                                    <asp:Label ID="ngaytao" Font-Bold="true" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Date_Create")!=System.DBNull.Value?Convert.ToDateTime(DataBinder.Eval(Container, "DataItem.Date_Create")).ToString("dd/MM/yyyy HH:mm"):"" %>'>
                                                                                    </asp:Label>
                                                                                    &nbsp;&nbsp;|&nbsp;&nbsp;<%= CommonLib.ReadXML("lblNguoinhap") %>: <b>
                                                                                        <%#HPCBusinessLogic.UltilFunc.GetUserName(Eval("Creator"))%></b>&nbsp;&nbsp;
                                                                                </div>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateColumn>
                                                                        <asp:TemplateColumn HeaderText="<%$Resources:cms.language, lblAnpham%>">
                                                                            <HeaderStyle HorizontalAlign="Center" Width="12%"></HeaderStyle>
                                                                            <ItemStyle HorizontalAlign="Center" Width="12%"></ItemStyle>
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblLangId" Text='<%# DataBinder.Eval(Container.DataItem, "Lang_ID")%>'
                                                                                    Visible="false"></asp:Label>
                                                                                <asp:Label runat="server" ID="lblAnpham" Text='<%#HPCBusinessLogic.UltilFunc.GetTenAnpham(Eval("Lang_ID"))%>'
                                                                                    Visible="false"></asp:Label>
                                                                                <asp:DropDownList CssClass="inputtext" ID="cboNgonNgu" runat="server" DataTextField="TenNgonNgu"
                                                                                    DataValueField="ID" Width="100%">
                                                                                </asp:DropDownList>
                                                                                <br />
                                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="cboNgonNgu"
                                                                                    Display="Dynamic" ErrorMessage="<%$Resources:cms.language, msgChonanpham%>" Font-Size="Small" InitialValue="0"
                                                                                    SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateColumn>
                                                                        <asp:TemplateColumn HeaderText="<%$Resources:cms.language, lblTacgia%>">
                                                                            <HeaderStyle HorizontalAlign="Center" Width="13%"></HeaderStyle>
                                                                            <ItemStyle HorizontalAlign="Center" Width="13%"></ItemStyle>
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblTacGia" Text='<%# DataBinder.Eval(Container.DataItem, "Author_Name")%>'
                                                                                    Visible="false"></asp:Label>
                                                                                <asp:TextBox ID="txt_tacgia" CssClass="inputtext" placeholder="<%$Resources:cms.language, lblNhaptacgia%>" Width="92%"
                                                                                    runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Author_Name")%>'>
                                                                                </asp:TextBox>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateColumn>
                                                                        <asp:TemplateColumn HeaderText="<%$Resources:cms.language, lblNguoitra%>">
                                                                            <HeaderStyle HorizontalAlign="Center" Width="8%"></HeaderStyle>
                                                                            <ItemStyle HorizontalAlign="Center" Width="8%"></ItemStyle>
                                                                            <ItemTemplate>
                                                                                <%#HPCBusinessLogic.UltilFunc.GetUserName(Eval("UserEditor"))%>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateColumn>
                                                                        <asp:TemplateColumn  HeaderText="<%$Resources:cms.language, lblNguoitra%>">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="ngaygui" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Date_Update")!=System.DBNull.Value?Convert.ToDateTime(DataBinder.Eval(Container, "DataItem.Date_Update")).ToString("dd/MM/yyyy HH:mm"):"" %>'>
                                                                                </asp:Label>
                                                                            </ItemTemplate>
                                                                            <HeaderStyle HorizontalAlign="Center" Width="9%"></HeaderStyle>
                                                                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                                        </asp:TemplateColumn>
                                                                        <asp:TemplateColumn HeaderText="<%$Resources:cms.language, lblGhichu%>">
                                                                                <ItemTemplate>
                                                                                    <asp:Label runat="server" ID="lblGhichu" Text='<%# DataBinder.Eval(Container.DataItem, "Photo_Desc")%>'
                                                                                        Visible="false"></asp:Label>
                                                                                    <asp:TextBox ID="txtGhichu" CssClass="inputtext" placeholder="<%$Resources:cms.language, msgNhapghichu%>" Width="94%"
                                                                                        runat="server" TextMode="MultiLine" Rows="3" Text='<%# DataBinder.Eval(Container.DataItem, "Photo_Desc")%>'></asp:TextBox>
                                                                                </ItemTemplate>
                                                                                <HeaderStyle HorizontalAlign="Center" Width="15%"></HeaderStyle>
                                                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
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
                                                                                    ToolTip="<%$Resources:cms.language, lblThoat%>" Style="height: 23px; width: 23px;" CausesValidation="false" CommandName="Edit"
                                                                                    CommandArgument="Back" />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateColumn>
                                                                    </Columns>
                                                                </asp:DataGrid>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <table border="0" style="width: 100%">
                                                                    <tr>
                                                                        <td class="pageNav" style="text-align: right">
                                                                            <cc1:CurrentPage runat="server" ID="CurrentPage1" CssClass="pageNavTotal"></cc1:CurrentPage>
                                                                            <cc1:Pager runat="server" ID="pages1" OnIndexChanged="pages_IndexChanged_Anhtralai"></cc1:Pager>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="TabContainer1" EventName="ActiveTabChanged" />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                        </ContentTemplate>
                                    </cc2:TabPanel>
                                </cc2:TabContainer>
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
                            <td style="width: 100%; padding: 1 0 0 0;" align="left">
                                <asp:Button runat="server" ID="linkSave" CssClass="iconSave" Font-Bold="true" OnClick="linkSave_Click"
                                    Text="<%$Resources:cms.language, lblLuu%>" />
                                <asp:Button runat="server" ID="LinkEdit" CssClass="iconAddNew" Font-Bold="true" OnClick="LinkEdit_Click"
                                    Text="<%$Resources:cms.language, lblSuatatca%>" />
                                <asp:Button runat="server" ID="LinkDangAnh1" OnClick="btnLinkDuyetAnh_Click" CssClass="iconPub"
                                    Text="<%$Resources:cms.language, lblGui%>" CausesValidation="false" />
                                <asp:Button runat="server" ID="LinkDelete" OnClick="btnLinkDelete_Click" CssClass="iconDel"
                                    Text="<%$Resources:cms.language, lblThoat%>" CausesValidation="false" />
                            </td>
                        </tr>
                    </table>
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

    <script type="text/javascript">
        $(window).load(
            function() {
                $("#FileUploadMutilhpc").uploadify({
                    'swf': '../Dungchung/Scripts/UploadMulti/uploadify.swf',
                    'buttonText': '<%= CommonLib.ReadXML("lblChonanh") %>',
                    'uploader': 'UploadAnhTS.ashx?user=<%=GetUserName()%>',
                    'folder': 'Upload',
                    'fileDesc': 'Images Files',
                    'fileExt': '*.jpg;*.png;*.gif;*.jpeg;',
                    'multi': true,
                    'auto': true,
                    'onQueueComplete': function(queueData) {
                        var url = '<%=Global.ApplicationPath %>/Anh24h/List_PhotosDexuat.aspx?<%=getUrlParameter()%>';
                        window.location = url;
                    }
                });
            }
        );
    </script>

</asp:Content>
