<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/ITC.Master" AutoEventWireup="true"
    CodeBehind="List_NgonNgu.aspx.cs" Inherits="ToasoanTTXVN.Danhmuc.List_NgonNgu" %>

<%@ Register TagPrefix="cc1" Namespace="HPCComponents.UI.WebControls" Assembly="HPCComponents" %>
<%@ Import Namespace="HPCComponents" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <script type="text/javascript" language="javascript">
        function check_num(obj, length, e) {
            var key = window.event ? e.keyCode : e.which;
            var len = obj.value.length + 1;
            if (length <= 3) begin = 48; else begin = 45;
            if (key >= begin && key <= 57 && len <= length || (key == 8 || key == 0)) {
            }
            else return false;
        }</script>

    <table border="0" cellpadding="0" width="100%" cellspacing="0">
        <tr>
            <td class="datagrid_top_left">
            </td>
            <td class="datagrid_top_center">
                <span class="TitlePanel" style="float: left;"></span>
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
                                        <asp:UpdatePanel ID="upnlbuttonsearch" runat="server">
                                            <ContentTemplate>
                                                <asp:Button runat="server" ID="btnAdd" CssClass="iconAdd" OnClick="btnAdd_Click"
                                                    Text="<%$Resources:cms.language, lblThemmoi%>"></asp:Button>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="height: 4px">
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 100%" colspan="2">
                                        <asp:UpdatePanel ID="UpdatePanellistlang" runat="server">
                                            <ContentTemplate>
                                                <div style="text-align: left; width: 100%">
                                                    <asp:GridView ID="GridViewNgonNgu" runat="Server" AutoGenerateColumns="False" BackColor="White"
                                                        CssClass="Grid"  AllowSorting="true" Width="100%" OnRowDataBound="GridViewNgonNgu_OnRowDataBound"
                                                        OnRowCommand="GridViewNgonNgu_OnRowCommand" OnRowDeleting="GridViewNgonNgu_RowDeleting"
                                                        OnRowEditing="GridViewNgonNgu_RowEditing" OnRowUpdating="GridViewNgonNgu_RowUpdating"
                                                        OnRowCancelingEdit="GridViewNgonNgu_RowCancelingEdit" ShowFooter="False" AutoGenerateEditButton="false"
                                                        DataKeyNames="ID" EnableViewState="True">
                                                        <RowStyle CssClass="GridItem" Height="25px" />
                                                        <AlternatingRowStyle CssClass="GridAltItem" />
                                                        <HeaderStyle CssClass="GridHeader" Height="30px"></HeaderStyle>
                                                        <Columns>
                                                            <asp:BoundField DataField="ID" HeaderText="" ReadOnly="True" Visible="false" />
                                                            <asp:TemplateField HeaderText="#">
                                                                <HeaderStyle HorizontalAlign="Center" Width="1%" />
                                                                <ItemStyle HorizontalAlign="Center" Width="1%" CssClass="GridBorderVerSolid" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblSTT" runat="server"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="<%$Resources:cms.language,lblTenngonngu %>">
                                                                <HeaderStyle HorizontalAlign="Left" Width="60%" />
                                                                <ItemStyle HorizontalAlign="Left" Width="60%" CssClass="GridBorderVerSolid" />
                                                                <ItemTemplate>
                                                                    <span class="linkGridForm">
                                                                        <%# Eval("TenNgonNgu")%></span>
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <asp:TextBox ID="txt_Tenphong" runat="Server" Text='<%# Eval("TenNgonNgu") %>' Width="90%"></asp:TextBox>
                                                                    <br />
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorLang1" runat="server" ErrorMessage="..."
                                                                        ControlToValidate="txt_Tenphong">*
                                                                    </asp:RequiredFieldValidator>
                                                                </EditItemTemplate>
                                                                <FooterTemplate>
                                                                    <asp:TextBox ID="txt_Tenphong" runat="Server" Width="90%" CssClass="inputtext"></asp:TextBox>
                                                                    <br />
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorLang2" runat="server" ErrorMessage="..."
                                                                        ControlToValidate="txt_Tenphong">*
                                                                    </asp:RequiredFieldValidator>
                                                                </FooterTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="STT">
                                                                <HeaderStyle HorizontalAlign="Center" Width="20%" />
                                                                <ItemStyle HorizontalAlign="Center" Width="20%" CssClass="GridBorderVerSolid" />
                                                                <ItemTemplate>
                                                                    <span class="linkGridForm">
                                                                        <%# Eval("ThuTu")%></span>
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <asp:TextBox ID="txt_STT" runat="Server" onKeyPress='return check_num(this,5,event)'
                                                                        Text='<%# Eval("ThuTu") %>' Width="90%"></asp:TextBox>
                                                                    <br />
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorSTT1" runat="server" ErrorMessage="..."
                                                                        ControlToValidate="txt_STT">*
                                                                    </asp:RequiredFieldValidator>
                                                                </EditItemTemplate>
                                                                <FooterTemplate>
                                                                    <asp:TextBox ID="txt_STT" runat="Server" CssClass="inputtext" onKeyPress='return check_num(this,5,event)'
                                                                        Width="90%"></asp:TextBox>
                                                                    <br />
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorSTT2" runat="server" ErrorMessage="..."
                                                                        ControlToValidate="txt_STT">*
                                                                    </asp:RequiredFieldValidator>
                                                                </FooterTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="<%$Resources:cms.language,lblHienthi %>">
                                                                <HeaderStyle HorizontalAlign="Center" Width="8%" />
                                                                <ItemStyle HorizontalAlign="Center" Width="8%" CssClass="GridBorderVerSolid" />
                                                                <ItemTemplate>
                                                                    <asp:ImageButton ID="ActiveLang" runat="server" ImageUrl='<%#Hoatdong(DataBinder.Eval(Container.DataItem, "Hoatdong").ToString())%>'
                                                                        CausesValidation="false" ImageAlign="AbsMiddle" ToolTip="Hiển thị" CommandName="Edit"
                                                                        CommandArgument="Active_BDT" BorderStyle="None" />
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <asp:CheckBox ID="chk_Hoatdong" Width="90%" runat="server" Checked='<%# Eval("Hoatdong") %>' />
                                                                </EditItemTemplate>
                                                                <FooterTemplate>
                                                                    <asp:CheckBox ID="chk_Hoatdong" Width="90%" runat="server" Checked="False" />
                                                                </FooterTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="<%$Resources:cms.language,lblSua %>">
                                                                <HeaderStyle HorizontalAlign="Center" Width="5%" />
                                                                <ItemStyle HorizontalAlign="Center" Width="5%" CssClass="GridBorderVerSolid" />
                                                                <ItemTemplate>
                                                                    <asp:ImageButton ID="btnAdd" Width="15px" runat="server" ImageUrl="~/Dungchung/images/action.gif"
                                                                        CausesValidation="false" ImageAlign="AbsMiddle" ToolTip="Sửa thông tin" CommandName="Edit"
                                                                        CommandArgument="Edit" BorderStyle="None"></asp:ImageButton>
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <asp:ImageButton ID="btnUpdate" Width="15px" runat="server" ImageUrl="~/Dungchung/images/save.gif"
                                                                        ImageAlign="AbsMiddle" ToolTip="Lưu giữ" CommandName="Update" BorderStyle="None">
                                                                    </asp:ImageButton>
                                                                    <asp:ImageButton ID="btnCancel" Width="15px" runat="server" ImageUrl="~/Dungchung/images/undo.gif"
                                                                        CausesValidation="false" ImageAlign="AbsMiddle" ToolTip="Hủy bỏ" CommandName="Cancel"
                                                                        BorderStyle="None"></asp:ImageButton>
                                                                </EditItemTemplate>
                                                                <FooterTemplate>
                                                                    <asp:ImageButton ID="btnAddNew" Width="15px" runat="server" ImageUrl="~/Dungchung/Images/Icons/Add.gif"
                                                                        ImageAlign="AbsMiddle" ToolTip="Thêm mới" CommandName="AddNew" BorderStyle="None">
                                                                    </asp:ImageButton>
                                                                </FooterTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="<%$Resources:cms.language,lblXoa %>">
                                                                <HeaderStyle HorizontalAlign="Center" Width="5%" />
                                                                <ItemStyle HorizontalAlign="Center" Width="5%" CssClass="GridBorderVerSolid" />
                                                                <ItemTemplate>
                                                                    <asp:ImageButton ID="btnDelete" Width="15px" runat="server" ImageUrl="~/Dungchung/images/cancel.gif"
                                                                        CausesValidation="false" ImageAlign="AbsMiddle" ToolTip="Xóa" CommandName="Delete"
                                                                        BorderStyle="None"></asp:ImageButton>
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    <asp:ImageButton ID="btnCancel" Width="15px" runat="server" ImageUrl="~/Dungchung/Images/Icons/Cancel.gif"
                                                                        ImageAlign="AbsMiddle" CausesValidation="false" ToolTip="Hủy" CommandName="Cancel"
                                                                        BorderStyle="None"></asp:ImageButton>
                                                                </FooterTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                        <FooterStyle BackColor="#CCCC99" />
                                                    </asp:GridView>
                                                </div>
                                                <div style="text-align: right; width: 100%" class="pageNav">
                                                    <cc1:CurrentPage runat="server" ID="curentPages"></cc1:CurrentPage>
                                                    <cc1:Pager runat="server" ID="pages" OnIndexChanged="pages_IndexChanged" />
                                                </div>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
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
