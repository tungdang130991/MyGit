<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ViewsTulieu.aspx.cs" Inherits="ToasoanTTXVN.Tulieu.ViewsTulieu" %>

<%@ Register TagPrefix="cc1" Namespace="HPCComponents.UI.WebControls" Assembly="HPCComponents" %>
<%@ Import Namespace="HPCComponents" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>TƯ LIỆU TIN</title>
    <link type="text/css" rel="Stylesheet" href="../Dungchung/Style/style.css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table width="100%" cellpadding="2" cellspacing="2" border="0">
            <tr>
                <td width="100%">
                    <table cellspacing="2" cellpadding="2" width="100%" border="0">
                        <tr>
                            <td>
                                <span class="chuyenmuc">Chuyên mục:
                                    <%=Chuyenmuc%></span>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="word-spacing: normal; width: 100%; padding-left: 20px; padding-right: 30px;
                                padding-top: 30px;">
                                <%=Noidung%>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Số từ: <b>
                                    <%=Sotu%></b>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Tác giả: <b>
                                    <%=Tacgia%></b>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 800px; height: 1px" bgcolor="#cccccc">
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" style="text-align: right; float: right">
                                <input id="button1" style="width: 100px;" onclick="window.close();" type="button"
                                    value="[Close]" class="iconExit" name="button1" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
