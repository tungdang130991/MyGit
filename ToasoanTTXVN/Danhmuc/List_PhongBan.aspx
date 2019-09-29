<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/ITC.Master" AutoEventWireup="true"
    CodeBehind="List_PhongBan.aspx.cs" Inherits="ToasoanTTXVN.Danhmuc.List_PhongBan" %>

<%@ Register TagPrefix="cc1" Namespace="HPCComponents.UI.WebControls" Assembly="HPCComponents" %>
<%@ Import Namespace="HPCComponents" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <table border="0" cellpadding="0" width="100%" cellspacing="0">
        <tr>
            <td class="datagrid_top_left">
            </td>
            <td class="datagrid_top_center">
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
                        <td style="text-align: left; width: 100%">
                            <table border="0" cellpadding="1" cellspacing="1" style="width: 100%; text-align: left;">
                                <tr>
                                    <td style="text-align: center; width: 90%;">
                                        <asp:Label ID="lblMessError" runat="server" ForeColor="Red"></asp:Label>
                                    </td>
                                    <td style="text-align: right; width: 10%;">
                                        <asp:Button runat="server" ID="btnAdd" CssClass="iconAdd" OnClick="btnAdd_Click"
                                            Text="<%$Resources:cms.language, lblThemmoi%>"></asp:Button>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="height: 4px">
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 100%" colspan="2">
                                        <asp:GridView ID="GridViewPhongBan" runat="Server" AutoGenerateColumns="False" BackColor="White"
                                            CssClass="Grid" AllowSorting="true" Width="100%" OnRowDataBound="GridViewPhongBan_OnRowDataBound"
                                            OnRowCommand="GridViewPhongBan_OnRowCommand" OnRowDeleting="GridViewPhongBan_RowDeleting"
                                            OnRowEditing="GridViewPhongBan_RowEditing" OnRowUpdating="GridViewPhongBan_RowUpdating"
                                            OnRowCancelingEdit="GridViewPhongBan_RowCancelingEdit" ShowFooter="False" AutoGenerateEditButton="false"
                                            DataKeyNames="Ma_Phongban" EnableViewState="True">
                                            <RowStyle CssClass="GridItem" Height="25px" />
                                            <AlternatingRowStyle CssClass="GridAltItem" />
                                            <HeaderStyle CssClass="GridHeader" Height="30px"></HeaderStyle>
                                            <Columns>
                                                <asp:BoundField DataField="Ma_Phongban" HeaderText="" ReadOnly="True" Visible="false" />
                                                <asp:TemplateField HeaderText="#">
                                                    <HeaderStyle HorizontalAlign="Center" Width="1%" />
                                                    <ItemStyle HorizontalAlign="Center" Width="1%" CssClass="GridBorderVerSolid" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSTT" runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="<%$Resources:cms.language,lblPhongban %>">
                                                    <HeaderStyle HorizontalAlign="Left" Width="50%" />
                                                    <ItemStyle HorizontalAlign="Left" Width="50%" CssClass="GridBorderVerSolid" VerticalAlign="Top" />
                                                    <ItemTemplate>
                                                        <span class="linkGridForm">
                                                            <%# Eval("Ten_Phongban")%></span>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="txt_Tenphong" CssClass="inputtext" runat="Server" Text='<%# Eval("Ten_Phongban") %>'
                                                            Width="90%"></asp:TextBox>
                                                    </EditItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:TextBox ID="txt_Tenphong" CssClass="inputtext" runat="Server" Width="90%"></asp:TextBox>
                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="<%$Resources:cms.language,lblSua %>">
                                                    <HeaderStyle HorizontalAlign="Center" Width="5%" />
                                                    <ItemStyle HorizontalAlign="Center" Width="5%" CssClass="GridBorderVerSolid" />
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="btnAdd" Width="15px" runat="server" ImageUrl="~/Dungchung/images/action.gif"
                                                            ImageAlign="AbsMiddle" ToolTip="Edit" CommandName="Edit" CommandArgument="Edit"
                                                            BorderStyle="None"></asp:ImageButton>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:ImageButton ID="btnUpdate" Width="15px" runat="server" ImageUrl="~/Dungchung/images/save.gif"
                                                            ImageAlign="AbsMiddle" ToolTip="Save" CommandName="Update" BorderStyle="None">
                                                        </asp:ImageButton>
                                                        <asp:ImageButton ID="btnCancel" Width="15px" runat="server" ImageUrl="~/Dungchung/images/undo.gif"
                                                            ImageAlign="AbsMiddle" ToolTip="Cancel" CommandName="Cancel" BorderStyle="None">
                                                        </asp:ImageButton>
                                                    </EditItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:ImageButton ID="btnAddNew" Width="15px" runat="server" ImageUrl="~/Dungchung/Images/Icons/Add.gif"
                                                            ImageAlign="AbsMiddle" ToolTip="Add new" CommandName="AddNew" BorderStyle="None">
                                                        </asp:ImageButton>
                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="<%$Resources:cms.language,lblXoa %>">
                                                    <HeaderStyle HorizontalAlign="Center" Width="5%" />
                                                    <ItemStyle HorizontalAlign="Center" Width="5%" CssClass="GridBorderVerSolid" />
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="btnDelete" Width="15px" runat="server" ImageUrl="~/Dungchung/images/cancel.gif"
                                                            ImageAlign="AbsMiddle" ToolTip="Delete" CommandName="Delete" BorderStyle="None">
                                                        </asp:ImageButton>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:ImageButton ID="btnCancel" Width="15px" runat="server" ImageUrl="~/Dungchung/Images/Icons/Cancel.gif"
                                                            ImageAlign="AbsMiddle" CausesValidation="false" ToolTip="Cancel" CommandName="Cancel"
                                                            BorderStyle="None"></asp:ImageButton>
                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                            <FooterStyle BackColor="#CCCC99" />
                                        </asp:GridView>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: right" class="pageNav" colspan="2">
                                        <cc1:CurrentPage runat="server" ID="curentPages"></cc1:CurrentPage>&nbsp;
                                        <cc1:Pager runat="server" ID="pages" OnIndexChanged="pages_IndexChanged" />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: center">
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
