<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Videos_Managerment.aspx.cs"
    Inherits="HPCApplication.UploadVideos.Videos_Managerment" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>UpLoad Video</title>
    <link rel="Stylesheet" type="text/css" href="../Until/scripts/uploadify.css" />

    <script src="../Until/scripts/jquery-1.7.2.min.js" type="text/javascript" />

    <script type="text/javascript" src="../Until/scripts/jquery.uploadify-3.1.min.js"></script>

    <script type="text/javascript" src="../Until/scripts/jquery.uploadify.js"></script>

    <script type="text/javascript">
        function getImgSrc(theImagePath, numberAgr, Size, FileExtension) {
            if (theImagePath != "") {
                window.close();
                opener.getPath(theImagePath, numberAgr, Size, FileExtension);
            }
        }

        function confirm_deleteFile() {
            var stringMess = "";
            stringMess = 'Bạn có thực sự muốn xóa thư các file đã chọn không?';
            if (confirm(stringMess) == true)
                return true;
            else
                return false;
        }
    </script>

    <style type="text/css">
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
            scrollbar-3dlight-color: #cacaca;
            scrollbar-arrow-color: #000000;
            padding-top: 1px;
            scrollbar-track-color: #cacaca;
            font-family: Verdana, Arial, Helvetica, sans-serif;
            scrollbar-darkshadow-color: #cacaca;
            height: 350px;
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
    </style>
