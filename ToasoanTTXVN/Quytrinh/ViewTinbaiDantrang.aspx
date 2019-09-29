<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ViewTinbaiDantrang.aspx.cs"
    Inherits="ToasoanTTXVN.Quytrinh.ViewTinbaiDantrang" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<%@ Register TagPrefix="cc1" Namespace="HPCComponents.UI.WebControls" Assembly="HPCComponents" %>
<%@ Import Namespace="HPCComponents" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
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
    <input type="hidden" id="txtOriginal" runat="server" />
    <input id="store_content" type="hidden" runat="server" style="width: 80px; height: 25px" />
    <table cellspacing="2" cellpadding="2" width="100%" border="0" bgcolor="#ffffff">
        <tr>
            <td colspan="2" class="chuyenmuc">
                <%=Chuyenmuc%>
            </td>
        </tr>
        <tr>
            <td colspan="2" class="TieudeBKHCM" id="td_tieude" style="text-align: left; padding-left: 150px">
                <%=Tieude%>
            </td>
        </tr>
        <tr>
            <td width="50%" style="text-align: left">
                <div style="position: relative; height: 30px; width: 40%; z-index: 99">
                    <asp:Button ID="btncopycontent1" runat="server" CssClass="iconCopy" Style="width: 80px;
                        height: 25px" Text="Copy" OnClick="btncopycontent_Click"></asp:Button>
                </div>
            </td>
            <td style="text-align: center; width: 50%">
                <asp:LinkButton ID="btn_downloadfile" runat="server" CssClass="iconSend" OnClick="btn_downloadimg_click"
                    Text="Download all"></asp:LinkButton>
            </td>
        </tr>
        <tr>
            <td width="70%" valign="top" class="fontOver" align="left" id="Noidung">
                <%=Noidung%>
            </td>
            <td valign="top" style="width: 30%">
                <asp:Panel runat="server" ID="plAnh">
                    <table cellspacing="2" cellpadding="2" width="100%" border="0">
                        <tr>
                            <td width="100%">
                                <asp:DataList ID="DataListAnh" runat="server" RepeatColumns="3" RepeatDirection="Horizontal"
                                    Width="100%" OnEditCommand="DataListAnh_EditCommand">
                                    <ItemStyle Width="30%" BorderWidth="0" VerticalAlign="top" HorizontalAlign="Left">
                                    </ItemStyle>
                                    <ItemTemplate>
                                        <div style="width: 90%; float: left; margin-top: 3px; text-align: center">
                                            <asp:ImageButton ID="btnUpdate" Width="20px" CausesValidation="false" runat="server"
                                                ImageUrl="../Dungchung/Images/dn3.gif" ImageAlign="AbsMiddle" ToolTip="lấy ảnh"
                                                CommandName="Edit" CommandArgument="Download" BorderStyle="None"></asp:ImageButton>
                                        </div>
                                        <div style="width: 90%; float: left;">
                                            <img id="imgView" style="cursor: hand" onclick="OpenImage('<%# DataBinder.Eval(Container.DataItem, "Duongdan_Anh")%>')"
                                                height="103px" src="<%= Global.TinPath%>/<%# DataBinder.Eval(Container.DataItem, "Duongdan_Anh") %>"
                                                width="121px" border="1" alt="" />
                                            <asp:Label ID="lbFileAttach" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Duongdan_Anh") %>'
                                                Visible="false">
                                            </asp:Label>
                                        </div>
                                        <div style="width: 90%; float: left" id="ImgDesc1_<%# DataBinder.Eval(Container.DataItem, "Ma_Anh") %>"
                                            class="GhichuBKHCM">
                                            <%# DataBinder.Eval(Container.DataItem, "Chuthich")%>
                                        </div>
                                    </ItemTemplate>
                                </asp:DataList>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td width="40%" align="left">
                <div style="position: relative; height: 30px; width: 40%; z-index: 99">
                    <asp:Button ID="btncopycontent2" runat="server" CssClass="iconCopy" Style="width: 80px;
                        height: 25px" Text="Copy" OnClick="btncopycontent_Click"></asp:Button>
                </div>
            </td>
            <td width="60%" align="right">
                <input id="buttonExit" onclick="window.close();" type="button" class="iconExit" value="Ðóng" />
            </td>
        </tr>
        <tr>
            <td colspan="2" height="2">
                <hr />
            </td>
        </tr>
    </table>
    </form>
</body>
</html>

<script language="javascript" type="text/javascript">
    var FontID = '<%=ConfigurationManager.AppSettings["FontID"].ToString() %>';
    HPC_Convert_Text('txtOriginal', 'td_tieude', FontID);
    HPC_Convert_Text('txtOriginal', 'Noidung', FontID);
    ConvertBKHCM2_IMGDESC('DataListAnh', FontID);
    function getvalue_exports() {
        var value_txt = getEditorValue_export('Noidung');
        document.getElementById("store_content").value = value_txt;
    }
</script>

