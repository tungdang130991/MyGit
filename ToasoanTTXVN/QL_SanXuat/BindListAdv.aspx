<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BindListAdv.aspx.cs" Inherits="ToasoanTTXVN.QL_SanXuat.BindListAdv" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">

    <script type="text/javascript">
        $(document).ready(function() {
        $("#DIVListAdvertis :checkbox").click(function() {
                var selectedAd = this.value;
                var e = $(".chkItemsQC");
                for (i = 0; i < e.length; i++) {
                    if (e[i].value != selectedAd)
                        e[i].checked = false;
                }
                var TextDiv = 'divtext' + $('#ctl00_MainContent_LayoutID').val();
                var $loader = $('#' + TextDiv);
                if (this.checked == true) {
                    var id_ = this.value;
                    var Links = 'BindTextAdv.aspx?IdAdv=' + id_ + '&LayoutID=' + $('#ctl00_MainContent_LayoutID').val();
                    $loader.load(Links);
                }
                else {
                    var Links = 'BindTextAdv.aspx';
                    $loader.load(Links);
                }
            });
        });
    </script>

    <div style="width: 100%; float: left" id="DIVListAdvertis">
        <div style="width: 100%; float: left; height: 5px">
        </div>
        <asp:Literal runat="server" ID="ltrListAdv"></asp:Literal>
    </div>
    </form>
</body>
</html>
