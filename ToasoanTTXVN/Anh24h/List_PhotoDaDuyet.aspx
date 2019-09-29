<%@ Page Language="C#" MasterPageFile="~/Masters/ITC.Master" AutoEventWireup="true"
    CodeBehind="List_PhotoDaDuyet.aspx.cs" Inherits="ToasoanTTXVN.Anh24h.List_PhotoDaDuyet" %>

<%@ Register Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register TagPrefix="cc1" Namespace="HPCComponents.UI.WebControls" Assembly="HPCComponents" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<%@ Register TagPrefix="nbc" Namespace="HPCComponents.UI" Assembly="HPCComponents" %>
<%@ Import Namespace="HPCComponents" %>
<%@ Import Namespace="HPCBusinessLogic" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <script type="text/javascript" language="javascript">
        function checkAllphoto(objRef, objectid) {
            var GridView = document.getElementById('<%=grdListCate.ClientID%>');
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
                            <span class="TitlePanel"><%= CommonLib.ReadXML("titXbanhts")%></span>
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
                    <table style="width: 100%" cellpadding="1" cellspacing="1" border="0">
                        <tr>
                            <td style="width: 8%; text-align: right" class="Titlelbl">
                                <asp:Label ID="Label2" class="Titlelbl" runat="server" 
                                                Text="<%$Resources:cms.language, lblAnpham%>"></asp:Label>
                            </td>
                            <td style="width: 10%; text-align: left">
                                <asp:DropDownList ID="cboNgonNgu" runat="server" Width="150" CssClass="inputtext"
                                    TabIndex="1">
                                </asp:DropDownList>
                            </td>
                            <td style="width: 6%; text-align: right" class="Titlelbl">
                                <asp:Label ID="Label1" class="Titlelbl" runat="server" 
                                                Text="<%$Resources:cms.language, lblTieude%>"></asp:Label>
                            </td>
                            <td style="width: 20%; text-align: left">
                                <asp:TextBox ID="txtSearch_Cate" Width="250px" CssClass="inputtext" onkeypress="return clickButton(event,'ctl00_MainContent_linkSearch');"
                                    runat="server"></asp:TextBox>
                            </td>
                            <td style="width: 6%; text-align: right" class="Titlelbl">
                                <%= CommonLib.ReadXML("lblTungay") %>
                            </td>
                            <td style="width: 15%; text-align: left">
                                <nbc:NetDatePicker ImageUrl="../Dungchung/Images/events.gif" ImageFolder="../Dungchung/Scripts/DatePicker/Images"
                                    Height="16px" Width="150px" ScriptSource="../Dungchung/Scripts/datepicker.js"
                                    ID="txt_FromDate" runat="server" onKeyPress="AscciiDisable()" onfocus="javascript:vDateType='3'"
                                    onKeyUp="DateFormat(this,this.value,event,false,'3')" onBlur="DateFormat(this,this.value,event,true,'3')"></nbc:NetDatePicker>
                                <asp:RegularExpressionValidator ID="Regularexpressionvalidator3" runat="server" ControlToValidate="txt_FromDate"
                                    ValidationExpression="^\d{1,2}/\d{1,2}/\d{4}$" Display="Dynamic"><%= CommonLib.ReadXML("lblKieungay")%></asp:RegularExpressionValidator>
                            </td>
                            <td style="width: 6%; text-align: right" class="Titlelbl">
                                <%= CommonLib.ReadXML("lblDenngay") %>
                            </td>
                            <td style="width: 15%; text-align: left">
                                <nbc:NetDatePicker ImageUrl="../Dungchung/Images/events.gif" ImageFolder="../Dungchung/Scripts/DatePicker/Images"
                                    Height="16px" Width="150px" ScriptSource="../Dungchung/Scripts/datepicker.js"
                                    ID="txt_ToDate" runat="server" onKeyPress="AscciiDisable()" onfocus="javascript:vDateType='3'"
                                    onKeyUp="DateFormat(this,this.value,event,false,'3')" onBlur="DateFormat(this,this.value,event,true,'3')"></nbc:NetDatePicker>
                                <asp:RegularExpressionValidator ID="Regularexpressionvalidator1" runat="server" ControlToValidate="txt_ToDate"
                                    ValidationExpression="^\d{1,2}/\d{1,2}/\d{4}$" Display="Dynamic"><%= CommonLib.ReadXML("lblKieungay")%></asp:RegularExpressionValidator><br />
                            </td>
                            <td style="width: 10%; text-align: left">
                                <asp:Button runat="server" ID="linkSearch" OnClick="linkSearch_Click" CssClass="iconFind"
                                    Text="<%$Resources:cms.language, lblTimkiem%>" />
                            </td>
                        </tr>
                    </table>
                </div>
                <br />
                <div class="classSearchHeader">
                    <table width="100%" cellspacing="2" cellpadding="2" border="0">
                        <tr>
                            <td style="text-align: left" colspan="2">
                                <asp:Button runat="server" CausesValidation="false" ID="LinkNgungDang" Font-Bold="true"
                                    OnClick="LinkNgungDang_Click" CssClass="iconReply" Text="<%$Resources:cms.language, lblHuydang%>" />
                                <asp:Button runat="server" ID="Button2" CssClass="iconSave" Font-Bold="true" OnClick="linkSave_Click"
                                    Text="<%$Resources:cms.language, lblChamnhuanbut%>" />
                            </td>
                        </tr>
                        <tr>
                            <td align="center" colspan="2">
                                <asp:DataGrid ID="grdListCate" runat="server" Width="100%" AutoGenerateColumns="False"
                                    DataKeyField="Photo_ID" OnEditCommand="grdListCategory_EditCommand" OnItemDataBound="grdListCategory_ItemDataBound"
                                    CssClass="Grid">
                                    <ItemStyle CssClass="GridItem"></ItemStyle>
                                    <AlternatingItemStyle CssClass="GridAltItem" />
                                    <HeaderStyle CssClass="GridHeader"></HeaderStyle>
                                    <Columns>
                                        <asp:TemplateColumn>
                                            <HeaderStyle HorizontalAlign="Center" Width="3%"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Center" Width="3%"></ItemStyle>
                                            <HeaderTemplate>
                                                <asp:CheckBox ID="chkAll" onclick="javascript:checkAllphoto(this);" runat="server"
                                                    ToolTip="<%$Resources:cms.language, lblChontatca%>"></asp:CheckBox>
                                            </HeaderTemplate>
                                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                            <ItemTemplate>
                                                <asp:CheckBox runat="server" Text='' ID='optSelect' AutoPostBack="False"></asp:CheckBox>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:BoundColumn Visible="False" DataField="Photo_ID">
                                            <HeaderStyle Width="1%"></HeaderStyle>
                                        </asp:BoundColumn>
                                        <asp:TemplateColumn HeaderText="<%$Resources:cms.language, lblHinhanh%>">
                                            <HeaderStyle HorizontalAlign="center" Width="8%"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="center" Width="8%"></ItemStyle>
                                            <ItemTemplate>
                                                <div>
                                                    <img style="<%#CommonLib.CheckImgView(DataBinder.Eval(Container.DataItem,"Photo_Medium"))%>"
                                                        src="<%# CommonLib.tinpathBDT(DataBinder.Eval(Container.DataItem, "Photo_Medium")) %>"
                                                        width="120px" height="80px" align="middle" style="padding-top: 3px; border: 0;
                                                        cursor: pointer" onclick="return openNewImage(this, '')">
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="<%$Resources:cms.language, lblTieude%>">
                                            <HeaderStyle HorizontalAlign="center" Width="25%"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Left" Width="25%"></ItemStyle>
                                            <ItemTemplate>
                                                <%# DataBinder.Eval(Container.DataItem, "Photo_Name")%>
                                                <div class="clear">
                                                </div>
                                                <div class="eol-level-small">
                                                    <%= CommonLib.ReadXML("lblNgaynhap") %>:
                                                    <asp:Label ID="ngaynhap" Font-Bold="true" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Date_Create")!=System.DBNull.Value?Convert.ToDateTime(DataBinder.Eval(Container, "DataItem.Date_Create")).ToString("dd/MM/yyyy HH:mm"):"" %>'>
                                                    </asp:Label>
                                                    &nbsp;&nbsp;|&nbsp;&nbsp;<%= CommonLib.ReadXML("lblNguoinhap") %>: <b>
                                                        <%#HPCBusinessLogic.UltilFunc.GetUserName(Eval("Creator"))%></b>&nbsp;&nbsp;
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="<%$Resources:cms.language, lblAnpham%>">
                                            <HeaderStyle HorizontalAlign="Center" Width="10%"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Center" Width="10%"></ItemStyle>
                                            <ItemTemplate>
                                                <%#HPCBusinessLogic.UltilFunc.GetTenAnpham(Eval("Lang_ID"))%>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="<%$Resources:cms.language, lblTacgia%>">
                                            <HeaderStyle HorizontalAlign="Center" Width="10%"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Center" Width="10%"></ItemStyle>
                                            <ItemTemplate>
                                                <%# DataBinder.Eval(Container.DataItem, "Author_Name")%>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="<%$Resources:cms.language, lblNhuanbut%>">
                                            <HeaderStyle HorizontalAlign="Center" Width="13%"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Center" Width="13%"></ItemStyle>
                                            <ItemTemplate>
                                                <asp:TextBox runat="server" ID="txt_tienNB" Width="70%" placeholder="<%$Resources:cms.language, lblNhuanbut%>" onKeyPress="return check_num(this,15,event);"
                                                    onkeyup="javascript:return CommaMonney(this.id);" CssClass="inputtext" 
                                                    Text='<%#DataBinder.Eval(Container, "DataItem.TienNB")!=System.DBNull.Value? String.Format("{0:00,0}", Convert.ToDecimal( DataBinder.Eval(Container.DataItem, "TienNB"))):""%>'>
                                                    </asp:TextBox>
                                                <asp:ImageButton ID="btnSave" runat="server" ImageUrl="~/Dungchung/Images/button/save-icon.png"
                                                    ToolTip="Lưu giữ" CommandName="Edit" CausesValidation="false" CommandArgument="SavePhoto" />
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        <%--<asp:TemplateColumn>
                                            <HeaderStyle HorizontalAlign="Center" Width="8%"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Center" Width="8%"></ItemStyle>
                                            <HeaderTemplate>
                                                Người nhập
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <%#HPCBusinessLogic.UltilFunc.GetUserName(Eval("Creator"))%>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="Ngày nhập">
                                            <ItemTemplate>
                                                <asp:Label ID="ngaytao" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Date_Create")!=System.DBNull.Value?Convert.ToDateTime(DataBinder.Eval(Container, "DataItem.Date_Create")).ToString("dd/MM/yyyy HH:mm"):"" %>'>
                                                </asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" Width="9%"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                        </asp:TemplateColumn>--%>
                                        <asp:TemplateColumn HeaderText="<%$Resources:cms.language, lblNguoiXB%>">
                                            <HeaderStyle HorizontalAlign="Center" Width="8%"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Center" Width="8%"></ItemStyle>
                                            <ItemTemplate>
                                                <%#HPCBusinessLogic.UltilFunc.GetUserName(Eval("UserEditor"))%>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="<%$Resources:cms.language, lblNgayXB%>">
                                            <ItemTemplate>
                                                <asp:Label ID="ngayxb" runat="server" Text='<%#Eval("Date_Update")!=System.DBNull.Value?Convert.ToDateTime(Eval("Date_Update")).ToString("dd/MM/yyyy HH:mm"):"" %>'>
                                                </asp:Label>
                                                <asp:Label ID="ID1" Visible="false" runat="server" Text='<%#Eval("Photo_ID")%>'>
                                                </asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" Width="9%"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Center" Width="9%"></ItemStyle>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="<%$Resources:cms.language, lblGhichu%>">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtGhichu" CssClass="inputtext" placeholder="<%$Resources:cms.language, msgNhapghichu%>" Width="90%"
                                                    runat="server" TextMode="MultiLine" Rows="3" Text='<%# DataBinder.Eval(Container.DataItem, "Photo_Desc")%>'></asp:TextBox>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" Width="14%"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                        </asp:TemplateColumn>
                                    </Columns>
                                </asp:DataGrid>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" style="height: 5px">
                                <span style="font-weight: bold; font-size: medium; color: #EB0C27; float: left;">
                                    <asp:Literal runat="server" ID="litMessages"></asp:Literal>
                                </span>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 40%; text-align: left">
                                <asp:Button runat="server" ID="LinkNgungDang1" CssClass="iconReply" Font-Bold="true"
                                    Text="<%$Resources:cms.language, lblHuydang%>" OnClick="LinkNgungDang_Click" />
                                <asp:Button runat="server" ID="Button1" CssClass="iconSave" Font-Bold="true" OnClick="linkSave_Click"
                                    Text="<%$Resources:cms.language, lblChamnhuanbut%>" />
                            </td>
                            <td style="text-align: right" class="pageNav">
                                <cc1:CurrentPage runat="server" ID="curentPages">
                                </cc1:CurrentPage>
                                <cc1:Pager runat="server" ID="pages" OnIndexChanged="pages_IndexChanged"></cc1:Pager>
                                <%= CommonLib.ReadXML("lblChuyentrang")%>
                                <asp:TextBox runat="server" ID="txtPageIndex" Width="60px"></asp:TextBox>&nbsp;
                                <asp:Button runat="server" ID="btnNextPage" CssClass="TitlelButton" Text="<%$Resources:cms.language, lblTim%>" OnClick="btnNextPage_Click" />
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
