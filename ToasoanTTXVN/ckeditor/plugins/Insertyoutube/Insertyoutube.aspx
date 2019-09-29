<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Insertyoutube.aspx.cs" Inherits="HPCApplication.ckeditor.plugins.Insertyoutube.Insertyoutube" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Chèn Youtube vào nội dung</title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    
    <style type="text/css">.cssvalueyoutube{height:200px; width:390px; margin-top:5px;}
        .buttonYoutube {
	background:transparent url(../../../DungChung/images/Icons/Them.gif) no-repeat scroll center center;
	border:0px;
	cursor:pointer;
	width:100%;
	height:24px;}
    </style>
    <script type="text/javascript">
        function InsertYoutube() {
            var strText = document.getElementById('idvalueyoutube').value;
            var id = '<%=Request["editorID"] %>';
            window.opener.InsertImage(id, strText);
            window.close();
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <center>
        <div>
            <textarea id="idvalueyoutube" class="cssvalueyoutube" ></textarea>
            <input class="buttonYoutube" type="button" onclick="InsertYoutube();" />
        </div>
    </center>
    </form>
</body>
</html>
