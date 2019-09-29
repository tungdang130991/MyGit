<%@ Page Language="C#" MasterPageFile="~/Masters/ITC.Master" AutoEventWireup="true"
    CodeBehind="EditAnPham.aspx.cs" Inherits="ToasoanTTXVN.Danhmuc.EditAnPham" Title="" %>

<%@ Register TagPrefix="cc1" Namespace="HPCComponents.UI.WebControls" Assembly="HPCComponents" %>
<%@ Import Namespace="HPCComponents" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<%@ Register TagPrefix="nbc" Namespace="HPCComponents.UI" Assembly="HPCComponents" %>
<%@ Register TagPrefix="anthem" Namespace="Anthem" Assembly="Anthem" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <script type="text/javascript" language="javascript">
        function check_num(obj, length, e) {
            var key = window.event ? e.keyCode : e.which;
            var len = obj.value.length + 1;
            if (length <= 3) begin = 48; else begin = 45;
            if (key >= begin && key <= 57 && len <= length || (key == 8 || key == 0)) {
            }
            else return false;
        }

        function f_SubmitImage() {
            SubmitImage('../Danhmuc/UploadFile.aspx', 840, 600);
        }
        function getPath(valuePath) {

            document.getElementById("ctl00_MainContent_txtThumbnail").value = valuePath;
            setSRCimg(valuePath);

        }
        function ClearImage() {
            document.getElementById('ctl00_MainContent_txtThumbnail').value = "";
        }
        function setSRCimg(pathsrc) {
            var str = "<img style='float: left;max-width:40px' id='DisplayImages' src='<%= Global.ApplicationPath %>/" + pathsrc + "' border='0' />";
            document.getElementById('displayimg').innerHTML = str;
        }
       
    </script>

    <table border="0" cellpadding="0" width="100%" cellspacing="0">
        <tr>
            <td class="datagrid_top_left">
            </td>
            <td class="datagrid_top_center">
            </td>
            <td class="datagrid_top_right">
            </td>
            <tr>
                <td class="datagrid_content_left">
                </td>
                <td>
                    <table width="60%" align="center" cellpadding="2" cellspacing="2">
                        <tr>
                            <td style="text-align: right; width: 20%" class="Titlelbl">
                                <%=CommonLib.ReadXML("lblAnpham") %>(<span class="req_Field">*</span>):
                            </td>
                            <td style="text-align: left;">
                                <asp:TextBox ID="txt_TenAnPham" runat="server" Width="60%" CssClass="inputtext"></asp:TextBox><br />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*"
                                    ControlToValidate="txt_TenAnPham" Display="Dynamic" SetFocusOnError="True" Font-Size="Small"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right;" class="Titlelbl">
                                <%=CommonLib.ReadXML("lblMota") %>:
                            </td>
                            <td style="text-align: left;">
                                <asp:TextBox ID="txt_Mota" runat="server" CssClass="inputtext" Width="60%" TextMode="MultiLine"
                                    Rows="4"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right;" class="Titlelbl">
                                <%=CommonLib.ReadXML("lblAnhdaidien") %>:
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtThumbnail" runat="server" CssClass="inputtext" Style="float: left"
                                    Width="60%"></asp:TextBox>
                                <input accesskey="S" onclick="f_SubmitImage()" type="button" class="PhotoSel" style="margin-left: 5px"
                                    value="Browse" name="cmd_SavePath2" />
                                <img style="cursor: hand;" onclick="ClearImage();" alt="Xóa ảnh" src="../Dungchung/Images/delete.gif"
                                    width="15px" height="18px" border="0" />
                            </td>
                        </tr>
                        <tr>
                            <td class="Titlelbl" align="right">
                                <%=CommonLib.ReadXML("lblSotrang") %>:
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txt_Sotrang" runat="server" CssClass="inputtext" Width="10%" onKeyPress='return check_num(this,5,event)'></asp:TextBox>
                                <br />
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txt_Sotrang"
                                    Display="Dynamic" ErrorMessage="Số trang phải là chữ số" Font-Size="Small" SetFocusOnError="True"
                                    ValidationExpression="^\d+$"></asp:RegularExpressionValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                            <td style="text-align: left" class="Titlelbl_ghichu">
                                - <u>
                                    <%=CommonLib.ReadXML("lblGhichu") %>:</u>
                                <%=CommonLib.ReadXML("lblGhichuluu") %>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:ValidationSummary ID="vs_Themmoi" runat="server" ValidationGroup="ValidCapNhap"
                                    ShowSummary="false" />
                                <asp:Label ID="txtMessage" CssClass="Error" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" style="height: 10px">
                            </td>
                        </tr>
                        <tr align="center">
                            <td>
                            </td>
                            <td align="left">
                                <asp:LinkButton runat="server" ID="linkSave" CssClass="iconSave" OnClick="Save_Click"
                                    Text="<%$ Resources:cms.language, lblLuu %>">
                                </asp:LinkButton>
                                <asp:LinkButton runat="server" ID="LinkCancel" CssClass="iconExit" OnClick="Cancel_Click"
                                    Text="<%$ Resources:cms.language, lblThoat %>" CausesValidation="false">

                                </asp:LinkButton>
                            </td>
                        </tr>
                    </table>
                </td>
                <td class="datagrid_content_right">
                </td>
            </tr>
        </tr>
    </table>
</asp:Content>
