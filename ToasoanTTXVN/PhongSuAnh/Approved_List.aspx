<%@ Page Language="C#" MasterPageFile="~/Masters/ITC.Master" AutoEventWireup="true"
    CodeBehind="Approved_List.aspx.cs" Inherits="ToasoanTTXVN.PhongSuAnh.Approved_List" %>

<%@ Register TagPrefix="anthem" Namespace="Anthem" Assembly="Anthem" %>
<%@ Register TagPrefix="cc1" Namespace="HPCComponents.UI.WebControls" Assembly="HPCComponents" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<%@ Import Namespace="HPCComponents" %>
<%@ Import Namespace="HPCBusinessLogic" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <script language="Javascript" type="text/javascript">
        function CheckAll_One(objRef) {
            var GridView = document.getElementById('<%=DataGrid_DaXB.ClientID%>');
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
                            <img src="../Dungchung/Images/Icons/to-do-list-cheked-all-icon.png" width="16px"
                                height="16px" />
                        </td>
                        <td style="vertical-align: middle;">
                            <span class="TitlePanel"><%= CommonLib.ReadXML("titGocanhXB") %></span>
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
            <td class="datagrid_content_center">
                <div class="classSearchHeader">
                    <table style="width: 100%" cellpadding="1" cellspacing="1">
                        <tr>
                            <td style="width: 20%; text-align: right" class="Titlelbl">
                                <asp:Label ID="Label3" class="Titlelbl" runat="server" 
                                                Text="<%$Resources:cms.language, lblAnpham%>"></asp:Label>
                            </td>
                            <td style="width: 15%; text-align: left">
                                <asp:DropDownList ID="cboNgonNgu" Width="200px" runat="server" CssClass="inputtext">
                                </asp:DropDownList>
                            </td>
                            <td style="width: 15%; text-align: right" class="Titlelbl">
                                <asp:Label ID="Label1" class="Titlelbl" runat="server" 
                                                Text="<%$Resources:cms.language, lblTengocanh%>"></asp:Label>
                            </td>
                            <td style="width: 30%; text-align: left">
                                <asp:TextBox ID="txtSearch_Cate" Width="80%" runat="server" CssClass="inputtext"
                                    onkeypress="return clickButton(event,'ctl00_MainContent_btnSearch');"></asp:TextBox>
                                
                            </td>
                            <td style="width: 20%; text-align: left">
                                <asp:Button runat="server" ID="btnSearch" CssClass="iconFind" Font-Bold="true" OnClick="linkSearch_Click"
                                    Text="<%$Resources:cms.language, lblTimkiem%>"></asp:Button>
                            </td>
                        </tr>
                    </table>
                </div>
            </td>
            <td class="datagrid_content_right">
            </td>
        </tr>
        <tr>
            <td class="datagrid_content_left">
            </td>
            <td class="" style="height: 15px">
            </td>
            <td class="datagrid_content_right">
            </td>
        </tr>
        <tr>
            <td class="datagrid_content_left">
            </td>
            <td style="text-align: left">
                <div class="classSearchHeader">
                    <table width="100%" cellspacing="0" cellpadding="0" border="0" style="text-align: left">
                        <tr>
                            <td>
                                <asp:UpdatePanel ID="UpdatePanel11" runat="server">
                                    <ContentTemplate>
                                        <table width="100%" cellspacing="2" cellpadding="2" border="0">
                                            <td style="text-align: left" colspan="2">
                                                <asp:Button runat="server" ID="link_HuyXB" CssClass="iconPub" CausesValidation="false"
                                                    Font-Bold="true" OnClick="link_HuyXB_Click" Text="<%$Resources:cms.language, lblHuydang%>" />
                                            </td>
                                            <tr>
                                                <td align="left">
                                                    <asp:DataGrid runat="server" ID="DataGrid_DaXB" AutoGenerateColumns="false" DataKeyField="Cat_Album_ID"
                                                        Width="100%" CssClass="Grid" OnEditCommand="DataGrid_DaXB_EditCommand">
                                                        <ItemStyle CssClass="GridItem"></ItemStyle>
                                                        <AlternatingItemStyle CssClass="GridAltItem" />
                                                        <HeaderStyle CssClass="GridHeader"></HeaderStyle>
                                                        <Columns>
                                                            <asp:TemplateColumn>
                                                                <HeaderStyle HorizontalAlign="Center" Width="3%"></HeaderStyle>
                                                                <ItemStyle HorizontalAlign="Center" Width="3%"></ItemStyle>
                                                                <HeaderTemplate>
                                                                    <asp:CheckBox ID="chkAll" onclick="javascript:CheckAll_One(this);" runat="server"
                                                                        ToolTip="<%$Resources:cms.language, lblChontatca%>"></asp:CheckBox>
                                                                </HeaderTemplate>
                                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                                <ItemTemplate>
                                                                    <asp:CheckBox runat="server" Text='' ID='optSelect' AutoPostBack="False"></asp:CheckBox>
                                                                </ItemTemplate>
                                                            </asp:TemplateColumn>
                                                            <asp:BoundColumn Visible="False" DataField="Cat_Album_ID">
                                                                <HeaderStyle Width="1%"></HeaderStyle>
                                                            </asp:BoundColumn>
                                                            <asp:TemplateColumn HeaderText="<%$Resources:cms.language, lblAnhdaidien%>">
                                                                <HeaderStyle HorizontalAlign="Center" Width="10%"></HeaderStyle>
                                                                <ItemStyle HorizontalAlign="Center" Width="10%"></ItemStyle>
                                                                <ItemTemplate>
                                                                    <asp:Label runat="server" ID="lblUrlPath" Text='<%# DataBinder.Eval(Container.DataItem, "Abl_Photo_Origin")%>'
                                                                        Visible="false"></asp:Label>
                                                                    <div>
                                                                        <asp:ImageButton ID="btnViewPhoto" Width="15px" runat="server" ImageUrl='<%# LoadImage(DataBinder.Eval(Container.DataItem, "Abl_Photo_Origin")) %>'
                                                                            ImageAlign="AbsMiddle" Style="padding-top: 3px; border: 0; cursor: pointer; width: 120px;
                                                                            height: 80px;" ToolTip="Xem phóng sự" BorderStyle="None"></asp:ImageButton>
                                                                    </div>
                                                                    <asp:Label ID="lblcatid" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container.DataItem, "Cat_Album_ID")%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateColumn>
                                                            <asp:TemplateColumn HeaderText="<%$Resources:cms.language, lblTengocanh%>">
                                                                <HeaderStyle HorizontalAlign="Left" Width="28%"></HeaderStyle>
                                                                <ItemStyle HorizontalAlign="Left" Width="28%"></ItemStyle>
                                                                <ItemTemplate>
                                                                    <%# DataBinder.Eval(Container.DataItem, "Cat_Album_Name")%>
                                                                    <asp:Label ID="lblTenPS" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container.DataItem, "Cat_Album_Name")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="btnEdit" CssClass="linkEdit" Text='<%# DataBinder.Eval(Container.DataItem, "Cat_Album_Name")%>'
                                                                        runat="server" CommandName="Edit" CommandArgument="Edit" ToolTip="Chỉnh sửa thông tin Album"></asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateColumn>
                                                            <asp:TemplateColumn HeaderText="<%$Resources:cms.language, lblNhapanh%>">
                                                                <HeaderStyle HorizontalAlign="Center" Width="5%"></HeaderStyle>
                                                                <ItemStyle HorizontalAlign="Center" Width="5%"></ItemStyle>
                                                                <ItemTemplate>
                                                                    <asp:ImageButton ID="btnInputPhoto" runat="server" ImageUrl="~/Dungchung/Images/Icons/cog-edit-icon.png"
                                                                        ImageAlign="AbsMiddle" ToolTip="Nhập ảnh cho phóng sự" CommandName="Edit" CommandArgument="InputPhoto"
                                                                        BorderStyle="None"></asp:ImageButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateColumn>
                                                            <asp:TemplateColumn Visible="false" HeaderText="<%$Resources:cms.language, lblAnpham%>">
                                                                <HeaderStyle HorizontalAlign="Center" Width="8%"></HeaderStyle>
                                                                <ItemStyle HorizontalAlign="Center" Width="8%"></ItemStyle>
                                                                <ItemTemplate>
                                                                    <%#HPCBusinessLogic.UltilFunc.GetTenAnpham(Eval("Lang_ID"))%>
                                                                </ItemTemplate>
                                                            </asp:TemplateColumn>
                                                            <asp:TemplateColumn HeaderText="<%$Resources:cms.language, lblNguoinhap%>">
                                                                <HeaderStyle HorizontalAlign="Center" Width="8%"></HeaderStyle>
                                                                <ItemStyle HorizontalAlign="Center" Width="8%"></ItemStyle>
                                                                <ItemTemplate>
                                                                    <%#HPCBusinessLogic.UltilFunc.GetUserName(Eval("UserCreated"))%>
                                                                </ItemTemplate>
                                                            </asp:TemplateColumn>
                                                            <asp:TemplateColumn HeaderText="<%$Resources:cms.language, lblNgaynhap%>">
                                                                <HeaderStyle HorizontalAlign="Center" Width="8%"></HeaderStyle>
                                                                <ItemStyle HorizontalAlign="Center" Width="8%"></ItemStyle>
                                                                <ItemTemplate>
                                                                    <%#Eval("Cat_Album_DateCreate") != System.DBNull.Value ? Convert.ToDateTime(Eval("Cat_Album_DateCreate")).ToString("dd/MM/yyyy hh:mm:ss") : ""%>
                                                                </ItemTemplate>
                                                            </asp:TemplateColumn>
                                                            <asp:TemplateColumn HeaderText="<%$Resources:cms.language, lblNguoiXB%>">
                                                                <HeaderStyle HorizontalAlign="Center" Width="8%"></HeaderStyle>
                                                                <ItemStyle HorizontalAlign="Center" Width="8%"></ItemStyle>
                                                                <ItemTemplate>
                                                                    <%#HPCBusinessLogic.UltilFunc.GetUserName(Eval("Cat_Album_UserIDApprove"))%>
                                                                </ItemTemplate>
                                                            </asp:TemplateColumn>
                                                            <asp:TemplateColumn HeaderText="<%$Resources:cms.language, lblNgayXB%>">
                                                                <HeaderStyle HorizontalAlign="Center" Width="8%"></HeaderStyle>
                                                                <ItemStyle HorizontalAlign="Center" Width="8%"></ItemStyle>
                                                                <ItemTemplate>
                                                                    <%#Eval("Cat_Album_DateApprove") != System.DBNull.Value ? Convert.ToDateTime(Eval("Cat_Album_DateApprove")).ToString("dd/MM/yyyy hh:mm:ss") : ""%>
                                                                </ItemTemplate>
                                                            </asp:TemplateColumn>
                                                            <asp:TemplateColumn HeaderText="<%$Resources:cms.language, lblGhichu%>">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtGhichu" CssClass="inputtext" placeholder="<%$Resources:cms.language, msgNhapghichu%>" Width="95%"
                                                                        runat="server" TextMode="MultiLine" Rows="3" Text='<%# DataBinder.Eval(Container.DataItem, "Comment")%>'></asp:TextBox>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" Width="14%"></HeaderStyle>
                                                                <ItemStyle HorizontalAlign="Left" Width="14%"></ItemStyle>
                                                            </asp:TemplateColumn>
                                                        </Columns>
                                                    </asp:DataGrid>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <table border="0" cellpadding="1" cellspacing="1" width="100%">
                                                        <tr>
                                                            <td style="text-align: left; width: 50%">
                                                                <asp:Button runat="server" ID="LinkButton1" CssClass="iconPub" CausesValidation="false"
                                                                    Font-Bold="true" OnClick="link_HuyXB_Click" Text="<%$Resources:cms.language, lblHuydang%>" />
                                                            </td>
                                                            <td style="text-align: right" class="pageNav">
                                                                <cc1:CurrentPage runat="server" ID="curentPages_DaXB" CssClass="pageNavTotal">
                                                                </cc1:CurrentPage>
                                                                <cc1:Pager runat="server" ID="Pager_DaXB" OnIndexChanged="Pager_DaXB_IndexChanged">
                                                                </cc1:Pager>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
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
