<%@ Page Language="C#" MasterPageFile="~/Masters/ITC.Master" AutoEventWireup="true"
    CodeBehind="Multimedia_Approve.aspx.cs" Inherits="ToasoanTTXVN.Multimedia.Multimedia_Approve" %>

<%@ Register TagPrefix="anthem" Namespace="Anthem" Assembly="Anthem" %>
<%@ Register TagPrefix="cc1" Namespace="HPCComponents.UI.WebControls" Assembly="HPCComponents" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<%@ Import Namespace="HPCComponents" %>
<%@ Import Namespace="HPCBusinessLogic" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <script type="text/javascript" language="javascript">
        function checkAll_DM_Clips(objRef, objectid) {
            var GridView = document.getElementById('<%=dgData_ChoXuatban.ClientID%>');
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
        function checkAll_CM(objRef, objectid) {
            var GridView = document.getElementById('<%=dgCategorysCopy.ClientID%>');
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
        function SetInnerProcess(_choxuatban, _daxuly) {
            document.getElementById("__tab_ctl00_MainContent_TabContainer1_tabChoduyet").innerHTML = _choxuatban;
            document.getElementById("__tab_ctl00_MainContent_TabContainer1_tabDaxuatban").innerHTML = _daxuly;
        }
        function cancel() {
            $find('ctl00_MainContent_ModalPopupExtender1').hide();
        }
    </script>

    <table border="0" cellpadding="0" width="100%" cellspacing="0">
        <tr>
            <td class="datagrid_top_left">
            </td>
            <td class="datagrid_top_center">
                <table border="0" cellpadding="1" cellspacing="1">
                    <tr>
                        <td>
                            <img src="../Dungchung/Images/Icons/to-do-list-cheked-all-icon.png" width="16px"
                                height="16px" />
                        </td>
                        <td style="vertical-align: middle">
                            <span class="TitlePanel"><%= CommonLib.ReadXML("titDuyetamthanh")%></span>
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
            <td>
                <div class="classSearchHeader">
                    <table>
                        <tr>
                            <td style="width: 10%; text-align: right;" class="Titlelbl">
                                <asp:Label ID="Label3" class="Titlelbl" runat="server" 
                                                Text="<%$Resources:cms.language, lblAnpham%>"></asp:Label>
                            </td>
                            <td style="width: 15%; text-align: right;" class="Titlelbl">
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                    <ContentTemplate>
                                        <asp:DropDownList AutoPostBack="true" ID="ddlLang" Width="150px" CssClass="inputtext"
                                            runat="server" OnSelectedIndexChanged="ddlLang_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                            <td style="width: 10%; text-align: right;" class="Titlelbl">
                                <asp:Label ID="lblChuyenmuc" class="Titlelbl" runat="server" 
                                                Text="<%$Resources:cms.language, lblChuyenmuc%>"></asp:Label>
                            </td>
                            <td style="width: 15%; text-align: right;" class="Titlelbl">
                                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                    <ContentTemplate>
                                        <asp:DropDownList AutoPostBack="true" ID="ddlCategorys" runat="server" Width="180px"
                                            CssClass="inputtext" TabIndex="5">
                                        </asp:DropDownList>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                            <td style="width: 10%; text-align: right;" class="Titlelbl">
                                <asp:Label ID="Label1" class="Titlelbl" runat="server" 
                                                Text="<%$Resources:cms.language, lblTieude%>"></asp:Label>
                            </td>
                            <td style="width: 30%; text-align: left">
                                <asp:TextBox ID="txtSearch" Width="300px" runat="server" CssClass="inputtext" onkeypress="return clickButton(event,'ctl00_MainContent_linkSearch');"></asp:TextBox>
                                
                            </td>
                            <td style="width: 10%; text-align: center;">
                                <asp:Button runat="server" ID="linkSearch" CssClass="iconFind" TabIndex="16" Font-Bold="true"
                                    OnClick="linkSearch_Click" Text="<%$Resources:cms.language, lblTimkiem%>"></asp:Button>
                            </td>
                        </tr>
                    </table>
                </div>
                <br />
                <div class="classSearchHeader">
                    <table width="100%" cellspacing="0" cellpadding="0" border="0" style="text-align: left">
                        <tr>
                            <td>
                                <cc2:TabContainer ID="TabContainer1" runat="server" CssClass="ajax__tab_yuitabview-theme"
                                    AutoPostBack="true" ActiveTabIndex="0" OnActiveTabChanged="TabContainer1_ActiveTabChanged">
                                    <cc2:TabPanel HeaderText="<%$Resources:cms.language, lblChoduyet%>" ID="tabChoduyet" runat="server">
                                        <ContentTemplate>
                                            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                                <ContentTemplate>
                                                    <table width="100%" cellspacing="2" cellpadding="2" border="0">
                                                        <tr>
                                                            <td style="text-align: left" colspan="2">
                                                                <asp:Button runat="server" ID="btnAddNew" CssClass="iconAddNew" Font-Bold="true"
                                                                    OnClick="cmdAdd_Click" Text="<%$Resources:cms.language, lblThemmoi%>" />
                                                                <asp:Button runat="server" ID="btnXuatBanOn" CssClass="iconPub" Font-Bold="true"
                                                                    Text="<%$Resources:cms.language, lblXuatban%>" OnClick="lbt_Xuatban_Click" />
                                                                <asp:Button runat="server" Visible="false" ID="btnTranslateOn" CssClass="iconCopy" CausesValidation="false"
                                                                    Font-Bold="true" OnClick="link_copy_Click" Text="<%$Resources:cms.language, lblDich%>" />
                                                                <asp:Button runat="server" ID="btnReturnOn" CssClass="iconReply" CausesValidation="false"
                                                                    Font-Bold="true" Text="<%$Resources:cms.language, lblTralai%>" OnClick="lbt_Tralai_Click" />
                                                                <asp:Button runat="server" ID="btnXoaOn" CssClass="iconDel" Font-Bold="true" OnClick="lbt_xoa_Click"
                                                                    Text="<%$Resources:cms.language, lblXoa%>" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="center" colspan="2">
                                                                <asp:DataGrid ID="dgData_ChoXuatban" runat="server" Width="100%" AutoGenerateColumns="False"
                                                                    DataKeyField="ID" OnEditCommand="dgData_ChoXuatban_EditCommandEditor" OnItemDataBound="dgData_ChoXuatban_ItemDataBoundEditor"
                                                                    CssClass="Grid">
                                                                    <ItemStyle CssClass="GridItem"></ItemStyle>
                                                                    <AlternatingItemStyle CssClass="GridAltItem" />
                                                                    <HeaderStyle CssClass="GridHeader"></HeaderStyle>
                                                                    <Columns>
                                                                        <asp:BoundColumn DataField="ID" HeaderText="ID" Visible="False"></asp:BoundColumn>
                                                                        <asp:TemplateColumn>
                                                                            <HeaderStyle HorizontalAlign="Center" Width="3%"></HeaderStyle>
                                                                            <ItemStyle HorizontalAlign="Center" Width="3%"></ItemStyle>
                                                                            <HeaderTemplate>
                                                                                <asp:CheckBox ID="chkAll" onclick="javascript:checkAll_DM_Clips(this);" runat="server"
                                                                                    ToolTip="<%$Resources:cms.language, lblChontatca%>"></asp:CheckBox>
                                                                            </HeaderTemplate>
                                                                            <ItemTemplate>
                                                                                <asp:CheckBox runat="server" Text='' ID='optSelect' AutoPostBack="False"></asp:CheckBox>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateColumn>
                                                                        <asp:TemplateColumn HeaderText="Video">
                                                                            <HeaderStyle Width="12%" HorizontalAlign="Center"></HeaderStyle>
                                                                            <ItemStyle Width="12%" HorizontalAlign="Center" />
                                                                            <ItemTemplate>
                                                                                <a href="Javascript:PopupWindowVideo('<%=Global.ApplicationPath%>/Multimedia/ViewVideo.aspx?Menu_ID=<%=Page.Request.QueryString["Menu_ID"]%>&ID=<%# DataBinder.Eval(Container.DataItem, "ID") %>');" />
                                                                                <img src='<%#CommonLib._returnimg(Eval("URL_Images"))%>' style="width: 120px;" border="0"
                                                                                    alt="Xem Video" onmouseover="(window.status=''); return true" style="cursor: pointer;
                                                                                    border: 0" title="Xem Video" />
                                                                                <asp:Label ID="lblcatid" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container.DataItem, "ID")%>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateColumn>
                                                                        <asp:TemplateColumn HeaderText="<%$Resources:cms.language, lblTieude%>">
                                                                            <HeaderStyle Width="20%" HorizontalAlign="Center"></HeaderStyle>
                                                                            <ItemStyle Width="20%" HorizontalAlign="Left" />
                                                                            <ItemTemplate>
                                                                                <asp:LinkButton ID="btnEdit" runat="server" CssClass="linkEdit" Text='<%# DataBinder.Eval(Container.DataItem, "Tittle") %>'
                                                                                    ToolTip="Sửa đổi" CommandName="Edit" CommandArgument="Edit"></asp:LinkButton>
                                                                                <asp:Label runat="server" ID="lblLogTitle" Visible="false" Text='<%# DataBinder.Eval(Container.DataItem, "Tittle") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateColumn>
                                                                        <asp:TemplateColumn HeaderText="<%$Resources:cms.language, lblChuyenmuc%>">
                                                                            <HeaderStyle Width="11%" HorizontalAlign="Center"></HeaderStyle>
                                                                            <ItemStyle Width="11%" HorizontalAlign="Left"></ItemStyle>
                                                                            <ItemTemplate>
                                                                                <%#HPCBusinessLogic.UltilFunc.GetCategoryName(Eval("Category"))%>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateColumn>
                                                                        <asp:TemplateColumn HeaderText="<%$Resources:cms.language, lblNguoinhap%>">
                                                                            <ItemTemplate>
                                                                                <%#HPCBusinessLogic.UltilFunc.GetUserName(Eval("UserCreated"))%>
                                                                            </ItemTemplate>
                                                                            <HeaderStyle HorizontalAlign="Center" Width="8%"></HeaderStyle>
                                                                            <ItemStyle HorizontalAlign="Center" Width="8%"></ItemStyle>
                                                                        </asp:TemplateColumn>
                                                                        <asp:TemplateColumn HeaderText="<%$Resources:cms.language, lblNgaynhap%>">
                                                                            <ItemTemplate>
                                                                                <%# DataBinder.Eval(Container, "DataItem.DateCreated")!=System.DBNull.Value?Convert.ToDateTime(DataBinder.Eval(Container, "DataItem.DateCreated")).ToString("dd/MM/yyyy HH:mm:ss"):"" %>
                                                                            </ItemTemplate>
                                                                            <HeaderStyle HorizontalAlign="Center" Width="9%"></HeaderStyle>
                                                                            <ItemStyle HorizontalAlign="Center" Width="9%"></ItemStyle>
                                                                        </asp:TemplateColumn>
                                                                        <asp:TemplateColumn HeaderText="<%$Resources:cms.language, lblNguoihieudinh%>">
                                                                            <ItemTemplate>
                                                                                <%#HPCBusinessLogic.UltilFunc.GetUserName(Eval("UserModify"))%>
                                                                            </ItemTemplate>
                                                                            <HeaderStyle HorizontalAlign="Center" Width="8%"></HeaderStyle>
                                                                            <ItemStyle HorizontalAlign="Center" Width="8%"></ItemStyle>
                                                                        </asp:TemplateColumn>
                                                                        <asp:TemplateColumn HeaderText="<%$Resources:cms.language, lblNgayhieudinh%>">
                                                                            <ItemTemplate>
                                                                                <%# DataBinder.Eval(Container, "DataItem.DateModify") != System.DBNull.Value ? Convert.ToDateTime(DataBinder.Eval(Container, "DataItem.DateModify")).ToString("dd/MM/yyyy HH:mm:ss") : ""%>
                                                                            </ItemTemplate>
                                                                            <HeaderStyle HorizontalAlign="Center" Width="9%"></HeaderStyle>
                                                                            <ItemStyle HorizontalAlign="Center" Width="9%"></ItemStyle>
                                                                        </asp:TemplateColumn>
                                                                        <asp:TemplateColumn HeaderText="<%$Resources:cms.language, lblAnpham%>">
                                                                            <ItemTemplate>
                                                                                <%#HPCBusinessLogic.UltilFunc.GetTenAnpham(Eval("Languages_ID"))%>
                                                                            </ItemTemplate>
                                                                            <HeaderStyle HorizontalAlign="Center" Width="8%"></HeaderStyle>
                                                                            <ItemStyle HorizontalAlign="Center" Width="8%"></ItemStyle>
                                                                        </asp:TemplateColumn>
                                                                        <asp:TemplateColumn HeaderText="<%$Resources:cms.language, lblGhichu%>">
                                                                            <ItemTemplate>
                                                                                <%# DataBinder.Eval(Container.DataItem, "Comment") %>
                                                                            </ItemTemplate>
                                                                            <HeaderStyle HorizontalAlign="Center" Width="12%"></HeaderStyle>
                                                                            <ItemStyle HorizontalAlign="Left" Width="12%"></ItemStyle>
                                                                        </asp:TemplateColumn>
                                                                    </Columns>
                                                                </asp:DataGrid>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="text-align: left;">
                                                            </td>
                                                            <td style="text-align: right" class="pageNav">
                                                                <cc1:CurrentPage runat="server" ID="CurrentChoxuatban" CssClass="pageNavTotal">
                                                                </cc1:CurrentPage>
                                                                <cc1:Pager runat="server" ID="pages_Choxuatban" OnIndexChanged="pages_Choxuatban_IndexChanged_Editor"></cc1:Pager>
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
                                    <cc2:TabPanel HeaderText="<%$Resources:cms.language, lblDaXB%>" ID="tabDaxuatban" runat="server">
                                        <ContentTemplate>
                                            <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                                <ContentTemplate>
                                                    <table width="100%" cellspacing="2" cellpadding="2" border="0">
                                                        <tr>
                                                            <td align="center">
                                                                <asp:DataGrid ID="DataGrid_Daxuatban" runat="server" Width="100%" AutoGenerateColumns="False"
                                                                    DataKeyField="ID" OnEditCommand="DataGrid_Daxuatban_EditCommandEditor" CssClass="Grid">
                                                                    <ItemStyle CssClass="GridItem"></ItemStyle>
                                                                    <AlternatingItemStyle CssClass="GridAltItem" />
                                                                    <HeaderStyle CssClass="GridHeader"></HeaderStyle>
                                                                    <Columns>
                                                                        <asp:BoundColumn DataField="ID" HeaderText="ID" Visible="False"></asp:BoundColumn>
                                                                        <asp:TemplateColumn HeaderText="Video">
                                                                            <HeaderStyle Width="12%" HorizontalAlign="Center"></HeaderStyle>
                                                                            <ItemStyle Width="12%" HorizontalAlign="Center" />
                                                                            <ItemTemplate>
                                                                                <a href="Javascript:PopupWindowVideo('<%=Global.ApplicationPath%>/Multimedia/ViewVideo.aspx?Menu_ID=<%=Page.Request.QueryString["Menu_ID"]%>&ID=<%# DataBinder.Eval(Container.DataItem, "ID") %>');" />
                                                                                <img src='<%#CommonLib._returnimg(Eval("URL_Images"))%>' style="width: 120px;" border="0"
                                                                                    alt="Xem Video" onmouseover="(window.status=''); return true" style="cursor: pointer;
                                                                                    border: 0" title="Xem Video" />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateColumn>
                                                                        <asp:TemplateColumn HeaderText="<%$Resources:cms.language, lblTieude%>">
                                                                            <HeaderStyle Width="25%" HorizontalAlign="Center"></HeaderStyle>
                                                                            <ItemStyle Width="25%" HorizontalAlign="Left" />
                                                                            <ItemTemplate>
                                                                                <%# DataBinder.Eval(Container.DataItem, "Tittle") %>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateColumn>
                                                                        <asp:TemplateColumn HeaderText="<%$Resources:cms.language, lblChuyenmuc%>">
                                                                            <HeaderStyle Width="13%" HorizontalAlign="Center"></HeaderStyle>
                                                                            <ItemStyle Width="13%" HorizontalAlign="Left"></ItemStyle>
                                                                            <ItemTemplate>
                                                                                <%#HPCBusinessLogic.UltilFunc.GetCategoryName(Eval("Category"))%>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateColumn>
                                                                        <asp:TemplateColumn HeaderText="<%$Resources:cms.language, lblNguoinhap%>">
                                                                            <ItemTemplate>
                                                                                <%#HPCBusinessLogic.UltilFunc.GetUserName(Eval("UserCreated"))%>
                                                                            </ItemTemplate>
                                                                            <HeaderStyle HorizontalAlign="Center" Width="10%"></HeaderStyle>
                                                                            <ItemStyle HorizontalAlign="Center" Width="10%"></ItemStyle>
                                                                        </asp:TemplateColumn>
                                                                        <asp:TemplateColumn HeaderText="<%$Resources:cms.language, lblNgaynhap%>">
                                                                            <ItemTemplate>
                                                                                <%# DataBinder.Eval(Container, "DataItem.DateCreated")!=System.DBNull.Value?Convert.ToDateTime(DataBinder.Eval(Container, "DataItem.DateCreated")).ToString("dd/MM/yyyy HH:mm:ss"):"" %>
                                                                            </ItemTemplate>
                                                                            <HeaderStyle HorizontalAlign="Center" Width="10%"></HeaderStyle>
                                                                            <ItemStyle HorizontalAlign="Center" Width="10%"></ItemStyle>
                                                                        </asp:TemplateColumn>
                                                                        <asp:TemplateColumn HeaderText="<%$Resources:cms.language, lblNguoiXB%>">
                                                                            <ItemTemplate>
                                                                                <%#HPCBusinessLogic.UltilFunc.GetUserName(Eval("UserPublish"))%>
                                                                            </ItemTemplate>
                                                                            <HeaderStyle HorizontalAlign="Center" Width="10%"></HeaderStyle>
                                                                            <ItemStyle HorizontalAlign="Center" Width="10%"></ItemStyle>
                                                                        </asp:TemplateColumn>
                                                                        <asp:TemplateColumn HeaderText="<%$Resources:cms.language, lblNgayXB%>">
                                                                            <ItemTemplate>
                                                                                <%# DataBinder.Eval(Container, "DataItem.DatePublish") != System.DBNull.Value ? Convert.ToDateTime(DataBinder.Eval(Container, "DataItem.DatePublish")).ToString("dd/MM/yyyy HH:mm:ss") : ""%>
                                                                            </ItemTemplate>
                                                                            <HeaderStyle HorizontalAlign="Center" Width="10%"></HeaderStyle>
                                                                            <ItemStyle HorizontalAlign="Center" Width="10%"></ItemStyle>
                                                                        </asp:TemplateColumn>
                                                                        
                                                                        <%--<asp:TemplateColumn HeaderText="Ngày hiệu đính">
                                                                            <ItemTemplate>
                                                                                <%# DataBinder.Eval(Container, "DataItem.DateModify") != System.DBNull.Value ? Convert.ToDateTime(DataBinder.Eval(Container, "DataItem.DateModify")).ToString("dd/MM/yyyy HH:mm:ss") : ""%>
                                                                            </ItemTemplate>
                                                                            <HeaderStyle HorizontalAlign="Center" Width="10%"></HeaderStyle>
                                                                            <ItemStyle HorizontalAlign="Center" Width="10%"></ItemStyle>
                                                                        </asp:TemplateColumn>--%>
                                                                        <asp:TemplateColumn HeaderText="<%$Resources:cms.language, lblAnpham%>">
                                                                            <ItemTemplate>
                                                                                <%#HPCBusinessLogic.UltilFunc.GetTenAnpham(Eval("Languages_ID"))%>
                                                                            </ItemTemplate>
                                                                            <HeaderStyle HorizontalAlign="Center" Width="10%"></HeaderStyle>
                                                                            <ItemStyle HorizontalAlign="Center" Width="10%"></ItemStyle>
                                                                        </asp:TemplateColumn>
                                                                    </Columns>
                                                                </asp:DataGrid>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="text-align: right" class="pageNav">
                                                                <cc1:CurrentPage runat="server" ID="CurrentPage_Xuatban" CssClass="pageNavTotal">
                                                                </cc1:CurrentPage>
                                                                <cc1:Pager runat="server" ID="Pager_Xuatban" OnIndexChanged="Pager_Xuatban_IndexChanged_Editor"></cc1:Pager>
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
                                                        <asp:DataGrid runat="server" ID="dgCategorysCopy" AutoGenerateColumns="false" DataKeyField="ID"
                                                            Width="100%" CssClass="Grid">
                                                            <ItemStyle CssClass="GridItem"></ItemStyle>
                                                            <AlternatingItemStyle CssClass="GridAltItem" />
                                                            <HeaderStyle CssClass="GridHeader"></HeaderStyle>
                                                            <Columns>
                                                                <asp:TemplateColumn>
                                                                    <HeaderStyle HorizontalAlign="Center" Width="6%"></HeaderStyle>
                                                                    <ItemStyle HorizontalAlign="Center" Width="6%"></ItemStyle>
                                                                    <HeaderTemplate>
                                                                        <asp:CheckBox ID="chkAll" onclick="javascript:checkAll_CM(this);" runat="server"
                                                                            ToolTip="Chọn tất cả"></asp:CheckBox>
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:CheckBox runat="server" Text='' ID='optSelect' AutoPostBack="False" Enabled="true">
                                                                        </asp:CheckBox>
                                                                    </ItemTemplate>
                                                                </asp:TemplateColumn>
                                                                <asp:TemplateColumn>
                                                                    <HeaderStyle Width="50%" HorizontalAlign="Center"></HeaderStyle>
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
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
</asp:Content>
