<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ViewCompare.aspx.cs" Inherits="ToasoanTTXVN.DeTai.ViewCompare" %>

<%@ Import Namespace="HPCComponents" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>BÁO ẢNH VIỆT NAM </title>
    <link href="../Dungchung/CSSVIEW/import.css" rel="Stylesheet" rev="Stylesheet" />
    <link href="../Dungchung/CSSVIEW/default.css" rel="Stylesheet" rev="Stylesheet" />
    <style type="text/css">
        body
        {
            font-family: Arial, Helvetica, sans-serif;
            font-size: 13px;
            background-color: white;
        }
        .content, .contentA
        {
            padding: 10px;
            width: 570px;
        }
        .left
        {
            width: 570px;
            float: left;
            padding: 7px 0px 0px 7px;
            min-height: 24px;
        }
        .clear
        {
            float: none;
            clear: both;
            height: 0px;
        }
        .row
        {
            display: block;
            min-height: 32px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div style="float: left; margin-right: 20px; width: 600px;">
        <h4>
            Bài gốc của phóng viên</h4>
        <div class="contentA">
            <div class="row">
                <div class="left">
                    <b>Chuyên mục :</b><i style="font-size: 16px; color: #003399;">
                        <asp:Literal ID="litCatName" runat="server"></asp:Literal></i></div>
                <div class="clear">
                </div>
            </div>
            <div class="row">
                <div class="left">
                    <b>Tên đề tài :</b><i style="font-size: 16px; color: #003399;">
                        <asp:Literal ID="litTittle" runat="server"></asp:Literal></i></div>
                <div class="clear">
                </div>
            </div>
            <div class="row">
                <div class="left">
                    <b>Người viết :</b><i style="font-size: 16px; color: #003399;">
                        <asp:Literal ID="litTacgia" runat="server"></asp:Literal></i></div>
                <div class="clear">
                </div>
            </div>
            <div class="row">
                <div class="left">
                    <ul>
                        <li style="background: transparent url('<%=Global.ApplicationPath %>/Images/icon-print.gif') no-repeat center center;
                            padding-left: 300px;"><a href="javascript:window.print()">Print </a></li>
                    </ul>
                </div>
                <div class="clear">
                </div>
            </div>
            <div class="row">
                <div class="left">
                    <b>Tổng số :</b><i style="font-size: 16px; color: red;">
                        <asp:Literal ID="litCounter" runat="server"></asp:Literal></i></div>
                <div class="clear">
                </div>
            </div>
        </div>
        <hr />
        <div class="contentA">
            <div class="row">
                <div class="left">
                    <asp:Literal ID="litContent" runat="server"></asp:Literal></div>
                <div class="clear">
                </div>
            </div>
        </div>
        <hr />
        <div class="contentA">
            <div class="row">
                <div class="left">
                    <asp:Literal ID="lit_baiviet" runat="server"></asp:Literal></div>
                <div class="clear">
                </div>
            </div>
        </div>
        <hr />
    </div>
    <div style="float: left; margin-right: 20px;">
        <h4>
            Bài đã biên tập</h4>
        <div class="content">
            <div class="row">
                <div class="left">
                    <b>Chuyên mục :</b><i style="font-size: 16px; color: #003399;">
                        <asp:Literal ID="litCM" runat="server"></asp:Literal></i></div>
                <div class="clear">
                </div>
            </div>
            <div class="row">
                <div class="left">
                    <b>Tên đề tài :</b><i style="font-size: 16px; color: #003399;">
                        <asp:Literal ID="litTenDetai" runat="server"></asp:Literal></i></div>
                <div class="clear">
                </div>
            </div>
            <div class="row">
                <div class="left">
                    <b>Biên tập :</b><i style="font-size: 16px; color: #003399;">
                        <asp:Literal ID="literNguoiviet" runat="server"></asp:Literal></i></div>
                <div class="clear">
                </div>
            </div>
            <div class="row">
                <div class="left">
                    <ul>
                        <li style="background: transparent url('<%=Global.ApplicationPath %>/Images/icon-print.gif') no-repeat center center;
                            padding-left: 300px;"><a href="javascript:window.print()">Print </a></li>
                    </ul>
                </div>
                <div class="clear">
                </div>
            </div>
            <div class="row">
                <div class="left">
                    <b>Tổng số :</b><i style="font-size: 16px; color: red;">
                        <asp:Literal ID="litCouter2" runat="server"></asp:Literal></i></div>
                <div class="clear">
                </div>
            </div>
        </div>
        <hr />
        <div class="content">
            <div class="row">
                <div class="left">
                    <asp:Literal ID="litContents" runat="server"></asp:Literal></div>
                <div class="clear">
                </div>
            </div>
        </div>
        <hr />
        <div class="content">
            <div class="row">
                <div class="left">
                    <asp:Literal ID="litbai" runat="server"></asp:Literal></div>
                <div class="clear">
                </div>
            </div>
        </div>
        <hr />
    </div>
    </form>
</body>
</html>
