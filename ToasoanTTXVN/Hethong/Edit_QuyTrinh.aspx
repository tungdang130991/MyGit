<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/ITC.Master" AutoEventWireup="true"
    CodeBehind="Edit_QuyTrinh.aspx.cs" Inherits="ToasoanTTXVN.Hethong.Edit_QuyTrinh" %>

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
                <table border="0" cellpadding="1" cellspacing="1">
                    <tr>
                        <td>
                        </td>
                        <td style="vertical-align: middle">
                            <span class="TitlePanel"></span>
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
            <td>
                <table width="60%" align="center" cellpadding="2" cellspacing="2">
                    <tr>
                        <td style="text-align: right;" class="Titlelbl">
                            <%=CommonLib.ReadXML("lblQuytrinh") %>(<span class="req_Field">*</span>):
                        </td>
                        <td style="text-align: left;">
                            <asp:TextBox ID="txt_TenQT" runat="server" Width="60%" CssClass="inputtext"></asp:TextBox><br />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Chưa nhập tên quy trình"
                                ControlToValidate="txt_TenQT" Display="Dynamic" SetFocusOnError="True" Font-Size="Small"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;" class="Titlelbl">
                            <%=CommonLib.ReadXML("lblMota")%>:
                        </td>
                        <td style="text-align: left;">
                            <asp:TextBox ID="txt_Mota" runat="server" CssClass="inputtext" Width="60%" TextMode="MultiLine"
                                Rows="4"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;" class="Titlelbl">
                            <%=CommonLib.ReadXML("lblAnhdaidien")%>:
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtThumbnail" runat="server" CssClass="inputtext" Style="float: left"
                                Width="60%"></asp:TextBox>
                            <input accesskey="S" onclick="f_SubmitImage()" type="button" class="myButton blue"
                                style="margin-left: 5px" value="Browse" name="cmd_SavePath2" />
                            <img style="cursor: hand;" onclick="ClearImage();" alt="Xóa ảnh" src="../Dungchung/Images/delete.gif"
                                width="15px" height="15px" border="0" />
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