</head>
<body>
    <form id="frmUpLoad" method="post" enctype="multipart/form-data" runat="server">
    <table style="height: 100%" cellspacing="1" cellpadding="1" border="0" width="100%">
        <tr>
            <td style="width: 100%" valign="top">
                <fieldset style="padding-right: 5px; padding-left: 5px; padding-bottom: 5px; width: 100%;
                    padding-top: 5px; text-align: left">
                    <legend style="font-weight: bold; font-size: 10px; width: 120px; font-family: Arial;
                        height: 16px" name="lgd1">Thông tin tìm kiếm</legend>
                    <table cellpadding="1" cellspacing="1" border="0" width="100%">
                        <tr>
                            <td>
                                <input type="file" name="fileuploadmedia" id="fileuploadmedia" />
                            </td>
                            <td align="right">
                                <!--<table border="0" cellpadding="2" cellspacing="2">
                                    <tr>
                                        <td class="Time">
                                            Ngày
                                        </td>
                                        <td class="Time">
                                            <asp:DropDownList CssClass="inputtext" runat="server" ID="combo_Ngay">
                                            </asp:DropDownList>
                                        </td>
                                        <td class="Time">
                                            Tháng
                                        </td>
                                        <td class="Time">
                                            <asp:DropDownList CssClass="inputtext" runat="server" ID="cbo_Thang">
                                            </asp:DropDownList>
                                        </td>
                                        <td class="Time">
                                            Năm
                                        </td>
                                        <td class="Time">
                                            <asp:DropDownList CssClass="inputtext" runat="server" ID="cbo_Nam">
                                            </asp:DropDownList>
                                        </td>
                                        <td class="Time">
                                            <asp:Button ID="btnSearchFolder" runat="server" Text="Tìm kiếm" CssClass="button">
                                            </asp:Button>
                                        </td>
                                    </tr>
                                </table>-->
                            </td>
                        </tr>
                        <tr>
                            <td colspan="10" width="100%">
                                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                    <tr>
                                        <td class="currentFolder" style="height: 25px" width="100%">
                                            <table border="0" cellpadding="1" cellspacing="1">
                                                <tr>
                                                    <td>
                                                        <img alt="" src="../DungChung/images/folder.png" align="left" />
                                                    </td>
                                                    <td class="currentFolderText">
                                                        <asp:Label ID="lblFolder2Upload" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="currentFolderContent">
                                            <div id="displayContainer">
                                                <asp:DataList ID="dlFolder" DataKeyField="FilePath" runat="server" RepeatColumns="4"
                                                    RepeatDirection="Horizontal" RepeatLayout="Table" Width="100%" ItemStyle-HorizontalAlign="Center"
                                                    ItemStyle-BorderWidth="1" ItemStyle-BackColor="#ffffff" ItemStyle-BorderStyle="Inset"
                                                    ItemStyle-VerticalAlign="Middle" CellSpacing="2" CellPadding="2">
                                                    <ItemTemplate>
                                                        <table border="0" cellspacing="1" cellpadding="1">
                                                            <tr>
                                                                <td align="center">
                                                                    <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/images/folder_closed.png"
                                                                        ImageAlign="AbsMiddle" CommandName="Edit"></asp:ImageButton>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="Time" align="center">
                                                                    <a href="">
                                                                        <%#DataBinder.Eval(Container.DataItem,"FileName")%>
                                                                    </a>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </ItemTemplate>
                                                </asp:DataList>
                                                <asp:DataList ID="dlImages" DataKeyField="ID" runat="server" RepeatColumns="4"
                                                    RepeatDirection="Horizontal" RepeatLayout="Table" Width="100%" ItemStyle-HorizontalAlign="Center"
                                                    ItemStyle-BorderWidth="0" ItemStyle-BackColor="#ffffff" ItemStyle-BorderStyle="Inset"
                                                    ItemStyle-VerticalAlign="Middle" CellSpacing="2" CellPadding="2">
                                                    <ItemTemplate>
                                                        <table border="0" cellspacing="1" cellpadding="1" width="100%" style="background-color: #f1f1f1">
                                                            <tr>
                                                                <td style="text-align: center; height: 100px; width: 100px; vertical-align: middle">
                                                                    <img alt="<%#Cut_Filename(DataBinder.Eval(Container.DataItem,"ImageFileName"))%>"
                                                                        src="<%#GetFileURL(DataBinder.Eval(Container.DataItem,"ImgeFilePath"))%>" border="1"
                                                                        width="80px" height="60px" style="cursor: pointer;" title="<%#DataBinder.Eval(Container.DataItem,"ImageFileName")%>"
                                                                        onclick="getImgSrc('<%#UrlPathImage_RemoveUpload(DataBinder.Eval(Container.DataItem,"ImgeFilePathOrizin"))%>','<%=strNumberArg%>','<%#DataBinder.Eval(Container.DataItem,"ImageFileSize")%>','<%#DataBinder.Eval(Container.DataItem,"ImageFileExtension")%>')">
                                                                    <br />
                                                                    <asp:Label ID="lbl_URL" Visible="false" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"ImgeFilePath")%>'></asp:Label>
                                                                    <asp:Label ID="lbl_ID" Visible="false" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"ID")%>'></asp:Label>
                                                                    <asp:Label ID="lblFilename" Width="80px" runat="server" Text='<%#Cut_Filename(DataBinder.Eval(Container.DataItem,"ImageFileName"))%>'></asp:Label>
                                                                    <br />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </ItemTemplate>
                                                </asp:DataList>
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="10" width="100%">
                                <!--<fieldset style="padding-right: 5px; padding-left: 5px; padding-bottom: 5px; width: 100%;
                                    padding-top: 5px; height: 100%" align="left">-->
                                <table cellpadding="1" cellspacing="1" border="0" width="100%">
                                    <tr>
                                        <td class="Time">
                                            <asp:Label ID="lblResult" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <!--<input class="inputtext" id="txtFile" style="width: 80%; height: 22px" type="file"
                                                    size="112" name="txtFile" runat="server" />-->
                                            <!--<asp:Button CssClass="inputtext" ID="btnUpload" Text="Upload" runat="server" Width="67"
                                                    Height="24"></asp:Button>-->
                                            <input class="inputtext" id="btnExit" style="width: 67px; height: 30px" type="button"
                                                value="Đóng" name="btnExit" onclick="javascript:window.close();" />
                                        </td>
                                    </tr>
                                </table>
                                <!--</fieldset>-->
                            </td>
                        </tr>
                    </table>

                    <script type="text/javascript">
                        $(window).load(
                            function() {
                                $("#fileuploadmedia").uploadify({
                                    'swf': '../Until/scripts/uploadify.swf',
                                    'buttonText': 'Chọn file',
                                    'uploader': 'uploadmedia.ashx?user=<%=GetUserName()%>',
                                    'folder': 'Upload',
                                    'fileDesc': 'Images Files',
                                    'fileExt': '*.mp4;*.flv;*.mp3;*.jpg;*.bmp;*.gif',
                                    'multi': true,
                                    'auto': true,
                                    'onQueueComplete': function(queueData) {
                                        var url = 'Videos_Managerment.aspx?vType=<%=Request["vType"]%>&vKey=<%=Request["vKey"]%>';
                                        window.location = url;
                                    }
                                });
                            }
                        );

                        function doPostBackAsync(eventTarget, eventArgs) {
                            var pageReqMgr = Sys.WebForms.PageRequestManager.getInstance();
                            if (!Array.contains(pageReqMgr._asyncPostBackControlIDs, eventTarget)) {
                                pageReqMgr._asyncPostBackControlIDs.push(eventTarget);
                            }
                            if (!Array.contains(pageReqMgr._asyncPostBackControlClientIDs, eventTarget)) {
                                pageReqMgr._asyncPostBackControlClientIDs.push(eventTarget);
                            }
                            __doPostBack(eventTarget, eventArgs);
                        }
                    </script>

                </fieldset>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
