<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AccessDenied.aspx.cs" Inherits="ToasoanTTXVN.Errors.AccessDenied" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Thông báo quyền truy nhập</title>
</head>
<body>
    <br /><br /><br /><br /><br /><br /><br /><br /><br /><br />
    <center> 
        <asp:Panel runat="server" ID="Panel1" Width="70%" CssClass="TitlePanel" GroupingText="<b>Thông báo</b>"
            BackColor="white" BorderStyle="NotSet">
            <br />
            <table cellpadding="0" cellspacing="0" border="0" width="100%">
                <tr>
                    <td align=center style="width: 100%; padding: 0 0 0 0;">                   
                        <font size="4">
                            <b><br /><br /><br />Bạn không có quyền truy nhập!&nbsp;<br /><br /><br /><br /></b> 
                        </font>  
                    </td>
                </tr>    
                <tr>
                        <td>
                            <a href="../Login.aspx" style="text-decoration:none; color:Blue"><b>[ Quay lại ]</b></a><br />   
                       </td>
                </tr>                 
            </table>
        </asp:Panel>
    </center>
</body>
</html>
