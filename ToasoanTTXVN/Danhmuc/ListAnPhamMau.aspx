<%@ Page Language="C#" MasterPageFile="~/Masters/ITC.Master" AutoEventWireup="true"
    CodeBehind="ListAnPhamMau.aspx.cs" Inherits="ToasoanTTXVN.Danhmuc.ListAnPhamMau"
    Title="" EnableEventValidation="true" %>

<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral,PublicKeyToken=31BF3856AD364E35" %>
<%@ Register TagPrefix="anthem" Namespace="Anthem" Assembly="Anthem" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<%@ Register TagPrefix="cc1" Namespace="HPCComponents.UI.WebControls" Assembly="HPCComponents" %>
<%@ Import Namespace="HPCComponents" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <script src="../Dungchung/Scripts/jquery-1.4.2.min.js" type="text/javascript"></script>

    <script src="../Dungchung/Scripts/jquery.blockUI.js" type="text/javascript"></script>

    <script type="text/javascript">
        function cancel() {
            $get('ctl00_MainContent_btnCancel').click();
        }
        function ValidateText(i) {
            if (i.value.length > 0) {
                i.value = i.value.replace(/[^\d]+/g, '');
            }
        }

        function Submit() {
            var txtNamefirst = "GVAnPhamLayout_ctl";
            var txtNameLast = "_txt_Trang";

            var chkNamefirst = "GVAnPhamLayout_ctl";
            var chkNameLast = "_ddl_Layout";

            var temptxtobj = document.getElementById(txtNamefirst + "02" + txtNameLast);
            var tempchkobj = document.getElementById(chkNamefirst + "02" + chkNameLast);
            var i = 1;
            var txtFlag = false;
            var drpFlag = false;
            var id;
            while (temptxtobj != null && tempchkobj != null) {
                if (!txtFlag && temptxtobj.value != "")
                    txtFlag = true;

                if (!drpFlag && tempchkobj.selectedIndex >= 0)
                    drpFlag = true;
                i++;
                if (i < 10)
                    id = "0" + i;
                else
                    id = i;
                temptxtobj = document.getElementById(txtNamefirst + id + txtNameLast);
                tempchkobj = document.getElementById(chkNamefirst + id + chkNameLast);
            }
            if (!(txtFlag && drpFlag))
            //alert("select");
                temptxtobj.style.background = 'orange';
            // return  txtFlag && drpFlag;

        }
    </script>

    <table border="0" cellpadding="0" width="100%" cellspacing="0">
        <tr>
            <td class="datagrid_top_left">
            </td>
            <td class="datagrid_top_center">
                <span class="TitlePanel" style="float: left;">DANH MỤC ẤN PHẨM MẪU</span>
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
                                    <td style="text-align: left; width: 10%;">
                                        <asp:Button runat="server" ID="btnAdd" CssClass="myButton blue" Font-Bold="true"
                                            OnClick="btnAdd_Click" Text="<%$Resources:cms.language, lblThemmoi%>" ToolTip="Thêm mới">
                                        </asp:Button>
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
                            <asp:GridView ID="GVAnphamMau" runat="Server" AutoGenerateColumns="False" BackColor="White"
                                CssClass="Grid" GridLines="Vertical" Width="100%" OnRowDataBound="GVAnphamMau_OnRowDataBound"
                                OnRowCommand="GVAnphamMau_OnRowCommand1" OnRowDeleting="GVAnphamMau_RowDeleting"
                                OnRowEditing="GVAnphamMau_RowEditing" OnRowUpdating="GVAnphamMau_RowUpdating"
                                OnRowCancelingEdit="GVAnphamMau_RowCancelingEdit" ShowFooter="False" AutoGenerateEditButton="false"
                                DataKeyNames="Ma_Mau" EnableViewState="True">
                                <RowStyle CssClass="GridItem" Height="25px" />
                                <AlternatingRowStyle CssClass="GridAltItem" />
                                <HeaderStyle CssClass="GridHeader"></HeaderStyle>
                                <Columns>
                                    <asp:BoundField DataField="Ma_Mau" HeaderText="" ReadOnly="True" Visible="false" />
                                    <asp:TemplateField HeaderText="STT">
                                        <HeaderStyle HorizontalAlign="Center" Width="5%" />
                                        <ItemStyle HorizontalAlign="Center" Width="5%" CssClass="GridBorderVerSolid" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblSTT" runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Mô tả">
                                        <HeaderStyle HorizontalAlign="Center" Width="50%" />
                                        <ItemStyle HorizontalAlign="Center" Width="50%" CssClass="GridBorderVerSolid" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblMota" runat="server" Text='<%# Eval("Mota")%>'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txt_mota" runat="Server" Text='<%# Eval("Mota") %>' TextMode="MultiLine"
                                                Rows="2" Width="100%"></asp:TextBox><br />
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="..."
                                                ControlToValidate="txt_mota">(*)
                                            </asp:RequiredFieldValidator>
                                        </EditItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox ID="txt_mota" runat="Server" TextMode="MultiLine" Rows="2" Width="100%"></asp:TextBox><br />
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="..."
                                                ControlToValidate="txt_mota">(*)
                                            </asp:RequiredFieldValidator>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Ấn phẩm">
                                        <HeaderStyle HorizontalAlign="Center" Width="20%" />
                                        <ItemStyle HorizontalAlign="Center" Width="20%" CssClass="GridBorderVerSolid" />
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
                                    <asp:TemplateField HeaderText="Sửa">
                                        <HeaderStyle HorizontalAlign="Center" Width="5%" />
                                        <ItemStyle HorizontalAlign="Center" Width="5%" CssClass="GridBorderVerSolid" />
                                        <ItemTemplate>
                                            <asp:ImageButton ID="btnAdd" Width="15px" runat="server" ImageUrl="~/Dungchung/images/action.gif"
                                                ImageAlign="AbsMiddle" ToolTip="Sửa thông tin" CommandName="Edit" CommandArgument="Edit"
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
                                    <asp:TemplateField>
                                        <HeaderStyle HorizontalAlign="Center" Width="8%"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Center" Width="8%" CssClass="GridBorderVerSolid"></ItemStyle>
                                        <HeaderTemplate>
                                            Chọn Măng xéc
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:ImageButton ID="btnViewMangxec" Width="15px" runat="server" ImageUrl="~/Dungchung/Images/vietnam.gif"
                                                ImageAlign="AbsMiddle" ToolTip="Chọn Layout" CommandName="Mangxec" BorderStyle="None">
                                            </asp:ImageButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Chọn Layout">
                                        <HeaderStyle HorizontalAlign="Center" Width="10%" />
                                        <ItemStyle HorizontalAlign="Center" Width="10%" CssClass="GridBorderVerSolid" />
                                        <ItemTemplate>
                                            <asp:ImageButton ID="btnView" Width="15px" runat="server" ImageUrl="~/Dungchung/Images/zoom.gif"
                                                ImageAlign="AbsMiddle" ToolTip="Chọn Layout" OnClick="Layout" BorderStyle="None">
                                            </asp:ImageButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Xóa">
                                        <HeaderStyle HorizontalAlign="Center" Width="5%" />
                                        <ItemStyle HorizontalAlign="Center" Width="5%" CssClass="GridBorderVerSolid" />
                                        <ItemTemplate>
                                            <asp:ImageButton ID="btnDelete" Width="15px" runat="server" ImageUrl="~/Dungchung/images/cancel.gif"
                                                ImageAlign="AbsMiddle" ToolTip="Xóa" CommandName="Delete" BorderStyle="None">
                                            </asp:ImageButton>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:ImageButton ID="btnCancel" Width="15px" runat="server" ImageUrl="~/Dungchung/Images/Icons/Cancel.gif"
                                                ImageAlign="AbsMiddle" ToolTip="Hủy" CausesValidation="false" CommandName="Cancel"
                                                BorderStyle="None"></asp:ImageButton>
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
                        <td>
                            <a id="hnkAddMenu" runat="server" style="visibility: hidden"></a>
                            <cc2:ModalPopupExtender ID="popup" BackgroundCssClass="ModalPopupBG" runat="server"
                                TargetControlID="hnkAddMenu" CancelControlID="btnCancel" PopupControlID="AnPham_Layout"
                                Drag="true" PopupDragHandleControlID="PopupHeader">
                            </cc2:ModalPopupExtender>
                            <div id="AnPham_Layout" style="display: none; width: 750px;">
                                <div class="popup_Container">
                                    <div class="popup_Titlebar" id="PopupHeader">
                                        <asp:UpdatePanel ID="UpnTit" runat="server">
                                            <ContentTemplate>
                                                <div class="TitlebarLeft">
                                                    <asp:Literal runat="server" ID="litTittleForm" Text="DANH SÁCH ẤN PHẨM MẪU - LAYOUT"></asp:Literal>
                                                </div>
                                                <div class="TitlebarRight" onclick="cancel();">
                                                </div>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                    <div class="popup_Body">
                                        <asp:UpdatePanel ID="Upbody" runat="server">
                                            <ContentTemplate>
                                                <div id="displayContainer2">
                                                    <table width="100%" cellspacing="2" cellpadding="4" border="0">
                                                        <tr>
                                                            <td style="text-align: center;" colspan="2">
                                                                <asp:Label ID="lbl_Mau" runat="server" Font-Bold="true"></asp:Label>
                                                                -
                                                                <asp:Label ID="lbl_TenAnPham" runat="server" Font-Bold="true"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="text-align: center">
                                                                <asp:Label ID="lblMess" runat="server"></asp:Label>
                                                            </td>
                                                            <td style="text-align: right">
                                                                <asp:ImageButton ID="btnAddPopUp" runat="server" Visible="false" ImageUrl="~/Dungchung/Images/add.jpg"
                                                                    OnClick="btnAddPopUp_Click" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="2" style="text-align: center">
                                                                <asp:GridView ID="GVAnPhamLayout" runat="Server" AutoGenerateColumns="False" BackColor="White"
                                                                    CssClass="Grid" GridLines="Vertical" Width="100%" OnRowDataBound="GVAnPhamLayout_OnRowDataBound"
                                                                    OnRowCommand="GVAnPhamLayout_OnRowCommand" OnRowEditing="GVAnPhamLayout_RowEditing"
                                                                    OnRowUpdating="GVAnPhamLayout_RowUpdating" OnRowCancelingEdit="GVAnPhamLayout_RowCancelingEdit"
                                                                    ShowFooter="False" AutoGenerateEditButton="false" DataKeyNames="ID" EnableViewState="True">
                                                                    <RowStyle CssClass="GridItem" Height="25px" />
                                                                    <AlternatingRowStyle CssClass="GridAltItem" />
                                                                    <HeaderStyle CssClass="GridHeader"></HeaderStyle>
                                                                    <Columns>
                                                                        <asp:BoundField DataField="ID" HeaderText="" ReadOnly="True" Visible="false" />
                                                                        <asp:TemplateField HeaderText="STT">
                                                                            <HeaderStyle HorizontalAlign="Center" Width="5%" />
                                                                            <ItemStyle HorizontalAlign="Center" Width="5%" CssClass="GridBorderVerSolid" />
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblSTT" runat="server"></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Trang">
                                                                            <HeaderStyle HorizontalAlign="Center" Width="50%" />
                                                                            <ItemStyle HorizontalAlign="Center" Width="50%" CssClass="GridBorderVerSolid" />
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblMota" runat="server" Text='<%# Eval("Trang")%>'></asp:Label>
                                                                            </ItemTemplate>
                                                                            <%-- <EditItemTemplate>
                                                                                <asp:TextBox ID="txt_Trang" runat="Server" Text='<%# Eval("Trang") %>' TextMode="MultiLine"
                                                                                    Rows="2" Width="100%" onkeyup="ValidateText(this);"></asp:TextBox>
                                                                            </EditItemTemplate>--%>
                                                                            <FooterTemplate>
                                                                                <asp:TextBox ID="txt_Trang" runat="Server" Width="100%"></asp:TextBox><br />
                                                                            </FooterTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Layout">
                                                                            <HeaderStyle HorizontalAlign="Center" Width="20%" />
                                                                            <ItemStyle HorizontalAlign="Center" Width="20%" CssClass="GridBorderVerSolid" />
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblLayout" runat="server" Text='<%# Eval("Mota")%>'></asp:Label>
                                                                            </ItemTemplate>
                                                                            <EditItemTemplate>
                                                                                <asp:Label ID='lblLayout' runat="server" Text='<%# Eval("Ma_layout") %> ' Visible="false"></asp:Label>
                                                                                <asp:DropDownList ID="ddl_Layout" CssClass="inputtext" runat="server" Width="100%"
                                                                                    DataTextField="Mota" DataValueField="Ma_Layout">
                                                                                </asp:DropDownList>
                                                                            </EditItemTemplate>
                                                                            <FooterTemplate>
                                                                                <asp:DropDownList ID="ddl_Layout" CssClass="inputtext" runat="server" Width="100%"
                                                                                    DataTextField="Mota" DataValueField="Ma_Layout">
                                                                                </asp:DropDownList>
                                                                            </FooterTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Sửa">
                                                                            <HeaderStyle HorizontalAlign="Center" Width="5%" />
                                                                            <ItemStyle HorizontalAlign="Center" Width="5%" CssClass="GridBorderVerSolid" />
                                                                            <ItemTemplate>
                                                                                <asp:ImageButton ID="btnAdd" Width="15px" runat="server" ImageUrl="~/Dungchung/images/action.gif"
                                                                                    ImageAlign="AbsMiddle" ToolTip="Sửa thông tin" CommandName="Edit" CommandArgument="Edit"
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
                                                                    </Columns>
                                                                    <FooterStyle BackColor="#CCCC99" />
                                                                </asp:GridView>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="text-align: center">
                                                                <asp:Label ID="lbl_Message" runat="server" ForeColor="Red" Text=""></asp:Label>
                                                            </td>
                                                            <td style="text-align: right">
                                                                <cc1:CurrentPage runat="server" ID="CurrentPage1"></cc1:CurrentPage>&nbsp;
                                                                <cc1:Pager runat="server" ID="Pager1" OnIndexChanged="pages1_IndexChanged" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="center">
                                                                <input id="btnCancel" value="Thoát" type="button" class="buttonhide" runat="server"
                                                                    onserverclick="btnCancel_Click" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <a id="A1" runat="server" style="visibility: hidden"></a>
                            <cc2:ModalPopupExtender ID="PopupMangXec" BackgroundCssClass="ModalPopupBG" runat="server"
                                TargetControlID="A1" CancelControlID="btnCancel" PopupControlID="Mangxec" Drag="true"
                                PopupDragHandleControlID="PopupHeader">
                            </cc2:ModalPopupExtender>
                            <div id="Mangxec" style="display: none; width: 750px;">
                                <div class="popup_Container">
                                    <div class="popup_Titlebar" id="DivHeder">
                                        <asp:UpdatePanel ID="UpdatePanelHeder" runat="server">
                                            <ContentTemplate>
                                                <div class="TitlebarLeft">
                                                    <asp:Literal runat="server" ID="Literal1" Text="DANH SÁCH MĂNG XÉC"></asp:Literal>
                                                </div>
                                                <div class="TitlebarRight" onclick="cancel();">
                                                </div>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                    <div class="popup_Body">
                                        <asp:UpdatePanel ID="UpdatePanelListMangXec" runat="server">
                                            <ContentTemplate>
                                                <div id="Div3">
                                                    <table width="100%" cellspacing="2" cellpadding="4" border="0">
                                                        <tr>
                                                            <td style="width: 100%" colspan="2">
                                                                <table border="0" cellspacing="1px" cellspacing="1px" style="text-align: right; width: 100%">
                                                                    <tr>
                                                                        <td style="width: 10%; text-align: right" class="Titlelbl">
                                                                            Tên măng xéc:
                                                                        </td>
                                                                        <td style="width: 15%; text-align: left">
                                                                            <asp:TextBox ID="txt_tenanh" runat="server" Width="250px"></asp:TextBox>
                                                                        </td>
                                                                        <td style="width: 20%; text-align: left;">
                                                                            <asp:Button CausesValidation="false" runat="server" ID="Button2" CssClass="myButton green"
                                                                                Font-Bold="true" Text="Tìm kiếm" OnClick="btnSearch_Click"></asp:Button>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 100%" colspan="2">
                                                                <asp:DataGrid runat="server" ID="dgrListAppro" AutoGenerateColumns="false" DataKeyField="Ma_Logo"
                                                                    OnEditCommand="dgrListAppro_OnEditCommand" Width="100%" CssClass="Grid" CellPadding="1">
                                                                    <ItemStyle CssClass="GridItem" Height="25px"></ItemStyle>
                                                                    <AlternatingItemStyle CssClass="GridAltItem" />
                                                                    <HeaderStyle CssClass="GridHeader"></HeaderStyle>
                                                                    <Columns>
                                                                        <asp:BoundColumn Visible="False" DataField="Ma_Logo">
                                                                            <HeaderStyle Width="1%"></HeaderStyle>
                                                                        </asp:BoundColumn>
                                                                        <asp:TemplateColumn>
                                                                            <HeaderTemplate>
                                                                                Măng xéc
                                                                            </HeaderTemplate>
                                                                            <HeaderStyle HorizontalAlign="Center" />
                                                                            <ItemStyle Width="12%" HorizontalAlign="Left" CssClass="GridBorderVerSolid"></ItemStyle>
                                                                            <ItemTemplate>
                                                                                <div class="gallery">
                                                                                    <div class="pictgalery" style="width: 142px; height: 60px">
                                                                                        <asp:LinkButton ID="btnEdit" runat="server" CssClass="linkGridForm" ToolTip="Chọn măng xéc"
                                                                                            CommandName="Edit" CommandArgument="Edit"> <img src="<%=Global.ApplicationPath%><%#Eval("Path_Logo")%>" style="width: 142px;
                                                                                            height: 60px" align="middle" onclick="return openNewImage(this, '')" alt='' /></asp:LinkButton>
                                                                                    </div>
                                                                                </div>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateColumn>
                                                                        <asp:TemplateColumn>
                                                                            <HeaderTemplate>
                                                                                Tiêu đề
                                                                            </HeaderTemplate>
                                                                            <HeaderStyle HorizontalAlign="Left" />
                                                                            <ItemStyle Width="40%" HorizontalAlign="Left" CssClass="GridBorderVerSolid"></ItemStyle>
                                                                            <ItemTemplate>
                                                                                <div class="stringtieudeandsc">
                                                                                    <div class="fontTitle" style="width: 90%;">
                                                                                        <asp:Label ID="lbtitle" runat="server" Text='<%#Eval("Ten_Logo")%>'></asp:Label>
                                                                                    </div>
                                                                                </div>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateColumn>
                                                                    </Columns>
                                                                </asp:DataGrid>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="2" style="height: 10px;">
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="text-align: right" class="pageNav">
                                                                <cc1:CurrentPage runat="server" ID="CurrentPage2"></cc1:CurrentPage>
                                                                &nbsp;
                                                                <cc1:Pager runat="server" ID="pageappro" OnIndexChanged="pageappro_IndexChanged"></cc1:Pager>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>
                            </div>
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
