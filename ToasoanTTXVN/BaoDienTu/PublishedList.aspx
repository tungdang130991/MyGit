<%@ Page Language="C#" MasterPageFile="~/Masters/ITC.Master" AutoEventWireup="true"
    CodeBehind="PublishedList.aspx.cs" Inherits="ToasoanTTXVN.BaoDienTu.PublishedList" %>

<%@ Register TagPrefix="anthem" Namespace="Anthem" Assembly="Anthem" %>
<%@ Register TagPrefix="cc1" Namespace="HPCComponents.UI.WebControls" Assembly="HPCComponents" %>
<%@ Register TagPrefix="nbc" Namespace="HPCComponents.UI" Assembly="HPCComponents" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<%@ Import Namespace="HPCComponents" %>
<%@ Import Namespace="HPCBusinessLogic" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <script language="Javascript" type="text/javascript">
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);
        function EndRequestHandler(sender, args) {
            if (args.get_error() != undefined) {
                args.set_errorHandled(true);
            }
        }
        function checkAll_DM_CM(objRef) {
            var GridView = document.getElementById('<%=dgCategorysCopy.ClientID%>');
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
        function checkAll_DM_CMAll(objRef) {
            var GridView = document.getElementById('<%=dgr_tintuc1.ClientID%>');
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
        function checkAllUnPublisher(objRef) {
            var GridView = document.getElementById('<%=dgListNewsUnPublish.ClientID%>');
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
        function SetInnerProcess(_dangdang, _tralai) {
            document.getElementById("__tab_ctl00_MainContent_TabContainer1_tabpnltinXuly").innerHTML = _dangdang;
            document.getElementById("__tab_ctl00_MainContent_TabContainer1_TabPanel1").innerHTML = _tralai;
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
                            <img src="../Dungchung/images/Icons/to-do-list-cheked-all-icon.png" width="16px"
                                height="16px" />
                        </td>
                        <td style="vertical-align: middle">
                            <span class="TitlePanel"><%= CommonLib.ReadXML("titDsTindaXB")%></span>
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
                <table border="0" width="100%" cellpadding="4" cellspacing="2">
                    <tr>
                        <td>
                            <div class="classSearchHeader">
                                <table style="border: 1" width="100%" cellpadding="2" cellspacing="2">
                                    <tr>
                                        <td style="text-align: left; width: 95%">
                                            <table style="border: 0" width="100%" cellpadding="2" cellspacing="2">
                                                <tr>
                                                    <td class="Titlelbl" style="text-align: right">
                                                        <asp:Label ID="Label1" class="Titlelbl" runat="server" 
                                                            Text="<%$Resources:cms.language, lblAnpham%>"></asp:Label>
                                                    </td>
                                                    <td style="text-align: left">
                                                        <anthem:DropDownList AutoCallBack="true" ID="cboNgonNgu" runat="server" Width="180"
                                                            CssClass="inputtext" OnSelectedIndexChanged="cbo_lanquage_SelectedIndexChanged"
                                                            TabIndex="2">
                                                        </anthem:DropDownList>
                                                    </td>
                                                    <td class="Titlelbl" style="text-align: right">
                                                        <asp:Label ID="Label2" class="Titlelbl" runat="server" 
                                                            Text="<%$Resources:cms.language, lblChuyenmuc%>"></asp:Label>
                                                    </td>
                                                    <td style="text-align: left;">
                                                        <anthem:DropDownList AutoCallBack="true" ID="cbo_chuyenmuc" runat="server" Width="180px"
                                                            CssClass="inputtext" DataTextField="tenchuyenmuc" DataValueField="id" TabIndex="2">
                                                        </anthem:DropDownList>
                                                    </td>
                                                    <td></td>
                                                    <td style="text-align: left;">
                                                        <asp:CheckBox runat="server" Text="<%$Resources:cms.language, lblNoibatchuyenmuc%>" ID="chkNewFocusChild" class="ChkBoxTit"
                                                            TabIndex="10" CssClass="Titlelbl"  Visible="true" />
                                                    </td>
                                                    <td></td>
                                                    <!--<td style="text-align: left;">
                                                        <asp:CheckBox runat="server" Text="Nổi bật trang chủ" ID="chkNewsIsBaidinh" class="ChkBoxTit"
                                                            TabIndex="6" Visible="true" />
                                                    </td>
                                                    <td style="text-align: left;">
                                                        <asp:CheckBox runat="server" Text="Nổi bật CM cha" ID="chkNewFocusParent" class="ChkBoxTit"
                                                            TabIndex="9" Visible="true" />
                                                    </td>
                                                    <td style="text-align: left;">
                                                        <asp:CheckBox runat="server" Text="Tin ảnh" ID="chkImageIsFocus" class="ChkBoxTit"
                                                            TabIndex="11" Visible="true" />
                                                    </td>
                                                    <td style="text-align: left;">
                                                        <asp:CheckBox runat="server" Text="Tin được quan tâm" ID="cbMoreViews" class="ChkBoxTit"
                                                            TabIndex="12" Visible="true" />
                                                    </td>-->
                                                </tr>
                                                <tr>
                                                    <td class="Titlelbl" style="text-align: right">
                                                        <asp:Label ID="Label3" class="Titlelbl" runat="server" 
                                                            Text="<%$Resources:cms.language, lblTungay%>"></asp:Label>
                                                    </td>
                                                    <td style="text-align: left">
                                                        <nbc:NetDatePicker ImageUrl="../Dungchung/images/events.gif" ImageFolder="../Dungchung/Scripts/DatePicker/Images"
                                                            CssClass="inputtext" Width="140px" ScriptSource="../Dungchung/Scripts/datepicker.js"
                                                            ID="txt_tungay" runat="server" onKeyPress="AscciiDisable()" onfocus="javascript:vDateType='3'"
                                                            onKeyUp="DateFormat(this,this.value,event,false,'3')" onBlur="DateFormat(this,this.value,event,true,'3')"
                                                            TabIndex="4"></nbc:NetDatePicker>
                                                        <asp:RegularExpressionValidator ID="Regularexpressionvalidator2" runat="server" ControlToValidate="txt_tungay"
                                                            ValidationExpression="^\d{1,2}/\d{1,2}/\d{4}$" Display="Dynamic"><%= CommonLib.ReadXML("lblKieungay") %></asp:RegularExpressionValidator>
                                                    </td>
                                                    <td class="Titlelbl" style="text-align: right">
                                                        <asp:Label ID="Label4" class="Titlelbl" runat="server" 
                                                            Text="<%$Resources:cms.language, lblDenngay%>"></asp:Label>
                                                    </td>
                                                    <td style="text-align: left">
                                                        <nbc:NetDatePicker ImageUrl="../Dungchung/images/events.gif" ImageFolder="../Dungchung/Scripts/DatePicker/Images"
                                                            CssClass="inputtext" Width="140px" ScriptSource="../Dungchung/Scripts/datepicker.js"
                                                            ID="txt_denngay" runat="server" onKeyPress="AscciiDisable()" onfocus="javascript:vDateType='3'"
                                                            onKeyUp="DateFormat(this,this.value,event,false,'3')" onBlur="DateFormat(this,this.value,event,true,'3')"
                                                            TabIndex="5"></nbc:NetDatePicker>
                                                        <asp:RegularExpressionValidator ID="Regularexpressionvalidator1" runat="server" ControlToValidate="txt_denngay"
                                                            ValidationExpression="^\d{1,2}/\d{1,2}/\d{4}$" Display="Dynamic"><%= CommonLib.ReadXML("lblKieungay") %></asp:RegularExpressionValidator>
                                                    </td>
                                                    <!--<td style="text-align: left;">
                                                        <asp:CheckBox runat="server" Text="Tin nóng trang chủ" ID="chkNewsIsFocus" class="ChkBoxTit"
                                                            TabIndex="7" Visible="true" />
                                                    </td>-->
                                                    
                                                    <!--<td style="text-align: left;">
                                                        <asp:CheckBox runat="server" Text="Tin ẩn" ID="chkHosoIsFocus" class="ChkBoxTit"
                                                            TabIndex="13" Visible="true" />
                                                    </td>-->
                                                    <td style="text-align: left;">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="Titlelbl" style="text-align: right">
                                                        <asp:Label ID="Label8" class="Titlelbl" runat="server" 
                                                            Text="<%$Resources:cms.language, lblTukhoa%>"></asp:Label>
                                                    </td>
                                                    <td style="text-align: left">
                                                        <asp:TextBox ID="txt_tieude" TabIndex="3" Width="169px" runat="server" CssClass="inputtext"
                                                            onkeypress="return clickButton(event,'ctl00_MainContent_cmdSeek');"></asp:TextBox>
                                                    </td>
                                                    <td class="Titlelbl" style="text-align: right">
                                                        <asp:Label ID="Label5" class="Titlelbl" runat="server" 
                                                            Text="<%$Resources:cms.language, lblTenbaiviet%>"></asp:Label>
                                                    </td>
                                                    <td style="text-align: left">
                                                        <asp:TextBox ID="txtTieuDetin" TabIndex="3" Width="85%" runat="server" CssClass="inputtext"
                                                            onkeypress="return clickButton(event,'ctl00_MainContent_cmdSeek');"></asp:TextBox>
                                                    </td>
                                                    <!--<td style="text-align: left;">
                                                        <asp:CheckBox runat="server" Text="Tin tiêu điểm" ID="chkNewTieudiem" class="ChkBoxTit"
                                                            TabIndex="8" Visible="true" />
                                                    </td>
                                                    <td style="text-align: left;">
                                                        <asp:CheckBox runat="server" Text="Tin video" ID="chkVideoIsFocus" class="ChkBoxTit"
                                                            TabIndex="12" Visible="true" />
                                                    </td>
                                                    <td style="text-align: left;">
                                                        <asp:CheckBox runat="server" Text="Không HT Mobile" ID="chDisplayMobi" class="ChkBoxTit"
                                                            TabIndex="12" Visible="true" />
                                                    </td>-->
                                                    <td style="text-align: left;">
                                                        <asp:Button runat="server" ID="cmdSeek" CssClass="iconFind" Style="margin-left: 10px;"
                                                            Font-Bold="true" OnClick="cmdSeek_Click" Text="<%$Resources:cms.language, lblTimkiem%>" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="text-align: center; height: 10px;" colspan="5">
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td style="text-align: left; width: 5%">
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 4px">
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <div class="classSearchHeader">
                                <table width="100%" cellspacing="0" cellpadding="0" border="0">
                                    <tr>
                                        <td align="left">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="height: 5px;">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left">
                                            <cc2:TabContainer ID="TabContainer1" runat="server" CssClass="ajax__tab_yuitabview-theme"
                                                AutoPostBack="true" ActiveTabIndex="0" OnActiveTabChanged="TabContainer1_ActiveTabChanged">
                                                <cc2:TabPanel HeaderText="<%$Resources:cms.language, lblTinxuatban%>" ID="tabpnltinXuly" runat="server">
                                                    <ContentTemplate>
                                                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                            <ContentTemplate>
                                                                <table border="0" cellpadding="2" cellspacing="2" width="100%">
                                                                    <tr>
                                                                        <td style="width: 100%" align="left" colspan="2">
                                                                            <asp:Button runat="server" ID="LinkButton1" CssClass="iconPub" OnClick="HuyDXB_Click"
                                                                                Text="<%$Resources:cms.language, lblHuydang%>" Font-Bold="true" CausesValidation="false" />&nbsp;
                                                                            <!--<asp:Button runat="server" ID="LinkButton_updateTT" CssClass="iconSave" Text="Cập nhật thứ tự hiển thị"
                                                                                Visible="false" Font-Bold="true" CausesValidation="false" OnClick="LinkButton_updateTT_Click" />-->
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td colspan="2">
                                                                            <asp:DataGrid ID="dgr_tintuc1" runat="server" Width="100%" AutoGenerateColumns="False"
                                                                                OnItemDataBound="dgr_tintuc1_ItemDataBound" OnEditCommand="dgr_tintuc1_EditCommand"
                                                                                DataKeyField="News_ID" CssClass="Grid">
                                                                                <ItemStyle CssClass="GridItem"></ItemStyle>
                                                                                <AlternatingItemStyle CssClass="GridAltItem" />
                                                                                <HeaderStyle CssClass="GridHeader"></HeaderStyle>
                                                                                <Columns>
                                                                                    <asp:TemplateColumn>
                                                                                        <HeaderStyle HorizontalAlign="Center" Width="3%"></HeaderStyle>
                                                                                        <ItemStyle HorizontalAlign="Center" Width="3%"></ItemStyle>
                                                                                        <HeaderTemplate>
                                                                                            <asp:CheckBox ID="chkAll" onclick="javascript:checkAll_DM_CMAll(this);" runat="server"
                                                                                                ToolTip="<%$Resources:cms.language, lblChontatca%>"></asp:CheckBox>
                                                                                        </HeaderTemplate>
                                                                                        <ItemTemplate>
                                                                                            <asp:CheckBox runat="server" Text='' ID='optSelect' AutoPostBack="False"></asp:CheckBox>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                    <asp:BoundColumn DataField="News_ID" HeaderText="News_ID" Visible="False"></asp:BoundColumn>
                                                                                    <asp:TemplateColumn HeaderText="<%$Resources:cms.language, lblAnhdaidien%>">
                                                                                        <HeaderStyle HorizontalAlign="Center" Width="10%"></HeaderStyle>
                                                                                        <ItemStyle HorizontalAlign="Left" Width="10%"></ItemStyle>
                                                                                        <ItemTemplate>
                                                                                            <div class="gallery">
                                                                                                <div class="pictgalery" style="width: 120px;">
                                                                                                    <%#UltilFunc.ReturnPath_Images(Eval("Images_Summary"))%>
                                                                                                </div>
                                                                                            </div>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                    <asp:TemplateColumn HeaderText="<%$Resources:cms.language, lblTenbaiviet%>">
                                                                                        <HeaderStyle HorizontalAlign="Center" Width="20%"></HeaderStyle>
                                                                                        <ItemStyle HorizontalAlign="Left" Width="20%"></ItemStyle>
                                                                                        <ItemTemplate>
                                                                                            <div class="stringtieudeandsc">
                                                                                                <div class="fontTitle" style="width: 98%;">
                                                                                                    <asp:LinkButton CssClass="linkEdit" Text='<%# DataBinder.Eval(Container.DataItem, "News_Tittle" )%>'
                                                                                                        Enabled="true" runat="server" ID="linkTittle" CommandName="Edit" CommandArgument="Edit"
                                                                                                        ToolTip="<%$Resources:cms.language, lblSuabai%>"></asp:LinkButton>
                                                                                                    <%#HPCBusinessLogic.UltilFunc.IsStatusGet(Eval("News_IsImages"),Eval("News_IsVideo"))%>
                                                                                                    <%#HPCBusinessLogic.UltilFunc.IsStatusImages(DataBinder.Eval(Container.DataItem, "News_Body").ToString())%>
                                                                                                </div>
                                                                                                <div class="chuthichcss">
                                                                                                    <asp:Label ID="lbdesc" runat="server" Text='<%#Eval("News_Summary")%>'></asp:Label>
                                                                                                </div>
                                                                                                <div class="fontTitle" style="width: 100%; text-align: right;">
                                                                                                    <asp:Label ID="lbtacgia" runat="server" Text='<%#Eval("News_AuthorName")%>'></asp:Label>
                                                                                                </div>
                                                                                            </div>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                    <asp:TemplateColumn HeaderText="<%$Resources:cms.language, lblAnpham%>">
                                                                                        <HeaderStyle Width="8%" HorizontalAlign="Center"></HeaderStyle>
                                                                                        <ItemStyle Width="8%" HorizontalAlign="Center"></ItemStyle>
                                                                                        <ItemTemplate>
                                                                                            <%#HPCBusinessLogic.UltilFunc.GetTenAnpham(Eval("Lang_ID"))%>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                   <asp:TemplateColumn HeaderText="<%$Resources:cms.language, lblChuyenmuc%>">
                                                                                        <HeaderStyle Width="10%" HorizontalAlign="Center"></HeaderStyle>
                                                                                        <ItemStyle Width="10%" HorizontalAlign="Left"></ItemStyle>
                                                                                        <ItemTemplate>
                                                                                            <%#HPCBusinessLogic.UltilFunc.GetCategoryName(Eval("CAT_ID"))%>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                    <asp:TemplateColumn HeaderText="<%$Resources:cms.language, lblNguoinhap%>">
                                                                                        <HeaderStyle Width="8%" HorizontalAlign="Center"></HeaderStyle>
                                                                                        <ItemStyle Width="8%" HorizontalAlign="Center"></ItemStyle>
                                                                                        <ItemTemplate>
                                                                                            <%#HPCBusinessLogic.UltilFunc.GetUserName(Eval("News_AuthorID"))%>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                    <asp:TemplateColumn HeaderText="<%$Resources:cms.language, lblNgaynhap%>">
                                                                                        <ItemTemplate>
                                                                                            <%# DataBinder.Eval(Container, "DataItem.News_DateCreated")!=System.DBNull.Value?Convert.ToDateTime(DataBinder.Eval(Container, "DataItem.News_DateCreated")).ToString("dd/MM/yyyy HH:mm"):"" %>
                                                                                        </ItemTemplate>
                                                                                        <HeaderStyle HorizontalAlign="Center" Width="8%"></HeaderStyle>
                                                                                        <ItemStyle HorizontalAlign="Center" Width="8%"></ItemStyle>
                                                                                    </asp:TemplateColumn>
                                                                                    <asp:TemplateColumn HeaderText="<%$Resources:cms.language, lblNguoiXB%>">
                                                                                        <HeaderStyle HorizontalAlign="Center" Width="9%"></HeaderStyle>
                                                                                        <ItemStyle HorizontalAlign="Center" Width="9%"></ItemStyle>
                                                                                        <ItemTemplate>
                                                                                            <%#HPCBusinessLogic.UltilFunc.GetUserName(Eval("News_PublishedID"))%>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                    <asp:TemplateColumn HeaderText="<%$Resources:cms.language, lblNgayXB%>">
                                                                                        <HeaderStyle HorizontalAlign="Center" Width="9%"></HeaderStyle>
                                                                                        <ItemStyle HorizontalAlign="Center" Width="9%"></ItemStyle>
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="ngaytao" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.News_DatePublished")!=System.DBNull.Value?Convert.ToDateTime(DataBinder.Eval(Container, "DataItem.News_DatePublished")).ToString("dd/MM/yyyy HH:mm:ss"):"" %>'>
                                                                                            </asp:Label>
                                                                                            <asp:Label ID="ID1" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.News_ID")%>'>
                                                                                            </asp:Label>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                    <asp:TemplateColumn>
                                                                                        <HeaderStyle HorizontalAlign="Center" Width="3%"></HeaderStyle>
                                                                                        <ItemStyle HorizontalAlign="Center" Width="3%"></ItemStyle>
                                                                                        <HeaderTemplate>
                                                                                            
                                                                                        </HeaderTemplate>
                                                                                        <ItemTemplate>
                                                                                            <asp:ImageButton ID="btnCopy" BorderWidth="0px" runat="server" ImageUrl="~/Dungchung/images/Copy.gif"
                                                                                                ImageAlign="AbsMiddle" ToolTip="<%$Resources:cms.language, lblXBchuyenmuckhac%>" CommandName="Edit"
                                                                                                CommandArgument="CopyCM" BorderStyle="None"></asp:ImageButton>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                    <asp:TemplateColumn HeaderText="<%$Resources:cms.language, lblDuyetanh%>">
                                                                                        <HeaderStyle HorizontalAlign="Center" Width="4%"></HeaderStyle>
                                                                                        <ItemStyle HorizontalAlign="Center" Width="4%" CssClass="GridBorderVerSolid"></ItemStyle>
                                                                                        <ItemTemplate>
                                                                                            <a href="Javascript:open_window_Scroll('<%=Global.ApplicationPath%>/BaoDienTu/ArticleApproveImage.aspx?Menu_ID=<%=Page.Request.QueryString["Menu_ID"]%>&ID=<%# DataBinder.Eval(Container.DataItem, "News_ID") %>',50,500,100,800);" />
                                                                                            <img src='../Dungchung/images/152009212557508.gif' border="0" alt="Xem/ In"
                                                                                                onmouseover="(window.status=''); return true" style='cursor: pointer;' title="Xem">
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                    <asp:TemplateColumn HeaderText="<%$Resources:cms.language, lblXem%>">
                                                                                        <HeaderStyle HorizontalAlign="Center" Width="5%"></HeaderStyle>
                                                                                        <ItemStyle HorizontalAlign="Center" Width="5%"></ItemStyle>
                                                                                        <ItemTemplate>
                                                                                            <a target="_blank" href="<%=Global.ApplicationPath%>/View/ViewHistory.aspx?Menu_ID=<%=Page.Request.QueryString["Menu_ID"]%>&ID=<%# DataBinder.Eval(Container.DataItem, "News_ID") %>" />
                                                                                            <img src='<%= Global.ApplicationPath %>/Dungchung/images/view.gif' border="0" alt="Xem"
                                                                                                onmouseover="(window.status=''); return true" style="cursor: pointer;" title="Xem">
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                    <asp:TemplateColumn>
                                                                                        <HeaderStyle HorizontalAlign="Center" Width="3%"></HeaderStyle>
                                                                                        <ItemStyle HorizontalAlign="Center" Width="3%"></ItemStyle>
                                                                                        <HeaderTemplate>
                                                                                            <img style="cursor: pointer;" src='<%= Global.ApplicationPath%>/Dungchung/images/doc.gif'
                                                                                                border="0" title="Download nội dung bài viết" alt='Download nội dung bài viết'>
                                                                                        </HeaderTemplate>
                                                                                        <ItemTemplate>
                                                                                            <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/Dungchung/images/doc.gif"
                                                                                                ToolTip="Export word" CommandName="Edit" CommandArgument="DownLoadAlias" />
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                </Columns>
                                                                            </asp:DataGrid>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="width: 30%; text-align: left">
                                                                            <asp:Button runat="server" ID="LinkHuyXB" CssClass="iconPub" Text="<%$Resources:cms.language, lblHuydang%>" Font-Bold="true"
                                                                                CausesValidation="false" OnClick="HuyDXB_Click"></asp:Button>
                                                                        </td>
                                                                        <td style="text-align: right" class="pageNav">
                                                                            <cc1:CurrentPage runat="server" ID="CurrentPage2" CssClass="pageNavTotal">
                                                                            </cc1:CurrentPage>
                                                                            <cc1:Pager runat="server" ID="pages" OnIndexChanged="pages_IndexChanged_baidangxuly"></cc1:Pager>
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
                                                <cc2:TabPanel HeaderText="<%$Resources:cms.language, lblTinngungdang%>" ID="TabPanel1" runat="server">
                                                    <ContentTemplate>
                                                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                            <ContentTemplate>
                                                                <table border="0" cellpadding="2" cellspacing="2" width="100%">
                                                                    <tr>
                                                                        <td style="width: 100%" align="left" colspan="2">
                                                                            <asp:Button runat="server" ID="btnDangBaiTop" OnClick="btnDangBaiTop_Click" CssClass="iconPub"
                                                                                Text="<%$Resources:cms.language, lblXuatban%>" Font-Bold="true" CausesValidation="false" />
                                                                            <asp:Button runat="server" Font-Bold="true" ID="btnReturnUnPubLisherTop" CssClass="iconReply"
                                                                                Text="<%$Resources:cms.language, lblTralai%>" OnClick="btnReturnUnPubLisherTop_Click"></asp:Button>
                                                                            <asp:Button runat="server" ID="btnDeleteTop" CausesValidation="false" CssClass="iconDel"
                                                                                Text="<%$Resources:cms.language, lblXoa%>" OnClick="Delete_Click"></asp:Button>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td colspan="2">
                                                                            <asp:DataGrid ID="dgListNewsUnPublish" runat="server" Width="100%" AutoGenerateColumns="False"
                                                                                OnItemDataBound="dgListNewsUnPublish_ItemDataBound" OnEditCommand="dgListNewsUnPublish_EditCommand"
                                                                                DataKeyField="News_ID" CssClass="Grid">
                                                                                <ItemStyle CssClass="GridItem"></ItemStyle>
                                                                                <AlternatingItemStyle CssClass="GridAltItem" />
                                                                                <HeaderStyle CssClass="GridHeader"></HeaderStyle>
                                                                                <Columns>
                                                                                    <asp:TemplateColumn>
                                                                                        <HeaderStyle HorizontalAlign="Center" Width="3%"></HeaderStyle>
                                                                                        <ItemStyle HorizontalAlign="Center" Width="3%"></ItemStyle>
                                                                                        <HeaderTemplate>
                                                                                            <asp:CheckBox ID="chkAll" onclick="javascript:checkAllUnPublisher(this);" runat="server"
                                                                                                ToolTip="<%$Resources:cms.language, lblChontatca%>"></asp:CheckBox>
                                                                                        </HeaderTemplate>
                                                                                        <ItemTemplate>
                                                                                            <asp:CheckBox runat="server" Text='' ID='optSelect' AutoPostBack="False"></asp:CheckBox>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                    <asp:BoundColumn DataField="News_ID" HeaderText="News_ID" Visible="False"></asp:BoundColumn>
                                                                                    <asp:TemplateColumn HeaderText="<%$Resources:cms.language, lblAnhdaidien%>">
                                                                                        <HeaderStyle HorizontalAlign="Center" Width="8%"></HeaderStyle>
                                                                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                                        <ItemTemplate>
                                                                                            <div class="gallery">
                                                                                                <div class="pictgalery" style="width: 120px;">
                                                                                                    <%#UltilFunc.ReturnPath_Images(Eval("Images_Summary"))%>
                                                                                                </div>
                                                                                            </div>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                    <asp:TemplateColumn HeaderText="<%$Resources:cms.language, lblTenbaiviet%>">
                                                                                        <HeaderStyle HorizontalAlign="Center" Width="25%"></HeaderStyle>
                                                                                        <ItemStyle HorizontalAlign="Left" Width="25%"></ItemStyle>
                                                                                        <ItemTemplate>
                                                                                            <div class="stringtieudeandsc">
                                                                                                <div class="fontTitle" style="width: 98%;">
                                                                                                    <asp:LinkButton CssClass="linkEdit" Text='<%# DataBinder.Eval(Container.DataItem, "News_Tittle" )%>'
                                                                                                        Enabled="true" runat="server" ID="linkTittle" CommandName="Edit" CommandArgument="Edit"
                                                                                                        ToolTip="<%$Resources:cms.language, lblSuabai%>"></asp:LinkButton>
                                                                                                    <%#HPCBusinessLogic.UltilFunc.IsStatusGet(Eval("News_IsImages"),Eval("News_IsVideo"))%>
                                                                                                    <%#HPCBusinessLogic.UltilFunc.IsStatusImages(DataBinder.Eval(Container.DataItem, "News_Body").ToString())%>
                                                                                                </div>
                                                                                                <div class="chuthichcss">
                                                                                                    <asp:Label ID="lbdesc" runat="server" Text='<%#Eval("News_Summary")%>'></asp:Label>
                                                                                                </div>
                                                                                                <div class="fontTitle" style="width: 100%; text-align: right;">
                                                                                                    <asp:Label ID="lbtacgia" runat="server" Text='<%#Eval("News_AuthorName")%>'></asp:Label>
                                                                                                </div>
                                                                                            </div>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                    <asp:TemplateColumn HeaderText="<%$Resources:cms.language, lblAnpham%>">
                                                                                        <HeaderStyle Width="8%" HorizontalAlign="Center"></HeaderStyle>
                                                                                        <ItemStyle Width="8%" HorizontalAlign="Center"></ItemStyle>
                                                                                        <ItemTemplate>
                                                                                            <%#HPCBusinessLogic.UltilFunc.GetTenAnpham(Eval("Lang_ID"))%>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                    <asp:TemplateColumn HeaderText="<%$Resources:cms.language, lblChuyenmuc%>">
                                                                                        <HeaderStyle Width="10%" HorizontalAlign="Center"></HeaderStyle>
                                                                                        <ItemStyle Width="10%" HorizontalAlign="Left"></ItemStyle>
                                                                                        <ItemTemplate>
                                                                                            <%#HPCBusinessLogic.UltilFunc.GetCategoryName(Eval("CAT_ID"))%>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                    <asp:TemplateColumn HeaderText="<%$Resources:cms.language, lblNguoinhap%>">
                                                                                        <HeaderStyle Width="8%" HorizontalAlign="Center"></HeaderStyle>
                                                                                        <ItemStyle Width="8%" HorizontalAlign="Center"></ItemStyle>
                                                                                        <ItemTemplate>
                                                                                            <%#HPCBusinessLogic.UltilFunc.GetUserName(Eval("News_AuthorID"))%>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                    <asp:TemplateColumn HeaderText="<%$Resources:cms.language, lblNgaynhap%>">
                                                                                        <ItemTemplate>
                                                                                            <%# DataBinder.Eval(Container, "DataItem.News_DateCreated")!=System.DBNull.Value?Convert.ToDateTime(DataBinder.Eval(Container, "DataItem.News_DateCreated")).ToString("dd/MM/yyyy HH:mm"):"" %>
                                                                                        </ItemTemplate>
                                                                                        <HeaderStyle HorizontalAlign="Center" Width="10%"></HeaderStyle>
                                                                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                                                    </asp:TemplateColumn>
                                                                                    <asp:TemplateColumn HeaderText="<%$Resources:cms.language, lblNguoingungdang%>">
                                                                                        <HeaderStyle HorizontalAlign="Center" Width="10%"></HeaderStyle>
                                                                                        <ItemStyle HorizontalAlign="Center" Width="10%"></ItemStyle>
                                                                                        <ItemTemplate>
                                                                                            <%#HPCBusinessLogic.UltilFunc.GetUserName(Eval("News_AprovedID"))%>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                    <asp:TemplateColumn HeaderText="<%$Resources:cms.language, lblNgayngungdang%>">
                                                                                        <HeaderStyle HorizontalAlign="Center" Width="10%"></HeaderStyle>
                                                                                        <ItemStyle HorizontalAlign="Center" Width="10%"></ItemStyle>
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="ngaytao" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.News_DateSend")!=System.DBNull.Value?Convert.ToDateTime(DataBinder.Eval(Container, "DataItem.News_DateSend")).ToString("dd/MM/yyyy HH:mm:ss"):"" %>'>
                                                                                            </asp:Label>
                                                                                            <asp:Label ID="ID1" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.News_ID")%>'>
                                                                                            </asp:Label>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                    <asp:TemplateColumn HeaderText="<%$Resources:cms.language, lblXem%>">
                                                                                        <HeaderStyle HorizontalAlign="Center" Width="3%"></HeaderStyle>
                                                                                        <ItemStyle HorizontalAlign="Center" Width="3%"></ItemStyle>
                                                                                        <ItemTemplate>
                                                                                            <a target="_blank" href="<%=Global.ApplicationPath%>/View/ViewHistory.aspx?Menu_ID=<%=Request["Menu_ID"]%>&ID=<%#Eval("News_ID") %>" />
                                                                                            <img src='<%= Global.ApplicationPath %>/Dungchung/images/view.gif' border="0" alt="Xem"
                                                                                                onmouseover="(window.status=''); return true" style="cursor: pointer;" title="Xem">
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                    <asp:TemplateColumn>
                                                                                        <HeaderStyle HorizontalAlign="Center" Width="3%"></HeaderStyle>
                                                                                        <ItemStyle HorizontalAlign="Center" Width="3%"></ItemStyle>
                                                                                        <HeaderTemplate>
                                                                                            <img style="cursor: pointer;" src='<%= Global.ApplicationPath%>/Dungchung/images/doc.gif'
                                                                                                border="0" title="Download nội dung bài viết" alt='Download nội dung bài viết'>
                                                                                        </HeaderTemplate>
                                                                                        <ItemTemplate>
                                                                                            <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/Dungchung/images/doc.gif"
                                                                                                ToolTip="Export word" CommandName="Edit" CommandArgument="DownLoadAlias" />
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                </Columns>
                                                                            </asp:DataGrid>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="width: 30%; text-align: left">
                                                                            <asp:Button runat="server" ID="btnDangBaiBottom" CssClass="iconPub" Text="<%$Resources:cms.language, lblXuatban%>"
                                                                                OnClick="btnDangBaiTop_Click" Font-Bold="true" CausesValidation="false"></asp:Button>
                                                                            <asp:Button runat="server" Font-Bold="true" ID="btnReturnUnPubLisherBottom" CssClass="iconReply"
                                                                                Text="<%$Resources:cms.language, lblTralai%>" OnClick="btnReturnUnPubLisherTop_Click"></asp:Button>
                                                                            <asp:Button runat="server" ID="linkDelete" CausesValidation="false" CssClass="iconDel"
                                                                                Text="<%$Resources:cms.language, lblXoa%>" OnClick="Delete_Click"></asp:Button>
                                                                        </td>
                                                                        <td style="text-align: right" class="pageNav">
                                                                            <cc1:CurrentPage runat="server" ID="CurrentPageUnPublish" CssClass="pageNavTotal">
                                                                            </cc1:CurrentPage>
                                                                            <cc1:Pager runat="server" ID="PagerUnPublish" OnIndexChanged="PagerUnPublish_IndexChanged"></cc1:Pager>
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
                                    <tr>
                                        <td>
                                            <table border="0" width="100%">
                                                <tr>
                                                    <td style="text-align: left; width: 50%">
                                                    </td>
                                                    <td style="text-align: right" class="pageNav">
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
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
    <!--Phần popup-->
    <div style="clear: both;" />
    <a id="hnkAddMenu" runat="server" style="visibility: hidden"></a>
    <asp:Label ID="lbl_CATID" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lbl_News_CopyFrom_ID" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lbl_News_ID" runat="server" Text="" Visible="false"></asp:Label>
    <cc2:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="hnkAddMenu"
        BackgroundCssClass="ModalPopupBG" PopupControlID="Panelone" Drag="true" PopupDragHandleControlID="PopupHeader">
    </cc2:ModalPopupExtender>
    <div id="Panelone" style="display: none;">
        <div class="popup_ContainerEvent">
            <div class="popup_Titlebar" id="PopupHeader">
                <div class="TitlebarLeft">
                    <%--<asp:Literal runat="server" ID="litTittleForm">Xuất bản ra chuyên mục khác</asp:Literal>--%>
                    <asp:Label ID="Label6" runat="server"  Text="<%$Resources:cms.language, lblXBchuyenmuckhac%>"></asp:Label>
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
                                    <td style="text-align: left" colspan="2">
                                        <div class="classSearchHeader" style="width: 98%">
                                            <table border="0" cellpadding="1" cellspacing="1" style="width: 100%; text-align: left;">
                                                <tr>
                                                    <td style="width: 15%; text-align: right" class="Titlelbl">
                                                        <asp:Label ID="Label7" class="Titlelbl" runat="server" 
                                                            Text="<%$Resources:cms.language, lblAnpham%>"></asp:Label>
                                                    </td>
                                                    <td style="width: 30%; text-align: left;">
                                                        <asp:DropDownList ID="ddlLang" Width="150px" runat="server" CssClass="inputtext">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <!--<td style="text-align: right; width: 15%;" class="Titlelbl">
                                                        Chuyên mục:
                                                    </td>-->
                                                    <td style="text-align: left">
                                                        <!--<asp:TextBox ID="txtSearch_name" Width="60%" runat="server" CssClass="inputtext"></asp:TextBox>-->
                                                        &nbsp;<asp:Button runat="server" ID="linkSearch" CssClass="iconFind" Font-Bold="true"
                                                            OnClick="linkSearch_Click" Text="<%$Resources:cms.language, lblTimkiem%>"></asp:Button>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 100%; text-align: right" colspan="2">
                                        <div class="popup_Body_Fix_width_heightCopy" style="width: 98%">
                                            <table border="0" cellpadding="1" cellspacing="1" style="width: 100%; text-align: left;">
                                                <tr>
                                                    <td>
                                                        <asp:DataGrid runat="server" ID="dgCategorysCopy" AutoGenerateColumns="false" DataKeyField="Ma_ChuyenMuc"
                                                            Width="100%" CssClass="Grid" OnEditCommand="dgCategorysCopy_EditCommand">
                                                            <ItemStyle CssClass="GridItem"></ItemStyle>
                                                            <AlternatingItemStyle CssClass="GridAltItem" />
                                                            <HeaderStyle CssClass="GridHeader"></HeaderStyle>
                                                            <Columns>
                                                                <asp:TemplateColumn>
                                                                    <HeaderStyle HorizontalAlign="Center" Width="6%"></HeaderStyle>
                                                                    <ItemStyle HorizontalAlign="Center" Width="6%"></ItemStyle>
                                                                    <HeaderTemplate>
                                                                        <asp:CheckBox ID="chkAll" onclick="javascript:checkAll_DM_CM(this);" runat="server"
                                                                            ToolTip="Chọn tất cả"></asp:CheckBox>
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:CheckBox runat="server" Text='' ID='optSelect' AutoPostBack="False" Enabled="true">
                                                                        </asp:CheckBox>
                                                                    </ItemTemplate>
                                                                </asp:TemplateColumn>
                                                                <asp:TemplateColumn HeaderText="<%$Resources:cms.language, lblChuyenmuc%>">
                                                                    <HeaderStyle Width="50%" HorizontalAlign="Center"></HeaderStyle>
                                                                    <ItemStyle HorizontalAlign="Left" Width="50%"></ItemStyle>
                                                                    <ItemTemplate>
                                                                        <%#Eval("Ten_ChuyenMuc")%>
                                                                    </ItemTemplate>
                                                                </asp:TemplateColumn>
                                                                <asp:TemplateColumn HeaderText="<%$Resources:cms.language, lblXuatban%>">
                                                                    <HeaderStyle HorizontalAlign="Center" Width="15%"></HeaderStyle>
                                                                    <ItemStyle HorizontalAlign="Center" Width="15%"></ItemStyle>
                                                                    <ItemTemplate>
                                                                        <asp:ImageButton ID="btnCopy" runat="server" ImageUrl="~/Dungchung/images/Copy.gif"
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
                                        <div class="classSearchHeader" style="width: 98%">
                                            <table style="width: 100%">
                                                <tr>
                                                    <td style="text-align: left">
                                                        <asp:Button runat="server" ID="but_XB" CssClass="iconPub" Font-Bold="true" OnClick="but_XB_Click"
                                                            Text="<%$Resources:cms.language, lblXuatban%>"></asp:Button>
                                        </div>
                                    </td>
                                    <td style="text-align: right;">
                                        <asp:Button runat="server" ID="iconExit" OnClientClick="cancel();"
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
