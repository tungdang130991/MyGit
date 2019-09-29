<%@ Page Language="C#" MasterPageFile="~/Masters/ITC.Master" AutoEventWireup="true"
    CodeBehind="ArticleApproveListTB.aspx.cs" Inherits="ToasoanTTXVN.BaoDienTu.ArticleApproveListTB" %>

<%@ Register TagPrefix="nbc" Namespace="HPCComponents.UI" Assembly="HPCComponents" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<%@ Register TagPrefix="cc1" Namespace="HPCComponents.UI.WebControls" Assembly="HPCComponents" %>
<%@ Register TagPrefix="anthem" Namespace="Anthem" Assembly="Anthem" %>
<%@ Import Namespace="HPCComponents" %>
<%@ Import Namespace="HPCBusinessLogic" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script language="Javascript" type="text/javascript">
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);
        function EndRequestHandler(sender, args) {
            if (args.get_error() != undefined) {
                args.set_errorHandled(true);
            }
        }
        function SetInnerProcess(choxuatban, _henXB, _daxuly, _dabixoa) {
            document.getElementById("__tab_ctl00_MainContent_TabContainer1_tabpnltinXuly").innerHTML = choxuatban;
            document.getElementById("__tab_ctl00_MainContent_TabContainer1_TabPanel_HenXB").innerHTML = _henXB;
            document.getElementById("__tab_ctl00_MainContent_TabContainer1_TabPanel1").innerHTML = _daxuly;
            document.getElementById("__tab_ctl00_MainContent_TabContainer1_TabPanelDelete").innerHTML = _dabixoa;
        }
        function checkAll_BaiXoa(objRef) {
            var GridView = document.getElementById('<%=dgr_BaiXoa.ClientID%>');
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
    </script>

    <table border="0" cellpadding="0" width="100%" cellspacing="0">
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
                        <td style="vertical-align: middle">
                            <span class="TitlePanel"><%= CommonLib.ReadXML("titDsTinchoXB") %></span>
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
                <table cellpadding="0" cellspacing="0" border="0" width="100%">
                    <tr>
                        <td>
                            <div class="classSearchHeader">
                                <table width="100%" cellspacing="0" cellpadding="0" border="0">
                                    <tr>
                                        <td style="width: 8%; text-align: right">
                                            <asp:Label ID="lblChuyenmuc" class="Titlelbl" runat="server" 
                                                Text="<%$Resources:cms.language, lblAnpham%>"></asp:Label>&nbsp;
                                        </td>
                                        <td style="width: 15%; text-align: left;">
                                            <anthem:DropDownList AutoCallBack="true" ID="ddlLang" runat="server" Width="180"
                                                CssClass="inputtext" OnSelectedIndexChanged="ddlLang_SelectedIndexChanged">
                                            </anthem:DropDownList>
                                        </td>
                                        <td style="width: 20%; text-align: right">
                                            <asp:Label ID="Label2" class="Titlelbl" runat="server" 
                                                Text="<%$Resources:cms.language, lblChuyenmuc%>"></asp:Label>&nbsp;
                                        </td>
                                        <td style="width: 20%; text-align: left;">
                                            <anthem:DropDownList AutoCallBack="true" ID="ddlCategorysAll" runat="server" Width="220"
                                                CssClass="inputtext">
                                            </anthem:DropDownList>
                                        </td>
                                        <td style="width: 10%; text-align: right">
                                            <asp:Label ID="Label3" class="Titlelbl" runat="server" 
                                                Text="<%$Resources:cms.language, lblTenbaiviet%>"></asp:Label>&nbsp;
                                        </td>
                                        <td style="width: 30%; text-align: left;">
                                            <asp:TextBox ID="txt_tieude" TabIndex="4" Width="90%" runat="server" CssClass="inputtext"
                                                onkeypress="return clickButton(event,'ctl00_MainContent_btnSearch');">
                                            </asp:TextBox>
                                            
                                        </td>
                                         <td style="width: 20%; text-align: center;">
                                            <asp:Button runat="server" ID="btnSearch" CssClass="iconFind" Font-Bold="true" OnClick="btnSearch_Click"
                                                Text="<%$Resources:cms.language, lblTimkiem%>"></asp:Button>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 4px">
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left">
                            <div class="classSearchHeader">
                                <cc2:TabContainer ID="TabContainer1" runat="server" CssClass="ajax__tab_yuitabview-theme"
                                    AutoPostBack="true" ActiveTabIndex="0" OnActiveTabChanged="TabContainer1_ActiveTabChanged">
                                    <cc2:TabPanel HeaderText="<%$Resources:cms.language, lblTinbientap%>" ID="tabpnltinXuly" runat="server">
                                        <ContentTemplate>
                                            <asp:UpdatePanel UpdateMode="Conditional" ID="UpdatePanel1" runat="server">
                                                <ContentTemplate>
                                                    <table border="0" width="100%">
                                                        <tr>
                                                            <td>
                                                                <asp:DataGrid runat="server" ID="grdList" AutoGenerateColumns="false" DataKeyField="News_ID"
                                                                    Width="100%" CssClass="Grid" OnEditCommand="grdList_EditCommand" 
                                                                    OnItemDataBound="grdList_ItemDataBound">
                                                                    <ItemStyle CssClass="GridItem"></ItemStyle>
                                                                    <AlternatingItemStyle CssClass="GridAltItem" />
                                                                    <HeaderStyle CssClass="GridHeader"></HeaderStyle>
                                                                    <Columns>
                                                                        <asp:BoundColumn Visible="False" DataField="News_ID">
                                                                            <HeaderStyle Width="1%"></HeaderStyle>
                                                                        </asp:BoundColumn>
                                                                        <asp:TemplateColumn>
                                                                            <HeaderStyle HorizontalAlign="Center" Width="3%"></HeaderStyle>
                                                                            <ItemStyle HorizontalAlign="Center" Width="3%"></ItemStyle>
                                                                            <HeaderTemplate>
                                                                                <asp:CheckBox ID="chkAll" onclick="javascript:SelectAllCheckboxes(this);" runat="server"
                                                                                    AutoPostBack="false" ToolTip="<%$Resources:cms.language, lblChontatca%>"></asp:CheckBox>
                                                                            </HeaderTemplate>
                                                                            <ItemTemplate>
                                                                                <asp:CheckBox runat="server" Text='' ID='optSelect' AutoPostBack="False"></asp:CheckBox>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateColumn>
                                                                        <asp:TemplateColumn HeaderText="<%$Resources:cms.language, lblAnhdaidien%>">
                                                                            <HeaderStyle HorizontalAlign="Center" Width="7%"></HeaderStyle>
                                                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                            <ItemTemplate>
                                                                                <div class="gallery">
                                                                                    <div class="pictgalery" style="width: 120px;">
                                                                                        <%#UltilFunc.ReturnPath_Images(Eval("Images_Summary"))%>
                                                                                    </div>
                                                                                </div>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateColumn>
                                                                        <asp:TemplateColumn HeaderText="<%$Resources:cms.language, lblTenbaiviet%>">
                                                                            <HeaderStyle HorizontalAlign="Left" Width="22%" />
                                                                            <ItemStyle HorizontalAlign="Left" Width="22%"></ItemStyle>
                                                                            <ItemTemplate>
                                                                                <div class="stringtieudeandsc">
                                                                                    <div class="fontTitle" style="width: 98%;">
                                                                                        <asp:LinkButton CssClass="linkEdit" Text='<%# DataBinder.Eval(Container.DataItem, "News_Tittle" )%>'
                                                                                            Enabled="true" runat="server" ID="linkTittle" CommandName="Edit" CommandArgument="Edit"
                                                                                            ToolTip="<%$Resources:cms.language, lblSuabai%>"></asp:LinkButton>
                                                                                        <%#HPCBusinessLogic.UltilFunc.IsStatusGet(Eval("News_IsImages"),Eval("News_IsVideo"))%>
                                                                                        <%#HPCBusinessLogic.UltilFunc.IsStatusImages(DataBinder.Eval(Container.DataItem, "News_Body").ToString())%>
                                                                                    </div>
                                                                                    <div class="chuthichcss">
                                                                                        <asp:Label ID="lbdesc" runat="server" Text='<%#Eval("News_Summary")%>'></asp:Label>
                                                                                    </div>
                                                                                    <div class="fontTitle" style="width: 100%; text-align: right;">
                                                                                        <asp:Label ID="lbtacgia" runat="server" Text='<%#Eval("News_AuthorName")%>'></asp:Label>
                                                                                    </div>
                                                                                    <div class="fontTitle" style="width: 100%; text-align: left;">
                                                                                        <%# HPCBusinessLogic.UltilFunc.LockedUser(DataBinder.Eval(Container.DataItem, "News_Lock").ToString(), DataBinder.Eval(Container.DataItem, "News_EditorID").ToString(), _user.UserID)%>
                                                                                    </div>
                                                                                </div>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateColumn>
                                                                        <asp:TemplateColumn HeaderText="<%$Resources:cms.language, lblAnpham%>">
                                                                            <HeaderStyle Width="7%" HorizontalAlign="Center"></HeaderStyle>
                                                                            <ItemStyle Width="7%" HorizontalAlign="Center"></ItemStyle>
                                                                            <ItemTemplate>
                                                                                <%#HPCBusinessLogic.UltilFunc.GetTenAnpham(Eval("Lang_ID"))%>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateColumn>
                                                                        <asp:TemplateColumn HeaderText="<%$Resources:cms.language, lblChuyenmuc%>">
                                                                            <HeaderStyle HorizontalAlign="Center" Width="10%"></HeaderStyle>
                                                                            <ItemStyle HorizontalAlign="Left" Width="10%"></ItemStyle>
                                                                            <ItemTemplate>
                                                                                <%#HPCBusinessLogic.UltilFunc.GetCategoryName(Eval("CAT_ID"))%>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateColumn>
                                                                        
                                                                        <asp:TemplateColumn HeaderText="<%$Resources:cms.language, lblNguoinhap%>">
                                                                            <HeaderStyle Width="7%" HorizontalAlign="Center"></HeaderStyle>
                                                                            <ItemStyle Width="7%" HorizontalAlign="Center"></ItemStyle>
                                                                            <ItemTemplate>
                                                                                <%#HPCBusinessLogic.UltilFunc.GetUserName(Eval("News_AuthorID"))%>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateColumn> 
                                                                        <asp:TemplateColumn HeaderText="<%$Resources:cms.language, lblNgaynhap%>">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="ngaytao" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.News_DateCreated")!=System.DBNull.Value?Convert.ToDateTime(DataBinder.Eval(Container, "DataItem.News_DateCreated")).ToString("dd/MM/yyyy HH:mm:ss"):"" %>'>
                                                                                </asp:Label>
                                                                            </ItemTemplate>
                                                                            <HeaderStyle HorizontalAlign="Center" Width="7%"></HeaderStyle>
                                                                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                                        </asp:TemplateColumn>
                                                                        <asp:TemplateColumn HeaderText="<%$Resources:cms.language, lblNguoihieudinh%>">
                                                                            <HeaderStyle HorizontalAlign="Center" Width="7%"></HeaderStyle>
                                                                            <ItemStyle HorizontalAlign="Center" Width="7%"></ItemStyle>
                                                                            <ItemTemplate>
                                                                                <%# HPCBusinessLogic.UltilFunc.GetUserName(Eval("News_AprovedID"))%>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateColumn>
                                                                        <asp:TemplateColumn HeaderText="<%$Resources:cms.language, lblNgayhieudinh%>">
                                                                            <HeaderStyle HorizontalAlign="Center" Width="7%"></HeaderStyle>
                                                                            <ItemStyle HorizontalAlign="Center" Width="7%"></ItemStyle>
                                                                            <ItemTemplate>
                                                                                <%#Eval("News_DateSend") != System.DBNull.Value ? Convert.ToDateTime(Eval("News_DateSend")).ToString("dd/MM hh:mm:ss") : ""%>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateColumn>
                                                                        <asp:TemplateColumn HeaderText="<%$Resources:cms.language, lblXem%>">
                                                                            <HeaderStyle HorizontalAlign="Center" Width="3%"></HeaderStyle>
                                                                            <ItemStyle HorizontalAlign="Center" Width="3%"></ItemStyle>
                                                                            <ItemTemplate>
                                                                                <a target="_blank" href="<%=Global.ApplicationPath%>/View/ViewHistory.aspx?Menu_ID=<%=Page.Request.QueryString["Menu_ID"]%>&ID=<%# DataBinder.Eval(Container.DataItem, "News_ID") %>" />
                                                                                <img src="<%=Global.ApplicationPath%>/Dungchung/Images/view.gif" border="0" alt="Xem"
                                                                                    onmouseover="(window.status=''); return true" style="cursor: pointer;" title="Xem">
                                                                            </ItemTemplate>
                                                                        </asp:TemplateColumn>
                                                                        <asp:TemplateColumn HeaderText="<%$Resources:cms.language, lblDuyetanh%>">
                                                                            <HeaderStyle HorizontalAlign="Center" Width="5%"></HeaderStyle>
                                                                            <ItemStyle HorizontalAlign="Center" Width="5%" CssClass="GridBorderVerSolid"></ItemStyle>
                                                                            <ItemTemplate>
                                                                                <a href="Javascript:open_window_Scroll('<%=Global.ApplicationPath%>/BaoDienTu/ArticleApproveImage.aspx?Menu_ID=<%=Page.Request.QueryString["Menu_ID"]%>&ID=<%# DataBinder.Eval(Container.DataItem, "News_ID") %>',50,500,100,800);" />
                                                                                <img src='../Dungchung/Images/152009212557508.gif' border="0" alt="Xem/ In" border="0"
                                                                                    onmouseover="(window.status=''); return true" style='cursor: pointer;' title="Xem">
                                                                            </ItemTemplate>
                                                                        </asp:TemplateColumn>
                                                                        <asp:TemplateColumn>
                                                                            <HeaderStyle HorizontalAlign="Center" Width="4%"></HeaderStyle>
                                                                            <ItemStyle HorizontalAlign="Center" Width="4%"></ItemStyle>
                                                                            <HeaderTemplate>
                                                                                <img style="cursor: pointer;" src='<%= Global.ApplicationPath%>/Dungchung/Images/doc.gif'
                                                                                    border="0" title="Download nội dung bài viết" alt='Download nội dung bài viết'>
                                                                            </HeaderTemplate>
                                                                            <ItemTemplate>
                                                                                <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Dungchung/Images/doc.gif"
                                                                                    ToolTip="Export word" CommandName="Edit" CommandArgument="DownLoadAlias" />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateColumn>
                                                                    </Columns>
                                                                </asp:DataGrid>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <table border="0" width="100%">
                                                                    <tr>
                                                                        <td style="width: 40%">
                                                                            <asp:Button runat="server" Font-Bold="true" ID="Link_GuiXuatBan" CssClass="iconPub"
                                                                                OnClick="Link_GuiXuatBan_Click" Text="<%$Resources:cms.language, lblGui%>"></asp:Button>
                                                                            <asp:Button runat="server" Font-Bold="true" ID="Link_TraLai" CssClass="iconReply"
                                                                                OnClick="Link_TraLai_Click" Text="<%$Resources:cms.language, lblTralai%>"></asp:Button>
                                                                            <asp:Button runat="server" Font-Bold="true" ID="Link_Delete" CssClass="iconDel" OnClick="Link_Delete_Click"
                                                                                Text="<%$Resources:cms.language, lblXoa%>"></asp:Button>
                                                                        </td>
                                                                        <td style="text-align: right;" class="pageNav">
                                                                            <cc1:CurrentPage runat="server" ID="curentPages" CssClass="pageNavTotal"></cc1:CurrentPage>
                                                                            <cc1:Pager runat="server" ID="pages" OnIndexChanged="pages_IndexChanged" />
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
                                    <cc2:TabPanel HeaderText="<%$Resources:cms.language, lblTintralai%>" ID="TabPanel_HenXB" runat="server">
                                        <ContentTemplate>
                                            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                                <ContentTemplate>
                                                    <table border="0" width="100%">
                                                        <tr>
                                                            <td>
                                                                <asp:DataGrid runat="server" ID="dgrTraLai" AutoGenerateColumns="false" DataKeyField="News_ID"
                                                                    Width="100%" CssClass="Grid" OnEditCommand="dgrTraLai_EditCommand" 
                                                                    OnItemDataBound="dgrTraLai_ItemDataBound">
                                                                    <ItemStyle CssClass="GridItem"></ItemStyle>
                                                                    <AlternatingItemStyle CssClass="GridAltItem" />
                                                                    <HeaderStyle CssClass="GridHeader"></HeaderStyle>
                                                                    <Columns>
                                                                        <asp:BoundColumn Visible="False" DataField="News_ID">
                                                                            <HeaderStyle Width="1%"></HeaderStyle>
                                                                        </asp:BoundColumn>
                                                                        <asp:TemplateColumn>
                                                                            <HeaderStyle HorizontalAlign="Center" Width="3%"></HeaderStyle>
                                                                            <ItemStyle HorizontalAlign="Center" Width="3%"></ItemStyle>
                                                                            <HeaderTemplate>
                                                                                <asp:CheckBox ID="chkAll" onclick="javascript:SelectAllCheckboxes(this);" runat="server"
                                                                                    AutoPostBack="false" ToolTip="<%$Resources:cms.language, lblChontatca%>"></asp:CheckBox>
                                                                            </HeaderTemplate>
                                                                            <ItemTemplate>
                                                                                <asp:CheckBox runat="server" Text='' ID='optSelect' AutoPostBack="False"></asp:CheckBox>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateColumn>
                                                                        <asp:TemplateColumn HeaderText="<%$Resources:cms.language, lblAnhdaidien%>">
                                                                            <HeaderStyle HorizontalAlign="Center" Width="8%"></HeaderStyle>
                                                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                            <ItemTemplate>
                                                                                <div class="gallery">
                                                                                    <div class="pictgalery" style="width: 120px;">
                                                                                        <%#UltilFunc.ReturnPath_Images(Eval("Images_Summary"))%>
                                                                                    </div>
                                                                                </div>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateColumn>
                                                                        <asp:TemplateColumn HeaderText="<%$Resources:cms.language, lblTenbaiviet%>">
                                                                            <HeaderStyle HorizontalAlign="Left" Width="20%" />
                                                                            <ItemStyle HorizontalAlign="Left" Width="20%"></ItemStyle>
                                                                            <ItemTemplate>
                                                                                <div class="stringtieudeandsc">
                                                                                    <div class="fontTitle" style="width: 98%;">
                                                                                        <asp:LinkButton CssClass="linkEdit" Text='<%# DataBinder.Eval(Container.DataItem, "News_Tittle" )%>'
                                                                                            Enabled="true" runat="server" ID="linkTittle" CommandName="Edit" CommandArgument="Edit"
                                                                                            ToolTip="<%$Resources:cms.language, lblSuabai%>"></asp:LinkButton>
                                                                                        <%#HPCBusinessLogic.UltilFunc.IsStatusGet(Eval("News_IsImages"),Eval("News_IsVideo"))%>
                                                                                        <%#HPCBusinessLogic.UltilFunc.IsStatusImages(DataBinder.Eval(Container.DataItem, "News_Body").ToString())%>
                                                                                    </div>
                                                                                    <div class="chuthichcss">
                                                                                        <asp:Label ID="lbdesc" runat="server" Text='<%#Eval("News_Summary")%>'></asp:Label>
                                                                                    </div>
                                                                                    <div class="fontTitle" style="width: 100%; text-align: right;">
                                                                                        <asp:Label ID="lbtacgia" runat="server" Text='<%#Eval("News_AuthorName")%>'></asp:Label>
                                                                                    </div>
                                                                                    <div class="fontTitle" style="width: 100%; text-align: left;">
                                                                                        <%# HPCBusinessLogic.UltilFunc.LockedUser(DataBinder.Eval(Container.DataItem, "News_Lock").ToString(), DataBinder.Eval(Container.DataItem, "News_EditorID").ToString(), _user.UserID)%>
                                                                                    </div>
                                                                                </div>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateColumn>
                                                                        <asp:TemplateColumn HeaderText="<%$Resources:cms.language, lblAnpham%>">
                                                                            <HeaderStyle Width="9%" HorizontalAlign="Center"></HeaderStyle>
                                                                            <ItemStyle Width="9%" HorizontalAlign="Center"></ItemStyle>
                                                                            <ItemTemplate>
                                                                                <%#HPCBusinessLogic.UltilFunc.GetTenAnpham(Eval("Lang_ID"))%>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateColumn>
                                                                        <asp:TemplateColumn HeaderText="<%$Resources:cms.language, lblChuyenmuc%>">
                                                                            <HeaderStyle HorizontalAlign="Center" Width="10%"></HeaderStyle>
                                                                            <ItemStyle HorizontalAlign="Left" Width="10%"></ItemStyle>
                                                                            <ItemTemplate>
                                                                                <%#HPCBusinessLogic.UltilFunc.GetCategoryName(Eval("CAT_ID"))%>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateColumn>
                                                                        <asp:TemplateColumn HeaderText="<%$Resources:cms.language, lblNguoinhap%>">
                                                                            <HeaderStyle Width="7%" HorizontalAlign="Center"></HeaderStyle>
                                                                            <ItemStyle Width="7%" HorizontalAlign="Center"></ItemStyle>
                                                                            <ItemTemplate>
                                                                                <%#HPCBusinessLogic.UltilFunc.GetUserName(Eval("News_AuthorID"))%>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateColumn> 
                                                                        <asp:TemplateColumn HeaderText="<%$Resources:cms.language, lblNgaynhap%>">
                                                                            <ItemTemplate>
                                                                                <%# DataBinder.Eval(Container, "DataItem.News_DateCreated") != System.DBNull.Value ? Convert.ToDateTime(DataBinder.Eval(Container, "DataItem.News_DateCreated")).ToString("dd/MM/yyyy HH:mm:ss") : ""%>
                                                                            </ItemTemplate>
                                                                            <HeaderStyle HorizontalAlign="Center" Width="9%"></HeaderStyle>
                                                                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                                        </asp:TemplateColumn>
                                                                        <asp:TemplateColumn HeaderText="<%$Resources:cms.language, lblNguoitra%>">
                                                                            <HeaderStyle HorizontalAlign="Center" Width="7%"></HeaderStyle>
                                                                            <ItemStyle HorizontalAlign="Center" Width="7%"></ItemStyle>
                                                                            <ItemTemplate>
                                                                                <%# HPCBusinessLogic.UltilFunc.GetUserName(Eval("News_AprovedID"))%>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateColumn>
                                                                        <asp:TemplateColumn HeaderText="<%$Resources:cms.language, lblNgaytra%>">
                                                                            <HeaderStyle HorizontalAlign="Center" Width="9%"></HeaderStyle>
                                                                            <ItemStyle HorizontalAlign="Center" Width="9%"></ItemStyle>
                                                                            <ItemTemplate>
                                                                                <%#Eval("News_DateSend") != System.DBNull.Value ? Convert.ToDateTime(Eval("News_DateSend")).ToString("dd/MM hh:mm:ss") : ""%>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateColumn>
                                                                        <asp:TemplateColumn HeaderText="<%$Resources:cms.language, lblXem%>">
                                                                            <HeaderStyle HorizontalAlign="Center" Width="3%"></HeaderStyle>
                                                                            <ItemStyle HorizontalAlign="Center" Width="3%"></ItemStyle>
                                                                            <ItemTemplate>
                                                                                <a target="_blank" href="<%=Global.ApplicationPath%>/View/ViewHistory.aspx?Menu_ID=<%=Page.Request.QueryString["Menu_ID"]%>&ID=<%# DataBinder.Eval(Container.DataItem, "News_ID") %>" />
                                                                                <img src="<%=Global.ApplicationPath%>/Dungchung/Images/view.gif" border="0" alt="Xem"
                                                                                    onmouseover="(window.status=''); return true" style="cursor: pointer;" title="Xem">
                                                                            </ItemTemplate>
                                                                        </asp:TemplateColumn>
                                                                        <asp:TemplateColumn>
                                                                            <HeaderStyle HorizontalAlign="Center" Width="5%"></HeaderStyle>
                                                                            <ItemStyle HorizontalAlign="Center" Width="5%"></ItemStyle>
                                                                            <HeaderTemplate>
                                                                                <img style="cursor: pointer;" src='<%= Global.ApplicationPath%>/Dungchung/Images/doc.gif'
                                                                                    border="0" title="Download nội dung bài viết" alt='Download nội dung bài viết'>
                                                                            </HeaderTemplate>
                                                                            <ItemTemplate>
                                                                                <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Dungchung/Images/doc.gif"
                                                                                    ToolTip="Export word" CommandName="Edit" CommandArgument="DownLoadAlias" />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateColumn>
                                                                    </Columns>
                                                                </asp:DataGrid>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <table border="0" width="100%">
                                                                    <tr>
                                                                        <td style="width: 40%">
                                                                            <asp:Button runat="server" Font-Bold="true" ID="LinkButton1" CssClass="iconPub" 
                                                                                OnClick="Link_GuiXuatBan_Click" Text="<%$Resources:cms.language, lblGui%>"></asp:Button>
                                                                            <asp:Button runat="server" Font-Bold="true" ID="LinkButton2" CssClass="iconReply"
                                                                                OnClick="Link_TraLai_Click" Text="<%$Resources:cms.language, lblTralai%>"></asp:Button>
                                                                        </td>
                                                                        <td style="text-align: right" class="pageNav">
                                                                            <cc1:CurrentPage runat="server" ID="CurrentPage_TraLai" CssClass="pageNavTotal"></cc1:CurrentPage>
                                                                            <cc1:Pager runat="server" ID="Pager_TraLai" OnIndexChanged="Pager_TraLai_IndexChanged" />
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
                                    <cc2:TabPanel HeaderText="<%$Resources:cms.language, lblTindagui%>" ID="TabPanel1" runat="server">
                                        <ContentTemplate>
                                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                <ContentTemplate>
                                                    <table border="0" width="100%">
                                                        <tr>
                                                            <td>
                                                                <asp:DataGrid runat="server" ID="dgBaiDaXyLy" OnItemDataBound="grdList_ItemDataBound"
                                                                    AutoGenerateColumns="false" DataKeyField="News_ID" Width="100%" CssClass="Grid">
                                                                    <ItemStyle CssClass="GridItem"></ItemStyle>
                                                                    <AlternatingItemStyle CssClass="GridAltItem" />
                                                                    <HeaderStyle CssClass="GridHeader"></HeaderStyle>
                                                                    <Columns>
                                                                        <asp:BoundColumn Visible="False" DataField="News_ID">
                                                                            <HeaderStyle Width="1%"></HeaderStyle>
                                                                        </asp:BoundColumn>
                                                                        <asp:TemplateColumn HeaderText="<%$Resources:cms.language, lblAnhdaidien%>">
                                                                            <HeaderStyle HorizontalAlign="Center" Width="10%"></HeaderStyle>
                                                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                            <ItemTemplate>
                                                                                <div class="gallery">
                                                                                    <div class="pictgalery" style="width: 120px;">
                                                                                        <%#UltilFunc.ReturnPath_Images(Eval("Images_Summary"))%>
                                                                                    </div>
                                                                                </div>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateColumn>
                                                                        <asp:TemplateColumn HeaderText="<%$Resources:cms.language, lblTenbaiviet%>">
                                                                            <HeaderStyle HorizontalAlign="Left" Width="25%"></HeaderStyle>
                                                                            <ItemStyle HorizontalAlign="Left" Width="25%"></ItemStyle>
                                                                            <ItemTemplate>
                                                                                <div class="stringtieudeandsc">
                                                                                    <div class="fontTitle" style="width: 90%;">
                                                                                        <asp:Label ID="Label1" runat="server" Text='<%#Eval("News_Tittle")%>'></asp:Label>
                                                                                    </div>
                                                                                    <div class="chuthichcss">
                                                                                        <asp:Label ID="lbdesc" runat="server" Text='<%#Eval("News_Summary")%>'></asp:Label>
                                                                                    </div>
                                                                                    <div class="fontTitle" style="width: 100%; text-align: right;">
                                                                                        <asp:Label ID="lbtacgia" runat="server" Text='<%#Eval("News_AuthorName")%>'></asp:Label>
                                                                                    </div>
                                                                                </div>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateColumn>
                                                                        <asp:TemplateColumn HeaderText="<%$Resources:cms.language, lblAnpham%>">
                                                                            <HeaderStyle Width="9%" HorizontalAlign="Center"></HeaderStyle>
                                                                            <ItemStyle Width="9%" HorizontalAlign="Center"></ItemStyle>
                                                                            <ItemTemplate>
                                                                                <%#HPCBusinessLogic.UltilFunc.GetTenAnpham(Eval("Lang_ID"))%>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateColumn>
                                                                        <asp:TemplateColumn HeaderText="<%$Resources:cms.language, lblChuyenmuc%>">
                                                                            <HeaderStyle HorizontalAlign="Center" Width="10%"></HeaderStyle>
                                                                            <ItemStyle HorizontalAlign="Left" Width="10%"></ItemStyle>
                                                                            <ItemTemplate>
                                                                                <%#HPCBusinessLogic.UltilFunc.GetCategoryName(Eval("CAT_ID"))%>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateColumn>
                                                                        <asp:TemplateColumn HeaderText="<%$Resources:cms.language, lblNguoinhap%>">
                                                                            <HeaderStyle Width="7%" HorizontalAlign="Center"></HeaderStyle>
                                                                            <ItemStyle Width="7%" HorizontalAlign="Center"></ItemStyle>
                                                                            <ItemTemplate>
                                                                                <%#HPCBusinessLogic.UltilFunc.GetUserName(Eval("News_AuthorID"))%>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateColumn> 
                                                                        <asp:TemplateColumn HeaderText="<%$Resources:cms.language, lblNgaynhap%>">
                                                                            <ItemTemplate>
                                                                                <%# DataBinder.Eval(Container, "DataItem.News_DateCreated") != System.DBNull.Value ? Convert.ToDateTime(DataBinder.Eval(Container, "DataItem.News_DateCreated")).ToString("dd/MM/yyyy HH:mm:ss") : ""%>
                                                                            </ItemTemplate>
                                                                            <HeaderStyle HorizontalAlign="Center" Width="10%"></HeaderStyle>
                                                                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                                        </asp:TemplateColumn>
                                                                        <asp:TemplateColumn HeaderText="<%$Resources:cms.language, lblNguoigui%>">
                                                                            <HeaderStyle HorizontalAlign="Center" Width="7%"></HeaderStyle>
                                                                            <ItemStyle HorizontalAlign="Center" Width="7%"></ItemStyle>
                                                                            <ItemTemplate>
                                                                                <%#HPCBusinessLogic.UltilFunc.GetUserName(Eval("News_EditorID"))%>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateColumn>
                                                                        <asp:TemplateColumn HeaderText="<%$Resources:cms.language, lblNgaygui%>">
                                                                            <HeaderStyle HorizontalAlign="Center" Width="10%"></HeaderStyle>
                                                                            <ItemStyle HorizontalAlign="Center" Width="10%"></ItemStyle>
                                                                            <ItemTemplate>
                                                                                <%#Eval("News_DateEdit") != System.DBNull.Value ? Convert.ToDateTime(Eval("News_DateEdit")).ToString("dd/MM hh:mm:ss") : ""%>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateColumn>
                                                                        <asp:TemplateColumn HeaderText="<%$Resources:cms.language, lblTrangthai%>">
                                                                            <HeaderStyle Width="10%" HorizontalAlign="Center"></HeaderStyle>
                                                                            <ItemStyle Width="10%" HorizontalAlign="Center"></ItemStyle>
                                                                            <ItemTemplate>
                                                                                <%#HPCComponents.Global.GetStatusT_NewsFrom_T_version(Eval("News_ID"))%>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateColumn>
                                                                        <asp:TemplateColumn HeaderText="<%$Resources:cms.language, lblXem%>">
                                                                            <HeaderStyle HorizontalAlign="Center" Width="3%"></HeaderStyle>
                                                                            <ItemStyle HorizontalAlign="Center" Width="3%"></ItemStyle>
                                                                            <ItemTemplate>
                                                                                <a target="_blank" href="<%=Global.ApplicationPath%>/View/ViewDetails.aspx?Menu_ID=<%=Page.Request.QueryString["Menu_ID"]%>&ID=<%# DataBinder.Eval(Container.DataItem, "ID") %>&Old_id=1" />
                                                                                <img src="<%=Global.ApplicationPath%>/Dungchung/Images/view.gif" border="0" alt="Xem"
                                                                                    onmouseover="(window.status=''); return true" style="cursor: pointer;">
                                                                            </ItemTemplate>
                                                                        </asp:TemplateColumn>
                                                                    </Columns>
                                                                </asp:DataGrid>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <table border="0" width="100%">
                                                                    <tr>
                                                                        <td>
                                                                        </td>
                                                                        <td style="text-align: right" class="pageNav">
                                                                            <cc1:CurrentPage runat="server" ID="CurrentPageTwo" CssClass="pageNavTotal"></cc1:CurrentPage>
                                                                            &nbsp;<cc1:Pager runat="server" ID="PagerTwo" OnIndexChanged="PagerTwo_IndexChanged" />
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
                                    <cc2:TabPanel HeaderText="<%$Resources:cms.language, lblTindaxoa%>" ID="TabPanelDelete" runat="server">
                                        <ContentTemplate>
                                            <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                                <ContentTemplate>
                                                    <table border="0" width="100%">
                                                        <tr>
                                                            <td>
                                                                <asp:DataGrid runat="server" ID="dgr_BaiXoa" AutoGenerateColumns="false" DataKeyField="News_ID"
                                                                    Width="100%" CssClass="Grid" OnEditCommand="dgr_BaiXoa_EditCommand" OnItemDataBound="dgr_BaiXoa_ItemDataBound">
                                                                    <ItemStyle CssClass="GridItem"></ItemStyle>
                                                                    <AlternatingItemStyle CssClass="GridAltItem" />
                                                                    <HeaderStyle CssClass="GridHeader"></HeaderStyle>
                                                                    <Columns>
                                                                        <asp:BoundColumn Visible="False" DataField="News_ID">
                                                                            <HeaderStyle Width="1%"></HeaderStyle>
                                                                        </asp:BoundColumn>
                                                                        <asp:TemplateColumn>
                                                                            <HeaderStyle HorizontalAlign="Center" Width="3%"></HeaderStyle>
                                                                            <ItemStyle HorizontalAlign="Center" Width="3%"></ItemStyle>
                                                                            <HeaderTemplate>
                                                                                <asp:CheckBox ID="chkAll" onclick="javascript:checkAll_BaiXoa(this);" runat="server"
                                                                                    AutoPostBack="false" ToolTip="<%$Resources:cms.language, lblChontatca%>"></asp:CheckBox>
                                                                            </HeaderTemplate>
                                                                            <ItemTemplate>
                                                                                <asp:CheckBox runat="server" Text='' ID='optSelect' AutoPostBack="False"></asp:CheckBox>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateColumn>
                                                                        <asp:TemplateColumn HeaderText="<%$Resources:cms.language, lblXem%>">
                                                                            <HeaderStyle HorizontalAlign="Center" Width="8%"></HeaderStyle>
                                                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                            <ItemTemplate>
                                                                                <div class="gallery">
                                                                                    <div class="pictgalery" style="width: 120px;">
                                                                                        <%#UltilFunc.ReturnPath_Images(Eval("Images_Summary"))%>
                                                                                    </div>
                                                                                </div>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateColumn>
                                                                        <asp:TemplateColumn HeaderText="<%$Resources:cms.language, lblTenbaiviet%>">
                                                                            <HeaderStyle HorizontalAlign="Left" Width="23%" />
                                                                            <ItemStyle HorizontalAlign="Left" Width="23%"></ItemStyle>
                                                                            <ItemTemplate>
                                                                                <div class="stringtieudeandsc">
                                                                                    <div class="fontTitle" style="width: 98%;">
                                                                                        <asp:LinkButton CssClass="linkEdit" Text='<%# DataBinder.Eval(Container.DataItem, "News_Tittle" )%>'
                                                                                            Enabled="true" runat="server" ID="linkTittle" CommandName="Edit" CommandArgument="Edit"
                                                                                            ToolTip="<%$Resources:cms.language, lblSuabai%>"></asp:LinkButton>
                                                                                        <%#HPCBusinessLogic.UltilFunc.IsStatusGet(Eval("News_IsImages"),Eval("News_IsVideo"))%>
                                                                                        <%#HPCBusinessLogic.UltilFunc.IsStatusImages(DataBinder.Eval(Container.DataItem, "News_Body").ToString())%>
                                                                                    </div>
                                                                                    <div class="chuthichcss">
                                                                                        <asp:Label ID="lbdesc" runat="server" Text='<%#Eval("News_Summary")%>'></asp:Label>
                                                                                    </div>
                                                                                    <div class="fontTitle" style="width: 100%; text-align: right;">
                                                                                        <asp:Label ID="lbtacgia" runat="server" Text='<%#Eval("News_AuthorName")%>'></asp:Label>
                                                                                    </div>
                                                                                </div>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateColumn>
                                                                        <asp:TemplateColumn HeaderText="<%$Resources:cms.language, lblAnpham%>">
                                                                            <HeaderStyle Width="8%" HorizontalAlign="Center"></HeaderStyle>
                                                                            <ItemStyle Width="8%" HorizontalAlign="Center"></ItemStyle>
                                                                            <ItemTemplate>
                                                                                <%#HPCBusinessLogic.UltilFunc.GetTenAnpham(Eval("Lang_ID"))%>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateColumn>
                                                                        <asp:TemplateColumn HeaderText="<%$Resources:cms.language, lblChuyenmuc%>">
                                                                            <HeaderStyle HorizontalAlign="Center" Width="10%"></HeaderStyle>
                                                                            <ItemStyle HorizontalAlign="Left" Width="10%"></ItemStyle>
                                                                            <ItemTemplate>
                                                                                <%#HPCBusinessLogic.UltilFunc.GetCategoryName(Eval("CAT_ID"))%>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateColumn>
                                                                        <asp:TemplateColumn HeaderText="<%$Resources:cms.language, lblNguoinhap%>">
                                                                            <HeaderStyle Width="7%" HorizontalAlign="Center"></HeaderStyle>
                                                                            <ItemStyle Width="7%" HorizontalAlign="Center"></ItemStyle>
                                                                            <ItemTemplate>
                                                                                <%#HPCBusinessLogic.UltilFunc.GetUserName(Eval("News_AuthorID"))%>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateColumn> 
                                                                        <asp:TemplateColumn HeaderText="<%$Resources:cms.language, lblNgaynhap%>">
                                                                            <ItemTemplate>
                                                                                <%# DataBinder.Eval(Container, "DataItem.News_DateCreated") != System.DBNull.Value ? Convert.ToDateTime(DataBinder.Eval(Container, "DataItem.News_DateCreated")).ToString("dd/MM/yyyy HH:mm:ss") : ""%>
                                                                            </ItemTemplate>
                                                                            <HeaderStyle HorizontalAlign="Center" Width="8%"></HeaderStyle>
                                                                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                                        </asp:TemplateColumn>
                                                                        <asp:TemplateColumn HeaderText="<%$Resources:cms.language, lblNguoixoa%>">
                                                                            <HeaderStyle HorizontalAlign="Center" Width="7%"></HeaderStyle>
                                                                            <ItemStyle HorizontalAlign="Center" Width="7%"></ItemStyle>
                                                                            <ItemTemplate>
                                                                                <%# HPCBusinessLogic.UltilFunc.GetUserName(Eval("News_EditorID"))%>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateColumn>
                                                                        <asp:TemplateColumn HeaderText="<%$Resources:cms.language, lblNgayxoa%>">
                                                                            <HeaderStyle HorizontalAlign="Center" Width="8%"></HeaderStyle>
                                                                            <ItemStyle HorizontalAlign="Center" Width="8%"></ItemStyle>
                                                                            <ItemTemplate>
                                                                                <%#Eval("News_DateEdit") != System.DBNull.Value ? Convert.ToDateTime(Eval("News_DateEdit")).ToString("dd/MM hh:mm:ss") : ""%>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateColumn>
                                                                        <asp:TemplateColumn HeaderText="<%$Resources:cms.language, lblDuyetanh%>">
                                                                            <HeaderStyle HorizontalAlign="Center" Width="4%"></HeaderStyle>
                                                                            <ItemStyle HorizontalAlign="Center" Width="4%" CssClass="GridBorderVerSolid"></ItemStyle>
                                                                            <ItemTemplate>
                                                                                <a href="Javascript:open_window_Scroll('<%=Global.ApplicationPath%>/BaoDienTu/ArticleApproveImage.aspx?Menu_ID=<%=Page.Request.QueryString["Menu_ID"]%>&ID=<%# DataBinder.Eval(Container.DataItem, "News_ID") %>',50,500,100,800);" />
                                                                                <img src='../Dungchung/Images/152009212557508.gif' border="0" alt="Xem/ In" border="0"
                                                                                    onmouseover="(window.status=''); return true" style='cursor: pointer;' title="Xem">
                                                                            </ItemTemplate>
                                                                        </asp:TemplateColumn>
                                                                        <asp:TemplateColumn HeaderText="<%$Resources:cms.language, lblXem%>">
                                                                            <HeaderStyle HorizontalAlign="Center" Width="3%"></HeaderStyle>
                                                                            <ItemStyle HorizontalAlign="Center" Width="3%"></ItemStyle>
                                                                            <ItemTemplate>
                                                                                <a target="_blank" href="<%=Global.ApplicationPath%>/View/ViewHistory.aspx?Menu_ID=<%=Page.Request.QueryString["Menu_ID"]%>&ID=<%# DataBinder.Eval(Container.DataItem, "News_ID") %>" />
                                                                                <img src="<%=Global.ApplicationPath%>/Dungchung/Images/view.gif" border="0" alt="Xem"
                                                                                    onmouseover="(window.status=''); return true" style="cursor: pointer;" title="Xem">
                                                                            </ItemTemplate>
                                                                        </asp:TemplateColumn>
                                                                    </Columns>
                                                                </asp:DataGrid>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <table border="0" width="100%">
                                                                    <tr>
                                                                        <td style="width: 50%">
                                                                            <asp:Button runat="server" ID="btnGuiDuyet" CssClass="iconPub" CausesValidation="false"
                                                                                OnClick="Link_GuiXuatBan_Click" Text="<%$Resources:cms.language, lblGui%>" Font-Bold="true"></asp:Button>
                                                                            <asp:Button runat="server" ID="btnTraLai" CssClass="iconReply" CausesValidation="false"
                                                                                OnClick="Link_TraLai_Click" Text="<%$Resources:cms.language, lblTralai%>" Font-Bold="true"></asp:Button>
                                                                            <!--<asp:Button runat="server" ID="BtnXoa" CausesValidation="false" CssClass="iconDel"
                                                                                Text="<%$Resources:cms.language, lblXoa%>" OnClick="Link_Delete_Click">
                                                                            </asp:Button>-->
                                                                        </td>
                                                                        <td class="pageNav" style="text-align: right">
                                                                            <cc1:CurrentPage runat="server" ID="CurrentPageBaixoa" CssClass="pageNavTotal"></cc1:CurrentPage>
                                                                            <cc1:Pager runat="server" ID="pageBaixoa" OnIndexChanged="pages_IndexChanged_Baixoa"></cc1:Pager>
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
