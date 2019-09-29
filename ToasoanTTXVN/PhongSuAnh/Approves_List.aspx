<%@ Page Language="C#" MasterPageFile="~/Masters/ITC.Master" AutoEventWireup="true"
    CodeBehind="Approves_List.aspx.cs" Inherits="ToasoanTTXVN.PhongSuAnh.Approves_List" %>

<%@ Register TagPrefix="anthem" Namespace="Anthem" Assembly="Anthem" %>
<%@ Register TagPrefix="cc1" Namespace="HPCComponents.UI.WebControls" Assembly="HPCComponents" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<%@ Import Namespace="HPCComponents" %>
<%@ Import Namespace="HPCBusinessLogic" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <script language="Javascript" type="text/javascript">
        function SetInnerProcess(PSchoduyet, PStralai, PSdaduyet) {
            document.getElementById("__tab_ctl00_MainContent_TabContainer1_TabPanel_choduyet").innerHTML = PSchoduyet;
            document.getElementById("__tab_ctl00_MainContent_TabContainer1_TabPanel_HuyXB").innerHTML = PStralai;
            document.getElementById("__tab_ctl00_MainContent_TabContainer1_TabPanel_daXB").innerHTML = PSdaduyet;
        }
        function checkAllOne(objRef) {
            var GridView = document.getElementById('<%=DataGrid_Choduyet.ClientID%>');
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
        function checkAll_CM(objRef, objectid) {
            var GridView = document.getElementById('<%=dgCategorysCopy.ClientID%>');
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
        function checkAllTwo(objRef) {
            var GridView = document.getElementById('<%=DataGrid_HuyXB.ClientID%>');
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
        function cancel() {
            $find('ctl00_MainContent_ModalPopupExtender1').hide();
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
                            <span class="TitlePanel"><%= CommonLib.ReadXML("titDuyetgocanh") %></span>
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
                                <cc2:TabContainer ID="TabContainer1" runat="server" CssClass="ajax__tab_yuitabview-theme"
                                    AutoPostBack="true" ActiveTabIndex="0" OnActiveTabChanged="TabContainer1_ActiveTabChanged">
                                    <cc2:TabPanel HeaderText="<%$Resources:cms.language, lblGocanhchoduyet%>" ID="TabPanel_choduyet" runat="server">
                                        <ContentTemplate>
                                            <asp:UpdatePanel ID="UpdatePanel11" runat="server">
                                                <ContentTemplate>
                                                    <table width="100%" cellspacing="2" cellpadding="2" border="0">
                                                        <td style="text-align: left" colspan="2">
                                                            <asp:Button runat="server" ID="btnSendXuLyOn" CssClass="iconPub" CausesValidation="false"
                                                                Font-Bold="true" OnClick="link_duyet_Click" Text="<%$Resources:cms.language, lblXuatban%>" />
                                                            <asp:Button runat="server" Visible="false" ID="btnTranslateOn" CssClass="iconCopy" CausesValidation="false"
                                                                Font-Bold="true" OnClick="link_copy_Click" Text="<%$Resources:cms.language, lblDich%>" />
                                                            <asp:Button runat="server" ID="btnReturnXuLyOn" CausesValidation="false" CssClass="iconReply"
                                                                Text="<%$Resources:cms.language, lblTralai%>" OnClick="link_tralai_Click" />
                                                        </td>
                                                        <tr>
                                                            <td align="left">
                                                                <asp:DataGrid runat="server" ID="DataGrid_Choduyet" AutoGenerateColumns="false" DataKeyField="Cat_Album_ID"
                                                                    Width="100%" CssClass="Grid" OnEditCommand="DataGrid_Choduyet_EditCommand">
                                                                    <ItemStyle CssClass="GridItem"></ItemStyle>
                                                                    <AlternatingItemStyle CssClass="GridAltItem" />
                                                                    <HeaderStyle CssClass="GridHeader"></HeaderStyle>
                                                                    <Columns>
                                                                        <asp:TemplateColumn>
                                                                            <HeaderStyle HorizontalAlign="Center" Width="3%"></HeaderStyle>
                                                                            <ItemStyle HorizontalAlign="Center" Width="3%"></ItemStyle>
                                                                            <HeaderTemplate>
                                                                                <asp:CheckBox ID="chkAll" onclick="javascript:checkAllOne(this);" runat="server"
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
                                                                                        ImageAlign="AbsMiddle" Style="padding-top: 3px; border: 0; cursor: pointer; width: 120px; height:80px;" ToolTip="Xem phóng sự" BorderStyle="None"></asp:ImageButton>
                                                                                </div>
                                                                                <asp:Label ID="lblcatid" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container.DataItem, "Cat_Album_ID")%>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateColumn>
                                                                        <asp:TemplateColumn HeaderText="<%$Resources:cms.language, lblTengocanh%>">
                                                                            <HeaderStyle HorizontalAlign="Center" Width="28%"></HeaderStyle>
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
                                                                        <asp:TemplateColumn Visible="false" HeaderText="<%$Resources:cms.language, lblNgonngu%>">
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
                                                                        <asp:TemplateColumn HeaderText="<%$Resources:cms.language, lblNguoihieudinh%>">
                                                                            <HeaderStyle HorizontalAlign="Center" Width="8%"></HeaderStyle>
                                                                            <ItemStyle HorizontalAlign="Center" Width="8%"></ItemStyle>
                                                                            <ItemTemplate>
                                                                                <%#HPCBusinessLogic.UltilFunc.GetUserName(Eval("UserCreated"))%>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateColumn>
                                                                        <asp:TemplateColumn HeaderText="<%$Resources:cms.language, lblNgayhieudinh%>">
                                                                            <HeaderStyle HorizontalAlign="Center" Width="8%"></HeaderStyle>
                                                                            <ItemStyle HorizontalAlign="Center" Width="8%"></ItemStyle>
                                                                            <ItemTemplate>
                                                                                <%#Eval("Cat_Album_DateSend") != System.DBNull.Value ? Convert.ToDateTime(Eval("Cat_Album_DateSend")).ToString("dd/MM/yyyy hh:mm:ss") : ""%>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateColumn>
                                                                        <asp:TemplateColumn HeaderText="<%$Resources:cms.language, lblGhichu%>">
                                                                            <ItemTemplate>
                                                                                <%# DataBinder.Eval(Container.DataItem, "Comment") %>
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
                                                                            <asp:Button runat="server" ID="btnSendXuLyBottom" CssClass="iconPub" CausesValidation="false"
                                                                                Font-Bold="true" OnClick="link_duyet_Click" Text="<%$Resources:cms.language, lblXuatban%>" />
                                                                            <asp:Button runat="server" ID="btnTranslateBottom" Visible="false" CssClass="iconCopy" CausesValidation="false"
                                                                                Font-Bold="true" OnClick="link_copy_Click" Text="<%$Resources:cms.language, lblDich%>" />
                                                                            <asp:Button runat="server" ID="btnReturnXuLyBottom" CausesValidation="false" CssClass="iconReply"
                                                                                Text="<%$Resources:cms.language, lblTralai%>" OnClick="link_tralai_Click" />
                                                                        </td>
                                                                        <td style="text-align: right" class="pageNav">
                                                                            <cc1:CurrentPage runat="server" ID="curentPages_choduyet" CssClass="pageNavTotal">
                                                                            </cc1:CurrentPage>
                                                                            <cc1:Pager runat="server" ID="Pager_choduyet" OnIndexChanged="Pager_choduyet_IndexChanged">
                                                                            </cc1:Pager>
                                                                        </td>
                                                                    </tr>
                                                                </table>
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
                                    <cc2:TabPanel HeaderText="<%$Resources:cms.language, lblGocanhhuyXB%>" ID="TabPanel_HuyXB" runat="server">
                                        <ContentTemplate>
                                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                <ContentTemplate>
                                                    <table width="100%" cellspacing="2" cellpadding="2" border="0">
                                                        <td style="text-align: left" colspan="2">
                                                            <asp:Button runat="server" ID="btnSendTwoOn" CssClass="iconPub" CausesValidation="false"
                                                                Font-Bold="true" OnClick="link_duyet_Click" Text="<%$Resources:cms.language, lblXuatban%>" />
                                                            &nbsp;&nbsp;&nbsp;
                                                            <asp:Button runat="server" ID="btnReturnTwoOn" CausesValidation="false" CssClass="iconReply"
                                                                Text="<%$Resources:cms.language, lblTralai%>" OnClick="link_tralai_Click" />
                                                        </td>
                                                        <tr>
                                                            <td align="left">
                                                                <asp:DataGrid runat="server" ID="DataGrid_HuyXB" AutoGenerateColumns="false" DataKeyField="Cat_Album_ID"
                                                                    Width="100%" CssClass="Grid" OnEditCommand="DataGrid_HuyXB_EditCommand">
                                                                    <ItemStyle CssClass="GridItem"></ItemStyle>
                                                                    <AlternatingItemStyle CssClass="GridAltItem" />
                                                                    <HeaderStyle CssClass="GridHeader"></HeaderStyle>
                                                                    <Columns>
                                                                        <asp:TemplateColumn>
                                                                            <HeaderStyle HorizontalAlign="Center" Width="3%"></HeaderStyle>
                                                                            <ItemStyle HorizontalAlign="Center" Width="3%"></ItemStyle>
                                                                            <HeaderTemplate>
                                                                                <asp:CheckBox ID="chkAll" onclick="javascript:checkAllTwo(this);" runat="server"
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
                                                                                        ImageAlign="AbsMiddle" Style="padding-top: 3px; border: 0; cursor: pointer; width: 120px; height:80px;" ToolTip="Xem phóng sự" BorderStyle="None"></asp:ImageButton>
                                                                                </div>
                                                                                <asp:Label ID="lblcatid" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container.DataItem, "Cat_Album_ID")%>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateColumn>
                                                                        <asp:TemplateColumn HeaderText="<%$Resources:cms.language, lblTengocanh%>">
                                                                            <HeaderStyle HorizontalAlign="Center" Width="28%"></HeaderStyle>
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
                                                                        <asp:TemplateColumn HeaderText="<%$Resources:cms.language, lblNguoingungdang%>">
                                                                            <HeaderStyle HorizontalAlign="Center" Width="8%"></HeaderStyle>
                                                                            <ItemStyle HorizontalAlign="Center" Width="8%"></ItemStyle>
                                                                            <ItemTemplate>
                                                                                <%#HPCBusinessLogic.UltilFunc.GetUserName(Eval("Cat_Album_UserIDApprove"))%>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateColumn>
                                                                        <asp:TemplateColumn HeaderText="<%$Resources:cms.language, lblNgayngungdang%>">
                                                                            <HeaderStyle HorizontalAlign="Center" Width="8%"></HeaderStyle>
                                                                            <ItemStyle HorizontalAlign="Center" Width="8%"></ItemStyle>
                                                                            <ItemTemplate>
                                                                                <%#Eval("Cat_Album_DateApprove") != System.DBNull.Value ? Convert.ToDateTime(Eval("Cat_Album_DateApprove")).ToString("dd/MM/yyyy hh:mm:ss") : ""%>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateColumn>
                                                                        <asp:TemplateColumn HeaderText="<%$Resources:cms.language, lblGhichu%>">
                                                                            <ItemTemplate>
                                                                                <%# DataBinder.Eval(Container.DataItem, "Comment") %>
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
                                                                            <asp:Button runat="server" ID="btnSendTwoBottom" CssClass="iconPub" CausesValidation="false"
                                                                                Font-Bold="true" OnClick="link_duyet_Click" Text="<%$Resources:cms.language, lblXuatban%>" />
                                                                            &nbsp;&nbsp;&nbsp;
                                                                            <asp:Button runat="server" ID="btnReturnTwoBottom" CausesValidation="false" CssClass="iconReply"
                                                                                Text="<%$Resources:cms.language, lblTralai%>" OnClick="link_tralai_Click" />
                                                                        </td>
                                                                        <td style="text-align: right" class="pageNav">
                                                                            <cc1:CurrentPage runat="server" ID="CurrentPage_HuyXB" CssClass="pageNavTotal">
                                                                            </cc1:CurrentPage>
                                                                            <cc1:Pager runat="server" ID="Pager_HuyXB" OnIndexChanged="Pager_HuyXB_IndexChanged">
                                                                            </cc1:Pager>
                                                                        </td>
                                                                    </tr>
                                                                </table>
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
                                    <cc2:TabPanel HeaderText="<%$Resources:cms.language, lblGocanhXB%>" ID="TabPanel_daXB" runat="server">
                                        <ContentTemplate>
                                            <asp:UpdatePanel ID="UpdatePanel12" runat="server">
                                                <ContentTemplate>
                                                    <table width="100%" cellspacing="2" cellpadding="2" border="0">
                                                        <tr>
                                                            <td align="left" colspan="2">
                                                                <asp:DataGrid runat="server" ID="DataGrid_daduyet" AutoGenerateColumns="false" DataKeyField="Cat_Album_ID"
                                                                    Width="100%" CssClass="Grid">
                                                                    <ItemStyle CssClass="GridItem"></ItemStyle>
                                                                    <AlternatingItemStyle CssClass="GridAltItem" />
                                                                    <HeaderStyle CssClass="GridHeader"></HeaderStyle>
                                                                    <Columns>
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
                                                                                        ImageAlign="AbsMiddle" Style="padding-top: 3px; border: 0; cursor: pointer; width: 120px; height:80px;" ToolTip="Xem phóng sự" BorderStyle="None"></asp:ImageButton>
                                                                                </div>
                                                                                <asp:Label ID="lblcatid" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container.DataItem, "Cat_Album_ID")%>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateColumn>
                                                                        <asp:TemplateColumn HeaderText="<%$Resources:cms.language, lblTengocanh%>">
                                                                            <HeaderStyle HorizontalAlign="Center" Width="40%"></HeaderStyle>
                                                                            <ItemStyle HorizontalAlign="Left" Width="40%"></ItemStyle>
                                                                            <ItemTemplate>
                                                                                <%# DataBinder.Eval(Container.DataItem, "Cat_Album_Name")%>
                                                                                <asp:Label ID="lblTenPS" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container.DataItem, "Cat_Album_Name")%>'></asp:Label>
                                                                            </ItemTemplate>
                                                                            <ItemTemplate>
                                                                                <%# DataBinder.Eval(Container.DataItem, "Cat_Album_Name")%>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateColumn>
                                                                        <asp:TemplateColumn Visible="false" HeaderText="<%$Resources:cms.language, lblAnpham%>">
                                                                            <HeaderStyle HorizontalAlign="Center" Width="10%"></HeaderStyle>
                                                                            <ItemStyle HorizontalAlign="Center" Width="10%"></ItemStyle>
                                                                            <ItemTemplate>
                                                                                <%#HPCBusinessLogic.UltilFunc.GetTenAnpham(Eval("Lang_ID"))%>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateColumn>
                                                                        <asp:TemplateColumn HeaderText="<%$Resources:cms.language, lblNguoinhap%>">
                                                                            <HeaderStyle HorizontalAlign="Center" Width="10%"></HeaderStyle>
                                                                            <ItemStyle HorizontalAlign="Center" Width="10%"></ItemStyle>
                                                                            <ItemTemplate>
                                                                                <%#HPCBusinessLogic.UltilFunc.GetUserName(Eval("UserCreated"))%>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateColumn>
                                                                        <asp:TemplateColumn HeaderText="<%$Resources:cms.language, lblNgaynhap%>">
                                                                            <HeaderStyle HorizontalAlign="Center" Width="10%"></HeaderStyle>
                                                                            <ItemStyle HorizontalAlign="Center" Width="10%"></ItemStyle>
                                                                            <ItemTemplate>
                                                                                <%#Eval("Cat_Album_DateCreate") != System.DBNull.Value ? Convert.ToDateTime(Eval("Cat_Album_DateCreate")).ToString("dd/MM/yyyy hh:mm:ss") : ""%>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateColumn>
                                                                        <asp:TemplateColumn HeaderText="<%$Resources:cms.language, lblNguoiXB%>">
                                                                            <HeaderStyle HorizontalAlign="Center" Width="10%"></HeaderStyle>
                                                                            <ItemStyle HorizontalAlign="Center" Width="10%"></ItemStyle>
                                                                            <ItemTemplate>
                                                                                <%#HPCBusinessLogic.UltilFunc.GetUserName(Eval("Cat_Album_UserIDApprove"))%>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateColumn>
                                                                        <asp:TemplateColumn HeaderText="<%$Resources:cms.language, lblNgayXB%>">
                                                                            <HeaderStyle HorizontalAlign="Center" Width="10%"></HeaderStyle>
                                                                            <ItemStyle HorizontalAlign="Center" Width="10%"></ItemStyle>
                                                                            <ItemTemplate>
                                                                                <%#Eval("Cat_Album_DateApprove") != System.DBNull.Value ? Convert.ToDateTime(Eval("Cat_Album_DateApprove")).ToString("dd/MM/yyyy hh:mm:ss") : ""%>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateColumn>
                                                                    </Columns>
                                                                </asp:DataGrid>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="2" style="text-align: right" class="pageNav">
                                                                <cc1:CurrentPage runat="server" ID="CurrentPage_daduyet" CssClass="pageNavTotal">
                                                                </cc1:CurrentPage>
                                                                <cc1:Pager runat="server" ID="Pager_daduyet" OnIndexChanged="Pager_daduyet_IndexChanged">
                                                                </cc1:Pager>
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
    <!--Phần popup-->
    <div style="clear: both;" />
    <a id="hnkAddMenu" runat="server" style="visibility: hidden"></a>
    <asp:Label ID="lbl_CATID" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lbl_News_CopyFrom_ID" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lbl_News_ID" runat="server" Text="" Visible="true"></asp:Label>
    <cc2:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="hnkAddMenu"
        BackgroundCssClass="ModalPopupBG" PopupControlID="Panelone" Drag="true" PopupDragHandleControlID="PopupHeader">
    </cc2:ModalPopupExtender>
    <div id="Panelone" style="display: none;">
        <div class="popup_ContainerEvent">
            <div class="popup_Titlebar" id="PopupHeader">
                <div class="TitlebarLeft">
                    <asp:Literal runat="server" ID="litTittleForm"></asp:Literal>
                </div>
                <div class="TitlebarRight" onclick="cancel();">
                </div>
            </div>
            <div class="popup_BodyCopy">
                <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                    <ContentTemplate>
                        <div id="displayContainer">
                            <table width="100%" cellspacing="2" cellpadding="2" border="0" style="background-color: white;">
                                <tr>
                                    <td style="width: 100%; text-align: right" colspan="2">
                                        <div class="popup_Body_Fix_width_heightCopy" style="width: 98%">
                                            <table border="0" cellpadding="1" cellspacing="1" style="width: 100%; text-align: left;">
                                                <tr>
                                                    <td>
                                                        <asp:DataGrid runat="server" ID="dgCategorysCopy" AutoGenerateColumns="false" DataKeyField="ID"
                                                            Width="100%" CssClass="Grid" OnEditCommand="dgCategorysCopy_EditCommand">
                                                            <ItemStyle CssClass="GridItem"></ItemStyle>
                                                            <AlternatingItemStyle CssClass="GridAltItem" />
                                                            <HeaderStyle CssClass="GridHeader"></HeaderStyle>
                                                            <Columns>
                                                                <asp:TemplateColumn>
                                                                    <HeaderStyle HorizontalAlign="Center" Width="6%"></HeaderStyle>
                                                                    <ItemStyle HorizontalAlign="Center" Width="6%"></ItemStyle>
                                                                    <HeaderTemplate>
                                                                        <asp:CheckBox ID="chkAll" onclick="javascript:checkAll_CM(this);" runat="server"
                                                                            ToolTip="<%$Resources:cms.language, lblChontatca%>"></asp:CheckBox>
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:CheckBox runat="server" Text='' ID='optSelect' AutoPostBack="False" Enabled="true">
                                                                        </asp:CheckBox>
                                                                    </ItemTemplate>
                                                                </asp:TemplateColumn>
                                                                <asp:TemplateColumn Visible="false" HeaderText="<%$Resources:cms.language, lblAnpham%>">
                                                                    <HeaderStyle Width="50%" HorizontalAlign="Center"></HeaderStyle>
                                                                    <ItemStyle HorizontalAlign="Left" Width="50%"></ItemStyle>
                                                                    <ItemTemplate>
                                                                        <%#Eval("TenNgonNgu")%>
                                                                    </ItemTemplate>
                                                                </asp:TemplateColumn>
                                                                <asp:TemplateColumn HeaderText="<%$Resources:cms.language, lblDich%>">
                                                                    <HeaderStyle HorizontalAlign="Center" Width="15%"></HeaderStyle>
                                                                    <ItemStyle HorizontalAlign="Center" Width="15%"></ItemStyle>
                                                                    <ItemTemplate>
                                                                        <asp:ImageButton ID="btnCopy" Width="15px" runat="server" ImageUrl="~/Dungchung/images/Copy.gif"
                                                                            ImageAlign="AbsMiddle" ToolTip="Copy" CommandName="Edit" CommandArgument="EditCopy"
                                                                            BorderStyle="None"></asp:ImageButton>
                                                                    </ItemTemplate>
                                                                </asp:TemplateColumn>
                                                            </Columns>
                                                        </asp:DataGrid>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: left;" align="left">
                                                        <asp:Button runat="server" ID="but_XB" CssClass="iconCopy" Font-Bold="true" OnClick="but_Trans_Click"
                                                            Text="<%$Resources:cms.language, lblDich%>"></asp:Button>
                                    </td>
                                    <td style="text-align: right;">
                                        <asp:Button runat="server" ID="Button1" CssClass="iconExit" OnClientClick="cancel();"
                                                            Text="<%$Resources:cms.language, lblThoat%>"></asp:Button>
                                        <%--<input class="iconExit" type="button" value="Thoát" onclick="cancel();" />--%>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        </td> </tr> </table> </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
</asp:Content>
