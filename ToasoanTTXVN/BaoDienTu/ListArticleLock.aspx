<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/ITC.Master" AutoEventWireup="true"
    CodeBehind="ListArticleLock.aspx.cs" Inherits="ToasoanTTXVN.BaoDienTu.ListArticleLock" %>

<%@ Register TagPrefix="anthem" Namespace="Anthem" Assembly="Anthem" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<%@ Register TagPrefix="cc1" Namespace="HPCComponents.UI.WebControls" Assembly="HPCComponents" %>
<%@ Import Namespace="HPCComponents" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <script language="Javascript" type="text/javascript">
        function SetInnerBaiDangxyLy(TotalRecords) {
            document.getElementById("__tab_ctl00_MainContent_TabContainer1_tabpnltinXuly").innerHTML = "Danh sách bài khóa (" + TotalRecords + ")";
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
                                <%= CommonLib.ReadXML("titDsTinbikhoa") %></span>
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
            <td style="text-align: left;">
                <div class="classSearchHeader">
                    <table>
                        <tr>
                            <td style="width: 6%; text-align: left;" class="Titlelbl">
                                <asp:Label ID="Label2" class="Titlelbl" runat="server" Text="<%$Resources:cms.language, lblAnpham%>"></asp:Label>
                            </td>
                            <td style="width: 15%; text-align: left">
                                <anthem:DropDownList AutoCallBack="true" ID="ddlLang" runat="server" Width="150px"
                                    CssClass="inputtext" OnSelectedIndexChanged="ddlLang_SelectedIndexChanged">
                                </anthem:DropDownList>
                            </td>
                            <td style="width: 20%; text-align: right" class="Titlelbl">
                                <asp:Label ID="lblChuyenmuc" class="Titlelbl" runat="server" Text="<%$Resources:cms.language, lblChuyenmuc%>"></asp:Label>
                            </td>
                            <td style="width: 20%; text-align: left">
                                <anthem:DropDownList AutoCallBack="true" ID="ddlCategorysAll" CssClass="inputtext"
                                    runat="server" Width="90%">
                                </anthem:DropDownList>
                            </td>
                            <td style="width: 10%; text-align: right" class="Titlelbl">
                                <asp:Label ID="Label3" class="Titlelbl" runat="server" Text="<%$Resources:cms.language, lblTenbaiviet%>"></asp:Label>
                            </td>
                            <td style="width: 30%; text-align: left">
                                <asp:TextBox ID="txt_tieude" TabIndex="1" Width="90%" runat="server" CssClass="inputtext"
                                    onkeypress="return clickButton(event,'ctl00_MainContent_btnSearch');"></asp:TextBox>
                            </td>
                            <td style="width: 20%; text-align: center">
                                <asp:Button runat="server" ID="btnSearch" CssClass="iconFind" OnClick="btnSearch_Click"
                                    Text="<%$Resources:cms.language, lblTimkiem%>"></asp:Button>
                            </td>
                        </tr>
                    </table>
                </div>
                <cc2:TabContainer ID="TabContainer1" runat="server" CssClass="ajax__tab_yuitabview-theme"
                    AutoPostBack="true" ActiveTabIndex="0" OnActiveTabChanged="TabContainer1_ActiveTabChanged">
                    <cc2:TabPanel HeaderText="<%$Resources:cms.language, lblDsTinbikhoa%>" ID="tabpnltinXuly" runat="server">
                        <ContentTemplate>
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>
                                    <table width="100%" cellspacing="0" cellpadding="0" border="0" style="padding-top: 8px;">
                                        <tr>
                                            <td align="left">
                                                <asp:DataGrid runat="server" ID="grdList" AutoGenerateColumns="false" DataKeyField="News_ID"
                                                    OnItemDataBound="grdList_ItemDataBound" Width="100%" CssClass="Grid" OnEditCommand="grdList_EditCommand">
                                                    <ItemStyle CssClass="GridItem"></ItemStyle>
                                                    <AlternatingItemStyle CssClass="GridAltItem" />
                                                    <HeaderStyle CssClass="GridHeader"></HeaderStyle>
                                                    <Columns>
                                                        <asp:BoundColumn Visible="False" DataField="News_ID">
                                                            <HeaderStyle Width="1%"></HeaderStyle>
                                                        </asp:BoundColumn>
                                                        <asp:TemplateColumn HeaderText="<%$Resources:cms.language, lblThutu%>">
                                                            <HeaderStyle HorizontalAlign="Center" Width="2%" />
                                                            <ItemStyle Width="2%" HorizontalAlign="Center"></ItemStyle>
                                                            <ItemTemplate>
                                                                <%#  Container.ItemIndex + 1%>
                                                            </ItemTemplate>
                                                        </asp:TemplateColumn>
                                                        <asp:TemplateColumn HeaderText="<%$Resources:cms.language, lblTenbaiviet%>">
                                                            <HeaderStyle HorizontalAlign="Left" Width="35%" />
                                                            <ItemStyle Width="35%" HorizontalAlign="Left"></ItemStyle>
                                                            <ItemTemplate>
                                                                <%# DataBinder.Eval(Container.DataItem, "News_Tittle")%>
                                                            </ItemTemplate>
                                                        </asp:TemplateColumn>
                                                        <asp:TemplateColumn HeaderText="<%$Resources:cms.language, lblChuyenmuc%>">
                                                            <HeaderStyle HorizontalAlign="Left" Width="10%"></HeaderStyle>
                                                            <ItemStyle HorizontalAlign="Left" Width="10%"></ItemStyle>
                                                            <ItemTemplate>
                                                                <%#HPCBusinessLogic.UltilFunc.GetCategoryName(Eval("CAT_ID"))%>
                                                            </ItemTemplate>
                                                        </asp:TemplateColumn>
                                                        <asp:TemplateColumn HeaderText="<%$Resources:cms.language, lblNguoikhoa%>">
                                                            <HeaderStyle HorizontalAlign="Center" Width="10%"></HeaderStyle>
                                                            <ItemStyle HorizontalAlign="Center" Width="10%"></ItemStyle>
                                                            <ItemTemplate>
                                                                <span style="color: Red;">
                                                                    <%#HPCBusinessLogic.UltilFunc.GetUserName(Eval("News_EditorID"))%>
                                                                </span>
                                                            </ItemTemplate>
                                                        </asp:TemplateColumn>
                                                        <asp:TemplateColumn HeaderText="<%$Resources:cms.language, lblXem%>">
                                                            <HeaderStyle HorizontalAlign="Center" Width="5%"></HeaderStyle>
                                                            <ItemStyle HorizontalAlign="Center" Width="5%"></ItemStyle>
                                                            <ItemTemplate>
                                                                <a target="_blank" href="<%=Global.ApplicationPath%>/View/ViewDetails.aspx?Menu_ID=<%=Page.Request.QueryString["Menu_ID"]%>&ID=<%# DataBinder.Eval(Container.DataItem, "News_ID") %>" />
                                                                <img src="<%=Global.ApplicationPath%>/Dungchung/images/view.gif" border="0" alt="Xem"
                                                                    onmouseover="(window.status=''); return true" style="cursor: pointer;" title="Xem">
                                                            </ItemTemplate>
                                                        </asp:TemplateColumn>
                                                        <asp:TemplateColumn>
                                                            <HeaderStyle HorizontalAlign="center" Width="5%"></HeaderStyle>
                                                            <ItemStyle HorizontalAlign="center" Width="5%"></ItemStyle>
                                                            <HeaderTemplate>
                                                                <img style="cursor: pointer;" src='<%= Global.ApplicationPath%>/Dungchung/images/doc.gif'
                                                                    border="0" title="Download nội dung bài viết" alt="Download nội dung bài viết">
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Dungchung/Images/doc.gif"
                                                                    ToolTip="Export word" CommandName="Edit" CommandArgument="DownLoadAlias" />
                                                            </ItemTemplate>
                                                        </asp:TemplateColumn>
                                                        <asp:TemplateColumn HeaderText="<%$Resources:cms.language, lblMokhoa%>">
                                                            <HeaderStyle HorizontalAlign="Center" Width="5%"></HeaderStyle>
                                                            <ItemStyle HorizontalAlign="Center" Width="5%"></ItemStyle>
                                                            <ItemTemplate>
                                                                <asp:ImageButton ID="btnIsUnlock" Width="15px" runat="server" ImageUrl="~/Dungchung/Images/Icons/uncheck.gif"
                                                                    ImageAlign="AbsMiddle" ToolTip="Unlock" CommandName="Edit" CommandArgument="IsUnlock"
                                                                    BorderStyle="None"></asp:ImageButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateColumn>
                                                        <asp:TemplateColumn HeaderText="<%$Resources:cms.language, lblNgonNgu%>">
                                                            <HeaderStyle HorizontalAlign="Center" Width="10%"></HeaderStyle>
                                                            <ItemStyle HorizontalAlign="Center" Width="10%"></ItemStyle>
                                                            <ItemTemplate>
                                                                <%#HPCBusinessLogic.UltilFunc.GetTenNgonNgu(Eval("Lang_ID"))%>
                                                            </ItemTemplate>
                                                        </asp:TemplateColumn>
                                                    </Columns>
                                                </asp:DataGrid>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: right" class="pageNav">
                                                <cc1:CurrentPage runat="server" ID="curentPages" CssClass="pageNavTotal"></cc1:CurrentPage>
                                                &nbsp;
                                                <cc1:Pager runat="server" ID="pages" OnIndexChanged="pages_IndexChanged" />
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
