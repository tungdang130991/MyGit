<%@ Page Language="C#" MasterPageFile="~/Masters/ITC.Master" AutoEventWireup="true"
    CodeBehind="List_Multimedia.aspx.cs" Inherits="ToasoanTTXVN.Multimedia.List_Multimedia" %>

<%@ Register TagPrefix="anthem" Namespace="Anthem" Assembly="Anthem" %>
<%@ Register TagPrefix="cc1" Namespace="HPCComponents.UI.WebControls" Assembly="HPCComponents" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<%@ Import Namespace="HPCComponents" %>
<%@ Import Namespace="HPCBusinessLogic" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <script type="text/javascript" language="javascript">
        function checkAll_DM_Clips(objRef, objectid) {
            var GridView = document.getElementById('<%=dgDataEditor.ClientID%>');
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
            var GridView = document.getElementById('<%=DataGrid_Tralai.ClientID%>');
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
        function SetInnerProcess(dangcapnhat, choxuatban, tralai, daxuatban) {
            document.getElementById("__tab_ctl00_MainContent_TabContainer1_tabpnltinXuly").innerHTML = dangcapnhat;
            document.getElementById("__tab_ctl00_MainContent_TabContainer1_TabPanel3").innerHTML = choxuatban;
            document.getElementById("__tab_ctl00_MainContent_TabContainer1_TabPanel1").innerHTML = tralai;
            document.getElementById("__tab_ctl00_MainContent_TabContainer1_TabPanel2").innerHTML = daxuatban;
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
                            <asp:Label ID="Label2" class="TitlePanel" runat="server" 
                                                Text="<%$Resources:cms.language, titNhapamthanh%>"></asp:Label>
                            <%--<span class="TitlePanel"><%= CommonLib.ReadXML("titNhapamthanh") %></span>--%>
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
                    <table width="100%" cellspacing="0" cellpadding="0" border="0">
                        <tr>
                            <td style="text-align: left">
                                <cc2:TabContainer ID="TabContainer1" runat="server" CssClass="ajax__tab_yuitabview-theme"
                                    AutoPostBack="true" ActiveTabIndex="0" OnActiveTabChanged="TabContainer1_ActiveTabChanged">
                                    <cc2:TabPanel HeaderText="<%$Resources:cms.language, lblDangcapnhat%>" ID="tabpnltinXuly" runat="server">
                                        <ContentTemplate>
                                            <asp:UpdatePanel ID="UpdatePanel11" runat="server">
                                                <ContentTemplate>
                                                    <table width="100%" cellspacing="2" cellpadding="2" border="0">
                                                        <tr>
                                                            <td style="text-align: left" colspan="2">
                                                                <asp:Button runat="server" ID="btnAddNewOnXuLy" CssClass="iconAddNew" Font-Bold="true"
                                                                    OnClick="cmdAdd_Click" Text="<%$Resources:cms.language, lblNhapvideo%>" />
                                                                <asp:Button runat="server" ID="btnGuiDuyetOnXuLy" CssClass="iconPub" Font-Bold="true"
                                                                    Text="<%$Resources:cms.language, lblGui%>" OnClick="lbt_Guiduyet_Click" />
                                                                <asp:Button runat="server" ID="btnDeleteOnXuLy" CssClass="iconDel" Font-Bold="true"
                                                                    OnClick="lbt_xoa_Click" Text="<%$Resources:cms.language, lblXoa%>" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="center" colspan="2">
                                                                <asp:DataGrid ID="dgDataEditor" runat="server" Width="100%" AutoGenerateColumns="False"
                                                                    DataKeyField="ID" OnEditCommand="dgData_EditCommandEditor" OnItemDataBound="dgData_ItemDataBoundEditor"
                                                                    CssClass="Grid">
                                                                    <ItemStyle CssClass="GridItem"></ItemStyle>
                                                                    <AlternatingItemStyle CssClass="GridAltItem" />
                                                                    <HeaderStyle CssClass="GridHeader"></HeaderStyle>
                                                                    <Columns>
                                                                        <asp:BoundColumn DataField="ID" HeaderText="ID" Visible="False"></asp:BoundColumn>
                                                                        <asp:TemplateColumn>
                                                                            <HeaderStyle HorizontalAlign="Center" Width="5%"></HeaderStyle>
                                                                            <ItemStyle HorizontalAlign="Center" Width="5%"></ItemStyle>
                                                                            <HeaderTemplate>
                                                                                <asp:CheckBox ID="chkAll" onclick="javascript:checkAll_DM_Clips(this);" runat="server"
                                                                                    ToolTip="<%$Resources:cms.language, lblChontatca%>"></asp:CheckBox>
                                                                            </HeaderTemplate>
                                                                            <ItemTemplate>
                                                                                <asp:CheckBox runat="server" Text='' ID='optSelect' AutoPostBack="False"></asp:CheckBox>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateColumn>
                                                                        <asp:TemplateColumn HeaderText="Video">
                                                                            <HeaderStyle Width="15%" HorizontalAlign="Center"></HeaderStyle>
                                                                            <ItemStyle Width="15%" HorizontalAlign="Center" />
                                                                            <ItemTemplate>
                                                                                <a href="Javascript:PopupWindowVideo('<%=Global.ApplicationPath%>/Multimedia/ViewVideo.aspx?Menu_ID=<%=Page.Request.QueryString["Menu_ID"]%>&ID=<%# DataBinder.Eval(Container.DataItem, "ID") %>');" />
                                                                                <img src='<%#CommonLib._returnimg(Eval("URL_Images"))%>' style="width: 120px;" border="0"
                                                                                    alt="Xem Video" onmouseover="(window.status=''); return true" style="cursor: pointer;
                                                                                    border: 0" title="Xem Video" />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateColumn>
                                                                        <asp:TemplateColumn HeaderText="<%$Resources:cms.language, lblTieude%>">
                                                                            <HeaderStyle Width="30%" HorizontalAlign="Center"></HeaderStyle>
                                                                            <ItemStyle Width="30%" HorizontalAlign="Left" />
                                                                            <ItemTemplate>
                                                                                <asp:LinkButton ID="btnEdit" runat="server" CssClass="linkEdit" Text='<%# DataBinder.Eval(Container.DataItem, "Tittle") %>'
                                                                                    ToolTip="Sửa đổi" CommandName="Edit" CommandArgument="Edit"></asp:LinkButton>
                                                                                <asp:Label runat="server" ID="lblLogTitle" Visible="false" Text='<%# DataBinder.Eval(Container.DataItem, "Tittle") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateColumn>
                                                                        <asp:TemplateColumn  HeaderText="<%$Resources:cms.language, lblChuyenmuc%>">
                                                                            <HeaderStyle Width="15%" HorizontalAlign="Center"></HeaderStyle>
                                                                            <ItemStyle Width="15%" HorizontalAlign="Left"></ItemStyle>
                                                                            <ItemTemplate>
                                                                                <%#HPCBusinessLogic.UltilFunc.GetCategoryName(Eval("Category"))%>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateColumn>
                                                                        <asp:TemplateColumn HeaderText="<%$Resources:cms.language, lblNgaynhap%>">
                                                                            <ItemTemplate>
                                                                                <%# DataBinder.Eval(Container, "DataItem.DateCreated")!=System.DBNull.Value?Convert.ToDateTime(DataBinder.Eval(Container, "DataItem.DateCreated")).ToString("dd/MM/yyyy HH:mm:ss"):"" %>
                                                                            </ItemTemplate>
                                                                            <HeaderStyle HorizontalAlign="Center" Width="10%"></HeaderStyle>
                                                                            <ItemStyle HorizontalAlign="Center" Width="10%"></ItemStyle>
                                                                        </asp:TemplateColumn>
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
                                                            <td style="text-align: left;">
                                                                
                                                            </td>
                                                            <td style="text-align: right" class="pageNav">
                                                                <cc1:CurrentPage runat="server" ID="CurrentPageEditor" CssClass="pageNavTotal">
                                                                </cc1:CurrentPage>
                                                                <cc1:Pager runat="server" ID="pagesEditor" OnIndexChanged="pages_IndexChanged_Editor"></cc1:Pager>
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
                                    <cc2:TabPanel HeaderText="<%$Resources:cms.language, lblTralai%>" ID="TabPanel1" runat="server">
                                        <ContentTemplate>
                                            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                                <ContentTemplate>
                                                    <table width="100%" cellspacing="2" cellpadding="2" border="0">
                                                        <tr>
                                                            <td style="text-align: left" colspan="2">
                                                                <asp:Button runat="server" ID="btnGuiDuyetTraLaiOn" CssClass="iconPub" Font-Bold="true"
                                                                    Text="<%$Resources:cms.language, lblGui%>" OnClick="lbt_tabtralai_gui_Click" />
                                                                <asp:Button runat="server" ID="btnReturnXoaOn" CssClass="iconDel" Font-Bold="true"
                                                                    Text="<%$Resources:cms.language, lblXoa%>" OnClick="lbt_tabtralai_xoa_Click" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="center" colspan="2">
                                                                <asp:DataGrid ID="DataGrid_Tralai" runat="server" Width="100%" AutoGenerateColumns="False"
                                                                    DataKeyField="ID" OnEditCommand="DataGrid_Tralai_EditCommandEditor" OnItemDataBound="DataGrid_Tralai_ItemDataBoundEditor"
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
                                                                                <asp:CheckBox ID="chkAll" onclick="javascript:checkAll_DM_Clips_tralai(this);" runat="server"
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
                                                                            </ItemTemplate>
                                                                        </asp:TemplateColumn>
                                                                        <asp:TemplateColumn  HeaderText="<%$Resources:cms.language, lblTieude%>">
                                                                            <HeaderStyle Width="23%" HorizontalAlign="Center"></HeaderStyle>
                                                                            <ItemStyle Width="23%" HorizontalAlign="Left" />
                                                                            <ItemTemplate>
                                                                                <asp:LinkButton ID="btnEdit" runat="server" CssClass="linkEdit" Text='<%# DataBinder.Eval(Container.DataItem, "Tittle") %>'
                                                                                    ToolTip="Sửa đổi" CommandName="Edit" CommandArgument="Edit"></asp:LinkButton>
                                                                                <asp:Label runat="server" ID="lblLogTitle" Visible="false" Text='<%# DataBinder.Eval(Container.DataItem, "Tittle") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateColumn>
                                                                        <asp:TemplateColumn HeaderText="<%$Resources:cms.language, lblChuyenmuc%>">
                                                                            <HeaderStyle Width="12%" HorizontalAlign="Center"></HeaderStyle>
                                                                            <ItemStyle Width="12%" HorizontalAlign="Left"></ItemStyle>
                                                                            <ItemTemplate>
                                                                                <%#HPCBusinessLogic.UltilFunc.GetCategoryName(Eval("Category"))%>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateColumn>
                                                                        <asp:TemplateColumn HeaderText="<%$Resources:cms.language, lblNgaynhap%>">
                                                                            <ItemTemplate>
                                                                                <%# DataBinder.Eval(Container, "DataItem.DateCreated")!=System.DBNull.Value?Convert.ToDateTime(DataBinder.Eval(Container, "DataItem.DateCreated")).ToString("dd/MM/yyyy HH:mm:ss"):"" %>
                                                                            </ItemTemplate>
                                                                            <HeaderStyle HorizontalAlign="Center" Width="9%"></HeaderStyle>
                                                                            <ItemStyle HorizontalAlign="Center" Width="9%"></ItemStyle>
                                                                        </asp:TemplateColumn>
                                                                        <asp:TemplateColumn HeaderText="<%$Resources:cms.language, lblNguoitralai%>">
                                                                            <ItemTemplate>
                                                                                <%#HPCBusinessLogic.UltilFunc.GetUserName(Eval("UserModify"))%>
                                                                            </ItemTemplate>
                                                                            <HeaderStyle HorizontalAlign="Center" Width="9%"></HeaderStyle>
                                                                            <ItemStyle HorizontalAlign="Center" Width="9%"></ItemStyle>
                                                                        </asp:TemplateColumn>
                                                                        <asp:TemplateColumn HeaderText="<%$Resources:cms.language, lblNgaytralai%>">
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
                                                                            <HeaderStyle HorizontalAlign="Center" Width="15%"></HeaderStyle>
                                                                            <ItemStyle HorizontalAlign="Left" Width="15%"></ItemStyle>
                                                                        </asp:TemplateColumn>
                                                                    </Columns>
                                                                </asp:DataGrid>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="text-align: left;">
                                                                
                                                            </td>
                                                            <td style="text-align: right" class="pageNav">
                                                                <cc1:CurrentPage runat="server" ID="CurrentPage_Tralai" CssClass="pageNavTotal">
                                                                </cc1:CurrentPage>
                                                                <cc1:Pager runat="server" ID="Pager_Tralai" OnIndexChanged="Pager_Tralai_IndexChanged_Editor"></cc1:Pager>
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
                                    <cc2:TabPanel HeaderText="<%$Resources:cms.language, lblChoXB%>" ID="TabPanel3" runat="server">
                                        <ContentTemplate>
                                            <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                                <ContentTemplate>
                                                    <table width="100%" cellspacing="2" cellpadding="2" border="0">
                                                        <tr>
                                                            <td align="center">
                                                                <asp:DataGrid ID="DataGrid_choduyet" runat="server" Width="100%" AutoGenerateColumns="False"
                                                                    DataKeyField="ID" OnEditCommand="DataGrid_choduyet_EditCommandEditor" CssClass="Grid">
                                                                    <ItemStyle CssClass="GridItem"></ItemStyle>
                                                                    <AlternatingItemStyle CssClass="GridAltItem" />
                                                                    <HeaderStyle CssClass="GridHeader"></HeaderStyle>
                                                                    <Columns>
                                                                        <asp:BoundColumn DataField="ID" HeaderText="ID" Visible="False"></asp:BoundColumn>
                                                                        <asp:TemplateColumn HeaderText="Video">
                                                                            <HeaderStyle Width="10%" HorizontalAlign="Center"></HeaderStyle>
                                                                            <ItemStyle Width="10%" HorizontalAlign="Center" />
                                                                            <ItemTemplate>
                                                                                <a href="Javascript:PopupWindowVideo('<%=Global.ApplicationPath%>/Multimedia/ViewVideo.aspx?Menu_ID=<%=Page.Request.QueryString["Menu_ID"]%>&ID=<%# DataBinder.Eval(Container.DataItem, "ID") %>');" />
                                                                                <img src='<%#CommonLib._returnimg(Eval("URL_Images"))%>' style="width: 120px;" border="0"
                                                                                    alt="Xem Video" onmouseover="(window.status=''); return true" style="cursor: pointer;
                                                                                    border: 0" title="Xem Video" />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateColumn>
                                                                        <asp:TemplateColumn HeaderText="<%$Resources:cms.language, lblTieude%>">
                                                                            <HeaderStyle Width="25%" HorizontalAlign="Center"></HeaderStyle>
                                                                            <ItemStyle Width="25%" HorizontalAlign="Left" CssClass="GridBorderVerSolid_Tittle" />
                                                                            <ItemTemplate>
                                                                                <%# DataBinder.Eval(Container.DataItem, "Tittle") %>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateColumn>
                                                                        <asp:TemplateColumn HeaderText="<%$Resources:cms.language, lblChuyenmuc%>">
                                                                            <HeaderStyle Width="15%" HorizontalAlign="Center"></HeaderStyle>
                                                                            <ItemStyle Width="15%" HorizontalAlign="Left"></ItemStyle>
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
                                                                        <asp:TemplateColumn HeaderText="<%$Resources:cms.language, lblNguoihieudinh%>">
                                                                            <ItemTemplate>
                                                                                <%#HPCBusinessLogic.UltilFunc.GetUserName(Eval("UserModify"))%>
                                                                            </ItemTemplate>
                                                                            <HeaderStyle HorizontalAlign="Center" Width="10%"></HeaderStyle>
                                                                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                                        </asp:TemplateColumn>
                                                                        <asp:TemplateColumn HeaderText="<%$Resources:cms.language, lblNgayhieudinh%>">
                                                                            <ItemTemplate>
                                                                                <%# DataBinder.Eval(Container, "DataItem.DateModify") != System.DBNull.Value ? Convert.ToDateTime(DataBinder.Eval(Container, "DataItem.DateModify")).ToString("dd/MM/yyyy HH:mm:ss") : ""%>
                                                                            </ItemTemplate>
                                                                            <HeaderStyle HorizontalAlign="Center" Width="10%"></HeaderStyle>
                                                                            <ItemStyle HorizontalAlign="Center" Width="10%"></ItemStyle>
                                                                        </asp:TemplateColumn>
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
                                                                <cc1:CurrentPage runat="server" ID="CurrentPage_Choduyet" CssClass="pageNavTotal">
                                                                </cc1:CurrentPage>
                                                                <cc1:Pager runat="server" ID="Pager_Choduyet" OnIndexChanged="Pager_Choduyet_IndexChanged_Editor"></cc1:Pager>
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
                                    <cc2:TabPanel HeaderText="<%$Resources:cms.language, lblDaXB%>" ID="TabPanel2" runat="server">
                                        <ContentTemplate>
                                            <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                                <ContentTemplate>
                                                    <table width="100%" cellspacing="2" cellpadding="2" border="0">
                                                        <tr>
                                                            <td align="center">
                                                                <asp:DataGrid ID="DataGrid_XB" runat="server" Width="100%" AutoGenerateColumns="False"
                                                                    OnEditCommand="DataGrid_XB_EditCommandEditor" DataKeyField="ID" CssClass="Grid">
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
                                                                            <ItemStyle HorizontalAlign="Left" Width="10%"></ItemStyle>
                                                                        </asp:TemplateColumn>
                                                                        <asp:TemplateColumn HeaderText="<%$Resources:cms.language, lblNgayXB%>">
                                                                            <ItemTemplate>
                                                                                <%# DataBinder.Eval(Container, "DataItem.DatePublish") != System.DBNull.Value ? Convert.ToDateTime(DataBinder.Eval(Container, "DataItem.DateCreated")).ToString("dd/MM/yyyy HH:mm:ss") : ""%>
                                                                            </ItemTemplate>
                                                                            <HeaderStyle HorizontalAlign="Center" Width="10%"></HeaderStyle>
                                                                            <ItemStyle HorizontalAlign="Center" Width="10%"></ItemStyle>
                                                                        </asp:TemplateColumn>
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
                                                                <cc1:CurrentPage runat="server" ID="CurrentPage_XB" CssClass="pageNavTotal">
                                                                </cc1:CurrentPage>
                                                                <cc1:Pager runat="server" ID="Pager_XB" OnIndexChanged="Pager_XB_IndexChanged_Editor"></cc1:Pager>
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
</asp:Content>
