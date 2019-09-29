<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/ITC.Master" AutoEventWireup="true"
    CodeBehind="AdsLogoEdit.aspx.cs" Inherits="ToasoanTTXVN.Quangcao.AdsLogoEdit" %>

<%@ Register TagPrefix="anthem" Namespace="Anthem" Assembly="Anthem" %>
<%@ Register TagPrefix="nbc" Namespace="HPCComponents.UI" Assembly="HPCComponents" %>
<%@ Import Namespace="HPCComponents" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <script type="text/javascript">
        function f_SubmitImageQC(check) {
            SubmitImage('../UploadFileMulti/Video_News.aspx?vType=2&vKey=' + check + '', 840, 580);
        }
        function getPath(valuePath, numArg, Size, FileExtension) {
            if (parseInt(numArg) == 1) {
                document.getElementById("ctl00_MainContent_txtThumbnail").value = valuePath;
                document.getElementById("ctl00_MainContent_ImgTemp").src = '<%=HPCComponents.Global.UploadPath%>' + valuePath;
                document.getElementById("ctl00_MainContent_ImgTemp").style.display = '';
            }
            if (parseInt(numArg) == 2) {
                document.getElementById("ctl00_MainContent_Txt_DiachiQC").value = valuePath;
            }
            if (parseInt(numArg) == 3) {
                document.getElementById("ctl00_MainContent_txtImageVideo").value = valuePath;
                document.getElementById("ctl00_MainContent_ImagesVd").src = '<%=HPCComponents.Global.UploadPath%>' + valuePath + '';
                document.getElementById("ctl00_MainContent_ImagesVd").style.display = '';
            }
        }
        function f_SubmitImage_FileDoc(check) {
            SubmitImage("../UploadVideos/Videos_Managerment.aspx?vType=3&vKey=" + check + "", 840, 580);
        }
        function checkAll_One(objRef) {
            var GridView = document.getElementById('<%=grdListCate.ClientID%>');
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
        function SetDisplay(_display) {
            document.getElementById("divCategorysAds").style.display = '' + _display + '';
        }
    </script>

    <table border="0" cellpadding="0" width="100%" cellspacing="0">
        <tr>
            <td class="datagrid_top_left">
            </td>
            <td class="datagrid_top_center">
                <table border="0" cellpadding="1" cellspacing="1" width="100%">
                    <tr>
                        <td style="width: 2%; text-align: left">
                            <img alt="CMS" src="../Dungchung/Images/Icons/cog-edit-icon.png" width="16px" height="16px" />
                        </td>
                        <td style="vertical-align: middle; text-align: left">
                            <span class="TitlePanel">Nhập thông tin quảng cáo</span>
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
                <table cellspacing="2" cellpadding="2" width="100%" border="0">
                    <tr>
                        <td colspan="4" align="right">
                            <asp:Label ID="lblMsg" runat="server" ForeColor="Red"></asp:Label><br>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;" class="Titlelbl">
                            <%= CommonLib.ReadXML("lblMotaquangcao")%>: <span class="req_Field">*</span>
                        </td>
                        <td style="text-align: left;">
                            <asp:TextBox ID="txtMota" TabIndex="7" runat="server" CssClass="inputtext" Width="420"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtMota"
                                Display="Dynamic" ErrorMessage="*" CssClass="req_Field" SetFocusOnError="True"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right" class="Titlelbl">
                            <%= CommonLib.ReadXML("lblKhachhang")%>:
                        </td>
                        <td style="text-align: left">
                            <asp:DropDownList ID="cbo_Khachhang" runat="server" Width="432" CssClass="inputtext"
                                DataTextField="Name" DataValueField="ID" TabIndex="1">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right" class="Titlelbl">
                            <%= CommonLib.ReadXML("lblAnpham") %>:
                        </td>
                        <td style="text-align: left">
                            <anthem:DropDownList ID="cbo_lanquage" AutoCallBack="true" OnSelectedIndexChanged="cbo_lanquage_SelectedIndexChanged"
                                runat="server" Width="200" CssClass="inputtext">
                            </anthem:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right" class="Titlelbl">
                            <%= CommonLib.ReadXML("lblChuyenmuc")%>:
                        </td>
                        <td style="text-align: left">
                            <anthem:DropDownList ID="cbo_Category" AutoCallBack="true" OnSelectedIndexChanged="cbo_Category_SelectedIndexChanged"
                                runat="server" Width="432px" CssClass="inputtext" TabIndex="1">
                                <asp:ListItem Selected="True" Value="All" Text="<%$Resources:cms.language, lblTatca%>"></asp:ListItem>
                                <asp:ListItem Value="CM" Text="<%$Resources:cms.language, lblChonchuyenmuc%>"></asp:ListItem>
                            </anthem:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td style="text-align: left">
                            <div class="showCategorysAds" id="divCategorysAds">
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                    <ContentTemplate>
                                        <asp:DataGrid runat="server" ID="grdListCate" AutoGenerateColumns="false" DataKeyField="Ma_ChuyenMuc"
                                            CssClass="Grid">
                                            <ItemStyle CssClass="GridItem"></ItemStyle>
                                            <AlternatingItemStyle CssClass="GridAltItem" />
                                            <HeaderStyle CssClass="GridHeader"></HeaderStyle>
                                            <Columns>
                                                <asp:BoundColumn Visible="False" DataField="Ma_ChuyenMuc">
                                                    <HeaderStyle Width="1%"></HeaderStyle>
                                                </asp:BoundColumn>
                                                <asp:TemplateColumn>
                                                    <HeaderStyle HorizontalAlign="Center" Width="3%"></HeaderStyle>
                                                    <ItemStyle HorizontalAlign="Center" Width="3%"></ItemStyle>
                                                    <HeaderTemplate>
                                                        <asp:CheckBox ID="chkAll" onclick="javascript:checkAll_One(this);" runat="server"
                                                            ToolTip="<%$Resources:cms.language, lblChontatca%>"></asp:CheckBox>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:CheckBox runat="server" Text='' ID='optSelect' AutoPostBack="False" Checked='<%# Eval("Role") %>'></asp:CheckBox>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="<%$Resources:cms.language, lblChuyenmuc%>">
                                                    <HeaderStyle HorizontalAlign="Left" Width="20%" />
                                                    <ItemStyle HorizontalAlign="Left" Width="20%"></ItemStyle>
                                                    <ItemTemplate>
                                                        <%# Eval("Ten_ChuyenMuc")%>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                            </Columns>
                                        </asp:DataGrid>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;" class="Titlelbl">
                            <%= CommonLib.ReadXML("lblLinkqc")%>:
                        </td>
                        <td style="text-align: left;">
                            <asp:TextBox ID="Txt_DiachiQC" TabIndex="7" runat="server" CssClass="inputtext" Width="420"></asp:TextBox>
                            <input accesskey="S" onclick="f_SubmitImageQC(2)" class="PhotoSel" type="button"
                                value="Browse" name="cmd_SavePath2" />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;" class="Titlelbl">
                            <%= CommonLib.ReadXML("lblAnhqcvideo")%>:
                        </td>
                        <td style="text-align: left;">
                            <asp:TextBox ID="txtImageVideo" TabIndex="7" runat="server" CssClass="inputtext"
                                Width="420"></asp:TextBox>
                            <input class="PhotoSel" accesskey="S" onclick="f_SubmitImageQC(3)" type="button"
                                value="Browse" name="cmd_SavePath3" />
                            <img id="Img1" src="<%= Global.ApplicationPath %>/Dungchung/images/find.gif" onclick="f_ViewAds('<%=txtImageVideo.ClientID%>','<%=txtImageVideo.ClientID %>');"
                                alt="Click Xem" title="Click Xem" style="border: 0px; vertical-align: middle;
                                cursor: pointer;" />
                            <img style="cursor: pointer; vertical-align: middle;" onclick="document.getElementById('<%=txtImageVideo.ClientID%>').value = ''"
                                height="20" alt="Xóa ảnh" src="<%= Global.ApplicationPath %>/Dungchung/images/delete.gif"
                                width="20" border="0" />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right" class="Titlelbl">
                            <%= CommonLib.ReadXML("lblFileqc")%>: <span class="req_Field">*</span>
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtThumbnail" runat="server" CssClass="inputtext" Width="420"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtThumbnail"
                                Display="Dynamic" ErrorMessage="*" CssClass="req_Field" SetFocusOnError="True"></asp:RequiredFieldValidator>
                            <input class="PhotoSel" accesskey="S" onclick="f_SubmitImageQC(1)" type="button"
                                value="Browse" name="cmd_SavePath2" />
                            <img id="ImgTemp" src="<%= Global.ApplicationPath %>/Dungchung/images/find.gif" onclick="f_ViewAds('<%=txtThumbnail.ClientID%>','<%=txtImageVideo.ClientID %>');"
                                alt="Click Xem" title="Click Xem" style="border: 0px; vertical-align: middle;
                                cursor: pointer;" />
                            <img style="cursor: pointer; vertical-align: middle;" onclick="document.getElementById('<%=txtThumbnail.ClientID%>').value = ''"
                                height="20" alt="Xóa ảnh" src="<%= Global.ApplicationPath %>/Dungchung/images/delete.gif"
                                width="20" border="0" />
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 30%; text-align: right;" class="Titlelbl">
                            <%= CommonLib.ReadXML("lblVitri")%>:
                        </td>
                        <td style="text-align: left;">
                            <anthem:DropDownList AutoCallBack="true" ID="cbo_Vitri_hienthi" runat="server" Width="432"
                                CssClass="inputtext" OnSelectedIndexChanged="cbo_Vitri_hienthi_SelectedIndexChanged">
                            </anthem:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;" class="Titlelbl">
                            Width:
                        </td>
                        <td style="text-align: left;">
                            <anthem:TextBox onKeyPress='return check_num(this,4,event)' ID="txtWidth" runat="server"
                                Width="90px" CssClass="inputtext"></anthem:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;" class="Titlelbl">
                            Height:
                        </td>
                        <td style="text-align: left;">
                            <anthem:TextBox onKeyPress='return check_num(this,4,event)' ID="txtHeight" CssClass="inputtext"
                                runat="server" Width="90px"></anthem:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;" class="Titlelbl">
                            <%= CommonLib.ReadXML("lblNgaybatdau")%>:
                        </td>
                        <td style="text-align: left; height: 28px; vertical-align: bottom">
                            <nbc:NetDatePicker ImageUrl="../Dungchung/Images/events.gif" ImageFolder="../Dungchung/scripts/DatePicker/Images"
                                CssClass="inputtext" Width="90px" ScriptSource="../Dungchung/Scripts/datepicker.js"
                                ID="txt_ngaybatdau" runat="server" onKeyPress="AscciiDisable()" onfocus="javascript:vDateType='3'"
                                onKeyUp="DateFormat(this,this.value,event,false,'3')" onBlur="DateFormat(this,this.value,event,true,'3')"></nbc:NetDatePicker>
                            <asp:RegularExpressionValidator ID="Regularexpressionvalidator3" runat="server" ControlToValidate="txt_ngaybatdau"
                                ValidationExpression="^\d{1,2}/\d{1,2}/\d{4}$" Display="Dynamic"><%= CommonLib.ReadXML("lblKieungay")%></asp:RegularExpressionValidator>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;" class="Titlelbl">
                            <%= CommonLib.ReadXML("lblNgayketthuc")%>:
                        </td>
                        <td style="text-align: left;">
                            <nbc:NetDatePicker ImageUrl="../Dungchung/Images/events.gif" ImageFolder="../Dungchung/Scripts/DatePicker/Images"
                                CssClass="inputtext" Width="90px" ScriptSource="../Dungchung/Scripts/datepicker.js"
                                ID="txt_ngayketthuc" runat="server" onKeyPress="AscciiDisable()" onfocus="javascript:vDateType='3'"
                                onKeyUp="DateFormat(this,this.value,event,false,'3')" onBlur="DateFormat(this,this.value,event,true,'3')"></nbc:NetDatePicker>
                            <asp:RegularExpressionValidator ID="Regularexpressionvalidator1" runat="server" ControlToValidate="txt_ngayketthuc"
                                ValidationExpression="^\d{1,2}/\d{1,2}/\d{4}$" Display="Dynamic"><%= CommonLib.ReadXML("lblKieungay")%></asp:RegularExpressionValidator><br />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;" class="Titlelbl">
                            <%= CommonLib.ReadXML("lblThutu")%>:
                        </td>
                        <td style="text-align: left;">
                            <asp:TextBox ID="txtOrder" runat="server" Width="90px" CssClass="inputtext" onKeyPress='return check_num(this,4,event)'></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;" class="Titlelbl">
                            <%= CommonLib.ReadXML("lblHienthi")%>:
                        </td>
                        <td style="text-align: left;">
                            <asp:CheckBox ID="chkDisplay" runat="server" />
                        </td>
                    </tr>
                    <!--<tr>
                        <td style="text-align: right;" class="Titlelbl">
                            Hiển thị tại:
                        </td>
                        <td style="text-align: left;">
                            <asp:DropDownList ID="cbo_Display" runat="server" Width="20%" CssClass="inputtext">
                                <asp:ListItem Value="0">Cửa sổ mới</asp:ListItem>
                                <asp:ListItem Value="1">Cửa sổ hiện tại</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>-->
                    <tr>
                        <td style="text-align: right;">
                        </td>
                        <td style="text-align: left;" class="Titlelbl">
                            -<u><%= CommonLib.ReadXML("lblGhichu") %>: </u><span class="Titlelbl_ghichu">
                                <%= CommonLib.ReadXML("lblGhichuluu")%> </i>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td align="left">
                            <asp:Button runat="server" ID="linkSave" CssClass="iconSave" Font-Bold="true" OnClick="linkSave_Click"
                                Text="<%$Resources:cms.language, lblLuu%>"></asp:Button>
                            <asp:Button runat="server" CssClass="iconExit" ValidationGroup="Login" ID="linkExit"
                                Font-Bold="true" OnClick="LinkCancel_Click" Text="<%$Resources:cms.language, lblThoat%>">
                            </asp:Button>
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
