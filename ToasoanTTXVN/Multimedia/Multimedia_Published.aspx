<%@ Page Language="C#" MasterPageFile="~/Masters/ITC.Master" AutoEventWireup="true"
    CodeBehind="Multimedia_Published.aspx.cs" Inherits="ToasoanTTXVN.Multimedia.Multimedia_Published" %>

<%@ Register Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register TagPrefix="anthem" Namespace="Anthem" Assembly="Anthem" %>
<%@ Register TagPrefix="cc1" Namespace="HPCComponents.UI.WebControls" Assembly="HPCComponents" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<%@ Import Namespace="HPCComponents" %>
<%@ Import Namespace="HPCBusinessLogic" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <script type="text/javascript" language="javascript">
        function checkAll_DM_Clips(objRef, objectid) {
            var GridView = document.getElementById('<%=DataGrid_Daxuatban.ClientID%>');
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
    </script>

    <table border="0" cellpadding="0" width="100%" cellspacing="0">
        <tr>
            <td class="datagrid_top_left">
            </td>
            <td class="datagrid_top_center">
                <table border="0" cellpadding="1" cellspacing="1">
                    <tr>
                        <td>
                            <img src="../Dungchung/Images/Icons/to-do-list-cheked-all-icon.png" width="16px"
                                height="16px" />
                        </td>
                        <td style="vertical-align: middle">
                            <span class="TitlePanel"><%= CommonLib.ReadXML("titXBamthanh")%></span>
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
                                        <asp:DropDownList AutoPostBack="true" ID="ddlLang" Width="150px" CssClass="inputtext"
                                            runat="server" OnSelectedIndexChanged="ddlLang_SelectedIndexChanged">
                                        </asp:DropDownList>
                            </td>
                            <td style="width: 10%; text-align: right;" class="Titlelbl">
                                <asp:Label ID="lblChuyenmuc" class="Titlelbl" runat="server" 
                                                Text="<%$Resources:cms.language, lblChuyenmuc%>"></asp:Label>
                            </td>
                            <td style="width: 15%; text-align: right;" class="Titlelbl">
                                        <asp:DropDownList AutoPostBack="true" ID="ddlCategorys" runat="server" Width="180px"
                                            CssClass="inputtext" TabIndex="5">
                                        </asp:DropDownList>
                            </td>
                            <td style="width:10%; text-align: right;" class="Titlelbl">
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
                    <table width="100%" cellspacing="2" cellpadding="2" border="0">
                        <tr>
                            <td style="text-align: left" colspan="2">
                                <asp:Button runat="server" CausesValidation="false" ID="btnHuyDang" Font-Bold="true"
                                    OnClick="lbt_HuyXuatban_Click" CssClass="iconPub" Text="<%$Resources:cms.language, lblHuydang%>" />
                            </td>
                        </tr>
                        <tr>
                            <td align="center" colspan="2">
                                <asp:DataGrid ID="DataGrid_Daxuatban" runat="server" Width="100%" AutoGenerateColumns="False"
                                    DataKeyField="ID" OnEditCommand="DataGrid_Daxuatban_EditCommandEditor" CssClass="Grid">
                                    <ItemStyle CssClass="GridItem"></ItemStyle>
                                    <AlternatingItemStyle CssClass="GridAltItem" />
                                    <HeaderStyle CssClass="GridHeader"></HeaderStyle>
                                    <Columns>
                                        <asp:BoundColumn DataField="ID" HeaderText="ID" Visible="False"></asp:BoundColumn>
                                        <asp:TemplateColumn>
                                            <HeaderStyle HorizontalAlign="Center" Width="3%"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Center" Width="3%"></ItemStyle>
                                            <HeaderTemplate>
                                                <asp:CheckBox ID="chkAll" onclick="javascript:checkAll_DM_Clips(this);" runat="server"
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
                                        <asp:TemplateColumn HeaderText="<%$Resources:cms.language, lblTieude%>">
                                            <HeaderStyle Width="20%" HorizontalAlign="Center"></HeaderStyle>
                                            <ItemStyle Width="20%" HorizontalAlign="Left" />
                                            <ItemTemplate>
                                                <asp:LinkButton CssClass="linkEdit" ID="btnEdit" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Tittle") %>'
                                                    ToolTip="Sửa đổi" CommandName="Edit" CommandArgument="Edit"></asp:LinkButton>
                                                <asp:Label runat="server" ID="lblLogTitle" Visible="false" Text='<%# DataBinder.Eval(Container.DataItem, "Tittle") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="<%$Resources:cms.language, lblChuyenmuc%>">
                                            <HeaderStyle Width="11%" HorizontalAlign="Center"></HeaderStyle>
                                            <ItemStyle Width="11%" HorizontalAlign="Left"></ItemStyle>
                                            <ItemTemplate>
                                                <%#HPCBusinessLogic.UltilFunc.GetCategoryName(Eval("Category"))%>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="<%$Resources:cms.language, lblNguoinhap%>">
                                            <ItemTemplate>
                                                <%#HPCBusinessLogic.UltilFunc.GetUserName(Eval("UserCreated"))%>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" Width="8%"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Center" Width="8%"></ItemStyle>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="<%$Resources:cms.language, lblNgaynhap%>">
                                            <ItemTemplate>
                                                <%# DataBinder.Eval(Container, "DataItem.DateCreated")!=System.DBNull.Value?Convert.ToDateTime(DataBinder.Eval(Container, "DataItem.DateCreated")).ToString("dd/MM/yyyy HH:mm:ss"):"" %>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" Width="9%"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Center" Width="9%"></ItemStyle>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="<%$Resources:cms.language, lblNguoiXB%>">
                                            <ItemTemplate>
                                                <%#HPCBusinessLogic.UltilFunc.GetUserName(Eval("UserPublish"))%>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" Width="8%"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Center" Width="8%"></ItemStyle>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="<%$Resources:cms.language, lblNgayXB%>">
                                            <ItemTemplate>
                                                <%# DataBinder.Eval(Container, "DataItem.DatePublish") != System.DBNull.Value ? Convert.ToDateTime(DataBinder.Eval(Container, "DataItem.DatePublish")).ToString("dd/MM/yyyy HH:mm:ss") : ""%>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" Width="9%"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                        </asp:TemplateColumn>
                                        <%-- <asp:TemplateColumn HeaderText="Ngày gửi">
                                            <ItemTemplate>
                                                <%# DataBinder.Eval(Container, "DataItem.DateModify") != System.DBNull.Value ? Convert.ToDateTime(DataBinder.Eval(Container, "DataItem.DateModify")).ToString("dd/MM/yyyy HH:mm:ss") : ""%>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" Width="10%"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Center" Width="10%"></ItemStyle>
                                        </asp:TemplateColumn>--%>
                                        <asp:TemplateColumn HeaderText="<%$Resources:cms.language, lblAnpham%>">
                                            <ItemTemplate>
                                                <%#HPCBusinessLogic.UltilFunc.GetTenAnpham(Eval("Languages_ID"))%>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" Width="8%"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Center" Width="8%"></ItemStyle>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="<%$Resources:cms.language, lblGhichu%>">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtGhichu" CssClass="inputtext" placeholder="<%$Resources:cms.language, msgNhapghichu%>" Width="95%"
                                                    runat="server" TextMode="MultiLine" Rows="3" Text='<%# DataBinder.Eval(Container.DataItem, "Comment")%>'></asp:TextBox>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" Width="12%"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Left" Width="12%"></ItemStyle>
                                        </asp:TemplateColumn>
                                    </Columns>
                                </asp:DataGrid>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 40%; text-align: left">
                                <asp:Button runat="server" ID="btnHuyDangBottom" CssClass="iconPub" Font-Bold="true"
                                    Text="<%$Resources:cms.language, lblHuydang%>" OnClick="lbt_HuyXuatban_Click" />
                            </td>
                            <td style="text-align: right" class="pageNav">
                                <cc1:CurrentPage runat="server" ID="CurrentPage_Xuatban" CssClass="pageNavTotal">
                                </cc1:CurrentPage>
                                <cc1:Pager runat="server" ID="Pager_Xuatban" OnIndexChanged="Pager_Xuatban_IndexChanged_Editor"></cc1:Pager>
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
