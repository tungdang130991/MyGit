<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/ITC.Master" AutoEventWireup="true"
    CodeBehind="MiTrang.aspx.cs" Inherits="ToasoanTTXVN.Quytrinh.MiTrang" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<%@ Register TagPrefix="cc1" Namespace="HPCComponents.UI.WebControls" Assembly="HPCComponents" %>
<%@ Import Namespace="HPCComponents" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <script type="text/javascript">
        $(document).ready(function() {
            $("#<%=txt_tungay.ClientID%>").datepicker({
                showOn: 'button',
                buttonText: 'Show Date',
                buttonImageOnly: true,
                buttonImage: '../Dungchung/Images/DatePicker/calendar.png'
            });
            $("#<%=txt_denngay.ClientID%>").datepicker({
                showOn: 'button',
                buttonText: 'Show Date',
                buttonImageOnly: true,
                buttonImage: '../Dungchung/Images/DatePicker/calendar.png'
            });
        });
 
    </script>

    <script type="text/javascript" language="javascript">
        function CheckItem() {
            var allitem = $('.checkitem').find("input");
            var check = false;
            $.each(allitem, function(index, key) {
                var item = $(key);
                var itemchek = item.is(":checked");
                if (itemchek)
                    check = true;
            });
            var divcontrol = $('#divbutton');
            if (check) {
                divcontrol.css("display", "");
            }
            else
                divcontrol.css("display", "none");
        }
    
    </script>

    <table border="0" cellpadding="0" width="100%" cellspacing="0">
        <tr>
            <td style="text-align: center;">
                <div class="divPanelSearch">
                    <div style="width: 99%; padding: 5px 0.5%">
                        <table border="0" cellpadding="1" cellspacing="1" style="width: 100%; text-align: right">
                            <tr>
                                <td class="Titlelbl" style="text-align: left">
                                    <%=CommonLib.ReadXML("lblAnpham") %>
                                </td>
                                <td class="Titlelbl" style="text-align: left">
                                    <%=CommonLib.ReadXML("lblSobao") %>
                                </td>
                                <td class="Titlelbl" style="text-align: left">
                                    <%=CommonLib.ReadXML("lblTrang")%>
                                </td>
                                <td class="Titlelbl" style="text-align: left">
                                    <%=CommonLib.ReadXML("lblChuyenmuc") %>
                                </td>
                                <td class="Titlelbl" style="text-align: left">
                                    <%=CommonLib.ReadXML("lblTenbaiviet")%>
                                </td>
                                <td class="Titlelbl" style="text-align: left">
                                    <%=CommonLib.ReadXML("lblTungay")%>
                                </td>
                                <td class="Titlelbl" style="text-align: left">
                                    <%=CommonLib.ReadXML("lblDenngay")%>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 10%; text-align: left">
                                    <asp:UpdatePanel ID="upnllb" runat="server">
                                        <ContentTemplate>
                                            <asp:DropDownList ID="cboAnPham" runat="server" Width="100%" CssClass="inputtext"
                                                AutoPostBack="true" OnSelectedIndexChanged="cboAnPham_SelectedIndexChanged" TabIndex="1">
                                            </asp:DropDownList>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td style="width: 10%; text-align: left">
                                    <asp:UpdatePanel ID="UpdatePanelsb" runat="server">
                                        <ContentTemplate>
                                            <asp:DropDownList ID="cboSoBao" runat="server" Width="100%" AutoPostBack="true" CssClass="inputtext"
                                                OnSelectedIndexChanged="cboSoBao_OnSelectedIndexChanged" TabIndex="5">
                                            </asp:DropDownList>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td style="width: 8%; text-align: left">
                                    <asp:UpdatePanel ID="UpdatePaneltrang" runat="server">
                                        <ContentTemplate>
                                            <asp:DropDownList ID="cboPage" runat="server" AutoPostBack="true" Width="100%" OnSelectedIndexChanged="cboPage_OnSelectedIndexChanged"
                                                CssClass="inputtext">
                                            </asp:DropDownList>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td style="width: 10%; text-align: left">
                                    <asp:UpdatePanel ID="UpdatePanelcm" runat="server">
                                        <ContentTemplate>
                                            <asp:DropDownList ID="cbo_chuyenmuc" runat="server" Width="98%" CssClass="inputtext"
                                                TabIndex="5">
                                            </asp:DropDownList>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td style="text-align: left; width: 10%">
                                    <asp:TextBox ID="txt_tieude" TabIndex="1" Width="95%" runat="server" CssClass="inputtext"
                                        placeholder="<%$Resources:cms.language, lblNhaptieudecantim%>" onkeypress="return clickButton(event,'ctl00_MainContent_cmdSeek');"></asp:TextBox>
                                </td>
                                <td style="width: 10%; text-align: left">
                                    <asp:TextBox ID="txt_tungay" runat="server" Width="120px" CssClass="inputtext" MaxLength="10"
                                        ToolTip="Từ ngày" onkeypress="AscciiDisable()" onfocus="javascript:vDateType='3'"
                                        onkeyup="DateFormat(this,this.value,event,false,'3')" onblur="DateFormat(this,this.value,event,true,'3')"></asp:TextBox>
                                    <asp:RegularExpressionValidator ID="Regularexpressionvalidator3" runat="server" ControlToValidate="txt_tungay"
                                        ValidationExpression="^\d{1,2}/\d{1,2}/\d{4}$" Display="Dynamic">Kiểu ngày:dd/MM/yyyy</asp:RegularExpressionValidator>&nbsp;&nbsp;
                                </td>
                                <td style="width: 10%; text-align: left">
                                    <asp:TextBox ID="txt_denngay" runat="server" Width="120px" CssClass="inputtext" MaxLength="10"
                                        ToolTip="Đến ngày" onkeypress="AscciiDisable()" onfocus="javascript:vDateType='3'"
                                        onkeyup="DateFormat(this,this.value,event,false,'3')" onblur="DateFormat(this,this.value,event,true,'3')"></asp:TextBox>
                                    <asp:RegularExpressionValidator ID="Regularexpressionvalidator1" runat="server" ControlToValidate="txt_denngay"
                                        ValidationExpression="^\d{1,2}/\d{1,2}/\d{4}$" Display="Dynamic">Kiểu ngày:dd/MM/yyyy</asp:RegularExpressionValidator>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: center; width: 100%" colspan="7">
                                    <asp:UpdatePanel ID="UpdatePanelTimKiem" runat="server">
                                        <ContentTemplate>
                                            <asp:Button runat="server" ID="btnTimkiem" CssClass="iconFind" OnClick="btnTimkiem_Click"
                                                Text="<%$Resources:cms.language, lblTimkiem%>"></asp:Button>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
                <div style="text-align: left; width: 100%; float: left; display: none" id="divbutton">
                    <asp:LinkButton runat="server" ID="Linkdownloadfile" CausesValidation="false" OnClick="Linkdownloadfile_Click"
                        Text="Download files" CssClass="iconBack"> </asp:LinkButton>
                </div>
                <div class="divPanelResult">
                    <asp:UpdatePanel ID="listnews" runat="server">
                        <Triggers>
                            <asp:PostBackTrigger ControlID="DataGrid_DanTrang" />
                        </Triggers>
                        <ContentTemplate>
                            <div style="text-align: left; width: 100%; vertical-align: top">
                                <div>
                                    <asp:DataGrid ID="DataGrid_DanTrang" runat="server" Width="100%" BorderStyle="None"
                                        AutoGenerateColumns="False" CssClass="Grid" BorderColor="#d4d4d4" CellPadding="0"
                                        DataKeyField="Ma_Tinbai" BackColor="White" BorderWidth="1px" AlternatingItemStyle-BackColor="#F1F1F2"
                                        OnEditCommand="DataGrid_EditCommand" OnItemDataBound="DataGrid_ItemDataBound">
                                        <ItemStyle CssClass="GridItem"></ItemStyle>
                                        <AlternatingItemStyle CssClass="GridAltItem" />
                                        <HeaderStyle CssClass="GridHeader"></HeaderStyle>
                                        <Columns>
                                            <asp:TemplateColumn>
                                                <HeaderStyle HorizontalAlign="Center" Width="4%"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center" CssClass="GridBorderVerSolid"></ItemStyle>
                                                <HeaderTemplate>
                                                    <asp:CheckBox ID="chkAll" onclick="checkAll_Current(this,'ctl00_MainContent_DataGrid_DanTrang');CheckItem();"
                                                        runat="server" ToolTip="Chọn tất cả"></asp:CheckBox>
                                                </HeaderTemplate>
                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:CheckBox runat="server" Text='' ID='optSelect' Enabled='<%# HPCBusinessLogic.Utils.IsEnable(_Role.R_Write,DataBinder.Eval(Container.DataItem, "Nguoi_Khoa").ToString(),_user.UserID)%>'
                                                        AutoPostBack="False" CssClass="checkitem" onclick="javascript:CheckItem();">
                                                    </asp:CheckBox>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:BoundColumn DataField="Ma_Tinbai" HeaderText="Mã tin bài" Visible="False"></asp:BoundColumn>
                                            <asp:TemplateColumn HeaderText="Image">
                                                <HeaderStyle HorizontalAlign="Center" Width="5%"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center" CssClass="GridBorderVerSolid"></ItemStyle>
                                                <ItemTemplate>
                                                    <div style="float: left; width: 100%; text-align: center">
                                                        <%#HPCBusinessLogic.UltilFunc.GetImageAttach(Eval("Ma_Tinbai"))%>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn>
                                                <HeaderStyle HorizontalAlign="Center" Width="5%"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center" Width="5%" CssClass="GridBorderVerSolid"></ItemStyle>
                                                <HeaderTemplate>
                                                    Download file
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="btnUpdate" Width="20px" CausesValidation="false" runat="server"
                                                        ImageUrl="../Dungchung/Images/dn3.gif" ImageAlign="AbsMiddle" ToolTip="lấy tin bài"
                                                        CommandName="Edit" CommandArgument="Download" BorderStyle="None"></asp:ImageButton>
                                                    <asp:Label ID="lblnoidung" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Noidung") %>'
                                                        Visible="false"></asp:Label>
                                                    <asp:Label ID="lbltieude" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Tieude") %>'
                                                        Visible="false">
                                                    </asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="<%$Resources:cms.language, lblTieudetin%>">
                                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Left" CssClass="GridBorderVerSolid"></ItemStyle>
                                                <ItemTemplate>
                                                    <a class="linkGridForm" href="Javascript:open_window_Scroll('<%=Global.ApplicationPath%>/Quytrinh/ViewTinbaiDantrang.aspx?Menu_ID=<%=Page.Request.QueryString["Menu_ID"]%>&ID=<%# DataBinder.Eval(Container.DataItem, "Ma_Tinbai") %>',50,500,100,800);">
                                                        <%#HPCComponents.CommonLib.GetStatusUpdateContents(Eval("Ma_Tinbai"))%></a>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="<%$Resources:cms.language, lblTrang%>">
                                                <HeaderStyle HorizontalAlign="Center" Width="5%"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center" Width="5%" CssClass="GridBorderVerSolid"></ItemStyle>
                                                <ItemTemplate>
                                                    <span class="linkGridForm">
                                                        <%#HPCBusinessLogic.UltilFunc.GetTenSoBaoFromT_Vitri_Tinbai(Eval("Ma_Tinbai"),1)%></span>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="<%$Resources:cms.language, lblAnpham%>">
                                                <HeaderStyle HorizontalAlign="Center" Width="8%"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center" Width="8%" CssClass="GridBorderVerSolid"></ItemStyle>
                                                <ItemTemplate>
                                                    <span class="linkGridForm">
                                                        <%#HPCBusinessLogic.UltilFunc.GetTenAnpham(Eval("Ma_Anpham"))%></span>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="<%$Resources:cms.language, lblSobao%>">
                                                <HeaderStyle HorizontalAlign="Center" Width="8%"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center" Width="8%" CssClass="GridBorderVerSolid"></ItemStyle>
                                                <ItemTemplate>
                                                    <span class="linkGridForm">
                                                        <%#HPCBusinessLogic.UltilFunc.GetTenSoBaoFromT_Vitri_Tinbai(Eval("Ma_Tinbai"),0)%></span>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="<%$Resources:cms.language, lblChuyenmuc%>">
                                                <HeaderStyle Width="12%" HorizontalAlign="Center"></HeaderStyle>
                                                <ItemStyle Width="12%" HorizontalAlign="Center" CssClass="GridBorderVerSolid"></ItemStyle>
                                                <ItemTemplate>
                                                    <span class="linkGridForm">
                                                        <%#HPCBusinessLogic.UltilFunc.GetTenChuyenMuc(Eval("Ma_Chuyenmuc"))%></span>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="<%$Resources:cms.language, lblNguoigui%>">
                                                <HeaderStyle HorizontalAlign="Center" Width="10%"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Left" Width="10%" CssClass="GridBorderVerSolid"></ItemStyle>
                                                <ItemTemplate>
                                                    <span class="linkGridForm">
                                                        <%#HPCBusinessLogic.UltilFunc.GetUserFullName(Eval("Ma_Nguoitao"))%></span>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="<%$Resources:cms.language, lblNgaygui%>">
                                                <ItemTemplate>
                                                    <asp:Label ID="ngaytao" CssClass="linkGridForm" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Ngaytao")!=System.DBNull.Value?Convert.ToDateTime(DataBinder.Eval(Container, "DataItem.Ngaytao")).ToString("dd/MM/yyyy HH:mm"):"" %>'>
                                                    </asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" Width="10%"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center" CssClass="GridBorderVerSolid"></ItemStyle>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="<%$Resources:cms.language, lblXem%>">
                                                <HeaderStyle HorizontalAlign="Center" Width="5%"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center" Width="5%" CssClass="GridBorderVerSolid"></ItemStyle>
                                                <ItemTemplate>
                                                    <a href="Javascript:open_window_Scroll('<%=Global.ApplicationPath%>/Quytrinh/ViewPhienBanTinBai.aspx?Menu_ID=<%=Page.Request.QueryString["Menu_ID"]%>&ID=<%# DataBinder.Eval(Container.DataItem, "Ma_Tinbai") %>',50,800,100,800);" />
                                                    <img src='<%=Global.ApplicationPath%>/Dungchung/Images/view.gif' onmouseover="(window.status=''); return true"
                                                        alt='' style="cursor: hand; border: 0" />
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn>
                                                <HeaderStyle HorizontalAlign="Center" Width="4%"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center" Width="4%" CssClass="GridBorderVerSolid"></ItemStyle>
                                                <HeaderTemplate>
                                                    Preview
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <a href="Javascript:open_window_Scroll('<%=Global.ApplicationPath%>/Quytrinh/Preview.aspx?Menu_ID=<%=Page.Request.QueryString["Menu_ID"]%>&ID=<%# DataBinder.Eval(Container.DataItem, "Ma_Tinbai") %>',50,500,100,800);">
                                                        <img src='<%=Global.ApplicationPath%>/Dungchung/Images/Icons/preview.png' border="0"
                                                            style="cursor: hand; border: 0" /></a>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                        </Columns>
                                    </asp:DataGrid>
                                </div>
                                <div style="text-align: right" class="pageNav">
                                    <cc1:CurrentPage runat="server" ID="CurrentPage" CssClass="pageNavTotal">
                                    </cc1:CurrentPage>
                                    <cc1:Pager runat="server" ID="pages" OnIndexChanged="pages_IndexChanged_baichoxuly">
                                    </cc1:Pager>
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </td>
        </tr>
    </table>
</asp:Content>
