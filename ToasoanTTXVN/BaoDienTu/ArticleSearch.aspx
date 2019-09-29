<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/ITC.Master" AutoEventWireup="true"
    CodeBehind="ArticleSearch.aspx.cs" Inherits="ToasoanTTXVN.BaoDienTu.ArticleSearch" %>

<%@ Register TagPrefix="nbc" Namespace="HPCComponents.UI" Assembly="HPCComponents" %>
<%@ Register TagPrefix="anthem" Namespace="Anthem" Assembly="Anthem" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<%@ Register TagPrefix="cc1" Namespace="HPCComponents.UI.WebControls" Assembly="HPCComponents" %>
<%@ Import Namespace="HPCComponents" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
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
                            <span class="TitlePanel"><%= CommonLib.ReadXML("titTimkiem") %></span>
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
                    <table border="0" cellpadding="1" cellspacing="1" style="width: 100%">
                        <tr>
                            <td style="width: 7%; text-align: right" class="Titlelbl">
                                <asp:Label ID="Label1" class="Titlelbl" runat="server" 
                                                Text="<%$Resources:cms.language, lblAnpham%>"></asp:Label>
                            </td>
                            <td style="width: 20%; text-align: left">
                                <anthem:DropDownList AutoCallBack="true" ID="cboNgonNgu" runat="server" Width="180px"
                                    CssClass="inputtext" DataTextField="Languages_Name" DataValueField="Languages_ID"
                                    OnSelectedIndexChanged="cbo_lanquage_SelectedIndexChanged" TabIndex="2">
                                </anthem:DropDownList>
                            </td>
                            <td style="width: 7%; text-align: right">
                                <asp:Label ID="lblChuyenmuc" class="Titlelbl" runat="server" 
                                                Text="<%$Resources:cms.language, lblChuyenmuc%>"></asp:Label>
                            </td>
                            <td style="width: 20%; text-align: left;">
                                <anthem:DropDownList AutoCallBack="true" ID="cbo_chuyenmuc" CssClass="inputtext"
                                    runat="server" Width="200px" DataTextField="tenchuyenmuc" DataValueField="id"
                                    TabIndex="3">
                                </anthem:DropDownList>
                            </td>
                            <td style="width: 7%; text-align: right">
                                <asp:Label ID="Label2" class="Titlelbl" runat="server" 
                                                Text="<%$Resources:cms.language, lblTenbaiviet%>"></asp:Label>
                            </td>
                            <td style="width: 29%; text-align: left;">
                                <asp:TextBox ID="txt_tieude" CssClass="inputtext" runat="server" Width="90%" onkeypress="return clickButton(event,'ctl00_MainContent_cmdSeek');"></asp:TextBox>
                            </td>
                            <td style="width: 10%; text-align: center;">
                                <asp:Button ID="cmd_Search" CssClass="iconFind" Font-Bold="true" runat="server" Text="<%$Resources:cms.language, lblTimkiem%>"
                                    OnClick="cmd_Search_Click" />
                            </td>
                            <td>
                                <asp:Panel ID="AdvancedSearch_HeaderPanel" runat="server" Style="cursor: pointer;">
                                    <div class="heading">
                                        <asp:ImageButton ID="AdvancedSearch_ToggleImage" runat="server" ImageUrl="~/Dungchung/images/collapse.jpg"
                                            AlternateText="collapse" />
                                    </div>
                                </asp:Panel>
                            </td>
                        </tr>
                    </table>
                </div>
                <hr style="height: 1px; border: 0px; color: #ffffff" />
                <asp:Panel ID="AdvancedSearch_ContentPanel" CssClass="classSearchAdvance" runat="server"
                    Style="overflow: hidden;">
                    <table border="0" cellpadding="1" cellspacing="0" style="width: 100%">
                        <tr>
                            <td style="height: 4px">
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: left; width: 20%;" class="Titlelbl">
                                <asp:Label ID="Label3" runat="server" Text="<%$Resources:cms.language, lblTrangthai%>"></asp:Label>
                            </td>
                            <td style="width: 20%; text-align: left" class="Titlelbl">
                                <asp:Label ID="Label4" runat="server" Text="<%$Resources:cms.language, lblTacgia%>"></asp:Label>
                            </td>
                            <td style="width: 20%; text-align: left" class="Titlelbl">
                                <asp:Label ID="Label5" runat="server" Text="<%$Resources:cms.language, lblTungay%>"></asp:Label>
                            </td>
                            <td style="text-align: left; width: 20%" class="Titlelbl">
                                <asp:Label ID="Label6" runat="server" Text="<%$Resources:cms.language, lblDenngay%>"></asp:Label>
                            </td>
                            <td style="width: 20%; text-align: left" class="Titlelbl">
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: left; vertical-align: top">
                                <asp:DropDownList ID="Drop_Status_Public" CssClass="inputtext" runat="server" Width="80%">
                                    <asp:ListItem Value="0" Text="<%$Resources:cms.language, lblTatca%>"></asp:ListItem>
                                    <asp:ListItem Value="12" Text="<%$Resources:cms.language, lblTinmoi%>"></asp:ListItem>
                                    <asp:ListItem Value="13" Text="<%$Resources:cms.language, lblTralainguoinhaptin%>"></asp:ListItem>
                                    <asp:ListItem Value="72" Text="<%$Resources:cms.language, lblChotrinhbay%>"></asp:ListItem>
                                    <asp:ListItem Value="73" Text="<%$Resources:cms.language, lblTralaitrinhbay%>"> </asp:ListItem>
                                    <asp:ListItem Value="82" Text="<%$Resources:cms.language, lblChobientap%>"></asp:ListItem>
                                    <asp:ListItem Value="83" Text="<%$Resources:cms.language, lblTralaibientap%>"></asp:ListItem>
                                    <asp:ListItem Value="92" Text="<%$Resources:cms.language, lblBaichoduyet%>"></asp:ListItem>
                                    <asp:ListItem Value="4" Text="<%$Resources:cms.language, lblTinngungdang%>"></asp:ListItem>
                                    <asp:ListItem Value="6" Text="<%$Resources:cms.language, lblTinxuatban%>"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td style="text-align: left; vertical-align: top">
                                <asp:DropDownList CssClass="inputtext" Width="190px" ID="Drop_Tacgia" runat="server">
                                </asp:DropDownList>
                            </td>
                            <td style="text-align: left; vertical-align: top">
                                <nbc:NetDatePicker ImageUrl="../Dungchung/Images/events.gif" ImageFolder="../Dungchung/Scripts/DatePicker/Images"
                                    CssClass="inputtext" Width="130px" ScriptSource="../Dungchung/Scripts/datepicker.js"
                                    ID="txt_tungay" runat="server" onKeyPress="AscciiDisable()" onfocus="javascript:vDateType='3'"
                                    onKeyUp="DateFormat(this,this.value,event,false,'3')" onBlur="DateFormat(this,this.value,event,true,'3')"></nbc:NetDatePicker>
                                <asp:RegularExpressionValidator ID="Regularexpressionvalidator2" runat="server" ControlToValidate="txt_tungay"
                                    ValidationExpression="^\d{1,2}/\d{1,2}/\d{4}$" Display="Dynamic"><%= CommonLib.ReadXML("lblKieungay") %></asp:RegularExpressionValidator><br />
                            </td>
                            <td style="text-align: left; vertical-align: top">
                                <nbc:NetDatePicker ImageUrl="../Dungchung/Images/events.gif" ImageFolder="../Dungchung/Scripts/DatePicker/Images"
                                    CssClass="inputtext" Width="130px" ScriptSource="../Dungchung/Scripts/datepicker.js"
                                    ID="txt_denngay" runat="server" onKeyPress="AscciiDisable()" onfocus="javascript:vDateType='3'"
                                    onKeyUp="DateFormat(this,this.value,event,false,'3')" onBlur="DateFormat(this,this.value,event,true,'3')"></nbc:NetDatePicker>
                                <asp:RegularExpressionValidator ID="Regularexpressionvalidator1" runat="server" ControlToValidate="txt_denngay"
                                    ValidationExpression="^\d{1,2}/\d{1,2}/\d{4}$" Display="Dynamic"><%= CommonLib.ReadXML("lblKieungay") %></asp:RegularExpressionValidator><br />
                            </td>
                            <td style="text-align: left; vertical-align: top">
                                <asp:CheckBox runat="server" Text="<%$Resources:cms.language, lblNoibatchuyenmuc%>" ID="chkNewFocusChild" CssClass="Titlelbl"
                                    TabIndex="16" Visible="true" />
                            </td>
                        </tr>
                        <%--<tr>
                            <td style="text-align: left; width: 100%; height: 10px;" colspan="5" class="Titlelbl">
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: left; width: 20%;" class="Titlelbl">
                                <asp:CheckBox runat="server" Text="Bài đinh" ID="chkNewsIsBaidinh" CssClass="Titlelbl"
                                    TabIndex="15" Visible="true" />
                                <asp:CheckBox runat="server" Text="Tin nóng trang chủ" ID="chkNewsIsFocus" CssClass="Titlelbl"
                                    TabIndex="16" Visible="true" />
                            </td>
                            <td style="width: 20%; text-align: left" class="Titlelbl">
                                <asp:CheckBox runat="server" Text="Tin tiêu điểm" ID="chkNewTieudiem" CssClass="Titlelbl"
                                    TabIndex="16" Visible="true" />
                                <asp:CheckBox runat="server" Text="Nổi bật CM cha" ID="chkNewFocusParent" CssClass="Titlelbl"
                                    TabIndex="16" Visible="true" />
                            </td>
                            <td style="width: 20%; text-align: left" colspan="3" class="Titlelbl">
                                
                                <asp:CheckBox runat="server" Text="Tin ảnh" ID="chkImageIsFocus" CssClass="Titlelbl"
                                    TabIndex="16" Visible="true" />
                                <asp:CheckBox runat="server" Text="Tin video" ID="chkVideoIsFocus" CssClass="Titlelbl"
                                    TabIndex="16" Visible="true" />
                                <asp:CheckBox runat="server" Text="Tin ẩn" ID="chkHosoIsFocus" CssClass="Titlelbl"
                                    TabIndex="16" Visible="true" />
                                <asp:CheckBox runat="server" Text="Không HT Mobile" ID="cbDisplayMobi" CssClass="Titlelbl"
                                    TabIndex="16" Visible="true" />
                                <asp:CheckBox runat="server" Text="Tin được quan tâm" ID="cbMoreViews" CssClass="Titlelbl"
                                    TabIndex="16" Visible="true" />
                            </td>
                        </tr>--%>
                    </table>
                </asp:Panel>
                <hr style="height: 1px; border: 0px; color: #f1f1f1" />
                <asp:Panel ID="Panel_DS_Ketqua" runat="server" Visible="false">
                    <div class="classSearchHeader">
                        <table border="0" width="100%" cellpadding="1" cellspacing="1">
                            <tr>
                                <td>
                                    <asp:DataGrid ID="dgr_tintuc" runat="server" Width="100%" AutoGenerateColumns="False"
                                        DataKeyField="News_ID" CssClass="Grid" OnEditCommand="dgData_EditCommand" OnItemDataBound="dgData_ItemDataBound">
                                        <ItemStyle CssClass="GridItem"></ItemStyle>
                                        <AlternatingItemStyle CssClass="GridAltItem" />
                                        <HeaderStyle CssClass="GridHeader"></HeaderStyle>
                                        <Columns>
                                            <asp:TemplateColumn HeaderText="<%$Resources:cms.language, lblThutu%>">
                                                <HeaderStyle HorizontalAlign="Center" Width="3%"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                <ItemTemplate>
                                                    <%# pages.PageIndex * 10 + Container.ItemIndex + 1%>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:BoundColumn DataField="News_ID" HeaderText="News_ID" Visible="False"></asp:BoundColumn>
                                            <asp:TemplateColumn HeaderText="<%$Resources:cms.language, lblTenbaiviet%>">
                                                <HeaderStyle HorizontalAlign="Left" Width="22%"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                <ItemTemplate>
                                                    <a class="linkGridForm" target="_blank" href="<%=Global.ApplicationPath%>/View/ViewHistory.aspx?Menu_ID=<%=Page.Request.QueryString["Menu_ID"]%>&ID=<%# DataBinder.Eval(Container.DataItem, "News_ID") %>">
                                                        <%# DataBinder.Eval(Container.DataItem, "News_Tittle" )%></a>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn  HeaderText="<%$Resources:cms.language, lblAnpham%>">
                                                <HeaderStyle Width="8%" HorizontalAlign="Center"></HeaderStyle>
                                                <ItemStyle Width="8%" HorizontalAlign="Center"></ItemStyle>
                                                <ItemTemplate>
                                                    <%#HPCBusinessLogic.UltilFunc.GetTenAnpham(Eval("Lang_ID"))%>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn  HeaderText="<%$Resources:cms.language, lblChuyenmuc%>">
                                                <HeaderStyle Width="10%" HorizontalAlign="Center"></HeaderStyle>
                                                <ItemStyle Width="10%" HorizontalAlign="Left"></ItemStyle>
                                                <ItemTemplate>
                                                    <%#HPCBusinessLogic.UltilFunc.GetCategoryName(Eval("CAT_ID"))%>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="<%$Resources:cms.language, lblTrangthai%>">
                                                <HeaderStyle HorizontalAlign="Center" Width="10%"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Left" Width="10%"></ItemStyle>
                                                <ItemTemplate>
                                                    <%#HPCComponents.Global.GetStatusT_NewsFrom_T_version(Eval("News_ID"))%>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="<%$Resources:cms.language, lblNguoinhap%>">
                                                <HeaderStyle HorizontalAlign="Center" Width="8%"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center" Width="8%"></ItemStyle>
                                                <ItemTemplate>
                                                    <%#HPCBusinessLogic.UltilFunc.GetUserName(Eval("News_AuthorID"))%>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn  HeaderText="<%$Resources:cms.language, lblNgaynhap%>">
                                                <ItemTemplate>
                                                    <asp:Label ID="ngaytao" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.News_DateCreated")!=System.DBNull.Value?Convert.ToDateTime(DataBinder.Eval(Container, "DataItem.News_DateCreated")).ToString("dd/MM/yyyy HH:mm"):"" %>'>
                                                    </asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" Width="10%"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="<%$Resources:cms.language, lblNguoiXB%>">
                                                <HeaderStyle HorizontalAlign="Center" Width="8%"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center" Width="8%"></ItemStyle>
                                                <ItemTemplate>
                                                    <%#HPCBusinessLogic.UltilFunc.GetUserName(Eval("News_PublishedID"))%>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="<%$Resources:cms.language, lblNgayXB%>">
                                                <HeaderStyle HorizontalAlign="Center" Width="10%"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                <ItemTemplate>
                                                    <%#Eval("News_DatePublished") != System.DBNull.Value ? Convert.ToDateTime(Eval("News_DatePublished")).ToString("dd/MM/yyyy HH:mm") : ""%>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <%-- <asp:TemplateColumn>
                                            <HeaderStyle HorizontalAlign="Center" Width="10%"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Center" Width="10%"></ItemStyle>
                                            <HeaderTemplate>
                                                Loại bài viết
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <%#GetstatusNoibat(Eval("News_Priority"), Eval("News_IsFocus"), Eval("News_IsHot"))%>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>--%>
                                            <asp:TemplateColumn>
                                                <HeaderStyle HorizontalAlign="Center" Width="5%"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center" Width="5%" CssClass="GridBorderVerSolid"></ItemStyle>
                                                <HeaderTemplate>
                                                    <img style="cursor: hand" src='<%= Global.ApplicationPath%>/Dungchung/images/History.png'
                                                        border="0" title="Xem lịch sử bài viết" alt='Xem lịch sử bài viết'>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <a target="_blank" href="<%=Global.ApplicationPath%>/View/ViewHistory.aspx?Menu_ID=<%=Page.Request.QueryString["Menu_ID"]%>&ID=<%# DataBinder.Eval(Container.DataItem, "News_ID") %>" />
                                                    <img src='<%= Global.ApplicationPath %>/Dungchung/images/History.png' border="0"
                                                        alt="Xem lịch sử bài viết" onmouseover="(window.status=''); return true" style="cursor: pointer;"
                                                        title="Xem lịch sử bài viết">
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="<%$Resources:cms.language, lblXem%>">
                                                <HeaderStyle HorizontalAlign="Center" Width="3%"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center" Width="3%"></ItemStyle>
                                                <ItemTemplate>
                                                    <a target="_blank" href="<%=Global.ApplicationPath%>/View/ViewDetails.aspx?Menu_ID=<%=Page.Request.QueryString["Menu_ID"]%>&ID=<%# DataBinder.Eval(Container.DataItem, "News_ID") %>" />
                                                    <img src='<%= Global.ApplicationPath %>/Dungchung/images/view.gif' border="0" alt="Xem tin"
                                                        onmouseover="(window.status=''); return true" style="cursor: pointer;" title="Xem tin">
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn>
                                                <HeaderStyle HorizontalAlign="Center" Width="3%"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center" Width="3%"></ItemStyle>
                                                <HeaderTemplate>
                                                    <img style="cursor: hand" src='<%= Global.ApplicationPath%>/Dungchung/images/doc.gif'
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
                                    <table border="0" width="100%">
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
                        </table>
                    </div>
                </asp:Panel>
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
    <cc2:CollapsiblePanelExtender ID="cpeDescription" runat="Server" TargetControlID="AdvancedSearch_ContentPanel"
        ExpandControlID="AdvancedSearch_HeaderPanel" CollapseControlID="AdvancedSearch_HeaderPanel"
        Collapsed="False" ImageControlID="AdvancedSearch_ToggleImage" />
</asp:Content>
