<%@ Page Language="C#" MasterPageFile="~/Masters/ITC.Master" AutoEventWireup="true"
    CodeBehind="Multimedia_Published_Edit.aspx.cs" Inherits="ToasoanTTXVN.Multimedia.Multimedia_Published_Edit" %>

<%@ Register Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Import Namespace="HPCComponents" %>
<%@ Register TagPrefix="anthem" Namespace="Anthem" Assembly="Anthem" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<%@ Register TagPrefix="cc1" Namespace="HPCComponents.UI.WebControls" Assembly="HPCComponents" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <%--<script src="../Dungchung/Scripts/jquery-1.4.2.min.js" type="text/javascript"></script>

    <script src="../Dungchung/Scripts/Lib.js" type="text/javascript"></script>

    <link href="../Dungchung/Style/jquery.autocomplete.css" rel="stylesheet" type="text/css" />

    <script src="../Dungchung/Scripts/JsAutoload/jquery.autocomplete.min.js" type="text/javascript"></script>--%>

    <script type="text/javascript">
        $(document).ready(function() {
            $("#<%= txt_tacgia.ClientID %>").autocomplete("../PhongSuAnh/AutoCompleteSearch.ashx").result(function(event, data, formatted) {
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
            SubmitImage('../UploadFileMulti/Video_News.aspx?vType=3&vKey=' + vKey + '', 840, 580);
        }
        
        function getPath(valuePath, numArg, numID) {
            if (parseInt(numArg) == 1) {
                document.getElementById("ctl00_MainContent_txtThumbnail").value = valuePath;
                document.getElementById("ctl00_MainContent_ImgTemp").src = '<%=HPCComponents.Global.UploadPathBDT%>' + valuePath;
                document.getElementById("ctl00_MainContent_ImgTemp").style.display = '';
            }
            if (parseInt(numArg) == 2) {
               
                document.getElementById("ctl00_MainContent_txtVideoPath").value = valuePath;
            }
            if (parseInt(numID) > 0) {
                document.getElementById("ctl00_MainContent_txtVideoID").value = numID;
            }

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
                <table border="0" cellpadding="1" cellspacing="1" style="width: 100%">
                    <tr>
                        <td style="width: 2%">
                            <img src="../Dungchung/Images/Icons/cog-edit-icon.png" width="16px" height="16px" />
                        </td>
                        <td style="vertical-align: middle; text-align: left">
                            <span class="TitlePanel"><%= CommonLib.ReadXML("titCapnhatvideo") %></span>
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
                <table width="80%" align="center" cellspacing="2" cellpadding="2">
                    <tr>
                        <td style="width: 20%; text-align: right;" class="Titlelbl">
                            <%= CommonLib.ReadXML("lblTieude") %>: <span class="req_Field">*</span>
                        </td>
                        <td style="text-align: left;">
                            <asp:TextBox ID="txtTitle" runat="server" Width="500" CssClass="inputtext"></asp:TextBox><br />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtTitle"
                                Display="Dynamic" ErrorMessage="<%$Resources:cms.language, msgNhaptieude%>" Font-Size="Small" CssClass="req_Field"
                                SetFocusOnError="True"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 20%; text-align: right" class="Titlelbl">
                            <%= CommonLib.ReadXML("lblAnpham") %>:
                        </td>
                        <td style="text-align: left">
                            <anthem:DropDownList AutoCallBack="true" ID="ddlLang" runat="server" Width="150px"
                                Enabled="true" CssClass="inputtext" DataTextField="TenNgonNgu" DataValueField="ID"
                                OnSelectedIndexChanged="ddlLang_OnSelectedIndexChanged" TabIndex="2">
                            </anthem:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 20%; text-align: right" class="Titlelbl">
                            <%= CommonLib.ReadXML("lblChuyenmuc") %>:
                        </td>
                        <td style="text-align: left">
                            <anthem:DropDownList AutoCallBack="true" ID="ddlCategorys" runat="server" Width="512px"
                                CssClass="inputtext" DataTextField="Ten_ChuyenMuc" DataValueField="Ma_ChuyenMuc"
                                TabIndex="3">
                            </anthem:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right" class="Titlelbl">
                            <%= CommonLib.ReadXML("lblAnhdaidien") %>:
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtThumbnail" runat="server" CssClass="inputtext" Width="500"></asp:TextBox>
                            <input class="PhotoSel" accesskey="S" onclick="BrowserVideoFile(1)" type="button"
                                value="Browse" name="cmd_SavePath2" />
                            <img runat="server" id="ImgTemp" onclick="openNewImage(this,'Close');" alt="Click Xem ảnh"
                                title="Xem ảnh" style="width: 40px; height: 28px; border: 0px; vertical-align: middle;
                                cursor: pointer;" />
                            <img style="cursor: pointer;" onclick="ClearImage();" height="20" alt="Xóa ảnh" src="<%= Global.ApplicationPath %>/Dungchung/Images/delete.gif"
                                width="20" border="0" />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right" class="Titlelbl">
                            <%= CommonLib.ReadXML("lblDuongdanvideo") %>: <span class="req_Field">*</span>
                        </td>
                        <td style="text-align: left">
                            <asp:TextBox CssClass="inputtext" ID="txtVideoPath" Width="500" runat="server"></asp:TextBox>
                            <input class="PhotoSel" accesskey="S" onclick="BrowserVideoFile(2)" type="button"
                                value="Browse" name="cmd_SavePath2" />
                            <asp:TextBox ID="txtVideoID" Width="10" runat="server" Style="visibility: hidden"></asp:TextBox>
                            <img id="Img1" src="<%= Global.ApplicationPath %>/Dungchung/Images/find.gif" onclick="f_ViewVideo('<%=txtVideoPath.ClientID%>','<%=txtThumbnail.ClientID %>');"
                                alt="Click Xem" title="Click Xem" style="border: 0px; vertical-align: middle;
                                cursor: pointer;" />
                            <br />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtVideoPath"
                                Display="Dynamic" ErrorMessage="<%$Resources:cms.language, msgNhapduongdan%>" CssClass="req_Field" Font-Size="Small"
                                SetFocusOnError="True"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;" vertical-align: top" class="Titlelbl">
                            <%= CommonLib.ReadXML("lblMota") %>:
                        </td>
                        <td style="text-align: left;">
                            <asp:TextBox ID="Txt_Desc" runat="server" Width="500" TextMode="MultiLine" CssClass="inputtext"
                                Rows="6"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;" class="Titlelbl">
                            <%= CommonLib.ReadXML("lblTacgia") %>:
                        </td>
                        <td style="text-align: left;">
                            <asp:TextBox ID="txt_tacgia" runat="server" Width="188px" CssClass="inputtext"></asp:TextBox>
                            <input runat="server" id="hdnValue" type="hidden" />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;" class="Titlelbl">
                            <%= CommonLib.ReadXML("lblNhuanbut") %>:
                        </td>
                        <td style="text-align: left;">
                            <asp:TextBox ID="txt_tien" runat="server" Width="500" onkeyup="javascript:return CommaMonney(this.id);"
                                onkeypress="return check_num(this,15,event)" CssClass="inputtext"></asp:TextBox>
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
                        <td colspan="2" style="text-align: left" class="Titlelbl_ghichu">
                            - <u><%= CommonLib.ReadXML("lblGhichu") %>:</u> &nbsp;<%= CommonLib.ReadXML("lblGhichuluu") %>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td align="left">
                            <asp:Button runat="server" ID="linkSave" OnClick="linkSave_Click" CssClass="iconSave"
                                Text="<%$Resources:cms.language, lblLuu%>" Font-Bold="true" />
                            <asp:Button runat="server" ID="btnReturnEdit" OnClick="linkTralai_Click" CssClass="iconReply"
                                Text="<%$Resources:cms.language, lblHuydang%>" Font-Bold="true" />
                            <asp:Button runat="server" ID="LinkCancel" OnClick="LinkCancel_Click" CssClass="iconExit"
                                CausesValidation="false" Text="<%$Resources:cms.language, lblThoat%>" Font-Bold="true" />
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
