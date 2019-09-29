<%@ Page Language="C#" MasterPageFile="~/Masters/ITC.Master" AutoEventWireup="true"
    CodeBehind="WebLinksEdit.aspx.cs" Inherits="ToasoanTTXVN.Quangcao.WebLinksEdit"%>

<%@ Register TagPrefix="cc1" Namespace="HPCComponents.UI.WebControls" Assembly="HPCComponents" %>
<%@ Import Namespace="HPCComponents" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <script type="text/javascript">
        var tmp_Window;
        function f_SubmitImage(check) {
            SubmitImage("../UploadFileMulti/Videos_Managerment.aspx?vType=1&vKey=" + check + "", 840, 540);
        }
        function getPath(valuePath, numArg) {
            //alert(numArg); alert(valuePath);
            if (parseInt(numArg) == 1) {
                document.getElementById("ctl00_MainContent_txtThumbnail").value = valuePath;
                document.getElementById("ctl00_MainContent_ImgTemp").src = '<%=HPCComponents.Global.TinPathBDT%>' + valuePath;
                document.getElementById("ctl00_MainContent_ImgTemp").style.display = '';
            }
        }
        function ClearImage() {
            document.getElementById("ctl00_MainContent_txtThumbnail").value = "";
            document.getElementById("ctl00_MainContent_ImgTemp").style.display = 'none';
        }
    </script>

    <table border="0" cellpadding="0" width="100%" cellspacing="0">
        <tr>
            <td class="datagrid_top_left">
            </td>
            <td class="datagrid_top_center" style="text-align: left">
                <span class="TitlePanel">+ <%= CommonLib.ReadXML("titCapnhatlienket")%></span>
            </td>
            <td class="datagrid_top_right">
            </td>
        </tr>
        <tr>
            <td class="datagrid_content_left">
            </td>
            <td style="text-align: center">
                <table cellspacing="0" cellpadding="2" width="100%" border="0">
                    <tr>
                        <td style="width: 20%; text-align: right" class="Titlelbl">
                            <%= CommonLib.ReadXML("lblAnpham")%>:
                        </td>
                        <td style="width: 80%; text-align: left">
                            <asp:DropDownList ID="cbo_lanquage" runat="server" Width="200" CssClass="inputtext"
                                TabIndex="1">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 20%; text-align: right" class="Titlelbl">
                            <%= CommonLib.ReadXML("lblType")%>:
                        </td>
                        <td style="width: 80%; text-align: left">
                            <asp:DropDownList ID="ddlType" runat="server" Width="200" CssClass="inputtext"
                                TabIndex="1">
                                <asp:ListItem Text="Web Links" Value="1"></asp:ListItem>
                                <asp:ListItem Text="Sponsored Links" Value="2"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 20%; text-align: right;" class="Titlelbl">
                            <%= CommonLib.ReadXML("lblTieude")%>:
                        </td>
                        <td style="width: 80%; text-align: left;">
                            <asp:TextBox ID="txtTieude" TabIndex="7" runat="server" CssClass="inputtext" Width="60%"></asp:TextBox><br />
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 20%; text-align: right;" class="Titlelbl">
                            <%= CommonLib.ReadXML("lblDiachiurl")%>: <span class="req_Field">*</span>
                        </td>
                        <td style="width: 80%; text-align: left;">
                            <asp:TextBox ID="Txt_URL" TabIndex="7" runat="server" CssClass="inputtext" Width="60%"></asp:TextBox><br />
                            <asp:RequiredFieldValidator ID="RFVTxt_URL" runat="server" ControlToValidate="Txt_URL"
                                Display="Dynamic" ErrorMessage="<%$Resources:cms.language, lblNhapUrl%>" Font-Size="Small" CssClass="req_Field"
                                SetFocusOnError="True"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 20%; text-align: right" class="Titlelbl">
                            Logo:
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtThumbnail" runat="server" CssClass="inputtext" Width="60%"></asp:TextBox>
                            <input accesskey="S" onclick="f_SubmitImage(1)" class="PhotoSel" type="button" value="Browse"
                                name="cmd_SavePath2" />
                            <img runat="server" id="ImgTemp" onclick="openNewImage(this,'Close');" alt="Click xem ảnh"
                                title="Xem ảnh" style="width: 40px; height: 28px; border: 0px; vertical-align: middle;
                                cursor: pointer;" />
                            <img style="cursor: pointer;" onclick="ClearImage();" height="20" alt="Xóa ảnh" src="<%= Global.ApplicationPath %>/Dungchung/Images/delete.gif"
                                width="20" border="0" />
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 30%; text-align: right;" valign="top" class="Titlelbl">
                            <%= CommonLib.ReadXML("lblMota")%>:
                        </td>
                        <td style="text-align: left">
                            <asp:TextBox ID="Txt_Address" CssClass="inputtext" runat="server" Width="60%" TextMode="MultiLine"
                                Rows="3"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 30%; text-align: right;" class="Titlelbl">
                            <%= CommonLib.ReadXML("lblThutu")%>:
                        </td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtOrder" runat="server" CssClass="inputtext" Width="5%" onKeyPress='return check_num(this,4,event)'></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td align="left">
                            <asp:Button runat="server" ID="linkSave" CssClass="iconSave" OnClick="linkSave_Click"
                                Text="<%$Resources:cms.language, lblLuu%>" />
                            <asp:Button runat="server" ValidationGroup="Login" ID="linkExit" CssClass="iconExit"
                                OnClick="LinkCancel_Click" Text="<%$Resources:cms.language, lblThoat%>" />
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
