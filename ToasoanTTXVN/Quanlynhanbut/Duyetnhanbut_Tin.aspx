<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/ITC.Master" AutoEventWireup="true"
    CodeBehind="Duyetnhanbut_Tin.aspx.cs" Inherits="ToasoanTTXVN.Quanlynhanbut.Duyetnhanbut_Tin" %>

<%@ Register TagPrefix="nbc" Namespace="HPCComponents.UI" Assembly="HPCComponents" %>
<%@ Register TagPrefix="anthem" Namespace="Anthem" Assembly="Anthem" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<%@ Register TagPrefix="cc1" Namespace="HPCComponents.UI.WebControls" Assembly="HPCComponents" %>
<%@ Import Namespace="HPCComponents" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <style type="text/css">
        .HeaderGrid
        {
            background: transparent url(../Dungchung/Images/bgMenu.jpg) repeat-x scroll left top;
            height: auto;
            color: White;
            font-size: 12px;
            font-weight: bold;
            padding-left: 3px;
        }
        .HideLable
        {
            display: none;
        }
        .boundposition
        {
            position: relative;
            width: 50px;
            float: left;
            top: 0;
            left: 0;
        }
        .Classtextbox
        {
            width: 30px;
            position: absolute;
            top: -10px;
            left: 0px;
            z-index: 2;
        }
        .Classdrop
        {
            top: -10px;
            left: 0px;
            z-index: 1;
            width: 51px;
            position: absolute;
        }
        .select-free
        {
            position: absolute;
            z-index: 10; /*any value*/
            overflow: hidden; /*must have*/
            width: 50px; /*must have for any value*/ /*width of area +5*/
        }
        .select-free iframe
        {
            display: none; /*sorry for IE5*/
            display: /**/ block; /*sorry for IE5*/
            position: absolute; /*must have*/
            top: 0px; /*must have*/
            left: 0px; /*must have*/
            z-index: -1; /*must have*/
            filter: mask(); /*must have*/
            width: 3000px; /*must have for any big value*/
            height: 3000px /*must have for any big value*/;
        }
        .select-free .bd
        {
            padding: 11px;
        }
    </style>

    <script src="../Dungchung/Scripts/Lib.js" type="text/javascript"></script>

    <script type="text/javascript" language="javascript">

        function open_window_View(url, height, width) {
            var tmp_Window = window.open(url, 'popup', 'location=no,directories=no,resizable=yes,status=yes,toolbar=no,menubar=no, width=' + width + ',height=' + height + ',scrollbars=yes,top=5,left=150');
        }

        function checktype(a) {
            if (a == 1) {
                var checktin = document.getElementById('ctl00_MainContent_Check_Tinbai');
                var checkvideo = document.getElementById('ctl00_MainContent_Check_Video');
                var checkanh = document.getElementById('ctl00_MainContent_Check_Image');
                if (checktin.checked) {
                    checkvideo.checked = false;
                    checkanh.checked = false;
                }
            }
            if (a == 3) {
                var checktin = document.getElementById('ctl00_MainContent_Check_Tinbai');
                var checkvideo = document.getElementById('ctl00_MainContent_Check_Video');
                var checkanh = document.getElementById('ctl00_MainContent_Check_Image');
                if (checkvideo.checked) {
                    checktin.checked = false;
                    checkanh.checked = false;
                }
            }

            if (a == 2) {
                var checktin = document.getElementById('ctl00_MainContent_Check_Tinbai');
                var checkvideo = document.getElementById('ctl00_MainContent_Check_Video');
                var checkanh = document.getElementById('ctl00_MainContent_Check_Image');
                if (checkanh.checked) {
                    checkvideo.checked = false;
                    checktin.checked = false;
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
                            <img src="../Dungchung/Images/Icons/to-do-list-cheked-all-icon.png" width="16px" height="16px" />
                        </td>
                        <td style="vertical-align: middle">
                            <span class="TitlePanel">TÌM KIẾM BÀI VIẾT CHẤM NHUẬN BÚT</span>
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
                <table cellpadding="2" cellspacing="2" border="0" width="100%">
                    <tr>
                        <td>
                            <div class="classSearchHeader">
                                <table border="0" cellpadding="2" cellspacing="2" style="width: 100%">
                                    <tr>
                                        <td style="text-align: right" class="Titlelbl">
                                            Ngôn ngữ:
                                        </td>
                                        <td style="text-align: left">
                                            <anthem:DropDownList AutoPostBack="true" ID="cboNgonNgu" runat="server" Width="80%"
                                                CssClass="inputtext" DataTextField="Languages_Name" DataValueField="Languages_ID"
                                                OnSelectedIndexChanged="cbo_lanquage_SelectedIndexChanged" TabIndex="1">
                                            </anthem:DropDownList>
                                        </td>
                                        <td style="text-align: right" class="Titlelbl">
                                            Chuyên mục:
                                        </td>
                                        <td style="text-align: left">
                                            <anthem:DropDownList AutoPostBack="true" ID="cbo_chuyenmuc" CssClass="inputtext"
                                                runat="server" Width="80%" TabIndex="5">
                                            </anthem:DropDownList>
                                        </td>
                                        <td style="text-align: right" class="Titlelbl">
                                            Người cập nhật :
                                        </td>
                                        <td style="text-align: left">
                                            <asp:DropDownList ID="Drop_Tacgia" runat="server" CssClass="inputtext" Width="80%">
                                            </asp:DropDownList>
                                        </td>
                                        <td style="text-align: right" class="Titlelbl">
                                            Nhuận bút:
                                        </td>
                                        <td align="left">
                                            <asp:DropDownList ID="Drop_nhanbut" CssClass="inputtext" runat="server" Width="60%">
                                                <asp:ListItem Value="1">Chưa chấm</asp:ListItem>
                                                <asp:ListItem Value="2">Đã chấm</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: right" class="Titlelbl">
                                            XB từ ngày :
                                        </td>
                                        <td style="text-align: left;">
                                            <nbc:NetDatePicker ImageUrl="../Dungchung/Images/events.gif" ImageFolder="../Dungchung/Scripts/DatePicker/Images"
                                                CssClass="inputtext" Width="130px" ScriptSource="../Dungchung/Scripts/datepicker.js" ID="txt_tungay"
                                                runat="server" onKeyPress="AscciiDisable()" onfocus="javascript:vDateType='3'"
                                                onKeyUp="DateFormat(this,this.value,event,false,'3')" onBlur="DateFormat(this,this.value,event,true,'3')"></nbc:NetDatePicker>
                                            <asp:RegularExpressionValidator ID="Regularexpressionvalidator2" runat="server" ControlToValidate="txt_tungay"
                                                ValidationExpression="^\d{1,2}/\d{1,2}/\d{4}$" Display="Dynamic">Kiểu ngày:dd/MM/yyyy</asp:RegularExpressionValidator><br />
                                        </td>
                                        <td style="text-align: right" class="Titlelbl">
                                            Đến ngày :
                                        </td>
                                        <td style="text-align: left">
                                            <nbc:NetDatePicker ImageUrl="../Dungchung/Images/events.gif" ImageFolder="../Dungchung/Scripts/DatePicker/Images"
                                                CssClass="inputtext" Width="130px" ScriptSource="../Dungchung/Scripts/datepicker.js" ID="txt_denngay"
                                                runat="server" onKeyPress="AscciiDisable()" onfocus="javascript:vDateType='3'"
                                                onKeyUp="DateFormat(this,this.value,event,false,'3')" onBlur="DateFormat(this,this.value,event,true,'3')"></nbc:NetDatePicker>
                                            <asp:RegularExpressionValidator ID="Regularexpressionvalidator1" runat="server" ControlToValidate="txt_denngay"
                                                ValidationExpression="^\d{1,2}/\d{1,2}/\d{4}$" Display="Dynamic">Kiểu ngày:dd/MM/yyyy</asp:RegularExpressionValidator><br />
                                        </td>
                                        <td style="text-align: right" class="Titlelbl">
                                            Tiêu đề bài viết :
                                        </td>
                                        <td style="text-align: left">
                                            <asp:TextBox ID="txt_tieude" runat="server" Width="90%" CssClass="inputtext" onkeypress="return clickButton(event,'ctl00_MainContent_cmdSeek');"></asp:TextBox>
                                        </td>
                                        <%--<td style="text-align: left" class="Titlelbl" colspan="2">
                                            <asp:CheckBox ID="Check_Tinbai" Text="Tin bài" runat="server" Checked="true" onclick="checktype(1)" />
                                            <asp:CheckBox ID="Check_Image" Text="Ảnh " runat="server" onclick="checktype(2)" />
                                            <asp:CheckBox ID="Check_Video" Text="Video " runat="server" onclick="checktype(3)" />
                                        </td>
                                        <td style="text-align: left">
                                        </td>--%>
                                        <td style="text-align: right; width: 10%;" class="Titlelbl">
                                            Loại tin:
                                        </td>
                                        <td style="text-align: left; width: 20%;">
                                            <asp:DropDownList AutoCallBack="true" ID="cbo_types" runat="server" CssClass="inputtext"
                                                Width="60%" TabIndex="2">
                                                <asp:ListItem Value="1" Text="Báo điện tử"></asp:ListItem>
                                                <asp:ListItem Value="2" Text="Góc ảnh"></asp:ListItem>
                                                <asp:ListItem Value="3" Text="Âm thanh - Hình ảnh"></asp:ListItem>
                                                <asp:ListItem Value="4" Text="Thời sự qua ảnh"></asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="8" style="height: 8px">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="8" align="center">
                                            <asp:Button ID="cmd_Search" CssClass="iconFind" Font-Bold="true" runat="server" Text="Tìm kiếm"
                                                OnClick="cmd_Search_Click" />
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" style="width: 10px">
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 4px">
                            <asp:Panel ID="Panel_DS_Ketqua" runat="server" Visible="false">
                                <table border="0" width="100%">
                                    <tr>
                                        <td>
                                            <asp:DataGrid ID="dgr_tintuc" runat="server" Width="100%" AutoGenerateColumns="False"
                                                BorderColor="#d4d4d4" CellPadding="0" BorderStyle="None" CssClass="Grid" BackColor="White"
                                                BorderWidth="1px" AlternatingItemStyle-BackColor="#F1F1F2" DataKeyField="News_ID"
                                                OnEditCommand="dgData_EditCommand">
                                                <SelectedItemStyle Font-Bold="True" ForeColor="#CCFF99" BackColor="#009999"></SelectedItemStyle>
                                                <AlternatingItemStyle CssClass="GridAltItem" />
                                                <HeaderStyle CssClass="GridHeader"></HeaderStyle>
                                                <Columns>
                                                    <asp:TemplateColumn>
                                                        <HeaderStyle HorizontalAlign="Center" Width="4%"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Center" CssClass="GridBorderVerSolid"></ItemStyle>
                                                        <HeaderTemplate>
                                                            STT
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <%# pages.PageIndex * 10 + Container.ItemIndex + 1%>
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                    <asp:BoundColumn DataField="News_ID" HeaderText="News_ID" Visible="False"></asp:BoundColumn>
                                                    <asp:TemplateColumn>
                                                        <HeaderStyle HorizontalAlign="Left" Width="25%"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Left" CssClass="GridBorderVerSolid"></ItemStyle>
                                                        <HeaderTemplate>
                                                            Tên bài viết
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <a style="font-weight: bold; font-size: 13px; color: #0a5ec1; margin-right: 5px;
                                                                display: block" href="<%#SetLinkPopup(Eval("LoaiID"),Eval("News_ID")) %>">
                                                                <%# DataBinder.Eval(Container.DataItem, "News_Tittle" )%></a> <span style="color: #cc0200;
                                                                    font-size: 12px; display: block;">
                                                                    <%--<%# gettonganh(Eval("CountImage"))%></span>--%>
                                                            <asp:Label ID="lbl_loaiID" runat="server" Text='<%# Eval("LoaiID")%>' Visible="false" />
                                                            <asp:Label ID="lbl_title" runat="server" Text='<%# Eval("News_Tittle")%>' Visible="false" />
                                                            <asp:Label ID="lbl_ID" runat="server" Text='<%# Eval("News_ID")%>' Visible="false" />
                                                            <%--<a target="_blank" style="text-decoration: none; font-size: 13px;" href="<%=Global.ApplicationPath%>/View/ViewDetails.aspx?Menu_ID=<%=Page.Request.QueryString["Menu_ID"]%>&ID=<%# DataBinder.Eval(Container.DataItem, "News_ID") %>">
                                                                <%# DataBinder.Eval(Container.DataItem, "News_Tittle" )%>
                                                            </a>
                                                            <asp:LinkButton CssClass="linkEdit" Text='<%# DataBinder.Eval(Container.DataItem, "News_Tittle" )%>'
                                                                runat="server" ID="linkTittle" Visible="false" ToolTip="Sửa bài "></asp:LinkButton>
                                                            <asp:Label ID="lbl_loaiID" runat="server" Text='<%# Eval("LoaiID")%>' Visible="false" />
                                                            <asp:Label ID="lbl_ID" runat="server" Text='<%# Eval("News_ID")%>' Visible="false" />--%>
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                    <asp:TemplateColumn>
                                                        <HeaderStyle HorizontalAlign="Center" Width="10%"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Center" CssClass="GridBorderVerSolid"></ItemStyle>
                                                        <HeaderTemplate>
                                                            Ngày xuất bản
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <%#Eval("News_DatePublished") != System.DBNull.Value ? Convert.ToDateTime(Eval("News_DatePublished")).ToString("dd/MM/yyyy HH:mm") : ""%>
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                    <asp:TemplateColumn HeaderText="Chuyên mục">
                                                        <HeaderStyle Width="10%" HorizontalAlign="Center"></HeaderStyle>
                                                        <ItemStyle Width="10%" HorizontalAlign="Center" CssClass="GridBorderVerSolid"></ItemStyle>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblchuyenmuc" runat="server" Text='<%# GetCategoryName(Eval("CAT_ID"),Eval("LoaiID"))%>' /></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                    <asp:TemplateColumn>
                                                        <HeaderStyle HorizontalAlign="Center" Width="10%"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Center" Width="10%" CssClass="GridBorderVerSolid"></ItemStyle>
                                                        <HeaderTemplate>
                                                            Tác giả
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbltacgia" runat="server" Text='<%# Eval("News_AuthorName")%>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                    <asp:TemplateColumn>
                                                        <HeaderStyle HorizontalAlign="Center" Width="10%"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Center" Width="10%" CssClass="GridBorderVerSolid"></ItemStyle>
                                                        <HeaderTemplate>
                                                            Tiền nhuận bút
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txt_tien" CssClass="inputtext" onKeyPress="return check_num(this,15,event);"
                                                                onkeyup="javascript:return CommaMonney(this.id);" runat="server" Width="90%"
                                                                Text='<%#Eval("News_TienNB")%>'></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                    <asp:TemplateColumn>
                                                        <HeaderStyle HorizontalAlign="Center" Width="5%"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Center" Width="5%" CssClass="GridBorderVerSolid"></ItemStyle>
                                                        <HeaderTemplate>
                                                            Số ảnh đính kèm
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblcount" CssClass="iconNumber" Text='<%# gettonganh(Eval("CountImage"))%>'
                                                                runat="server" />
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                    <asp:TemplateColumn>
                                                        <HeaderStyle HorizontalAlign="Center" Width="5%"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Center" Width="5%" CssClass="GridBorderVerSolid"></ItemStyle>
                                                        <HeaderTemplate>
                                                            Nhuận bút ảnh
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <a href="Javascript:PopupWindow('<%=Global.ApplicationPath%>/Quanlynhanbut/Duyetnhanbut_Anh.aspx?Menu_ID=<%=Page.Request.QueryString["Menu_ID"]%>&ID=<%# DataBinder.Eval(Container.DataItem, "News_ID") %>&TypeID=<%# DataBinder.Eval(Container.DataItem, "LoaiID") %>',50,700,100,500);" />
                                                            <img src="../Dungchung/Images/payment.JPG" border="0" alt="<%# DataBinder.Eval(Container.DataItem, "News_Tittle") %>"
                                                                border="0" onmouseover="(window.status=''); return true" style='cursor: hand;
                                                                display: <%# Visible_CountImage(Eval("CountImage")) %>' title="Chấm nhuận bút ảnh" />
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                    <%--<asp:TemplateColumn Visible="false">
                                                        <HeaderStyle HorizontalAlign="Center" Width="5%"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Center" Width="5%" CssClass="GridBorderVerSolid"></ItemStyle>
                                                        <HeaderTemplate>
                                                            Xem
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <a href="Javascript:open_window_Scroll('<%=Global.ApplicationPath%>/BaoDienTu/ViewAndPrint.aspx?Menu_ID=<%=Page.Request.QueryString["Menu_ID"]%>&ID=<%# DataBinder.Eval(Container.DataItem, "News_ID") %>',50,500,100,800);" />
                                                            <asp:Label ID="lbl_view" runat="server" Text="Xem nội dung"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>--%>
                                                    <%--    <asp:TemplateColumn>
                                                        <HeaderStyle HorizontalAlign="Center" Width="5%"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Center" Width="5%" CssClass="GridBorderVerSolid"></ItemStyle>
                                                        <HeaderTemplate>
                                                            Sửa
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="btnEdit" Width="15px" runat="server" ImageUrl="~/Images/edit.gif" ToolTip="Sửa thông tin" 
                                                            CommandName="Edit" CommandArgument="EditTT" />
                                                            <asp:ImageButton ID="btnUpdate" Width="15px" runat="server" ImageUrl="~/images/save.gif"
                                                                ImageAlign="AbsMiddle" ToolTip="Cập nhật" Visible="false" CommandName="Edit"
                                                                CommandArgument="update" BorderStyle="None" />
                                                            <asp:ImageButton ID="btnCancel" Width="15px" runat="server" ImageUrl="~/images/undo.gif"
                                                                ImageAlign="AbsMiddle" ToolTip="Hủy bỏ" Visible="false" CommandName="Edit" CommandArgument="back"
                                                                BorderStyle="None" />
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>--%>
                                                </Columns>
                                            </asp:DataGrid>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <div class="classSearchHeader">
                                                <table border="0" width="100%">
                                                    <tr>
                                                        <td style="text-align: left">
                                                            <asp:Button ID="cmd_Chamnhanbut" runat="server" Text="CHẤM NHUẬN BÚT" CssClass="iconSave"
                                                                Visible="false" OnClick="cmd_Chamnhanbut_Click" />&nbsp;&nbsp;<!--<asp:Button ID="btnExport" CssClass="iconFind"
                                                    runat="server" Text="[Xuất Excel]" OnClick="linkExport_OnClick"></asp:Button>-->
                                                        </td>
                                                        <td style="text-align: right" class="pageNav">
                                                            <cc1:CurrentPage runat="server" ID="CurrentPage2" CssClass="pageNavTotal">
                                                        &nbsp;
                                                            </cc1:CurrentPage>
                                                            <cc1:Pager runat="server" ID="pages" OnIndexChanged="pages_IndexChanged"></cc1:Pager>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </td>
                    </tr>
                    <tr>
                        <td valign="top">
                            <table width="100%" cellspacing="0" cellpadding="0" border="0">
                                <tr>
                                    <td align="left">
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
