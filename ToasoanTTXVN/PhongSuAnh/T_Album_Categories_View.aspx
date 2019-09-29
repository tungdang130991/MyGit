<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="T_Album_Categories_View.aspx.cs"
    Inherits="ToasoanTTXVN.PhongSuAnh.T_Album_Categories_View" %>

<%@ Register TagPrefix="HPC" Namespace="WDF.UI.WebControls" Assembly="WDF.UI.WebControls" %>
<%@ Import Namespace="HPCComponents" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <link href="CSS/jquery.ad-gallery.css" rel="stylesheet" type="text/css" />

    <script src="../Dungchung/Scripts/jquery.1.4.min.js" type="text/javascript"></script>

    <script src="../Dungchung/Scripts/jquery.ad-gallery.js" type="text/javascript"></script>

</head>
<body style="margin:0;padding:0;">
    <form id="form1" runat="server">
    <div>
        <table width="100%">
            <tr>
                <td align="center" valign="middle">
                    <div class="pageImages">
                        <div id="container">
                            <div id="gallery" class="ad-gallery">
                                <div class="ad-image-wrapper">
                                </div>
                                <div class="ad-controls" style="display: none">
                                </div>
                                <div class="ad-nav">
                                    <div class="ad-thumbs">
                                        <ul class="ad-thumb-list">
                                            <asp:Repeater runat="server" ID="rptAlbumActive">
                                                <ItemTemplate>
                                                    <li><a href="<%#GetURLImage(Eval("Abl_Photo_Origin"))%>">
                                                        <img src="<%#GetURLImage(Eval("Abl_Photo_Origin"))%>" title="<%#Eval("Abl_Photo_Desc").ToString()%>"
                                                            longdesc="<%#Eval("Date_Create")!=System.DBNull.Value?Convert.ToDateTime(Eval("Date_Create")).ToString("dd/MM/yyyy HH:mm:ss"):"" %>"
                                                            alt="<%#Eval("Abl_Photo_Desc").ToString()%>" class="image2">
                                                    </a></li>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </ul>
                                    </div>
                                </div>
                            </div>

                            <script type="text/javascript">
                                $(function() {
                                    var galleries = $('.ad-gallery').adGallery();
                                    $('#switch-effect').change(
                                  function() {
                                      galleries[0].settings.effect = $(this).val();
                                      return false;
                                  });
                                });
                            </script>

                        </div>
                    </div>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
