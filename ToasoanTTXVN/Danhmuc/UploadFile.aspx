<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UploadFile.aspx.cs" Inherits="ToasoanTTXVN.Danhmuc.UploadFile" %>

<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral,PublicKeyToken=31BF3856AD364E35" %>
<%@ Import Namespace="HPCComponents" %>
<%@ Register TagPrefix="nbc" Namespace="HPCComponents.UI" Assembly="HPCComponents" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Quản lý upload file</title>
    <link type="text/css" rel="Stylesheet" href="../Dungchung/Style/style.css" />
    <link rel="Stylesheet" type="text/css" href="../Dungchung/Scripts/jsUpload/CSS/Uploadicon.css" />

    <script type="text/javascript" src="../Dungchung/Scripts/jsUpload/scripts/jquery-1.3.2.min.js"></script>

    <script type="text/javascript" src="../Dungchung/Scripts/jsUpload/scripts/jquery.uploadify.js"></script>

    <script type="text/javascript">
        function getImgSrc(theImagePath) {
            if (theImagePath != "") {
                window.close();
                opener.getPath(theImagePath);
            }
        }        
    </script>

    <style>
        BODY
        {
            border-right: 0px;
            border-top: 0px;
            margin: 0px;
            overflow: hidden;
            border-left: 0px;
            border-bottom: 0px;
            background-color: buttonface;
        }
        .button
        {
            font-size: 12px;
            color: #000099;
            font-family: 'Arial';
        }
        .inputtext
        {
            border-right: #cccccc 1px solid;
            border-top: #cccccc 1px solid;
            font-weight: normal;
            font-size: 12px;
            border-left: #cccccc 1px solid;
            cursor: hand;
            color: #000000;
            border-bottom: #cccccc 1px solid;
            font-family: Arial, Helvetica, sans-serif;
        }
        .Time
        {
            font-weight: normal;
            font-size: 11px;
            color: #000000;
            font-family: Arial, Helvetica, sans-serif;
        }
        #displayContainer
        {
            padding-right: 1px;
            padding-left: 1px;
            scrollbar-face-color: #cacaca;
            font-size: 10pt;
            padding-bottom: 1px;
            margin: 0px;
            scrollbar-highlight-color: #cacaca;
            overflow: auto;
            width: 100%;
            scrollbar-shadow-color: #cacaca;
            color: #000000;
            padding-top: 0px;
            font-family: Verdana, Arial, Helvetica, sans-serif;
            height: 300px;
        }
        #displayUoload
        {
            padding-right: 1px;
            padding-left: 1px;
            scrollbar-face-color: #cacaca;
            font-size: 10pt;
            padding-bottom: 1px;
            margin: 0px;
            scrollbar-highlight-color: #cacaca;
            overflow: auto;
            width: 100%;
            scrollbar-shadow-color: #cacaca;
            color: #000000;
            padding-top: 0px;
            font-family: Verdana, Arial, Helvetica, sans-serif;
            height: 150px;
        }
        .pageNav
        {
            font-weight: normal;
            font-size: 12px;
            color: #000099;
            font-family: 'Tahoma';
        }
        TD.currentFolder
        {
            border-right: #cccccc 1px solid;
            border-top: #cccccc 1px solid;
            font-weight: bold;
            font-size: 12px;
            border-left: #cccccc 1px solid;
            color: #000099;
            border-bottom: #cccccc 1px solid;
            font-family: Arial, Helvetica, sans-serif;
            background-color: #f1f1f1;
        }
        .currentFolderText
        {
            font-weight: bold;
            font-size: 12px;
            color: #000099;
            font-family: Arial, Helvetica, sans-serif;
            background-color: #f1f1f1;
        }
        TD.currentFolderContent
        {
            border-right: #cccccc 2px solid;
            border-top: #cccccc 2px solid;
            font-weight: bold;
            font-size: 12px;
            border-left: #cccccc 2px solid;
            color: #000099;
            border-bottom: #cccccc 2px solid;
            font-family: Arial, Helvetica, sans-serif;
            background-color: #ffffff;
        }
        fieldset
        {
            -moz-border-radius: 4px;
            border-radius: 4px;
            -webkit-border-radius: 4px;
            border: solid 1px #d4d4d4;
            padding: 2px;
        }
        legend
        {
            color: black;
            font-size: 100%;
            width: 150px;
        }
    </style>
</head>
<body style="margin-top: 5px; margin-left: 5px; margin-right: 5px; margin-bottom: 5px">
    <form id="frmUpLoad" method="post" enctype="multipart/form-data" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <table border="0" cellpadding="0" width="100%" cellspacing="0">
        <tr>
            <td class="datagrid_top_left">
            </td>
            <td class="datagrid_top_center">
            </td>
            <td class="datagrid_top_right">
            </td>
        </tr>
        <tr>
            <td class="datagrid_content_left">
            </td>
            <td style="text-align: center">
                <table cellpadding="1" cellspacing="1" border="0" width="100%">
                    <tr>
                        <td style="text-align: left; height: 25px; padding-left: 4px" bgcolor="#cccccc" class="TitlePanel">
                            + DANH SÁCH CÁC FILE ĐƯỢC CẬP NHẬT
                        </td>
                    </tr>
                    <tr>
                        <td style="vertical-align: top;">
                            <div id="displayContainer">
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                    <ContentTemplate>
                                        <asp:DataList ID="dlImages" runat="server" RepeatColumns="5" RepeatDirection="Horizontal"
                                            RepeatLayout="Table" Width="100%" ItemStyle-HorizontalAlign="Center" ItemStyle-BorderWidth="0"
                                            ItemStyle-BackColor="#ffffff" ItemStyle-BorderStyle="Inset" ItemStyle-Width="20%"
                                            ItemStyle-VerticalAlign="Middle" CellSpacing="2" CellPadding="2">
                                            <ItemTemplate>
                                                <table border="0" cellspacing="1" cellpadding="1" width="100%" style="background-color: #f1f1f1">
                                                    <tr>
                                                        <td style="text-align: center; vertical-align: middle; padding: 5px 5px 5px 5px">
                                                            <img alt="" src='<%#DataBinder.Eval(Container.DataItem,"PhotoPath")%>' id="img1"
                                                                width="100" onclick="getImgSrc('<%#DataBinder.Eval(Container.DataItem,"LinkPhotoPath")%>')">
                                                        </td>
                                                    </tr>
                                                </table>
                                            </ItemTemplate>
                                        </asp:DataList>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left; height: 5px; padding-left: 4px" bgcolor="#cccccc">
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left; vertical-align: top; height: 150px;">
                            <div id="displayUoload">
                                <asp:FileUpload ID="FileUpload1" runat="server" />
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
    </form>
</body>
</html>

<script type="text/javascript">
    $(window).load(
    function() {
        $("#<%=FileUpload1.ClientID %>").fileUpload({
            'uploader': '../Dungchung/Scripts/jsUpload/scripts/uploader.swf',
            'cancelImg': '../Dungchung/Scripts/jsUpload/images/cancel.png',
            'buttonText': 'Browse Files',
            'script': 'UploadFile.ashx',
            'folder': 'Anh_AnPham',
            'fileDesc': 'All Files',
            'fileExt': '*.jpg;*.png;*.gif;*.jpeg;',
            'multi': true,
            'auto': true,
            'onComplete': function() {
                var url = '<%=Global.ApplicationPath %>/Danhmuc/UploadFile.aspx';
                window.location = url;
            }
        });
    }
);

</script>

