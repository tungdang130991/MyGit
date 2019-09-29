<%@ Page Language="C#" MasterPageFile="~/Masters/ITC.Master" AutoEventWireup="true"
    CodeBehind="ListKichthuoc.aspx.cs" Inherits="ToasoanTTXVN.Danhmuc.ListKichthuoc"
    Title="" EnableEventValidation="true" %>

<%@ Register TagPrefix="cc1" Namespace="HPCComponents.UI.WebControls" Assembly="HPCComponents" %>
<%@ Import Namespace="HPCComponents" %>
<%@ Register TagPrefix="anthem" Namespace="Anthem" Assembly="Anthem" %>
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
    </script>

    <table border="0" cellpadding="0" width="100%" cellspacing="0">
        <tr>
            <td class="datagrid_top_left">
            </td>
            <td class="datagrid_top_center">
                
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
                                    <td style="width: 10%; text-align: right" class="Titlelbl">
                                    </td>
                                    <td style="width: 30%; text-align: left;">
                                    </td>
                                    <td style="text-align: right; width: 10%;" class="Titlelbl">
                                    </td>
                                    <td style="text-align: left; width: 40%;">
                                    </td>
                                    <td style="text-align: left; width: 10%;">
                                    </td>
                                    <td style="text-align: right; width: 10%;">
                                        <asp:Button runat="server" ID="btnAdd" CssClass="iconAdd" Font-Bold="true"
                                            OnClick="btnAdd_Click" Text="<%$Resources:cms.language, lblThemmoi%>"></asp:Button>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 4px">
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 100%">
                            <asp:GridView ID="GVKichthuoc" runat="Server" AutoGenerateColumns="False" BackColor="White"
                                CssClass="Grid"  Width="100%" OnRowDataBound="GVKichthuoc_OnRowDataBound"
                                OnRowCommand="GVKichthuoc_OnRowCommand1" OnRowDeleting="GVKichthuoc_RowDeleting"
                                OnRowEditing="GVKichthuoc_RowEditing" OnRowUpdating="GVKichthuoc_RowUpdating"
                                OnRowCancelingEdit="GVKichthuoc_RowCancelingEdit" ShowFooter="False" AutoGenerateEditButton="false"
                                DataKeyNames="Ma_Kichthuoc" EnableViewState="True">
                                <RowStyle CssClass="GridItem" Height="25px" />
                                <AlternatingRowStyle CssClass="GridAltItem" />
                                <HeaderStyle CssClass="GridHeader" Height="30px"></HeaderStyle>
                                <Columns>
                                    <asp:BoundField DataField="Ma_Kichthuoc" HeaderText="" ReadOnly="True" Visible="false" />
                                    <asp:TemplateField HeaderText="#">
                                        <HeaderStyle HorizontalAlign="Center" Width="5%" />
                                        <ItemStyle HorizontalAlign="Center" Width="5%" CssClass="GridBorderVerSolid" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblSTT" runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="<%$Resources:cms.language, lblKichthuocQC%>">
                                        <HeaderStyle HorizontalAlign="Left" Width="30%" />
                                        <ItemStyle HorizontalAlign="Left"  CssClass="GridBorderVerSolid"/>
                                        <ItemTemplate>
                                            <%# Eval("Ten_Kichthuoc")%>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txt_Kichthuoc" CssClass="inputtext" runat="Server" Text='<%# Eval("Ten_Kichthuoc") %>'
                                                TextMode="MultiLine" Rows="2" Width="90%"></asp:TextBox><br />
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="..."
                                                ControlToValidate="txt_Kichthuoc">*
                                            </asp:RequiredFieldValidator>
                                        </EditItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox ID="txt_Kichthuoc" CssClass="inputtext" runat="Server" TextMode="MultiLine" Rows="2" Width="90%"></asp:TextBox><br />
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="..."
                                                ControlToValidate="txt_Kichthuoc">*
                                            </asp:RequiredFieldValidator>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="<%$Resources:cms.language,lblMota %>">
                                        <HeaderStyle HorizontalAlign="Left" Width="30%" />
                                        <ItemStyle HorizontalAlign="Left"  CssClass="GridBorderVerSolid" />
                                        <ItemTemplate>
                                            <%# Eval("Mota")%>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txt_mota" runat="Server" CssClass="inputtext" Text='<%# Eval("Mota") %>' TextMode="MultiLine"
                                                Rows="2" Width="90%"></asp:TextBox>
                                        </EditItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox ID="txt_mota" runat="Server" CssClass="inputtext" TextMode="MultiLine" Rows="2" Width="90%"></asp:TextBox>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="<%$Resources:cms.language,lblAnpham %>">
                                        <HeaderStyle HorizontalAlign="Left" Width="20%" />
                                        <ItemStyle HorizontalAlign="Left"  CssClass="GridBorderVerSolid" />
                                        <ItemTemplate>
                                            <asp:Label ID='lblAnpham' runat="server" Text=' <%#TenAnpham(DataBinder.Eval(Container.DataItem, "Ma_AnPham").ToString())%>'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:Label ID='lblAnpham' runat="server" Text='<%# Eval("Ma_AnPham") %> ' Visible="false"></asp:Label>
                                            <asp:DropDownList ID="ddl_AnPham" CssClass="inputtext" runat="server" Width="100%"
                                                DataTextField="Ten_AnPham" DataValueField="Ma_AnPham">
                                            </asp:DropDownList>
                                        </EditItemTemplate>
                                        <FooterTemplate>
                                            <asp:DropDownList ID="ddl_AnPham" CssClass="inputtext" runat="server" Width="100%"
                                                DataTextField="Ten_AnPham" DataValueField="Ma_AnPham">
                                            </asp:DropDownList>
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
                                                ImageAlign="AbsMiddle" ToolTip="Cancel" CausesValidation="false" CommandName="Cancel" BorderStyle="None">
                                            </asp:ImageButton>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <FooterStyle BackColor="#CCCC99" />
                            </asp:GridView>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right" class="pageNav">
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
