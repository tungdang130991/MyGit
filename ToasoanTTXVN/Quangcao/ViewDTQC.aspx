<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ViewDTQC.aspx.cs" Inherits="ToasoanTTXVN.Quangcao.ViewDTQC" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<%@ Import Namespace="HPCComponents" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>DÀN TRANG</title>
    <link type="text/css" rel="Stylesheet" href="../Dungchung/Style/style.css" />

    <script language="javascript" src="../Dungchung/Scripts/JSDantrang/prototype.js"
        type="text/javascript">
    </script>

    <script language="javascript" src="../Dungchung/Scripts/JSDantrang/effects.js" type="text/javascript">
    </script>

    <script language="javascript" src="../Dungchung/Scripts/JSDantrang/scriptaculous.js"
        type="text/javascript">
    </script>

    <script language="javascript" type="text/javascript" src="../Dungchung/Scripts/Lib.js"></script>

    <script language="JavaScript" src="../Dungchung/Scripts/JSDantrang/vietuni.js" type='text/javascript'>
    </script>

    <script language="JavaScript" src="../Dungchung/Scripts/JSDantrang/vumods.js" type='text/javascript'>
    </script>

    <script language="JavaScript" src="../Dungchung/Scripts/JSDantrang/vumaps.js" type='text/javascript'>
    </script>

    <script language="JavaScript" src="../Dungchung/Scripts/JSDantrang/vumaps2.js" type='text/javascript'>
    </script>

    <script language="javascript" type="text/javascript">

        var tmp_Window;

        function OpenImage(_value) {

            tmp_Window = window.open("../UploadFileMulti/ViewImages.aspx?url=" + _value, "", "directories=no,menubar=no, resizable=no,toolbar=no");

        }
        
    </script>

    <style type="text/css">
        .fontOver
        {
            font-family: VNI-Times !important;
            font-size: medium;
        }
        .fontOver p
        {
            font-family: VNI-Times !important;
            font-size: medium;
        }
        .fontOver span
        {
            font-family: VNI-Times !important;
            font-size: medium;
        }
        .fontOver font
        {
            font-family: VNI-Times !important;
            font-size: medium;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div style="width: 60%; float: left">
        <input type="hidden" id="txtOriginal" />
        <input id="store_content" type="hidden" runat="server" style="width: 80px; height: 25px" />
        <table cellspacing="2" cellpadding="2" width="100%" border="0" bgcolor="#ffffff">
            <tr>
                <td style="width: 15%; text-align: right; font-family: Arial; font-size: 14px;">
                     <%=CommonLib.ReadXML("lblTieude")%>
                </td>
                <td style="text-align: left; font-family: Arial; font-size: 14px; font-weight: bold">
                    <%=Tenquangcao%>
                </td>
            </tr>
            <tr>
                <td style="width: 15%; text-align: right; font-family: Arial; font-size: 14px;">
                     <%=CommonLib.ReadXML("lblAnpham")%>:
                </td>
                <td style="text-align: left; font-family: Arial; font-size: 14px; font-weight: bold">
                    <%=Loaibao%>
                </td>
            </tr>
            <tr>
                <td style="width: 15%; text-align: right; font-family: Arial; font-size: 14px;">
                     <%=CommonLib.ReadXML("lblTrang")%>:
                </td>
                <td style="text-align: left; font-family: Arial; font-size: 14px; font-weight: bold">
                    <%=Sotrang%>
                </td>
            </tr>
            <tr>
                <td style="width: 15%; text-align: right; font-family: Arial; font-size: 14px;">
                     <%=CommonLib.ReadXML("lblKichthuocquangcao")%>:
                </td>
                <td style="text-align: left; font-family: Arial; font-size: 14px; font-weight: bold">
                    <%=Kichthuoc%>
                </td>
            </tr>
            <tr>
                <td style="width: 15%; text-align: right; font-family: Arial; font-size: 14px;">
                     <%=CommonLib.ReadXML("lblNgaydangquangcao")%>:
                </td>
                <td style="text-align: left; font-family: Arial; font-size: 14px; font-weight: bold">
                    <%=Ngaydang%>
                </td>
            </tr>
            <tr>
                <td width="100%" style="text-align: left" colspan="2">
                    <div style="position: relative; height: 30px; width: 40%; z-index: 99">
                        <asp:Button ID="btncopycontent1" runat="server" CssClass="iconCopy" Style="width: 80px;
                            height: 25px" Text="Copy" OnClick="btncopycontent_Click"></asp:Button>
                    </div>
                </td>
            </tr>
            <tr>
                <td colspan="2" width="100%" valign="top" class="fontOver" align="left" id="Noidung">
                    <%=Noidung%>
                </td>
            </tr>
            <tr>
                <td width="100%" align="left" colspan="2">
                    <div style="position: relative; height: 30px; width: 40%; z-index: 99">
                        <asp:Button ID="btncopycontent2" runat="server" CssClass="iconCopy" Style="width: 80px;
                            height: 25px" Text="Copy" OnClick="btncopycontent_Click"></asp:Button>
                    </div>
                </td>
            </tr>
            <tr>
                <td width="100%" align="right" colspan="2">
                    <input id="buttonExit" onclick="window.close();" type="button" class="iconExit" value="Ðóng" />
                </td>
            </tr>
            <tr>
                <td colspan="2" height="2">
                    <hr />
                </td>
            </tr>
        </table>
    </div>
    <div style="width: 40%; float: right">
        <div style="text-align: left; float: left; width: 100%">
            <asp:LinkButton ID="btn_downloadfile" runat="server" CssClass="iconSend" OnClick="btn_downloadimg_click"
                Text="Download all"></asp:LinkButton>
        </div>
        <div style="text-align: left; float: left; width: 100%">
            <asp:DataList ID="DataListAnh" runat="server" RepeatColumns="4" RepeatDirection="Horizontal"
                DataKeyField="ID" Width="100%" OnEditCommand="DataListAnh_EditCommand">
                <ItemStyle Width="20%" BorderWidth="0" VerticalAlign="top" HorizontalAlign="Left">
                </ItemStyle>
                <ItemTemplate>
                    <div style="width: 90%; float: left; margin-top: 3px; text-align: center">
                        <asp:ImageButton ID="btnUpdate" Width="20px" CausesValidation="false" runat="server"
                            ImageUrl="../Dungchung/Images/dn3.gif" ImageAlign="AbsMiddle" ToolTip="lấy ảnh"
                            CommandName="Edit" CommandArgument="Download" BorderStyle="None"></asp:ImageButton>
                    </div>
                    <div style="width: 90%; float: left;">
                        <img id="imgView" style="cursor: hand" onclick="OpenImage('<%=Global.PathImageFTP%><%# DataBinder.Eval(Container.DataItem, "PathFile")%>')"
                            height="103px" src="<%#HPCComponents.CommonLib.GetPathImgWordPDF( DataBinder.Eval(Container.DataItem, "PathFile")) %>"
                            width="121px" border="1" alt="" />
                        <asp:Label ID="lbFileAttach" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "PathFile") %>'
                            Visible="false">
                        </asp:Label>
                    </div>
                </ItemTemplate>
            </asp:DataList>
        </div>
    </div>
    </form>
</body>
</html>

<script language="javascript" type="text/javascript">
    var FontID = '<%=ConfigurationManager.AppSettings["FontID"].ToString() %>';

    HPC_Convert_Text('txtOriginal', 'Noidung', FontID);
    ConvertBKHCM2_IMGDESC('DataListAnh', FontID);
    function getvalue_exports() {
        var value_txt = getEditorValue_export('Noidung');
        document.getElementById("store_content").value = value_txt;
    }
</script>

