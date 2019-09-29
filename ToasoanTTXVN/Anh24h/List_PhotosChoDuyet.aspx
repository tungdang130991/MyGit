<%@ Page Language="C#" MasterPageFile="~/Masters/ITC.Master" AutoEventWireup="true"
    CodeBehind="List_PhotosChoDuyet.aspx.cs" Inherits="ToasoanTTXVN.Anh24h.List_PhotosChoDuyet" %>

<%@ Register TagPrefix="anthem" Namespace="Anthem" Assembly="Anthem" %>
<%@ Register TagPrefix="cc1" Namespace="HPCComponents.UI.WebControls" Assembly="HPCComponents" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<%@ Import Namespace="HPCComponents" %>
<%@ Import Namespace="HPCBusinessLogic" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <script language="Javascript" type="text/javascript">
        function SetTotal(_dangxl, _xuatban, _tralai) {
            document.getElementById("__tab_ctl00_MainContent_TabContainer1_tabpnltinXuly").innerHTML = _dangxl;
            document.getElementById("__tab_ctl00_MainContent_TabContainer1_tabpnltindadang").innerHTML = _xuatban;
            document.getElementById("__tab_ctl00_MainContent_TabContainer1_tabpnltinbtl").innerHTML = _tralai;
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
        function checkAll_DM_Clips_xuatBan(objRef, objectid) {
            var GridView = document.getElementById('<%=DataGrid1.ClientID%>');
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
        function checkboxlang(objRef) {
            var GridView = document.getElementById('<%=dgCopyNgonNgu.ClientID%>');
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
        function cancel() {
            $find('ctl00_MainContent_ModalPopupExtender1').hide();
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
                            <span class="TitlePanel"><%= CommonLib.ReadXML("titDuyetanhts")%></span>
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
                                <asp:TextBox ID="txtSearch_Cate" Width="80%" CssClass="inputtext" onkeypress="return clickButton(event,'<%=linkSearch.ClientID%>');"
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
                            <td align="left">
                                <cc2:TabContainer ID="TabContainer1" runat="server" CssClass="ajax__tab_yuitabview-theme"
                                    AutoPostBack="true" ActiveTabIndex="0" OnActiveTabChanged="TabContainer1_ActiveTabChanged">
                                    <cc2:TabPanel HeaderText="<%$Resources:cms.language, lblAnhchoduyet%>" ID="tabpnltinXuly" runat="server">
                                        <ContentTemplate>
                                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                <ContentTemplate>
                                                    <table style="width: 100%" cellpadding="0" cellspacing="0">
                                                        <tr>
                                                            <td>
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
                                                                            <HeaderStyle HorizontalAlign="Center" Width="26%"></HeaderStyle>
                                                                            <ItemStyle HorizontalAlign="Left" Width="26%"></ItemStyle>
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
                                                                            <HeaderStyle HorizontalAlign="Center" Width="10%"></HeaderStyle>
                                                                            <ItemStyle HorizontalAlign="Center" Width="10%"></ItemStyle>
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblLangId" Text='<%# DataBinder.Eval(Container.DataItem, "Lang_ID")%>'
                                                                                    Visible="false"></asp:Label>
                                                                                <asp:Label runat="server" ID="lblAnpham" Text='<%#HPCBusinessLogic.UltilFunc.GetTenAnpham(Eval("Lang_ID"))%>'
                                                                                    Visible="false"></asp:Label>
                                                                                <asp:DropDownList CssClass="inputtext" ID="cboNgonNgu" runat="server" DataTextField="TenNgonNgu"
                                                                                    DataValueField="ID" Width="95%">
                                                                                </asp:DropDownList>
                                                                                <br />
                                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="cboNgonNgu"
                                                                                    Display="Dynamic" ErrorMessage="<%$Resources:cms.language, msgChonanpham%>" Font-Size="Small" InitialValue="0"
                                                                                    SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateColumn>
                                                                        <asp:TemplateColumn HeaderText="<%$Resources:cms.language, lblTacgia%>">
                                                                            <HeaderStyle HorizontalAlign="Center" Width="10%"></HeaderStyle>
                                                                            <ItemStyle HorizontalAlign="Center" Width="10%"></ItemStyle>
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblTacGia" Text='<%# DataBinder.Eval(Container.DataItem, "Author_Name")%>'
                                                                                    Visible="false"></asp:Label>
                                                                                <asp:TextBox ID="txt_tacgia" CssClass="inputtext" placeholder="<%$Resources:cms.language, lblNhaptacgia%>" Width="90%"
                                                                                    runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Author_Name")%>'>
                                                                                </asp:TextBox>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateColumn>
                                                                        <asp:TemplateColumn HeaderText="<%$Resources:cms.language, lblNhuanbut%>">
                                                                            <HeaderStyle HorizontalAlign="Center" Width="10%"></HeaderStyle>
                                                                            <ItemStyle HorizontalAlign="Center" Width="10%"></ItemStyle>
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblTienNB" Font-Bold="true"
                                                                                    Text='<%#DataBinder.Eval(Container, "DataItem.TienNB")!=System.DBNull.Value? String.Format("{0:00,0}", Convert.ToDecimal( DataBinder.Eval(Container.DataItem, "TienNB"))):""%>'
                                                                                    Visible="false"></asp:Label>
                                                                                <asp:TextBox runat="server" ID="txt_tienNB" Width="90%" placeholder="<%$Resources:cms.language, lblNhuanbut%>" onKeyPress="return check_num(this,15,event);"
                                                                                    onkeyup="javascript:return CommaMonney(this.id);" CssClass="inputtext" 
                                                                                    Text='<%#DataBinder.Eval(Container, "DataItem.TienNB")!=System.DBNull.Value? String.Format("{0:00,0}", Convert.ToDecimal( DataBinder.Eval(Container.DataItem, "TienNB"))):""%>'>
                                                                                    </asp:TextBox>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateColumn>
                                                                        <%--<asp:TemplateColumn>
                                                                            <HeaderStyle HorizontalAlign="Center" Width="8%"></HeaderStyle>
                                                                            <ItemStyle HorizontalAlign="Center" Width="8%"></ItemStyle>
                                                                            <HeaderTemplate>
                                                                                Người nhập
                                                                            </HeaderTemplate>
                                                                            <ItemTemplate>
                                                                                <%#HPCBusinessLogic.UltilFunc.GetUserName(Eval("Creator"))%>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateColumn>
                                                                        <asp:TemplateColumn HeaderText="Ngày nhập">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="ngaytao" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Date_Create")!=System.DBNull.Value?Convert.ToDateTime(DataBinder.Eval(Container, "DataItem.Date_Create")).ToString("dd/MM/yyyy HH:mm"):"" %>'>
                                                                                </asp:Label>
                                                                                <asp:Label ID="ID1" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Photo_ID")%>'>
                                                                                </asp:Label>
                                                                            </ItemTemplate>
                                                                            <HeaderStyle HorizontalAlign="Center" Width="8%"></HeaderStyle>
                                                                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                                        </asp:TemplateColumn>--%>
                                                                        <asp:TemplateColumn HeaderText="<%$Resources:cms.language, lblNguoihieudinh%>">
                                                                            <HeaderStyle HorizontalAlign="Center" Width="8%"></HeaderStyle>
                                                                            <ItemStyle HorizontalAlign="Center" Width="8%"></ItemStyle>
                                                                            <ItemTemplate>
                                                                                <%#HPCBusinessLogic.UltilFunc.GetUserName(Eval("UserEditor"))%>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateColumn>
                                                                        <asp:TemplateColumn HeaderText="<%$Resources:cms.language, lblNgayhieudinh%>">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="ngaygui" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Date_Update")!=System.DBNull.Value?Convert.ToDateTime(DataBinder.Eval(Container, "DataItem.Date_Update")).ToString("dd/MM/yyyy HH:mm"):"" %>'>
                                                                                </asp:Label>
                                                                            </ItemTemplate>
                                                                            <HeaderStyle HorizontalAlign="Center" Width="8%"></HeaderStyle>
                                                                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                                        </asp:TemplateColumn>
                                                                        <asp:TemplateColumn HeaderText="<%$Resources:cms.language, lblGhichu%>">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblGhichu" Text='<%# DataBinder.Eval(Container.DataItem, "Photo_Desc")%>'
                                                                                    Visible="false"></asp:Label>
                                                                                <asp:TextBox ID="txtGhichu" CssClass="inputtext" placeholder="<%$Resources:cms.language, msgNhapghichu%>" Width="90%" runat="server" 
                                                                                    TextMode="MultiLine" Rows="3" Text='<%# DataBinder.Eval(Container.DataItem, "Photo_Desc")%>'></asp:TextBox>
                                                                            </ItemTemplate>
                                                                            <HeaderStyle HorizontalAlign="Center" Width="12%"></HeaderStyle>
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
                                                                        <td style="text-align: right" class="pageNav">
                                                                            <cc1:CurrentPage runat="server" ID="CurrentPage2" CssClass="pageNavTotal">
                                                                            </cc1:CurrentPage>
                                                                            <cc1:Pager runat="server" ID="pages" OnIndexChanged="pages_IndexChanged"></cc1:Pager>
                                                                        </td>
                                                                    </tr>
                                                                </table>
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
                                                                    Text="<%$Resources:cms.language, lblDanganh%>" CausesValidation="false" />
                                                                <asp:Button runat="server" ID="LinkTrans" Visible="false" CssClass="iconCopy" OnClick="link_copy_Click"
                                                                    Text="<%$Resources:cms.language, lblDich%>" CausesValidation="false" />
                                                                <asp:Button runat="server" ID="LinkDelete" OnClick="btnLinkTra_Click" CssClass="iconReply"
                                                                    Text="<%$Resources:cms.language, lblTralai%>" CausesValidation="false" />
                                                                <asp:Button runat="server" ID="btnXoa" OnClick="btnLinkDelete_Click" CssClass="iconDel"
                                                                    Text="<%$Resources:cms.language, lblXoa%>" CausesValidation="false" />
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
                                    <cc2:TabPanel HeaderText="<%$Resources:cms.language, lblAnhdadang%>" ID="tabpnltindadang" runat="server">
                                        <ContentTemplate>
                                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                <ContentTemplate>
                                                    <table border="0" style="width: 100%">
                                                        <tr>
                                                            <td>
                                                                <asp:DataGrid ID="DataGrid1" runat="server" Width="100%" AutoGenerateColumns="False"
                                                                    DataKeyField="Photo_ID" CssClass="Grid" OnItemDataBound="grdListCategory_ItemDataBound1"
                                                                    OnEditCommand="grdListCategory_EditCommand1">
                                                                    <ItemStyle CssClass="GridItem"></ItemStyle>
                                                                    <AlternatingItemStyle CssClass="GridAltItem" />
                                                                    <HeaderStyle CssClass="GridHeader"></HeaderStyle>
                                                                    <Columns>
                                                                        <asp:BoundColumn DataField="Photo_ID" HeaderText="Photo_ID" Visible="False"></asp:BoundColumn>
                                                                        <asp:TemplateColumn HeaderText="<%$Resources:cms.language, lblHinhanh%>">
                                                                            <HeaderStyle HorizontalAlign="center" Width="13%"></HeaderStyle>
                                                                            <ItemStyle HorizontalAlign="center" Width="13%"></ItemStyle>
                                                                            <ItemTemplate>
                                                                                <div>
                                                                                    <img style="<%#CommonLib.CheckImgView(DataBinder.Eval(Container.DataItem,"Photo_Medium"))%>"
                                                                                        src="<%# CommonLib.tinpathBDT(DataBinder.Eval(Container.DataItem, "Photo_Medium")) %>"
                                                                                        width="120px" height="80px" align="middle" style="padding-top: 3px; border: 0;
                                                                                        cursor: pointer" onclick="return openNewImage(this, '')">
                                                                                </div>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateColumn>
                                                                        <asp:TemplateColumn HeaderText="<%$Resources:cms.language, lblTieude%>">
                                                                            <HeaderStyle HorizontalAlign="center" Width="34%"></HeaderStyle>
                                                                            <ItemStyle HorizontalAlign="Left" Width="34%"></ItemStyle>
                                                                            <ItemTemplate>
                                                                                <%# DataBinder.Eval(Container.DataItem, "Photo_Name")%>
                                                                                <div class="clear"></div>
                                                                                <div class="eol-level-small">
                                                                                    Ngày nhập:
                                                                                    <asp:Label ID="ngaytao" Font-Bold="true" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Date_Create")!=System.DBNull.Value?Convert.ToDateTime(DataBinder.Eval(Container, "DataItem.Date_Create")).ToString("dd/MM/yyyy HH:mm"):"" %>'>
                                                                                    </asp:Label>
                                                                                    &nbsp;&nbsp;|&nbsp;&nbsp;Người nhập: <b>
                                                                                        <%#HPCBusinessLogic.UltilFunc.GetUserName(Eval("Creator"))%></b>&nbsp;&nbsp;
                                                                                </div>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateColumn>
                                                                        <asp:TemplateColumn HeaderText="<%$Resources:cms.language, lblAnpham%>">
                                                                            <HeaderStyle HorizontalAlign="Center" Width="10%"></HeaderStyle>
                                                                            <ItemStyle HorizontalAlign="Center" Width="10%"></ItemStyle>
                                                                            <ItemTemplate>
                                                                                <%#HPCBusinessLogic.UltilFunc.GetTenAnpham(Eval("Lang_ID"))%>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateColumn>
                                                                        <%--<asp:TemplateColumn>
                                                                            <HeaderStyle HorizontalAlign="Center" Width="10%"></HeaderStyle>
                                                                            <ItemStyle HorizontalAlign="Center" Width="10%"></ItemStyle>
                                                                            <HeaderTemplate>
                                                                                Người nhập
                                                                            </HeaderTemplate>
                                                                            <ItemTemplate>
                                                                                <%#HPCBusinessLogic.UltilFunc.GetUserName(Eval("Creator"))%>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateColumn>
                                                                        <asp:TemplateColumn HeaderText="Ngày nhập">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="ngaytao" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Date_Create")!=System.DBNull.Value?Convert.ToDateTime(DataBinder.Eval(Container, "DataItem.Date_Create")).ToString("dd/MM/yyyy HH:mm"):"" %>'>
                                                                                </asp:Label>
                                                                                <asp:Label ID="ID1" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Photo_ID")%>'>
                                                                                </asp:Label>
                                                                            </ItemTemplate>
                                                                            <HeaderStyle HorizontalAlign="Center" Width="10%"></HeaderStyle>
                                                                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                                        </asp:TemplateColumn>--%>
                                                                        <asp:TemplateColumn HeaderText="<%$Resources:cms.language, lblNguoiXB%>">
                                                                            <HeaderStyle HorizontalAlign="Center" Width="10%"></HeaderStyle>
                                                                            <ItemStyle HorizontalAlign="Center" Width="10%"></ItemStyle>
                                                                            <ItemTemplate>
                                                                                <%#HPCBusinessLogic.UltilFunc.GetUserName(Eval("UserEditor"))%>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateColumn>
                                                                        <asp:TemplateColumn HeaderText="<%$Resources:cms.language, lblNgayXB%>">
                                                                            <ItemTemplate>
                                                                                <%# DataBinder.Eval(Container, "DataItem.Date_Update")!=System.DBNull.Value?Convert.ToDateTime(DataBinder.Eval(Container, "DataItem.Date_Update")).ToString("dd/MM/yyyy HH:mm"):"" %>
                                                                            </ItemTemplate>
                                                                            <HeaderStyle HorizontalAlign="Center" Width="13%"></HeaderStyle>
                                                                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
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
                                                                            <cc1:Pager runat="server" ID="pages1" OnIndexChanged="pages_IndexChanged"></cc1:Pager>
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
                                    <cc2:TabPanel HeaderText="<%$Resources:cms.language, lblAnhbientaplai%>" ID="tabpnltinbtl" runat="server">
                                        <ContentTemplate>
                                            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                                <ContentTemplate>
                                                    <table border="0" style="width: 100%">
                                                        <tr>
                                                            <td>
                                                                <asp:DataGrid ID="dgr_anhbientaplai" runat="server" Width="100%" AutoGenerateColumns="False"
                                                                    DataKeyField="Photo_ID" CssClass="Grid" OnItemDataBound="dgData_ItemDataBound1"
                                                                    OnEditCommand="dgData_EditCommand1">
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
                                                                            <ItemTemplate>
                                                                                <asp:CheckBox runat="server" Text='' ID='optSelect' AutoPostBack="False"></asp:CheckBox>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateColumn>
                                                                        <asp:BoundColumn DataField="Photo_ID" HeaderText="Photo_ID" Visible="False"></asp:BoundColumn>
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
                                                                            <HeaderStyle HorizontalAlign="Center" Width="26%"></HeaderStyle>
                                                                            <ItemStyle HorizontalAlign="Left" Width="26%"></ItemStyle>
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
                                                                            <HeaderStyle HorizontalAlign="Center" Width="10%"></HeaderStyle>
                                                                            <ItemStyle HorizontalAlign="Center" Width="10%"></ItemStyle>
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblLangId" Text='<%# DataBinder.Eval(Container.DataItem, "Lang_ID")%>'
                                                                                    Visible="false"></asp:Label>
                                                                                <asp:Label runat="server" ID="lblAnpham" Text='<%#HPCBusinessLogic.UltilFunc.GetTenAnpham(Eval("Lang_ID"))%>'
                                                                                    Visible="false"></asp:Label>
                                                                                <asp:DropDownList CssClass="inputtext" ID="cboNgonNgu" runat="server" DataTextField="TenNgonNgu"
                                                                                    DataValueField="ID" Width="95%">
                                                                                </asp:DropDownList>
                                                                                <br />
                                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="cboNgonNgu"
                                                                                    Display="Dynamic" ErrorMessage="<%$Resources:cms.language, msgChonanpham%>" Font-Size="Small" InitialValue="0"
                                                                                    SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateColumn>
                                                                        <asp:TemplateColumn HeaderText="<%$Resources:cms.language, lblTacgia%>">
                                                                            <HeaderStyle HorizontalAlign="Center" Width="10%"></HeaderStyle>
                                                                            <ItemStyle HorizontalAlign="Center" Width="10%"></ItemStyle>
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblTacGia" Text='<%# DataBinder.Eval(Container.DataItem, "Author_Name")%>'
                                                                                    Visible="false"></asp:Label>
                                                                                <asp:TextBox ID="txt_tacgia" CssClass="inputtext" placeholder="<%$Resources:cms.language, lblNhaptacgia%>" Width="90%"
                                                                                    runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Author_Name")%>'>
                                                                                </asp:TextBox>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateColumn>
                                                                        <asp:TemplateColumn>
                                                                            <HeaderStyle HorizontalAlign="Center" Width="10%"></HeaderStyle>
                                                                            <ItemStyle HorizontalAlign="Center" Width="10%"></ItemStyle>
                                                                            <HeaderTemplate>
                                                                                Tiền nhuận bút
                                                                            </HeaderTemplate>
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblTienNB" Font-Bold="true"
                                                                                    Text='<%#DataBinder.Eval(Container, "DataItem.TienNB")!=System.DBNull.Value? String.Format("{0:00,0}", Convert.ToDecimal( DataBinder.Eval(Container.DataItem, "TienNB"))):""%>'
                                                                                    Visible="false"></asp:Label>
                                                                                <asp:TextBox runat="server" ID="txt_tienNB" Width="90%" placeholder="<%$Resources:cms.language, lblNhuanbut%>" onKeyPress="return check_num(this,15,event);"
                                                                                    onkeyup="javascript:return CommaMonney(this.id);" CssClass="inputtext" 
                                                                                    Text='<%#DataBinder.Eval(Container, "DataItem.TienNB")!=System.DBNull.Value? String.Format("{0:00,0}", Convert.ToDecimal( DataBinder.Eval(Container.DataItem, "TienNB"))):""%>'>
                                                                                    </asp:TextBox>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateColumn>
                                                                        <%--<asp:TemplateColumn>
                                                                            <HeaderStyle HorizontalAlign="Center" Width="8%"></HeaderStyle>
                                                                            <ItemStyle HorizontalAlign="Center" Width="8%"></ItemStyle>
                                                                            <HeaderTemplate>
                                                                                Người nhập
                                                                            </HeaderTemplate>
                                                                            <ItemTemplate>
                                                                                <%#HPCBusinessLogic.UltilFunc.GetUserName(Eval("Creator"))%>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateColumn>
                                                                        <asp:TemplateColumn HeaderText="Ngày nhập">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="ngaytao" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Date_Create")!=System.DBNull.Value?Convert.ToDateTime(DataBinder.Eval(Container, "DataItem.Date_Create")).ToString("dd/MM/yyyy HH:mm"):"" %>'>
                                                                                </asp:Label>
                                                                                <asp:Label ID="ID1" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Photo_ID")%>'>
                                                                                </asp:Label>
                                                                            </ItemTemplate>
                                                                            <HeaderStyle HorizontalAlign="Center" Width="8%"></HeaderStyle>
                                                                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                                        </asp:TemplateColumn>--%>
                                                                        <asp:TemplateColumn HeaderText="<%$Resources:cms.language, lblNguoitra%>">
                                                                            <HeaderStyle HorizontalAlign="Center" Width="8%"></HeaderStyle>
                                                                            <ItemStyle HorizontalAlign="Center" Width="8%"></ItemStyle>
                                                                            <ItemTemplate>
                                                                                <%#HPCBusinessLogic.UltilFunc.GetUserName(Eval("UserEditor"))%>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateColumn>
                                                                        <asp:TemplateColumn HeaderText="<%$Resources:cms.language, lblNgaytra%>">
                                                                            <ItemTemplate>
                                                                                <%# DataBinder.Eval(Container, "DataItem.Date_Update")!=System.DBNull.Value?Convert.ToDateTime(DataBinder.Eval(Container, "DataItem.Date_Update")).ToString("dd/MM/yyyy HH:mm"):"" %>
                                                                            </ItemTemplate>
                                                                            <HeaderStyle HorizontalAlign="Center" Width="8%"></HeaderStyle>
                                                                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                                        </asp:TemplateColumn>
                                                                        <asp:TemplateColumn HeaderText="<%$Resources:cms.language, lblGhichu%>">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblGhichu" Text='<%# DataBinder.Eval(Container.DataItem, "Photo_Desc")%>'
                                                                                    Visible="false"></asp:Label>
                                                                                <asp:TextBox ID="txtGhichu" CssClass="inputtext" placeholder="<%$Resources:cms.language, msgNhapghichu%>" Width="90%" runat="server" 
                                                                                    TextMode="MultiLine" Rows="3" Text='<%# DataBinder.Eval(Container.DataItem, "Photo_Desc")%>'></asp:TextBox>
                                                                            </ItemTemplate>
                                                                            <HeaderStyle HorizontalAlign="Center" Width="12%"></HeaderStyle>
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
                                                                            <cc1:CurrentPage runat="server" ID="curentPagesNgung">
                                                                            </cc1:CurrentPage>
                                                                            <cc1:Pager runat="server" ID="pageNgung" OnIndexChanged="pages_IndexChanged_AnhNgung"></cc1:Pager>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="2" style="height: 5px">
                                                                <span style="font-weight: bold; font-size: medium; color: #EB0C27; float: left;">
                                                                    <asp:Literal runat="server" ID="litMessages1"></asp:Literal>
                                                                </span>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 100%; padding: 1 0 0 0;" align="left">
                                                                <asp:Button runat="server" ID="Button1" CssClass="iconSave" Font-Bold="true" OnClick="linkSave_Click"
                                                                    Text="<%$Resources:cms.language, lblLuu%>" />
                                                                <asp:Button runat="server" ID="Button2" CssClass="iconAddNew" Font-Bold="true" OnClick="LinkEdit_Click"
                                                                    Text="<%$Resources:cms.language, lblSuatatca%>" />
                                                                <asp:Button runat="server" ID="btnDangAnh2" OnClick="btnLinkDuyetAnh_Click" CssClass="iconPub"
                                                                    Text="<%$Resources:cms.language, lblDanganh%>" CausesValidation="false" />
                                                                <asp:Button runat="server" ID="LinkTrans2" CssClass="iconCopy" OnClick="link_copy_Click"
                                                                    Text="<%$Resources:cms.language, lblDich%>" Visible="false" CausesValidation="false" />
                                                                <asp:Button runat="server" ID="btnXoa2" OnClick="btnLinkDelete_Click" CssClass="iconDel"
                                                                    Text="<%$Resources:cms.language, lblXoa%>" CausesValidation="false" />
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
                                    <td style="width: 100%; text-align: right" colspan="2">
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
                                                        <asp:Button runat="server" ID="but_XB" CssClass="iconCopy" Font-Bold="true" OnClick="but_Trans_Click"
                                                            Text="Dịch ngữ"></asp:Button>
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
</asp:Content>
