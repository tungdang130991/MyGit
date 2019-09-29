<%@ Page Language="C#" MasterPageFile="~/Masters/ITC.Master" AutoEventWireup="true"
    CodeBehind="HopdongPHList.aspx.cs" Inherits="ToasoanTTXVN.Phathanh.HopdongPHList"
    Title="" %>

<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral,PublicKeyToken=31BF3856AD364E35" %>
<%@ Register TagPrefix="anthem" Namespace="Anthem" Assembly="Anthem" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<%@ Register TagPrefix="cc1" Namespace="HPCComponents.UI.WebControls" Assembly="HPCComponents" %>
<%@ Import Namespace="HPCComponents" %>
<%@ Register TagPrefix="nbc" Namespace="HPCComponents.UI" Assembly="HPCComponents" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <script src="../Dungchung/Scripts/jquery-1.4.2.min.js" type="text/javascript"></script>

    <script src="../Dungchung/Scripts/jquery.autocomplete.js" type="text/javascript"></script>

    <link href="../Dungchung/Style/jquery.autocomplete.css" rel="stylesheet" type="text/css" />

    <script src="../Dungchung/Scripts/jquery.blockUI.js" type="text/javascript"></script>

    <script type="text/javascript">
        $(document).ready(function() { 
            $("#<%= txtTen_Khachhang.ClientID %>").autocomplete("AutoCompleteSearch.ashx").result(function (event, data, formatted) {
                    if (data) {                       
                        $("#<%= hdnValue.ClientID %>").val(data[1]);                     
                    }
                    else {
                        $("#<%= hdnValue.ClientID %>").val('0');
                    }
                });
                });
        
        function cancel() {
            $get('ctl00_MainContent_btnCancel').click();
        }  
     
        function ValidateText(i)
        {
            if(i.value.length>0)
            {
            i.value = i.value.replace(/[^\d]+/g, '');
            }
        }    
            function check_num(obj, length, e) {
             var key = window.event ? e.keyCode : e.which;
             var len = obj.value.length + 1;
             if (length <= 3) begin = 48; else begin = 45;
             if (key >= begin && key <= 57 && len <= length || (key == 8 || key == 0)) {
             }
             else return false;
         }
         function forceNumber(event){ 
            var keyCode = event.keyCode ? event.keyCode : event.charCode;
            if((keyCode < 48 || keyCode > 58) && keyCode != 8 && keyCode != 9 && keyCode != 32 && keyCode != 37 && keyCode != 39 && keyCode != 40 && keyCode != 41 && keyCode != 43 && keyCode != 45 && keyCode != 46)
            return false;
            }
          function Comma(id) {

            var number = document.getElementById(id).value;
            var no = number;
            no.indexOf(',');
            if (no != -1) {
                no = no.replace(',', ''); no = no.replace(',', ''); no = no.replace(',', '');
                no = no.replace(',', ''); no = no.replace(',', ''); no = no.replace(',', '');
                number = no;
                number = '' + number;
                if (number.length > 3) {
                    var mod = number.length % 3;
                    var output = (mod > 0 ? (number.substring(0, mod)) : '');
                    for (i = 0; i < Math.floor(number.length / 3); i++) {
                        if ((mod == 0) && (i == 0))
                            output += number.substring(mod + 3 * i, mod + 3 * i + 3);
                        else
                            output += ',' + number.substring(mod + 3 * i, mod + 3 * i + 3);
                        document.getElementById(id).value = output;
                    }

                    return (output);

                }

                else return number;

            }
            else {
                number = '' + number;
                if (number.length > 3) {
                    var mod = number.length % 3;
                    var output = (mod > 0 ? (number.substring(0, mod)) : '');
                    for (i = 0; i < Math.floor(number.length / 3); i++) {
                        if ((mod == 0) && (i == 0))
                            output += number.substring(mod + 3 * i, mod + 3 * i + 3);
                        else
                            output += ',' + number.substring(mod + 3 * i, mod + 3 * i + 3);
                        document.getElementById(id).value = output;
                    }

                    return (output);

                }

                else return number;

            }

        }
        
    </script>

    <table border="0" cellpadding="0" width="100%" cellspacing="0">
        <tr>
            <td class="datagrid_top_left">
            </td>
            <td class="datagrid_top_center">
                <span class="TitlePanel" style="float: left;">DANH SÁCH HỢP ĐỒNG</span>
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
                                    <td style="width: 80%; text-align: right" class="Titlelbl">
                                        Tên khách hàng:&nbsp;
                                        <asp:TextBox ID="txtTen_Khachhang" Width="300px" runat="server" CssClass="inputtext"
                                            onkeypress="return clickButton(event,'ctl00_MainContent_btnSearch');"></asp:TextBox>
                                        <input runat="server" id="hdnValue" type="hidden" />&nbsp; Số HĐ:&nbsp;
                                        <asp:TextBox ID="txt_SoHD" Width="200px" runat="server" CssClass="inputtext" onkeypress="return clickButton(event,'ctl00_MainContent_btnSearch');"></asp:TextBox>&nbsp;
                                        Ngày ký:&nbsp;
                                        <nbc:NetDatePicker CssClass="inputtext" ImageUrl="../Dungchung/Images/events.gif"
                                            ImageFolder="../Dungchung/scripts/DatePicker/Images" Height="16px" Width="150px"
                                            ScriptSource="../Dungchung/scripts/datepicker.js" ID="txt_NgayKy" runat="server"
                                            onKeyPress="AscciiDisable()" onfocus="javascript:vDateType='3'" onKeyUp="DateFormat(this,this.value,event,false,'3')"
                                            onBlur="DateFormat(this,this.value,event,true,'3')" MaxLength="10">
                                        </nbc:NetDatePicker>
                                    </td>
                                    <td style="text-align: right; width: 10%;">
                                        <asp:Button runat="server" ID="btnSearch" CssClass="myButton blue" Font-Bold="true"
                                            OnClick="Search_Click" Text="Tìm kiếm"></asp:Button>
                                    </td>
                                    <td style="text-align: left; width: 10%;">
                                        <asp:Button runat="server" ID="btnAdd" CssClass="myButton blue" Font-Bold="true"
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
                        <td>
                            <asp:GridView ID="GVListHopdong" runat="Server" AutoGenerateColumns="False" CssClass="Grid"
                                Width="100%" OnRowDataBound="GVListHopdong_OnRowDataBound" OnRowCommand="GVListHopdong_OnRowCommand"
                                OnRowDeleting="GVListHopdong_RowDeleting" OnRowEditing="GVListHopdong_OnRowEditing"
                                AutoGenerateEditButton="false" DataKeyNames="ID" EnableViewState="True" Visible="true">
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
                                    <asp:TemplateField HeaderText="Tên khách hàng">
                                        <HeaderStyle HorizontalAlign="Center" Width="25%" />
                                        <ItemStyle HorizontalAlign="Left" Width="25%" CssClass="GridBorderVerSolid" />
                                        <ItemTemplate>
                                            <asp:LinkButton ID="btnEdit" runat="server" CssClass="linkGridForm" Text=' <%# TenKhachHang(DataBinder.Eval(Container.DataItem, "Ma_KhachHang").ToString())%>'
                                                ToolTip="Sửa thông tin hợp đồng " CommandName="Edit" CommandArgument="Edit"></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Số hợp đồng">
                                        <HeaderStyle HorizontalAlign="Center" Width="15%" />
                                        <ItemStyle HorizontalAlign="Center" Width="15%" CssClass="GridBorderVerSolid" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblSohopdong" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "hopdongso")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Tiền HÐ">
                                        <HeaderStyle HorizontalAlign="Center" Width="10%" />
                                        <ItemStyle HorizontalAlign="Center" Width="10%" CssClass="GridBorderVerSolid" />
                                        <ItemTemplate>
                                            <%#DataBinder.Eval(Container, "DataItem.Sotien") != System.DBNull.Value ? String.Format("{0:00,0}", Convert.ToDecimal(DataBinder.Eval(Container.DataItem, "Sotien"))) : ""%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Ngày hết hạn">
                                        <HeaderStyle HorizontalAlign="Center" Width="10%" />
                                        <ItemStyle HorizontalAlign="Center" Width="10%" CssClass="GridBorderVerSolid" />
                                        <ItemTemplate>
                                            <%# DataBinder.Eval(Container, "DataItem.Ngayketthuc") != System.DBNull.Value ? Convert.ToDateTime(DataBinder.Eval(Container, "DataItem.Ngayketthuc")).ToString("dd/MM/yyyy") : ""%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Thanh toán HÐ">
                                        <HeaderStyle HorizontalAlign="Center" Width="10%" />
                                        <ItemStyle HorizontalAlign="Center" Width="10%" CssClass="GridBorderVerSolid" />
                                        <ItemTemplate>
                                            <asp:ImageButton ID="btnPay" Width="15px" runat="server" ImageUrl="~/Dungchung/Images/Icons/edit.gif"
                                                ImageAlign="AbsMiddle" ToolTip="Thanh toán hợp đồng" OnClick="Layout" BorderStyle="None">
                                            </asp:ImageButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Xem">
                                        <HeaderStyle HorizontalAlign="Center" Width="5%" />
                                        <ItemStyle HorizontalAlign="Center" Width="5%" CssClass="GridBorderVerSolid" />
                                        <ItemTemplate>
                                            <asp:ImageButton ID="btnView" Width="15px" runat="server" ImageUrl="~/Dungchung/Images/zoom.gif"
                                                ImageAlign="AbsMiddle" ToolTip="Xem thông tin hợp đồng" CommandName="Edit" CommandArgument="View"
                                                BorderStyle="None"></asp:ImageButton>
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
                                    </asp:TemplateField>
                                </Columns>
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
                                TargetControlID="hnkAddMenu" CancelControlID="btnCancel" PopupControlID="Thanhtoan_Hopdong"
                                Drag="true" PopupDragHandleControlID="PopupHeader">
                            </cc2:ModalPopupExtender>
                            <div id="Thanhtoan_Hopdong" style="display: none; width: 750px;">
                                <div class="popup_Container">
                                    <div class="popup_Titlebar" id="PopupHeader">
                                        <asp:UpdatePanel ID="UpnTit" runat="server">
                                            <ContentTemplate>
                                                <div class="TitlebarLeft">
                                                    <asp:Literal runat="server" ID="litTittleForm" Text="THANH TOÁN HỢP ĐỒNG PHÁT HÀNH"></asp:Literal>
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
                                                            <td style="text-align: left;" colspan="2">
                                                                <asp:Label ID="lbl_TenKH" runat="server"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="text-align: left;" colspan="2">
                                                                <asp:Label ID="lbl_SoHD" runat="server"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="text-align: left;" colspan="2">
                                                                <asp:Label ID="lblTongtien" runat="server"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="text-align: center">
                                                                <asp:Label ID="lblMessError" runat="server" ForeColor="Red"></asp:Label>
                                                            </td>
                                                            <td style="text-align: right">
                                                                <asp:ImageButton ID="btnAddPopUp" runat="server" ImageUrl="~/Dungchung/Images/add.jpg"
                                                                    OnClick="btnAddPopUp_Click" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="2" style="text-align: center">
                                                                <asp:GridView ID="GVThanhtoanHD" runat="Server" AutoGenerateColumns="False" BackColor="White"
                                                                    CssClass="Grid" GridLines="Vertical" Width="100%" OnRowDataBound="GVThanhtoanHD_OnRowDataBound"
                                                                    OnRowCommand="GVThanhtoanHD_OnRowCommand" OnRowDeleting="GVThanhtoanHD_RowDeleting"
                                                                    OnRowEditing="GVThanhtoanHD_RowEditing" OnRowUpdating="GVThanhtoanHD_RowUpdating"
                                                                    OnRowCancelingEdit="GVThanhtoanHD_RowCancelingEdit" ShowFooter="False" AutoGenerateEditButton="false"
                                                                    DataKeyNames="ID" EnableViewState="True">
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
                                                                        <asp:TemplateField HeaderText="Số tiền">
                                                                            <HeaderStyle HorizontalAlign="Center" Width="20%" />
                                                                            <ItemStyle HorizontalAlign="Center" Width="20%" CssClass="GridBorderVerSolid" />
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblsotien" runat="server" Text='<%#DataBinder.Eval(Container, "DataItem.SOTIEN")!=System.DBNull.Value? String.Format("{0:00,0}", Convert.ToDecimal( DataBinder.Eval(Container.DataItem, "SOTIEN"))):""%>'></asp:Label>
                                                                            </ItemTemplate>
                                                                            <EditItemTemplate>
                                                                                <asp:TextBox ID="txtsotien" runat="server" Text='<%#DataBinder.Eval(Container, "DataItem.SOTIEN")%>'
                                                                                    onkeyup="javascript:return Comma(this.id)"></asp:TextBox>
                                                                                <asp:Label ID="lblthongbao" runat="server" ForeColor="Red"></asp:Label>
                                                                            </EditItemTemplate>
                                                                            <FooterTemplate>
                                                                                <asp:TextBox ID="txtsotien" runat="Server" OnKeyPress=" return ValidateText(this);"
                                                                                    onkeyup="javascript:return Comma(this.id)" Width="100%"></asp:TextBox><br />
                                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="..."
                                                                                    ControlToValidate="txtsotien">(Nhập số tiền)
                                                                                </asp:RequiredFieldValidator>
                                                                                <asp:Label ID="lblthongbaoSotien" runat="server" ForeColor="Red"></asp:Label>
                                                                            </FooterTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField>
                                                                            <HeaderStyle HorizontalAlign="Center" />
                                                                            <HeaderTemplate>
                                                                                <asp:Literal ID="LiteralNguoithu" runat="server" Text="Người thu"></asp:Literal>
                                                                            </HeaderTemplate>
                                                                            <ItemStyle Width="20%" HorizontalAlign="Left" CssClass="GridBorderVerSolid"></ItemStyle>
                                                                            <ItemTemplate>
                                                                                <%# HPCBusinessLogic.UltilFunc.GetUserFullName(DataBinder.Eval(Container.DataItem, "NGUOITHU"))%>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField>
                                                                            <HeaderStyle HorizontalAlign="Center" />
                                                                            <HeaderTemplate>
                                                                                <asp:Literal ID="Literal12" runat="server" Text="Ngày thu"></asp:Literal>
                                                                            </HeaderTemplate>
                                                                            <ItemStyle Width="25%" HorizontalAlign="Center" CssClass="GridBorderVerSolid"></ItemStyle>
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblNgaythu" runat="server" Text='<%#DataBinder.Eval(Container, "DataItem.NGAYTHU")!=System.DBNull.Value?Convert.ToDateTime( DataBinder.Eval(Container.DataItem, "NGAYTHU")).ToString("dd/MM/yyyy"):""%>'></asp:Label>
                                                                            </ItemTemplate>
                                                                            <EditItemTemplate>
                                                                                <asp:TextBox ID="txtngaythu" runat="server" Text='<%#DataBinder.Eval(Container, "DataItem.NGAYTHU")!=System.DBNull.Value?Convert.ToDateTime( DataBinder.Eval(Container.DataItem, "NGAYTHU")).ToString("dd/MM/yyyy"):""%>'></asp:TextBox>
                                                                            </EditItemTemplate>
                                                                            <FooterTemplate>
                                                                                <asp:TextBox ID="txtngaythu" runat="Server" Width="100%" onkeypress="AscciiDisable()"
                                                                                    onfocus="javascript:vDateType='3'" onkeyup="DateFormat(this,this.value,event,false,'3')"
                                                                                    onblur="DateFormat(this,this.value,event,true,'3')"></asp:TextBox>
                                                                                <br />
                                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidatorNgaythu" runat="server" ErrorMessage="..."
                                                                                    ControlToValidate="txtngaythu">(Nhập ngày thu tiền)
                                                                                </asp:RequiredFieldValidator>
                                                                            </FooterTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField>
                                                                            <HeaderStyle HorizontalAlign="Center" />
                                                                            <HeaderTemplate>
                                                                                <asp:Literal ID="LiteralTT" runat="server" Text="Người thanh toán"></asp:Literal>
                                                                            </HeaderTemplate>
                                                                            <ItemStyle Width="25%" HorizontalAlign="Left" CssClass="GridBorderVerSolid"></ItemStyle>
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblNguoiThanhtoan" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "TENNGUOINOP")%>'></asp:Label>
                                                                            </ItemTemplate>
                                                                            <EditItemTemplate>
                                                                                <asp:TextBox ID="txtnguoithanhtoan" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "TENNGUOINOP")%>'></asp:TextBox>
                                                                            </EditItemTemplate>
                                                                            <FooterTemplate>
                                                                                <asp:TextBox ID="txtnguoithanhtoan" runat="Server" Width="100%"></asp:TextBox><br />
                                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidatornguoithanhtoan" runat="server"
                                                                                    ErrorMessage="..." ControlToValidate="txtngaythu">(Nhập người thanh toán)
                                                                                </asp:RequiredFieldValidator>
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
                                                            <td style="text-align: right" colspan="2">
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
