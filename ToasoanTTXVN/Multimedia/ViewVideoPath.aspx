<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ViewVideoPath.aspx.cs" Inherits="ToasoanTTXVN.Multimedia.ViewVideoPath" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Xem File Quang Cao</title>

    <script type="text/javascript" src='../Dungchung/Scripts/jwplayer/jwplayer.js'></script>

    <script language="javascript" type="text/javascript">        jwplayer.key = "4cFrCsBdTSWp87XH5zQW4VsWi+mFFzQIIqiC4kpnEoU="</script>

</head>
<body>
    <form id="form1" runat="server">
    <center>
        <div>
            <asp:Literal runat="server" ID="litContent"></asp:Literal>
            <div id="checkDisplay" runat="server">
                <embed width="500px" height="auto" type="application/x-shockwave-flash"
                    src="<%=_urlFile%>" style="undefined" id="Advertisement" name="Advertisement"
                    quality="high" wmode="transparent" allowscriptaccess="always" flashvars="clickTARGET=_self&amp;clickTAG=#"></embed>
            </div>
            <div id="checkDisplayVideo" runat="server">
                <div id="MediaPlayer">
                    <div id="liveTV">
                    </div>

                    <script type="text/javascript">
                        jwplayer("liveTV").setup({
                            image: '<%=imgPath()%>',
                            file: '<%=videoPath()%>',
                            width: 600,
                            height: 410,
                            primary: "flash"
                        });
                     
                    </script>

                </div>
            </div>
        </div>
    </center>
    </form>
</body>
</html>
