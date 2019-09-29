<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="ToasoanTTXVN.Login" %>

<%@ Import Namespace="HPCComponents" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>HỆ THỐNG QUẢN LÝ QUY TRÌNH BIÊN TẬP</title>
    <link rel="stylesheet" href="Dungchung/Style/css/Login/reset.css" />
    <link rel="stylesheet" href="Dungchung/Style/css/Login/animate.css" />
    <link rel="stylesheet" href="Dungchung/Style/css/Login/styles.css" />
</head>
<body>
    <div id="container">
        <form id="Form1" runat="server">
        <h1>
            ĐĂNG NHẬP HỆ THỐNG</h1>
        <label for="Username">
            Tên đăng nhập
        </label>
        <asp:TextBox ID="txtUsername" runat="server" onfocus="if(this.value=='Tên đăng nhập') { this.value=''; }"
            onblur="if (this.value=='') { this.value='Tên đăng nhập'; }"></asp:TextBox>
        <label for="Password">
            Mật khẩu
        </label>
        <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" onfocus="if(this.value=='Mật khẩu') { this.value=''; }"
            onblur="if (this.value=='') { this.value='Mật khẩu'; }"></asp:TextBox>
        <div id="lower">
            <asp:Button ID="btnLogin" runat="server" Text="Đăng nhập" OnClick="btLogon_Click" />
        </div>
        <asp:Panel ID="pnlcapcha" runat="server" Visible="false">
            <label for="Password">
                Mã code:</label>
            <asp:TextBox ID="txtCaptcha" runat="server"></asp:TextBox>
            <div>
                <asp:Image ID="imgCaptcha" runat="server" />
                <asp:ImageButton ID="imbReLoad" runat="server" ImageUrl="~/Dungchung/Images/reload.png"
                    Height="24px" ValidationGroup="Captcha" Width="24px" OnClick="imbReLoad_Click" />
            </div>
        </asp:Panel>
        </form>
    </div>
</body>
</html>
