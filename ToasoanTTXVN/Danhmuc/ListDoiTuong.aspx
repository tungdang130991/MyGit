<%@ Page Language="C#" MasterPageFile="~/Masters/ITC.Master" AutoEventWireup="true"
    CodeBehind="ListDoiTuong.aspx.cs" Inherits="ToasoanTTXVN.Danhmuc.ListDoiTuong"
    Title="" %>

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
        }
        function ValidateText(i) {
            if (i.value.length > 0) {
                i.value = i.value.replace(/[^\d]+/g, '');
            }
        }    
    </script>

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
                                        <asp:Button runat="server" ID="btnAdd" CssClass="iconAdd" Font-Bold="true" OnClick="btnAdd_Click"
                                            Text="<%$Resources:cms.language, lblThemmoi%>"></asp:Button>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="height: 4px">
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 100%" colspan="2">
                                        <asp:GridView ID="GVDoituong" runat="Server" AutoGenerateColumns="False" BackColor="White"
                                            CssClass="Grid" AllowSorting="true" OnSorting="GVDoituong_Sorting" Width="100%"
                                            OnRowDataBound="GVDoituong_OnRowDataBound" OnRowCommand="GVDoituong_OnRowCommand1"
                                            OnRowDeleting="GVDoituong_RowDeleting" OnRowEditing="GVDoituong_RowEditing" OnRowUpdating="GVDoituong_RowUpdating"
                                            OnRowCancelingEdit="GVDoituong_RowCancelingEdit" OnRowCreated="GVDoituong_OnRowCreated"
                                            ShowFooter="False" AutoGenerateEditButton="false" DataKeyNames="ID" EnableViewState="True">
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
                                                <asp:TemplateField HeaderText="<%$Resources:cms.language,lblMadoituong %>">
                                                    <HeaderStyle HorizontalAlign="Center" Width="10%" />
                                                    <ItemStyle HorizontalAlign="Center" CssClass="GridBorderVerSolid" />
                                                    <FooterStyle HorizontalAlign="Center"/>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblMaDT" CssClass="linkGridForm" runat="server" Text='<%# Eval("Ma_Doituong")%>'></asp:Label>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="txt_MaDoituong" runat="Server" CssClass="inputtext" Text='<%# Eval("Ma_Doituong") %>'
                                                            Width="80%"></asp:TextBox><br />
                                                        <asp:Label ID="lblMaDT_Error" runat="server" ForeColor="Red"></asp:Label>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="..."
                                                            ControlToValidate="txt_MaDoituong">*
                                                        </asp:RequiredFieldValidator>
                                                    </EditItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:TextBox ID="txt_MaDoituong" CssClass="inputtext" runat="Server" Width="80%"></asp:TextBox><br />
                                                        <asp:Label ID="lblMaDT_Error" runat="server" ForeColor="Red"></asp:Label><asp:RequiredFieldValidator
                                                            ID="RequiredFieldValidator1" runat="server" ErrorMessage="..." ControlToValidate="txt_MaDoituong">*
                                                        </asp:RequiredFieldValidator>
                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="<%$Resources:cms.language,lblTendoituong %>">
                                                    <HeaderStyle HorizontalAlign="Left" Width="30%" />
                                                    <ItemStyle HorizontalAlign="Left" CssClass="GridBorderVerSolid" />
                                                    <FooterStyle HorizontalAlign="Center"/>
                                                    <ItemTemplate>
                                                        <span class="linkGridForm">
                                                            <%# Eval("Ten_Doituong")%></span>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="txt_Tendoituong" runat="Server" Text='<%# Eval("Ten_Doituong") %>'
                                                            CssClass="inputtext" Width="90%"></asp:TextBox>
                                                    </EditItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:TextBox ID="txt_Tendoituong" CssClass="inputtext" runat="Server" Width="90%"></asp:TextBox>
                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="EnglishName">
                                                    <HeaderStyle HorizontalAlign="Left" Width="30%" />
                                                    <ItemStyle HorizontalAlign="Left" CssClass="GridBorderVerSolid" />
                                                    <FooterStyle HorizontalAlign="Center"/>
                                                    <ItemTemplate>
                                                        <span class="linkGridForm">
                                                            <%# Eval("EnglishName")%></span>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="txt_EnglishName" runat="Server" Text='<%# Eval("EnglishName") %>'
                                                            CssClass="inputtext" Width="90%"></asp:TextBox>
                                                    </EditItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:TextBox ID="txt_EnglishName" CssClass="inputtext" runat="Server" Width="90%"></asp:TextBox>
                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="<%$Resources:cms.language,lblSothutu %>">
                                                    <HeaderStyle HorizontalAlign="Center" Width="10%" />
                                                    <ItemStyle HorizontalAlign="Center" Width="10%" CssClass="GridBorderVerSolid" />
                                                    <FooterStyle HorizontalAlign="Center"/>
                                                    <ItemTemplate>
                                                        <span class="linkGridForm">
                                                            <%# Eval("STT")%></span>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="txt_STT" CssClass="inputtext" runat="Server" Text='<%# Eval("STT") %>'
                                                            Width="60%" onKeyPress="return check_num(this,5,event);"></asp:TextBox><br />
                                                        <asp:Label ID="lblSTT_Error" runat="server" ForeColor="Red"></asp:Label><br />
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="..."
                                                            ControlToValidate="txt_STT">(Nhập số thứ tự)
                                                        </asp:RequiredFieldValidator>
                                                    </EditItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:TextBox ID="txt_STT" CssClass="inputtext" runat="Server" Width="60%" onKeyPress="return check_num(this,5,event);"></asp:TextBox><br />
                                                        <asp:Label ID="lblSTT_Error" runat="server" ForeColor="Red"></asp:Label><br />
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ErrorMessage="..."
                                                            ControlToValidate="txt_STT">(Nhập số thứ tự)
                                                        </asp:RequiredFieldValidator>
                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="<%$Resources:cms.language,lblSua %>">
                                                    <HeaderStyle HorizontalAlign="Center" Width="5%" />
                                                    <ItemStyle HorizontalAlign="Center" Width="5%" CssClass="GridBorderVerSolid" />
                                                    <FooterStyle HorizontalAlign="Center"/>
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="btnAdd" Width="15px" runat="server" ImageUrl="~/Dungchung/images/action.gif"
                                                            ImageAlign="AbsMiddle" ToolTip="Edit" CommandName="Edit" CommandArgument="Edit"
                                                            BorderStyle="None"></asp:ImageButton>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:ImageButton ID="btnUpdate" Width="15px" runat="server" ImageUrl="~/Dungchung/images/save.gif"
                                                            ImageAlign="AbsMiddle" ToolTip="Lưu giữ" CommandName="Update" BorderStyle="None">
                                                        </asp:ImageButton>
                                                        <asp:ImageButton ID="btnCancel" Width="15px" runat="server" ImageUrl="~/Dungchung/images/undo.gif"
                                                            ImageAlign="AbsMiddle" ToolTip="Hủy bỏ" CommandName="Cancel" BorderStyle="None">
                                                        </asp:ImageButton>
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
                                                    <FooterStyle HorizontalAlign="Center"/>
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="btnDelete" Width="15px" runat="server" ImageUrl="~/Dungchung/images/cancel.gif"
                                                            ImageAlign="AbsMiddle" ToolTip="Delete" CommandName="Delete" BorderStyle="None">
                                                        </asp:ImageButton>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:ImageButton ID="btnCancel" Width="15px" runat="server" ImageUrl="~/Dungchung/Images/Icons/Cancel.gif"
                                                            ImageAlign="AbsMiddle" CausesValidation="false" ToolTip="Hủy" CommandName="Cancel"
                                                            BorderStyle="None"></asp:ImageButton>
                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                            <FooterStyle BackColor="#CCCC99" HorizontalAlign="Center" />
                                            <EditRowStyle HorizontalAlign="Center" />
                                        </asp:GridView>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: right" class="pageNav" colspan="2">
                                        <cc1:CurrentPage runat="server" ID="curentPages">
                                        </cc1:CurrentPage>&nbsp;
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
