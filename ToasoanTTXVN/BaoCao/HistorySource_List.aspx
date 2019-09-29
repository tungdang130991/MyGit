<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/ITC.Master" AutoEventWireup="true" CodeBehind="HistorySource_List.aspx.cs" Inherits="ToasoanTTXVN.BaoCao.HistorySource_List" %>
<%@ Register TagPrefix="cc1" Namespace="HPCComponents.UI.WebControls" Assembly="HPCComponents" %>
<%@ Register TagPrefix="nbc" Namespace="HPCComponents.UI" Assembly="HPCComponents" %>
<%@ Import Namespace="HPCComponents" %>
<%@ Import Namespace="HPCBusinessLogic" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <table border="0" cellpadding="0" width="100%" cellspacing="0">
        <tr>
            <td class="datagrid_top_left">
            </td>
            <td class="datagrid_top_center">
                <table border="0" cellpadding="1" cellspacing="1">
                    <tr>
                        <td>
                            <img src="../Dungchung/Images/Icons/cog-edit-icon.png" width="16px" height="16px" />
                        </td>
                        <td style="vertical-align: middle">
                            <span class="TitlePanel" style="float: left;"><%= CommonLib.ReadXML("lblDstracuu")%></span>
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
                            <div class="divPanelSearch">
                                <div style="width: 99%; padding: 5px 0.5%">
                                    <table border="0" cellpadding="1" cellspacing="1" style="width: 100%; text-align: right">
                                        <tr>
                                            <td style="width: 10%; text-align: right" class="Titlelbl">
                                                <%= CommonLib.ReadXML("lblTendangnhap")%>:
                                            </td>
                                            <td style="width: 15%; text-align: right" class="Titlelbl">
                                                <asp:DropDownList ID="ddlTenTruyCap" TabIndex="3" runat="server" Width="200px" CssClass="inputtext">
                                                </asp:DropDownList>
                                            </td>
                                            <td style="width: 10%; text-align: right" class="Titlelbl">
                                                <%= CommonLib.ReadXML("lblTracuu")%>:
                                            </td>
                                            <td style="width: 15%; text-align: right" class="Titlelbl">
                                                <asp:DropDownList ID="ddlTraCuu" TabIndex="3" runat="server" Width="200px" CssClass="inputtext">
                                                </asp:DropDownList>
                                            </td>
                                            <td style="width: 10%; text-align: right; padding-top: 10px;" class="Titlelbl">
                                                <%= CommonLib.ReadXML("lblTungay")%>:
                                            </td>
                                            <td style="width: 15%; text-align: left" class="Titlelbl">
                                                <nbc:NetDatePicker ImageUrl="../Dungchung/Images/events.gif" ImageFolder="../Dungchung/Scripts/DatePicker/Images"
                                                    Height="16px" Width="150px" ScriptSource="../Dungchung/Scripts/datepicker.js"
                                                    ID="txt_FromDate" runat="server" onKeyPress="AscciiDisable()" onfocus="javascript:vDateType='3'"
                                                    onKeyUp="DateFormat(this,this.value,event,false,'3')" onBlur="DateFormat(this,this.value,event,true,'3')"></nbc:NetDatePicker>
                                                <asp:RegularExpressionValidator ID="Regularexpressionvalidator2" runat="server" ControlToValidate="txt_FromDate"
                                                    ValidationExpression="^\d{1,2}/\d{1,2}/\d{4}$" Display="Dynamic"><%= CommonLib.ReadXML("lblKieungay") %></asp:RegularExpressionValidator>
                                            </td>
                                            <td style="width: 10%; text-align: right; padding-top: 10px;" class="Titlelbl">
                                                <%= CommonLib.ReadXML("lblDenngay")%>:
                                            </td>
                                            <td style="width: 15%; text-align: left" class="Titlelbl">
                                                <nbc:NetDatePicker ImageUrl="../Dungchung/Images/events.gif" ImageFolder="../Dungchung/Scripts/DatePicker/Images"
                                                    Height="16px" Width="150px" ScriptSource="../Dungchung/Scripts/datepicker.js"
                                                    ID="txt_ToDate" runat="server" onKeyPress="AscciiDisable()" onfocus="javascript:vDateType='3'"
                                                    onKeyUp="DateFormat(this,this.value,event,false,'3')" onBlur="DateFormat(this,this.value,event,true,'3')"></nbc:NetDatePicker>
                                                <asp:RegularExpressionValidator ID="Regularexpressionvalidator4" runat="server" ControlToValidate="txt_ToDate"
                                                    ValidationExpression="^\d{1,2}/\d{1,2}/\d{4}$" Display="Dynamic"><%= CommonLib.ReadXML("lblKieungay") %></asp:RegularExpressionValidator>
                                            </td>
                                            <td>
                                                <asp:Button runat="server" ID="Button1" CssClass="iconFind" Font-Bold="true" OnClick="cmdSeek_Click"
                                                    Text="<%$Resources:cms.language, lblTimkiem%>"></asp:Button>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 4px">
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left">
                            <div class="divPanelResult">
                                <asp:DataGrid ID="gdListActionHistorys" runat="server" Width="100%" AutoGenerateColumns="False"
                                    CssClass="Grid" BorderColor="#d4d4d4" CellPadding="0" DataKeyField="Log_ID" BorderStyle="None"
                                    BackColor="White" BorderWidth="1px" AlternatingItemStyle-BackColor="#F1F1F2"
                                    OnItemDataBound="gdListActionHistorys_ItemDataBound">
                                    <ItemStyle CssClass="GridItem" Height="28px"></ItemStyle>
                                    <AlternatingItemStyle CssClass="GridAltItem" />
                                    <HeaderStyle CssClass="GridHeader"></HeaderStyle>
                                    <Columns>
                                        <asp:TemplateColumn HeaderText="<%$Resources:cms.language, lblTendangnhap%>">
                                            <HeaderStyle Width="15%" HorizontalAlign="Center"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Center" CssClass="GridBorderVerSolid" />
                                            <ItemTemplate>
                                                <%# DataBinder.Eval(Container.DataItem, "UserName")%>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="<%$Resources:cms.language, lblThaotac%>">
                                            <HeaderStyle HorizontalAlign="Center" Width="20%"></HeaderStyle>
                                            <ItemStyle CssClass="GridBorderVerSolid" />
                                            <ItemTemplate>
                                                <%# DataBinder.Eval(Container.DataItem, "Note")%>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="<%$Resources:cms.language, lblTieude%>">
                                            <HeaderStyle HorizontalAlign="Center" Width="20%"></HeaderStyle>
                                            <ItemStyle CssClass="GridBorderVerSolid" />
                                            <ItemTemplate>
                                                <%# DataBinder.Eval(Container.DataItem, "Tittle")%>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="<%$Resources:cms.language, lblChuyenmuc%>">
                                            <HeaderStyle HorizontalAlign="Center" Width="15%"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Center" CssClass="GridBorderVerSolid" />
                                            <ItemTemplate>
                                                <%# DataBinder.Eval(Container.DataItem, "Category")%>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="<%$Resources:cms.language, lblNgaytracuu%>">
                                            <HeaderStyle Width="10%" HorizontalAlign="Center"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Center" CssClass="GridBorderVerSolid" />
                                            <ItemTemplate>
                                                <%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "DateCreated")).ToString("dd/MM/yyyy HH:mm:ss")%>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                    </Columns>
                                </asp:DataGrid>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left">
                            <table border="0" width="100%">
                                <tr>
                                    <td style="width: 40%">
                                    </td>
                                    <td style="text-align: right" class="pageNav">
                                        <cc1:CurrentPage runat="server" ID="curentPages"></cc1:CurrentPage>&nbsp;
                                        <cc1:Pager runat="server" ID="pages" OnIndexChanged="pages_IndexChanged" />
                                    </td>
                                </tr>
                            </table>
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
