<%@ Page Language="C#" MasterPageFile="~/Masters/ITC.Master" AutoEventWireup="true"
    CodeBehind="EditChuyenMuc.aspx.cs" Inherits="ToasoanTTXVN.Danhmuc.EditChuyenMuc" %>

<%@ Import Namespace="HPCComponents" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <script type="text/javascript">
        var tmp_Window;
        function f_SubmitImage(check) {
            SubmitImage("../UploadFileMulti/Videos_Managerment.aspx?vType=1&vKey=" + check + "", 840, 540);
        }
        function getPath(valuePath, numArg) {
            //alert(numArg); alert(valuePath);
            if (parseInt(numArg) == 1) {
                document.getElementById("ctl00_MainContent_txtThum").value = valuePath;
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
            <td class="datagrid_top_center">
                <span class="TitlePanel" style="float: left;"></span>
            </td>
            <td class="datagrid_top_right">
            </td>
        </tr>
        <tr>
            <td class="datagrid_content_left">
            </td>
            <td style="text-align: center">
                <table width="60%" align="center" cellpadding="2" cellspacing="2" border="0">
                    <tr>
                        <td style="text-align: right;" class="Titlelbl">
                            <%=CommonLib.ReadXML("lblChuyenmuc") %>:(<span class="req_Field">*</span>)&nbsp;
                        </td>
                        <td style="text-align: left;" colspan="3">
                            <asp:TextBox ID="txtTenCM" runat="server" Width="438px" CssClass="inputtext"></asp:TextBox><br />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Chưa nhập tên chuyên mục"
                                ControlToValidate="txtTenCM" Display="Dynamic" SetFocusOnError="True" Font-Size="Small"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;" class="Titlelbl">
                            <%=CommonLib.ReadXML("lblAnhdaidien") %>:
                        </td>
                        <td style="text-align: left;" colspan="3">
                            <asp:TextBox ID="txtThum" runat="server" Width="438px" CssClass="inputtext"></asp:TextBox>
                            <input accesskey="S" onclick="f_SubmitImage(1)" class="PhotoSel" type="button" style="margin-left:5px" value="Browse"
                                name="cmd_SavePath2" />
                            <img runat="server" id="ImgTemp" onclick="openNewImage(this,'Close');" alt="View"
                                title="" style="width: 40px; height: 25px; border: 0px; vertical-align: middle;
                                cursor: pointer;" />
                            <img style="cursor: pointer;" onclick="ClearImage();" height="18" alt="Delete" src="<%= Global.ApplicationPath %>/Dungchung/Images/delete.gif"
                                width="20" border="0" />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;" class="Titlelbl">
                            <%=CommonLib.ReadXML("lblChuyenmuccha") %>:
                        </td>
                        <td style="text-align: left;" colspan="3">
                            <asp:DropDownList ID="ddl_ChuyenMucCha" CssClass="inputtext" runat="server" Width="450px"
                                DataTextField="Ten_ChuyenMuc" DataValueField="Ma_ChuyenMuc">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;" class="Titlelbl">
                            <%=CommonLib.ReadXML("lblAnpham") %>:
                        </td>
                        <td style="text-align: left;" colspan="3">
                            <asp:DropDownList ID="cbo_Anpham" CssClass="inputtext" runat="server" Width="450px">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right; width: 15%" class="Titlelbl">
                            <%=CommonLib.ReadXML("lblSothutu") %>:
                        </td>
                        <td style="text-align: left;" colspan="3">
                            <asp:TextBox ID="txt_stt" Width="80px" runat="server" onKeyPress='return check_num(this,5,event)'
                                CssClass="inputtext" />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right; width: 10%" class="Titlelbl">                            
                        </td>
                        <td style="text-align: left;">
                            <asp:CheckBox ID="chk_Hoatdong"  Width="15%" CssClass="inputtext" runat="server" Checked="False" Text="<%$Resources:cms.language,lblDangsudung %>" />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right; width: 10%" class="Titlelbl">                           
                        </td>
                        <td style="text-align: left;">
                            <asp:CheckBox ID="chkHienThi"  Width="25%" CssClass="inputtext" runat="server" Checked="False" Text="<%$Resources:cms.language,lblHienthimenu %>" />                            
                        </td>
                    </tr>         
                    <tr>
                        <td style="text-align: right; width: 10%" class="Titlelbl">
                        </td>
                        <td style="text-align: left;">
                            <asp:CheckBox ID="CheckBoxBaoDT"  Width="15%" CssClass="inputtext" runat="server" Checked="False" Text="<%$Resources:cms.language,lblBaodientu %>" />          
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right; width: 10%" class="Titlelbl">
                        </td>
                        <td style="text-align: left;">
                            <asp:CheckBox ID="CheckBoxBaoIn"  Width="15%" CssClass="inputtext" runat="server" Checked="False" Text="<%$Resources:cms.language,lblBaoin %>" />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right; width: 10%" class="Titlelbl">
                        </td>
                        <td style="text-align: left;">
                            <asp:CheckBox ID="CheckBoxCD"  Width="15%" CssClass="inputtext" runat="server" Checked="False" Text="<%$Resources:cms.language,lblChuyende %>" />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right; width: 10%" class="Titlelbl">
                        </td>
                        <td style="text-align: left;">
                            <asp:CheckBox ID="CheckBoxRss" Width="15%" CssClass="inputtext" runat="server" Checked="False" Text="RSS" />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right; width: 10%" class="Titlelbl">
                        </td>
                        <td style="text-align: left;">
                            <asp:CheckBox ID="CheckBoxLeft" Width="15%" runat="server" CssClass="inputtext" Checked="False" Text="<%$Resources:cms.language,lblTrai %>" />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right; width: 10%" class="Titlelbl">
                        </td>
                        <td style="text-align: left;">
                            <asp:CheckBox ID="CheckBoxRight" Width="15%" runat="server" CssClass="inputtext" Checked="False" Text="<%$Resources:cms.language,lblPhai %>" />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right; width: 10%" class="Titlelbl">
                        </td>
                        <td style="text-align: left;">
                            <asp:CheckBox ID="CheckBoxTop" Width="15%" runat="server" CssClass="inputtext" Checked="False" Text="<%$Resources:cms.language,lblTren %>" />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right; width: 10%" class="Titlelbl">
                        </td>
                        <td style="text-align: left;">
                            <asp:CheckBox ID="CheckBoxButtom" Width="15%" runat="server" CssClass="inputtext" Checked="False" Text="<%$Resources:cms.language,lblDuoi %>" />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right; width: 10%" class="Titlelbl">
                        </td>
                        <td style="text-align: left;">
                            <asp:CheckBox ID="CheckBoxCenter" Width="15%" runat="server" CssClass="inputtext" Checked="False" Text="<%$Resources:cms.language,lblGiua %>" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td colspan="3" style="text-align: left" class="Titlelbl_ghichu">
                            - <u>
                                <%=CommonLib.ReadXML("lblGhichu") %>:</u> &nbsp;<%=CommonLib.ReadXML("lblGhichuluu") %>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <asp:ValidationSummary ID="vs_Themmoi" runat="server" ValidationGroup="ValidCapNhap"
                                ShowSummary="false" />
                            <asp:Label ID="txtMessage" CssClass="Error" runat="server" />
                        </td>
                    </tr>
                    <tr align="center">
                        <td>
                        </td>
                        <td align="left" colspan="3">
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
