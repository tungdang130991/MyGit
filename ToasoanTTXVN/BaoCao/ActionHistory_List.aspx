<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/ITC.Master" AutoEventWireup="true"
    CodeBehind="ActionHistory_List.aspx.cs" Inherits="ToasoanTTXVN.BaoCao.ActionHistory_List" %>

<%@ Register TagPrefix="cc1" Namespace="HPCComponents.UI.WebControls" Assembly="HPCComponents" %>
<%@ Import Namespace="HPCComponents" %>
<%@ Register TagPrefix="nbc" Namespace="HPCComponents.UI" Assembly="HPCComponents" %>
<%@ Register TagPrefix="anthem" Namespace="Anthem" Assembly="Anthem" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Panel ID="pnList" runat="server" Width="100%" Visible="true">
        <table border="0" cellpadding="0" width="100%" cellspacing="0">
            <tr>
                <td class="datagrid_top_left">
                </td>
                <td class="datagrid_top_center">
                    <table border="0" cellpadding="1" cellspacing="1" style="float: left;">
                        <tr>
                            <td>
                                <img src="../Dungchung/Images/Icons/to-do-list-cheked-all-icon.png" width="16px" height="16px" />
                            </td>
                            <td style="vertical-align: middle">
                                <span class="TitlePanel"><%= CommonLib.ReadXML("lblTimkiemthongtin")%></span>
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
                                <td style="text-align: right; width: 10%;" class="Titlelbl">
                                    <%= CommonLib.ReadXML("lblLoaitin")%>:
                                </td>
                                <td style="text-align: left; width: 20%;">
                                    <asp:DropDownList AutoCallBack="true" ID="cbo_types" runat="server" CssClass="inputtext"
                                        Width="206px" TabIndex="2">
                                        <asp:ListItem Value="1" Text="<%$Resources:cms.language, lblBaodientu%>"></asp:ListItem>
                                        <asp:ListItem Value="2" Text="<%$Resources:cms.language, lblPhongsuanh%>"></asp:ListItem>
                                        <asp:ListItem Value="3" Text="<%$Resources:cms.language, lblAmthanhhinhanh%>"></asp:ListItem>
                                        <%--<asp:ListItem Value="4" Text="Thời sự qua ảnh"></asp:ListItem>--%>
                                    </asp:DropDownList>
                                </td>
                                <td style="text-align: right; width: 10%;" class="Titlelbl">
                                    <%= CommonLib.ReadXML("lblNgonNgu")%>:
                                </td>
                                <td style="text-align: left; width: 20%;">
                                    <anthem:DropDownList AutoCallBack="true" ID="cboNgonNgu" runat="server" CssClass="inputtext"
                                        Width="206px" OnSelectedIndexChanged="cbo_lanquage_SelectedIndexChanged" TabIndex="2">
                                    </anthem:DropDownList>
                                </td>
                                <td style="text-align: right; width: 10%;" class="Titlelbl">
                                    <%= CommonLib.ReadXML("lblChuyenmuc")%>:
                                </td>
                                <td style="text-align: left; width: 20%;">
                                    <anthem:DropDownList AutoCallBack="true" ID="cbo_chuyenmuc" runat="server" CssClass="inputtext"
                                        Width="206px" TabIndex="3">
                                    </anthem:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: right" class="Titlelbl">
                                    <%= CommonLib.ReadXML("lblTungay")%>:
                                </td>
                                <td style="text-align: left">
                                    <nbc:NetDatePicker ImageUrl="../Dungchung/Images/events.gif" CssClass="inputtext" ImageFolder="../Dungchung/scripts/DatePicker/Images"
                                        Height="16px" Width="150px" ScriptSource="../Dungchung/Scripts/datepicker.js" ID="txt_FromDate"
                                        runat="server" onKeyPress="AscciiDisable()" onfocus="javascript:vDateType='3'"
                                        onKeyUp="DateFormat(this,this.value,event,false,'3')" onBlur="DateFormat(this,this.value,event,true,'3')"></nbc:NetDatePicker>
                                    <asp:RegularExpressionValidator ID="Regularexpressionvalidator3" runat="server" ControlToValidate="txt_FromDate"
                                        ValidationExpression="^\d{1,2}/\d{1,2}/\d{4}$" Display="Dynamic"><%= CommonLib.ReadXML("lblKieungay")%></asp:RegularExpressionValidator>
                                </td>
                                <td style="text-align: right" class="Titlelbl">
                                    <%= CommonLib.ReadXML("lblDenngay")%>:
                                </td>
                                <td style="text-align: left">
                                    <nbc:NetDatePicker ImageUrl="../Dungchung/Images/events.gif" CssClass="inputtext" ImageFolder="../Dungchung/Scripts/DatePicker/Images"
                                        Height="16px" Width="150px" ScriptSource="../Dungchung/Scripts/datepicker.js" ID="txt_ToDate"
                                        runat="server" onKeyPress="AscciiDisable()" onfocus="javascript:vDateType='3'"
                                        onKeyUp="DateFormat(this,this.value,event,false,'3')" onBlur="DateFormat(this,this.value,event,true,'3')"></nbc:NetDatePicker>
                                    <asp:RegularExpressionValidator ID="Regularexpressionvalidator1" runat="server" ControlToValidate="txt_ToDate"
                                        ValidationExpression="^\d{1,2}/\d{1,2}/\d{4}$" Display="Dynamic"><%= CommonLib.ReadXML("lblKieungay")%></asp:RegularExpressionValidator><br />
                                </td>
                                <td style="text-align: right" class="Titlelbl">
                                    <%= CommonLib.ReadXML("lblTieude")%>:
                                </td>
                                <td style="text-align: left">
                                    <asp:TextBox runat="server" ID="txtTieude" Width="90%" CssClass="inputtext"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="6" style="text-align: right;margin-right:50px">
                                    <asp:Button runat="server" style="margin-right:19px;" CssClass="iconFind" ID="lbkSearch" Font-Bold="true" OnClick="lbkSearch_Click"
                                        Text="<%$Resources:cms.language, lblTimkiem%>" />
                                </td>
                            </tr>
                        </table>
                    </div>
                    <br />
                    <div class="classSearchHeader">
                        <table cellpadding="0" cellspacing="0" border="0" width="100%">
                            <tr>
                                <td valign="top">
                                    <table width="100%" cellspacing="2" cellpadding="2" border="0">
                                        <tr>
                                            <td align="left">
                                                <asp:DataGrid runat="server" ID="grdList" AutoGenerateColumns="false" DataKeyField="News_ID"
                                                    BorderColor="#D9D9D9" CellPadding="4" AlternatingItemStyle-BackColor="#F1F1F2"
                                                    OnEditCommand="grdList_EditCommand" OnItemDataBound="grdList_ItemDataBound" BackColor="White"
                                                    Width="100%" BorderWidth="1px" CssClass="Grid">
                                                    <SelectedItemStyle Font-Bold="True" ForeColor="#CCFF99" BackColor="#009999"></SelectedItemStyle>
                                                    <ItemStyle CssClass="GridItem"></ItemStyle>
                                                    <AlternatingItemStyle CssClass="GridAltItem" />
                                                    <HeaderStyle CssClass="GridHeader"></HeaderStyle>
                                                    <Columns>
                                                        <asp:BoundColumn Visible="False" DataField="News_ID">
                                                            <HeaderStyle Width="1%"></HeaderStyle>
                                                        </asp:BoundColumn>
                                                        <asp:TemplateColumn HeaderText="<%$Resources:cms.language, lblSTT%>">
                                                            <HeaderStyle HorizontalAlign="Center" Width="5%"></HeaderStyle>
                                                            <HeaderStyle Width="5%" HorizontalAlign="Center" />
                                                            <ItemStyle Width="5%" HorizontalAlign="Center"></ItemStyle>
                                                            <ItemTemplate>
                                                                <%# pages.PageIndex * 20 + Container.ItemIndex + 1%>
                                                            </ItemTemplate>
                                                        </asp:TemplateColumn>
                                                        <asp:TemplateColumn HeaderText="<%$Resources:cms.language, lblTieude%>">
                                                            <ItemStyle Width="40%" HorizontalAlign="Left"></ItemStyle>
                                                            <ItemTemplate>
                                                                <asp:Image ID="img" runat="server" ToolTip='<%# BindToolTip(DataBinder.Eval(Container.DataItem, "News_Status").ToString())%>'
                                                                    ImageUrl='<%# ImageStatus(DataBinder.Eval(Container.DataItem, "News_Status").ToString())%>' />
                                                                <asp:LinkButton ID="lbkView" runat="server" Text=' <%# Eval("News_Tittle")%>' CommandName="Edit"
                                                                    CommandArgument="Edit" CssClass="linkEdit">
                                                                </asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateColumn>
                                                        <asp:TemplateColumn HeaderText="<%$Resources:cms.language, lblChuyenmuc%>">
                                                            <ItemStyle Width="15%" HorizontalAlign="Left"></ItemStyle>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblcate" runat="server" Text='<%#HPCBusinessLogic.UltilFunc.GetCategoryName(Eval("CAT_ID"))%>'
                                                                    CssClass="linkGridForm">
                                                                </asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateColumn>
                                                        <asp:TemplateColumn HeaderText="<%$Resources:cms.language, lblNgaynhap%>">
                                                            <HeaderStyle HorizontalAlign="Center" Width="10%"></HeaderStyle>
                                                            <ItemStyle Width="10%" HorizontalAlign="Center"></ItemStyle>
                                                            <ItemTemplate>
                                                                <%# DataBinder.Eval(Container, "DataItem.News_DateCreated") != System.DBNull.Value ? Convert.ToDateTime(DataBinder.Eval(Container, "DataItem.News_DateCreated")).ToString("dd/MM/yyyy HH:mm") : ""%>
                                                            </ItemTemplate>
                                                        </asp:TemplateColumn>
                                                        <asp:TemplateColumn HeaderText="<%$Resources:cms.language, lblNgayXB%>">
                                                            <HeaderStyle HorizontalAlign="Center" Width="10%"></HeaderStyle>
                                                            <ItemStyle Width="10%" HorizontalAlign="Center"></ItemStyle>
                                                            <ItemTemplate>
                                                                <%# DataBinder.Eval(Container, "DataItem.News_DatePublished") != System.DBNull.Value ? Convert.ToDateTime(DataBinder.Eval(Container, "DataItem.News_DatePublished")).ToString("dd/MM/yyyy HH:mm") : ""%>
                                                            </ItemTemplate>
                                                        </asp:TemplateColumn>
                                                        <asp:TemplateColumn HeaderText="<%$Resources:cms.language, lblLuottruycap%>">
                                                            <HeaderStyle HorizontalAlign="Center" Width="10%"></HeaderStyle>
                                                            <ItemStyle Width="10%" HorizontalAlign="Center"></ItemStyle>
                                                            <ItemTemplate>
                                                                <%#GetNumberOfRead(Eval("NumberOfRead"))%>
                                                            </ItemTemplate>
                                                        </asp:TemplateColumn>
                                                    </Columns>
                                                </asp:DataGrid>
                                                <div style="clear: both; float: right;">
                                                    <div id="pagenav1" class="pageNav">
                                                        <cc1:CurrentPage runat="server" ID="currentPage"></cc1:CurrentPage>
                                                        &nbsp;<cc1:Pager runat="server" ID="pages" OnIndexChanged="pages_IndexChanged" />
                                                    </div>
                                                </div>
                                            </td>
                                        </tr>
                                    </table>
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
    </asp:Panel>
    <asp:Panel ID="pnView" runat="server" Width="100%" Visible="false">
        <table cellpadding="0" cellspacing="0" border="0" width="100%">
            <tr>
                <td class="datagrid_top_left">
                </td>
                <td class="datagrid_top_center" style="text-align: left;">
                    <asp:Button runat="server" Font-Bold="true" ID="lbkBack" style="margin-top:10px; margin-bottom:10px" CssClass="iconExit" OnClick="lbkBack_OnClick"
                    Text="Quay lại" />
                </td>
                <td class="datagrid_top_right">
                </td>
            </tr>
            <tr>
                <td class="datagrid_content_left">
                </td>
                <td>
                    <asp:DataGrid CssClass="Grid" runat="server" ID="grdDetail" AutoGenerateColumns="false"
                        DataKeyField="Log_ID" BorderColor="#7F8080" CellPadding="0" AlternatingItemStyle-BackColor="#F1F1F2"
                        BackColor="White" Width="100%" BorderWidth="1px" BorderStyle="None" OnItemDataBound="grdDetail_ItemDataBound">
                        <ItemStyle CssClass="GridItem"></ItemStyle>
                        <AlternatingItemStyle CssClass="GridAltItem" />
                        <HeaderStyle CssClass="GridHeader"></HeaderStyle>
                        <Columns>
                            <asp:BoundColumn Visible="False" DataField="Log_ID">
                                <HeaderStyle Width="1%"></HeaderStyle>
                            </asp:BoundColumn>
                            <asp:TemplateColumn>
                                <HeaderStyle HorizontalAlign="Left" Width="15%" />
                                <ItemStyle HorizontalAlign="Left" Width="15%" CssClass="GridBorderVerSolid"></ItemStyle>
                                <HeaderTemplate>
                                    Tên người dùng
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <%# Eval("FullName")%>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn>
                                <HeaderStyle HorizontalAlign="Center" Width="10%"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center" Width="10%" CssClass="GridBorderVerSolid"></ItemStyle>
                                <HeaderTemplate>
                                    IP
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <%# Eval("HostIP")%>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn>
                                <HeaderStyle HorizontalAlign="Center" Width="10%"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center" Width="10%" CssClass="GridBorderVerSolid"></ItemStyle>
                                <HeaderTemplate>
                                    Thời gian
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <%#Eval("DateModify") != System.DBNull.Value ? Convert.ToDateTime(Eval("DateModify")).ToString("dd/MM/yyyy HH:mm:ss") : ""%>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn>
                                <HeaderStyle HorizontalAlign="Left" Width="50%"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Left" Width="50%" CssClass="GridBorderVerSolid"></ItemStyle>
                                <HeaderTemplate>
                                    Thao tác
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <%# Eval("ActionsCode")%>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                        </Columns>
                    </asp:DataGrid>
                </td>
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
    </asp:Panel>
</asp:Content>
