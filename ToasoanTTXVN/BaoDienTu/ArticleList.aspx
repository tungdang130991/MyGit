<%@ Page Language="C#" MasterPageFile="~/Masters/ITC.Master" AutoEventWireup="true"
    CodeBehind="ArticleList.aspx.cs" Inherits="HPCApplication.Article.ArticleList" %>

<%@ Register TagPrefix="anthem" Namespace="Anthem" Assembly="Anthem" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<%@ Register TagPrefix="cc1" Namespace="HPCComponents.UI.WebControls" Assembly="HPCComponents" %>
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
        function SetInnerProcess(baimoi, tralai, Published, _dabixoa) {
            document.getElementById("__tab_ctl00_MainContent_TabContainer1_tabpnltinXuly").innerHTML = baimoi;
            document.getElementById("__tab_ctl00_MainContent_TabContainer1_TabPanel1").innerHTML = tralai;
            document.getElementById("__tab_ctl00_MainContent_TabContainer1_TabPanel2").innerHTML = Published;
            document.getElementById("__tab_ctl00_MainContent_TabContainer1_TabPanelDelete").innerHTML = _dabixoa;
        }
        function checkAll_One(objRef) {
            var GridView = document.getElementById('<%=dgr_tintuc1.ClientID%>');
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
        function checkAll_Two(objRef) {
            var GridView = document.getElementById('<%=dgr_tintuc2.ClientID%>');
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
                            <img alt="" src="../Dungchung/Images/Icons/to-do-list-cheked-all-icon.png" width="16px"
                                height="16px" />
                        </td>
                        <td style="vertical-align: middle;">
                            <span class="TitlePanel">
                                <%= CommonLib.ReadXML("titDsTinbai") %></span><br />
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
                <table cellpadding="2" cellspacing="2" border="0" width="100%">
                    <tr>
                        <td style="text-align: right">
                            <div class="classSearchHeader">
                                <table width="100%" cellspacing="0" cellpadding="0" border="0">
                                    <tr>
                                        <td style="width: 8%; text-align: right">
                                            <asp:Label ID="lblAnpham" class="Titlelbl" runat="server" Text="<%$Resources:cms.language, lblAnpham%>"></asp:Label>&nbsp;
                                        </td>
                                        <td style="width: 15%; text-align: left;">
                                            <anthem:DropDownList AutoCallBack="true" ID="cboNgonNgu" runat="server" Width="150"
                                                CssClass="inputtext" OnSelectedIndexChanged="cbo_lanquage_SelectedIndexChanged"
                                                TabIndex="2">
                                            </anthem:DropDownList>
                                        </td>
                                        <td style="width: 8%; text-align: right">
                                            <asp:Label ID="lblChuyenmuc" class="Titlelbl" runat="server" Text="<%$Resources:cms.language, lblChuyenmuc%>"></asp:Label>&nbsp;
                                        </td>
                                        <td style="width: 20%; text-align: left;">
                                            <anthem:DropDownList AutoCallBack="true" ID="cbo_chuyenmuc" runat="server" Width="280"
                                                CssClass="inputtext" TabIndex="3">
                                            </anthem:DropDownList>
                                        </td>
                                        <td style="width: 10%; text-align: right">
                                            <asp:Label ID="Label2" class="Titlelbl" runat="server" Text="<%$Resources:cms.language, lblTenbaiviet%>"></asp:Label>&nbsp;
                                        </td>
                                        <td style="width: 20%; text-align: left;">
                                            <asp:TextBox ID="txt_tieude" TabIndex="4" Width="250px" runat="server" CssClass="inputtext"
                                                onkeypress="return clickButton(event,'ctl00_MainContent_cmdSeek');">
                                            </asp:TextBox>
                                        </td>
                                        <td style="width: 25%; text-align: center;">
                                            <asp:Button runat="server" ID="cmdSeek" CssClass="iconFind" OnClick="cmdSeek_Click"
                                                Text="<%$Resources:cms.language, lblTimkiem%>"></asp:Button>
                                            <asp:Button runat="server" ID="LinkAddNewsOne" CssClass="iconAddNew" OnClick="cmdAdd_Click"
                                                Font-Bold="true" Text="<%$Resources:cms.language, lblThemmoi%>"></asp:Button>
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
                        <td>
                            <div class="classSearchHeader">
                                <table width="100%" cellspacing="0" cellpadding="0" border="0">
                                    <tr>
                                        <td align="left">
                                            <cc2:TabContainer ID="TabContainer1" runat="server" CssClass="ajax__tab_yuitabview-theme"
                                                AutoPostBack="true" ActiveTabIndex="0" OnActiveTabChanged="TabContainer1_ActiveTabChanged">
                                                <cc2:TabPanel HeaderText="<%$Resources:cms.language, lblTinmoi%>" ID="tabpnltinXuly"
                                                    runat="server">
                                                    <ContentTemplate>
                                                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                            <ContentTemplate>
                                                                <table style="width: 100%" cellpadding="0" cellspacing="0">
                                                                    <tr>
                                                                        <td>
                                                                            <asp:DataGrid ID="dgr_tintuc1" runat="server" Width="100%" BorderStyle="None" AutoGenerateColumns="False"
                                                                                DataKeyField="News_ID" CssClass="Grid" OnEditCommand="dgData_EditCommand" OnItemDataBound="dgData_ItemDataBound">
                                                                                <ItemStyle CssClass="GridItem"></ItemStyle>
                                                                                <AlternatingItemStyle CssClass="GridAltItem" />
                                                                                <HeaderStyle CssClass="GridHeader"></HeaderStyle>
                                                                                <Columns>
                                                                                    <asp:TemplateColumn>
                                                                                        <HeaderStyle HorizontalAlign="Center" Width="5%"></HeaderStyle>
                                                                                        <ItemStyle HorizontalAlign="Center" Width="5%"></ItemStyle>
                                                                                        <HeaderTemplate>
                                                                                            <asp:CheckBox ID="chkAll" onclick="javascript:checkAll_One(this);" runat="server"
                                                                                                ToolTip="<%$Resources:cms.language, lblChontatca%>"></asp:CheckBox>
                                                                                        </HeaderTemplate>
                                                                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                                                        <ItemTemplate>
                                                                                            <asp:CheckBox runat="server" Text='' ID='optSelect' AutoPostBack="False"></asp:CheckBox>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                    <asp:BoundColumn DataField="News_ID" HeaderText="News_ID" Visible="False"></asp:BoundColumn>
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
                                                                                        <HeaderStyle HorizontalAlign="Left" Width="40%"></HeaderStyle>
                                                                                        <ItemStyle HorizontalAlign="Left" Width="40%"></ItemStyle>
                                                                                        <ItemTemplate>
                                                                                            <div class="stringtieudeandsc">
                                                                                                <div class="fontTitle" style="width: 90%;">
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
                                                                                    <asp:TemplateColumn HeaderText="<%$Resources:cms.language, lblChuyenmuc%>">
                                                                                        <HeaderStyle Width="15%" HorizontalAlign="Center"></HeaderStyle>
                                                                                        <ItemStyle Width="15%" HorizontalAlign="Left"></ItemStyle>
                                                                                        <ItemTemplate>
                                                                                            <%#HPCBusinessLogic.UltilFunc.GetCategoryName(Eval("CAT_ID"))%>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                    <asp:TemplateColumn HeaderText="<%$Resources:cms.language, lblNguoinhap%>">
                                                                                        <HeaderStyle Width="10%" HorizontalAlign="Center"></HeaderStyle>
                                                                                        <ItemStyle Width="10%" HorizontalAlign="Center"></ItemStyle>
                                                                                        <ItemTemplate>
                                                                                            <%#HPCBusinessLogic.UltilFunc.GetUserName(Eval("News_AuthorID"))%>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                    <asp:TemplateColumn HeaderText="<%$Resources:cms.language, lblNgaynhap%>">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="ngaytao" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.News_DateCreated")!=System.DBNull.Value?Convert.ToDateTime(DataBinder.Eval(Container, "DataItem.News_DateCreated")).ToString("dd/MM/yyyy HH:mm:ss"):"" %>'>
                                                                                            </asp:Label>
                                                                                            <asp:Label ID="ID1" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.News_ID")%>'>
                                                                                            </asp:Label>
                                                                                        </ItemTemplate>
                                                                                        <HeaderStyle HorizontalAlign="Center" Width="10%"></HeaderStyle>
                                                                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                                                    </asp:TemplateColumn>
                                                                                    <asp:TemplateColumn HeaderText="<%$Resources:cms.language, lblXem%>">
                                                                                        <HeaderStyle HorizontalAlign="Center" Width="5%"></HeaderStyle>
                                                                                        <ItemStyle HorizontalAlign="Center" Width="5%"></ItemStyle>
                                                                                        <ItemTemplate>
                                                                                            <a target="_blank" href="<%=Global.ApplicationPath%>/View/ViewHistory.aspx?Menu_ID=<%=Page.Request.QueryString["Menu_ID"]%>&ID=<%# DataBinder.Eval(Container.DataItem, "News_ID") %>" />
                                                                                            <img src='<%= Global.ApplicationPath %>/Dungchung/images/view.gif' border="0" alt="Xem tin"
                                                                                                onmouseover="(window.status=''); return true" style="cursor: pointer;" title="Xem tin">
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                    <asp:TemplateColumn>
                                                                                        <HeaderStyle HorizontalAlign="Center" Width="5%"></HeaderStyle>
                                                                                        <ItemStyle HorizontalAlign="Center" Height="25px" Width="5%"></ItemStyle>
                                                                                        <HeaderTemplate>
                                                                                            <img style="cursor: pointer" src='<%= Global.ApplicationPath%>/Dungchung/images/doc.gif'
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
                                                                            <table border="0" style="width: 100%">
                                                                                <tr>
                                                                                    <td style="width: 50%">
                                                                                        <asp:Button runat="server" ID="LinkPubOne" CausesValidation="false" OnClick="Send_TKTS"
                                                                                            Text="<%$Resources:cms.language, lblGui%>" Font-Bold="true" CssClass="iconPub">
                                                                                        </asp:Button>
                                                                                        <asp:Button runat="server" ID="LinkDeleteOne" CausesValidation="false" OnClick="Delete_Click"
                                                                                            Text="<%$Resources:cms.language, lblXoa%>" CssClass="iconDel"></asp:Button>
                                                                                    </td>
                                                                                    <td style="text-align: right" class="pageNav">
                                                                                        <cc1:CurrentPage runat="server" ID="CurrentPage2" CssClass="pageNavTotal">
                                                                                        </cc1:CurrentPage>
                                                                                        <cc1:Pager runat="server" ID="pages" OnIndexChanged="pages_IndexChanged_baidangxuly"></cc1:Pager>
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
                                                <cc2:TabPanel HeaderText="<%$Resources:cms.language, lblTintralai%>" ID="TabPanel1"
                                                    runat="server">
                                                    <ContentTemplate>
                                                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                            <ContentTemplate>
                                                                <table border="0" style="width: 100%">
                                                                    <tr>
                                                                        <td>
                                                                            <asp:DataGrid ID="dgr_tintuc2" runat="server" Width="100%" AutoGenerateColumns="False"
                                                                                DataKeyField="News_ID" CssClass="Grid" OnItemDataBound="dgData_ItemDataBound1"
                                                                                OnEditCommand="dgData_EditCommand1">
                                                                                <ItemStyle CssClass="GridItem"></ItemStyle>
                                                                                <AlternatingItemStyle CssClass="GridAltItem" />
                                                                                <HeaderStyle CssClass="GridHeader"></HeaderStyle>
                                                                                <Columns>
                                                                                    <asp:TemplateColumn>
                                                                                        <HeaderStyle HorizontalAlign="Center" Width="3%"></HeaderStyle>
                                                                                        <ItemStyle HorizontalAlign="Center" Width="3%"></ItemStyle>
                                                                                        <HeaderTemplate>
                                                                                            <asp:CheckBox ID="chkAll" onclick="javascript:checkAll_Two(this);" runat="server"
                                                                                                ToolTip="<%$Resources:cms.language, lblChontatca%>"></asp:CheckBox>
                                                                                        </HeaderTemplate>
                                                                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                                                        <ItemTemplate>
                                                                                            <asp:CheckBox runat="server" Text='' ID='optSelect' AutoPostBack="False"></asp:CheckBox>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                    <asp:BoundColumn DataField="News_ID" HeaderText="ID" Visible="False"></asp:BoundColumn>
                                                                                    <asp:TemplateColumn HeaderText="<%$Resources:cms.language, lblAnhdaidien%>">
                                                                                        <HeaderStyle HorizontalAlign="Center" Width="10%"></HeaderStyle>
                                                                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                                        <ItemTemplate>
                                                                                            <div class="gallery">
                                                                                                <div class="pictgalery" style="width: 100px;">
                                                                                                    <%#UltilFunc.ReturnPath_Images(Eval("Images_Summary"))%>
                                                                                                </div>
                                                                                            </div>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                    <asp:TemplateColumn HeaderText="<%$Resources:cms.language, lblTenbaiviet%>">
                                                                                        <HeaderStyle HorizontalAlign="Left" Width="32%"></HeaderStyle>
                                                                                        <ItemStyle HorizontalAlign="Left" Width="32%"></ItemStyle>
                                                                                        <ItemTemplate>
                                                                                            <div class="stringtieudeandsc">
                                                                                                <div class="fontTitle" style="width: 90%;">
                                                                                                    <asp:LinkButton CssClass='<%#UltilFunc.GetStyleComments(Eval("News_ID"))%>' Text='<%# DataBinder.Eval(Container.DataItem, "News_Tittle" )%>'
                                                                                                        Enabled="true" runat="server" ID="linkTittle" CommandName="Edit" CommandArgument="Edit"
                                                                                                        ToolTip="<%$Resources:cms.language, lblSuabai%>"></asp:LinkButton><%#HPCBusinessLogic.UltilFunc.IsStatusGet(Eval("News_IsImages"),Eval("News_IsVideo"))%><%#HPCBusinessLogic.UltilFunc.IsStatusImages(DataBinder.Eval(Container.DataItem, "News_Body").ToString())%>
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
                                                                                    <asp:TemplateColumn HeaderText="<%$Resources:cms.language, lblChuyenmuc%>">
                                                                                        <HeaderStyle Width="12%" HorizontalAlign="Center"></HeaderStyle>
                                                                                        <ItemStyle Width="12%" HorizontalAlign="Left"></ItemStyle>
                                                                                        <ItemTemplate>
                                                                                            <%#HPCBusinessLogic.UltilFunc.GetCategoryName(Eval("CAT_ID"))%>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                    <asp:TemplateColumn HeaderText="<%$Resources:cms.language, lblNguoinhap%>">
                                                                                        <HeaderStyle Width="9%" HorizontalAlign="Center"></HeaderStyle>
                                                                                        <ItemStyle Width="9%" HorizontalAlign="Center"></ItemStyle>
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
                                                                                    <asp:TemplateColumn HeaderText="<%$Resources:cms.language, lblNguoitra%>">
                                                                                        <HeaderStyle HorizontalAlign="Center" Width="9%"></HeaderStyle>
                                                                                        <ItemStyle HorizontalAlign="Center" Width="9%"></ItemStyle>
                                                                                        <ItemTemplate>
                                                                                            <%#HPCBusinessLogic.UltilFunc.GetUserName(Eval("News_AprovedID"))%>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                    <asp:TemplateColumn HeaderText="<%$Resources:cms.language, lblNgaytra%>">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="ngaytao" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.News_DateSend")!=System.DBNull.Value?Convert.ToDateTime(DataBinder.Eval(Container, "DataItem.News_DateSend")).ToString("dd/MM/yyyy HH:mm:ss"):"" %>'>
                                                                                            </asp:Label>
                                                                                            <asp:Label ID="ID1" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.News_ID")%>'>
                                                                                            </asp:Label>
                                                                                        </ItemTemplate>
                                                                                        <HeaderStyle HorizontalAlign="Center" Width="10%"></HeaderStyle>
                                                                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                                                    </asp:TemplateColumn>
                                                                                    <asp:TemplateColumn HeaderText="<%$Resources:cms.language, lblXem%>">
                                                                                        <HeaderStyle HorizontalAlign="Center" Width="5%"></HeaderStyle>
                                                                                        <ItemStyle HorizontalAlign="Center" Width="5%"></ItemStyle>
                                                                                        <ItemTemplate>
                                                                                            <a target="_blank" href="<%=Global.ApplicationPath%>/View/ViewHistory.aspx?Menu_ID=<%=Page.Request.QueryString["Menu_ID"]%>&ID=<%# DataBinder.Eval(Container.DataItem, "News_ID") %>" />
                                                                                            <img src='<%= Global.ApplicationPath %>/Dungchung/images/view.gif' border="0" alt="Xem tin"
                                                                                                onmouseover="(window.status=''); return true" style="cursor: pointer;" title="Xem tin">
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                    <asp:TemplateColumn>
                                                                                        <HeaderStyle HorizontalAlign="Center" Width="5%"></HeaderStyle>
                                                                                        <ItemStyle HorizontalAlign="Center" Width="5%"></ItemStyle>
                                                                                        <HeaderTemplate>
                                                                                            <img style="cursor: pointer;" src='<%= Global.ApplicationPath%>/Dungchung/images/doc.gif'
                                                                                                border="0" title="Download nội dung bài viết" alt='Download nội dung bài viết'>
                                                                                        </HeaderTemplate>
                                                                                        <ItemTemplate>
                                                                                            <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/Dungchung/Images/doc.gif"
                                                                                                ToolTip="Export word" CommandName="Edit" CommandArgument="DownLoadAlias" />
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
                                                                                    <td style="width: 50%">
                                                                                        <asp:Button runat="server" ID="LinkPubTwo" CausesValidation="false" OnClick="Send_TKTS"
                                                                                            CssClass="iconPub" Text="<%$Resources:cms.language, lblGui%>" Font-Bold="true">
                                                                                        </asp:Button>
                                                                                        <asp:Button runat="server" ID="LinkDeleteTwo" CausesValidation="false" OnClick="Delete_Click"
                                                                                            CssClass="iconDel" Text="<%$Resources:cms.language, lblXoa%>" Font-Bold="true">
                                                                                        </asp:Button>
                                                                                    </td>
                                                                                    <td class="pageNav" style="text-align: right">
                                                                                        <cc1:CurrentPage runat="server" ID="CurrentPage1" CssClass="pageNavTotal"></cc1:CurrentPage>
                                                                                        <cc1:Pager runat="server" ID="pages1" OnIndexChanged="pages_IndexChanged_baitralai"></cc1:Pager>
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
                                                <cc2:TabPanel HeaderText="<%$Resources:cms.language, lblTindagui%>" ID="TabPanel2"
                                                    runat="server">
                                                    <ContentTemplate>
                                                        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                                            <ContentTemplate>
                                                                <table border="0" style="width: 100%">
                                                                    <tr>
                                                                        <td>
                                                                            <asp:DataGrid ID="Dgr_Baidaxuly" runat="server" Width="100%" AutoGenerateColumns="False"
                                                                                OnItemDataBound="Dgr_Baidaxuly_ItemDataBound" DataKeyField="ID" CssClass="Grid">
                                                                                <ItemStyle CssClass="GridItem"></ItemStyle>
                                                                                <AlternatingItemStyle CssClass="GridAltItem" />
                                                                                <HeaderStyle CssClass="GridHeader"></HeaderStyle>
                                                                                <Columns>
                                                                                    <asp:BoundColumn DataField="ID" HeaderText="ID" Visible="False"></asp:BoundColumn>
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
                                                                                        <HeaderStyle HorizontalAlign="Left" Width="30%"></HeaderStyle>
                                                                                        <ItemStyle HorizontalAlign="Left" Width="30%"></ItemStyle>
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
                                                                                    <asp:TemplateColumn HeaderText="<%$Resources:cms.language, lblChuyenmuc%>">
                                                                                        <HeaderStyle Width="10%" HorizontalAlign="Center"></HeaderStyle>
                                                                                        <ItemStyle Width="10%" HorizontalAlign="Left"></ItemStyle>
                                                                                        <ItemTemplate>
                                                                                            <%#HPCBusinessLogic.UltilFunc.GetCategoryName(Eval("CAT_ID"))%>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                    <asp:TemplateColumn HeaderText="<%$Resources:cms.language, lblNguoinhap%>">
                                                                                        <HeaderStyle Width="8%" HorizontalAlign="Center"></HeaderStyle>
                                                                                        <ItemStyle Width="8%" HorizontalAlign="Center"></ItemStyle>
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
                                                                                    <asp:TemplateColumn HeaderText="<%$Resources:cms.language, lblNguoihieudinh%>">
                                                                                        <ItemTemplate>
                                                                                            <%#HPCBusinessLogic.UltilFunc.GetUserName(Eval("News_EditorID"))%>
                                                                                        </ItemTemplate>
                                                                                        <HeaderStyle HorizontalAlign="Center" Width="8%"></HeaderStyle>
                                                                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                                                    </asp:TemplateColumn>
                                                                                    <asp:TemplateColumn HeaderText="<%$Resources:cms.language, lblNgayhieudinh%>">
                                                                                        <ItemTemplate>
                                                                                            <%# DataBinder.Eval(Container, "DataItem.News_DateEdit")!=System.DBNull.Value?Convert.ToDateTime(DataBinder.Eval(Container, "DataItem.News_DateEdit")).ToString("dd/MM/yyyy HH:mm:ss"):"" %>
                                                                                            <asp:Label ID="ID1" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ID")%>'>
                                                                                            </asp:Label>
                                                                                        </ItemTemplate>
                                                                                        <HeaderStyle HorizontalAlign="Center" Width="10%"></HeaderStyle>
                                                                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                                                    </asp:TemplateColumn>
                                                                                    <asp:TemplateColumn HeaderText="<%$Resources:cms.language, lblTrangthai%>">
                                                                                        <HeaderStyle Width="9%" HorizontalAlign="Center"></HeaderStyle>
                                                                                        <ItemStyle Width="9%" HorizontalAlign="Center"></ItemStyle>
                                                                                        <ItemTemplate>
                                                                                            <%#HPCComponents.Global.GetStatusT_NewsFrom_T_version(Eval("News_ID"))%>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                    <asp:TemplateColumn HeaderText="<%$Resources:cms.language, lblXem%>">
                                                                                        <HeaderStyle HorizontalAlign="Center" Width="5%"></HeaderStyle>
                                                                                        <ItemStyle HorizontalAlign="Center" Width="5%"></ItemStyle>
                                                                                        <ItemTemplate>
                                                                                            <a target="_blank" href="<%=Global.ApplicationPath%>/View/ViewDetails.aspx?Menu_ID=<%=Page.Request.QueryString["Menu_ID"]%>&ID=<%# DataBinder.Eval(Container.DataItem, "ID") %>&Old_id=1" />
                                                                                            <img src='<%= Global.ApplicationPath %>/DungChung/images/view.gif' border="0" alt="Xem tin"
                                                                                                onmouseover="(window.status=''); return true" style="cursor: pointer;" title="Xem tin">
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                </Columns>
                                                                            </asp:DataGrid>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="pageNav" style="text-align: right">
                                                                            <cc1:CurrentPage runat="server" ID="CurrentPage3" CssClass="pageNavTotal">
                                                                            </cc1:CurrentPage>
                                                                            <cc1:Pager runat="server" ID="Pager3" OnIndexChanged="pages_IndexChanged_baidaxuly"></cc1:Pager>
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
                                                <cc2:TabPanel HeaderText="<%$Resources:cms.language, lblTindaxoa%>" ID="TabPanelDelete"
                                                    runat="server">
                                                    <ContentTemplate>
                                                        <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                                            <ContentTemplate>
                                                                <table border="0" width="100%">
                                                                    <tr>
                                                                        <td>
                                                                            <asp:DataGrid ID="dgr_BaiXoa" runat="server" Width="100%" AutoGenerateColumns="False"
                                                                                DataKeyField="News_ID" CssClass="Grid" OnItemDataBound="dgr_BaiXoa_ItemDataBound"
                                                                                OnEditCommand="dgr_BaiXoa_EditCommand">
                                                                                <ItemStyle CssClass="GridItem"></ItemStyle>
                                                                                <AlternatingItemStyle CssClass="GridAltItem" />
                                                                                <HeaderStyle CssClass="GridHeader"></HeaderStyle>
                                                                                <Columns>
                                                                                    <asp:TemplateColumn>
                                                                                        <HeaderStyle HorizontalAlign="Center" Width="3%"></HeaderStyle>
                                                                                        <ItemStyle HorizontalAlign="Center" Width="3%"></ItemStyle>
                                                                                        <HeaderTemplate>
                                                                                            <asp:CheckBox ID="chkAll" onclick="javascript:checkAll_BaiXoa(this);" runat="server"
                                                                                                ToolTip="<%$Resources:cms.language, lblChontatca%>"></asp:CheckBox>
                                                                                        </HeaderTemplate>
                                                                                        <ItemTemplate>
                                                                                            <asp:CheckBox runat="server" Text='' ID='optSelect' AutoPostBack="False"></asp:CheckBox>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                    <asp:BoundColumn DataField="News_ID" HeaderText="ID" Visible="False"></asp:BoundColumn>
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
                                                                                                    <%# HPCBusinessLogic.UltilFunc.LockedUser(DataBinder.Eval(Container.DataItem, "News_Lock").ToString(), DataBinder.Eval(Container.DataItem, "News_EditorID").ToString(),_user.UserID)%>
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
                                                                                        <HeaderStyle Width="12%" HorizontalAlign="Center"></HeaderStyle>
                                                                                        <ItemStyle Width="12%" HorizontalAlign="Left"></ItemStyle>
                                                                                        <ItemTemplate>
                                                                                            <%#HPCBusinessLogic.UltilFunc.GetCategoryName(Eval("CAT_ID"))%>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                    <asp:TemplateColumn HeaderText="<%$Resources:cms.language, lblNguoinhap%>">
                                                                                        <HeaderStyle Width="9%" HorizontalAlign="Center"></HeaderStyle>
                                                                                        <ItemStyle Width="9%" HorizontalAlign="Center"></ItemStyle>
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
                                                                                    <asp:TemplateColumn HeaderText="<%$Resources:cms.language, lblNguoixoa%>">
                                                                                        <HeaderStyle HorizontalAlign="Center" Width="9%"></HeaderStyle>
                                                                                        <ItemStyle HorizontalAlign="Center" Width="9%"></ItemStyle>
                                                                                        <ItemTemplate>
                                                                                            <%#HPCBusinessLogic.UltilFunc.GetUserName(Eval("News_EditorID"))%>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                    <asp:TemplateColumn HeaderText="<%$Resources:cms.language, lblNgayxoa%>">
                                                                                        <ItemTemplate>
                                                                                            <%# DataBinder.Eval(Container, "DataItem.News_DateEdit") != System.DBNull.Value ? Convert.ToDateTime(DataBinder.Eval(Container, "DataItem.News_DateEdit")).ToString("dd/MM/yyyy HH:mm:ss") : ""%>
                                                                                        </ItemTemplate>
                                                                                        <HeaderStyle HorizontalAlign="Center" Width="10%"></HeaderStyle>
                                                                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                                                    </asp:TemplateColumn>
                                                                                    <asp:TemplateColumn HeaderText="<%$Resources:cms.language, lblXem%>">
                                                                                        <HeaderStyle HorizontalAlign="Center" Width="3%"></HeaderStyle>
                                                                                        <ItemStyle HorizontalAlign="Center" Width="3%"></ItemStyle>
                                                                                        <ItemTemplate>
                                                                                            <%--<a href="Javascript:open_window_Scroll('<%=Global.ApplicationPath%>/BaoDienTu/ViewAndPrint.aspx?Menu_ID=<%=Page.Request.QueryString["Menu_ID"]%>&ID=<%# DataBinder.Eval(Container.DataItem, "News_ID") %>',50,500,100,800);" />
                                                                                            <img src='../images/view.gif' border="0" alt="Xem/ In" border="0" onmouseover="(window.status=''); return true"
                                                                                                style="cursor: pointer;" title="Xem/ In">--%>
                                                                                            <a target="_blank" href="<%=Global.ApplicationPath%>/View/ViewHistory.aspx?Menu_ID=<%=Page.Request.QueryString["Menu_ID"]%>&ID=<%# DataBinder.Eval(Container.DataItem, "News_ID") %>" />
                                                                                            <img src='<%= Global.ApplicationPath %>/Dungchung/images/view.gif' border="0" alt="Xem tin"
                                                                                                onmouseover="(window.status=''); return true" style="cursor: pointer;" title="Xem tin">
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
                                                                                            OnClick="Send_TKTS" Text="<%$Resources:cms.language, lblGui%>" Font-Bold="true">
                                                                                        </asp:Button>
                                                                                        <!--<asp:Button runat="server" ID="BtnXoa" CausesValidation="false" CssClass="iconDel"
                                                                                            Text="" OnClick="Delete_Click">
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
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </td>
                    </tr>
                </table>
            </td>
            <td class="datagrid_content_right">
            </td>
            <tr>
                <td class="datagrid_bottom_left">
                </td>
                <td class="datagrid_bottom_center">
                </td>
                <td class="datagrid_bottom_right">
                </td>
            </tr>
        </tr>
    </table>
</asp:Content>
