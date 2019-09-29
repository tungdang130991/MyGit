<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LoadTinbai.aspx.cs"   Inherits="ToasoanTTXVN.QL_SanXuat.LoadTinbai" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <script language="javascript" type="text/javascript">
        //Chon tin bai
        $(document).ready(function() {
            $("#DIVChonbaivietHolderID :checkbox").click(function() {
                var selected = this.value;
                var e = $(".chkItems");
                for (i = 0; i < e.length; i++) {
                    if (e[i].value != selected)
                        e[i].checked = false;
                }
                var TxtID = 'divtext' + $('#ctl00_MainContent_LayoutID').val();
                var $loader = $('#' + TxtID);
                if (this.checked == true) {
                    var id_ = this.value;
                    var Links = 'BindText.aspx?IDlo=' + id_ + '&LayoutID=' + $('#ctl00_MainContent_LayoutID').val();
                    $loader.load(Links);
                }
                else {
                    var Links = 'BindText.aspx';
                    $loader.load(Links);
                }
            });
        });

    </script>
    <div style="width: 100%; float: left; height: 5px">
    </div>
    <asp:Literal runat="server" ID="ltrChonbaiviet"></asp:Literal>
    </form>
</body>
</html>
