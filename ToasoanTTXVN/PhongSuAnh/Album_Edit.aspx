<%@ Page Language="C#" MasterPageFile="~/Masters/ITC.Master" AutoEventWireup="true"
    CodeBehind="Album_Edit.aspx.cs" Inherits="ToasoanTTXVN.PhongSuAnh.Album_Edit" %>

<%@ Register TagPrefix="anthem" Namespace="Anthem" Assembly="Anthem" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<%@ Register TagPrefix="cc1" Namespace="HPCComponents.UI.WebControls" Assembly="HPCComponents" %>
<%@ Import Namespace="HPCComponents" %>
<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <%--<script type="text/javascript" src="/Dungchung/Scripts//jquery-1.3.2.min.js"></script>

    <link href="../Dungchung/Style/jquery.autocomplete.css" rel="stylesheet" type="text/css" />

    <script src="..//Dungchung/Scripts//JsAutoload/jquery.autocomplete.min.js" type="text/javascript"></script>--%>

    <script type="text/javascript">
        $(document).ready(function() {
            $("#<%= txt_Author_name.ClientID %>").autocomplete("AutoCompleteSearch.ashx").result(function(event, data, formatted) {
                if (data) {
                    $("#<%= hdnValue.ClientID %>").val(data[1]);
                }
                else {
                    $("#<%= hdnValue.ClientID %>").val('0');
                }
            });
        });
    </script>

    <script language="Javascript" type="text/javascript">
        function BrowserVideoFile(vKey) {
            SubmitImage('../UploadVideos/Videos_Managerment.aspx?vType=3&vKey=' + vKey + '', 840, 580);
        }
        function getPath(valuePath, numArg) {
            if (parseInt(numArg) == 1) {
                document.getElementById("ctl00_MainContent_txtThumbnail").value = valuePath;
                document.getElementById("ctl00_MainContent_ImgTemp").src = '<%=HPCComponents.Global.TinPath%>' + valuePath;
                document.getElementById("ctl00_MainContent_ImgTemp").style.display = '';
            }
            if (parseInt(numArg) == 2)
                document.getElementById("ctl00_MainContent_txtVideoPath").value = valuePath;
        }
        function ClearImage() {
            document.getElementById("ctl00_MainContent_txtThumbnail").value = "";
            document.getElementById("ctl00_MainContent_txtThumbnail").value = "";
            document.getElementById("ctl00_MainContent_ImgTemp").style.display = 'none';
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
                            <img src="../Dungchung/Images/Icons/cog-edit-icon.png" width="16px" height="16px" />
                        </td>
                        <td style="vertical-align: middle; text-align: left">
                            <span class="TitlePanel">SỬA ĐỔI PHÓNG SỰ ẢNH</span>
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
                <table id="Table7" cellspacing="2" cellpadding="2" width="100%" border="0">
                    <tr>
                        <td colspan="4" align="right">
                            <asp:Label ID="lblMsg" runat="server" ForeColor="Red"></asp:Label><br>
                            <asp:Label ID="lbl_status" Visible="false" runat="server" ForeColor="Red"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 20%; text-align: right" class="Titlelbl">
                            <%= CommonLib.ReadXML("lblAnpham") %> <span class="req_Field">(*)</span>:
                        </td>
                        <td style="text-align: left">
                            <anthem:DropDownList AutoCallBack="true" ID="cbo_lanquage" runat="server" Width="150px"
                                Enabled="true" CssClass="inputtext" DataTextField="TenNgonNgu" DataValueField="ID"
                                OnSelectedIndexChanged="cbo_lanquage_SelectedIndexChanged" TabIndex="2">
                            </anthem:DropDownList>
                            <br />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="cbo_lanquage"
                                Display="Dynamic" ErrorMessage="Bạn phải chọn Ngôn ngữ" Font-Bold="false"
                                Font-Size="Small" SetFocusOnError="True" InitialValue="0"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 20%; text-align: right" class="Titlelbl">
                            <%= CommonLib.ReadXML("lblChuyenmuc") %> :
                        </td>
                        <td style="text-align: left">
                            <anthem:DropDownList AutoCallBack="true" ID="cbo_chuyenmuc" runat="server" Width="500px"
                                CssClass="inputtext" DataTextField="Ten_ChuyenMuc" DataValueField="Ma_ChuyenMuc"
                                TabIndex="3">
                            </anthem:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 20%; text-align: right" class="Titlelbl">
                            <%= CommonLib.ReadXML("lblTengocanh") %>  <span class="req_Field">(*)</span>:
                        </td>
                        <td style="text-align: left;">
                            <asp:TextBox ID="Txt_tieudeAbum" TabIndex="7" runat="server" CssClass="inputtext"
                                Width="698"></asp:TextBox><br />
                            <asp:RequiredFieldValidator ID="Validator_tieude" runat="server" ControlToValidate="Txt_tieudeAbum"
                                Display="Dynamic" ErrorMessage="Bạn phải nhập tên Album" Font-Bold="True" SetFocusOnError="True"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <!--<tr>
                        <td style="text-align: right;" class="Titlelbl">
                            Thứ tự:
                        </td>
                        <td style="text-align: left;">
                            <asp:TextBox ID="txtOrder" runat="server" Width="10%" CssClass="inputtext" onKeyPress='return check_num(this,5,event)'></asp:TextBox>
                        </td>
                    </tr>-->
                    <%-- <tr>
                        <td style="text-align: right;" class="Titlelbl">Hiển thị trên Website:
                        </td>
                        <td style="text-align: left;">
                            <asp:CheckBox ID="chkDisplay" runat="server" Checked="True" Width="2%" CssClass="inputtext" />
                        </td>
                    </tr>--%>
                    <tr>
                        <td style="text-align: right" class="Titlelbl">
                            <%= CommonLib.ReadXML("lblNoidung") %>:
                        </td>
                        <td style="text-align: left">
                            <CKEditor:CKEditorControl ID="txt_noidungAlbum" Skin="v2" Toolbar="SapoBDT" runat="server"
                                BasePath="~/ckeditor" ContentsCss="../ckeditor/contentsbdt.css" Height="300px"
                                Width="710px" ErrorMessage="Bạn phải nhập nội dung mô tả Album" ToolbarStartupExpanded="true"
                                 InitialValue='6'>
                            </CKEditor:CKEditorControl>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txt_noidungAlbum"
                                Display="Dynamic" ErrorMessage="Bạn phải nhập nội dung mô tả Album" SetFocusOnError="True"
                                InitialValue='6'></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right" class="Titlelbl">
                            <%= CommonLib.ReadXML("lblTacgia") %>:
                        </td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txt_Author_name" Width="698" runat="server" CssClass="inputtext"
                                TabIndex="12"></asp:TextBox>
                            <input runat="server" id="hdnValue" type="hidden" />
                        </td>
                    </tr>
                    <!--<tr>
                        <td style="text-align: right" class="Titlelbl">
                            Chất lượng :
                        </td>
                        <td style="text-align: left">
                            <anthem:DropDownList AutoCallBack="true" ID="ddlnews_chatluong" runat="server" Width="200px"
                                Enabled="true" CssClass="inputtext" AutoPostBack="true">
                                <asp:ListItem Value="0" Selected="True">Copy</asp:ListItem>
                                <asp:ListItem Value="1">Thực hiện</asp:ListItem>
                            </anthem:DropDownList>
                        </td>
                    </tr>-->
                    <%--    <tr>
                        <td style="text-align: right" class="Titlelbl">
                            Thể loại:
                        </td>
                        <td style="text-align: left">
                            <anthem:DropDownList AutoCallBack="true" ID="ddlNews_IsType" runat="server" Width="200"
                                Enabled="true" CssClass="inputtext" AutoPostBack="true" TabIndex="1" 
                                OnSelectedIndexChanged="ddl_theloai_SelectedIndexChanged" 
                                AutoUpdateAfterCallBack="True">
                            </anthem:DropDownList>
                          </td>
                    </tr>
                   
                    <tr>
                        <td style="text-align: right" class="Titlelbl">
                            Loại hình:
                        </td>
                        <td style="text-align: left">
                            <anthem:DropDownList AutoCallBack="true" ID="Drop_loaihinh" runat="server" Width="200"
                                Enabled="true" CssClass="inputtext" AutoPostBack="true">
                            </anthem:DropDownList>
                        </td>
                    </tr>--%>
                    <tr>
                        <td style="text-align: right" class="Titlelbl">
                            <asp:Label ID="lbl_tien" runat="server" Text="<%$Resources:cms.language, lblNhuanbut%>"></asp:Label>
                        </td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txt_tiennhanbut" runat="server" CssClass="inputtext" Width="200px"
                                onkeyup="return CommaMonney(this.id)" onkeypress="return check_num(this,15,event);"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;" class="Titlelbl">
                           <%= CommonLib.ReadXML("lblGhichu") %>:
                        </td>
                        <td style="text-align: left;">
                            <asp:TextBox ID="txtGhichu" runat="server" Width="500" TextMode="MultiLine" Rows="3" CssClass="inputtext"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td style="text-align: left">
                            <asp:Button runat="server" ID="linkSave" CssClass="iconSave" Font-Bold="true" OnClick="linkSave_Click"
                                Text="<%$Resources:cms.language, lblLuu%>" />
                            <asp:Button runat="server" CssClass="iconExit" ValidationGroup="Login" ID="linkExit"
                                Font-Bold="true" OnClick="LinkCancel_Click" Text="<%$Resources:cms.language, lblThoat%>" />
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
