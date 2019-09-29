<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ViewImages.aspx.cs" Inherits="ToasoanTTXVN.UploadFileMulti.ViewImage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <meta name="vs_defaultClientScript" content="JavaScript" />
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5" />
</head>

<script type="text/javascript" language="javascript">
    function DisableRightClick() {
        document.oncontextmenu = function() { return false }
    }
    function close_windown() {
        var tmp_Window = window.close();
    }
</script>

<script language="javascript" type="text/javascript">
    function fitPic() {
        if (window.innerWidth) {
            iWidth = window.innerWidth;
            iHeight = window.innerHeight;
        } else {
            iWidth = document.body.clientWidth;
            iHeight = document.body.clientHeight;
        }
        iWidth = document.images[0].width - iWidth;
        iHeight = document.images[0].height - iHeight;
        window.resizeBy(iWidth, iHeight);
        window.moveTo(window.screen.width / 2 - (document.body.clientWidth / 2), window.screen.height / 2 - (document.body.clientHeight / 2));
    };
</script>

<body style="margin: 0" bgcolor="#e7e8eb" onload="fitPic();">
    <form id="Form1" method="post" runat="server">
    <table cellpadding="0" cellspacing="0" border="0" width="100%">
        <tr>
            <td>
                <a href="javascript:close_windown();">
                    <img src="<%=Imagescr%>" border="1" style="border: 1px solid #a9a9a9" alt="" /></a>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
