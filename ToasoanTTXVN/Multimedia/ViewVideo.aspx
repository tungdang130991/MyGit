<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ViewVideo.aspx.cs" Inherits="ToasoanTTXVN.Multimedia.ViewVideo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Xem Video</title>

    <script type="text/javascript" src="../Dungchung/Scripts/jwplayer/jwplayer.js"></script>

    <script language="javascript" type="text/javascript">        jwplayer.key = "4cFrCsBdTSWp87XH5zQW4VsWi+mFFzQIIqiC4kpnEoU="</script>

    <script language="Javascript" type="text/javascript">
        function clearBuffer() {
            sound.stop();
            delete sound;
            stream();
        } 
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <div id="MediaPlayer">
        <div id="liveTV">
        </div>

        <script type="text/javascript">
            jwplayer("liveTV").setup({
                image: '<%=videoImage()%>',
                file: '<%=videoPath()%>',
                width: 560,
                height: 370,
                primary: "flash"
            });
                     
        </script>
    </div>
    </form>
</body>
</html>
