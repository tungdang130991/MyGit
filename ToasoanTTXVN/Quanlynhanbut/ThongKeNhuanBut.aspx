<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/ITC.Master" AutoEventWireup="true"
    CodeBehind="ThongKeNhuanBut.aspx.cs" Inherits="ToasoanTTXVN.Quanlynhanbut.ThongKeNhuanBut" %>

<%@ Register TagPrefix="cc1" Namespace="HPCComponents.UI.WebControls" Assembly="HPCComponents" %>
<%@ Import Namespace="HPCComponents" %>
<%@ Register TagPrefix="nbc" Namespace="HPCComponents.UI" Assembly="HPCComponents" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<%@ Register TagPrefix="anthem" Namespace="Anthem" Assembly="Anthem" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <script language="javascript" type="text/ecmascript">
        $(document).ready(function() {
            $("#<%= txt_Tacgia.ClientID %>").autocomplete("../PhongSuAnh/AutoCompleteSearch.ashx").result(function(event, data, formatted) {
                if (data) {
                    $("#<%= hdnValue.ClientID %>").val(data[1]);
                }
                else {
                    $("#<%= hdnValue.ClientID %>").val('0');
                }
            });
        });
        function checktype(a) {
            if (a == 1) {
                var checktin = document.getElementById('ctl00_MainContent_check_tinbai');
                var checkvideo = document.getElementById('ctl00_MainContent_check_Video');
                var checkanh = document.getElementById('ctl00_MainContent_check_TinAnh');
                if (checktin.checked) {
                    checkvideo.checked = false;
                    checkanh.checked = false;
                }
            }
            if (a == 2) {
                var checktin = document.getElementById('ctl00_MainContent_check_tinbai');
                var checkvideo = document.getElementById('ctl00_MainContent_check_Video');
                var checkanh = document.getElementById('ctl00_MainContent_check_TinAnh');
                if (checkvideo.checked) {
                    checktin.checked = false;
                    checkanh.checked = false;
                }
            }

            if (a == 3) {
                var checktin = document.getElementById('ctl00_MainContent_check_tinbai');
                var checkvideo = document.getElementById('ctl00_MainContent_check_Video');
                var checkanh = document.getElementById('ctl00_MainContent_check_TinAnh');
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
                            <img src="../Dungchung/Images/Icons/to-do-list-cheked-all-icon.png" width="16px"
                                height="16px" />
                        </td>
                        <td style="vertical-align: middle" align="left">
                            <span class="TitlePanel">BẢNG KÊ THANH TOÁN TIN, BÀI</span>
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
            <td class="datagrid_content_center" style="text-align: center">
                <table cellpadding="0" cellspacing="0" border="0" width="100%">
                    <tr>
                        <td style="text-align: left">
                            <table border="0" cellpadding="1" cellspacing="1" style="width: 100%; text-align: left;">
                                <tr>
                                    <td style="width: 50%;">
                                        <table border="0" cellpadding="3" cellspacing="1" style="width: 100%; text-align: left;">
                                            <tr>
                                                <td style="width: 10%; text-align: right" class="Titlelbl">
                                                    Ngôn ngữ<span class="req_Field">*</span>:
                                                </td>
                                                <td style="width: 20%; text-align: left;">
                                                    <asp:DropDownList AutoCallBack="true" ID="Drop_Ngonngu" runat="server" Width="200px"
                                                        CssClass="inputtext" AutoPostBack="True" OnSelectedIndexChanged="Drop_Ngonngu_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </td>
                                                <td style="width: 10%; text-align: right" class="Titlelbl">
                                                    Chuyên mục:
                                                </td>
                                                <td style="width: 20%; text-align: left;">
                                                    <asp:DropDownList ID="DropCM" runat="server" CssClass="inputtext" Width="200px">
                                                    </asp:DropDownList>
                                                </td>
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
                                                <!--<td>
                                                </td>
                                                <td style="text-align: left;">
                                                    <asp:CheckBox ID="check_tinbai" onclick="checktype(1)" Checked="true" runat="server"
                                                        Text="Tin bài" />
                                                    &nbsp;
                                                    <asp:CheckBox ID="check_Video" runat="server" onclick="checktype(2)" Text="Video" />
                                                            &nbsp;
                                                            <asp:CheckBox ID="check_TinAnh" runat="server" onclick="checktype(3)" Text="Ảnh" />
                                                </td>-->
                                            </tr>
                                            <tr>
                                                <td style="width: 10%; text-align: right" class="Titlelbl">
                                                    Tác giả:
                                                </td>
                                                <td style="width: 20%; text-align: left;">
                                                    <asp:TextBox ID="txt_Tacgia" runat="server" Width="60%" CssClass="inputtext"></asp:TextBox>
                                                    <input runat="server" id="hdnValue" type="hidden" />
                                                </td>
                                                <td style="width: 10%; text-align: right" class="Titlelbl">
                                                    Xuất bản từ ngày<span class="req_Field">*</span>:
                                                </td>
                                                <td style="width: 20%; text-align: left">
                                                    <nbc:NetDatePicker ImageUrl="../Dungchung/Images/events.gif" ImageFolder="../Dungchung/scripts/DatePicker/Images"
                                                        Height="16px" CssClass="inputtext" Width="130px" ScriptSource="../Dungchung/Scripts/datepicker.js"
                                                        ID="txt_FromDate" runat="server" onKeyPress="AscciiDisable()" onfocus="javascript:vDateType='3'"
                                                        onKeyUp="DateFormat(this,this.value,event,false,'3')" onBlur="DateFormat(this,this.value,event,true,'3')"></nbc:NetDatePicker>
                                                    <asp:RegularExpressionValidator ID="Regularexpressionvalidator3" runat="server" ControlToValidate="txt_FromDate"
                                                        ValidationExpression="^\d{1,2}/\d{1,2}/\d{4}$" Display="Dynamic">Kiểu ngày:dd/MM/yyyy</asp:RegularExpressionValidator>
                                                </td>
                                                <td style="width: 10%; text-align: right;" class="Titlelbl">
                                                    Đến ngày<span class="req_Field">*</span>:
                                                </td>
                                                <td>
                                                    <nbc:NetDatePicker ImageUrl="../Dungchung/Images/events.gif" ImageFolder="../Dungchung/Scripts/DatePicker/Images"
                                                        Height="16px" CssClass="inputtext" Width="130px" ScriptSource="../Dungchung/Scripts/datepicker.js"
                                                        ID="txt_ToDate" runat="server" onKeyPress="AscciiDisable()" onfocus="javascript:vDateType='3'"
                                                        onKeyUp="DateFormat(this,this.value,event,false,'3')" onBlur="DateFormat(this,this.value,event,true,'3')"></nbc:NetDatePicker>
                                                    <asp:RegularExpressionValidator ID="Regularexpressionvalidator1" runat="server" ControlToValidate="txt_ToDate"
                                                        ValidationExpression="^\d{1,2}/\d{1,2}/\d{4}$" Display="Dynamic">Kiểu ngày:dd/MM/yyyy</asp:RegularExpressionValidator><br />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="height: 10px" colspan="6">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 10%; text-align: right" class="Titlelbl">
                                                </td>
                                                <td style="text-align: center;" colspan="5">
                                                    <!--<asp:RadioButtonList ID="rblIsReporter" runat="server" RepeatDirection="Horizontal">
                                                                <asp:ListItem Value="1" Selected="True" Text="Phóng viên"></asp:ListItem>
                                                                <asp:ListItem Value="0" Text="Cộng tác viên"></asp:ListItem>
                                                            </asp:RadioButtonList>-->
                                                    <asp:Button ID="cmd_Search" CssClass="iconFind" Font-Bold="true" runat="server" Text="Tìm kiếm"
                                                        OnClick="cmd_Search_Click" />
                                                    <asp:Button ID="btnExport" CssClass="iconSave" runat="server" Text="Xuất báo cáo"
                                                        Width="100px" OnClick="linkExport_OnClick"></asp:Button>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 10px">
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:DataGrid ID="dgr_tintuc" runat="server" Width="100%" AutoGenerateColumns="False"
                                BorderColor="#d4d4d4" CellPadding="0" BorderStyle="None" CssClass="Grid" BackColor="White"
                                BorderWidth="1px" AlternatingItemStyle-BackColor="#F1F1F2" DataKeyField="News_ID"
                                OnItemDataBound="dgr_tintuc_ItemDataBound">
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
                                            <asp:Label ID="lbSTT" runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:BoundColumn DataField="News_ID" HeaderText="News_ID" Visible="False"></asp:BoundColumn>
                                    <asp:TemplateColumn>
                                        <HeaderStyle HorizontalAlign="Left" Width="44%"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Left" CssClass="GridBorderVerSolid"></ItemStyle>
                                        <HeaderTemplate>
                                            Tên bài viết
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <%--<a target="_blank" style="text-decoration: none; font-size: 13px;" href="<%=Global.ApplicationPath%>/View/ViewDetails.aspx?Menu_ID=<%=Page.Request.QueryString["Menu_ID"]%>&ID=<%# DataBinder.Eval(Container.DataItem, "News_ID") %>">
                                                <%# DataBinder.Eval(Container.DataItem, "Tieude")%>
                                            </a>--%>
                                            <a style="font-weight: bold; font-size: 13px; color: #0a5ec1; margin-right: 5px;
                                                    display: block" href="<%#SetLinkPopup(Eval("News_ID")) %>">
                                                    <%# DataBinder.Eval(Container.DataItem, "Tieude")%></a> <span style="color: #cc0200;
                                                        font-size: 12px; display: block;">
                                            <asp:Label ID="lbl_title" runat="server" Text='<%# Eval("Tieude")%>' Visible="false" />
                                            <asp:LinkButton CssClass="linkEdit" Text='<%# DataBinder.Eval(Container.DataItem, "Tieude" )%>'
                                                runat="server" ID="linkTittle" Visible="false" ToolTip="Sửa bài "></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn>
                                        <HeaderStyle HorizontalAlign="Center" Width="12%"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Center" Width="12%" CssClass="GridBorderVerSolid"></ItemStyle>
                                        <HeaderTemplate>
                                            Số ảnh đính kèm
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblcount" CssClass="iconNumber" Font-Bold="true" Text='<%#Eval("Tonganh")%>'
                                                runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn>
                                        <HeaderStyle HorizontalAlign="Center" Width="12%"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Center" CssClass="GridBorderVerSolid"></ItemStyle>
                                        <HeaderTemplate>
                                            Ngày xuất bản
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <%#Eval("NgayXB") != System.DBNull.Value ? Convert.ToDateTime(Eval("NgayXB")).ToString("dd/MM/yyyy HH:mm") : ""%>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn>
                                        <HeaderStyle HorizontalAlign="Center" Width="16%"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Left" Width="16%" CssClass="GridBorderVerSolid"></ItemStyle>
                                        <HeaderTemplate>
                                            Tác giả
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lbltacgia" runat="server" Text='<%# Eval("Nguoitao")%>' />
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn>
                                        <HeaderStyle HorizontalAlign="Center" Width="12%"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Right" Width="12%" CssClass="GridBorderVerSolid"></ItemStyle>
                                        <HeaderTemplate>
                                            Tiền nhuận bút
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lbTien" CssClass="iconNumber" Font-Bold="true" Text='<%#Eval("Total")%>'
                                                runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                </Columns>
                            </asp:DataGrid>
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 10px">
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;" class="Titlelbl">
                            <asp:Label ID="lbTongTien" runat="server"></asp:Label>
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
